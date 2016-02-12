using System;
using System.Data;
using System.Data.Common;
using CRM.Model.CustomersModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.InquiryDal
{
    public class CustomerInquiryFollowupDal
    {

        public DataSet GetCustomerFollowsUp(int inquiryId, int ownerCompanyId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_INQUIRIES_FOLLOWUPS_SELECT);
                db.AddInParameter(dbCmd, "@INQUIRY_NO", DbType.Int32, inquiryId);
                db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, ownerCompanyId);
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

        public int InsertCustomerFollowsUp(InquiryFollowUpBDto inquiryFollowUp)
        {
            int result = 0;
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_INQUIRIES_FOLLOWUPS_INSERT);

                db.AddInParameter(dbCmd, "@INQUIRY_NO", DbType.Int32, inquiryFollowUp.InquiryNo);

                if (!String.IsNullOrEmpty(inquiryFollowUp.FollowupFor))
                    db.AddInParameter(dbCmd, "@FOLLOWUP_FOR", DbType.String, inquiryFollowUp.FollowupFor);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_FOR", DbType.String, DBNull.Value);

                if (inquiryFollowUp.NextFollowupDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@NEXT_FOLLOWUP_DATE", DbType.DateTime, inquiryFollowUp.NextFollowupDate);
                else
                    db.AddInParameter(dbCmd, "@NEXT_FOLLOWUP_DATE", DbType.DateTime, DBNull.Value);

                if (inquiryFollowUp.FollowupDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@FOLLOWUP_DATE", DbType.DateTime, inquiryFollowUp.FollowupDate);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_DATE", DbType.DateTime, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryFollowUp.FollowupDescription))
                    db.AddInParameter(dbCmd, "@FOLLOWUP_DESC", DbType.String, inquiryFollowUp.FollowupDescription);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_DESC", DbType.String, DBNull.Value);

                if (inquiryFollowUp.SalesPersonId != 0)
                    db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, inquiryFollowUp.SalesPersonId);
                else
                    db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, DBNull.Value);

                if (inquiryFollowUp.FollowupModeId != 0)
                    db.AddInParameter(dbCmd, "@FOLLOWUP_MODE", DbType.Int32, inquiryFollowUp.FollowupModeId);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_MODE", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryFollowUp.InternalRemarks))
                    db.AddInParameter(dbCmd, "@INTERNAL_REMARKS", DbType.String, inquiryFollowUp.InternalRemarks);
                else
                    db.AddInParameter(dbCmd, "@INTERNAL_REMARKS", DbType.String, DBNull.Value);

                if (inquiryFollowUp.FollowupStatusId != 0)
                    db.AddInParameter(dbCmd, "@FOLLOWUP_STATUS", DbType.Int32, inquiryFollowUp.FollowupStatusId);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_STATUS", DbType.Int32, DBNull.Value);

                if (inquiryFollowUp.OwnerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, inquiryFollowUp.OwnerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, inquiryFollowUp.UserId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                int t = db.ExecuteNonQuery(dbCmd);
                result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
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


            return result;
        }

        public int UpdateCustomerFollowsUp(InquiryFollowUpBDto inquiryFollowUp)
        {
            int result = 0;
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_INQUIRIES_FOLLOWUPS_UPDATE);

                db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, inquiryFollowUp.FollowupNo);
                db.AddInParameter(dbCmd, "@INQUIRY_NO", DbType.Int32, inquiryFollowUp.InquiryNo);

                if (!String.IsNullOrEmpty(inquiryFollowUp.FollowupFor))
                    db.AddInParameter(dbCmd, "@FOLLOWUP_FOR", DbType.String, inquiryFollowUp.FollowupFor);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_FOR", DbType.String, DBNull.Value);

                if (inquiryFollowUp.NextFollowupDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@NEXT_FOLLOWUP_DATE", DbType.DateTime, inquiryFollowUp.NextFollowupDate);
                else
                    db.AddInParameter(dbCmd, "@NEXT_FOLLOWUP_DATE", DbType.DateTime, DBNull.Value);

                if (inquiryFollowUp.FollowupDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@FOLLOWUP_DATE", DbType.DateTime, inquiryFollowUp.FollowupDate);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_DATE", DbType.DateTime, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryFollowUp.FollowupDescription))
                    db.AddInParameter(dbCmd, "@FOLLOWUP_DESC", DbType.String, inquiryFollowUp.FollowupDescription);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_DESC", DbType.String, DBNull.Value);

                if (inquiryFollowUp.SalesPersonId != 0)
                    db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, inquiryFollowUp.SalesPersonId);
                else
                    db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, DBNull.Value);

                if (inquiryFollowUp.FollowupModeId != 0)
                    db.AddInParameter(dbCmd, "@FOLLOWUP_MODE", DbType.Int32, inquiryFollowUp.FollowupModeId);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_MODE", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryFollowUp.InternalRemarks))
                    db.AddInParameter(dbCmd, "@INTERNAL_REMARKS", DbType.String, inquiryFollowUp.InternalRemarks);
                else
                    db.AddInParameter(dbCmd, "@INTERNAL_REMARKS", DbType.String, DBNull.Value);

                if (inquiryFollowUp.FollowupStatusId != 0)
                    db.AddInParameter(dbCmd, "@FOLLOWUP_STATUS", DbType.Int32, inquiryFollowUp.FollowupStatusId);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_STATUS", DbType.Int32, DBNull.Value);

                if (inquiryFollowUp.OwnerCompanyId != 0)
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, inquiryFollowUp.OwnerCompanyId);
                else
                    db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, inquiryFollowUp.UserId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
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
            return result;
        }

        public int DeleteCustomerFollowsUp(int followupNo, int inquiryNo)
        {
            int result = 0;
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_INQUIRIES_FOLLOWUPS_DELETE);
                db.AddInParameter(dbCmd, "@INQUIRY_NO", DbType.Int32, inquiryNo);
                db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, followupNo);
                db.AddOutParameter(dbCmd, "@IS_DELETE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_DELETE"));
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
            return result;
        }

        public DataSet GetInquiryFollowsUp(int inquiryId, int followupNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRIES_FOLLOWUPS_SELECT);

                db.AddInParameter(dbCmd, "@INQUIRY_NO", DbType.Int32, inquiryId);

                if (followupNo != 0)
                    db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, followupNo);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, DBNull.Value);
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

        public int InsertInquiryFollowsUp(CRM.Model.Inquiry.InquiryFollowupBDto inquiryFollowUp, ref int inqFollowupSrNo)
        {
            int result = 0;
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_FOLLOWUP_INSERT);

                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryFollowUp.InquiryNo);

                if (inquiryFollowUp.InquirySerialNo != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_SR_NO", DbType.String, inquiryFollowUp.InquirySerialNo);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_SR_NO", DbType.String, DBNull.Value);

                //if (inquiryFollowUp.FollowupFor !=0)
                //    db.AddInParameter(dbCmd, "@FOLLOWUP_FOR", DbType.String, inquiryFollowUp.FollowupFor);
                //else
                //    db.AddInParameter(dbCmd, "@FOLLOWUP_FOR", DbType.String, DBNull.Value);

                if (inquiryFollowUp.AskedFollowupDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@ASKED_FOLLOWUP_DATE", DbType.DateTime, inquiryFollowUp.AskedFollowupDate);
                else
                    db.AddInParameter(dbCmd, "@ASKED_FOLLOWUP_DATE", DbType.DateTime, DBNull.Value);

                if (inquiryFollowUp.FollowupDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@FOLLOWUP_DATE", DbType.DateTime, inquiryFollowUp.FollowupDate);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_DATE", DbType.DateTime, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryFollowUp.FollowupDescription))
                    db.AddInParameter(dbCmd, "@FOLLOWUP_DESC", DbType.String, inquiryFollowUp.FollowupDescription);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_DESC", DbType.String, DBNull.Value);

                if (inquiryFollowUp.SalesPersonId != 0)
                    db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, inquiryFollowUp.SalesPersonId);
                else
                    db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, DBNull.Value);

                if (inquiryFollowUp.FollowupModeId != 0)
                    db.AddInParameter(dbCmd, "@FOLLOWUP_MODE", DbType.Int32, inquiryFollowUp.FollowupModeId);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_MODE", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryFollowUp.InternalRemarks))
                    db.AddInParameter(dbCmd, "@INTERNAL_REMARKS", DbType.String, inquiryFollowUp.InternalRemarks);
                else
                    db.AddInParameter(dbCmd, "@INTERNAL_REMARKS", DbType.String, DBNull.Value);

                if (inquiryFollowUp.FollowupStatusId != 0)
                    db.AddInParameter(dbCmd, "@FOLLOWUP_STATUS", DbType.Int32, inquiryFollowUp.FollowupStatusId);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_STATUS", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryFollowUp.Tours))
                    db.AddInParameter(dbCmd, "@SELECTED_TOURS", DbType.String, inquiryFollowUp.Tours);
                else
                    db.AddInParameter(dbCmd, "@SELECTED_TOURS", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@IS_FINISHED", DbType.Boolean, inquiryFollowUp.IsFinished);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, inquiryFollowUp.UserId);

                db.AddOutParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, 8);

                result = db.ExecuteNonQuery(dbCmd);
                Int32.TryParse(Convert.ToString(db.GetParameterValue(dbCmd, "@FOLLOWUP_NO")), out inqFollowupSrNo);
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


            return result;
        }

        public int UpdateInquiryFollowsUp(CRM.Model.Inquiry.InquiryFollowupBDto inquiryFollowUp)
        {
            int result = 0;
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_FOLLOWUP_UPDATE);

                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryFollowUp.InquiryNo);
                db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, inquiryFollowUp.FollowupNo);

                if (inquiryFollowUp.FollowupFor != 0)
                    db.AddInParameter(dbCmd, "@FOLLOWUP_FOR", DbType.String, inquiryFollowUp.FollowupFor);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_FOR", DbType.String, DBNull.Value);

                if (inquiryFollowUp.AskedFollowupDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@ASKED_FOLLOWUP_DATE", DbType.DateTime, inquiryFollowUp.AskedFollowupDate);
                else
                    db.AddInParameter(dbCmd, "@ASKED_FOLLOWUP_DATE", DbType.DateTime, DBNull.Value);

                if (inquiryFollowUp.FollowupDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@FOLLOWUP_DATE", DbType.DateTime, inquiryFollowUp.FollowupDate);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_DATE", DbType.DateTime, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryFollowUp.FollowupDescription))
                    db.AddInParameter(dbCmd, "@FOLLOWUP_DESC", DbType.String, inquiryFollowUp.FollowupDescription);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_DESC", DbType.String, DBNull.Value);

                if (inquiryFollowUp.SalesPersonId != 0)
                    db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, inquiryFollowUp.SalesPersonId);
                else
                    db.AddInParameter(dbCmd, "@SALES_PERSON_ID", DbType.Int32, DBNull.Value);

                if (inquiryFollowUp.FollowupModeId != 0)
                    db.AddInParameter(dbCmd, "@FOLLOWUP_MODE", DbType.Int32, inquiryFollowUp.FollowupModeId);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_MODE", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryFollowUp.InternalRemarks))
                    db.AddInParameter(dbCmd, "@INTERNAL_REMARKS", DbType.String, inquiryFollowUp.InternalRemarks);
                else
                    db.AddInParameter(dbCmd, "@INTERNAL_REMARKS", DbType.String, DBNull.Value);

                if (inquiryFollowUp.FollowupStatusId != 0)
                    db.AddInParameter(dbCmd, "@FOLLOWUP_STATUS", DbType.Int32, inquiryFollowUp.FollowupStatusId);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_STATUS", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(inquiryFollowUp.Tours))
                    db.AddInParameter(dbCmd, "@SELECTED_TOURS", DbType.String, inquiryFollowUp.Tours);
                else
                    db.AddInParameter(dbCmd, "@SELECTED_TOURS", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, inquiryFollowUp.UserId);

                //db.AddInParameter(dbCmd, "@IS_FINISHED", DbType.Boolean, inquiryFollowUp.IsFinished);

                result = db.ExecuteNonQuery(dbCmd);

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


            return result;
        }

        public DataTable GeInquiryTourForFollowUp(int InquiryId, int SrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_INQUIRY_TOUR_SELECT_FOR_FOLLOWUP");
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

        public DataTable GeFollowUpStausDetail(int followUpNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.INQ_INQUIRY_STATUS_DETAIL_SELECT");
                db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, followUpNo);
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

    }
}
