using System;

namespace CRM.Model.General
{
    public class AddressBDto
    {
        private Int32 m_AddressTypeId;
        private String m_AddressLine1;
        private String m_AddressLine2;
        private Int32 m_CityId;
        private Int32 m_StateId;
        private Int32 m_CountryId;
        private String m_PinCodeNo;
        private String m_PhoneNo;

        #region AddressTypeId
        /// <summary>
        /// Gets or sets a value indicating AddressTypeId.
        /// </summary>

        public Int32 AddressTypeId
        {
            get { return m_AddressTypeId; }
            set { m_AddressTypeId = value; }
        }
        #endregion

        #region AddressLine1
        /// <summary>
        /// Gets or sets a value indicating AddressLine1.
        /// </summary>

        public String AddressLine1
        {
            get { return m_AddressLine1; }
            set { m_AddressLine1 = value; }
        }
        #endregion

        #region AddressLine2
        /// <summary>
        /// Gets or sets a value indicating AddressLine2.
        /// </summary>

        public String AddressLine2
        {
            get { return m_AddressLine2; }
            set { m_AddressLine2 = value; }
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

        #region PinCodeNo
        /// <summary>
        /// Gets or sets a value indicating PinCodeNo.
        /// </summary>

        public String PinCodeNo
        {
            get { return m_PinCodeNo; }
            set { m_PinCodeNo = value; }
        }
        #endregion

        #region PhoneNo
        /// <summary>
        /// Gets or sets a value indicating PhoneNo.
        /// </summary>

        public String PhoneNo
        {
            get { return m_PhoneNo; }
            set { m_PhoneNo = value; }
        }
        #endregion
    }
}