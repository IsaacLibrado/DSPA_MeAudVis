

namespace DSPA_MeAudVis.Web.Helpers
{
    using System.Linq;
    using Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext dataContext;

        public CombosHelper(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        //public IEnumerable<SelectListItem> GetComboGenders()
        //{
        //    var list = dataContext.Genders.Select(
        //        c => new SelectListItem
        //        {
        //            Text = c.Name,
        //            Value = $"{c.Id}"
        //        }).ToList();
        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "[You have to choose a gender...]",
        //        Value = "0"
        //    });
        //    return list;
        //}

        //public IEnumerable<SelectListItem> GetComboStages()
        //{
        //    var list = dataContext.Stages.Select(
        //        c => new SelectListItem
        //        {
        //            Text = c.Name,
        //            Value = $"{c.Id}"
        //        }).ToList();
        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "[You have to choose a stage...]",
        //        Value = "0"
        //    });
        //    return list;
        //}
    }
}