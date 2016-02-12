using System;
using CRM.Model.Security;

namespace CRM.Model.Booking.TourBooking
{
    public class TourBookingDetailsBDto : UserProfileBDto
    {
        public int SerialNo { get; set; }
        public int BookingId { get; set; }
		public int CustomerSerialNo { get; set; }
        public int TitleId { get; set; }
		
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public int RelationId { get; set; }
        public string Diet { get; set; }
		public int MealId { get; set; }
        public int RoomTypeId { get; set; }
        public DateTime BirthDate { get; set; }
        public string PassportNo { get; set; }
        public DateTime PassportValidDate { get; set; }
        public DateTime VisaValidDate { get; set; }
        public DateTime DOE { get; set; }
        public string CruiseDeckCabin { get; set; }
        public bool AdultOrChild { get; set; }
		public bool InfantRd { get; set; }
        public bool WithBedOrWithoutBed { get; set; }
        public decimal TotalCost { get; set; }
        public decimal InrCost { get; set; }
        public int ForeignCurrency { get; set; }
        public decimal ForeignCurrencyRate { get; set; }
        public decimal ForeignCurrencyCost { get; set; }
        public string FrequentlyFlyNo { get; set; }
        public int FrequentlyFlyId { get; set; }
		public int infant { get; set; }

		
    }
}
