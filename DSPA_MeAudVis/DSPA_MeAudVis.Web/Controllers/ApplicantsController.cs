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
    public class ApplicantsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper combosHelper;
        private readonly IImageHelper imageHelper;
        private readonly IUserHelper userHelper;

        public ApplicantsController(DataContext context, 
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
        // GET: Applicants
        public IActionResult Index()
        {
            return View(_context.Applicants
                .Include(s => s.User)
                .Include(s => s.Type));
        }

        [Authorize(Roles = "Administrator, Owner")]
        // GET: Applicants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ApplicantNotFound");
            }

            var applicant = await _context.Applicants
                .Include(s => s.User)
                .Include(s => s.Loans)
                .ThenInclude(c => c.LoanDetails)
                .ThenInclude(v => v.Status)
                .Include(s => s.Loans)
                .ThenInclude(c => c.LoanDetails)
                .ThenInclude(v => v.Material)
                .Include(s => s.Loans)
                .ThenInclude(c => c.LoanDetails)
                .ThenInclude(v => v.Loan)
                .ThenInclude(x => x.Intern)
                .ThenInclude(y => y.User)
                .Include(s=>s.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicant == null)
            {
                return new NotFoundViewResult("ApplicantNotFound");
            }

            return View(applicant);
        }

        [Authorize(Roles = "Administrator, Owner")]
        // GET: Applicants/Create
        public IActionResult Create()
        {
            var model = new ApplicantViewModel
            {
                Users = combosHelper.GetComboUsers(),
                Types = combosHelper.GetComboApplicantTypes()
            };

            return View(model);
        }

        // POST: Applicants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicantViewModel model)
        {
            if (model.UserUserName != "[You have to choose a username...]" && model.TypeId!= 0)
            {
                var user = await userHelper.GetUserByNameAsync(model.UserUserName);

                if (user == null)
                {
                    return new NotFoundViewResult("ApplicantNotFound");
                }

                foreach (Applicant appTemp in _context.Applicants.Include(c => c.User))
                {
                    if (appTemp.User == user)
                    {
                        ModelState.AddModelError(string.Empty, "Applicant already exists");
                        return View(model);
                    }
                }

                var type = await _context.ApplicantTypes.FirstOrDefaultAsync(m => m.Id == model.TypeId);
                var applicant = new Applicant { User = user, Type=type, Debtor=model.Debtor};


                await userHelper.AddUserToRoleAsync(user, "Applicant");

                _context.Add(applicant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [Authorize(Roles = "Administrator, Owner")]
        // GET: Applicants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ApplicantNotFound");
            }

            var applicant = await _context.Applicants
                .Include(s => s.User)
                .Include(s => s.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicant == null)
            {
                return new NotFoundViewResult("ApplicantNotFound");
            }

            var model = new ApplicantViewModel
            {
                Id = applicant.Id,
                User = applicant.User,
                Debtor = applicant.Debtor,
                TypeId = applicant.Type.Id,
                Types = combosHelper.GetComboApplicantTypes(),
                Type = applicant.Type,
                UserUserName=applicant.User.UserName,
                Users=combosHelper.GetComboUsers()
            };

            return View(model);
        }

        // POST: Applicants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ApplicantViewModel model)
        {
            if (id != model.Id)
            {
                return new NotFoundViewResult("ApplicantNotFound");
            }

            if (model.TypeId>0)
            {

                var applicant = await _context.Applicants.FindAsync(model.Id);

                if(applicant==null)
                {
                    return new NotFoundViewResult("ApplicantNotFound");
                }

                applicant.Id = model.Id;
                applicant.Debtor = model.Debtor;
                applicant.Type = await _context.ApplicantTypes.FindAsync(model.TypeId);
                _context.Update(applicant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);

        }

        [Authorize(Roles = "Administrator, Owner")]
        // GET: Applicants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ApplicantNotFound");
            }

            var applicant = await _context.Applicants
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicant == null)
            {
                return new NotFoundViewResult("ApplicantNotFound");
            }

            return View(applicant);
        }

        // POST: Applicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicant = await _context.Applicants.Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            await userHelper.RemoveUserFromRoleAsync(applicant.User, "Applicant");
            _context.Applicants.Remove(applicant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantExists(int id)
        {
            return _context.Applicants.Any(e => e.Id == id);
        }
    }
}
