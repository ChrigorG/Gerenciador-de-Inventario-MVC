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
            return value ? "bg-success" : "bg-danger";
        }

        public static IEnumerable<SelectListItem> GetListActiveOrInative()
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem { Text = Constants.Active, Value = "1" },
                new SelectListItem { Text = Constants.Inactive, Value = "0" }
            };

            return list;
        }
    }
}
