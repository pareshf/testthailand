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
    /// Summary description for DesignationMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class DesignationMasterWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_DESIGNATION_MASTER> DESIGNATION = new DesignationMasterDataContext().VIEW_DESIGNATION_MASTERs.AsQueryable<VIEW_DESIGNATION_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_DESIGNATION_MASTER> GetDesignationName(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                DESIGNATION = DESIGNATION.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                DESIGNATION = DESIGNATION.OrderBy(sortExpression);
            }
            else
            {
                DESIGNATION = DESIGNATION.OrderBy("DESIGNATION_ID ASC");
            }

            return DESIGNATION.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetDesignationCount()
        {
            return (int)DESIGNATION.Count();
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateDesignation(ArrayList Designation)
        {

            DesignationStoredProcedure objdesignation = new DesignationStoredProcedure();
            Designation.Insert(2, Session["usersid"].ToString());
            objdesignation.InsertUpdateDesignation(Designation);

        }
        [WebMethod(EnableSession = true)]
        public void deleteDesignationName(int deldesignation)
        {
            DesignationStoredProcedure objdeldesig = new DesignationStoredProcedure();
            objdeldesig.deleteDesignationName(deldesignation);
        }
    }
}
