using Learning_World.Data;
using Learning_World.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Learning_World.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ElearningPlatformContext _context = new ElearningPlatformContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
