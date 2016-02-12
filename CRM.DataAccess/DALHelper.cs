using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess
{
    public static class DALHelper
    {
        #region Connection Sting Constant
        public const String CRM_CONNECTION_STRING = "CRM";
        #endregion

        #region Constants DAL_EXP_POLICYNAME
        public const String DAL_EXP_POLICYNAME = "DAL Exception Policy";
        #endregion

        /****************************** Useraster Procedures Constant - Bony ************************/

        #region User Master Sps
        public const String USP_ADMINISTRATION_USER_INSERT = "[dbo].[USP_ADMINISTRATION_USER_INSERT]";
        public const String USP_ADMINISTRATION_USER_SELECT = "[dbo].[USP_ADMINISTRATION_USER_SELECT]";
        public const String USP_ADMINISTRATION_USER_DYNAMIC_SELECT = "[dbo].[USP_ADMINISTRATION_USER_DYNAMIC_SELECT]";
        public const String USP_ADMINISTRATION_USER_UPDATE = "[dbo].[USP_ADMINISTRATION_USER_UPDATE]";
        public const String USP_ADMINISTRATION_USER_DELETE = "[dbo].[USP_ADMINISTRATION_USER_DELETE]";
        public const String USP_EMPLOYEE_SELECT_KEYVALUE = "[dbo].[USP_EMPLOYEE_SELECT_KEYVALUE]";
        public const String USP_ADMINISTRATION_COMPANY_SELECT_BY_EMPLOYEE_ID = "[dbo].[USP_ADMINISTRATION_COMPANY_SELECT_BY_EMPLOYEE_ID]";
        public const String USP_USER_SECURITY_QUESTION_KEYVALUE = "[dbo].[USP_USER_SECURITY_QUESTION_KEYVALUE]";
        public const String USP_USER_COMPANY_FRANCHIES_MAPPING_INSERT = "[dbo].[USP_USER_COMPANY_FRANCHIES_MAPPING_INSERT]";
        public const String USP_USER_COMPANY_FRANCHIES_MAPPING_DELETE = "[dbo].[USP_USER_COMPANY_FRANCHIES_MAPPING_DELETE]";
        public const String USP_USER_COMPANY_FRANCHIES_MAPPING_SELECT_BYUSERID = "[dbo].[USP_USER_COMPANY_FRANCHIES_MAPPING_SELECT_BYUSERID]";
        public const String USP_USER_ROLE_MAPPING_INSERT = "[dbo].[USP_USER_ROLE_MAPPING_INSERT]";
        public const String USP_USER_ROLE_MAPPING_DELETE = "[dbo].[USP_USER_ROLE_MAPPING_DELETE]";
        public const String USP_USER_ROLE_MAPPING_SELECT_ASSIGNED = "[dbo].[USP_USER_ROLE_MAPPING_SELECT_ASSIGNED]";

        #endregion

        #region Role Access Mapping
        public const String USP_ADMINISTRATION_MODULE_MASTER_KEYVALUE = "[dbo].[USP_ADMINISTRATION_MODULE_MASTER_KEYVALUE]";
        public const String USP_ADMINISTRATION_MODULE_MASTER_SELECT_BYROLE = "[dbo].[USP_ADMINISTRATION_MODULE_MASTER_SELECT_BYROLE]";
        public const String USP_ROLE_ACCESS_MAPPING_SELECT_BYROLE = "[dbo].[USP_ROLE_ACCESS_MAPPING_SELECT_BYROLE]";
        public const String USP_ROLE_ACCESS_MAPPING_INSERT = "[dbo].[USP_ROLE_ACCESS_MAPPING_INSERT]";
        public const String USP_ROLE_ACCESS_MAPPING_DELETE = "[dbo].[USP_ROLE_ACCESS_MAPPING_DELETE]";
        public const String USP_ROLE_ACCESS_MAPPING_SELECT_BY_PROGRAM_NAME = "[dbo].[USP_ROLE_ACCESS_MAPPING_SELECT_BY_PROGRAM_NAME]";
        #endregion

        #region Role Gadget mapping
        public const String USP_ROLE_GADGET_MAPPING_SELECT_BYROLE = "[dbo].[USP_ROLE_GADGET_MAPPING_SELECT_BYROLE]";
        public const String USP_GADGET_ACCESS_MAPPING_INSERT = "[dbo].[USP_GADGET_ACCESS_MAPPING_INSERT]";
        #endregion

        #region Navigation Panel (Menu Item By Role Id)
        public const String USP_ROLE_ACCESS_MAPPING_SELECT_LEFTMENU = "[dbo].[USP_ROLE_ACCESS_MAPPING_SELECT_LEFTMENU]";
        #endregion

        #region  Module Panel (Module Name By Role Id)
        public const String USP_ROLE_ACCESS_MAIN_MODULE_SELECT_BYROLE = "[dbo].[USP_ROLE_ACCESS_MAIN_MODULE_SELECT_BYROLE]";
        #endregion

        #region  Preference grid (By user Id)
        public const String USP_USER_ROLE_MAPPING_SELECT_BY_USERID = "[dbo].[USP_USER_ROLE_MAPPING_SELECT_BY_USERID]";
        #endregion

        #region  User Login Log
        public const String USP_SYS_USER_LOGIN_LOG_INSERT = "[dbo].[USP_SYS_USER_LOGIN_LOG_INSERT]";
        #endregion
		#region
		public static string USP_SYS_ONLINE_USER = "[dbo].[USP_SYS_ONLINE_USER]";
		public static string USP_SYS_SIGNOUT_USER = "[dbo].[USP_SYS_SIGNOUT_USER]";
        public static string USP_SYS_GET_USERTYPE = "[dbo].[USP_SYS_GET_USERTYPE]";
		
		#endregion

		#region  Gadgets

		public const String USP_SYS_USER_DASHBOARD_PERSONALIZATION_SELECT = "[dbo].[USP_SYS_USER_DASHBOARD_PERSONALIZATION_SELECT]";
        public const String USP_SYS_USER_DASHBOARD_PERSONALIZATION_INSERT = "[dbo].[USP_SYS_USER_DASHBOARD_PERSONALIZATION_INSERT]";
        //Myportal (fetch user log)
        public const String USP_SYS_USER_LOGIN_LOG_SELECT = "[dbo].[USP_SYS_USER_LOGIN_LOG_SELECT]";
        //Inquiry Followups
        public const String USP_CUST_CUSTOMER_INQURIES_GADGETS_SELECT = "[dbo].[USP_CUST_CUSTOMER_INQURIES_GADGETS_SELECT]";
        //Today Inquiry Followups by Next_Inquiry_followup_date
        public const String USP_CUST_CUSTOMER_INQURIES_GADGETS_SELECT_BYDATE = "[dbo].[USP_CUST_CUSTOMER_INQURIES_GADGETS_SELECT_BYDATE]";
        //Staff wise total Inquiry Followups by Company and Department
        public const String USP_CUST_CUSTOMER_INQURIES_GADGETS_BYSTAFF_SELECT = "[dbo].[USP_CUST_CUSTOMER_INQURIES_GADGETS_BYSTAFF_SELECT]";
        //Staff wise total Inquiry Followups by Company
        public const String USP_CUST_CUSTOMER_INQURIES_GADGETS_SELECT_BYCOMPANY = "[dbo].[USP_CUST_CUSTOMER_INQURIES_GADGETS_SELECT_BYCOMPANY]";
        //Team wise total Inquiry By Company Manager
        public const String USP_CUST_CUSTOMER_INQURIES_GADGETS_BYMANAGER_SELECT = "[dbo].[USP_CUST_CUSTOMER_INQURIES_GADGETS_BYMANAGER_SELECT]";
        #endregion

        #region NewEnquiry
        public const String USP_CUST_CUSTOMER_PROFILE_BY_CUSTID_SELECT = "[dbo].[USP_CUST_CUSTOMER_PROFILE_BY_CUSTID_SELECT]";
        #endregion

        #region Backup
        public const String USP_SYS_BACKUP_INSERT = "[dbo].[USP_SYS_BACKUP_INSERT]";
        #endregion

        #region Inquiry - Phase-2 - NEW
        public const String USP_CUST_CUSTOMER_INQURIES_INSERT = "[dbo].[USP_CUST_CUSTOMER_INQURIES_INSERT]";
        public const String USP_CUST_CUSTOMER_INQURIES_SELECT = "[dbo].[USP_CUST_CUSTOMER_INQURIES_SELECT]";
        public const String USP_CUST_CUSTOMER_INQURIES_UPDATE = "[dbo].[USP_CUST_CUSTOMER_INQURIES_UPDATE]";
        public const String USP_CUST_CUSTOMER_INQURIES_DELETE = "[dbo].[USP_CUST_CUSTOMER_INQURIES_DELETE]";


        public const String USP_INQ_INQURIES_TOUR_INSERT = "[dbo].[USP_INQ_INQURIES_TOUR_INSERT]";
        public const String USP_INQ_INQURIES_TOUR_UPDATE = "[dbo].[USP_INQ_INQURIES_TOUR_UPDATE]";
        public const String USP_INQ_INQUIRY_FOR_TICKET_BOOKING_INSERT = "[USP_INQ_INQUIRY_FOR_TICKET_BOOKING_INSERT]";
        public const String USP_INQ_INQUIRY_FOR_TICKET_BOOKING_UPDATE = "[USP_INQ_INQUIRY_FOR_TICKET_BOOKING_UPDATE]";
        public const String USP_INQ_INQUIRY_MAIN_HEAD_INSERT = "[USP_INQ_INQUIRY_MAIN_HEAD_INSERT]";
        public const String USP_INQ_INQUIRY_MAIN_HEAD_SELECT = "[USP_INQ_INQUIRY_MAIN_HEAD_SELECT]";
        public const String USP_INQ_INQUIRY_MAINHEAD_BY_INQID_SELECT = "[USP_INQ_INQUIRY_MAINHEAD_BY_INQID_SELECT]";

        public const String USP_INQ_INQUIRY_MAIN_HEAD_UPDATE = "[dbo].[USP_INQ_INQUIRY_MAIN_HEAD_UPDATE]";
        public const String USP_INQ_INQURIES_SUBHEAD_INSERT = "[dbo].[USP_INQ_INQURIES_SUBHEAD_INSERT]";
        public const String USP_INQ_INQUIRY_SUB_HEAD_SELECT = "[dbo].[USP_INQ_INQUIRY_SUB_HEAD_SELECT]";
        public const String USP_INQ_INQUIRY_SUB_HEAD_DELETE = "[dbo].[USP_INQ_INQUIRY_SUB_HEAD_DELETE]";
        public const String USP_INQ_INQUIRY_SUB_HEAD_UPDATE = "[dbo].[USP_INQ_INQUIRY_SUB_HEAD_UPDATE]";
        public const String USP_INQ_INQUIRY_FOR_TICKET_BOOKING_SELECT = "[dbo].[USP_INQ_INQUIRY_FOR_TICKET_BOOKING_SELECT]";
        public const String USP_INQ_INQUIRY_FOLLOWUP_INSERT = "[dbo].[USP_INQ_INQUIRY_FOLLOWUP_INSERT]";
        public const String USP_INQ_INQUIRY_FOLLOWUP_UPDATE = "[dbo].[USP_INQ_INQUIRY_FOLLOWUP_UPDATE]";
        public const String USP_INQ_INQUIRIES_FOLLOWUPS_SELECT = "[dbo].[USP_INQ_INQUIRIES_FOLLOWUPS_SELECT]";
        public const String USP_INQ_INQUIRY_STATUS_DETAIL_INSERT = "[dbo].[USP_INQ_INQUIRY_STATUS_DETAIL_INSERT]";
        public const String USP_INQ_INQURIES_TOUR_SELECT = "[dbo].[USP_INQ_INQURIES_TOUR_SELECT]";
        public const String USP_FARE_TOUR_HOTEL_DETAILS_DELETE = "[dbo].[USP_FARE_TOUR_HOTEL_DETAILS_DELETE]";


        #endregion

        /***************************************** ROLE LOOKUP - DEVANG *******************************************/

        #region Role Lookup Procedures
        public const String USP_ADMINISTRATION_ROLE_SELECT_KEYVALUE = "[DBO].[USP_ADMINISTRATION_ROLE_SELECT_KEYVALUE]";
        public const String USP_ADMINISTRATION_DESIGNATION_GETKEYVALUE = "[DBO].[USP_ADMINISTRATION_DESIGNATION_GETKEYVALUE]";
        public const String USP_ADMINISTRATION_QUALIFICATION_GETKEYVALUE = "[DBO].[USP_ADMINISTRATION_QUALIFICATION_GETKEYVALUE]";
        #endregion

        #region Module MASTER Procedures
        public const String USP_ADMINISTRATION_MODULEMASTER_SELECT = "[DBO].USP_ADMINISTRATION_MODULEMASTER_SELECT";
        public const String USP_ADMINISTRATION_MODULEMASTER_UPDATE = "[DBO].[USP_ADMINISTRATION_MODULEMASTER_UPDATE]";
        public const String USP_ADMINISTRATION_MODULEMASTER_CREATE = "[DBO].[USP_ADMINISTRATION_MODULEMASTER_INSERT]";
        public const String USP_ADMINISTRATION_MODULEMASTER_DELETE = "[DBO].[USP_ADMINISTRATION_MODULEMASTER_DELETE]";
        #endregion

        #region Company Master Procedures
        public const String USP_ADMINISTRATION_COMPANY_SELECT = "[DBO].[USP_ADMINISTRATION_COMPANY_SELECT]";
        public const String USP_ADMINISTRATION_COMPANY_UPDATE = "[DBO].[USP_ADMINISTRATION_COMPANY_UPDATE]";
        public const String USP_ADMINISTRATION_COMPANY_INSERT = "[DBO].[USP_ADMINISTRATION_COMPANY_INSERT]";
        public const String USP_ADMINISTRATION_COMPANY_DELETE = "[DBO].[USP_ADMINISTRATION_COMPANY_DELETE]";
        public const String USP_ADMINISTRATION_COMPANY_CONTACT_SELECT = "[dbo].[USP_ADMINISTRATION_COMPANY_CONTACT_SELECT]";
        public const String USP_ADMINISTRATION_COMPANY_CONTACT_INSERT = "[dbo].[USP_ADMINISTRATION_COMPANY_CONTACT_INSERT]";
        public const String USP_ADMINISTRATION_COMPANY_CONTACT_UPDATE = "[dbo].[USP_ADMINISTRATION_COMPANY_CONTACT_UPDATE]";
        public const String USP_ADMINISTRATION_COMPANY_CONTACT_DELETE = "[dbo].[USP_ADMINISTRATION_COMPANY_CONTACT_DELETE]";
        //  public const String USP_HR_COMPANY_DEPARTMENT_MAP_INSERT = "[DBO].[USP_HR_COMPANY_DEPARTMENT_MAP_INSERT]";
        public const String USP_ADMINISTRATION_COMPANY_DEPARTMENT_MAP_INSERT = "[dbo].[USP_ADMINISTRATION_COMPANY_DEPARTMENT_MAP_INSERT]";
        public const String USP_ADMINISTRATION_COMPANY_DEPARTMENT_MAP_KEYVALUE = "[dbo].[USP_ADMINISTRATION_COMPANY_DEPARTMENT_MAP_KEYVALUE]";
        public const String USP_ADMINISTRATION_COMPANY_DEPARTMENT_SELECT_BY_COMPANY_ID = "[dbo].[USP_ADMINISTRATION_COMPANY_DEPARTMENT_SELECT_BY_COMPANY_ID]";
        public const String USP_ADMINISTRATION_COMPANY_SELECT_KEYVALUE = "[DBO].[USP_ADMINISTRATION_COMPANY_SELECT_KEYVALUE]";
        public const String USP_ADMINISTRATION_COMPANY_SELECT_NAME_TYPE = "[DBO].[USP_ADMINISTRATION_COMPANY_SELECT_NAME_TYPE]";
        public const String USP_HR_COMPANY_EMPLOYEE_MAP_KEYVALUE = "[DBO].[USP_HR_COMPANY_EMPLOYEE_MAP_KEYVALUE]";
        public const String USP_HR_COMPANY_EMPLOYEE_MAP_INSERT = "[DBO].[USP_HR_COMPANY_EMPLOYEE_MAP_INSERT]";
        public const String USP_CUSTOMER_PROFILE_CREDENTIALS_UPDATE = "[DBO].[USP_CUSTOMER_PROFILE_CREDENTIALS_UPDATE]";

        #endregion

        #region Employee Master Procedures
        public const String USP_HR_EMPLOYEE_SELECT = "[dbo].[USP_HR_EMPLOYEE_SELECT]";
        public const String USP_HR_EMPLOYEE_SELECT_BY_USERID = "[dbo].[USP_HR_EMPLOYEE_SELECT_BY_USERID]";
        public const String USP_HR_EMPLOYEE_SELECT_BYID = "[dbo].[USP_HR_EMPLOYEE_SELECT_BY_ID]";
        public const String USP_HR_EMPLOYEE_UPDATE = "[dbo].[USP_HR_EMPLOYEE_UPDATE]";
        public const String USP_HR_EMPLOYEE_PROFILE_UPDATE = "[dbo].[USP_HR_EMPLOYEE_PROFILE_UPDATE]";
        public const String USP_HR_EMPLOYEE_INSERT = "[dbo].[USP_HR_EMPLOYEE_INSERT]";
        public const String USP_HR_EMPLOYEE_DELETE = "[dbo].[USP_HR_EMPLOYEE_DELETE]";
        public const String USP_HR_EMPLOYEE_CONTACT_SELECT = "[dbo].[USP_HR_EMPLOYEE_CONTACT_SELECT]";
        public const String USP_HR_EMPLOYEE_CONTACT_UPDATE = "[dbo].[USP_HR_EMPLOYEE_CONTACT_UPDATE]";
        public const String USP_HR_EMPLOYEE_CONTACT_INSERT = "[dbo].[USP_HR_EMPLOYEE_CONTACT_INSERT]";
        public const String USP_HR_EMPLOYEE_CONTACT_DELETE = "[dbo].[USP_HR_EMPLOYEE_CONTACT_DELETE]";
        public const String USP_ADMINISTRATION_MANAGER_GETKEYVALUE = "[DBO].[USP_ADMINISTRATION_MANAGER_GETKEYVALUE]";
        public const String USP_HR_EMPLOYEE_COMPANY_MAP_SELECT = "[dbo].[USP_HR_EMPLOYEE_COMPANY_MAP_SELECT]";
        public const String USP_HR_EMPLOYEE_COMPANY_MAP_UPDATE = "[dbo].[USP_HR_EMPLOYEE_COMPANY_MAP_UPDATE]";
        public const String USP_HR_EMPLOYEE_COMPANY_MAP_DELETE = "[dbo].[USP_HR_EMPLOYEE_COMPANY_MAP_DELETE]";
        #endregion

        #region Country Master Procedures
        public const String USP_ADMINISTRATION_COUNTRY_GETKEYVALUE = "[DBO].[UDP_ADMINISTRATION_COUNTRY_GET_KEYVALUE]";
        #endregion

        #region State Master Procedures
        public const String USP_ADMINISTRATION_STATE_GETKEYVALUE = "[DBO].[UDP_ADMINISTRATION_STATE_GET_KEYVALUE]";
        #endregion

        #region City Master Procedures
        public const String USP_ADMINISTRATION_CITY_GETKEYVALUE = "[DBO].[UDP_ADMINISTRATION_CITY_GET_KEYVALUE]";
        #endregion

        #region Region Master Procedures
        public const String USP_ADMINISTRATION_REGION_GETKEYVALUE = "[DBO].[UDP_ADMINISTRATION_REGION_GET_KEYVALUE]";
        #endregion

        #region Company Competitor Agent
        public const String USP_ADMINISTRATION_COMPETITOR_AGENT_INSERT = "[DBO].[USP_ADMINISTRATION_COMPETITOR_AGENT_INSERT]";
        public const String USP_ADMINISTRATION_COMPETITOR_AGENT_UPDATE = "[DBO].[USP_ADMINISTRATION_COMPETITOR_AGENT_UPDATE]";
        public const String USP_ADMINISTRATION_COMPETITOR_AGENT_DELETE = "[DBO].[USP_ADMINISTRATION_COMPETITOR_AGENT_DELETE]";
        public const String USP_ADMINISTRATION_COMPETITOR_AGENT_SELECT = "[DBO].[USP_ADMINISTRATION_COMPETITOR_AGENT_SELECT]";
        public const String USP_ADMINISTRATION_COMPETITOR_CUSTOMER_SELECT = "[DBO].[USP_ADMINISTRATION_COMPETITOR_CUSTOMER_SELECT]";
        #endregion

        #region Customer Export
        public const String USP_CUST_CUSTOMER_PROFILE_FOR_EXPORT = "[dbo].[USP_CUST_CUSTOMER_PROFILE_FOR_EXPORT]";
        public const String USP_CUSTOMER_EXPORT_SELECT_REPORT = "[dbo].[USP_CUSTOMER_EXPORT_SELECT_REPORT]";
        #endregion

        #region Reports

        #region Employee reports
        public const String USP_HR_EMPLOYEE_LIST_BY_DEPARTMENT_REPORT = "[dbo].[USP_HR_EMPLOYEE_LIST_BY_DEPARTMENT_REPORT]";
        public const String USP_HR_EMPLOYEE_LIST_BY_COMPANY_REPORT = "[dbo].[USP_HR_EMPLOYEE_LIST_BY_COMPANY_REPORT]";
        public const String USP_HR_EMPLOYEE_CARD_REPORT = "[dbo].[USP_HR_EMPLOYEE_CARD_REPORT]";
        public const String USP_HR_EMPLOYEE_CONTACT_REPORT = "[dbo].[USP_HR_EMPLOYEE_CONTACT_REPORT]";
        public const String USP_HR_EMPLOYEE_CARD_OTHER_COMPANY_REPORT = "[dbo].[USP_HR_EMPLOYEE_CARD_OTHER_COMPANY_REPORT]";
        #endregion

        #region Customer Gadgets reports
        public const String USP_CUST_CUSTOMER_GADGET_REPORT = "[dbo].[USP_CUST_CUSTOMER_GADGET_REPORT]";
        public const String USP_CUST_CUSTOMER_PROFILE_FULL_REPORT = "[dbo].[USP_CUST_CUSTOMER_PROFILE_FULL_REPORT]";
        public const String USP_CUST_CUSTOMER_CONTACT_DETAILS_REPORT = "[dbo].[USP_CUST_CUSTOMER_CONTACT_DETAILS_REPORT]";
        #endregion

        #endregion

        #region Gadgets
        //Company wise total Inquiry Followups by Company and Department
        public const String USP_CUST_CUSTOMER_INQURIES_GADGETS_BYCOMPANY_SELECT = "[dbo].[USP_CUST_CUSTOMER_INQURIES_GADGETS_BYCOMPANY_SELECT]";
        #endregion

        /**********************************************************************************************************/

        /********************************* Religion Lookup Sps Procedures - Chirag ********************************/

        #region Religion Lookup Sps
        public const String USP_ADMINISTRATION_RELIGION_SELECT = "[dbo].[USP_ADMINISTRATION_RELIGION_SELECT]";
        public const String USP_ADMINISTRATION_RELIGION_INSERT = "[dbo].[USP_ADMINISTRATION_RELIGION_INSERT]";
        public const String USP_ADMINISTRATION_RELIGION_DELETE = "[dbo].[USP_ADMINISTRATION_RELIGION_DELETE]";
        public const String USP_ADMINISTRATION_RELIGION_UPDATE = "[dbo].[USP_ADMINISTRATION_RELIGION_UPDATE]";
        #endregion

        #region GeographicLocation Lookup

        #region Country
        public const String USP_ADMINISTRATION_COUNTRY_SELECT = "[dbo].[USP_ADMINISTRATION_COUNTRY_SELECT]";
        public const String USP_ADMINISTRATION_COUNTRY_INSERT = "[dbo].[USP_ADMINISTRATION_COUNTRY_INSERT]";
        public const String USP_ADMINISTRATION_COUNTRY_DELETE = "[dbo].[USP_ADMINISTRATION_COUNTRY_DELETE]";
        public const String USP_ADMINISTRATION_COUNTRY_UPDATE = "[dbo].[USP_ADMINISTRATION_COUNTRY_UPDATE]";
        #endregion

        #region  State
        public const String USP_ADMINISTRATION_STATE_SELECT = "[dbo].[USP_ADMINISTRATION_STATE_SELECT]";
        public const String USP_ADMINISTRATION_STATE_INSERT = "[dbo].[USP_ADMINISTRATION_STATE_INSERT]";
        public const String USP_ADMINISTRATION_STATE_DELETE = "[dbo].[USP_ADMINISTRATION_STATE_DELETE]";
        public const String USP_ADMINISTRATION_STATE_UPDATE = "[dbo].[USP_ADMINISTRATION_STATE_UPDATE]";
        #endregion

        #region City
        public const String USP_ADMINISTRATION_CITY_SELECT = "[dbo].[USP_ADMINISTRATION_CITY_SELECT]";
        public const String USP_ADMINISTRATION_CITY_INSERT = "[dbo].[USP_ADMINISTRATION_CITY_INSERT]";
        public const String USP_ADMINISTRATION_CITY_DELETE = "[dbo].[USP_ADMINISTRATION_CITY_DELETE]";
        public const String USP_ADMINISTRATION_CITY_UPDATE = "[dbo].[USP_ADMINISTRATION_CITY_UPDATE]";
        #endregion

        #endregion

        #region ProductLookUp SpS
        public const String USP_ADMINISTRATION_PRODUCT_SELECT = "[dbo].[USP_ADMINISTRATION_PRODUCT_SELECT]";
        public const String USP_ADMINISTRATION_PRODUCT_INSERT = "[dbo].[USP_ADMINISTRATION_PRODUCT_INSERT]";
        public const String USP_ADMINISTRATION_PRODUCT_DELETE = "[dbo].[USP_ADMINISTRATION_PRODUCT_DELETE]";
        public const String USP_ADMINISTRATION_PRODUCT_UPDATE = "[dbo].[USP_ADMINISTRATION_PRODUCT_UPDATE]";
        #endregion

        #region StatusLookUp SpS
        public const String USP_ADMINISTRATION_STATUS_INSERT = "[dbo].[USP_ADMINISTRATION_STATUS_INSERT]";
        public const String USP_ADMINISTRATION_STATUS_DELETE = "[dbo].[USP_ADMINISTRATION_STATUS_DELETE]";
        public const String USP_ADMINISTRATION_STATUS_UPDATE = "[dbo].[USP_ADMINISTRATION_STATUS_UPDATE]";
        #endregion

        #region Marital_StatusLookUp SpS
        public const String USP_ADMINISTRATION_MARITAL_STATUS_INSERT = "[dbo].[USP_ADMINISTRATION_MARITAL_STATUS_INSERT]";
        // public const String USP_ADMINISTRATION_MARITAL_STATUS_SELECT = "[dbo].[USP_ADMINISTRATION_MARITAL_STATUS_SELECT]";
        public const String USP_ADMINISTRATION_MARITAL_STATUS_DELETE = "[dbo].[USP_ADMINISTRATION_MARITAL_STATUS_DELETE]";
        public const String USP_ADMINISTRATION_MARITAL_STATUS_UPDATE = "[dbo].[USP_ADMINISTRATION_MARITAL_STATUS_UPDATE]";
        #endregion

        #region Forgot Password
        public const String USP_SYS_USER_MASTER_FATCH_EMAILID = "[dbo].[USP_SYS_USER_MASTER_FATCH_EMAILID]";
        public const String USP_SYS_USER_PASSWORD_UPDATE = "[dbo].[USP_SYS_USER_PASSWORD_UPDATE]";
        #endregion

        #region InquiriesFollowsModeLookup SpS
        public const String USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_INSERT = "[dbo].[USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_INSERT]";
        public const String USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_SELECT = "[dbo].[USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_SELECT]";
        public const String USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_DELETE = "[dbo].[USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_DELETE]";
        public const String USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_UPDATE = "[dbo].[USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_UPDATE]";
        #endregion

        #region VehicleTypeLookup SpS
        public const String USP_ADMINISTRATION_VEHICLE_TYPE_INSERT = "[dbo].[USP_ADMINISTRATION_VEHICLE_TYPE_INSERT]";
        public const String USP_ADMINISTRATION_VEHICLE_TYPE_SELECT = "[dbo].[USP_ADMINISTRATION_VEHICLE_TYPE_SELECT]";
        public const String USP_ADMINISTRATION_VEHICLE_TYPE_DELETE = "[dbo].[USP_ADMINISTRATION_VEHICLE_TYPE_DELETE]";
        public const String USP_ADMINISTRATION_VEHICLE_TYPE_UPDATE = "[dbo].[USP_ADMINISTRATION_VEHICLE_TYPE_UPDATE]";
        #endregion

        #region SecurityQuestionLookup SpS
        public const String USP_ADMINISTRATION_SECURITY_QUESTION_INSERT = "[dbo].[USP_ADMINISTRATION_SECURITY_QUESTION_INSERT]";
        public const String USP_ADMINISTRATION_SECURITY_QUESTION_SELECT = "[dbo].[USP_ADMINISTRATION_SECURITY_QUESTION_SELECT]";
        public const String USP_ADMINISTRATION_SECURITY_QUESTION_DELETE = "[dbo].[USP_ADMINISTRATION_SECURITY_QUESTION_DELETE]";
        public const String USP_ADMINISTRATION_SECURITY_QUESTION_UPDATE = "[dbo].[USP_ADMINISTRATION_SECURITY_QUESTION_UPDATE]";
        #endregion

        #region InquiryStatusLookup SpS
        public const String USP_ADMINISTRATION_INQUIRY_STATUS_INSERT = "[dbo].[USP_ADMINISTRATION_INQUIRY_STATUS_INSERT]";
        public const String USP_ADMINISTRATION_INQUIRY_STATUS_SELECT = "[dbo].[USP_ADMINISTRATION_INQUIRY_STATUS_SELECT]";
        public const String USP_ADMINISTRATION_INQUIRY_STATUS_DELETE = "[dbo].[USP_ADMINISTRATION_INQUIRY_STATUS_DELETE]";
        public const String USP_ADMINISTRATION_INQUIRY_STATUS_UPDATE = "[dbo].[USP_ADMINISTRATION_INQUIRY_STATUS_UPDATE]";
        #endregion

        #region ReferenceLookup SpS
        public const String USP_ADMINISTRATION_REFERENCE_INSERT = "[dbo].[USP_ADMINISTRATION_REFERENCE_INSERT]";
        public const String USP_ADMINISTRATION_REFERENCE_SELECT = "[dbo].[USP_ADMINISTRATION_REFERENCE_SELECT]";
        public const String USP_ADMINISTRATION_REFERENCE_DELETE = "[dbo].[USP_ADMINISTRATION_REFERENCE_DELETE]";
        public const String USP_ADMINISTRATION_REFERENCE_UPDATE = "[dbo].[USP_ADMINISTRATION_REFERENCE_UPDATE]";
        #endregion

        #region Tour Master
        public const String USP_FARE_TOUR_INSERT = "[dbo].[USP_FARE_TOUR_INSERT]";
        public const String USP_FARE_TOUR_SELECT = "[dbo].[USP_FARE_TOUR_SELECT]";
        public const String USP_FARE_TOUR_DELETE = "[dbo].[USP_FARE_TOUR_DELETE]";
        public const String USP_FARE_TOUR_UPDATE = "[dbo].[USP_FARE_TOUR_UPDATE]";
        public const String USP_FARE_TOUR_SELECT_BROWSE = "[dbo].[USP_FARE_TOUR_SELECT_BROWSE]";
        public const String USP_ADMINISTRATION_CITY_BY_COUNTRY = "[dbo].[USP_ADMINISTRATION_CITY_BY_COUNTRY]";
        public const String USP_FARE_TOUR_TRAVELLING_CITY_SELECT = "[dbo].[USP_FARE_TOUR_TRAVELLING_CITY_SELECT]";
        public const String USP_FARE_TOUR_COUNTRYCITY_BY_TOURID_SELECT = "USP_FARE_TOUR_COUNTRYCITY_BY_TOURID_SELECT";
        public const String USP_FARE_HOTEL_HOTELNAME_BY_COUNTRYCITY_SELECT = "USP_FARE_HOTEL_HOTELNAME_BY_COUNTRYCITY_SELECT";
        public const String USP_FARE_TOUR_HOTEL_INSERT_UPDATE = "USP_FARE_TOUR_HOTEL_INSERT_UPDATE";
        public const String USP_FARE_TOUR_HOTEL_DETAILS_SELECT = "USP_FARE_TOUR_HOTEL_DETAILS_SELECT";
        public const String USP_FARE_TOUR_TRAVEL_DETAIL_INSERT = "[dbo].USP_FARE_TOUR_TRAVEL_DETAIL_INSERT";
        public const String USP_FARE_TOUR_TRAVEL_DETAIL_UPDATE = "[dbo].USP_FARE_TOUR_TRAVEL_DETAIL_UPDATE";
        public const String USP_FARE_TOUR_TRAVEL_DETAIL_DELETE = "[dbo].USP_FARE_TOUR_TRAVEL_DETAIL_DELETE";
        public const String USP_FARE_TOUR_MASTER_WEB_HIGHLIGHT_UPDATE = "[dbo].USP_FARE_TOUR_MASTER_WEB_HIGHLIGHT_UPDATE";
        public const String USP_FARE_TOUR_MASTER_WEB_IMPORTANT_NOTES_UPDATE = "[dbo].USP_FARE_TOUR_MASTER_WEB_IMPORTANT_NOTES_UPDATE";
        public const String USP_FARE_TOUR_MASTER_WEB_ITENARY_UPDATE = "[dbo].USP_FARE_TOUR_MASTER_WEB_ITENARY_UPDATE";
        public const String USP_FARE_TOUR_MASTER_WEB_TERMS_UPDATE = "[dbo].USP_FARE_TOUR_MASTER_WEB_TERMS_UPDATE";
        public const String USP_FARE_TOUR_MASTER_WEB_TOUR_COST_UPDATE = "[dbo].USP_FARE_TOUR_MASTER_WEB_TOUR_COST_UPDATE";
        public const String USP_FARE_TOUR_MASTER_WEB_HIGHLIGHT_SELECT = "[dbo].USP_FARE_TOUR_MASTER_WEB_HIGHLIGHT_SELECT";
        public const String USP_FARE_TOUR_MASTER_WEB_ITENARY_SELECT = "[dbo].USP_FARE_TOUR_MASTER_WEB_ITENARY_SELECT";
        public const String USP_FARE_TOUR_MASTER_WEB_TOUR_COST_SELECT = "[dbo].USP_FARE_TOUR_MASTER_WEB_TOUR_COST_SELECT";
        public const String USP_FARE_TOUR_MASTER_WEB_TERMS_SELECT = "[dbo].USP_FARE_TOUR_MASTER_WEB_TERMS_SELECT";
        public const String USP_FARE_TOUR_MASTER_WEB_IMPORTANT_NOTES_SELECT = "[dbo].USP_FARE_TOUR_MASTER_WEB_IMPORTANT_NOTES_SELECT";
        public const String USP_FARE_TRAVEL_FLIGHT_KEYVALUE = "dbo.USP_FARE_TRAVEL_FLIGHT_KEYVALUE";
        public const String USP_FARE_FLIGHT_MASTER_SELECT_BY_ID = "USP_FARE_FLIGHT_MASTER_SELECT_BY_ID";
        public const String USP_FARE_TOUR_MASTER_BROWSE_DATA_SELECT = "USP_FARE_TOUR_MASTER_BROWSE_DATA_SELECT";
        public const String USP_FARE_TOUR_TRAVEL_DETAIL_SELECT = "[dbo].USP_FARE_TOUR_TRAVEL_DETAIL_SELECT";


        public const String USP_FARE_FLIGHT_CURRENCY_PRICE_INSERT = "[dbo].USP_FARE_FLIGHT_CURRENCY_PRICE_INSERT";
        public const String USP_FARE_FLIGHT_CURRENCY_PRICE_UPDATE = "[dbo].USP_FARE_FLIGHT_CURRENCY_PRICE_UPDATE";
        public const String USP_FARE_FLIGHT_CURRENCY_PRICE_SELECT = "[dbo].USP_FARE_FLIGHT_CURRENCY_PRICE_SELECT";


        public const String USP_FARE_TOUR_TRAVEL_CURRENCY_PRICE_INSERT = "[dbo].USP_FARE_TOUR_TRAVEL_CURRENCY_PRICE_INSERT";
        public const String USP_FARE_TOUR_TRAVEL_CURRENCY_PRICE_SELECT = "[dbo].USP_FARE_TOUR_TRAVEL_CURRENCY_PRICE_SELECT";
        public const String USP_FARE_TOUR_TRAVEL_CURRENCY_PRICE_UPDATE = "[dbo].USP_FARE_TOUR_TRAVEL_CURRENCY_PRICE_UPDATE";
        public const String USP_FARE_FLIGHT_CURRENCY_PRICE_SELECT_BYID = "[dbo].USP_FARE_FLIGHT_CURRENCY_PRICE_SELECT_BYID";
        public const String USP_FARE_FLIGHT_CURRENCY_SELECT_BYID = "[dbo].USP_FARE_FLIGHT_CURRENCY_SELECT_BYID";
        public const String USP_FARE_TOUR_HOTEL_REMARKS_SELECT = "[dbo].USP_FARE_TOUR_HOTEL_REMARKS_SELECT";
        public const String USP_FARE_FLIGHT_CURRENCY_SELECT = "[dbo].USP_FARE_FLIGHT_CURRENCY_SELECT";
        public const String USP_FARE_TOUR_TRAVEL_CURRENCY_PRICE_REMARKS_SELECT = "[dbo].USP_FARE_TOUR_TRAVEL_CURRENCY_PRICE_REMARKS_SELECT";










        #endregion

        /**********************************************************************************************************/

        /********************************* Relation Lookup Sps Procedures - Priyam ********************************/

        #region Relation LookUp Sps

        public const String USP_ADMINISTRATION_RELATION_BYNAME = "[dbo].[USP_ADMINISTRATION_RELATION_BYNAME]";
        public const String USP_ADMINISTRATION_RELATION_INSERT = "[dbo].[USP_ADMINISTRATION_RELATION_INSERT]";
        public const String USP_ADMINISTRATION_RELATION_UPDATE = "[dbo].[USP_ADMINISTRATION_RELATION_UPDATE]";
        public const String USP_ADMINISTRATION_RELATION_DELETE = "[dbo].[USP_ADMINISTRATION_RELATION_DELETE]";

        #endregion

        #region Title LookUp Sps
        public const String USP_ADMINISTRATION_TITLE_SELECT = "[dbo].[USP_ADMINISTRATION_TITLE_SELECT]";
        public const String USP_ADMINISTRATION_TITLE_INSERT = "[DBO].[USP_ADMINISTRATION_TITLE_INSERT]";
        public const String USP_ADMINISTRATION_TITLE_UPDATE = "[DBO].[USP_ADMINISTRATION_TITLE_UPDATE]";
        public const String USP_ADMINISTRATION_TITLE_DELETE = "[DBO].[USP_ADMINISTRATION_TITLE_DELETE]";
        #endregion

        #region Profession LookUp Sps
        public const String USP_ADMINISTRATION_PROFESSION_SELECT = "[dbo].[USP_ADMINISTRATION_PROFESSION_SELECT]";
        public const String USP_ADMINISTRATION_PROFESSION_INSERT = "[dbo].[USP_ADMINISTRATION_PROFESSION_INSERT]";
        public const String USP_ADMINISTRATION_PROFESSION_DELETE = "[dbo].[USP_ADMINISTRATION_PROFESSION_DELETE]";
        public const String USP_ADMINISTRATION_PROFESSION_UPDATE = "[dbo].[USP_ADMINISTRATION_PROFESSION_UPDATE]";
        #endregion

        #region Qualification LookUp Sps
        public const String USP_ADMINISTRATION_QUALIFICATION_SELECT = "[dbo].[USP_ADMINISTRATION_QUALIFICATION_SELECT]";
        public const String USP_ADMINISTRATION_QUALIFICATION_INSERT = "[dbo].[USP_ADMINISTRATION_QUALIFICATION_INSERT]";
        public const String USP_ADMINISTRATION_QUALIFICATION_DELETE = "[dbo].[USP_ADMINISTRATION_QUALIFICATION_DELETE]";
        public const String USP_ADMINISTRATION_QUALIFICATION_UPDATE = "[dbo].[USP_ADMINISTRATION_QUALIFICATION_UPDATE]";
        #endregion

        #region Department LookUp Sps
        public const String USP_ADMINISTRATION_DEPARTMENT_SELECT = "[dbo].[USP_ADMINISTRATION_DEPARTMENT_SELECT]";
        public const String USP_ADMINISTRATION_DEPARTMENT_INSERT = "[dbo].[USP_ADMINISTRATION_DEPARTMENT_INSERT]";
        public const String USP_ADMINISTRATION_DEPARTMENT_DELETE = "[dbo].[USP_ADMINISTRATION_DEPARTMENT_DELETE]";
        public const String USP_ADMINISTRATION_DEPARTMENT_UPDATE = "[dbo].[USP_ADMINISTRATION_DEPARTMENT_UPDATE]";
        #endregion

        #region Region LookUp Sps
        public const String USP_ADMINISTRATION_REGION_DELETE = "[dbo].[USP_ADMINISTRATION_REGION_DELETE]";
        public const String USP_ADMINISTRATION_REGION_INSERT = "[dbo].[USP_ADMINISTRATION_REGION_INSERT]";
        public const String USP_ADMINISTRATION_REGION_SELECT = "[dbo].[USP_ADMINISTRATION_REGION_SELECT]";
        public const String USP_ADMINISTRATION_REGION_UPDATE = "[dbo].[USP_ADMINISTRATION_REGION_UPDATE]";
        #endregion

        #region ROLE Lookup Procedures
        public const String USP_ADMINISTRATION_ROLE_GETROLE = "[DBO].[USP_ADMINISTRATION_ROLE_GETROLE]";
        public const String USP_ADMINISTRATION_ROLE_UPDATE = "[DBO].[USP_ADMINISTRATION_ROLE_UPDATE]";
        public const String USP_ADMINISTRATION_ROLE_INSERT = "[DBO].[USP_ADMINISTRATION_ROLE_INSERT]";
        public const String USP_ADMINISTRATION_ROLE_DELETE = "[DBO].[USP_ADMINISTRATION_ROLE_DELETE]";
        #endregion

        #region Designation LookUp Sps
        public const String USP_ADMINISTRATION_DESIGNATION_SELECT = "[dbo].[USP_ADMINISTRATION_DESIGNATION_SELECT]";
        public const String USP_ADMINISTRATION_DESIGNATION_INSERT = "[DBO].[USP_ADMINISTRATION_DESIGNATION_INSERT]";
        public const String USP_ADMINISTRATION_DESIGNATION_UPDATE = "[DBO].[USP_ADMINISTRATION_DESIGNATION_UPDATE]";
        public const String USP_ADMINISTRATION_DESIGNATION_DELETE = "[DBO].[USP_ADMINISTRATION_DESIGNATION_DELETE]";
        #endregion

        #region ADDRESS MASTER LookUp Sps
        public const String USP_ADMINISTRATION_ADDRESS_TYPE_SELECT = "[dbo].[USP_ADMINISTRATION_ADDRESS_TYPE_SELECT]";
        public const String USP_ADMINISTRATION_ADDRESS_TYPE_INSERT = "[DBO].[USP_ADMINISTRATION_ADDRESS_TYPE_INSERT]";
        public const String USP_ADMINISTRATION_ADDRESS_TYPE_UPDATE = "[DBO].[USP_ADMINISTRATION_ADDRESS_TYPE_UPDATE]";
        public const String USP_ADMINISTRATION_ADDRESS_TYPE_DELETE = "[DBO].[USP_ADMINISTRATION_ADDRESS_TYPE_DELETE]";
        #endregion

        #region Cust Code LookUp Sps
        public const String USP_CUST_CODE_SELECT = "[dbo].[USP_CUST_CODE_SELECT]";
        public const String USP_CUST_CODE_INSERT = "[DBO].[USP_CUST_CODE_INSERT]";
        public const String USP_CUST_CODE_UPDATE = "[DBO].[USP_CUST_CODE_UPDATE]";
        public const String USP_CUST_CODE_DELETE = "[DBO].[USP_CUST_CODE_DELETE]";
        #endregion

        #region Tour Type LookUp Sps
        public const String USP_ADMINISTRATION_TOUR_SELECT = "[dbo].[USP_ADMINISTRATION_TOUR_SELECT]";
        public const String USP_ADMINISTRATION_TOUR_INSERT = "[DBO].[USP_ADMINISTRATION_TOUR_INSERT]";
        public const String USP_ADMINISTRATION_TOUR_UPDATE = "[DBO].[USP_ADMINISTRATION_TOUR_UPDATE]";
        public const String USP_ADMINISTRATION_TOUR_DELETE = "[DBO].[USP_ADMINISTRATION_TOUR_DELETE]";
        #endregion

        #region Room Type LookUp Sps
        public const String USP_FARE_HOTEL_ROOM_TYPE_SELECT = "[DBO].[USP_FARE_HOTEL_ROOM_TYPE_SELECT]";
        public const String USP_FARE_HOTEL_ROOM_TYPE_INSERT = "[DBO].[USP_FARE_HOTEL_ROOM_TYPE_INSERT]";
        public const String USP_FARE_HOTEL_ROOM_TYPE_DELETE = "[DBO].[USP_FARE_HOTEL_ROOM_TYPE_DELETE]";
        public const String USP_FARE_HOTEL_ROOM_TYPE_UPDATE = "[DBO].[USP_FARE_HOTEL_ROOM_TYPE_UPDATE]";
        #endregion

        #region Agent LookUp Sps
        public const String USP_FARE_AGENT_SELECT = "[dbo].[USP_FARE_AGENT_SELECT]";
        public const String USP_FARE_AGENT_INSERT = "[DBO].[USP_FARE_AGENT_INSERT]";
        public const String USP_FARE_AGENT_UPDATE = "[DBO].[USP_FARE_AGENT_UPDATE]";
        public const String USP_FARE_AGENT_DELETE = "[DBO].[USP_FARE_AGENT_DELETE]";
        #endregion

        #region Currency LookUp Sps
        public const String USP_BOOKING_CURRENCY_DELETE = "[dbo].[USP_BOOKING_CURRENCY_DELETE]";
        public const String USP_BOOKING_CURRENCY_SELECT = "[DBO].[USP_BOOKING_CURRENCY_SELECT]";
        public const String USP_BOOKING_CURRENCY_INSERT = "[DBO].[USP_BOOKING_CURRENCY_INSERT]";
        public const String USP_BOOKING_CURRENCY_UPDATE = "[DBO].[USP_BOOKING_CURRENCY_UPDATE]";
        #endregion

        #region CheckList LookUp Sps
        public const String USP_BOOKING_CHECKLIST_DELETE = "[DBO].[USP_BOOKING_CHECKLIST_DELETE]";
        public const String USP_BOOKING_CHECKLIST_SELECT = "[DBO].[USP_BOOKING_CHECKLIST_SELECT]";
        public const String USP_BOOKING_CHECKLIST_INSERT = "[DBO].[USP_BOOKING_CHECKLIST_INSERT]";
        public const String USP_BOOKING_CHECKLIST_UPDATE = "[DBO].[USP_BOOKING_CHECKLIST_UPDATE]";
        #endregion

        #region Bank LookUp Sps
        public const String USP_BOOKING_BANK_DELETE = "[DBO].[USP_BOOKING_BANK_DELETE]";
        public const String USP_BOOKING_BANK_INSERT = "[DBO].[USP_BOOKING_BANK_INSERT]";
        public const String USP_BOOKING_BANK_SELECT = "[DBO].[USP_BOOKING_BANK_SELECT]";
        public const String USP_BOOKING_BANK_UPDATE = "[DBO].[USP_BOOKING_BANK_UPDATE]";
        #endregion

        #region Hotel Sps
        public const String USP_FARE_HOTEL_DELETE = "[dbo].[USP_FARE_HOTEL_DELETE]";
        public const String USP_FARE_HOTEL_INSERT = "[dbo].[USP_FARE_HOTEL_INSERT]";
        public const String USP_FARE_HOTEL_SELECT = "[dbo].[USP_FARE_HOTEL_SELECT]";
        public const String USP_FARE_HOTEL_UPDATE = "[dbo].[USP_FARE_HOTEL_UPDATE]";
        public const String USP_FARE_HOTEL_SELECT_BY_HOTEL_ID = "[dbo].[USP_FARE_HOTEL_SELECT_BY_HOTEL_ID]";

        public const String USP_FARE_HOTEL_ROOM_DETAILS_DELETE = "[dbo].[USP_FARE_HOTEL_ROOM_DETAILS_DELETE]";
        public const String USP_FARE_HOTEL_ROOM_DETAILS_INSERT = "[dbo].[USP_FARE_HOTEL_ROOM_DETAILS_INSERT]";
        public const String USP_FARE_HOTEL_ROOM_DETAILS_UPDATE = "[dbo].[USP_FARE_HOTEL_ROOM_DETAILS_UPDATE]";
        public const String USP_FARE_HOTEL_ROOM_DETAILS_SELECT = "[dbo].[USP_FARE_HOTEL_ROOM_DETAILS_SELECT]";

        public const String USP_FARE_HOTEL_CURRENCY_PRICE_INSERT = "[dbo].[USP_FARE_HOTEL_CURRENCY_PRICE_INSERT]";
        public const String USP_FARE_HOTEL_CURRENCY_PRICE_UPDATE = "[dbo].[USP_FARE_HOTEL_CURRENCY_PRICE_UPDATE]";
        public const String USP_FARE_HOTEL_CURRENCY_PRICE_SELECT = "[dbo].[USP_FARE_HOTEL_CURRENCY_PRICE_SELECT]";
        public const String USP_FARE_HOTEL_CURRENCY_PRICE_DELETE = "[dbo].[USP_FARE_HOTEL_CURRENCY_PRICE_DELETE]";
        public const String USP_FARE_HOTEL_CURRENCY_PRICE_SELECT_BY_ID = "[dbo].[USP_FARE_HOTEL_CURRENCY_PRICE_SELECT_BY_ID]";



        #endregion

        #region BOOKING_FOREIGN_CURRENCY_AGENT Sps
        public const String USP_BOOKING_FOREIGN_CURRENCY_AGENT_DELETE = "[dbo].[USP_BOOKING_FOREIGN_CURRENCY_AGENT_DELETE]";
        public const String USP_BOOKING_FOREIGN_CURRENCY_AGENT_INSERT = "[dbo].[USP_BOOKING_FOREIGN_CURRENCY_AGENT_INSERT]";
        public const String USP_BOOKING_FOREIGN_CURRENCY_AGENT_SELECT = "[dbo].[USP_BOOKING_FOREIGN_CURRENCY_AGENT_SELECT]";
        public const String USP_BOOKING_FOREIGN_CURRENCY_AGENT_UPDATE = "[dbo].[USP_BOOKING_FOREIGN_CURRENCY_AGENT_UPDATE]";
        #endregion

        #region FARE_AIRLINE Sps
        public const String USP_FARE_AIRLINE_DELETE = "[dbo].[USP_FARE_AIRLINE_DELETE]";
        public const String USP_FARE_AIRLINE_INSERT = "[dbo].[USP_FARE_AIRLINE_INSERT]";
        public const String USP_FARE_AIRLINE_SELECT = "[dbo].[USP_FARE_AIRLINE_SELECT]";
        public const String USP_FARE_AIRLINE_UPDATE = "[dbo].[USP_FARE_AIRLINE_UPDATE]";
        #endregion

        #region FARE_FLIGHT_CLASS Sps
        public const String USP_FARE_FLIGHT_CLASS_DELETE = "[dbo].[USP_FARE_FLIGHT_CLASS_DELETE]";
        public const String USP_FARE_FLIGHT_CLASS_INSERT = "[dbo].[USP_FARE_FLIGHT_CLASS_INSERT]";
        public const String USP_FARE_FLIGHT_CLASS_SELECT = "[dbo].[USP_FARE_FLIGHT_CLASS_SELECT]";
        public const String USP_FARE_FLIGHT_CLASS_UPDATE = "[dbo].[USP_FARE_FLIGHT_CLASS_UPDATE]";
        #endregion

        #region INQ_RATING Sps
        public const String USP_INQ_RATING_DELETE = "[dbo].[USP_INQ_RATING_DELETE]";
        public const String USP_INQ_RATING_INSERT = "[dbo].[USP_INQ_RATING_INSERT]";
        public const String USP_INQ_RATING_SELECT = "[dbo].[USP_INQ_RATING_SELECT]";
        public const String USP_INQ_RATING_UPDATE = "[dbo].[USP_INQ_RATING_UPDATE]";
        #endregion

        #region FARE_TRAVEL_MODE_MASTER Sps
        public const String USP_FARE_TRAVEL_MODE_DELETE = "[dbo].[USP_FARE_TRAVEL_MODE_DELETE]";
        public const String USP_FARE_TRAVEL_MODE_INSERT = "[dbo].[USP_FARE_TRAVEL_MODE_INSERT]";
        public const String USP_FARE_TRAVEL_MODE_SELECT = "[dbo].[USP_FARE_TRAVEL_MODE_SELECT]";
        public const String USP_FARE_TRAVEL_MODE_UPDATE = "[dbo].[USP_FARE_TRAVEL_MODE_UPDATE]";
        #endregion

        #region FARE_TICKET_TYPE Sps
        public const String USP_FARE_TICKET_TYPE_DELETE = "[dbo].[USP_FARE_TICKET_TYPE_DELETE]";
        public const String USP_FARE_TICKET_TYPE_INSERT = "[dbo].[USP_FARE_TICKET_TYPE_INSERT]";
        public const String USP_FARE_TICKET_TYPE_SELECT = "[dbo].[USP_FARE_TICKET_TYPE_SELECT]";
        public const String USP_FARE_TICKET_TYPE_UPDATE = "[dbo].[USP_FARE_TICKET_TYPE_UPDATE]";
        #endregion

        #region INQ_INQUIRY_AIRLINE_GDS_MAPPING Sps
        public const String USP_INQ_INQUIRY_AIRLINE_GDS_MAPPING_SELECT = "[dbo].[USP_INQ_INQUIRY_AIRLINE_GDS_MAPPING_SELECT]";
        public const String USP_INQ_INQUIRY_AIRLINE_GDS_MAPPING_INSERT = "[dbo].[USP_INQ_INQUIRY_AIRLINE_GDS_MAPPING_INSERT]";
        public const String USP_INQ_INQUIRY_AIRLINE_GDS_MAPPING_UPDATE = "[dbo].[USP_INQ_INQUIRY_AIRLINE_GDS_MAPPING_UPDATE]";
        public const String USP_INQ_INQUIRY_AIRLINE_GDS_MAPPING_DELETE = "[dbo].[USP_INQ_INQUIRY_AIRLINE_GDS_MAPPING_DELETE]";
        public const String USP_INQ_INQUIRY_AIRLINE_GDS_INQUIRY_SELECT_KEYVALUE = "[dbo].[USP_INQ_INQUIRY_AIRLINE_GDS_INQUIRY_SELECT_KEYVALUE]";
        public const String USP_INQ_INQUIRY_AIRPORT_CODE_SELECT_KEYVALUE = "[dbo].[USP_INQ_INQUIRY_AIRPORT_CODE_SELECT_KEYVALUE]";
        public const String USP_INQ_INQUIRY_AIRLINE_SELECT_KEYVALUE = "[dbo].[USP_INQ_INQUIRY_AIRLINE_SELECT_KEYVALUE]";
        public const String USP_INQUIRY_AIRLINE_GDSCODE_KEYVALUE = "[dbo].[USP_INQUIRY_AIRLINE_GDSCODE_KEYVALUE]";

        #endregion []

        #region GRDAirportMaster Sps
        public const String USP_FARE_AIRLINE_GDS_AIRPORT_DELETE = "[dbo].[USP_FARE_AIRLINE_GDS_AIRPORT_DELETE]";
        public const String USP_FARE_AIRLINE_GDS_AIRPORT_INSERT = "[dbo].[USP_FARE_AIRLINE_GDS_AIRPORT_INSERT]";
        public const String USP_FARE_AIRLINE_GDS_AIRPORT_SELECT = "[dbo].[USP_FARE_AIRLINE_GDS_AIRPORT_SELECT]";
        public const String USP_FARE_AIRLINE_GDS_AIRPORT_UPDATE = "[dbo].[USP_FARE_AIRLINE_GDS_AIRPORT_UPDATE]";

        #endregion

        #region PAYMENT LookUp Sps
        public const String USP_BOOKING_PAYMENT_MODE_DELETE = "[DBO].[USP_BOOKING_PAYMENT_MODE_DELETE]";
        public const String USP_BOOKING_PAYMENT_MODE_INSERT = "[DBO].[USP_BOOKING_PAYMENT_MODE_INSERT]";
        public const String USP_BOOKING_PAYMENT_MODE_UPDATE = "[DBO].[USP_BOOKING_PAYMENT_MODE_UPDATE]";
        public const String USP_BOOKING_PAYMENT_MODE_SELECT = "[DBO].[USP_BOOKING_PAYMENT_MODE_SELECT]";
        #endregion

        #region CheckListDetails LookUp Sps
        public const String USP_BOOKING_CHECKLIST_MST_DELETE = "[DBO].[USP_BOOKING_CHECKLIST_MST_DELETE]";
        public const String USP_BOOKING_CHECKLIST_MST_UPDATE = "[DBO].[USP_BOOKING_CHECKLIST_MST_UPDATE]";
        public const String USP_BOOKING_CHECKLIST_MST_SELECT = "[DBO].[USP_BOOKING_CHECKLIST_MST_SELECT]";
        public const String USP_BOOKING_CHECKLIST_MST_INSERT = "[DBO].[USP_BOOKING_CHECKLIST_MST_INSERT]";
        public const String USP_BOOKING_CHECKLIST_MST_SELECT_KEYVALUE = "[DBO].[USP_BOOKING_CHECKLIST_MST_SELECT_KEYVALUE]";
        #endregion

        #region TOUR_ITENARY_TYPE_MASTER  Sps
        public const String USP_FARE_TOUR_ITENARY_TYPE_MASTER_SELECT = "[dbo].[USP_FARE_TOUR_ITENARY_TYPE_MASTER_SELECT]";
        public const String USP_FARE_TOUR_ITENARY_TYPE_MASTER_INSERT = "[DBO].[USP_FARE_TOUR_ITENARY_TYPE_MASTER_INSERT]";
        public const String USP_FARE_TOUR_ITENARY_TYPE_MASTER_DELETE = "[DBO].[USP_FARE_TOUR_ITENARY_TYPE_MASTER_DELETE]";
        public const String USP_FARE_TOUR_ITENARY_TYPE_MASTER_UPDATE = "[DBO].[USP_FARE_TOUR_ITENARY_TYPE_MASTER_UPDATE]";
        #endregion

        #region CONTINENT MASTER  LookUp Sps
        public const String USP_COMMON_CONTINENT_MASTER_SELECT = "[dbo].[USP_COMMON_CONTINENT_MASTER_SELECT]";
        public const String USP_COMMON_CONTINENT_MASTER_DELETE = "[DBO].[USP_COMMON_CONTINENT_MASTER_DELETE]";
        public const String USP_COMMON_CONTINENT_MASTER_INSERT = "[DBO].[USP_COMMON_CONTINENT_MASTER_INSERT]";
        public const String USP_COMMON_CONTINENT_MASTER_UPDATE = "[DBO].[USP_COMMON_CONTINENT_MASTER_UPDATE]";
        #endregion

        /**********************************************************************************************************/

        /******************************** Login Procedures Constant - Brindesh ************************************/

        #region Login Sps
        public const String USP_SYS_VALIDATE_LOGIN = "[dbo].[SP_VALIDATE_LOGIN]";
        public const String USP_UPDATE_USER_ROLE_MAPPING_DEFAULT_PREFERENCE = "[dbo].[USP_UPDATE_USER_ROLE_MAPPING_DEFAULT_PREFERENCE]";
        #endregion

        /**********************************************************************************************************/

        /********************************* Customer Master Procedures - Nitesh ************************************/

        #region Customer Master Sps

        public const String USP_CUST_CUSTOMER_SELECT = "[dbo].[USP_CUST_CUSTOMER_SELECT]";
        public const String USP_CUST_CUSTOMER_INSERT = "[dbo].[USP_CUST_CUSTOMER_INSERT]";
        public const String USP_CUST_CUSTOMER_UPDATE = "[dbo].[USP_CUST_CUSTOMER_UPDATE]";
        public const String USP_CUST_CUSTOMER_DELETE = "[dbo].[USP_CUST_CUSTOMER_DELETE]";
        public const String USP_CUST_CUSTOMER_SELECT_EXPORT = "[dbo].[USP_CUST_CUSTOMER_SELECT_EXPORT]";
        public const String USP_CUST_CUSTOMER_SELECT_BY_PARAMETER = "[dbo].[USP_CUST_CUSTOMER_SELECT_BY_PARAMETER]";

        public const String USP_CUST_CUSTOMER_SELECT_PASSPORT_NO = "[dbo].[USP_CUST_CUSTOMER_SELECT_PASSPORT_NO]";

        public const String USP_CUST_CUSTOMER_RELATION_DETAILS_INSERT = "[dbo].[USP_CUST_CUSTOMER_RELATION_DETAILS_INSERT]";
        public const String USP_CUST_CUSTOMER_RELATION_DETAILS_UPDATE = "[dbo].[USP_CUST_CUSTOMER_RELATION_DETAILS_UPDATE]";
        public const String USP_CUST_CUSTOMER_RELATION_DETAILS_DELETE = "[dbo].[USP_CUST_CUSTOMER_RELATION_DETAILS_DELETE]";
        public const String USP_CUST_CUSTOMER_RELATION_DETAILS_SELECT = "[dbo].[USP_CUST_CUSTOMER_RELATION_DETAILS_SELECT]";

        public const String USP_CUST_NEXT_TRAVEL_PLAN_INSERT = "[dbo].[USP_CUST_NEXT_TRAVEL_PLAN_INSERT]";
        public const String USP_CUST_NEXT_TRAVEL_PLAN_SELECT = "[dbo].[USP_CUST_NEXT_TRAVEL_PLAN_SELECT]";
        public const String USP_CUST_NEXT_TRAVEL_PLAN_UPDATE = "[dbo].[USP_CUST_NEXT_TRAVEL_PLAN_UPDATE]";
        public const String USP_CUST_NEXT_TRAVEL_PLAN_DELETE = "[dbo].[USP_CUST_NEXT_TRAVEL_PLAN_DELETE]";

        public const String USP_CUST_TRAVEL_HISTORY_WITH_US_INSERT = "[dbo].[USP_CUST_TRAVEL_HISTORY_WITH_US_INSERT]";
        public const String USP_CUST_TRAVEL_HISTORY_WITH_US_SELECT = "[dbo].[USP_CUST_TRAVEL_HISTORY_WITH_US_SELECT]";
        public const String USP_CUST_TRAVEL_HISTORY_WITH_US_UPDATE = "[dbo].[USP_CUST_TRAVEL_HISTORY_WITH_US_UPDATE]";
        public const String USP_CUST_TRAVEL_HISTORY_WITH_US_DELETE = "[dbo].[USP_CUST_TRAVEL_HISTORY_WITH_US_DELETE]";

        public const String USP_CUST_TRAVEL_HISTORY_WITH_OTHER_INSERT = "[dbo].[USP_CUST_TRAVEL_HISTORY_WITH_OTHER_INSERT]";
        public const String USP_CUST_TRAVEL_HISTORY_WITH_OTHER_SELECT = "[dbo].[USP_CUST_TRAVEL_HISTORY_WITH_OTHER_SELECT]";
        public const String USP_CUST_TRAVEL_HISTORY_WITH_OTHER_UPDATE = "[dbo].[USP_CUST_TRAVEL_HISTORY_WITH_OTHER_UPDATE]";
        public const String USP_CUST_TRAVEL_HISTORY_WITH_OTHER_DELETE = "[dbo].[USP_CUST_TRAVEL_HISTORY_WITH_OTHER_DELETE]";


        public const String USP_CUST_CUSTOMER_INQUIRIES_FOLLOWUPS_DELETE = "[dbo].[USP_CUST_CUSTOMER_INQUIRIES_FOLLOWUPS_DELETE]";
        public const String USP_CUST_CUSTOMER_INQUIRIES_FOLLOWUPS_INSERT = "[dbo].[USP_CUST_CUSTOMER_INQUIRIES_FOLLOWUPS_INSERT]";
        public const String USP_CUST_CUSTOMER_INQUIRIES_FOLLOWUPS_SELECT = "[dbo].[USP_CUST_CUSTOMER_INQUIRIES_FOLLOWUPS_SELECT]";
        public const String USP_CUST_CUSTOMER_INQUIRIES_FOLLOWUPS_UPDATE = "[dbo].[USP_CUST_CUSTOMER_INQUIRIES_FOLLOWUPS_UPDATE]";

        public const String USP_CUST_CUSTOMER_CONTACT_DETAILS_SELECT = "[dbo].[USP_CUST_CUSTOMER_CONTACT_DETAILS_SELECT]";
        public const String USP_CUST_CUSTOMER_CONTACT_DETAILS_DELETE = "[dbo].[USP_CUST_CUSTOMER_CONTACT_DETAILS_DELETE]";

        public const String USP_CUST_CUSTOMER_DOCUMENTS_DELETE = "[dbo].[USP_CUST_CUSTOMER_DOCUMENTS_DELETE]";
        public const String USP_CUST_CUSTOMER_DOCUMENTS_INSERT = "[dbo].[USP_CUST_CUSTOMER_DOCUMENTS_INSERT]";
        public const String USP_CUST_CUSTOMER_DOCUMENTS_SELECT = "[dbo].[USP_CUST_CUSTOMER_DOCUMENTS_SELECT]";
        public const String USP_CUST_CUSTOMER_DOCUMENTS_UPDATE = "[dbo].[USP_CUST_CUSTOMER_DOCUMENTS_UPDATE]";

        public const String USP_CUST_CODE_MASTER_SELECT_KEYVALUE = "[dbo].[USP_CUST_CODE_MASTER_SELECT_KEYVALUE]";
        public const String USP_CUST_TYPE_MASTER_SELECT_KEYVALUE = "[dbo].[USP_CUST_TYPE_MASTER_SELECT_KEYVALUE]";
        public const String USP_ADMINISTRATION_MARITAL_STATUS_SELECT = "[dbo].[USP_ADMINISTRATION_MARITAL_STATUS_SELECT]";
        //public const String USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_SELECT = "[dbo].[USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_SELECT]";
        public const String USP_ADMINISTRATION_STATUS_SELECT = "[dbo].[USP_ADMINISTRATION_STATUS_SELECT]";
        public const String USP_COMPETITOR_AGENT_SELECT_KEYVALUE = "[dbo].[USP_COMPETITOR_AGENT_SELECT_KEYVALUE]";
        public const String USP_FARE_TOUR_SELECT_KEYVALUE = "[dbo].[USP_FARE_TOUR_SELECT_KEYVALUE]";
        public const String USP_ADMINISTRATION_ADDRESS_TYPE_SELECT_KEYVALUE = "[dbo].[USP_ADMINISTRATION_ADDRESS_TYPE_SELECT_KEYVALUE]";
        public const String USP_ADMINISTRATION_TOUR_TYPE_SELECT_KEYVALUE = "[dbo].[USP_ADMINISTRATION_TOUR_TYPE_SELECT_KEYVALUE]";
        public const String USP_COMPETITOR_AGENT_INSERT_TEMP = "[dbo].[USP_COMPETITOR_AGENT_INSERT_TEMP]";
        public const String USP_CUST_CUSTOMER_PROFILE_SELECT = "[dbo].[USP_CUST_CUSTOMER_PROFILE_SELECT]";
        public const String USP_CUST_CUSTOMER_PROFILE_FULL_SELECT = "[dbo].[USP_CUST_CUSTOMER_PROFILE_FULL_SELECT]";
        public const String USP_ADMINISTRATION_REFERENCE_SELECT_KEYVALUE = "[dbo].[USP_ADMINISTRATION_REFERENCE_SELECT_KEYVALUE]";
        public const String USP_ADMINISTRATION_INQUIRY_STATUS_SELECT_KEYVALUE = "[dbo].[USP_ADMINISTRATION_INQUIRY_STATUS_SELECT_KEYVALUE]";
        public const String USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_SELECT_KEYVALUE = "[dbo].[USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_SELECT_KEYVALUE]";
        public const String USP_ADMINISTRATION_STATUS_SELECT_KEYVALUE = "[dbo].[USP_ADMINISTRATION_STATUS_SELECT_KEYVALUE]";
        public const String USP_ADMINISTRATION_PRODUCT_SELECT_KEYVALUE = "[dbo].[USP_ADMINISTRATION_PRODUCT_SELECT_KEYVALUE]";

        #endregion

        public static string USP_FARE_AGENT_SELECT_KEYVALUE = "[dbo].[USP_FARE_AGENT_SELECT_KEYVALUE]";
        public static string USP_COMPANY_SELECT_KEYVALUE = "[dbo].[USP_COMPANY_SELECT_KEYVALUE]";
        public static string USP_BOOKING_CURRENCY_SELECT_KEYVALUE = "[dbo].[USP_BOOKING_CURRENCY_SELECT_KEYVALUE]";
        public static string USP_HR_EMPLOYEE_SELECT_KEYVALUE = "[dbo].[USP_HR_EMPLOYEE_SELECT_KEYVALUE]";
        public static string UPS_FARE_TOUR_SELECT_BY_ID = "[dbo].[UPS_FARE_TOUR_SELECT_BY_ID]";
        public static string USP_BOOKING_TOUR_BOOKING_HRD_INSERT = "[dbo].[USP_BOOKING_TOUR_BOOKING_HRD_INSERT]";
        public static string USP_BOOKING_TOUR_BOOKING_HRD_UPDATE = "[dbo].[USP_BOOKING_TOUR_BOOKING_HRD_UPDATE]";
        public static string USP_BOOKING_TOUR_BOOKING_HDR_SELECT = "[dbo].[USP_BOOKING_TOUR_BOOKING_HDR_SELECT]";
        public static string USP_BOOKING_TOUR_BOOKING_HDR_SELECT_BY_BOOKING_ID = "[dbo].[USP_BOOKING_TOUR_BOOKING_HDR_SELECT_BY_BOOKING_ID]";

        public static string USP_BOOKING_TOUR_BOOKING_DETAILS_SELECT = "[dbo].[USP_BOOKING_TOUR_BOOKING_DETAILS_SELECT]";
        public static string USP_BOOKING_TOUR_BOOKING_DETAILS_SELECT_FOR_ROOM_ALLOCATION = "[dbo].[USP_BOOKING_TOUR_BOOKING_DETAILS_SELECT_FOR_ROOM_ALLOCATION]";
        public static string USP_BOOKING_TOUR_BOOKING_DETAILS_SELECT_BY_ID = "[dbo].[USP_BOOKING_TOUR_BOOKING_DETAILS_SELECT_BY_ID]";
        public static string USP_BOOKING_TOUR_BOOKING_DETAILS_INSERT = "[dbo].[USP_BOOKING_TOUR_BOOKING_DETAILS_INSERT]";
        public static string USP_BOOKING_TOUR_BOOKING_DETAILS_UPDATE = "[dbo].[USP_BOOKING_TOUR_BOOKING_DETAILS_UPDATE]";
        public static string USP_BOOKING_TOUR_BOOKING_DETAILS_DELETE = "[dbo].[USP_BOOKING_TOUR_BOOKING_DETAILS_DELETE]";
        public static string USP_BOOKING_TOUR_BOOKING_ADDITIONAL_DETAILS_INSERT = "[dbo].[USP_BOOKING_TOUR_BOOKING_ADDITIONAL_DETAILS_INSERT]";
        public static string USP_BOOKING_TOUR_BOOKING_ADDITIONAL_DETAILS_UPDATE = "[dbo].[USP_BOOKING_TOUR_BOOKING_ADDITIONAL_DETAILS_UPDATE]";
        public static string USP_BOOKING_TOUR_BOOKING_ADDITIONAL_DETAILS_SELECT = "[dbo].[USP_BOOKING_TOUR_BOOKING_ADDITIONAL_DETAILS_SELECT]";
        public static string USP_BOOKING_TOUR_OPERATION_DETAILS_INSERT = "[dbo].[USP_BOOKING_TOUR_OPERATION_DETAILS_INSERT]";
        public static string USP_BOOKING_TOUR_OPERATION_DETAILS_UPDATE = "[dbo].[USP_BOOKING_TOUR_OPERATION_DETAILS_UPDATE]";
        public static string USP_BOOKING_TOUR_OPERATION_DETAILS_SELECT = "[dbo].[USP_BOOKING_TOUR_OPERATION_DETAILS_SELECT]";
        public static string USP_BOOKING_TOUR_BOOKING_CHECKLIST_SELECT = "[dbo].[USP_BOOKING_TOUR_BOOKING_CHECKLIST_SELECT]";
        public static string USP_BOOKING_TOUR_BOOKING_CHECKLIST_INSERT_UPDATE = "[dbo].[USP_BOOKING_TOUR_BOOKING_CHECKLIST_INSERT_UPDATE]";

        public static string USP_BOOKING_TOUR_BOOKING_HOTEL_ROOM_DETAILS_SELECT = "[dbo].[USP_BOOKING_TOUR_BOOKING_HOTEL_ROOM_DETAILS_SELECT]";
        public static string USP_BOOKING_TOUR_BOOKING_HOTEL_ROOM_DETAILS_INSERT = "[dbo].[USP_BOOKING_TOUR_BOOKING_HOTEL_ROOM_DETAILS_INSERT]";
        public static string USP_BOOKING_HOTEL_ROOM_COUNT = "[dbo].[USP_BOOKING_HOTEL_ROOM_COUNT]";
        public static string USP_FARE_TOUR_COPY_BY_TOUR = "[dbo].[USP_FARE_TOUR_COPY_BY_TOUR]";
        public static string USP_CUSTOMER_RELATION_BY_RELATIONID_SELECT = "[dbo].[USP_CUSTOMER_RELATION_BY_RELATIONID_SELECT]";
        public static string USP_INQ_INQUIRY_TOUR_NAME_BYID_SELECT = "[dbo].[USP_INQ_INQUIRY_TOUR_NAME_BYID_SELECT]";
        public static string USP_FARE_HOTEL_ROOMTYPE_BY_HOTELID_SELECT = "[dbo].[USP_FARE_HOTEL_ROOMTYPE_BY_HOTELID_SELECT]";
        public static string USP_FARE_HOTEL_CURRENCY_HOTEL_ROOMID_SELECT = "[dbo].[USP_FARE_HOTEL_CURRENCY_HOTEL_ROOMID_SELECT]";
        public static string USP_FARE_HOTEL_AMOUNT_ROOM_CURRENCYID_SELECT = "[dbo].[USP_FARE_HOTEL_AMOUNT_ROOM_CURRENCYID_SELECT]";
        public static string USP_CUST_TOURTYPE_ITENARY_SELECT = "[dbo].[USP_CUST_TOURTYPE_ITENARY_SELECT]";
        public static string USP_CUST_TOURNAME_CODE_ITENARY_BYID_SELECT = "[dbo].[USP_CUST_TOURNAME_CODE_ITENARY_BYID_SELECT]";
        public static string USP_FARE_TOUR_SELECT_BROWSE_CONTENT_KEYVALUE = "[dbo].[USP_FARE_TOUR_SELECT_BROWSE_CONTENT_KEYVALUE]";
        public static string SP_EMAIL_CUSTOMER = "[dbo].[SP_EMAIL_CUSTOMER]";
        public static string USP_EMAIL_TEMPLATE_SELECT = "[dbo].[USP_EMAIL_TEMPLATE_SELECT]";
        public static string USP_EMAIL_TEMPLATE_BYID_SELECT = "[dbo].[USP_EMAIL_TEMPLATE_BYID_SELECT]";
        public static string USP_FARE_TOUR_CURRENCY_PRICE_UPDATE = "[dbo].[USP_FARE_TOUR_CURRENCY_PRICE_UPDATE]";
        public static string USP_FARE_TOUR_CURRENCY_PRICE_INSERT = "[dbo].[USP_FARE_TOUR_CURRENCY_PRICE_INSERT]";
        public static string USP_FARE_TOUR_CURRENCY_PRICE_SELECT = "[USP_FARE_TOUR_CURRENCY_PRICE_SELECT]";

        public static string USP_HR_EMPLOYEE_SIGNATURE_SELECT = "[dbo].[USP_HR_EMPLOYEE_SIGNATURE_SELECT]";
        public static string USP_EMAIL_TEMPLATE_SIGNATURE_SELECT = "[dbo].[USP_EMAIL_TEMPLATE_SIGNATURE_SELECT]";

        public static string USP_FARE_BUS_NO_SELECT_BY_BUSNAME = "[dbo].[USP_FARE_BUS_NO_SELECT_BY_BUSNAME]";
        public static string USP_FARE_CRUISE_NO_SELECT_BY_CRUISENAME = "[dbo].[USP_FARE_CRUISE_NO_SELECT_BY_CRUISENAME]";


        


        #region TicketBooking

        public static string USP_BOOKING_TICKET_BOOKING_INSERT = "[dbo].[USP_BOOKING_TICKET_BOOKING_INSERT]";
        public static string USP_BOOKING_TICKET_BOOKING_UPDATE = "[dbo].[USP_BOOKING_TICKET_BOOKING_UPDATE]";
        public static string USP_BOOKING_TICKET_BOOKING_SELECT = "[dbo].[USP_BOOKING_TICKET_BOOKING_SELECT]";




        #endregion


        /**********************************************************************************************************/

        #region Destroy Command Object
        /// <summary>
        /// Closes the Command connection and destroys the Command Object.
        /// </summary>
        /// <param name="dbCmd">Command Object</param>
        public static void Destroy(ref DbCommand dbCmd)
        {
            try
            {
                if (dbCmd != null)
                {
                    if (dbCmd.Connection.State == ConnectionState.Open)
                    {
                        dbCmd.Connection.Close();
                    }
                }
                dbCmd = null;
            }
            catch (Exception) { }

        }

        #endregion

        #region Destroy DataReader Object
        /// <summary>
        /// Closes the reader and destroys the datareader Object.
        /// </summary>
        /// <param name="dread">DataReader Object</param>
        public static void Destroy(ref IDataReader dread)
        {
            try
            {
                if (dread != null)
                {
                    dread.Close();
                }

                dread = null;
            }
            catch (Exception) { }
        }
        #endregion


        #region Customer Email Template Type

        public static string USP_EMAIL_TEMPLATE_TYPE_MASTER_INSERT = "[dbo].[USP_EMAIL_TEMPLATE_TYPE_MASTER_INSERT]";
        public static string USP_EMAIL_TEMPLATE_TYPE_MASTER_SELECT = "[dbo].[USP_EMAIL_TEMPLATE_TYPE_MASTER_SELECT]";
        public static string USP_EMAIL_TEMPLATE_TYPE_MASTER_UPDATE = "[dbo].[USP_EMAIL_TEMPLATE_TYPE_MASTER_UPDATE]";
        public static string USP_EMAIL_TEMPLATE_TYPE_MASTER_DELETE = "[dbo].[USP_EMAIL_TEMPLATE_TYPE_MASTER_DELETE]";
        public static string USP_EMAIL_TEMPLATE_TYPE_MASTER_SELECT_BYID = "[dbo].[USP_EMAIL_TEMPLATE_TYPE_MASTER_SELECT_BYID]";





        #endregion

        #region FareBusMaster

        public static string USP_FARE_BUS_MASTER_INSERT = "[dbo].[USP_FARE_BUS_MASTER_INSERT]";
        public static string USP_FARE_BUS_MASTER_SELECT = "[dbo].[USP_FARE_BUS_MASTER_SELECT]";
        public static string USP_FARE_BUS_MASTER_SELECT_BY_ID = "[dbo].[USP_FARE_BUS_MASTER_SELECT_BY_ID]";
        public static string USP_FARE_BUS_MASTER_UPDATE = "[dbo].[USP_FARE_BUS_MASTER_UPDATE]";
        public static string USP_FARE_BUS_MASTER_DELETE = "[dbo].[USP_FARE_BUS_MASTER_DELETE]";


        public static string USP_FARE_BUS_CURRENCY_PRICE_INSERT = "[dbo].[USP_FARE_BUS_CURRENCY_PRICE_INSERT]";
        public static string USP_FARE_BUS_CURRENCY_PRICE_SELECT = "[dbo].[USP_FARE_BUS_CURRENCY_PRICE_SELECT]";
        public static string USP_FARE_BUS_CURRENCY_PRICE_UPDATE = "[dbo].[USP_FARE_BUS_CURRENCY_PRICE_UPDATE]";








        #endregion


        #region Cruise Master

        public static string USP_FARE_CRUISE_SCHEDULE_MASTER_INSERT = "[dbo].[USP_FARE_CRUISE_SCHEDULE_MASTER_INSERT]";
        public static string USP_FARE_CRUISE_SCHEDULE_MASTER_SELECT = "[dbo].[USP_FARE_CRUISE_SCHEDULE_MASTER_SELECT]";
        public static string USP_FARE_CRUISE_SCHEDULE_MASTER_UPDATE = "[dbo].[USP_FARE_CRUISE_SCHEDULE_MASTER_UPDATE]";
        public static string USP_FARE_CRUISE_SCHEDULE_MASTER_SELECT_BYID = "[dbo].[USP_FARE_CRUISE_SCHEDULE_MASTER_SELECT_BYID]";
        public static string USP_FARE_CRUISE_SCHEDULE_MASTER_DELETE = "[dbo].[USP_FARE_CRUISE_SCHEDULE_MASTER_DELETE]";
        public static string USP_FARE_CRUISE_CURRENCY_PRICE_INSERT = "[dbo].[USP_FARE_CRUISE_CURRENCY_PRICE_INSERT]";
        public static string USP_FARE_CRUISE_CURRENCY_PRICE_SELECT = "[dbo].[USP_FARE_CRUISE_CURRENCY_PRICE_SELECT]";
        public static string USP_FARE_CRUISE_CURRENCY_PRICE_UPDATE = "[dbo].[USP_FARE_CRUISE_CURRENCY_PRICE_UPDATE]";

        #endregion

        #region Email Template Lookup

        public static string USP_ADMINISTRATION_EMAIL_TEMPLATE_BYNAME = "[dbo].[USP_ADMINISTRATION_EMAIL_TEMPLATE_BYNAME]";
        public static string USP_ADMINISTRATION_EMAIL_TEMPLATE_DELETE = "[dbo].[USP_ADMINISTRATION_EMAIL_TEMPLATE_DELETE]";
        public static string USP_ADMINISTRATION_EMAIL_TEMPLATE_INSERT = "[dbo].[USP_ADMINISTRATION_EMAIL_TEMPLATE_INSERT]";
        public static string USP_ADMINISTRATION_EMAIL_TEMPLATE_UPDATE = "[dbo].[USP_ADMINISTRATION_EMAIL_TEMPLATE_UPDATE]";

        #endregion


        #region Airport Master

        public static string USP_FARE_AIRPORT_MASTER_INSERT_UPDATE = "[dbo].[USP_FARE_AIRPORT_MASTER_INSERT_UPDATE]";
        public static string USP_FARE_AIRPORT_MASTER_DELETE = "[dbo].[USP_FARE_AIRPORT_MASTER_DELETE]";
        public static string USP_FARE_AIRPORT_MASTER_DELETE_BYID = "[dbo].[USP_FARE_AIRPORT_MASTER_DELETE_BYID]";
        public static string USP_FARE_AIRPORT_MASTER_SELECT = "[dbo].[USP_FARE_AIRPORT_MASTER_SELECT]";
        public static string USP_FARE_AIRPORT_MASTER_SELECT_BYID = "[dbo].[USP_FARE_AIRPORT_MASTER_SELECT_BYID]";


        #endregion


        #region AirportAirLineMap

        public static string USP_FARE_AIRLINE_AIRPORT_MAP_DELETE = "[dbo].[USP_FARE_AIRLINE_AIRPORT_MAP_DELETE]";
        public static string USP_FARE_AIRLINE_AIRPORT_MAP_INSERT = "[dbo].[USP_FARE_AIRLINE_AIRPORT_MAP_INSERT]";
        public static string USP_FARE_AIRLINE_AIRPORT_MAP_SELECT = "[dbo].[USP_FARE_AIRLINE_AIRPORT_MAP_SELECT]";
        public static string USP_FARE_AIRLINE_AIRPORT_MAP_SELECT_BYID = "[dbo].[USP_FARE_AIRLINE_AIRPORT_MAP_SELECT_BYID]";
        public static string USP_FARE_AIRLINE_AIRPORT_MAP_UPDATE = "[dbo].[USP_FARE_AIRLINE_AIRPORT_MAP_UPDATE]";
        public static string USP_FARE_AIRLINE_AIRPORT_MAP_SELECT_BYCITY = "[dbo].[USP_FARE_AIRLINE_AIRPORT_MAP_SELECT_BYCITY]";

		
        #endregion

        #region by sunil for attachment in view email
        public static string USP_INQUIRY_EMAIL_ATTACHMENT_GET = "[dbo].[USP_INQUIRY_EMAIL_ATTACHMENT_GET]";
        public static string USP_INQUIRY_EMAIL_ATTACHMENT_SELECT = "[dbo].[USP_INQUIRY_EMAIL_ATTACHMENT_SELECT]";
        #endregion
        #region by sunil for inquiry delete
        public static string USP_INQ_INQUIRY_MAIN_DELETE = "[dbo].[USP_INQ_INQUIRY_MAIN_DELETE]";
        #endregion
    }
}