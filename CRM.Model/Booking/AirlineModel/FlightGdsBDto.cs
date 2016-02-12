using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.Booking.AirlineModel
{
    public class FlightGdsBDto : UserProfileBDto
    {
        public int SerialNo { get; set; }
        public int AirlineId { get; set; }
        public int FlightId { get; set; }
        public string GDSAirportCode { get; set; }
        public string TimeLimit { get; set; }
        public string BaggageAllwance { get; set; }
        public string Cancellation { get; set; }
        public string DateChange { get; set; }
        public string PaymentPolicy { get; set; }
    }
}
