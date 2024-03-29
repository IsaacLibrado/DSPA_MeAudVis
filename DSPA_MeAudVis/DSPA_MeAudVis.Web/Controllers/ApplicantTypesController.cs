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

namespace DSPA_MeAudVis.Web.Controllers
{
    public class ApplicantTypesController : Controller
    {
        private readonly DataContext _context;

        public ApplicantTypesController(DataContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrator")]
        // GET: ApplicantTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicantTypes.ToListAsync());
        }

        [Authorize(Roles = "Administrator")]
        // GET: ApplicantTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicantTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] ApplicantType applicantType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicantType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicantType);
        }

        [Authorize(Roles = "Administrator")]
        // GET: ApplicantTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ApplicantTypeNotFound");
            }

            var applicantType = await _context.ApplicantTypes.FindAsync(id);
            if (applicantType == null)
            {
                return new NotFoundViewResult("ApplicantTypeNotFound");
            }
            return View(applicantType);
        }

        // POST: ApplicantTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] ApplicantType applicantType)
        {
            if (id != applicantType.Id)
            {
                return new NotFoundViewResult("ApplicantTypeNotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantTypeExists(applicantType.Id))
                    {
                        return new NotFoundViewResult("ApplicantTypeNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(applicantType);
        }

        [Authorize(Roles = "Administrator")]
        // GET: ApplicantTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ApplicantTypeNotFound");
            }

            var applicantType = await _context.ApplicantTypes
                .Include(s=>s.Applicants)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantType == null)
            {
                return new NotFoundViewResult("ApplicantTypeNotFound");
            }

            if (applicantType.Applicants.Count != 0)
            {
                ModelState.AddModelError(string.Empty, "This type is used in one or more applicant, delete them first before deleting this.");
                return RedirectToAction("Index", "ApplicantTypes");
            }

            return View(applicantType);
        }

        // POST: ApplicantTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicantType = await _context.ApplicantTypes.FindAsync(id);
            _context.ApplicantTypes.Remove(applicantType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantTypeExists(int id)
        {
            return _context.ApplicantTypes.Any(e => e.Id == id);
        }
    }
}
