using InContact.DeveloperPortal.Web.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InContact.DeveloperPortal.Web.Models;
using Newtonsoft.Json;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace InContact.DeveloperPortal.Web.Controllers
{

    public class SearchController : DeveloperPortalControllerBase
    {

        private readonly ISearchLogic _logic;
        public SearchController(ISearchLogic logic)
        {
            _logic = logic;
        }


        [HttpGet]
        public ViewResult Index(string searchTerm)
        {
            var model = new SearchModel();
            model.SearchTerm = searchTerm;

            BreadcrumbTrail.AddNode("Search");
            return View("Index", model);
        }


        public ActionResult Results(string searchTerm)
        {
            if ((searchTerm != null) && (searchTerm != string.Empty))
            {
                var model = _logic.GetSearchResults(searchTerm);
                return Json(model.Results.ToDataSourceResult(new DataSourceRequest()));
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

    }
}