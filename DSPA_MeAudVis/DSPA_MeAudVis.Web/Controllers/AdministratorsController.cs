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
    public class AdministratorsController : Controller
    {
        private readonly DataContext _context;

        public AdministratorsController(DataContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrator")]
        // GET: Administrators
        public async Task<IActionResult> Index()
        {
            return View(await _context.Administrators.ToListAsync());
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
            return View();
        }

        [Authorize(Roles = "Administrator")]
        // POST: Administrators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administrator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "User doesn't exists");

            return View(administrator);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Administrators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("AdministratorNotFound");
            }

            var administrator = await _context.Administrators.FindAsync(id);
            if (administrator == null)
            {
                return new NotFoundViewResult("AdministratorNotFound");
            }
            return View(administrator);
        }

        [Authorize(Roles = "Administrator")]
        // POST: Administrators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Administrator administrator)
        {
            if (id != administrator.Id)
            {
                return new NotFoundViewResult("AdministratorNotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administrator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministratorExists(administrator.Id))
                    {
                        return new NotFoundViewResult("AdministratorNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(administrator);
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
            var administrator = await _context.Administrators.FindAsync(id);
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
