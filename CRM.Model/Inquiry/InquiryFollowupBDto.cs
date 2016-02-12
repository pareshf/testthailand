using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Inquiry;
using CRM.Model.Security;

namespace CRM.Model.Inquiry
{
    public class InquiryFollowupBDto
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

        private Int32 m_FollowupForId;
        #region FollowupForId
        /// <summary>
        /// Gets or sets a value indicating FollowupForId.
        /// </summary>

        public Int32 FollowupForId
        {
            get { return m_FollowupForId; }
            set { m_FollowupForId = value; }
        }
        #endregion

        private Int32 m_FollowupFor;
        #region FollowupFor
        /// <summary>
        /// Gets or sets a value indicating FollowupFor.
        /// </summary>

        public Int32 FollowupFor
        {
            get { return m_FollowupFor; }
            set { m_FollowupFor = value; }
        }
        #endregion

        private Int32 m_FollowupNo;
        #region FollowupNo
        /// <summary>
        /// Gets or sets a value indicating FollowupNo.
        /// </summary>

        public Int32 FollowupNo
        {
            get { return m_FollowupNo; }
            set { m_FollowupNo = value; }
        }
        #endregion

        private DateTime m_AskedFollowupDate;
        #region AskedFollowupDate
        /// <summary>
        /// Gets or sets a value indicating AskedFollowupDate.
        /// </summary>

        public DateTime AskedFollowupDate
        {
            get { return m_AskedFollowupDate; }
            set { m_AskedFollowupDate = value; }
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

        private DateTime m_FollowupDate;
        #region FollowupDate
        /// <summary>
        /// Gets or sets a value indicating FollowupDate.
        /// </summary>

        public DateTime FollowupDate
        {
            get { return m_FollowupDate; }
            set { m_FollowupDate = value; }
        }
        #endregion

        private String m_FollowupDescription;
        #region FollowupDescription
        /// <summary>
        /// Gets or sets a value indicating FollowupDescription.
        /// </summary>

        public String FollowupDescription
        {
            get { return m_FollowupDescription; }
            set { m_FollowupDescription = value; }
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

        private Int32 m_FollowupModeId;
        #region FollowupModeId
        /// <summary>
        /// Gets or sets a value indicating FollowupModeId.
        /// </summary>

        public Int32 FollowupModeId
        {
            get { return m_FollowupModeId; }
            set { m_FollowupModeId = value; }
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

        private Int32 m_FollowupStatusId;
        #region FollowupStatusId
        /// <summary>
        /// Gets or sets a value indicating FollowupStatusId.
        /// </summary>

        public Int32 FollowupStatusId
        {
            get { return m_FollowupStatusId; }
            set { m_FollowupStatusId = value; }
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

        private Int32 m_UserId;
        #region UserId
        /// <summary>
        /// Gets or sets a value indicating OwnerCompanyId.
        /// </summary>

        public Int32 UserId
        {
            get { return m_UserId; }
            set { m_UserId = value; }
        }
        #endregion

        public int InquirySerialNo { get; set; }

        public bool IsFinished { get; set; }

		public string Tours { get; set; }

    }
}
