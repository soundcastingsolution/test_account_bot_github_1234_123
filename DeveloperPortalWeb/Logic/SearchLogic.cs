using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InContact.DeveloperPortal.Web.Models;
using InContact.DeveloperPortal.Web.Common.Services;
using InContact.Common.Extensions;
using System.Text;
using System.Text.RegularExpressions;

namespace InContact.DeveloperPortal.Web.Logic
{
    public interface ISearchLogic
    {
        SearchModel GetSearchResults(string searchTerm);
    }


    public class SearchLogic : ISearchLogic
    {
        private ILuceneSearchDevClient _searchService;
        const string _NoTitleText = "[No Title]";

        public SearchLogic(ILuceneSearchDevClient searchClient)
        {
            
            _searchService = searchClient;
        }


        public SearchModel GetSearchResults(string searchTerm)
        {
            searchTerm = Regex.Replace(searchTerm, @"[+\-&|!(){}[\]^""~*?:\\]", "\\$0");

            var searchResults = _searchService.GetSearchData(searchTerm);
            var result = new SearchModel()
            {                
                SearchTerm = searchTerm
            };
            var rnd = new Random();
            foreach (var searchResult in searchResults)
            {
                result.Results.Add(new SearchResult() {
                    Blerb = FormatBlerbForSearch(searchResult.Blerb),
                    Link = searchResult.Link,
                    Title = (searchResult.Title != null) ? searchResult.Title : _NoTitleText,
                    Location = GetLocation(searchResult.Link) 
                });
            }

            return result;
        }

        private SearchResultType GetLocation(string url)
        {
            if (url.ToLower().StartsWith("api/"))
            {
                return SearchResultType.API;
            }
            else if (url.ToLower().Contains("forum/"))
            {
                return SearchResultType.Forum;
            }
            else if (url.ToLower().StartsWith("downloads/"))
            {
                return SearchResultType.Downloads;
            }
            else if (url.ToLower().StartsWith("faq/"))
            {
                return SearchResultType.FAQ;
            }
            else
            {
                return SearchResultType.Documentation;
            }
        }	       

        private string FormatBlerbForSearch(string originalBlerb)
        {
            StringBuilder result = new StringBuilder();

            var allWords = originalBlerb.Split().Take(50).ToList();

            foreach(var word in allWords)
            {
                result.Append(word + " ");
            }

            return result.ToString();
        }


        private List<Models.SearchResult> GetSearchStubData()
        {
            var searchResults = new List<Models.SearchResult>();
            var rnd = new Random();

            for (int i = 0; i < 25; i++)
            {
                searchResults.Add(new Models.SearchResult()
                {
                    Title = String.Format("Search Result {0}", i),
                    Location = (SearchResultType)rnd.Next(1, 4),
                    LastUpdated = DateTime.Now.AddDays(-(new Random()).Next(1, 365)).AddHours(-(rnd).Next(1, 24)),
                    Link =@"https://lab-developer.blfdev.lab/Documentation/" + i.ToString(),
                    Blerb = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. "
                });
            }

            return searchResults;
        }
    }
}