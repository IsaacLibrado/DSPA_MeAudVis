﻿using System;
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
    public class LoansController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper combosHelper;
        private readonly IImageHelper imageHelper;
        private readonly IUserHelper userHelper;

        public LoansController(DataContext context,
            ICombosHelper combosHelper,
            IImageHelper imageHelper,
            IUserHelper userHelper)
        {
            _context = context;
            this.combosHelper = combosHelper;
            this.imageHelper = imageHelper;
            this.userHelper = userHelper;
        }

        [Authorize(Roles = "Intern, Applicant, Owner, Administrator")]
        // GET: Loans
        public  IActionResult Index()
        {
            var applicant= _context.Applicants.FirstOrDefault();

            applicant = null;

            foreach (Applicant applicantObj in _context.Applicants.Include(s => s.User).Include(c=>c.Loans))
            {
                if (applicantObj.User.UserName == this.User.Identity.Name)
                {
                    applicant = applicantObj;
                }
            }

            if (applicant!=null && this.User.IsInRole("Applicant"))
            {
                if(applicant.Loans==null || applicant.Loans.Count==0)
                    return new NotFoundViewResult("LoanNotFound");
            }

            return View(_context.Loans
                 .Include(s => s.Applicant)
                 .ThenInclude(c => c.User)
                 .Include(s => s.Intern).ThenInclude(c => c.User)
                 .Include(s => s.LoanDetails).ThenInclude(c => c.Material)
                 .Include(s => s.LoanDetails).ThenInclude(c => c.Status));
        }

        [Authorize(Roles = "Intern, Applicant, Owner, Administrator")]
        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("LoanNotFound");
            }

            var loan = await _context.Loans
                .Include(s => s.Applicant)
                .ThenInclude(c => c.User)
                .Include(s => s.Intern).ThenInclude(c => c.User)
                .Include(c => c.LoanDetails)
                .ThenInclude(v => v.Status)
                .Include(c => c.LoanDetails)
                .ThenInclude(v => v.Material)
                .Include(c => c.LoanDetails)
                .ThenInclude(v => v.Loan)
                .ThenInclude(x => x.Applicant)
                .ThenInclude(y => y.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loan == null)
            {
                return new NotFoundViewResult("LoanNotFound");
            }

            if (this.User.Identity.Name != loan.Intern.User.UserName && !this.User.IsInRole("Owner") && !this.User.IsInRole("Administrator"))
                return new NotFoundViewResult("LoanNotFound");

            return View(loan);
        }

        [Authorize(Roles = "Intern")]
        // GET: Loans/Create
        public IActionResult Create()
        {
            var model = new LoanViewModel
            {
                ApplicantId = 1,
                Applicants = combosHelper.GetComboApplicants(),
                Materials = combosHelper.GetComboMaterials(),
                MaterialId=1
            };

            return View(model);
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanViewModel model)
        {
            if (ModelState.IsValid)
            {
                var applicant = await _context.Applicants.FirstOrDefaultAsync(m => m.Id == model.ApplicantId);
                var intern = await _context.Interns.FirstOrDefaultAsync();
                foreach (Intern internObj in _context.Interns.Include(s => s.User))
                {
                    if (internObj.User.UserName == this.User.Identity.Name)
                    {
                        intern = internObj;
                    }
                }
                var loan = new Loan {Applicant=applicant, Intern=intern};

                var status = _context.Statuses.FirstOrDefault(m => m.Id == 2);
                var material = await _context.Materials.FirstOrDefaultAsync(m => m.Id == model.MaterialId);
                _context.LoanDetails.Add(new LoanDetail { Loan = loan, DateTimeOut = DateTime.Now, DateTimeIn=DateTime.MinValue, Material = material, Status = status, Observations = string.Empty });

                material.Status = status;
                applicant.Debtor = true;

                _context.Materials.Update(material);
                _context.Applicants.Update(applicant);
                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [Authorize(Roles = "Owner")]
        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("LoanNotFound");
            }

            var loan = await _context.Loans
                .Include(s => s.Applicant)
                .ThenInclude(c => c.User)
                .Include(s => s.Intern).ThenInclude(c => c.User)
                .Include(c => c.LoanDetails)
                .ThenInclude(v => v.Status)
                .Include(c => c.LoanDetails)
                .ThenInclude(v => v.Material)
                .Include(c => c.LoanDetails)
                .ThenInclude(v => v.Loan)
                .ThenInclude(x => x.Applicant)
                .ThenInclude(y => y.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loan == null)
            {
                return new NotFoundViewResult("LoanNotFound");
            }

            if (loan.LoanDetails.Count != 0)
            {
                ModelState.AddModelError(string.Empty, "This loan has details, delete them first before deleting this.");
                return RedirectToAction("Index", "Loans");
            }

            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.Id == id);
        }
    }
}
