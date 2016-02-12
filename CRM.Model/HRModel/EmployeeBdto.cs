using System;
using CRM.Model.Security;

namespace CRM.Model.HRModel
{
   public  class EmployeeBDto : UserProfileBDto
    {

        public int EmpId { get; set; }
        public int TitleId { get; set; }
        public String TitleName { get; set; }
        public String EmpSurName  { get; set; }
        public String EmpName { get; set; }
        public int DesignationId { get; set; }
        public String Designation { get; set; }
        public int DepartmentId { get; set; }
        public String Department { get; set; }
        public  DateTime DateofBirth { get; set; }
        public int MaritalStatusId{ get; set; }
        public String MaritalStatus { get; set; }
        public int GenderId { get; set; }
        public String Gender{ get; set; }
        public int ManageId { get; set; }
        public String ManageNmae { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public int QualificationId { get; set; }
        public int Qualification { get; set; }
        public DateTime DateofJoin { get; set; }
        public int StatusId { get; set; }
        public String Status { get; set; }

        public byte[] Photo { get; set; }
        public String Phototype { get; set; }

        public String Signature1 { get; set; }
        public String Signature2 { get; set; }
        public String Signature3 { get; set; }


    }
}
