using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InContact.DeveloperPortal.Web.Models
{
    public class SearchResult
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public SearchResultType Location { get; set; }
        public string Blerb { get; set; }
        public DateTime LastUpdated { get; set; }

    }

    public enum SearchResultType
    {
        Documentation = 1,
        Forum = 2,
        API = 3,
        FAQ = 4,
        Downloads = 5
    }
}
