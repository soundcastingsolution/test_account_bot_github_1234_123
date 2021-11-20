using InContact.DeveloperPortal.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace InContact.DeveloperPortal.Web.Controllers
{
    public class ELearningController : DeveloperPortalControllerBase
    {
        // GET: Home
        public ActionResult Index()
        {
            var model = new List<SectionItemViewModel>()
            {               
                new SectionItemViewModel
                {
                    Title = "Help Portal",
                    Link = "https://help.incontact.com",
                    Icon = "/Content/images/eLearning/eLearning_HelpPortal_Icon.png",
                    Description = "Find the product-specific information with NICE CXone online help portal. Review product updates and features with intuitive online help landing page."
                },
                new SectionItemViewModel
                {
                    Title = "Knowledge Base",
                    Link = "https://support.niceincontact.com/Support/KB",
                    Icon = "/Content/images/eLearning/eLearning_KnowledgeBase_Icon.png",
                    Description = "Get helpful how-to info using NICE CXone solutions. Search for solutions to challenges you are currently experiencing and find answers to frequently asked questions."
                },
                new SectionItemViewModel
                {
                    Title = "DEVone Documentation",
                    Link = "Documentation",
                    Icon = "/Content/images/eLearning/eLearning_DEVoneDocumentation_Icon.png",
                    Description = "NICE CXone DEVone Quick Start Guide. This guide will help DEVone partner(s) get started on NICE CXone platform."
                }
            };

            return View(model);
        }
    }
}