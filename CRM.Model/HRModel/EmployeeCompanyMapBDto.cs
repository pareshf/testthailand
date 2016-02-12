using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Model.HRModel
{
   public  class EmployeeCompanyMapBDto
    {

       public Int32 EmployeeId { get; set; }
       public Int32 CompanyId { get; set; }
       public Int32 ManagerId { get; set; }
       public Int32 DepartmentId { get; set; }
       public Int32 DesignationId { get; set; }

       public DateTime JoiningDate { get; set; }

       public String Company { get; set; }
       public String Manager { get; set; }
       public String Department { get; set; }
       public String Designation { get; set; }

    }
}
