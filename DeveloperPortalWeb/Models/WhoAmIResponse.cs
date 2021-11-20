using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using InContact.DeveloperPortal.Web.Common;

namespace InContact.DeveloperPortal.Web.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WhoAmIResponse : RestResponse
    {
        [JsonProperty("agent_id")]
        public int AgentNo { get; set; }

        [JsonProperty("team_id")]
        public int TeamNo { get; set; }

        [JsonProperty("bus_no")]
        public int BusNo { get; set; }

        [JsonProperty("iss")]
        public string Issuer { get; set; }

        [JsonProperty("sub")]
        public string Subject { get; set; }

        [JsonProperty("resource_server_base_uri")]
        public string ResourceBaseUri { get; set; }

        [JsonProperty("refresh_token_server_uri")]
        public string TokenBaseUri { get; set; }

        public override string ToString()
        {
            return String.Format("{{ AgentNo: {0}, TeamNo:{1}, BusNo:{2}, Issuer:{3}, Subject:{4}, ResourceBaseUri:{5}, TokenBaseUri:{6} }}",
                AgentNo, TeamNo, BusNo, Issuer, Subject, ResourceBaseUri, TokenBaseUri);
        }
    }
}