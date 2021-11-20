using InContact.DeveloperPortal.Web.Models;

namespace InContact.DeveloperPortal.Web.Data
{
    public interface IInContactCentralApiData
    {
        InContactCentralAuthenticationResult Authenticate(SigninViewModel loginData);
        InContactCentralAuthenticationResult Authenticate(string endPoint, SigninViewModel loginData);
        InContactCentralAuthenticationResult RefreshToken(string authToken, string refreshToken, string refreshTokenServerUri);
    }
}
