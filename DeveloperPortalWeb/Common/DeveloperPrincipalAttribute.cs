using System.Web;
using System.Web.Mvc;
using InContact.DeveloperPortal.Web.Models;

namespace InContact.DeveloperPortal.Web.Common
{
    public class DeveloperPrincipalAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var principal = new DeveloperPrincipal(HttpContext.Current.User.Identity);
            var developerTokenInformation = GetDevToken(filterContext);

            if (developerTokenInformation != null)
            {
                principal.AddDeveloperTokenInformation(developerTokenInformation);
            }

            HttpContext.Current.User = principal;

            base.OnActionExecuting(filterContext);
        }

        private static DeveloperAuthenticationToken GetDevToken(ActionExecutingContext filterContext)
        {
            DeveloperAuthenticationToken token = null;

            var serializedDevTokenCookie = filterContext.HttpContext.Request.Cookies["zDevToken"];

            if (null != serializedDevTokenCookie)
            {
                token = new DeveloperAuthenticationToken(serializedDevTokenCookie.Value);

                if (!filterContext.HttpContext.User.Identity.Name.Equals(token.UserName))
                {
                    filterContext.HttpContext.Response.Cookies.Remove("zDevToken");
                    token = null;
                }
            }

            return token;
        }
    }
}