using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using CRM.DataAccess.AdministratorEntity;


namespace CRM.WebApp.webservice
{
    /// <summary>
    /// Summary description for AutoComplete
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class AutoComplete : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public void searchQueryResult(string context)
        {
                System.IO.File.WriteAllText(Server.MapPath("~/webservice/Temp.htm"), context);
        }
        public string returnsession()
        {
            return System.IO.File.ReadAllText(Server.MapPath("~/webservice/Temp.htm")).ToString();
        }
        [WebMethod(EnableSession = true)]
        public void searchQueryResultOnSecondFile(string context)
        {
                    System.IO.File.WriteAllText(Server.MapPath("~/webservice/Temp2.htm"), context);
        }
        public string returnsessionFromSecondFile()
        {
            return System.IO.File.ReadAllText(Server.MapPath("~/webservice/Temp2.htm")).ToString();
        }
    }
}
