namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define a los prestamos que se realizan en el sistema
    /// </summary>
    /// Version 2.1
    /// Fecha de creacion 09/09/20
    /// Creador David Hernandez
    /// Fecha de Modificacion 14/09/20
    /// Modificador Isaac Librado
    public class Loan : IEntity
    {
        [Required(ErrorMessage = "{0} is required")]
        public int Id { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Borrowed Material")]
        public Material Material { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Grant Holder")]
        public Scholar scholarOut { set; get; }

        [Display(Name = "Grant Receiver")]
        public Scholar scholarIn { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Applicant")]
        public Applicant Applicant { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Date and time of the loan")]
        public DateTime DateTimeOut { set; get; }

        [Display(Name = "Date and time received")]
        public DateTime DateTimeIn { set; get; }

        [Display(Name = "Loan status")]
        public bool Status { set; get; }

    }
}
