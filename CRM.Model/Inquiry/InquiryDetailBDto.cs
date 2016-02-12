using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Inquiry;
using CRM.Model.Security;

namespace CRM.Model.Inquiry
{
  public  class InquiryDetailBDto
    {
        public int InquiryId { get; set; }
        public int InquiryFor { get; set; }
        public string InquiryDesc { get; set; }
        public int NoOfAudults { get; set; }
        public int NoOfChild { get; set; }
        public DateTime ApproaxTravelDate { get; set; }
        public string NoOfINF { get; set; }
        public decimal ApproaxBudgetPerPerson { get; set; }
        public decimal ApproaxBudgetFamily { get; set; }
        public decimal AmountQuoted { get; set; }
        public decimal MarginAmount { get; set; }
        public DateTime ApproaxArrivalDate { get; set; }
        public DateTime NextFollowupDate { get; set; }
        public int InquiryStatusId { get; set; }
        public int SrNo { get; set; }
        public int InqRating {get; set;}
		public int InquiryMode { get; set; }

    }
}
