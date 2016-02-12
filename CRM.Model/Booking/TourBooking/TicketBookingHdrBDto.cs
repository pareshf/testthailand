using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.Booking.TourBooking
{
    public class TicketBookingHdrBDto : UserProfileBDto
    {
        public int TicketBookingId { get; set; }
        public int TicketTypeId { get; set; }
        public int ModeOfTravel { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int PlaceFrom { get; set; }
        public int PlaceTo { get; set; }
        public string PnrNo { get; set; }
        public int AirlineId { get; set; }
        public int InquiryId { get; set; }
        public string EmergencyContactPersonName { get; set; }
        public string EmergencyContactPersonAddress { get; set; }
        public string EmergencyPhoneNo { get; set; }
        public string EmergencyMobileNo { get; set; }
        public string EmergencyEmail { get; set; }      

    }
}
