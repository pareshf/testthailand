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
    /// Summary description for CompanyWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CompanyWebService : System.Web.Services.WebService
    {
        IQueryable<VIEW_COMPANY_MASTER> company = new CompanyDataContext().VIEW_COMPANY_MASTERs.AsQueryable<VIEW_COMPANY_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_COMPANY_MASTER> GetCompanyName(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                company = company.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                company = company.OrderBy(sortExpression);
            }
            else
            {
                company = company.OrderBy("COMPANY_ID ASC");
            }

            return company.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int CompanyCount()
        {
            return (int)company.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCompany(ArrayList Company)
        {

            CRM.DataAccess.AdministratorEntity.CompanyStoredProcedure objinsertCompany = new CRM.DataAccess.AdministratorEntity.CompanyStoredProcedure();
            Company.Insert(24, Session["usersid"].ToString());
            objinsertCompany.InsertUpdateCompanyMaster(Company);
        }
        [WebMethod(EnableSession = true)]
        public void deleteCompany(int delCompany)
        {
            CRM.DataAccess.AdministratorEntity.CompanyStoredProcedure objdelCompany = new CRM.DataAccess.AdministratorEntity.CompanyStoredProcedure();
            objdelCompany.delCompanyName(delCompany);
        }
    }
}
