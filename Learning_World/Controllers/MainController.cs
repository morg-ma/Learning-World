using Learning_World.Data;
using Microsoft.AspNetCore.Mvc;

namespace Learning_World.Controllers
{
    public class MainController : Controller
    {
        private readonly ElearningPlatformContext _context;

        public MainController(ElearningPlatformContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var topCourses = _context.Courses.OrderByDescending(c => c.MaxEnrollment).Take(3).ToList();
            return View(topCourses);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
