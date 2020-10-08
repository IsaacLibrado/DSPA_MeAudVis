namespace DSPA_MeAudVis.Web.Controllers
{
    using DSPA_MeAudVis.Web.Data.Entities;
    using DSPA_MeAudVis.Web.Helpers;
    using DSPA_MeAudVis.Web.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    //controlador para el inicio de sesion
    public class AccountController : Controller
    {
        private readonly IUserHelper userHelper;

        public AccountController(IUserHelper userHelper)
        {
            this.userHelper = userHelper;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userHelper.LoginAsync(model.UserName, model.Password, model.RememberMe);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "User or password invalid.");
            model.Password = string.Empty;
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userHelper.GetUserByEmailAsync(model.Email);
                
                if(user==null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        RegistrationNumber = model.RegistrationNumber,
                        UserName = model.RegistrationNumber.ToString()
                    };

                    var result = await userHelper.AddUserAsync(user, model.Password);
                    
                    if(result!=IdentityResult.Success)
                    {
                        ModelState.AddModelError(string.Empty, "User could not be created");
                        return View(model);
                    }

                    if (ModelState.IsValid)
                    {
                       return RedirectToAction("Register", "Account");
                    }
                }

                ModelState.AddModelError(string.Empty, "User already exists");
            }

            return View(model);
        }


    }
}
