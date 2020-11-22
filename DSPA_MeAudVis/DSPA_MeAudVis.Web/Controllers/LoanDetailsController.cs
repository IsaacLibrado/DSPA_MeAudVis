namespace DSPA_MeAudVis.Web.Controllers
{
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

    public class LoanDetailsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper combosHelper;
        private readonly IImageHelper imageHelper;
        private readonly IUserHelper userHelper;

        public LoanDetailsController(DataContext context,
            ICombosHelper combosHelper,
            IImageHelper imageHelper,
            IUserHelper userHelper)
        {
            _context = context;
            this.combosHelper = combosHelper;
            this.imageHelper = imageHelper;
            this.userHelper = userHelper;
        }


        ///
        [Authorize(Roles = "Intern")]
        // GET: Loans/Create
        public IActionResult Create(int? id)
        {
            var model = new LoanDetailViewModel
            {
                StatusId = 2,
                Statuses = combosHelper.GetComboStatuses(),
                Materials = combosHelper.GetComboMaterials(),
                MaterialId = 1,
                Loan=_context.Loans.Include(s=>s.Intern).ThenInclude(c=>c.User).FirstOrDefault(m => m.Id == id),
            };

            if (this.User.Identity.Name != model.Loan.Intern.User.UserName)
                return new NotFoundViewResult("LoanDetailNotFound");

            model.LoanID = model.Loan.Id;

            return View(model);
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanDetailViewModel model)
        {
            var status = _context.Statuses.FirstOrDefault(m => m.Id == 2);
            var material = await _context.Materials.FirstOrDefaultAsync(m => m.Id == model.MaterialId);
            var loan = await _context.Loans.FirstOrDefaultAsync(m => m.Id == model.LoanID);

            material.Status = status;

            _context.Materials.Update(material);

            var ld = new LoanDetail { Loan = loan, DateTimeOut = DateTime.Now, DateTimeIn = DateTime.MinValue, Material = material, Status = status, Observations = string.Empty };


            _context.LoanDetails.Add(ld);
            await _context.SaveChangesAsync();
            return RedirectToAction(String.Format("Details/{0}", ld.Loan.Id), "Loans");
        }
        ///

        [Authorize(Roles = "Intern")]
        // GET: Materials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("LoanDetailNotFound");
            }

            var loandetail = await _context.LoanDetails
                .Include(s => s.Status)
                .Include(s=>s.Material)
                .Include(s=>s.Loan)
                .ThenInclude(c=>c.Applicant)
                .ThenInclude(v=>v.User)
                .Include(s => s.Loan)
                .ThenInclude(c => c.Intern)
                .ThenInclude(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loandetail == null)
            {
                return new NotFoundViewResult("LoanDetailNotFound");
            }

            if(this.User.Identity.Name!=loandetail.Loan.Intern.User.UserName && !this.User.IsInRole("Owner") && !this.User.IsInRole("Administrator"))
                return new NotFoundViewResult("LoanDetailNotFound");

            return View(loandetail);
        }

        [Authorize(Roles = "Owner")]
        // GET: Materials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("LoanDetailNotFound");
            }

            var loandetail = await _context.LoanDetails
                .Include(s => s.Status)
                .Include(s => s.Material)
                .Include(s => s.Loan)
                .ThenInclude(c => c.Applicant)
                .ThenInclude(v => v.User)
                .Include(s => s.Loan)
                .ThenInclude(c => c.Intern)
                .ThenInclude(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loandetail == null)
            {
                return new NotFoundViewResult("LoanDetailNotFound");
            }

            return View(loandetail);
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loanDetail = await _context.LoanDetails.Include(s => s.Loan).FirstOrDefaultAsync(m => m.Id == id);
            _context.LoanDetails.Remove(loanDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(String.Format("Details/{0}", loanDetail.Loan.Id), "Loans");
        }

        private bool MaterialExists(int id)
        {
            return _context.Materials.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Intern")]
        // GET: Materials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("LoanDetailNotFound");
            }

            var loanDetail = await _context.LoanDetails
                .Include(s => s.Status)
                .Include(s=>s.Material)
                .Include(s => s.Loan)
                .ThenInclude(c => c.Intern)
                .ThenInclude(v => v.User)
                .Include(s => s.Loan)
                .ThenInclude(c => c.Applicant)
                .ThenInclude(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loanDetail == null)
            {
                return new NotFoundViewResult("LoanDetailNotFound");
            }

            var model = new LoanDetailViewModel
            {
                Id = loanDetail.Id,
                Observations=loanDetail.Observations,
                DateTimeIn=loanDetail.DateTimeIn,
                DateTimeOut=loanDetail.DateTimeOut,
                Material=loanDetail.Material,
                Status=loanDetail.Status,
                Loan=loanDetail.Loan,
                Statuses=combosHelper.GetComboStatuses(),
                Materials=combosHelper.GetComboMaterials()
            };

            if (this.User.Identity.Name != loanDetail.Loan.Intern.User.UserName && !this.User.IsInRole("Owner") && !this.User.IsInRole("Administrator"))
                return new NotFoundViewResult("LoanDetailNotFound");

            return View(model);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LoanDetailViewModel model)
        {

            var ld = await _context.LoanDetails
                .Include(s=>s.Loan)
                .ThenInclude(c=>c.Applicant)
                .ThenInclude(x=>x.Loans)
                .ThenInclude(y=>y.LoanDetails)
                .ThenInclude(z=>z.Status)
                .Include(s => s.Material)
                .ThenInclude(c=>c.Status)
                .FirstOrDefaultAsync(m => m.Id == model.Id);

            if (ld == null)
            {
                return new NotFoundViewResult("LoanDetailNotFound");
            }

            ld.Observations = model.Observations;
            ld.DateTimeIn = DateTime.Now;

            var status = await _context.Statuses.FirstOrDefaultAsync(m => m.Id == 3);
            ld.Status = status;

            var debtor = false;

            foreach(Loan loanApp in ld.Loan.Applicant.Loans)
            {
                foreach (LoanDetail loanDetails in loanApp.LoanDetails)
                {
                    if(loanDetails.Status.Id==2)
                    {
                        debtor = true;
                    }
                }
            }

            status = await _context.Statuses.FirstOrDefaultAsync(m => m.Id == 1);
            ld.Material.Status = status;

            ld.Loan.Applicant.Debtor = debtor;

            _context.Applicants.Update(ld.Loan.Applicant);
            _context.Materials.Update(ld.Material);
            _context.Update(ld);
            await _context.SaveChangesAsync();
            return RedirectToAction(String.Format("Details/{0}",ld.Loan.Id), "Loans");

        }
    }
}
