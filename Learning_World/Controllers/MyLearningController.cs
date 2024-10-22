using Learning_World.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Learning_World.Models;
using Learning_World.ViewModels;
using System.Reflection;
using Microsoft.Data.SqlClient.DataClassification;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Learning_World.Repositories;

namespace Learning_World.Controllers
{
    [Authorize]
    public class MyLearningController : Controller
    {
        private readonly MyLearningRepository _myLearningRepository;

        public MyLearningController(MyLearningRepository myLearningRepository)
        {
            _myLearningRepository = myLearningRepository;
        }

        public IActionResult Index()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            var courses = _myLearningRepository.FilterCourses(userId);
            return View(_myLearningRepository.GetMyLearningCourses(courses, userId));
        }

        public IActionResult MyCourses()
        {
            return View();
        }

        public IActionResult InProgressCourses()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            var courses = _myLearningRepository.FilterCourses(userId);
            var inProgressCourses = _myLearningRepository.GetMyLearningCourses(courses, userId);

            return PartialView("_MyLearningCoursesPartial", inProgressCourses);
        }

        public IActionResult CompletedCourses()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            var courses = _myLearningRepository.FilterCourses(userId).Where(c => c.Value == 100);

            var completedCourses = _myLearningRepository.GetMyLearningCourses(courses.ToDictionary(), userId);

            return PartialView("_MyLearningCoursesPartial", completedCourses);
        }


    }
}
