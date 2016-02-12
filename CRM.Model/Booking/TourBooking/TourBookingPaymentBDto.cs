using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.Booking.TourBooking
{
	public class TourBookingPaymentBDto : UserProfileBDto
	{
		public int ProductId { get; set; }
		public int BookingId { get; set; }
		public DateTime PaymentDate { get; set; }
		public int SerialNo { get; set; }
		public int CurrencyId { get; set; }
		public int PaymentModeId { get; set; }
		public string RecChqDdNo { get; set; }
		public decimal Amount { get; set; }
		public decimal TokenAmount { get; set; }




		public int BankId { get; set; }
		public string BankBranchName { get; set; }
		public string ReceiptNo { get; set; }
		public DateTime ReceiptDate { get; set; }
		public int ReceiveBy { get; set; }
		public int CurrencyAgentId { get; set; }




		public int SentMode { get; set; }
		public decimal ConversionRate { get; set; }
		public decimal Tax { get; set; }
		public decimal InrAmount { get; set; }
		public string BillNo { get; set; }
		public string InTheNameOf { get; set; }







	}
}
