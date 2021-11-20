using System.Web.Mvc;
using System.Web.Routing;
using Elmah;

namespace InContact.DeveloperPortal.Web.Common
{
    public class UnhandledErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
                filterContext.ExceptionHandled = true;

                var routeDictionary = new RouteValueDictionary { { "controller", "Error" }, { "action", "Index" } };
                filterContext.Result = new RedirectToRouteResult(routeDictionary);
            }
        }
    }
}