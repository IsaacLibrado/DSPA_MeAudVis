namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Define a los solicitantes (maestros y alumnos) de material
    /// </summary>
    /// Version 1.1
    /// Fecha de creacion 08/09/20
    /// Creador Arturo Villegas
    /// Fecha de Modificacion 10/09/20
    /// Modificador Isaac Librado
    public class CSolicitante : IEntity
    {
        public int Id { set; get; }

        public CUsuario Usuario { set; get; }

        public bool Deudor { set; get; }

        public ICollection<CPrestamo> Prestamos { set; get; }

        //Metodos
        public void ConsultarInventario()
        {

        }
    }
}
