using System.Web.Mvc;
using InContact.DeveloperPortal.Web.Common;

namespace InContact.DeveloperPortal.Web.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new UnhandledErrorAttribute());
        }
    }
}