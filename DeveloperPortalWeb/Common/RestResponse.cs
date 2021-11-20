using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace InContact.DeveloperPortal.Web.Common
{
    public class RestResponse
    {
        public HttpStatusCode Status { get; set; }

        public string StatusDescription { get; set; }

        public string Content { get; set; }

        public bool IsSuccess
        {
            get
            {
                return Status < HttpStatusCode.BadRequest;
            }
        }

        public override string ToString()
        {
            return String.Format("{{  status: {0}, description: {1}, content: {2} }}", (int)Status, StatusDescription, Content);
        }
    }
}