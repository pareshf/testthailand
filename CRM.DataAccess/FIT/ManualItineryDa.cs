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
    public class ManualItineryDa
    {
        /* COMMON SP WITHOUT PARAMETERS */
        public DataSet CommonSp(string SP_NAME)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(SP_NAME);



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

        public DataSet fetchInvoice(String SPNAME, int CUST_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(SPNAME);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, CUST_ID);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsData;
        }

        public DataSet fetchInvoiceDetail(String SPNAME, string INVOICE)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(SPNAME);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, INVOICE);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsData;
        }

        /* HOTELS */
        public DataSet fetchComboDataforHotelroomtype(String sp_name, String param, String CITY_NAME)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, param);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, CITY_NAME);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetchComboDataforHotel(String SPNAME, String CITY_NAME)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(SPNAME);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, CITY_NAME);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }


        /* Insert Main, Hotel, schedule and passenger Detail */

        public DataSet InsertMainDetails(int MANUAL_ITINERY_ID, string CUST_ID, String Adate, String Ddate, String ARRIVAL_FLIGHT, String atime, String DEPARTURE_FLIGHT, String Dtime,
            String INVOICE_NO, String PAX_NAME, String NO_OF_ADULTS, String NO_OF_CHILD, String NO_OF_CWB, String NO_OF_CNB, String NO_OF_INFANTS, String NO_OF_SINGLE_ROOM, String NO_OF_DOUBLE_ROOM, String NO_OF_TRIPLE_ROOM, String MEETING_POINT, String REMARKS)
        {
            Database db = null;
            DataSet ds = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_MAIN_MANUAL_ITINERY");

                db.AddInParameter(dbCmd, "@MANUAL_ITINERY_ID", DbType.Int32, MANUAL_ITINERY_ID);

                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, int.Parse(CUST_ID));

                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, INVOICE_NO);

                db.AddInParameter(dbCmd, "@PAX_NAME", DbType.String, PAX_NAME);

                db.AddInParameter(dbCmd, "@ARRIVAL_FLIGHT", DbType.String, ARRIVAL_FLIGHT);

                if (Adate.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ARRIVAL_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ARRIVAL_DATE", DbType.DateTime, DateTime.ParseExact(Adate.ToString(), "dd/MM/yyyy", null));
                }

                if (atime.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, DateTime.Parse("00:00:00"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, DateTime.Parse(atime.ToString()));
                }

                db.AddInParameter(dbCmd, "@DEPARTURE_FLIGHT", DbType.String, DEPARTURE_FLIGHT);

                if (Ddate.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DEPARTURE_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DEPARTURE_DATE", DbType.DateTime, DateTime.ParseExact(Ddate.ToString(), "dd/MM/yyyy", null));
                }

                if (Dtime.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DEPARTURE_TIME", DbType.DateTime, DateTime.Parse("00:00:00"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DEPARTURE_TIME", DbType.DateTime, DateTime.Parse(Dtime.ToString()));
                }

                if (NO_OF_ADULTS.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, int.Parse(NO_OF_ADULTS));
                }

                if (NO_OF_CHILD.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CHILD", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CHILD", DbType.Int32, int.Parse(NO_OF_CHILD));
                }

                if (NO_OF_CWB.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32, int.Parse(NO_OF_CWB));
                }

                if (NO_OF_CNB.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32, int.Parse(NO_OF_CNB));
                }

                if (NO_OF_INFANTS.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, int.Parse(NO_OF_INFANTS));
                }
                if (NO_OF_SINGLE_ROOM.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_SINGLE_ROOM", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_SINGLE_ROOM", DbType.Int32, int.Parse(NO_OF_SINGLE_ROOM));
                }

                if (NO_OF_DOUBLE_ROOM.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_DOUBLE_ROOM", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_DOUBLE_ROOM", DbType.Int32, int.Parse(NO_OF_DOUBLE_ROOM));
                }
                if (@NO_OF_TRIPLE_ROOM.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_TRIPLE_ROOM", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_TRIPLE_ROOM", DbType.Int32, int.Parse(@NO_OF_TRIPLE_ROOM));
                }

                db.AddInParameter(dbCmd, "@MEETING_POINT", DbType.String, MEETING_POINT);

                db.AddInParameter(dbCmd, "@REMARKS", DbType.String, REMARKS);

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

        public void InsertHotelDetails(int HOTEL_ITINERY_ID, int MANUAL_ITINERY_ID, String CITY_ID, String HOTEL_ID, String ROOM_TYPE_ID, String CHECKIN, String CHECKOUT)
        {
            Database db = null;
            DataSet ds = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_HOTEL_MANUAL_ITINERY");

                db.AddInParameter(dbCmd, "@HOTEL_ITINERY_ID", DbType.Int32, HOTEL_ITINERY_ID);
                db.AddInParameter(dbCmd, "@MANUAL_ITINERY_ID", DbType.Int32, MANUAL_ITINERY_ID);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.String, CITY_ID);
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.String, HOTEL_ID);
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.String, ROOM_TYPE_ID);
                if (CHECKIN.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CHECKIN", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHECKIN", DbType.DateTime, DateTime.ParseExact(CHECKIN.ToString(), "dd/MM/yyyy", null));
                }
                if (CHECKOUT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CHECKOUT", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHECKOUT", DbType.DateTime, DateTime.ParseExact(CHECKOUT.ToString(), "dd/MM/yyyy", null));
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

        public void InsertScheduleDetails(int SCHEDULE_ITINERY_ID, int MANUAL_ITINERY_ID, String CITY_ID, String TSM, String SICPVT, String SIGNATURE, String SCHEDULE_DATE, String PICKTIME)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SCHEDULE_MANUAL_ITINERY");

                db.AddInParameter(dbCmd, "@SCHEDULE_ITINERY_ID", DbType.Int32, SCHEDULE_ITINERY_ID);
                db.AddInParameter(dbCmd, "@MANUAL_ITINERY_ID", DbType.Int32, MANUAL_ITINERY_ID);
                if (SCHEDULE_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@SCHEDULE_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@SCHEDULE_DATE", DbType.DateTime, DateTime.ParseExact(SCHEDULE_DATE.ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@TSM", DbType.String, TSM);
                if (PICKTIME.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@PICKTIME", DbType.DateTime, DateTime.Parse("00:00:00"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PICKTIME", DbType.DateTime, DateTime.Parse(PICKTIME.ToString()));
                }

                db.AddInParameter(dbCmd, "@SICPVT", DbType.String, SICPVT);
                db.AddInParameter(dbCmd, "@SIGNATURE", DbType.String, SIGNATURE);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.String, CITY_ID);
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

        public void InsertPassengerDetails(int TRAVELLING_PASSENGERS_ID, int MANUAL_ITINERY_ID, String NAME, String PASSOPRT_NO, String NATIONALITY_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_TRAVELLING_PASSENGERS_FOR_MANUAL_ITINERY");

                db.AddInParameter(dbCmd, "@TRAVELLING_PASSENGERS_ID", DbType.Int32, TRAVELLING_PASSENGERS_ID);
                db.AddInParameter(dbCmd, "@MANUAL_ITINERY_ID", DbType.Int32, MANUAL_ITINERY_ID);
                db.AddInParameter(dbCmd, "@NAME", DbType.String, NAME);
                db.AddInParameter(dbCmd, "@PASSOPRT_NO", DbType.String, PASSOPRT_NO);
                db.AddInParameter(dbCmd, "@NATIONALITY_ID", DbType.String, NATIONALITY_ID);

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


        /* fetch Main, Hotel, schedule and passenger Detail */

        public DataSet FetchAllDetail(String SPNAME, int id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(SPNAME);
                db.AddInParameter(dbCmd, "@MANUAL_ITINERY_ID", DbType.String, id);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsData;
        }


        public void  DeleteHotelItinery(String SPNAME, int hotel_itinery_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(SPNAME);
                db.AddInParameter(dbCmd, "@HOTEL_ITINERY_ID", DbType.Int32, hotel_itinery_id);
                db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        public void DeleteScheduleItinery(String SPNAME, int SCHEDULE_ITINERY_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(SPNAME);
                db.AddInParameter(dbCmd, "@SCHEDULE_ITINERY_ID", DbType.Int32, SCHEDULE_ITINERY_ID);
                db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        public void DeletePasslItinery(String SPNAME, int TRAVELLING_PASSENGERS_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(SPNAME);
                db.AddInParameter(dbCmd, "@TRAVELLING_PASSENGERS_ID", DbType.Int32, TRAVELLING_PASSENGERS_ID);
                db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /* SEARCH GRID*/

        public DataSet fetchallDatasearch(String sp_name, String CUST_ID, String INVOICE, String Adate, String Ddate)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                if (CUST_ID != "")
                {
                    db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, int.Parse(CUST_ID));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, 0);
                }
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, INVOICE);
                if (Adate.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ARRIVAL_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ARRIVAL_DATE", DbType.DateTime, DateTime.ParseExact(Adate.ToString(), "dd/MM/yyyy", null));
                }
                if (Ddate.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DEPARTURE_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DEPARTURE_DATE", DbType.DateTime, DateTime.ParseExact(Ddate.ToString(), "dd/MM/yyyy", null));
                }

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
    }
}
