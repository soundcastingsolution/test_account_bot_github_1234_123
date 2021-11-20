using System.Web.Mvc;
using ForumWeb.Common;

namespace ForumWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new InContactDeveloperPortalAuthAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
