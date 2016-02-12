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
   public  class AcoountVouchersStoredProcedure
    {
       public DataSet fetch_voucher_type(String sp_name)
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

       public DataSet fetch_voucher_status(String sp_name)
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

       public DataSet fetch_bank_name(String sp_name)
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

       public DataSet fetch_paymentmode(String sp_name)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       public DataSet fetch_branch(String sp_name, String bank_name)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@BANK_NAME", DbType.String, bank_name);
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       public DataSet fetch_gl_code(String sp_name, String account_grp_name)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@ACCOUNT_GROUP_NAME", DbType.String, account_grp_name);
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

        public DataSet fetch_currency_name(String sp_name)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetch_group_name(String sp_name)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetch_invoice_no(String sp_name, String gl_code)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, gl_code);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetch_invoice_dateials(String sp_name, String invoice_no)
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

            }

            return dsData;
        }

        public DataSet set_gl_code(String sp_name, String cust_id, String flg)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.String, cust_id);
                db.AddInParameter(dbCmd, "@FLAG", DbType.String, flg);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

       public DataSet fetch_conversion_rate()
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_CONVERSION_RATE");
            //   db.AddInParameter(dbCmd, "@CUST_ID", DbType.String, cust_id);
           //    db.AddInParameter(dbCmd, "@FLAG", DbType.String, flg);
               ds = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return ds;
       }

       public void insert_accounts_entry(int account_voucher_id, String gl_code, String invoice_no, String voucher_date, String voucher_type, int employee_id, String narration, int prepared_by, int approved_by, int posted_by, String voucher_status_id, int voucher_detail_id, String cr_amount, String dr_amount, String payment_mode, String cheque_no, String cheque_date, String bank_id, String branch, String remarks, String receipt_no, String ex_rate, String forex_amount, String forex_currency, int flag)
        {
             Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_ACCOUNT_VOUCHER_HEADER");
               // ds=db.ExecuteDataSet(dbCmd);
                db.AddInParameter(dbCmd, "@ACCOUNT_VOUCHER_ID", DbType.Int32, account_voucher_id);
               
                db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, gl_code);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
               


                if (voucher_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@VOUCHER_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@VOUCHER_DATE", DbType.DateTime, DateTime.ParseExact(voucher_date.ToString(), "dd/MM/yyyy", null));
                }

            //    db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal, voucher_amount);
                db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, voucher_type);
           //     db.AddInParameter(dbCmd, "@VOUCHER_CURRENCY_ID", DbType.String, voucher_currency);
                db.AddInParameter(dbCmd, "@BRANCH_EMPLOYEE_ID", DbType.Int32, employee_id);

                db.AddInParameter(dbCmd, "@NARRATION", DbType.String, narration);
                db.AddInParameter(dbCmd, "@PREPARED_BY", DbType.Int32, prepared_by);
                db.AddInParameter(dbCmd, "@APPROVED_BY", DbType.Int32, approved_by);
                db.AddInParameter(dbCmd, "@POSTED_BY", DbType.Int32, posted_by);
                db.AddInParameter(dbCmd, "@VOUCHER_STATUS_ID", DbType.String, voucher_status_id);

                db.AddInParameter(dbCmd, "@ACCOUNT_VOUCHER_DETAILS_ID", DbType.Int32, voucher_detail_id);

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

                if (cheque_date.ToString().Equals("") || cheque_date.ToString()==null )
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


       public void insert_accounts_entry_CONTRA_JOURNAL(int account_voucher_id, String gl_code, String invoice_no, String voucher_date, String voucher_type, int employee_id, String narration, int prepared_by, int approved_by, int posted_by, String voucher_status_id, int voucher_detail_id, String cr_amount, String dr_amount, String payment_mode, String cheque_no, String cheque_date, String bank_id, String branch, String remarks, String receipt_no, String ex_rate, String forex_amount, String forex_currency, int comapny_id, String currency, int flag)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_ACCOUNT_VOUCHER_HEADER");
               // ds=db.ExecuteDataSet(dbCmd);
               db.AddInParameter(dbCmd, "@ACCOUNT_VOUCHER_ID", DbType.Int32, account_voucher_id);

               db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, gl_code);
               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);



               if (voucher_date.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@VOUCHER_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@VOUCHER_DATE", DbType.DateTime, DateTime.ParseExact(voucher_date.ToString(), "dd/MM/yyyy", null));
               }

               //    db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal, voucher_amount);
               db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, voucher_type);
               //     db.AddInParameter(dbCmd, "@VOUCHER_CURRENCY_ID", DbType.String, voucher_currency);
               db.AddInParameter(dbCmd, "@BRANCH_EMPLOYEE_ID", DbType.Int32, employee_id);

               db.AddInParameter(dbCmd, "@NARRATION", DbType.String, narration);
               db.AddInParameter(dbCmd, "@PREPARED_BY", DbType.Int32, prepared_by);
               db.AddInParameter(dbCmd, "@APPROVED_BY", DbType.Int32, approved_by);
               db.AddInParameter(dbCmd, "@POSTED_BY", DbType.Int32, posted_by);
               db.AddInParameter(dbCmd, "@VOUCHER_STATUS_ID", DbType.String, voucher_status_id);

               db.AddInParameter(dbCmd, "@ACCOUNT_VOUCHER_DETAILS_ID", DbType.Int32, voucher_detail_id);

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

               db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, comapny_id);

               db.AddInParameter(dbCmd, "@CURRENCY", DbType.String, currency);
              
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

        public DataSet fetch_account_records(String invoice_no, String voucher_type)
        {
              Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_RECORD_FROM_ACCOUNT");
               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
               db.AddInParameter(dbCmd, "@VOUCHER_TYPE_ID", DbType.String, voucher_type);
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

        public void update_accounts_entry(int account_voucher_id, String gl_code, String invoice_no, String voucher_date, String voucher_type, int employee_id, String narration, int prepared_by, int approved_by, int posted_by, String voucher_status_id, int voucher_detail_id, String cr_amount, String dr_amount, String payment_mode, String cheque_no, String cheque_date, String bank_id, String branch, String remarks, String receipt_no, String ex_rate, String forex_amount, String forex_currency, int seq_no, String gldate, int flag )
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_ACCOUNT_VOUCHER_HEADER");
                // ds=db.ExecuteDataSet(dbCmd);
                db.AddInParameter(dbCmd, "@ACCOUNT_VOUCHER_ID", DbType.Int32, account_voucher_id);

                db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, gl_code);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);



                if (voucher_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@VOUCHER_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@VOUCHER_DATE", DbType.DateTime, DateTime.ParseExact(voucher_date.ToString(), "dd/MM/yyyy", null));
                }

                //    db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal, voucher_amount);
                db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, voucher_type);
                //     db.AddInParameter(dbCmd, "@VOUCHER_CURRENCY_ID", DbType.String, voucher_currency);
                db.AddInParameter(dbCmd, "@BRANCH_EMPLOYEE_ID", DbType.Int32, employee_id);

                db.AddInParameter(dbCmd, "@NARRATION", DbType.String, narration);
                db.AddInParameter(dbCmd, "@PREPARED_BY", DbType.Int32, prepared_by);
                db.AddInParameter(dbCmd, "@APPROVED_BY", DbType.Int32, approved_by);
                db.AddInParameter(dbCmd, "@POSTED_BY", DbType.Int32, posted_by);
                db.AddInParameter(dbCmd, "@VOUCHER_STATUS_ID", DbType.String, voucher_status_id);

                db.AddInParameter(dbCmd, "@ACCOUNT_VOUCHER_DETAILS_ID", DbType.Int32, voucher_detail_id);

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
                db.AddInParameter(dbCmd, "@SEQ_NO", DbType.Int32, seq_no);
                db.AddInParameter(dbCmd, "@FLAG", DbType.Int32, flag);
                if (gldate.ToString().Equals("") || cheque_date.ToString() == null)
                {
                    db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DateTime.ParseExact(gldate.ToString(), "dd/MM/yyyy", null));
                }
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

        public void update_chart_of_account(int cust_id, String flag, String op_balnce, String op_balnce_type, String cl_balance, String cl_balance_type)
        {
              Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_AMOUNT_CHART_OF_ACCOUNT");
                // ds=db.ExecuteDataSet(dbCmd);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, cust_id);

                db.AddInParameter(dbCmd, "@FLAG", DbType.String, flag);
                if (op_balnce.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@OP_BALANCE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@OP_BALANCE", DbType.Decimal, Decimal.Parse(op_balnce));
                }
                db.AddInParameter(dbCmd, "@OP_BALANCE_TYPE_ID", DbType.String, op_balnce_type);

                if (cl_balance.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CL_BALANCE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CL_BALANCE", DbType.Decimal, Decimal.Parse(cl_balance));
                }
                db.AddInParameter(dbCmd, "@CL_BALANCE_TYPE_ID", DbType.String, cl_balance_type);

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

        public DataSet fetch_balance_type()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_BALANCE_TYPE");
                //    db.AddInParameter(dbCmd, "@AGENT_COMPANY_NAME", DbType.String, company_name);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

       //ADD
        public DataSet fetch_hotel_data(String sp_name, String invoice_no, String supplier_name)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
                db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, supplier_name);

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

        public DataSet fetch_fit_package_amount(String sp_name, String gl_code)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, gl_code);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public void update_fit_amount(String glcode, String op_balnce, String op_balnce_type, String cl_balance, String cl_balance_type)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_AMOUNT_FIT_PACKAGE");
                // ds=db.ExecuteDataSet(dbCmd);
                //    db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, cust_id);

                db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, glcode);
                if (op_balnce.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@OP_BALANCE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@OP_BALANCE", DbType.Decimal, Decimal.Parse(op_balnce));
                }
                db.AddInParameter(dbCmd, "@OP_BALANCE_TYPE_ID", DbType.String, op_balnce_type);

                if (cl_balance.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CL_BALANCE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CL_BALANCE", DbType.Decimal, Decimal.Parse(cl_balance));
                }
                db.AddInParameter(dbCmd, "@CL_BALANCE_TYPE_ID", DbType.String, cl_balance_type);

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

        public DataSet fetch_all_gl_code()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_GL_CODE");
                //   db.AddInParameter(dbCmd, "@CUST_ID", DbType.String, cust_id);
                //    db.AddInParameter(dbCmd, "@FLAG", DbType.String, flg);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return ds;
        }

        public DataSet fetch_transfer_package(String sp_name, String invoice_no, String supplier_name)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
                db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, supplier_name);
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

        public DataSet fetch_common_data(String sp_name, String invoice_no)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
                //    db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, supplier_name);

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

        public void insertCancellationFees(string INVOICE_NO, decimal CANCELLATION_FEES)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_CANCELLATION_FEES_FOR_INVOICE");

                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, INVOICE_NO);
                db.AddInParameter(dbCmd, "@CANCELLATION_FEES", DbType.Decimal, CANCELLATION_FEES);

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

        } // Insert cancellation fees in sales invoice header

        public void updateCreditNote(string INVOICE_NO, int VOUCHER_TYPE_ID, decimal VOUCHER_AMOUNT)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_CREDIT_NOTE_MANUALLY");

                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, INVOICE_NO);
                db.AddInParameter(dbCmd, "@VOUCHER_TYPE_ID", DbType.Int32, VOUCHER_TYPE_ID);

                db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal, VOUCHER_AMOUNT);

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

        } // Insert cancellation fees in account voucher header for credit note
    }
}
