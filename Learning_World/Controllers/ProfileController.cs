using Learning_World.Data;
using Learning_World.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Learning_World.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ElearningPlatformContext _context;

        public ProfileController(IWebHostEnvironment hostingEnvironment, ElearningPlatformContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult View(int UserId = 1)
        {
            var user = _context.Users
                .Include(u => u.Roles)
                .Include(u => u.Courses)
                .Include(u => u.Certificates)
                    .ThenInclude(c => c.Course)
                .FirstOrDefault(u => u.UserId == UserId);

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new UserProfileViewModel
            {
                Id = UserId,
                Name = user.Name,
                Email = user.Email,
                Image = user.Image,
                RegistrationDate = user.RegistrationDate,
                Roles = user.Roles.Select(r => r.RoleName).ToList(),
                Certificates = user.Certificates.Select(c => new CertificateViewModel
                {
                    CourseName = c.Course.Title,
                    IssueDate = c.IssueDate
                }).ToList()
            };

            return View(viewModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .Include(u => u.Certificates)
                    .ThenInclude(c => c.Course)
                .FirstOrDefaultAsync(e => e.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new UserProfileViewModel
            {
                Id = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Image = user.Image,
                RegistrationDate = user.RegistrationDate,
                Roles = user.Roles.Select(r => r.RoleName).ToList(),
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
        public async Task<IActionResult> SaveEdit(int id, UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Handle image upload
            if (model.ImageFile != null)
            {
                var imagePath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "images");
                Directory.CreateDirectory(imagePath); // Ensure directory exists

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
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                user.Image = uniqueFileName;
            }
            // Update other user details
            user.Name = model.Name;
            user.Email = model.Email;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("View", new { UserId = user.UserId });
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
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