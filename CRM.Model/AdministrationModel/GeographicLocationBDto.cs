using System;
using CRM.Model.Security;
namespace CRM.Model.AdministrationModel
{
    public class GeographicLocationBDto
    {
        #region Members
        private Int32 m_CountryId = 0;
        private String m_CountryName = String.Empty;
        private String m_CountryCode = String.Empty;
        private String m_CurrencySymbol = String.Empty;
        private String m_CurrencyName = String.Empty;
        private Int32 m_StateId = 0;
        private String m_StateName = String.Empty;
        private Int32 m_CityId = 0;
        private String m_CityName = String.Empty;
        private UserProfileBDto m_UserProfile = new UserProfileBDto(); 
        #endregion

        #region Properties
        #region CountryId
        /// <summary>
        /// Gets or sets a value indicating CountryId.
        /// </summary>

        public Int32 CountryId
        {
            get { return m_CountryId; }
            set { m_CountryId = value; }
        }
        #endregion

        #region CountryName
        /// <summary>
        /// Gets or sets a value indicating CountryName.
        /// </summary>

        public String CountryName
        {
            get { return m_CountryName; }
            set { m_CountryName = value; }
        }
        #endregion

        #region CountryCode
        /// <summary>
        /// Gets or sets a value indicating CountryCode.
        /// </summary>

        public String CountryCode
        {
            get { return m_CountryCode; }
            set { m_CountryCode = value; }
        }
        #endregion

        #region CurrencySymbol
        /// <summary>
        /// Gets or sets a value indicating CurrencySymbol.
        /// </summary>

        public String CurrencySymbol
        {
            get { return m_CurrencySymbol; }
            set { m_CurrencySymbol = value; }
        }
        #endregion

        #region CurrencyName
        /// <summary>
        /// Gets or sets a value indicating CurrencyName.
        /// </summary>

        public String CurrencyName
        {
            get { return m_CurrencyName; }
            set { m_CurrencyName = value; }
        }
        #endregion

        #region StateId
        /// <summary>
        /// Gets or sets a value indicating StateId.
        /// </summary>

        public Int32 StateId
        {
            get { return m_StateId; }
            set { m_StateId = value; }
        }
        #endregion

        #region StateName
        /// <summary>
        /// Gets or sets a value indicating StateName.
        /// </summary>

        public String StateName
        {
            get { return m_StateName; }
            set { m_StateName = value; }
        }
        #endregion

        #region CityId
        /// <summary>
        /// Gets or sets a value indicating CityId.
        /// </summary>

        public Int32 CityId
        {
            get { return m_CityId; }
            set { m_CityId = value; }
        }
        #endregion

        #region CityName
        /// <summary>
        /// Gets or sets a value indicating CityName.
        /// </summary>

        public String CityName
        {
            get { return m_CityName; }
            set { m_CityName = value; }
        }
        #endregion

        #region UserProfile
        /// <summary>
        /// Gets or sets a value indicating UserProfile.
        /// </summary>

        public UserProfileBDto UserProfile
        {
            get { return m_UserProfile; }
            set { m_UserProfile = value; }
        }
        #endregion 
        #endregion
    }
}
