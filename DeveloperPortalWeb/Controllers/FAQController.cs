using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InContact.DeveloperPortal.Web.Controllers
{
    public class FAQController : DeveloperPortalControllerBase
    {
        const string ROUTE_NAME = "Default";

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = BreadcrumbTrail;
            return View();
        }
    }
}