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
        public int Id { set; get; }

        [Display(Name = "Observations")]
        public string Observations { set; get; }

        [Display(Name = "Date and time of loan")]
        public DateTime DateTimeOut { set; get; }

        [Display(Name = "Date and time of return")]
        public DateTime DateTimeIn { set; get; }

        public Material Material { set; get; }

        public Status Status { set; get; }

        public Loan Loan { set; get; }
    }
}
