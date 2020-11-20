namespace DSPA_MeAudVis.Web.Controllers
{
    using DSPA_MeAudVis.Web.Data;
    using DSPA_MeAudVis.Web.Data.Entities;
    using DSPA_MeAudVis.Web.Helpers;
    using DSPA_MeAudVis.Web.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    //controlador para el inicio de sesion
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper userHelper;
        private readonly ICombosHelper combosHelper;
        private readonly IImageHelper imageHelper;

        public AccountController(DataContext context,IUserHelper userHelper, ICombosHelper combosHelper,
            IImageHelper imageHelper)
        {
            _context = context;
            this.userHelper = userHelper;
            this.combosHelper = combosHelper;
            this.imageHelper = imageHelper;
        }

        [Authorize(Roles = "Owner, Administrator")]
        // GET: Statuses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
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

        [Authorize(Roles = "Owner, Administrator")]
        public IActionResult Register()
        {
            var model = new RegisterViewModel{ Roles=combosHelper.GetComboRoles(), Types=combosHelper.GetComboApplicantTypes()};

            return View(model);
        }

        [Authorize(Roles = "Owner, Administrator")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userHelper.GetUserByNameAsync(model.RegistrationNumber.ToString());
                
                if(user==null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        RegistrationNumber=model.RegistrationNumber,
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
                        if (model.RoleName != "0")
                        {
                            await userHelper.AddUserToRoleAsync(user, model.RoleName);
                        }

                        if (model.ImageFile != null)
                        {
                            user.ImageURL = await imageHelper.UploadImageAsync(model.ImageFile, user.FullName, "FotosEstudiantes");
                        }

                        switch (model.RoleName)
                        {
                            case "Administrator":
                                _context.Administrators.Add(new Administrator { User = user });
                                break;
                            case "Intern":
                                _context.Interns.Add(new Intern { User = user, DepartureTime = 0, EntryTime = 0 });
                                break;
                            case "Owner":
                                _context.Owners.Add(new Owner { User = user });
                                break;
                            case "Applicant":
                                var applicantType = _context.ApplicantTypes.FirstOrDefault(m => m.Id == model.TypeId);
                                _context.Applicants.Add(new Applicant { User = user, Type = applicantType, Debtor = false });
                                break;
                            default:
                                applicantType = _context.ApplicantTypes.FirstOrDefault(m => m.Id == model.TypeId);
                                _context.Applicants.Add(new Applicant { User = user, Type = applicantType, Debtor = false });
                                await userHelper.AddUserToRoleAsync(user, "Applicant");
                                break;       
                        }

                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index", "Account");
                    }
                }

                ModelState.AddModelError(string.Empty, "User already exists");
            }

            return View(model);
        }


        public IActionResult NotAuthorized()
        {
            return View();
        }
    }
}
