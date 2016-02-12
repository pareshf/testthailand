using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;  


namespace CRM.DataAccess.AdministratorEntity
{
    public class CruiseRoomAllocationCs
    {
        public void InsertNewCruise(int Tour_Id, int Booking_Detail_Id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_BOOKING_CRUISE");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Convert.ToInt32(Tour_Id));
                db.AddInParameter(dbCmd, "@BOOKING_DETAIL_ID", DbType.Int32, Convert.ToInt32(Booking_Detail_Id));
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

        public void InserUpdateBookingCruiseDetails(ArrayList Cruise)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CRUISE_BOOKING_DETAILS");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, Convert.ToInt32(Cruise[0]));
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Convert.ToInt32(Cruise[1]));
                db.AddInParameter(dbCmd, "@CRUISE_ID", DbType.String,Cruise[2]);
                db.AddInParameter(dbCmd, "@CABINES_TO_BE_BLOCKED", DbType.Int32, Convert.ToInt32(Cruise[4]));
                db.AddInParameter(dbCmd, "@CHECK_REQUEST_TO", DbType.String, Cruise[5]);
                db.AddInParameter(dbCmd, "@CHECK_COMMENTS", DbType.String, Cruise[6]);
                db.AddInParameter(dbCmd, "@BOOKING_REQUEST_TO", DbType.String, Cruise[7]);
                db.AddInParameter(dbCmd, "@TOTAL_ROOMS_BLOCKED", DbType.Int32, Convert.ToInt32(Cruise[8]));
                if (Cruise[9].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.DateTime, DateTime.ParseExact(Cruise[9].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@BOOKING_STATUS", DbType.String, Cruise[10]);
                db.AddInParameter(dbCmd, "@TOTAL_AMOUNT_PAID", DbType.Decimal, Convert.ToDecimal(Cruise[11]));
                db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, Convert.ToDecimal(Cruise[12]));
                db.AddInParameter(dbCmd, "@GST", DbType.Decimal, Convert.ToDecimal(Cruise[13]));
                db.AddInParameter(dbCmd, "@CABINE_CATEGORY", DbType.String, Cruise[3]);
                db.AddInParameter(dbCmd, "@CRUISE_NAME", DbType.String,Cruise[14]);
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

        public void InserUpdateRoom(ArrayList CruiseRoom)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_CRUISE_ROOM_ALLOCATION");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, Convert.ToInt32(CruiseRoom[0]));
                db.AddInParameter(dbCmd, "@DECK_NO", DbType.Int32, Convert.ToInt32(CruiseRoom[1]));
                db.AddInParameter(dbCmd, "@CABINE_NO", DbType.Int32, Convert.ToInt32(CruiseRoom[2]));
                db.AddInParameter(dbCmd, "@ADULT1", DbType.String, CruiseRoom[3]);
                db.AddInParameter(dbCmd, "@ADULT2", DbType.String, CruiseRoom[4]);
                db.AddInParameter(dbCmd, "@ADULT3", DbType.String, CruiseRoom[5]);
                db.AddInParameter(dbCmd, "@CWB", DbType.String, CruiseRoom[6]);
                db.AddInParameter(dbCmd, "@CNB1", DbType.String, CruiseRoom[7]);
                db.AddInParameter(dbCmd, "@CNB2", DbType.String, CruiseRoom[8]);
                db.AddInParameter(dbCmd, "@INFANT", DbType.String, CruiseRoom[9]);
                db.AddInParameter(dbCmd, "@ROOM_SHARED", DbType.String, CruiseRoom[10]);
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
