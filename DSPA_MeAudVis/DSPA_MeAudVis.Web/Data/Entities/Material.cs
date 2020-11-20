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
        public int Id { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(30, ErrorMessage = "{0} must have maximun {1} characters")]
        [Display(Name = "Name")]
        public string Name { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(6, ErrorMessage = "{0} must have maximun {1} characters")]
        [Display(Name = "Label")]
        public string Label { set; get; }

        [MaxLength(15, ErrorMessage = "{0} must have maximun {1} characters")]
        [Display(Name = "Brand")]
        [Required(ErrorMessage = "{0} is required")]
        public string Brand { set; get; }

        [MaxLength(15, ErrorMessage = "{0} must have maximun {1} characters")]
        [Display(Name = "Model")]
        [Required(ErrorMessage = "{0} is required")]
        public string MaterialModel { set; get; }

        [MaxLength(15, ErrorMessage = "{0} must have maximun {1} characters")]
        [Display(Name = "Serial Number")]
        [Required(ErrorMessage = "{0} is required")]
        public string SerialNum { set; get; }

        public Applicant reserverApplicant { set; get; }

        public Status Status { set; get; }

        public ICollection<LoanDetail> LoanDetails { set; get; }
    }
}
