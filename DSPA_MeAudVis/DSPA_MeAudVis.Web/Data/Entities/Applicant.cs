namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Define a los solicitantes (maestros y alumnos) de material
    /// </summary>
    /// Version 2.1
    /// Fecha de creacion 08/09/20
    /// Creador Arturo Villegas
    /// Fecha de Modificacion 14/09/20
    /// Modificador Isaac Librado
    public class Applicant : IEntity
    {
        public int Id { set; get; }

        public User User { set; get; }

        public bool Debtor { set; get; }
    }
}
