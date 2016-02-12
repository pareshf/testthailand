using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Model.Inquiry.Gadgets
{
    public class CustomerInquiryGadgetsBDto
    {
        public int InquiryNo { get; set; }
        public int InquiryFor { get; set; }
        public int InquiryStatus { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime InquiryFromDate { get; set; }
        public DateTime InquiryToDate { get; set; }
    }
}
