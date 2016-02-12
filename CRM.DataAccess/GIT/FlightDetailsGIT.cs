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
    public class FlightDetailsGIT
    {
        #region Save Record
        public DataSet InsertArrivalFlightDetails(int flightid, String flightname, String flighttime, int tourid, string flightflag, int noofpassnger, String Date)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;
            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_FLIGHT_DETAILS");
                db.AddInParameter(dbCmd, "@GIT_FLIGHT_ID", DbType.Int32, flightid);
                db.AddInParameter(dbCmd, "@FLIGHT_NAME", DbType.String, flightname);
                if (flighttime.ToString().Equals("DD/MM/YYYY") || flighttime.ToString().Equals(""))
                {

                    db.AddInParameter(dbCmd, "@FLIGHT_TIME", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FLIGHT_TIME", DbType.DateTime, DateTime.Parse(flighttime.ToString()));
                }
                //db.AddInParameter(dbCmd, "@FLIGHT_TIME", DbType.DateTime, flighttime);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourid);
                db.AddInParameter(dbCmd, "@FLIGHT_FLAG", DbType.String, flightflag);
                db.AddInParameter(dbCmd, "@NO_OF_PASSENGER", DbType.Int32, noofpassnger);

                if (Date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FLIGHT_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FLIGHT_DATE", DbType.DateTime, DateTime.ParseExact(Date.ToString(), "dd/MM/yyyy", null));
                }


                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsData;
        }

        public DataSet InsertDepartureFlightDetails(int flightid, String flightname, String flighttime, int tourid, string flightflag, int noofpassnger,String Date)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;
            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_FLIGHT_DETAILS");
                db.AddInParameter(dbCmd, "@GIT_FLIGHT_ID", DbType.Int32, flightid);
                db.AddInParameter(dbCmd, "@FLIGHT_NAME", DbType.String, flightname);
                if (flighttime.ToString().Equals("DD/MM/YYYY") || flighttime.ToString().Equals(""))
                {

                    db.AddInParameter(dbCmd, "@FLIGHT_TIME", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FLIGHT_TIME", DbType.DateTime, DateTime.Parse(flighttime.ToString()));
                }
                //db.AddInParameter(dbCmd, "@FLIGHT_TIME", DbType.DateTime, flighttime);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourid);
                db.AddInParameter(dbCmd, "@FLIGHT_FLAG", DbType.String, flightflag);
                db.AddInParameter(dbCmd, "@NO_OF_PASSENGER", DbType.Int32, noofpassnger);
                if (Date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FLIGHT_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FLIGHT_DATE", DbType.DateTime, DateTime.ParseExact(Date.ToString(), "dd/MM/yyyy", null));
                }
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsData;
        }

        #endregion

        /******************************************************* Flight Details ***************************************/
        public DataSet GetFlightDetails(int TOUR_ID, string FlightFlag)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("SELECT_DATA_GIT_FLIGHT_DETAILS");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, TOUR_ID);
                db.AddInParameter(dbCmd, "@FLAG", DbType.String, FlightFlag);
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

        public DataSet DeleteFlightDetails(int tourid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;
            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_DATA_GIT_FLIGHT_DETAILS");

                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourid);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsData;
        }

    }
}
