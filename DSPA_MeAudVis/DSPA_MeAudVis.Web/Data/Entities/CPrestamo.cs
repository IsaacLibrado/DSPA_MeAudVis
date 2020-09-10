namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Define a los prestamos que se realizan en el sistema
    /// </summary>
    /// Version 1.1
    /// Fecha de creacion 09/09/20
    /// Creador David Hernandez
    /// Fecha de Modificacion 10/09/20
    /// Modificador Isaac Librado
    public class CPrestamo : IEntity
    {
        public int Id { set; get; }
        
        public ICollection<CMaterial> Materiales { set; get; }

        public CBecario becarioSalida { set; get; }
        
        public CBecario becarioEntrega { set; get; }
        
        public CSolicitante solicitante { set; get; }
        
        public DateTime FechaHoraSalida { set; get; }
        
        public DateTime FechaHoraEntrega { set; get; }
        
        public bool Estado { set; get; }

    }
}
