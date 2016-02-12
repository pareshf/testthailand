using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Inquiry;
using CRM.Model.Security;
using CRM.Model.Booking.AirlineModel;

namespace CRM.Model.Inquiry
{
    public class InquiryGRDMappingBDto
    {
        public int SrNo { get; set; }
        public int InquiryId { get; set; }
        public int InquirySerialNo { get; set; }
		public int ProductId { get; set; }
        public int AirlinesId { get; set; }
        public string GDSAirportCode { get; set; }
        public int FlightId { get; set; }
        public string TimeDetails { get; set; }
        public string TimeLimit { get; set; }
        public string BaggageAllwance { get; set; }
        public string Cancellation { get; set; }
        public string DateChange { get; set; }
        public string PaymentPolicy { get; set; }
        public decimal TotalFareWithTax { get; set; }
        public string PaxNo { get; set; }
        public string Email { get; set; }
        public byte[] FaqDocument { get; set; }
        public string FaqDocType { get; set; }
        public string FaqDocFileName { get; set; }
        public byte[] TermsDocument { get; set; }
        public string TermsDocType { get; set; }
        public string TermsDocFileName { get; set; }
        public int PreferedAirlinesId { get; set; }
		public int TicketTabId { get; set; }
		public int TicketInqSrNo { get; set; }
		public int DestinationCityId { get; set; }
		public int SerialNo { get; set; }




		public int CurrencyId { get; set; }
		public decimal QuotedAmtAdult { get; set; }
		public decimal TaxAdult { get; set; }
		public decimal GstAdult { get; set; }

		public decimal QuotedAmtChildWithBed { get; set; }
		public decimal TaxChildWitBed { get; set; }
		public decimal GstChildWitBed { get; set; }


		public decimal QuotedAmtInfant { get; set; }
		public decimal TaxInfant { get; set; }
		public decimal GstInfant { get; set; }

    }
}
