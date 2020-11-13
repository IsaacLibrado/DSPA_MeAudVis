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

        // GET: Loans
        public IActionResult Index()
        {
            return View( _context.Loans
                .Include(s => s.Applicant)
                .ThenInclude(c=>c.User)
                .Include(s => s.Intern).ThenInclude(c => c.User)
                .Include(s=>s.LoanDetails).ThenInclude(c=>c.Material));
        }

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

            return View(loan);
        }

        [Authorize(Roles = "Intern")]
        // GET: Loans/Create
        public IActionResult Create()
        {
            var model = new LoanViewModel
            {
                InternId = 1,
                ApplicantId = 1,
                Applicants = combosHelper.GetComboApplicants(),
                Interns = combosHelper.GetComboInterns()
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
                var intern = await _context.Interns.FirstOrDefaultAsync(m => m.Id == model.InternId);
                var loan = new Loan {Applicant=applicant, Intern=intern};


                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [Authorize(Roles = "Intern")]
        // GET: Loans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("LoanNotFound");
            }

            var loan = await _context.Loans
                .Include(s => s.Intern)
                .Include(s=>s.Applicant)
                .Include(s=>s.LoanDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loan == null)
            {
                return new NotFoundViewResult("LoanNotFound");
            }


            var model = new LoanViewModel
            {
                Id = loan.Id,
                Intern = loan.Intern,
                Applicant = loan.Applicant,
                LoanDetails = loan.LoanDetails,
                InternId = loan.Intern.Id,
                ApplicantId = loan.Applicant.Id,
                Applicants = combosHelper.GetComboApplicants(),
                Interns=combosHelper.GetComboInterns()
            };

            return View(model);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LoanViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loan = await _context.Loans.FirstOrDefaultAsync(m => m.Id == model.Id);

                if (loan== null)
                {
                    return new NotFoundViewResult("LoanNotFound");
                }

                var applicant = await _context.Applicants.FirstOrDefaultAsync(m => m.Id == model.ApplicantId);
                loan.Applicant = applicant;

                var intern = await _context.Interns.FirstOrDefaultAsync(m => m.Id == model.InternId);
                loan.Intern = intern;

                _context.Update(loan);
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loan == null)
            {
                return new NotFoundViewResult("LoanNotFound");
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
