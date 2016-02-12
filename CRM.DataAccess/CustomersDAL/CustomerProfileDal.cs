using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Data.Common;
using CRM.Model.CustomersModel;

namespace CRM.DataAccess.CustomersDAL
{
    public class CustomerProfileDal
    {
        public DataSet GetCustomerProfile(CustomerBDto customer, DateTime fromCreatedDate, DateTime toCreatedDate, DateTime fromModifiedDate, DateTime toModifiedDate)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_PROFILE_SELECT);

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

        public DataSet GetCustomerFullProfile(int customerId, int customerRelationId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_PROFILE_FULL_SELECT);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, customerId);
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, customerRelationId);
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

        public DataSet GetCustomerExport(String columns, string ownerCompanyId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_PROFILE_FOR_EXPORT);
                
                db.AddInParameter(dbCmd, "@SELECTED_COLUMN", DbType.String, columns);
                
                if (!String.IsNullOrEmpty(ownerCompanyId))
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.String, ownerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.String, DBNull.Value);

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

        public DataSet GetCustomerReport()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUSTOMER_EXPORT_SELECT_REPORT);
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
    }
}
