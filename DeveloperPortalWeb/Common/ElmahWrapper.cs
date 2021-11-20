using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InContact.DeveloperPortal.Web.Common
{
    public class ElmahWrapper : IElmahWrapper
    {
        public void Raise(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
}