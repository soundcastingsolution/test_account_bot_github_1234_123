using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using InContact.DeveloperPortal.Web.Common;

namespace InContact.DeveloperPortal.Web.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GetUserResponse : RestResponse
    {

        [JsonProperty("agentId")]
        private int AgentId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }
        
        [JsonProperty("teamId")]
        public int TeamId { get; set; }
        
        public override string ToString()
        {
            return String.Format("{{ AgentNo: {0}, UserName:{1}, FirstName:{2}, MiddleName:{3}, LastName:{4}, UserId:{5}, EmailAddress:{6} }}",
                AgentId, UserName, FirstName, MiddleName, LastName, UserId, EmailAddress);
        }

    }
}
