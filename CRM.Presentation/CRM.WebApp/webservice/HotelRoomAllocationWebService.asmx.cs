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
    /// Summary description for HotelRoomAllocationWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class HotelRoomAllocationWebService : System.Web.Services.WebService
    {
        IQueryable<VIEW_PASSENGER_DETAIL_FOR_HOTEL> Passenger = new HotelRoomAllocationDataContext().VIEW_PASSENGER_DETAIL_FOR_HOTELs.AsQueryable<VIEW_PASSENGER_DETAIL_FOR_HOTEL>();
        IQueryable<VIEW_BOOKING_HOTEL_DETAIL> Hotel = new HotelRoomAllocationDataContext().VIEW_BOOKING_HOTEL_DETAILs.AsQueryable<VIEW_BOOKING_HOTEL_DETAIL>();
        IQueryable<VIEW_HOTEL_ROOM_ALLOCATION> Room = new HotelRoomAllocationDataContext().VIEW_HOTEL_ROOM_ALLOCATIONs.AsQueryable<VIEW_HOTEL_ROOM_ALLOCATION>();
        IQueryable<VIEW_BOOKING_HOTEL_DETAIL_FOR_REPORT> Tour_Hotel = new HotelRoomAllocationDataContext().VIEW_BOOKING_HOTEL_DETAIL_FOR_REPORTs.AsQueryable<VIEW_BOOKING_HOTEL_DETAIL_FOR_REPORT>();
        IQueryable<VIEW_HOTEL_ROOM_ALLOCATION_SELF_CUST_DETAIL> cust = new HotelRoomAllocationDataContext().VIEW_HOTEL_ROOM_ALLOCATION_SELF_CUST_DETAILs.AsQueryable<VIEW_HOTEL_ROOM_ALLOCATION_SELF_CUST_DETAIL>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_PASSENGER_DETAIL_FOR_HOTEL> GetPassengerDetail(string Booking_id)
        {
            Passenger = Passenger.Where(String.Format(@"BOOKING_ID == {0}", Booking_id));
            return Passenger.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_BOOKING_HOTEL_DETAIL> GetHotelDetail(string Booking_Detail_id)
        {
            Hotel = Hotel.Where(String.Format(@"BOOKING_DETAIL_ID == {0}", Booking_Detail_id));
            return Hotel.ToList();
        }      

        [WebMethod(EnableSession = true)]
        public List<VIEW_BOOKING_HOTEL_DETAIL_FOR_REPORT> GetTourHotelDetail(string Tour_Id)
        {
            Tour_Hotel = Tour_Hotel.Where(String.Format(@"TOUR_ID == {0}", Tour_Id));
            return Tour_Hotel.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_HOTEL_ROOM_ALLOCATION_SELF_CUST_DETAIL> GetSelfCustDetail(string Tour_Id)
        {
            cust = cust.Where(String.Format(@"TOUR_ID == {0}", Tour_Id));
            return cust.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_HOTEL_ROOM_ALLOCATION> GetRoomDetail(string Hotel_Srno)
        {
            Room = Room.Where(String.Format(@"BOOKING_HOTEL_SRNO == {0}", Hotel_Srno));
            return Room.ToList();
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateHotel(ArrayList Hotel)
        {
            HotelRoomAllocation objHotel = new HotelRoomAllocation();
            Hotel.Insert(20, Session["empid"].ToString());
            objHotel.InsertUpdateHotel(Hotel);
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateRoom(ArrayList Room)
        {
            HotelRoomAllocation objHotel = new HotelRoomAllocation();
            objHotel.InsertUpdateRoomAllocation(Room);
        }

        [WebMethod(EnableSession = true)]
        public void InsertNewHotel(String Tour_id, String Booking_Detail_id)
        {
            HotelRoomAllocation objHotel = new HotelRoomAllocation();
            objHotel.InsertNewHotel(Convert.ToInt32(Tour_id), Convert.ToInt32(Booking_Detail_id));
        }

        [WebMethod(EnableSession = true)]
        public void InsertHotelRoomAllocation(String booking_id)
        {
            HotelRoomAllocation objHotel = new HotelRoomAllocation();
            objHotel.InsertHotelNOSR(Convert.ToInt32(booking_id));
        }
        [WebMethod(EnableSession = true)]
        public void GenerateHotelWiseRoom(String tour_id)
        {
            HotelRoomAllocation objHotel = new HotelRoomAllocation();
            objHotel.ConsolidateRooms(Convert.ToInt32(tour_id));
        }

        [WebMethod(EnableSession = true)]
        public void FinalizingRooms(String tour_id)
        {
            HotelRoomAllocation objHotel = new HotelRoomAllocation();
            objHotel.Finalize(Convert.ToInt32(tour_id));
        }
    }
}
