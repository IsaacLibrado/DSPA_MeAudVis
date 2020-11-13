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
    public class InternsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper combosHelper;
        private readonly IImageHelper imageHelper;
        private readonly IUserHelper userHelper;

        public InternsController(DataContext context,
            ICombosHelper combosHelper,
            IImageHelper imageHelper,
            IUserHelper userHelper)
        {
            _context = context;
            this.combosHelper = combosHelper;
            this.imageHelper = imageHelper;
            this.userHelper = userHelper;
        }

        [Authorize(Roles = "Administrator, Owner")]
        // GET: Interns
        public IActionResult Index()
        {
            return View( _context.Interns
                .Include(s => s.User));
        }

        [Authorize(Roles = "Administrator, Owner")]
        // GET: Interns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("InternNotFound");
            }

            var intern = await _context.Interns
                .Include(s => s.User)
                .Include(s => s.Loans)
                .ThenInclude(c=>c.LoanDetails)
                .ThenInclude(v=>v.Status)
                .Include(s => s.Loans)
                .ThenInclude(c => c.LoanDetails)
                .ThenInclude(v => v.Material)
                .Include(s => s.Loans)
                .ThenInclude(c => c.LoanDetails)
                .ThenInclude(v => v.Loan)
                .ThenInclude(x=>x.Applicant)
                .ThenInclude(y=>y.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intern == null)
            {
                return new NotFoundViewResult("InternNotFound");
            }

            return View(intern);
        }

        [Authorize(Roles = "Administrator, Owner")]
        // GET: Interns/Create
        public IActionResult Create()
        {
            var model = new InternViewModel
            {
                Users = combosHelper.GetComboUsers()
            };

            return View(model);
        }

        // POST: Interns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InternViewModel model)
        {
            if (model.UserUserName != "[You have to choose a username...]")
            {
                var user = await userHelper.GetUserByNameAsync(model.UserUserName);

                if (user == null)
                {
                    return new NotFoundViewResult("InternNotFound");
                }

                var intern = new Intern { User = user, DepartureTime=model.DepartureTime, EntryTime=model.EntryTime };


                await userHelper.AddUserToRoleAsync(user, "Intern");

                _context.Add(intern);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [Authorize(Roles = "Administrator, Owner")]
        // GET: Interns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("InternNotFound");
            }

            var intern = await _context.Interns
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intern == null)
            {
                return new NotFoundViewResult("InternNotFound");
            }
            var model = new InternViewModel
            {
                Id = intern.Id,
                User = intern.User,
                UserUserName = intern.User.UserName,
                Users = combosHelper.GetComboUsers()
            };

            return View(model);
        }

        // POST: Interns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EntryTime,DepartureTime")] Intern intern)
        {
            if (id != intern.Id)
            {
                return new NotFoundViewResult("InternNotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intern);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternExists(intern.Id))
                    {
                        return new NotFoundViewResult("InternNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(intern);
        }

        [Authorize(Roles = "Administrator, Owner")]
        // GET: Interns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("InternNotFound");
            }

            var intern = await _context.Interns
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intern == null)
            {
                return new NotFoundViewResult("InternNotFound");
            }

            return View(intern);
        }

        // POST: Interns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intern = await _context.Interns.Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            await userHelper.RemoveUserFromRoleAsync(intern.User, "Intern");
            _context.Interns.Remove(intern);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InternExists(int id)
        {
            return _context.Interns.Any(e => e.Id == id);
        }
    }
}
