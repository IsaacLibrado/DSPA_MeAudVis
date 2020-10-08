namespace DSPA_MeAudVis.Web.Helpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboStatuses();
        IEnumerable<SelectListItem> GetComboApplicantTypes();
        IEnumerable<SelectListItem> GetComboMaterials();
    }
}
