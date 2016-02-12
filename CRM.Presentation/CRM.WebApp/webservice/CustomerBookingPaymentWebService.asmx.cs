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
    /// Summary description for CustomerBookingPaymentWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CustomerBookingPaymentWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_CUSTOMER_BOOKING_PAYMENT> PAYMENT = new CustomerPaymentDataContext().VIEW_CUSTOMER_BOOKING_PAYMENTs.AsQueryable<VIEW_CUSTOMER_BOOKING_PAYMENT>();
        IQueryable<VIEW_CUSTOMER_DETAIL_BOOKING_PAYMENT> CUSTOMER = new CustomerPaymentDataContext().VIEW_CUSTOMER_DETAIL_BOOKING_PAYMENTs.AsQueryable<VIEW_CUSTOMER_DETAIL_BOOKING_PAYMENT>();

        //[WebMethod(EnableSession = true)]
        //public List<VIEW_CUSTOMER_BOOKING_PAYMENT> GetPaymentDetail(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        //{
        //    if (!String.IsNullOrEmpty(filterExpression))
        //    {
        //        PAYMENT = PAYMENT.Where(filterExpression);
        //    }
        //    if (!String.IsNullOrEmpty(sortExpression))
        //    {
        //        PAYMENT = PAYMENT.OrderBy(sortExpression);
        //    }
        //    else
        //    {
        //        PAYMENT = PAYMENT.OrderBy("PAYMENT_SRNO ASC");
        //    }

        //    return PAYMENT.Skip(startIndex).Take(maximumRows).ToList();


        //}
        [WebMethod(EnableSession = true)]
        public List<VIEW_CUSTOMER_DETAIL_BOOKING_PAYMENT>GetCustomerDetail(string TOUR_ID)
        {
            
            CUSTOMER = CUSTOMER.Where(String.Format(@"TOUR_ID == {0}", TOUR_ID));
            return CUSTOMER.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_CUSTOMER_DETAIL_BOOKING_PAYMENT>GetBookingCustomerDetail(string BOOKING_ID)
        {

            CUSTOMER = CUSTOMER.Where(String.Format(@"BOOKING_ID == {0}", BOOKING_ID));
            return CUSTOMER.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_CUSTOMER_BOOKING_PAYMENT> GetPaymentDetail(string BOOKING_ID)
        {
            PAYMENT = PAYMENT.Where(String.Format(@"BOOKING_ID == {0}", BOOKING_ID));
            return PAYMENT.ToList();
        }
        [WebMethod(EnableSession = true)]
        public int PaymentCount()
        {
            return (int)PAYMENT.Count();
        }
        [WebMethod(EnableSession = true)]
        public int CustomerCount()
        {
            return (int)CUSTOMER.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateBookingPaymentDetail(ArrayList Payment)
        {

            CustomerPaymentStoredProcedure objpayment = new CustomerPaymentStoredProcedure();
            Payment.Insert(18, Session["usersid"].ToString());
            objpayment.InsertUpdateBookingPaymentDetail(Payment);
        }
        [WebMethod(EnableSession = true)]
        public void delPayment(int Payment)
        {
            CustomerPaymentStoredProcedure objdelpayment = new CustomerPaymentStoredProcedure();
            objdelpayment.delPayment(Payment);
        }
        [WebMethod(EnableSession = true)]
        public void delCustomer(int delcustomer)
        {
            CustomerPaymentStoredProcedure objdelpayment = new CustomerPaymentStoredProcedure();
            objdelpayment.delPayment(delcustomer);
        }
        [WebMethod(EnableSession = true)]
        public void InsertNewpayment(string paymentid, string recivedby, string pdate, string rdate)
        {
            CustomerPaymentStoredProcedure objinsertnew = new CustomerPaymentStoredProcedure();
            objinsertnew.InsertNewpayment(paymentid,recivedby,pdate,rdate);
        }
       
    }
}
