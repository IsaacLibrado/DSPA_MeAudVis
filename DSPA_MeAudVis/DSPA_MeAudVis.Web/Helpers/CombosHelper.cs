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

        public IEnumerable<SelectListItem> GetComboStatuses()
        {
            var list = dataContext.Statuses.Select(
                c => new SelectListItem
                {
                    Text = c.StatusName,
                    Value = $"{c.StatusName}"
                }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[You have to choose a status...]",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboApplicantTypes()
        {
            var list = dataContext.ApplicantTypes.Select(
                c => new SelectListItem
                {
                    Text = c.Type,
                    Value = $"{c.Id}"
                }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[You have to choose a type...]",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboMaterials()
        {
            var list = dataContext.Materials.Select(
                c => new SelectListItem
                {
                    Text = c.Name,
                    Value = $"{c.Id}"
                }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[You have to choose a material...]",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboUsers()
        {
            var list = dataContext.Users.Select(
                c => new SelectListItem
                {
                    Text = c.UserName,
                    Value = $"{c.UserName}"
                }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[You have to choose a username...]",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboRoles()
        {
            var list = dataContext.Roles.Select(
                c => new SelectListItem
                {
                    Text = c.Name,
                    Value = $"{c.Name}"
                }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[You have to choose a role...]",
                Value = "0"
            });
            return list;
        }
    }
}