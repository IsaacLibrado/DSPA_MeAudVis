namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;

    /// <summary>
    /// Define a los manuales para los usuarios
    /// </summary>
    /// Version 1.0
    /// Fecha de creacion 7/09/20
    /// Creador Isaac Librado
    public class CManual : IEntity
    {
        public int Id { set; get; }

        public string Nombre { set; get; }

        //Pendiente para imagenes
        public string Contenido { set; get; }
    }
}
