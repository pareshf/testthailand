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
    /// <summary>
    /// Summary description for CruiseRoomAllocationWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CruiseRoomAllocationWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_PASSENGER_DETAIL_FOR_CRUISE> Passenger = new CruiseRoomAllocationDataContext().VIEW_PASSENGER_DETAIL_FOR_CRUISEs.AsQueryable<VIEW_PASSENGER_DETAIL_FOR_CRUISE>();
        IQueryable<VIEW_BOOKING_CRUISE_DETAIL> Cruise = new CruiseRoomAllocationDataContext().VIEW_BOOKING_CRUISE_DETAILs.AsQueryable<VIEW_BOOKING_CRUISE_DETAIL>();
        IQueryable<VIEW_CRUISE_ROOMING_LIST> CruiseRoom = new CruiseRoomAllocationDataContext().VIEW_CRUISE_ROOMING_LISTs.AsQueryable<VIEW_CRUISE_ROOMING_LIST>();
        IQueryable<VIEW_BOOKING_CRUISE_DETAIL_FOR_REPORT> CruiseReport = new CruiseRoomAllocationDataContext().VIEW_BOOKING_CRUISE_DETAIL_FOR_REPORTs.AsQueryable<VIEW_BOOKING_CRUISE_DETAIL_FOR_REPORT>();
        
        [WebMethod(EnableSession = true)]
        public List<VIEW_PASSENGER_DETAIL_FOR_CRUISE> GetCruisePassengerDetails(string booking_id)
        {
            Passenger = Passenger.Where(String.Format(@"BOOKING_ID == {0}", booking_id));
            return Passenger.ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_PASSENGER_DETAIL_FOR_CRUISE> GetCruisePassDetails(string bookingid)
        {
            Passenger = Passenger.Where(String.Format(@"BOOKING_ID == {0}", bookingid));
            return Passenger.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_BOOKING_CRUISE_DETAIL> GetCruiseDetails(string Booking_Detail_id)
        {
            Cruise = Cruise.Where(String.Format(@"BOOKING_DETAIL_ID == {0}", Booking_Detail_id));
            return Cruise.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_BOOKING_CRUISE_DETAIL_FOR_REPORT> GetCruiseDetailsReport(string Tour_id)
        {
            CruiseReport = CruiseReport.Where(String.Format(@"TOUR_ID == {0}", Tour_id));
            return CruiseReport.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_CRUISE_ROOMING_LIST> GetCruiseRooms(string booking_cruise_srno)
        {
            CruiseRoom = CruiseRoom.Where(String.Format(@"BOOKING_CRUISE_SRNO == {0}", booking_cruise_srno));
            return CruiseRoom.ToList();
        }

        [WebMethod(EnableSession = true)]
        public void NewCruiseInsert(String Tour_id, String Booking_Detail_id)
        {
            CruiseRoomAllocationCs objCruise = new CruiseRoomAllocationCs();
            objCruise.InsertNewCruise(Convert.ToInt32(Tour_id), Convert.ToInt32(Booking_Detail_id));
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateCruiseDetails(ArrayList Cruise1)
        {
            CruiseRoomAllocationCs objInsert = new CruiseRoomAllocationCs();
            objInsert.InserUpdateBookingCruiseDetails(Cruise1);
        }

        [WebMethod(EnableSession = true)]
        public void InsertCruiseRoom(ArrayList room)
        {
            CruiseRoomAllocationCs InsertRoom = new CruiseRoomAllocationCs();
            InsertRoom.InserUpdateRoom(room);
        }


    }
}
