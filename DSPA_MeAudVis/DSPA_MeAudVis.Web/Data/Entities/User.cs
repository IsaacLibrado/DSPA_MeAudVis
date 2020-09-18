namespace DSPA_MeAudVis.Web.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Clase User para el inicio de sesión de los usuarios
    /// </summary>
    /// Version 2.0
    /// Fecha de creacion 7/09/20
    /// Fecha de Modificacion 14/09/20
    /// Creador Isaac Librado
    public class User : IdentityUser
    {
        //Propiedades

        [Required(ErrorMessage = "{0} is required")]
        [MinLength(10, ErrorMessage = "{0} must have minimun {1} charactes")]
        [MaxLength(30, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Password")]
        public string Password { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [MinLength(4, ErrorMessage = "{0} must have minimun {1} charactes")]
        [MaxLength(8, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Registration Number")]
        public int RegistrationNumber { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(15, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Name")]
        public string FirstName { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Last name")]
        public string LastName { set; get; }

        [Display(Name = "Complete name")]
        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }
}
