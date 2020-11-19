using DSPA_MeAudVis.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace DSPA_MeAudVis.Web.Models
{
    public class LoanViewModel:Loan
    {
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, int.MaxValue, ErrorMessage = "You have to select an applicant")]
        [Display(Name = "Applicant")]
        public int ApplicantId { get; set; }

        public IEnumerable<SelectListItem> Applicants { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Range(1, int.MaxValue, ErrorMessage = "You have to select a material")]
        [Display(Name = "Material")]
        public int MaterialId { get; set; }

        public IEnumerable<SelectListItem> Materials { get; set; }


    }
}
