using System.Collections.Generic;
using System.Web.Routing;

namespace InContact.DeveloperPortal.Web.Models
{
    public class MainMenuItem
    {
        public string LinkText { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public RouteValueDictionary RouteData { get; set; }
        public IDictionary<string, object> HtmlAttributes { get; set; }
        public List<MainMenuItem> SubMenu { get; set; }
        public string SectionTitle { get; set; }

        public MainMenuItem()
        {
            RouteData = new RouteValueDictionary();
            HtmlAttributes = new Dictionary<string, object>();
            SubMenu = new List<MainMenuItem>();
        }
    }
}