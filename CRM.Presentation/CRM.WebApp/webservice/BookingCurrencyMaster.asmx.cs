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
    /// Summary description for BookingCurrencyMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class BookingCurrencyMaster : System.Web.Services.WebService
    {

        
        IQueryable<VIEW_BOOKING_CURRENCY_MASTER> Currency = new BookingCurrencyMasterDataContext().VIEW_BOOKING_CURRENCY_MASTERs.AsQueryable<VIEW_BOOKING_CURRENCY_MASTER>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_BOOKING_CURRENCY_MASTER> GetCurrency(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                Currency = Currency.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                Currency = Currency.OrderBy(sortExpression);
            }
            else
            {
                Currency = Currency.OrderBy("CURRENCY_ID ASC");
            }

            return Currency.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int GetBookingCurrencyCount()
        {
            return (int)Currency.Count();
        }
        [WebMethod(EnableSession = true)]
        public void deleteBookingCurrency(int CURRENCYID)
        {
            CRM.DataAccess.AdministratorEntity.BookingCurrencyMaster objdelcurrency = new DataAccess.AdministratorEntity.BookingCurrencyMaster();
            objdelcurrency.deleteBookingCurrency(CURRENCYID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateBookingCurrency(ArrayList Currency)
        {
            CRM.DataAccess.AdministratorEntity.BookingCurrencyMaster objinsertcurrency = new DataAccess.AdministratorEntity.BookingCurrencyMaster();
            Currency.Insert(3, Session["usersid"].ToString());
            objinsertcurrency.InsertUpdateBookingCurrency(Currency);
        }
    }
}
