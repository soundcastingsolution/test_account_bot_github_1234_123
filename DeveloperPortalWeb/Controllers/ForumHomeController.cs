using InContact.DeveloperPortal.Web.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace InContact.DeveloperPortal.Web.Controllers
{
    public class ForumHomeController : Controller
    {
        // GET: ForumHome
        [Authorize]
        public ActionResult Index()
        {
            var model = new List<SectionItemViewModel>
            {
                new SectionItemViewModel
                {
                    Title = "API Forum",
                    Link = ConfigurationManager.AppSettings["APIForumUrl"],
                    Icon = "/Content/images/Documentation/APIAuthToken_BlueCircle.png",
                    Description = "Community for integrator to discuss API use cases and ask questions of NICE CXone."
                },
                new SectionItemViewModel
                {
                    Title = "Studio Forum",
                    Link = ConfigurationManager.AppSettings["StudioForumUrl"],
                    Icon = "/Content/images/Documentation/GetStarted_BlueCircle.png",
                    Description = "Community to discuss use cases and development for NICE CXone Studio application."
                }
            };

            return View(model);
        }
    }
}