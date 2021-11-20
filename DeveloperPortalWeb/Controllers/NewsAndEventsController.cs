using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InContact.DeveloperPortal.Web.Controllers
{   
    public class NewsAndEventsController : DeveloperPortalControllerBase
    {
        [Authorize]
        // GET: NewsAndEvents
        public ActionResult Index()
        {
            return View();
        }
    }
}