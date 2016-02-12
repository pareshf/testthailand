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
    /// Summary description for CustomerTypeWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CustomerTypeWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_CUST_TYPE_MASTER> Customer = new CustomerTypeDataContext().VIEW_CUST_TYPE_MASTERs.AsQueryable<VIEW_CUST_TYPE_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_CUST_TYPE_MASTER> GetCustomerType(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                Customer = Customer.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                Customer = Customer.OrderBy(sortExpression);
            }
            else
            {
                Customer = Customer.OrderBy("CUST_TYPE_ID ASC");
            }

            return Customer.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int GetCustomerTypeCount()
        {
            return (int)Customer.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCustomerType(ArrayList Ctype)
        {

            CustomerTypeStoredProcedure objctype = new CustomerTypeStoredProcedure();
            Ctype.Insert(3, Session["usersid"].ToString());
            objctype.InsertUpdateCustomerType(Ctype);

        }
        [WebMethod(EnableSession = true)]
        public void delCustomerType(int delCtype)
        {
            CustomerTypeStoredProcedure objdelctype = new CustomerTypeStoredProcedure();
            objdelctype.delCustomerType(delCtype);
        }
    }
}
