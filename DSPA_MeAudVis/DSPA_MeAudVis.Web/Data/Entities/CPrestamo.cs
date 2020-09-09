namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System;
    using System.Collections.Generic;

    public class CPrestamo : IEntity
    {
        public int Id { set; get; }
        public ICollection<CMaterial> Materiales { set; get; }
        public CBecario becarioSalida { set; get; }
        public CBecario becarioEntrega { set; get; }
        public CSolicitante solicitante { set; get; }
        public DateTime FechaHoraSalida { set; get; }
        public DateTime FechaHoraEntrega { set; get; }

    }
}
