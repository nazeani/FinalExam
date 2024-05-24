using Core.Models;
using LumiaMVC1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LumiaMVC1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM userRegisterVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = null;
           user = await _userManager.FindByNameAsync(userRegisterVM.UserName);
            if (user != null)
            {
                ModelState.AddModelError("UserName", "Bele bir UserName movcuddur!");
                return View();
            }
           user = await _userManager.FindByNameAsync(userRegisterVM.Email);
            if (user != null)
            {
                ModelState.AddModelError("Email", "Bele bir Email movcuddur!");
                return View();
            }
            user = new AppUser()
            {
                UserName = userRegisterVM.UserName,
                Email = userRegisterVM.Email
            };
            var result = await _userManager.CreateAsync(user,userRegisterVM.Password);
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVM userLoginVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(userLoginVM.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "UserName or Password is not valid!");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user, userLoginVM.Password, userLoginVM.IsPersistent, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is not valid!");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
