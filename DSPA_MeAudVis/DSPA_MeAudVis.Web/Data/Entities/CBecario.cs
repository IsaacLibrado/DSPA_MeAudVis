namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define a los becarios que usan el sistema
    /// </summary>
    /// Version 1.0
    /// Fecha de creacion 08/09/20
    /// Creador Arturo Villegas
    /// Fecha de Modificacion 10/09/20
    /// Modificador Isaac Librado
    public class CBecario : IEntity
    {
        [Required(ErrorMessage = "{0} is required")]
        public int Id { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        public CUsuario Usuario { set; get; }

        //Metodos
        public void PrestamoDeMateria()
        {

        }

        public void RecepcionDeMaterial()
        {

        }
    }
}
