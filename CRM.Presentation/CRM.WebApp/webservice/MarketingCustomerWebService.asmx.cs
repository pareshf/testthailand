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
    /// Summary description for MarketingCustomerWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MarketingCustomerWebService : System.Web.Services.WebService {

        IQueryable<VIEW_MARKETING_CUSTOMER_NEW> MarketingCustomer = new MarketingDataContext().VIEW_MARKETING_CUSTOMER_NEWs.AsQueryable<VIEW_MARKETING_CUSTOMER_NEW>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_MARKETING_CUSTOMER_NEW> GetMarketingCustomer(int startIndex, int maximumRows, string sortExpression, string filterExpression, string fname, string lname, string city, string state, string country, string mobile, string phone, string cmode, string ccompany)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                MarketingCustomer = MarketingCustomer.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                MarketingCustomer = MarketingCustomer.OrderBy(sortExpression);
            }
            if (!String.IsNullOrEmpty(mobile))
            {

                MarketingDataContext db = new MarketingDataContext();
                MarketingCustomer = db.VIEW_MARKETING_CUSTOMER_NEWs.Where(p => (p.MOBILE_NO.Contains(mobile)));
            }
            if (!String.IsNullOrEmpty(phone))
            {

                MarketingDataContext db = new MarketingDataContext();
                MarketingCustomer = db.VIEW_MARKETING_CUSTOMER_NEWs.Where(p => (p.PHONE_NO.Contains(phone)));
            }
            if (!String.IsNullOrEmpty(fname))
            {

                MarketingDataContext db = new MarketingDataContext();
                MarketingCustomer = db.VIEW_MARKETING_CUSTOMER_NEWs.Where(p => (p.MAR_DATA_NAME.Contains(fname)));
            }
            if (!String.IsNullOrEmpty(lname))
            {

                MarketingDataContext db = new MarketingDataContext();
                MarketingCustomer = db.VIEW_MARKETING_CUSTOMER_NEWs.Where(p => (p.MAR_DATA_SURNAME.Contains(lname)));
            }
            if (!String.IsNullOrEmpty(city))
            {

                MarketingDataContext db = new MarketingDataContext();
                MarketingCustomer = db.VIEW_MARKETING_CUSTOMER_NEWs.Where(p => (p.CITY_NAME.Contains(city)));
            }
            if (!String.IsNullOrEmpty(state))
            {

                MarketingDataContext db = new MarketingDataContext();
                MarketingCustomer = db.VIEW_MARKETING_CUSTOMER_NEWs.Where(p => (p.STATE_NAME.Contains(state)));
            }
            if (!String.IsNullOrEmpty(country))
            {

                MarketingDataContext db = new MarketingDataContext();
                MarketingCustomer = db.VIEW_MARKETING_CUSTOMER_NEWs.Where(p => (p.COUNTRY_NAME.Contains(country)));
            }
            if (!String.IsNullOrEmpty(cmode))
            {

                MarketingDataContext db = new MarketingDataContext();
                MarketingCustomer = db.VIEW_MARKETING_CUSTOMER_NEWs.Where(p => (p.COMMUNICATION_MODE_NAME.Contains(cmode)));
            }
            if (!String.IsNullOrEmpty(ccompany))
            {

                MarketingDataContext db = new MarketingDataContext();
                MarketingCustomer = db.VIEW_MARKETING_CUSTOMER_NEWs.Where(p => (p.CUST_COMPANY_NAME.Contains(ccompany)));
            }
            else
            {
                MarketingCustomer = MarketingCustomer.OrderBy("MAR_DATA_ID ASC");
            }

            return MarketingCustomer.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_MARKETING_CUSTOMER_NEW> GetData(string TARGETLIST_ID)
        {

            MarketingCustomer = MarketingCustomer.Where(String.Format(@"TARGETLIST_ID == {0}", TARGETLIST_ID));
            return MarketingCustomer.ToList();
        }
        //[WebMethod(EnableSession = true)]
        //public List<VIEW_MARKETING_DATA_MASTER> GetSearchTour(string param)
        //{
        //    MarketingCustomerDataContext db = new MarketingCustomerDataContext();
        //    MarketingCustomer = db.VIEW_MARKETING_DATA_MASTERs.Where(p => p.MOBILE_NO.Contains(param));
        //    return MarketingCustomer.ToList();
        //}
        [WebMethod(EnableSession = true)]
        public int GetMarCustomerCount()
        {
            return (int)MarketingCustomer.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateMarketingCustomer(ArrayList MarCustomer)
        {

            MarketingCustomerStoredProcedure objmarcust = new MarketingCustomerStoredProcedure();
            MarCustomer.Insert(13, Session["usersid"].ToString());
            objmarcust.InsertUpdateMarketingCustomer(MarCustomer);

        }
        [WebMethod(EnableSession = true)]
        public void delMarketingCustomer(int delmarcust)
        {
            MarketingCustomerStoredProcedure objdelCust = new MarketingCustomerStoredProcedure();
            objdelCust.delMarketingCustomer(delmarcust);
        }
    }
}
