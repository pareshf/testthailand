using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.Booking.TourBooking
{
    public class CheckListMasterDetailsBDto : UserProfileBDto
    {
        public int SerialNo { get; set; }
        public int CheckListId { get; set; }
        public string Description { get; set; }
    }
}
