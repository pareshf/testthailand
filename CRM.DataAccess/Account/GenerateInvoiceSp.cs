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
    public class GenerateInvoiceSp
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

        public DataTable insert_sales_invoice_header(int invoic, int quote_id, int order_by_id, int agent_id, String from_date, String to_date, String no_of_nights, decimal amount, decimal tax_amount, decimal total_amount, int no_of_adults, int no_of_child, int no_of_cwb, int no_of_cnb, int no_of_infant, String order_staus, String payment_mode, String ref_no, String GL_DATE, string ORDER_PLACED_BY, string PERSON_EMAIL)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_SALES_INVOICE_HEADER_NEW_MANUALLY");
                db.AddInParameter(dbCmd, "@SALES_INVOICE_ID", DbType.Int32, invoic);

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
                if (GL_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DateTime.ParseExact(GL_DATE.ToString(), "dd/MM/yyyy", null));
                }
                
                db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, no_of_nights);

                db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, amount);
                db.AddInParameter(dbCmd, "@TOTAL_TAX_AMOUNT", DbType.Decimal, tax_amount);
                db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, total_amount);

                //db.AddInParameter(dbCmd, "@CURRNCY", DbType.Boolean, posted_flag);

                db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, no_of_adults);

                db.AddInParameter(dbCmd, "@NO_OF_CHILD", DbType.Int32, no_of_child);
                db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32, no_of_cwb);
                db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32, no_of_cwb);
                db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, no_of_infant);

                db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, order_staus);
                db.AddInParameter(dbCmd, "@PAYMENT_MODE", DbType.String, payment_mode);
                db.AddInParameter(dbCmd, "@COMPANY_BOOKING_REFERENCE_NO", DbType.String, ref_no);
                //new
                db.AddInParameter(dbCmd, "@ORDER_PLACED_BY", DbType.String, ORDER_PLACED_BY);
                db.AddInParameter(dbCmd, "@PERSON_EMAIL", DbType.String, PERSON_EMAIL);

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

        public DataSet insert_sales_invoice_details(int invoic, int id1, int id2, int id3, int id4, int id5, int id6, int id7, int id8, int id9, int id10, int id11, int id12, int id13, int id14, int id15,
            String a1, String a2, String a3, String a4, String a5, String a6, String a7, String a8, String a9, String a10, String a11, String a12, String a13, String a14, String a15, 
            String in1, String in2, String in3, String in4, String in5, String in6, String in7, String in8, String in9, String in10,String in11,String in12,String in13,String in14,String in15,String Currency, 
            String unit1,
            String unit2, String unit3, String unit4, String unit5, String unit6, String unit7, String unit8, String unit9, String unit10, String unit11, String unit12, String unit13, String unit14, String unit15
            )
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_SALES_INVOICE_DETAILS_MANUALLY");
                db.AddInParameter(dbCmd, "@SALES_INVOICE_ID", DbType.Int32, invoic);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID1", DbType.Int32,id1 );
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID2", DbType.Int32, id2);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID3", DbType.Int32, id3);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID4", DbType.Int32, id4);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID5", DbType.Int32, id5);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID6", DbType.Int32, id6);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID7", DbType.Int32, id7);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID8", DbType.Int32, id8);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID9", DbType.Int32, id9);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID10", DbType.Int32, id10);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID11", DbType.Int32, id11);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID12", DbType.Int32, id12);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID13", DbType.Int32, id13);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID14", DbType.Int32, id14);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID15", DbType.Int32, id15);

                if (a1.ToString().Equals(""))
                {
                db.AddInParameter(dbCmd, "@AMOUNT1", DbType.Decimal,Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@AMOUNT1", DbType.Decimal,Convert.ToDecimal(a1));
                }
                if (a2.ToString().Equals(""))
                {
                db.AddInParameter(dbCmd, "@AMOUNT2", DbType.Decimal,Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@AMOUNT2", DbType.Decimal, Convert.ToDecimal(a2));
                }
                if (a3.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT3", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@AMOUNT3", DbType.Decimal, Convert.ToDecimal(a3));
                }
                if (a4.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT4", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@AMOUNT4", DbType.Decimal, Convert.ToDecimal(a4));
                }
                if (a5.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT5", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else 
                {
                    db.AddInParameter(dbCmd, "@AMOUNT5", DbType.Decimal, Convert.ToDecimal(a5));
                }
                if (a6.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT6", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@AMOUNT6", DbType.Decimal, Convert.ToDecimal(a6));
                }
                if (a7.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT7", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else 
                {
                    db.AddInParameter(dbCmd, "@AMOUNT7", DbType.Decimal, Convert.ToDecimal(a7));
                }
                if (a8.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT8", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else 
                {
                    db.AddInParameter(dbCmd, "@AMOUNT8", DbType.Decimal, Convert.ToDecimal(a8));
                }
                if (a9.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT9", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@AMOUNT9", DbType.Decimal, Convert.ToDecimal(a9));
                }

                if (a10.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT10", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else 
                {
                    db.AddInParameter(dbCmd, "@AMOUNT10", DbType.Decimal, Convert.ToDecimal(a10));
                }
                if (a11.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT11", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@AMOUNT11", DbType.Decimal, Convert.ToDecimal(a11));
                }
                if (a12.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT12", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@AMOUNT12", DbType.Decimal, Convert.ToDecimal(a12));
                }
                if (a13.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT13", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@AMOUNT13", DbType.Decimal, Convert.ToDecimal(a13));
                }
                if (a14.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT14", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@AMOUNT14", DbType.Decimal, Convert.ToDecimal(a14));
                }
                if (a15.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT15", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@AMOUNT15", DbType.Decimal, Convert.ToDecimal(a15));
                }
                db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION1", DbType.String, in1);
                db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION2", DbType.String, in2);
                db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION3", DbType.String, in3);
                db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION4", DbType.String, in4);
                db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION5", DbType.String, in5);
                db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION6", DbType.String, in6);
                db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION7", DbType.String, in7);
                db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION8", DbType.String, in8);
                db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION9", DbType.String, in9);
                db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION10", DbType.String, in10);
                db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION11", DbType.String, in11);
                db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION12", DbType.String, in12);
                db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION13", DbType.String, in13);
                db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION14", DbType.String, in14);
                db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION15", DbType.String, in15);
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.String, Currency);

                if (unit1.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@UNIT1", DbType.Int32, int.Parse("1"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@UNIT1", DbType.Int32, int.Parse(unit1));
                }

                if (unit2.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@UNIT2", DbType.Int32, int.Parse("1"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@UNIT2", DbType.Int32, int.Parse(unit2));
                }

                if (unit3.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@UNIT3", DbType.Int32, int.Parse("1"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@UNIT3", DbType.Int32, int.Parse(unit3));
                }

                if (unit4.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@UNIT4", DbType.Int32, int.Parse("1"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@UNIT4", DbType.Int32, int.Parse(unit4));
                }

                if (unit5.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@UNIT5", DbType.Int32, int.Parse("1"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@UNIT5", DbType.Int32, int.Parse(unit5));
                }

                if (unit6.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@UNIT6", DbType.Int32, int.Parse("1"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@UNIT6", DbType.Int32, int.Parse(unit6));
                }

                if (unit7.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@UNIT7", DbType.Int32, int.Parse("1"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@UNIT7", DbType.Int32, int.Parse(unit7));
                }

                if (unit8.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@UNIT8", DbType.Int32, int.Parse("1"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@UNIT8", DbType.Int32, int.Parse(unit8));
                }

                if (unit9.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@UNIT9", DbType.Int32, int.Parse("1"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@UNIT9", DbType.Int32, int.Parse(unit9));
                }

                if (unit10.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@UNIT10", DbType.Int32, int.Parse("1"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@UNIT10", DbType.Int32, int.Parse(unit10));
                }

                if (unit11.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@UNIT11", DbType.Int32, int.Parse("1"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@UNIT11", DbType.Int32, int.Parse(unit11));
                }

                if (unit12.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@UNIT12", DbType.Int32, int.Parse("1"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@UNIT12", DbType.Int32, int.Parse(unit12));
                }

                if (unit13.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@UNIT13", DbType.Int32, int.Parse("1"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@UNIT13", DbType.Int32, int.Parse(unit13));
                }

                if (unit14.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@UNIT14", DbType.Int32, int.Parse("1"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@UNIT14", DbType.Int32, int.Parse(unit14));
                }

                if (unit15.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@UNIT15", DbType.Int32, int.Parse("1"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@UNIT15", DbType.Int32, int.Parse(unit15));
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
      
        public DataSet GetEmpid(String currency, String s)
        {

            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(currency);
                db.AddInParameter(dbCmd, "@AGENT_COMPANY_NAME", DbType.String,s);
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

        public DataSet fetchallData(String sp_name)
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

            }

            return dsData;
        }
        public DataSet fetchallData_login(String sp_name,String parm1)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@USERID", DbType.Int32, int.Parse(parm1));
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        public DataSet fetchallDatasearch(String sp_name, String CUST_ID, String QID, String INVOICE, String param5, String param6, String param7)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                if (param5 == "0")
                {
                    param5 = "";
                }
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
               // db.AddInParameter(dbCmd, "@USERID", DbType.Int32, int.Parse(param1));
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, int.Parse(CUST_ID));
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(QID));
                //db.AddInParameter(dbCmd, "@AGENT_NAME_S", DbType.String, AGENT);
                //db.AddInParameter(dbCmd, "@CLIENT_NAME_S", DbType.String, param3);
                //db.AddInParameter(dbCmd, "@TOUR_NAME_S", DbType.String, param4);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, INVOICE);
                if (param6.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@PERIOD_STAY_FROM", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PERIOD_STAY_FROM", DbType.DateTime, DateTime.ParseExact(param6.ToString(), "dd/MM/yyyy", null));
                }
                if (param7.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@PERIOD_STAY_TO", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PERIOD_STAY_TO", DbType.DateTime, DateTime.ParseExact(param7.ToString(), "dd/MM/yyyy", null));
                }
                if (param5.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@INVOICE_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@INVOICE_DATE", DbType.DateTime, DateTime.ParseExact(param5.ToString(), "dd/MM/yyyy", null));
                }

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetchallDatasearch_login(String sp_name, String QID, String INVOICE, String param5, String param6, String param7, String parm1)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                if (param5 == "0")
                {
                    param5 = "";
                }
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(QID));
                db.AddInParameter(dbCmd, "@USERID", DbType.Int32, int.Parse(parm1));
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, INVOICE);
                if (param6.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@PERIOD_STAY_FROM", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PERIOD_STAY_FROM", DbType.DateTime, DateTime.ParseExact(param6.ToString(), "dd/MM/yyyy", null));
                }
                if (param7.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@PERIOD_STAY_TO", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PERIOD_STAY_TO", DbType.DateTime, DateTime.ParseExact(param7.ToString(), "dd/MM/yyyy", null));
                }
                if (param5.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@INVOICE_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@INVOICE_DATE", DbType.DateTime, DateTime.ParseExact(param5.ToString(), "dd/MM/yyyy", null));
                }

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public void insert_sales_voucher_header(int accid,int detailid,String sp_name,String AGENT, String INVOICE, String eid, String CR, String DR ,int flag)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@ACCOUNT_VOUCHER_ID", DbType.Int32, accid);
                db.AddInParameter(dbCmd, "@GLCODE", DbType.String, AGENT);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, INVOICE);
                db.AddInParameter(dbCmd, "@BRANCH_EMPLOYEE_ID", DbType.Int32, int.Parse(eid));
                db.AddInParameter(dbCmd, "@PREPARED_BY", DbType.Int32, int.Parse(eid));
                db.AddInParameter(dbCmd, "@APPROVED_BY", DbType.Int32, int.Parse(eid));
                db.AddInParameter(dbCmd, "@POSTED_BY", DbType.Int32, int.Parse(eid));
                db.AddInParameter(dbCmd, "@ACCOUNT_VOUCHER_DETAILS_ID", DbType.Int32, detailid);
                db.AddInParameter(dbCmd, "@CR_AMOUNT", DbType.Decimal, Convert.ToDecimal(CR));
                db.AddInParameter(dbCmd, "@DR_AMOUNT", DbType.Decimal, Convert.ToDecimal(DR));

                db.AddInParameter(dbCmd, "@FLAG", DbType.Int32, flag);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

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


        #region sachin 12-03-2012

        public DataSet insert_sales_fare_tour(int quote_id, int tour_id, int SINGLE, int DOUBLE, int TRIPLE, int CWB, int CNB, String name, String from_date, String to_date, String no_of_nights, String total_amount, int no_of_adults, int no_of_child, int no_of_cwb, int no_of_cnb, int no_of_infant, String created_by, String order_status)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_DATA_IN_TOUR_QUOTE_FOR_INVOICE");


                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quote_id);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tour_id);

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

                if (total_amount.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, Convert.ToDecimal(total_amount));
                }

                db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, no_of_adults);
                db.AddInParameter(dbCmd, "@CLINTNAME", DbType.String, name);
                db.AddInParameter(dbCmd, "@NO_OF_CHILD", DbType.Int32, no_of_child);
                db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32, no_of_cwb);
                db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32, no_of_cwb);
                db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, no_of_infant);
                db.AddInParameter(dbCmd, "@SINGEL_SHARE", DbType.Int32, SINGLE);
                db.AddInParameter(dbCmd, "@DOUBLE_SHARE", DbType.Int32, DOUBLE);
                db.AddInParameter(dbCmd, "@TRIPLE_SHARE", DbType.Int32, TRIPLE);
                db.AddInParameter(dbCmd, "@CWB", DbType.Int32, CWB);
                db.AddInParameter(dbCmd, "@CNB", DbType.Int32, CNB);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, int.Parse(created_by));
                db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String , order_status);

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

        // FOR VOUCHER STATUS TRASH
        public DataSet fetch_purchase_voucher(String sp_name, String invoice_no)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_NO", DbType.String, invoice_no);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public void update_voucher_status_account_header(String sp_name, String voucher_status, String purchase_invoice_no, int voucher_type_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@VOCUHER_STATUS", DbType.String, voucher_status);
                db.AddInParameter(dbCmd, "@PURCHASE_INVOICE_NO", DbType.String, purchase_invoice_no);
                db.AddInParameter(dbCmd, "@VOUCHER_TYPE_ID", DbType.Int32, voucher_type_id);
                //UPDATE_VOUCHER_STATUS_ON_TRASH

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

        }

        public void update_sales_status_account_header(String sp_name, String voucher_status, String purchase_invoice_no)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@VOCUHER_STATUS", DbType.String, voucher_status);
                db.AddInParameter(dbCmd, "@PURCHASE_INVOICE_NO", DbType.String, purchase_invoice_no);
                //  db.AddInParameter(dbCmd, "@VOUCHER_TYPE_ID", DbType.Int32, voucher_type_id);


                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

        }

         public void update_quote_status_quote_master(String sp_name, int quote_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.String, quote_id);
               
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

        }

       

        // FOR DISCOUNT AMOUNT
        public void update_discount(String sp_name, String invoice_no, String  total_amount, String discount)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_NO", DbType.String, invoice_no);
                if (total_amount.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, total_amount);
                }

                if (discount.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DISCOUNT_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DISCOUNT_AMOUNT", DbType.Decimal, discount);
                }


                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

        }

        public void update_discount_tour_quote(String sp_name, int quote_id, int tour_id, String discount, decimal quoted_cost)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.String, quote_id);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Decimal, tour_id);

                if (discount.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DISCOUNT_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DISCOUNT_AMOUNT", DbType.Decimal, discount);
                }

                db.AddInParameter(dbCmd, "@TOTAL_QUOTED_COST", DbType.Decimal, quoted_cost);
                

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

        }

        #region 22-06-2012

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

        public DataSet FetchDataForManualVoucher(String currency, String quote_id, String p)
        {

            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(currency);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(quote_id));
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, p);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet FetchDataForHotelManual(String sp_name, String parm1)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, parm1);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        #endregion
        
        
        // COPY BOOKING

        public DataSet fetch_all_data_Quote_id(int quote_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_FOR_COPY_BOOKINGS");
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.String, quote_id);
           
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return ds;
        }


        #region QUOTE MASTER
        public DataSet INSERT_TOUR_QUOTE_COPY(
int CURRANCY_ID	, 
String TOTAL_QUOTED_ADULT	,
String TOTAL_QUOTED_CWB	,
String TOTAL_QUOTED_CNB	,
String TOTAL_QUOTED_INFANT	,
String TOTAL_QUOTED_COST ,

int ORDER_STATUS  ,

String CANCELLATION_FEES	,

int CREATE_BY	,
//String CREATE_DATE	,
String  MODIFY_BY	, 
//String MODIFY_DATE	 ,
String TOTAL_ADULT_COST_ON_SINGLE_SHARE	,
String TOTAL_ADULT_COST_ON_DOUBLE_SHARE	,
String TOTAL_ADULT_COST_ON_TRIPLE_SHARE	,
String NO_OF_PERSON_ON_SINGLE_SHARE	,
String NO_OF_PERSON_ON_DOUBLE_SHARE	,
String NO_OF_PERSON_ON_TRIPLE_SHARE	,
String AGENT_RECONFIRMATION_DATE	,
             
String AUTHORISATION_NO	,
String NO_OF_PERSON_ON_CHILD_WITH_BED,
String NO_OF_PERSON_ON_CHILD_WITH_NO_BED,
String DISCOUNT_AMOUNT )
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("COPY_INSERT_TOUR_QUOTE_MASTER");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32 , 0);
                db.AddInParameter(dbCmd, "@CURRANCY_ID", DbType.Int32, CURRANCY_ID);

                if (TOTAL_QUOTED_ADULT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOTAL_QUOTED_ADULT", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOTAL_QUOTED_ADULT", DbType.Decimal, Decimal.Parse(TOTAL_QUOTED_ADULT));
                }

                if (TOTAL_QUOTED_CWB.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOTAL_QUOTED_CWB", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOTAL_QUOTED_CWB", DbType.Decimal, Decimal.Parse(TOTAL_QUOTED_CWB));
                }

                if (TOTAL_QUOTED_CNB.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOTAL_QUOTED_CNB", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOTAL_QUOTED_CNB", DbType.Decimal, Decimal.Parse(TOTAL_QUOTED_CNB));
                }

                if (TOTAL_QUOTED_INFANT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOTAL_QUOTED_INFANT", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOTAL_QUOTED_INFANT", DbType.Decimal, Decimal.Parse(TOTAL_QUOTED_INFANT));
                }

                if (TOTAL_QUOTED_COST.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOTAL_QUOTED_COST", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOTAL_QUOTED_COST", DbType.Decimal, Decimal.Parse(TOTAL_QUOTED_COST));
                }


                db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.Int32, ORDER_STATUS);

                if (CANCELLATION_FEES.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CANCELLATION_FEES", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CANCELLATION_FEES", DbType.Decimal, Decimal.Parse(CANCELLATION_FEES));
                }

                db.AddInParameter(dbCmd, "@CREATE_BY", DbType.Int32, CREATE_BY);

                //if (CREATE_DATE.ToString().Equals(""))
                //{
                //    db.AddInParameter(dbCmd, "@CREATE_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@CREATE_DATE", DbType.DateTime, DateTime.ParseExact(CREATE_DATE.ToString(), "dd/MM/yyyy", null));
                //}

                if (MODIFY_BY.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@MODIFY_BY", DbType.Int32, int.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@MODIFY_BY", DbType.Int32, int.Parse(MODIFY_BY));
                }

                //if (MODIFY_DATE.ToString().Equals(""))
                //{
                //    db.AddInParameter(dbCmd, "@MODIFY_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@MODIFY_DATE", DbType.DateTime, DateTime.ParseExact(MODIFY_DATE.ToString(), "dd/MM/yyyy", null));
                //}

                 if (TOTAL_ADULT_COST_ON_SINGLE_SHARE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOTAL_ADULT_COST_ON_SINGLE_SHARE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOTAL_ADULT_COST_ON_SINGLE_SHARE", DbType.Decimal, Decimal.Parse(TOTAL_ADULT_COST_ON_SINGLE_SHARE));
                }

                if (TOTAL_ADULT_COST_ON_DOUBLE_SHARE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOTAL_ADULT_COST_ON_DOUBLE_SHARE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOTAL_ADULT_COST_ON_DOUBLE_SHARE", DbType.Decimal, Decimal.Parse(TOTAL_ADULT_COST_ON_DOUBLE_SHARE));
                }

                   if (TOTAL_ADULT_COST_ON_TRIPLE_SHARE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOTAL_ADULT_COST_ON_TRIPLE_SHARE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOTAL_ADULT_COST_ON_TRIPLE_SHARE", DbType.Decimal, Decimal.Parse(TOTAL_ADULT_COST_ON_TRIPLE_SHARE));
                }

                  if (NO_OF_PERSON_ON_SINGLE_SHARE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_PERSON_ON_SINGLE_SHARE", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_PERSON_ON_SINGLE_SHARE", DbType.Int32, Int32.Parse(NO_OF_PERSON_ON_SINGLE_SHARE));
                }

                 if (NO_OF_PERSON_ON_DOUBLE_SHARE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_PERSON_ON_DOUBLE_SHARE", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_PERSON_ON_DOUBLE_SHARE", DbType.Int32, Int32.Parse(NO_OF_PERSON_ON_DOUBLE_SHARE));
                }


                 if (NO_OF_PERSON_ON_TRIPLE_SHARE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_PERSON_ON_TRIPLE_SHARE", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_PERSON_ON_TRIPLE_SHARE", DbType.Int32, Int32.Parse(NO_OF_PERSON_ON_TRIPLE_SHARE));
                }


         if (AGENT_RECONFIRMATION_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AGENT_RECONFIRMATION_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@AGENT_RECONFIRMATION_DATE", DbType.DateTime, DateTime.ParseExact(AGENT_RECONFIRMATION_DATE.ToString(), "dd/MM/yyyy", null));
                }

                db.AddInParameter(dbCmd, "@AUTHORISATION_NO", DbType.String , AUTHORISATION_NO);
             
                 if (NO_OF_PERSON_ON_CHILD_WITH_BED.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_PERSON_ON_CHILD_WITH_BED", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_PERSON_ON_CHILD_WITH_BED", DbType.Int32, Int32.Parse(NO_OF_PERSON_ON_CHILD_WITH_BED));
                }

                  if (NO_OF_PERSON_ON_CHILD_WITH_NO_BED.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_PERSON_ON_CHILD_WITH_NO_BED", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_PERSON_ON_CHILD_WITH_NO_BED", DbType.Int32, Int32.Parse(NO_OF_PERSON_ON_CHILD_WITH_NO_BED));
                }
  if (DISCOUNT_AMOUNT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DISCOUNT_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DISCOUNT_AMOUNT", DbType.Decimal, Decimal.Parse(DISCOUNT_AMOUNT));
                }


                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return ds;
        }
        #endregion

        #region FARE TOUR MASTER
        public DataSet insert_FARE_TOUR_MASTER(

String TOUR_SHORT_NAME,
String TOUR_SUB_TYPE_ID,
String TOUR_FROM_DATE,
String TOUR_TO_DATE,
String NO_OF_DAYS,
String NO_OF_NIGHTS,
String CREATE_BY,
//String CREATE_DATE	,
String TOUR_ITENARY_TYPE_ID,
String TOUR_TYPE_ID,
String NO_OF_ADULTS,
String NO_OF_CHILD,
String NO_OF_CWB,
String NO_OF_CNB,
String NO_OF_INFANT,

String ARRIVAL_TIME,
String DEPARTURE_TIME,
String ARRIVAL_FLIGHT,
String DEPARTURE_FLIGHT,

String CLIENT_NAME,
String REMARKS,
String FIT_PACKAGE_ID,
Boolean  MY_FAVOURITE_PACKAGE,
String CLIENT_SURNAME,
String CLIENT_TITLE,
String PACKAGE_FLAG_ID)
            
            
            
            
            
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("COPY_INSERT_FARE_TOUR_MASTER");

                db.AddInParameter(dbCmd, "@TOUR_SHORT_NAME", DbType.String, TOUR_SHORT_NAME);

                if (TOUR_SUB_TYPE_ID.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOUR_SUB_TYPE_ID", DbType.Int32 , Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOUR_SUB_TYPE_ID", DbType.Int32, Decimal.Parse(TOUR_SUB_TYPE_ID));
                }


                if (TOUR_FROM_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOUR_FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOUR_FROM_DATE", DbType.DateTime, DateTime.ParseExact(TOUR_FROM_DATE.ToString(), "dd/MM/yyyy", null));
                }

                if (TOUR_TO_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOUR_TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOUR_TO_DATE", DbType.DateTime, DateTime.ParseExact(TOUR_TO_DATE.ToString(), "dd/MM/yyyy", null));
                }

                if (NO_OF_DAYS.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_DAYS", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_DAYS", DbType.Int32, NO_OF_DAYS);
                }

                if (NO_OF_NIGHTS.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, NO_OF_NIGHTS);
                }

                // if (CANCELLATION_FEES.ToString().Equals(""))
                //{
                //    db.AddInParameter(dbCmd, "@CANCELLATION_FEES", DbType.Decimal, Decimal.Parse("0"));
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@CANCELLATION_FEES", DbType.Decimal, Decimal.Parse(CANCELLATION_FEES));
                //}
              
  db.AddInParameter(dbCmd, "@CREATE_BY", DbType.Int32, CREATE_BY);
             //   db.AddInParameter(dbCmd, "@CREATE_DATE", DbType.DateTime, DateTime.ParseExact(CREATE_DATE.ToString(), "dd/MM/yyyy", null));
                 if (TOUR_ITENARY_TYPE_ID.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOUR_ITENARY_TYPE_ID", DbType.Int32 , int.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOUR_ITENARY_TYPE_ID", DbType.Int32, int.Parse(TOUR_ITENARY_TYPE_ID));
                }

                 if (TOUR_TYPE_ID.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, int.Parse("0"));
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, int.Parse(TOUR_TYPE_ID));
                 }


                if (NO_OF_ADULTS.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32 , int.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, int.Parse(NO_OF_ADULTS));
                }

                if (NO_OF_CHILD.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CHILD", DbType.Int32 , int.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CHILD", DbType.Int32, int.Parse(NO_OF_CHILD));
                }

                  if (NO_OF_CWB.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32 , int.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32, int.Parse(NO_OF_CWB));
                }

                if (NO_OF_CNB.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32 , int.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32, int.Parse(NO_OF_CNB));
                }

                 if (NO_OF_INFANT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32 , int.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, int.Parse(NO_OF_INFANT));
                }


              // db.AddInParameter(dbCmd, "@CREATE_DATE", DbType.DateTime, DateTime.ParseExact(CREATE_DATE.ToString(), "dd/MM/yyyy", null));

                 if (ARRIVAL_TIME.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, DBNull.Value);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, DateTime.Parse(ARRIVAL_TIME.ToString()));
                 }

                 if (DEPARTURE_TIME.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@DEPARTURE_TIME", DbType.DateTime, DBNull.Value);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@DEPARTURE_TIME", DbType.DateTime, DateTime.Parse(DEPARTURE_TIME.ToString()));
                 }
                  db.AddInParameter(dbCmd, "@ARRIVAL_FLIGHT", DbType.String, ARRIVAL_FLIGHT);

                   db.AddInParameter(dbCmd, "@DEPARTURE_FLIGHT", DbType.String, DEPARTURE_FLIGHT);

                db.AddInParameter(dbCmd, "@CLIENT_NAME", DbType.String, CLIENT_NAME);

                db.AddInParameter(dbCmd, "@REMARKS", DbType.String, REMARKS);

                if (FIT_PACKAGE_ID.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FIT_PACKAGE_ID", DbType.Int32 , int.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FIT_PACKAGE_ID", DbType.Int32, int.Parse(FIT_PACKAGE_ID));
                }
                db.AddInParameter(dbCmd, "@MY_FAVOURITE_PACKAGE", DbType.Boolean, MY_FAVOURITE_PACKAGE);

                db.AddInParameter(dbCmd, "@CLIENT_SURNAME", DbType.String, CLIENT_SURNAME);
       //if (CLIENT_TITLE.ToString().Equals(""))
       //         {
       //             db.AddInParameter(dbCmd, "@CLIENT_TITLE", DbType.Int32 , int.Parse("0"));
       //         }
       //         else
       //         {
       //             db.AddInParameter(dbCmd, "@CLIENT_TITLE", DbType.Int32, int.Parse(CLIENT_TITLE));
       //         }
                db.AddInParameter(dbCmd, "@CLIENT_TITLE", DbType.String, CLIENT_TITLE);

                if (PACKAGE_FLAG_ID.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@PACKAGE_FLAG_ID", DbType.Int32, int.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PACKAGE_FLAG_ID", DbType.Int32, int.Parse(PACKAGE_FLAG_ID));
                }

               

             

                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return ds;
        }
        #endregion

        public  void update_tour_id(int quote_id, int tour_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_TOUR_ID");
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.String, quote_id);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.String, tour_id);

                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            
        }


        #region SERVICE CART COPY
        public void COPY_SERVICE_CART(String TRANSFER_SIGHT_SEEING_PACKAGE_ID, String TRANSFER_SIGHT_SEEING_PACKAGE_FLAG,
            
            String DATE,
            String TIME,
            String ORDER_STATUS,
            String CREATED_BY,
            String NO_OF_MEALS,
            int QUOTE_ID,
            String SIC_PVT_FLAG	,
String TRANSFER_PACKAGE_DETAIL_ID,
String PAYMENT_DUE_DATE	,
String  CANCELLATION_FEE	,
String SERVICE_VOUCHER_NO	,
Boolean  PACKAGE_FLAG	,
String ADULT_PVT_RATE	,
String ADULT_SIC_RATE	,
String CHILD_PVT_RATE	,
String CHILD_SIC_RATE	
            
            
            )
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SERVICE_CART_DETAIL_FOR_COPY_TOUR");
                db.AddInParameter(dbCmd, "@SERVICE_CART_ID", DbType.Int32 , 0);

                if (TRANSFER_SIGHT_SEEING_PACKAGE_ID.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TRANSFER_SIGHT_SEEING_PACKAGE_ID", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TRANSFER_SIGHT_SEEING_PACKAGE_ID", DbType.Int32, TRANSFER_SIGHT_SEEING_PACKAGE_ID);
                }


                db.AddInParameter(dbCmd, "@TRANSFER_SIGHT_SEEING_PACKAGE_FLAG", DbType.String, TRANSFER_SIGHT_SEEING_PACKAGE_FLAG);


                if (DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(DATE.ToString(), "dd/MM/yyyy", null));
                }

                 db.AddInParameter(dbCmd, "@TIME", DbType.String, TIME);

                if (ORDER_STATUS.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.Int32, ORDER_STATUS);
                }
            
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, int.Parse(CREATED_BY));

           if (NO_OF_MEALS.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_MEALS", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_MEALS", DbType.Int32, NO_OF_MEALS);
                }
           
                 db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, QUOTE_ID);
          
                db.AddInParameter(dbCmd, "@SIC_PVT_FLAG", DbType.String, SIC_PVT_FLAG);
           
                 if (TRANSFER_PACKAGE_DETAIL_ID.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_DETAIL_ID", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_DETAIL_ID", DbType.Int32, TRANSFER_PACKAGE_DETAIL_ID);
                }

                  if (PAYMENT_DUE_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_DUE_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_DUE_DATE", DbType.DateTime, DateTime.ParseExact(PAYMENT_DUE_DATE.ToString(), "dd/MM/yyyy", null));
                }

                if (CANCELLATION_FEE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CANCELLATION_FEE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CANCELLATION_FEE", DbType.Decimal, Decimal.Parse(CANCELLATION_FEE));
                }

                 db.AddInParameter(dbCmd, "@SERVICE_VOUCHER_NO", DbType.String, SERVICE_VOUCHER_NO);

                db.AddInParameter(dbCmd, "@PACKAGE_FLAG", DbType.Boolean, PACKAGE_FLAG);

                if (ADULT_PVT_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ADULT_PVT_RATE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ADULT_PVT_RATE", DbType.Decimal, Decimal.Parse(ADULT_PVT_RATE));
                }

                if (ADULT_SIC_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ADULT_SIC_RATE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ADULT_SIC_RATE", DbType.Decimal, Decimal.Parse(ADULT_SIC_RATE));
                }

                if (CHILD_PVT_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CHILD_PVT_RATE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHILD_PVT_RATE", DbType.Decimal, Decimal.Parse(CHILD_PVT_RATE));
                }

                 if (CHILD_SIC_RATE	.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CHILD_SIC_RATE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHILD_SIC_RATE", DbType.Decimal, Decimal.Parse(CHILD_SIC_RATE));
                }

                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }


        }
        #endregion


        #region HOTEL CART DETAILS
        public void COPY_HOTEL_CART(String CITY_ID, String SUPPLIER_HOTEL_PRICE_LIST_ID,

           String FROM_DATE,
           String TO_DATE,
           String NO_OF_ROOMS,
           String ORDER_STATUS,
           String CREATED_BY,
           String ROOM_TYPE,
           String ROOM_TYPE_ID,
int QUOTE_ID,
String HOTEL_RECONFIRMATATION_DATE,
String HOTEL_STATUS,
String CONFIRMATION_NUMBER,
String PAYMENT_DUE_DATE,
String CANCELLATION_FEE,
String SERVICE_VOUCHER_NO,
Boolean  PACKAGE_FLAG



           )
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_HOTEL_CART_DETAIL_COPY_TOUR");

                db.AddInParameter(dbCmd, "@HOTEL_CART_ID", DbType.Int32, Int32.Parse("0"));

                if (CITY_ID.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, CITY_ID);
                }

                if (SUPPLIER_HOTEL_PRICE_LIST_ID.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@SUPPLIER_HOTEL_PRICE_LIST_ID", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@SUPPLIER_HOTEL_PRICE_LIST_ID", DbType.Int32, SUPPLIER_HOTEL_PRICE_LIST_ID);
                }


                //db.AddInParameter(dbCmd, "@TRANSFER_SIGHT_SEEING_PACKAGE_FLAG", DbType.String, TRANSFER_SIGHT_SEEING_PACKAGE_FLAG);


                if (FROM_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(FROM_DATE.ToString(), "dd/MM/yyyy", null));
                }

                if (TO_DATE .ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(TO_DATE.ToString(), "dd/MM/yyyy", null));
                }

                //db.AddInParameter(dbCmd, "@TIME", DbType.String, TIME);

                if (ORDER_STATUS.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.Int32, ORDER_STATUS);
                }

                

                if (NO_OF_ROOMS.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_ROOMS", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_ROOMS", DbType.Int32, NO_OF_ROOMS);
                }

                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, int.Parse(CREATED_BY));
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, QUOTE_ID);

               

                if (ROOM_TYPE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.Int32, ROOM_TYPE);
                }

                if (ROOM_TYPE_ID.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, ROOM_TYPE_ID);
                }


                if (PAYMENT_DUE_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_DUE_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_DUE_DATE", DbType.DateTime, DateTime.ParseExact(PAYMENT_DUE_DATE.ToString(), "dd/MM/yyyy", null));
                }

                if (HOTEL_RECONFIRMATATION_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@HOTEL_RECONFIRMATATION_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@HOTEL_RECONFIRMATATION_DATE", DbType.DateTime, DateTime.ParseExact(HOTEL_RECONFIRMATATION_DATE.ToString(), "dd/MM/yyyy", null));
                }

                if (HOTEL_STATUS.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@HOTEL_STATUS", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@HOTEL_STATUS", DbType.Int32, HOTEL_STATUS);
                }

                if (CANCELLATION_FEE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CANCELLATION_FEE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CANCELLATION_FEE", DbType.Decimal, Decimal.Parse(CANCELLATION_FEE));
                }

                db.AddInParameter(dbCmd, "@SERVICE_VOUCHER_NO", DbType.String, SERVICE_VOUCHER_NO);

                db.AddInParameter(dbCmd, "@PACKAGE_FLAG", DbType.Boolean, PACKAGE_FLAG);

                //if (ADULT_PVT_RATE.ToString().Equals(""))
                //{
                //    db.AddInParameter(dbCmd, "@ADULT_PVT_RATE", DbType.Decimal, Decimal.Parse("0"));
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@ADULT_PVT_RATE", DbType.Decimal, Decimal.Parse(ADULT_PVT_RATE));
                //}

                //if (ADULT_SIC_RATE.ToString().Equals(""))
                //{
                //    db.AddInParameter(dbCmd, "@ADULT_SIC_RATE", DbType.Decimal, Decimal.Parse("0"));
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@ADULT_SIC_RATE", DbType.Decimal, Decimal.Parse(ADULT_SIC_RATE));
                //}

                //if (CHILD_PVT_RATE.ToString().Equals(""))
                //{
                //    db.AddInParameter(dbCmd, "@CHILD_PVT_RATE", DbType.Decimal, Decimal.Parse("0"));
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@CHILD_PVT_RATE", DbType.Decimal, Decimal.Parse(CHILD_PVT_RATE));
                //}

                //if (CHILD_SIC_RATE.ToString().Equals(""))
                //{
                //    db.AddInParameter(dbCmd, "@CHILD_SIC_RATE", DbType.Decimal, Decimal.Parse("0"));
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@CHILD_SIC_RATE", DbType.Decimal, Decimal.Parse(CHILD_SIC_RATE));
                //}

                db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }


        }
        #endregion



        #region HOTEL CART SUB DETAILS
        public void COPY_HOTEL_CART_SUB_DETAILS(String SUPPLIER_HOTEL_PRICE_LIST_ID,

           String FROM_DATE,
           String TO_DATE,
           //String NO_OF_ROOMS,
           //String ORDER_STATUS,
          // String CREATED_BY,
            Boolean PACKAGE_FLAG,
           String SINGLE_ROOM_RATE,
           String DOUBLE_ROOM_RATE,

String TRIPLE_ROOM_RATE,
String EXTRA_ADULT_RATE,
String EXTRA_CWB_RATE,
String EXTRA_CNB_RATE





           )
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_HOTEL_SUB_CART_DETAIL_FOR_COPY_TOUR");

                db.AddInParameter(dbCmd, "@HOTEL_CART_SUB_DETAIL_ID", DbType.Int32, Int32.Parse("0"));

                if (SUPPLIER_HOTEL_PRICE_LIST_ID.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@SUPPLIER_HOTEL_PRICE_LIST_ID", DbType.Int32, Int32.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@SUPPLIER_HOTEL_PRICE_LIST_ID", DbType.Int32, SUPPLIER_HOTEL_PRICE_LIST_ID);
                }


                //db.AddInParameter(dbCmd, "@TRANSFER_SIGHT_SEEING_PACKAGE_FLAG", DbType.String, TRANSFER_SIGHT_SEEING_PACKAGE_FLAG);


                if (FROM_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(FROM_DATE.ToString(), "dd/MM/yyyy", null));
                }

                if (TO_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(TO_DATE.ToString(), "dd/MM/yyyy", null));
                }

               
            //    db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, int.Parse(CREATED_BY));
                if (SINGLE_ROOM_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@SINGLE_ROOM_RATE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@SINGLE_ROOM_RATE", DbType.Decimal, Decimal.Parse(SINGLE_ROOM_RATE));
                }

                if (DOUBLE_ROOM_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DOUBLE_ROOM_RATE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DOUBLE_ROOM_RATE", DbType.Decimal, Decimal.Parse(DOUBLE_ROOM_RATE));
                }

                if (TRIPLE_ROOM_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TRIPLE_ROOM_RATE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TRIPLE_ROOM_RATE", DbType.Decimal, Decimal.Parse(TRIPLE_ROOM_RATE));
                }

                if (EXTRA_ADULT_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@EXTRA_ADULT_RATE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EXTRA_ADULT_RATE", DbType.Decimal, Decimal.Parse(EXTRA_ADULT_RATE));
                }

                if (EXTRA_CWB_RATE .ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@EXTRA_CWB_RATE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EXTRA_CWB_RATE", DbType.Decimal, Decimal.Parse(EXTRA_CWB_RATE));
                }

                if (EXTRA_CNB_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@EXTRA_CNB_RATE", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EXTRA_CNB_RATE", DbType.Decimal, Decimal.Parse(EXTRA_CNB_RATE));
                }

                db.AddInParameter(dbCmd, "@PACKAGE_FLAG", DbType.Boolean, PACKAGE_FLAG);

              

                 db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }


        }
        #endregion

        /*Check invoice Type*/
        public DataSet checkInvoiceType(int INVOICE_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("CHECK_INVOICE_TYPE");
                db.AddInParameter(dbCmd, "@INVOICE_ID", DbType.String, INVOICE_ID);

                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return ds;
        }

        /*FOR MULTIPLE RECORDS IN DETAILS*/
        public DataSet getSalesInvoiceAmount(int INVOICE_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_INVOICE_AMOUNT_DETAILS");
                db.AddInParameter(dbCmd, "@SALES_INVOICE_ID", DbType.String, INVOICE_ID);

                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return ds;
        }

        public DataSet UpdateInvoiceforclose1andclose2(String sp_name, String invoice_no)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_NO", DbType.String, invoice_no);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetchclosestatus(String sp_name, String invoice_no)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_NO", DbType.String, invoice_no);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

    }
}
