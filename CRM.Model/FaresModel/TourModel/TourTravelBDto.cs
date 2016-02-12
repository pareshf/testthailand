using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.FaresModel.TourModel;
using CRM.Model.Security;

namespace CRM.Model.FaresModel.TourModel
{
    public class TourTravelBDto
    {
        public int tourId { get; set; }
        public int SrNo { get; set; }
        public int TransportModeId { get; set; }
        public int TransportId { get; set; }
        public DateTime DateOfArrival { get; set; }
        public DateTime TimeOfArrival { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime TimeOfDeparture { get; set; }
        public string TransportDetail { get; set; }
        public string FlightNo { get; set; }
        public int PlaceOfDepartureSource { get; set; }
        public int PlaceOfArrivalDestination { get; set; }
        public int NoOfSeats { get; set; }

		public int Branch { get; set; }
		public int BookingRequestTo { get; set; }   
		
		public int SrNoPrice { get; set; }
        public int transporationId { get; set; }
        public int Currency { get; set; }
        public int SeatsFor { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public decimal Gst { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public string Remarks { get; set; }

        
    }
}
