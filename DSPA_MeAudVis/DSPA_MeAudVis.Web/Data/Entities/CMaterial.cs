using DSPA_MeAudVis.Web.Controllers.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSPA_MeAudVis.Web.Data.Entities
{
    public class CMaterial : IEntity
    {
        public int Id { set; get; }
        public string Nombre { set; get; }
        public string Etiqueta { set; get; }
        public string Marca { set; get; }
        public string Modelo { set; get; }
        public string NumSerie { set; get; } 
    }
}
