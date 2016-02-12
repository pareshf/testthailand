using System.Data;

namespace CRM.Model.Security
{
    public class AuthorizationBDto
    {
        #region Member
        private UserProfileBDto m_UserProfile = new UserProfileBDto();
        private int m_UserSelectedCompanyId;
        private int m_UserSelectedRoleId;
        private int m_UserSelectedModuleId;
        private string m_UserSelectedCompanyName;
        private string m_UserSelectedRoleName;
        private string m_UserSelectedModuleName;
        private DataTable m_UserPermission = new DataTable();
        private string m_UserSelectedDepartmentName;
        private int m_UserSelectedDepartmentId;
        #endregion

        #region Properties

        #region UserProfile
        /// <summary>
        /// Gets or sets a value indicating authorised User's profile.
        /// </summary>

        public UserProfileBDto UserProfile
        {
            get { return m_UserProfile; }
            set { m_UserProfile = value; }
        }
        #endregion

        #region UserPermission
        /// <summary>
        /// Gets or sets a value of data table indicating authorised User's permission.
        /// </summary>

        public DataTable UserPermission
        {
            get { return m_UserPermission; }
            set { m_UserPermission = value; }
        }
        #endregion

        #region Program Access by roll
        public bool ProgramReadAccess { get; set; }
        public bool ProgramWriteAccess { get; set; }
        public bool ProgramDeleteAccess { get; set; }
        public bool ProgramPrintAccess { get; set; }
        #endregion

        #endregion

        /// <summary>
        /// Get prefered company id of authenticated user 
        /// </summary>
        public int UserSelectedCompanyId
        {
            get { return m_UserSelectedCompanyId; }
        }

        /// <summary>
        /// Get prefered company name of authenticated user 
        /// </summary>
        public string UserSelectedCompanyName
        {
            get { return m_UserSelectedCompanyName; }
        }

        /// <summary>
        /// Get prefered role id of authenticated user 
        /// </summary>
        public int UserSelectedRoleId
        {
            get { return m_UserSelectedRoleId; }
        }

        /// <summary>
        /// Get prefered role name of authenticated user 
        /// </summary>
        public string UserSelectedRoleName
        {
            get { return m_UserSelectedRoleName; }
        }


        /// <summary>
        /// Get prefered module id of authenticated user 
        /// </summary>
        public int UserSelectedModuleId
        {
            get { return m_UserSelectedModuleId; }
        }

        /// <summary>
        /// Get prefered module name of authenticated user 
        /// </summary>
        public string UserSelectedModuleName
        {
            get { return m_UserSelectedModuleName; }
        }

        /// <summary>
        /// Get prefered department id of authenticated user 
        /// </summary>
        public int UserSelectedDepartmentId
        {
            get { return m_UserSelectedDepartmentId; }
        }

        /// <summary>
        /// Get prefered department name of authenticated user 
        /// </summary>
        public string UserSelectedDepartmentName
        {
            get { return m_UserSelectedDepartmentName; }
        }

        /// <summary>
        /// Set user selected preferences
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="CompanyName"></param>
        /// <param name="RoleId"></param>
        /// <param name="RoleName"></param>
        public void SetUserSelectedPreferences(int CompanyId, string CompanyName, int RoleId, string RoleName,int DeptId,string DeptName)
        {
            this.m_UserSelectedCompanyId = CompanyId;
            this.m_UserSelectedCompanyName = CompanyName;
            this.m_UserSelectedRoleId = RoleId;
            this.m_UserSelectedRoleName = RoleName;
            this.m_UserSelectedDepartmentId = DeptId;
            this.m_UserSelectedDepartmentName = DeptName;
        }

        /// <summary>
        /// Set user selected preferences
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="CompanyName"></param>
        /// <param name="RoleId"></param>
        /// <param name="RoleName"></param>
        public void SetUserSelectedPreferences(DataTable UserPermission, int CompanyId, string CompanyName, int RoleId, string RoleName, int DeptId, string DeptName)
        {
            this.m_UserPermission = UserPermission;
            this.m_UserSelectedCompanyId = CompanyId;
            this.m_UserSelectedCompanyName = CompanyName;
            this.m_UserSelectedRoleId = RoleId;
            this.m_UserSelectedRoleName = RoleName;
            this.m_UserSelectedDepartmentId = DeptId;
            this.m_UserSelectedDepartmentName = DeptName;
        }

        /// <summary>
        /// Set user selected preferences
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="CompanyName"></param>
        /// <param name="RoleId"></param>
        /// <param name="RoleName"></param>
        public void SetUserSelectedPreferences(int CompanyId, string CompanyName, int RoleId, string RoleName, int ModuleId, string ModuleName, int DeptId, string DeptName)
        {
            this.m_UserSelectedCompanyId = CompanyId;
            this.m_UserSelectedCompanyName = CompanyName;
            this.m_UserSelectedRoleId = RoleId;
            this.m_UserSelectedRoleName = RoleName;
            this.m_UserSelectedModuleId = ModuleId;
            this.m_UserSelectedModuleName = ModuleName;
            this.m_UserSelectedDepartmentId = DeptId;
            this.m_UserSelectedDepartmentName = DeptName;
        }



    }
}