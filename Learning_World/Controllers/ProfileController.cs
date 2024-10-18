using Learning_World.Data;
using Learning_World.Models;
using Learning_World.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Edit(int Id)
        {
            User user = _context.Users.FirstOrDefault(e=>e.UserId == Id);
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
        public IActionResult SaveEdit(int id, UserProfileViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit");
            }

            User UserFromDataBase = _context.Users.FirstOrDefault(e => e.UserId == id);
            if (UserFromDataBase == null)
            {
                return NotFound();
            }

            // Handle image upload
            if (user.ImageFile != null)
            {
                // Ensure wwwroot/images directory exists
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images");

                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                // Save the image with a unique name to avoid conflicts
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(user.ImageFile.FileName);
                var filePath = Path.Combine(imagePath, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    user.ImageFile.CopyTo(fileStream);
                }

                // Save only the file name (e.g., "unique_filename.jpg") in the database
                UserFromDataBase.Image = uniqueFileName;
            }

            // Update other user details
            UserFromDataBase.Name = user.Name;
            UserFromDataBase.Email = user.Email;

            _context.Users.Update(UserFromDataBase);
            _context.SaveChanges();

            return RedirectToAction("View", new { UserId = UserFromDataBase.UserId });
        }

    }
}
