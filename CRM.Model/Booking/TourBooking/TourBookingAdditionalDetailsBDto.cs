using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.Booking.TourBooking
{
    public class TourBookingAdditionalDetailsBDto : UserProfileBDto
    {
       public int BookingId { get; set; }
       public string AdditionalRequestService { get; set; }
       public string PaymentForBalanceAmountMadeBy { get; set; }
       public string DocToProcessVisaFormToBeHandedBy { get; set; }
   }
}
