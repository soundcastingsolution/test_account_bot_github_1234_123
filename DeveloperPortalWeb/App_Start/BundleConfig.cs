using System.Web.Optimization;

namespace InContact.DeveloperPortal.Web.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/bundles/swagger.css").Include(
                //"~/content/swagger/css/print.css",
                //"~/content/swagger/css/reset.css",
                "~/content/swagger/css/screen.css",
                "~/content/swagger/css/swagger-ui.css",
                //"~/content/swagger/css/style.css",
                //"~/content/swagger/css/typography.css",
                "~/content/swagger/css/incontact.custom.css"));



            bundles.Add(new ScriptBundle("~/bundles/swagger.js").Include(
                "~/Scripts/jquery-migrate-1.2.1.min.js",
                "~/content/swagger/lib/jquery.slideto.min.js",
                "~/content/swagger/lib/jquery.wiggle.min.js",
                "~/content/swagger/lib/jquery.ba-bbq.min.js",
                "~/content/swagger/lib/handlebars-2.0.0.js",
                "~/content/swagger/lib/underscore-min.js",
                "~/content/swagger/lib/backbone-min.js",
                "~/content/swagger/swagger-ui-bundle.js",
                "~/content/swagger/lib/highlight.7.3.pack.js",
                "~/content/swagger/lib/marked.js",
                "~/content/swagger/lib/swagger-oauth.js",
                "~/content/swagger/json-refs-standalone-min.js",
                "~/scripts/incontact.swagger.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/kendo.js").Include(
                "~/Scripts/kendo/kendo.all.min.js"));

            bundles.Add(new StyleBundle("~/bundles/prettify.css").Include(
                "~/content/prettify.css"));

            bundles.Add(new ScriptBundle("~/bundles/prettify.js").Include(
                "~/scripts/prettify.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
            "~/Scripts/kendo/kendo.all.min.js",
            // "~/Scripts/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
            "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
            "~/Content/kendo/kendo.common.min.css",
            "~/Content/kendo/kendo.common-bootstrap.min.css",
            "~/Content/kendo/kendo.bootstrap.min.css"));


            // Clear all items from the default ignore list to allow minified CSS and JavaScript files to be included in debug mode
            bundles.IgnoreList.Clear();

            // Add back the default ignore list rules sans the ones which affect minified files and debug mode
            bundles.IgnoreList.Ignore("*.intellisense.js");
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
        }
    }
}