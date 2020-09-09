namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    public class CBecario : IEntity
    {
        public int Id { set; get; }
        public CUsuario Usuario { set; get; }

        public void PrestamoDeMateria()
        {

        }

        public void RecepcionDeMaterial()
        {

        }
    }
}
