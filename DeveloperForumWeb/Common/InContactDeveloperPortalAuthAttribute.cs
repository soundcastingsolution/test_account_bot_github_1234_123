using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Security;

namespace ForumWeb.Common
{
    public class InContactDeveloperPortalAuthAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var cookie = filterContext.HttpContext.Request.Cookies["zDevToken"];
            if (cookie != null)
            {
                FormsAuthentication.SetAuthCookie("itdevelopers@incontact.com", false);
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var cookie = filterContext.HttpContext.Request.Cookies["zDevToken"];
            if (cookie != null)
            {
                FormsAuthentication.SetAuthCookie("itdevelopers@incontact.com", false);
            }
            //else
            //{
            //    filterContext.Result = new HttpUnauthorizedResult();
            //}
        }
    }
}