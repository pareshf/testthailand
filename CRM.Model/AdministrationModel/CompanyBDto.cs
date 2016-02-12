using System;
using CRM.Model.Security;
namespace CRM.Model.AdministrationModel
{
    public class CompanyBDto : UserProfileBDto
    {
        #region Member Variable

        private Int32 m_CompanyId =0;
        private String m_Type =String.Empty;
        private String m_CompanyName = String.Empty;
        private String m_AddressLine1 = String.Empty;
        private String m_AddressLine2 = String.Empty;
        private Int32 m_CityId = 0;
        private Int32 m_StateId = 0;
        private Int32 m_CountryId = 0;
        private String m_Pincode = String.Empty;
        private String m_Mobile = String.Empty;
        private String m_Phone = String.Empty;
        private String m_Fax = String.Empty;
        private String m_Email = String.Empty;
        private Int32 m_RegionId = 0;
        private Int32 m_ParentCompanyId = 0;
        private String m_ParentCompanyName = String.Empty;
        #endregion

        #region Property
        #region CompanyId
        public Int32 CompanyId
        {
            get { return m_CompanyId; }
            set { m_CompanyId = value; }
        }
        #endregion

        #region CompanyName


        public String CompanyName
        {
            get { return m_CompanyName; }
            set { m_CompanyName = value; }
        }
        #endregion

        #region CompanyType


        public String CompanyType
        {
            get { return m_Type; }
            set { m_Type = value; }
        }
        #endregion

        #region AddressLine1


        public String AddressLine1
        {
            get { return m_AddressLine1; }
            set { m_AddressLine1 = value; }
        }
        #endregion

        #region AddressLine2


        public String AddressLine2
        {
            get { return m_AddressLine2; }
            set { m_AddressLine2 = value; }
        }
        #endregion

        #region CityId
        public Int32 CityId
        {
            get { return m_CityId; }
            set { m_CityId = value; }
        }
        #endregion

        #region StateId
        public Int32 StateId
        {
            get { return m_StateId; }
            set { m_StateId = value; }
        }
        #endregion

        #region CountryId
        public Int32 CountryId
        {
            get { return m_CountryId; }
            set { m_CountryId = value; }
        }
        #endregion

        #region PinCode


        public String Pincode
        {
            get { return m_Pincode; }
            set { m_Pincode = value; }
        }
        #endregion

        #region Mobile
        public String Mobile
        {
            get { return m_Mobile; }
            set { m_Mobile = value; }
        }
        #endregion

        #region Phone


        public String Phone
        {
            get { return m_Phone; }
            set { m_Phone = value; }
        }
        #endregion

        #region FAX


        public String Fax
        {
            get { return m_Fax; }
            set { m_Fax = value; }
        }
        #endregion

        #region Email


        public String Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }
        #endregion

        #region Region Id
        public Int32 RegionId
        {
            get { return m_RegionId; }
            set { m_RegionId = value; }
        }
        #endregion

        #region Parent Company  Id
        public Int32 ParentCompanyId
        {
            get { return m_ParentCompanyId; }
            set { m_ParentCompanyId = value; }
        }
        #endregion

        #region Parent Company  Name
        public String ParentCompanyName
        {
            get { return m_ParentCompanyName; }
            set { m_ParentCompanyName = value; }
        }
        #endregion


        public byte[] Photo { get; set; }
        public String Phototype { get; set; }

        #endregion

      

    }
}
