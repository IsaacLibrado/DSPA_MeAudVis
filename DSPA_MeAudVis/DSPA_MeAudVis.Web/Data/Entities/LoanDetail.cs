using DSPA_MeAudVis.Web.Controllers.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSPA_MeAudVis.Web.Data.Entities
{
    public class LoanDetail:IEntity
    {
        [Required(ErrorMessage = "{0} is required")]
        public int Id { set; get; }

        [Display(Name = "Observations")]
        public string Observations { set; get; }

        //[Required(ErrorMessage = "{0} is required")]
        public Material Material { set; get; }

        //[Required(ErrorMessage = "{0} is required")]
        public Status Status { set; get; }

        public Loan Loan { set; get; }
    }
}
