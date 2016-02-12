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
    /// Summary description for CustomerCodeWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CustomerCodeWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_CUSTOMER_CODE_MASTER> CUSTOMER = new CustomerCodeDataContext().VIEW_CUSTOMER_CODE_MASTERs.AsQueryable<VIEW_CUSTOMER_CODE_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_CUSTOMER_CODE_MASTER> GetCustomerCode(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                CUSTOMER = CUSTOMER.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                CUSTOMER = CUSTOMER.OrderBy(sortExpression);
            }
            else
            {
                CUSTOMER = CUSTOMER.OrderBy("CUST_CODE_ID ASC");
            }

            return CUSTOMER.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int CustomerCodeCount()
        {
            return (int)CUSTOMER.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCustomerCode(ArrayList customerCode)
        {

            CustomerCodeStoredProcedure objcustomer = new CustomerCodeStoredProcedure();
            customerCode.Insert(3, Session["usersid"].ToString());
            objcustomer.InsertUpdateCustomerCode(customerCode);

        }
        [WebMethod(EnableSession = true)]
        public void deleteCustomerCode(int delCustomerCode)
        {
            CustomerCodeStoredProcedure objdelcustcode = new CustomerCodeStoredProcedure();
            objdelcustcode.deleteCustomerCode(delCustomerCode);
        }
    }
}
