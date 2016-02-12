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
   public  class PurchaseVoucherStoredProcedure
    {
       public DataSet fetch_supplier_type(String sp_name)
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

       public DataSet fetch_supplier(String sp_name, String supplier_type)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@SUPPLIER_TYPE", DbType.String, supplier_type);
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

       public DataSet fetch_invoice_no(String sp_name, String supplier_type)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, supplier_type);
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
             
       public void insert_purchase_entry(int purchase_id, String supplier_name, String invoice_no, int user_id, String due_date, String amount, String tax_amount, String total_amount, String currency, String paymentmode, int posted_by, String supplier_type , int purchase_details_id, String description, int no_of_adult, int no_of_cwb, int no_of_cnb, int no_of_infant, String period_stay_from, String period_stay_to, int no_of_night, int no_of_single_room, int no_of_double_room, int no_of_triple_room, String room_type, int tp_detail_id, String tp_flag, String ss_name, String ss_date, String ss_flag, String details_amount, int flag)
       {
            Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_PURCHASE_HEADER_DETAILS");

               db.AddInParameter(dbCmd, "@PURCHASE_INVOICE_ID", DbType.Int32, purchase_id);
               db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, supplier_name);
               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
               db.AddInParameter(dbCmd, "@ORDER_BY_ID", DbType.Int32, user_id);

               if (due_date.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DUE_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DUE_DATE", DbType.DateTime, DateTime.ParseExact(due_date.ToString(), "dd/MM/yyyy", null));
               }

               if (amount.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, Decimal.Parse("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, Decimal.Parse(amount));
               }

               if (tax_amount.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@TOTAL_TAX_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TOTAL_TAX_AMOUNT", DbType.Decimal, Decimal.Parse(tax_amount));
               }

               if (total_amount.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, Decimal.Parse(total_amount));
               }

               db.AddInParameter(dbCmd, "@CURRENCY", DbType.String, currency);
               db.AddInParameter(dbCmd, "@PAYMENT_MODE", DbType.String, paymentmode);
               db.AddInParameter(dbCmd, "@POSTED_BY", DbType.Int32, posted_by);
               db.AddInParameter(dbCmd, "@SUPPLIER_TYPE", DbType.String, supplier_type);
               
               // DETAILS TABLES

               db.AddInParameter(dbCmd, "@PURCHASE_INVOICE_DETAILS_ID", DbType.Int32, purchase_details_id);
               db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION", DbType.String, description);

               db.AddInParameter(dbCmd, "@NO_OF_ADULT", DbType.Int32, no_of_adult );
               db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32, no_of_cwb );
               db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32, no_of_cnb );
               db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, no_of_infant );

               if (period_stay_from .ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@PERIOD_STAY_FROM", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@PERIOD_STAY_FROM", DbType.DateTime, DateTime.ParseExact(period_stay_from.ToString(), "dd/MM/yyyy", null));
               }

               if (period_stay_to.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@PERIOD_STAY_TO", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@PERIOD_STAY_TO", DbType.DateTime, DateTime.ParseExact(period_stay_to.ToString(), "dd/MM/yyyy", null));
               }

               db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, no_of_night );
               db.AddInParameter(dbCmd, "@NO_OF_ROOM_SINGLE", DbType.Int32, no_of_single_room );
               db.AddInParameter(dbCmd, "@NO_OF_ROOM_DOUBLE", DbType.Int32, no_of_double_room );
               db.AddInParameter(dbCmd, "@NO_OF_ROOM_TRIPLE", DbType.Int32, no_of_triple_room );

               db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, room_type );

               db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_DETAIL_ID", DbType.Int32, tp_detail_id );
               db.AddInParameter(dbCmd, "@TRANSFER_SIC_PVT_FLAG", DbType.String, tp_flag );

               db.AddInParameter(dbCmd, "@SIGHT_SEEING_NAME", DbType.String, ss_name);
               if (ss_date .ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@SIGHT_SEEING_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@SIGHT_SEEING_DATE", DbType.DateTime, DateTime.ParseExact(ss_date.ToString(), "dd/MM/yyyy", null));
               }
               db.AddInParameter(dbCmd, "@SIGHT_SEEING_SIC_PVT_FLAG", DbType.String, ss_flag );

               if (details_amount.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DETAILS_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DETAILS_AMOUNT", DbType.Decimal, Decimal.Parse(details_amount));
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
       }

       public DataSet fetch_record_while_edit(String sp_name, int purchase_invoice_id)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@PURCHASE_INVOICE_ID", DbType.String, purchase_invoice_id);
               //    db.AddInParameter(dbCmd, "@FLAG", DbType.String, flg);
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

       public void update_purchase_entry(int purchase_id, String supplier_name, String invoice_no, int user_id, String due_date, String amount, String tax_amount, String total_amount, String currency, String paymentmode, int posted_by, String supplier_type, int purchase_details_id, String description, int no_of_adult, int no_of_cwb, int no_of_cnb, int no_of_infant, String period_stay_from, String period_stay_to, int no_of_night, int no_of_single_room, int no_of_double_room, int no_of_triple_room, String room_type, int tp_detail_id, String tp_flag, String ss_name, String ss_date, String ss_flag, String details_amount, String gl_Date, int flag)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_PURCHASE_HEADER_DETAILS");

               db.AddInParameter(dbCmd, "@PURCHASE_INVOICE_ID", DbType.Int32, purchase_id);
               db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, supplier_name);
               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
               db.AddInParameter(dbCmd, "@ORDER_BY_ID", DbType.Int32, user_id);

               if (due_date.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DUE_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DUE_DATE", DbType.DateTime, DateTime.ParseExact(due_date.ToString(), "dd/MM/yyyy", null));
               }

               if (gl_Date.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DateTime.ParseExact(gl_Date.ToString(), "dd/MM/yyyy", null));
               }
               

               if (amount.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, Decimal.Parse("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, Decimal.Parse(amount));
               }

               if (tax_amount.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@TOTAL_TAX_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TOTAL_TAX_AMOUNT", DbType.Decimal, Decimal.Parse(tax_amount));
               }

               if (total_amount.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, Decimal.Parse(total_amount));
               }

               db.AddInParameter(dbCmd, "@CURRENCY", DbType.String, currency);
               db.AddInParameter(dbCmd, "@PAYMENT_MODE", DbType.String, paymentmode);
               db.AddInParameter(dbCmd, "@POSTED_BY", DbType.Int32, posted_by);
               db.AddInParameter(dbCmd, "@SUPPLIER_TYPE", DbType.String, supplier_type);

               // DETAILS TABLES

               db.AddInParameter(dbCmd, "@PURCHASE_INVOICE_DETAILS_ID", DbType.Int32, purchase_details_id);
               db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION", DbType.String, description);

               db.AddInParameter(dbCmd, "@NO_OF_ADULT", DbType.Int32, no_of_adult);
               db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32, no_of_cwb);
               db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32, no_of_cnb);
               db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, no_of_infant);

               if (period_stay_from.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@PERIOD_STAY_FROM", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@PERIOD_STAY_FROM", DbType.DateTime, DateTime.ParseExact(period_stay_from.ToString(), "dd/MM/yyyy", null));
               }

               if (period_stay_to.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@PERIOD_STAY_TO", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@PERIOD_STAY_TO", DbType.DateTime, DateTime.ParseExact(period_stay_to.ToString(), "dd/MM/yyyy", null));
               }

               db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, no_of_night);
               db.AddInParameter(dbCmd, "@NO_OF_ROOM_SINGLE", DbType.Int32, no_of_single_room);
               db.AddInParameter(dbCmd, "@NO_OF_ROOM_DOUBLE", DbType.Int32, no_of_double_room);
               db.AddInParameter(dbCmd, "@NO_OF_ROOM_TRIPLE", DbType.Int32, no_of_triple_room);

               db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, room_type);

               db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_DETAIL_ID", DbType.Int32, tp_detail_id);
               db.AddInParameter(dbCmd, "@TRANSFER_SIC_PVT_FLAG", DbType.String, tp_flag);

               db.AddInParameter(dbCmd, "@SIGHT_SEEING_NAME", DbType.String, ss_name);
               if (ss_date.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@SIGHT_SEEING_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@SIGHT_SEEING_DATE", DbType.DateTime, DateTime.ParseExact(ss_date.ToString(), "dd/MM/yyyy", null));
               }
               db.AddInParameter(dbCmd, "@SIGHT_SEEING_SIC_PVT_FLAG", DbType.String, ss_flag);

               if (details_amount.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DETAILS_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DETAILS_AMOUNT", DbType.Decimal, Decimal.Parse(details_amount));
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
       }

       public void update_account_voucher_amount(int account_voucher_id, String invoice_no, int seq_no, int account_details_id, String cr_amount, String dr_amount, String voucher_amount, String gl_Date, int flag)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_PURCHASE_VOUCHER_AMOUNT");
               db.AddInParameter(dbCmd, "@ACCOUNT_VOUCHER_ID", DbType.Int32, account_voucher_id);

               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);

               db.AddInParameter(dbCmd, "@SEQ_NO", DbType.Int32, seq_no);
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

       public void update_additional_service_cart_amount(String amount, int id)
       {
            Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_ADDITIONLA_CART_DETAILS_AMOUNT");
               if (amount.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@PURCHASE_INVOICE_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@PURCHASE_INVOICE_AMOUNT", DbType.Decimal, Decimal.Parse(amount));
               }
               
               db.AddInParameter(dbCmd, "@ADDITIONAL_SERVICE_CART_ID", DbType.Int32, id);


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
