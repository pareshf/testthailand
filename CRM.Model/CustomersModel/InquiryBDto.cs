using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.CustomersModel
{
    public class InquiryBDto : UserProfileBDto
    {
        private Int32 m_InquiryNo;
        #region InquiryNo
        /// <summary>
        /// Gets or sets a value indicating InquiryNo.
        /// </summary>

        public Int32 InquiryNo
        {
            get { return m_InquiryNo; }
            set { m_InquiryNo = value; }
        }
        #endregion
        
        private Int32 m_CustomerId;
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
        
        private Int32 m_CustomerRelationId;
        #region CustomerRelationId
        /// <summary>
        /// Gets or sets a value indicating CustomerRelationId.
        /// </summary>

        public Int32 CustomerRelationId
        {
            get { return m_CustomerRelationId; }
            set { m_CustomerRelationId = value; }
        }
        #endregion
        
        private Int32 m_CustomerRelationSerialNo;
        #region CustomerRelationSerialNo
        /// <summary>
        /// Gets or sets a value indicating CustomerRelationSerialNo.
        /// </summary>

        public Int32 CustomerRelationSerialNo
        {
            get { return m_CustomerRelationSerialNo; }
            set { m_CustomerRelationSerialNo = value; }
        }
        #endregion
        
        private Int32 m_InquiryForId;
        #region InquiryForId
        /// <summary>
        /// Gets or sets a value indicating InquiryForId.
        /// </summary>

        public Int32 InquiryForId
        {
            get { return m_InquiryForId; }
            set { m_InquiryForId = value; }
        }
        #endregion

        private Int32 m_ReferenceId;
        #region ReferenceId
        /// <summary>
        /// Gets or sets a value indicating ReferenceId.
        /// </summary>

        public Int32 ReferenceId
        {
            get { return m_ReferenceId; }
            set { m_ReferenceId = value; }
        }
        #endregion

        private Int32 m_CustomerReferenceId;
        #region CustomerReferenceId
        /// <summary>
        /// Gets or sets a value indicating CustomerReferenceId.
        /// </summary>

        public Int32 CustomerReferenceId
        {
            get { return m_CustomerReferenceId; }
            set { m_CustomerReferenceId = value; }
        }
        #endregion

        private DateTime m_InquiryDate;
        #region InquiryDate
        /// <summary>
        /// Gets or sets a value indicating InquiryDate.
        /// </summary>

        public DateTime InquiryDate
        {
            get { return m_InquiryDate; }
            set { m_InquiryDate = value; }
        }
        #endregion
        
        private String m_InquiryDescription;
        #region InquiryDescription
        /// <summary>
        /// Gets or sets a value indicating InquiryDescription.
        /// </summary>

        public String InquiryDescription
        {
            get { return m_InquiryDescription; }
            set { m_InquiryDescription = value; }
        }
        #endregion
        
        private Int32 m_SalesPersonId;
        #region SalesPersonId
        /// <summary>
        /// Gets or sets a value indicating SalesPersonId.
        /// </summary>

        public Int32 SalesPersonId
        {
            get { return m_SalesPersonId; }
            set { m_SalesPersonId = value; }
        }
        #endregion
        
        private Int32 m_InquiryModeId;
        #region InquiryModeId
        /// <summary>
        /// Gets or sets a value indicating InquiryModeId.
        /// </summary>

        public Int32 InquiryModeId
        {
            get { return m_InquiryModeId; }
            set { m_InquiryModeId = value; }
        }
        #endregion
        
        private DateTime m_NextFollowupDate;
        #region NextFollowupDate
        /// <summary>
        /// Gets or sets a value indicating NextFollowupDate.
        /// </summary>

        public DateTime NextFollowupDate
        {
            get { return m_NextFollowupDate; }
            set { m_NextFollowupDate = value; }
        }
        #endregion
        
        private String m_InternalRemarks;
        #region InternalRemarks
        /// <summary>
        /// Gets or sets a value indicating InternalRemarks.
        /// </summary>

        public String InternalRemarks
        {
            get { return m_InternalRemarks; }
            set { m_InternalRemarks = value; }
        }
        #endregion
        
        private Int32 m_InquiryStatusId;
        #region InquiryStatusId
        /// <summary>
        /// Gets or sets a value indicating InquiryStatusId. 
        /// </summary>

        public Int32 InquiryStatusId
        {
            get { return m_InquiryStatusId; }
            set { m_InquiryStatusId = value; }
        }
        #endregion
        
        private Int32 m_CurrentInquiryStatusId;
        #region CurrentInquiryStatusId
        /// <summary>
        /// Gets or sets a value indicating CurrentInquiryStatusId.
        /// </summary>

        public Int32 CurrentInquiryStatusId
        {
            get { return m_CurrentInquiryStatusId; }
            set { m_CurrentInquiryStatusId = value; }
        }
        #endregion
        
        private Int32 m_OwnerCompanyId;
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
    }
}