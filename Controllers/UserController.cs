using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagerInAspNet.Models;
using TaskManagerInAspNet.Servicios;

namespace TaskManagerInAspNet.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ApplicationDbContext applicationDbContext;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext applicationDbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.applicationDbContext = applicationDbContext;
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

        [AllowAnonymous]
        public IActionResult Login(string? message = null)
        {
            if (message is not null)
            {
                ViewData["message"] = message;
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var operation = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (operation.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Nombre de usuario o password incorrecto");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ExternalLogin(string provider, string? returnUrl = null)
        {
            var redirectUrl = Url.Action("RegisterExternalUser", values: new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> RegisterExternalUser(string? returnUrl = null, string? remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            var message = "";
            if(remoteError is not null)
            {
                message = $"Error del proveedor externo: {remoteError}";
                return RedirectToAction("Login", routeValues: new {message});
            }
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info is null)
            {
                message = "Error al cargar la data del inicio de sesión externo";
                return RedirectToAction("Login", routeValues: new { message });
            }
            var externalLoginResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true);
            if (externalLoginResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            string? email = null;
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                email = info.Principal.FindFirstValue(ClaimTypes.Email);
            }
            else
            {
                message = "Error al obtener el email del usuario del proveedor";
                return RedirectToAction("login", routeValues: new { message });
            }
            var user = new IdentityUser(){ Email = email, UserName = email};
            var userCreationResult = await userManager.CreateAsync(user);
            if (!userCreationResult.Succeeded)
            {
                message = userCreationResult.Errors.First().Description;
                return RedirectToAction("Login", routeValues: new { message });
            }
            var userLoginResult = await userManager.AddLoginAsync(user, info);
            if (userLoginResult.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: true, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }
            message = "Ha ocurrido un error al agregar el login";
            return RedirectToAction("Login", routeValues: new { message });
        }

        [HttpGet]
        [Authorize(Roles = Constants.AdminRole)]
        public async Task<IActionResult> UsersList(string? message = null)
        {
            var users = await applicationDbContext.Users.Select(u => new UserViewModel{ Email = u.Email }).ToListAsync();
            var model = new UsersListViewModel();
            model.Users = users;
            model.Message = message;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Constants.AdminRole)]
        public async Task<IActionResult> MakeAdministrator(string email)
        {
            var user = await applicationDbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if(user is null)
            {
                return NotFound();
            }
            await userManager.AddToRoleAsync(user, Constants.AdminRole);
            return RedirectToAction("UsersList", routeValues: new { message = "Rol asignado a " + email });
        }

        [HttpPost]
        [Authorize(Roles = Constants.AdminRole)]
        public async Task<IActionResult> RemoveAdministrator(string email)
        {
            var user = await applicationDbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if (user is null)
            {
                return NotFound();
            }
            await userManager.RemoveFromRoleAsync(user, Constants.AdminRole);
            return RedirectToAction("UsersList", routeValues: new { message = "Rol removido de " + email });
        }
    }
}
