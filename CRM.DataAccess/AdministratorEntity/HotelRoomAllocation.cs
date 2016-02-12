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
    public class HotelRoomAllocation
    {
        public void InsertUpdateHotel(ArrayList Hotel)
        {
            Database db = null;
            DbCommand dbCmd = null;
            Database db_task = null;
            DbCommand dbCmd_task = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_BOOKING_HOTEL_DETAIL");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Convert.ToInt32(Hotel[0]));
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, Convert.ToInt32(Hotel[1]));
                if (Hotel[2].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(Hotel[2].ToString(), "dd/MM/yyyy", null));
                }
                if (Hotel[3].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(Hotel[3].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.String, Hotel[4]);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.String, Hotel[5]);
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.String, Hotel[6]);
                db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, Convert.ToDecimal(Hotel[7]));
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.String, Hotel[8]);
               // db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.String, Hotel[9]);
                db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, Convert.ToDecimal(Hotel[10]));
                db.AddInParameter(dbCmd, "@GST", DbType.Decimal, Convert.ToDecimal(Hotel[11]));
                db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, Convert.ToDecimal(Hotel[12]));
                db.AddInParameter(dbCmd, "@FINAL_AMOUNT", DbType.Decimal, Convert.ToDecimal(0));
                db.AddInParameter(dbCmd, "@REMARKS", DbType.String, Hotel[13]);
                db.AddInParameter(dbCmd, "@BOOKING_REQUEST_TO", DbType.String, Hotel[14]);
                db.AddInParameter(dbCmd, "@CHECK_REQUEST_TO", DbType.String, Hotel[15]);
                if (Hotel[16].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.DateTime, DateTime.ParseExact(Hotel[16].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@BOOKING_STATUS", DbType.String, Hotel[17]);
                db.AddInParameter(dbCmd, "@ROOM_TO_BE_BLOCKED", DbType.Int32, Convert.ToInt32(Hotel[18]));
                db.AddInParameter(dbCmd, "@CHECK_COMMENTS", DbType.String, Hotel[19]);

                db.ExecuteNonQuery(dbCmd);

                if (Hotel[15].ToString() != "0")
                {
                    db_task = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                    dbCmd_task = db.GetStoredProcCommand("GENERATE_TASK_FOR_TOUR");
                    string check_req_title = "Check Hotel Availibility";
                    string check_req_regarding = "Check rooms in hotel " + Hotel[6];
                    string check_req_type = "Check Hotel Availibility";
                    DateTime start_date = DateTime.Today;
                    DateTime end_date = DateTime.Today.AddDays(5);

                    db_task.AddInParameter(dbCmd_task, "@TITLE", DbType.String, check_req_title);
                    db_task.AddInParameter(dbCmd_task, "@REGARDING", DbType.String, check_req_regarding);
                    db_task.AddInParameter(dbCmd_task, "@TASK_TYPE", DbType.String, check_req_type);
                    db_task.AddInParameter(dbCmd_task, "@ASSIGN_BY", DbType.Int32, Convert.ToInt32(Hotel[20]));
                    db_task.AddInParameter(dbCmd_task, "@ASSIGN_TO", DbType.String, Hotel[15]);
                    db_task.AddInParameter(dbCmd_task, "@START_DATE", DbType.DateTime, start_date);
                    db_task.AddInParameter(dbCmd_task, "@END_DATE", DbType.DateTime, end_date);
                    db_task.AddInParameter(dbCmd_task, "@PRIORITY_ID", DbType.String, "Medium");
                    db_task.AddInParameter(dbCmd_task, "@PRODUCT_CODE", DbType.String, Hotel[1]);
                    db_task.AddInParameter(dbCmd_task, "@STATUS_ID", DbType.String, "Pending");
                    db_task.ExecuteNonQuery(dbCmd_task);
                }

                if (Hotel[14].ToString() != "0")
                {
                    db_task = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                    dbCmd_task = db.GetStoredProcCommand("GENERATE_TASK_FOR_TOUR");
                    string booking_req_title = "Book Hotel";
                    string booking_req_regarding = "Book hotel " + Hotel[6];
                    string booking_req_type = "Hotel Booking";
                    DateTime start_date = DateTime.Today;
                    DateTime end_date = DateTime.ParseExact(Hotel[16].ToString(), "dd/MM/yyyy", null).AddDays(-3);

                    db_task.AddInParameter(dbCmd_task, "@TITLE", DbType.String, booking_req_title);
                    db_task.AddInParameter(dbCmd_task, "@REGARDING", DbType.String, booking_req_regarding);
                    db_task.AddInParameter(dbCmd_task, "@TASK_TYPE", DbType.String, booking_req_type);
                    db_task.AddInParameter(dbCmd_task, "@ASSIGN_BY", DbType.Int32, Convert.ToInt32(Hotel[20]));
                    db_task.AddInParameter(dbCmd_task, "@ASSIGN_TO", DbType.String, Hotel[14]);
                    db_task.AddInParameter(dbCmd_task, "@START_DATE", DbType.DateTime, start_date);
                    db_task.AddInParameter(dbCmd_task, "@END_DATE", DbType.DateTime, end_date);
                    db_task.AddInParameter(dbCmd_task, "@PRIORITY_ID", DbType.String, "Medium");
                    db_task.AddInParameter(dbCmd_task, "@PRODUCT_CODE", DbType.String, Hotel[1]);
                    db_task.AddInParameter(dbCmd_task, "@STATUS_ID", DbType.String, "Pending");
                    db_task.ExecuteNonQuery(dbCmd_task);
                }

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

        public void InsertUpdateRoomAllocation(ArrayList Room)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_HOTEL_ROOM_ALLOCATION");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32,Convert.ToInt32(Room[0]));
                db.AddInParameter(dbCmd, "@BOOKING_HOTEL_SRNO", DbType.Int32, Convert.ToInt32(Room[1]));
                db.AddInParameter(dbCmd, "@ROOM_NO", DbType.Int32, Convert.ToInt32(Room[2]));
                if (Room[3].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@ADULT1", DbType.String, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ADULT1", DbType.String, Room[3]);
                }
                if (Room[4].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@ADULT2", DbType.String, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ADULT2", DbType.String, Room[4]);
                }
                if (Room[5].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@ADULT3", DbType.String, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ADULT3", DbType.String, Room[5]);
                }
                if (Room[6].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@CWB", DbType.String, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CWB", DbType.String, Room[6]);
                }
                if (Room[7].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@CNB1", DbType.String, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CNB1", DbType.String, Room[7]);
                }
                if (Room[8].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@CNB2", DbType.String, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CNB2", DbType.String, Room[8]);
                }
                if (Room[9].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@INFANT", DbType.String, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@INFANT", DbType.String, Room[9]);
                }
                db.AddInParameter(dbCmd, "@ROOM_SHARE", DbType.String, Room[10]);
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

        public void InsertNewHotel(int Tour_Id,int Booking_Detail_Id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_BOOKING_HOTEL");
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

        public void InsertHotelNOSR(int booking_id)
        { 
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_BOOKING_HOTEL_SRNO");
                db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, Convert.ToInt32(booking_id));
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

        public void ConsolidateRooms(int Tour_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GENERATE_HOTEL_WISE_ROOM");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Convert.ToInt32(Tour_id));
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

        public void Finalize(int Tour_id)
        {

            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_BLANK_ROOMS_FOR_ROOM_ALLOCATION");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Convert.ToInt32(Tour_id));
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
