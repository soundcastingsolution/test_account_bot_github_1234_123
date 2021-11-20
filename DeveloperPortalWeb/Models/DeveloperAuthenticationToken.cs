using System;
using System.Web.Script.Serialization;

namespace InContact.DeveloperPortal.Web.Models
{
    public class DeveloperAuthenticationToken
    {
        public int BusinessUnitNumber { get; set; }
        public int AgentId { get; set; }
        public string UserName { get; set; }

        //Number of seconds until the token expires
        public int ExpiresIn { get; set; }

        public string AuthorizationToken { get; set; }
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string RefreshToken { get; set; }
        public string ResourceServerBaseUri { get; set; }
        public string ApiResourceBaseUri { get; set; }
        public string DigitalApiResourceBaseUri { get; set; }
        public string RefreshTokenServerUri { get; set; }
        public string Scope { get; set; }
        public int TeamId { get; set; }
        public DateTime? TokenRetrievalDateTimeUTC { get; set; }
        public string  AuthServer { get; set; }

        //Returns the time we retrieved the token plus ExpiresIn
        public DateTime? ExpirationDateTimeUTC
        {
            get
            {
                DateTime? returnValue = TokenRetrievalDateTimeUTC;
                if (null != returnValue)
                    returnValue = ((DateTime)returnValue).AddSeconds(ExpiresIn);

                return returnValue;
            }
        }

        public DeveloperAuthenticationToken() { }

        public DeveloperAuthenticationToken(string serializedObject)
        {
            var serializer = new JavaScriptSerializer();
            var deserializedObject = serializer.Deserialize<DeveloperAuthenticationToken>(serializedObject);

            BusinessUnitNumber = deserializedObject.BusinessUnitNumber;
            AgentId = deserializedObject.AgentId;
            UserName = deserializedObject.UserName;
            ExpiresIn = deserializedObject.ExpiresIn;
            AccessToken = deserializedObject.AccessToken;
            TokenType = deserializedObject.TokenType;
            RefreshToken = deserializedObject.RefreshToken;
            ResourceServerBaseUri = deserializedObject.ResourceServerBaseUri;
            ApiResourceBaseUri = deserializedObject.ApiResourceBaseUri;
            DigitalApiResourceBaseUri = deserializedObject.DigitalApiResourceBaseUri;
            RefreshTokenServerUri = deserializedObject.RefreshTokenServerUri;
            Scope = deserializedObject.Scope;
            TeamId = deserializedObject.TeamId;
            TokenRetrievalDateTimeUTC = deserializedObject.TokenRetrievalDateTimeUTC;
            AuthorizationToken = deserializedObject.AuthorizationToken;
            AuthServer = deserializedObject.AuthServer;
        }

        public bool NeedsRefresh
        {
            get
            {
                return (null != this.RefreshToken &&
                    this.RefreshToken.Length > 0 &&
                    null != this.RefreshTokenServerUri &&
                    this.RefreshTokenServerUri.Length > 0 &&
                    null != this.ExpirationDateTimeUTC &&
                    DateTime.UtcNow >= this.ExpirationDateTimeUTC);
            }
        }

        public override string ToString()
        {
            var serializer = new JavaScriptSerializer();

            return serializer.Serialize(this);
        }
    }
}
