using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.GIT
{
   public class AgentInvoiceGitDA
    {
        public DataSet GetPaymentMode(String payment_mode)
        {

            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(payment_mode);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet GetCurrency(String currency)
        {

            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(currency);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet GetOrderStatus(String Status)
        {

            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(Status);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet FetchDataForInvoice(String currency, String s, String p)
        {

            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(currency);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(s));
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, p);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetchismanual(String sp_name, String parm1)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(parm1));
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet GetEmpid(String currency, String s)
        {

            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(currency);
                db.AddInParameter(dbCmd, "@AGENT_COMPANY_NAME", DbType.String, s);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataTable objfetchusername(String sp_name, String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, int.Parse(param));
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
            return ds.Tables[0];
        }

        public DataSet fetch_credit_limit(int customer_emp_no)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_CREDIT_LIMIT");
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, customer_emp_no);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataTable fetchorderstatusname(String sp_name, String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@order_id", DbType.Int32, int.Parse(param));
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
            return ds.Tables[0];
        }

        public DataSet fetch_total_invoice(int quote_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_TOTAL_INVOICE_AMOUNT");
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quote_id);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataTable FETCH_TOUR_ID_FROM_QUOTE_ID(string QUT)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_TOUR_ID_FROM_QUOTE_ID_GIT");
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(QUT));
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
            return dsData.Tables[0];
        }

        public DataSet insert_sales_git_fare_tour(int quote_id, int tour_id, String name, String from_date, String to_date, String no_of_nights, String total_amount, int no_of_adults, int no_of_child, int no_of_cwb, int no_of_cnb, int no_of_infant, String created_by, String order_status)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_DATA_IN_GIT_TOUR_QUOTE_FOR_INVOICE");


                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quote_id);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tour_id);

                if (from_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@START_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@START_DATE", DbType.DateTime, DateTime.ParseExact(from_date.ToString(), "dd/MM/yyyy", null));
                }
                if (to_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@END_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@END_DATE", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
                }

                db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, no_of_nights);
                db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, no_of_adults);
                db.AddInParameter(dbCmd, "@GROUPNAME", DbType.String, name);
                db.AddInParameter(dbCmd, "@NO_OF_CHILD", DbType.Int32, no_of_child);
                db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32, no_of_cwb);
                db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32, no_of_cnb);
                db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, no_of_infant);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, int.Parse(created_by));
                db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, order_status);

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

        public DataSet insert_sales_invoice_header(String SALES_ID, int quote_id, int order_by_id, int agent_id, String from_date, String to_date, int no_of_nights, String amount, decimal tax_amount, String total_amount, bool posted_flag, int no_of_adults, int no_of_child, int no_of_cwb, int no_of_cnb, int no_of_infant, String order_staus, String payment_mode, String ref_no, int company_id, String currency,String GL_DATE)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_SALES_INVOICE_HEADER_GIT");
                db.AddInParameter(dbCmd, "@SALES_INVOICE_ID", DbType.Int32, int.Parse(SALES_ID));
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quote_id);
                db.AddInParameter(dbCmd, "@ORDER_BY_ID", DbType.Int32, order_by_id);
                db.AddInParameter(dbCmd, "@ISSUED_BY_COMPANY_ID", DbType.Int32, agent_id);
                if (from_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@PERIOD_STAY_FROM", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PERIOD_STAY_FROM", DbType.DateTime, DateTime.ParseExact(from_date.ToString(), "dd/MM/yyyy", null));
                }
                if (to_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@PERIOD_STAY_TO", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PERIOD_STAY_TO", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, no_of_nights);
                db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, amount);
                db.AddInParameter(dbCmd, "@TOTAL_TAX_AMOUNT", DbType.Decimal, tax_amount);
                db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, total_amount);
                db.AddInParameter(dbCmd, "@POSTED_FLAG", DbType.Boolean, posted_flag);
                db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, no_of_adults);
                db.AddInParameter(dbCmd, "@NO_OF_CHILD", DbType.Int32, no_of_child);
                db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32, no_of_cwb);
                db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32, no_of_cnb);
                db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, no_of_infant);
                db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, order_staus);
                db.AddInParameter(dbCmd, "@PAYMENT_MODE", DbType.String, payment_mode);
                db.AddInParameter(dbCmd, "@COMPANY_BOOKING_REFERENCE_NO", DbType.String, ref_no);
                db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, company_id);
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.String, currency);
                if (GL_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DateTime.ParseExact(GL_DATE.ToString(), "dd/MM/yyyy", null));
                } 
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
       
        public DataSet insert_sales_invoice_details(int invoice_DETAIL_id,string description,string currency,string noofper,string amount)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_SALES_INVOICE_DETAILS_GIT");
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID", DbType.Int32, invoice_DETAIL_id);
                db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, description);
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.String, currency);
                if (amount.Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, decimal.Parse(amount));
                }
                if (noofper.Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_PERSON", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_PERSON", DbType.Int32, int.Parse(noofper));
                }
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

        #region EDIT CURRENT USABLE IN CUST_CUSTOMER_MASTER
        
         public void edit_current_usable(int emp_id, decimal inovoice_amount)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("EDIT_CUURENT_USABLE_CUSTOMER_MASTER");
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, emp_id);
                db.AddInParameter(dbCmd, "@CURRENT_USABLE_CREDIT_LIMIT", DbType.Decimal, inovoice_amount);

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
       
         public void edit_current_usable_MINUS(int emp_id, decimal inovoice_amount)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("EDIT_CUURENT_USABLE_CUSTOMER_MASTER_MINUS");
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, emp_id);
                db.AddInParameter(dbCmd, "@CURRENT_USABLE_CREDIT_LIMIT", DbType.Decimal, inovoice_amount);

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
        
        #endregion


    }
}
