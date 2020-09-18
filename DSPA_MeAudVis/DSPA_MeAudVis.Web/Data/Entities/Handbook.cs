namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define a los manuales para los usuarios
    /// </summary>
    /// Version 2.0
    /// Fecha de creacion 7/09/20
    /// Fecha de Modificacion 14/09/20
    /// Creador Isaac Librado
    public class Handbook : IEntity
    {
        [Required(ErrorMessage = "{0} is required")]
        public int Id { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(100, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Handbook name")]
        public string Name { set; get; }

        //Pendiente para imagenes

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Handbook content")]
        public string Content { set; get; }

        [Display(Name = "Handbooks")]
        public Administrator Administrator { set; get; }
    }
}
