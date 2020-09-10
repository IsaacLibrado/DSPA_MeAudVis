namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System.Collections.Generic;

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
        public int Id { set; get; }

        public CUsuario Usuario { set; get; }

        public ICollection<CPrestamo> Prestamos { set; get; }

        //Metodos
        public void PrestamoDeMateria()
        {

        }

        public void RecepcionDeMaterial()
        {

        }
    }
}
