
namespace DSPA_MeAudVis.Web.Controllers
{
    using DSPA_MeAudVis.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.Threading.Tasks;

    //controlador para el inicio de sesion
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            return View(model);
        }
    }
}
