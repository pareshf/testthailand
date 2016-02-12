using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.Booking.AirlineModel
{
    public class GdsAirportBDto : UserProfileBDto
    {
        public int AirlineId { get; set; }
        public string GDSAirportCode { get; set; }
        public string GDSPriliminaryBooking { get; set; }
        public decimal TotalFare { get; set; }
        public decimal TotalTaxs { get; set; }
        public DateTime TimeLimit { get; set; }
        public string BaggageAllwance { get; set; }
        public string CancellationPolicy { get; set; }
        public string DatechangePolicy { get; set; }
        public string LocalContactPax { get; set; }
        public string Email { get; set; }
        public string PaymentPolicy { get; set; }
        public byte[] FaqDocument { get; set; }
        public byte[] TermsAndConditionDocument { get; set; }



        public int SrNo { get; set; }
        public int DestinationCity { get; set; }
        public string AirportCode { get; set; }
        public string AirportName { get; set; }

    }
}
