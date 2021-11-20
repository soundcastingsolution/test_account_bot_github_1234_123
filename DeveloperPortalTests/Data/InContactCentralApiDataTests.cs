using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InContact.DeveloperPortal.Web.Data;
using NUnit.Framework;
using InContact.DeveloperPortal.Web.Models;
namespace InContact.DeveloperPortal.Web.Data.Tests
{
    [TestFixture()]
    public class InContactCentralApiDataTests
    {
        [Test()]
        [Explicit("Integration")]
        public void InContactCentralApiData_RefreshToken_ReturnsValidTokenData()
        {
            //arrange
            string endpoint = @"https://home-sc1.ucnlabext.com/InContactAuthorizationServer/Token";//https://home-tc4.ucnlabext.com/InContactAuthorizationServer/Token
            string defaultApplicationName = @"DeveloperPortal";
            string defaultVendorName = @"NICEinContact Inc.";
            string defaultBusinessUnitNumber = @"QzZFRDlENTUwRUFGNDZBMUI5RkIyMzlCRDk4NjI5N0M=";
            InContactCentralApiData sut = new InContactCentralApiData(endpoint, defaultApplicationName, defaultVendorName, defaultBusinessUnitNumber);

            var viewModel = new SigninViewModel
            {
                ApplicationName = "Dev",
                VendorName = "CorpIT",
                BusinessUnitNumber = 4500,
                UserName = "itdevelopers@sc1.com",
                Password = "ProductSite1"
            };
            var result = sut.Authenticate(viewModel);

            //act
            var actual = sut.RefreshToken(result.auth_token, result.refresh_token, result.refresh_token_server_uri);

            //assert
            Assert.AreEqual(viewModel.BusinessUnitNumber, actual.bus_no);
        }


        [Test()]
        [Explicit("Integration")]
        public void InContactCentralApiData_RefreshToken_ReturnsValidTokenData_Without_AN_VN_BU()
        {
            //arrange
            string endpoint = @"https://home-sc1.ucnlabext.com/InContactAuthorizationServer/Token";//https://home-tc4.ucnlabext.com/InContactAuthorizationServer/Token
            string defaultApplicationName = @"DeveloperPortal";
            string defaultVendorName = @"NICEinContact Inc.";
            string defaultBusinessUnitNumber = @"QzZFRDlENTUwRUFGNDZBMUI5RkIyMzlCRDk4NjI5N0M=";
            InContactCentralApiData sut = new InContactCentralApiData(endpoint, defaultApplicationName, defaultVendorName, defaultBusinessUnitNumber);

            var viewModel = new SigninViewModel
            {
                UserName = "itdevelopers@sc1.com",
                Password = "ProductSite1"
            };
            var result = sut.Authenticate(viewModel);

            //act
            var actual = sut.RefreshToken(result.auth_token, result.refresh_token, result.refresh_token_server_uri);

            //assert
            Assert.AreEqual(4500, actual.bus_no);
        }
    }
}
