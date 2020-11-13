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
        public int Id { set; get; }

        [Display(Name = "Loan Detail")]
        public ICollection<LoanDetail> LoanDetails { set; get; }

        [Display(Name = "Intern")]
        public Intern Intern { set; get; }

        [Display(Name = "Applicant")]
        public Applicant Applicant { set; get; }
       
    }
}
