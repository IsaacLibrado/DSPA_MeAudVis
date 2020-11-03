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

        // GET: Applicants
        public IActionResult Index()
        {
            return View(_context.Applicants
                .Include(s => s.User)
                .Include(s => s.Type));
        }

        // GET: Applicants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants
                .Include(s => s.User)
                .Include(s => s.Type)
                .Include(s=>s.Loans)
                .ThenInclude(a=>a.Intern)
                .ThenInclude(c=>c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // GET: Applicants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Applicants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Debtor")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicant);
        }

        // GET: Applicants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants
                .Include(s => s.User)
                .Include(s => s.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicant == null)
            {
                return NotFound();
            }

            var model = new ApplicantViewModel
            {
                Id = applicant.Id,
                User = applicant.User,
                ImageURL = applicant.ImageURL,
                Debtor = applicant.Debtor,
                TypeId = applicant.Type.Id,
                Types = combosHelper.GetComboApplicantTypes(),
                Type = applicant.Type
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
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await userHelper.GetUserByEmailAsync(model.User.Email);

                if(user==null)
                {
                    return NotFound();
                }

                user.FirstName = model.User.FirstName;
                user.LastName = model.User.LastName;
                user.PhoneNumber = model.User.PhoneNumber;
                user.RegistrationNumber = model.User.RegistrationNumber;
                user.Email = model.User.Email;
                user.UserName = model.User.UserName;
                var applicant = await _context.Applicants.FindAsync(model.Id);

                if(applicant==null)
                {
                    return NotFound();
                }

                if(model.ImageFile!=null)
                {
                    applicant.ImageURL = await imageHelper.UploadImageAsync(model.ImageFile, model.User.FullName, "FotosEstudiantes");
                }
                applicant.Id = model.Id;
                applicant.Debtor = model.Debtor;
                applicant.User = user;
                applicant.Type = await _context.ApplicantTypes.FindAsync(model.TypeId);
                _context.Update(applicant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);

        }

        // GET: Applicants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // POST: Applicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicant = await _context.Applicants.FindAsync(id);
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
