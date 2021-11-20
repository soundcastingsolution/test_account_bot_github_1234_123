using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InContact.DeveloperPortal.Web.Models
{
    public class SearchModel
    {

        public string SearchTerm { get; set; }

        public List<SearchResult> Results {get; set;}

        /// <summary>
        /// Initializes a new instance of the SearchModel class.
        /// </summary>
        /// <param name="new"></param>
        public SearchModel()
        {
            Results = new List<SearchResult>();
            SearchTerm = String.Empty;
        }
    }
}