using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.Account
{
    public class ExpenceVoucherSp
    {
        public void update_accounts_entry(int account_voucher_id, String gl_code, String invoice_no, String voucher_date, String voucher_type, int prepared_by, int approved_by, int posted_by, String REMARKS, String voucher_status_id, int voucher_detail_id, String cr_amount, String dr_amount, String payment_mode, int flag, String AGAINST_SALES, int CPID,String GL_date)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_EXPENCE_VOUCHER_HEADER");
                db.AddInParameter(dbCmd, "@EXPENCE_VOUCHER_ID", DbType.Int32, account_voucher_id);

                db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, gl_code);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
                db.AddInParameter(dbCmd, "@AGAINST_SALES", DbType.String, AGAINST_SALES);


                if (voucher_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@VOUCHER_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@VOUCHER_DATE", DbType.DateTime, DateTime.ParseExact(voucher_date.ToString(), "dd/MM/yyyy", null));
                }

                db.AddInParameter(dbCmd, "@NERRATION", DbType.String, REMARKS);
                db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, voucher_type);
                db.AddInParameter(dbCmd, "@VOUCHER_CURRENCY_ID", DbType.String, "THB");
                db.AddInParameter(dbCmd, "@PREPARED_BY", DbType.Int32, prepared_by);
                db.AddInParameter(dbCmd, "@APPROVED_BY", DbType.Int32, approved_by);
                db.AddInParameter(dbCmd, "@POSTED_BY", DbType.Int32, posted_by);
                db.AddInParameter(dbCmd, "@VOUCHER_STATUS_ID", DbType.String, voucher_status_id);

                db.AddInParameter(dbCmd, "@EXPENCE_VOUCHER_DETAILS_ID", DbType.Int32, voucher_detail_id);

                if (cr_amount.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CR_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CR_AMOUNT", DbType.Decimal, Decimal.Parse(cr_amount));
                }

                if (dr_amount.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DR_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DR_AMOUNT", DbType.Decimal, Decimal.Parse(dr_amount));
                }

                db.AddInParameter(dbCmd, "@ACCOUNTS_PAYMENT_MODE_ID", DbType.String, payment_mode);
                db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, CPID);
                if (GL_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DateTime.ParseExact(GL_date.ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@FLAG", DbType.Int32, flag);
                db.ExecuteNonQuery(dbCmd);
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
            // return ds;

        }

        public void updateExpenceVoucher(int account_voucher_id, String gl_code, String invoice_no, String voucher_date, String voucher_type, int prepared_by, int approved_by, int posted_by, String REMARKS, String voucher_status_id, int voucher_detail_id, String cr_amount, String dr_amount, String payment_mode, int flag, String AGAINST_SALES, int CPID, String GL_date)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_EXPENCE_VOUCHER_HEADER");
                db.AddInParameter(dbCmd, "@EXPENCE_VOUCHER_ID", DbType.Int32, account_voucher_id);

                db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, gl_code);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
                db.AddInParameter(dbCmd, "@AGAINST_SALES", DbType.String, AGAINST_SALES);


                if (voucher_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@VOUCHER_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@VOUCHER_DATE", DbType.DateTime, DateTime.ParseExact(voucher_date.ToString(), "dd/MM/yyyy", null));
                }

                db.AddInParameter(dbCmd, "@NERRATION", DbType.String, REMARKS);
                db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, voucher_type);
                db.AddInParameter(dbCmd, "@VOUCHER_CURRENCY_ID", DbType.String, "THB");
                db.AddInParameter(dbCmd, "@PREPARED_BY", DbType.Int32, prepared_by);
                db.AddInParameter(dbCmd, "@APPROVED_BY", DbType.Int32, approved_by);
                db.AddInParameter(dbCmd, "@POSTED_BY", DbType.Int32, posted_by);
                db.AddInParameter(dbCmd, "@VOUCHER_STATUS_ID", DbType.String, voucher_status_id);

                db.AddInParameter(dbCmd, "@EXPENCE_VOUCHER_DETAILS_ID", DbType.Int32, voucher_detail_id);

                if (cr_amount.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CR_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CR_AMOUNT", DbType.Decimal, Decimal.Parse(cr_amount));
                }

                if (dr_amount.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DR_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DR_AMOUNT", DbType.Decimal, Decimal.Parse(dr_amount));
                }

                db.AddInParameter(dbCmd, "@ACCOUNTS_PAYMENT_MODE_ID", DbType.String, payment_mode);
                db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, CPID);
                if (GL_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DateTime.ParseExact(GL_date.ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@FLAG", DbType.Int32, flag);
                db.ExecuteNonQuery(dbCmd);
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
            // return ds;

        }

        public DataSet FETCH_INVOICE_EXPENCE(String sp_name)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                dsData = db.ExecuteDataSet(dbCmd);
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
            return dsData;
        }

        public DataSet fectchalldataforexpence(String sp_name, String invoice_no)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
                dsData = db.ExecuteDataSet(dbCmd);
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
            return dsData;
        }

        /** INSER VOUCHER NO FOR EXPENCE **/
        public void insertVoucherNoforExpence(String spName, String vouhcer_status, String seq_no, String voucher_no)
        {

            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(spName);


                db.AddInParameter(dbCmd, "@VOUCHER_STATUS", DbType.String, vouhcer_status);
                db.AddInParameter(dbCmd, "@SEQ_NO", DbType.String, seq_no);

                db.AddInParameter(dbCmd, "@VOUCHER_NO", DbType.String, voucher_no);


                db.ExecuteNonQuery(dbCmd);
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

        }

        /*Get data for Edit Mode*/
        public DataSet getDataForEditMode(int EXPENCE_VOUCHER_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_EXPENCE_VOUCHER_EDIT_DETAILS");
                db.AddInParameter(dbCmd, "@EXPENCE_VOUCHER_ID", DbType.Int32, EXPENCE_VOUCHER_ID);
                dsData = db.ExecuteDataSet(dbCmd);
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
            return dsData;
        }

        public DataSet commonSp(string spName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(spName);
                
                dsData = db.ExecuteDataSet(dbCmd);
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
            return dsData;
        }

    }
}
