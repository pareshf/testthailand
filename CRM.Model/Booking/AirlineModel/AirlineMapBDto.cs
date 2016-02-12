using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.Booking.AirlineModel
{
   public class AirlineMapBDto
    {
	   public int srNo { get; set; }
       public int AirportId { get; set; }
       public int AirLineID { get; set; }
       public int DestinationCityId { get; set; }
       public string BaggageAllowance  { get; set; }
	   public string Cancellation { get; set; }
	   public string DateChange { get; set; }
	   public string PaymentPolicy { get; set; }
	   public byte[] FaqDocument { get; set; }
	   public string FaqDocType { get; set; }
	   public string FaqDocFileName { get; set; }
	   public byte[] TermsDocument { get; set; }
	   public string TermsDocType { get; set; }
	   public string TermsDocFileName { get; set; }
    }
}
