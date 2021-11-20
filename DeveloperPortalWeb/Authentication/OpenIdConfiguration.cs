using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace InContact.DeveloperPortal.Web.Authentication
{
    [JsonObject(MemberSerialization.OptIn)]
    public class OpenIdConfiguration
    {
        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("authorization_endpoint")]
        public string AuthorizationEndpoint { get; set; }

        [JsonProperty("display_values_supported")]
        public string[] DisplayValuesSupported { get; set; }

        [JsonProperty("response_types_supported")]
        public string[] ResponseTypesSupported { get; set; }

        [JsonProperty("scopes_supported")]
        public string[] ScopesSupported { get; set; }

        [JsonProperty("token_endpoint")]
        public string TokenEndpoint { get; set; }

        [JsonProperty("end_session_endpoint")]
        public string EndSessionEndpoint { get; set; }
    }
}