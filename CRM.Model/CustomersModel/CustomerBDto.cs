using System;
using CRM.Model.Security;
using CRM.Model.General;
namespace CRM.Model.CustomersModel
{
    public class CustomerBDto : UserProfileBDto
    {
        #region Member Variable
        private Int32 m_CustomerId;
        private Int32 m_TitleId;
        private String m_SurName;
        private String m_Name;
        private String m_Profile;
        private Int32 m_CodeId;
        private Int32 m_RelationId;
        private Int32 m_TypeId; 
        private String m_CompanyName;
        private Int32 m_ProfessionId;
        private Decimal m_AnnualIncome;
        private GeneralInfoBDto m_GeneralInfo;
        private AddressBDto m_AddressInfo;
        private String m_XmlData;
        private ContactBDto m_ContactInfo;
        private PassportBDto m_PassPortInfo;
        private String m_RelationState;
        private Int32 m_OwnerCompanyId;
        #endregion

        #region Property

        /// <summary>
        /// Gets or sets a value indicating SerialNo.
        /// </summary>
        public int SerialNo { get; set; }

        /// <summary>
        /// Gets or sets a value initial part of customer unique code.
        /// </summary>
        public string UniqueId { get; set; }

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

        #region RelationId
        /// <summary>
        /// Gets or sets a value indicating RelationId.
        /// </summary>

        public Int32 RelationId
        {
            get { return m_RelationId; }
            set { m_RelationId = value; }
        }
        #endregion

        #region TypeId
        /// <summary>
        /// Gets or sets a value indicating TypeId.
        /// </summary>

        public Int32 TypeId
        {
            get { return m_TypeId; }
            set { m_TypeId = value; }
        }
        #endregion

        #region TitleId
        /// <summary>
        /// Gets or sets a value indicating TitleId.
        /// </summary>

        public Int32 TitleId
        {
            get { return m_TitleId; }
            set { m_TitleId = value; }
        }
        #endregion

        #region SurName
        /// <summary>
        /// Gets or sets a value indicating SurName.
        /// </summary>

        public String SurName
        {
            get { return m_SurName; }
            set { m_SurName = value; }
        }
        #endregion

        #region Name
        /// <summary>
        /// Gets or sets a value indicating Name.
        /// </summary>

        public String Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        #endregion

        #region Profile
        /// <summary>
        /// Gets or sets a value indicating Profile.
        /// </summary>

        public String Profile
        {
            get { return m_Profile; }
            set { m_Profile = value; }
        }
        #endregion

        #region ProfessionId
        /// <summary>
        /// Gets or sets a value indicating ProfessionId.
        /// </summary>

        public Int32 ProfessionId
        {
            get { return m_ProfessionId; }
            set { m_ProfessionId = value; }
        }
        #endregion

        #region CodeId
        /// <summary>
        /// Gets or sets a value indicating CodeId.
        /// </summary>

        public Int32 CodeId
        {
            get { return m_CodeId; }
            set { m_CodeId = value; }
        }
        #endregion

        #region CompanyName
        /// <summary>
        /// Gets or sets a value indicating CompanyName.
        /// </summary>

        public String CompanyName
        {
            get { return m_CompanyName; }
            set { m_CompanyName = value; }
        }
        #endregion 

        #region AnnualIncome
        /// <summary>
        /// Gets or sets a value indicating AnnualIncome.
        /// </summary>

        public Decimal AnnualIncome
        {
            get { return m_AnnualIncome; }
            set { m_AnnualIncome = value; }
        }
        #endregion

        #region RelationState
        /// <summary>
        /// Gets or sets a value indicating RelationState.
        /// </summary>

        public String RelationState
        {
            get { return m_RelationState; }
            set { m_RelationState = value; }
        }
        #endregion

        #region GeneralInfo
        /// <summary>
        /// Gets or sets a value indicating GeneralInfo.
        /// </summary>

        public GeneralInfoBDto GeneralInfo
        {
            get { return m_GeneralInfo; }
            set { m_GeneralInfo = value; }
        }
        #endregion

        #region AddressInfo
        /// <summary>
        /// Gets or sets a value indicating AddressInfo.
        /// </summary>

        public AddressBDto AddressInfo
        {
            get { return m_AddressInfo; }
            set { m_AddressInfo = value; }
        }
        #endregion
        
        #region XmlData
        /// <summary>
        /// Gets or sets a value indicating XmlData.
        /// </summary>

        public String XmlData
        {
            get { return m_XmlData; }
            set { m_XmlData = value; }
        }
        #endregion

        #region ContactInfo
        /// <summary>
        /// Gets or sets a value indicating ContactInfo.
        /// </summary>

        public ContactBDto ContactInfo
        {
            get { return m_ContactInfo; }
            set { m_ContactInfo = value; }
        }
        #endregion

        #region PassPortInfo
        /// <summary>
        /// Gets or sets a value indicating PassPortInfo.
        /// </summary>

        public PassportBDto PassPortInfo
        {
            get { return m_PassPortInfo; }
            set { m_PassPortInfo = value; }
        }
        #endregion

        #region OwnerCompanyId
        /// <summary>
        /// Gets or sets a value indicating OwnerCompanyId.
        /// </summary>

        public Int32 OwnerCompanyId
        {
            get { return m_OwnerCompanyId; }
            set { m_OwnerCompanyId = value; }
        }
        #endregion

        public Byte[] CustomerPhoto { get; set; }

        public String CustomerPhotoType { get; set; }

        public Boolean IsPhotoChanged { get; set; }

        public String  Remarks { get; set; }

        public string PrefAirlineXmlData { get; set; }

        public string VisaXmlData { get; set; }

        #endregion
    }
}
 