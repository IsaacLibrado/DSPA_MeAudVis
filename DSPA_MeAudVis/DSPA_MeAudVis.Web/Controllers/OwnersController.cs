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
using Microsoft.AspNetCore.Authorization;
using DSPA_MeAudVis.Web.Models;

namespace DSPA_MeAudVis.Web.Controllers
{
    public class OwnersController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper combosHelper;
        private readonly IImageHelper imageHelper;
        private readonly IUserHelper userHelper;

        public OwnersController(DataContext context,
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
        // GET: Owners
        public IActionResult Index()
        {
            return View( _context.Owners.Include(s => s.User));
        }

        [Authorize(Roles = "Administrator")]
        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("OwnerNotFound");
            }

            var owner = await _context.Owners
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
            {
                return new NotFoundViewResult("OwnerNotFound");
            }

            return View(owner);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Owners/Create
        public IActionResult Create()
        {
            var model = new OwnerViewModel
            {
                Users = combosHelper.GetComboUsers()
            };

            return View(model);
        }

        // POST: Owners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OwnerViewModel model)
        {
            if (model.UserUserName != "[You have to choose a username...]")
            {
                var user = await userHelper.GetUserByNameAsync(model.UserUserName);

                if (user == null)
                {
                    return new NotFoundViewResult("OwnerNotFound");
                }

                var owner = new Owner { User = user };


                await userHelper.AddUserToRoleAsync(user, "Owner");

                _context.Add(owner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("OwnerNotFound");
            }

            var owner = await _context.Owners.
                Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
            {
                return new NotFoundViewResult("OwnerNotFound");
            }

            var model = new OwnerViewModel
            {
                Id = owner.Id,
                User = owner.User,
                UserUserName = owner.User.UserName,
                Users = combosHelper.GetComboUsers()
            };

            return View(model);
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OwnerViewModel model)
        {
            if (id != model.Id)
            {
                return new NotFoundViewResult("OwnerNotFound");
            }

            if (ModelState.IsValid)
            {
                var user = await userHelper.GetUserByEmailAsync(model.User.Email);

                if (user == null)
                {
                    return new NotFoundViewResult("OwnerNotFound");
                }

                user.FirstName = model.User.FirstName;
                user.LastName = model.User.LastName;
                user.PhoneNumber = model.User.PhoneNumber;
                user.RegistrationNumber = model.User.RegistrationNumber;
                user.Email = model.User.Email;
                user.UserName = model.User.UserName;
                var owner = await _context.Owners.FindAsync(model.Id);

                if (owner == null)
                {
                    return new NotFoundViewResult("OwnerNotFound");
                }

                owner.Id = model.Id;
                owner.User = user;
                _context.Update(owner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("OwnerNotFound");
            }

            var owner = await _context.Owners
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
            {
                return new NotFoundViewResult("OwnerNotFound");
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _context.Owners
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            await userHelper.RemoveUserFromRoleAsync(owner.User, "Administrator");
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerExists(int id)
        {
            return _context.Owners.Any(e => e.Id == id);
        }
    }
}
