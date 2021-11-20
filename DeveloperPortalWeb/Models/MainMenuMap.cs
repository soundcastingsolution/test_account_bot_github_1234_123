using System.Collections.Generic;

namespace InContact.DeveloperPortal.Web.Models
{
    public class MainMenuMap
    {
        public List<MainMenuSection> Sections { get; set; }

        public MainMenuMap()
        {
            Sections = new List<MainMenuSection>();
        }
    }
}