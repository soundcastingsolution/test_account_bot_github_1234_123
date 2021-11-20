using InContact.DeveloperPortal.Common;
using InContact.DeveloperPortal.Web.Authentication;
using InContact.DeveloperPortal.Web.Common;
using InContact.DeveloperPortal.Web.Logic;
using InContact.DeveloperPortal.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InContact.DeveloperPortal.Web.Controllers
{
    public class AccountController : DeveloperPortalControllerBase
    {
        private readonly IAccountLogic _accountLogic;
        private readonly NameValueCollection _appSettings;
        private IElmahWrapper _elmah;
        const string _loginError = "LoginError";

        public AccountController(IAccountLogic accountLogic, NameValueCollection appSettings, IElmahWrapper elmah)
        {
            _elmah = elmah;
            _appSettings = appSettings;
            _accountLogic = accountLogic;
        }

        public ActionResult Index()
        {
            ViewBag.NoDownloadAsPDF = true;
            return View();
        }

        public ActionResult SignIn(string returnUrl, bool isFedramp = false)
        {
            // if the user directly enter the url when the login is disabled
            if (!ConfigurationReader.IsLoginEnabled)
            {
                return RedirectToAction("index", "home");
            }
            //Clear the return url, if the user is logging from error page.
            if (returnUrl.Contains("Account/Authenticate"))
            {
                returnUrl = "/";
            }
            string authServer = isFedramp ? _appSettings["CXoneGovAuthentication"] : _appSettings["CXoneAuthentication"];
            ICXoneAuthClient authClient = new CXoneAuthClient(authServer, _appSettings["ClientId"], _appSettings["ClientSecret"], AuthCallbackUrl);
            CXoneClientState clientState = new CXoneClientState("DEVone", returnUrl, authClient.AuthenticationServer);
            Session["ClientState"] = clientState;
            string authEndpoint = authClient.GetAuthorizeUrl(OpenIdKey.OpenID, OpenIdKey.Code, OpenIdKey.Popup, clientState.UrlEncode());
            Logging.LogMessage(string.Format("[AccountController][SignIn]: returnUrl:{0}, isFedramp: {1}, clientState:{2}, authEndpoint: {3}", returnUrl, isFedramp, clientState.Id, authEndpoint));
            if (string.IsNullOrEmpty(authEndpoint))
            {
                return new EmptyResult();
            }
            return Redirect(authEndpoint);
        }

        public ActionResult Authenticate()
        {
            Uri navigatingUri = new Uri(Request.Url.AbsoluteUri);
            NameValueCollection queryParams = HttpUtility.ParseQueryString(navigatingUri.Query);
            string error = queryParams[OpenIdKey.Error];
            string errorDesc = queryParams[OpenIdKey.ErrorDescription];
            string loginError = queryParams[_loginError];

            if (!string.IsNullOrEmpty(loginError))
            {
                ViewBag.error = loginError;
                return View();
            }
            if (!string.IsNullOrEmpty(error))
            {
                ViewBag.error = error;
                if (!string.IsNullOrEmpty(errorDesc))
                {
                    ViewBag.errorDesc = errorDesc;
                }
                return View();
            }
            string state = HttpUtility.UrlDecode(queryParams[OpenIdKey.State]);
            try
            {
                CXoneClientState callbackState = CXoneClientState.UrlDecode(state);
                CXoneClientState sessionState = Session["ClientState"] as CXoneClientState;
                string code = queryParams[OpenIdKey.Code];
                Logging.LogMessage(string.Format("[AccountController][Authenticate] authcode: {0}, callbackClientState: {1}, sessionClientState: {2}", code, callbackState.Id, sessionState.Id));
                if (!string.IsNullOrEmpty(code) && string.Equals(sessionState.Id, callbackState.Id))
                {
                    ViewBag.AuthCode = code;
                    // Store client state object to retrive the redirect url and redirect user after authentication 
                    ViewBag.ClientState = state;
                    return View();
                }
                else
                {
                    ViewBag.error = "Code is empty or state is corrupted";
                    return View();
                }
            }
            catch (Exception ex)
            {
                Logging.LogMessage(string.Format("Error: {0}", ex.Message.ToString()));
                ViewBag.error = "Code is empty or state is corrupted";
                return View();
            }

        }

        public ActionResult AuthenticateUser(string authCode, string clientState)
        {
            CXoneClientState sessionState = Session["ClientState"] as CXoneClientState;
            CXoneClientState callbackState = CXoneClientState.UrlDecode(clientState);
            Logging.LogMessage(string.Format("[AccountController][AuthenticateUser] authcode: {0}, callbackClientState: {1}, sessionClientState: {2}", authCode, callbackState.Id, sessionState.Id));
            if (string.IsNullOrEmpty(authCode))
            {
                return Json(new
                {
                    error = "Authcode cannot be null or empty"
                });
            }
            string endSessionUrl = null;
            try
            {
                if (!string.Equals(sessionState.Id, callbackState.Id))
                {
                    return Json(new
                    {
                        error = "Invalid client_state"
                    });
                }
                CXoneAuthClient authClient = new CXoneAuthClient(callbackState.CXoneAuthServer, _appSettings["ClientId"], _appSettings["ClientSecret"], AuthCallbackUrl);
                endSessionUrl = authClient.GetEndSessionUrl(AuthCallbackUrl);
                AuthenticationResult authResult = DoAuthentication(authClient, authCode);
                Logging.LogMessage(string.Format("[AccountController][AuthenticateUser] clientState: {0}, authcode: {1}, isUserAuthenticated: {2}", callbackState.Id, authCode, authResult.IsAuthenticated));
                if (authResult.IsAuthenticated)
                {
                    _accountLogic.SetCookie(authResult.Token);
                    HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    Logging.LogMessage(string.Format("[AccountController][AuthenticateUser]: Cookie Name: {0}, Cookie Value: {1}, Cookie Domain: {2}", authCookie.Name, authCookie.Value, authCookie.Domain));
                    CookieContainer cookieContainer = new CookieContainer();
                    Cookie cookie = new Cookie
                    {
                        Name = authCookie.Name,
                        Value = authCookie.Value,
                        Domain = authCookie.Domain
                    };
                    cookieContainer.Add(cookie);
                    var handler = new HttpClientHandler()
                    {
                        CookieContainer = cookieContainer
                    };
                    var client = new HttpClient(handler);
                    SigninViewModel viewModel = new SigninViewModel
                    {
                        UserName = authResult.Token.UserName
                    };
                    CreateForumUser(viewModel, client);
                }
                else
                {
                    Logging.LogMessage(string.Format("Error: {0}", authResult.Message));
                    return Json(new
                    {
                        error = authResult.Message,
                        logoutUrl = endSessionUrl + "?LoginError=" + authResult.Message
                    });
                }
            }
            catch (Exception ex)
            {
                Logging.LogMessage(string.Format("Error: {0}", ex.Message.ToString()));
                return Json(new
                {
                    error = "Error: Unable to login",
                    logoutUrl = endSessionUrl + "?LoginError=" + "Error: Unable to login"
                });
            }
            return Json(new { url = callbackState.ReturnUrl });
        }

        public ActionResult SignOut(string returnUrl)
        {
            string devPortalUrl = Request.Url.Scheme + Uri.SchemeDelimiter + Request.Url.Authority;
            CXoneClientState clientState = null;
            string authServer = string.Empty;
            if (!String.IsNullOrEmpty(returnUrl))
                devPortalUrl = devPortalUrl + returnUrl;
            if (Session["ClientState"] != null)
            {
                clientState = Session["ClientState"] as CXoneClientState;
                authServer = clientState.CXoneAuthServer;
            }
            else
            {
                authServer = JObject.Parse(Request.Cookies["zDevToken"].Value)["AuthServer"].ToString();
            }
            CXoneAuthClient authClient = new CXoneAuthClient(authServer, _appSettings["ClientId"], _appSettings["ClientSecret"], AuthCallbackUrl);
            string endSessionEndPoint = authClient.GetEndSessionUrl(devPortalUrl);
            ViewBag.NoDownloadAsPDF = true;

            //Clear the session and cookie data
            _accountLogic.ClearCookie();
            Session.Clear();
            Logging.LogMessage(string.Format("[AccountController][SignOut] returnUrl: {0}, endSessionEndPoint: {1}", returnUrl, endSessionEndPoint));
            return Redirect(endSessionEndPoint);
        }

        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public object GetAccessToken(string apiHost = "ACD")
        {
            Uri apiUrl;
            if (User.AccessToken == null)
            {
                return Json(new JsonAuthToken
                {
                    AccessToken = "",
                    TokenType = "",
                    BusinessUnitNumber = 0,
                    ResourceServerBaseUri = "",
                    Authenticated = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //Construct a URL from the user and version                  
                if (apiHost.Equals("UserHub", StringComparison.OrdinalIgnoreCase))
                {
                    apiUrl = new Uri(User.ApiResourceBaseUri);
                }
                else if (apiHost.Equals("Digital", StringComparison.OrdinalIgnoreCase))
                {
                    apiUrl = new Uri(new Uri(User.DigitalApiResourceBaseUri), _appSettings["DigitalAPIVersion"]);
                }
                else
                {
                    apiUrl = new Uri(new Uri(User.AcdResourceServerBaseUri), _appSettings["APIVersion"]);
                }
                //Return the data needed client side
                return Json(new JsonAuthToken
                {
                    AccessToken = User.AccessToken,
                    TokenType = User.TokenType,
                    BusinessUnitNumber = User.BusinessUnitNumber,
                    ResourceServerBaseUri = apiUrl.ToString(),
                    Authenticated = true
                }, JsonRequestBehavior.AllowGet);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signinViewModel"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool CreateForumUser(SigninViewModel signinViewModel, HttpClient client)
        {
            ServicePointManager.ServerCertificateValidationCallback = MyCertHandler;

            bool result = false;
            try
            {
                var user = new InContactUserModel()
                {
                    UserName = signinViewModel.UserName,
                    VendorName = signinViewModel.VendorName
                };

                string userSerialized = JsonConvert.SerializeObject(user);
                var uriBuilder = new UriBuilder(HttpContext.Request.Url)
                {
                    Path = "/Forum/InContactMembership/CreateUser",
                    Query = ""
                };

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var postasync = client.PostAsync(uriBuilder.Uri, new StringContent(userSerialized, Encoding.UTF8, "application/json"));
                var response = postasync.Result;
                string strResult = response.Content.ReadAsStringAsync().Result;
                result = strResult == "\"success\"";

                if (!response.IsSuccessStatusCode || !result)
                {
                    _elmah.Raise(new ApplicationException("AccountController.CreateForumUser().  Username= " + signinViewModel.UserName + ", response.IsSuccessStatusCode=" + response.IsSuccessStatusCode + ", result=" + result));
                }
            }
            catch (Exception ex)
            {
                Logging.LogMessage(string.Format("Error: {0}", ex.Message.ToString()));
                _elmah.Raise(ex);
            }
            return result;
        }

        static bool MyCertHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors error)
        {
            // Ignore errors
            return true;
        }

        private string AuthCallbackUrl
        {
            get
            {
                var redirectUrl = new System.UriBuilder(Request.Url.AbsoluteUri)
                {
                    Path = Url.Action("Authenticate", "Account"),
                    Query = null,
                };
                return redirectUrl.ToString();
            }
        }

        private AuthenticationResult DoAuthentication(CXoneAuthClient authClient, string authCode)
        {
            string errorMsg = string.Empty;
            Logging.LogMessage("[AccountController][DoAuthentication] User authentication");
            AuthenticationResult portalAuthResult = new AuthenticationResult();
            try
            {
                OpenIDConnectToken openIdToken = authClient.GenerateToken(authCode);
                if (openIdToken != null)
                {
                    Logging.LogMessage(string.Format("[AccountController][DoAuthentication]: DoAuthentication Method of account controller invoked, Authcode : {0} Token : {1}", authCode, openIdToken.IdToken));
                    CXoneConfiguration cxoneConfig = authClient.ParseClaims(openIdToken.IdToken.ToString());
                    WhoAmIResponse whoAmIresponse = authClient.GetWhoAmI(cxoneConfig, openIdToken);
                    cxoneConfig.AcdApiResourceBaseUri = cxoneConfig.IsUserHub ? String.Format("https://api-{0}.{1}/incontactapi/", cxoneConfig.Cluster, cxoneConfig.Domain) : whoAmIresponse.ResourceBaseUri;
                    cxoneConfig.AgentNo = whoAmIresponse.AgentNo;
                    GetUserResponse userResponse = authClient.GetUserDetails(cxoneConfig, openIdToken, out errorMsg);
                    if (string.IsNullOrEmpty(errorMsg) && userResponse != null)
                    {
                        Logging.LogMessage(string.Format("[AccountController][DoAuthentication]: DoAuthentication Method of account controller invoked, Cluster : {0} Domain : {1} UserId : {2} AgentNo : {3}, Username : {4},TeamId : {5}", cxoneConfig.Cluster, cxoneConfig.Domain, cxoneConfig.UserId, cxoneConfig.AgentNo, userResponse.UserName, userResponse.TeamId));
                        portalAuthResult.IsAuthenticated = true;
                        portalAuthResult.Token = new DeveloperAuthenticationToken()
                        {
                            BusinessUnitNumber = cxoneConfig.BusNo,
                            AgentId = cxoneConfig.AgentNo,
                            UserName = userResponse.UserName,
                            ExpiresIn = Convert.ToInt32(openIdToken.ExpiresIn),
                            AuthorizationToken = openIdToken.IdToken,
                            AccessToken = openIdToken.AccessToken,
                            TokenType = openIdToken.TokenType,
                            RefreshToken = openIdToken.RefreshToken,
                            TeamId = userResponse.TeamId,
                            ResourceServerBaseUri = cxoneConfig.AcdApiResourceBaseUri,
                            ApiResourceBaseUri = cxoneConfig.ApiResourceBaseUri,
                            DigitalApiResourceBaseUri = cxoneConfig.DigitalApiResourceBaseUri,
                            AuthServer = authClient.AuthenticationServer
                        };
                    }
                    else
                    {
                        Logging.LogMessage(string.Format("Error: {0}", "Failed to get the user details, user is restricted"));
                        portalAuthResult = new AuthenticationResult
                        {
                            IsAuthenticated = false,
                            Message = string.IsNullOrEmpty(errorMsg) ? "Failed to get the user details, please try with someother user" : errorMsg
                        };
                    }
                }
                else
                {
                    Logging.LogMessage(string.Format("Error: {0}", "Failed to generate token, invalid auth code"));
                    portalAuthResult = new AuthenticationResult
                    {
                        IsAuthenticated = false,
                        Message = "Failed to generate token,invalid auth code."
                    };
                }
            }
            catch (Exception ex)
            {
                Logging.LogMessage(string.Format("Error: {0}", ex.Message.ToString()));
                portalAuthResult = new AuthenticationResult
                {
                    IsAuthenticated = false,
                    Message = "Something went wrong while authenticating the user, please try with someother user credentials."
                };
            }
            return portalAuthResult;
        }
    }
}