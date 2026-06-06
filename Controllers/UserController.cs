using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagerInAspNet.Models;

namespace TaskManagerInAspNet.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser() {Email = viewModel.Email, UserName = viewModel.Email};
                var createUserOperation = await userManager.CreateAsync(user, password: viewModel.Password);
                if (createUserOperation.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in createUserOperation.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(viewModel);
                }
            }
            else
            {
                return View(viewModel);
            }
        }
    }
}
