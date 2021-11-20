using System.Collections.Generic;
using System.Web.Routing;
using System.Xml.Serialization;

namespace InContact.DeveloperPortal.Web.Models
{
    public class MenuMvcLink
    {
        // We need to use backing stores because we have to trim the strings to eliminate whitespace from the XML file.
        //
        private string action;
        private string controller;
        private string linkText;
        private string routeDataValues;
        private readonly RouteValueDictionary routeData = new RouteValueDictionary();
        private string htmlAttributeValues;
        private readonly IDictionary<string, object> htmlAttributes = new Dictionary<string, object>();

        public string LinkText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(linkText))
                    return linkText;
                else
                    return linkText.Trim();
            }
            set { linkText = value; }
        }

        public string Controller
        {
            get
            {
                if (string.IsNullOrWhiteSpace(controller))
                    return controller;
                else
                    return controller.Trim();
            }
            set { controller = value; }
        }

        public string Action
        {
            get
            {
                if (string.IsNullOrWhiteSpace(action))
                    return action;
                else
                    return action.Trim();
            }
            set { action = value; }
        }

        [XmlIgnore]
        public RouteValueDictionary RouteData
        {
            get { return routeData; }
        }

        public string RouteDataValues
        {
            get { return routeDataValues; }
            set
            {
                routeDataValues = value;

                var sets = routeDataValues.Split(',');
                foreach (var set in sets)
                {
                    var values = set.Split('=');
                    if (values.Length == 2)
                        routeData.Add(values[0].Trim(), values[1].Trim());
                }
            }
        }

        [XmlIgnore]
        public IDictionary<string, object> HtmlAttributes
        {
            get { return htmlAttributes; }
        }

        public string HtmlAttributeValues
        {
            get { return htmlAttributeValues; }
            set
            {
                htmlAttributeValues = value;

                var sets = htmlAttributeValues.Split(',');
                foreach (var set in sets)
                {
                    var values = set.Split('=');
                    if (values.Length == 2)
                        htmlAttributes.Add(values[0].Trim(), values[1].Trim());
                }
            }

        }
    }
}
