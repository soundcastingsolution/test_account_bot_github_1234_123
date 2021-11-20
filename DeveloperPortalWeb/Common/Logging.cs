using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace InContact.DeveloperPortal.Web.Common
{    
    public static class Logging
    {
        private static TraceSource _traceSource = new TraceSource(Assembly.GetExecutingAssembly().GetName().Name);
        public static void LogMessage(string message)
        {
            try
            {
                _traceSource.TraceInformation(message);
            }
            catch { } //don't stop  processing just because we can't log
        }

        public static void LogError(string format, params object[] args)
        {
            try
            {
                _traceSource.TraceEvent(TraceEventType.Error, 100, format, args);
            }
            catch { }
        }

        public static void LogException(Exception ex, string format, params object[] args)
        {
            try
            {
                StringBuilder msgBuilder = new StringBuilder();
                msgBuilder.AppendFormat(format, args);
                LogError("{0}. Exception: {1}, StackTrace", msgBuilder, ex.Message, ex.StackTrace);
                if (ex.InnerException != null)
                {
                    LogError("Inner Exception: {0}, StackTrace: {1}", ex.InnerException.Message, ex.InnerException.StackTrace);
                }
            }
            catch { }
        }
    }
}