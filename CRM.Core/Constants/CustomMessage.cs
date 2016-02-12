using System;

namespace CRM.Core.Constants
{
    public static class SuccessMessage
    {
        /// <summary>
        /// Save message for success.
        /// </summary>
        public const String Save = "SaveRecord";

        /// <summary>
        /// Update message for success.
        /// </summary>
        public const String Update = "UpdateRecord";

        /// <summary>
        /// Delete message for success.
        /// </summary>
        public const String Delete = "DeleteRecord";

        /// <summary>
        /// Copy message for success.
        /// </summary>
        public const String Copy = "CopyRecord";

        /// <summary>
        /// Save message for backup.
        /// </summary>
        public const String Backup = "SaveBackup";

        /// <summary>
        /// Send message for Sucessfully.
        /// </summary>
        public const String sms = "SendMessage";
    }

    public static class FailureMessage
    {
        /// <summary>
        /// Save message for failure.
        /// </summary>
        public const String Save = "UnableSaveRecord";

        /// <summary>
        /// Update message for failure.
        /// </summary>
        public const String Update = "UnableUpdateRecord";

        /// <summary>
        /// Delete message for failure.
        /// </summary>
        public const String Delete = "UnableDeleteRecord";

        /// <summary>
        /// Copy message for failure.
        /// </summary>
        public const String Copy = "UnableCopyRecord";

        /// <summary>
        /// Copy message for failure.
        /// </summary>
        public const String Backup = "UnableSaveBackup";

        /// <summary>
        /// Unable send message.
        /// </summary>
        public const String sms = "UnableSendMessage";

        /// <summary>
        /// Exist message.
        /// </summary>
       // public const String ExistRecord = "Record already exists.";        
    }

    public static class GeneralMessage
    {
        /// <summary>
        /// Information message for already existing record.
        /// </summary>
        public const String ExistRecord = "ExistRecord";

        /// <summary> 
        /// Information message for NoRecord foond.
        /// </summary>
        public const String NoRecord = "NoRecord";

        /// <summary>
        /// Alert message for select atleast one record.
        /// </summary>
        public const String AtleastOneRecord = "AtleastOneRecord";

        /// <summary>
        /// Alert message for select only one record.
        /// </summary>
        public const String OnlyOneRecord = "OnlyOneRecord";

        /// <summary>
        ///  Alert message for Delete operation.
        /// </summary>
        public const String DeleteAlert = "DeleteAlert";
    }
}
