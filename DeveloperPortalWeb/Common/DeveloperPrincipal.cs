using System.Security.Principal;
using System.Web.Security;
using InContact.DeveloperPortal.Web.Models;

namespace InContact.DeveloperPortal.Web.Common
{
    public class DeveloperPrincipal : IDeveloperPrincipal
    {
        public IIdentity Identity { get; set; }

        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int BusinessUnitNumber { get; set; }
        public int AgentId { get; set; }
        public string UserName { get; set; }
        public bool NeedsRefresh { get; set; }
        public string AcdResourceServerBaseUri { get; set; }
        public string ApiResourceBaseUri { get; set; }
        public string DigitalApiResourceBaseUri { get; set; }
        public bool IsInRole(string role)
        {
            return Roles.IsUserInRole(role);
        }

        public DeveloperPrincipal(IIdentity identity)
        {
            this.Identity = identity;
        }

        public void AddDeveloperTokenInformation(DeveloperAuthenticationToken token)
        {
            AccessToken = token.AccessToken;
            AgentId = token.AgentId;
            TokenType = token.TokenType;
            BusinessUnitNumber = token.BusinessUnitNumber;
            UserName = token.UserName;
            NeedsRefresh = token.NeedsRefresh;
            AcdResourceServerBaseUri = token.ResourceServerBaseUri;
            ApiResourceBaseUri = token.ApiResourceBaseUri;
            DigitalApiResourceBaseUri = token.DigitalApiResourceBaseUri;
        }
    }
}
