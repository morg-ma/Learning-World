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
            Dictionary<int, List<int>> keyValuePairs = new Dictionary<int, List<int>>();
            foreach (var module in modules)
            {
                keyValuePairs.Add(module.ModuleId,
                    new List<int>() { _db.Lessons.Include(e => e.Part).Where(e => e.Part.ModuleId == moduleId).Count(),
                    _db.LessonCompletions.Where(e=>e.ModuleId == module.ModuleId).Count() });
            }
            ViewBag.KeyValuePairs = keyValuePairs;
            return View(modules);

        }


        public IActionResult PartsPartialView(int id)
        {
            var partsWithLessons = _db.Parts.Include(e => e.Lessons).ThenInclude(e => e.LessonType).Where(e => e.ModuleId == id).ToList();
            return PartialView(partsWithLessons);

        }
        [Route("Learn/lesson/{moduleId?}/{lessonType}/{lessonId}")]

        public IActionResult Lessons(int moduleId, int lessonId)
        {
            var partsWithLessons = _db.Parts.Include(e => e.Lessons).ThenInclude(e => e.LessonType).Where(e => e.ModuleId == moduleId).ToList();
            int cid = (int)_db.Modules.Include(e => e.Course).FirstOrDefault(e => e.ModuleId == moduleId).CourseId;
            ViewBag.C_Id = cid;
            ViewBag.isCompleted = _db.LessonCompletions.Where(e => e.ModuleId == moduleId).ToList();
            ViewBag.ModuleName = partsWithLessons[0].Module.Title;
            return View(partsWithLessons);
        }



        [Route("Learn/LessonDisplayPartialView/{moduleId?}/{lessonType}/{lessonId}")]
        public IActionResult LessonDisplayPartialView(int lessonId, string lessonType, int moduleId)
        {
            ViewBag.isCompleted = _db.LessonCompletions.Where(e => e.ModuleId == moduleId).ToList()?? null;
            ViewBag.cousreId = _db.Modules.FirstOrDefault(e => e.ModuleId == moduleId).CourseId;
            switch (lessonType)
            {
                case "Video":
                    var lesson1 = _db.Lessons.Include(e => e.LessonVideo).Include(e => e.Part).FirstOrDefault(e => e.LessonId == lessonId);
                    return PartialView("LessonVideoDisplay", lesson1);

                case "Text":
                    var lesson2 = _db.Lessons.Include(e => e.LessonText).Include(e => e.Part).FirstOrDefault(e => e.LessonId == lessonId);
                    return PartialView("LessonTextDisplay", lesson2);

                case "Quiz":
                    return RedirectToAction("GetQuiz", new { lessonId = lessonId });

                default:
                    return NotFound();
            }

        }

        [HttpPost]
        [Route("Learn/CompleteLesson/{ModuleId}/{LessonId}/{UserId}")]
        public void CompleteLesson(int lessonId, int UserId, int ModuleId)
        {
            _db.LessonCompletions.Add(new LessonCompletion() { LessonID = lessonId, UserId = UserId, ModuleId = ModuleId });
            _db.SaveChanges();
        }


        [HttpGet]
        public IActionResult GetQuiz(int lessonId)
        {
            var lesson = _db.Lessons.Include(e => e.LessonQuiz)
                .ThenInclude(e => e.QuizQuestions).
                ThenInclude(e => e.QuizAnswers).FirstOrDefault(l => l.LessonId == lessonId);
            return PartialView("LessonQuizDisplay", lesson);
        }

        [HttpPost]
        public IActionResult QuizResult(IFormCollection form, int lessonId)
        {
            var quiz = _db.LessonQuizzes
                .Include(q => q.QuizQuestions)
                .ThenInclude(qq => qq.QuizAnswers)
                .FirstOrDefault(q => q.LessonId == lessonId);

            if (quiz == null)
            {
                return NotFound();
            }

            int correctAnswers = 0;
            var results = new List<object>();

            foreach (var question in quiz.QuizQuestions)
            {
                var selectedAnswerIdStr = form[$"answer_{question.QuestionId}"];
                if (int.TryParse(selectedAnswerIdStr, out int selectedAnswerId))
                {
                    var correctAnswer = question.QuizAnswers.FirstOrDefault(a => a.IsCorrect);

                    bool isCorrect = correctAnswer != null && selectedAnswerId == correctAnswer.AnswerId;
                    if (isCorrect) correctAnswers++;

                    results.Add(new
                    {
                        questionId = question.QuestionId,
                        selectedAnswerId = selectedAnswerId,
                        correctAnswerId = correctAnswer.AnswerId,
                        isCorrect = isCorrect
                    });
                }
            }

            double percentage = (double)correctAnswers / quiz.QuizQuestions.Count * 100;
            bool passed = percentage >= quiz.PassingScore;
            if (passed)
            {
                CompleteLesson(lessonId, 1, (int)_db.Lessons.Include(e => e.Part).FirstOrDefault(e => e.LessonId == lessonId).Part.ModuleId);
            }

            // Return the percentage as part of the response
            return Json(new { passed = passed, percentage = percentage, results = results, lessonId = lessonId });
        }
    /*    [HttpPost]
        [Route("Learn/Next/{lessonId}/{moduleId}/{courseId}")]
        public IActionResult NextLesson(int lessonId, int moduleId,int courseId)
        {
            // Fetch the next lesson based on lessonId
            var nextLesson = _db.Lessons
                .Include(e => e.LessonType)
                .Include(e => e.Part)
                .ThenInclude(e=>e.Module)
                .FirstOrDefault(e => e.LessonId == lessonId + 1);
            var d = _db.Modules.FirstOrDefault(e=>e.ModuleId == nextLesson.Part.Module.ModuleId && e.CourseId == courseId);

            // Check if a next lesson exists and belongs to the correct module
            if (nextLesson != null)
            {
                // Return the lesson data as JSON
                return Json(new
                {
                    lessonId = nextLesson.LessonId,
                    lessonType = nextLesson.LessonType.TypeName
                });
            }

            return null;
        }*/


    }
}
