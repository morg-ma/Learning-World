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
        public IActionResult Enroll(int courseId)
        {
            ViewBag.CourseId = courseId; // Pass the course ID to the view
            return View();
        }
        [HttpPost]
        public IActionResult Enroll(UserRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Save user information to the database
                if (model.PaymentMethod == "CreditCard")
                {
                    //var CreditPayment = new PaymentMethod { CardName = model.CardName, CardNumber = model.CardNumber,ExpiryDate=model.ExpiryDate,CVC=model.CVC,PaymentType=model.PaymentMethod,UserID=model.UserId };
                    //_context.PaymentMethods.Add(CreditPayment);
                    //_context.SaveChanges();
                    // Validate and process credit card information
                }
                else if (model.PaymentMethod == "PayPal")
                {
                    //var PayPalPayment = new PaymentMethod { };
                    // Process PayPal payment
                }
                // Redirect to a success page or return view
                return RedirectToAction("new view");
            }

            return View("Enroll");
        }
    }
}
