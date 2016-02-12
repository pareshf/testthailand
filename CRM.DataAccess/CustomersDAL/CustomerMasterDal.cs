#region Program Information
/**********************************************************************************************************************************************
 Class Name           : CustomerMasterDal
 Class Description    : Implementation Logic customer database releated transaction.
 Author               : Nitesh.
 Created Date         : Mar 2, 2010
***********************************************************************************************************************************************/
#endregion

#region Impoerts assemblies
using System;
using System.Data;
using System.Data.Common;
using CRM.Model.CustomersModel;
using CRM.Model.Security;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
#endregion

namespace CRM.DataAccess.CustomersDAL
{
    public class CustomerMasterDal : IDisposable
    {
        #region Customer Pesronal

        #region Get Customer void
        /// <summary>
        /// Gets customer list.
        /// </summary>
        /// <returns>Returns dataset contains custromer data.</returns>
        public DataSet GetCustomer()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_SELECT);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }
        #endregion

        #region Get Customer void
        /// <summary>
        /// Gets customer list.
        /// </summary>
        /// <returns>Returns dataset contains custromer data.</returns>
        public DataSet GetCustomerByCustId(int CustID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_PROFILE_BY_CUSTID_SELECT);
                if (CustID != 0)
                    db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, CustID);
                else
                    db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, DBNull.Value);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }
        #endregion

        #region Get Customer globle
        /// <summary>
        /// Gets customer list by filter criteria.
        /// </summary>
        /// <returns>Returns dataset contains custromer data.</returns>
        public DataSet GetCustomer(string searchParameter, int ownerCompanyId, int userId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_SELECT);
                if (!String.IsNullOrEmpty(searchParameter))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, searchParameter);
                else
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, DBNull.Value);

                if (ownerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, ownerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);

                if (userId != 0)
                    db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userId);
                else
                    db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, DBNull.Value);

                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                //bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                //if (rethrow)
                //{
                //    throw ex;
                //}
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }
        #endregion

        #region Get Customer parameter
        /// <summary>
        /// Gets customer list.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="fromCreatedDate"></param>
        /// <param name="toCreatedDate"></param>
        /// <param name="fromModifiedDate"></param>
        /// <param name="toModifiedDate"></param>
        /// <returns>Returns dataset contains custromer data.</returns>
        public DataSet GetCustomer(CustomerBDto customer, int ownerCompanyId, DateTime fromCreatedDate, DateTime toCreatedDate, DateTime fromModifiedDate, DateTime toModifiedDate)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_SELECT_BY_PARAMETER);

                if (!String.IsNullOrEmpty(customer.UniqueId))
                    db.AddInParameter(dbCmd, "@UNIQUE_ID", DbType.String, customer.UniqueId);
                else
                    db.AddInParameter(dbCmd, "@UNIQUE_ID", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.SurName))
                    db.AddInParameter(dbCmd, "@SURNAME", DbType.String, customer.SurName);
                else
                    db.AddInParameter(dbCmd, "@SURNAME", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.Name))
                    db.AddInParameter(dbCmd, "@NAME", DbType.String, customer.Name);
                else
                    db.AddInParameter(dbCmd, "@NAME", DbType.String, DBNull.Value);

                if (customer.GeneralInfo.Gender.ToString() != "0")
                    db.AddInParameter(dbCmd, "@GENDER", DbType.String, customer.GeneralInfo.Gender);
                else
                    db.AddInParameter(dbCmd, "@GENDER", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.ContactInfo.EmailId))
                    db.AddInParameter(dbCmd, "@EMAIL", DbType.String, customer.ContactInfo.EmailId);
                else
                    db.AddInParameter(dbCmd, "@EMAIL", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.ContactInfo.MobileNo))
                    db.AddInParameter(dbCmd, "@MOBILE", DbType.String, customer.ContactInfo.MobileNo);
                else
                    db.AddInParameter(dbCmd, "@MOBILE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.ContactInfo.PhoneNo))
                    db.AddInParameter(dbCmd, "@PHONE", DbType.String, customer.ContactInfo.PhoneNo);
                else
                    db.AddInParameter(dbCmd, "@PHONE", DbType.String, DBNull.Value);

                if (customer.GeneralInfo.MarriageStatusId != 0)
                    db.AddInParameter(dbCmd, "@MARITAL_STATUS_ID", DbType.Int32, customer.GeneralInfo.MarriageStatusId);
                else
                    db.AddInParameter(dbCmd, "@MARITAL_STATUS_ID", DbType.Int32, DBNull.Value);

                if (customer.GeneralInfo.ReligionId != 0)
                    db.AddInParameter(dbCmd, "@RELIGION_ID", DbType.Int32, customer.GeneralInfo.ReligionId);
                else
                    db.AddInParameter(dbCmd, "@RELIGION_ID", DbType.Int32, DBNull.Value);


                if (customer.AddressInfo.AddressTypeId != 0)
                    db.AddInParameter(dbCmd, "@ADDRESS_TYPE_ID", DbType.Int32, customer.AddressInfo.AddressTypeId);
                else
                    db.AddInParameter(dbCmd, "@ADDRESS_TYPE_ID", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.AddressInfo.AddressLine1))
                    db.AddInParameter(dbCmd, "@ADDRESS_LINE1", DbType.String, customer.AddressInfo.AddressLine1);
                else
                    db.AddInParameter(dbCmd, "@ADDRESS_LINE1", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.AddressInfo.AddressLine2))
                    db.AddInParameter(dbCmd, "@ADDRESS_LINE2", DbType.String, customer.AddressInfo.AddressLine2);
                else
                    db.AddInParameter(dbCmd, "@ADDRESS_LINE2", DbType.String, DBNull.Value);

                if (customer.AddressInfo.CityId != 0)
                    db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, customer.AddressInfo.CityId);
                else
                    db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, DBNull.Value);

                if (customer.AddressInfo.StateId != 0)
                    db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, customer.AddressInfo.StateId);
                else
                    db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, DBNull.Value);

                if (customer.AddressInfo.CountryId != 0)
                    db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, customer.AddressInfo.CountryId);
                else
                    db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.AddressInfo.PinCodeNo))
                    db.AddInParameter(dbCmd, "@PINCODE", DbType.String, customer.AddressInfo.PinCodeNo);
                else
                    db.AddInParameter(dbCmd, "@PINCODE", DbType.String, DBNull.Value);


                if (customer.TypeId != 0)
                    db.AddInParameter(dbCmd, "@TYPE_ID", DbType.Int32, customer.TypeId);
                else
                    db.AddInParameter(dbCmd, "@TYPE_ID", DbType.Int32, DBNull.Value);

                if (customer.CodeId != 0)
                    db.AddInParameter(dbCmd, "@CODE_ID", DbType.Int32, customer.CodeId);
                else
                    db.AddInParameter(dbCmd, "@CODE_ID", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.CompanyName))
                    db.AddInParameter(dbCmd, "@COMPANY_NAME", DbType.String, customer.CompanyName);
                else
                    db.AddInParameter(dbCmd, "@COMPANY_NAME", DbType.String, DBNull.Value);

                if (customer.ProfessionId != 0)
                    db.AddInParameter(dbCmd, "@PROFESSION_ID", DbType.Int32, customer.ProfessionId);
                else
                    db.AddInParameter(dbCmd, "@PROFESSION_ID", DbType.Int32, DBNull.Value);

                if (customer.AnnualIncome != 0)
                    db.AddInParameter(dbCmd, "@ANNUAL_INCOME", DbType.Decimal, customer.AnnualIncome);
                else
                    db.AddInParameter(dbCmd, "@ANNUAL_INCOME", DbType.Decimal, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.PassPortInfo.PassportNo))
                    db.AddInParameter(dbCmd, "@PASSPORT_NO", DbType.String, customer.PassPortInfo.PassportNo);
                else
                    db.AddInParameter(dbCmd, "@PASSPORT_NO", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.PassPortInfo.PrintName))
                    db.AddInParameter(dbCmd, "@PASSPORT_PRINTED_NAME", DbType.String, customer.PassPortInfo.PrintName);
                else
                    db.AddInParameter(dbCmd, "@PASSPORT_PRINTED_NAME", DbType.String, DBNull.Value);


                if (customer.PassPortInfo.IssueDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@PASSPORT_ISSUE_DATE", DbType.DateTime, customer.PassPortInfo.IssueDate);
                else
                    db.AddInParameter(dbCmd, "@PASSPORT_ISSUE_DATE", DbType.DateTime, DBNull.Value);

                if (customer.PassPortInfo.EntryDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@PASSPORT_EXPIRY_DATE", DbType.DateTime, customer.PassPortInfo.EntryDate);
                else
                    db.AddInParameter(dbCmd, "@PASSPORT_EXPIRY_DATE", DbType.DateTime, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.PassPortInfo.IssuePlace))
                    db.AddInParameter(dbCmd, "@PASSPORT_ISSUE_PLACE", DbType.String, customer.PassPortInfo.IssuePlace);
                else
                    db.AddInParameter(dbCmd, "@PASSPORT_ISSUE_PLACE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.PassPortInfo.IssueCountry))
                    db.AddInParameter(dbCmd, "@PASSPORT_ISSUE_COUNTRY", DbType.String, customer.PassPortInfo.IssueCountry);
                else
                    db.AddInParameter(dbCmd, "@PASSPORT_ISSUE_COUNTRY", DbType.String, DBNull.Value);

                if (ownerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, ownerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);

                if (customer.UserId != 0)
                    db.AddInParameter(dbCmd, "@USER_ID", DbType.Decimal, customer.UserId);
                else
                    db.AddInParameter(dbCmd, "@USER_ID", DbType.Decimal, DBNull.Value);

                if (fromCreatedDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CREATED_DATE_FROM", DbType.DateTime, fromCreatedDate);
                else
                    db.AddInParameter(dbCmd, "@CREATED_DATE_FROM", DbType.DateTime, DBNull.Value);

                if (toCreatedDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CREATED_DATE_TO", DbType.DateTime, toCreatedDate);
                else
                    db.AddInParameter(dbCmd, "@CREATED_DATE_TO", DbType.DateTime, DBNull.Value);

                if (fromModifiedDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@MODIFIED_DATE_FROM", DbType.DateTime, fromModifiedDate);
                else
                    db.AddInParameter(dbCmd, "@MODIFIED_DATE_FROM", DbType.DateTime, DBNull.Value);

                if (toModifiedDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@MODIFIED_DATE_TO", DbType.DateTime, toModifiedDate);
                else
                    db.AddInParameter(dbCmd, "@MODIFIED_DATE_TO", DbType.DateTime, DBNull.Value);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }
        #endregion

        #region Get Customer For Export
        /// <summary>
        /// Gets customer list.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="fromCreatedDate"></param>
        /// <param name="toCreatedDate"></param>
        /// <param name="fromModifiedDate"></param>
        /// <param name="toModifiedDate"></param>
        /// <returns>Returns dataset contains custromer data.</returns>
        public DataSet GetCustomerExport(CustomerBDto customer, int ownerCompanyId, DateTime fromCreatedDate, DateTime toCreatedDate, DateTime fromModifiedDate, DateTime toModifiedDate)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                //UserProfileBDto UserName,
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_SELECT_EXPORT);
                // customer.EmployeeName =@EMP_NAME_ALL

                if (!String.IsNullOrEmpty(customer.EmployeeName) && customer.EmployeeName == "All" )

                //    db.AddInParameter(dbCmd, "@EMP_NAME_ALL", DbType.String, customer.EmployeeId);
                //else 
                    db.AddInParameter(dbCmd, "@EMP_NAME_ALL", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.EmployeeName) && customer.EmployeeName != "All")

                  db.AddInParameter(dbCmd, "@EMP_NAME", DbType.String, customer.EmployeeName);
              else
                  db.AddInParameter(dbCmd, "@EMP_NAME", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.UniqueId))
                    db.AddInParameter(dbCmd, "@UNIQUE_ID", DbType.String, customer.UniqueId);
                else
                    db.AddInParameter(dbCmd, "@UNIQUE_ID", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.SurName))
                    db.AddInParameter(dbCmd, "@SURNAME", DbType.String, customer.SurName);
                else
                    db.AddInParameter(dbCmd, "@SURNAME", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.Name))
                    db.AddInParameter(dbCmd, "@NAME", DbType.String, customer.Name);
                else
                    db.AddInParameter(dbCmd, "@NAME", DbType.String, DBNull.Value);

                if (customer.GeneralInfo.Gender.ToString() != "0")
                    db.AddInParameter(dbCmd, "@GENDER", DbType.String, customer.GeneralInfo.Gender);
                else
                    db.AddInParameter(dbCmd, "@GENDER", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.ContactInfo.EmailId))
                    db.AddInParameter(dbCmd, "@EMAIL", DbType.String, customer.ContactInfo.EmailId);
                else
                    db.AddInParameter(dbCmd, "@EMAIL", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.ContactInfo.MobileNo))
                    db.AddInParameter(dbCmd, "@MOBILE", DbType.String, customer.ContactInfo.MobileNo);
                else
                    db.AddInParameter(dbCmd, "@MOBILE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.ContactInfo.PhoneNo))
                    db.AddInParameter(dbCmd, "@PHONE", DbType.String, customer.ContactInfo.PhoneNo);
                else
                    db.AddInParameter(dbCmd, "@PHONE", DbType.String, DBNull.Value);

                if (customer.GeneralInfo.MarriageStatusId != 0)
                    db.AddInParameter(dbCmd, "@MARITAL_STATUS_ID", DbType.Int32, customer.GeneralInfo.MarriageStatusId);
                else
                    db.AddInParameter(dbCmd, "@MARITAL_STATUS_ID", DbType.Int32, DBNull.Value);

                if (customer.GeneralInfo.ReligionId != 0)
                    db.AddInParameter(dbCmd, "@RELIGION_ID", DbType.Int32, customer.GeneralInfo.ReligionId);
                else
                    db.AddInParameter(dbCmd, "@RELIGION_ID", DbType.Int32, DBNull.Value);


                if (customer.AddressInfo.AddressTypeId != 0)
                    db.AddInParameter(dbCmd, "@ADDRESS_TYPE_ID", DbType.Int32, customer.AddressInfo.AddressTypeId);
                else
                    db.AddInParameter(dbCmd, "@ADDRESS_TYPE_ID", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.AddressInfo.AddressLine1))
                    db.AddInParameter(dbCmd, "@ADDRESS_LINE1", DbType.String, customer.AddressInfo.AddressLine1);
                else
                    db.AddInParameter(dbCmd, "@ADDRESS_LINE1", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.AddressInfo.AddressLine2))
                    db.AddInParameter(dbCmd, "@ADDRESS_LINE2", DbType.String, customer.AddressInfo.AddressLine2);
                else
                    db.AddInParameter(dbCmd, "@ADDRESS_LINE2", DbType.String, DBNull.Value);

                if (customer.AddressInfo.CityId != 0)
                    db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, customer.AddressInfo.CityId);
                else
                    db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, DBNull.Value);

                if (customer.AddressInfo.StateId != 0)
                    db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, customer.AddressInfo.StateId);
                else
                    db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, DBNull.Value);

                if (customer.AddressInfo.CountryId != 0)
                    db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, customer.AddressInfo.CountryId);
                else
                    db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.AddressInfo.PinCodeNo))
                    db.AddInParameter(dbCmd, "@PINCODE", DbType.String, customer.AddressInfo.PinCodeNo);
                else
                    db.AddInParameter(dbCmd, "@PINCODE", DbType.String, DBNull.Value);


                if (customer.TypeId != 0)
                    db.AddInParameter(dbCmd, "@TYPE_ID", DbType.Int32, customer.TypeId);
                else
                    db.AddInParameter(dbCmd, "@TYPE_ID", DbType.Int32, DBNull.Value);

                if (customer.CodeId != 0)
                    db.AddInParameter(dbCmd, "@CODE_ID", DbType.Int32, customer.CodeId);
                else
                    db.AddInParameter(dbCmd, "@CODE_ID", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.CompanyName))
                    db.AddInParameter(dbCmd, "@COMPANY_NAME", DbType.String, customer.CompanyName);
                else
                    db.AddInParameter(dbCmd, "@COMPANY_NAME", DbType.String, DBNull.Value);

                if (customer.ProfessionId != 0)
                    db.AddInParameter(dbCmd, "@PROFESSION_ID", DbType.Int32, customer.ProfessionId);
                else
                    db.AddInParameter(dbCmd, "@PROFESSION_ID", DbType.Int32, DBNull.Value);

                if (customer.AnnualIncome != 0)
                    db.AddInParameter(dbCmd, "@ANNUAL_INCOME", DbType.Decimal, customer.AnnualIncome);
                else
                    db.AddInParameter(dbCmd, "@ANNUAL_INCOME", DbType.Decimal, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.PassPortInfo.PassportNo))
                    db.AddInParameter(dbCmd, "@PASSPORT_NO", DbType.String, customer.PassPortInfo.PassportNo);
                else
                    db.AddInParameter(dbCmd, "@PASSPORT_NO", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.PassPortInfo.PrintName))
                    db.AddInParameter(dbCmd, "@PASSPORT_PRINTED_NAME", DbType.String, customer.PassPortInfo.PrintName);
                else
                    db.AddInParameter(dbCmd, "@PASSPORT_PRINTED_NAME", DbType.String, DBNull.Value);


                if (customer.PassPortInfo.IssueDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@PASSPORT_ISSUE_DATE", DbType.DateTime, customer.PassPortInfo.IssueDate);
                else
                    db.AddInParameter(dbCmd, "@PASSPORT_ISSUE_DATE", DbType.DateTime, DBNull.Value);

                if (customer.PassPortInfo.EntryDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@PASSPORT_EXPIRY_DATE", DbType.DateTime, customer.PassPortInfo.EntryDate);
                else
                    db.AddInParameter(dbCmd, "@PASSPORT_EXPIRY_DATE", DbType.DateTime, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.PassPortInfo.IssuePlace))
                    db.AddInParameter(dbCmd, "@PASSPORT_ISSUE_PLACE", DbType.String, customer.PassPortInfo.IssuePlace);
                else
                    db.AddInParameter(dbCmd, "@PASSPORT_ISSUE_PLACE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.PassPortInfo.IssueCountry))
                    db.AddInParameter(dbCmd, "@PASSPORT_ISSUE_COUNTRY", DbType.String, customer.PassPortInfo.IssueCountry);
                else
                    db.AddInParameter(dbCmd, "@PASSPORT_ISSUE_COUNTRY", DbType.String, DBNull.Value);

                if (ownerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, ownerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);

                if (fromCreatedDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CREATED_DATE_FROM", DbType.DateTime, fromCreatedDate);
                else
                    db.AddInParameter(dbCmd, "@CREATED_DATE_FROM", DbType.DateTime, DBNull.Value);

                if (toCreatedDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CREATED_DATE_TO", DbType.DateTime, toCreatedDate);
                else
                    db.AddInParameter(dbCmd, "@CREATED_DATE_TO", DbType.DateTime, DBNull.Value);

                if (fromModifiedDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@MODIFIED_DATE_FROM", DbType.DateTime, fromModifiedDate);
                else
                    db.AddInParameter(dbCmd, "@MODIFIED_DATE_FROM", DbType.DateTime, DBNull.Value);

                if (toModifiedDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@MODIFIED_DATE_TO", DbType.DateTime, toModifiedDate);
                else
                    db.AddInParameter(dbCmd, "@MODIFIED_DATE_TO", DbType.DateTime, DBNull.Value);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }
        #endregion

        public DataTable GetCustomerPassportNo(int CustId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_SELECT_PASSPORT_NO);
                if (CustId != 0)
                    db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, CustId);
                else
                    db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, DBNull.Value);
                ds = db.ExecuteDataSet(dbCmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return dt;
        }


       

        

        public DataTable GetCustomerRelativePassportNo(int CustId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_CUST_CUSTOMER_RELATION_SELECT_PASSPORT_NO");
                if (CustId != 0)
                    db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, CustId);
                else
                    db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, DBNull.Value);
                ds = db.ExecuteDataSet(dbCmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return dt;
        }


        public DataTable GetCustomerMobileNo(int CustId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_CUST_CUSTOMER_MOBLIE_SELECT");
                if (CustId != 0)
                    db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, CustId);
                else
                    db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, DBNull.Value);
                ds = db.ExecuteDataSet(dbCmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return dt;
        }


        public DataTable GetCustomerRelationMobileNo(int CustId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_CUST_CUSTOMER_RELATION_MOBLIE_SELECT");
                if (CustId != 0)
                    db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, CustId);
                else
                    db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, DBNull.Value);
                ds = db.ExecuteDataSet(dbCmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return dt;
        }

        #region Insert Customer
        /// <summary>
        /// Insert customer detail.
        /// </summary>
        /// <param name="customer">CustomerBDto object that customer data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int InsertCustomer(CustomerBDto customer, ref string customerUniqueId)
        {
            string str1;
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_INSERT);

                //db.AddInParameter(dbCmd, "@Cust_Unique_Id_Initial", DbType.String, customer.UniqueIdInitial);

                db.AddInParameter(dbCmd, "@CUST_TYPE_ID", DbType.Int32, customer.TypeId);
                db.AddInParameter(dbCmd, "@CUST_TITLE", DbType.Int32, customer.TitleId);
                db.AddInParameter(dbCmd, "@CUST_SURNAME", DbType.String, customer.SurName);
                db.AddInParameter(dbCmd, "@CUST_NAME", DbType.String, customer.Name);
                db.AddInParameter(dbCmd, "@CUST_PROFILE", DbType.String, customer.Profile);
                if (customer.ProfessionId != 0)
                    db.AddInParameter(dbCmd, "@CUST_PROFESSION_ID", DbType.Int32, customer.ProfessionId);
                else
                    db.AddInParameter(dbCmd, "@CUST_PROFESSION_ID", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@CUST_CODE_ID", DbType.Int32, customer.CodeId);
                db.AddInParameter(dbCmd, "@CUST_COMPANY_NAME", DbType.String, customer.CompanyName);

                if (customer.OwnerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, customer.OwnerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.Remarks))
                    db.AddInParameter(dbCmd, "@REMARKS", DbType.String, customer.Remarks);
                else
                    db.AddInParameter(dbCmd, "@REMARKS", DbType.String, DBNull.Value);

                if (customer.CustomerPhoto != null)
                    db.AddInParameter(dbCmd, "@PHOTO", DbType.Binary, customer.CustomerPhoto);
                else
                    db.AddInParameter(dbCmd, "@PHOTO", DbType.Binary, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.CustomerPhotoType))
                    db.AddInParameter(dbCmd, "@PHOTO_CONTENT_TYPE", DbType.String, customer.CustomerPhotoType);
                else
                    db.AddInParameter(dbCmd, "@PHOTO_CONTENT_TYPE", DbType.String, DBNull.Value);

                if (customer.GeneralInfo.Gender != '0')
                    db.AddInParameter(dbCmd, "@CUST_REL_GENDER", DbType.String, customer.GeneralInfo.Gender);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_GENDER", DbType.String, DBNull.Value);

                if (customer.GeneralInfo.BirthDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CUST_BIRTH_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@CUST_BIRTH_DATE", DbType.DateTime, customer.GeneralInfo.BirthDate);

                if (customer.GeneralInfo.MarriageStatusId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_MARITAL_STATUS_ID", DbType.Int32, customer.GeneralInfo.MarriageStatusId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_MARITAL_STATUS_ID", DbType.Int32, DBNull.Value);

                if (customer.GeneralInfo.MarriageDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CUST_REL_MARRIAGE_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_MARRIAGE_DATE", DbType.DateTime, customer.GeneralInfo.MarriageDate);

                if (customer.GeneralInfo.ReligionId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_RELIGION_ID", DbType.Int32, customer.GeneralInfo.ReligionId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_RELIGION_ID", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@CUST_REL_EMAIL", DbType.String, customer.ContactInfo.EmailId);
                db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, customer.ContactInfo.MobileNo);
                db.AddInParameter(dbCmd, "@CUST_REL_PHONE", DbType.String, customer.ContactInfo.PhoneNo);

                if (customer.AnnualIncome != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_ANNUAL_INCOME", DbType.Decimal, customer.AnnualIncome);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_ANNUAL_INCOME", DbType.Decimal, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.PassPortInfo.PassportNo))
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_NO", DbType.String, customer.PassPortInfo.PassportNo);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_NO", DbType.String, DBNull.Value);

                if (customer.PassPortInfo.IssueDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_DATE", DbType.DateTime, customer.PassPortInfo.IssueDate);

                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_PLACE", DbType.String, customer.PassPortInfo.IssuePlace);

                if (customer.PassPortInfo.EntryDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_EXPIRY_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_EXPIRY_DATE", DbType.DateTime, customer.PassPortInfo.EntryDate);


                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_PRINTED_NAME", DbType.String, customer.PassPortInfo.PrintName);
                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_COUNTRY", DbType.String, customer.PassPortInfo.IssueCountry);
                db.AddInParameter(dbCmd, "@CUST_REL_STATE", DbType.String, customer.RelationState);

                if (!string.IsNullOrEmpty(customer.XmlData))
                {
                    str1 = customer.XmlData;
                    customer.XmlData = str1.Replace("&", "&amp;");
                    db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml, customer.XmlData);

                }
                else
                    db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml, DBNull.Value);

                if (!string.IsNullOrEmpty(customer.PrefAirlineXmlData))
                    db.AddInParameter(dbCmd, "@PREF_AIRLINE_XML_DATA", DbType.Xml, customer.PrefAirlineXmlData);
                else
                    db.AddInParameter(dbCmd, "@PREF_AIRLINE_XML_DATA", DbType.Xml, DBNull.Value);

                if (!string.IsNullOrEmpty(customer.VisaXmlData))
                    db.AddInParameter(dbCmd, "@VISA_XML_DATA", DbType.Xml, customer.VisaXmlData);
                else
                    db.AddInParameter(dbCmd, "@VISA_XML_DATA", DbType.Xml, DBNull.Value);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, customer.UserId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                db.AddOutParameter(dbCmd, "@CUST_UNQ_ID", DbType.String, 50);
                int x = db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
                customerUniqueId = db.GetParameterValue(dbCmd, "@CUST_UNQ_ID").ToString();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }
        #endregion

        #region Update Customer
        /// <summary>
        /// Update customer detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateCustomer(CustomerBDto customer)
        {
            string str1;
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_UPDATE);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, customer.CustomerId);
                db.AddInParameter(dbCmd, "@CUST_TYPE_ID", DbType.Int32, customer.TypeId);
                db.AddInParameter(dbCmd, "@CUST_TITLE", DbType.Int32, customer.TitleId);
                db.AddInParameter(dbCmd, "@CUST_SURNAME", DbType.String, customer.SurName);
                db.AddInParameter(dbCmd, "@CUST_NAME", DbType.String, customer.Name);
                db.AddInParameter(dbCmd, "@CUST_PROFILE", DbType.String, customer.Profile);

                if (customer.ProfessionId != 0)
                    db.AddInParameter(dbCmd, "@CUST_PROFESSION_ID", DbType.Int32, customer.ProfessionId);
                else
                    db.AddInParameter(dbCmd, "@CUST_PROFESSION_ID", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@CUST_CODE_ID", DbType.Int32, customer.CodeId);

                if (customer.OwnerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, customer.OwnerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.CompanyName))
                    db.AddInParameter(dbCmd, "@CUST_COMPANY_NAME", DbType.String, customer.CompanyName);
                else
                    db.AddInParameter(dbCmd, "@CUST_COMPANY_NAME", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.Remarks))
                    db.AddInParameter(dbCmd, "@REMARKS", DbType.String, customer.Remarks);
                else
                    db.AddInParameter(dbCmd, "@REMARKS", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@IsPhotoChange", DbType.Boolean, customer.IsPhotoChanged);

                if (customer.CustomerPhoto != null)
                    db.AddInParameter(dbCmd, "@PHOTO", DbType.Binary, customer.CustomerPhoto);
                else
                    db.AddInParameter(dbCmd, "@PHOTO", DbType.Binary, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.CustomerPhotoType))
                    db.AddInParameter(dbCmd, "@PHOTO_CONTENT_TYPE", DbType.String, customer.CustomerPhotoType);
                else
                    db.AddInParameter(dbCmd, "@PHOTO_CONTENT_TYPE", DbType.String, DBNull.Value);

                if (customer.GeneralInfo.Gender != '0')
                    db.AddInParameter(dbCmd, "@CUST_REL_GENDER", DbType.String, customer.GeneralInfo.Gender);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_GENDER", DbType.String, DBNull.Value);

                if (customer.GeneralInfo.BirthDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CUST_BIRTH_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@CUST_BIRTH_DATE", DbType.DateTime, customer.GeneralInfo.BirthDate);

                if (customer.GeneralInfo.MarriageStatusId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_MARITAL_STATUS_ID", DbType.Int32, customer.GeneralInfo.MarriageStatusId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_MARITAL_STATUS_ID", DbType.Int32, DBNull.Value);

                if (customer.GeneralInfo.MarriageDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CUST_REL_MARRIAGE_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_MARRIAGE_DATE", DbType.DateTime, customer.GeneralInfo.MarriageDate);

                if (customer.GeneralInfo.ReligionId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_RELIGION_ID", DbType.Int32, customer.GeneralInfo.ReligionId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_RELIGION_ID", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@CUST_REL_EMAIL", DbType.String, customer.ContactInfo.EmailId);
                db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, customer.ContactInfo.MobileNo);
                db.AddInParameter(dbCmd, "@CUST_REL_PHONE", DbType.String, customer.ContactInfo.PhoneNo);

                if (customer.AnnualIncome != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_ANNUAL_INCOME", DbType.Decimal, customer.AnnualIncome);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_ANNUAL_INCOME", DbType.Decimal, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.PassPortInfo.PassportNo))
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_NO", DbType.String, customer.PassPortInfo.PassportNo);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_NO", DbType.String, DBNull.Value);

                if (customer.PassPortInfo.IssueDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_DATE", DbType.DateTime, customer.PassPortInfo.IssueDate);

                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_PLACE", DbType.String, customer.PassPortInfo.IssuePlace);

                if (customer.PassPortInfo.EntryDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_EXPIRY_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_EXPIRY_DATE", DbType.DateTime, customer.PassPortInfo.EntryDate);


                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_PRINTED_NAME", DbType.String, customer.PassPortInfo.PrintName);
                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_COUNTRY", DbType.String, customer.PassPortInfo.IssueCountry);
                db.AddInParameter(dbCmd, "@CUST_REL_STATE", DbType.String, customer.RelationState);

                if (!string.IsNullOrEmpty(customer.XmlData))
                {
                    str1 = customer.XmlData;
                    customer.XmlData = str1.Replace("&", "&amp;");
                    db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml, customer.XmlData);
                }
                else
                    db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml, DBNull.Value);

                if (!string.IsNullOrEmpty(customer.PrefAirlineXmlData))
                    db.AddInParameter(dbCmd, "@PREF_AIRLINE_XML_DATA", DbType.Xml, customer.PrefAirlineXmlData);
                else
                    db.AddInParameter(dbCmd, "@PREF_AIRLINE_XML_DATA", DbType.Xml, DBNull.Value);

                if (!string.IsNullOrEmpty(customer.VisaXmlData))
                    db.AddInParameter(dbCmd, "@VISA_XML_DATA", DbType.Xml, customer.VisaXmlData);
                else
                    db.AddInParameter(dbCmd, "@VISA_XML_DATA", DbType.Xml, DBNull.Value);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, customer.UserId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }
        #endregion

        #region DeleteCustomer
        /// <summary>
        /// Delete customers detail.
        /// </summary>
        /// <param name="idCollections">Customer Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteCustomer(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_DELETE);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.String, idCollections);
                db.AddOutParameter(dbCmd, "@IS_DELETE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_DELETE"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }
        #endregion

        #endregion

        #region Customer Relation

        #region Get Customer Relation
        /// <summary>
        /// Gets Customer Relation list.
        /// </summary>
        /// <returns>Returns dataset contains custromer relation data.</returns>
        public DataSet GetCustomerRelation(int customerId, int ownerCompanyId, string searchParameter)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_RELATION_DETAILS_SELECT);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, customerId);
                db.AddInParameter(dbCmd, "@REL_ID", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(searchParameter))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, searchParameter);
                else
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, DBNull.Value);

                if (ownerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, ownerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);

                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }
        #endregion

        #region Get Customer Relation
        /// <summary>
        /// Gets Customer Relation list.
        /// </summary>
        /// <returns>Returns dataset contains custromer relation data.</returns>
        public DataSet GetCustomerRelation(int customerId, int ownerCompanyId, int relationId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_RELATION_DETAILS_SELECT);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, customerId);
                if (relationId != 0)
                    db.AddInParameter(dbCmd, "@REL_ID", DbType.Int32, relationId);
                else
                    db.AddInParameter(dbCmd, "@REL_ID", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, DBNull.Value);

                if (ownerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, ownerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);

                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }
        #endregion

        #region Insert Customer Relation
        /// <summary>
        /// Insert customer relation detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertCustomerRelation(CustomerBDto customer)
        {
            string str1;

            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_RELATION_DETAILS_INSERT);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, customer.CustomerId);
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, customer.RelationId);

                if (customer.TitleId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_TITLE", DbType.Int32, customer.TitleId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_TITLE", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@CUST_REL_SURNAME", DbType.String, customer.SurName);
                db.AddInParameter(dbCmd, "@CUST_REL_NAME", DbType.String, customer.Name);


                if (customer.GeneralInfo.Gender != '0')
                    db.AddInParameter(dbCmd, "@CUST_REL_GENDER", DbType.String, customer.GeneralInfo.Gender);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_GENDER", DbType.String, DBNull.Value);


                if (customer.GeneralInfo.BirthDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CUST_BIRTH_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@CUST_BIRTH_DATE", DbType.DateTime, customer.GeneralInfo.BirthDate);

                if (customer.GeneralInfo.MarriageStatusId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_MARITAL_STATUS_ID", DbType.Int32, customer.GeneralInfo.MarriageStatusId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_MARITAL_STATUS_ID", DbType.Int32, DBNull.Value);

                if (customer.GeneralInfo.MarriageDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CUST_REL_MARRIAGE_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_MARRIAGE_DATE", DbType.DateTime, customer.GeneralInfo.MarriageDate);

                if (customer.GeneralInfo.ReligionId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_RELIGION_ID", DbType.Int32, customer.GeneralInfo.ReligionId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_RELIGION_ID", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@CUST_REL_EMAIL", DbType.String, customer.ContactInfo.EmailId);
                //db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, customer.ContactInfo.MobileNo);
                if (!string.IsNullOrEmpty(customer.ContactInfo.MobileNo))
                    db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, customer.ContactInfo.MobileNo);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, DBNull.Value);
                db.AddInParameter(dbCmd, "@CUST_REL_PHONE", DbType.String, customer.ContactInfo.PhoneNo);

                if (customer.ProfessionId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_PROFESSION_ID", DbType.Int32, customer.ProfessionId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_PROFESSION_ID", DbType.Int32, DBNull.Value);

                if (customer.AnnualIncome != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_ANNUAL_INCOME", DbType.Decimal, customer.AnnualIncome);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_ANNUAL_INCOME", DbType.Decimal, DBNull.Value);

                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_NO", DbType.String, customer.PassPortInfo.PassportNo);

                if (customer.PassPortInfo.IssueDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_DATE", DbType.DateTime, customer.PassPortInfo.IssueDate);

                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_PLACE", DbType.String, customer.PassPortInfo.IssuePlace);

                if (customer.PassPortInfo.EntryDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_EXPIRY_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_EXPIRY_DATE", DbType.DateTime, customer.PassPortInfo.EntryDate);

                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_PRINTED_NAME", DbType.String, customer.PassPortInfo.PrintName);
                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_COUNTRY", DbType.String, customer.PassPortInfo.IssueCountry);
                db.AddInParameter(dbCmd, "@CUST_REL_STATE", DbType.String, customer.RelationState);

                if (!string.IsNullOrEmpty(customer.XmlData))
                {
                    str1 = customer.XmlData;
                    customer.XmlData = str1.Replace("&", "&amp;");
                    db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml, customer.XmlData);

                }
                else
                    db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml, DBNull.Value);

                if (!string.IsNullOrEmpty(customer.PrefAirlineXmlData))
                    db.AddInParameter(dbCmd, "@PREF_AIRLINE_XML_DATA", DbType.Xml, customer.PrefAirlineXmlData);
                else
                    db.AddInParameter(dbCmd, "@PREF_AIRLINE_XML_DATA", DbType.Xml, DBNull.Value);

                if (!string.IsNullOrEmpty(customer.VisaXmlData))
                    db.AddInParameter(dbCmd, "@VISA_XML_DATA", DbType.Xml, customer.VisaXmlData);
                else
                    db.AddInParameter(dbCmd, "@VISA_XML_DATA", DbType.Xml, DBNull.Value);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, customer.UserId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }
        #endregion

        #region Update Customer Relation

        /// <summary>
        /// Update customer relation detail.
        /// </summary>
        /// <param name="customer">Data transfer object contains customer data.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateCustomerRelation(CustomerBDto customer)
        {
            string str1;

            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_RELATION_DETAILS_UPDATE);
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, customer.SerialNo);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, customer.CustomerId);
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, customer.RelationId);

                if (customer.TitleId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_TITLE", DbType.Int32, customer.TitleId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_TITLE", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@CUST_REL_SURNAME", DbType.String, customer.SurName);
                db.AddInParameter(dbCmd, "@CUST_REL_NAME", DbType.String, customer.Name);

                if (customer.GeneralInfo.Gender != '0')
                    db.AddInParameter(dbCmd, "@CUST_REL_GENDER", DbType.String, customer.GeneralInfo.Gender);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_GENDER", DbType.String, DBNull.Value);

                if (customer.GeneralInfo.BirthDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CUST_BIRTH_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@CUST_BIRTH_DATE", DbType.DateTime, customer.GeneralInfo.BirthDate);

                if (customer.GeneralInfo.MarriageStatusId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_MARITAL_STATUS_ID", DbType.Int32, customer.GeneralInfo.MarriageStatusId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_MARITAL_STATUS_ID", DbType.Int32, DBNull.Value);

                if (customer.GeneralInfo.MarriageDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CUST_REL_MARRIAGE_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_MARRIAGE_DATE", DbType.DateTime, customer.GeneralInfo.MarriageDate);

                if (customer.GeneralInfo.ReligionId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_RELIGION_ID", DbType.Int32, customer.GeneralInfo.ReligionId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_RELIGION_ID", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@CUST_REL_EMAIL", DbType.String, customer.ContactInfo.EmailId);
                if (!string.IsNullOrEmpty(customer.ContactInfo.MobileNo))
                    db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, customer.ContactInfo.MobileNo);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, DBNull.Value);
                db.AddInParameter(dbCmd, "@CUST_REL_PHONE", DbType.String, customer.ContactInfo.PhoneNo);

                if (customer.ProfessionId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_PROFESSION_ID", DbType.Int32, customer.ProfessionId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_PROFESSION_ID", DbType.Int32, DBNull.Value);

                if (customer.AnnualIncome != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_ANNUAL_INCOME", DbType.Decimal, customer.AnnualIncome);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_ANNUAL_INCOME", DbType.Decimal, DBNull.Value);

                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_NO", DbType.String, customer.PassPortInfo.PassportNo);

                if (customer.PassPortInfo.IssueDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_DATE", DbType.DateTime, customer.PassPortInfo.IssueDate);

                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_PLACE", DbType.String, customer.PassPortInfo.IssuePlace);

                if (customer.PassPortInfo.EntryDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_EXPIRY_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_EXPIRY_DATE", DbType.DateTime, customer.PassPortInfo.EntryDate);

                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_PRINTED_NAME", DbType.String, customer.PassPortInfo.PrintName);
                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_COUNTRY", DbType.String, customer.PassPortInfo.IssueCountry);
                db.AddInParameter(dbCmd, "@CUST_REL_STATE", DbType.String, customer.RelationState);

                if (!string.IsNullOrEmpty(customer.XmlData))
                {
                    str1 = customer.XmlData;
                    customer.XmlData = str1.Replace("&", "&amp;");

                    db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml, customer.XmlData);

                }
                else
                    db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml, DBNull.Value);

                if (!string.IsNullOrEmpty(customer.PrefAirlineXmlData))
                    db.AddInParameter(dbCmd, "@PREF_AIRLINE_XML_DATA", DbType.Xml, customer.PrefAirlineXmlData);
                else
                    db.AddInParameter(dbCmd, "@PREF_AIRLINE_XML_DATA", DbType.Xml, DBNull.Value);

                if (!string.IsNullOrEmpty(customer.VisaXmlData))
                    db.AddInParameter(dbCmd, "@VISA_XML_DATA", DbType.Xml, customer.VisaXmlData);
                else
                    db.AddInParameter(dbCmd, "@VISA_XML_DATA", DbType.Xml, DBNull.Value);


                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, customer.UserId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }

        #endregion

        #region Delete Customer Relation
        /// <summary>
        /// Delete customers detail.
        /// </summary>
        /// <param name="idCollections">Customer Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteCustomerRelation(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_RELATION_DETAILS_DELETE);
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.String, idCollections);
                db.AddOutParameter(dbCmd, "@IS_DELETE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_DELETE"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }
        #endregion

        #endregion

        #region Next Travel Plan

        #region Get Customer
        /// <summary>
        /// Gets customer list.
        /// </summary>
        /// <returns>Returns dataset contains custromer data.</returns>
        public DataSet GetCustomerNextTravelPlan(int custId, int ownerCompanyId, string searchParameter)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_NEXT_TRAVEL_PLAN_SELECT);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, custId);
                if (!String.IsNullOrEmpty(searchParameter))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, searchParameter);
                else
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, DBNull.Value);

                if (ownerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, ownerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);

                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }
        #endregion

        #region InsertCustomerNextTravelPlan
        /// <summary>
        /// Insert customer detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertCustomerNextTravelPlan(TravelBDto nextPlan)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_NEXT_TRAVEL_PLAN_INSERT);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, nextPlan.CustomerId);
                db.AddInParameter(dbCmd, "@PLAN_YEAR_MONTH", DbType.Int32, nextPlan.YearMonth);

                if (!String.IsNullOrEmpty(nextPlan.CountryName))
                    db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, nextPlan.CountryName);
                else
                    db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, DBNull.Value);

                if (nextPlan.TourTypeId != 0)
                    db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.String, nextPlan.TourTypeId);
                else
                    db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.String, DBNull.Value);

                if (nextPlan.TourId != 0)
                    db.AddInParameter(dbCmd, "@TOUR_ID", DbType.String, nextPlan.TourId);
                else
                    db.AddInParameter(dbCmd, "@TOUR_ID", DbType.String, DBNull.Value);

                if (nextPlan.NoOfPersons != 0)
                    db.AddInParameter(dbCmd, "@NO_OF_PERSONS", DbType.Int32, nextPlan.NoOfPersons);
                else
                    db.AddInParameter(dbCmd, "@NO_OF_PERSONS", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(nextPlan.Description))
                    db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, nextPlan.Description);
                else
                    db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, DBNull.Value);

                if (nextPlan.InquiryMode != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, nextPlan.InquiryMode);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, DBNull.Value);

                if (nextPlan.StatusId != 0)
                    db.AddInParameter(dbCmd, "@STATUS_ID", DbType.Int32, nextPlan.StatusId);
                else
                    db.AddInParameter(dbCmd, "@STATUS_ID", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, nextPlan.UserId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }
        #endregion

        #region Update CustomerNext Travel Plan
        /// <summary>
        /// Update customer detail.
        /// </summary>
        /// <param name="travel">Data transfer object contains customer data.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateCustomerNextTravelPlan(TravelBDto nextPlan)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_NEXT_TRAVEL_PLAN_UPDATE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, nextPlan.SerialNo);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, nextPlan.CustomerId);
                db.AddInParameter(dbCmd, "@PLAN_YEAR_MONTH", DbType.Int32, nextPlan.YearMonth);

                if (!String.IsNullOrEmpty(nextPlan.CountryName))
                    db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, nextPlan.CountryName);
                else
                    db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, DBNull.Value);

                if (nextPlan.TourTypeId != 0)
                    db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.String, nextPlan.TourTypeId);
                else
                    db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.String, DBNull.Value);

                if (nextPlan.TourId != 0)
                    db.AddInParameter(dbCmd, "@TOUR_ID", DbType.String, nextPlan.TourId);
                else
                    db.AddInParameter(dbCmd, "@TOUR_ID", DbType.String, DBNull.Value);

                if (nextPlan.NoOfPersons != 0)
                    db.AddInParameter(dbCmd, "@NO_OF_PERSONS", DbType.Int32, nextPlan.NoOfPersons);
                else
                    db.AddInParameter(dbCmd, "@NO_OF_PERSONS", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(nextPlan.Description))
                    db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, nextPlan.Description);
                else
                    db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, DBNull.Value);

                if (nextPlan.InquiryMode != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, nextPlan.InquiryMode);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, DBNull.Value);

                if (nextPlan.StatusId != 0)
                    db.AddInParameter(dbCmd, "@STATUS_ID", DbType.Int32, nextPlan.StatusId);
                else
                    db.AddInParameter(dbCmd, "@STATUS_ID", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, nextPlan.UserId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }
        #endregion

        #region Delete Customer Next Travel Plan
        /// <summary>
        /// Delete customers detail.
        /// </summary>
        /// <param name="idCollections">Customer Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteCustomerNextTravelPlan(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_NEXT_TRAVEL_PLAN_DELETE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.String, idCollections);
                db.AddOutParameter(dbCmd, "@IS_DELETE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_DELETE"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }
        #endregion

        #endregion

        #region Travel History With Us

        #region Get Customer Travel History With Us
        /// <summary>
        /// Gets Customer Travel History With Us list.
        /// </summary>
        /// <returns>Returns dataset contains custromer data.</returns>
        public DataSet GetCustomerTravelHistoryWithUs(int custId, int ownerCompanyId, string searchParameter)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_TRAVEL_HISTORY_WITH_US_SELECT);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, custId);
                if (!String.IsNullOrEmpty(searchParameter))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, searchParameter);
                else
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, DBNull.Value);

                if (ownerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, ownerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);

                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }
        #endregion

        #region InsertCustomerTravelHistoryWithUs
        /// <summary>
        /// Insert Customer Travel History With Us detail.
        /// </summary>
        /// <param name="travel">Data transfer object contains customer data.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertCustomerTravelHistoryWithUs(TravelBDto travel)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_TRAVEL_HISTORY_WITH_US_INSERT);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, travel.CustomerId);
                db.AddInParameter(dbCmd, "@YEAR_MONTH", DbType.Int32, travel.YearMonth);

                if (travel.TourTypeId != 0)
                    db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, travel.TourTypeId);
                else
                    db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, DBNull.Value);

                if (travel.TourId != 0)
                    db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, travel.TourId);
                else
                    db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(travel.CountryName))
                    db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, travel.CountryName);
                else
                    db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, DBNull.Value);
                if (travel.NoOfPersons != 0)
                    db.AddInParameter(dbCmd, "@NO_OF_PERSONS", DbType.Int32, travel.NoOfPersons);
                else
                    db.AddInParameter(dbCmd, "@NO_OF_PERSONS", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(travel.Description))
                    db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, travel.Description);
                else
                    db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, travel.UserId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }
        #endregion

        #region UpdateCustomerTravelHistoryWithUs
        /// <summary>
        /// Insert Customer Travel History With Us detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateCustomerTravelHistoryWithUs(TravelBDto travel)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_TRAVEL_HISTORY_WITH_US_UPDATE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, travel.SerialNo);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, travel.CustomerId);
                db.AddInParameter(dbCmd, "@YEAR_MONTH", DbType.Int32, travel.YearMonth);

                if (travel.TourTypeId != 0)
                    db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, travel.TourTypeId);
                else
                    db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, DBNull.Value);

                if (travel.TourId != 0)
                    db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, travel.TourId);
                else
                    db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(travel.CountryName))
                    db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, travel.CountryName);
                else
                    db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, DBNull.Value);

                if (travel.NoOfPersons != 0)
                    db.AddInParameter(dbCmd, "@NO_OF_PERSONS", DbType.Int32, travel.NoOfPersons);
                else
                    db.AddInParameter(dbCmd, "@NO_OF_PERSONS", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(travel.Description))
                    db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, travel.Description);
                else
                    db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, travel.UserId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }
        #endregion

        #region DeleteCustomerTravelHistoryWithUs
        /// <summary>
        /// Delete customers detail.
        /// </summary>
        /// <param name="idCollections">Customer Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteCustomerTravelHistoryWithUs(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_TRAVEL_HISTORY_WITH_US_DELETE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.String, idCollections);
                db.AddOutParameter(dbCmd, "@IS_DELETE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_DELETE"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }
        #endregion

        #endregion

        #region Travel History With Others

        #region Get Customer Travel History With Other
        /// <summary>
        /// Gets Customer Travel History With Us list.
        /// </summary>
        /// <returns>Returns dataset contains custromer data.</returns>
        public DataSet GetCustomerTravelHistoryWithOther(int custId, int ownerCompanyId, string searchParameter)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_TRAVEL_HISTORY_WITH_OTHER_SELECT);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, custId);
                if (!String.IsNullOrEmpty(searchParameter))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, searchParameter);
                else
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, DBNull.Value);

                if (ownerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, ownerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);

                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }
        #endregion

        #region InsertCustomerTravelHistoryWithOther
        /// <summary>
        /// Insert customer travel history with other detail.
        /// </summary>
        /// <param name="travel">Data transfer object contains customer data.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertCustomerTravelHistoryWithOther(TravelBDto travel)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_TRAVEL_HISTORY_WITH_OTHER_INSERT);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, travel.CustomerId);
                db.AddInParameter(dbCmd, "@YEAR_MONTH", DbType.Int32, travel.YearMonth);

                if (travel.AgentId != 0)
                    db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, travel.AgentId);
                else
                    db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, DBNull.Value);

                if (travel.TourTypeId != 0)
                    db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.String, travel.TourTypeId);
                else
                    db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(travel.CountryName))
                    db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, travel.CountryName);
                else
                    db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, DBNull.Value);

                if (travel.NoOfPersons != 0)
                    db.AddInParameter(dbCmd, "@NO_OF_PERSONS", DbType.Int32, travel.NoOfPersons);
                else
                    db.AddInParameter(dbCmd, "@NO_OF_PERSONS", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(travel.Description))
                    db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, travel.Description);
                else
                    db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, DBNull.Value);


                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, travel.UserId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }
        #endregion

        public int AddNewAgent(CompetitorAgentMasterBDto agent)
        {

            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_COMPETITOR_AGENT_INSERT_TEMP);
                db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.String, agent.AgentName);
                db.AddInParameter(dbCmd, "@AGENT_ADDRESS", DbType.String, agent.AgentAddress);

                db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, agent.CityId);
                db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, agent.StateId);
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, agent.CountryId);
                db.AddInParameter(dbCmd, "@PHONE_NO", DbType.String, agent.Phone);
                db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, agent.OwnerCompanyId);

                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, agent.UserId);
                db.AddOutParameter(dbCmd, "@AGENT_ID", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@AGENT_ID"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;

        }

        #region UpdateCustomerTravelHistoryWithOther
        /// <summary>
        /// Update customer travel history with other detail.
        /// </summary>
        /// <param name="travel">Data transfer object contains customer data.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateCustomerTravelHistoryWithOther(TravelBDto travel)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_TRAVEL_HISTORY_WITH_OTHER_UPDATE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, travel.SerialNo);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, travel.CustomerId);
                db.AddInParameter(dbCmd, "@YEAR_MONTH", DbType.Int32, travel.YearMonth);

                if (travel.AgentId != 0)
                    db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, travel.AgentId);
                else
                    db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, DBNull.Value);

                if (travel.TourTypeId != 0)
                    db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.String, travel.TourTypeId);
                else
                    db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(travel.CountryName))
                    db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, travel.CountryName);
                else
                    db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, DBNull.Value);

                if (travel.NoOfPersons != 0)
                    db.AddInParameter(dbCmd, "@NO_OF_PERSONS", DbType.Int32, travel.NoOfPersons);
                else
                    db.AddInParameter(dbCmd, "@NO_OF_PERSONS", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(travel.Description))
                    db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, travel.Description);
                else
                    db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, travel.UserId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }
        #endregion

        #region Delete Customer Next Travel Plan
        /// <summary>
        /// Delete customers detail.
        /// </summary>
        /// <param name="idCollections">Customer Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteCustomerTravelHistoryWithOther(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_TRAVEL_HISTORY_WITH_OTHER_DELETE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.String, idCollections);
                db.AddOutParameter(dbCmd, "@IS_DELETE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_DELETE"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }
        #endregion

        #endregion

        #region Documents

        public int InsertCustomerDocument(DocumentBDto document)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_DOCUMENTS_INSERT);

                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, document.CustomerId);
                db.AddInParameter(dbCmd, "@DOC_NAME", DbType.String, document.DocumentName);

                if (!String.IsNullOrEmpty(document.DocumentDescription))
                    db.AddInParameter(dbCmd, "@DOC_DESCRIPTION", DbType.String, document.DocumentDescription);
                else
                    db.AddInParameter(dbCmd, "@DOC_DESCRIPTION", DbType.String, DBNull.Value);

                if (document.DocumentDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@DOC_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@DOC_DATE", DbType.DateTime, document.DocumentDate);

                if (!String.IsNullOrEmpty(document.DocumentFileName))
                    db.AddInParameter(dbCmd, "@DOC_FILE_NAME", DbType.String, document.DocumentFileName);
                else
                    db.AddInParameter(dbCmd, "@DOC_FILE_NAME", DbType.String, DBNull.Value);

                if (document.DocumentFile != null && document.DocumentFile.Length != 0)
                    db.AddInParameter(dbCmd, "@DOC_ACTUAL_FILE", DbType.Binary, document.DocumentFile);
                else
                    db.AddInParameter(dbCmd, "@DOC_ACTUAL_FILE", DbType.Binary, DBNull.Value);

                if (!String.IsNullOrEmpty(document.DocumentType))
                    db.AddInParameter(dbCmd, "@DOC_CONTENT_TYPE", DbType.String, document.DocumentType);
                else
                    db.AddInParameter(dbCmd, "@DOC_CONTENT_TYPE", DbType.String, DBNull.Value);

                Result = db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }

        public int UpdateCustomerDocument(DocumentBDto document)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_DOCUMENTS_UPDATE);

                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, document.SerialNo);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, document.CustomerId);
                db.AddInParameter(dbCmd, "@DOC_NAME", DbType.String, document.DocumentName);

                if (!String.IsNullOrEmpty(document.DocumentDescription))
                    db.AddInParameter(dbCmd, "@DOC_DESCRIPTION", DbType.String, document.DocumentDescription);
                else
                    db.AddInParameter(dbCmd, "@DOC_DESCRIPTION", DbType.String, DBNull.Value);

                if (document.DocumentDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@DOC_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@DOC_DATE", DbType.DateTime, document.DocumentDate);

                if (!String.IsNullOrEmpty(document.DocumentFileName))
                    db.AddInParameter(dbCmd, "@DOC_FILE_NAME", DbType.String, document.DocumentFileName);
                else
                    db.AddInParameter(dbCmd, "@DOC_FILE_NAME", DbType.String, DBNull.Value);

                if (document.DocumentFile != null && document.DocumentFile.Length != 0)
                    db.AddInParameter(dbCmd, "@DOC_ACTUAL_FILE", DbType.Binary, document.DocumentFile);
                else
                    db.AddInParameter(dbCmd, "@DOC_ACTUAL_FILE", DbType.Binary, DBNull.Value);

                if (!String.IsNullOrEmpty(document.DocumentType))
                    db.AddInParameter(dbCmd, "@DOC_CONTENT_TYPE", DbType.String, document.DocumentType);
                else
                    db.AddInParameter(dbCmd, "@DOC_CONTENT_TYPE", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@IsDocumentChanged", DbType.Boolean, document.IsDocumentChanged);

                Result = db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }

        public DataSet GetCustomerDocument(int custId, int ownerCompanyId, string searchParameter)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_DOCUMENTS_SELECT);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, custId);
                if (!String.IsNullOrEmpty(searchParameter))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, searchParameter);
                else
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, DBNull.Value);

                if (ownerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, ownerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);



                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }

        public DataSet GetCustomerDocumentFiles(int custId, int docSerialNo, int ownerCompanyId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_CUSTOMER_DOCUMENTS_SELECT_BYTE");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, custId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, docSerialNo);

                if (ownerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, ownerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);



                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }

        #endregion

        #region Delete Customer Next Travel Plan
        /// <summary>
        /// Delete customers detail.
        /// </summary>
        /// <param name="idCollections">Customer Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteCustomerDocument(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_DOCUMENTS_DELETE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.String, idCollections);
                db.AddOutParameter(dbCmd, "@IS_DELETE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_DELETE"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }
        #endregion

        #region Customer Address

        public DataSet GetCustomerAddress(int customerId, int ownerCompanyId, int relationId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_CONTACT_DETAILS_SELECT);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, customerId);

                if (relationId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, relationId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.String, DBNull.Value);

                if (ownerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, ownerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);


                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }

        public int DeleteCustomerAddress(int serialNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_CONTACT_DETAILS_DELETE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, serialNo);
                db.AddOutParameter(dbCmd, "@IS_DELETE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_DELETE"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }

        public int SendEmailToCustomer(string emailTo, string emailCc, string emailBody, string subject)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.SP_EMAIL_CUSTOMER);
                db.AddInParameter(dbCmd, "@EMAIL_TO", DbType.String, emailTo);
                db.AddInParameter(dbCmd, "@EMAIL_CC", DbType.String, emailCc);
                db.AddInParameter(dbCmd, "@EMAIL_BODY", DbType.String, emailBody);
                db.AddInParameter(dbCmd, "@EMAIL_SUBJECT", DbType.String, subject);
                Result = db.ExecuteNonQuery(dbCmd);
                return Result;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }

        #endregion

        public DataSet GetCustomerPrefAirline(int customerId, int ownerCompanyId, int relationId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_CUSTOMER_PREF_AIRLINE_DETAILS_SELECT");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, customerId);

                if (relationId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, relationId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.String, DBNull.Value);

                if (ownerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, ownerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);


                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }

        public int DeleteCustomerPrefAirline(int serialNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_CUSTOMER_PREF_AIRLINE_DETAILS_DELETE");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, serialNo);
                db.AddOutParameter(dbCmd, "@IS_DELETE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_DELETE"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }


        public DataSet GetCustomerVisa(int customerId, int ownerCompanyId, int relationId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_CUSTOMER_VISA_DETAILS_SELECT");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, customerId);

                if (relationId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, relationId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.String, DBNull.Value);

                if (ownerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, ownerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);


                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }

        public int DeleteCustomerVisa(int serialNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_CUSTOMER_VISA_DETAILS_DELETE");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, serialNo);
                db.AddOutParameter(dbCmd, "@IS_DELETE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_DELETE"));
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }

        #region IDisposable Members
        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
		#region for online user by sunil

		public DataSet GetOnlineUsers(int USERID,int ISLOGIN,string LOGINDATE )
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_SYS_ONLINE_USER);
                db.AddInParameter(dbCmd, "@DATE", DbType.String, LOGINDATE);
				db.AddInParameter(dbCmd, "@USERID", DbType.String, USERID);
				if (ISLOGIN == 1)
				{
					ISLOGIN=0;
					db.AddInParameter(dbCmd, "@IS_LOGIN", DbType.Int32, ISLOGIN);
				}
				
               
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
		}

		#endregion
	}
}