namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    public class CSolicitante : IEntity
    {

        public int Id { set; get; }
        public CUsuario Usuario { set; get; }
        public bool Deudor { set; get; }

        public void ConsultarInventario()
        {

        }
    }
}
