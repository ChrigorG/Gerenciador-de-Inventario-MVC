using System.Web.Mvc;

namespace Shared.Helper
{
    public static class ListStandard
    {
        //**************************************** Validation List ****************************************//

        public static string ValidateActiveInative(string value)
        {
            return value == Constants.Active ? "Ativo" : "Inativo";
        }

        public static string ValidateActiveInativeColor(string value)
        {
            return value == Constants.Active ? "green lighten-5" : "red lighten-5";
        }

        public static string ValidateActiveInativeTextColor(string value)
        {
            return value == Constants.Active ? "green-text" : "red-text";
        }

        //**************************************** List ****************************************//

        public static IEnumerable<SelectListItem> GetListActiveOrInative()
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem { Text = Constants.TextActive, Value = Constants.Active },
                new SelectListItem { Text = Constants.TextInative, Value = Constants.Inative }
            };

            return list;
        }
    }
}
