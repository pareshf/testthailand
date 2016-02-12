using System;

namespace CRM.Model.General
{
    public class ContactBDto
    {
        private String m_EmailId;
        private String m_MobileNo;
        private String m_PhoneNo;
        private String m_FaxNo;

        #region EmailId
        /// <summary>
        /// Gets or sets a value indicating EmailId.
        /// </summary>

        public String EmailId
        {
            get { return m_EmailId; }
            set { m_EmailId = value; }
        }
        #endregion

        #region MobileNo
        /// <summary>
        /// Gets or sets a value indicating MobileNo.
        /// </summary>

        public String MobileNo
        {
            get { return m_MobileNo; }
            set { m_MobileNo = value; }
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

        #region FaxNo
        /// <summary>
        /// Gets or sets a value indicating FaxNo.
        /// </summary>

        public String FaxNo
        {
            get { return m_FaxNo; }
            set { m_FaxNo = value; }
        }
        #endregion
    } 
}