using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using CRM.DataAccess.AdministratorEntity;
using System.Web.Services.Protocols;
using CRM.WebApp.Views.dbmlfile;
using CRM.WebApp.Views;
using System.Collections;
using CRM.Model.Security;
namespace CRM.WebApp.webservice
{
    /// <summary>
    /// Summary description for RoleMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RoleMasterWebService : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public void insertupdateRole(ArrayList MyRole)
        {
               RoleMaster objrolemaster = new RoleMaster();
               MyRole.Insert(3, Session["empid"].ToString());
               objrolemaster.insertupdateRole(MyRole);
        }
           
    }
}
