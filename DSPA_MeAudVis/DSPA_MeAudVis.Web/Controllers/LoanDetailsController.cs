namespace DSPA_MeAudVis.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DSPA_MeAudVis.Web.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class LoanDetailsController : Controller
    {
        private readonly DataContext _context;

        public LoanDetailsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.LoanDetails.ToListAsync());
        }
    }
}
