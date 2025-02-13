using ECommerce.Entities.Concrete.Enums;
using ECommerce.UI.Entities;
using ECommerce.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly RoleManager<CustomIdentityRole> _roleManager;
        private readonly SignInManager<CustomIdentityUser> _signInManager;

        public AccountController(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager, SignInManager<CustomIdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            bool isAdmin;
            if (string.IsNullOrEmpty(returnUrl))
                isAdmin = false;
            else
                isAdmin = returnUrl.StartsWith("/" + nameof(Roles.Admin)) ? true : false;
            var vm = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                IsAdmin = isAdmin
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);
                return RedirectToAction("Index", "Product");
            }
            TempData["message"] = "Wrong username or password!";
            return View(model);

        }

        [HttpGet]
        public IActionResult Register(bool isAdmin = false)
        {
            var vm = new RegisterViewModel()
            {
                IsAdmin = isAdmin
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Roles roleToBeAdded = model.IsAdmin ? Roles.Admin : Roles.User;
                CustomIdentityUser user = new CustomIdentityUser()
                {
                    UserName = model.Username,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    string roleStr = roleToBeAdded.ToString();
                    if (!(await _roleManager.RoleExistsAsync(roleToBeAdded.ToString())))
                    {
                        CustomIdentityRole role = new CustomIdentityRole()
                        {
                            Name = roleToBeAdded.ToString()
                        };
                        var roleResult = await _roleManager.CreateAsync(role);
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("RoleError", "Can't add this role");
                            return View(model);
                        }
                    }
                    await _userManager.AddToRoleAsync(user, roleToBeAdded.ToString());
                    var returnUrlValue = model.IsAdmin ? "/" + roleToBeAdded.ToString() : null;
                    return RedirectToAction("Login", new {returnUrl = returnUrlValue});
                }
                else
                {
                    ModelState.AddModelError("UsernameError", result.Errors.ToString());
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Product");
        }
    }
}
