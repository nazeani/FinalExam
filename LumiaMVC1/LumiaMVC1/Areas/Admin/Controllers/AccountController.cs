using Core.Models;
using LumiaMVC1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LumiaMVC1.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public async Task<IActionResult> CreateAdmin()
        {
            AppUser user = new AppUser()
            {
                UserName = "SuperAdmin",
                FullName = "Nazani Mustafayeva"
            };
            await _userManager.CreateAsync(user,"Admin123@");
            await _userManager.AddToRoleAsync(user, "SuperAdmin");
            return Ok("Admin yarandi!");
        }
        public async Task<IActionResult> CreateRoles()
        {
            IdentityRole identityRole1 = new IdentityRole("SuperAdmin");
            IdentityRole identityRole2 = new IdentityRole("Admin");
            IdentityRole identityRole3 = new IdentityRole("Member");
            await _roleManager.CreateAsync(identityRole1);
            await _roleManager.CreateAsync(identityRole2);
            await _roleManager.CreateAsync(identityRole3);
            return Ok("Rollar yarandi!");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVM adminLoginVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user= await _userManager.FindByNameAsync(adminLoginVM.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "UserName or Password is not valid!");
                return View();
            }
            var result= await _signInManager.PasswordSignInAsync(user,adminLoginVM.Password,adminLoginVM.IsPersistent,false);
            if (!result.Succeeded) 
            {
                ModelState.AddModelError("", "UserName or Password is not valid!");
                return View();
            }
            return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
