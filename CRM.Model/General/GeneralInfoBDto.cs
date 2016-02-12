using System;

namespace CRM.Model.General
{
    public class GeneralInfoBDto
    {
        private char m_Gender;
        private DateTime m_BirthDate;
        private Int32 m_ReligionId;
        private Int32 m_MarriageStatusId;
        private DateTime m_MarriageDate;
         
        #region Gender
        /// <summary>
        /// Gets or sets a value indicating Gender.
        /// </summary>

        public char Gender
        {
            get { return m_Gender; }
            set { m_Gender = value; }
        }
        #endregion

        #region BirthDate
        /// <summary>
        /// Gets or sets a value indicating BirthDate.
        /// </summary>

        public DateTime BirthDate
        {
            get { return m_BirthDate; }
            set { m_BirthDate = value; }
        }
        #endregion

        #region ReligionId
        /// <summary>
        /// Gets or sets a value indicating ReligionId.
        /// </summary>

        public Int32 ReligionId
        {
            get { return m_ReligionId; }
            set { m_ReligionId = value; }
        }
        #endregion

        #region MarriageStatusId
        /// <summary>
        /// Gets or sets a value indicating MarriageStatusId.
        /// </summary>

        public Int32 MarriageStatusId
        {
            get { return m_MarriageStatusId; }
            set { m_MarriageStatusId = value; }
        }
        #endregion

        #region MarriageDate
        /// <summary>
        /// Gets or sets a value indicating MarriageDate.
        /// </summary>

        public DateTime MarriageDate
        {
            get { return m_MarriageDate; }
            set { m_MarriageDate = value; }
        }
        #endregion
    }
}
