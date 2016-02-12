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
     public class PurchaseHeader
    {

         public void InsertPurchaseHeader(String DUE_DATE, String SUPPLIER_TYPE, String SUPPLIER_NAME, String GL_DATE, int ORDER_ID, int ISSUED_BY_COMPANY_ID, String VOUCHER_AMOUNT, String TAX_AMOUNT, String TOTAL_AMOUNT, String NARRATION, String VOUCHER_STATUS, String SALES_INVOICE_NO, String SETTLED_AMOUNT, int FLAG, String CURRANCY, int USER_ID, string ArrivalDate, string DepartureDate, string ServiceDate)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("INSERT_PURCHASE_HEADER");

                 if (DUE_DATE.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@DUE_DATE", DbType.DateTime, DBNull.Value);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@DUE_DATE", DbType.DateTime, DateTime.ParseExact(DUE_DATE.ToString(), "dd/MM/yyyy", null));
                 }

                 db.AddInParameter(dbCmd, "@SUPPLIER_TYPE", DbType.String, SUPPLIER_TYPE);
                 db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, SUPPLIER_NAME);

                 if (GL_DATE.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DBNull.Value);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DateTime.ParseExact(GL_DATE.ToString(), "dd/MM/yyyy", null));
                 }


                 if (ArrivalDate.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@ARRIVAL_DATE", DbType.DateTime, DBNull.Value);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@ARRIVAL_DATE", DbType.DateTime, DateTime.ParseExact(ArrivalDate.ToString(), "dd/MM/yyyy", null));
                 }

                 if (DepartureDate.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@DEPARTURE_DATE", DbType.DateTime, DBNull.Value);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@DEPARTURE_DATE", DbType.DateTime, DateTime.ParseExact(DepartureDate.ToString(), "dd/MM/yyyy", null));
                 }

                 if (ServiceDate.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@SERVICE_DATE", DbType.DateTime, DBNull.Value);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@SERVICE_DATE", DbType.DateTime, DateTime.ParseExact(ServiceDate.ToString(), "dd/MM/yyyy", null));
                 }
                 db.AddInParameter(dbCmd, "@ORDER_ID", DbType.Int32, ORDER_ID);
                 db.AddInParameter(dbCmd, "@ISSUED_BY_COMPANY_ID ", DbType.Int32, ISSUED_BY_COMPANY_ID);

                 if (VOUCHER_AMOUNT.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal, "0.00");
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal, decimal.Parse(VOUCHER_AMOUNT));
                 }

                 if (TAX_AMOUNT.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@TAX_AMOUNT", DbType.Decimal, "0.00");
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@TAX_AMOUNT", DbType.Decimal, decimal.Parse(TAX_AMOUNT));
                 }

                 if (TOTAL_AMOUNT.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, "0.00");
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, decimal.Parse(TOTAL_AMOUNT));
                 }
                 db.AddInParameter(dbCmd, "@NARRATION", DbType.String, NARRATION);  
                 db.AddInParameter(dbCmd, "@VOUCHER_STATUS", DbType.String, VOUCHER_STATUS);
                 db.AddInParameter(dbCmd, "@SALES_INVOICE_NO", DbType.String, SALES_INVOICE_NO);

                 if (SETTLED_AMOUNT.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@SETTLED_AMOUNT", DbType.Decimal, "0.00");
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@SETTLED_AMOUNT", DbType.Decimal, decimal.Parse(SETTLED_AMOUNT));
                 }
                 db.AddInParameter(dbCmd, "@FLAG", DbType.Int32, FLAG);
                 db.AddInParameter(dbCmd, "@CURRANCY", DbType.String, CURRANCY);
                 db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, USER_ID);
                 
                 db.ExecuteNonQuery(dbCmd);
                 
             }

             catch(Exception ex)
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

         public DataSet getInvoiceDescription(String sp_name, String INVOICE_NO, String SUPPLIER_COMPANY_NAME)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand(sp_name);
                 db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, INVOICE_NO);
                 db.AddInParameter(dbCmd, "@SUPPLIER_COMPANY_NAME", DbType.String, SUPPLIER_COMPANY_NAME);
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
             return dsData;
         }

         public DataSet getRemainingBalance(String sp_name, String INVOICE_NO, String SUPPLIER_COMPANY_NAME)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand(sp_name);
                 db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, INVOICE_NO);
                 db.AddInParameter(dbCmd, "@SUPPLIER_COMPANY_NAME", DbType.String, SUPPLIER_COMPANY_NAME);
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
             return dsData;
         }

         public DataSet commonSp(String sp_name)
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

         public DataSet fetchallDatasearch(String sp_name, String invoice_no, String voucher_status, String from_date, String to_date, String sales_invoice, String supplier_type, String supplier_name)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand(sp_name);
                 db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
                 db.AddInParameter(dbCmd, "@VOUCHER_STATUS", DbType.String, voucher_status);

                
                
                 if (from_date.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@FROM_DATE_S", DbType.DateTime, DBNull.Value);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@FROM_DATE_S", DbType.DateTime, DateTime.ParseExact(from_date.ToString(), "dd/MM/yyyy", null));
                 }
                 if (to_date.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@TO_DATE_S", DbType.DateTime, DBNull.Value);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@TO_DATE_S", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
                 }

                 db.AddInParameter(dbCmd, "@SALES_INVOICE", DbType.String, sales_invoice);
                 db.AddInParameter(dbCmd, "@SUPPLIER_TYPE", DbType.String, supplier_type);
                 db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, supplier_name);

                 dsData = db.ExecuteDataSet(dbCmd);
             }
             catch (Exception ex)
             {

             }

             return dsData;
         }

         public DataSet getAllInformationEditMode(String sp_name, String PURCHASE_INVOICE_NO)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand(sp_name);
                 db.AddInParameter(dbCmd, "@PURCHASE_INVOICE_NO", DbType.String, PURCHASE_INVOICE_NO);
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

         public void InsertUpdatePurchaseHeaderonEdit(String DUE_DATE, String SUPPLIER_TYPE, String SUPPLIER_NAME, String GL_DATE, int ORDER_ID, int ISSUED_BY_COMPANY_ID, String VOUCHER_AMOUNT, String TAX_AMOUNT, String TOTAL_AMOUNT, String NARRATION, String VOUCHER_STATUS, String SALES_INVOICE_NO, String SETTLED_AMOUNT, int FLAG, String PURCHASE_INVOICE_NO, int USER_ID, string ArrivalDate, string DepartureDate, string ServiceDate)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_PURCHASE_HEADER_ON_EDIT");

                 if (DUE_DATE.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@DUE_DATE", DbType.DateTime, DBNull.Value);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@DUE_DATE", DbType.DateTime, DateTime.ParseExact(DUE_DATE.ToString(), "dd/MM/yyyy", null));
                 }

                 db.AddInParameter(dbCmd, "@SUPPLIER_TYPE", DbType.String, SUPPLIER_TYPE);
                 db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, SUPPLIER_NAME);

                 if (GL_DATE.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DBNull.Value);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DateTime.ParseExact(GL_DATE.ToString(), "dd/MM/yyyy", null));
                 }

                 db.AddInParameter(dbCmd, "@ORDER_ID", DbType.Int32, ORDER_ID);
                 db.AddInParameter(dbCmd, "@ISSUED_BY_COMPANY_ID ", DbType.Int32, ISSUED_BY_COMPANY_ID);

                 if (VOUCHER_AMOUNT.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal, "0.00");
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal, decimal.Parse(VOUCHER_AMOUNT));
                 }

                 if (TAX_AMOUNT.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@TAX_AMOUNT", DbType.Decimal, "0.00");
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@TAX_AMOUNT", DbType.Decimal, decimal.Parse(TAX_AMOUNT));
                 }

                 if (TOTAL_AMOUNT.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, "0.00");
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, decimal.Parse(TOTAL_AMOUNT));
                 }
                 db.AddInParameter(dbCmd, "@NARRATION", DbType.String, NARRATION);
                 db.AddInParameter(dbCmd, "@VOUCHER_STATUS", DbType.String, VOUCHER_STATUS);
                 db.AddInParameter(dbCmd, "@SALES_INVOICE_NO", DbType.String, SALES_INVOICE_NO);

                 if (SETTLED_AMOUNT.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@SETTLED_AMOUNT", DbType.Decimal, "0.00");
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@SETTLED_AMOUNT", DbType.Decimal, decimal.Parse(SETTLED_AMOUNT));
                 }

                 if (ArrivalDate.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@ARRIVAL_DATE", DbType.DateTime, DBNull.Value);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@ARRIVAL_DATE", DbType.DateTime, DateTime.ParseExact(ArrivalDate.ToString(), "dd/MM/yyyy", null));
                 }

                 if (DepartureDate.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@DEPARTURE_DATE", DbType.DateTime, DBNull.Value);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@DEPARTURE_DATE", DbType.DateTime, DateTime.ParseExact(DepartureDate.ToString(), "dd/MM/yyyy", null));
                 }

                 if (ServiceDate.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@SERVICE_DATE", DbType.DateTime, DBNull.Value);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@SERVICE_DATE", DbType.DateTime, DateTime.ParseExact(ServiceDate.ToString(), "dd/MM/yyyy", null));
                 }


                 db.AddInParameter(dbCmd, "@FLAG", DbType.Int32, FLAG);
                 db.AddInParameter(dbCmd, "@PURCHASE_INVOICE_NO", DbType.String, PURCHASE_INVOICE_NO);
                 db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, USER_ID);
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

         public DataSet updatePurchaseInvoiceStatus(String sp_name, String SALES_INVOICE_NO, String SUPPLIER_COMPANY_NAME)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand(sp_name);
                 db.AddInParameter(dbCmd, "@SALES_INVOICE_NO", DbType.String, SALES_INVOICE_NO);
                 db.AddInParameter(dbCmd, "@SUPPLIER_COMPANY_NAME", DbType.String, SUPPLIER_COMPANY_NAME);
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

         public void insertAccountEntry(int account_voucher_id, String gl_code, String invoice_no, String voucher_date, String voucher_type, int employee_id, String narration, int prepared_by, int approved_by, int posted_by, String voucher_status_id, int voucher_detail_id, String cr_amount, String dr_amount,  int comapny_id, String currency, int flag)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet ds = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("INSERT_ACCOUNT_VOUCHER_ENTRY_ON_PURCHASE");
                 
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

                
                 db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, voucher_type);
              
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

         public DataSet fetch_all_gl_code()
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet ds = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("FETCH_FIT_TOUR_CODE");

                
                 ds = db.ExecuteDataSet(dbCmd);
             }
             catch (Exception ex)
             {

             }

             return ds;
         }

         public DataSet getGlDesription(String sp_name, String SUPPLIER_COMPANY_NAME)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand(sp_name);
                 db.AddInParameter(dbCmd, "@SUPPLIER_COMPANY_NAME", DbType.String, SUPPLIER_COMPANY_NAME);
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

         public DataSet getInvoiceNo(String sp_name, String SUPPLIER_TYPE, String SUPPLIER_COMPANY_NAME)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand(sp_name);
                 db.AddInParameter(dbCmd, "@SUPPLIER_TYPE", DbType.String, SUPPLIER_TYPE);
                 db.AddInParameter(dbCmd, "@SUPPLIER_COMPANY_NAME", DbType.String, SUPPLIER_COMPANY_NAME);
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
             return dsData;
         }

         public DataSet getAccountVouchersDeatils(String sp_name, String PURCHASE_INVOICE_NO, String VOUCHER_TYPE)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand(sp_name);
                 db.AddInParameter(dbCmd, "@PURCHASE_INVOICE_NO", DbType.String, PURCHASE_INVOICE_NO);
                 db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, VOUCHER_TYPE);
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
             return dsData;
         }

         public void updateAccountVoucherAmount(int account_voucher_id, int account_details_id, String cr_amount, String dr_amount, String voucher_amount, String gl_Date, String Narration, int flag)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet ds = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("UPDATE_ACCOUNT_VOUCHER_AMOUNT");
                 db.AddInParameter(dbCmd, "@ACCOUNT_VOUCHER_ID", DbType.Int32, account_voucher_id);

                
                 db.AddInParameter(dbCmd, "@ACCOUNT_VOUCHER_DETAILS_ID", DbType.Int32, account_details_id);

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
                 if (voucher_amount.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal, Decimal.Parse(voucher_amount));
                 }

                 if (gl_Date.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DBNull.Value);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DateTime.ParseExact(gl_Date.ToString(), "dd/MM/yyyy", null));
                 }
                    db.AddInParameter(dbCmd, "@NARRATION", DbType.String, Narration);
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
         }


         public DataSet getSalesInvoiceSettledAmount(String sp_name, String INVOICE_NO, String SUPPLIER_COMPANY_NAME)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand(sp_name);
                 db.AddInParameter(dbCmd, "@SALES_INVOICE_NO", DbType.String, INVOICE_NO);
                 db.AddInParameter(dbCmd, "@SUPPLIER_COMPANY_NAME", DbType.String, SUPPLIER_COMPANY_NAME);
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
             return dsData;
         }

         public DataSet getInvoiceTotalReceiptAmount(String sp_name, String INVOICE_NO)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand(sp_name);
                 db.AddInParameter(dbCmd, "@SALES_INVOICE_NO", DbType.String, INVOICE_NO);
                 
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
             return dsData;
         }

         /*Emp Percentage validations*/
         public DataSet getEmpPercentage(String sp_name, int EMP_ID)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand(sp_name);
                 db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, EMP_ID);

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
             return dsData;

         }

         public DataSet getInvoiceAmountSettled(String sp_name, String SALES_INVOICE_NO)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand(sp_name);
                 db.AddInParameter(dbCmd, "@SALES_INVOICE_NO", DbType.String, SALES_INVOICE_NO);

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
             return dsData;

         }
    }
}
