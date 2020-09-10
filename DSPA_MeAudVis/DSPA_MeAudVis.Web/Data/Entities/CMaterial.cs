namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;

    /// <summary>
    /// Define a los materiales que se prestan en el sistema
    /// </summary>
    /// Version 1.0
    /// Fecha de creacion 09/09/20
    /// Creador David Hernandez
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
