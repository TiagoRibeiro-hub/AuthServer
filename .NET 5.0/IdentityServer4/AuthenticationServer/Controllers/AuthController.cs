using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthenticationServer.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        
        public AuthController(
            SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager,
            IIdentityServerInteractionService identityServerInteractionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _identityServerInteractionService = identityServerInteractionService;
        }
        
        [HttpGet]
        [Route("/Auth/LogIn")]
        public IActionResult LogIn(string returnUrl)
        {
            return View(new LoginViewModel 
            { 
                ReturnUrl = returnUrl
            });
        }


        [HttpPost]
        [Route("/Auth/LogIn")]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            var res = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (res.Succeeded)
            {
                return Redirect(model.ReturnUrl);
            }
            else if (res.IsLockedOut)
            {

            }

            return View();
        }


        [HttpGet]
        [Route("/Auth/LogOut")]
        public async Task<IActionResult> LogOut(string logOutId)
        {
            await _signInManager.SignOutAsync();

            var logoutRequest = await _identityServerInteractionService.GetLogoutContextAsync(logOutId);

            if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
                return RedirectToAction("Index", "Home");

            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }



        [HttpGet]
        [Route("/Auth/Register")]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [Route("/Auth/Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser(model.UserName);
            var res = await _userManager.CreateAsync(user, model.Password);

            if (res.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Redirect(model.ReturnUrl);

            }

            return Redirect(model.ReturnUrl);
        }
    }
}
