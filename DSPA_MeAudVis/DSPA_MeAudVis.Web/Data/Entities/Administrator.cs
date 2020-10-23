namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define a los adminisradores del sistema
    /// </summary>
    /// Version 2.0
    /// Fecha de creacion 08/09/20
    /// Fecha de Modificacion 14/09/20
    /// Creador Arturo Villegas
    public class Administrator : IEntity
    {
        [Required(ErrorMessage = "{0} is required")]
        public int Id { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        public User User { set; get; }
    }
}
