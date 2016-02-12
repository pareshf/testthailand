using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Inquiry;
using CRM.Model.Security;

namespace CRM.Model.Inquiry
{
   public  class InquiryForTicketBDto
    {
        public int InquiryId { get; set; }
        public int SerialNo { get; set; }
        public int TicketType { get; set; }
        public int TravelMode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int FromPlace { get; set; }
        public int ToPlace { get; set; }
        public int PreferredAirline { get; set; }
        public int Via { get; set; }
        public int AirlineClassId { get; set; }
        public string FrequentFlyerNo { get; set; }
        public int FrequentFlyerAirlineId { get; set; }
        public char OneWayOrReturn { get; set; }
        public bool StopAllowed { get; set; }
        public bool HaveValidPassport { get; set; }
        public bool HaveValidVisa { get; set; }
        public int MealType { get; set; }
        public string SpecialRequest { get; set; }
        public string DestinationContactNo { get; set; }
	    public string Remarks { get; set; }
		public int ProductId { get; set; }
		public int TicketTabSrNo { get; set; }
		public int SrNo { get; set; }
	    
    }
}
