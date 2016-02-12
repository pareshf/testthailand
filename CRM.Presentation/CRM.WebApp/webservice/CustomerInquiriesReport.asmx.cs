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
    /// Summary description for CustomerInquiriesReport
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class CustomerInquiriesReport : System.Web.Services.WebService
    {
        IQueryable<VIEW_CUST_CUSTOMER_MASTER> Customer = new CustomerMasterDataContext().VIEW_CUST_CUSTOMER_MASTERs.AsQueryable<VIEW_CUST_CUSTOMER_MASTER>();
        [WebMethod]
        public List<VIEW_CUST_CUSTOMER_MASTER> GetCustomer(int startIndex, int maximumRows, string sortExpression, string filterExpression, string scomapany, string scity, string scode, string stype, string sbranch, string semp, string scommode, string srelation, string suniquetid, string sfname, string slname, string semail, string smob, string stele)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                Customer = Customer.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                Customer = Customer.OrderBy(sortExpression);
            }
            if (!String.IsNullOrEmpty(scomapany))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Customer = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_COMPANY_NAME.Contains(scomapany)));
            }
            if (!String.IsNullOrEmpty(scity))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Customer = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CITY_NAME.Contains(scity)));
            }
            if (!String.IsNullOrEmpty(scode))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Customer = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_CODE_NAME.Contains(scode)));
            }
            if (!String.IsNullOrEmpty(stype))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Customer = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_TYPE_NAME.Contains(stype)));
            }
            if (!String.IsNullOrEmpty(sbranch))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Customer = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.BRANCH.Contains(sbranch)));
            }
            if (!String.IsNullOrEmpty(semp))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Customer = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.EMPLOYEE.Contains(semp)));
            }
            if (!String.IsNullOrEmpty(scommode))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Customer = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.COMMUNICATION_MODE_NAME.Contains(scommode)));
            }
            if (!String.IsNullOrEmpty(srelation))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Customer = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.RELATION_DESC.Contains(srelation)));
            }
            if (!String.IsNullOrEmpty(suniquetid))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Customer = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_UNQ_ID.Contains(suniquetid)));
            }
            if (!String.IsNullOrEmpty(sfname))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Customer = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_SURNAME.Contains(sfname)));
            }
            if (!String.IsNullOrEmpty(slname))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Customer = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_NAME.Contains(slname)));
            }
            if (!String.IsNullOrEmpty(semail))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Customer = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_REL_EMAIL.Contains(semail)));
            }
            if (!String.IsNullOrEmpty(smob))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Customer = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_REL_MOBILE.Contains(smob)));
            }
            if (!String.IsNullOrEmpty(stele))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Customer = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_REL_PHONE.Contains(stele)));
            }

            else
            {
                Customer = Customer.OrderBy("CUST_ID");
            }

            return Customer.Skip(startIndex).Take(maximumRows).ToList();
        }
        [WebMethod(EnableSession = true)]
        public int GetCustCount()
        {
            return (int)Customer.Count();
        }
    }
}
