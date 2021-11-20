using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InContact.DeveloperPortal.Web.Authentication
{
    public class CXoneConfiguration
    {
        public string Cluster { get; set; }

        public string UserHubArea { get; set; }

        public string Domain { get; set; }

        public string UserId { get; set; }

        public int AgentNo { get; set; }

        public int BusNo { get; set; }

        public bool IsUserHub
        {
            get
            {
                return !String.IsNullOrEmpty(UserId);
            }
        }

        /// <summary>
        /// UserHub / API Facade Base Uri
        /// </summary>
        public string ApiResourceBaseUri
        {
            get
            {
                return String.Format("https://{0}.{1}/", UserHubArea, Domain);
            }
        }

        /// <summary>
        /// inContact API Base Uri, ACD RESTful API
        /// </summary>
        public string AcdApiResourceBaseUri { get; set; }

        /// <summary>
        /// Digital Engagement API
        /// </summary>
        public string DigitalApiResourceBaseUri
        {
            get
            {
                return String.Format("https://api-de-{0}.{1}/", UserHubArea, Domain.Replace("-", ""));
            }
        }

        /// <summary>
        /// inContact Authorization Server, this is used to invoke whoami
        /// </summary>
        public string AcdAuthorizationTokenEndpoint
        {
            get
            {
                return String.Format("https://api-{0}.{1}/incontactauthorizationserver/token", Cluster, Domain);
            }
        }
    }
}