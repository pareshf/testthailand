using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.Booking.Cruise
{
    public class CruiseBDto
    {
        public Int32 CruiseId { get; set; }
        public string SourcePortName { get; set; }
        public string CruiseName { get; set; }
        public string DestinationPortName { get; set; }
        public Int32 SourceCity { get; set; }
        public Int32 DestinationCity { get; set; }
        public DateTime DepTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime SecheduleDate { get; set; }
        public decimal Duration { get; set; }


        public int CurrancyPriceId { get; set; }
        public int Currancy { get; set; }

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
