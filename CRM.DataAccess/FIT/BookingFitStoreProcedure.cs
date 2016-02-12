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
   public class BookingFitStoreProcedure
    {


        #region  FETCH ORDER STAUTS RAD COMBO BOX
        public DataSet fetchComboData(String sp_name)
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
        public DataSet fetchComboData_LOGIN(String sp_name,String USERID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, int.Parse(USERID));
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        #endregion

        #region GET ALL PACKAGES
        public DataTable fetchPackages()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_FIT_PACKAGE_NAME");

               
             
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

        #region TITLE COMBOBOX
        public DataSet fetchTitle()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_TITLE");
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        #endregion

        public DataSet fetch_no_of_nights(String sp_name, int package_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@PACKAGE_ID", DbType.String, package_id);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetch_packge_close_Date(String sp_name, int package_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@PACKAGE_ID", DbType.Int32, package_id);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetch_bookig_days(String sp_name)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
            //    db.AddInParameter(dbCmd, "@PACKAGE_ID", DbType.Int32, package_id);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet UpdateTourdiscount(String disccount, int tourid, int quoteid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_DISCOUNT_AMOUNT");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourid);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quoteid);
                db.AddInParameter(dbCmd, "@DISCOUNT_AMOUNT", DbType.Decimal, Decimal.Parse(disccount));
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet GET_QUTED_COST(int tourid, int quoteid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_QUOTED_COST");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourid);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quoteid);
                
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public void update_amount_COA(String AMOUNT, String GL_CODE)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_DISCOUNT_AMOUNT_COA");
                if (AMOUNT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, Decimal.Parse(AMOUNT));
                }

                db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, GL_CODE);

                db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {

            }

           
        }

        public void update_amount_in_thb(String AMOUNT, String SALES_INVOICE_NO)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_INVOICE_HEADER_DICOUNT");
                if (AMOUNT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DISCOUNT_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DISCOUNT_AMOUNT", DbType.Decimal, Decimal.Parse(AMOUNT));
                }

                db.AddInParameter(dbCmd, "@SALES_INVOICE_NO", DbType.String, SALES_INVOICE_NO);

                db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {

            }


        }

        public DataSet GET_GL_DESCRIPTION(int cust_id, String flag)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_GL_DESCRIPTION_FOR_AGENT_LEDGER_REPORT");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, cust_id);
                db.AddInParameter(dbCmd, "@FLAG", DbType.String, flag);
                
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

       // AGENT QUTE ID 

        public DataSet GET_AGENT_QUOTEID (int userid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_QUOTE_ID_FOR_DOWNLOAD_VOUCHER");
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userid);
               // db.AddInParameter(dbCmd, "@FLAG", DbType.String, flag);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

       // GET ALL SUB AGENT AND AGENT

        public DataSet GET_SUB_AGENT(int userid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_ALL_SUB_AGENT");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, userid);
                // db.AddInParameter(dbCmd, "@FLAG", DbType.String, flag);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet GET_SUB_AGENT_REL_NO(int userid, String AgentName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_ALL_SUB_AGENT_REL_NO");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, userid);
                db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.String, AgentName);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

       // GET CUST ID FOR COMPANY
        public DataSet GET_comapny_CUSTID(String CompanyName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_AGENT_CUST_ID");
                db.AddInParameter(dbCmd, "@COMPANY_NAME", DbType.String, CompanyName);
                //db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.String, AgentName);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet UpdateTourFlyingAndLandingDate(int tourid, String flydate, String landdate)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_LANDING_DATE_FLYING_DATE");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourid);

                if (flydate.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FLYING_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FLYING_DATE", DbType.DateTime, DateTime.ParseExact(flydate.ToString(), "dd/MM/yyyy", null));
                }
                if (landdate.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@LANDING_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@LANDING_DATE", DbType.DateTime, DateTime.ParseExact(landdate.ToString(), "dd/MM/yyyy", null));
                }
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }


        public DataSet fetchinvoicompanywise(String sp_name, String CUST_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, int.Parse(CUST_ID));
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
    }
}