using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSPA_MeAudVis.Web.Controllers.Data.Entities;

namespace DSPA_MeAudVis.Web.Data.Entities
{
    public class CPrestamo : IEntity
    {
        public int Id { set; get; }
        public CMaterial material { set; get; }
        public CBecario becarioSalida { set; get; }
        public CBecario becarioEntrega { set; get; }
        public CSolicitante solicitante { set; get; }
        public DateTime FechaHoraSalida { set; get; }
        public DateTime FechaHoraEntrega { set; get; }

    }
}
