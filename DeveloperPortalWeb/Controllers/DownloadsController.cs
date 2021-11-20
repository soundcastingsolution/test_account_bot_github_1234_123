using InContact.DeveloperPortal.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace InContact.DeveloperPortal.Web.Controllers
{
    public class DownloadsController : DeveloperPortalControllerBase
    {
        // GET: Download
        public ActionResult Index()
        {
            var model = new List<SectionItemViewModel>()
            {
                new SectionItemViewModel
                {
                    Title = "Mobile iOS",
                    Link = Url.Action("Download", "Downloads", new { id = 1 }),
                    Icon = "/Content/images/Downloads/Download_BlueCircle.png",
                    Description = "This SDK allows you to create a simple iOS application by adding widgets supported by the NICE CXone Mobile SDK."
                },
                new SectionItemViewModel
                {
                    Title = "Mobile Android",
                    Link = Url.Action("Download", "Downloads", new { id = 2 }),
                    Icon = "/Content/images/Downloads/Download_BlueCircle.png",
                    Description = "This SDK allows you to create a simple Android application by adding widgets supported by the NICE CXone Mobile SDK."
                },
                new SectionItemViewModel
                {
                    Title = "Agent HTML5",
                    Link = Url.Action("Download", "Downloads", new { id = 3 }),
                    Icon = "/Content/images/Downloads/Download_BlueCircle.png",
                    Description = "This SDK provides you a fully functional example of an Agent Application built in HTML5 including the ability to handle events published to an active Agent Session."
                },
                new SectionItemViewModel
                {
                    Title = "Workitem SDK",
                    Link = Url.Action("Download", "Downloads", new { id = 4 }),
                    Icon = "/Content/images/Downloads/Download_BlueCircle.png",
                    Description = "This SDK provides examples of how to use Workitems with the NICE CXone platform."
                },
                new SectionItemViewModel
                {
                    Title = "Federated Identity Management using ADFS",
                    Link = Url.Action("Download", "Downloads", new { id = 5 }),
                    Icon = "/Content/images/Downloads/Download_BlueCircle.png",
                    Description = "This document provides an example of how to configure Federated Identity Management for NICE CXone Central using ADFS as the Identity Provider."
                },
                  new SectionItemViewModel
                {
                    Title = "NICE CXone DEVone Partner Quick Start Guide",
                    Link = Url.Action("Download", "Downloads", new { id = 6 }),
                    Icon = "/Content/images/Downloads/Download_BlueCircle.png",
                    Description = "This quick start guide for DEVone partners takes you through the steps to administrate your business unit, register your application in the NICE CXone Central Website, retrieve the authorization token, and begin using NICE CXone APIs."
                },
                new SectionItemViewModel
                {
                    Title="Implement a Chatbot solution",
                    Link =Url.Action("Download", "Downloads", new { id = 7 }),
                    Icon = "/Content/images/Downloads/Download_BlueCircle.png",
                    Description="This quick start guide for DEVone partners takes you through the steps to integrate a Chatbot directly with NICE CXone Chat and using Studio."
                },
                /* new SectionItemViewModel
                {
                    Title="Create Access Key",
                    Link =Url.Action("Download", "Downloads", new { id = 8 }),
                    Icon = "/Content/images/Downloads/Download_BlueCircle.png",
                    Description="This is a quick start guide to create your first access key in developer portal."
                },*/
                  new SectionItemViewModel
                {
                    Title="MAX POST Messages Integration",
                    Link =Url.Action("Download", "Downloads", new { id = 8 }),
                    Icon = "/Content/images/Downloads/Download_BlueCircle.png",
                    Description="This document is a guide to create POST messages for IFrame integration with MAX."
                }

            };

			return View(model);
		}

        // For downloading files add a case to the switch that will return the file desired.
        [Authorize]
        public ActionResult Download(int id)
        {
            try
            {
                switch (id)
                {
                    case 1:
                        return File("~/Content/Downloads/ioskit.zip", "application/zip", "ioskit.zip");
                    case 2:
                        return File("~/Content/Downloads/androidkit.zip", "application/zip", "androidkit.zip");
                    case 3:
                        return File("~/Content/Downloads/html5Kit.zip", "application/zip", "html5Kit.zip");
                    case 4:
                        return File("~/Content/Downloads/workItemKit.zip", "application/zip", "workItemKit.zip");
                    case 5:
                        return File("~/Content/Downloads/inContactFederatedIdentityManagementGuide.pdf", "application/pdf", "inContactFederatedIdentityManagementGuide.pdf");
                    case 6:
                        return File("~/Content/Downloads/DEVonePartnerQuickStartGuide.pdf", "application/pdf", "DEVonePartnerQuickStartGuide.pdf");
                    case 7:
                        return File("~/Content/Downloads/ChatbotAPIInstructions.pdf", "application/pdf", "ChatbotAPIInstructions.pdf");
                    case 8:
                        return File("~/Content/Downloads/MAXPOSTMessageIntegration.pdf", "application/pdf", "MAXPOSTMessageIntegration.pdf");
                    default:
                        return View("Index");
                }
            }
            catch
            {
                return View("Index");
            }
        }
    }
}