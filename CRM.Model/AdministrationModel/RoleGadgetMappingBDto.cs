using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Model.AdministrationModel
{
    public class RoleGadgetMappingBDto
    {
        public int RoleID { get; set; }
        public int ModuleID { get; set; }
        public int GadgetID { get; set; }
        public bool ReadAccess { get; set; }
    }
}
