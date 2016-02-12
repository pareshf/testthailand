using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Inquiry;
using CRM.Model.Security;

namespace CRM.Model.Inquiry
{
  public  class InquiryStatusBDto
    {
        public int InquiryId { get; set; }
        public int InquirySrNo{ get; set; }
		public int FollowUpNo { get; set; }
        public int InquiryFor { get; set; }
        public int statusId { get; set; }
        public int CompitititorId { get; set; }
        public DateTime TravelDate { get; set; }
        public DateTime CallBackDate { get; set; }
        public decimal CompititorPrice { get; set; }                           
        public string OverLap { get; set; }
        public int FrequentFlyerAirlineId { get; set; }
        public string Remarks { get; set; } 
    }
}
