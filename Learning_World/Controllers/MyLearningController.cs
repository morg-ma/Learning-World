using Learning_World.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Learning_World.Models;
using Learning_World.ViewModels;

namespace Learning_World.Controllers
{
    public class MyLearningController : Controller
    {
        private readonly ElearningPlatformContext _context;

        public MyLearningController(ElearningPlatformContext context)
        {
            _context = context;
        }

        // http://localhost:5020/MyLearning/Index?userId=1
        public IActionResult Index(int userId = 1)
        {
            var inprogressCourses = FilterCourses(userId).Where(c => c.IsCompleted == false).ToList();
            ViewBag.UserId = userId;
            return View(inprogressCourses);
        }

        public IActionResult MyCourses()
        {
            return View();
        }

        public IActionResult InProgressCourses(int userId)
        {
            var inprogressCourses = FilterCourses(userId).Where(c => c.IsCompleted == false).ToList();
            ViewBag.UserId = userId;
            return PartialView("_MyLearningCoursesPartial", inprogressCourses);
        }

        public IActionResult CompletedCourses(int userId)
        {
            var completedCourse = FilterCourses(userId).Where(c => c.IsCompleted == true).ToList();
            ViewBag.UserId = userId;
            return PartialView("_MyLearningCoursesPartial", completedCourse);
        }

        public List<MyLearningCoursesViewModel> FilterCourses(int userId)
        {
            // Fetch all courses the user is enrolled in, along with their total and completed parts
            var userCourses = _context.Enrollments
                .Where(e => e.UserId == userId)
                .Select(e => new
                {
                    Course = e.Course,
                    TotalParts = e.Course.Modules.SelectMany(m => m.Parts).Count(),
                    CompletedParts = e.Course.Modules.SelectMany(m => m.Parts)
                        .Count(p => _context.Progresses.Any(pr => pr.UserId == userId && pr.PartId == p.PartId && pr.CompletionStatus))
                })
                .ToList();

            List<MyLearningCoursesViewModel> inprogressCourses = new List<MyLearningCoursesViewModel>();

            foreach (var courseData in userCourses)
            {
                if (courseData.TotalParts == 0)
                    continue;

                var completionPercentage = Math.Round((courseData.CompletedParts * 100m) / courseData.TotalParts, 2);

                inprogressCourses.Add(new MyLearningCoursesViewModel()
                {
                    UserId = userId,
                    CompletionPercentage = completionPercentage,
                    Course = courseData.Course,
                    CompletionDate = _context.Progresses
                        .Where(p => p.UserId == userId && p.CompletionStatus == true)
                        .Max(p => p.CompletionDate),
                    IsCompleted = courseData.TotalParts == courseData.CompletedParts ? true : false
                });
            }

            return inprogressCourses;
        }
    }
}
