namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Status : IEntity
    {
        public int Id { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(20, ErrorMessage = "{0} must have maximun {1} characters")]
        [Display(Name = "Status name")]
        public string StatusName { set; get; }

        public ICollection<LoanDetail> LoanDetails { set; get; }

        public ICollection<Material> Materials { set; get; }
    }
}