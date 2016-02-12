using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Model.AdministrationModel
{
    public class RoleAccessMappingBDto
    {
        public int RoleID { get; set; }
        public int ModuleID { get; set; }
        public int ProgramID { get; set; }
        public bool ReadAccess { get; set; }
        public bool WriteAccess { get; set; }
        public bool DeleteAccess { get; set; }
        public bool PrintAccess { get; set; }
    }
}
