using Learning_World.Data;
using Learning_World.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Learning_World.Controllers
{
    public class MainController : Controller
    {
        private readonly CoursesRepository _coursesRepository;

        public MainController(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }
        public IActionResult Index()
        {
            var topCourses = _coursesRepository.GetMostPopularCourses(3);
            return View(topCourses);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
