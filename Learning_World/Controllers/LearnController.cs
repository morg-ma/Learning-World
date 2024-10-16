using System.Linq;
using System.Reflection;
using Learning_World.Data;
using Learning_World.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace Learning_World.Controllers
{
    public class LearnController : Controller
    {
        ElearningPlatformContext _db;
        public LearnController(ElearningPlatformContext _DbContext)
        {
            _db = _DbContext;
        }
        public IActionResult Home(int id, int moduleNumber = 1)
        {
            var course = _db.Courses.FirstOrDefault(e => e.CourseId == id);
            ViewBag.CourseTilte = course.Title;
            ViewBag.moduleNumber = moduleNumber;
            var modules = _db.Modules.Where(e => e.CourseId == id).ToList();
            return View(modules);
        }


        public IActionResult PartsPartialView(int id)
        {
            var partsWithLessons = _db.Parts.Include(e => e.Lessons).ThenInclude(e => e.LessonType).Where(e => e.ModuleId == id).ToList();
            return PartialView(partsWithLessons);

        }

        public IActionResult LessonsPartialView(int id)
        {
            var partsWithLessons = _db.Parts.Include(e => e.Lessons).ThenInclude(e => e.LessonType).Where(e => e.ModuleId == id).ToList();
            return View(partsWithLessons);


        }

        public IActionResult LessonDiaplayPartialView(int id, string type)
        {
            switch (type)
            {
                case "Video":
                    var lesson1 = _db.Lessons.Include(e => e.LessonVideo).FirstOrDefault(e => e.LessonId == id);
                    return PartialView("LessonVideoDisplay", lesson1);
                case "Text":
                    var lesson2 = _db.Lessons.Include(e => e.LessonText).FirstOrDefault(e => e.LessonId == id);
                    return PartialView("LessonTextDisplay", lesson2);
                case "Quiz":
                    //var lesson3 = _db.Lessons.Include(e => e.LessonQuiz).FirstOrDefault(e => e.LessonId == id);
                    return RedirectToAction("GetQuiz", new { lessonId = id });
                default:
                    var lesson4 = _db.Lessons.Include(e => e.LessonVideo).FirstOrDefault(e => e.LessonId == id);
                    return View("LessonVideoDisplay", lesson4);
            }
        }

    
        public IActionResult GetQuiz(int lessonId)
        {
            var lesson = _db.Lessons.Include(e => e.LessonQuiz)
                .ThenInclude(e => e.QuizQuestions).
                ThenInclude(e => e.QuizAnswers).FirstOrDefault(l => l.LessonId == lessonId);
            return PartialView("LessonQuizDisplay", lesson);
        }



    }
}
