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
    public class InvoiceThb
    {

        public DataTable insert_sales_invoice_header(int invoic, int quote_id, int order_by_id, int agent_id, String from_date, String to_date, String no_of_nights, decimal amount, decimal tax_amount, decimal total_amount, String no_of_adults, String no_of_child, String no_of_cwb, String no_of_cnb, String no_of_infant, String order_staus, String payment_mode, String ref_no, String GL_DATE, String currency, int companyid, String CLIENTNAME, decimal EX_RATE, string ORDER_PLACED_BY, string PERSON_EMAIL)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_SALES_INVOICE_THB");
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


                if (no_of_adults.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, int.Parse(no_of_adults));
                }

                if (no_of_child.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CHILD", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CHILD", DbType.Int32, int.Parse( no_of_child));
                }

                if (no_of_cwb.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32, int.Parse( no_of_cwb));
                }

                if (no_of_cwb.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32, int.Parse( no_of_cwb));
                }

                if (no_of_infant.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, int.Parse(no_of_infant));
                }

                db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, order_staus);
                db.AddInParameter(dbCmd, "@PAYMENT_MODE", DbType.String, payment_mode);
                db.AddInParameter(dbCmd, "@COMPANY_BOOKING_REFERENCE_NO", DbType.String, ref_no);
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.String, currency);
                db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, companyid);
                db.AddInParameter(dbCmd, "@CLIENTNAME", DbType.String, CLIENTNAME);
                db.AddInParameter(dbCmd, "@EX_RATE", DbType.Decimal, EX_RATE);
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

        public void insertSalesDetails(int SALES_INVOICE_ID, String INVOICE_DESCRIPTION, String AMOUNT, String NO_OF_PERSON)
        { Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_SALES_DETAILS_THB_INVOICE");
                db.AddInParameter(dbCmd, "@SALES_INVOICE_ID", DbType.Int32, SALES_INVOICE_ID);
                db.AddInParameter(dbCmd, "@INVOICE_DESCRIPTION", DbType.String, INVOICE_DESCRIPTION);
                db.AddInParameter(dbCmd, "@CURRANCY", DbType.String, "THB");
                if (AMOUNT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, 0.00);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, AMOUNT);
                }

                if (NO_OF_PERSON.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_PERSON", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_PERSON", DbType.Int32, NO_OF_PERSON);
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

            

        }

        public DataSet FetchDataForInvoice(int p)
        {

            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_DATA_FOR_GENREATE_INVOICE_THB");
                //db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(s));
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, p);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet insert_sales_invoice_details(int invoic, int id1, int id2, int id3, int id4, int id5, int id6, int id7, int id8, int id9, int id10, int id11, int id12, int id13, int id14, int id15,
      String a1, String a2, String a3, String a4, String a5, String a6, String a7, String a8, String a9, String a10, String a11, String a12, String a13, String a14, String a15,
      String in1, String in2, String in3, String in4, String in5, String in6, String in7, String in8, String in9, String in10, String in11, String in12, String in13, String in14, String in15, String Currency, String unit1,
            String unit2, String unit3, String unit4, String unit5, String unit6, String unit7, String unit8, String unit9, String unit10, String unit11, String unit12, String unit13, String unit14, String unit15)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_SALES_INVOICE_DETAILS_MANUALLY");
                db.AddInParameter(dbCmd, "@SALES_INVOICE_ID", DbType.Int32, invoic);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAILS_ID1", DbType.Int32, id1);
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
                    db.AddInParameter(dbCmd, "@AMOUNT1", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@AMOUNT1", DbType.Decimal, Convert.ToDecimal(a1));
                }
                if (a2.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT2", DbType.Decimal, Convert.ToDecimal("0"));
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


        public void insertInvoiceDetails(int SALES_INVOICE_DETAIL_ID,int SALES_INVOICE_ID, String INVOICE_DESCRIPTION, String AMOUNT, String NO_OF_PERSON, String Currency)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_INTO_SALES_INVOICE_DETAILS");
                db.AddInParameter(dbCmd, "@SALES_INVOICE_DETAIL_ID", DbType.Int32, SALES_INVOICE_DETAIL_ID);
                db.AddInParameter(dbCmd, "@SALES_INVOICE_ID", DbType.Int32, SALES_INVOICE_ID);
                db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, INVOICE_DESCRIPTION);
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.String, Currency);
                if (AMOUNT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, 0.00);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, AMOUNT);
                }

                if (NO_OF_PERSON.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@UNIT_NO", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@UNIT_NO", DbType.Int32, NO_OF_PERSON);
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



        }
    }
}
