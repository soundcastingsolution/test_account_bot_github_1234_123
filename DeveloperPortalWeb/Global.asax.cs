using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using InContact.DeveloperPortal.Web.App_Start;
using System;
using System.Web;
using System.Net;

namespace InContact.DeveloperPortal.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        void Application_PreSendRequestHeaders(Object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate, private");
        }
    }
}
