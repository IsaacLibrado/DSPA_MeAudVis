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
using DSPA_MeAudVis.Web.Models;

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
        public IActionResult Index()
        {
            return View(_context.Handbooks.Include(s => s.Owner).ThenInclude(c=>c.User));
        }

        // GET: Handbooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("HandbookNotFound");
            }

            var handbook = await _context.Handbooks.Include(s => s.Owner).ThenInclude(c => c.User)
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
            var model = new HandbookViewModel();

            return View(model);
        }

        // POST: Handbooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HandbookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var owner = _context.Owners.FirstOrDefault();
                var hb = new Handbook { Name = model.Name, Id = model.Id, Owner=owner };
                

                if (model.ImageFile != null)
                {
                    hb.ImageURL = await imageHelper.UploadImageAsync(model.ImageFile, hb.Name, "Manuales");
                }

                _context.Add(hb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Owner")]
        // GET: Handbooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("HandbookNotFound");
            }

            var handbook = await _context.Handbooks.Include(s => s.Owner).ThenInclude(c => c.User).FirstOrDefaultAsync(m => m.Id == id);
            if (handbook == null)
            {
                return new NotFoundViewResult("HandbookNotFound");
            }

            if (this.User.Identity.Name != handbook.Owner.User.UserName)
                return new NotFoundViewResult("HandbookNotFound");

            var hb = new HandbookViewModel { Id = handbook.Id, ImageURL = handbook.ImageURL, Name = handbook.Name, Owner = handbook.Owner };

            return View(hb);
        }

        // POST: Handbooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HandbookViewModel model)
        {
            if (id != model.Id)
            {
                return new NotFoundViewResult("HandbookNotFound");
            }

            if (ModelState.IsValid)
            {
                var handbook = await _context.Handbooks.FirstOrDefaultAsync(m => m.Id == model.Id);

                if (handbook == null)
                {
                    return new NotFoundViewResult("HandbookNotFound");
                }

                handbook.Id = model.Id;
                if (model.ImageFile != null)
                {
                    handbook.ImageURL = await imageHelper.UploadImageAsync(model.ImageFile, handbook.Name, "Manuales");
                }
                handbook.Name = model.Name;
                handbook.Owner = model.Owner;

                _context.Update(handbook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [Authorize(Roles = "Owner")]
        // GET: Handbooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("HandbookNotFound");
            }

            var handbook = await _context.Handbooks.Include(s => s.Owner).ThenInclude(c => c.User).FirstOrDefaultAsync(m => m.Id == id);

            if (handbook == null)
            {
                return new NotFoundViewResult("HandbookNotFound");
            }

            if (this.User.Identity.Name != handbook.Owner.User.UserName)
                return new NotFoundViewResult("HandbookNotFound");

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
