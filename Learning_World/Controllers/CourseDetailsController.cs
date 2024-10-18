using Learning_World.Data;
using Learning_World.Models;
using Learning_World.ViewModels;
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
		[Route("ViewCourse/Enroll/{courseId}/{userId}")]

		[Route("ViewCourse/Start/{courseId}/{userId}")]

		[Route("CourseDetails/ShowCourse/{courseId}/{userId}")]
		public IActionResult ShowCourse(int courseId = 1, int userId = 1)
		{
			Course course = _context.Courses.Include(e => e.Users).FirstOrDefault(e => e.CourseId == courseId);
			if (course == null)
			{
				return NotFound();
			}
			ViewBag.modules = _context.Modules.Where(e => e.CourseId == courseId).ToList();
			ViewBag.parts = _context.Parts.ToList();
			ViewBag.lessons = _context.Lessons.ToList();
			ViewBag.UserId = userId;

			ViewBag.IsEnrolled = _context.Enrollments.FirstOrDefault(e => e.CourseId == courseId &&
			e.UserId == userId) == null ? false : true;

			return View(course);
		}

		[Route("/CourseDetails/EnrollPartialView/{courseId}/{userId}")]

		public IActionResult EnrollPartialView(int courseId, int userId)
		{
			ViewBag.CourseId = courseId;
			ViewBag.UserId = userId;
			return PartialView();
		}
		[Route("/CourseDetails/GoToCoursePartialView/{courseId}/{userId}")]

		public IActionResult GoToCoursePartialView(int courseId, int userId)
		{
			ViewBag.CourseId = courseId;
			ViewBag.UserId = userId;
			return PartialView();
		}


		public IActionResult Enroll(int courseId, int userId)
		{
			//ViewBag.CourseId = courseId; // Pass the course ID to the view
			var course = _context.Courses.FirstOrDefault(c => c.CourseId == courseId);
			if (course == null)
				return NotFound();

			var userRegistration = new UserRegistrationViewModel();
			userRegistration.CourseId = course.CourseId;
			userRegistration.CourseName = course.Title;
			userRegistration.CoursePrice = course.Price;
			userRegistration.UserId = userId;

			return View(userRegistration);
		}
		[HttpPost]
		public IActionResult Enroll(UserRegistrationViewModel model)
		{
			if (ModelState.IsValid)
			{
				// Save user information to the database
				//if (model.PaymentMethod == "CreditCard")
				//{

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
					UserId = model.UserId,
					EnrollmentDate = DateTime.UtcNow,
					Payment = CreditPayment
				};

				_context.Payments.Add(CreditPayment);
				_context.Enrollments.Add(enrollment);
				_context.SaveChanges();


				// Validate and process credit card information
				//}
				//else if (model.PaymentMethod == "PayPal")
				//{
				//    //var PayPalPayment = new PaymentMethod { };
				//    // Process PayPal payment
				//}
				// Redirect to a success page or return view
				return RedirectToAction("Index", "MyLearning", new { userId = model.UserId });
				// return RedirectToAction("CourseView", new {courseId = model.Course.CourseId, userId = model.UserId});
			}

			return View("Enroll", model);
		}
	}
}
