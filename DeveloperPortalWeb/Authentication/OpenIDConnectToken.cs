using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using InContact.DeveloperPortal.Web.Common;

namespace InContact.DeveloperPortal.Web.Authentication
{
    [JsonObject(MemberSerialization.OptIn)]
    public class OpenIDConnectToken 
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        public override string ToString()
        {
            return String.Format("{{ AccessToken: {0}, TokenType:{1}, ExpiresIn:{2}, RefreshToken:{3}, IdToken:{4} }}",
                AccessToken, TokenType, ExpiresIn, RefreshToken, IdToken);
        }
    }
}