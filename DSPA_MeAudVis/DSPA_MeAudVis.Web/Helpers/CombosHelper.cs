namespace DSPA_MeAudVis.Web.Helpers
{
    using System.Linq;
    using Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

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
                    Value = $"{c.Id}"
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

        public IEnumerable<SelectListItem> GetComboInterns()
        {
            var list = dataContext.Interns.Select(
                c => new SelectListItem
                {
                    Text = string.Format("{0} {1}",c.User.UserName ,c.User.FullName),
                    Value = $"{c.Id}"
                }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[You have to choose an Intern...]",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboApplicants()
        {
            var list = dataContext.Applicants.Where(item => item.Debtor!=true)
                .Select(
                c => new SelectListItem
                {
                    Text = string.Format("{0} {1}", c.User.UserName, c.User.FullName),
                    Value = $"{c.Id}"
                }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[You have to choose an Applicant...]",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboMaterials()
        {
            var list = dataContext.Materials.Where(item => item.Status.Id!=2).Where(item => item.Status.Id != 4)
                .Select(
                c =>   new SelectListItem
                {
                    Text = string.Format("{0} {1}", c.Name, c.Label),
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
            var list = dataContext.Users
                .Select(
                c => new SelectListItem
                {
                    Text = string.Format("{0} {1}",c.UserName, c.FullName),
                    Value = $"{c.UserName}"
                }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[You have to choose an user...]",
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