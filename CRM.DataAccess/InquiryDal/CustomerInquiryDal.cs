using System;
using System.Data;
using System.Data.Common;
using CRM.Model.CustomersModel;
using CRM.Model.Inquiry;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.InquiryDal
{
    public class CustomerInquiryDal
    {

        public DataSet GetCustomerInquries(CustomerBDto customer, InquiryBDto inquiry)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_INQURIES_SELECT);

                if (!String.IsNullOrEmpty(customer.SurName))
                    db.AddInParameter(dbCmd, "@SURNAME", DbType.String, customer.SurName);
                else
                    db.AddInParameter(dbCmd, "@SURNAME", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.UniqueId))
                    db.AddInParameter(dbCmd, "@UNIQUE_ID", DbType.String, customer.UniqueId);
                else
                    db.AddInParameter(dbCmd, "@UNIQUE_ID", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.Name))
                    db.AddInParameter(dbCmd, "@NAME", DbType.String, customer.Name);
                else
                    db.AddInParameter(dbCmd, "@NAME", DbType.String, DBNull.Value);

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

                if (!String.IsNullOrEmpty(customer.CompanyName))
                    db.AddInParameter(dbCmd, "@COMPANY", DbType.String, customer.CompanyName);
                else
                    db.AddInParameter(dbCmd, "@COMPANY", DbType.String, DBNull.Value);

                if (customer.ProfessionId != 0)
                    db.AddInParameter(dbCmd, "@PROFESSION_ID", DbType.Int32, customer.ProfessionId);
                else
                    db.AddInParameter(dbCmd, "@PROFESSION_ID", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(customer.PassPortInfo.PassportNo))
                    db.AddInParameter(dbCmd, "@PASSPORT_NO", DbType.String, customer.PassPortInfo.PassportNo);
                else
                    db.AddInParameter(dbCmd, "@PASSPORT_NO", DbType.String, DBNull.Value);

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

                if (inquiry.InquiryNo != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_NO", DbType.Int32, inquiry.InquiryNo);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_NO", DbType.Int32, DBNull.Value);

                if (inquiry.InquiryForId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_FOR", DbType.Int32, inquiry.InquiryForId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_FOR", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiry.InquiryDescription))
                    db.AddInParameter(dbCmd, "@INQUIRY_DESC", DbType.String, inquiry.InquiryDescription);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DESC", DbType.String, DBNull.Value);

                if (inquiry.InquiryStatusId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_STATUS", DbType.Int32, inquiry.InquiryStatusId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_STATUS", DbType.Int32, DBNull.Value);

                if (inquiry.InquiryDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_FROM", DbType.DateTime, inquiry.InquiryDate);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_FROM", DbType.DateTime, DBNull.Value);

                if (inquiry.NextFollowupDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_TO", DbType.DateTime, inquiry.NextFollowupDate);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_TO", DbType.DateTime, DBNull.Value);

                if (inquiry.SalesPersonId != 0)
                    db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, inquiry.SalesPersonId);
                else
                    db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, DBNull.Value);

                if (inquiry.OwnerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, inquiry.OwnerCompanyId);
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

        public DataTable GetCustomers(int companyId, int userId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_CUSTOMER_SELECT_BY_COMPANY_USER");
                db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, companyId);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userId);
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

        public DataTable GetCustomersRelatives(int custId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_CUSTOMER_RELATIVES_SELECT");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, custId);
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

        public int InsertCustomerInquiry(InquiryBDto inquiry)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_INQURIES_INSERT);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, inquiry.CustomerId);
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, inquiry.CustomerRelationId);
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, inquiry.CustomerRelationSerialNo);

                if (inquiry.InquiryForId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_FOR", DbType.Int32, inquiry.InquiryForId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_FOR", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiry.InquiryDescription))
                    db.AddInParameter(dbCmd, "@INQUIRY_DESC", DbType.String, inquiry.InquiryDescription);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DESC", DbType.String, DBNull.Value);

                if (inquiry.InquiryDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE", DbType.DateTime, inquiry.InquiryDate);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE", DbType.DateTime, DBNull.Value);

                if (inquiry.ReferenceId != 0)
                    db.AddInParameter(dbCmd, "@REFERENCE_ID", DbType.Int32, inquiry.ReferenceId);
                else
                    db.AddInParameter(dbCmd, "@REFERENCE_ID", DbType.Int32, DBNull.Value);

                if (inquiry.CustomerReferenceId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REFERENCE_ID", DbType.Int32, inquiry.CustomerReferenceId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REFERENCE_ID", DbType.Int32, DBNull.Value);

                if (inquiry.SalesPersonId != 0)
                    db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, inquiry.SalesPersonId);
                else
                    db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, DBNull.Value);

                if (inquiry.InquiryModeId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, inquiry.InquiryModeId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, DBNull.Value);

                if (inquiry.NextFollowupDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@NEXT_FOLLOWUP_DATE", DbType.DateTime, inquiry.NextFollowupDate);
                else
                    db.AddInParameter(dbCmd, "@NEXT_FOLLOWUP_DATE", DbType.DateTime, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiry.InternalRemarks))
                    db.AddInParameter(dbCmd, "@INTERNAL_REMARKS", DbType.String, inquiry.InternalRemarks);
                else
                    db.AddInParameter(dbCmd, "@INTERNAL_REMARKS", DbType.String, DBNull.Value);

                if (inquiry.InquiryStatusId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_STATUS", DbType.Int32, inquiry.InquiryStatusId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_STATUS", DbType.Int32, DBNull.Value);

                if (inquiry.CurrentInquiryStatusId != 0)
                    db.AddInParameter(dbCmd, "@CURRENT_INQUIRY_STATUS", DbType.Int32, inquiry.CurrentInquiryStatusId);
                else
                    db.AddInParameter(dbCmd, "@CURRENT_INQUIRY_STATUS", DbType.Int32, DBNull.Value);

                if (inquiry.OwnerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, inquiry.OwnerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, inquiry.UserId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 10);
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

        public int UpdateCustomerInquiry(InquiryBDto inquiry)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_INQURIES_UPDATE);
                db.AddInParameter(dbCmd, "@INQUIRY_NO", DbType.Int32, inquiry.InquiryNo);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, inquiry.CustomerId);
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, inquiry.CustomerRelationId);
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, inquiry.CustomerRelationSerialNo);
                db.AddInParameter(dbCmd, "@INQUIRY_FOR", DbType.Int32, inquiry.InquiryForId);

                if (!String.IsNullOrEmpty(inquiry.InquiryDescription))
                    db.AddInParameter(dbCmd, "@INQUIRY_DESC", DbType.String, inquiry.InquiryDescription);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DESC", DbType.String, DBNull.Value);

                if (inquiry.InquiryDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE", DbType.DateTime, inquiry.InquiryDate);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE", DbType.DateTime, DBNull.Value);

                if (inquiry.ReferenceId != 0)
                    db.AddInParameter(dbCmd, "@REFERENCE_ID", DbType.Int32, inquiry.ReferenceId);
                else
                    db.AddInParameter(dbCmd, "@REFERENCE_ID", DbType.Int32, DBNull.Value);

                if (inquiry.CustomerReferenceId != 0)
                    db.AddInParameter(dbCmd, "@CUST_REFERENCE_ID", DbType.Int32, inquiry.CustomerReferenceId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REFERENCE_ID", DbType.Int32, DBNull.Value);

                if (inquiry.SalesPersonId != 0)
                    db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, inquiry.SalesPersonId);
                else
                    db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, DBNull.Value);

                if (inquiry.InquiryModeId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, inquiry.InquiryModeId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, DBNull.Value);

                if (inquiry.NextFollowupDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@NEXT_FOLLOWUP_DATE", DbType.DateTime, inquiry.NextFollowupDate);
                else
                    db.AddInParameter(dbCmd, "@NEXT_FOLLOWUP_DATE", DbType.DateTime, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiry.InternalRemarks))
                    db.AddInParameter(dbCmd, "@INTERNAL_REMARKS", DbType.String, inquiry.InternalRemarks);
                else
                    db.AddInParameter(dbCmd, "@INTERNAL_REMARKS", DbType.String, DBNull.Value);

                if (inquiry.InquiryStatusId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_STATUS", DbType.Int32, inquiry.InquiryStatusId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_STATUS", DbType.Int32, DBNull.Value);

                if (inquiry.CurrentInquiryStatusId != 0)
                    db.AddInParameter(dbCmd, "@CURRENT_INQUIRY_STATUS", DbType.Int32, inquiry.CurrentInquiryStatusId);
                else
                    db.AddInParameter(dbCmd, "@CURRENT_INQUIRY_STATUS", DbType.Int32, DBNull.Value);

                if (inquiry.OwnerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, inquiry.OwnerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, inquiry.UserId);
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

        public int UpdateCustomerEmailMobile(int CustID, string MobileNo, string EmailId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_CUSTOMER_MOBILE_EMAIL_UPDATE_1");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, CustID);
                if (!string.IsNullOrEmpty(EmailId))
                    db.AddInParameter(dbCmd, "@CUST_REL_EMAIL", DbType.String, EmailId);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_EMAIL", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(MobileNo))
                    db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, MobileNo);
                else
                    db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, DBNull.Value);
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




        public int DeleteCustomerInquiry(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_INQURIES_DELETE);
                db.AddInParameter(dbCmd, "@INQUIRY_NO", DbType.String, idCollections);
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

        public int InquiryMainHeadInsert(NewInquiryBDto objNewInquiryBDto, ref int inquiryId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_MAIN_HEAD_INSERT);

                if (objNewInquiryBDto.InquiryDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE", DbType.DateTime, objNewInquiryBDto.InquiryDate);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE", DbType.DateTime, DBNull.Value);

                db.AddInParameter(dbCmd, "@CUSTOMER_ID", DbType.Int32, objNewInquiryBDto.CustomerId);

                if (objNewInquiryBDto.RelationId != 0)
                    db.AddInParameter(dbCmd, "@RELATION_ID", DbType.Int32, objNewInquiryBDto.RelationId);
                else
                    db.AddInParameter(dbCmd, "@RELATION_ID", DbType.Int32, DBNull.Value);

                if (!string.IsNullOrEmpty(objNewInquiryBDto.RelativeName))
                    db.AddInParameter(dbCmd, "@RELATIVE_NAME", DbType.String, objNewInquiryBDto.RelativeName);
                else
                    db.AddInParameter(dbCmd, "@RELATIVE_NAME", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, objNewInquiryBDto.SalesPersonId);

                if (objNewInquiryBDto.InquiryMode != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, objNewInquiryBDto.InquiryMode);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, DBNull.Value);

                if (objNewInquiryBDto.ReferenceId != 0)
                    db.AddInParameter(dbCmd, "@REFERENCE_ID", DbType.Int32, objNewInquiryBDto.ReferenceId);
                else
                    db.AddInParameter(dbCmd, "@REFERENCE_ID", DbType.Int32, DBNull.Value);

                if (!string.IsNullOrEmpty(objNewInquiryBDto.ReferenceDesc))
                    db.AddInParameter(dbCmd, "@REFERENCE_DESC", DbType.String, objNewInquiryBDto.ReferenceDesc);
                else
                    db.AddInParameter(dbCmd, "@REFERENCE_DESC", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, objNewInquiryBDto.Branch);

                if (objNewInquiryBDto.AgentId != 0)
                    db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, objNewInquiryBDto.AgentId);
                else
                    db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, DBNull.Value);

                if (objNewInquiryBDto.Rating != 0)
                    db.AddInParameter(dbCmd, "@RATING", DbType.Int32, objNewInquiryBDto.Rating);
                else
                    db.AddInParameter(dbCmd, "@RATING", DbType.Int32, DBNull.Value);

                if (objNewInquiryBDto.CurrentStatus != 0)
                    db.AddInParameter(dbCmd, "@CURRENT_STATUS", DbType.Int32, objNewInquiryBDto.CurrentStatus);
                else
                    db.AddInParameter(dbCmd, "@CURRENT_STATUS", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(objNewInquiryBDto.Remarks))
                    db.AddInParameter(dbCmd, "@REMARKS", DbType.String, objNewInquiryBDto.Remarks);
                else
                    db.AddInParameter(dbCmd, "@REMARKS", DbType.String, DBNull.Value);

                db.AddOutParameter(dbCmd, "@INQUIRY_ID", DbType.String, 8);
                Result = db.ExecuteNonQuery(dbCmd);
                Int32.TryParse(db.GetParameterValue(dbCmd, "@INQUIRY_ID").ToString(), out inquiryId);
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

        public int InquiryMainHeadUpdate(NewInquiryBDto objNewInquiryBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_MAIN_HEAD_UPDATE);
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, objNewInquiryBDto.InquiryId);
                if (objNewInquiryBDto.InquiryDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE", DbType.DateTime, objNewInquiryBDto.InquiryDate);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE", DbType.DateTime, DBNull.Value);

                db.AddInParameter(dbCmd, "@CUSTOMER_ID", DbType.Int32, objNewInquiryBDto.CustomerId);
                db.AddInParameter(dbCmd, "@RELATION_ID", DbType.Int32, objNewInquiryBDto.RelationId);
                db.AddInParameter(dbCmd, "@RELATIVE_NAME", DbType.String, objNewInquiryBDto.RelativeName);
                db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, objNewInquiryBDto.SalesPersonId);
                db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, objNewInquiryBDto.InquiryMode);
                db.AddInParameter(dbCmd, "@REFERENCE_ID", DbType.Int32, objNewInquiryBDto.ReferenceId);
                db.AddInParameter(dbCmd, "@REFERENCE_DESC", DbType.String, objNewInquiryBDto.ReferenceDesc);
                db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, objNewInquiryBDto.Branch);
                db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, objNewInquiryBDto.AgentId);
                db.AddInParameter(dbCmd, "@RATING", DbType.Int32, objNewInquiryBDto.Rating);
                db.AddInParameter(dbCmd, "@CURRENT_STATUS", DbType.Int32, objNewInquiryBDto.CurrentStatus);
                db.AddInParameter(dbCmd, "@REMARKS", DbType.String, objNewInquiryBDto.Remarks);

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

        public int InquirySubHeadInsert(InquiryDetailBDto objInquiryDetailBDto, ref int inqSerialNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQURIES_SUBHEAD_INSERT);
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, objInquiryDetailBDto.InquiryId);
                db.AddInParameter(dbCmd, "@INQUIRY_FOR", DbType.Int32, objInquiryDetailBDto.InquiryFor);

                if (!string.IsNullOrEmpty(objInquiryDetailBDto.InquiryDesc))
                    db.AddInParameter(dbCmd, "@INQUIRY_DESC", DbType.String, objInquiryDetailBDto.InquiryDesc);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DESC", DbType.String, DBNull.Value);

                if (objInquiryDetailBDto.NoOfAudults != 0)
                    db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, objInquiryDetailBDto.NoOfAudults);
                else
                    db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, DBNull.Value);

                if (objInquiryDetailBDto.NoOfChild != 0)
                    db.AddInParameter(dbCmd, "@NO_OF_CHILD", DbType.Int32, objInquiryDetailBDto.NoOfChild);
                else
                    db.AddInParameter(dbCmd, "@NO_OF_CHILD", DbType.Int32, DBNull.Value);

                if (!string.IsNullOrEmpty(objInquiryDetailBDto.NoOfINF) && objInquiryDetailBDto.NoOfINF != "0")
                    db.AddInParameter(dbCmd, "@NO_OF_INF", DbType.String, objInquiryDetailBDto.NoOfINF);
                else
                    db.AddInParameter(dbCmd, "@NO_OF_INF", DbType.String, DBNull.Value);

                if (objInquiryDetailBDto.ApproaxBudgetPerPerson != 0)
                    db.AddInParameter(dbCmd, "@APPROX_BUDGET_PER_PERSON", DbType.Decimal, objInquiryDetailBDto.ApproaxBudgetPerPerson);
                else
                    db.AddInParameter(dbCmd, "@APPROX_BUDGET_PER_PERSON", DbType.Decimal, DBNull.Value);

                if (objInquiryDetailBDto.ApproaxBudgetFamily != 0)
                    db.AddInParameter(dbCmd, "@APPROX_BUDGET_FAMILY", DbType.Decimal, objInquiryDetailBDto.ApproaxBudgetFamily);
                else
                    db.AddInParameter(dbCmd, "@APPROX_BUDGET_FAMILY", DbType.Decimal, DBNull.Value);

                if (objInquiryDetailBDto.ApproaxTravelDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@APPROX_TRAVEL_DATE", DbType.DateTime, objInquiryDetailBDto.ApproaxTravelDate);
                else
                    db.AddInParameter(dbCmd, "@APPROX_TRAVEL_DATE", DbType.DateTime, DBNull.Value);

                if (objInquiryDetailBDto.ApproaxArrivalDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@APPROX_ARRIVAL_DATE", DbType.DateTime, objInquiryDetailBDto.ApproaxArrivalDate);
                else
                    db.AddInParameter(dbCmd, "@APPROX_ARRIVAL_DATE", DbType.DateTime, DBNull.Value);

                db.AddInParameter(dbCmd, "@AMOUNT_QUOTED", DbType.Decimal, objInquiryDetailBDto.AmountQuoted);
                db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, DBNull.Value);

                if (objInquiryDetailBDto.NextFollowupDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@NEXT_FOLLOWUP_DATE", DbType.DateTime, objInquiryDetailBDto.NextFollowupDate);
                else
                    db.AddInParameter(dbCmd, "@NEXT_FOLLOWUP_DATE", DbType.DateTime, DBNull.Value);

                if (objInquiryDetailBDto.InquiryStatusId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_STATUS_ID", DbType.Int32, objInquiryDetailBDto.InquiryStatusId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_STATUS_ID", DbType.Int32, DBNull.Value);

                if (objInquiryDetailBDto.InquiryMode != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, objInquiryDetailBDto.InquiryMode);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, DBNull.Value);

                if (objInquiryDetailBDto.InqRating != 0)
                    db.AddInParameter(dbCmd, "@RATING", DbType.Int32, objInquiryDetailBDto.InqRating);
                else
                    db.AddInParameter(dbCmd, "@RATING", DbType.Int32, DBNull.Value);

                db.AddOutParameter(dbCmd, "@SR_NO", DbType.Int32, 9);
                Result = db.ExecuteNonQuery(dbCmd);
                inqSerialNo = Convert.ToInt32(db.GetParameterValue(dbCmd, "@SR_NO"));
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
        public DataTable InquiryMainDelete(int inquiryID)
        {
            Database db = null;
            DataSet ds = null;
            DataTable dt=null;
            DbCommand dbCmd = null;
            
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_MAIN_DELETE);
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryID);

              
               
                ds = db.ExecuteDataSet(dbCmd);

                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GeInquries(NewInquiryBDto objNewInquiryBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_MAIN_HEAD_SELECT);

                if (objNewInquiryBDto.InquiryId != 0)
                    db.AddInParameter(dbCmd, "@INQ_NO", DbType.Int32, objNewInquiryBDto.InquiryId);
                else
                    db.AddInParameter(dbCmd, "@INQ_NO", DbType.Int32, DBNull.Value);

                if (objNewInquiryBDto.InquiryDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE", DbType.DateTime, objNewInquiryBDto.InquiryDate);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE", DbType.DateTime, DBNull.Value);

                if (objNewInquiryBDto.InquiryToDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_TO_DATE", DbType.DateTime, objNewInquiryBDto.InquiryToDate);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_TO_DATE", DbType.DateTime, DBNull.Value);

                if (!string.IsNullOrEmpty(objNewInquiryBDto.CustomerName) && objNewInquiryBDto.CustomerName != "0")
                    db.AddInParameter(dbCmd, "@CUSTOMER_ID", DbType.Int32, Convert.ToInt32(objNewInquiryBDto.CustomerName));
                else
                    db.AddInParameter(dbCmd, "@CUSTOMER_ID", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(objNewInquiryBDto.CustomerUniqueId))
                    db.AddInParameter(dbCmd, "@CUSTOMER_UNQ_ID", DbType.String, objNewInquiryBDto.CustomerUniqueId);
                else
                    db.AddInParameter(dbCmd, "@CUSTOMER_UNQ_ID", DbType.String, DBNull.Value);

                if (objNewInquiryBDto.SalesPersonId != 0) // it is user id not emp.id
                    db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, objNewInquiryBDto.SalesPersonId);
                else
                    db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, DBNull.Value);

                if (objNewInquiryBDto.InquiryMode != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, objNewInquiryBDto.InquiryMode);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, DBNull.Value);

                if (objNewInquiryBDto.ReferenceId != 0)
                    db.AddInParameter(dbCmd, "@REFERENCE_ID", DbType.Int32, objNewInquiryBDto.ReferenceId);
                else
                    db.AddInParameter(dbCmd, "@REFERENCE_ID", DbType.Int32, DBNull.Value);

                if (objNewInquiryBDto.Branch != 0)
                    db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, objNewInquiryBDto.Branch);
                else
                    db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, DBNull.Value);

                if (objNewInquiryBDto.AgentId != 0)
                    db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, objNewInquiryBDto.AgentId);
                else
                    db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, DBNull.Value);

                if (objNewInquiryBDto.TourSubType != 0)
                    db.AddInParameter(dbCmd, "@TOUR_SUBTYPE", DbType.Int32, objNewInquiryBDto.TourSubType);
                else
                    db.AddInParameter(dbCmd, "@TOUR_SUBTYPE", DbType.Int32, DBNull.Value);




                //if (objNewInquiryBDto.Rating != 0)
                //    db.AddInParameter(dbCmd, "@RATING", DbType.Int32, objNewInquiryBDto.Rating);
                //else
                //    db.AddInParameter(dbCmd, "@RATING", DbType.Int32, DBNull.Value);

                if (objNewInquiryBDto.CurrentStatus != 0)
                    db.AddInParameter(dbCmd, "@CURRENT_STATUS", DbType.Int32, objNewInquiryBDto.CurrentStatus);
                else
                    db.AddInParameter(dbCmd, "@CURRENT_STATUS", DbType.Int32, DBNull.Value);

                if (objNewInquiryBDto.TravelFromDate1 != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@NEXT_TRAVEL_FROM_DATE", DbType.DateTime, objNewInquiryBDto.TravelFromDate1);
                else
                    db.AddInParameter(dbCmd, "@NEXT_TRAVEL_FROM_DATE", DbType.DateTime, DBNull.Value);

                if (objNewInquiryBDto.TravelTODate1 != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@NEXT_TRAVEL_TO_DATE", DbType.DateTime, objNewInquiryBDto.TravelTODate1);
                else
                    db.AddInParameter(dbCmd, "@NEXT_TRAVEL_TO_DATE", DbType.DateTime, DBNull.Value);

                if (objNewInquiryBDto.CountryId != 0)
                    db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, objNewInquiryBDto.CountryId);
                else
                    db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, DBNull.Value);

                if (objNewInquiryBDto.Rating != 0)
                    db.AddInParameter(dbCmd, "@INQ_RATING", DbType.Int32, objNewInquiryBDto.Rating);
                else
                    db.AddInParameter(dbCmd, "@INQ_RATING", DbType.Int32, DBNull.Value);

                ds = db.ExecuteDataSet(dbCmd);

                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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



        public DataTable GeInquriesDetailsById(int InquiryId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_INQ_INQUIRY_MAIN_HEAD_DETAILS_SELECT");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, InquiryId);
                ds = db.ExecuteDataSet(dbCmd);

                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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


        public DataTable GeCustomerInquriesDetailsByCustId(int custId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_CUST_CUSTOMER_INQUIRY_SELECT");
                db.AddInParameter(dbCmd, "@CUSTOMER_ID", DbType.Int32, custId);
                ds = db.ExecuteDataSet(dbCmd);

                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GeInquriesById(int InquiryId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_MAINHEAD_BY_INQID_SELECT);
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, InquiryId);
                ds = db.ExecuteDataSet(dbCmd);

                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GetTourDetailsById(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.UPS_FARE_TOUR_SELECT_BY_ID);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
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

        public DataTable GetTourTravelingCountries(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TOUR_COUNTRY_FOR_VISA_SELECT");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
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

        public DataTable GetTourTravelingCities(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TOUR_CITY_TO_TRAVEL_SELECT");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
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

        public DataTable GeInquriesSubHeadById(int InquiryId, int SrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_SUB_HEAD_SELECT);
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, InquiryId);

                if (SrNo != 0)
                    db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, SrNo);
                else
                    db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, DBNull.Value);



                ds = db.ExecuteDataSet(dbCmd);

                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataSet GetTourQuotedForRoadMap(int SrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQUIRY_TOUR_QUOTED_AMOUNT_SELECT_FOR_ROADMAP");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, SrNo);
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


        public DataSet GetTicketQuotedForRoadMap(int SrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQUIRY_TICKET_QUOTED_AMOUNT_SELECT_FOR_ROADMAP");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, SrNo);
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






        public int TransferCustomerAndInquiry(int customerId, int inquiryId, int branchId, int salsePersonId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_TRANSFER_CUSTOMER_AND_INQUIRY");
                db.AddInParameter(dbCmd, "@CUSTOMER_ID", DbType.Int32, customerId);
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
                db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, branchId);
                db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, salsePersonId);

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

        public int UpadateCustomerPassportExpiryDAte(int CustRelSrNo, DateTime ExpiryDate)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_CUSTOMER_PASSPORT_EXPIRY_DATE_UPDATE");
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, CustRelSrNo);
                db.AddInParameter(dbCmd, "@PASSPORT_EXPIRY_DATE", DbType.DateTime, ExpiryDate);

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


        public int UpadateCustomerVisaExpiryDAte(int CustRelSrNo, DateTime ExpiryDate)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_CUSTOMER_VISA_EXPIRY_DATE_UPDATE");
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, CustRelSrNo);
                db.AddInParameter(dbCmd, "@VISA_EXPIRY_DATE", DbType.DateTime, ExpiryDate);

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



        public DataSet ValidationForCustomerPassport(int inquiryId, DateTime FromDate, DateTime ToDate)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_PASSPORT_VALIDATION");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);

                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, FromDate);
                else
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                if (ToDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, ToDate);
                else
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);

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




        public DataSet ValidationForTourCustomerPassport(int inquiryId, DateTime FromDate)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_TOUR_PASSPORT_VALIDATION");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);

                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@APPROAX_TRAVEL_DATE", DbType.DateTime, FromDate);
                else
                    db.AddInParameter(dbCmd, "@APPROAX_TRAVEL_DATE", DbType.DateTime, DBNull.Value);
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


        public DataSet ValidationForCustomerVisa(int inquiryId, DateTime FromDate, DateTime ToDate, int CountryId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_VISA_VALIDATION");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);

                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, FromDate);
                else
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                if (ToDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, ToDate);
                else
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);

                db.AddInParameter(dbCmd, "@TO_PLACE", DbType.Int32, CountryId);


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


        public DataSet ValidationForTourCustomerVisa(int inquiryId, DateTime FromDate, int CountryId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_TOUR_VISA_VALIDATION");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);

                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@APPROAX_TRAVEL_DATE", DbType.DateTime, FromDate);
                else
                    db.AddInParameter(dbCmd, "@APPROAX_TRAVEL_DATE", DbType.DateTime, DBNull.Value);


                db.AddInParameter(dbCmd, "@TO_PLACE", DbType.Int32, CountryId);


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







        public int InquirySubHeadDelete(int srNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_SUB_HEAD_DELETE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, srNo);

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

        public int InquirySubHeadUpdate(InquiryDetailBDto objInquiryDetailBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_SUB_HEAD_UPDATE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, objInquiryDetailBDto.SrNo);

                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, objInquiryDetailBDto.InquiryId);
                db.AddInParameter(dbCmd, "@INQUIRY_FOR", DbType.Int32, objInquiryDetailBDto.InquiryFor);

                if (!string.IsNullOrEmpty(objInquiryDetailBDto.InquiryDesc))
                    db.AddInParameter(dbCmd, "@INQUIRY_DESC", DbType.String, objInquiryDetailBDto.InquiryDesc);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DESC", DbType.String, DBNull.Value);

                if (objInquiryDetailBDto.NoOfAudults != 0)
                    db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, objInquiryDetailBDto.NoOfAudults);
                else
                    db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, DBNull.Value);

                if (objInquiryDetailBDto.NoOfChild != 0)
                    db.AddInParameter(dbCmd, "@NO_OF_CHILD", DbType.Int32, objInquiryDetailBDto.NoOfChild);
                else
                    db.AddInParameter(dbCmd, "@NO_OF_CHILD", DbType.Int32, DBNull.Value);

                if (!string.IsNullOrEmpty(objInquiryDetailBDto.NoOfINF) && objInquiryDetailBDto.NoOfINF != "0")
                    db.AddInParameter(dbCmd, "@NO_OF_INF", DbType.String, objInquiryDetailBDto.NoOfINF);
                else
                    db.AddInParameter(dbCmd, "@NO_OF_INF", DbType.String, DBNull.Value);

                if (objInquiryDetailBDto.ApproaxBudgetPerPerson != 0)
                    db.AddInParameter(dbCmd, "@APPROX_BUDGET_PER_PERSON", DbType.Decimal, objInquiryDetailBDto.ApproaxBudgetPerPerson);
                else
                    db.AddInParameter(dbCmd, "@APPROX_BUDGET_PER_PERSON", DbType.Decimal, DBNull.Value);

                if (objInquiryDetailBDto.ApproaxBudgetFamily != 0)
                    db.AddInParameter(dbCmd, "@APPROX_BUDGET_FAMILY", DbType.Decimal, objInquiryDetailBDto.ApproaxBudgetFamily);
                else
                    db.AddInParameter(dbCmd, "@APPROX_BUDGET_FAMILY", DbType.Decimal, DBNull.Value);

                if (objInquiryDetailBDto.ApproaxTravelDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@APPROX_TRAVEL_DATE", DbType.DateTime, objInquiryDetailBDto.ApproaxTravelDate);
                else
                    db.AddInParameter(dbCmd, "@APPROX_TRAVEL_DATE", DbType.DateTime, DBNull.Value);

                if (objInquiryDetailBDto.ApproaxArrivalDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@APPROX_ARRIVAL_DATE", DbType.DateTime, objInquiryDetailBDto.ApproaxArrivalDate);
                else
                    db.AddInParameter(dbCmd, "@APPROX_ARRIVAL_DATE", DbType.DateTime, DBNull.Value);

                db.AddInParameter(dbCmd, "@AMOUNT_QUOTED", DbType.Decimal, objInquiryDetailBDto.AmountQuoted);

                db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, DBNull.Value);

                if (objInquiryDetailBDto.NextFollowupDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@NEXT_FOLLOWUP_DATE", DbType.DateTime, objInquiryDetailBDto.NextFollowupDate);
                else
                    db.AddInParameter(dbCmd, "@NEXT_FOLLOWUP_DATE", DbType.DateTime, DBNull.Value);

                if (objInquiryDetailBDto.InquiryStatusId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_STATUS_ID", DbType.Int32, objInquiryDetailBDto.InquiryStatusId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_STATUS_ID", DbType.Int32, DBNull.Value);

                if (objInquiryDetailBDto.InqRating != 0)
                    db.AddInParameter(dbCmd, "@RATING", DbType.Int32, objInquiryDetailBDto.InqRating);
                else
                    db.AddInParameter(dbCmd, "@RATING", DbType.Int32, DBNull.Value);

                if (objInquiryDetailBDto.InquiryMode != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, objInquiryDetailBDto.InquiryMode);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_MODE", DbType.Int32, DBNull.Value);


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

        public int InquirySubHeadStateUpdate(int serialNo, bool flag)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.INQ_INQUIRY_SUB_HEAD_STATE_UPDATE");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, serialNo);
                db.AddInParameter(dbCmd, "@IS_FINISHED", DbType.Boolean, flag);
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

        public int InquiryStatusDetailInsert(InquiryStatusBDto objInquiryStatusBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_STATUS_DETAIL_INSERT);
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, objInquiryStatusBDto.InquiryId);
                db.AddInParameter(dbCmd, "@INQUIRY_SR_NO", DbType.Int32, objInquiryStatusBDto.InquirySrNo);
                db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, objInquiryStatusBDto.FollowUpNo);

                if (objInquiryStatusBDto.statusId != 0)
                    db.AddInParameter(dbCmd, "@STATUS_ID", DbType.Int32, objInquiryStatusBDto.statusId);
                else
                    db.AddInParameter(dbCmd, "@STATUS_ID", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(objInquiryStatusBDto.Remarks))
                    db.AddInParameter(dbCmd, "@REMARKS", DbType.String, objInquiryStatusBDto.Remarks);
                else
                    db.AddInParameter(dbCmd, "@REMARKS", DbType.String, DBNull.Value);

                if (objInquiryStatusBDto.CompitititorId != 0)
                    db.AddInParameter(dbCmd, "@COMPITITOR_ID", DbType.Int32, objInquiryStatusBDto.CompitititorId);
                else
                    db.AddInParameter(dbCmd, "@COMPITITOR_ID", DbType.Int32, DBNull.Value);

                if (objInquiryStatusBDto.CompititorPrice != 0)
                    db.AddInParameter(dbCmd, "@COMPITITOR_PRICE", DbType.Decimal, objInquiryStatusBDto.CompititorPrice);
                else
                    db.AddInParameter(dbCmd, "@COMPITITOR_PRICE", DbType.Decimal, DBNull.Value);

                //if (objInquiryStatusBDto. != 0)
                //    db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, objInquiryStatusBDto.SalesPersonId);
                //else
                //    db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, DBNull.Value);

                if (!string.IsNullOrEmpty(objInquiryStatusBDto.OverLap))
                    db.AddInParameter(dbCmd, "@OVERLAP", DbType.String, objInquiryStatusBDto.OverLap);
                else
                    db.AddInParameter(dbCmd, "@OVERLAP", DbType.String, DBNull.Value);

                if (objInquiryStatusBDto.TravelDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@TRAVEL_DATE", DbType.DateTime, objInquiryStatusBDto.TravelDate);
                else
                    db.AddInParameter(dbCmd, "@TRAVEL_DATE", DbType.DateTime, DBNull.Value);

                if (objInquiryStatusBDto.CallBackDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@CALL_BACK_DATE", DbType.DateTime, objInquiryStatusBDto.CallBackDate);
                else
                    db.AddInParameter(dbCmd, "@CALL_BACK_DATE", DbType.DateTime, DBNull.Value);
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

        public DataSet GetFollowupForKeyValue(int inquiryId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQUIRY_FOR_FOLLOWUP_SELECT");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
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

        public DataSet GetInquiryAndCustomerEmail(int serialNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQUIRY_CUSTOMERS_DETAIL_SELECT_FOR_EMAIL");
                db.AddInParameter(dbCmd, "@INQUIRY_SRNO", DbType.Int32, serialNo);
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

        /// <summary>
        /// Get Tour Detail From Inquiry Table
        /// </summary>
        /// <param name="inquirySerialNo"></param>
        /// <returns></returns>
        public DataTable GetTourNameCodeItenaryById(int inquirySerialNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_TOURNAME_CODE_ITENARY_SELECT");
                db.AddInParameter(dbCmd, "@INQUIRY_SRNO", DbType.Int32, inquirySerialNo);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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


        public DataTable GetPassportExpiryDateById(int CustRelSrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_CUSTOMER_PASSPORT_EXPIRY_DATE_SELECT");
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, CustRelSrNo);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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


        public DataTable GetVisaExpiryDateById(int CustRelSrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_CUSTOMER_VISA_EXPIRY_DATE_SELECT");
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, CustRelSrNo);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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



        public DataTable GetDestinationCityByInqId(int InqID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQUIRY_TICKET_DESTINATION_BY_ID");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, InqID);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        /// <summary>
        /// Get Tour Detail From Follow-up Table
        /// </summary>
        /// <param name="followupNo"></param>
        /// <returns></returns>
        public DataTable GetTourNameCodeItenaryForFollowup(int followupNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_TOURNAME_CODE_ITENARY_SELECT_FOR_FOLLOWUP");
                db.AddInParameter(dbCmd, "@FOLLOWUO_NO", DbType.Int32, followupNo);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GetTicketDestinationForFollowup(int followupNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_TICKET_DESTINATION_SELECT_FOR_FOLLOWUP");
                db.AddInParameter(dbCmd, "@FOLLOWUO_NO", DbType.Int32, followupNo);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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


        public DataTable GetCustomerRelationName(int CustId, int RelID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUSTOMER_RELATION_BY_RELATIONID_SELECT);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, CustId);
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, RelID);
                ds = db.ExecuteDataSet(dbCmd);

                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GetTourNameById(int TourTypeId, int TourItenaryType, string InterDomest, int country_Id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TOURS_SELECT_BY_COUNTRY");
                db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, TourTypeId);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY_TYPE_ID", DbType.Int32, TourItenaryType);
                db.AddInParameter(dbCmd, "@TOUR_INTER_DOMEST", DbType.String, InterDomest);
                db.AddInParameter(dbCmd, "@tour_country", DbType.Int32, country_Id);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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


        public DataTable GetTourNameByRegionId(int TourTypeId, int TourItenaryType, string InterDomest, int region)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TOURS_SELECT_BY_REGION");
                db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, TourTypeId);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY_TYPE_ID", DbType.Int32, TourItenaryType);
                db.AddInParameter(dbCmd, "@TOUR_INTER_DOMEST", DbType.String, InterDomest);
                db.AddInParameter(dbCmd, "@REGION_ID", DbType.Int32, region);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GetTourNameByStateId(int TourTypeId, int TourItenaryType, string InterDomest, int state_Id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TOURS_SELECT_BY_STATE");
                db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, TourTypeId);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY_TYPE_ID", DbType.Int32, TourItenaryType);
                db.AddInParameter(dbCmd, "@TOUR_INTER_DOMEST", DbType.String, InterDomest);
                db.AddInParameter(dbCmd, "@tour_state", DbType.Int32, state_Id);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GetTourMainAndSubType()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_TOURTYPE_ITENARY_SELECT);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GetTourNameCodeItenaryById(int TourTypeId, string InterDomest)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_TOURNAME_CODE_ITENARY_BYID_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, TourTypeId);
                db.AddInParameter(dbCmd, "@TOUR_INTER_DOMEST", DbType.String, InterDomest);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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


        public DataTable GetBrowseDocumentsName(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_SELECT_BROWSE_CONTENT_KEYVALUE);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);

                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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


        #region GetCustomerCombo
        /// <summary>
        /// Gets Customer Type.
        /// </summary>
        /// <returns>Returns dataset contains Customer code.</returns>
        public DataSet GetCustomerCombo(int CustId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_CUST_CUSTOMER_MASTER_SELECT_COMBO");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, CustId);
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

        public DataTable GetEmailTemplate()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_EMAIL_TEMPLATE_SELECT);

                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataSet GetCountryByRegion(int REGION_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_INQUIRY_COUNTRY_BY_REGION");
                db.AddInParameter(dbCmd, "@REGION_ID", DbType.Int32, REGION_ID);

                ds = db.ExecuteDataSet(dbCmd);
                //using (ds)
                //{
                //    if (ds != null && ds.Tables.Count > 0)
                //    {
                //        dt = ds.Tables[0];
                //    }
                //}
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



        public DataTable GetEmailTemplateById(int TemplateId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_EMAIL_TEMPLATE_BYID_SELECT);
                db.AddInParameter(dbCmd, "@TEMPLATE_ID", DbType.Int32, TemplateId);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GetEmailTemplateForSignatureById(int EmployeeId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_EMAIL_TEMPLATE_SIGNATURE_SELECT);
                db.AddInParameter(dbCmd, "@EmployeeId", DbType.Int32, EmployeeId);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataSet GetInquiryEmail_Attachment_Select(int InquirySrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_INQUIRY_EMAIL_ATTACHMENT_SELECT");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, InquirySrNo);
                ds = db.ExecuteDataSet(dbCmd);
                //using (ds)
                //{
                //    if (ds != null && ds.Tables.Count > 0)
                //    {
                //        dt = ds.Tables[0];
                //    }
                //}
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
        public DataSet GetInquiryEmail(int InquirySrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQUIRY_EMAIL_SELECT");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, InquirySrNo);
                ds = db.ExecuteDataSet(dbCmd);
                //using (ds)
                //{
                //    if (ds != null && ds.Tables.Count > 0)
                //    {
                //        dt = ds.Tables[0];
                //    }
                //}
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
        #region by sunil for emailinquiry
        public int InsertCustomerEmail1(int userId, int CustId, int InquiryId, int followupNo, string EmailTo, string EmailCc, string EmailFrom, string Subject, byte[] Attachment1,
                                    string Attachment1ContentType, string Attachment1FileName, byte[] Attachment2, string Attachment2ContentType,
                                    string Attachment2Filename, string EmailBody)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUSTOMER_EMAIL_INSERT");
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userId);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, CustId);
                if (InquiryId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, InquiryId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, DBNull.Value);
                if (followupNo != 0)
                    db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, followupNo);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, DBNull.Value);
                db.AddInParameter(dbCmd, "@EMAIL_TO", DbType.String, EmailTo);
                if (!string.IsNullOrEmpty(EmailCc))
                    db.AddInParameter(dbCmd, "@EMAIL_CC", DbType.String, EmailCc);
                else
                    db.AddInParameter(dbCmd, "@EMAIL_CC", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(EmailFrom))
                    db.AddInParameter(dbCmd, "@EMAIL_FROM", DbType.String, EmailFrom);
                else
                    db.AddInParameter(dbCmd, "@EMAIL_FROM", DbType.String, DBNull.Value);
                db.AddInParameter(dbCmd, "@SUBJECT", DbType.String, Subject);

                db.AddInParameter(dbCmd, "@ATTACHMENT1", DbType.Binary, Attachment1);

                if (!string.IsNullOrEmpty(Attachment1ContentType))
                    db.AddInParameter(dbCmd, "@ATTACHMENT1_CONTENT_TYPE", DbType.String, Attachment1ContentType);
                else
                    db.AddInParameter(dbCmd, "@ATTACHMENT1_CONTENT_TYPE", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(Attachment1FileName))
                    db.AddInParameter(dbCmd, "@ATTACHMENT1_FILE_NAME", DbType.String, Attachment1FileName);
                else
                    db.AddInParameter(dbCmd, "@ATTACHMENT1_FILE_NAME", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@ATTACHMENT2", DbType.Binary, Attachment2);
                if (!string.IsNullOrEmpty(Attachment2ContentType))
                    db.AddInParameter(dbCmd, "@ATTACHMENT2_CONTENT_TYPE", DbType.String, Attachment2ContentType);
                else
                    db.AddInParameter(dbCmd, "@ATTACHMENT2_CONTENT_TYPE", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(Attachment2Filename))
                    db.AddInParameter(dbCmd, "@ATTACHMENT2_FILE_NAME", DbType.String, Attachment2Filename);
                else
                    db.AddInParameter(dbCmd, "@ATTACHMENT2_FILE_NAME", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@EMAIL_BODY", DbType.String, EmailBody);

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
        #endregion

        public DataTable GeInquriesForById(int InquiryId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQUIRY_FOR_SELECT_BYID");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, InquiryId);


                ds = db.ExecuteDataSet(dbCmd);

                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GetFollowUpEmail(int FollowUpNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FOLLOWUP_EMAIL_SELECT");
                db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, FollowUpNo);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GetFollowUpEmailFullDetail(int FollowUpNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FOLLOWUP_EMAIL_SELECT_FULL_DETAIL");
                db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, FollowUpNo);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GetFollowUpEmailFullDetailTicket(int FollowUpNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FOLLOWUP_EMAIL_SELECT_FULL_DETAIL_TICKET");
                db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, FollowUpNo);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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




        public DataTable GetCustEmailByCustomerId(int CustID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_CUSTOMER_EMAIL_BY_CUSTID_SELECT");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, CustID);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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




        public int FollowUpStateUpdate(int serialNo, bool flag)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.INQ_INQUIRY_FOLLOWUPS_STATE_UPDATE");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, serialNo);
                db.AddInParameter(dbCmd, "@IS_FINISHED", DbType.Boolean, flag);
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

        public DataTable SelectDummyTour()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_FARE_TOUR_MASTER_DUMMY_TOUR_SELECT");
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

        public int SaveInquiryRoadMap(int inquiryId, int inquirySerialNo, int followUpNo, string action, string description)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.INQ_INQUIRY_ROAD_MAP_SAVE");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
                db.AddInParameter(dbCmd, "@INQUIRY_SR_NO", DbType.Int32, inquirySerialNo);
                db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, followUpNo);
                db.AddInParameter(dbCmd, "@ACTION", DbType.String, action);
                db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, description);
                Result = db.ExecuteNonQuery(dbCmd);
                return Result;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return Result;
        }

        public DataSet GetInquiryRoadMap(int inquiryId, int CountryId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.INQ_INQUIRY_ROAD_MAP_SELECT");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, CountryId);
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


        public DataSet GetInquiryRoadMapForTicket(int inquiryId, int DestinationCityId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.INQ_INQUIRY_ROAD_MAP_TICKET_SELECT");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
                db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, DestinationCityId);
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


        public DataSet GetAirlineByDestinationCity(int inquiryId, int SrNo, int DestinationCityId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQUIRY_TICKET_AIRLINE_BY_INQID");
                if (inquiryId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, DBNull.Value);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, SrNo);
                db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, DestinationCityId);
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


        public DataSet GetGdsAirlineDetailsById(int inquiryId, int SrNo, int DestinationCityId, int AirlineID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQUIRY_TICKET_AIRLINE_DETAILS_BY_INQID");
                if (inquiryId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, DBNull.Value);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, SrNo);
                db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, DestinationCityId);
                db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, AirlineID);
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



         



        public DataTable GetAirportNameCodeByAirLine(int AirlineId, int DestinationId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_AIRPORT_CODE_NAME_BY_AIRLINE");
                db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, AirlineId);
                db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, DestinationId);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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






        #region Ticket Inquiry

        public DataTable GetTicketBooking(int InquiryId, int ProductId, int TicketTabsrNo, int SrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_FOR_TICKET_BOOKING_SELECT);

                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, InquiryId);
                db.AddInParameter(dbCmd, "@PRODUCTID", DbType.Int32, ProductId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, SrNo);



                if (TicketTabsrNo != 0)
                    db.AddInParameter(dbCmd, "@TICKET_TAB_SR_NO", DbType.Int32, TicketTabsrNo);
                else
                    db.AddInParameter(dbCmd, "@TICKET_TAB_SR_NO", DbType.Int32, DBNull.Value);
                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public int InquiryForTicketBookingInsert(InquiryForTicketBDto ObjInquiryForTicketBDto, ref int TicketTabSerialNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_FOR_TICKET_BOOKING_INSERT);
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, ObjInquiryForTicketBDto.InquiryId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, ObjInquiryForTicketBDto.SerialNo);

                db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, ObjInquiryForTicketBDto.ProductId);
                db.AddInParameter(dbCmd, "@TICKET_TYPE", DbType.Int32, ObjInquiryForTicketBDto.TicketType);
                db.AddInParameter(dbCmd, "@TRAVEL_MODE", DbType.Int32, ObjInquiryForTicketBDto.TravelMode);

                if (ObjInquiryForTicketBDto.FromDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, ObjInquiryForTicketBDto.FromDate);
                else
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);

                if (ObjInquiryForTicketBDto.ToDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, ObjInquiryForTicketBDto.ToDate);
                else
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);

                db.AddInParameter(dbCmd, "@FROM_PLACE", DbType.Int32, ObjInquiryForTicketBDto.FromPlace);
                db.AddInParameter(dbCmd, "@TO_PLACE", DbType.Int32, ObjInquiryForTicketBDto.ToPlace);
                db.AddInParameter(dbCmd, "@PREFERRED_AIRLINE", DbType.Int32, ObjInquiryForTicketBDto.PreferredAirline);
                db.AddInParameter(dbCmd, "@VIA", DbType.Int32, ObjInquiryForTicketBDto.Via);
                db.AddInParameter(dbCmd, "@AIRLINE_CLASS_ID", DbType.Int32, ObjInquiryForTicketBDto.AirlineClassId);
                db.AddInParameter(dbCmd, "@FREQUENT_FLYER_NO", DbType.String, ObjInquiryForTicketBDto.FrequentFlyerNo);
                db.AddInParameter(dbCmd, "@FREQUENT_FLYER_AIRLINE_ID", DbType.Int32, ObjInquiryForTicketBDto.FrequentFlyerAirlineId);
                db.AddInParameter(dbCmd, "@DESTINATION_CONTACT_NO", DbType.String, ObjInquiryForTicketBDto.DestinationContactNo);
                db.AddInParameter(dbCmd, "@ONEWAY_OR_RETURN", DbType.String, ObjInquiryForTicketBDto.OneWayOrReturn);
                db.AddInParameter(dbCmd, "@STOP_ALLOWED", DbType.Boolean, ObjInquiryForTicketBDto.StopAllowed);
                db.AddInParameter(dbCmd, "@HAVE_VALID_PASSPORT", DbType.Boolean, ObjInquiryForTicketBDto.HaveValidPassport);
                db.AddInParameter(dbCmd, "@HAVE_VALID_VISA", DbType.Boolean, ObjInquiryForTicketBDto.HaveValidVisa);
                db.AddInParameter(dbCmd, "@MEAL_TYPE", DbType.String, ObjInquiryForTicketBDto.MealType);
                db.AddInParameter(dbCmd, "@SPECIAL_REQUEST", DbType.String, ObjInquiryForTicketBDto.SpecialRequest);
                db.AddInParameter(dbCmd, "@REMARKS", DbType.String, ObjInquiryForTicketBDto.Remarks);
                db.AddOutParameter(dbCmd, "@TICKET_TAB_ID", DbType.Int32, 8);

                Result = db.ExecuteNonQuery(dbCmd);
                int.TryParse(db.GetParameterValue(dbCmd, "@TICKET_TAB_ID").ToString(), out TicketTabSerialNo);
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

        public int InquiryForTicketBookingUpdate(InquiryForTicketBDto ObjInquiryForTicketBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_FOR_TICKET_BOOKING_UPDATE);
                db.AddInParameter(dbCmd, "@TICKET_TAB_SR_NO", DbType.Int32, ObjInquiryForTicketBDto.TicketTabSrNo);
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, ObjInquiryForTicketBDto.InquiryId);
                db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, ObjInquiryForTicketBDto.ProductId);
                db.AddInParameter(dbCmd, "@TICKET_TYPE", DbType.Int32, ObjInquiryForTicketBDto.TicketType);
                db.AddInParameter(dbCmd, "@TRAVEL_MODE", DbType.Int32, ObjInquiryForTicketBDto.TravelMode);
                if (ObjInquiryForTicketBDto.FromDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, ObjInquiryForTicketBDto.FromDate);
                else
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);

                if (ObjInquiryForTicketBDto.ToDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, ObjInquiryForTicketBDto.ToDate);
                else
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);

                db.AddInParameter(dbCmd, "@FROM_PLACE", DbType.Int32, ObjInquiryForTicketBDto.FromPlace);
                db.AddInParameter(dbCmd, "@TO_PLACE", DbType.Int32, ObjInquiryForTicketBDto.ToPlace);
                db.AddInParameter(dbCmd, "@PREFERRED_AIRLINE", DbType.Int32, ObjInquiryForTicketBDto.PreferredAirline);
                db.AddInParameter(dbCmd, "@VIA", DbType.Int32, ObjInquiryForTicketBDto.Via);
                db.AddInParameter(dbCmd, "@AIRLINE_CLASS_ID", DbType.Int32, ObjInquiryForTicketBDto.AirlineClassId);
                db.AddInParameter(dbCmd, "@FREQUENT_FLYER_NO", DbType.String, ObjInquiryForTicketBDto.FrequentFlyerNo);
                db.AddInParameter(dbCmd, "@FREQUENT_FLYER_AIRLINE_ID", DbType.Int32, ObjInquiryForTicketBDto.FrequentFlyerAirlineId);
                db.AddInParameter(dbCmd, "@DESTINATION_CONTACT_NO", DbType.String, ObjInquiryForTicketBDto.DestinationContactNo);
                db.AddInParameter(dbCmd, "@ONEWAY_OR_RETURN", DbType.String, ObjInquiryForTicketBDto.OneWayOrReturn);
                db.AddInParameter(dbCmd, "@STOP_ALLOWED", DbType.Boolean, ObjInquiryForTicketBDto.StopAllowed);
                db.AddInParameter(dbCmd, "@HAVE_VALID_PASSPORT", DbType.Boolean, ObjInquiryForTicketBDto.HaveValidPassport);
                db.AddInParameter(dbCmd, "@HAVE_VALID_VISA", DbType.Boolean, ObjInquiryForTicketBDto.HaveValidVisa);
                db.AddInParameter(dbCmd, "@MEAL_TYPE", DbType.String, ObjInquiryForTicketBDto.MealType);
                db.AddInParameter(dbCmd, "@SPECIAL_REQUEST", DbType.String, ObjInquiryForTicketBDto.SpecialRequest);
                db.AddInParameter(dbCmd, "@REMARKS", DbType.String, ObjInquiryForTicketBDto.Remarks);
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



        public int InquiryTicketBookingDelete(int ticketTabID, int inquiryId, int srNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_INQ_INQUIRY_FOR_TICKET_BOOKING_DELETE");
                db.AddInParameter(dbCmd, "@TICKET_TAB_ID", DbType.Int32, ticketTabID);
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, srNo);

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


        public int UpdatePassportInquiryTicketBooking(int inquiryId, int TicketTabId, int srNo, bool haveValidPass)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_INQ_INQUIRY_FOR_TICKET_PASSPORT_UPDATE");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
                db.AddInParameter(dbCmd, "@TICKET_TAB_ID", DbType.Int32, TicketTabId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, srNo);
                db.AddInParameter(dbCmd, "@HAVE_VALID_PASSPORT", DbType.Boolean, haveValidPass);

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

        public int UpdateVisaInquiryTicketBooking(int inquiryId, int TicketTabId, int srNo, bool haveValidPass)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_INQ_INQUIRY_FOR_TICKET_VISA_UPDATE");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
                db.AddInParameter(dbCmd, "@TICKET_TAB_ID", DbType.Int32, TicketTabId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, srNo);
                db.AddInParameter(dbCmd, "@HAVE_VALID_VISA", DbType.Boolean, haveValidPass);

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



        public int UpdatePassportInquiryTourBooking(int inquiryId, int TicketTabId, int srNo, bool haveValidPass)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_INQ_INQUIRY_FOR_TOUR_PASSPORT_UPDATE");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
                db.AddInParameter(dbCmd, "@TICKET_TAB_ID", DbType.Int32, TicketTabId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, srNo);
                db.AddInParameter(dbCmd, "@HAVE_VALID_PASSPORT", DbType.Boolean, haveValidPass);

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

        public int UpdateVisaInquiryTourBooking(int inquiryId, int TicketTabId, int srNo, bool haveValidPass)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_INQ_INQUIRY_FOR_TOUR_VISA_UPDATE");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
                db.AddInParameter(dbCmd, "@TICKET_TAB_ID", DbType.Int32, TicketTabId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, srNo);
                db.AddInParameter(dbCmd, "@HAVE_VALID_VISA", DbType.Boolean, haveValidPass);

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

        #region Tour Inquiry

        public int InquiryForTourBookingInsert(InquiryForTourBDto ObjInquiryGRDMappingBDto, ref int tourInquirySerialNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQURIES_TOUR_INSERT);

                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, ObjInquiryGRDMappingBDto.InquiryId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, ObjInquiryGRDMappingBDto.SerialNo);
                db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, ObjInquiryGRDMappingBDto.ProductId);
                db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, ObjInquiryGRDMappingBDto.TourTypeId);

                if (!string.IsNullOrEmpty(ObjInquiryGRDMappingBDto.StandardOrCustomized.ToString()))
                    db.AddInParameter(dbCmd, "@STANDARD_OR_CUSTOMIZED_TOUR", DbType.String, ObjInquiryGRDMappingBDto.StandardOrCustomized);
                else
                    db.AddInParameter(dbCmd, "@STANDARD_OR_CUSTOMIZED_TOUR", DbType.String, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.FromDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, ObjInquiryGRDMappingBDto.FromDate);
                else
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.ToDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, ObjInquiryGRDMappingBDto.ToDate);
                else
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.FromPlace != 0)
                    db.AddInParameter(dbCmd, "@FROM_PLACE", DbType.Int32, ObjInquiryGRDMappingBDto.FromPlace);
                else
                    db.AddInParameter(dbCmd, "@FROM_PLACE", DbType.Int32, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.ToPlace != 0)
                    db.AddInParameter(dbCmd, "@TO_PLACE", DbType.Int32, ObjInquiryGRDMappingBDto.ToPlace);
                else
                    db.AddInParameter(dbCmd, "@TO_PLACE", DbType.Int32, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.TourId != 0)
                    db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, ObjInquiryGRDMappingBDto.TourId);
                else
                    db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.Country != 0)
                    db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, ObjInquiryGRDMappingBDto.Country);
                else
                    db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.Region != 0)
                    db.AddInParameter(dbCmd, "@REGION_ID", DbType.Int32, ObjInquiryGRDMappingBDto.Region);
                else
                    db.AddInParameter(dbCmd, "@REGION_ID", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@HAVE_VALID_PASSPORT", DbType.Boolean, ObjInquiryGRDMappingBDto.HaveValidPassport);
                db.AddInParameter(dbCmd, "@HAVE_VALID_VISA", DbType.Boolean, ObjInquiryGRDMappingBDto.HaveValidVisa);

                if (!string.IsNullOrEmpty(ObjInquiryGRDMappingBDto.Remarks))
                    db.AddInParameter(dbCmd, "@REMARKS", DbType.String, ObjInquiryGRDMappingBDto.Remarks);
                else
                    db.AddInParameter(dbCmd, "@REMARKS", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(ObjInquiryGRDMappingBDto.Attachment1ContentType))
                {
                    db.AddInParameter(dbCmd, "@ATTACHMENT1", DbType.Binary, ObjInquiryGRDMappingBDto.Attachment1);
                    db.AddInParameter(dbCmd, "@ATTACHMENT1_CONTENT_TYPE", DbType.String, ObjInquiryGRDMappingBDto.Attachment1ContentType);
                    db.AddInParameter(dbCmd, "@ATTACHMENT1_FILE_NAME", DbType.String, ObjInquiryGRDMappingBDto.Attachment1FileName);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ATTACHMENT1", DbType.Binary, DBNull.Value);
                    db.AddInParameter(dbCmd, "@ATTACHMENT1_CONTENT_TYPE", DbType.String, DBNull.Value);
                    db.AddInParameter(dbCmd, "@ATTACHMENT1_FILE_NAME", DbType.String, DBNull.Value);
                }

                if (!string.IsNullOrEmpty(ObjInquiryGRDMappingBDto.Attachment2ContentType))
                {
                    db.AddInParameter(dbCmd, "@ATTACHMENT2", DbType.Binary, ObjInquiryGRDMappingBDto.Attachment2);
                    db.AddInParameter(dbCmd, "@ATTACHMENT2_CONTENT_TYPE", DbType.String, ObjInquiryGRDMappingBDto.Attachment2ContentType);
                    db.AddInParameter(dbCmd, "@ATTACHMENT2_FILE_NAME", DbType.String, ObjInquiryGRDMappingBDto.Attachment2FileName);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ATTACHMENT2", DbType.Binary, DBNull.Value);
                    db.AddInParameter(dbCmd, "@ATTACHMENT2_CONTENT_TYPE", DbType.String, DBNull.Value);
                    db.AddInParameter(dbCmd, "@ATTACHMENT2_FILE_NAME", DbType.String, DBNull.Value);
                }

                if (ObjInquiryGRDMappingBDto.MarginAmount != 0)
                    db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, ObjInquiryGRDMappingBDto.MarginAmount);
                else
                    db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, DBNull.Value);

                db.AddOutParameter(dbCmd, "@TOUR_INQ_SR_NO", DbType.Int32, 8);

                Result = db.ExecuteNonQuery(dbCmd);
                int.TryParse(db.GetParameterValue(dbCmd, "@TOUR_INQ_SR_NO").ToString(), out tourInquirySerialNo);
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

        public int InquiryForTourBookingUpdate(InquiryForTourBDto ObjInquiryGRDMappingBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQURIES_TOUR_UPDATE);

                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, ObjInquiryGRDMappingBDto.InquiryId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, ObjInquiryGRDMappingBDto.SerialNo);
                db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, ObjInquiryGRDMappingBDto.ProductId);
                db.AddInParameter(dbCmd, "@TOUR_INQ_SR_NO", DbType.Int32, ObjInquiryGRDMappingBDto.TourInquirySerialNo);
                db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, ObjInquiryGRDMappingBDto.TourTypeId);

                if (!string.IsNullOrEmpty(ObjInquiryGRDMappingBDto.StandardOrCustomized.ToString()))
                    db.AddInParameter(dbCmd, "@STANDARD_OR_CUSTOMIZED_TOUR", DbType.String, ObjInquiryGRDMappingBDto.StandardOrCustomized);
                else
                    db.AddInParameter(dbCmd, "@STANDARD_OR_CUSTOMIZED_TOUR", DbType.String, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.FromDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, ObjInquiryGRDMappingBDto.FromDate);
                else
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.ToDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, ObjInquiryGRDMappingBDto.ToDate);
                else
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.FromPlace != 0)
                    db.AddInParameter(dbCmd, "@FROM_PLACE", DbType.Int32, ObjInquiryGRDMappingBDto.FromPlace);
                else
                    db.AddInParameter(dbCmd, "@FROM_PLACE", DbType.Int32, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.ToPlace != 0)
                    db.AddInParameter(dbCmd, "@TO_PLACE", DbType.Int32, ObjInquiryGRDMappingBDto.ToPlace);
                else
                    db.AddInParameter(dbCmd, "@TO_PLACE", DbType.Int32, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.TourId != 0)
                    db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, ObjInquiryGRDMappingBDto.TourId);
                else
                    db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.Country != 0)
                    db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, ObjInquiryGRDMappingBDto.Country);
                else
                    db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.Region != 0)
                    db.AddInParameter(dbCmd, "@REGION_ID", DbType.Int32, ObjInquiryGRDMappingBDto.Region);
                else
                    db.AddInParameter(dbCmd, "@REGION_ID", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@HAVE_VALID_PASSPORT", DbType.Boolean, ObjInquiryGRDMappingBDto.HaveValidPassport);
                db.AddInParameter(dbCmd, "@HAVE_VALID_VISA", DbType.Boolean, ObjInquiryGRDMappingBDto.HaveValidVisa);

                if (!string.IsNullOrEmpty(ObjInquiryGRDMappingBDto.Remarks))
                    db.AddInParameter(dbCmd, "@REMARKS", DbType.String, ObjInquiryGRDMappingBDto.Remarks);
                else
                    db.AddInParameter(dbCmd, "@REMARKS", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(ObjInquiryGRDMappingBDto.Attachment1ContentType))
                {
                    db.AddInParameter(dbCmd, "@ATTACHMENT1", DbType.Binary, ObjInquiryGRDMappingBDto.Attachment1);
                    db.AddInParameter(dbCmd, "@ATTACHMENT1_CONTENT_TYPE", DbType.String, ObjInquiryGRDMappingBDto.Attachment1ContentType);
                    db.AddInParameter(dbCmd, "@ATTACHMENT1_FILE_NAME", DbType.String, ObjInquiryGRDMappingBDto.Attachment1FileName);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ATTACHMENT1", DbType.Binary, DBNull.Value);
                    db.AddInParameter(dbCmd, "@ATTACHMENT1_CONTENT_TYPE", DbType.String, DBNull.Value);
                    db.AddInParameter(dbCmd, "@ATTACHMENT1_FILE_NAME", DbType.String, DBNull.Value);
                }

                if (!string.IsNullOrEmpty(ObjInquiryGRDMappingBDto.Attachment2ContentType))
                {
                    db.AddInParameter(dbCmd, "@ATTACHMENT2", DbType.Binary, ObjInquiryGRDMappingBDto.Attachment2);
                    db.AddInParameter(dbCmd, "@ATTACHMENT2_CONTENT_TYPE", DbType.String, ObjInquiryGRDMappingBDto.Attachment2ContentType);
                    db.AddInParameter(dbCmd, "@ATTACHMENT2_FILE_NAME", DbType.String, ObjInquiryGRDMappingBDto.Attachment2FileName);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ATTACHMENT2", DbType.Binary, DBNull.Value);
                    db.AddInParameter(dbCmd, "@ATTACHMENT2_CONTENT_TYPE", DbType.String, DBNull.Value);
                    db.AddInParameter(dbCmd, "@ATTACHMENT2_FILE_NAME", DbType.String, DBNull.Value);
                }

                if (ObjInquiryGRDMappingBDto.MarginAmount != 0)
                    db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, ObjInquiryGRDMappingBDto.MarginAmount);
                else
                    db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, DBNull.Value);

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

        public int SaveTourInquiryQuotedAmount(InquiryForTourBDto ObjInquiryGRDMappingBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQUIRY_FOR_TOUR_QUOTED_AMOUNT_SAVE");

                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, ObjInquiryGRDMappingBDto.InquiryId);
                db.AddInParameter(dbCmd, "@INQUIRY_SR_NO", DbType.Int32, ObjInquiryGRDMappingBDto.SerialNo);
                db.AddInParameter(dbCmd, "@TOUR_INQ_SR_NO", DbType.Int32, ObjInquiryGRDMappingBDto.TourInquirySerialNo);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, ObjInquiryGRDMappingBDto.TourId);
                db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, ObjInquiryGRDMappingBDto.CurrencyId);
                db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, ObjInquiryGRDMappingBDto.ProductId);

                if (ObjInquiryGRDMappingBDto.QuotedAmtAdult != 0)
                    db.AddInParameter(dbCmd, "@QUOTED_ADULT", DbType.Decimal, ObjInquiryGRDMappingBDto.QuotedAmtAdult);
                else
                    db.AddInParameter(dbCmd, "@QUOTED_ADULT", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.TaxAdult != 0)
                    db.AddInParameter(dbCmd, "@TAX_ADULT", DbType.Decimal, ObjInquiryGRDMappingBDto.TaxAdult);
                else
                    db.AddInParameter(dbCmd, "@TAX_ADULT", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.GstAdult != 0)
                    db.AddInParameter(dbCmd, "@GST_ADULT", DbType.Decimal, ObjInquiryGRDMappingBDto.GstAdult);
                else
                    db.AddInParameter(dbCmd, "@GST_ADULT", DbType.Decimal, DBNull.Value);


                if (ObjInquiryGRDMappingBDto.QuotedAmtChildWithBed != 0)
                    db.AddInParameter(dbCmd, "@QUOTED_CHILD_WITH_BED", DbType.Decimal, ObjInquiryGRDMappingBDto.QuotedAmtChildWithBed);
                else
                    db.AddInParameter(dbCmd, "@QUOTED_CHILD_WITH_BED", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.TaxChildWitBed != 0)
                    db.AddInParameter(dbCmd, "@TAX_CHILD_WITH_BED", DbType.Decimal, ObjInquiryGRDMappingBDto.TaxChildWitBed);
                else
                    db.AddInParameter(dbCmd, "@TAX_CHILD_WITH_BED", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.GstChildWitBed != 0)
                    db.AddInParameter(dbCmd, "@GST_CHILD_WITH_BED", DbType.Decimal, ObjInquiryGRDMappingBDto.GstChildWitBed);
                else
                    db.AddInParameter(dbCmd, "@GST_CHILD_WITH_BED", DbType.Decimal, DBNull.Value);


                if (ObjInquiryGRDMappingBDto.QuotedAmtChildWithoutBed != 0)
                    db.AddInParameter(dbCmd, "@QUOTED_CHILD_WITHOUT_BED", DbType.Decimal, ObjInquiryGRDMappingBDto.QuotedAmtChildWithoutBed);
                else
                    db.AddInParameter(dbCmd, "@QUOTED_CHILD_WITHOUT_BED", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.TaxChildWitoutBed != 0)
                    db.AddInParameter(dbCmd, "@TAX_CHILD_WITHOUT_BED", DbType.Decimal, ObjInquiryGRDMappingBDto.TaxChildWitoutBed);
                else
                    db.AddInParameter(dbCmd, "@TAX_CHILD_WITHOUT_BED", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.GstChildWitoutBed != 0)
                    db.AddInParameter(dbCmd, "@GST_CHILD_WITHOUT_BED", DbType.Decimal, ObjInquiryGRDMappingBDto.GstChildWitoutBed);
                else
                    db.AddInParameter(dbCmd, "@GST_CHILD_WITHOUT_BED", DbType.Decimal, DBNull.Value);


                if (ObjInquiryGRDMappingBDto.QuotedAmtInfant != 0)
                    db.AddInParameter(dbCmd, "@QUOTED_INFANT", DbType.Decimal, ObjInquiryGRDMappingBDto.QuotedAmtInfant);
                else
                    db.AddInParameter(dbCmd, "@QUOTED_INFANT", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.TaxInfant != 0)
                    db.AddInParameter(dbCmd, "@TAX_INFANT", DbType.Decimal, ObjInquiryGRDMappingBDto.TaxInfant);
                else
                    db.AddInParameter(dbCmd, "@TAX_INFANT", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.GstInfant != 0)
                    db.AddInParameter(dbCmd, "@GST_INFANT", DbType.Decimal, ObjInquiryGRDMappingBDto.GstInfant);
                else
                    db.AddInParameter(dbCmd, "@GST_INFANT", DbType.Decimal, DBNull.Value);

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


        public int SaveTicketInquiryQuotedAmount(InquiryGRDMappingBDto ObjInquiryGRDMappingBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQUIRY_FOR_TICKET_QUOTED_AMOUNT_SAVE");

                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, ObjInquiryGRDMappingBDto.InquiryId);
                db.AddInParameter(dbCmd, "@INQUIRY_SR_NO", DbType.Int32, ObjInquiryGRDMappingBDto.SerialNo);
                db.AddInParameter(dbCmd, "@TICKET_TAB_ID", DbType.Int32, ObjInquiryGRDMappingBDto.TicketTabId);
                db.AddInParameter(dbCmd, "@TICKET_INQ_SR_NO", DbType.Int32, ObjInquiryGRDMappingBDto.TicketInqSrNo);

                db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, ObjInquiryGRDMappingBDto.AirlinesId);

                db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, ObjInquiryGRDMappingBDto.CurrencyId);
                db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, ObjInquiryGRDMappingBDto.ProductId);

                if (ObjInquiryGRDMappingBDto.QuotedAmtAdult != 0)
                    db.AddInParameter(dbCmd, "@QUOUTED_ADULT", DbType.Decimal, ObjInquiryGRDMappingBDto.QuotedAmtAdult);
                else
                    db.AddInParameter(dbCmd, "@QUOUTED_ADULT", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.TaxAdult != 0)
                    db.AddInParameter(dbCmd, "@TAX_ADULT", DbType.Decimal, ObjInquiryGRDMappingBDto.TaxAdult);
                else
                    db.AddInParameter(dbCmd, "@TAX_ADULT", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.GstAdult != 0)
                    db.AddInParameter(dbCmd, "@GST_ADULT", DbType.Decimal, ObjInquiryGRDMappingBDto.GstAdult);
                else
                    db.AddInParameter(dbCmd, "@GST_ADULT", DbType.Decimal, DBNull.Value);


                if (ObjInquiryGRDMappingBDto.QuotedAmtChildWithBed != 0)
                    db.AddInParameter(dbCmd, "@QUOTED_CHILD", DbType.Decimal, ObjInquiryGRDMappingBDto.QuotedAmtChildWithBed);
                else
                    db.AddInParameter(dbCmd, "@QUOTED_CHILD", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.TaxChildWitBed != 0)
                    db.AddInParameter(dbCmd, "@TAX_CHILD", DbType.Decimal, ObjInquiryGRDMappingBDto.TaxChildWitBed);
                else
                    db.AddInParameter(dbCmd, "@TAX_CHILD", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.GstChildWitBed != 0)
                    db.AddInParameter(dbCmd, "@GST_CHILD", DbType.Decimal, ObjInquiryGRDMappingBDto.GstChildWitBed);
                else
                    db.AddInParameter(dbCmd, "@GST_CHILD", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.QuotedAmtInfant != 0)
                    db.AddInParameter(dbCmd, "@QUOTED_INFANT", DbType.Decimal, ObjInquiryGRDMappingBDto.QuotedAmtInfant);
                else
                    db.AddInParameter(dbCmd, "@QUOTED_INFANT", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.TaxInfant != 0)
                    db.AddInParameter(dbCmd, "@TAX_INFANT", DbType.Decimal, ObjInquiryGRDMappingBDto.TaxInfant);
                else
                    db.AddInParameter(dbCmd, "@TAX_INFANT", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.GstInfant != 0)
                    db.AddInParameter(dbCmd, "@GST_INFANT", DbType.Decimal, ObjInquiryGRDMappingBDto.GstInfant);
                else
                    db.AddInParameter(dbCmd, "@GST_INFANT", DbType.Decimal, DBNull.Value);

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



        public int UpdateTicketInquiryQuotedAmount(InquiryGRDMappingBDto ObjInquiryGRDMappingBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQUIRY_FOR_TICKET_QUOTED_AMOUNT_UPDATE");

                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, ObjInquiryGRDMappingBDto.InquiryId);
                db.AddInParameter(dbCmd, "@INQUIRY_SR_NO", DbType.Int32, ObjInquiryGRDMappingBDto.SerialNo);
                db.AddInParameter(dbCmd, "@TICKET_TAB_ID", DbType.Int32, ObjInquiryGRDMappingBDto.TicketTabId);
                db.AddInParameter(dbCmd, "@TICKET_INQ_SR_NO", DbType.Int32, ObjInquiryGRDMappingBDto.TicketInqSrNo);
                db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, ObjInquiryGRDMappingBDto.AirlinesId);

                db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, ObjInquiryGRDMappingBDto.CurrencyId);
                db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, ObjInquiryGRDMappingBDto.ProductId);

                if (ObjInquiryGRDMappingBDto.QuotedAmtAdult != 0)
                    db.AddInParameter(dbCmd, "@QUOUTED_ADULT", DbType.Decimal, ObjInquiryGRDMappingBDto.QuotedAmtAdult);
                else
                    db.AddInParameter(dbCmd, "@QUOUTED_ADULT", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.TaxAdult != 0)
                    db.AddInParameter(dbCmd, "@TAX_ADULT", DbType.Decimal, ObjInquiryGRDMappingBDto.TaxAdult);
                else
                    db.AddInParameter(dbCmd, "@TAX_ADULT", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.GstAdult != 0)
                    db.AddInParameter(dbCmd, "@GST_ADULT", DbType.Decimal, ObjInquiryGRDMappingBDto.GstAdult);
                else
                    db.AddInParameter(dbCmd, "@GST_ADULT", DbType.Decimal, DBNull.Value);


                if (ObjInquiryGRDMappingBDto.QuotedAmtChildWithBed != 0)
                    db.AddInParameter(dbCmd, "@QUOTED_CHILD", DbType.Decimal, ObjInquiryGRDMappingBDto.QuotedAmtChildWithBed);
                else
                    db.AddInParameter(dbCmd, "@QUOTED_CHILD", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.TaxChildWitBed != 0)
                    db.AddInParameter(dbCmd, "@TAX_CHILD", DbType.Decimal, ObjInquiryGRDMappingBDto.TaxChildWitBed);
                else
                    db.AddInParameter(dbCmd, "@TAX_CHILD", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.GstChildWitBed != 0)
                    db.AddInParameter(dbCmd, "@GST_CHILD", DbType.Decimal, ObjInquiryGRDMappingBDto.GstChildWitBed);
                else
                    db.AddInParameter(dbCmd, "@GST_CHILD", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.QuotedAmtInfant != 0)
                    db.AddInParameter(dbCmd, "@QUOTED_INFANT", DbType.Decimal, ObjInquiryGRDMappingBDto.QuotedAmtInfant);
                else
                    db.AddInParameter(dbCmd, "@QUOTED_INFANT", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.TaxInfant != 0)
                    db.AddInParameter(dbCmd, "@TAX_INFANT", DbType.Decimal, ObjInquiryGRDMappingBDto.TaxInfant);
                else
                    db.AddInParameter(dbCmd, "@TAX_INFANT", DbType.Decimal, DBNull.Value);

                if (ObjInquiryGRDMappingBDto.GstInfant != 0)
                    db.AddInParameter(dbCmd, "@GST_INFANT", DbType.Decimal, ObjInquiryGRDMappingBDto.GstInfant);
                else
                    db.AddInParameter(dbCmd, "@GST_INFANT", DbType.Decimal, DBNull.Value);

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



        public int InquiryForTourBookingDelete(int srNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int i = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQURIES_TOUR_DELETE");
                db.AddInParameter(dbCmd, "@TOUR_INQ_SR_NO", DbType.Int32, srNo);
                i = db.ExecuteNonQuery(dbCmd);

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
            return i;
        }

        public int InquiryForTourRevise(int InquiryId, int srNo, int productId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int i = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQURIES_TOUR_REVISE");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, InquiryId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, srNo);
                db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, productId);
                i = db.ExecuteNonQuery(dbCmd);

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
            return i;
        }

        public int InquiryForTicketRevise(int InquiryId, int srNo, int productId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int i = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQURIES_TICKET_REVISE");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, InquiryId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, srNo);
                db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, productId);

                i = db.ExecuteNonQuery(dbCmd);

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
            return i;
        }





        public int InquiryForTourReviseRollBack(int InquiryId, int srNo, int productId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int i = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQURIES_TOUR_REVISE_ROLLBACK");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, InquiryId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, srNo);
                db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, productId);
                i = db.ExecuteNonQuery(dbCmd);

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
            return i;
        }


        public int InquiryForTicketReviseRollBack(int InquiryId, int srNo, int productId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int i = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQURIES_TICKET_REVISE_ROLLBACK");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, InquiryId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, srNo);
                db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, productId);
                i = db.ExecuteNonQuery(dbCmd);

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
            return i;
        }





        public DataTable GetTourBooking(int InquiryId, int srNo, string type)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQURIES_TOUR_SELECT);

                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, InquiryId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, srNo);
                db.AddInParameter(dbCmd, "@type", DbType.String, type);

                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GetInquiredTourCost(int inquirySerialNo, int toutId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQUIRY_FOR_TOUR_QUOTED_AMOUNT_SELECT_FOR_EMAIL");

                db.AddInParameter(dbCmd, "@INQUIRY_SR_NO", DbType.Int32, inquirySerialNo);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, toutId);

                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GetStandardTourCost(int toutId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TOUR_CURRENCY_PRICE_SELECT");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, toutId);

                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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


        public DataTable GetTourBooking(int srNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQURIES_TOUR_SELECT_BY_ID");
                db.AddInParameter(dbCmd, "@TOUR_INQ_SR_NO", DbType.Int32, srNo);

                ds = db.ExecuteDataSet(dbCmd);
                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GetTourQuotedAmount(int inquiryId, int srNo, int tourInqSrNo, int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQUIRY_FOR_TOUR_QUOTED_AMOUNT_SELECT");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
                db.AddInParameter(dbCmd, "@INQUIRY_SR_NO", DbType.Int32, srNo);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
                db.AddInParameter(dbCmd, "@TOUR_INQ_SR_NO", DbType.Int32, tourInqSrNo);
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
        public DataTable GetTicketQuotedAmount(int inquiryId, int InqSrNo, int TicketTabId, int TicketInqSrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQUIRY_FOR_TICKET_QUOTED_AMOUNT_SELECT");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
                db.AddInParameter(dbCmd, "@INQUIRY_SR_NO", DbType.Int32, InqSrNo);
                db.AddInParameter(dbCmd, "@TICKET_TAB_ID", DbType.Int32, TicketTabId);
                db.AddInParameter(dbCmd, "@TICKET_INQ_SR_NO", DbType.Int32, TicketInqSrNo);
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




        public DataSet GetTourCountry()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TOUR_COUNTRY_SELECT");
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

        public DataSet GetTourCountry1(int inquiryId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TOUR_COUNTRY_SELECT_1");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
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

        public DataSet GetTourState()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TOUR_STATE_SELECT");
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

        public DataSet GetTourState1(int inquiryId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TOUR_STATE_SELECT_1");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
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

        #region Ticket GDS

        public DataTable GetTicketGdsDetailById(int TicketInqSrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQUIRY_FOR_TICKET_GDS_SELECT_BY_ID");
                db.AddInParameter(dbCmd, "@TICKET_INQ_SR_NO", DbType.Int32, TicketInqSrNo);

                ds = db.ExecuteDataSet(dbCmd);

                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GetTicketGdsDetail(int inquiryId, int ticketTabId, int SrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQUIRY_FOR_TICKET_GDS_SELECT");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
                db.AddInParameter(dbCmd, "@TICKET_TAB_ID", DbType.Int32, ticketTabId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, SrNo);
                ds = db.ExecuteDataSet(dbCmd);

                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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



        public int InsertTicketGdsDetail(InquiryGRDMappingBDto inquiryGDS, ref int serialNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQUIRY_FOR_TICKET_GDS_INSERT");

                if (inquiryGDS.InquiryId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryGDS.InquiryId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, inquiryGDS.SerialNo);

                if (inquiryGDS.TicketTabId != 0)
                    db.AddInParameter(dbCmd, "@TICKET_TAB_ID", DbType.Int32, inquiryGDS.TicketTabId);
                else
                    db.AddInParameter(dbCmd, "@TICKET_TAB_ID", DbType.Int32, DBNull.Value);


                if (inquiryGDS.DestinationCityId != 0)
                    db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, inquiryGDS.DestinationCityId);
                else
                    db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, DBNull.Value);

                if (inquiryGDS.ProductId != 0)
                    db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, inquiryGDS.ProductId);
                else
                    db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, DBNull.Value);

                if (inquiryGDS.AirlinesId != 0)
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, inquiryGDS.AirlinesId);
                else
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, DBNull.Value);


                if (!String.IsNullOrEmpty(inquiryGDS.GDSAirportCode))
                    db.AddInParameter(dbCmd, "@GDS_AIRPORT_CODE", DbType.String, inquiryGDS.GDSAirportCode);
                else
                    db.AddInParameter(dbCmd, "@GDS_AIRPORT_CODE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.TimeDetails))
                    db.AddInParameter(dbCmd, "@TIME_DETAILS", DbType.String, inquiryGDS.TimeDetails);
                else
                    db.AddInParameter(dbCmd, "@TIME_DETAILS", DbType.String, DBNull.Value);

                if (inquiryGDS.TotalFareWithTax != 0)
                    db.AddInParameter(dbCmd, "@TOTAL_FARE_WITH_TAX", DbType.Decimal, inquiryGDS.TotalFareWithTax);
                else
                    db.AddInParameter(dbCmd, "@TOTAL_FARE_WITH_TAX", DbType.Decimal, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.TimeLimit))
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.String, inquiryGDS.TimeLimit);
                else
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.BaggageAllwance))
                    db.AddInParameter(dbCmd, "@BAGGAGE_ALLOWANCE", DbType.String, inquiryGDS.BaggageAllwance);
                else
                    db.AddInParameter(dbCmd, "@BAGGAGE_ALLOWANCE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.Cancellation))
                    db.AddInParameter(dbCmd, "@CANCELLATION", DbType.String, inquiryGDS.Cancellation);
                else
                    db.AddInParameter(dbCmd, "@CANCELLATION", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.DateChange))
                    db.AddInParameter(dbCmd, "@DATE_CHANGE", DbType.String, inquiryGDS.DateChange);
                else
                    db.AddInParameter(dbCmd, "@DATE_CHANGE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.PaxNo))
                    db.AddInParameter(dbCmd, "@LOCAL_PAX_NO", DbType.String, inquiryGDS.PaxNo);
                else
                    db.AddInParameter(dbCmd, "@LOCAL_PAX_NO", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.Email))
                    db.AddInParameter(dbCmd, "@EMAIL", DbType.String, inquiryGDS.Email);
                else
                    db.AddInParameter(dbCmd, "@EMAIL", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.PaymentPolicy))
                    db.AddInParameter(dbCmd, "@PAYMENT_POLICY", DbType.String, inquiryGDS.PaymentPolicy);
                else
                    db.AddInParameter(dbCmd, "@PAYMENT_POLICY", DbType.String, DBNull.Value);

                if (inquiryGDS.FaqDocument != null)
                    db.AddInParameter(dbCmd, "@FAQ_DOC", DbType.Binary, inquiryGDS.FaqDocument);
                else
                    db.AddInParameter(dbCmd, "@FAQ_DOC", DbType.Binary, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.FaqDocType))
                    db.AddInParameter(dbCmd, "@FAQ_DOC_CONTENT_TYPE", DbType.String, inquiryGDS.FaqDocType);
                else
                    db.AddInParameter(dbCmd, "@FAQ_DOC_CONTENT_TYPE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.FaqDocFileName))
                    db.AddInParameter(dbCmd, "@FAQ_DOC_FILE_NAME", DbType.String, inquiryGDS.FaqDocFileName);
                else
                    db.AddInParameter(dbCmd, "@FAQ_DOC_FILE_NAME", DbType.String, DBNull.Value);

                if (inquiryGDS.TermsDocument != null)
                    db.AddInParameter(dbCmd, "@T_C_DOC", DbType.Binary, inquiryGDS.TermsDocument);
                else
                    db.AddInParameter(dbCmd, "@T_C_DOC", DbType.Binary, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.TermsDocType))
                    db.AddInParameter(dbCmd, "@T_C_DOC_CONTENT_TYPE", DbType.String, inquiryGDS.TermsDocType);
                else
                    db.AddInParameter(dbCmd, "@T_C_DOC_CONTENT_TYPE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.TermsDocFileName))
                    db.AddInParameter(dbCmd, "@T_C_DOC_FILE_NAME", DbType.String, inquiryGDS.TermsDocFileName);
                else
                    db.AddInParameter(dbCmd, "@T_C_DOC_FILE_NAME", DbType.String, DBNull.Value);

                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 10);

                Result = db.ExecuteNonQuery(dbCmd);
                serialNo = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
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

        public int UpdateTicketGdsDetail(InquiryGRDMappingBDto inquiryGDS)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQUIRY_FOR_TICKET_GDS_UPDATE");

                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, inquiryGDS.SerialNo);

                if (inquiryGDS.InquiryId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryGDS.InquiryId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, DBNull.Value);

                if (inquiryGDS.TicketInqSrNo != 0)
                    db.AddInParameter(dbCmd, "@TICKET_INQ_SR_NO", DbType.Int32, inquiryGDS.TicketInqSrNo);
                else
                    db.AddInParameter(dbCmd, "@TICKET_INQ_SR_NO", DbType.Int32, DBNull.Value);

                if (inquiryGDS.TicketTabId != 0)
                    db.AddInParameter(dbCmd, "@TICKET_TAB_ID", DbType.Int32, inquiryGDS.TicketTabId);
                else
                    db.AddInParameter(dbCmd, "@TICKET_TAB_ID", DbType.Int32, DBNull.Value);


                if (inquiryGDS.ProductId != 0)
                    db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, inquiryGDS.ProductId);
                else
                    db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, DBNull.Value);


                if (inquiryGDS.DestinationCityId != 0)
                    db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, inquiryGDS.DestinationCityId);
                else
                    db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, DBNull.Value);

                if (inquiryGDS.AirlinesId != 0)
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, inquiryGDS.AirlinesId);
                else
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.GDSAirportCode))
                    db.AddInParameter(dbCmd, "@GDS_AIRPORT_CODE", DbType.String, inquiryGDS.GDSAirportCode);
                else
                    db.AddInParameter(dbCmd, "@GDS_AIRPORT_CODE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.TimeDetails))
                    db.AddInParameter(dbCmd, "@TIME_DETAILS", DbType.String, inquiryGDS.TimeDetails);
                else
                    db.AddInParameter(dbCmd, "@TIME_DETAILS", DbType.String, DBNull.Value);

                if (inquiryGDS.TotalFareWithTax != 0)
                    db.AddInParameter(dbCmd, "@TOTAL_FARE_WITH_TAX", DbType.Decimal, inquiryGDS.TotalFareWithTax);
                else
                    db.AddInParameter(dbCmd, "@TOTAL_FARE_WITH_TAX", DbType.Decimal, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.TimeLimit))
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.String, inquiryGDS.TimeLimit);
                else
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.BaggageAllwance))
                    db.AddInParameter(dbCmd, "@BAGGAGE_ALLOWANCE", DbType.String, inquiryGDS.BaggageAllwance);
                else
                    db.AddInParameter(dbCmd, "@BAGGAGE_ALLOWANCE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.Cancellation))
                    db.AddInParameter(dbCmd, "@CANCELLATION", DbType.String, inquiryGDS.Cancellation);
                else
                    db.AddInParameter(dbCmd, "@CANCELLATION", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.DateChange))
                    db.AddInParameter(dbCmd, "@DATE_CHANGE", DbType.String, inquiryGDS.DateChange);
                else
                    db.AddInParameter(dbCmd, "@DATE_CHANGE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.PaxNo))
                    db.AddInParameter(dbCmd, "@LOCAL_PAX_NO", DbType.String, inquiryGDS.PaxNo);
                else
                    db.AddInParameter(dbCmd, "@LOCAL_PAX_NO", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.Email))
                    db.AddInParameter(dbCmd, "@EMAIL", DbType.String, inquiryGDS.Email);
                else
                    db.AddInParameter(dbCmd, "@EMAIL", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.PaymentPolicy))
                    db.AddInParameter(dbCmd, "@PAYMENT_POLICY", DbType.String, inquiryGDS.PaymentPolicy);
                else
                    db.AddInParameter(dbCmd, "@PAYMENT_POLICY", DbType.String, DBNull.Value);

                if (inquiryGDS.FaqDocument != null)
                    db.AddInParameter(dbCmd, "@FAQ_DOC", DbType.Binary, inquiryGDS.FaqDocument);
                else
                    db.AddInParameter(dbCmd, "@FAQ_DOC", DbType.Binary, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.FaqDocType))
                    db.AddInParameter(dbCmd, "@FAQ_DOC_CONTENT_TYPE", DbType.String, inquiryGDS.FaqDocType);
                else
                    db.AddInParameter(dbCmd, "@FAQ_DOC_CONTENT_TYPE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.FaqDocFileName))
                    db.AddInParameter(dbCmd, "@FAQ_DOC_FILE_NAME", DbType.String, inquiryGDS.FaqDocFileName);
                else
                    db.AddInParameter(dbCmd, "@FAQ_DOC_FILE_NAME", DbType.String, DBNull.Value);

                if (inquiryGDS.TermsDocument != null)
                    db.AddInParameter(dbCmd, "@T_C_DOC", DbType.Binary, inquiryGDS.TermsDocument);
                else
                    db.AddInParameter(dbCmd, "@T_C_DOC", DbType.Binary, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.TermsDocType))
                    db.AddInParameter(dbCmd, "@T_C_DOC_CONTENT_TYPE", DbType.String, inquiryGDS.TermsDocType);
                else
                    db.AddInParameter(dbCmd, "@T_C_DOC_CONTENT_TYPE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryGDS.TermsDocFileName))
                    db.AddInParameter(dbCmd, "@T_C_DOC_FILE_NAME", DbType.String, inquiryGDS.TermsDocFileName);
                else
                    db.AddInParameter(dbCmd, "@T_C_DOC_FILE_NAME", DbType.String, DBNull.Value);

                db.AddOutParameter(dbCmd, "@IS_UPDATE", DbType.Int32, 10);

                Result = db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_UPDATE"));


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

        public int DeleteTicketGdsDetail(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQUIRY_FOR_TICKET_GDS_DELETE");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.String, idCollections);
                db.AddOutParameter(dbCmd, "@ERRORCODE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ERRORCODE"));
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

        public DataTable GetAirportGdsDetail(int airlineId, string airportGdsCode)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_AIRPORT_GDS_DETAIL_SELECT");
                db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, airlineId);
                db.AddInParameter(dbCmd, "@GDS_AIRPORT_CODE", DbType.String, airportGdsCode);
                ds = db.ExecuteDataSet(dbCmd);

                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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

        public DataTable GetBaggageDetail(int airlineId, int airportId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_AIRPORT_BAGGAGE_SELECT_BYID");
                db.AddInParameter(dbCmd, "@AIRPORT_ID", DbType.Int32, airportId);
                db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.String, airlineId);
                ds = db.ExecuteDataSet(dbCmd);

                using (ds)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
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





        #endregion
    }
}

