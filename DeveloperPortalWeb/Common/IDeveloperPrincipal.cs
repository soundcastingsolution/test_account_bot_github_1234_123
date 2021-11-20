using System.Security.Principal;

namespace InContact.DeveloperPortal.Web.Common
{
    public interface IDeveloperPrincipal : IPrincipal
    {
        new IIdentity Identity { get; set; }
        string AccessToken { get; set; }
        string TokenType { get; set; }
        int BusinessUnitNumber { get; set; }
        int AgentId { get; set; }
        string UserName { get; set; }
        bool NeedsRefresh { get; set; }
        string AcdResourceServerBaseUri { get; set; }
        string ApiResourceBaseUri { get; set; }
        string DigitalApiResourceBaseUri { get; set; }
    }
}
