using System;
using System.Web;
using System.Web.Security;
using InContact.DeveloperPortal.Web.Models;
using InContact.Common.Extensions;
using Newtonsoft.Json;

namespace InContact.DeveloperPortal.Web.Logic
{
    public interface IAccountLogic
    {
        void ClearCookie();
        void SetCookie(DeveloperAuthenticationToken token);
      
    }


    public class AccountLogic : IAccountLogic
    {       
        private readonly HttpResponseBase _response;
        private readonly HttpRequestBase _request;

        public AccountLogic(HttpResponseBase response, HttpRequestBase request)
        {
            _response = response;
            _request = request;
        }
        

        public void ClearCookie()
        {
            var expiredCookie = new HttpCookie("zDevToken")
            {
                Expires = DateTime.Today.AddDays(-1)
            };
            _response.Cookies.Add(expiredCookie);
            FormsAuthentication.SignOut();
        }

        public void SetCookie(DeveloperAuthenticationToken token)
        {
            if (string.IsNullOrWhiteSpace(token.UserName))
                throw new ArgumentException("token.UserName");

            SetDevTokenCookie(token);
            FormsAuthentication.SetAuthCookie(token.UserName, false);
        }
        

        private void SetDevTokenCookie(DeveloperAuthenticationToken token)
        {
            var cookie = new HttpCookie("zDevToken")
            {
                Value = token.ToString(),

                //Add a bit of time to the cookie expiration to ensure the forms auth cookie expires first
                Expires = DateTime.Now.AddSeconds(FormsAuthentication.Timeout.TotalSeconds + 10)
            };

            _response.Cookies.Add(cookie);
        }
        
    }
}