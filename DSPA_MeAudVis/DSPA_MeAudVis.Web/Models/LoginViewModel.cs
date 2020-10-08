namespace DSPA_MeAudVis.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        [MinLength(4, ErrorMessage = "{0} must have minimun {1} characters")]
        [EmailAddress]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MinLength(4, ErrorMessage = "{0} must have minimun {1} characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
