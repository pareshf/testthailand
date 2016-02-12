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
    /// Summary description for TargetListMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class TargetListMasterWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_TARGET_LIST_MASTER> TARGETLIST = new TargetListMasterDataContext().VIEW_TARGET_LIST_MASTERs.AsQueryable<VIEW_TARGET_LIST_MASTER>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_TARGET_LIST_MASTER> GetTargetList(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                TARGETLIST = TARGETLIST.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                TARGETLIST = TARGETLIST.OrderBy(sortExpression);
            }
            else
            {
                TARGETLIST = TARGETLIST.OrderBy("TARGETLIST_ID ASC");
            }

            return TARGETLIST.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetTargetTypeCount()
        {
            return (int)TARGETLIST.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateTargetList(ArrayList Target)
        {

            TargetListStoredProcedure objinserttarget = new TargetListStoredProcedure();
            Target.Insert(7, Session["usersid"].ToString());
            objinserttarget.InsertUpdateTargetList(Target);
        }
        [WebMethod(EnableSession = true)]
        public void delTargetList(int delTarget)
        {
            TargetListStoredProcedure objdeltarget = new TargetListStoredProcedure();
            objdeltarget.delTargetList(delTarget);
        }
    }
}
