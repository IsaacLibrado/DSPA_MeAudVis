using DSPA_MeAudVis.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSPA_MeAudVis.Web.Models
{
    public class MaterialViewModel:Material 
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Status")]
        public string StatusStatusName { get; set; }

        public IEnumerable<SelectListItem> Statuses { get; set; }
    }
}
