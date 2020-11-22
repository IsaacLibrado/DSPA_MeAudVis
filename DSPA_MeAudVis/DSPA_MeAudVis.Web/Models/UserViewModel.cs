
namespace DSPA_MeAudVis.Web.Models
{
    using DSPA_MeAudVis.Web.Data.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserViewModel:User
    {
        [Display(Name = "Photo")]
        public IFormFile ImageFile { get; set; }
    }
}
