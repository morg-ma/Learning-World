using Learning_World.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Learning_World.Models;
using Learning_World.ViewModels;
using System.Reflection;
using Microsoft.Data.SqlClient.DataClassification;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Learning_World.Controllers
{
	[Authorize]
	public class MyLearningController : Controller
	{
		private readonly ElearningPlatformContext _context;

		public MyLearningController(ElearningPlatformContext context)
		{
			_context = context;
			
		}

		public IActionResult Index()
		{
			var courses = FilterCourses().Select(c => new
			{
				CourseName = c.Key,
				CompletionPercentage = c.Value,
				IsCompleted = c.Value == 100
			}).ToList();
			int userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

			List<MyLearningCoursesViewModel> myLearningCoursesViewModels = courses.Select(course => new MyLearningCoursesViewModel
			{
				UserId = userId,  // Assign UserId from your controller context
				Course = _context.Courses.FirstOrDefault(e => e.CourseId == course.CourseName),
				CompletionPercentage = course.CompletionPercentage,
				CompletionDate = (from lc in _context.LessonCompletions
								  join module in _context.Modules on lc.ModuleId equals module.ModuleId
								  where module.CourseId == course.CourseName && lc.UserId == userId
								  orderby lc.CompletionDate descending
								  select lc.CompletionDate).FirstOrDefault(), // Get 
				IsCompleted = course.IsCompleted
			}).ToList();
			return View(myLearningCoursesViewModels);
		}

		public IActionResult MyCourses()
		{
			return View();
		}

		public IActionResult InProgressCourses()
		{
			int userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

			var courses = FilterCourses().Where(c => c.Value < 100).Select(c => new
			{
				CourseName = c.Key,  // Assuming c.Key is the course name
				CompletionPercentage = c.Value,
				IsCompleted = c.Value == 100  // Completed if the value is 100%
			}).ToList();
			List<MyLearningCoursesViewModel> myLearningCoursesViewModels = courses.Select(course => new MyLearningCoursesViewModel
			{
				UserId = userId,  // Assign UserId from your controller context
				Course = _context.Courses.FirstOrDefault(e => e.CourseId == course.CourseName),
				CompletionPercentage = course.CompletionPercentage,
				IsCompleted = course.IsCompleted
			}).ToList();

			return PartialView("_MyLearningCoursesPartial", myLearningCoursesViewModels);
		}

		public IActionResult CompletedCourses()
		{
			var courses = FilterCourses().Where(c => c.Value == 100)  // Only select completed courses
		.Select(c => new
		{
			CourseName = c.Key,  // Assuming c.Key is the course name
			CompletionPercentage = c.Value,
			IsCompleted = c.Value == 100  // Completed if the value is 100%
		}).ToList();
			int userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

			List<MyLearningCoursesViewModel> myLearningCoursesViewModels = courses.Select(course => new MyLearningCoursesViewModel
			{
				UserId = userId,  // Assign UserId from your controller context
				Course = _context.Courses.FirstOrDefault(e => e.CourseId == course.CourseName),
				CompletionDate = (from lc in _context.LessonCompletions
								  join module in _context.Modules on lc.ModuleId equals module.ModuleId
								  where module.CourseId == course.CourseName && lc.UserId == userId
								  orderby lc.CompletionDate descending
								  select lc.CompletionDate).FirstOrDefault(), // Get 
				CompletionPercentage = course.CompletionPercentage,
				IsCompleted = course.IsCompleted
			}).ToList();
			return PartialView("_MyLearningCoursesPartial", myLearningCoursesViewModels);
		}

		public Dictionary<int?, int> FilterCourses()
		{
			int userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

			var userCourses = _context.Enrollments.Include(e => e.Course)
				.Where(e => e.UserId == userId).ToList();
			Dictionary<int?, int> keyValuePairs = new();
			foreach (var courseData in userCourses)
			{
				var modules = _context.Modules.Where(e => e.CourseId == courseData.Course.CourseId).ToList();
				int lessons = 0;
				int completedLessons = 0;
				foreach (var module in modules)
				{
					lessons += _context.Lessons.Include(e => e.Part).Where(e => e.Part.ModuleId == module.ModuleId).Count();
					completedLessons += _context.LessonCompletions.Where(e => e.ModuleId == module.ModuleId && e.UserId == userId).Count();
				}
				int progress = lessons > 0 ? (int)((double)completedLessons / lessons * 100) : 0;
				keyValuePairs.Add(courseData.CourseId, progress);
			}
			return keyValuePairs;
		}
	}
}
