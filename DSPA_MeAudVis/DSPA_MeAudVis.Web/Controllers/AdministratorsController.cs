using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DSPA_MeAudVis.Web.Data;
using DSPA_MeAudVis.Web.Data.Entities;
using DSPA_MeAudVis.Web.Helpers;
using DSPA_MeAudVis.Web.Models;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.AspNetCore.Authorization;

namespace DSPA_MeAudVis.Web.Controllers
{
    public class AdministratorsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper combosHelper;
        private readonly IImageHelper imageHelper;
        private readonly IUserHelper userHelper;

        public AdministratorsController(DataContext context,
            ICombosHelper combosHelper,
            IImageHelper imageHelper,
            IUserHelper userHelper)
        {
            _context = context;
            this.combosHelper = combosHelper;
            this.imageHelper = imageHelper;
            this.userHelper = userHelper;
        }

        [Authorize(Roles = "Administrator")]
        // GET: Administrators
        public IActionResult Index()
        {
            return View( _context.Administrators
                .Include(s => s.User));
        }

        [Authorize(Roles = "Administrator")]
        // GET: Administrators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("AdministratorNotFound");
            }

            var administrator = await _context.Administrators
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrator == null)
            {
                return new NotFoundViewResult("AdministratorNotFound");
            }

            return View(administrator);
        }



        [Authorize(Roles = "Administrator")]
        // GET: Administrators/Create
        public IActionResult Create()
        {
            var model = new AdministratorViewModel
            {
                Users = combosHelper.GetComboUsers()
            };

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        // POST: Administrators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdministratorViewModel model)
        { 
            if(model.UserUserName!= "[You have to choose a username...]")
            { 
                var user = await userHelper.GetUserByNameAsync(model.UserUserName);

                if (user == null)
                {
                    return new NotFoundViewResult("AdministratorNotFound");
                }

                foreach (Administrator adminTemp in _context.Administrators.Include(c => c.User))
                {
                    if (adminTemp.User == user)
                    {
                        ModelState.AddModelError(string.Empty, "Administrator already exists");
                        return View(model);
                    }
                }

                var administrator = new Administrator { User = user };


                await userHelper.AddUserToRoleAsync(user, "Administrator");

                _context.Add(administrator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        

        [Authorize(Roles = "Administrator")]
        // GET: Administrators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("AdministratorNotFound");
            }

            var administrator = await _context.Administrators
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrator == null)
            {
                return new NotFoundViewResult("AdministratorNotFound");
            }

            return View(administrator);
        }

        [Authorize(Roles = "Administrator")]
        // POST: Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administrator = await _context.Administrators
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            await userHelper.RemoveUserFromRoleAsync(administrator.User, "Administrator");
            _context.Administrators.Remove(administrator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministratorExists(int id)
        {
            return _context.Administrators.Any(e => e.Id == id);
        }
    }
}
