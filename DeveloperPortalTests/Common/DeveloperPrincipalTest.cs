using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using NUnit.Framework;
using InContact.DeveloperPortal.Web.Common;
using InContact.DeveloperPortal.Web.Models;
namespace InContact.DeveloperPortal.Web.Common.Tests
{
    [TestFixture()]
    public class DeveloperPrincipalTest
    {
        [Test()]
        public void DeveloperPrincipal_AddDeveloperTokenInformation_FillsDeveloperPrincipal()
        {
            //Arrange
            DeveloperAuthenticationToken inputToken = new DeveloperAuthenticationToken 
            { 
                AccessToken = "AccessToken",
                AgentId = 88888,
                AuthorizationToken = "AuthToken",
                BusinessUnitNumber = 12345,
                ExpiresIn = 678,
                RefreshToken = "RefreshToken",
                RefreshTokenServerUri = "RefreshTokenServerUri",
                ResourceServerBaseUri = "ResourceServerBaseUri",
                Scope = "Scope",
                TeamId = 90,
                TokenRetrievalDateTimeUTC = DateTime.UtcNow,
                TokenType = "TokenType",
                UserName = "UserName"
            };

            //Act
            DeveloperPrincipal sut = new DeveloperPrincipal(null);
            sut.AddDeveloperTokenInformation(inputToken);

            //Assert
            Assert.AreEqual(inputToken.AccessToken, sut.AccessToken, "AccessToken not as expected");
            Assert.AreEqual(inputToken.AgentId, sut.AgentId, "AgentId not as expected");
            Assert.AreEqual(inputToken.BusinessUnitNumber, sut.BusinessUnitNumber, "BusinessUnitNumber not as expected");
            Assert.AreEqual(inputToken.NeedsRefresh, sut.NeedsRefresh, "NeedsRefresh not as expected");
            Assert.AreEqual(inputToken.ResourceServerBaseUri, sut.AcdResourceServerBaseUri, "ResourceServerBaseUri not as expected");
            Assert.AreEqual(inputToken.TokenType, sut.TokenType, "TokenType not as expected");
            Assert.AreEqual(inputToken.UserName, sut.UserName, "UserName not as expected");
        }
    }
}