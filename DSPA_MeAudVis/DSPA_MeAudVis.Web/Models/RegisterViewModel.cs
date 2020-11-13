namespace DSPA_MeAudVis.Web.Models
{
    using DSPA_MeAudVis.Web.Data.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Linq;
    using System.Threading.Tasks;


    public class RegisterViewModel : User
    {
        [Display(Name = "Photo")]
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MinLength(6, ErrorMessage = "{0} must have minimun {1} characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Role")]
        public string RoleName { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
