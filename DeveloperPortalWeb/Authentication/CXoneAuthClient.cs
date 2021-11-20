using System;
using System.Linq;
using System.Web;
using InContact.DeveloperPortal.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using InContact.DeveloperPortal.Web.Common;

namespace InContact.DeveloperPortal.Web.Authentication
{
    public class CXoneAuthClient : ICXoneAuthClient
    {
        #region Constants    

        const string Path_OpenIdConfig = "/.well-known/openid-configuration";

        #endregion

        #region Public Properties

        public string AuthenticationServer { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RedirectUri { get; set; }

        public CXoneConfiguration CXoneConfig { get; set; }

        public OpenIdConfiguration Configuration { get; set; }

        #endregion

        #region Constructor

        public CXoneAuthClient(string authServer, string clientId, string clientSecret, string callbackUrl)
        {
            AuthenticationServer = authServer;
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = callbackUrl;
        }

        #endregion

        #region Public Methods

        public OpenIdConfiguration GetConfiguration()
        {
            try
            {
                Logging.LogMessage("[AuthClient][GetConfiguration] Get the openId configuration");
                string openIdConfigUrl = Combine(AuthenticationServer, Path_OpenIdConfig);
                RestResponse restResponse = RestClient.HttpGet(openIdConfigUrl);
                if (restResponse.IsSuccess)
                {
                    Configuration = JsonConvert.DeserializeObject<OpenIdConfiguration>(restResponse.Content);
                }
                else
                {
                    Logging.LogError("[AuthClient][GetConfiguration] API Error {0}", restResponse);
                }
            }
            catch (Exception ex)
            {
                Logging.LogException(ex, "[AuthClient][GetConfiguration]");
            }
            return Configuration;
        }

        public string GetAuthorizeUrl(string scope, string responseType, string display, string state)
        {
            Logging.LogMessage("[AuthClient][GetAuthorizeUrl] Get the Authorization  URL");
            GetConfiguration();
            if (Configuration == null)
                throw new Exception("Configuration is not initialized, invoke DiscoverConfiguration()");
            string authUrl = Configuration.AuthorizationEndpoint;
            if (String.IsNullOrWhiteSpace(authUrl))
                throw new Exception("authorization_endpoint can not be empty");
            if (!Configuration.ScopesSupported.Contains(scope))
                throw new Exception("scope not supported");
            if (!Configuration.ResponseTypesSupported.Contains(responseType))
                throw new Exception("responseType not supported");
            if (!Configuration.DisplayValuesSupported.Contains(display))
                throw new Exception("display not supported");
            return String.Format("{0}?{1}={2}&{3}={4}&{5}={6}&{7}={8}&{9}={10}&{11}={12}", authUrl, OpenIdKey.Scope, scope, OpenIdKey.ResponseType, responseType,
               OpenIdKey.Display, display, OpenIdKey.ClientId, ClientId, OpenIdKey.RedirectUri, HttpUtility.UrlEncode(RedirectUri), OpenIdKey.State, state);
        }

        public OpenIDConnectToken GenerateToken(string code = null, string refershToken = null)
        {
            Logging.LogMessage(string.Format("[AuthClient][GenerateToken] Generate token for authcode:{0}", code));
            RestResponse restResponse = null;
            GetConfiguration();
            if (Configuration == null)
                throw new Exception("Configuration is not initialized, invoke DiscoverConfiguration()");
            if (String.IsNullOrWhiteSpace(Configuration.TokenEndpoint))
                throw new Exception("token_endpoint can not be empty");
            try
            {
                Logging.LogMessage(string.Format("[AuthClient][GenerateToken] Generate token for authcode: {0} url: {1}", code, Configuration.TokenEndpoint));
                JObject tokenReqData = new JObject();
                if (!string.IsNullOrEmpty(code))
                {
                    tokenReqData.Add("grant_type", "authorization_code");
                    tokenReqData.Add("code", code);
                    tokenReqData.Add("redirect_uri", RedirectUri);
                }
                else if (!string.IsNullOrEmpty(refershToken))
                {
                    tokenReqData.Add("grant_type", "refresh_token");
                    tokenReqData.Add("refresh_token", refershToken);
                }
                string authValue = Base64Encode(String.Format("{0}:{1}", ClientId, ClientSecret));
                restResponse = RestClient.HttpPost("Basic", authValue, Configuration.TokenEndpoint, tokenReqData, RestClient.Content_UrlEncoded);
                if (restResponse.IsSuccess)
                {
                    return JsonConvert.DeserializeObject<OpenIDConnectToken>(restResponse.Content);
                }
                else
                {
                    Logging.LogError("[AuthClient][GenerateToken] API Error {0}", restResponse);
                }
            }
            catch (Exception ex)
            {
                Logging.LogException(ex, "[AuthClient][GenerateToken]");                
            }
            return null;
        }

        public CXoneConfiguration ParseClaims(string idToken)
        {
            try
            {
                Logging.LogMessage(string.Format("[AuthClient][ParseClaims] Parse JWT  token:{0}", idToken));
                var handler = new JwtSecurityTokenHandler();
                SecurityToken securityIdToken = handler.ReadToken(idToken);
                JwtSecurityToken jwtIdToken = (securityIdToken as JwtSecurityToken);
                CXoneConfig = new CXoneConfiguration();
                foreach (Claim claim in jwtIdToken.Claims)
                {
                    if (claim.Type == "icClusterId")
                        CXoneConfig.Cluster = claim.Value;
                    else if (claim.Type == "icDomain")
                        CXoneConfig.Domain = claim.Value;
                    else if (claim.Type == "userId")
                        CXoneConfig.UserId = claim.Value;
                    else if (claim.Type == "icAgentId")
                        CXoneConfig.AgentNo = int.Parse(claim.Value);
                    else if (claim.Type == "icBUId")
                        CXoneConfig.BusNo = int.Parse(claim.Value);
                }
                Logging.LogMessage(string.Format("[AuthClient][ParseClaims] Parsed claims, cluster:{0} domain:{1}, userId:{2}", CXoneConfig.Cluster, CXoneConfig.Domain, CXoneConfig.UserId));
                if (String.IsNullOrEmpty(CXoneConfig.Cluster))
                {
                    Logging.LogMessage(string.Format("[AuthClient][ParseClaims] Cluster claim not found, setting to C4 and incontact.com domain"));
                    CXoneConfig.Cluster = "c4";
                    CXoneConfig.Domain = "incontact.com";
                }
                else
                {
                    // to get the cxone api url the condition is removed.the cxone api url needs to be computed yet.
                    CXoneConfig.UserHubArea = GetUserHubArea(CXoneConfig.Cluster);
                    if (String.IsNullOrEmpty(CXoneConfig.Domain))
                    {
                        CXoneConfig.Domain = GetClusterDomain(CXoneConfig.Cluster);
                    }
                    Logging.LogMessage(string.Format("[AuthClient][ParseClaims] Determined UserHubArea:{0} Domain:{1}", CXoneConfig.UserHubArea, CXoneConfig.Domain));
                }
                return CXoneConfig;
            }
            catch (Exception ex)
            {
                Logging.LogException(ex, "[AuthClient][ParseClaims]");
            }
            return CXoneConfig;
        }

        public GetUserResponse GetUserDetails(CXoneConfiguration cxoneConfig, OpenIDConnectToken token, out string errorMsg)
        {
            errorMsg = string.Empty;
            Logging.LogMessage(string.Format("[AuthClient][GetUserDetails] Fetching user details using AgentId  :{0}", cxoneConfig.AgentNo));
            string getUserEndPoint = cxoneConfig.IsUserHub ? string.Format("/services/v20.0/agents/{0}", cxoneConfig.UserId) : string.Format("/services/v20.0/agents/{0}", cxoneConfig.AgentNo);
            string getUserApiEndPoint = Combine(cxoneConfig.AcdApiResourceBaseUri, getUserEndPoint);
            try
            {
                RestResponse restResponse = RestClient.HttpGet(getUserApiEndPoint, "Bearer", token.AccessToken);
                if (restResponse.IsSuccess)
                {
                    JObject json = JObject.Parse(restResponse.Content);
                    return JsonConvert.DeserializeObject<GetUserResponse>(json["agents"][0].ToString());
                }
                else
                {
                    Logging.LogError("[AuthClient][GetUserDetails] API Error {0}", restResponse);
                    errorMsg = restResponse.Content + " <br> Status Description : " + restResponse.StatusDescription;
                }
            }
            catch (Exception ex)
            {
                Logging.LogException(ex, "[AuthClient][GetUserDetails]");
            }
            return null;
        }

        public WhoAmIResponse GetWhoAmI(CXoneConfiguration cxoneConfig, OpenIDConnectToken token)
        {
            WhoAmIResponse WhoAmIResponse = new WhoAmIResponse();
            try
            {
                string authValue = Base64Encode(String.Format("{0}:{1}", ClientId, ClientSecret));
                JObject whoAmIReqData = new JObject();
                whoAmIReqData.Add("token", token.AccessToken);
                RestResponse restResponse = RestClient.HttpPost("Basic", authValue, CXoneConfig.AcdAuthorizationTokenEndpoint + "/whoami", whoAmIReqData, RestClient.Content_UrlEncoded);
                if (restResponse.IsSuccess)
                {
                    WhoAmIResponse = JsonConvert.DeserializeObject<WhoAmIResponse>(restResponse.Content);
                }
                else
                {
                    Logging.LogError("[AuthClient][WhoAmI] API Error {0}", restResponse);
                }
            }
            catch (Exception ex)
            {
                Logging.LogException(ex, "[AuthClient][WhoAmI]");
            }
            return WhoAmIResponse;
        }

        public string GetEndSessionUrl(string postLogoutUri = null)
        {
            GetConfiguration();
            if (string.IsNullOrWhiteSpace(Configuration.EndSessionEndpoint))
                return postLogoutUri;
            return String.Format("{0}?post_logout_redirect_uri={1}", Configuration.EndSessionEndpoint, postLogoutUri);
        }

        #endregion

        #region helper methods

        private string Base64Encode(string toEncode)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(toEncode);
            return Convert.ToBase64String(bytes);
        }

        private string Combine(string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return string.Format("{0}/{1}", uri1, uri2);
        }


        private string GetClusterDomain(string clusterId)
        {
            string Cluster=string.Empty;
            Logging.LogMessage(string.Format("[AuthClient][GetClusterDomain] Determining Cluster Domain from clusterId:{0}", clusterId));
            if (!clusterId.Equals("C7", StringComparison.InvariantCultureIgnoreCase) &&
                clusterId.StartsWith("C7", StringComparison.InvariantCultureIgnoreCase))
                return "niceincontact.com";
            else if (clusterId.StartsWith("C", StringComparison.InvariantCultureIgnoreCase) ||
                clusterId.StartsWith("B", StringComparison.InvariantCultureIgnoreCase))
                return "nice-incontact.com";
            else if (clusterId.StartsWith("A", StringComparison.InvariantCultureIgnoreCase))
                return "nice-incontact.com";
            else if (clusterId.StartsWith("E", StringComparison.InvariantCultureIgnoreCase))
                return "niceincontact.com";
            else if (clusterId.StartsWith("SO", StringComparison.InvariantCultureIgnoreCase))
                return "staging.nice-incontact.com";
            else if (clusterId.StartsWith("SC", StringComparison.InvariantCultureIgnoreCase) ||
                    clusterId.StartsWith("TC", StringComparison.InvariantCultureIgnoreCase))
                return "ucnlabext.com";
            else if (clusterId.StartsWith("TO", StringComparison.InvariantCultureIgnoreCase))
                return "test.nice-incontact.com";
            else if (clusterId.StartsWith("TV", StringComparison.InvariantCultureIgnoreCase))
                return "test.niceincontact.com";
            return "dev.nice-incontact.com";
        }

        private string GetUserHubArea(string clusterId)
        {
            Logging.LogMessage(string.Format("[AuthClient][GetUserHubArea] Determining UserHub Area from clusterId:{0}", clusterId));
            if (!clusterId.Equals("C7", StringComparison.InvariantCultureIgnoreCase) &&
                clusterId.StartsWith("C7", StringComparison.InvariantCultureIgnoreCase))
                return "na2";
            else if (clusterId.StartsWith("A", StringComparison.InvariantCultureIgnoreCase))
                return "au1";
            else if (clusterId.StartsWith("E", StringComparison.InvariantCultureIgnoreCase))
                return "eu1";
            else if (clusterId.StartsWith("TV", StringComparison.InvariantCultureIgnoreCase))
                return "nvir";
            return "na1";
        }

        #endregion
    }
}