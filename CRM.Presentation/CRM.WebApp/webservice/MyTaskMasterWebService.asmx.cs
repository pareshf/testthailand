using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using CRM.DataAccess.Dashboard;
using CRM.DataAccess.AdministratorEntity;
using System.Web.Services.Protocols;
using CRM.WebApp.Views.dbmlfile;
using CRM.WebApp.Views;
using System.Collections;
using CRM.Model.Security;


namespace CRM.WebApp.webservice
{
    /// <summary>
    /// Summary description for MyTaskMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class MyTaskMasterWebService : System.Web.Services.WebService
    {
      
        IQueryable<VIEW_MYTASK_DETAIL> TASK = new MyTaskDetailsDataContext().VIEW_MYTASK_DETAILs.AsQueryable<VIEW_MYTASK_DETAIL>();
       


        [WebMethod (EnableSession = true)]
        public List<VIEW_MYTASK_DETAIL> GetTaskDetails(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                TASK = TASK.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                TASK = TASK.OrderBy(sortExpression);
            }
            else
            {
                TASK = TASK.OrderBy("MYTASK_ID");
            }

            return TASK.Skip(startIndex).Take(maximumRows).ToList();

           // TASK = TASK.Where(String.Format(@"MYTASK_ID == {0}", MYTASK_ID));
            //return TASK.ToList();
        }

        [WebMethod(EnableSession = true)]
        public void DeleteTaskByMYTASKID(int MytaskId)
        {
            MyTaskmaster objmytaskmaster = new MyTaskmaster();
            objmytaskmaster.DeleteMytask(MytaskId);   
        }
       

        [WebMethod(EnableSession = true)]
        public void InsertUpdateMytask(ArrayList Mytask)
        {
            MyTaskmaster objmytaskmaster = new MyTaskmaster();
            Mytask.Insert(14, Session["empid"].ToString());
            objmytaskmaster.InsertUpdateTask(Mytask);
            
        }

        [WebMethod(EnableSession = true)]
        public int GetTaskCount()
        { 
            return (int)TASK.Count();
        }
    }
}
