using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSPA_MeAudVis.Web.Models
{
    public class HandbookViewModel
    {
        [Display(Name = "Photo")]
        public IFormFile ImageFile { get; set; }
    }
}
