namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    public class CAdministrador : IEntity
    {
        public int Id { set; get; }
        public CUsuario Usuario { set; get; }

        public void AltaDeMaterial()
        {

        }

        public void BajaDeMaterial()
        {

        }

        public void AltaDeBecarios()
        {

        }

        public void BajaDeBecarios()
        {

        }
    }
}
