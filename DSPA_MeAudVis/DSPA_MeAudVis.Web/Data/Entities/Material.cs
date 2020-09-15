namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define a los materiales que se prestan en el sistema
    /// </summary>
    /// Version 2.0
    /// Fecha de creacion 09/09/20
    /// Fecha de Modificacion 14/09/20
    /// Creador David Hernandez
    public class Material : IEntity
    {
        [Required(ErrorMessage = "{0} is required")]
        public int Id { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(30, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Name")]
        public string Name { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(6, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Label")]
        public string Label { set; get; }

        [MaxLength(15, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Brand")]
        public string Brand { set; get; }

        [MaxLength(15, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Model")]
        public string Model { set; get; }

        [MaxLength(15, ErrorMessage = "{0} must have maximun {1} charactes")]
        [Display(Name = "Serial Number")]
        public string SerialNum { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Loan")]
        public ICollection<Loan> Loans { set; get; }
    }
}
