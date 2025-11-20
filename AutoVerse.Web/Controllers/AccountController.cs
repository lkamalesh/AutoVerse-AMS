using AutoVerse.Core.DTOs;
using AutoVerse.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AutoVerse.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        public AccountController(UserManager<ApplicationUser> usermanager, SignInManager<ApplicationUser> signinmanager)
        {
            _userManager = usermanager;
            _signinManager = signinmanager;
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            var user = new ApplicationUser
            {
                FullName = register.FullName,
                UserName = register.Email,
                Email = register.Email             
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                Log.Information($"New user registered: {user.Email}");
                await _userManager.AddToRoleAsync(user, "Customer");
                await _signinManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

           return View(register);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(login);
            }

            var result = await _signinManager.PasswordSignInAsync(user, login.Password, login.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                Log.Warning($"Failed login attempt for: {login.Email}");
                ModelState.AddModelError("", "Invalid email or password!");
                return View(login);
            }

            Log.Information($"User logged in: {user.Email}");
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public IActionResult AccesDenied()
        {
            return View();
        }
    }
}
