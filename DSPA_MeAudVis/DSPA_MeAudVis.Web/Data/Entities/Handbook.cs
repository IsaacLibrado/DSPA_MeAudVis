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
        public int Id { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(100, ErrorMessage = "{0} must have maximun {1} characters")]
        [Display(Name = "Handbook name")]
        public string Name { set; get; }

        [Display(Name = "Handbook content")]
        public string ImageURL { set; get; }


        public Owner Owner { set; get; }
    }
}
