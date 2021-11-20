using System.Web.Mvc;
using InContact.Common.Web.Mvc;
using InContact.DeveloperPortal.Web.Common;
using System.Web.Routing;
using Rotativa.MVC;
using System.IO;

namespace InContact.DeveloperPortal.Web.Controllers
{
    [DeveloperPrincipal]
    public class DeveloperPortalControllerBase : Controller
    {
        private readonly BreadcrumbTrail _breadcrumbTrail = new BreadcrumbTrail();

        public virtual new IDeveloperPrincipal User { get { return base.User as IDeveloperPrincipal; } }

        public BreadcrumbTrail BreadcrumbTrail
        {
            get { return _breadcrumbTrail; }
        }

        public DeveloperPortalControllerBase()
        {
            _breadcrumbTrail.AddNode("Home", "Default", new { controller = "Home", action = "Index" });
            ViewData["zBreadcrumbTrail"] = _breadcrumbTrail;
        }

        public ActionResult C(string id, string format, string section)
        {
            ViewBag.Breadcrumb = BreadcrumbTrail;
            ViewBag.IsPdfRequest = (format == "PDF").ToString().ToLower();
            if (id == "PageAsPdf")
            {
                string filename = Request.Params["source"];
                if (filename == null)
                {
                    filename = "Index";
                }
                var result = DownloadAsPdf(section, filename);
                return result ?? RedirectToAction("NotFound", "Error");
            }
            else
            {
                return View(id);
            }
        }

        public ActionResult DownloadAsPdf(string section, string source)
        {
            string fileName = HttpContext.Server.MapPath(string.Format("~/Content/pdfs/{0}/{1}.pdf", section, source));
            return System.IO.File.Exists(fileName) ? File(fileName, "application/octet-stream", source + ".pdf") : null;
        }

        public ActionResult PageAsPdf(string source)
        {
            if (source == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            else if (source != "PageAsPdf")
            {
                var pdfParams = new RouteValueDictionary();
                pdfParams.Add("id", source);
                pdfParams.Add("format", "PDF");
                return new ActionAsPdf("C", pdfParams)
                {
                    FileName = source + ".pdf",
                    RotativaOptions = new Rotativa.Core.DriverOptions { IsLowQuality = true, IsBackgroundDisabled = false }
                };
            }
            else
            {
                return View(source);
            }
        }
    }
}