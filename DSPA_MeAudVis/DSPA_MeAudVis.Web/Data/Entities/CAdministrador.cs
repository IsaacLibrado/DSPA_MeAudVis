namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;

    /// <summary>
    /// Define a los adminisradores del sistema
    /// </summary>
    /// Version 1.0
    /// Fecha de creacion 08/09/20
    /// Creador Arturo Villegas
    public class CAdministrador : IEntity
    {
        public int Id { set; get; }

        public CUsuario Usuario { set; get; }

        //Metodos
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
