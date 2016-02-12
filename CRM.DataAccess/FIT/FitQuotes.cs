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
    public class FitQuotes
    {
        #region  FETCH ORDER STAUTS RAD COMBO BOX
        public DataSet fetchallData(String sp_name, String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@USERID", DbType.Int32, int.Parse(param));
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetchallDatasearch(String sp_name, String param1, String param2, String param3, String param4, String param5, String param6, String param7)
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
                db.AddInParameter(dbCmd, "@USERID", DbType.Int32, int.Parse(param1));
                db.AddInParameter(dbCmd, "@QUOTATION_NO", DbType.Int32, int.Parse(param2));
                //db.AddInParameter(dbCmd, "@AGENT_NAME_S", DbType.String, AGENT);
                db.AddInParameter(dbCmd, "@CLIENT_NAME_S", DbType.String, param3);
                db.AddInParameter(dbCmd, "@TOUR_NAME_S", DbType.String, param4);
                db.AddInParameter(dbCmd, "@STATUS", DbType.String, param5);
                if (param6.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE_S", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE_S", DbType.DateTime, DateTime.ParseExact(param6.ToString(), "dd/MM/yyyy", null));
                }
                if (param7.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TO_DATE_S", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE_S", DbType.DateTime, DateTime.ParseExact(param7.ToString(), "dd/MM/yyyy", null));
                }


                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fillagentdropdown(String sp_name)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                //db.AddInParameter(dbCmd, "@USERID", DbType.Int32, int.Parse(param));
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        #endregion




        #region  FETCH ORDER STAUTS1 RAD COMBO BOX
        public DataSet FetchForFitBooking(String sp_name, String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, int.Parse(param));
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        #endregion

        public DataSet FetchForFitBookingquote(String sp_name, String param)
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

            return dsData;
        }

        public void DeleteQuote(String param)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_QUOTE_FROM_TOUR_QUOTE");
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(param));
                db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {

            }

           
        }

        public void Agent_Reminder_Entry(String quoteid)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_REMINDER_ENTRY");
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(quoteid));
                db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {

            }


        }

        public DataSet FetchNoofRooms(String sp_name, String QuoteId, String RoomType)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(QuoteId));
                db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.Int32, int.Parse(RoomType));
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        // VALIDATION ON RECONFIRM BUTTON

        public DataTable fetchsiteseeingForValidation(String sp_name, String quote_id, String City)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(quote_id));
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, City);
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

        // Update of Client Name
        public void UpdateAgentName(String TourId, String ClientName, String ClientSurName, String ClientTitle, String tourName)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_CLIENT_DETAILS");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, int.Parse(TourId));
                db.AddInParameter(dbCmd, "@CLIENT_NAME", DbType.String, ClientName);
                db.AddInParameter(dbCmd, "@CLIENT_SURNAME", DbType.String, ClientSurName);
                db.AddInParameter(dbCmd, "@CLIENT_TITLE", DbType.String, ClientTitle);
                db.AddInParameter(dbCmd, "@TOUR_SHORT_NAME", DbType.String, tourName);
                
                db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {

            }


        }

        //update stautus of quote
        public void Update_Status_Quote(String quoteid)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_QUOTE_STATUS_JUNK");
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(quoteid));
                db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }


        }

        //
        //public void Get_Currency_quotewise(String quoteid)
        //{
        //    Database db = null;
        //    DbCommand dbCmd = null;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
        //        dbCmd = db.GetStoredProcCommand("GET_CURRENCY_QUOTEWISE");
        //        db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(quoteid));
        //        db.ExecuteNonQuery(dbCmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {

        //    }


        //}

        public DataSet Get_Currency_quotewise(String sp_name, String param)
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

            return dsData;
        }
        
    }
}
