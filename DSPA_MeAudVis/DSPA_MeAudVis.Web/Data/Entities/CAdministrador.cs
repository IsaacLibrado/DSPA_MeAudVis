namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define a los adminisradores del sistema
    /// </summary>
    /// Version 1.0
    /// Fecha de creacion 08/09/20
    /// Creador Arturo Villegas
    public class CAdministrador : IEntity
    {
        [Required(ErrorMessage = "{0} is required")]
        public int Id { set; get; }

        [Required(ErrorMessage = "{0} is required")]
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
