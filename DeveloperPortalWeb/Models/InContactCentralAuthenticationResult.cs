
namespace InContact.DeveloperPortal.Web.Models
{
    public class InContactCentralAuthenticationResult
    {
        public string auth_token { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public string resource_server_base_uri { get; set; }
        public string refresh_token_server_uri { get; set; }
        public int agent_id { get; set; }
        public int team_id { get; set; }
        public int bus_no { get; set; }
        public int status_code { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
    }
}