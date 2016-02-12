using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.Booking.TourBooking
{
    public class TourBookingHrdBDto : UserProfileBDto
    {
        public int BookingId { get; set; }
        public int TourTypeId { get; set; }
        public int TourId { get; set; }
        public DateTime BookingDate { get; set; }
        public int SalesExecutiveId { get; set; }
        public int BranchId { get; set; }
        public int AgentId { get; set; }
        public string CustomerUniqueId { get; set; }
        public int CustomerId { get; set; }
        public int InquiryId { get; set; }
        public int BoardingFrom { get; set; }
        public int ArrivalTo { get; set; }
        public DateTime DepartureDate { get; set; }
        public string EmergencyPersonName { get; set; }
        public string EmergencyPersonAddress { get; set; }
        public string EmergencyPhoneNo { get; set; }
        public string EmergencyMobileNo { get; set; }
        public string EmergencyEmail { get; set; }
        public int NoOfAdult { get; set; }
        public int NoOfChild { get; set; }
        public int NoOfChildWithBed { get; set; }
        public int NoOfChildWithoutBed { get; set; }
        public decimal AdultCost { get; set; }
        public decimal ChildCostWithBed { get; set; }
        public decimal ChildCostWithoutBed { get; set; }
        public decimal AmountDeposite { get; set; }
        public int ModeOfPayment { get; set; }
        public string ReceiptNoChequeNoDdNo { get; set; }
        public decimal Amount { get; set; }
        public int BankId { get; set; }
        public string BankBranchName { get; set; }
        public decimal BalanceToBePaid { get; set; }
        public decimal MarginAmount { get; set; }
        public bool IsPercent { get; set; }
        public bool IsFinal { get; set; }
        public byte[] AmendmentForm { get; set; }
        public string AmendmentFormContentType { get; set; }
        public string AmendmentFormFileName { get; set; }
    }
}
