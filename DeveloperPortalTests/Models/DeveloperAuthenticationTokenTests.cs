using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using InContact.DeveloperPortal.Web.Models;
using NUnit.Framework;

namespace DeveloperPortal.Tests.Models
{
    [TestFixture]
    public class DeveloperAuthenticationTokenTests
    {
        [Test]
        public void DeveloperAuthenticationToken_ToString_WorksProperly()
        {
            var token = new DeveloperAuthenticationToken
            {
                AgentId = 123456789,
                AccessToken = "accessToken",
                BusinessUnitNumber = 23456789,
                ExpiresIn = 3600,
                RefreshToken = "refreshToken",
                RefreshTokenServerUri = "refresh_token_server_uri",
                ResourceServerBaseUri = "resource_server_base_uri",
                Scope = "scope",
                TeamId = 3456789,
                TokenRetrievalDateTimeUTC = DateTime.Now,
                TokenType = "tokenType",
                UserName = "userName"
            };

            var expected = new JavaScriptSerializer().Serialize(token);

            var actual = token.ToString();

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void DeveloperAuthenticationToken_ConstructerWithSerializedObject_DeserializesCorrectly()
        {

            var inputToken = new DeveloperAuthenticationToken
            {
                AgentId = 123456789,
                AccessToken = "accessToken",
                BusinessUnitNumber = 23456789,
                ExpiresIn = 3600,
                RefreshToken = "refreshToken",
                RefreshTokenServerUri = "refresh_token_server_uri",
                ResourceServerBaseUri = "resource_server_base_uri",
                Scope = "scope",
                TeamId = 3456789,
                TokenRetrievalDateTimeUTC = DateTime.UtcNow,
                TokenType = "tokenType",
                UserName = "userName"
            };

            var serializedObject = inputToken.ToString();

            var actual = new DeveloperAuthenticationToken(serializedObject);

            Assert.That(Utilities.AreObjectsEquivalent(actual, inputToken), "Expected actual object to be identical to serialized object");
            Assert.AreEqual(inputToken.TokenRetrievalDateTimeUTC.ToString(), actual.TokenRetrievalDateTimeUTC.ToString(), "Expected TokenRetrievalDateTime hydrated from original value");
        }

        [Test]
        public void DeveloperAuthenticationToken_NullDate_DeserializesCorrectly()
        {

            var inputToken = new DeveloperAuthenticationToken
            {
                AgentId = 123456789,
                AccessToken = "accessToken",
                BusinessUnitNumber = 23456789,
                ExpiresIn = 3600,
                RefreshToken = "refreshToken",
                RefreshTokenServerUri = "refresh_token_server_uri",
                ResourceServerBaseUri = "resource_server_base_uri",
                Scope = "scope",
                TeamId = 3456789,
                TokenRetrievalDateTimeUTC = null,
                TokenType = "tokenType",
                UserName = "userName"
            };

            var serializedObject = inputToken.ToString();

            var actual = new DeveloperAuthenticationToken(serializedObject);

            Assert.That(Utilities.AreObjectsEquivalent(actual, inputToken), "Expected actual object to be identical to serialized object");
            Assert.IsNull(actual.TokenRetrievalDateTimeUTC, "Expected NULL TokenRetrievalDateTime");
        }

        [Test]
        public void DeveloperAuthenticationToken_ExpirationDateTime_CalculatedProperly()
        {
            int inputExpiresIn = 3600; //1 hour
            DateTime now = DateTime.UtcNow;
            DeveloperAuthenticationToken sut = new DeveloperAuthenticationToken
            {
                ExpiresIn = inputExpiresIn, 
                TokenRetrievalDateTimeUTC = now
            };

            Assert.IsNotNull(sut.ExpirationDateTimeUTC, "Expected an expiration date");
            Assert.IsTrue(sut.ExpirationDateTimeUTC >= now.AddSeconds(inputExpiresIn), "Expected expiration date to be ~ Now + ExpiresIn");
            Assert.AreEqual(inputExpiresIn, sut.ExpiresIn, "Expected ExpiresIn to be set");
        }

        [Test]
        public void DeveloperAuthenticationToken_ExpirationDateTime_Null_WhenTokenRetrievalDateTimeNotSet()
        {
            DeveloperAuthenticationToken sut = new DeveloperAuthenticationToken();

            Assert.IsNull(sut.ExpirationDateTimeUTC, "Did not expect an expiration date");
            Assert.AreEqual(0, sut.ExpiresIn, "Did not expect a value for ExpiresIn");
        }

        [Test]
        public void DeveloperAuthenticationToken_NeedsRefresh_ReturnsFalseWhenTokenIsNew()
        {
            DeveloperAuthenticationToken sut = new DeveloperAuthenticationToken
            {
                ExpiresIn = 3600, //1 hour
                TokenRetrievalDateTimeUTC = DateTime.UtcNow
            };

            Assert.IsFalse(sut.NeedsRefresh, "Did not expect the token to need refreshed");
        }

        [Test]
        public void DeveloperAuthenticationToken_NeedsRefresh_ReturnsFalseWhenRefreshTokenNull()
        {
            DeveloperAuthenticationToken sut = new DeveloperAuthenticationToken
            {
                ExpiresIn = 0,
                TokenRetrievalDateTimeUTC = DateTime.UtcNow
            };

            Assert.IsFalse(sut.NeedsRefresh, "Did not expect the token to need refreshed");
        }

        [Test]
        public void DeveloperAuthenticationToken_NeedsRefresh_ReturnsFalseWhenRefreshTokenEmpty()
        {
            DeveloperAuthenticationToken sut = new DeveloperAuthenticationToken
            {
                ExpiresIn = 0,
                TokenRetrievalDateTimeUTC = DateTime.UtcNow,
                RefreshToken = string.Empty
            };

            Assert.IsFalse(sut.NeedsRefresh, "Did not expect the token to need refreshed");
        }

        [Test]
        public void DeveloperAuthenticationToken_NeedsRefresh_ReturnsFalseWhenRefreshUrlNull()
        {
            DeveloperAuthenticationToken sut = new DeveloperAuthenticationToken
            {
                ExpiresIn = 0,
                TokenRetrievalDateTimeUTC = DateTime.UtcNow,
                RefreshToken = "something"
            };

            Assert.IsFalse(sut.NeedsRefresh, "Did not expect the token to need refreshed");
        }

        [Test]
        public void DeveloperAuthenticationToken_NeedsRefresh_ReturnsFalseWhenRefreshUrlEmpty()
        {
            DeveloperAuthenticationToken sut = new DeveloperAuthenticationToken
            {
                ExpiresIn = 0,
                TokenRetrievalDateTimeUTC = DateTime.UtcNow,
                RefreshToken = "something",
                RefreshTokenServerUri = string.Empty
            };

            Assert.IsFalse(sut.NeedsRefresh, "Did not expect the token to need refreshed");
        }

        [Test]
        public void DeveloperAuthenticationToken_NeedsRefresh_ReturnsTrueWhenExpiresInElapsed()
        {
            DeveloperAuthenticationToken sut = new DeveloperAuthenticationToken
            {
                ExpiresIn = 0,
                TokenRetrievalDateTimeUTC = DateTime.UtcNow,
                RefreshToken = "something",
                RefreshTokenServerUri = "http://something"
            };

            Assert.IsTrue(sut.NeedsRefresh, "Expected the token to need refreshed");
        }
    }
}
