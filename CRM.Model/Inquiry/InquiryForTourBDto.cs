using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Inquiry;
using CRM.Model.Security;
namespace CRM.Model.Inquiry
{  
    public class InquiryForTourBDto
    {
        public int InquiryId { get; set; }
		public int ProductId { get; set; }
		public int TourInquirySerialNo { get; set; }
        public int SerialNo { get; set; }
        public int TourTypeId { get; set; }
        public int TourId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int FromPlace { get; set; }
        public int ToPlace { get; set; }
        public int Country { get; set; }
        public int Region { get; set; }
        public bool HaveValidPassport { get; set; }
        public bool HaveValidVisa { get; set; }       
        public char StandardOrCustomized { get; set; }                     
        public string Remarks { get; set; }
        public byte[] Attachment1 { get; set; }
        public string Attachment1ContentType { get; set; }
        public string Attachment1FileName { get; set; }
        public decimal MarginAmount { get; set; }
        public byte[] Attachment2 { get; set; }
        public string Attachment2ContentType { get; set; }
        public string Attachment2FileName { get; set; }

        public int CurrencyId { get; set; }
        public decimal QuotedAmtAdult { get; set; }
        public decimal TaxAdult { get; set; }
        public decimal GstAdult { get; set; }

        public decimal QuotedAmtChildWithBed { get; set; }
        public decimal TaxChildWitBed { get; set; }
        public decimal GstChildWitBed { get; set; }

        public decimal QuotedAmtChildWithoutBed { get; set; }
        public decimal TaxChildWitoutBed { get; set; }
        public decimal GstChildWitoutBed { get; set; }

        public decimal QuotedAmtInfant { get; set; }
        public decimal TaxInfant { get; set; }
        public decimal GstInfant { get; set; }

    }
}
