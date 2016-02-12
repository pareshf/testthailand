using System;
using CRM.Model.Security;
namespace CRM.Model.AdministrationModel
{
  public   class CompanyContactBDto : UserProfileBDto
    {

        public String Name { get; set; }
        public int CompanyId { get; set; }
        public int SrNo { get; set; }
        public int TitleId { get; set; }

        public String Email { get; set; }
        public String Mobile { get; set; }
        public String Phone { get; set; }



    }
}
