using System.Security.Claims;
using Learning_World.Data;
using Learning_World.Models;
using Learning_World.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

		[Route("Courses/view/{courseId}")]

		public IActionResult ShowCourse(int courseId = 1)
		{
			Course course = _context.Courses.Include(e => e.Users).FirstOrDefault(e => e.CourseId == courseId);
			if (course == null)
			{
				return View("NotFound404");
			}
			ViewBag.modules = _context.Modules.Where(e => e.CourseId == courseId).ToList();
			ViewBag.parts = _context.Parts.ToList();
			ViewBag.lessons = _context.Lessons.ToList();
			ViewBag.IsEnrolled = false;
			if (User.Identity.IsAuthenticated)
			{
			int userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);
			ViewBag.IsEnrolled = _context.Enrollments.FirstOrDefault(e => e.CourseId == courseId &&e.UserId == userId) == null ? false : true;
			}
			return View(course);
		}

		[Authorize]
		[HttpGet]
		public IActionResult Enroll(int courseId)
		{
			var course = _context.Courses.FirstOrDefault(c => c.CourseId == courseId);
			if (course == null)
				return View("NotFound404");

			var userRegistration = new UserRegistrationViewModel();
			userRegistration.CourseId = course.CourseId;
			userRegistration.CourseName = course.Title;
			userRegistration.CoursePrice = course.Price;

			return View(userRegistration);
		}
		[Authorize]
		[HttpPost]
		public IActionResult Enroll(UserRegistrationViewModel model)
		{
			if (ModelState.IsValid)
			{
				// Save user information to the database
				//if (model.PaymentMethod == "CreditCard")
				//{
				int userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);

				var CreditPayment = new Payment
				{
					CardName = model.CardName,
					CardNumber = model.CardNumber,
					ExpiryDate = model.ExpiryDate,
					CVC = model.CVC,
					Country = model.Country
				};

				var enrollment = new Enrollment
				{
					CourseId = model.CourseId,
					UserId = userId,
					EnrollmentDate = DateTime.UtcNow,
					Payment = CreditPayment
				};

				_context.Payments.Add(CreditPayment);
				_context.Enrollments.Add(enrollment);
				_context.SaveChanges();


				
				return RedirectToAction("Index", "MyLearning");
				// return RedirectToAction("CourseView", new {courseId = model.Course.CourseId, userId = model.UserId});
			}

			return View("Enroll", model);
		}
	}
}
