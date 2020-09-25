namespace DSPA_MeAudVis.Web.Controllers
{
    using DSPA_MeAudVis.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.Linq;
    using System.Threading.Tasks;

    //controlador para el inicio de sesion
    public class AccountController : Controller
    {
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
                var result = await _userHelper.LoginAsync(model.Username, model.Password, model.RememberMe);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Usuario o contraseña no válida.");
            model.Password = string.Empty;
            return View(model);
        }
    }
}
