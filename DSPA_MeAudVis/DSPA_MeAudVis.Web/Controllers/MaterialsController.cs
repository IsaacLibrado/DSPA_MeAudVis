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
    public class MaterialsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper combosHelper;
        private readonly IImageHelper imageHelper;
        private readonly IUserHelper userHelper;

        public MaterialsController(DataContext context,
            ICombosHelper combosHelper,
            IImageHelper imageHelper,
            IUserHelper userHelper)
        {
            _context = context;
            this.combosHelper = combosHelper;
            this.imageHelper = imageHelper;
            this.userHelper = userHelper;
        }

        // GET: Materials
        public IActionResult Index()
        {
            return View( _context.Materials.Include(s => s.Status));
        }

        // GET: Materials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("MaterialNotFound");
            }

            var material = await _context.Materials
                .Include(s => s.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return new NotFoundViewResult("MaterialNotFound");
            }

            return View(material);
        }

        [Authorize(Roles = "Owner")]
        // GET: Materials/Create
        public IActionResult Create()
        {
            var model = new MaterialViewModel
            {
                Statuses = combosHelper.GetComboStatuses()
            };

            return View(model);
        }

        // POST: Materials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MaterialViewModel model)
        {
            if (ModelState.IsValid)
            {
                var status =await  _context.Statuses.FirstOrDefaultAsync(m => m.Id == model.Status.Id);
                var Material = new Material { Brand=model.Brand, Id=model.Id, Label=model.Label, LoanDetails=model.LoanDetails, Model=model.Model, Name=model.Name, SerialNum=model.SerialNum, Status=status};

                _context.Add(Material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [Authorize(Roles = "Owner")]
        // GET: Materials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("MaterialNotFound");
            }

            var material = await _context.Materials
                .Include(s => s.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return new NotFoundViewResult("MaterialNotFound");
            }

            var model = new MaterialViewModel
            {
                Id=material.Id,
                Brand=material.Brand,
                Name=material.Name,
                Model=material.Model,
                Status=material.Status,
                Label=material.Label,
                SerialNum=material.SerialNum,
                Statuses= combosHelper.GetComboStatuses()
            };

            return View(model);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MaterialViewModel model)
        {
            if (id != model.Id)
            {
                return new NotFoundViewResult("MaterialNotFound");
            }

            if (ModelState.IsValid)
            {
                var material = await _context.Materials.FirstOrDefaultAsync(m => m.Id == model.Id);

                if (material == null)
                {
                    return new NotFoundViewResult("MaterialNotFound");
                }

                material.Id = model.Id;
                material.Brand = model.Brand;
                material.Name = model.Name;
                material.Model = model.Model;
                material.Label = model.Label;
                material.SerialNum = model.SerialNum;

                var status = await _context.Statuses.FirstOrDefaultAsync(m => m.Id == model.Status.Id);
                material.Status = status; 

                _context.Update(material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [Authorize(Roles = "Owner")]
        // GET: Materials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("MaterialNotFound");
            }

            var material = await _context.Materials
                .Include(s => s.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return new NotFoundViewResult("MaterialNotFound");
            }

            return View(material);
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialExists(int id)
        {
            return _context.Materials.Any(e => e.Id == id);
        }
    }
}
