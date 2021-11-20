using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using APIDocUnitTest.Models;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;

namespace APIDocUnitTest.Models

{
    public class Operation
    {
        APIproperties apiob = new APIproperties();
        List<string> APIURL = new List<string>();
        public string accessToken = string.Empty;
        public string baseURL = string.Empty;
        public string version = string.Empty;
        public string tokenURL = string.Empty;
        public string CXone_baseURI = string.Empty;

        public Operation()
        {
            version = ConfigurationManager.AppSettings["APIVersion"].ToString();
            apiob.UserName = ConfigurationManager.AppSettings["Username"].ToString();
            apiob.Password = ConfigurationManager.AppSettings["Password"].ToString();
            tokenURL = ConfigurationManager.AppSettings["APITokenBaseUrl"].ToString();
            CXone_baseURI = ConfigurationManager.AppSettings["APICXoneUrl"].ToString();

            // Token Generation

            accessToken = Authentication(apiob.UserName.Trim(), apiob.Password.Trim());
            baseURL = ConfigurationManager.AppSettings["ServiceUrl"].ToString();

        }

        //V13.0
        public string getAgents(string fields, string updatedSince, bool isActive, string searchString,
            int skip, int top, string orderBy)
        {

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string queryString = "?fields=" + fields + "&updatedSince=" + updatedSince + "&isActive=" + isActive +
                       "&searchString=" + searchString + "&fields=" + fields + "&skip=" + skip + "&top=" + top + "&orderby=" + orderBy;
                string apiURL = "/services/v13.0/agents" + queryString;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;

                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error  
                    Console.WriteLine(ex.Message);
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "Failure";
            }

            return "";
        }

        public string PostAgentsV13(string firstName, string middleName, string lastName, string teamId, string teamUuid, string reportToId,
            string internalId, string profileId, string roleId, string password, bool forceChangeOnLogon, string emailAddress,
            string userName, string userId, string timeZone, string country, string state, string city, string chatRefusalTimeout,
            string phoneRefusalTimeout, string workItemRefusalTimeout, string defaultDialingPattern, string maxConcurrentChats,
            bool useTeamMaxConcurrentChats, bool isActive, string locationId, string notes, string hireDate, string terminationDate,
            string hourlyCost, bool rehireStatus, string employmentType, string referral,
            bool atHome, string hiringSource, string ntLoginName, string custom1, string custom2, string custom3, string custom4,
            string custom5, string scheduleNotification, string federatedId, string maxEmailAutoParkingLimit, string useTeamEmailAutoParkingLimit,
            string sipUser, string systemUser, string systemDomain, string crmUserName, bool useAgentTimeZone, string timeDisplayFormat,
            bool sendEmailNotifications, string apiKey, string telephone1, string telephone2, string userType, bool isWhatIfAgent,
            bool requestContact, bool useTeamRequestContact, string chatThreshold, bool useTeamChatThreshold, string emailThreshold,
            bool useTeamEmailThreshold, string workItemThreshold, bool useTeamWorkItemThreshold, bool contactAutoFocus,
            bool useTeamContactAutoFocus, string subject, string issuer, bool isOpenIdProfileComplete, string recordingNumbers)
        {
            //string PostAgentJSON = @"{""agents"": [{""firstName"": ""FirstHc8"",""middleName"": ""midHc8"",""lastName"": ""LastHC8""}]}";
            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/agents";
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;

                string postData = "{\"agents\":[{\"firstName\":\"" + firstName +
                   "\",\"middleName\":\"" + middleName +
                   "\",\"lastName\":\"" + lastName +
                   "\",\"teamId\":\"" + teamId +
                   "\",\"teamUuid\":\"" + teamUuid +
                   "\",\"reportToId\":\"" + reportToId +
                   "\",\"internalId\":\"" + internalId +
                   "\",\"profileId\":\"" + profileId +
                  "\",\"roleId\":" + roleId +
                   ",\"password\":\"" + password +
                   "\",\"forceChangeOnLogon\":\"" + forceChangeOnLogon +
                   "\",\"emailAddress\":\"" + emailAddress +
                   "\",\"userName\":\"" + userName +
                   "\",\"userId\":\"" + userId +
                   "\",\"timeZone\":\"" + timeZone +
                   "\",\"country\":\"" + country +
                   "\",\"state\":\"" + state +
                  "\",\"city\":" + city +
                  "\",\"chatRefusalTimeout\":\"" + chatRefusalTimeout +
                   "\",\"phoneRefusalTimeout\":\"" + phoneRefusalTimeout +
                   "\",\"workItemRefusalTimeout\":\"" + workItemRefusalTimeout +
                   "\",\"defaultDialingPattern\":\"" + defaultDialingPattern +
                   "\",\"maxConcurrentChats\":\"" + maxConcurrentChats +
                   "\",\"useTeamMaxConcurrentChats\":\"" + useTeamMaxConcurrentChats +
                   "\",\"isActive\":\"" + isActive +
                  "\",\"locationId\":" + locationId +
                   ",\"notes\":\"" + notes +
                   "\",\"hireDate\":\"" + hireDate +
                   "\",\"hourlyCost\":\"" + hourlyCost +
                   "\",\"rehireStatus\":\"" + rehireStatus +
                   "\",\"employmentType\":\"" + employmentType +
                   "\",\"referral\":\"" + referral +
                   "\",\"atHome\":\"" + atHome +
                   "\",\"hiringSource\":\"" + hiringSource +
                  "\",\"ntLoginName\":" + ntLoginName +
                  "\",\"custom1\":\"" + custom1 +
                   "\",\"custom2\":\"" + custom2 +
                   "\",\"custom3\":\"" + custom3 +
                   "\",\"custom4\":\"" + custom4 +
                   "\",\"custom5\":\"" + custom5 +
                   "\",\"scheduleNotification\":\"" + scheduleNotification +
                   "\",\"federatedId\":\"" + federatedId +
                  "\",\"maxEmailAutoParkingLimit\":" + maxEmailAutoParkingLimit +
                   ",\"useTeamEmailAutoParkingLimit\":\"" + useTeamEmailAutoParkingLimit +
                   "\",\"sipUser\":\"" + sipUser +
                   "\",\"systemUser\":\"" + systemUser +
                   "\",\"systemDomain\":\"" + systemDomain +
                   "\",\"crmUserName\":\"" + crmUserName +
                   "\",\"useAgentTimeZone\":\"" + useAgentTimeZone +
                   "\",\"timeDisplayFormat\":\"" + timeDisplayFormat +
                   "\",\"sendEmailNotifications\":\"" + sendEmailNotifications +
                  "\",\"apiKey\":" + apiKey +
                  "\",\"telephone1\":\"" + telephone1 +
                   "\",\"telephone2\":\"" + telephone2 +
                   "\",\"userType\":\"" + userType +
                   "\",\"isWhatIfAgent\":\"" + isWhatIfAgent +
                   "\",\"requestContact\":\"" + requestContact +
                   "\",\"useTeamRequestContact\":\"" + useTeamRequestContact +
                   "\",\"chatThreshold\":\"" + chatThreshold +
                  "\",\"useTeamChatThreshold\":" + useTeamChatThreshold +
                   ",\"emailThreshold\":\"" + emailThreshold +
                   "\",\"useTeamEmailThreshold\":\"" + useTeamEmailThreshold +
                   "\",\"workItemThreshold\":\"" + workItemThreshold +
                   "\",\"useTeamWorkItemThreshold\":\"" + useTeamWorkItemThreshold +
                   "\",\"contactAutoFocus\":\"" + contactAutoFocus +
                   "\",\"useTeamContactAutoFocus\":\"" + useTeamContactAutoFocus +
                   "\",\"subject\":\"" + subject +
                   "\",\"issuer\":\"" + issuer +
                  "\",\"isOpenIdProfileComplete\":\"" + isOpenIdProfileComplete +
                  "\",\"recordingNumbers\": [{\"number\":\"" + recordingNumbers + "\"}]} }]}";

                //string postData = PostAgentJSON;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "Failure";
            }
            return "";
        }
        public string GetAgentID(int agentID, string fields)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/agents/" + agentID + "?fields=" + fields;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "Failure";
            }
            return "";
        }
        public string UpdateAgent(int agentId)
        {

            string agent = @"{""agents"": [{""firstName"": ""FirstHC8"",""middleName"": ""MidHC8"",""lastName"": ""LastHC8"",""teamId"": """",""reportToId"": ""1"",""internalId"": """",""profileId"": ""0"",""roleId"": """",""password"": """",""forceChangeOnLogon"": ""true"",""emailAddress"": """",""userName"": """",""userId"": """",""timeZone"": ""(GMT-07:00) Arizona"",""country"": """",""state"": """",""city"": """",""chatRefusalTimeout"": ""15"",""phoneRefusalTimeout"": ""15"",""workItemRefusalTimeout"": ""15"",""defaultDialingPattern"": ""0"",""maxConcurrentChats"": ""1"",""useTeamMaxConcurrentChats"": ""false"",""isActive"": ""true"",""locationId"": ""0"",""notes"": """",""hireDate"": ""10/10/1800"",""terminationDate"": ""10/10/1800"",""hourlyCost"": ""0"",""rehireStatus"": ""true"",""employmentType"": ""1"",""referral"": """",""atHome"": ""false"",""hiringSource"": """",""ntLoginName"": """",""custom1"": """",""custom2"": """",""custom3"": """",""custom4"": """",""custom5"": """",""federatedId"": """",""maxEmailAutoParkingLimit"": ""1"",""useTeamEmailAutoParkingLimit"": ""false"",""sipUser"": """",""systemUser"": """",""systemDomain"": """",""crmUserName"": """",""useAgentTimeZone"": ""false"",""timeDisplayFormat"": ""0"",""hiringSource"": """",""sendEmailNotifications"": ""false"",""apiKey"": """",""telephone1"": """",""telephone2"": """",""userType"": """",""isWhatIfAgent"": """"}]}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/agent/" + agentId.ToString();

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = agent; //agent JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "Failure";
            }

            return "";
        }
        public string GetAgentTeams(string fields, string updatedSince, bool isActive, string searchString,
            int skip, int top, string orderBy)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string queryString = "?fields=" + fields + "&updatedSince=" + updatedSince + "&isActive=" + isActive +
                       "&searchString=" + searchString + "&fields=" + fields + "&skip=" + skip + "&top=" + top + "&orderBy=" + orderBy;
                string apiURL = "/services/v13.0/teams" + queryString;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    //.NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string CreateTeam()
        {
            string team = @"{""teams"": [{""teamName"": ""newTeam"",""isActive"": ""true""}]}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/teams";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "Post";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = team; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string GetAgentTeamByID(int teamID, string fields)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/teams/" + teamID + "?fields=" + fields;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    //.NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string UpdateTeam(int teamId)
        {
            string team = @"{""teams"": [{""teamName"": ""myTeam"",""isActive"": ""true""}]}";
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/teams/" + teamId.ToString();

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "Put";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = team; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error      
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string GetUnavailableCodesTeamID(int teamID, string activeOnly)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                //You must supply a teamID
                //string teamID = "";
                string apiURL = "/services/v13.0/teams/" + teamID + "/unavailable-codes";
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    //.NET will throw a System.Net.webException on StatusCodes other than 200.  You can catch that exception and retain
                    //the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string GetUnavailableCodes(string activeOnly)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/unavailable-codes?activeOnly=" + activeOnly;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    string JSONresponseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    //.NET will throw a System.Net.webException on StatusCodes other than 200.  You can catch that exception and retain
                    //the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string GetSkills()
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/skills";
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error  
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string postSkill()
        {
            string SkillJSONData = @"{""skills"": [{""mediaTypeId"": ""4"",""skillName"": """"}]}";
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/skills";
                string endpoint = baseURL + apiURL;
                //Build POST data as a JSON string                   
                string postData = SkillJSONData; //Create the JSON data for skill object
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string GetSkillID(int skillID, string fields)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/skills/" + skillID + "?fields=" + fields;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error       
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";

        }
        public string updateSkill(int skillId)
        {
            string SkillJSONData = @"{""skills"": [{""mediaTypeId"": ""4"",""skillName"": """"}]}";
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/skills/" + skillId;
                string endpoint = baseURL + apiURL;
                //Build POST data as a JSON string                   
                string postData = SkillJSONData;//Create the JSON data for skill object
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string getSkillsGeneralSettingBySkillIdV13(int skillId, string fields = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/skills/" + skillId + "/parameters/general-settings";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
            }

            return "";
        }
        public string updateSkillsGeneralSettingBySkillIdV13(int skillId)
        {
            string generalSettingJSONData = @"{""generalSettings"": [{""minimumRetryMinutes"": ""12"",""maximumAttempts"": ""10""}]}";
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/skills/" + skillId + "/parameters/general-settings";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                   
                string postData = generalSettingJSONData;//Create the JSON data for General Setting object

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string GetCPAManagement(int skillId, string fields = "")
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/skills/" + skillId + "/parameters/cpa-management";
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string Putskillcpamanagement(int skillId)
        {
            string cpaSettingsJSONData = @"{""cpaSettings"": [{""abandonMessagePath"": """",""abandonMsgMode"": ""0"",""abandonTimeout"": ""0""}]}";
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/skills/" + skillId + "/parameters/cpa-management";
                string endpoint = baseURL + apiURL;
                //Build POST data as a JSON string
                string postData = cpaSettingsJSONData;//Create the JSON data for cpa management object
                                                      //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string Getxssettings(int skillId, string fields = "")
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/skills/" + skillId + "/parameters/xs-settings";
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string Putxssettings(int skillId)
        {
            string xsSettingsJSONData = @"{""xsSettings"": [{""xsScriptID"": ""0"",""xsCheckinScriptID"": ""0"",""externalOutboundSkill_No"": """",""xsSkillChangedActive"": ""true"",""xsGetContactsActive"": ""true"",""xsFreshThreshold"": ""0"",""xsAvailableThreshold"": ""0"",""xsReadyThreshold"": ""0"",""xsNumberToRetrieve"": ""0""}]}";
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/skills/" + skillId + "/parameters/XS-settings";
                string endpoint = baseURL + apiURL;
                //Build POST data as a JSON string
                string postData = xsSettingsJSONData;//Create the JSON data for xs settings object
                                                     //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string Getdeliverypreference(int skillId, string fields = "")
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/skills/" + skillId + "/parameters/delivery-preference" + "?fields=" + fields;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string Putskilldeliverypreference(String skillId)
        {
            string deliveryPreferencesJSONData = @"{""deliveryPreferences"": [{""confirmationRequiredDisabled"": ""true"",""confirmationRequiredDeliveryType"": ""0"",""confirmationRequiredTimeout"": ""0"",""confirmationRequiredTimeoutSubsequent"": ""0""}]}";
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/skills/" + skillId + "/parameters/delivery-preference";
                string endpoint = baseURL + apiURL;
                //Build POST data as a JSON string
                string postData = deliveryPreferencesJSONData;//Create the JSON data for delivery Preferences object
                                                              //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string Getskillretrysetting(int skillId, string fields = "")
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/skills/" + skillId + "/parameters/retry-settings" + "?fields=" + fields;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string Putskillretrysetting(int skillId)
        {
            string retrySettingsJSONData = @"{""retrySettings"": [{""loadNonFresh"": ""true"",""finalizeWhenExhausted"": ""0"",""maximumAttempts"": ""0"",""minimumRetryMinutes"": ""0"",""maximumNumberOfHandledCalls"": ""0"",""restrictedCallingMinutes"": ""0"",""restrictedCallingMaxAttempts"": ""0"",""generalStaleMinutes"": ""0"",,""callbackRestMinutes"": ""0"",""releaseAgentSpecificCalls"": ""true"",""maximumNumberOfCallbacks"": ""0"",""callbackStaleMinutes"": ""0""}]}";
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/skills/" + skillId + "/parameters/retry-settings";
                string endpoint = baseURL + apiURL;
                //Build POST data as a JSON string
                string postData = retrySettingsJSONData;//Create the JSON data for retry Settings object
                                                        //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string Getschedulesettings(int skillId)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/skills/" + skillId + "/parameters/schedule-settings";
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string Putschedulesettings(int skillId)
        {
            string scheduleSettingsJSONData = @"{""scheduleSettings"": [{""isScheduled"": ""true"",""sundayStartTime"": """",""sundayEndTime"": """",""sundayIsActive"": ""true"",""mondayStartTime"": """",""mondayEndTime"": """",""mondayIsActive"": ""true"",""tuesdayStartTime"": """",,""tuesdayEndTime"": """",""tuesdayIsActive"": ""true"",""wednesdayStartTime"": """",""wednesdayEndTime"": """",""wednesdayIsActive"": ""true"",""thursdayStartTime"": """",,""thursdayEndTime"": """",""thursdayIsActive"": ""true"",""fridayStartTime"": """",""fridayEndTime"": """",""fridayIsActive"": ""true"",""saturdayStartTime"": """",,""saturdayEndTime"": """",""saturdayIsActive"": ""true""}]}";
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v13.0/skills/" + skillId + "/parameters/schedule-settings";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                   
                string postData = scheduleSettingsJSONData;//Create the JSON data for schedule Settings object

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        //
        //
        //

        public string postContactChatSendEmail(string fromAddress, string toAddress = "", string emailBody = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/contacts/chats/send-email";
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;

                string postData = "{\"toAddress\":\"" + toAddress +
                    "\",\"fromAddress\":\"" + fromAddress +
                    "\",\"emailBody\":\"" + emailBody +
                    "\"}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription); // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                                                                                                    // You can catch that exception and retain the StatusCode for error handling.

                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getThankYouForSkillId(int SkillId)
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/skills/" + SkillId + "/thank-you-page";
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                                                 // You can catch that exception and retain the StatusCode for error handling.

                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;

                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    

            }
        }
        public string getTeamIDPerformance(int teamId, string startDate = "", string endDate = "", string fields = "inboundHandled,inboundTalkTime,inboundAvgTalkTime,outboundHandled,outboundTalkTime,outboundAvgTalkTime,totalHandled,totalAvgHandled,totalTalkTime,totalAvgTalkTime,consultTime,availableTime,unavailableTime,avgUnavailableTime,acwTime,refused,percentRefused,loginTime,workingRate,occupancy")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/teams/" + teamId + "/performance-total" + "?startDate=" + startDate + "&endDate=" + endDate + "&fields=" + fields;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;

                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    

            }
        }
        public string getwfmdataSkillsAgentPerformance(string fields = "", string StartDate = "", string EndDate = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/wfm-data/skills/agent-performance" + "?fields=" + fields + "&StartDate=" + StartDate + "&EndDate=" + EndDate;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;

                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                                                 // You can catch that exception and retain the StatusCode for error handling.

                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;

                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }
        public string getwfmdataAgentsScorecards(string fields = "", string StartDate = "", string EndDate = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/wfm-data/agents/scorecards" + "?fields=" + fields + "&StartDate=" + StartDate + "&EndDate=" + EndDate;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.

                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error     

            }
        }
        public string getwfmdataAgentsScheduleAdherence(string fields = "", string StartDate = "", string EndDate = "")
        {


            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/wfm-data/agents/schedule-adherence" + "?fields=" + fields + "&StartDate=" + StartDate + "&EndDate=" + EndDate;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message;
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error    
                return "Failure";
            }
        }
        public string getwfmdataSkillsDialerContacts(string fields = "", string StartDate = "", string EndDate = "")
        {


            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/wfm-data/skills/dialer-contacts" + "?fields=" + fields + "&StartDate=" + StartDate + "&EndDate=" + EndDate;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getwfmdataAgents(string fields = "", string StartDate = "", string EndDate = "")
        {


            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/wfm-data/agents" + "?fields=" + fields + "&StartDate=" + StartDate + "&EndDate=" + EndDate;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getwfmdataSkillsContacts(string fields = "", string StartDate = "", string EndDate = "", string mediaTypeId = "")
        {


            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/wfm-data/skills/contacts" + "?fields=" + fields + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&mediaTypeId=" + mediaTypeId;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getwfodataqm(string fields = "", string StartDate = "", string EndDate = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/wfo-data/qm" + "?fields=" + fields + "&StartDate=" + StartDate + "&EndDate=" + EndDate;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getwfodataosi(string fields = "", string StartDate = "", string EndDate = "")
        {
            //string baseURL = "https://hc8.ucnlabext.com/InContactAPI";

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/wfo-data/osi" + "?fields=" + fields + "&StartDate=" + StartDate + "&EndDate=" + EndDate;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getwfodataftci(string fields = "", string StartDate = "", string EndDate = "")
        {


            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/wfo-data/ftci" + "?fields=" + fields + "&StartDate=" + StartDate + "&EndDate=" + EndDate;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getwfodatacsi(string fields = "", string StartDate = "", string EndDate = "")
        {


            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/wfo-data/csi" + "?fields=" + fields + "&StartDate=" + StartDate + "&EndDate=" + EndDate;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getwfodataasi(string fields = "", string StartDate = "", string EndDate = "")
        {


            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/wfo-data/asi" + "?fields=" + fields + "&StartDate=" + StartDate + "&EndDate=" + EndDate;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getwfodataascm(string fields = "", string StartDate = "", string EndDate = "")
        {


            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/wfo-data/ascm" + "?fields=" + fields + "&StartDate=" + StartDate + "&EndDate=" + EndDate;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string postReportjobsDatadownlaodByReportId(string reportId, string saveAsFile = "", string fileName = "", string startDate = "", string endDate = "", string includeHeaders = "false")
        {


            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/report-jobs/datadownload/" + reportId;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;

                string postData = "{\"saveAsFile\":\"" + saveAsFile +
                    "\",\"fileName\":\"" + fileName +
                    "\",\"startDate\":\"" + startDate +
                    "\",\"endDate\":\"" + endDate +
                    "\",\"includeHeaders\":" + includeHeaders +
                    "}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getReportJobsByJobId(string jobId, string fields = "")
        {


            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/report-jobs/" + "?fields=" + fields;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getContactsContactIdCallquality(string contactId)
        {


            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/contacts/" + contactId + "/call-quality";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getContactsContactIdTranscripts(string contactId, string transportCode = "null", string startDate = "09/01/2016", string endDate = "09/30/2016", string skip = "null", string top = "null")
        {


            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/contacts/" + contactId + "/sms-transcripts" +
                     "?transportCode=" + transportCode + "&startDate=" + startDate + "&endDate=" + endDate + "&skip=" + skip + "&top=" + top;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getContactsSmsTranscripts(string transportCode = "null", string startDate = "09/01/2016", string endDate = "09/30/2016", string skip = "null", string top = "null", string orderBy = "", string agentId = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/contacts/sms-transcripts" +
                     "?transportCode=" + transportCode + "&startDate=" + startDate + "&endDate=" + endDate + "&skip=" + skip + "&top=" + top + "&orderBy=" + orderBy + "&agentId=" + agentId;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getAgentsAgentIdLoginhistory(double agentId, string startDate = "", string endDate = "", string searchString = "", string fields = "", int skip = 1, int top = 100, string orderBy = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agents/" + agentId + "/login-history" +
                     "?startDate=" + startDate + "&endDate=" + endDate + "&searchString=" + searchString + "&fields=" + fields + "&skip=" + skip + "&top=" + top + "&orderBy=" + orderBy;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getChatprofileByPOCId(string pointOfContactId)
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/points-of-contact/" + pointOfContactId + "/chat-profile";
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string postContactsChatSessionTypingPreview(string chatSession, string previewText = "This text is 100% awesome!!", string label = "testLabel")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/contacts/chats/" + chatSession + "/typing-preview";

                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;

                string postData = "{\"previewText\":\"" + previewText +
                                   "\",\"label\":\"" + label +
                                    "\"}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string postContactsChatSessionTyping(string chatSession, string isTyping = "", string isTextEntered = "", string label = "testLabel")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/contacts/chats/" + chatSession + "/typing";
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;

                // string postData = "{\"isImmediate\":"+ isImmediate+"}";
                string postData = "{\"isTyping\":" + isTyping +
                                   ",\"isTextEntered\":" + isTextEntered +
                                   ",\"label\":" + label + "}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string posttAgentSessionIdInteractionConatactIdActivate(string sessionId, string ConatctId)
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agent-sessions/" + sessionId + "/interactions/" + ConatctId + "/activate";
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;

                // string postData = "{\"isImmediate\":"+ isImmediate+"}";
                string postData = "{}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string posttAgentSessionIdAddcontact(string sessionId, string chat = "true", string email = "true", string workitem = "true")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agent-sessions/" + sessionId + "/add-contact";
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;

                // string postData = "{}";
                string postData = "{\"chat\":" + chat +
                                  ",\"email\":" + email +
                                  ",\"workitem\":" + workitem + "}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string agentCallbacksPostCancel(string sessionId, string callbackId, string notes = " ")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agent-sessions/" + sessionId + "/interactions/+callbackId" + "/cancel";
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;

                // string postData = "{}";
                string postData = "{\"notes\":\"" + notes + "\"}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string posttAgentSessionIdInteractionConatactIdSnooze(string sessionId, string ConatctId)
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agent-sessions/" + sessionId + "/interactions/" + ConatctId + "/snooze";
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;

                // string postData = "{\"isImmediate\":"+ isImmediate+"}";
                string postData = "{}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string posttAgentSessionIdInteractionConatactIdEmailRestore(string sessionId, string ConatctId)
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agent-sessions/" + sessionId + "/interactions/" + ConatctId + "/email-restore";
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;

                // string postData = "{\"isImmediate\":"+ isImmediate+"}";
                string postData = "{}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string posttAgentSessionIdInteractionConatactIdPreview(string sessionId, string ConatctId)
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agent-sessions/" + sessionId + "/interactions/" + ConatctId + "/email-preview";
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;

                // string postData = "{\"isImmediate\":"+ isImmediate+"}";
                string postData = "{}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string posttAgentSessionIdInteractionConatactIdUnparkemail(string sessionId, string ConatctId, string isImmediate = "true")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agent-sessions/" + sessionId + "/interactions/" + ConatctId + "/email-unpark";
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;

                // string postData = "{\"isImmediate\":"+ isImmediate+"}";
                string postData = "{}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string posttAgentSessionIdInteractionConatactIdParkemail(string sessionId, string ConatctId, string toAddress = "", string fromAddress = "", string ccAddress = "",
         string bccAddress = "", string subject = "", string bodyHtml = "", string attachments = "", string attachmentNames = "", string isDraft = "false", string originalAttachmentNames = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agent-sessions/" + sessionId + "/interactions/" + ConatctId + "/email-park";
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;

                string postData = "{\"toAddress\":\"" + toAddress +
                   "\",\"fromAddress\":\"" + fromAddress +
                   "\",\"ccAddress\":\"" + ccAddress +
                   "\",\"bccAddress\":\"" + bccAddress +
                   "\",\"subject\":\"" + subject +
                   "\",\"bodyHtml\":\"" + bodyHtml +
                   "\",\"attachments\":\"" + attachments +
                   "\",\"attachmentNames\":\"" + attachmentNames +
                  "\",\"isDraft\":" + isDraft +
                   ",\"originalAttachmentNames\":\"" + originalAttachmentNames + "\"}";


                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string posttAgentSessionSessionIdInteractionAddEmail(string sessionId)
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agent-sessions/" + sessionId + "/interactions/add-email";
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string postData = "{ }";


                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string postSessionIdInteractionsContactIdtyping(string sessionId, string contactId, bool isTyping = true, bool isTextEntered = true)
        {
            //accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjEwNDMsIm5hbWUiOiJyYWZpcS5qQGhjOC5jb20iLCJpc3MiOiJodHRwczovL2FwaS5pbmNvbnRhY3QuY29tIiwic3ViIjoidXNlcjoyNzQ5OCIsImF1ZCI6ImludGVybmFsQGluQ29udGFjdCBJbmMuIiwiZXhwIjoxNTE3NTU2OTAyLCJpYXQiOjE1MTc1NTMzMDMsImljU2NvcGUiOiIxLDIsNCw1LDcsOCIsImljQ2x1c3RlcklkIjoiSEM4IiwiaWNBZ2VudElkIjoyNzQ5OCwiaWNTUElkIjo3ODMsIm5iZiI6MTUxNzU1MzMwMn0.jzmRgJKE6ax6oGxL7i1fZXfMK3O-UVmCna02LBVIGY0zP1_MVTugbknMCny0xN5PiQV7vJX6FI0aCzjr6pZLZloc5f5m86vKhLVKsWEbnk_qPAsjlSnxJJUQO3k36WwLg4nnjcG2numVbETRfOxSvXTQfZzFFgIdEbe_4OxoNaSjOWkL0evGLpzBbPVi4tj2XliAhhkCSbA8iJQft-z1VFNYXeZ0VfkQ_NBi6C4XbkRIegcRIUh-1GtvF9_MgUvjv4r9fUkig6eTeZ0YbclAnKvQIbHhk3Jf5koLG7a_-FoRf4y3gy5pgizF6x_Z2qPOe0_e2WqtmWOsY3PTxsqJ1A";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/agent-sessions/" + sessionId + "/interactions/" + contactId + "/typing";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"isTyping\":" + isTyping.ToString().ToLower() +
                    ",\"isTextEntered\":" + isTextEntered.ToString().ToLower() + "}";


                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string deleteListsCallListsByListId(int listId, string forceInactive = "false", string forceDelete = "false")
        {

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/lists/call-lists/" + listId;
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"forceInactive\":\"" + forceInactive +
                    "\",\"forceDelete\":\"" + forceDelete + "\"}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "DELETE";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getListsCallListListIdAttempts(int listId, string updatedSince = "", string finalized = "", string fields = "", string skip = "", string top = "", string orderby = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/lists/call-lists/" + listId + "/attempts" +
                    "?updatedSince=" + updatedSince + "&finalized=" + finalized + "&fields=" + fields + "&skip=" + skip + "&top=" + top + "&orderby=" + orderby;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getListsCallListJobsByJobId(int jobId, string fields = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/lists/call-lists/jobs" + jobId + "?fields=" + fields;

                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string deleteListsCallListsJobsByJobId(int jobId)
        {

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/lists/call-lists/jobs/" + jobId;
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "DELETE";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getListsCallListJobs(string fields = "", int top = 10, int skip = 10, string orderBy = "", string startDate = "2016-12-1", string endDate = "2017-1-1")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/lists/call-lists/jobs" +
                    "?fields=" + fields + "&top=" + top + "&skip=" + skip + "&orderBy=" + orderBy + "&startDate=" + startDate + "&endDate=" + endDate;

                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string postCalllistListUpload(int listId, string skillId = "", string fileName = "", string forceOverwrite = "false", string expirationDate = "", string listFile = "")
        {
            //accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjEwNDMsIm5hbWUiOiJyYWZpcS5qQGhjOC5jb20iLCJpc3MiOiJodHRwczovL2FwaS5pbmNvbnRhY3QuY29tIiwic3ViIjoidXNlcjoyNzQ5OCIsImF1ZCI6ImludGVybmFsQGluQ29udGFjdCBJbmMuIiwiZXhwIjoxNTE3NTU2OTAyLCJpYXQiOjE1MTc1NTMzMDMsImljU2NvcGUiOiIxLDIsNCw1LDcsOCIsImljQ2x1c3RlcklkIjoiSEM4IiwiaWNBZ2VudElkIjoyNzQ5OCwiaWNTUElkIjo3ODMsIm5iZiI6MTUxNzU1MzMwMn0.jzmRgJKE6ax6oGxL7i1fZXfMK3O-UVmCna02LBVIGY0zP1_MVTugbknMCny0xN5PiQV7vJX6FI0aCzjr6pZLZloc5f5m86vKhLVKsWEbnk_qPAsjlSnxJJUQO3k36WwLg4nnjcG2numVbETRfOxSvXTQfZzFFgIdEbe_4OxoNaSjOWkL0evGLpzBbPVi4tj2XliAhhkCSbA8iJQft-z1VFNYXeZ0VfkQ_NBi6C4XbkRIegcRIUh-1GtvF9_MgUvjv4r9fUkig6eTeZ0YbclAnKvQIbHhk3Jf5koLG7a_-FoRf4y3gy5pgizF6x_Z2qPOe0_e2WqtmWOsY3PTxsqJ1A";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/lists/call-lists/" + listId + "/upload";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"skillId\":\"" + skillId +
                "\",\"fileName\": \"" + fileName +
                "\",\"forceOverwrite\":\"" + forceOverwrite +
                "\",\"expirationDate\":\"" + expirationDate +
                 "\",\"listFile\":\"" + listFile +
                "\"}";


                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string deleteDncGroupsScrubbedSkillsByGroupIdSkillId(int groupId, int skillId)
        {

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/dnc-groups/" + groupId + "/scrubbed-skills/" + skillId;
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "DELETE";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string postDncGroupsScrubbedSkillsByGroupIdSkillId(int groupId, int skillId)
        {
            //accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjEwNDMsIm5hbWUiOiJyYWZpcS5qQGhjOC5jb20iLCJpc3MiOiJodHRwczovL2FwaS5pbmNvbnRhY3QuY29tIiwic3ViIjoidXNlcjoyNzQ5OCIsImF1ZCI6ImludGVybmFsQGluQ29udGFjdCBJbmMuIiwiZXhwIjoxNTE3NTU2OTAyLCJpYXQiOjE1MTc1NTMzMDMsImljU2NvcGUiOiIxLDIsNCw1LDcsOCIsImljQ2x1c3RlcklkIjoiSEM4IiwiaWNBZ2VudElkIjoyNzQ5OCwiaWNTUElkIjo3ODMsIm5iZiI6MTUxNzU1MzMwMn0.jzmRgJKE6ax6oGxL7i1fZXfMK3O-UVmCna02LBVIGY0zP1_MVTugbknMCny0xN5PiQV7vJX6FI0aCzjr6pZLZloc5f5m86vKhLVKsWEbnk_qPAsjlSnxJJUQO3k36WwLg4nnjcG2numVbETRfOxSvXTQfZzFFgIdEbe_4OxoNaSjOWkL0evGLpzBbPVi4tj2XliAhhkCSbA8iJQft-z1VFNYXeZ0VfkQ_NBi6C4XbkRIegcRIUh-1GtvF9_MgUvjv4r9fUkig6eTeZ0YbclAnKvQIbHhk3Jf5koLG7a_-FoRf4y3gy5pgizF6x_Z2qPOe0_e2WqtmWOsY3PTxsqJ1A";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/dnc-groups/" + groupId + "/scrubbed-skills/" + skillId;
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string postDncGroupsByGroupIdSkillId(int groupId, int skillId)
        {
            //accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjEwNDMsIm5hbWUiOiJyYWZpcS5qQGhjOC5jb20iLCJpc3MiOiJodHRwczovL2FwaS5pbmNvbnRhY3QuY29tIiwic3ViIjoidXNlcjoyNzQ5OCIsImF1ZCI6ImludGVybmFsQGluQ29udGFjdCBJbmMuIiwiZXhwIjoxNTE3NTU2OTAyLCJpYXQiOjE1MTc1NTMzMDMsImljU2NvcGUiOiIxLDIsNCw1LDcsOCIsImljQ2x1c3RlcklkIjoiSEM4IiwiaWNBZ2VudElkIjoyNzQ5OCwiaWNTUElkIjo3ODMsIm5iZiI6MTUxNzU1MzMwMn0.jzmRgJKE6ax6oGxL7i1fZXfMK3O-UVmCna02LBVIGY0zP1_MVTugbknMCny0xN5PiQV7vJX6FI0aCzjr6pZLZloc5f5m86vKhLVKsWEbnk_qPAsjlSnxJJUQO3k36WwLg4nnjcG2numVbETRfOxSvXTQfZzFFgIdEbe_4OxoNaSjOWkL0evGLpzBbPVi4tj2XliAhhkCSbA8iJQft-z1VFNYXeZ0VfkQ_NBi6C4XbkRIegcRIUh-1GtvF9_MgUvjv4r9fUkig6eTeZ0YbclAnKvQIbHhk3Jf5koLG7a_-FoRf4y3gy5pgizF6x_Z2qPOe0_e2WqtmWOsY3PTxsqJ1A";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/dnc-groups/" + groupId + "/contributing-skills/" + skillId;
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string deleteDncGroupsByGroupIdSkillId(int groupId, int skillId)
        {
            //accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjEwNDMsIm5hbWUiOiJyYWZpcS5qQGhjOC5jb20iLCJpc3MiOiJodHRwczovL2FwaS5pbmNvbnRhY3QuY29tIiwic3ViIjoidXNlcjoyNzQ5OCIsImF1ZCI6ImludGVybmFsQGluQ29udGFjdCBJbmMuIiwiZXhwIjoxNTE3NTU2OTAyLCJpYXQiOjE1MTc1NTMzMDMsImljU2NvcGUiOiIxLDIsNCw1LDcsOCIsImljQ2x1c3RlcklkIjoiSEM4IiwiaWNBZ2VudElkIjoyNzQ5OCwiaWNTUElkIjo3ODMsIm5iZiI6MTUxNzU1MzMwMn0.jzmRgJKE6ax6oGxL7i1fZXfMK3O-UVmCna02LBVIGY0zP1_MVTugbknMCny0xN5PiQV7vJX6FI0aCzjr6pZLZloc5f5m86vKhLVKsWEbnk_qPAsjlSnxJJUQO3k36WwLg4nnjcG2numVbETRfOxSvXTQfZzFFgIdEbe_4OxoNaSjOWkL0evGLpzBbPVi4tj2XliAhhkCSbA8iJQft-z1VFNYXeZ0VfkQ_NBi6C4XbkRIegcRIUh-1GtvF9_MgUvjv4r9fUkig6eTeZ0YbclAnKvQIbHhk3Jf5koLG7a_-FoRf4y3gy5pgizF6x_Z2qPOe0_e2WqtmWOsY3PTxsqJ1A";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/dnc-groups/" + groupId + "/contributing-skills/" + skillId;
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "DELETE";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string UpdateDncGroupsByGroupId(int groupId, string dncGroupName, string dncGroupDescription, string isActive = "true")
        {
            //accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjEwNDMsIm5hbWUiOiJyYWZpcS5qQGhjOC5jb20iLCJpc3MiOiJodHRwczovL2FwaS5pbmNvbnRhY3QuY29tIiwic3ViIjoidXNlcjoyNzQ5OCIsImF1ZCI6ImludGVybmFsQGluQ29udGFjdCBJbmMuIiwiZXhwIjoxNTE3NTU2OTAyLCJpYXQiOjE1MTc1NTMzMDMsImljU2NvcGUiOiIxLDIsNCw1LDcsOCIsImljQ2x1c3RlcklkIjoiSEM4IiwiaWNBZ2VudElkIjoyNzQ5OCwiaWNTUElkIjo3ODMsIm5iZiI6MTUxNzU1MzMwMn0.jzmRgJKE6ax6oGxL7i1fZXfMK3O-UVmCna02LBVIGY0zP1_MVTugbknMCny0xN5PiQV7vJX6FI0aCzjr6pZLZloc5f5m86vKhLVKsWEbnk_qPAsjlSnxJJUQO3k36WwLg4nnjcG2numVbETRfOxSvXTQfZzFFgIdEbe_4OxoNaSjOWkL0evGLpzBbPVi4tj2XliAhhkCSbA8iJQft-z1VFNYXeZ0VfkQ_NBi6C4XbkRIegcRIUh-1GtvF9_MgUvjv4r9fUkig6eTeZ0YbclAnKvQIbHhk3Jf5koLG7a_-FoRf4y3gy5pgizF6x_Z2qPOe0_e2WqtmWOsY3PTxsqJ1A";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/dnc-groups/" + groupId;
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"dncGroupName\":\"" + dncGroupName
                    + "\",\"dncGroupDescription\":\"" + dncGroupDescription
                    + "\",\"isActive\":\"" + isActive +
                 "\"}";


                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string postDncGroups(string dncGroupName, string dncGroupDescription)
        {
            //accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjEwNDMsIm5hbWUiOiJyYWZpcS5qQGhjOC5jb20iLCJpc3MiOiJodHRwczovL2FwaS5pbmNvbnRhY3QuY29tIiwic3ViIjoidXNlcjoyNzQ5OCIsImF1ZCI6ImludGVybmFsQGluQ29udGFjdCBJbmMuIiwiZXhwIjoxNTE3NTU2OTAyLCJpYXQiOjE1MTc1NTMzMDMsImljU2NvcGUiOiIxLDIsNCw1LDcsOCIsImljQ2x1c3RlcklkIjoiSEM4IiwiaWNBZ2VudElkIjoyNzQ5OCwiaWNTUElkIjo3ODMsIm5iZiI6MTUxNzU1MzMwMn0.jzmRgJKE6ax6oGxL7i1fZXfMK3O-UVmCna02LBVIGY0zP1_MVTugbknMCny0xN5PiQV7vJX6FI0aCzjr6pZLZloc5f5m86vKhLVKsWEbnk_qPAsjlSnxJJUQO3k36WwLg4nnjcG2numVbETRfOxSvXTQfZzFFgIdEbe_4OxoNaSjOWkL0evGLpzBbPVi4tj2XliAhhkCSbA8iJQft-z1VFNYXeZ0VfkQ_NBi6C4XbkRIegcRIUh-1GtvF9_MgUvjv4r9fUkig6eTeZ0YbclAnKvQIbHhk3Jf5koLG7a_-FoRf4y3gy5pgizF6x_Z2qPOe0_e2WqtmWOsY3PTxsqJ1A";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/dnc-groups";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"dncGroupName\":\"" + dncGroupName
                    + "\",\"dncGroupDescription\":\"" + dncGroupDescription +
                 "\"}";


                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string postContactsTagsByContactId(double contactId, string tagId)
        {
            //accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjEwNDMsIm5hbWUiOiJyYWZpcS5qQGhjOC5jb20iLCJpc3MiOiJodHRwczovL2FwaS5pbmNvbnRhY3QuY29tIiwic3ViIjoidXNlcjoyNzQ5OCIsImF1ZCI6ImludGVybmFsQGluQ29udGFjdCBJbmMuIiwiZXhwIjoxNTE3NTU2OTAyLCJpYXQiOjE1MTc1NTMzMDMsImljU2NvcGUiOiIxLDIsNCw1LDcsOCIsImljQ2x1c3RlcklkIjoiSEM4IiwiaWNBZ2VudElkIjoyNzQ5OCwiaWNTUElkIjo3ODMsIm5iZiI6MTUxNzU1MzMwMn0.jzmRgJKE6ax6oGxL7i1fZXfMK3O-UVmCna02LBVIGY0zP1_MVTugbknMCny0xN5PiQV7vJX6FI0aCzjr6pZLZloc5f5m86vKhLVKsWEbnk_qPAsjlSnxJJUQO3k36WwLg4nnjcG2numVbETRfOxSvXTQfZzFFgIdEbe_4OxoNaSjOWkL0evGLpzBbPVi4tj2XliAhhkCSbA8iJQft-z1VFNYXeZ0VfkQ_NBi6C4XbkRIegcRIUh-1GtvF9_MgUvjv4r9fUkig6eTeZ0YbclAnKvQIbHhk3Jf5koLG7a_-FoRf4y3gy5pgizF6x_Z2qPOe0_e2WqtmWOsY3PTxsqJ1A";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/contacts/" + contactId + "/tags";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"tags\":[{\"tagId\":\"" + tagId +
                 "\"}]}";


                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getContactFileByContactId(double contactId, string fields = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/contacts/" + contactId + "/files";

                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string deleteAgentGroupsByGroupId(int groupId, int agents)
        {
            //accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjEwNDMsIm5hbWUiOiJyYWZpcS5qQGhjOC5jb20iLCJpc3MiOiJodHRwczovL2FwaS5pbmNvbnRhY3QuY29tIiwic3ViIjoidXNlcjoyNzQ5OCIsImF1ZCI6ImludGVybmFsQGluQ29udGFjdCBJbmMuIiwiZXhwIjoxNTE3NTU2OTAyLCJpYXQiOjE1MTc1NTMzMDMsImljU2NvcGUiOiIxLDIsNCw1LDcsOCIsImljQ2x1c3RlcklkIjoiSEM4IiwiaWNBZ2VudElkIjoyNzQ5OCwiaWNTUElkIjo3ODMsIm5iZiI6MTUxNzU1MzMwMn0.jzmRgJKE6ax6oGxL7i1fZXfMK3O-UVmCna02LBVIGY0zP1_MVTugbknMCny0xN5PiQV7vJX6FI0aCzjr6pZLZloc5f5m86vKhLVKsWEbnk_qPAsjlSnxJJUQO3k36WwLg4nnjcG2numVbETRfOxSvXTQfZzFFgIdEbe_4OxoNaSjOWkL0evGLpzBbPVi4tj2XliAhhkCSbA8iJQft-z1VFNYXeZ0VfkQ_NBi6C4XbkRIegcRIUh-1GtvF9_MgUvjv4r9fUkig6eTeZ0YbclAnKvQIbHhk3Jf5koLG7a_-FoRf4y3gy5pgizF6x_Z2qPOe0_e2WqtmWOsY3PTxsqJ1A";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/groups/" + groupId + "/agents";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"agents\":[{\"agents\":" + agents +
                 "}]}";


                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "DELETE";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string postAgentGroupsByGroupId(int groupId, int agents)
        {
            //accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjEwNDMsIm5hbWUiOiJyYWZpcS5qQGhjOC5jb20iLCJpc3MiOiJodHRwczovL2FwaS5pbmNvbnRhY3QuY29tIiwic3ViIjoidXNlcjoyNzQ5OCIsImF1ZCI6ImludGVybmFsQGluQ29udGFjdCBJbmMuIiwiZXhwIjoxNTE3NTU2OTAyLCJpYXQiOjE1MTc1NTMzMDMsImljU2NvcGUiOiIxLDIsNCw1LDcsOCIsImljQ2x1c3RlcklkIjoiSEM4IiwiaWNBZ2VudElkIjoyNzQ5OCwiaWNTUElkIjo3ODMsIm5iZiI6MTUxNzU1MzMwMn0.jzmRgJKE6ax6oGxL7i1fZXfMK3O-UVmCna02LBVIGY0zP1_MVTugbknMCny0xN5PiQV7vJX6FI0aCzjr6pZLZloc5f5m86vKhLVKsWEbnk_qPAsjlSnxJJUQO3k36WwLg4nnjcG2numVbETRfOxSvXTQfZzFFgIdEbe_4OxoNaSjOWkL0evGLpzBbPVi4tj2XliAhhkCSbA8iJQft-z1VFNYXeZ0VfkQ_NBi6C4XbkRIegcRIUh-1GtvF9_MgUvjv4r9fUkig6eTeZ0YbclAnKvQIbHhk3Jf5koLG7a_-FoRf4y3gy5pgizF6x_Z2qPOe0_e2WqtmWOsY3PTxsqJ1A";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/groups/" + groupId + "/agents";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"agents\":[{\"agents\":" + agents +
                 "}]}";


                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getAgentGroupsByGroupId(int groupId, int top = 10000, int skip = 0, string orderby = "", string searchString = "", string assigned = "true", string fields = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/groups/" + groupId + "/agents" +
                 "?top=" + top + "&skip=" + skip + "&orderby=" + orderby + "&searchString=" + searchString + "&assigned=" + assigned + "&fields=" + fields;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string updateGroupsByGroupId(int groupId, string groupName, string isActive = "true", string notes = "")
        {


            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/groups/" + groupId;
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"groupName\":\"" + groupName +
                    "\",\"isActive\":" + isActive +
                    ",\"notes\":\"" + notes +
                    "\"}";

                //Create the request object

                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body               
                byte[] postDataBytes = new UTF8Encoding(true).GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getGroupsByGroupId(int groupId, string fields = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/groups/" + groupId;

                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string createGroups(string groupName = "", string isActive = "true", string notes = "")
        {
            //accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjEwNDMsIm5hbWUiOiJyYWZpcS5qQGhjOC5jb20iLCJpc3MiOiJodHRwczovL2FwaS5pbmNvbnRhY3QuY29tIiwic3ViIjoidXNlcjoyNzQ5OCIsImF1ZCI6ImludGVybmFsQGluQ29udGFjdCBJbmMuIiwiZXhwIjoxNTE3NTU2OTAyLCJpYXQiOjE1MTc1NTMzMDMsImljU2NvcGUiOiIxLDIsNCw1LDcsOCIsImljQ2x1c3RlcklkIjoiSEM4IiwiaWNBZ2VudElkIjoyNzQ5OCwiaWNTUElkIjo3ODMsIm5iZiI6MTUxNzU1MzMwMn0.jzmRgJKE6ax6oGxL7i1fZXfMK3O-UVmCna02LBVIGY0zP1_MVTugbknMCny0xN5PiQV7vJX6FI0aCzjr6pZLZloc5f5m86vKhLVKsWEbnk_qPAsjlSnxJJUQO3k36WwLg4nnjcG2numVbETRfOxSvXTQfZzFFgIdEbe_4OxoNaSjOWkL0evGLpzBbPVi4tj2XliAhhkCSbA8iJQft-z1VFNYXeZ0VfkQ_NBi6C4XbkRIegcRIUh-1GtvF9_MgUvjv4r9fUkig6eTeZ0YbclAnKvQIbHhk3Jf5koLG7a_-FoRf4y3gy5pgizF6x_Z2qPOe0_e2WqtmWOsY3PTxsqJ1A";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/groups";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"groups\":[{\"groupName\":\"" + groupName +
                "\",\"isActive\": " + isActive +
                ",\"notes\":\"" + notes + "\"}]}";


                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getGroups(int top = 10000, int skip = 0, string orderby = "groupId", string searchString = "", bool isActive = true, string fields = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/groups" +
                 "?top=" + top + "&skip=" + skip + "&orderby=" + orderby + "&searchString=" + searchString + "&isActive=" + isActive + "&fields=" + fields;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string updateSkillsGeneralSettingBySkillId(int skillId, string generalSettingJSONData)
        {

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/skills/" + skillId + "/parameters/general-settings";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                   

                string postData = generalSettingJSONData;//Create the JSON data for General Setting object

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getSkillsGeneralSettingBySkillId(int skillId, string fields = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/skills/" + skillId + "/parameters/general-settings" + "?fields=" + fields;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getUnassignedDispositionsBySkillId(int skillId, string searchString = "", string fields = "", int skip = 0, int top = 100, string orderby = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/skills/" + skillId + "/dispositions/unassigned" +
                 "?searchString=" + searchString + "&fields=" + fields + "&skip=" + skip + "&top=" + top + "&orderby=" + orderby;

                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getDispositionsBySkillId(int skillId, string searchString = "", string fields = "", string skip = "", string top = "", string orderby = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/skills/" + skillId + "/dispositions" +
                 "?searchString=" + searchString + "&fields=" + fields + "&skip=" + skip + "&top=" + top + "&orderby=" + orderby;

                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getSkillIdAgentsUnassigned(int skillId, string searchString = "", string fields = "", string skip = "", string top = "", string orderby = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/skills/" + skillId + "/agents/unassigned" +
                "?searchString=" + searchString + "&fields=" + fields + "&skip=" + skip + "&top=" + top + "&orderby=" + orderby;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string updateSkill(int skillId, string SkillJSONData)
        {

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/skills/" + skillId;
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                   

                string postData = SkillJSONData;//Create the JSON data for skill object

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string postSkill(string SkillJSONData)
        {

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/skills";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                   

                string postData = SkillJSONData;//Create the JSON data for skill object

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getDispositionsByClassifications(string fields = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/dispositions/classifications";
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string updateFilesExternal(string fileName, bool needsProcessing = true)
        {


            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/files/external";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"fileName\":\"" + fileName +
                    "\",\"needsProcessing\":" + needsProcessing + "}";

                //Create the request object

                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body               
                byte[] postDataBytes = new UTF8Encoding(true).GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string updateDispositionsByDispositionID(int dispositionId, string dispositionName = "", bool isPreviewDisposition = false, int classificationId = 1, bool isActive = true)
        {

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "services/" + version + "/dispositions/" + dispositionId;
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string               
                string postData = "{\"dispositionName\":\"" + dispositionName +
                  "\",\"isPreviewDisposition\":" + isPreviewDisposition +
                  ",\"classificationId\":" + classificationId +
                  ",\"isActive\":" + isActive + "}";
                // string postData = dispostionData; //We need to create postData for this request

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                //byte[] postDataBytes = System.Text.Encoding.GetEncoding().GetBytes(postData);
                byte[] postDataBytes = new UTF8Encoding(true).GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd(); return "success";
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                            return "Failure";
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getDispositionsByDispositionID(int dispositionId, string fields = "")
        {

            //Test to see if you have obtained a token
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/dispositions/" + dispositionId;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string createDispositions(string dispositionName, bool isPreviewDisposition, int classificationId)
        {
            // accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjEwNDMsIm5hbWUiOiJyYWZpcS5qQGhjOC5jb20iLCJpc3MiOiJodHRwczovL2FwaS5pbmNvbnRhY3QuY29tIiwic3ViIjoidXNlcjoyNzQ5OCIsImF1ZCI6ImludGVybmFsQGluQ29udGFjdCBJbmMuIiwiZXhwIjoxNTE3NTU2OTAyLCJpYXQiOjE1MTc1NTMzMDMsImljU2NvcGUiOiIxLDIsNCw1LDcsOCIsImljQ2x1c3RlcklkIjoiSEM4IiwiaWNBZ2VudElkIjoyNzQ5OCwiaWNTUElkIjo3ODMsIm5iZiI6MTUxNzU1MzMwMn0.jzmRgJKE6ax6oGxL7i1fZXfMK3O-UVmCna02LBVIGY0zP1_MVTugbknMCny0xN5PiQV7vJX6FI0aCzjr6pZLZloc5f5m86vKhLVKsWEbnk_qPAsjlSnxJJUQO3k36WwLg4nnjcG2numVbETRfOxSvXTQfZzFFgIdEbe_4OxoNaSjOWkL0evGLpzBbPVi4tj2XliAhhkCSbA8iJQft-z1VFNYXeZ0VfkQ_NBi6C4XbkRIegcRIUh-1GtvF9_MgUvjv4r9fUkig6eTeZ0YbclAnKvQIbHhk3Jf5koLG7a_-FoRf4y3gy5pgizF6x_Z2qPOe0_e2WqtmWOsY3PTxsqJ1A";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/dispositions";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"dispositions\" :[{\"dispositionName\":\"" + dispositionName +
                "\",\"isPreviewDisposition\": " + isPreviewDisposition +
                ",\"classificationId\":" + classificationId + "}]}";


                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                // byte[] postDataBytes = new UTF8Encoding(true).GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string createFile(string fileName, string file, bool overwrite = true)
        {
            //accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjEwNDMsIm5hbWUiOiJyYWZpcS5qQGhjOC5jb20iLCJpc3MiOiJodHRwczovL2FwaS5pbmNvbnRhY3QuY29tIiwic3ViIjoidXNlcjoyNzQ5OCIsImF1ZCI6ImludGVybmFsQGluQ29udGFjdCBJbmMuIiwiZXhwIjoxNTE3NTU2OTAyLCJpYXQiOjE1MTc1NTMzMDMsImljU2NvcGUiOiIxLDIsNCw1LDcsOCIsImljQ2x1c3RlcklkIjoiSEM4IiwiaWNBZ2VudElkIjoyNzQ5OCwiaWNTUElkIjo3ODMsIm5iZiI6MTUxNzU1MzMwMn0.jzmRgJKE6ax6oGxL7i1fZXfMK3O-UVmCna02LBVIGY0zP1_MVTugbknMCny0xN5PiQV7vJX6FI0aCzjr6pZLZloc5f5m86vKhLVKsWEbnk_qPAsjlSnxJJUQO3k36WwLg4nnjcG2numVbETRfOxSvXTQfZzFFgIdEbe_4OxoNaSjOWkL0evGLpzBbPVi4tj2XliAhhkCSbA8iJQft-z1VFNYXeZ0VfkQ_NBi6C4XbkRIegcRIUh-1GtvF9_MgUvjv4r9fUkig6eTeZ0YbclAnKvQIbHhk3Jf5koLG7a_-FoRf4y3gy5pgizF6x_Z2qPOe0_e2WqtmWOsY3PTxsqJ1A";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/files";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"fileName\":\"" + fileName +
                "\",\"file\": \"" + file +
                "\",\"overwrite\":" + overwrite + "}";


                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string createFilesExternal(string fileName, string file, bool overwrite = true, bool needsProcessing = true)
        {


            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/files/external";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"fileName\":\"" + fileName +
                    "\",\"file\":\"" + file +
                    "\",\"overwrite\":" + overwrite +
                    ",\"needsProcessing\":" + needsProcessing + "}";

                //Create the request object

                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                // byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                byte[] postDataBytes = new UTF8Encoding(true).GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getFilesExternal(string folderPath)
        {
            //string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjE0MTAxNCwibmFtZSI6Im5pa2hpbC5tQHNjMS5jb20iLCJpc3MiOiJodHRwczovL2FwaS5pbmNvbnRhY3QuY29tIiwic3ViIjoidXNlcjoyMDc5MzIiLCJhdWQiOiJpbnRlcm5hbEBpbkNvbnRhY3QgSW5jLiIsImV4cCI6MTUxNzMyMTAwMSwiaWF0IjoxNTE3MzE3NDAyLCJpY1Njb3BlIjoiMSwyLDQsNSw3LDgiLCJpY0NsdXN0ZXJJZCI6IlNDMSIsImljQWdlbnRJZCI6MjA3OTMyLCJpY1NQSWQiOjUyNzQsIm5iZiI6MTUxNzMxNzQwMX0.FP5XQ41YsaeM-nXZv140HwEvt1ZlgFaqxf6wy4VnKyfUybtlri9P23FHcoZP_ilFlcqFiM1d_2KU6JWqQvsUyrXqebGvcVGNC0IHEt6VP5qMmFhwge8BDyJCS85CnCRz0khjwm6L39-KC6GhVIsbw0X-1eobpPHKtaYSzOP609qZULn9LNA2pTjJjx14v2I7Tnr0Lx5irrIaa5j_wOjRno0snVQI_lFnbBqK55JqpXA2vPZN7-Yb0Y2-XLObn0RtGAkmhTy0r301YuCiTJiib_e3g2-AX5FbAVS7Mli3HDvY0FdapylryQIsJTyK5I2LqhSgnOwYAtzIjtrVCsnjHg";

            //Test to see if you have obtained a token
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/files/external" + "?folderPath=" + folderPath;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string deleteFolderByName(string folderName)
        {


            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/folders";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string               
                string postData = "{\"folderName\":\"" + folderName +
                  "\"}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "DELETE";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return "success";
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                            return "Failure";
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getFolders(string folderName)
        {


            //Test to see if you have obtained a token
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/folders" + "?folderName=" + folderName;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string updateFile(string oldPath = "", string newPath = "", bool overwrite = false)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/files";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string               
                string postData = "{\"oldPath\":\"" + oldPath +
                  "\",\"newPath\":\"" + newPath +
                  "\",\"bool\":" + overwrite + "}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return "success";
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                            return "Failure";
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string deleteFileByName(string fileName)
        {

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/files";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string               
                string postData = "{\"fileName\":\"" + fileName +
                  "\"}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "DELETE";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return "success";
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }

                            return "Failure";
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        // public string createFile(string fileName, string file, bool overwrite=true)
        //{

        //    
        //    //Test to see if you have obtained a token
        //    if (!string.IsNullOrEmpty(accessToken))
        //    {

        //        string apiURL = "/services/"+version+"/files";
        //        string endpoint = baseURL + apiURL;

        //        //Build POST data as a JSON string                    

        //        string postData = "{\"fileName\":\"" + fileName +
        //            "\",\"file\":\"" + file +
        //            "\",\"overwrite\":" + overwrite + "}";

        //        //Create the request object

        //        var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
        //        request.Method = "POST";
        //        request.ContentType = "application/json";

        //        //Add any and all necessary headers
        //        request.Headers.Add("Authorization", "bearer " + accessToken);
        //        //Accept is a reserved header, so you must modify it rather than add
        //        request.Accept = "application/json, text/javascript, */*; q=0.01";

        //        //Write the PostData into the Request Body
        //        byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
        //        request.ContentLength = postDataBytes.Length;
        //        using (var writeStream = request.GetRequestStream())
        //        {
        //            writeStream.Write(postDataBytes, 0, postDataBytes.Length);
        //        }

        //        //Make the request
        //        try
        //        {
        //            // Get the Response from the Web Request
        //            using (var response = (System.Net.HttpWebResponse)request.GetResponse())
        //            {
        //                // Success do something with the response here.
        //                return response.StatusDescription;;
        //            }
        //        }
        //        catch (System.Net.WebException webException)
        //        {
        //            return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
        //            // You can catch that exception and retain the StatusCode for error handling.
        //        }
        //        catch (Exception ex)
        //        {
        //             return ex.Message; //Non Web Exception error, handle error
        //        }
        //    }
        //    else
        //    {
        //        return "Failure";  //No token - get one or handle error
        //    }
        //}
        // public string createFile(string fileName, string file, bool overwrite)
        //{

        //    //Test to see if you have obtained a token
        //    if (!string.IsNullOrEmpty(accessToken))
        //    {

        //        string apiURL = "/services/"+version+"/files";
        //        string endpoint = baseURL + apiURL;

        //        //Build POST data as a JSON string                    

        //        string postData = "{\"fileName\":\"" + fileName +
        //            "\", \"file\":\"" + file +
        //            "\",\"overwrite\":"+ overwrite + "}";

        //        //Create the request object
        //        var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
        //        request.Method = "POST";
        //        request.ContentType = "application/json";

        //        //Add any and all necessary headers
        //        request.Headers.Add("Authorization", "bearer " + accessToken);
        //        //Accept is a reserved header, so you must modify it rather than add
        //        request.Accept = "application/json, text/javascript, */*; q=0.01";

        //        //Write the PostData into the Request Body
        //        byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
        //        request.ContentLength = postDataBytes.Length;
        //        using (var writeStream = request.GetRequestStream())
        //        {
        //            writeStream.Write(postDataBytes, 0, postDataBytes.Length);
        //        }

        //        //Make the request
        //        try
        //        {
        //            // Get the Response from the Web Request
        //            using (var response = (System.Net.HttpWebResponse)request.GetResponse())
        //            {
        //                // Success do something with the response here.
        //                return response.StatusDescription;;
        //            }
        //        }
        //        catch (System.Net.WebException webException)
        //        {
        //            return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
        //            // You can catch that exception and retain the StatusCode for error handling.
        //        }
        //        catch (Exception ex)
        //        {
        //             return ex.Message; //Non Web Exception error, handle error
        //        }
        //    }
        //    else
        //    {
        //        return "Failure";  //No token - get one or handle error
        //    }
        //}
        public string UpdateTagsByTagId(int tagId, string tagName = "", string notes = "", string isActive = "")
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/tags/" + tagId;
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string               
                string postData = "{\"tagName\":\"" + tagName +
                    "\",\"notes\":\"" + isActive +
                    "\",\"isActive\":\"" + isActive +
                    "\"}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return "success";
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }

                            return "Failure";
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getTagsByTagId(int tagId)
        {
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/tags/" + tagId;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string postTags(string tagName = "", string notes = "")
        {


            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/tags";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"tagName\":\"" + tagName +
                    "\",\"notes\":\"" + notes + "\"}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getSecurityProfiles(int profileId)
        {

            //Test to see if you have obtained a token
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                //string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjUwLCJuYW1lIjoicGFzaGFAaGM5LmNvbSIsImlzcyI6Imh0dHBzOi8vYXBpLmluY29udGFjdC5jb20iLCJzdWIiOiJ1c2VyOjgxNzYiLCJhdWQiOiJpbnRlcm5hbEBpbkNvbnRhY3QgSW5jLiIsImV4cCI6MTUxNjI3ODY3NywiaWF0IjoxNTE2Mjc1MDc3LCJpY1Njb3BlIjoiMSwyLDQsNSw3LDgiLCJpY0NsdXN0ZXJJZCI6IkhDOSIsImljQWdlbnRJZCI6ODE3NiwiaWNTUElkIjo5NzQsIm5iZiI6MTUxNjI3NTA3N30.Q0hXFM4XXFx-ZyY1o-FDc2ThUVbabGdMyaUovxfjFyD1vk7GqZBLjTbNH2aLMyFYf1tG6NZN1VmWVK61t-Fjz5kqin2iTjj8zIjCt7K9PnPIUycmQj_BavLRIc6ZLNgiTQ6tKmKYbgLay_i0iA4PXGajLyarS8gGK2nkrEo4QNeup-8SeSQrPei4JMBvy-osfduGDVjZ-bYzt1x89r9qMPr7l7IZVlJAk7TfSzyDJJ5fK5GXCXu-e6m2CdZ03C88ovnTxHEm0jXXsIWbcIgaoOPLbXwOwo6-2Y4IGgt3F6QgTmti_yxVDK35dBYFzFsx9C5oY0lnmof6t_6KHKDk5w";
                string apiURL = "/services/" + version + "/security-profiles/" + profileId + "?profileId=" + profileId;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string UpdateMessageTemplateByTemplateId(int templateId, string templateName = "", bool isActive = false, string smsContent = "")
        {
            //get the toeken from jtest            

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/message-templates/" + templateId;
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string               
                string postData = "{\"templateName\":\"" + templateName +
                    "\",\"isActive\":" + isActive +
                    ",\"smsContent\":\"" + smsContent +
                    "\"}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return "success";
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }

                            return "Failure";
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getMessageTemplatesbyTemplateId(int templateId)
        {

            //Test to see if you have obtained a token
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                //string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjUwLCJuYW1lIjoicGFzaGFAaGM5LmNvbSIsImlzcyI6Imh0dHBzOi8vYXBpLmluY29udGFjdC5jb20iLCJzdWIiOiJ1c2VyOjgxNzYiLCJhdWQiOiJpbnRlcm5hbEBpbkNvbnRhY3QgSW5jLiIsImV4cCI6MTUxNjI3ODY3NywiaWF0IjoxNTE2Mjc1MDc3LCJpY1Njb3BlIjoiMSwyLDQsNSw3LDgiLCJpY0NsdXN0ZXJJZCI6IkhDOSIsImljQWdlbnRJZCI6ODE3NiwiaWNTUElkIjo5NzQsIm5iZiI6MTUxNjI3NTA3N30.Q0hXFM4XXFx-ZyY1o-FDc2ThUVbabGdMyaUovxfjFyD1vk7GqZBLjTbNH2aLMyFYf1tG6NZN1VmWVK61t-Fjz5kqin2iTjj8zIjCt7K9PnPIUycmQj_BavLRIc6ZLNgiTQ6tKmKYbgLay_i0iA4PXGajLyarS8gGK2nkrEo4QNeup-8SeSQrPei4JMBvy-osfduGDVjZ-bYzt1x89r9qMPr7l7IZVlJAk7TfSzyDJJ5fK5GXCXu-e6m2CdZ03C88ovnTxHEm0jXXsIWbcIgaoOPLbXwOwo6-2Y4IGgt3F6QgTmti_yxVDK35dBYFzFsx9C5oY0lnmof6t_6KHKDk5w";
                string apiURL = "/services/" + version + "/message-templates/" + templateId.ToString() + "?templateId=" + templateId;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getMessageTemplates(int templateTypeId)
        {

            //Test to see if you have obtained a token
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                //string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjUwLCJuYW1lIjoicGFzaGFAaGM5LmNvbSIsImlzcyI6Imh0dHBzOi8vYXBpLmluY29udGFjdC5jb20iLCJzdWIiOiJ1c2VyOjgxNzYiLCJhdWQiOiJpbnRlcm5hbEBpbkNvbnRhY3QgSW5jLiIsImV4cCI6MTUxNjI3ODY3NywiaWF0IjoxNTE2Mjc1MDc3LCJpY1Njb3BlIjoiMSwyLDQsNSw3LDgiLCJpY0NsdXN0ZXJJZCI6IkhDOSIsImljQWdlbnRJZCI6ODE3NiwiaWNTUElkIjo5NzQsIm5iZiI6MTUxNjI3NTA3N30.Q0hXFM4XXFx-ZyY1o-FDc2ThUVbabGdMyaUovxfjFyD1vk7GqZBLjTbNH2aLMyFYf1tG6NZN1VmWVK61t-Fjz5kqin2iTjj8zIjCt7K9PnPIUycmQj_BavLRIc6ZLNgiTQ6tKmKYbgLay_i0iA4PXGajLyarS8gGK2nkrEo4QNeup-8SeSQrPei4JMBvy-osfduGDVjZ-bYzt1x89r9qMPr7l7IZVlJAk7TfSzyDJJ5fK5GXCXu-e6m2CdZ03C88ovnTxHEm0jXXsIWbcIgaoOPLbXwOwo6-2Y4IGgt3F6QgTmti_yxVDK35dBYFzFsx9C5oY0lnmof6t_6KHKDk5w";
                string apiURL = "/services/" + version + "/message-templates" + "?templateTypeId=" + templateTypeId;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string postHiringSources(string sourceName)
        {


            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/hiring-sources";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"sourceName\":" + sourceName + "}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getDispositions(int skip = 0, int top = 100, string searchString = "", string fields = "", string orderby = "", bool isPreviewDispositions = true, string updatedSince = "")
        {


            //Test to see if you have obtained a token
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                //string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjUwLCJuYW1lIjoicGFzaGFAaGM5LmNvbSIsImlzcyI6Imh0dHBzOi8vYXBpLmluY29udGFjdC5jb20iLCJzdWIiOiJ1c2VyOjgxNzYiLCJhdWQiOiJpbnRlcm5hbEBpbkNvbnRhY3QgSW5jLiIsImV4cCI6MTUxNjI3ODY3NywiaWF0IjoxNTE2Mjc1MDc3LCJpY1Njb3BlIjoiMSwyLDQsNSw3LDgiLCJpY0NsdXN0ZXJJZCI6IkhDOSIsImljQWdlbnRJZCI6ODE3NiwiaWNTUElkIjo5NzQsIm5iZiI6MTUxNjI3NTA3N30.Q0hXFM4XXFx-ZyY1o-FDc2ThUVbabGdMyaUovxfjFyD1vk7GqZBLjTbNH2aLMyFYf1tG6NZN1VmWVK61t-Fjz5kqin2iTjj8zIjCt7K9PnPIUycmQj_BavLRIc6ZLNgiTQ6tKmKYbgLay_i0iA4PXGajLyarS8gGK2nkrEo4QNeup-8SeSQrPei4JMBvy-osfduGDVjZ-bYzt1x89r9qMPr7l7IZVlJAk7TfSzyDJJ5fK5GXCXu-e6m2CdZ03C88ovnTxHEm0jXXsIWbcIgaoOPLbXwOwo6-2Y4IGgt3F6QgTmti_yxVDK35dBYFzFsx9C5oY0lnmof6t_6KHKDk5w";
                string apiURL = "/services/" + version + "/dispositions" + "?skip=" + skip + "&top=" + top + "&searchString=" + searchString +
                    "&fields=" + fields + "&orderby=" + orderby + "&isPreviewDispositions=" + isPreviewDispositions + "&updatedSince=" + updatedSince;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getDataDefinitionDataTypes()
        {
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                //string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjUwLCJuYW1lIjoicGFzaGFAaGM5LmNvbSIsImlzcyI6Imh0dHBzOi8vYXBpLmluY29udGFjdC5jb20iLCJzdWIiOiJ1c2VyOjgxNzYiLCJhdWQiOiJpbnRlcm5hbEBpbkNvbnRhY3QgSW5jLiIsImV4cCI6MTUxNjI3ODY3NywiaWF0IjoxNTE2Mjc1MDc3LCJpY1Njb3BlIjoiMSwyLDQsNSw3LDgiLCJpY0NsdXN0ZXJJZCI6IkhDOSIsImljQWdlbnRJZCI6ODE3NiwiaWNTUElkIjo5NzQsIm5iZiI6MTUxNjI3NTA3N30.Q0hXFM4XXFx-ZyY1o-FDc2ThUVbabGdMyaUovxfjFyD1vk7GqZBLjTbNH2aLMyFYf1tG6NZN1VmWVK61t-Fjz5kqin2iTjj8zIjCt7K9PnPIUycmQj_BavLRIc6ZLNgiTQ6tKmKYbgLay_i0iA4PXGajLyarS8gGK2nkrEo4QNeup-8SeSQrPei4JMBvy-osfduGDVjZ-bYzt1x89r9qMPr7l7IZVlJAk7TfSzyDJJ5fK5GXCXu-e6m2CdZ03C88ovnTxHEm0jXXsIWbcIgaoOPLbXwOwo6-2Y4IGgt3F6QgTmti_yxVDK35dBYFzFsx9C5oY0lnmof6t_6KHKDk5w";
                string apiURL = "/services/" + version + "/data-definitions/data-types";
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getBusinessUnit(string fields = "", bool includeTrustedBusinessUnits = false)
        {
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                //string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjUwLCJuYW1lIjoicGFzaGFAaGM5LmNvbSIsImlzcyI6Imh0dHBzOi8vYXBpLmluY29udGFjdC5jb20iLCJzdWIiOiJ1c2VyOjgxNzYiLCJhdWQiOiJpbnRlcm5hbEBpbkNvbnRhY3QgSW5jLiIsImV4cCI6MTUxNjI3ODY3NywiaWF0IjoxNTE2Mjc1MDc3LCJpY1Njb3BlIjoiMSwyLDQsNSw3LDgiLCJpY0NsdXN0ZXJJZCI6IkhDOSIsImljQWdlbnRJZCI6ODE3NiwiaWNTUElkIjo5NzQsIm5iZiI6MTUxNjI3NTA3N30.Q0hXFM4XXFx-ZyY1o-FDc2ThUVbabGdMyaUovxfjFyD1vk7GqZBLjTbNH2aLMyFYf1tG6NZN1VmWVK61t-Fjz5kqin2iTjj8zIjCt7K9PnPIUycmQj_BavLRIc6ZLNgiTQ6tKmKYbgLay_i0iA4PXGajLyarS8gGK2nkrEo4QNeup-8SeSQrPei4JMBvy-osfduGDVjZ-bYzt1x89r9qMPr7l7IZVlJAk7TfSzyDJJ5fK5GXCXu-e6m2CdZ03C88ovnTxHEm0jXXsIWbcIgaoOPLbXwOwo6-2Y4IGgt3F6QgTmti_yxVDK35dBYFzFsx9C5oY0lnmof6t_6KHKDk5w";
                string apiURL = "/services/" + version + "/business-unit" + "?fields=" + fields + "&includeTrustedBusinessUnits=" + includeTrustedBusinessUnits;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getBrandingProfile(int businessUnitId, string fields = "")
        {
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/branding-profiles" + "?businessUnitId=" + businessUnitId + "&fields=" + fields;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string deleteTeamsUnavailablebyTeamId(int teamId, string outstateId)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/teams/" + teamId + "/unavailable-codes";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string               
                string postData = "{\"codes\":[{\"outstateId\":\"" + outstateId +
                  "\"}]}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "Delete";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return "success";
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                            return "Failure";
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string createTeamsUnavailablebyTeamId(int teamId, string outstateId)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/teams/" + teamId + "/unavailable-codes";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"codes\":[{\"outstateId\":\"" + outstateId +
                   "\"}]}";



                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "Post";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return "success";
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                            return "Failure";
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string createTeamsAgentbyTeamId(int teamId, string agentId)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/teams/" + teamId + "/agents";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"agents\":[{\"agentId\":\"" + agentId +
                   "\"}]}";



                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "Post";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return "success";
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                            return "Failure";
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getTeamsAgentbyTeamId(int teamId, string fields = "")
        {
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                //string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjUwLCJuYW1lIjoicGFzaGFAaGM5LmNvbSIsImlzcyI6Imh0dHBzOi8vYXBpLmluY29udGFjdC5jb20iLCJzdWIiOiJ1c2VyOjgxNzYiLCJhdWQiOiJpbnRlcm5hbEBpbkNvbnRhY3QgSW5jLiIsImV4cCI6MTUxNjI3ODY3NywiaWF0IjoxNTE2Mjc1MDc3LCJpY1Njb3BlIjoiMSwyLDQsNSw3LDgiLCJpY0NsdXN0ZXJJZCI6IkhDOSIsImljQWdlbnRJZCI6ODE3NiwiaWNTUElkIjo5NzQsIm5iZiI6MTUxNjI3NTA3N30.Q0hXFM4XXFx-ZyY1o-FDc2ThUVbabGdMyaUovxfjFyD1vk7GqZBLjTbNH2aLMyFYf1tG6NZN1VmWVK61t-Fjz5kqin2iTjj8zIjCt7K9PnPIUycmQj_BavLRIc6ZLNgiTQ6tKmKYbgLay_i0iA4PXGajLyarS8gGK2nkrEo4QNeup-8SeSQrPei4JMBvy-osfduGDVjZ-bYzt1x89r9qMPr7l7IZVlJAk7TfSzyDJJ5fK5GXCXu-e6m2CdZ03C88ovnTxHEm0jXXsIWbcIgaoOPLbXwOwo6-2Y4IGgt3F6QgTmti_yxVDK35dBYFzFsx9C5oY0lnmof6t_6KHKDk5w";
                string apiURL = "/services/" + version + "/teams/" + teamId + "/agents" + "?fields=" + fields;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string deleteTeamsAgentbyTeamId(int teamId, int transferTeamId, int agentId)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/teams/" + teamId + "/agents";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"transferTeamId\":\"" + transferTeamId +
                   "\",\"agents\":[{\"agentId\":\"" + agentId +
                   "\"}]}";



                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "Delete";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return "success";
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                            return "Failure";
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getAgentTeams(string fields = "", string updatedSince = "")
        {
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                //string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjUwLCJuYW1lIjoicGFzaGFAaGM5LmNvbSIsImlzcyI6Imh0dHBzOi8vYXBpLmluY29udGFjdC5jb20iLCJzdWIiOiJ1c2VyOjgxNzYiLCJhdWQiOiJpbnRlcm5hbEBpbkNvbnRhY3QgSW5jLiIsImV4cCI6MTUxNjI3ODY3NywiaWF0IjoxNTE2Mjc1MDc3LCJpY1Njb3BlIjoiMSwyLDQsNSw3LDgiLCJpY0NsdXN0ZXJJZCI6IkhDOSIsImljQWdlbnRJZCI6ODE3NiwiaWNTUElkIjo5NzQsIm5iZiI6MTUxNjI3NTA3N30.Q0hXFM4XXFx-ZyY1o-FDc2ThUVbabGdMyaUovxfjFyD1vk7GqZBLjTbNH2aLMyFYf1tG6NZN1VmWVK61t-Fjz5kqin2iTjj8zIjCt7K9PnPIUycmQj_BavLRIc6ZLNgiTQ6tKmKYbgLay_i0iA4PXGajLyarS8gGK2nkrEo4QNeup-8SeSQrPei4JMBvy-osfduGDVjZ-bYzt1x89r9qMPr7l7IZVlJAk7TfSzyDJJ5fK5GXCXu-e6m2CdZ03C88ovnTxHEm0jXXsIWbcIgaoOPLbXwOwo6-2Y4IGgt3F6QgTmti_yxVDK35dBYFzFsx9C5oY0lnmof6t_6KHKDk5w";
                string apiURL = "/services/" + version + "/teams" + "?fields=" + fields + "&updatedSince=" + updatedSince;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string UpdateAgent(int agentId, string agent)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agent/" + agentId.ToString();

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = agent; //agent JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd(); return "success";
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                            return "Failure";
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error       
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        //create message in central and get that using /services/v3.0/agents/{agentId}/messages
        //note message get expired after 1 hour 
        public string postAgentState(int agentId, string state, string outStateId)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/agents/" + agentId + "/state";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"state\":\"" + state +
                    "\", \"outStateId\":\"" + outStateId + "\"}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string postAgentMessage(int agentId, string state, int outStateId)
        {
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agents/" + agentId + "/state" + "?state=" + state + "&outStateId=" + outStateId;
                // string apiURL = "/services/{version}/agents/messages/"+MessageId;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "Post";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string postTeam(string teams)
        {

            //Test to see if you have obtained a token
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agents/messages/" + "MessageId";
                // string apiURL = "/services/{version}/agents/messages/"+MessageId;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "Delete";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string deleteAgentMessagesByMessageId(int MessageId)
        {


            //Test to see if you have obtained a token
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agents/messages/" + MessageId;
                // string apiURL = "/services/{version}/agents/messages/"+MessageId;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "Delete";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string getAgentPatterns()
        {

            //Test to see if you have obtained a token
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agent-patterns";
                // string apiURL = "/services/{version}/agent-patterns";
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }

        public string PostAgentMessages(string message, DateTime startDate, string subject, DateTime validUntil, int targetId = 0, string targetType = "Agent", int expireMinutes = 5)
        {

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/{version}/agents/messages";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"agentMessages\":[{\"message\":\"" + message +
                    "\", \"startDate\":\"" + startDate +
                    "\", \"subject\":\"" + subject +
                    "\", \"targetId\":" + targetId +
                     ", \"validUntil\":\"" + validUntil +
                     "\", \"expireMinutes\":" + expireMinutes + "}]}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd(); return "success";
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                            return "Failure";
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        public string getAgentGroupsByAgentId(int AgentId, string fields = "")
        {

            //Test to see if you have obtained a token
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agents/" + AgentId + "/groups" + "?fields=" + fields;
                // string apiURL = "/services/{version}/agents/" + AgentId + "?fields=" + fields;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    Console.WriteLine(webException.StackTrace);
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;
                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    
            }
        }
        public string GetAddress()
        {

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/address-books";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {

                        return "Success";
                        // Success do something with the response here.
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message;
                    //.NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }

        public string GetAgentSkills(string updatedSince = "", string searchString = "", string fields = "", string skip = "",
        string top = "", string orderBy = "")
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agents/skills?updatedSince=" + updatedSince +
                        "&searchString=" + searchString + "&fields=" + fields + "&skip=" + skip +
                        "&top=" + top + "&orderBy=" + orderBy;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return "Success";  // Success do something with the response here.
                    }
                }
                catch (System.Net.WebException webException)
                {
                    //.NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }
        //public string GetAgentSkills()
        //{
        //    //Test to see if you have obtained a token
        //    if (!string.IsNullOrEmpty(accessToken))
        //    {
        //        string apiURL = "/services/v11.0/agents-states";

        //        //baseURL was returned with your successful token request
        //        string endpoint = baseURL + apiURL;
        //        string authorizationHeader = "Authorization:bearer " + accessToken;
        //        //Create the request object
        //        var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
        //        request.Method = "GET";
        //        request.ContentType = "application/x-www-form-urlencoded";

        //        //Add any and all necessary headers
        //        request.Headers.Add("Authorization", "bearer " + accessToken);
        //        //Accept is a reserved header, so you must modify it rather than add
        //        request.Accept = "application/json, text/javascript, */*; q=0.01";

        //        //Make the request
        //        try
        //        {
        //            using (var response = (System.Net.HttpWebResponse)request.GetResponse())
        //            {
        //                //baseURL = string.Empty;
        //                return "Success";
        //                // Success do something with the response here.
        //            }
        //        }
        //        catch (System.Net.WebException webException)
        //        {
        //            return webException.Message;
        //            //.NET will throw a System.Net.WebException on StatusCodes other than 200.  
        //            // You can catch that exception and retain the StatusCode for error handling.
        //        }
        //        catch (Exception ex)
        //        {
        //             return ex.Message; //Non Web Exception error, handle error
        //        }
        //    }
        //    else
        //    {
        //        return "Failure";  //No token - get one or handle error
        //    }
        //}

        /*  public string ContructURL(string APIURL)
        {
            string URL = string.Empty;
            if (!string.IsNullOrEmpty(APIURL))
            {
                string varPattern = @"\{([^}]*)\}";
                string varReplace = apiob.APIversion;
                URL = Regex.Replace(APIURL, varPattern, varReplace);
                apiob.InvokeUrl = apiob.APIBaseURL + URL;
            }
            return URL;
        }*/

        public string Authentication(string username, string password, string grant_type = "password")
        {

            //Test to see if you have obtained a token

            string apiURL = "InContactAuthorizationServer/Token";
            string endpoint = tokenURL + apiURL;

            //Build POST data as a JSON string                    

            string postData = "{\"grant_type\":\"" + grant_type +
                "\", \"password\":\"" + password +
                "\", \"username\":\"" + username +
                 "\"}";

            //Create the request object
            var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
            request.Method = "POST";
            request.ContentType = "application/json";

            //Add any and all necessary headers
            request.Headers.Add("Authorization", "basic " + "aW50ZXJuYWxAaW5Db250YWN0IEluYy46UVVFNVFrTkdSRE0zUWpFME5FUkRSamczUlVORFJVTkRRakU0TlRrek5UYz0=");
            //Accept is a reserved header, so you must modify it rather than add
            request.Accept = "application/json, text/javascript, */*; q=0.01";

            //Write the PostData into the Request Body
            byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
            request.ContentLength = postDataBytes.Length;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            using (var writeStream = request.GetRequestStream())
            {
                writeStream.Write(postDataBytes, 0, postDataBytes.Length);
            }

            //Make the request
            try
            {
                using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                {
                    string responseBody = "";
                    int statusCode = (int)response.StatusCode;
                    string statusDescription = response.StatusDescription;
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var reader = new System.IO.StreamReader(responseStream))
                            {
                                responseBody = reader.ReadToEnd();
                                //responseBody now contains the JSON string of the response.
                                //save and parse it as you see fit.
                            }
                        }

                    }
                    return accessToken = responseBody.ToString().Split(':')[1].Split(',')[0].Replace('\"', ' ').ToString();
                }
            }
            catch (System.Net.WebException webException)
            {
                return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                // You can catch that exception and retain the StatusCode for error handling.
            }



        }
        //public string postDncGroupsScrubbedSkillsByGroupIdSkillId(int groupId, int skillId)
        //{
        //    //accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjEwNDMsIm5hbWUiOiJyYWZpcS5qQGhjOC5jb20iLCJpc3MiOiJodHRwczovL2FwaS5pbmNvbnRhY3QuY29tIiwic3ViIjoidXNlcjoyNzQ5OCIsImF1ZCI6ImludGVybmFsQGluQ29udGFjdCBJbmMuIiwiZXhwIjoxNTE3NTU2OTAyLCJpYXQiOjE1MTc1NTMzMDMsImljU2NvcGUiOiIxLDIsNCw1LDcsOCIsImljQ2x1c3RlcklkIjoiSEM4IiwiaWNBZ2VudElkIjoyNzQ5OCwiaWNTUElkIjo3ODMsIm5iZiI6MTUxNzU1MzMwMn0.jzmRgJKE6ax6oGxL7i1fZXfMK3O-UVmCna02LBVIGY0zP1_MVTugbknMCny0xN5PiQV7vJX6FI0aCzjr6pZLZloc5f5m86vKhLVKsWEbnk_qPAsjlSnxJJUQO3k36WwLg4nnjcG2numVbETRfOxSvXTQfZzFFgIdEbe_4OxoNaSjOWkL0evGLpzBbPVi4tj2XliAhhkCSbA8iJQft-z1VFNYXeZ0VfkQ_NBi6C4XbkRIegcRIUh-1GtvF9_MgUvjv4r9fUkig6eTeZ0YbclAnKvQIbHhk3Jf5koLG7a_-FoRf4y3gy5pgizF6x_Z2qPOe0_e2WqtmWOsY3PTxsqJ1A";

        //    //Test to see if you have obtained a token
        //    if (!string.IsNullOrEmpty(accessToken))
        //    {

        //        string apiURL = "/services/"+version+"/dnc-groups/" + groupId + "/scrubbed-skills/" + skillId;
        //        string endpoint = baseURL + apiURL;

        //        //Build POST data as a JSON string                    

        //        string postData = "{}";

        //        //Create the request object
        //        var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
        //        request.Method = "POST";
        //        request.ContentType = "application/json";

        //        //Add any and all necessary headers
        //        request.Headers.Add("Authorization", "bearer " + accessToken);
        //        //Accept is a reserved header, so you must modify it rather than add
        //        request.Accept = "application/json, text/javascript, */*; q=0.01";

        //        //Write the PostData into the Request Body
        //        byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
        //        request.ContentLength = postDataBytes.Length;
        //        using (var writeStream = request.GetRequestStream())
        //        {
        //            writeStream.Write(postDataBytes, 0, postDataBytes.Length);
        //        }

        //        //Make the request
        //        try
        //        {
        //            // Get the Response from the Web Request
        //            using (var response = (System.Net.HttpWebResponse)request.GetResponse())
        //            {
        //                // Success do something with the response here.
        //                return "Success";
        //            }
        //        }
        //        catch (System.Net.WebException webException)
        //        {
        //            return webException.ToString();
        //            return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
        //            // You can catch that exception and retain the StatusCode for error handling.
        //        }
        //        catch (Exception ex)
        //        {
        //            return ex.ToString();
        //             return ex.Message; //Non Web Exception error, handle error
        //        }
        //    }
        //    else
        //    {
        //        return "Failure";  //No token - get one or handle error
        //        return "Generating token failed";
        //    }
        //}   
        public string getAgentStates()
        {

            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agents-states";
                //string apiURL = "/services/{version}/agents-states";
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                                                 // You can catch that exception and retain the StatusCode for error handling.

                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;

                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    

            }
        }
        public string getTeams(string fields = "", string updatedSince = "", bool isActive = true, string searchString = "", int skip = 0, int top = 10000, string orderby = "")
        {

            //Test to see if you have obtained a token
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/teams"
                 + "?fields=" + fields + "&updatedSince=" +
                          updatedSince + "&isActive=" + isActive + "&searchString=" + searchString + "&skip=" + skip + "&top=" + top + "&orderby=" + orderby;
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                                                 // You can catch that exception and retain the StatusCode for error handling.

                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;

                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    

            }
        }
        public string getTeamByTeamId(int TeamId)
        {
            //Test to see if you have obtained a token
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/teams/" + TeamId;
                // string apiURL = "/services/{version}/agents-states";
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {


                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                                                 // You can catch that exception and retain the StatusCode for error handling.

                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;

                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    

            }
        }
        public string getAgentSkillsByAgentId(int AgentId, string fields = "", string updatedSince = "", string mediaTypeId = "", string outboundStrategy = "", string isSkillActive = "true", string searchString = "", string orderBy = "", int skip = 0, int top = 100)
        {

            //Test to see if you have obtained a token
            //See the sample on obtaining a token [link to token code] here
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/" + version + "/agents/" + AgentId + "/skills" + "?fields=" + fields + "&updatedSince=" + updatedSince + "&mediaTypeId=" + mediaTypeId + "&outboundStrategy=" + outboundStrategy +
                    "&outboundStrategy=" + outboundStrategy + "&isSkillActive=" + isSkillActive + "&searchString=" + searchString + "&orderBy=" + orderBy + "&skip=" + skip + "&top=" + top;

                // string apiURL = "/services/{version}/agents/"+ AgentId + "/skills";
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {


                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                                                 // You can catch that exception and retain the StatusCode for error handling.

                }
                catch (Exception ex)
                {
                    return ex.Message;  // Catch any non Web Exception error return ex.Message;

                }
            }
            else
            {
                return "Failure"; // No token - get one or handle error    

            }
        }

        public string getStandardAddressBookEntries(int addressBookId, string searchString = "", string fields = "", int skip = 1, int top = 100, string orderBy = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v12.0/address-books/" + addressBookId + "/entries" +
                     "?searchString=" + searchString + "&fields=" + fields + "&skip=" + skip + "&top=" + top + "&orderBy=" + orderBy;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }

        public string PostAgents()
        {
            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v12.0/agents";
                ////baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string postData = "{ }";


                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "Failure";
            }
        }
        //Version 15
        public string PutUnavailableCodeByID(int unavailableCodeId)
        {
            string Unavailablecode = @"{""unavailableCodeName"": ""new"",""isActive"": ""true"",""postContact"":""true"",""agentTimeout"":""0""}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/unavailable-codes/" + unavailableCodeId;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = Unavailablecode; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }

        public string getAccessKeys(string agentId, string fields = "", int skip = 1, int top = 100, string orderBy = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/access-keys" +
                     "?agentId=" + agentId + "&fields=" + fields + "&skip=" + skip + "&top=" + top + "&orderBy=" + orderBy;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }

        public string postAccessKeys(int agentId)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/V15.0/access-keys";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"agentId\":\"" + agentId + "\"}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }

        public string deleteAccessKeyByID(string accesskeyId)
        {

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/v15.0/access-keys/" + accesskeyId;
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "DELETE";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }

        public string getAccessKeysByID(string accesskeyId)
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/access-keys/" + accesskeyId;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }

        public string patchAccessKeysByID(string accesskeyId, bool isActive)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/v15.0/access-keys/" + accesskeyId;
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"isActive\":\"" + isActive + "\"}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PATCH";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }

        public string PostHoursofOperation()
        {
            string HoursofOperation = @"{""profileName"": ""new""}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/hours-of-operation";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = HoursofOperation; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }

        public string putHoursofOperationByID(int profileId)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/v15.0/hours-of-operation/" + profileId;
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }

        public string PostPointofContact()
        {
            string PointofContact = @"{""pointOfContact"": ""new"",""pointOfContactName"":""sample"",""skillId"":""0"",""isActive"":""true"",""mediaTypeId"":""0"",""scriptName"":""samp"",""ivrReportingEnabled"":""true""}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/points-of-contact";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = PointofContact; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }

        public string PutPointofContactByID(int pointOfContactId)
        {
            string PointofContactByID = @"{""pointOfContactName"":""sample"",""skillId"":""0"",""isActive"":""true"",""scriptName"":""samp"",""ivrReportingEnabled"":""true""}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/points-of-contact" + pointOfContactId;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = PointofContactByID; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }

        public string PostUnavailableCodes()
        {
            string UnavailableCodes = @"{""unavailableCodeName"": ""new"",""isActive"":""true"",""postContact"":""true""}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/unavailable-codes";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = UnavailableCodes; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }

        public string getPhoneNumbers(string searchString, int skip = 1, int top = 100)
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/phone-numbers" +
                     "?searchString=" + searchString + "&skip=" + skip + "&top=" + top;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }
        public string PostCampaigns()
        {
            string Campaigns = @"{""campaigns"": [{""campaignName"": ""newTeam"",""isActive"": ""true"",""description"":""newcamp"",""notes"":""""}]}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/campaigns";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = Campaigns; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }

        public string PutCampaignsByID(int campaignId)
        {
            string CampaignsByID = @"{""campaigns"": [{""campaignName"": ""newTeam"",""isActive"": ""true"",""description"":""newcamp"",""notes"":""""}]}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/campaigns" + campaignId;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = CampaignsByID; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }

        public string getDispositionByskill(string updatedSince, bool isActive, string searchString = "", string fields = "", int skip = 1, int top = 100, string orderBy = "")
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/dispositions/skills" +
                     "?searchString=" + searchString + "&fields=" + fields + "&skip=" + skip + "&top=" + top + "&orderBy=" + orderBy + "&updatedSince=" + updatedSince + "&isActive=" + isActive;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }
        public string DeleteCampaignBySkill(int campaignId)
        {
            string CampaignBySkill = @"{""skills"": [{""skillId"": ""0"",""transferCampaignId"": ""0""}]}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/campaigns/" + campaignId + "/skills";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "DELETE";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = CampaignBySkill; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }

        public string PostCampaignBySkill(int campaignId)
        {
            string PostCampaignBySkill = @"{""skills"": [{""skillId"": ""0""}]}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/campaigns/" + campaignId + "/skills";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = PostCampaignBySkill; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }

        public string getSmsHistoricalTranscript(int contactId, int businessUnitId)
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/contacts/" + contactId + "/sms-historical-transcript" +
                     "?businessUnitId=" + businessUnitId;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }

        public string getSmsHistoricalContacts(int skillId, int businessUnitId, int ani)
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/contacts/sms-historical-contacts" + "?businessUnitId=" + businessUnitId + "&skillId=" + skillId + "&ani=" + ani;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }

        public string postEmailSaveDraft(String SessionID, int ContactID)
        {
            string PostEmailSaveDraft = @"{""toAddress"": "" "",""fromAddress"": "" "",""bccAddress"": "" "",""campaignId"": ""0"",""subject"": "" "",""bodyHtml"": ""0"",
""attachments"": "" "",""attachmentNames"": "" "",""draftEmailGuidStr"": "" "",""primaryDispositionId"": ""0"",""secondaryDispositionId"": ""0"",""tags"": "" "",
""notes"": "" "",""originalAttachmentNames"": "" ""}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/agent-sessions/" + SessionID + "/interactions/" + ContactID + "/email-save-draft";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = PostEmailSaveDraft; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string postAddText(String SessionID)
        {
            string postAddText = @"{""mediaType"": ""0""}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/agent-sessions/" + SessionID + "/interactions/add-text";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = postAddText; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string putUnavailableCodebyIDTeams(String UnavailableCodeID)
        {
            string putUnavailableCodebyIDTeams = @"{""SecurityUser"": "" "",{""teams"": [{""teamId"": ""string""}]}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v15.0/unavailable-codes/" + UnavailableCodeID + "/teams";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = putUnavailableCodebyIDTeams; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }

        public string postjobsync()
        {
            string postjobsync = @"{""entityName"": ""qm-workflows"",""version"":""1"",""startDate"":""2019-07-10"", ""endDate"":""2019-07-13""}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = CXone_baseURI + "data-extraction/v1/jobs/sync";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = postjobsync; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string GetMediaplaybackByID(string contactID, string mediaType, Boolean excludeWaveforms)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = CXone_baseURI + "media-playback/v1/contacts/" + contactID + "?mediaType=" + mediaType + "?excludeWaveforms=" + excludeWaveforms;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "Failure";
            }
            return "";
        }
        public string GetMediaplaybackBySegmentID(string contactID, string segmentID, string mediaType, Boolean excludeWaveforms)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = CXone_baseURI + "media-playback/v1/contacts/" + contactID + "/segments/" + segmentID + "?mediaType=" + mediaType + "?excludeWaveforms=" + excludeWaveforms;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "Failure";
            }
            return "";
        }
        public string GetMediaplaybackContacts(string acdCallID, string mediaType, Boolean excludeWaveforms)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = CXone_baseURI + "media-playback/v1/contacts/" + acdCallID + "?mediaType=" + mediaType + "?excludeWaveforms=" + excludeWaveforms;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "Failure";
            }
            return "";
        }
        public string GetActiveFlag(string activeFlag)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/v16.0/workflow-data/list/" + activeFlag;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "Failure";
            }
            return "";
        }

        public string postWorkflowData()
        {
            string postWorkflowData = @"{""profile"": [{""ProfileName"": ""new"",""Description"": ""new1"",""ProfileID"": ""0""},""data"":{""Value"": ""vary"",""Visible"": ""1"",""Type"": "" "",""Ref"": "" "" }]""}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v16.0/workflow-data";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = postWorkflowData; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string GetWorkflowById(string workflowDataId)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/v16.0/workflow-data/" + workflowDataId;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "Failure";
            }
            return "";
        }
        public string PutWorkflowById(string workflowDataId)
        {
            string putworkflowDataId = @"{""profile"": [{""ProfileName"": ""new"",""Description"": ""new1"",""ProfileID"": ""0""},""data"":{""Value"": ""vary"",""Visible"": ""1"",""Type"": "" "",""Ref"": "" "" }]""}";
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v16.0/workflow-data/" + workflowDataId;
                string endpoint = baseURL + apiURL;
                //Build POST data as a JSON string
                string postData = putworkflowDataId;//Create the JSON data for cpa management object
                                                    //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string PutWorkflowByIdActivate(string workflowDataId)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v16.0/workflow-data/" + workflowDataId + "/activate";
                string endpoint = baseURL + apiURL;
                string postData = " ";
                //Build POST data as a JSON string
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string PutWorkflowByIdDeactivate(string workflowDataId)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v16.0/workflow-data/" + workflowDataId + "/deactivate";
                string endpoint = baseURL + apiURL;
                string postData = " ";
                //Build POST data as a JSON string
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }

        public string getJobs()
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = CXone_baseURI + "data-extraction/v1/jobs";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }

        public string postjobs()
        {
            string postjobs = @"{""entityName"": ""qm-workflows"",""version"":""1"",""startDate"":""2019-07-10"", ""endDate"":""2019-07-13""}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = CXone_baseURI + "data-extraction/v1/jobs";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = postjobs; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string getJobsByID(string jobId)
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = CXone_baseURI + "data-extraction/v1/jobs/" + jobId;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }
        protected void deleteSuppressedContactBySuppressedContactId(int suppressedContactId)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/{version}/suppressed-contact/" + suppressedContactId.ToString();

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    //.NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                }
            }
            else
            {
                //No token - get one or handle error
            }
        }

        protected void getSuppressedContactBySuppressedContactId(int suppressedContactId)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/{version}/suppressed-contact/" + suppressedContactId.ToString();

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    //.NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                }
            }
            else
            {
                //No token - get one or handle error
            }
        }

        public string putSuppressedContactBySuppressedContactId(int suppressedContactId)
        {
            string SuppressedContactBySuppressedContactId = @"{""suppressedContactData"":[{""startDate"": ""2019-12-04T11:54:38.736Z"",""endDate"": ""2019-12-04T11:54:38.736Z"",""Value"": """",""Skill"": """"}]}";
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/{version}/suppressed-contact" + suppressedContactId;
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Create Post Data and add it to the request
                string postData = SuppressedContactBySuppressedContactId; //team JSON data
                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }
            return "";
        }

        public string getContactsByIdHierachy(int contactId)
        {
            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/{version}/contacts/{contactID}/hierarchy";
                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }

        protected void GetScripts(string scriptPath, int scriptId)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/{version}/scripts?scriptPath=" + scriptPath.ToString() +
                        "&scriptId=" + scriptId;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        //Success.  Do something with the response.
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    //.NET will throw a System.Net.webException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                }
            }
            else
            {
                //No token - get one or handle error
            }
        }
        protected void postScript(string scriptPath, string body)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/{version}/scripts/";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string
                //Start Date and Parameters are not required, at this time skillId IS required.
                //If you are sending paramters into your script, you will use a pipe separated string of
                //parameter values, for example "value1|value2|value3".  The parameters will be available
                //to the script as p1=value1, p2=value2, p3=value3.
                string postData = "{\"scriptPath\":\"" + scriptPath.ToString() + "\", \"body\":\"" + body.ToString();
                //If you do not have a Start Date or Parameters, you can leave either or both out.

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                }
            }
            else
            {
                //No token - get one or handle error
                return;
            }
        }
        protected void updateScripts(string scriptPath = "", bool lockScript = false)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/{version}/scripts";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string               
                string postData = "{\"scriptPath\":\"" + scriptPath +
                  "\",\"bool\":" + lockScript + "}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                }
            }
            else
            {
                //No token - get one or handle error
            }
        }
        protected void postScriptKick(string scriptPath)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/{version}/scripts/kick";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string
                //Start Date and Parameters are not required, at this time skillId IS required.
                //If you are sending paramters into your script, you will use a pipe separated string of
                //parameter values, for example "value1|value2|value3".  The parameters will be available
                //to the script as p1=value1, p2=value2, p3=value3.
                string postData = "{\"scriptPath\":\"" + scriptPath.ToString();
                //If you do not have a Start Date or Parameters, you can leave either or both out.

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                }
            }
            else
            {
                //No token - get one or handle error
            }
        }
        protected void GetScriptsHistoryByName(string scriptPath)
        {
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/{version}/scripts/historyByName/" + scriptPath.ToString();

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        //Success.  Do something with the response.
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    //.NET will throw a System.Net.webException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                }
            }
            else
            {
                //No token - get one or handle error
            }
        }

        public string PutSkillListManagement(int skillId)
        {
            string listManagementJSONData = @"{""displayField1Name"": """",""displayField2Name"": """",""listOrderingOptions"": [{""orderType"": """",""direction"": """"}]}";
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v18.0/skills/" + skillId + "/parameters/list-management";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                   
                string postData = listManagementJSONData;//Create the JSON data for schedule Settings object

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }

        public string getSkillParameters()
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "services/{version}/skills/parameters";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }
        public string PutSkillCadenceSettings(int skillId)
        {
            string CadenceSettingsJSONData = @"""{""attemptMode"":"""",""recordRequestMode"":"""",""destinationRetryRestMinutes"":"""",""maximumAttempts"":[{""fieldName"":"""",""attempts"":0}],""cadences"":[{""fieldName"":"""",""attempts"":0,""timeConstraints"":{""weekdayTimeConstraintsweekdayTimeConstraints"":[{""startTime"":""2019-07-10"",""endTime"":""2019-07-15""}],""weekendTimeConstraints"":[{""startTime"":""2019-07-10"",""endTime"":""2019-07-15""}]}}]}""";
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v18.0/skills/" + skillId + "/parameters/cadence-settings";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                   
                string postData = CadenceSettingsJSONData;//Create the JSON data for schedule Settings object

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }

        public string getSkillSkillIdParameters(int skillId)
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL ="services/{version}/skills/" +skillId+ "/parameters";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }

        public string postSetDisposition(uint contactId)
        {
            string postSetDisposition = @"{""dispositionInfo"":{""Skill"": ""1007"",""DispositionCode"":""1"",""CallbackNumber"":"""", ""CallbackTime"":"""",""notes"":"""",""CommitmentAmount"":""""}}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "services/{version}/contacts/"+contactId+"/set-disposition";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = postSetDisposition; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string getBusinessUnitOutboundRoutes()
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "services/{version}/business-unit/outbound-routes";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }
        //V19
        public string getClientData()
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "services/{version}/agents/client-data";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }

        public string PutClientData()
        {
            string ClientdataJSONData = @"""{""agentId"":"""",""dataset"":""""}""";
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v18.0/agents/client-data";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                   
                string postData = ClientdataJSONData;//Create the JSON data for schedule Settings object

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }

        public string Putpersistentcontacts(int contactId)
        {
            string CadenceSettingsJSONData = @"""{""agentId"":"""",""dataset"":""""}""";
            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "/services/v18.0/persistent-contacts/"+ contactId;
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                   
                string postData = CadenceSettingsJSONData;//Create the JSON data for schedule Settings object

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "PUT";
                request.ContentType = "application/json";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                }
                catch (Exception ex)
                {
                    //Non Web Exception error, handle error
                    return ex.Message;
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string getAgentSettings(int agentId)
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "services/{version}/agents/" + agentId + "/agent-settings";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }

        public string postSmsOutbound(string sessionId)
        {
            string postSmsOutbound = @"{""phoneNumber"": ""1234567890"",""skillId"":"""",""parentContactId"":""""}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "services/{version}/agent-sessions/" + sessionId + "/interactions/sms-outbound";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = postSmsOutbound; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }
        public string getScriptsById(int scriptId)
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "services/{version}/scripts/" + scriptId;

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }

        public string getScriptsSearch()
        {

            //Test to see if you have obtained a token            
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "services/{version}/scripts/search";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                // Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        return response.StatusDescription;
                    }
                }
                catch (System.Net.WebException webException)
                {

                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                    return webException.Message;
                }
                catch (Exception ex)
                {
                    // Catch any non Web Exception error
                    return ex.Message;
                }
            }
            else
            {
                // No token - get one or handle error
                return "Failure";
            }
        }

        public string posttransformnumbers(int agentpatternId)
        {
            string posttransformnumbers = @"{""inputPhoneNumbers"":{""inputPhoneNum"":"""",""externalId"":""""}}";

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {
                string apiURL = "services/{version}/agent-patterns/" + agentpatternId + "/transform-phonenumbers";

                //baseURL was returned with your successful token request
                string endpoint = baseURL + apiURL;
                string authorizationHeader = "Authorization:bearer " + accessToken;

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Create Post Data and add it to the request
                string postData = posttransformnumbers; //team JSON data

                var encoding = new System.Text.UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                //Make the request
                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        string responseBody = "";
                        int statusCode = (int)response.StatusCode;
                        string statusDescription = response.StatusDescription;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new System.IO.StreamReader(responseStream))
                                {
                                    responseBody = reader.ReadToEnd();
                                    return response.StatusDescription;
                                    //responseBody now contains the JSON string of the response.
                                    //save and parse it as you see fit.
                                }
                            }
                        }
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return (((System.Net.HttpWebResponse)webException.Response).StatusDescription);
                    // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //Non Web Exception error, handle error       
                }
            }
            else
            {
                //No token - get one or handle error
                return "No token";
            }

            return "";
        }

        public string deleteremoveprospects(string sourceName)
        {

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/lists/call-lists/" + sourceName + "/removeProspects";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{\"forceInactive\":{\"" +
                    "\",\"externalId\":\""  + "\"}}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "DELETE";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }

        public string deletescripts()
        {

            //Test to see if you have obtained a token
            if (!string.IsNullOrEmpty(accessToken))
            {

                string apiURL = "/services/" + version + "/scripts";
                string endpoint = baseURL + apiURL;

                //Build POST data as a JSON string                    

                string postData = "{}";

                //Create the request object
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
                request.Method = "DELETE";
                request.ContentType = "application/json";

                //Add any and all necessary headers
                request.Headers.Add("Authorization", "bearer " + accessToken);
                //Accept is a reserved header, so you must modify it rather than add
                request.Accept = "application/json, text/javascript, */*; q=0.01";

                //Write the PostData into the Request Body
                byte[] postDataBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(postData.ToLower());
                request.ContentLength = postDataBytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //Make the request
                try
                {
                    // Get the Response from the Web Request
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        // Success do something with the response here.
                        return response.StatusDescription; ;
                    }
                }
                catch (System.Net.WebException webException)
                {
                    return webException.Message; // .NET will throw a System.Net.WebException on StatusCodes other than 200.  
                    // You can catch that exception and retain the StatusCode for error handling.
                }
                catch (Exception ex)
                {
                    return ex.Message; //Non Web Exception error, handle error
                }
            }
            else
            {
                return "Failure";  //No token - get one or handle error
            }
        }

    }
}
    