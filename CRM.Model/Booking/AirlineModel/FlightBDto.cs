using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.Booking.AirlineModel
{
    public class FlightBDto : UserProfileBDto
    {
        public int AirlineId { get; set; }
        
        public DateTime EffectiveFrom { get; set; }
        public string FlightNo { get; set; }
        public int SourceCity { get; set; }
        public int DestinationCity { get; set; }
        public DateTime TimeOfArrival { get; set; }
        public DateTime TimeOfDeparture { get; set; }
        public int DaysInOperation { get; set; }
        public int Currancy { get; set; }
        public int TotalFare { get; set; }
        public Decimal Tax { get; set; }
        public Decimal GST { get; set; }
        public Boolean IsStop { get; set; }
        public Boolean IsDisplayonweb { get; set; }
        public string Via { get; set; }
        public Decimal Duration { get; set; }



        public int CurrancyPriceId { get; set; }
        public int FlightId { get; set; }      
        public Decimal Adult_Amt { get; set; }
        public Decimal Adult_Tax { get; set; }
        public Decimal Adult_Gst { get; set; }
        public Decimal Child_Amt { get; set; }
        public Decimal Child_Tax { get; set; }
        public Decimal Child_Gst { get; set; }
        public Decimal Infant_Amt { get; set; }
        public Decimal Infant_Tax { get; set; }
        public Decimal Infant_Gst { get; set; }
    }
}
