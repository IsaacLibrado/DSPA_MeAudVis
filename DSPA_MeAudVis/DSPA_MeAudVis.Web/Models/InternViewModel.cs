using DSPA_MeAudVis.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSPA_MeAudVis.Web.Models
{
    public class InternViewModel:Intern
    {
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, int.MaxValue, ErrorMessage = "You have to select an User")]
        [Display(Name = "User")]
        public string UserUserName { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
