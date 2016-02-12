using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.Booking.TourBooking
{
    public class TourOperationDetailsBDto : UserProfileBDto
    {
        public int SerialNo { get; set; }
        public int TourTypeId { get; set; }
        public int TourId { get; set; }
        public int BookingId { get; set; }
        public DateTime PassportTakenDate { get; set; }
        public DateTime PhotoGraphTakenDate { get; set; }
        public DateTime DocumentTakenDate { get; set; }
        public DateTime SendVisaDate { get; set; }
        public DateTime PassportDeliveredBackOnDate { get; set; }
        public bool IsVisaChecked { get; set; }
        public DateTime VoucherGivenDate { get; set; }
        public DateTime IntenaryGivenDate { get; set; }
    }
}
