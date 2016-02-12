using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.Booking.TourBooking
{
    public class TourBookingInquiryHrdBDto : UserProfileBDto
    {
        public int InquiryId { get; set; }
        public DateTime InquiryDate { get; set; }
        public int CustomerId { get; set; }
        public int InquiryFor { get; set; }
        public string InquryDesctiption { get; set; }
        public DateTime ApproxDateToConfirm { get; set; }
        public string PassengerName { get; set; }
        public int NoOfAdult { get; set; }
        public int NoOfChild { get; set; }
        public DateTime ApproxTravelDate { get; set; }
        public DateTime ApporxArrivalDate { get; set; }
        public string PlaceFrom { get; set; }
        public string PlaceTo { get; set; }
        public decimal ApproxBudgetPerPerson { get; set; }
        public decimal ApporxBudgetOfFamily { get; set; }
        public int PreferedAirlineId { get; set; }
        public int AirLineClassId { get; set; }
        public string PurposeOfTravel { get; set; }
        public bool OnWayOrReturn { get; set; }
        public int CountryId { get; set; }
        public bool VisaStatus { get; set; }
        public DateTime VisaValidUpTo { get; set; }
        public string FrequentFlyerNo { get; set; }
        public int FrequentFlyerAirLineId { get; set; }
        public decimal AmountQuoted { get; set; }
        public decimal MarginAmount { get; set; }
    }
}
