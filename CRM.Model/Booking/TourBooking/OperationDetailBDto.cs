using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Model.Booking.TourBooking
{
    public class OperationDetailBDto
    {
        public int SerialNo { get; set; }
        public int BookingId { get; set; }
        public int TourTypeId { get; set; }
        public int TourId { get; set; }
        public DateTime PassportTakenDate { get; set; }
        public DateTime PhotographTakenDate { get; set; }
        public DateTime DocumentsTakenDate { get; set; }
        public DateTime VisaSentDate { get; set; }
        public DateTime PassportDeliveredBackOnDate { get; set; }
        public DateTime VouchuersGivenDate { get; set; }
        public DateTime ItenaryGivenDate { get; set; }
        public bool IsVisaChecked { get; set; }
    }
}
