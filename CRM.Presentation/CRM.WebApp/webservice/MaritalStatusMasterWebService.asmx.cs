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
    /// Summary description for MaritalStatusMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MaritalStatusMasterWebService : System.Web.Services.WebService
    {
        IQueryable<VIEW_MARITAL_STATUS_MASTER> MARITAL = new MaritalStatusDataContext().VIEW_MARITAL_STATUS_MASTERs.AsQueryable<VIEW_MARITAL_STATUS_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_MARITAL_STATUS_MASTER> GetMaritalName(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                MARITAL = MARITAL.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                MARITAL = MARITAL.OrderBy(sortExpression);
            }
            else
            {
                MARITAL = MARITAL.OrderBy("MARITAL_STATUS_ID ASC");
            }

            return MARITAL.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int MaritalStatusCount()
        {
            return (int)MARITAL.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateMaritalStatus(ArrayList marital)
        {

            MaritalStatusStoredProcedure objMarritalstatus = new MaritalStatusStoredProcedure();
            marital.Insert(2, Session["usersid"].ToString());
            objMarritalstatus.InsertUpdateMaritalStatus(marital);

        }
        [WebMethod(EnableSession = true)]
        public void deleteMaritalStatus(int delmarital)
        {
            MaritalStatusStoredProcedure objdelMarital = new MaritalStatusStoredProcedure();
            objdelMarital.deleteMaritalStatus(delmarital);
        }
    }
    
}
