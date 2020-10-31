

namespace DSPA_MeAudVis.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DSPA_MeAudVis.Web.Data.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.ComponentModel.DataAnnotations;

    public class ApplicantViewModel:Applicant
    {
        [Display(Name = "Photo")]
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Range(1, int.MaxValue, ErrorMessage = "You have to select a type")]
        [Display(Name = "Type")]
        public int TypeId { get; set; }

        public IEnumerable<SelectListItem> Types { get; set; }
    }
}
