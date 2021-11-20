using InContact.DeveloperPortal.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace InContact.DeveloperPortal.Web.Controllers
{
	public class DocumentationController : DeveloperPortalControllerBase
	{
        // GET: Home
		public ActionResult Index()
		{
            var model = new List<SectionItemViewModel>
			{
				new SectionItemViewModel
				{
					Title = "API Authentication Token",
					Link = "/Documentation/APIAuthenticationToken",
					Icon = "/Content/images/Documentation/APIAuthToken_BlueCircle.png",
					Description = "This tutorial will explain how the NICE CXone RESTful APIs use the OAuth 2.0 specification to control and limit what information and capabilities are provided to applications and users of the NICE CXone RESTful APIs.",
					Reference = new Reference() { Title = "Reference", Date = "2014-01-03" }
				},
				new SectionItemViewModel
				{
					Title = "Central: Getting Started",
					Link = "/Documentation/GettingStarted",
					Icon = "/Content/images/Documentation/GetStarted_BlueCircle.png",
					Description = "This example demonstrates how you should register your application in the NICE CXone Central Website and retrieve the authorization token.This is for Central users only.",
					Reference = new Reference() { Title = "Tutorial", Date = "2014-01-05" }
				},
                new SectionItemViewModel
                {
                    Title = "Mobile SDK",
                    Link = "/Documentation/MobileSDK",
                    Icon = "/Content/images/Documentation/AdminAPI_BlueCircle.png",
                    Description = "This tutorial will show you how to use the Mobile SDK to easily add chat and callback widgets into an Android application.",
                    Reference = new Reference() { Title = "Tutorial", Date = "2014-01-22" }
                },
                new SectionItemViewModel
				{
					Title = "Using Events",
					Link = "/Documentation/UsingEvents",
					Icon = "/Content/images/Documentation/APIAuthToken_BlueCircle.png",
					Description = "In this tutorial, you will learn how to create/end agent session and how to listen to system events on the platform.",
					Reference = new Reference() { Title = "Tutorial", Date = "2014-01-22" }
				},
                new SectionItemViewModel
                {
                    Title = "Agent Session Events",
                    Link = "/Documentation/AgentSessionEvents",
                    Icon = "/Content/images/Documentation/AgentSessionEvents_BlueCircle.png",
                    Description = "Your application must request notification about events as soon as the agent session is started. There are a number of events that can occur on the platform. This reference outlines the JSON schema for each event type that you could receive.",
                    Reference = new Reference() { Title = "Tutorial", Date = "2014-01-22" }
                },
                new SectionItemViewModel
                {
                    Title = "eLearning",
                    Link = "/eLearning",
                    Icon = "/Content/images/Home/Documentation_Icon.png",
                    Description = "eLearning Tools",
                    Reference = new Reference() { Title = "Tutorial", Date = "2018-03-08" }
                },
                 new SectionItemViewModel
                {
                    Title = "UserHub: Getting Started",
                    Link = "/Documentation/UserHubGettingStarted",
                    Icon = "/Content/images/Documentation/APIAuthToken_BlueCircle.png",
                    Description = "This tutorial will explain the process required to create the first access key for an administrative user on a tenant and to gain access to CXone APIs",
                    Reference = new Reference() { Title = "Tutorial", Date = "2018-03-08" }
                }
            };

			return View(model);
		}

        #region Getting Started
        public ActionResult GettingStarted()
        {
            var model = new List<AccordionItemViewModel>
            {
                new AccordionItemViewModel
                {
                    Title = "Register Your Application",
                    FileNameOfView ="RegisterYourApplication"
                },
                new AccordionItemViewModel
                {
                    Title = "Create an Authorization Key",
                    FileNameOfView = "CreateAnAuthorizationKey"
                },
                new AccordionItemViewModel
                {
                    Title = "Send a Token Request",
                    FileNameOfView = "SendATokenRequest"
                },
                new AccordionItemViewModel
                {
                    Title = "Send an Implicit Request",
                    FileNameOfView = "SendAnImplicitRequest"
                },
                new AccordionItemViewModel
                {
                    Title = "Retrieve the Token",
                    FileNameOfView = "RetrieveTheToken"
                },
                new AccordionItemViewModel
                {
                    Title = "Use the Token",
                    FileNameOfView = "UseTheToken"
                },
                new AccordionItemViewModel
                {
                    Title = "Expired Tokens",
                    FileNameOfView = "ExpiredTokens"
                },
                new AccordionItemViewModel
                {
                    Title = "Token Request Example Code",
                    FileNameOfView = "TokenRequestExampleCode"
                }
            };

            return View(model);
        }

        #endregion

        #region Using Events
        public ActionResult UsingEvents()
	    {
            var model = new List<AccordionItemViewModel>
            {
                new AccordionItemViewModel
                {
                    Title = "Create/End an Agent Session",
                    FileNameOfView ="CreateEndAnAgentSession"
                },
                new AccordionItemViewModel
                {
                    Title = "Requesting Events",
                    FileNameOfView = "RequestingEvents"
                },
                new AccordionItemViewModel
                {
                    Title = "Event Types",
                    FileNameOfView = "EventTypes"
                }
            };

            return View(model);
        }
        #endregion

        #region Agent Session Events
        public ActionResult AgentSessionEvents()
        {

            var model = new List<AccordionItemViewModel>
            {
                new AccordionItemViewModel
                {
                    Title = "Agent Events",
                    FileNameOfView ="AgentEvents"
                },
                new AccordionItemViewModel
                {
                    Title = "Personal Connection Events",
                    FileNameOfView = "PersonalConnectionEvents"
                },
                new AccordionItemViewModel
                {
                    Title = "Call Contact Events",
                    FileNameOfView = "CallContactEvents"
                },
                new AccordionItemViewModel
                {
                    Title = "Work Item Contact Events",
                    FileNameOfView = "WorkItemContactEvents"
                },
                new AccordionItemViewModel
                {
                    Title = "Indicator Events",
                    FileNameOfView = "IndicatorEvents"
                },
                new AccordionItemViewModel
                {
                    Title = "Page Open Events",
                    FileNameOfView = "PageOpenEvents"
                },
                new AccordionItemViewModel
                {
                    Title = "Chat Contact Events",
                    FileNameOfView = "ChatContactEvents"
                },
                new AccordionItemViewModel
                {
                    Title = "Supervisor Events",
                    FileNameOfView = "SupervisorEvents"
                },
                new AccordionItemViewModel
                {
                    Title = "Other Events",
                    FileNameOfView = "OtherEvents"
                }
            };

            return View(model);
        }
        #endregion

        #region API Authentication Token
        public ActionResult APIAuthenticationToken()
        {
            return View();
        }
        #endregion

        #region Android Mobile SDK
        public ActionResult MobileSDK()
        {
            var model = new List<AccordionItemViewModel>
            {
                new AccordionItemViewModel
                {
                    Title = "Adding the Mobile SDK",
                    FileNameOfView ="AddingMobileSDK"
                },
                new AccordionItemViewModel
                {
                    Title = "Adding the Chat Widget",
                    FileNameOfView = "AddingChatWidget"
                },
                new AccordionItemViewModel
                {
                    Title = "Adding the Callback Widget",
                    FileNameOfView = "AddingCallbackWidget"
                },
                new AccordionItemViewModel
                {
                    Title = "Customizing the Widget Interfaces",
                    FileNameOfView = "CustomizeWidgetInterface"
                },
                new AccordionItemViewModel
                {
                    Title = "Customizing the Callback Widget Interface",
                    FileNameOfView = "CustomizeCallbackWidget"
                },
                new AccordionItemViewModel
                {
                    Title = "Using Queue Stats in Your Application",
                    FileNameOfView = "UsingQueueStats"
                },
                new AccordionItemViewModel
                {
                    Title = "Key Resource Classes",
                    FileNameOfView = "KeyResourceClasses"
                }
            };            
            return View(model);
        }
        #endregion

        #region UserHub GettingStarted
        public ActionResult UserHubGettingStarted()
        {
            var model = new List<AccordionItemViewModel>
            {
                new AccordionItemViewModel
                {
                    Title = "Before You Begin",
                    FileNameOfView ="BeforeYouBegin"
                },
                new AccordionItemViewModel
                {
                    Title = "Generate Key in CXone",
                    FileNameOfView = "GenerateKeyinCXone"
                },
                new AccordionItemViewModel
                {
                    Title = "Generate Token for Domain",
                    FileNameOfView = "GenerateTokenforDomain"
                },
                new AccordionItemViewModel
                {
                    Title = "Refresh Token",
                    FileNameOfView = "RefreshToken"
                }    
            };

            return View(model);
        }
        #endregion
    }
}