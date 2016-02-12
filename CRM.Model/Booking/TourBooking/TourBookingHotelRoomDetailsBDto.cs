using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.Booking.TourBooking
{
    public class TourBookingHotelRoomDetailsBDto : UserProfileBDto
    {
        public int SerialNo { get; set; }
        public int TourTypeId { get; set; }
        public int TourId { get; set; }
        public int BookingId { get; set; }
        public int HotelId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string RoonNo { get; set; }
    }
}
