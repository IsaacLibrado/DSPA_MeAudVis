using DSPA_MeAudVis.Web.Controllers.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSPA_MeAudVis.Web.Data.Entities
{
    public class BorrowingDetail:IEntity
    {
        public int Id { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        public Material Material { set; get; }

        [Display(Name = "Borrowing status")]
        public bool Returned { set; get; }

        [Display(Name = "Observations")]
        public string Observations { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Date and time of the borrowing")]
        public DateTime DateTimeOut { set; get; }

        [Display(Name = "Date and time received")]
        public DateTime DateTimeIn { set; get; }
    }
}
