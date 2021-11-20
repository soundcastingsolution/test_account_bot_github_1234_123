using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Web;

namespace InContact.DeveloperPortal.Web.Common
{
    public class RestRequest
    {
        public const string Content_UrlEncoded = "application/x-www-form-urlencoded";
        public const string Content_Json = "application/json";

        static RestRequest()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        public static RestResponse HttpGet(string reqUri)
        {
            HttpWebResponse webResponse;
            try
            {
                HttpWebRequest webRequest = CreateRequest(reqUri, "GET");
                webResponse = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException wex)
            {
                webResponse = (HttpWebResponse)wex.Response;
            }
            return ReadResponse(webResponse);
        }

        public static RestResponse HttpGet(string reqUri, string authType, string authValue)
        {
            HttpWebResponse webResponse;
            try
            {
                HttpWebRequest webRequest = CreateRequest(reqUri, "GET", authType, authValue);
                webResponse = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException wex)
            {
                webResponse = (HttpWebResponse)wex.Response;
            }
            return ReadResponse(webResponse);
        }

        public static RestResponse HttpPost(string authType, string authValue, string reqUri, JObject reqContent, string contentType = Content_Json, string acceptType = Content_Json)
        {
            HttpWebResponse webResponse;
            try
            {
                HttpWebRequest webRequest = CreateRequest(reqUri, "POST", authType, authValue);
                webRequest.ContentType = contentType;
                //Get content bytes
                byte[] bytes = GetContentBytes(contentType, reqContent);
                webRequest.ContentLength = bytes.Length;
                //Write bytes to request
                using (System.IO.Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }
                webResponse = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException wex)
            {
                webResponse = (HttpWebResponse)wex.Response;
            }
            return ReadResponse(webResponse);
        }

        private static HttpWebRequest CreateRequest(string reqUri, string method, string authType = null, string authValue = null, string acceptType = Content_Json)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(reqUri);
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            request.Method = method;
            request.Accept = acceptType;
            if (!String.IsNullOrWhiteSpace(authType) && !String.IsNullOrWhiteSpace(authValue))
                request.Headers["Authorization"] = authType + " " + authValue;
            return request;
        }

        private static RestResponse ReadResponse(HttpWebResponse webResp)
        {
            RestResponse restResp = new RestResponse();
            restResp.Status = webResp.StatusCode;
            restResp.StatusDescription = webResp.StatusDescription;
            using (StreamReader sr = new StreamReader(webResp.GetResponseStream()))
            {
                restResp.Content = sr.ReadToEnd();
            }
            return restResp;
        }

        private static byte[] GetContentBytes(string contentType, JObject jsonContent)
        {
            if (jsonContent == null)
                return null;
            string content = null;
            switch (contentType)
            {
                case Content_UrlEncoded:
                    IEnumerable<JProperty> props = jsonContent.Properties();
                    content = String.Join("&", props.Select(p => String.Format("{0}={1}", p.Name, p.Value.ToString())));
                    break;
                case Content_Json:
                    content = jsonContent.ToString();
                    break;
            }
            return Encoding.UTF8.GetBytes(content);
        }
    }
}