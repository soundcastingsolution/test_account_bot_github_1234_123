using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InContact.DeveloperPortal.Web.Common;

namespace InContact.DeveloperPortal.Web.Controllers
{
    public class HomeController : DeveloperPortalControllerBase
    {        
        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            Logging.LogMessage("[HomeController][Login] Home controller Login method is invoked");
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index");
            
            ViewBag.Title = "Login";
            ViewBag.returnUrl = returnUrl;
            return View();
        }
    }
}