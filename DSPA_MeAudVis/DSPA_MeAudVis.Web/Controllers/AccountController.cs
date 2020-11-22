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
    using System;
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

        // GET: Administrators/Details/5
        public async Task<IActionResult> Details(string Id)
        {
            if (Id == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == Id);

            if (user == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            if (!this.User.IsInRole("Owner") && !this.User.IsInRole("Administrator") && user.UserName != this.User.Identity.Name)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            return View(user);
        }

        [HttpGet]
        // GET: Administrators/Details/5
        public async Task<IActionResult> DetailsActual(int? id)
        {
            var userName = id.ToString();
            var user = await userHelper.GetUserByNameAsync(userName);

            if (user == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            if (!this.User.IsInRole("Owner") && !this.User.IsInRole("Administrator") && userName != this.User.Identity.Name)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            return RedirectToAction(String.Format("Details/{0}", user.Id), "Account");
        }

        [Authorize(Roles = "Administrator")]
        // GET: Administrators/Delete/5
        public async Task<IActionResult> Delete(string Id)
        {
            if (Id == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == Id);

            

            if (user == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            return View(user);
        }

        [Authorize(Roles = "Administrator")]
        // POST: Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string Id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == Id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Administrators/Edit/5
        public async Task<IActionResult> Edit(string Id)
        {

            if (Id == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == Id);

            if (user == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            var model = new UserViewModel
            {
                Id = user.Id,
                ImageURL=user.ImageURL,
                RegistrationNumber=user.RegistrationNumber,
                UserName=user.UserName,
                FirstName=user.FirstName,
                LastName=user.LastName,
                PhoneNumber=user.PhoneNumber,
                Email=user.Email
                
            };

            if (!this.User.IsInRole("Owner") && !this.User.IsInRole("Administrator") && user.UserName != this.User.Identity.Name)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            return View(model);
        }


        // POST: Administrators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserViewModel model)
        {
            if (id != model.Id)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            if (ModelState.IsValid)
            {

                var user = await userHelper.GetUserByNameAsync(model.UserName);

                if (user == null)
                {
                    return new NotFoundViewResult("UserNotFound");
                }

                var user2 = await userHelper.GetUserByNameAsync(model.RegistrationNumber.ToString());

                if (user2 != null && user2!=user)
                {
                    ModelState.AddModelError(string.Empty, "Registration number already asigned");
                    return View(model);
                }

                user2 = await userHelper.GetUserByEmailAsync(model.Email);

                if (user2 != null && user2!=user)
                {
                    ModelState.AddModelError(string.Empty, "Email already asigned");
                    return View(model);
                }



                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                user.RegistrationNumber = model.RegistrationNumber;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                if (model.ImageFile != null)
                {
                    user.ImageURL = await imageHelper.UploadImageAsync(model.ImageFile, user.FullName, "FotosEstudiantes");
                }

                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(String.Format("Details/{0}", user.Id), "Account");

            }

            return View(model);
        }

        // GET: Administrators/Edit/5
        public async Task<IActionResult> ChangePassword(string Id)
        {

            if (Id == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == Id);

            if (user == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            var model = new ChangePasswordViewModel
            {
                Id = user.Id,
                ImageURL = user.ImageURL,
                RegistrationNumber = user.RegistrationNumber,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email

            };

            if (!this.User.IsInRole("Owner") && !this.User.IsInRole("Administrator") && user.UserName != this.User.Identity.Name)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            return View(model);
        }


        // POST: Administrators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string id, ChangePasswordViewModel model)
        {
            if (id != model.Id)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            if (model.Password!=null && model.Password!=string.Empty && model.OldPassword != null && model.OldPassword != string.Empty)
            {

                var user = await userHelper.GetUserByNameAsync(model.UserName);

                if (user == null)
                {
                    return new NotFoundViewResult("UserNotFound");
                }

                var result = await userHelper.ChangePasswordAsync(user, model.OldPassword, model.Password);

                if (result != IdentityResult.Success)
                {
                    ModelState.AddModelError(string.Empty, "Password could have not be changed");
                    return View(model);
                }


                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(String.Format("Details/{0}", user.Id), "Account");

            }

            return View(model);
        }
    }
}
