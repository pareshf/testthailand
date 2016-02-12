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
    /// Summary description for CompanyMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CompanyMasterWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_CUST_COMPANY_MASTER> COMPANY = new CompanyMasterDataContext().VIEW_CUST_COMPANY_MASTERs.AsQueryable<VIEW_CUST_COMPANY_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_CUST_COMPANY_MASTER> GetCompanyName(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                COMPANY = COMPANY.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                COMPANY = COMPANY.OrderBy(sortExpression);
            }
            else
            {
                COMPANY = COMPANY.OrderBy("CUST_COMPANY_ID ASC");
            }

            return COMPANY.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int CompanyNameCount()
        {
            return (int)COMPANY.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCompanyName(ArrayList company)
        {

            CompanyMasterStoredProcedure objcompany = new CompanyMasterStoredProcedure();
            objcompany.InsertUpdateCompanyName(company);
           
            

        }
        [WebMethod(EnableSession = true)]
        public void delCompanyName(int delCompany)
        {
            CompanyMasterStoredProcedure objdelcompany = new CompanyMasterStoredProcedure();
            objdelcompany.delCompanyName(delCompany);
        }
    }
}
