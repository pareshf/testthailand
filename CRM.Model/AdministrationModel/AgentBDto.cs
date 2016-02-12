using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.AdministrationModel
{
    public class AgentBDto : UserProfileBDto
    {
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        #region BY SUNIL

        private string CountryName;

        public string CountryName1
        {
            get { return CountryName; }
            set { CountryName = value; }
        }
        private int CtreatedBy;

        public int CtreatedBy1
        {
            get { return CtreatedBy; }
            set { CtreatedBy = value; }
        }
        private int CreatedDate;

        public int CreatedDate1
        {
            get { return CreatedDate; }
            set { CreatedDate = value; }
        }
        private int ModifyBy;

        public int ModifyBy1
        {
            get { return ModifyBy; }
            set { ModifyBy = value; }
        }

        private DateTime ModifyDate;

              public DateTime ModifyDate1
              {
                  get { return ModifyDate; }
                  set { ModifyDate = value; }

              }	


        #endregion

    }
}
