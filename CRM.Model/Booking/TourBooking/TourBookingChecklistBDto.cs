using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.Booking.TourBooking
{
    public class TourBookingChecklistBDto : UserProfileBDto
    {
        public int SerialNo { get; set; }
        public int BookingId { get; set; }
        public int CheckListId { get; set; }
        public bool Answer { get; set; }
        public string Remarks { get; set; }

		public string OutputCheckListId { get; set; }
		public string documentName { get; set; }
		public string DocumentContent { get; set; }

    }
}
