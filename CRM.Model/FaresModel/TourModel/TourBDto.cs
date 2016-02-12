using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.FaresModel.TourModel;
using CRM.Model.Security;

namespace CRM.Model.FaresModel.TourModel
{
    public class TourBDto : UserProfileBDto
    {
        public int TourId { get; set; }
		public int BaseTourId { get; set; }
        public int CurrencyPriceId { get; set; }
        public int TourItenaryId { get; set; }
        public string TourCode { get; set; }
        public string TourSortName { get; set; }
        public string TourLongName { get; set; }
        public int TourTypeId { get; set; }
        public DateTime TourFormDate { get; set; }
        public DateTime TourToDate { get; set; }
        public int NoOfDays { get; set; }
        public int TourCost { get; set; }
        public int NoOfSeats { get; set; }
        public int NoOfNights { get; set; }
        public int TourCostAdult { get; set; }
        public int TourCostChildWithBed { get; set; }
        public int TourCostChildWithoutBed { get; set; }
        public int InfantPrice { get; set; }
        public int TourCurrancy { get; set; }
        public string CountriesForVisa { get; set; }
        public string CityToTravel { get; set; }
        public string StartEndCity { get; set; }
        public string TourInterDomestic { get; set; }
        public byte[] TourHighlight { get; set; }
        public byte[] ImportantNotes { get; set; }
        public byte[] TermsAndConditions { get; set; }
        public byte[] PaymentTerms { get; set; }
        public byte[] CancellationCharges { get; set; }
        public byte[] OptionalSiteSeeingCost { get; set; }
        public byte[] Usp { get; set; }
        public byte[] Limitations { get; set; }
        public byte[] CompetitorsComparison { get; set; }
        public byte[] FinalIternary { get; set; }
        public byte[] TourItenary { get; set; }
        public byte[] FinalTermsConditions { get; set; }
        public byte[] FinalVouchures { get; set; }
        public byte[] WebPhoto { get; set; }
        public byte[] DestinationDetails { get; set; }
        public byte[] GeneralGuideLines { get; set; }
        public byte[] Brochure1 { get; set; }
        public byte[] Brochure2 { get; set; }
        public byte[] Brochure3 { get; set; }



        public string TourHighlightContentType { get; set; }
        public string ImportantNotesContentType { get; set; }
        public string TermsAndConditionsContentType { get; set; }
        public string PaymentTermsContentType { get; set; }
        public string CancellationChargesContentType { get; set; }
        public string OptionalSiteSeeingCostContentType { get; set; }
        public string UspContentType { get; set; }
        public string LimitationsContentType { get; set; }
        public string CompetitorsComparisonContentType { get; set; }
        public string FinalIternaryContentType { get; set; }
        public string TourItenaryContentType { get; set; }
        public string FinalTermsConditionsContentType { get; set; }
        public string FinalVouchuresContentType { get; set; }
        public string WebPhotoContent { get; set; }
        public string DestinationDetailsContentType { get; set; }
        public string GeneralGuideLinesContentType { get; set; }
        public string Brochure1ContentType { get; set; }
        public string Brochure2ContentType { get; set; }
        public string Brochure3ContentType { get; set; }




        public string TourHighlightFileName { get; set; }
        public string ImportantNotesFileName { get; set; }
        public string TermsAndConditionsFileName { get; set; }
        public string PaymentTermsFileName { get; set; }
        public string CancellationChargesFileName { get; set; }
        public string OptionalSiteSeeingCostFileName { get; set; }
        public string UspFileName { get; set; }
        public string LimitationsFileName { get; set; }
        public string CompetitorsComparisonFileName { get; set; }
        public string FinalIternaryFileName { get; set; }
        public string TourItenaryFileName { get; set; }
        public string FinalTermsConditionsFileName { get; set; }
        public string FinalVouchuresFileName { get; set; }
        public string DestinationDetailsFileName { get; set; }
        public string GeneralGuideLinesFileName { get; set; }
        public string Brochure1FileName { get; set; }
        public string Brochure2FileName { get; set; }
        public string Brochure3FileName { get; set; } 
        public bool IsTourPhotoUpdate { get; set; }


    }
}
