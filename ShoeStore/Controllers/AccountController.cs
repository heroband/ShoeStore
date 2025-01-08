using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.DataAccess.Repository;
using ShoeStore.Models.Entities;
using ShoeStore.Models.Interfaces;
using ShoeStore.Models.ViewModels;
using System.Security.Cryptography;

namespace ShoeStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(IUserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "No account registered with this email.");
                return View(loginViewModel);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginViewModel.Password, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Password", "Invalid password." + loginViewModel.Password);
                return View(loginViewModel);
            }

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }


        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }

            if (await _userRepository.IsEmailTakenAsync(userViewModel.Email))
            {
                ModelState.AddModelError("Email", "This email is already taken.");
                return View(userViewModel);
            }

            var user = new User
            {
                UserName = userViewModel.Name,
                Email = userViewModel.Email,
            };

            var createdUser = await _userManager.CreateAsync(user, userViewModel.Password);

            if (!createdUser.Succeeded)
            {
                foreach (var error in createdUser.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(userViewModel); // Возвращаем ошибку, если создание не удалось
            }

            await _userManager.AddToRoleAsync(user, "User");
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
