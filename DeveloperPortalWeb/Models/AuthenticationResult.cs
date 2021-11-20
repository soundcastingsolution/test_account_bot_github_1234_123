
namespace InContact.DeveloperPortal.Web.Models
{
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; }
        public DeveloperAuthenticationToken Token { get; set; }

        public AuthenticationResult()
        {
            Token = new DeveloperAuthenticationToken();
        }
    }
}