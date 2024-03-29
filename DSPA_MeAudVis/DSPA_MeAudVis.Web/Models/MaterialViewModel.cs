﻿using DSPA_MeAudVis.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSPA_MeAudVis.Web.Models
{
    public class MaterialViewModel:Material 
    {
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, int.MaxValue, ErrorMessage = "You have to select a status")]
        [Display(Name = "Type")]
        public int StatusId { get; set; }

        public IEnumerable<SelectListItem> Statuses { get; set; }

        [Display(Name = "Applicant")]
        public int ApplicantId { get; set; }

        public IEnumerable<SelectListItem> Applicants { get; set; }
    }
}
