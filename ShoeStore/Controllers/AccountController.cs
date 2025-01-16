using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.DataAccess.Repository;
using ShoeStore.Models.Entities;
using ShoeStore.Models.Interfaces;
using ShoeStore.Models.ViewModels;
using System.Security.Claims;
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
                ModelState.AddModelError("Password", "Invalid password.");
                return View(loginViewModel);
            }

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                TempData["ErrorMessage"] = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (signInResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new User
                    {
                        UserName = email,
                        Email = email
                    };
                    await _userManager.CreateAsync(user);
                    await _userManager.AddToRoleAsync(user, "User");
                    await _userManager.AddLoginAsync(user, info);
                }

                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Email claim not received.";
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
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
        [HttpGet("Settings")]
        public async Task<IActionResult> Settings()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var viewModel = new SettingsViewModel
            {
                DisplayName = user.UserName,
                Email = user.Email,
                Theme = Request.Cookies["Theme"],
            };

            return View(viewModel);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost("Settings")]
        public async Task<IActionResult> UpdateSettings(SettingsViewModel settingsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Settings", settingsViewModel);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            /* check email change */
            if (settingsViewModel.Email != user.Email)
            {
                if (await _userRepository.IsEmailTakenAsync(settingsViewModel.Email))
                {
                    ModelState.AddModelError("Email", "This email is already taken.");
                    return View("Settings", settingsViewModel);
                }
                user.Email = settingsViewModel.Email;
            }

            /* check name change */
            if (settingsViewModel.DisplayName != user.UserName)
            {
                user.UserName = settingsViewModel.DisplayName;
            }

            /* check password change */
            if (!string.IsNullOrEmpty(settingsViewModel.CurrentPassword))
            {
                if (string.IsNullOrEmpty(settingsViewModel.NewPassword))
                {
                    ModelState.AddModelError("NewPassword", "Please enter the new password.");
                    return View("Settings", settingsViewModel);
                }

                var isCurrentPasswordCorrect = await _userManager.CheckPasswordAsync(user, settingsViewModel.CurrentPassword);
                if (!isCurrentPasswordCorrect) 
                {
                    ModelState.AddModelError("CurrentPassword", "The current password is incorrect.");
                    return View("Settings", settingsViewModel);
                }
                await _userManager.ChangePasswordAsync(user, settingsViewModel.CurrentPassword, settingsViewModel.NewPassword);
            }

            Response.Cookies.Append("Theme", settingsViewModel.Theme, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1)
            });

            await _userManager.UpdateAsync(user);

            TempData["SuccessMessage"] = "Settings updated successfully.";
            return RedirectToAction("Settings");
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("Theme");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
