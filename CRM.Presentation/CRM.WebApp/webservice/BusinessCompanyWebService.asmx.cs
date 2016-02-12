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
    /// Summary description for BusinessCompanyWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class BusinessCompanyWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_BUSINESS_COMPANY> BusinessCompany = new BusinessCompanyDataContext().VIEW_BUSINESS_COMPANies.AsQueryable<VIEW_BUSINESS_COMPANY>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_BUSINESS_COMPANY> GetBusinessCompany(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                BusinessCompany = BusinessCompany.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                BusinessCompany = BusinessCompany.OrderBy(sortExpression);
            }
            else
            {
                BusinessCompany = BusinessCompany.OrderBy("COMPANY_ID ASC");
            }

            return BusinessCompany.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetBusinessCompanyCount()
        {
            return (int)BusinessCompany.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateBusinessCompany(ArrayList Bcompany)
        {

            BusinessCompanyStoredProcedure objcompanyinsert = new BusinessCompanyStoredProcedure();
            Bcompany.Insert(15, Session["usersid"].ToString());
            objcompanyinsert.InsertUpdateBusinessCompany(Bcompany);

        }
        [WebMethod(EnableSession = true)]
        public void delBusinessCompany(int delCompany)
        {
            BusinessCompanyStoredProcedure objdelcompany = new BusinessCompanyStoredProcedure();
            objdelcompany.delBusinessCompany(delCompany);
        }
    }
}
