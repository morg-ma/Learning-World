using Learning_World.Data;
using Learning_World.Models;
using Learning_World.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Security.Claims;

namespace Learning_World.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ElearningPlatformContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> signInManager;

        public ProfileController(IWebHostEnvironment hostingEnvironment, ElearningPlatformContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _userManager = userManager;
            this.signInManager = signInManager;
        }
        [Route("Profile/View")] // Explicitly defining the route without id
        public async Task<IActionResult> View()
        {
            // Retrieve the UserId from cookies
            var userIdCookie = Request.Cookies["UserId"];

            if (string.IsNullOrEmpty(userIdCookie) || !int.TryParse(userIdCookie, out int userId))
            {
                return BadRequest("Invalid or missing UserId in cookies.");
            }

            // Retrieve the user from UserManager
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return View("NotFound404");
            }

            // Retrieve roles and user details as before
            var roles = await _userManager.GetRolesAsync(user);
            var dbUser = _context.Users
                .Include(u => u.Courses)
                .Include(u => u.Certificates)
                    .ThenInclude(c => c.Course)
                .FirstOrDefault(u => u.Id == userId);

            if (dbUser == null)
            {
                return View("NotFound404");
            }

            var viewModel = new UserProfileViewModel
            {
                Id = dbUser.Id,
                Name = dbUser.UserName,
                Email = dbUser.Email,
                Image = dbUser.Image,
                Roles = roles.ToList(),
                Certificates = dbUser.Certificates.Select(c => new CertificateViewModel
                {
                    CourseName = c.Course.Title,
                    IssueDate = c.IssueDate
                }).ToList()
            };

            // Return the View with the view model
            return View(viewModel);
        }


        public async Task<IActionResult> Edit()
        {
            // Retrieve the UserId from cookies
            var userIdCookie = Request.Cookies["UserId"];

            if (string.IsNullOrEmpty(userIdCookie) || !int.TryParse(userIdCookie, out int userId))
            {
                return BadRequest("Invalid or missing UserId in cookies.");
            }

            // Retrieve the user based on the UserId from cookies
            var user = await _context.Users
                .Include(u => u.Certificates)
                    .ThenInclude(c => c.Course)
                .FirstOrDefaultAsync(e => e.Id == userId);

            if (user == null)
            {
                return View("NotFound404");
            }

            var viewModel = new UserProfileViewModel
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                Image = user.Image,
                Certificates = user.Certificates.Select(c => new CertificateViewModel
                {
                    CourseName = c.Course.Title,
                    IssueDate = c.IssueDate
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEdit(UserProfileViewModel model)
        {
            // Retrieve the UserId from cookies
            var userIdCookie = Request.Cookies["UserId"];

            if (string.IsNullOrEmpty(userIdCookie) || !int.TryParse(userIdCookie, out int userId))
            {
                return BadRequest("Invalid or missing UserId in cookies.");
            }

            if (!ModelState.IsValid)
            {
                return View("Edit", model); // Return the Edit view if model state is invalid
            }

            var user = await _context.Users.FindAsync(userId); // Retrieve user based on UserId from cookies
            if (user == null)
            {
                return View("NotFound404");
            }

            // Handle image upload
            if (model.ImageFile != null)
            {
                var imagePath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "images");
                Directory.CreateDirectory(imagePath); // Ensure the directory exists

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + System.IO.Path.GetFileName(model.ImageFile.FileName);
                var filePath = System.IO.Path.Combine(imagePath, uniqueFileName);

                using (var image = Image.Load(model.ImageFile.OpenReadStream()))
                {
                    image.Mutate(x => x
                        .Resize(new ResizeOptions
                        {
                            Size = new Size(230, 230),  // Resize to 230x230 pixels
                            Mode = ResizeMode.Crop      // Crop to ensure the image fits exactly
                        })
                        .ApplyRoundedCorners(115)       // Apply circular crop with a radius of 115 (half of 230)
                    );

                    await image.SaveAsync(filePath, new PngEncoder());
                }
                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(user.Image))
                {
                    var oldImagePath = System.IO.Path.Combine(imagePath, user.Image);
                    if (System.IO.File.Exists(oldImagePath) && user.Image != "default.png")
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                user.Image = uniqueFileName; // Update the user's image
            }

            // Update other user details
            user.UserName = model.Name;
            user.Email = model.Email;
            await UpdateUserClaims(user, false);
            try
            {
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userId))
                {
                    return View("NotFound404");
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Login", "Account"); // Redirect to the View action without ID in the route
        }

        // Helper method to update the user claims
        private async Task UpdateUserClaims(User user, bool remember)
        {
            // Remove the existing identity
            await signInManager.SignOutAsync();

            // Create a new identity with updated claims
            var claims = new List<Claim>
    {
        new Claim("Image", user.Image) // Add the image claim
    };
            // Re-sign in the user with the updated identity
            await signInManager.SignInWithClaimsAsync(user, isPersistent: remember, claims);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
    public static class ImageProcessingExtensions
    {
        public static IImageProcessingContext ApplyRoundedCorners(this IImageProcessingContext ctx, float cornerRadius)
        {
            Size size = ctx.GetCurrentSize();
            IPathCollection corners = BuildCorners(size.Width, size.Height, cornerRadius);
            ctx.SetGraphicsOptions(new GraphicsOptions()
            {
                Antialias = true,
                AlphaCompositionMode = PixelAlphaCompositionMode.DestOut
            });
            foreach (var c in corners)
            {
                ctx = ctx.Fill(Color.Red, c);
            }
            return ctx;
        }
        private static IPathCollection BuildCorners(int imageWidth, int imageHeight, float cornerRadius)
        {
            // Create a square
            var rect = new RectangularPolygon(-0.5f, -0.5f, cornerRadius, cornerRadius);
            // then cut out of the square a circle so we are left with a corner
            IPath cornerTopLeft = rect.Clip(new EllipsePolygon(cornerRadius - 0.5f, cornerRadius - 0.5f, cornerRadius));
            // corner is now a corner shape positions top left
            var cornerTopRight = cornerTopLeft.RotateDegree(90).Translate(imageWidth - cornerRadius, 0);
            var cornerBottomLeft = cornerTopLeft.RotateDegree(-90).Translate(0, imageHeight - cornerRadius);
            var cornerBottomRight = cornerTopLeft.RotateDegree(180).Translate(imageWidth - cornerRadius, imageHeight - cornerRadius);

            return new PathCollection(cornerTopLeft, cornerTopRight, cornerBottomLeft, cornerBottomRight);
        }
    }
}