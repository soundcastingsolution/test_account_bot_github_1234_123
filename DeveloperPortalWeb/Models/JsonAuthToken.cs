
namespace InContact.DeveloperPortal.Web.Models
{
    public class JsonAuthToken
    {
        public string AccessToken { get; set; }
        public int BusinessUnitNumber { get; set; }
        public string TokenType { get; set; }
        public string ResourceServerBaseUri { get; set; }
        public bool Authenticated { get; set; }
    }
}