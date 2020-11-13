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

        [Authorize(Roles = "Owner, Administrator")]
        public IActionResult Index()
        {
            return View(_context.LoanDetails
                .Include(s => s.Status)
                .Include(s => s.Material));
        }

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
            var loanDetail = await _context.LoanDetails.FindAsync(id);
            _context.LoanDetails.Remove(loanDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

            return View(model);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LoanDetailViewModel model)
        {

            if (ModelState.IsValid)
            {
                var ld = await _context.LoanDetails.FirstOrDefaultAsync(m => m.Id == model.Id);

                if (ld == null)
                {
                    return new NotFoundViewResult("LoanDetailNotFound");
                }

                ld.Observations = model.Observations;
                ld.DateTimeIn = model.DateTimeIn;
                ld.DateTimeOut = model.DateTimeOut;

                var status = await _context.Statuses.FirstOrDefaultAsync(m => m.Id == model.StatusId);
                ld.Status = status;

                var material = await _context.Materials.FirstOrDefaultAsync(m => m.Id == model.MaterialId);
                ld.Material = material;

                _context.Update(ld);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
