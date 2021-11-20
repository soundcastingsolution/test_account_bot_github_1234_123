using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using NUnit.Framework;

namespace DeveloperPortal.Tests
{
    public class AuthenticationTests
    {
        /// <summary>
        /// Retrieves an inContact Central token
        /// </summary>
        [Test]
        [Explicit("Integration")]
        public void Authentication_GetToken_ReturnsToken()
        {
            //Authorization endpoint
            string endpoint = @"https://home-tc4.ucnlabext.com/InContactAuthorizationServer/Token";
            string appName = "TestApp";
            string vendorName = "inCloud";
            string businessUnit = "121";
            string userName = "itdevelopers@incontact.com";
            string password = "Tester123!!";
            string scope = string.Empty;
            inContactCentralToken actual = null;

            //Encoded request string
            string authToken = String.Format("{0}@{1}:{2}", appName, vendorName, businessUnit);
            string encodedAuthToken = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(authToken));

            string postData = String.Format("{{\"grant_type\":\"password\",\"username\":\"{0}\",\"password\":\"{1}\",\"scope\":\"{2}\"}}",
                userName, password, scope);

            using (HttpClient client = new HttpClient(new HttpClientHandler()) { BaseAddress = new Uri(endpoint) })
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "basic " + encodedAuthToken);

                StringContent content = new StringContent(postData);
                content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");

                HttpResponseMessage response = client.PostAsync(endpoint, content).Result;
                string responseString = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    actual = new JavaScriptSerializer().Deserialize<inContactCentralToken>(responseString);
                }
            }

            Assert.IsTrue(actual.access_token.Length > 0, "Expected token returned");
            Assert.IsTrue(actual.token_type.Length > 0, "Expected token_type returned");
            Assert.IsTrue(actual.expires_in > 0, "Expected expires_in value returned");
            Assert.IsTrue(actual.refresh_token.Length > 0, "Expected refresh_token returned");
            Assert.IsTrue(actual.scope.Length > 0, "Expected scope returned");
            Assert.IsTrue(actual.resource_server_base_uri.Length > 0, "Expected resource_server_base_uri returned");
            Assert.IsTrue(actual.refresh_token_server_uri.Length > 0, "Expected refresh_token_server_uri returned");
            Assert.IsTrue(actual.agent_id > 0, "Expected agent_id value returned");
            Assert.IsTrue(actual.team_id > 0, "Expected team_id value returned");
            Assert.AreEqual(businessUnit, actual.bus_no.ToString(), "business unit not as expected");
        }

        /// <summary>
        /// Captures an authentication error returned from inContact Central
        /// </summary>
        [Test]
        [Explicit("Integration")]
        public void Authentication_GetToken_ReturnsFailureMessage()
        {
            //Authorization endpoint
            string endpoint = @"https://home-tc4.ucnlabext.com/InContactAuthorizationServer/Token";
            string appName = "TestApp";
            string vendorName = "inCloud";
            string businessUnit = "121";
            string userName = "itdevelopers@incontact.com";
            string password = "BAD PASSWORD";
            string scope = string.Empty;
            inContactCentralToken actual = null;

            //Encoded request string
            string authToken = String.Format("{0}@{1}:{2}", appName, vendorName, businessUnit);
            string encodedAuthToken = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(authToken));

            string postData = String.Format("{{\"grant_type\":\"password\",\"username\":\"{0}\",\"password\":\"{1}\",\"scope\":\"{2}\"}}",
                userName, password, scope);

            using (HttpClient client = new HttpClient(new HttpClientHandler()) { BaseAddress = new Uri(endpoint) })
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "basic " + encodedAuthToken);

                StringContent content = new StringContent(postData);
                content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");

                HttpResponseMessage response = client.PostAsync(endpoint, content).Result;
                string responseString = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    actual = new JavaScriptSerializer().Deserialize<inContactCentralToken>(responseString);
                }
            }

            Assert.IsNotNull(actual, "expected a response");
            Assert.IsTrue(actual.error.Length > 0, "Expected error returned");
            Assert.IsTrue(actual.error_description.Length > 0, "Expected error description returned");

            //Run the "success" test to avoid locking out the account
            Authentication_GetToken_ReturnsToken();
        }

        /// <summary>
        /// Checks to see that logging into the devleoper portal creates the forum user if it doesn't exist. 
        /// </summary>
        [Test]
        public void AccountLogic_Authenticate_CreatesForumUser()
        {
            //Arrange



            //Act


            //Assert
        }

        /// <summary>
        /// Checks to see that logging into the devleoper portal doesn't create the forum user if it already exists. 
        /// </summary>
        [Test]
        public void AccountLogic_Authenticate_DoesNotCreateForumUser()
        {
            //Arrange
            //Act
            //Assert
        }

        private class inContactCentralToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
            public string scope { get; set; }
            public string resource_server_base_uri { get; set; }
            public string refresh_token_server_uri { get; set; }
            public int agent_id { get; set; }
            public int team_id { get; set; }
            public int bus_no { get; set; }
            public int status_code { get; set; }
            public string error { get; set; }
            public string error_description { get; set; }
        }
    }
}
