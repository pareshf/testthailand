//Bony Patel
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;


/// <summary>
/// Summary description for pro_tbl_users
/// </summary>
/// 
namespace CRM.Model.AdministrationModel
{
    public class UserMasterBDto : UserProfileBDto
    {
        public Int32 m_UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Int32 EmpID { get; set; }
        public Int32 SecurityQusId { get; set; }
        public string SecurityQusAns { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public UserProfileBDto UserProfile { get; set; }

        
    }
}
