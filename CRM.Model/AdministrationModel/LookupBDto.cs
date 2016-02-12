using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;

namespace CRM.Model.AdministrationModel
{
    public class LookupBDto
    {
        #region Declarations
        private Int32 m_lookupId = 0;
        private String m_lookupName = String.Empty;
        private String m_lookupShortName = String.Empty;
        private String m_description = String.Empty;
        private int m_Int_Order = 0;
        UserProfileBDto m_UserProfile = new UserProfileBDto();

        #endregion

        #region Properties
        #region LookupId

        public Int32 LookupId
        {
            get
            { return m_lookupId; }
            set
            { m_lookupId = value; }
        }

        #endregion

        #region LookupName

        public String LookupName
        {
            get
            { return m_lookupName.Trim(); }
            set
            { m_lookupName = value.Trim(); }
        }

        #endregion

        #region LookupShortName

        public String LookupShortName
        {
            get
            { return m_lookupShortName.Trim(); }
            set
            { m_lookupShortName = value.Trim(); }
        }

        #endregion

        #region Description

        public String Description
        {
            get
            { return m_description.Trim(); }
            set
            { m_description = value.Trim(); }
        }

        #endregion

        #region User Profile

        public UserProfileBDto UserProfile
        {
            get
            { return m_UserProfile; }
            set
            { m_UserProfile = value; }
        }

        #endregion

        #region Int Order

        public Int32 Int_order
        {
            get
            { return m_Int_Order; }
            set
            { m_Int_Order = value; }
        }
        #endregion 


        #endregion


    }
}
