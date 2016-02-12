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
    /// Summary description for BookingMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class BookingMaster : System.Web.Services.WebService
    {
        IQueryable<VIEW_CUSTOMER_DETAIL> ADDRESS = new NewBookingDetailDataContext().VIEW_CUSTOMER_DETAILs.AsQueryable<VIEW_CUSTOMER_DETAIL>();
        IQueryable<VIEW_CUSTOMER_REL_DETAIL> RELATION = new NewBookingDetailDataContext().VIEW_CUSTOMER_REL_DETAILs.AsQueryable<VIEW_CUSTOMER_REL_DETAIL>();
        IQueryable<VIEW_CUST_VISA_DETAIL> VISA = new NewBookingDetailDataContext().VIEW_CUST_VISA_DETAILs.AsQueryable<VIEW_CUST_VISA_DETAIL>();
        IQueryable<VIEW_BOOKING_PAYMENT_DETAIL> PAYMENT = new NewBookingDetailDataContext().VIEW_BOOKING_PAYMENT_DETAILs.AsQueryable<VIEW_BOOKING_PAYMENT_DETAIL>();
        IQueryable<VIEW_BOOKING_FORM_TEXTBOX> PAGE = new NewBookingDetailDataContext().VIEW_BOOKING_FORM_TEXTBOXes.AsQueryable<VIEW_BOOKING_FORM_TEXTBOX>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_CUSTOMER_DETAIL> GetaddressDetails(string value)
        {
            ADDRESS = ADDRESS.OrderBy("INQUIRY_ID");
            ADDRESS = ADDRESS.Where(String.Format(@"INQUIRY_ID=={0}", value));
            return ADDRESS.ToList();
        }
        
        [WebMethod(EnableSession = true)]
        public List<VIEW_CUSTOMER_REL_DETAIL> Getcustomerrelation(string CUST_ID)
        {
            RELATION = RELATION.Where(String.Format(@"CUST_ID=={0}", CUST_ID));
            return RELATION.ToList();

        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_CUST_VISA_DETAIL>Getcustomervisa(string CUST_ID)
        {
            VISA = VISA.Where(String.Format(@"CUST_REL_SRNO=={0}", CUST_ID));
            return VISA.ToList();
        }
        
        [WebMethod(EnableSession = true)]
        public List<VIEW_BOOKING_PAYMENT_DETAIL> Getpaymentinfo(string CUST_ID)
        {
            PAYMENT = PAYMENT.Where(String.Format(@"CUST_ID=={0}", CUST_ID));
            return PAYMENT.ToList();
        }

        [WebMethod(EnableSession = true)]
        public ArrayList Getbookinginfo(string INQUIRY_ID)
        {
            PAGE = PAGE.Where(String.Format(@"INQUIRY_ID=={0}", INQUIRY_ID));
            ArrayList arr = new ArrayList();
            arr.Add(PAGE.ToList());
            return arr;
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateaddressDetail(ArrayList Address)
        {
            NewBookingInformation objbookinginfo = new NewBookingInformation();
            objbookinginfo.InsertUpdateaddressDetail(Address);

        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateRelationDetail(ArrayList Relation)
        {
            NewBookingInformation objbookinginfo = new NewBookingInformation();
            objbookinginfo.InsertUpdateRelationDetail(Relation);

        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdatevisadetail(ArrayList Visa)
        {
            NewBookingInformation objbookinginfo = new NewBookingInformation();
            objbookinginfo.InsertUpdatevisadetail(Visa);

        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdatePaymentDetail(ArrayList Payment)
        {
            NewBookingInformation objbookinfo = new NewBookingInformation();
            objbookinfo.InsertUpdatePayment(Payment);
        
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateBookingInfo(ArrayList INFO)
        {
            NewBookingInformation objbookinginfo = new NewBookingInformation();
            //INFO.Insert(17, Session["usersid"].ToString());
            objbookinginfo.InsertUpdateBookingInfo(INFO);

        }
        [WebMethod(EnableSession = true)]
        public void InsertNewVisa(int CUST_ID,int CUST_REL_ID)
        {
            NewBookingInformation objbookinginfo = new NewBookingInformation();
            objbookinginfo.InsertNewVisa(CUST_ID,CUST_REL_ID);
        }
    }
 }
