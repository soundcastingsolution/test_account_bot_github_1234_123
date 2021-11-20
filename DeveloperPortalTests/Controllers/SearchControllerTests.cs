using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InContact.DeveloperPortal.Web.Common;
using InContact.DeveloperPortal.Web.Logic;
using InContact.DeveloperPortal.Web.Models;
using InContact.DeveloperPortal.Web.Controllers;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace DeveloperPortal.Tests.Controllers
{

    [TestFixture()]
    public class SearchControllerTests
    {

        private SearchController controller;
        private Mock<ISearchLogic> logic;
        private SearchModel submittedModel;




        [SetUp()]
        public void Setup()
        {
            logic = new Mock<ISearchLogic>();
            SearchModel searchReturnSet = new SearchModel()
            {
                SearchTerm = "api",
                Results = new List<SearchResult>(){
                    new SearchResult(){ Title= "Search Result 1 Title", Link="http://www.incontact.com/1", Blerb= "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 1"},
                    new SearchResult(){ Title= "Search Result 2 Title", Link="http://www.incontact.com/1", Blerb= "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 2"},
                    new SearchResult(){ Title= "Search Result 3 Title", Link="http://www.incontact.com/1", Blerb= "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. 3"}
                }
            };
            logic.Setup(x => x.GetSearchResults(It.IsAny<string>())).Returns(searchReturnSet).Verifiable();


            controller = new SearchController(logic.Object);
            submittedModel = new SearchModel()
            {
                SearchTerm = "api"
            };


        }

        [Test()]
        public void SearchController_Index_Get_Returns_IndexView()
        {
            //Arrange
            var expectedViewName = "Index";

            //Act

            var result = controller.Index("") as ViewResult;
            var model = (SearchModel)result.ViewData.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedViewName, result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            Assert.AreEqual(model.SearchTerm, "");
            Assert.AreEqual(model.Results.Count, 0);


        }
    }
}
