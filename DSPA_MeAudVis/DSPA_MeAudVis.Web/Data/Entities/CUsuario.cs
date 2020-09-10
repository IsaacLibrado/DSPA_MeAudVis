namespace DSPA_MeAudVis.Web.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Clase User para el inicio de sesión de los usuarios
    /// </summary>
    /// Version 1.0
    /// Fecha de creacion 7/09/20
    /// Creador Isaac Librado
    public class CUsuario : IdentityUser
    {
        //Propiedades

        [Required(ErrorMessage = "{0} is required")]
        [MinLength(10, ErrorMessage = "{0} must have minimun {1} charactes")]
        [MaxLength(30, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Contraseña")]
        public string Contrasena { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [MinLength(4, ErrorMessage = "{0} must have minimun {1} charactes")]
        [MaxLength(8, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Matricula")]
        public int Matricula { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(15, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Nombre")]
        public string Nombre { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Apellido")]
        public string Apellido { set; get; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get { return $"{Nombre} {Apellido}"; } }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Tipo de Usuario")]
        public string TipoDeUsuario { set; get; }

        [Display(Name = "Manuales")]
        public ICollection<CManual> Manuales { set; get; }

        //Metodos
        public void InicioSesion()
        {

        }

        public void CierreSesion()
        {

        }

        public void ConsultaInformacion()
        {

        }

        public void CambiarContrasena()
        {

        }

        public void RevisionManuales()
        {

        }
    }
}
