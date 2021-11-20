using InContact.DeveloperPortal.Web.Controllers;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Linq;
using System.Web.Mvc;

namespace DeveloperPortal.Tests.Controllers
{
    [TestFixture]
    public class NewsAndEventsControllerTests
    {
        [Test]
        public void NewsAndEventsController_Index_HasAuthorizeAttribute()
        {
            var controller = new NewsAndEventsController();
            var type = controller.GetType();
            var methodInfo = type.GetMethod("Index");
            object[] attributes = methodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), true);
            var attribute = attributes.Count() == 0 ? null : (AuthorizeAttribute)attributes[0];

            Assert.AreEqual(attributes[0].ToString(), "System.Web.Mvc.AuthorizeAttribute");
        }
    }
}
