using System;
using CRM.Model.Security;
namespace CRM.Model.HRModel
{
   public class EmployeeContactBDto : UserProfileBDto
        {


       public int EmployeeId { get; set; }
       public String EmployeeName { get; set; }
        public String ContactTypeName { get; set; }
        public int ContactTypeId { get; set; }
        public int SrNo { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public int CountryId { get; set; }

        public String CountryName { get; set; }
        public String  StateName { get; set; }
        public int StateId { get; set; }

        public String CityName { get; set; }
        public int CityId { get; set; }

         public String Phone { get; set; }
         public String Pincode { get; set; }




      }
}
