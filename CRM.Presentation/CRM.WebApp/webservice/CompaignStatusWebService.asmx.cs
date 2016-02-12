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
    /// Summary description for CompaignStatusWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CompaignStatusWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_COMPAIGN_STATUS> STATUS = new CompaignStatusDataContext().VIEW_COMPAIGN_STATUS.AsQueryable<VIEW_COMPAIGN_STATUS>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_COMPAIGN_STATUS> GetCompaignStatus(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                STATUS = STATUS.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                STATUS = STATUS.OrderBy(sortExpression);
            }
            else
            {
                STATUS = STATUS.OrderBy("STATUS_ID ASC");
            }

            return STATUS.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetStatusCount()
        {
            return (int)STATUS.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCompaignStatus(ArrayList Status)
        {

            CompaignStatusStoredProcedure objStatusInsert = new CompaignStatusStoredProcedure();
            Status.Insert(2, Session["usersid"].ToString());
            objStatusInsert.InsertUpdateCompaignStatus(Status);

        }
        [WebMethod(EnableSession = true)]
        public void delCompaignStatus(int delStatus)
        {
            CompaignStatusStoredProcedure objdelStatus = new CompaignStatusStoredProcedure();
            objdelStatus.delCompaignStatus(delStatus);
        }
    }
}
