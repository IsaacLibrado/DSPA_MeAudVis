using DSPA_MeAudVis.Web.Controllers.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSPA_MeAudVis.Web.Data.Entities
{
    public class CManual : IEntity
    {
        public int Id { set; get; }

        public string Nombre { set; get; }

        //Pendiente para imagenes
        public string Contenido { set; get; }
    }
}
