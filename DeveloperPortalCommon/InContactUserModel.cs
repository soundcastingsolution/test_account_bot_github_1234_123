using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InContact.DeveloperPortal.Common
{
    public class InContactUserModel
    {
        public string VendorName { get; set; }
        public string UserName { get; set; }

        public string Password
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                string hash = VendorName + "/!#$!Dasfaw/" + UserName;
                using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                {
                    byte[] retVal = md5.ComputeHash(Encoding.Unicode.GetBytes(hash));
                    for (int i = 0; i < retVal.Length; i++)
                    {
                        sb.Append(retVal[i].ToString("x2"));
                    }
                }
                return sb.ToString();
            }
        }

        public InContactUserModel()
        {

        }
    }
}
