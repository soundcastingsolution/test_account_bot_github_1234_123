using System;
using System.Web;
using InContact.DeveloperPortal.Web.Data;
using InContact.DeveloperPortal.Web.Logic;
using InContact.DeveloperPortal.Web.Models;
using Moq;
using NUnit.Framework;

namespace DeveloperPortal.Tests.Logic
{
    [TestFixture]
    public class AccountLogicTests
    {
        [Test]
        public void AccountLogic_Authenticate_ReturnsValidAuthenticationResult()
        {
            var viewModel = new SigninViewModel
            {
                ApplicationName = "Application Name",
                VendorName = "Vendor Name",
                BusinessUnitNumber = 123456,
                Password = "Password",
                UserName = "UserName"
            };

            var mockInContactCentralApiData = new Mock<IInContactCentralApiData>();
            mockInContactCentralApiData.Setup(x => x.Authenticate(viewModel)).Returns(new InContactCentralAuthenticationResult());
            var mockFedrampApiData = new Mock<IInContactCentralApiData>();
            mockFedrampApiData.Setup(x => x.Authenticate(viewModel)).Returns(new InContactCentralAuthenticationResult());

            var accountLogic = new AccountLogic(mockInContactCentralApiData.Object, mockFedrampApiData.Object, null, null);
            var result = accountLogic.Authenticate(viewModel);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void AccountLogic_Authenticate_CallsInContactCentralAuthenticate()
        {
            var viewModel = new SigninViewModel
            {
                ApplicationName = "Application Name",
                VendorName = "Vendor Name",
                BusinessUnitNumber = 123456,
                Password = "Password",
                UserName = "UserName"
            };

            var mockInContactCentralApiData = new Mock<IInContactCentralApiData>();
            mockInContactCentralApiData.Setup(x => x.Authenticate(viewModel)).Returns(new InContactCentralAuthenticationResult());
            var mockFedrampApiData = new Mock<IInContactCentralApiData>();
            mockFedrampApiData.Setup(x => x.Authenticate(viewModel)).Returns(new InContactCentralAuthenticationResult());

            var accountLogic = new AccountLogic(mockInContactCentralApiData.Object, mockFedrampApiData.Object, null, null);

            var result = accountLogic.Authenticate(viewModel);

            mockInContactCentralApiData.Verify(x => x.Authenticate(viewModel), Times.Once);
        }

        [Test]
        public void AccountLogic_Authenticate_ParsesSuccessfulAuthenticationResultProperly()
        {
            var viewModel = new SigninViewModel
            {
                ApplicationName = "Application Name",
                VendorName = "Vendor Name",
                BusinessUnitNumber = 123456,
                Password = "Password",
                UserName = "UserName"
            };

            var centralAuthResult = new InContactCentralAuthenticationResult
            {
                access_token = "accessToken",
                token_type = "TokenType",
                expires_in = 3600,
                refresh_token = "refreshToken",
                scope = "scope",
                resource_server_base_uri = "resourceUri",
                refresh_token_server_uri = "refreshUri",
                agent_id = 123456789,
                team_id = 23456789,
                bus_no = 3456789
            };

            var expectedToken = new DeveloperAuthenticationToken
                {
                    AgentId = centralAuthResult.agent_id,
                    AccessToken = centralAuthResult.access_token,
                    BusinessUnitNumber = centralAuthResult.bus_no,
                    ExpiresIn = centralAuthResult.expires_in,
                    RefreshToken = centralAuthResult.refresh_token,
                    RefreshTokenServerUri = centralAuthResult.refresh_token_server_uri,
                    ResourceServerBaseUri = centralAuthResult.resource_server_base_uri,
                    Scope = centralAuthResult.scope,
                    TeamId = centralAuthResult.team_id,
                    TokenType = centralAuthResult.token_type,
                    TokenRetrievalDateTimeUTC = DateTime.UtcNow,
                    UserName = viewModel.UserName
                };

            var mockInContactCentralApiData = new Mock<IInContactCentralApiData>();

            mockInContactCentralApiData.Setup(x => x.Authenticate(viewModel)).Returns(centralAuthResult);
            var mockFedrampApiData = new Mock<IInContactCentralApiData>();
            mockFedrampApiData.Setup(x => x.Authenticate(viewModel)).Returns(new InContactCentralAuthenticationResult());

            var accountLogic = new AccountLogic(mockInContactCentralApiData.Object, mockFedrampApiData.Object, null, null);

            var actual = accountLogic.Authenticate(viewModel);

            Assert.That(actual.IsAuthenticated);
            Assert.That(actual.Message, Is.Null);
            Assert.That(Utilities.AreObjectsEquivalent(actual.Token, expectedToken));
        }

        [Test]
        public void AccountLogic_Authenticate_ParsesFailedAuthenticationResultProperly()
        {
            var viewModel = new SigninViewModel
            {
                ApplicationName = "Application Name",
                VendorName = "Vendor Name",
                BusinessUnitNumber = 123456,
                Password = "Password",
                UserName = "UserName"
            };

            var centralAuthResult = new InContactCentralAuthenticationResult
            {
                error = "NOT_Authorized",
                error_description = "Login Failed"
            };


            var mockInContactCentralApiData = new Mock<IInContactCentralApiData>();
            var mockFedrampApiData = new Mock<IInContactCentralApiData>();

            mockInContactCentralApiData.Setup(x => x.Authenticate(viewModel)).Returns(centralAuthResult);
            mockFedrampApiData.Setup(x => x.Authenticate(viewModel)).Returns(centralAuthResult);

            var accountLogic = new AccountLogic(mockInContactCentralApiData.Object, mockFedrampApiData.Object, null, null);

            var actual = accountLogic.Authenticate(viewModel);

            Assert.That(actual.IsAuthenticated, Is.False);
            Assert.That(actual.Message, Is.EquivalentTo("Login Failed"));
        }

        [Test]
        public void AccountLogic_RefreshToken_HappyPath()
        {
            var originalToken = new DeveloperAuthenticationToken
            {
                AgentId = 123456789,
                AccessToken = "AccessToken",
                AuthorizationToken = "AuthorizationToken",
                BusinessUnitNumber = 3456789,
                ExpiresIn = 3600,
                RefreshToken = "RefreshToken",
                RefreshTokenServerUri = "RefreshUri",
                ResourceServerBaseUri = "ResourceUri",
                Scope = "Scope",
                TeamId = 23456789,
                TokenType = "TokenType",
                TokenRetrievalDateTimeUTC = DateTime.UtcNow,
                UserName = "username"
            };

            var centralRefreshResult = new InContactCentralAuthenticationResult
            {
                access_token = "NewAccessToken",
                token_type = "NewTokenType",
                expires_in = 3601,
                refresh_token = "NewRefreshToken",
                scope = "NewScope",
                resource_server_base_uri = "NewResourceUri",
                refresh_token_server_uri = "NewRefreshUri",
                agent_id = 1234567890,
                team_id = 234567890,
                bus_no = 34567890
            };

            var expectedToken = new DeveloperAuthenticationToken
            {
                AgentId = centralRefreshResult.agent_id,
                AccessToken = centralRefreshResult.access_token,
                BusinessUnitNumber = centralRefreshResult.bus_no,
                ExpiresIn = centralRefreshResult.expires_in,
                RefreshToken = centralRefreshResult.refresh_token,
                RefreshTokenServerUri = centralRefreshResult.refresh_token_server_uri,
                ResourceServerBaseUri = centralRefreshResult.resource_server_base_uri,
                Scope = centralRefreshResult.scope,
                TeamId = centralRefreshResult.team_id,
                TokenType = centralRefreshResult.token_type,
                TokenRetrievalDateTimeUTC = DateTime.UtcNow,
                UserName = originalToken.UserName,
                AuthorizationToken = originalToken.AuthorizationToken
            };

            var mockInContactCentralApiData = new Mock<IInContactCentralApiData>();
            
            var mockFedrampApiData = new Mock<IInContactCentralApiData>();
            var mockRequest = new Mock<HttpRequestBase>();
            var mockResponse = new Mock<HttpResponseBase>();

            mockRequest.Setup(x => x.Cookies).Returns(new HttpCookieCollection { new HttpCookie("zDevToken", originalToken.ToString()) });

            var responseCookies = new HttpCookieCollection();

            mockResponse.Setup(x => x.Cookies).Returns(responseCookies);

            mockInContactCentralApiData.Setup(x => x.RefreshToken("AuthorizationToken", "RefreshToken", "RefreshUri")).Returns(centralRefreshResult);
            mockFedrampApiData.Setup(x => x.RefreshToken("AuthorizationToken", "RefreshToken", "RefreshUri")).Returns(centralRefreshResult);

            var accountLogic = new AccountLogic(mockInContactCentralApiData.Object, mockFedrampApiData.Object, mockResponse.Object, mockRequest.Object);

            var actual = accountLogic.RefreshToken();

            mockInContactCentralApiData.Verify(x => x.RefreshToken("AuthorizationToken", "RefreshToken", "RefreshUri"), Times.Once());
            Assert.That(responseCookies["zDevToken"], Is.Not.Null);
            Assert.That(Utilities.AreObjectsEquivalent(actual, expectedToken));
            Assert.That(Utilities.AreObjectsEquivalent(new DeveloperAuthenticationToken(responseCookies["zDevToken"].Value), actual));
        }
    }
}
