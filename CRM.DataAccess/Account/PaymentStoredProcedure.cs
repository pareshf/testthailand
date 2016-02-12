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
  public   class PaymentStoredProcedure
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

      public DataSet fetch_supplier(String sp_name, String supplier_type, String supplier_type_name)
      {
          Database db = null;
          DbCommand dbCmd = null;
          DataSet dsData = null;

          try
          {
              db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
              dbCmd = db.GetStoredProcCommand(sp_name);
              db.AddInParameter(dbCmd, "@SUPPLIER_TYPE", DbType.String, supplier_type);
              db.AddInParameter(dbCmd, "@SUPPLIER_TYPE_NAME", DbType.String, supplier_type_name);
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

      public DataSet fetch_supplier_names(String sp_name, String supplier_type)
      {
          Database db = null;
          DbCommand dbCmd = null;
          DataSet dsData = null;

          try
          {
              db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
              dbCmd = db.GetStoredProcCommand(sp_name);
              db.AddInParameter(dbCmd, "@SUPPLIER_TYPE", DbType.String, supplier_type);
            //  db.AddInParameter(dbCmd, "@SUPPLIER_TYPE_NAME", DbType.String, supplier_type_name);
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
      /*************************************************** STILL LEFT TO CHANGE****************************************************************/

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

      public void insert_accounts_entry(int sales_voucher_id, String voucher_no, String gl_code, String invoice_no, String voucher_amount, String voucher_type, String voucher_currency, int employee_id, String narration, int prepared_by, int approved_by, int posted_by, String voucher_status_id, int voucher_detail_id,  String cr_amount, String dr_amount, String payment_mode,  String remarks, String receipt_no, String supplier_type, String supplier_name, String sales_invoice_no, String payment_date,  int comapny_id, String gl_date, Boolean on_account_payment, int flag)
      {
          Database db = null;
          DbCommand dbCmd = null;
          DataSet ds = null;

          try
          {
              db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
              dbCmd = db.GetStoredProcCommand("INSERT_PURCHASE_PAYMENT_VOUCHER_HEADER");

              db.AddInParameter(dbCmd, "@PURCHASE_PAYMENT_VOUCHER_HEADER_ID", DbType.Int32, sales_voucher_id);
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

              /****************************   *****************************/

              db.AddInParameter(dbCmd, "@PURCHASE_PAYMENT_VOUCHER_DETAIL_ID", DbType.Int32, voucher_detail_id);

              //if (sales_forex_amount.ToString().Equals(""))
              //{
              //    db.AddInParameter(dbCmd, "@SALES_FOREX_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
              //}
              //else
              //{
              //    db.AddInParameter(dbCmd, "@SALES_FOREX_AMOUNT", DbType.Decimal, Decimal.Parse(sales_forex_amount));
              //}

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
             // db.AddInParameter(dbCmd, "@CHEQUE_NO", DbType.String, cheque_no);
              //}

              //if (cheque_date.ToString().Equals("") || cheque_date.ToString() == null)
              //{
              //    db.AddInParameter(dbCmd, "@CHEQUE_DATE", DbType.DateTime, DBNull.Value);
              //}
              //else
              //{
              //    db.AddInParameter(dbCmd, "@CHEQUE_DATE", DbType.DateTime, DateTime.ParseExact(cheque_date.ToString(), "dd/MM/yyyy", null));
              //}


              //db.AddInParameter(dbCmd, "@BANK_ID", DbType.String, bank_id);
              //db.AddInParameter(dbCmd, "@BRANCH", DbType.String, branch);
              db.AddInParameter(dbCmd, "@REMARKS", DbType.String, remarks);
              db.AddInParameter(dbCmd, "@CASH_RECIEPT_NO", DbType.String, receipt_no);

              //if (receipt_date.ToString().Equals("") || receipt_date.ToString() == null)
              //{
              //    db.AddInParameter(dbCmd, "@CASH_RECEIPT_DATE", DbType.DateTime, DBNull.Value);
              //}
              //else
              //{
              //    db.AddInParameter(dbCmd, "@CASH_RECEIPT_DATE", DbType.DateTime, DateTime.ParseExact(receipt_date.ToString(), "dd/MM/yyyy", null));
              //}

              //if (ex_rate.ToString().Equals(""))
              //{
              //    db.AddInParameter(dbCmd, "@EX_CHANGE_RATE", DbType.Decimal, Decimal.Parse("0"));
              //}
              //else
              //{
              //    db.AddInParameter(dbCmd, "@EX_CHANGE_RATE", DbType.Decimal, Decimal.Parse(ex_rate));
              //}

              //if (forex_amount.ToString().Equals(""))
              //{
              //    db.AddInParameter(dbCmd, "@FOREX_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
              //}
              //else
              //{
              //    db.AddInParameter(dbCmd, "@FOREX_AMOUNT", DbType.Decimal, Decimal.Parse(forex_amount));
              //}

              db.AddInParameter(dbCmd, "@SUPPLIER_TYPE", DbType.String, supplier_type);
              db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, supplier_name);

              db.AddInParameter(dbCmd, "@SALES_INVOICE_NO", DbType.String, sales_invoice_no);

              if (payment_date.ToString().Equals(""))
              {
                  db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime, DBNull.Value);
              }
              else
              {
                  db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime, DateTime.ParseExact(payment_date.ToString(), "dd/MM/yyyy", null));
              }

              db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, comapny_id);

              if (gl_date.ToString().Equals(""))
              {
                  db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DBNull.Value);
              }
              else
              {
                  db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DateTime.ParseExact(gl_date.ToString(), "dd/MM/yyyy", null));
              }

              db.AddInParameter(dbCmd, "@ON_ACCOUNT_PAYMENT", DbType.Boolean, on_account_payment);

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

      public DataSet get_cust_rel_sr_no(String sp_name, String company_name)
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

      public void update_accounts_entry(int sales_voucher_id, String voucher_no, String gl_code, String invoice_no, String voucher_amount, String voucher_type, String voucher_currency, int employee_id, String narration, int prepared_by, int approved_by, int posted_by, String voucher_status_id, int voucher_detail_id, String cr_amount, String dr_amount, String payment_mode, String remarks, String receipt_no, String supplier_type, String supplier_name, String sales_invoice_no, String gl_date, int flag)
      {
          Database db = null;
          DbCommand dbCmd = null;
          DataSet ds = null;

          try
          {
              db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
              dbCmd = db.GetStoredProcCommand("UPDATE_PURCHASE_PAYMENT_VOUCHER_HEADER");

              db.AddInParameter(dbCmd, "@PURCHASE_PAYMENT_VOUCHER_HEADER_ID", DbType.Int32, sales_voucher_id);
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

              /****************************   *****************************/

              db.AddInParameter(dbCmd, "@PURCHASE_PAYMENT_VOUCHER_DETAIL_ID", DbType.Int32, voucher_detail_id);

              //if (sales_forex_amount.ToString().Equals(""))
              //{
              //    db.AddInParameter(dbCmd, "@SALES_FOREX_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
              //}
              //else
              //{
              //    db.AddInParameter(dbCmd, "@SALES_FOREX_AMOUNT", DbType.Decimal, Decimal.Parse(sales_forex_amount));
              //}

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
              // db.AddInParameter(dbCmd, "@CHEQUE_NO", DbType.String, cheque_no);
              //}

              //if (cheque_date.ToString().Equals("") || cheque_date.ToString() == null)
              //{
              //    db.AddInParameter(dbCmd, "@CHEQUE_DATE", DbType.DateTime, DBNull.Value);
              //}
              //else
              //{
              //    db.AddInParameter(dbCmd, "@CHEQUE_DATE", DbType.DateTime, DateTime.ParseExact(cheque_date.ToString(), "dd/MM/yyyy", null));
              //}


              //db.AddInParameter(dbCmd, "@BANK_ID", DbType.String, bank_id);
              //db.AddInParameter(dbCmd, "@BRANCH", DbType.String, branch);
              db.AddInParameter(dbCmd, "@REMARKS", DbType.String, remarks);
              db.AddInParameter(dbCmd, "@CASH_RECIEPT_NO", DbType.String, receipt_no);

              //if (receipt_date.ToString().Equals("") || receipt_date.ToString() == null)
              //{
              //    db.AddInParameter(dbCmd, "@CASH_RECEIPT_DATE", DbType.DateTime, DBNull.Value);
              //}
              //else
              //{
              //    db.AddInParameter(dbCmd, "@CASH_RECEIPT_DATE", DbType.DateTime, DateTime.ParseExact(receipt_date.ToString(), "dd/MM/yyyy", null));
              //}

              //if (ex_rate.ToString().Equals(""))
              //{
              //    db.AddInParameter(dbCmd, "@EX_CHANGE_RATE", DbType.Decimal, Decimal.Parse("0"));
              //}
              //else
              //{
              //    db.AddInParameter(dbCmd, "@EX_CHANGE_RATE", DbType.Decimal, Decimal.Parse(ex_rate));
              //}

              //if (forex_amount.ToString().Equals(""))
              //{
              //    db.AddInParameter(dbCmd, "@FOREX_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
              //}
              //else
              //{
              //    db.AddInParameter(dbCmd, "@FOREX_AMOUNT", DbType.Decimal, Decimal.Parse(forex_amount));
              //}

              db.AddInParameter(dbCmd, "@SUPPLIER_TYPE", DbType.String, supplier_type);
              db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, supplier_name);

              db.AddInParameter(dbCmd, "@SALES_INVOICE_NO", DbType.String, sales_invoice_no);

              if (gl_date.ToString().Equals(""))
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

      /*****************************************PART PAYMENTS SPS************************************************************************/

      public DataSet fetch_invoice_description(String invoice_no)
      {
          Database db = null;
          DbCommand dbCmd = null;
          DataSet dsData = null;

          try
          {
              db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
              dbCmd = db.GetStoredProcCommand("GET_DETIALS_OF_PURCHASE_INVOICE");
              db.AddInParameter(dbCmd, "@PURCHASE_INOIVICE_NO", DbType.String, invoice_no);

              dsData = db.ExecuteDataSet(dbCmd);
          }
          catch (Exception ex)
          {

          }

          return dsData;
      }

      public void updaet_due_date(String invoice_no, String due_date)
      {
          Database db = null;
          DbCommand dbCmd = null;
          DataSet dsData = null;

          try
          {
              db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
              dbCmd = db.GetStoredProcCommand("UPDATE_DUE_DATE");
              db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
              if (due_date.ToString().Equals(""))
              {
                  db.AddInParameter(dbCmd, "@DUE_DATE", DbType.DateTime, DBNull.Value);
              }
              else
              {
                  db.AddInParameter(dbCmd, "@DUE_DATE", DbType.DateTime, DateTime.ParseExact(due_date.ToString(), "dd/MM/yyyy", null));
              }
              dsData = db.ExecuteDataSet(dbCmd);
          }

          catch
          {

          }

          finally
          {
              DALHelper.Destroy(ref dbCmd);
          }
      }

      /** NEW ACCOUNT SPS **/

      public DataSet getSupplierInvoice(String sp_name, String supplierCompanyName, String supplierType)
      {
          Database db = null;
          DbCommand dbCmd = null;
          DataSet dsData = null;

          try
          {
              db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
              dbCmd = db.GetStoredProcCommand(sp_name);
              db.AddInParameter(dbCmd, "@SUPPLIER_COMPANY_NAME", DbType.String, supplierCompanyName);
              db.AddInParameter(dbCmd, "@SUPPLIER_TYPE_NAME", DbType.String, supplierType);
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

      public DataSet getPurchaseInvoiceDetails(String sp_name, String invoice_no)
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

      /** BANK FESS **/
      public DataSet inserBankFees(String sp_name, String BankFees)
      {
          Database db = null;
          DbCommand dbCmd = null;
          DataSet dsData = null;

          try
          {
              db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
              dbCmd = db.GetStoredProcCommand(sp_name);

              if (BankFees.ToString().Equals(""))
              {
                  db.AddInParameter(dbCmd, "@BANK_CHARGE", DbType.Decimal, Decimal.Parse("0"));
              }
              else
              {
                  db.AddInParameter(dbCmd, "@BANK_CHARGE", DbType.Decimal, Decimal.Parse(BankFees));
              }
              dsData = db.ExecuteDataSet(dbCmd);
          }
          catch (Exception ex)
          {

          }

          return dsData;
      }

      public DataSet updatBankFees(String sp_name, String BankFees, int seqNo)
      {
          Database db = null;
          DbCommand dbCmd = null;
          DataSet dsData = null;

          try
          {
              db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
              dbCmd = db.GetStoredProcCommand(sp_name);

              if (BankFees.ToString().Equals(""))
              {
                  db.AddInParameter(dbCmd, "@BANK_CHARGE", DbType.Decimal, Decimal.Parse("0"));
              }
              else
              {
                  db.AddInParameter(dbCmd, "@BANK_CHARGE", DbType.Decimal, Decimal.Parse(BankFees));

              }
              db.AddInParameter(dbCmd, "@SEQ_NO", DbType.Int32, seqNo);
              dsData = db.ExecuteDataSet(dbCmd);
          }
          catch (Exception ex)
          {

          }

          return dsData;
      }
    }
}
