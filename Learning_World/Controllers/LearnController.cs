using System.Linq;
using System.Reflection;
using Learning_World.Data;
using Learning_World.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using NuGet.Versioning;

namespace Learning_World.Controllers
{
    public class LearnController : Controller
    {
        ElearningPlatformContext _db;
        public LearnController(ElearningPlatformContext _DbContext)
        {
            _db = _DbContext;
        }
        public IActionResult Index(int id, int moduleId = 1)
        {
            var course = _db.Courses.FirstOrDefault(e => e.CourseId == id);
            ViewBag.CourseTilte = course.Title;
            var modules = _db.Modules.Where(e => e.CourseId == id).ToList();
            ViewBag.Modules = modules;
            ViewBag.SelectedModuleId = moduleId;
            return View(modules);
        }


        public IActionResult PartsPartialView(int id)
        {
            var partsWithLessons = _db.Parts.Include(e => e.Lessons).ThenInclude(e => e.LessonType).Where(e => e.ModuleId == id).ToList();
            return PartialView(partsWithLessons);

        }
		[Route("Learn/lesson/{moduleId?}/{lessonType}/{lessonId}")]

		public IActionResult LessonsPartialView(int moduleId, int lessonId)
        {
            var partsWithLessons = _db.Parts.Include(e => e.Lessons).ThenInclude(e => e.LessonType).Where(e => e.ModuleId == moduleId).ToList();
			int cid = (int)_db.Modules.Include(e => e.Course).FirstOrDefault(e => e.ModuleId == moduleId).CourseId;
			ViewBag.C_Id = cid;
            ViewBag.ModuleName= partsWithLessons[0].Module.Title;
			return View(partsWithLessons);
        }



        [Route("Learn/LessonDisplayPartialView/{moduleId?}/{lessonType}/{lessonId}")]
        public IActionResult LessonDisplayPartialView(int lessonId, string lessonType)
        {
            switch (lessonType)
            {
                case "Video":
                    var lesson1 = _db.Lessons.Include(e => e.LessonVideo).FirstOrDefault(e => e.LessonId == lessonId);
                    return PartialView("LessonVideoDisplay", lesson1);

                case "Text":
                    var lesson2 = _db.Lessons.Include(e => e.LessonText).FirstOrDefault(e => e.LessonId == lessonId);
                    return PartialView("LessonTextDisplay", lesson2);

                case "Quiz":
                    return RedirectToAction("GetQuiz", new { lessonId = lessonId });

                default:
                    var lesson4 = _db.Lessons.Include(e => e.LessonVideo).FirstOrDefault(e => e.LessonId == lessonId);
                    return PartialView("LessonVideoDisplay", lesson4);
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
