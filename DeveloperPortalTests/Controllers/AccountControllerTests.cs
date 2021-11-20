using InContact.DeveloperPortal.Web.Authentication;
using InContact.DeveloperPortal.Web.Common;
using InContact.DeveloperPortal.Web.Logic;
using InContact.DeveloperPortal.Web.Models;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InContact.DeveloperPortal.Web.Controllers.Tests
{
    [TestFixture()]
    public class AccountControllerTests
    {
        private Mock<IElmahWrapper> mockElmah;
        private Mock<ICXoneAuthClient> mockAuthClient;
        private NameValueCollection appSettings;

        [Test()]
        public void AccountController_SignIn_Exception()
        {
            //arrange
            string returnUrl = string.Empty;
            var mockContext = new Mock<HttpContextBase>();
            var mockPrincipal = new Mock<IDeveloperPrincipal>();
            var mockIdentity = new Mock<IIdentity>();
            var mockUriBuilder = new Mock<UriBuilder>();
            var mockRequest = new Mock<HttpRequestBase>();
            var session = new Mock<HttpSessionStateBase>();
            var mockAccountLogic = new Mock<IAccountLogic>();
            var mockAuthClient = new Mock<ICXoneAuthClient>();

            ConfigurationManager.AppSettings["IsLoginEnabled"] = "true";
            mockIdentity.Setup(x => x.IsAuthenticated).Returns(true);
            mockPrincipal.Setup(x => x.Identity).Returns(mockIdentity.Object);
            mockContext.Setup(x => x.User).Returns(mockPrincipal.Object);
            mockRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));
            mockContext.Setup(x => x.Request).Returns(mockRequest.Object);
            mockContext.Setup(x => x.Session).Returns(session.Object);
                       
            //act
            AccountController sut = new AccountController(mockAccountLogic.Object, appSettings, mockElmah.Object);
            var requestContext = new RequestContext(mockContext.Object, new RouteData());
            sut.Url = new UrlHelper(requestContext, new RouteCollection());
            sut.ControllerContext = new ControllerContext(mockContext.Object, new RouteData(), sut);
            ActionResult actual = sut.SignIn(returnUrl, false);

            //assert
            Assert.AreEqual(typeof(System.Web.Mvc.RedirectResult), actual.GetType(), "Expected a Redirect result");
        }

        [Test()]
        public void AccountController_Authenticate_SetsErrorOnClientStateMismatch()
        {
            //arrange            
            var mockContext = new Mock<HttpContextBase>();
            var mockPrincipal = new Mock<IDeveloperPrincipal>();
            var mockIdentity = new Mock<IIdentity>();
            var mockUriBuilder = new Mock<UriBuilder>();
            var mockRequest = new Mock<HttpRequestBase>();
            var session = new Mock<HttpSessionStateBase>();
            var mockAccountLogic = new Mock<IAccountLogic>();

            mockIdentity.Setup(x => x.IsAuthenticated).Returns(true);
            mockPrincipal.Setup(x => x.Identity).Returns(mockIdentity.Object);
            mockContext.Setup(x => x.User).Returns(mockPrincipal.Object);
            mockRequest.Setup(x => x.Url).Returns(new Uri("http://localhost:8080/Account/Authenticate?code=ck0z413dbab78ab64d80956f42778802f21dXE8b&state=eyJuYW1lIjoiREVWb25lIFBvcnRhbCIsImlkIjoiMDFmMjQxMGYtNTM5ZC00MjVhLWFiOTEtYjFkZTk3ODVhMmEzIiwicmV0dXJuVXJsIjoiLyJ9"));
            mockContext.Setup(x => x.Request).Returns(mockRequest.Object);
            mockContext.Setup(x => x.Session).Returns(session.Object);

            //act
            AccountController sut = new AccountController(mockAccountLogic.Object, appSettings, mockElmah.Object);
            var requestContext = new RequestContext(mockContext.Object, new RouteData());
            sut.Url = new UrlHelper(requestContext, new RouteCollection());
            sut.ControllerContext = new ControllerContext(mockContext.Object, new RouteData(), sut);
            var actual = sut.Authenticate() as ViewResult;

            //assert
            Assert.AreEqual(typeof(System.Web.Mvc.ViewResult), actual.GetType(), "Expected type of ViewResult");
            Assert.AreEqual("Code is empty or state is corrupted", actual.ViewBag.error, "Error message didn't matched");
        }

        [Test()]
        public void AccountController_Authenticate_ReturnsAuthenticateViewWithAuthCode()
        {
            CXoneClientState clientState = new CXoneClientState("DEVone", "https://cxone.niceincontact.com", "/")
            {
                Name = "DEVone",
                Id = Guid.Parse("d3a0cde1-4372-4c1a-a7d1-54b6375207eb"),
                CXoneAuthServer = "https://cxone.niceincontact.com",
                ReturnUrl = "/"
            };
            //arrange            
            var mockContext = new Mock<HttpContextBase>();
            var mockPrincipal = new Mock<IDeveloperPrincipal>();
            var mockIdentity = new Mock<IIdentity>();
            var mockUriBuilder = new Mock<UriBuilder>();
            var mockRequest = new Mock<HttpRequestBase>();
            var session = new Mock<HttpSessionStateBase>();
            var mockAccountLogic = new Mock<IAccountLogic>();

            mockIdentity.Setup(x => x.IsAuthenticated).Returns(true);
            mockPrincipal.Setup(x => x.Identity).Returns(mockIdentity.Object);
            mockContext.Setup(x => x.User).Returns(mockPrincipal.Object);
            mockRequest.Setup(x => x.Url).Returns(new Uri("http://localhost:8080/Account/Authenticate?code=aHMJ11bc703aa7014f0181c920a8b0621dadcHLR&state=eyJuYW1lIjoiREVWb25lIiwiaWQiOiJkM2EwY2RlMS00MzcyLTRjMWEtYTdkMS01NGI2Mzc1MjA3ZWIiLCJhdXRoU2VydmVyIjoiaHR0cHM6Ly9jeG9uZS5uaWNlaW5jb250YWN0LmNvbSIsInJldHVyblVybCI6Ii8ifQ"));
            mockContext.Setup(x => x.Request).Returns(mockRequest.Object);
            session.Setup(x=>x["ClientState"]).Returns(clientState);
            mockContext.Setup(x => x.Session).Returns(session.Object);

            //act
            AccountController sut = new AccountController(mockAccountLogic.Object, appSettings, mockElmah.Object);
            var requestContext = new RequestContext(mockContext.Object, new RouteData());
            sut.Url = new UrlHelper(requestContext, new RouteCollection());
            sut.ControllerContext = new ControllerContext(mockContext.Object, new RouteData(), sut);
            var actual = sut.Authenticate() as ViewResult;

            //assert
            Assert.AreEqual(typeof(System.Web.Mvc.ViewResult), actual.GetType(), "Expected type of ViewResult");
            Assert.IsNotNullOrEmpty(actual.ViewBag.AuthCode);
            Assert.IsNotNullOrEmpty(actual.ViewBag.ClientState);
        }

        [Test()]
        public void AccountController_AuthenticateUser_returnJsonResult()
        {
            CXoneClientState clientState = new CXoneClientState("DEVone", "https://cxone.niceincontact.com", "/")
            {
                Name = "DEVone",
                Id = Guid.Parse("d3a0cde1-4372-4c1a-a7d1-54b6375207eb"),
                CXoneAuthServer = "https://cxone.niceincontact.com",
                ReturnUrl = "/"
            };

            //arrange      
            var authcode = "ck0z413dbab78ab64d80956f42778802f21dXE8b";
            var state = "eyJuYW1lIjoiREVWb25lIiwiaWQiOiJkM2EwY2RlMS00MzcyLTRjMWEtYTdkMS01NGI2Mzc1MjA3ZWIiLCJhdXRoU2VydmVyIjoiaHR0cHM6Ly9jeG9uZS5uaWNlaW5jb250YWN0LmNvbSIsInJldHVyblVybCI6Ii8ifQ";
            var mockContext = new Mock<HttpContextBase>();
            var mockPrincipal = new Mock<IDeveloperPrincipal>();
            var mockIdentity = new Mock<IIdentity>();
            var mockUriBuilder = new Mock<UriBuilder>();
            var mockRequest = new Mock<HttpRequestBase>();
            var mockResponse = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var mockAccountLogic = new Mock<IAccountLogic>();
            var responseCookies = new HttpCookieCollection();


            HttpCookie c = new HttpCookie(".ASPXAUTH")
            {
                Value = "123",
                Domain = "localhost"
            };

            mockResponse.Setup(x => x.Cookies).Returns(responseCookies);
            HttpContext.Current = new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(null));
            System.Web.HttpContext.Current.Request.Cookies.Add(c);
            mockIdentity.Setup(x => x.IsAuthenticated).Returns(true);
            mockPrincipal.Setup(x => x.Identity).Returns(mockIdentity.Object);
            mockContext.Setup(x => x.User).Returns(mockPrincipal.Object);
            mockRequest.Setup(x => x.Url).Returns(new Uri("http://localhost:8080/Account/Authenticate?code=ck0z413dbab78ab64d80956f42778802f21dXE8b&state=eyJuYW1lIjoiREVWb25lIFBvcnRhbCIsImlkIjoiMDFmMjQxMGYtNTM5ZC00MjVhLWFiOTEtYjFkZTk3ODVhMmEzIiwicmV0dXJuVXJsIjoiLyJ9"));
            mockContext.Setup(x => x.Request).Returns(mockRequest.Object);
            session.Setup(x => x["ClientState"]).Returns(clientState);
            mockContext.Setup(x => x.Session).Returns(session.Object);

            //act
            AccountController sut = new AccountController(mockAccountLogic.Object, appSettings, mockElmah.Object);
            var requestContext = new RequestContext(mockContext.Object, new RouteData());
            sut.Url = new UrlHelper(requestContext, new RouteCollection());            
            sut.ControllerContext = new ControllerContext(mockContext.Object, new RouteData(), sut);
            var actual = sut.AuthenticateUser(authcode, state);

            //assert
            Assert.AreEqual(typeof(System.Web.Mvc.JsonResult), actual.GetType(), "Expected type of JsonResult");
            Assert.AreEqual(true, ((JsonResult)actual).Data.ToString().Contains("error"), "should contain error message");
        }

        [Test()]
    public void AccountController_AuthenticateUser_Fails()
    {
        CXoneClientState clientState = new CXoneClientState("DEVone", "https://cxone.niceincontact.com", "/")
        {
            Name = "DEVone",
            Id = Guid.Parse("d3a0cde1-4372-4c1a-a7d1-54b6375207eb"),
            CXoneAuthServer = "https://cxone.niceincontact.com",
            ReturnUrl = "/"
        };
        //arrange      
        var authcode = "";
        var state = "eyJuYW1lIjoiREVWb25lIiwiaWQiOiJkM2EwY2RlMS00MzcyLTRjMWEtYTdkMS01NGI2Mzc1MjA3ZWIiLCJhdXRoU2VydmVyIjoiaHR0cHM6Ly9jeG9uZS5uaWNlaW5jb250YWN0LmNvbSIsInJldHVyblVybCI6Ii8ifQ";
        var mockContext = new Mock<HttpContextBase>();
        var mockPrincipal = new Mock<IDeveloperPrincipal>();
        var mockIdentity = new Mock<IIdentity>();
        var mockUriBuilder = new Mock<UriBuilder>();
        var mockRequest = new Mock<HttpRequestBase>();
        var mockResponse = new Mock<HttpResponseBase>();
        var session = new Mock<HttpSessionStateBase>();
        var mockAccountLogic = new Mock<IAccountLogic>();
        var responseCookies = new HttpCookieCollection();

        //Arrange            
        HttpCookie c = new HttpCookie(".ASPXAUTH")
        {
            Value = "123",
            Domain = "localhost"
        };

        mockResponse.Setup(x => x.Cookies).Returns(responseCookies);
        HttpContext.Current = new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(null));
        System.Web.HttpContext.Current.Request.Cookies.Add(c);

        mockIdentity.Setup(x => x.IsAuthenticated).Returns(true);
        mockPrincipal.Setup(x => x.Identity).Returns(mockIdentity.Object);
        mockContext.Setup(x => x.User).Returns(mockPrincipal.Object);

        mockRequest.Setup(x => x.Url).Returns(new Uri("http://localhost:8080/Account/Authenticate?code=aHMJ11bc703aa7014f0181c920a8b0621dadcHLR&state=eyJuYW1lIjoiREVWb25lIiwiaWQiOiJkM2EwY2RlMS00MzcyLTRjMWEtYTdkMS01NGI2Mzc1MjA3ZWIiLCJhdXRoU2VydmVyIjoiaHR0cHM6Ly9jeG9uZS5uaWNlaW5jb250YWN0LmNvbSIsInJldHVyblVybCI6Ii8ifQ"));
        mockContext.Setup(x => x.Request).Returns(mockRequest.Object);
        session.Setup(x => x["ClientState"]).Returns(clientState);
        mockContext.Setup(x => x.Session).Returns(session.Object);

        //act
        AccountController sut = new AccountController(mockAccountLogic.Object, appSettings, mockElmah.Object);
        var requestContext = new RequestContext(mockContext.Object, new RouteData());
        sut.Url = new UrlHelper(requestContext, new RouteCollection());
        sut.ControllerContext = new ControllerContext(mockContext.Object, new RouteData(), sut);
        var actual = sut.AuthenticateUser(authcode, state);

        //assert
        Assert.AreEqual(((JsonResult)actual).Data.ToString(), "{ error = Authcode cannot be null or empty }");
        Assert.AreEqual(typeof(System.Web.Mvc.JsonResult), actual.GetType(), "Expected type of JsonResult");
    }

    [Test()]
    public void AccountController_CreateUser_GetsSuccess()
    {
        //arrange
        SigninViewModel viewModel = new SigninViewModel
        {
            ApplicationName = "TestApp",
            BusinessUnitNumber = 121,
            VendorName = "inCloud",
            UserName = "itdevs@stg1.com",
        };

        //Basic HttpClient
        var moqMessageHandler = new Mock<HttpMessageHandler>();
        moqMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns(Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("\"success\"")
                };
            }));

        var client = new HttpClient(moqMessageHandler.Object);
        var mockContext = new Mock<HttpContextBase>();
        var mockPrincipal = new Mock<IDeveloperPrincipal>();
        var mockIdentity = new Mock<IIdentity>();
        var mockUriBuilder = new Mock<UriBuilder>();
        var mockRequest = new Mock<HttpRequestBase>();
        var session = new Mock<HttpSessionStateBase>();
        var mockAccountLogic = new Mock<IAccountLogic>();

        mockIdentity.Setup(x => x.IsAuthenticated).Returns(true);
        mockPrincipal.Setup(x => x.Identity).Returns(mockIdentity.Object);
        mockContext.Setup(x => x.User).Returns(mockPrincipal.Object);
        mockRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));
        mockContext.Setup(x => x.Request).Returns(mockRequest.Object);
        mockContext.Setup(x => x.Session).Returns(session.Object);

        //act
        AccountController sut = new AccountController(mockAccountLogic.Object, appSettings, mockElmah.Object);
        sut.ControllerContext = new ControllerContext(mockContext.Object, new RouteData(), sut);
        bool actual = sut.CreateForumUser(viewModel, client);

        //assert
        Assert.IsTrue(actual);
    }


    [Test()]
    public void AccountController_CreateUser_GetsFailure()
    {
        //arrange
        SigninViewModel viewModel = new SigninViewModel
        {
            ApplicationName = "TestApp",
            BusinessUnitNumber = 121,
            VendorName = "inCloud",
            UserName = "itdevs@stg1.com",
        };

        // Basic HttpClient
        var moqMessageHandler = new Mock<HttpMessageHandler>();
        moqMessageHandler.Protected()
           .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
           .Returns(Task<HttpResponseMessage>.Factory.StartNew(() =>
           {
               return new HttpResponseMessage(HttpStatusCode.OK)
               {
                   Content = new StringContent("\"failure\"")
               };
           }));
        var client = new HttpClient(moqMessageHandler.Object);
        var mockContext = new Mock<HttpContextBase>();
        var mockPrincipal = new Mock<IDeveloperPrincipal>();
        var mockIdentity = new Mock<IIdentity>();
        var mockCXoneAuthClient = new Mock<CXoneAuthClient>();
        var mockUriBuilder = new Mock<UriBuilder>();
        var mockRequest = new Mock<HttpRequestBase>();
        var session = new Mock<HttpSessionStateBase>();
        var mockAccountLogic = new Mock<IAccountLogic>();

        mockIdentity.Setup(x => x.IsAuthenticated).Returns(true);
        mockPrincipal.Setup(x => x.Identity).Returns(mockIdentity.Object);
        mockContext.Setup(x => x.User).Returns(mockPrincipal.Object);
        mockRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));
        mockContext.Setup(x => x.Request).Returns(mockRequest.Object);
        mockContext.Setup(x => x.Session).Returns(session.Object);

        //act
        AccountController sut = new AccountController(mockAccountLogic.Object, appSettings, mockElmah.Object);
        sut.ControllerContext = new ControllerContext(mockContext.Object, new RouteData(), sut);
        bool actual = sut.CreateForumUser(viewModel, client);

        //assert
        Assert.IsFalse(actual);
    }

    [TestFixtureSetUp()]
    public void Setup()
    {
        // Basic Elmah
        string errorMsg = string.Empty;
        mockElmah = new Mock<IElmahWrapper>();
        mockElmah.Setup(x => x.Raise(It.IsAny<Exception>())).Verifiable();
        // App Settings
        appSettings = new NameValueCollection
            {
                { "APIVersion", "services/v18.0/" },
                { "APIForumUrl", "/Forum/cat/api-forum" },
                { "ClientId", "test-client-id" },
                { "ClientSecret", "test-client-secret" },
                { "CXoneAuthentication", "https://cxone.niceincontact.com/" },
                { "CXoneGovAuthentication" , "https://cxone-gov.niceincontact.com/"}
            };
            // OpenID/CXone Auth client
        mockAuthClient = new Mock<ICXoneAuthClient>();
        OpenIDConnectToken mockToken = new OpenIDConnectToken
        {
            AccessToken = "mock.open-id.access-token",
            ExpiresIn = "3600",
            IdToken = "mock.open-id.id-token",
            RefreshToken = "mock.open-id.refresh-token",
            TokenType = "Bearer"
        };
        CXoneConfiguration mockCXoneConfig = new CXoneConfiguration
        {
            AgentNo = 1234,
            BusNo = 5678,
            Cluster = "B32",
            Domain = "nice-incontact.com"
        };
        WhoAmIResponse mockWhoAmIResponse = new WhoAmIResponse
        {
            AgentNo = 1234,
            ResourceBaseUri = "https://api-do77.dev.nice-incontact.com/InContactAPI/"
        };

        GetUserResponse mockUserResponse = new GetUserResponse
        {
            UserName = "agent@customer.com",
            TeamId = 3456
        };


        OpenIdConfiguration mockOpenIdConfig = new OpenIdConfiguration();        
        mockAuthClient.Setup(c => c.ClientId).Returns("test-client-id");
        mockAuthClient.Setup(c => c.ClientSecret).Returns("test-client-secret");
        mockAuthClient.Setup(c => c.AuthenticationServer).Returns("https://server-host.openid");
        mockAuthClient.Setup(c => c.RedirectUri).Returns("https://client-host.cb");
        mockAuthClient.Setup(x => x.Configuration).Returns(mockOpenIdConfig);
        mockAuthClient.Setup(x => x.GenerateToken(It.IsAny<string>(), null)).Returns(mockToken);
        mockAuthClient.Setup(x => x.ParseClaims(It.IsAny<string>())).Returns(mockCXoneConfig);
        mockAuthClient.Setup(x => x.GetWhoAmI(mockCXoneConfig, mockToken)).Returns(mockWhoAmIResponse);
        mockAuthClient.Setup(x => x.GetUserDetails(mockCXoneConfig, mockToken, out errorMsg)).Returns(mockUserResponse);
        mockAuthClient.Setup(x => x.GetAuthorizeUrl(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns("");
    }
}
}
