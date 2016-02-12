using System;
using CRM.Model.Security;

namespace CRM.Model.CustomersModel
{
    public class TravelBDto : UserProfileBDto
    {
        #region Member
        private Int32 m_CustomerId;
        private Int32 m_YearMonth;
        private String m_CountryName;
        private Char m_TravelingType;
        private Int32 m_NoOfPersons;
        private Int32 m_InquiryMode;
        private Int32 m_StatusId;
        private String m_Description;
        private Int32 m_AgentId;
        private Int32 m_TourId;
        private Int32 m_TourTypeId;
        #endregion

        #region Property

        #region Serial No
        /// <summary>
        /// Gets or sets a value indicating Serial number.
        /// </summary>
        public int SerialNo { get; set; }
        #endregion

        #region CustomerId
        /// <summary>
        /// Gets or sets a value indicating CustomerId.
        /// </summary>

        public Int32 CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }
        #endregion

        #region YearMonth
        /// <summary>
        /// Gets or sets a value indicating YearMonth.
        /// </summary>

        public Int32 YearMonth
        {
            get { return m_YearMonth; }
            set { m_YearMonth = value; }
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

        #region TravelingType
        /// <summary>
        /// Gets or sets a value indicating TravelingType.
        /// </summary>

        public Char TravelingType
        {
            get { return m_TravelingType; }
            set { m_TravelingType = value; }
        }
        #endregion

        #region NoOfPersons
        /// <summary>
        /// Gets or sets a value indicating NoOfPersons.
        /// </summary>

        public Int32 NoOfPersons
        {
            get { return m_NoOfPersons; }
            set { m_NoOfPersons = value; }
        }
        #endregion

        #region InquiryMode
        /// <summary>
        /// Gets or sets a value indicating InquiryMode.
        /// </summary>

        public Int32 InquiryMode
        {
            get { return m_InquiryMode; }
            set { m_InquiryMode = value; }
        }
        #endregion

        #region StatusId
        /// <summary>
        /// Gets or sets a value indicating StatusId.
        /// </summary>

        public Int32 StatusId
        {
            get { return m_StatusId; }
            set { m_StatusId = value; }
        }
        #endregion

        #region Description
        /// <summary>
        /// Gets or sets a value indicating Description.
        /// </summary>

        public String Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }
        #endregion

        #region AgentId
        /// <summary>
        /// Gets or sets a value indicating AgentId.
        /// </summary>

        public Int32 AgentId
        {
            get { return m_AgentId; }
            set { m_AgentId = value; }
        }
        #endregion

        #region TourId
        /// <summary>
        /// Gets or sets a value indicating TourId.
        /// </summary>

        public Int32 TourId
        {
            get { return m_TourId; }
            set { m_TourId = value; }
        }
        #endregion 

        #region TourTypeId
        /// <summary>
        /// Gets or sets a value indicating TourTypeId.
        /// </summary>

        public Int32 TourTypeId
        {
            get { return m_TourTypeId; }
            set { m_TourTypeId = value; }
        }
        #endregion

        #endregion
    }
}
