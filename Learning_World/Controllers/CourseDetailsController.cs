using Learning_World.Data;
using Learning_World.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learning_World.Controllers
{
    public class CourseDetailsController : Controller
    {
        private readonly ElearningPlatformContext _context;
        public CourseDetailsController(ElearningPlatformContext elearningPlatform)
        {
            _context = elearningPlatform;
        }
        public IActionResult ShowCourse(int id = 1)
        {
            Course course = _context.Courses.Include(e => e.Users).FirstOrDefault(e => e.CourseId == id);

            if (course == null)
            {
                return NotFound();
            }

            ViewBag.modules = _context.Modules.Where(e => e.CourseId == id).ToList();
            ViewBag.parts = _context.Parts.ToList();
            ViewBag.lessons = _context.Lessons.ToList();
            return View(course);
        }

    }
}
