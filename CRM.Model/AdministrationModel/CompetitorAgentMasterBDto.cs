using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.AdministrationModel
{
    public class CompetitorAgentMasterBDto : UserProfileBDto
    {

        public int AgentId { get; set; }
        public String AgentName { get; set; }
        public String AgentAddress { get; set; }

        public int CityId { get; set; }
        public String CityName { get; set; }

        public int StateId { get; set; }
        public String StateName { get; set; }

        public int CountryId { get; set; }
        public String CountryName { get; set; }

        public String Phone { get; set; }

        public int OwnerCompanyId { get; set; }


    }
}
