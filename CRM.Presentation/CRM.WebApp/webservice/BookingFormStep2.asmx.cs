using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CRM.DataAccess.AdministratorEntity;
using System.Web.Services.Protocols;
using CRM.WebApp.Views.dbmlfile;
using CRM.WebApp.Views;
using System.Collections;
using CRM.Model.Security;
using System.Data;

namespace CRM.WebApp.webservice
{
   

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class BookingFormStep2 : System.Web.Services.WebService
    {
        
        IQueryable<VIEW_BOOKING_INFORMATION_MASTER> BookingMaster = new BookingFormStep2DataContext().VIEW_BOOKING_INFORMATION_MASTERs.AsQueryable<VIEW_BOOKING_INFORMATION_MASTER>();
        IQueryable<VIEW_RELATION_DETAILS_BOOKING_STEP2> BookingDetail = new BookingFormStep2DataContext().VIEW_RELATION_DETAILS_BOOKING_STEP2s.AsQueryable<VIEW_RELATION_DETAILS_BOOKING_STEP2>();
        IQueryable<VIEW_BOOKING_PAYMENT_DETAILS_NEW> BookingPaymentDetail = new BookingFormStep2DataContext().VIEW_BOOKING_PAYMENT_DETAILS_NEWs.AsQueryable<VIEW_BOOKING_PAYMENT_DETAILS_NEW>();
        IQueryable<VIEW_FOR_COST_FOR_BOOKING_STEP2> CostInfo = new BookingFormStep2DataContext().VIEW_FOR_COST_FOR_BOOKING_STEP2s.AsQueryable<VIEW_FOR_COST_FOR_BOOKING_STEP2>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_BOOKING_INFORMATION_MASTER> GetBooking(string booking_id,string param)
        {
            
            if (param.Equals("-1"))
            {
                BookingFormStep2DataContext db = new BookingFormStep2DataContext();
                BookingMaster = db.VIEW_BOOKING_INFORMATION_MASTERs.OrderByDescending(o => o.BOOKING_ID).Take(1);  
            }
            else 
            {
                BookingMaster = BookingMaster.Where(String.Format(@"BOOKING_ID == {0}", booking_id));
            }
            return BookingMaster.ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_RELATION_DETAILS_BOOKING_STEP2> GetBookingInformationDetail(string bookingid)
        {
            BookingDetail = BookingDetail.Where(String.Format(@"BOOKING_ID == {0}", bookingid));
            return BookingDetail.ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_BOOKING_PAYMENT_DETAILS_NEW> GetBookingPaymentDetail(string booking_id)
        {
            BookingPaymentDetail = BookingPaymentDetail.Where(String.Format(@"BOOKING_ID == {0}",booking_id));
            return BookingPaymentDetail.ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_COST_FOR_BOOKING_STEP2> GetCostinfo(string booking_id)
        { 
            CostInfo = CostInfo.Where(String.Format(@"BOOKING_ID == {0}",booking_id));
            return CostInfo.ToList();
        }


        [WebMethod(EnableSession = true)]
        public void InsertUpdateBookingMaster(ArrayList Booking)
        {
            CRM.DataAccess.AdministratorEntity.BookingFormStep2 objBooking2 = new CRM.DataAccess.AdministratorEntity.BookingFormStep2();
            objBooking2.InsertUpdateBookingInfoMaster(Booking);
        }

        [WebMethod(EnableSession = true)]
        public void InsertCostInfoForAgegroup(ArrayList Cost)
        {
            CRM.DataAccess.AdministratorEntity.BookingFormStep2 objBooking2 = new CRM.DataAccess.AdministratorEntity.BookingFormStep2();
            objBooking2.InsertupdateCostInfo(Cost);

        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateBookingPaymentDetails(ArrayList Payment)
        {
            CRM.DataAccess.AdministratorEntity.BookingFormStep2 objBooking2 = new CRM.DataAccess.AdministratorEntity.BookingFormStep2();
            objBooking2.InsertUpdateBookingPaymentDetail(Payment);
        
        }

        [WebMethod(EnableSession = true)]
        public void UpdateBookingDetails(ArrayList detail)
        {
            CRM.DataAccess.AdministratorEntity.BookingFormStep2 objBooking2 = new CRM.DataAccess.AdministratorEntity.BookingFormStep2();
            objBooking2.Insertingnulvalues(detail);

        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateBookingInformation(ArrayList infodetail)
        {
            CRM.DataAccess.AdministratorEntity.BookingFormStep2 objBooking2 = new CRM.DataAccess.AdministratorEntity.BookingFormStep2();
            objBooking2.InsertUpdateBookinginfoDetail(infodetail);

        }

        [WebMethod(EnableSession = true)]
        public String GetExistingBookingId(int inq_id,int cust_id,string tour_snm,string tour_code) 
        {
            string a = null;
            DataTable dt = null;
            CRM.DataAccess.AdministratorEntity.BookingFormStep2 objBooking2 = new CRM.DataAccess.AdministratorEntity.BookingFormStep2();
            dt = objBooking2.getBookingId(inq_id,cust_id,tour_snm,tour_code);
            foreach (DataRow dr in dt.Rows)
            {
                a = dr["RESULT"].ToString();
            }
            return a;
        }

        [WebMethod(EnableSession = true)]
        public String GetTourType(string tour_snm, string tour_code)
        {
            string a = null;
            DataTable dt = null;
            CRM.DataAccess.AdministratorEntity.BookingFormStep2 objBooking2 = new CRM.DataAccess.AdministratorEntity.BookingFormStep2();
            dt = objBooking2.getTourType(tour_snm, tour_code);
            foreach (DataRow dr in dt.Rows)
            {
                a = dr["TOUR_TYPE_ID"].ToString();
            }
            return a;
        }

    }

}
