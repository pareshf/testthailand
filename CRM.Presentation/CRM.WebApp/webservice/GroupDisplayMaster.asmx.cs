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
    /// Summary description for GroupDisplayMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GroupDisplayMaster : System.Web.Services.WebService
    {

        
        IQueryable<VIEW_GROUP_DISPLAY_MASTER> display = new GroupDisplayMasterDataContext().VIEW_GROUP_DISPLAY_MASTERs.AsQueryable<VIEW_GROUP_DISPLAY_MASTER>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_GROUP_DISPLAY_MASTER> GetGroupDisplay(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                display = display.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                display = display.OrderBy(sortExpression);
            }
            else
            {
                display = display.OrderBy("GROUP_DISPLAY_ID ASC");
            }

            return display.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int GetGroupDisplayCount()
        {
            return (int)display.Count();
        }
        [WebMethod(EnableSession = true)]
        public void deleteGroupDisplay(int GROUPDISPLAYID)
        {
            CRM.DataAccess.AdministratorEntity.GroupDisplayMaster objdelGroupDisplay = new DataAccess.AdministratorEntity.GroupDisplayMaster();
            objdelGroupDisplay.deleteGroupDisplay(GROUPDISPLAYID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateGroupDisplay(ArrayList Display)
        {
            CRM.DataAccess.AdministratorEntity.GroupDisplayMaster objinsertGroupDisplay = new DataAccess.AdministratorEntity.GroupDisplayMaster();
            Display.Insert(3, Session["usersid"].ToString());
            objinsertGroupDisplay.InsertUpdateGroupDisplayMaster(Display);
        }
    }

}