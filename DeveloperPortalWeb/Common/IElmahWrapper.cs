using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InContact.DeveloperPortal.Web.Common
{
    public interface IElmahWrapper
    {
        void Raise(Exception ex);
    }
}