namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    /// Define a los solicitantes (maestros y alumnos) de material
    /// </summary>
    /// Version 2.1
    /// Fecha de creacion 08/09/20
    /// Creador Arturo Villegas
    /// Fecha de Modificacion 14/09/20
    /// Modificador Isaac Librado
    public class Applicant : IEntity
    {
        public int Id { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        public bool Debtor { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        public User User { set; get; }

        //[Required(ErrorMessage = "{0} is required")]
        public ApplicantType Type { set; get; }

        public ICollection<Loan> Loans { set; get; }

        public static implicit operator Task<object>(Applicant v)
        {
            throw new NotImplementedException();
        }
    }
}
