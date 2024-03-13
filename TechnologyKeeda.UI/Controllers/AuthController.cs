using Microsoft.AspNetCore.Mvc;
using TechnologyKeeda.Entities;
using TechnologyKeeda.Repositories.Interfaces;
using TechnologyKeeda.UI.ViewModels.UserInfoViewModels;

namespace TechnologyKeeda.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepo _userRepo;

        public AuthController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpGet]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserInforViewModel vm)
        {
            if(ModelState.IsValid) {
            var userInfo =  await _userRepo.GetUserInfo(vm.UserName,vm.Password);
                if (userInfo != null)
                {
                    HttpContext.Session.SetInt32("userId", userInfo.UserId);
                    HttpContext.Session.SetString("userName", userInfo.UserName);
                    return RedirectToAction("Index", "Countries");
                }

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserInforViewModel vm)
        {
            var model = new UserInfo
            {
                UserName = vm.UserName,
                Password = vm.Password,
            };
            await _userRepo.RegisterUser(model);

            return RedirectToAction("Login");
        }
    }
}
