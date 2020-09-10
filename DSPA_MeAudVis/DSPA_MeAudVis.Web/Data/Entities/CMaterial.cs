namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define a los materiales que se prestan en el sistema
    /// </summary>
    /// Version 1.0
    /// Fecha de creacion 09/09/20
    /// Creador David Hernandez
    public class CMaterial : IEntity
    {
        [Required(ErrorMessage = "{0} is required")]
        public int Id { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(30, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Nombre")]
        public string Nombre { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(6, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Etiqueta")]
        public string Etiqueta { set; get; }

        [MaxLength(15, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Marca")]
        public string Marca { set; get; }

        [MaxLength(15, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Modelo")]
        public string Modelo { set; get; }

        [MaxLength(15, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Numero de serie")]
        public string NumSerie { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Materiales prestado")]
        public ICollection<CPrestamo> Prestamos { set; get; }
    }
}
