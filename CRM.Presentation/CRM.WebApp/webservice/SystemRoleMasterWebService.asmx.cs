using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using CRM.WebApp.Views.dbmlfile;
using CRM.WebApp.Views;
using System.Collections;
using CRM.Model.Security;
using CRM.DataAccess.AdministratorEntity;

namespace CRM.WebApp.webservice
{
    /// <summary>
    /// Summary description for SystemRoleMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SystemRoleMasterWebService : System.Web.Services.WebService
    {
        IQueryable<VIEW_FOR_SYS_ROLE_MASTER> USER_ROLE = new SystemRoleMasterDataContext().VIEW_FOR_SYS_ROLE_MASTERs.AsQueryable<VIEW_FOR_SYS_ROLE_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_SYS_ROLE_MASTER> GetUserRole(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                USER_ROLE = USER_ROLE.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                USER_ROLE = USER_ROLE.OrderBy(sortExpression);
            }
            else
            {
                USER_ROLE = USER_ROLE.OrderBy("ROLE_ID ASC");
            }

            return USER_ROLE.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int GetSystemRoleCount()
        {
            return (int)USER_ROLE.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateRoleMaster(ArrayList RoleMaster)
        {

            SystemRoleMasterStoredProcedure objinsertrole = new SystemRoleMasterStoredProcedure();
            RoleMaster.Insert(2, Session["usersid"].ToString());
            objinsertrole.InsertUpdateRoleMaster(RoleMaster);

        }
        [WebMethod(EnableSession = true)]
        public void deleteUserRole(int delrole)
        {
            SystemRoleMasterStoredProcedure objdelrole = new SystemRoleMasterStoredProcedure();
            objdelrole.deleteUserRole(delrole);
        }
    }
}
