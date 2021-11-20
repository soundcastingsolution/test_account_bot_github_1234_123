using InContact.DeveloperPortal.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace InContact.DeveloperPortal.Web.Controllers
{
    public class APIController : DeveloperPortalControllerBase
    {
        // GET: Home
        public ActionResult Index()
        {
            var model = new List<SectionItemViewModel>
            {
                new SectionItemViewModel
                {
                    Title = "Explore Admin APIs",
                    Link = "API/AdminAPI",
                    Icon = "/Content/images/API/AdminAPI.png",
                    Description = "The Admin API is a collection of calls that deal with system resources in the NICE CXone and Customer Interaction Cloud system such as Agents, Skills, AddressBooks etc..."
                },
                new SectionItemViewModel
                {
                    Title = "Explore Agent APIs",
                    Link = "API/AgentAPI",
                    Icon = "/Content/images/API/AgentSessionAPI.png",
                    Description = "The Agent API is used to create, manage, and end \"agent sessions.\"  With an agent session, you are able to manage interactions on channels such as Phone Calls, Chats, Emails, Voice Mails, SMS, and Work Items."
                },
                new SectionItemViewModel
                {
                    Title = "Explore Authentication APIs",
                    Link = "API/AuthenticationAPI",
                    Icon = "/Content/images/API/AuthAPI.png",
                    Description = "The Authentication API is a collection of API calls that deal specifically with the system resources required to login to the CXone platform."
                },
                new SectionItemViewModel
                {
                    Title = "Explore Patron APIs",
                    Link = "API/PatronAPI",
                    Icon = "/Content/images/API/PatronAPI.png",
                    Description = "The Patron API can be used to create patron-facing applications. For example, you can create a mobile application that allows a patron to request a call back or a web site that allows users to request a live chat session."
                },
                new SectionItemViewModel
                {
                    Title = "Explore Real Time Data APIs",
                    Link = "API/RealTimeDataAPI",
                    Icon = "/Content/images/API/RealTimeAPI.png",
                    Description = "The Real-time Data API is a collection of API calls that provide access to \"real-time\" data on the platform. This API can be used to create custom dashboards and control panels, and to provide information to agents (leaderboards, etc.)."
                },
                new SectionItemViewModel
                {
                    Title = "Explore Reporting APIs",
                    Link = "API/ReportingAPI",
                    Icon = "/Content/images/API/ReportingAPI.png",
                    Description = "The Reporting API is a collection of API calls that provide access to \"historical\" data on the platform.  This API can be used to run Custom Reporting reports and retrieve calculated metrics."
                },

                new SectionItemViewModel
                {
                    Title = "Explore UserHub APIs",
                    Link = "API/UserHubAPI",
                    Icon = "/Content/images/API/ReportingAPI.png",
                    Description = " The purpose of this page is to provide documentation on using Access Key APIs "
                },

                new SectionItemViewModel
                {
                    Title="Explore Data Extraction APIs",
                    Link="API/DataExtractionAPI",
                    Icon="/Content/images/API/ReportingAPI.png",
                    Description="The Data Extraction API lets you extract data from CXone for external reporting purposes."
                },

                 new SectionItemViewModel
                {
                    Title="Explore Media Playback APIs",
                    Link="API/MediaPlaybackAPI",
                    Icon="/Content/images/API/ReportingAPI.png",
                    Description="The Media Playback API lets you to access CXone recording media."
                },

                  new SectionItemViewModel
                {
                    Title="Explore Digital Engagement APIs",
                    Link="API/DigitalEngagementAPI",
                    Icon="/Content/images/API/ReportingAPI.png", 
                    Description="Digital Engagement API can be used to configure an integration between the NICE CXone Digital platform and a third party messaging application."
                },
            };

            return View(model);
        }

        public ActionResult AdminAPI()
        {
            return View();
        }

        public ActionResult AgentAPI()
        {
            return View();
        }

        public ActionResult AuthenticationAPI()
        {
            return View();
        }

        public ActionResult CompleteRequestExampleCode()
        {
            return View();
        }

        public ActionResult PatronAPI()
        {
            return View();
        }

        public ActionResult RealTimeDataAPI()
        {
            return View();
        }

        public ActionResult ReportingAPI()
        {
            return View();
        }

        public ActionResult UserHubAPI()
        {
            return View();
        }

        public ActionResult DataExtractionAPI()
        {
            return View();
        }

        public ActionResult MediaPlaybackAPI()
        {
            return View();
        }
        public ActionResult DigitalEngagementAPI()
        {
            return View();
        }
    }
}