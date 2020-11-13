using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DSPA_MeAudVis.Web.Data;
using DSPA_MeAudVis.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using DSPA_MeAudVis.Web.Helpers;

namespace DSPA_MeAudVis.Web.Controllers
{
    public class HandbooksController : Controller
    {
        private readonly DataContext _context;
        private readonly IImageHelper imageHelper;

        public HandbooksController(DataContext context, IImageHelper imageHelper)
        {
            _context = context;
            this.imageHelper = imageHelper;
        }

        // GET: Handbooks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Handbooks.ToListAsync());
        }

        // GET: Handbooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("HandbookNotFound");
            }

            var handbook = await _context.Handbooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (handbook == null)
            {
                return new NotFoundViewResult("HandbookNotFound");
            }

            return View(handbook);
        }

        [Authorize(Roles = "Owner")]
        // GET: Handbooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Handbooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Handbook handbook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(handbook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(handbook);
        }

        [Authorize(Roles = "Owner")]
        // GET: Handbooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("HandbookNotFound");
            }

            var handbook = await _context.Handbooks.FindAsync(id);
            if (handbook == null)
            {
                return new NotFoundViewResult("HandbookNotFound");
            }
            return View(handbook);
        }

        // POST: Handbooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageURL")] Handbook handbook)
        {
            if (id != handbook.Id)
            {
                return new NotFoundViewResult("HandbookNotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(handbook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HandbookExists(handbook.Id))
                    {
                        return new NotFoundViewResult("HandbookNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(handbook);
        }

        [Authorize(Roles = "Owner")]
        // GET: Handbooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("HandbookNotFound");
            }

            var handbook = await _context.Handbooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (handbook == null)
            {
                return new NotFoundViewResult("HandbookNotFound");
            }

            return View(handbook);
        }

        // POST: Handbooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var handbook = await _context.Handbooks.FindAsync(id);
            _context.Handbooks.Remove(handbook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HandbookExists(int id)
        {
            return _context.Handbooks.Any(e => e.Id == id);
        }
    }
}
