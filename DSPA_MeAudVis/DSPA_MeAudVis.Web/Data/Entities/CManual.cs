namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define a los manuales para los usuarios
    /// </summary>
    /// Version 1.0
    /// Fecha de creacion 7/09/20
    /// Creador Isaac Librado
    public class CManual : IEntity
    {
        [Required(ErrorMessage = "{0} is required")]
        public int Id { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(100, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Nombre del Manual")]
        public string Nombre { set; get; }

        //Pendiente para imagenes

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Contenido del manual")]
        public string Contenido { set; get; }

        [Display(Name = "Manuales")]
        public ICollection<CUsuario> Usuarios { set; get; }
    }
}
