using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.Booking.Ticket 
{
    public class TicketBookingHdr : UserProfileBDto
    {
        public int TourBookingId { get; set; }
        public int TicketBookingId { get; set; }
        public int TicketTypeId { get; set; }
        public int TravelModeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int PlaceFrom { get; set; }
        public int PlaceTo { get; set; }
        public string PnrNo { get; set; }
        public int AirlineId { get; set; }
        public int InquiryId { get; set; }
        public string EmergencyPersonName { get; set; }
        public string EmergencyPersonAddress { get; set; }
        public string EmergencyPhoneNo { get; set; }
        public string EmergencyMobileNo { get; set; }
        public string EmergencyEmail { get; set; }
    }
}
