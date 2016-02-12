using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.Booking.TourBooking
{
    public class CheckListBDto : UserProfileBDto
    {
        public int CheckListId { get; set; }
        public string CheckListFor { get; set; }
    }
}
