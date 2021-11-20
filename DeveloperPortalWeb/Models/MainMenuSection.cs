using System.Collections.Generic;

namespace InContact.DeveloperPortal.Web.Models
{
    public class MainMenuSection
    {
        private string title;

        public string Title
        {
            get
            {
                if (string.IsNullOrWhiteSpace(title))
                    return title;
                else
                    return title.Trim();
            }
            set
            {
                title = value;
            }
        }

        public List<MenuMvcLink> MvcLinks { get; set; }

        public MainMenuSection()
        {
            MvcLinks = new List<MenuMvcLink>();
        }
    }
}
