using System.Web.Mvc;
using System.Web.Routing;

namespace InContact.DeveloperPortal.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "APIContent",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "API", action = "C" }
                );

            routes.MapRoute(
                name: "DocumentationContent",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Documentation", action = "C" }
                );

            routes.MapRoute(
                name: "DownloadsContent",
                url: "Downloads/{id}",
                defaults: new { controller = "Downloads", action = "C" }
                );

            routes.MapRoute(
                name: "FAQContent",
                url: "FAQ/{id}",
                defaults: new { controller = "FAQ", action = "C" }
                );

            routes.MapRoute(
                name: "eLearningContent",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "ELearning", action = "C" }
                );

            routes.MapRoute(
                name: "NewsAndEventsContent",
                url: "NewsAndEvents",
                defaults: new { controller = "NewsAndEvents", action = "Index" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
