using InContact.DeveloperPortal.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InContact.DeveloperPortal.Web.Authentication
{
    public interface ICXoneAuthClient
    {
        string AuthenticationServer { get; set; }
        string ClientId { get; set; }

        string ClientSecret { get; set; }

        string RedirectUri { get; set; }

        CXoneConfiguration CXoneConfig { get; set; }

        OpenIdConfiguration Configuration { get; set; }

        string GetAuthorizeUrl(string scope, string responseType, string display, string state);

        string GetEndSessionUrl(string postLogoutUri = null);

        OpenIDConnectToken GenerateToken(string code = null, string refershToken = null);

        CXoneConfiguration ParseClaims(string idToken);

        GetUserResponse GetUserDetails(CXoneConfiguration cxoneConfig, OpenIDConnectToken token, out string errorMsg);

        WhoAmIResponse GetWhoAmI(CXoneConfiguration cxoneConfig, OpenIDConnectToken token);
    }
}