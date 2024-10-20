//using Learning_World.Models;
//using Learning_World.ViewModels;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace Learning_World.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly UserManager<ApplicationUser> _UserManager;
//        private readonly SignInManager<ApplicationUser> _SignInManager;
//        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
//        {
//            _UserManager = userManager;
//            _SignInManager = signInManager;
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }
//        //[HttpGet]
//        //public IActionResult Registration()
//        //{
//        //    return View("Registration");
//        //}
//        //[HttpPost]
//        //public async Task<IActionResult> Registration(RegistrationViewModel userViewModel)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        ApplicationUser applicationUser = new ApplicationUser();
//        //        applicationUser.UserName = userViewModel.UserName;
//        //        applicationUser.PasswordHash = userViewModel.Password;
//        //        applicationUser.Email = userViewModel.Email;
//        //        IdentityResult identityResult = await _UserManager.CreateAsync(applicationUser, userViewModel.Password);
//        //        if (identityResult.Succeeded)
//        //        {
//        //            // add Cookies
//        //            await _SignInManager.SignInAsync(applicationUser, false);
//        //            return RedirectToAction("GetAll", "Instructor");
//        //        }
//        //        foreach (var item in identityResult.Errors)
//        //        {
//        //            ModelState.AddModelError("", item.Description);
//        //        }
//        //    }
//        //    return View("Registration", userViewModel);
//        //}
//        //[HttpGet]
//        //public IActionResult Login()
//        //{
//        //    return View();
//        //}
//        //[HttpPost]
//        //public async Task<IActionResult> SaveLogin(LoginViewModel loginUserViewModel)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        ApplicationUser user = await _UserManager.FindByNameAsync(loginUserViewModel.Name);
//        //        if (user != null)
//        //        {
//        //            bool found = await _UserManager.CheckPasswordAsync(user, loginUserViewModel.Password);
//        //            if (found)
//        //            {
//        //                List<Claim> claims = new List<Claim>();
//        //                claims.Add(new Claim("Image", user.Image));
//        //                await _SignInManager.SignInWithClaimsAsync(user, loginUserViewModel.Remember, claims);
//        //                return RedirectToAction("GetAll", "Instructor");
//        //            }
//        //        }
//        //        ModelState.AddModelError("", "UserName Or Password Wrong");
//        //    }
//        //    return View("Login", loginUserViewModel);
//        //}
//        //public async Task<IActionResult> SignOutAsync()
//        //{
//        //    await _SignInManager.SignOutAsync();
//        //    return RedirectToAction("Login");
//        //}
//    }
//}
