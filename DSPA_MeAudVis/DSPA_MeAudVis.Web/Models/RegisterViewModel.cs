namespace DSPA_MeAudVis.Web.Models
{
    using DSPA_MeAudVis.Web.Data.Entities;
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel : User
    {

        [Required(ErrorMessage = "{0} is required")]
        [MinLength(6, ErrorMessage = "{0} must have minimun {1} charactes")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
