using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InContact.DeveloperPortal.Web.Authentication
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CXoneClientState 
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("authServer")]
        public string CXoneAuthServer { get; set; }

        [JsonProperty("returnUrl")]
        public string  ReturnUrl { get; set; }

        public CXoneClientState(string name, string returnUrl, string authServer)
        {
            Name = name;
            Id = Guid.NewGuid();
            ReturnUrl = returnUrl;
            CXoneAuthServer = authServer;
        }

        /// <summary>
        /// Gets base64 string that can be passed to IDP in query parameter
        /// </summary>
        /// <returns></returns>
        public string UrlEncode()
        {
            string json = JsonConvert.SerializeObject(this);
            return Base64UrlEncoder.Encode(json);
        }

        /// <summary>
        /// Parse the query parameter from IDP  into object
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static CXoneClientState UrlDecode(string base64)
        {
            string json = Base64UrlEncoder.Decode(base64);
            return JsonConvert.DeserializeObject<CXoneClientState>(json);
        }
       
        public override bool Equals(object obj)
        {
            if (obj is CXoneClientState)
            {
                CXoneClientState obj2 = obj as CXoneClientState;
                return obj2.Name == this.Name && obj2.Id == this.Id && obj2.ReturnUrl == this.ReturnUrl;
            }
            return false;
        }
        public override int GetHashCode()
        {
            var hashCode = 939761442;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(Id);
            return hashCode;
        }
    }
}