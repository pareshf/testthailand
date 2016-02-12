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
   public  class VouchersStoredProcedure
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

       public void insert_accounts_entry(int sales_voucher_id, String voucher_no ,String gl_code, String invoice_no, String voucher_amount, String voucher_type, String voucher_currency, int employee_id, String narration, int prepared_by, int approved_by, int posted_by, String voucher_status_id, int voucher_detail_id, String sales_forex_amount, String cr_amount, String dr_amount, String payment_mode, String cheque_no, String cheque_date, String bank_id, String branch, String remarks, String receipt_no, String receipt_date,  String ex_rate, String forex_amount, String forex_currency, int cust_rel_no, int comapny_id, String gl_date, Boolean on_acc_flag, int flag)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_SALES_RECEIPT_VOUCHER_HEADER");

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

               db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal,Decimal.Parse (voucher_amount));
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

       public DataSet get_invoice_amount(String sp_name, String invoice_no)
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

       public DataSet get_invoice_no_agent_vise(String sp_name, int emp_id)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@USERID", DbType.String, emp_id);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       public DataSet get_cust_rel_sr_no(String sp_name, String  company_name)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@AGENT_COMPANY_NAME", DbType.String, company_name);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       public DataSet get_records_for_edit(String sp_name, String voucher_name)
       {
            Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@VOUCHER_NO", DbType.String, voucher_name);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }
       
       public void update_accounts_entry(int sales_voucher_id, String voucher_no ,  String gl_code, String invoice_no, String voucher_amount, String voucher_type, String voucher_currency, int employee_id, String narration, int prepared_by, int approved_by, int posted_by, String voucher_status_id, int voucher_detail_id, String sales_forex_amount, String cr_amount, String dr_amount, String payment_mode, String cheque_no, String cheque_date, String bank_id, String branch, String remarks, String receipt_no, String receipt_date,  String ex_rate, String forex_amount, String forex_currency, String gl_date, int flag)
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

               db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal,Decimal.Parse (voucher_amount));
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

       // GENERATE VOUCHER NO FOR ALL VOUCHERS

       // COMMON 
       public DataSet get_max_voucher_no(String sp_name, String voucher_type)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, voucher_type);
               //    db.AddInParameter(dbCmd, "@AGENT_COMPANY_NAME", DbType.String, company_name);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       public DataSet get_voucher_no_for_check(String sp_name, String invoice_no, String voucher_type)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
               db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, voucher_type);

               //    db.AddInParameter(dbCmd, "@AGENT_COMPANY_NAME", DbType.String, company_name);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       public DataSet get_voucher_no_for_paymnet_check(String sp_name, String seq_no)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@VOUCHER_NO", DbType.String, seq_no);
               // db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, voucher_type);

               //    db.AddInParameter(dbCmd, "@AGENT_COMPANY_NAME", DbType.String, company_name);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       // SALES AND PURCHASE VOUCHER NO
       public void update_accounts_voucher_no(String invoice_no, String vouhcer_status, String vocher_type, String voucher_no)
       {

           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_FINAL_VOUCHER_NO_FOR_VOUCHER");

               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
               db.AddInParameter(dbCmd, "@VOUCHER_STATUS", DbType.String, vouhcer_status);
               db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, vocher_type);

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

       

       // PAYMENT VOUCHER NO
       public void insert_accounts_voucher_no_purchase_payment( String vouhcer_status, String seq_no, String voucher_no)
       {

           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_VOUCHER_NO_FOR_PURCHASE_PAYMENT");

              
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

       // RECEIPT VOUCHER NO
       public void insert_accounts_voucher_no_sales_receipt(String vouhcer_status, String seq_no, String voucher_no)
       {

           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_VOUCHER_NO_FOR_SALES_RECEIPT");


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

       // EXPENCE VOUCHER NO

       public void insert_accounts_voucher_no_expense_voucher(String voucher_no, String status, String invoice_no)
       {

           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_EXPENCE_HEADER_VOUCHER_NO");


               db.AddInParameter(dbCmd, "@VOUCHER_NO", DbType.String, voucher_no);
               db.AddInParameter(dbCmd, "@STATUS_ID", DbType.String, status);

               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);


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

       // CONTRA AND JOURNAL VOUCHER NO
       public void update_accounts_voucher_no_contra_journal(String invoice_no, String vouhcer_status, String vocher_type, String voucher_no)
       {

           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_VOUCHER_NO_FOR_VOUCHER_CONTRA_JOURNAL");

               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
               db.AddInParameter(dbCmd, "@VOUCHER_STATUS", DbType.String, vouhcer_status);
               db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, vocher_type);

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

       //SALARY VOUCHER NO
       public void update_accounts_voucher_no_salary(String vouhcer_status, String seq_no, String voucher_no)
       {

           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_VOUCHER_NO_FOR_SALARY");

               
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

       // NEW FOR RECEIPT VOUCHER 
       public DataSet get_seq_no_from_invoice_no(String sp_name, String invoice_no)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
               //    db.AddInParameter(dbCmd, "@AGENT_COMPANY_NAME", DbType.String, company_name);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       /********************************************* FOR    Currency Exchange Variation   *********************************/
       public DataSet get_invoices_amount(String sp_name, String invoice_no)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
               //    db.AddInParameter(dbCmd, "@AGENT_COMPANY_NAME", DbType.String, company_name);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       public void update_profi_gl_code(Decimal cl_balance, String balance_type)
       {

           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_CURRENCY_EXCHANGE_CODE");

               db.AddInParameter(dbCmd, "@CL_BALANCE", DbType.Decimal, cl_balance);
               db.AddInParameter(dbCmd, "@CL_TYPE", DbType.String, balance_type);
               


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

       // ON ACCOUNT VOUCHER NO
       public DataSet get_seq_no_for_voucher_no_generation(String sp_name, int seq_no)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@SEQ_NO", DbType.Int32, seq_no);
               //    db.AddInParameter(dbCmd, "@AGENT_COMPANY_NAME", DbType.String, company_name);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       // CHANGES AFTER 30TH MAY AFTER DISCUSSED WITH SIR 
       public DataSet get_invoice_status(String sp_name, String seq_no)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, seq_no);
               //    db.AddInParameter(dbCmd, "@AGENT_COMPANY_NAME", DbType.String, company_name);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       public DataSet get_invoice_left(String sp_name, int USER_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@USERID", DbType.String, USER_ID);
               //    db.AddInParameter(dbCmd, "@AGENT_COMPANY_NAME", DbType.String, company_name);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }


       public void update_profi_amount_in_invoice_table(String PROFIT_LOSS_AMOUNT, String SALES_INOVICE_NO, String PROFIT_LOSS_FLAG)
       {

           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_PROFIT_LOSS_INVOICE");

               db.AddInParameter(dbCmd, "@SALES_INOVICE_NO", DbType.String, SALES_INOVICE_NO);

               if (PROFIT_LOSS_AMOUNT.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@PROFIT_LOSS_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@PROFIT_LOSS_AMOUNT", DbType.Decimal, Decimal.Parse(PROFIT_LOSS_AMOUNT));
               }
               db.AddInParameter(dbCmd, "@PROFIT_LOSS_FLAG", DbType.String, PROFIT_LOSS_FLAG);
               

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
    }
}
