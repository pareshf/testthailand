using System;

namespace CRM.Model.Security
{
    public class UserProfileBDto 
    {
        #region Members
        private Int32 m_UserId = 0;
        private String m_UserName = String.Empty;
        private String m_DefaultTheme = String.Empty; 
        #endregion

        #region Properties
        
        #region UserId
        /// <summary>
        /// Gets or sets a value indicating authorised UserId.
        /// </summary>

        public Int32 UserId
        {
            get { return m_UserId; }
            set { m_UserId = value; }
        }
        #endregion

        #region UserName
        /// <summary>
        /// Gets or sets a value indicating authorised UserName.
        /// </summary>

        public String UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }
        #endregion 


        #region DefaultTheme
        /// <summary>
        /// Gets or sets a value indicating authorised UserName.
        /// </summary>

        public String DefaultTheme
        {
            get { return m_DefaultTheme; }
            set { m_DefaultTheme = value; }
        }
        #endregion 

        public int EmployeeId { get; set; }

        public String EmployeeName { get; set; }

        public string EmployeeEmail { get; set; }

        public string EmployeeMobile { get; set; }

        public string Signature { get; set; }

        public string Password { get; set; }

        public string Cust_id { get; set; }

        public string Supplier_id { get; set; }

        public string EmployeeFlag { get; set; }

        public string CompanyName { get; set; }
        #endregion
    }
}