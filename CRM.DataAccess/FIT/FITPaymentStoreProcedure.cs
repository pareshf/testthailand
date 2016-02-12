using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;


namespace CRM.DataAccess.FIT
{
    public class FITPaymentStoreProcedure
    {

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

        #region  INSERT INTO SALES INVOICE HEADER

        public DataTable insert_sales_invoice_header(int quote_id, int order_by_id, int agent_id, String from_date, String to_date, String no_of_nights, decimal amount, decimal tax_amount, decimal total_amount, bool posted_flag, int no_of_adults, int no_of_child, int no_of_cwb, int no_of_cnb, int no_of_infant, String order_staus, String payment_mode, String ref_no, int company_id, String currency)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_SALES_INVOICE_HEADER");
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
        #endregion

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


        #region Insert Order Status


        public void INSERT_ORDER_STATUS(String sp_name, int param, String param1)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, param);
                db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, param1);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

           //eturn dsData;
        }

        public DataTable Fetch_Order_Status_from_quoteid(String sp_name, String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(param));
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData.Tables[0];
        }

        public DataTable fetch_authorisationno(String sp_name, String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(param));
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData.Tables[0];
        }
        
        
        
        #endregion


        //for paypal account

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

        // SPS FOR GENERATE SALES VOUCHER OF INVOIC NO 
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

        public void insert_accounts_entry(int account_voucher_id, String gl_code, String invoice_no, String voucher_date, String voucher_type, int employee_id, String narration, int prepared_by, int approved_by, int posted_by, String voucher_status_id, int voucher_detail_id, String cr_amount, String dr_amount, String payment_mode, String cheque_no, String cheque_date, String bank_id, String branch, String remarks, String receipt_no, String ex_rate, String forex_amount, String forex_currency, int comapny_id, String currency, int flag)
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

        public DataSet fetch_all_gl_code()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_FIT_TOUR_CODE");

                //FETCH_ALL_GL_CODE
                // 
             
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return ds;
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

        // METHOD FOR CREATING PURCHASE INVOICES AUTO FOR HOTELS
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

        public DataSet insert_purchase_entry(int purchase_id, String supplier_name, String invoice_no, int user_id, String due_date, String amount, String tax_amount, String total_amount, String currency, String paymentmode, int posted_by, String supplier_type, int purchase_details_id, String description, int no_of_adult, int no_of_cwb, int no_of_cnb, int no_of_infant, String period_stay_from, String period_stay_to, int no_of_night, int no_of_single_room, int no_of_double_room, int no_of_triple_room, String room_type, int tp_detail_id, String tp_flag, String ss_name, String ss_date, String ss_flag, String details_amount, int comapny_id, int flag)
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

        // FOR TRANSFER PACKAGES
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

        // SERVICE NO
        public DataSet fetch_hotel_service_no(String sp_name, int quoteid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.String, quoteid);
             //   db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, supplier_name);
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

        public void update_hotel_service_no(int quote_id, String city, String service_no)
        {
             Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_SERVICE_VOUCHER_NO_FOR_HOTEL");

                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.String, quote_id);
                db.AddInParameter(dbCmd, "@CITY", DbType.String, city);
                db.AddInParameter(dbCmd, "@SERVICE_VOUCHER_NO", DbType.String, service_no);

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

        public void update_transfer_service_no(int quote_id, int tp_detial_id, String service_no)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_SERVICE_VOUCHER_NO_FOR_TRANSFER");

                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.String, quote_id);
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_DETAIL_ID", DbType.String, tp_detial_id);
                db.AddInParameter(dbCmd, "@SERVICE_VOUCHER_NO", DbType.String, service_no);

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

        

        public void update_sight_service_no(int quote_id, int tp_detial_id, String service_no, int service_cart_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_SERVICE_VOUCHER_NO_FOR_SIGHT");

                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.String, quote_id);
                db.AddInParameter(dbCmd, "@TRANSFER_SIGHT_SEEING_PACKAGE_ID", DbType.String, tp_detial_id);
                db.AddInParameter(dbCmd, "@SERVICE_VOUCHER_NO", DbType.String, service_no);
                db.AddInParameter(dbCmd, "@SERVICE_CART_ID", DbType.String, service_cart_id);
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

        public DataTable FETCH_TOUR_ID_FROM_QUOTE_ID(string QUT)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_TOUR_ID_FROM_QUOTE_ID");
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

        public DataSet fetch_hotel_detials(String sp_name, String  hotel_name, String room_type)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, hotel_name);
                db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, room_type);
                //   db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, supplier_name);
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

        // ADDITIONAL SERVICE 

        public void insert_additional_service(int service_cart_id, String service_no)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_SERVICE_VOUCHER_NO_FOR_ADDITIONAL_SERVICES");

                db.AddInParameter(dbCmd, "@ADDITIONAL_SERVICE_CART_ID", DbType.String, service_cart_id);

                db.AddInParameter(dbCmd, "@SERVICE_VOUCHER_NO", DbType.String, service_no);

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
                // db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, room_type);
                //   db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, supplier_name);
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

        // CANCELLATION

        public void updte_voucher_status_on_cancel(String voucher_status, int seq_no)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("EDIT_VOUCHER_STATUS_ON_CANCELLATION");

                db.AddInParameter(dbCmd, "@VOUCHER_STATUS", DbType.String, voucher_status);

                db.AddInParameter(dbCmd, "@SEQ_NO", DbType.String, seq_no);

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
