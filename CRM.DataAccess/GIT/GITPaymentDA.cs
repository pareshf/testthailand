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
    public class GITPaymentDA
    {

        // FETCH CURRENCY FOR COMPANY

        public DataSet fetch_currency_for_company(String sp_name, int company_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, company_id);
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

        //**------------------------- FETCH PAYMENT MODE ------------------------------**//
        public DataSet fetch_paymentmode(String sp_name)
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

        //**------------------------- FETCH HOTEL RATE ------------------------------**//
        public DataSet Fetch_Hotel_Rate(String sp_name, int tour_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, tour_id);
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

        //**------------------------- FETCH RESTAURENT RATE ------------------------------**//
        public DataSet Fetch_Restaurant_Rate(String sp_name, int tour_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, tour_id);
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

        //**------------------------- FETCH CONFERENCE RATE ------------------------------**//
        public DataSet Fetch_Conference_Rate(String sp_name, int tour_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, tour_id);
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

        //**------------------------- FETCH GALADINNER RATE ------------------------------**//
        public DataSet Fetch_Gala_Dinner_Rate(String sp_name, int tour_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, tour_id);
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

        //**------------------------- FETCH SIGHTSEEING RATE ------------------------------**//
        public DataSet Fetch_Sight_Seeing_Supplier(String sp_name, int tour_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, tour_id);
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

        public DataSet Fetch_Site_Seeing_Rate(String sp_name, String Supplier_name, int tour_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, tour_id);
                db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, Supplier_name);
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

        //**------------------------- FETCH COACH RATE ------------------------------**//
        public DataSet Fetch_Coach_Rate(String sp_name, int tour_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, tour_id);
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

        //**------------------------- FETCH BOAT RATE ------------------------------**//
        public DataSet Fetch_Boat_Rate(String sp_name, int tour_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, tour_id);
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

        //**------------------------- FETCH GUIDE RATE ------------------------------**//
        public DataSet Fetch_Guide_Rate(String sp_name, int tour_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, tour_id);
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

        //**-------------------------- FETCH_GIT_ADDITIONAL_SERVICE_CART ---------------**//

        public DataSet Fetch_Additional_Rate(String sp_name, int tour_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, tour_id);
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


        #region SALES INVOICE HEADER & SALES INVOICE DETAILS
        
        //------------------------- INSERT SALES INVOICE HEADER ------------------------------
        public DataSet insert_sales_invoice_header(int quote_id, int order_by_id, int agent_id, String from_date, String to_date, int no_of_nights, String amount, decimal tax_amount, String total_amount, bool posted_flag, int no_of_adults, int no_of_child, int no_of_cwb, int no_of_cnb, int no_of_infant, String order_staus, String payment_mode, String ref_no, int company_id, String currency)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_SALES_INVOICE_HEADER_GIT");
                db.AddInParameter(dbCmd, "@SALES_INVOICE_ID", DbType.Int32, 0);
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
                db.AddInParameter(dbCmd, "GL_DATE", DbType.DateTime, DBNull.Value);
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

        //------------------------- INSERT SALES INVOICE DETAILS ------------------------------
        public DataSet insert_sales_invoice_details(int invoice_id,string currency ,decimal single, decimal doubl, decimal triple, decimal cwb, decimal cnb
            , int noofper_single, int noofper_double, int noofper_triple, int noofper_cwb, int noofper_cnb)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_SALES_INVOICE_DETAILS_GIT");
                db.AddInParameter(dbCmd, "@INVOICE", DbType.Int32, invoice_id);
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.String, currency);
                db.AddInParameter(dbCmd, "@TOTAL_SINGLE_SHARE_AMOUNT", DbType.Decimal, single);
                db.AddInParameter(dbCmd, "@TOTAL_DOUBLE_SHARE_AMOUNT", DbType.Decimal, doubl);
                db.AddInParameter(dbCmd, "@TOTAL_TRIPLE_SHARE_AMOUNT", DbType.Decimal, triple);
                db.AddInParameter(dbCmd, "@TOTAL_CWB_AMOUNT", DbType.Decimal, cwb);
                db.AddInParameter(dbCmd, "@TOTAL_CNB_AMOUNT", DbType.Decimal, cnb);
                db.AddInParameter(dbCmd, "@NO_OF_PERSON_SINGLE", DbType.Int32, noofper_single);
                db.AddInParameter(dbCmd, "@NO_OF_PERSON_DOUBLE", DbType.Int32, noofper_double);
                db.AddInParameter(dbCmd, "@NO_OF_PERSON_TRIPLE", DbType.Int32, noofper_triple);
                db.AddInParameter(dbCmd, "@NO_OF_PERSON_CWB", DbType.Int32, noofper_cwb);
                db.AddInParameter(dbCmd, "@NO_OF_PERSON_CNB", DbType.Int32, noofper_cnb);
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

        //------------------------- FETCH SALES INVOICE HEADER ------------------------------
        public DataSet FetchSalesInvoiceHeader(String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_SALES_INVOICE_HEADER_GIT");
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, int.Parse(param));
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

        //------------------------- FETCH CREDIT LIMIT ------------------------------
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

        //---------------------- EDIT CURRENT USABLE IN CUST_CUSTOMER_MASTER -------------------
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

        //------------------------- FETCH PASSWORD ------------------------------
        public DataSet fetch_password(int customer_emp_no)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_PASSWORD_FOR_INVOICE");
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, customer_emp_no);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        //------------------------- FETCH CURRENCY ------------------------------
        public DataTable fetch_currency()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_CURRENCY_NAME");
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData.Tables[0];
        }

        //------------------------- FETCH ORDER STATUS NAME ------------------------------
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

        //------------------------- FETCH TOTAL INVOICE ------------------------------
        public DataSet fetch_total_invoice(int quote_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_TOTAL_INVOICE_AMOUNT_GIT");
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quote_id);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        
        #endregion

        /* FETCH CITIES IN FIT PACKAGES */
        public DataSet fetchGitCities(String SPNAME, String PACKAGE_NAME)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(SPNAME);
                db.AddInParameter(dbCmd, "@GIT_PACKAGE_ID", DbType.String, PACKAGE_NAME);



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
        
        /* for paypal account */
        public string getItemNameAndCost(string itemName, string itemCost)
        {

            //Converting String Money Value Into Decimal
            decimal price = Convert.ToDecimal(itemCost);
            //declaring empty String
            string returnURL = "";
            returnURL += "https://www.paypal.com/xclick/business=sudhir@travelzunlimited.com";
            //Passing Item Name as dynamic
            returnURL += "&item_name=" + itemName;
            ////Assigning Name as Statically to Parameter
            //string fname = "Raghunadh Babu";
            //returnURL += "&first_name" + fname;
            ////Assigning City as Statically to Parameter
            //string myCity = "Ahmedabad";
            //returnURL += "&city" + myCity;
            ////Assigning State as Statically to Parameter
            //string myState = "Andra Pradesh";
            //returnURL += "&state" + myState;
            //Passing Amount as Dynamic
            returnURL += "&amount=" + price;
            //Passing Currency as your 
            returnURL += "&currency=USD";
            //retturn Url if Customer wants To return to Previous Page
            returnURL += "&return=http://bangarubabupureti.spaces.live.com";
            //retturn Url if Customer Wants To Cancel the Transaction
            returnURL += "&cancel_return=http://bangarubabupureti.spaces.live.com";
            return returnURL;

        }

        #region PURCHASE INVOICE DETAILS FOR CONFERENCE
        public DataSet conferencePurchaseInvoice(int purchase_id, String supplier_name, String invoice_no, int user_id, String due_date, String amount, String tax_amount, String total_amount, String currency, String paymentmode, int posted_by, String supplier_type, int purchase_details_id, String description, int no_of_adult, int no_of_cwb, int no_of_cnb, int no_of_infant, String period_stay_from, String period_stay_to, int no_of_night, int no_of_single_room, int no_of_double_room, int no_of_triple_room, String room_type, int tp_detail_id, String tp_flag, String ss_name, String ss_date, String ss_flag, String details_amount, int comapny_id, int flag, String CONFERENCE_DATE, String CONFERENCE_TYPE, String quantity, String time)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_PURCHASE_HEADER_DETAILS_FOR_CONFERENCE");

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

                db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, comapny_id);


                db.AddInParameter(dbCmd, "@FLAG", DbType.Int32, flag);

                if (CONFERENCE_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CONFERENCE_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CONFERENCE_DATE", DbType.DateTime, DateTime.ParseExact(CONFERENCE_DATE.ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@CONFERENCE_TYPE", DbType.String, CONFERENCE_TYPE);

                if (quantity.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@QUANTITY", DbType.Int32, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@QUANTITY", DbType.Int32, int.Parse(quantity));
                }

                if (time.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse(time.ToString()));
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
        #endregion

        #region PURCHASE INVOICE DETAILS FOR CONFERENCE
        public DataSet galaDinnerPurchaseDetails(int purchase_id, String supplier_name, String invoice_no, int user_id, String due_date, String amount, String tax_amount, String total_amount, String currency, String paymentmode, int posted_by, String supplier_type, int purchase_details_id, String description, int no_of_adult, int no_of_cwb, int no_of_cnb, int no_of_infant, String period_stay_from, String period_stay_to, int no_of_night, int no_of_single_room, int no_of_double_room, int no_of_triple_room, String room_type, int tp_detail_id, String tp_flag, String ss_name, String ss_date, String ss_flag, String details_amount, int comapny_id, int flag, String CONFERENCE_DATE, String CONFERENCE_TYPE, String quantity, String time)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_PURCHASE_HEADER_DETAILS_FOR_GALA_DINNER");

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

                db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, comapny_id);


                db.AddInParameter(dbCmd, "@FLAG", DbType.Int32, flag);

                if (CONFERENCE_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CONFERENCE_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CONFERENCE_DATE", DbType.DateTime, DateTime.ParseExact(CONFERENCE_DATE.ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@GALA_DINNER_TYPE", DbType.String, CONFERENCE_TYPE);

                if (quantity.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@QUANTITY", DbType.Int32, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@QUANTITY", DbType.Int32, int.Parse(quantity));
                }

                if (time.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse(time.ToString()));
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
        #endregion

        public void updateInvoiceAsGIT(int invoiceId)
        {
            try
            {
                Database db = null;
                DbCommand dbCmd = null;
                DataSet ds = null;

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_INVOICE_TO_GIT");

                db.AddInParameter(dbCmd, "@INVOICE_ID", DbType.Int32, invoiceId);

                db.ExecuteNonQuery(dbCmd);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
        }
    }
}
