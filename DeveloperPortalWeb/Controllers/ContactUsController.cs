using InContact.DeveloperPortal.Web.Common;
using InContact.DeveloperPortal.Web.Models;
using InContact.DeveloperPortal.Web.NotificationService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InContact.DeveloperPortal.Web.Controllers
{
    public class ContactUsController : DeveloperPortalControllerBase
    {
        ISupportEmail _emailClient;

        public ContactUsController(ISupportEmail emailClient)
        {
            _emailClient = emailClient;
        }

        // GET: ContactUs
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendMessage(ContactUsModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (Convert.ToString(this.Session["captcha"]) == viewModel.Captcha)
                {
                    string[] recipients = { ConfigurationManager.AppSettings["ContactUsToEmailAddress"] };

                    string subject = ConfigurationManager.AppSettings["ContactUsSubject"];

                    var message = ViewHelper.RenderViewToString(this.ControllerContext, "_EmailBody", viewModel);

                   var result= _emailClient.SendEmail(recipients, viewModel.Email, subject, message, null, 10);
                    if (result)
                    {
                        ViewBag.sucess = ConfigurationManager.AppSettings["SendMailSucessMsg"];
                        ModelState.Clear();
                        return View("Index");
                    }
                    else
                    {
                        throw new Exception("Error generated while sending message");
                    }
                }
                else
                {
                    ModelState.AddModelError("Captcha", "Invalid Captcha, Please try again");
          
                }
              
            }
            return View("Index",viewModel);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public Captcha ShowCaptchaImage()
        {
            return new Captcha();
        }

    }
}