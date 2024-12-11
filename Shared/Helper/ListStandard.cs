using System.Web.Mvc;

namespace Shared.Helper
{
    public static class ListStandard
    {
        //**************************************** Validation List ****************************************//

        public static string ValidateActiveInative(bool value)
        {
            return value ? Constants.Active : Constants.Inactive;
        }

        public static string ValidateActiveInativeColor(bool value)
        {
            return value ? "border border-success" : "border border-danger";
        }

        public static string ValidateActiveInativeColorText(bool value)
        {
            return value ? "text-success" : "text-danger";
        }

        public static IEnumerable<SelectListItem> GetListActiveOrInative()
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem { Text = Constants.Active, Value = "true" },
                new SelectListItem { Text = Constants.Inactive, Value = "false" }
            };

            return list;
        }

        public static IEnumerable<SelectListItem> GetListValuesPermission()
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Acessa", Value = Constants.PermissionAccess },
                new SelectListItem { Text = "Não Acessa", Value = Constants.PermissionDenied },
                new SelectListItem { Text = "Somente visualiza", Value = Constants.PermissionView }
            };

            return list;
        }
    }
}
