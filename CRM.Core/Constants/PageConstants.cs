using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Core.Constants
{
    public class PageConstants
    {
        /// <summary>
        /// Constant value indicates Viewstate key value for ItemIndexs.
        /// </summary>
        public const String vsItemIndexes = "ItemIndexes";
        
        /// <summary>
        /// Constant value indicates Session key value for HasTableIndex.
        /// </summary>
        public const String ssnHasTableIndex = "HasTableIndex";

        /// <summary>
        /// Constant value indicates Session key value for authorised user's profile.
        /// </summary>
        public const String ssnUserAuthorization = "USER_AUTHORIZATION";

        public const String SmtpUserName = "SmtpUserName";
        public const String SmtpPassword = "SmtpPassword";
        public const String SmtpHost = "SmtpHost";
        public const String SmtpPort = "SmtpPort";
        public const String FromAddress = "FromAddress";
        public const String FromDisplayName = "FromDisplayName";

        public const String DataBaseServer = "DBServer";
        public const String DataBaseName = "DBName";
        public const String DataBaseUserPassword = "DBUserPass";
        public const String DataBaseUserName = "DBUserName";

        public const String ThemeName = "ThemeName";
    }
}
