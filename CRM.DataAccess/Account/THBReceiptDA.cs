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
    public class THBReceiptDA
    {

        public void insert_accounts_entry(int sales_voucher_id, String voucher_no, String gl_code, String invoice_no, String voucher_amount, String voucher_type, String voucher_currency, int employee_id, String narration, int prepared_by, int approved_by, int posted_by, String voucher_status_id, int voucher_detail_id, String sales_forex_amount, String cr_amount, String dr_amount, String payment_mode, String cheque_no, String cheque_date, String bank_id, String branch, String remarks, String receipt_no, String receipt_date, String ex_rate, String forex_amount, String forex_currency, int cust_rel_no, int comapny_id, String gl_date, Boolean on_acc_flag, int flag, Boolean IS_THB_RECEIPT)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_SALES_RECEIPT_VOUCHER_HEADER_THB");

                db.AddInParameter(dbCmd, "@SALES_RECEIPT_VOUCHER_ID", DbType.Int32, sales_voucher_id);
                db.AddInParameter(dbCmd, "@VOUCHER_NO", DbType.String, voucher_no);
                db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, gl_code);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);



                //if (voucher_date.ToString().Equals(""))
                //{
                //    db.AddInParameter(dbCmd, "@VOUCHER_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@VOUCHER_DATE", DbType.DateTime, DateTime.ParseExact(voucher_date.ToString(), "dd/MM/yyyy", null));
                //}

                db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal, Decimal.Parse(voucher_amount));
                db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, voucher_type);
                db.AddInParameter(dbCmd, "@VOUCHER_CURRENCY_ID", DbType.String, voucher_currency);
                db.AddInParameter(dbCmd, "@BRANCH_EMPLOYEE_ID", DbType.Int32, employee_id);

                db.AddInParameter(dbCmd, "@NARRATION", DbType.String, narration);
                db.AddInParameter(dbCmd, "@PREPARED_BY", DbType.Int32, prepared_by);
                db.AddInParameter(dbCmd, "@APPROVED_BY", DbType.Int32, approved_by);
                db.AddInParameter(dbCmd, "@POSTED_BY", DbType.Int32, posted_by);

                db.AddInParameter(dbCmd, "@VOUCHER_STATUS_ID", DbType.String, voucher_status_id);

                db.AddInParameter(dbCmd, "@SALES_RECEIPT_VOUCHER_DETAIL_ID", DbType.Int32, voucher_detail_id);

                if (sales_forex_amount.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@SALES_FOREX_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@SALES_FOREX_AMOUNT", DbType.Decimal, Decimal.Parse(sales_forex_amount));
                }

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

                //if (cheque_no.ToString().Equals("") || cheque_no.ToString() == null)
                //{
                //}
                //else
                //{
                db.AddInParameter(dbCmd, "@CHEQUE_NO", DbType.String, cheque_no);
                //}

                if (cheque_date.ToString().Equals("") || cheque_date.ToString() == null)
                {
                    db.AddInParameter(dbCmd, "@CHEQUE_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHEQUE_DATE", DbType.DateTime, DateTime.ParseExact(cheque_date.ToString(), "dd/MM/yyyy", null));
                }


                db.AddInParameter(dbCmd, "@BANK_ID", DbType.String, bank_id);
                db.AddInParameter(dbCmd, "@BRANCH", DbType.String, branch);
                db.AddInParameter(dbCmd, "@REMARKS", DbType.String, remarks);
                db.AddInParameter(dbCmd, "@CASH_RECIEPT_NO", DbType.String, receipt_no);

                if (receipt_date.ToString().Equals("") || receipt_date.ToString() == null)
                {
                    db.AddInParameter(dbCmd, "@CASH_RECEIPT_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CASH_RECEIPT_DATE", DbType.DateTime, DateTime.ParseExact(receipt_date.ToString(), "dd/MM/yyyy", null));
                }

                if (ex_rate.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@EX_CHANGE_RATE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EX_CHANGE_RATE", DbType.Decimal, Decimal.Parse(ex_rate));
                }

                if (forex_amount.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FOREX_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FOREX_AMOUNT", DbType.Decimal, Decimal.Parse(forex_amount));
                }

                db.AddInParameter(dbCmd, "@FOREX_CURRENCY", DbType.String, forex_currency);

                db.AddInParameter(dbCmd, "@CUST_REL_SR_NO", DbType.Int32, cust_rel_no);

                db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, comapny_id);

                if (gl_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DateTime.ParseExact(gl_date.ToString(), "dd/MM/yyyy", null));
                }

                db.AddInParameter(dbCmd, "@ON_ACCOUNT_RECEIPT", DbType.Boolean, on_acc_flag);

                db.AddInParameter(dbCmd, "@FLAG", DbType.Int32, flag);

                db.AddInParameter(dbCmd, "@IS_THB_RECEIPT", DbType.Boolean, IS_THB_RECEIPT);

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

        public void update_accounts_entry(int sales_voucher_id, String voucher_no, String gl_code, String invoice_no, String voucher_amount, String voucher_type, String voucher_currency, int employee_id, String narration, int prepared_by, int approved_by, int posted_by, String voucher_status_id, int voucher_detail_id, String sales_forex_amount, String cr_amount, String dr_amount, String payment_mode, String cheque_no, String cheque_date, String bank_id, String branch, String remarks, String receipt_no, String receipt_date, String ex_rate, String forex_amount, String forex_currency, String gl_date, int flag)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_SALES_RECEIPT_VOUCHER_HEADER_DETAILS");

                db.AddInParameter(dbCmd, "@SALES_RECEIPT_VOUCHER_ID", DbType.Int32, sales_voucher_id);
                db.AddInParameter(dbCmd, "@VOUCHER_NO", DbType.String, voucher_no);
                db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, gl_code);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);



                //if (voucher_date.ToString().Equals(""))
                //{
                //    db.AddInParameter(dbCmd, "@VOUCHER_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@VOUCHER_DATE", DbType.DateTime, DateTime.ParseExact(voucher_date.ToString(), "dd/MM/yyyy", null));
                //}

                db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal, Decimal.Parse(voucher_amount));
                db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, voucher_type);
                db.AddInParameter(dbCmd, "@VOUCHER_CURRENCY_ID", DbType.String, voucher_currency);
                db.AddInParameter(dbCmd, "@BRANCH_EMPLOYEE_ID", DbType.Int32, employee_id);

                db.AddInParameter(dbCmd, "@NARRATION", DbType.String, narration);
                db.AddInParameter(dbCmd, "@PREPARED_BY", DbType.Int32, prepared_by);
                db.AddInParameter(dbCmd, "@APPROVED_BY", DbType.Int32, approved_by);
                db.AddInParameter(dbCmd, "@POSTED_BY", DbType.Int32, posted_by);

                db.AddInParameter(dbCmd, "@VOUCHER_STATUS_ID", DbType.String, voucher_status_id);

                db.AddInParameter(dbCmd, "@SALES_RECEIPT_VOUCHER_DETAIL_ID", DbType.Int32, voucher_detail_id);

                if (sales_forex_amount.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@SALES_FOREX_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@SALES_FOREX_AMOUNT", DbType.Decimal, Decimal.Parse(sales_forex_amount));
                }

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

                //if (cheque_no.ToString().Equals("") || cheque_no.ToString() == null)
                //{
                //}
                //else
                //{
                db.AddInParameter(dbCmd, "@CHEQUE_NO", DbType.String, cheque_no);
                //}

                if (cheque_date.ToString().Equals("") || cheque_date.ToString() == null)
                {
                    db.AddInParameter(dbCmd, "@CHEQUE_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHEQUE_DATE", DbType.DateTime, DateTime.ParseExact(cheque_date.ToString(), "dd/MM/yyyy", null));
                }


                db.AddInParameter(dbCmd, "@BANK_ID", DbType.String, bank_id);
                db.AddInParameter(dbCmd, "@BRANCH", DbType.String, branch);
                db.AddInParameter(dbCmd, "@REMARKS", DbType.String, remarks);
                db.AddInParameter(dbCmd, "@CASH_RECIEPT_NO", DbType.String, receipt_no);

                if (receipt_date.ToString().Equals("") || receipt_date.ToString() == null)
                {
                    db.AddInParameter(dbCmd, "@CASH_RECEIPT_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CASH_RECEIPT_DATE", DbType.DateTime, DateTime.ParseExact(receipt_date.ToString(), "dd/MM/yyyy", null));
                }

                if (ex_rate.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@EX_CHANGE_RATE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EX_CHANGE_RATE", DbType.Decimal, Decimal.Parse(ex_rate));
                }

                if (forex_amount.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FOREX_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FOREX_AMOUNT", DbType.Decimal, Decimal.Parse(forex_amount));
                }

                db.AddInParameter(dbCmd, "@FOREX_CURRENCY", DbType.String, forex_currency);

                if (gl_date.ToString().Equals("") || cheque_date.ToString() == null)
                {
                    db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DateTime.ParseExact(gl_date.ToString(), "dd/MM/yyyy", null));
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
    }
}
