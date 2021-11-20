using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using InContact.DeveloperPortal.Web.Models;
using InContact.DeveloperPortal.Web.Common;
using System.Net;

namespace InContact.DeveloperPortal.Web.Data
{
    public class InContactCentralApiData : IInContactCentralApiData
    {
        private readonly string _inContactApiEndpoint;
        private readonly string _defaultApplicationName;
        private readonly string _defaultVendorName;
        private readonly string _defaultBusinessUnitNumber;

        public InContactCentralApiData(string inContactApiEndpoint, string defaultApplicationName, string defaultVendorName, string defaultBusinessUnitNumber)
        {
            _inContactApiEndpoint = inContactApiEndpoint;
            _defaultApplicationName = defaultApplicationName;
            _defaultVendorName = defaultVendorName;
            _defaultBusinessUnitNumber = defaultBusinessUnitNumber;
        }

        public InContactCentralAuthenticationResult RefreshToken(string authToken, string refreshToken, string refreshTokenServerUri)
        {
            InContactCentralAuthenticationResult result = null;

            string postData = string.Format("{{\"grant_type\":\"refresh_token\",\"refresh_token\":\"{0}\"}}", refreshToken);
            Logging.LogMessage(string.Format("RefreshToken: authToken: {0}, url: {1}, request: {2}", authToken, refreshTokenServerUri, postData));

            using (HttpClient client = new HttpClient(new HttpClientHandler()) { BaseAddress = new Uri(refreshTokenServerUri) })
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "basic " + authToken);

                StringContent content = new StringContent(postData);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                HttpResponseMessage response = client.PostAsync(refreshTokenServerUri, content).Result;
                string responseString = response.Content.ReadAsStringAsync().Result;
                Logging.LogMessage(string.Format("RefreshToken: authToken: {0}, response: {1}", authToken, responseString));

                result = new JavaScriptSerializer().Deserialize<InContactCentralAuthenticationResult>(responseString);
            }

            return result;
        }

        public InContactCentralAuthenticationResult Authenticate(SigninViewModel loginData)
        {
            InContactCentralAuthenticationResult result = null;

            result = Authenticate(_inContactApiEndpoint, loginData);
          
            return result;
        }

        public InContactCentralAuthenticationResult Authenticate(string endPoint, SigninViewModel loginData)
        {
            InContactCentralAuthenticationResult result = null;

            //Encoded request string
            string authToken;
            if (string.IsNullOrEmpty(loginData.ApplicationName) || string.IsNullOrEmpty(loginData.VendorName) || loginData.BusinessUnitNumber == null || loginData.BusinessUnitNumber == 0)
            {
                authToken = String.Format("{0}@{1}:{2}", _defaultApplicationName, _defaultVendorName, _defaultBusinessUnitNumber);
            }
            else
            {
                authToken = String.Format("{0}@{1}:{2}", loginData.ApplicationName, loginData.VendorName, loginData.BusinessUnitNumber);
            }

            string encodedAuthToken = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(authToken));

            string postData = String.Format("{{\"grant_type\":\"password\",\"username\":\"{0}\",\"password\":\"{1}\",\"scope\":\"\"}}",
                loginData.UserName, loginData.Password);

            Logging.LogMessage(string.Format("Authenticate: authToken: {0}, url: {1}, request: {2}", authToken, endPoint, postData.Replace(loginData.Password, "(removed)")));

            using (HttpClient client = new HttpClient(new HttpClientHandler()) { BaseAddress = new Uri(endPoint) })
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "basic " + encodedAuthToken);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                StringContent content = new StringContent(postData);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                HttpResponseMessage response = client.PostAsync(endPoint, content).Result;
                string responseString = response.Content.ReadAsStringAsync().Result;
                Logging.LogMessage(string.Format("Authenticate: authToken: {0}, response: {1}", authToken, responseString));

                result = new JavaScriptSerializer().Deserialize<InContactCentralAuthenticationResult>(responseString);
            }

            result.auth_token = encodedAuthToken;


            return result;
        }
    }
}