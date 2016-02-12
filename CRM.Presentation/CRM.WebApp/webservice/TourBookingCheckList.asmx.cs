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
    /// Summary description for TourBookingCheckList
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class TourBookingCheckList : System.Web.Services.WebService
    {
        IQueryable<VIEW_PASSENGER_CHECKLIST_DETAIL> CHECK = new TourCheckListDataContext().VIEW_PASSENGER_CHECKLIST_DETAILs.AsQueryable<VIEW_PASSENGER_CHECKLIST_DETAIL>();
        IQueryable<VIEW_BOOKING_DETAIL_FOR_CHECKLIST> BOOKINGCHECKLIST = new TourCheckListDataContext().VIEW_BOOKING_DETAIL_FOR_CHECKLISTs.AsQueryable<VIEW_BOOKING_DETAIL_FOR_CHECKLIST>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_PASSENGER_CHECKLIST_DETAIL> GetCheckListName(string Booking_Id)
        {
            CHECK = CHECK.Where(String.Format(@"BOOKING_ID == {0}", Booking_Id));
            return CHECK.ToList();

        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_BOOKING_DETAIL_FOR_CHECKLIST> GetBookingCheckListOnTourId(int startIndex, int maximumRows, string sortExpression, string filterExpression,string Tour_Id)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                BOOKINGCHECKLIST = BOOKINGCHECKLIST.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                BOOKINGCHECKLIST = BOOKINGCHECKLIST.OrderBy(sortExpression);
            }
            BOOKINGCHECKLIST = BOOKINGCHECKLIST.Where(String.Format(@"TOUR_ID == {0}", Tour_Id));

            return BOOKINGCHECKLIST.ToList();


        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_BOOKING_DETAIL_FOR_CHECKLIST> GetBookingCheckList(int startIndex, int maximumRows, string sortExpression, string filterExpression, string Booking_Id)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                BOOKINGCHECKLIST = BOOKINGCHECKLIST.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                BOOKINGCHECKLIST = BOOKINGCHECKLIST.OrderBy(sortExpression);
            }
            BOOKINGCHECKLIST = BOOKINGCHECKLIST.Where(String.Format(@"BOOKING_ID == {0}", Booking_Id));

            return BOOKINGCHECKLIST.ToList();


        }

        [WebMethod(EnableSession = true)]
        public int GetCheckListNameCount()
        {
            return (int)CHECK.Count();
        }
        [WebMethod(EnableSession = true)]
        public int GetBookingCheckListCount()
        {
            return (int)CHECK.Count();
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateChecklist(ArrayList ary)
        {
            BookingCheckList obj = new BookingCheckList();
            obj.UpdateCheckListDetail(ary);
        }

        [WebMethod(EnableSession = true)]
        public void UpdateAgreement(String Booking_Id)
        {
            BookingCheckList obj = new BookingCheckList();
            obj.UpdateAgreementInBookingMaster(Booking_Id);
        }
        
    }
}
