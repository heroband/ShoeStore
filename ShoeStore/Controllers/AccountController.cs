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
        private readonly SignInManager<User> _signInManager;
        public AccountController(IUserRepository userRepository, SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
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
                Role = "User" 
            };

            await _userRepository.AddUserAsync(user, userViewModel.Password);
            await _signInManager.SignInAsync(user, isPersistent: false);
            
            return RedirectToAction("Index", "Home");
        }
    }
}
