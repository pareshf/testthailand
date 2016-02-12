using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Inquiry;
using CRM.Model.Security;


namespace CRM.Model.Inquiry
{
    public class NewInquiryBDto
    {
        public string CustomerUniqueId { get; set; }
        public int CustomerId { get; set; }
        public int RelationId { get; set; }
        public string RelativeName { get; set; }
        public int SalesPersonId { get; set; }
        public int InquiryMode { get; set; }
        public DateTime InquiryDate { get; set; }
		public DateTime InquiryToDate { get; set; }

        public int ReferenceId { get; set; }
        public int Branch { get; set; }
        public string ReferenceDesc { get; set; }
        public int AgentId { get; set; }
        public int Rating { get; set; }
        public string Remarks { get; set; }
        public int CurrentStatus { get; set; }
        public string CustomerName { get; set; }
        public int InquiryId { get; set; }
        public int TourSubType { get; set; }
        private DateTime TravelTODate;

        public DateTime TravelTODate1
        {
            get { return TravelTODate; }
            set { TravelTODate = value; }
        }
        private DateTime TravelFromDate;

        public DateTime TravelFromDate1
        {
            get { return TravelFromDate; }
            set { TravelFromDate = value; }
        } 
        private Int32 m_CountryId;

        public Int32 CountryId
        {
            get { return m_CountryId; }
            set { m_CountryId = value; }

        }
		private Int32 CountryId2;

		public Int32 CountryIdnw
		{
			get { return CountryId2; }
			set { CountryId2 = value; }

		}
		private Int32 StateId1;

		public Int32 StateId
		{
			get { return StateId1; }
			set { StateId1 = value; }

		}

		private Int32 RegionId;

		public Int32 RegionId1
		{
			get { return RegionId; }
			set { RegionId = value; }
		}
		


    }
}
