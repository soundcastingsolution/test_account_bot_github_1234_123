using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace InContact.DeveloperPortal.Web.Common
{
    public static class ConfigurationReader
    {

        public static bool IsLoginEnabled
        {
            get { return ReadBooleanSetting("IsLoginEnabled", false); }

        }
        private static bool ReadBooleanSetting(string configKey, bool fallback = false)
        {
            bool temp;
            if (bool.TryParse(WebConfigurationManager.AppSettings[configKey], out temp))
                return temp;

            return fallback;
        }

    }
}