using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;

namespace SoapWebService.asmx
{
    /// <summary>
    /// Summary description for SoapService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SoapService : WebService
    {
        public AuthHeader Credentials;

        [SoapHeader("Credentials")]
        [WebMethod]

        public string PasswordAuth(string UserName, string Password)
        {
            if (Credentials.UserName.ToLower() != "bpolonij" || Credentials.Password.ToLower() != "password")
            {
                throw new SoapException("Neovlasteni upad", SoapException.ClientFaultCode);
            }
            else
                return "uspješno ste usli";

        }
 

    public class AuthHeader : SoapHeader
    {
        public string UserName;
        public string Password;
    }
}
}