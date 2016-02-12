using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using CRM.Model.Inquiry.Gadgets;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.InquiryDal.Gadgets
{
    public class CustomerInquiryGadgetsDAL
    {
        #region Inquiry Followup Gadgets
        public DataSet GetCustomerInquries(CustomerInquiryGadgetsBDto inquiry)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_INQURIES_GADGETS_SELECT);

                if (inquiry.InquiryNo != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_NO", DbType.Int32, inquiry.InquiryNo);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_NO", DbType.Int32, DBNull.Value);

                if (inquiry.InquiryFor != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_FOR", DbType.Int32, inquiry.InquiryFor);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_FOR", DbType.Int32, DBNull.Value);

                if (inquiry.InquiryStatus != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_STATUS", DbType.Int32, inquiry.InquiryStatus);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_STATUS", DbType.Int32, DBNull.Value);

                if (inquiry.InquiryFromDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_FROM", DbType.DateTime, inquiry.InquiryFromDate);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_FROM", DbType.DateTime, DBNull.Value);

                if (inquiry.InquiryToDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_TO", DbType.DateTime, inquiry.InquiryToDate);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_TO", DbType.DateTime, DBNull.Value);

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

        #region Today Inquiry Followup Gadgets by Next_Inquiry_followup_date
        public DataSet GetTodayCustomerInquries(CustomerInquiryGadgetsBDto inquiry)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_INQURIES_GADGETS_SELECT_BYDATE);

                if (inquiry.InquiryFromDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@NEXT_FOLLOWUP_DATE_FROM", DbType.DateTime, inquiry.InquiryFromDate);
                else
                    db.AddInParameter(dbCmd, "@NEXT_FOLLOWUP_DATE_FROM", DbType.DateTime, DBNull.Value);

                if (inquiry.InquiryToDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@NEXT_FOLLOWUP_DATE_To", DbType.DateTime, inquiry.InquiryToDate);
                else
                    db.AddInParameter(dbCmd, "@NEXT_FOLLOWUP_DATE_To", DbType.DateTime, DBNull.Value);

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

        #region Inquiry Followup Gadgets By staff
        public DataSet GetInquriesByStaff(CustomerInquiryGadgetsBDto inquiry)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_INQURIES_GADGETS_BYSTAFF_SELECT);

                if (inquiry.CompanyId != 0)
                    db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, inquiry.CompanyId);
                else
                    db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, DBNull.Value);

                if (inquiry.DepartmentId != 0)
                    db.AddInParameter(dbCmd, "@DEPARTMENT_ID", DbType.Int32, inquiry.DepartmentId);
                else
                    db.AddInParameter(dbCmd, "@DEPARTMENT_ID", DbType.Int32, DBNull.Value);

                if (inquiry.InquiryFromDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_FROM", DbType.DateTime, inquiry.InquiryFromDate);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_FROM", DbType.DateTime, DBNull.Value);

                if (inquiry.InquiryToDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_TO", DbType.DateTime, inquiry.InquiryToDate);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_TO", DbType.DateTime, DBNull.Value);

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

        #region Inquiry Followup Gadgets By Company
        public DataSet GetInquriesByCompany(CustomerInquiryGadgetsBDto inquiry)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_INQURIES_GADGETS_SELECT_BYCOMPANY);

                if (inquiry.CompanyId != 0)
                    db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, inquiry.CompanyId);
                else
                    db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, DBNull.Value);

                if (inquiry.InquiryFromDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_FROM", DbType.DateTime, inquiry.InquiryFromDate);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_FROM", DbType.DateTime, DBNull.Value);

                if (inquiry.InquiryToDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_TO", DbType.DateTime, inquiry.InquiryToDate);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_TO", DbType.DateTime, DBNull.Value);

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

        #region Total Inquiry Detail By Manager
        public DataSet GetInquriesByManager(CustomerInquiryGadgetsBDto inquiry)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_INQURIES_GADGETS_BYMANAGER_SELECT);

                if (inquiry.CompanyId != 0)
                    db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, inquiry.CompanyId);
                else
                    db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, DBNull.Value);

                if (inquiry.EmployeeId != 0)
                    db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, inquiry.EmployeeId);
                else
                    db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, DBNull.Value);

                if (inquiry.InquiryFromDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_FROM", DbType.DateTime, inquiry.InquiryFromDate);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_FROM", DbType.DateTime, DBNull.Value);

                if (inquiry.InquiryToDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_TO", DbType.DateTime, inquiry.InquiryToDate);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_TO", DbType.DateTime, DBNull.Value);

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

        #region Inquiry Followup by Company Id
        public DataSet GetInquriesByCompany(int CompanyId, DateTime DateFrom, DateTime DateTo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_INQURIES_GADGETS_BYCOMPANY_SELECT);

                if (CompanyId != 0)
                    db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, CompanyId);
                else
                    db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, DBNull.Value);



                if (DateFrom != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_FROM", DbType.DateTime, DateFrom);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_FROM", DbType.DateTime, DBNull.Value);

                if (DateTo != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_TO", DbType.DateTime, DateTo);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_DATE_TO", DbType.DateTime, DBNull.Value);





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
