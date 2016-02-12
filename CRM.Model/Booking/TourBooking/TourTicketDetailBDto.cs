using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;
using CRM.Model.Booking.Ticket;

namespace CRM.Model.Booking.TourBooking
{
    public class TourTicketDetailBDto : UserProfileBDto
    {
        public int SerialNo { get; set; }
        public int TourBookingId { get; set; }
        public int TourBookingSerialNo { get; set; }
        public string PnrNo { get; set; }
        public string TicketNo { get; set; }
        public int TitleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfArrival { get; set; }
        public int PlaceFrom { get; set; }
        public int PlaceTo { get; set; }
        public int AirLineId { get; set; }
        public int ClassId { get; set; }
        public string FlightFlyNo { get; set; }
        public string FrequentFlyNo { get; set; }
        public decimal FareAmount { get; set; }
        public decimal MarginAmount { get; set; }
        public bool IsETicket { get; set; }
        public string xmlString { get; set; }
        public TicketBookingHdr TicketBookingHeader { get; set; }	  
		
		public int BranchId { get; set; }
		public int BookingRequestTo { get; set; }
		public int SrNoAiline { get; set; }
		public int ModeOfTravel { get; set; }
		public int CustRelSrNo { get; set; }
		public string DeptPlace { get; set; }
		public string ArrivalPlace { get; set; }



    }
}
