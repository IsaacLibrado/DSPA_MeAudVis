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

namespace DSPA_MeAudVis.Web.Controllers
{
    public class StatusesController : Controller
    {
        private readonly DataContext _context;

        public StatusesController(DataContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Owner")]
        // GET: Statuses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Statuses.ToListAsync());
        }

        [Authorize(Roles = "Owner")]
        // GET: Statuses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("StatusNotFound");
            }

            var status = await _context.Statuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (status == null)
            {
                return new NotFoundViewResult("StatusNotFound");
            }

            return View(status);
        }

        [Authorize(Roles = "Owner")]
        // GET: Statuses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Statuses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StatusName")] Status status)
        {
            if (ModelState.IsValid)
            {
                _context.Add(status);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        [Authorize(Roles = "Owner")]
        // GET: Statuses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("StatusNotFound");
            }

            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return new NotFoundViewResult("StatusNotFound");
            }
            return View(status);
        }

        // POST: Statuses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StatusName")] Status status)
        {
            if (id != status.Id)
            {
                return new NotFoundViewResult("StatusNotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(status);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusExists(status.Id))
                    {
                        return new NotFoundViewResult("StatusNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        [Authorize(Roles = "Owner")]
        // GET: Statuses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("StatusNotFound");
            }

            var status = await _context.Statuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (status == null)
            {
                return new NotFoundViewResult("StatusNotFound");
            }

            return View(status);
        }

        // POST: Statuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusExists(int id)
        {
            return _context.Statuses.Any(e => e.Id == id);
        }

        public IActionResult StatusNotFound()
        {
            return View();
        }
    }
}
