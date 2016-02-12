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
    public class OrderSummaryStoreProcedure
    {

        #region GET RECORDS FOR ALL SUMMARY

        #region FILL RECORDS IN HOTEL DETAILS
        public DataTable fetchHotelDetails(int userid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_HOTEL_DETAIL_FOR_ORDER_SUMMARY");
                
                db.AddInParameter(dbCmd, "@USER_ID",DbType.Int32,userid);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
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

        #region FILL RECORDS IN CRUISE DETAILS
        public DataTable fetchCruiseDetails(int userid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_CRUISE_DETAIL_FOR_ORDER_SUMMARY");

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userid);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
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

        #region FILL RECORDS IN SIGHT DETAILS
        public DataTable fetchSightseeDetails(int userid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_SIGHT_DETAIL_FOR_ORDER_SUMMARY");

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userid);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
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

        #region FILL RECORDS IN TRANSFER PACKAGE DETAILS
        public DataTable fetchTransferPackageDetails(int userid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_TRANSFER_DETAIL_FOR_ORDER_SUMMARY");

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userid);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
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

        #endregion
        
        #region DELETE RECORDS

        #region DELETE RECORDS FOR HOTEL DETAILS
        public void deleteHotelDetails(int cartorderid, int hotelcartid)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_HOTEL_DETAIL_ORDER_SUMMARY");

                db.AddInParameter(dbCmd, "@CART_ORDER_ID", DbType.Int32, cartorderid);
                db.AddInParameter(dbCmd, "@HOTEL_CART_ID", DbType.Int32, hotelcartid);

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

        #region DELETE RECORDS FOR CRUISE DETAILS
        public void deleteCruiseDetails(int cartorderid, int cruisecartid)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_CRUISE_DETAIL_ORDER_SUMMARY");

                db.AddInParameter(dbCmd, "@CART_ORDER_ID", DbType.Int32, cartorderid);
                db.AddInParameter(dbCmd, "@CRUISE_CART_ID", DbType.Int32, cruisecartid);

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

        #region DELETE RECORDS FOR SIGHT DETAILS
        public void deleteSightDetails(int cartorderid, int servicecartid)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_SIGHT_DETAIL_ORDER_SUMMARY");

                db.AddInParameter(dbCmd, "@CART_ORDER_ID", DbType.Int32, cartorderid);
                db.AddInParameter(dbCmd, "@SERVICE_CART_ID", DbType.Int32, servicecartid);

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

        #region DELETE RECORDS FOR TRANSFER DETAILS
        public void deleteTransferDetails(int cartorderid, int servicecartid)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_TRANSFER_DETAIL_ORDER_SUMMARY");

                db.AddInParameter(dbCmd, "@CART_ORDER_ID", DbType.Int32, cartorderid);
                db.AddInParameter(dbCmd, "@SERVICE_CART_ID", DbType.Int32, servicecartid);

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

        #endregion

        #region UPDATE RECORDS

        #region UPADTE HOTEL RECORDS
        public void updateHotelDetails(int hotelid, String from_date, String to_date, int noofrooms)
        {
             Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_HOTEL_DETAILS_SUMMARY");

                db.AddInParameter(dbCmd, "@HOTEL_CART_ID",DbType.Int32, hotelid);
                if (from_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(from_date.ToString(), "dd/MM/yyyy", null));
                }

                if (to_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
                }
               // db.AddInParameter(dbCmd, "@FROM_DATE",DbType.DateTime, DateTime.ParseExact(from_date.ToString(), "dd/MM/yyyy", null));
               // db.AddInParameter(dbCmd, "@TO_DATE",DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
                db.AddInParameter(dbCmd, "@NO_OF_ROOMS",DbType.Int32, noofrooms);

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

        #region UPADTE CRUISE RECORDS
        public void updateCruiseDetails(int cruiseid, String from_date, String to_date, int noofrooms)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_CRUISE_DETAILS_SUMMARY");

                db.AddInParameter(dbCmd, "@CRUISE_CART_ID", DbType.Int32, cruiseid);
                if (from_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(from_date.ToString(), "dd/MM/yyyy", null));
                }
                 
                if (to_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
                }
            //    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(from_date.ToString(), "dd/MM/yyyy", null));
           //     db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
                db.AddInParameter(dbCmd, "@NO_OF_ROOMS", DbType.Int32, noofrooms);

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

        #region UPADTE SIGHT RECORDS
        public void updateSightDetails(int sightid, String date, String time)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_SERVICE_DETAILS_SUMMARY");

                db.AddInParameter(dbCmd, "@SERVICE_CART_ID", DbType.Int32, sightid);

                if (date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
                }

                if (time.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse("00:00:00"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse(time.ToString()));
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
        #endregion

        #region UPADTE TRANSFER RECORDS
        public void updateTransferDetails(int sightid, String date, String time)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_TRANSFER_DETAILS_SUMMARY");

                db.AddInParameter(dbCmd, "@SERVICE_CART_ID", DbType.Int32, sightid);
            //    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));

                if (date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
                }

                if (time.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse("00:00:00"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse(time.ToString()));
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
        #endregion

        #endregion

        #region GET QUOTE INSERT UPADTE 
        public void insertupadtegetquotes(String tour_id,String tourshortname, String tourfromdate, String tourtodate, String noofdays, String noofnights, String createdbysession, String no_of_adults, String no_of_cwb, String no_of_cnb, String no_of_infant, String arrival_tiem, String departure_time, String arrival_flight, String departure_flight, String clientname, String remarks)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FIT_TOUR_GENERATE");

                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Convert.ToInt32(tour_id));
                db.AddInParameter(dbCmd, "@TOUR_SHORT_NAME", DbType.String, tourshortname);
                db.AddInParameter(dbCmd, "@TOUR_SUB_TYPE_ID", DbType.Int32, Convert.ToInt32("1"));


                if (tourfromdate.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOUR_FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOUR_FROM_DATE", DbType.DateTime, DateTime.ParseExact(tourfromdate.ToString(), "dd/MM/yyyy", null));
                }

                if (tourtodate.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TOUR_TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOUR_TO_DATE", DbType.DateTime, DateTime.ParseExact(tourtodate.ToString(), "dd/MM/yyyy", null));
                }

                if (noofdays.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_DAYS", DbType.Int32, Convert.ToInt32("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_DAYS", DbType.Int32, Convert.ToInt32(noofdays));
                }


                if (noofnights.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, Convert.ToInt32("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, Convert.ToInt32(noofnights));
                }
                db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, Convert.ToInt32("24"));
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, Convert.ToInt32(createdbysession));

                if (no_of_adults.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, Convert.ToInt32("0"));
                }
                else
                {

                    db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, Convert.ToInt32(no_of_adults));
                }
                //db.AddInParameter(dbCmd, "@NO_OF_CHILD", DbType.Int32, Convert.ToInt32(no_of_child));
                if (no_of_cwb.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32, Convert.ToInt32("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32, Convert.ToInt32(no_of_cwb));
                }

                if (no_of_cnb.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32, Convert.ToInt32("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32, Convert.ToInt32(no_of_cnb));
                }

                if (no_of_infant.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, Convert.ToInt32("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, Convert.ToInt32(no_of_infant));
                }

                if (arrival_tiem.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, DateTime.Parse("00:00:00"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, DateTime.Parse(arrival_tiem.ToString()));
                }

                if (departure_time.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DEPARTURE_TIME", DbType.DateTime, DateTime.Parse("00:00:00"));
                }
                else
                {
                     db.AddInParameter(dbCmd, "@DEPARTURE_TIME", DbType.DateTime, DateTime.Parse(departure_time.ToString()));
                }

                db.AddInParameter(dbCmd, "@ARRIVAL_FLIGHT", DbType.String, arrival_flight);
                db.AddInParameter(dbCmd, "@DEPARTURE_FLIGHT", DbType.String, departure_flight);

                db.AddInParameter(dbCmd, "@CLIENT_NAME", DbType.String, clientname);
                db.AddInParameter(dbCmd, "@REMARKS", DbType.String, remarks);

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

        #region GENERATE QUOTE FOR HOTEL
        public DataTable generate_quote_for_hotel(int noofadult, int noofcwb, int noofcnb, int noofinfant, int userid, String orderstatus)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GENERATE_QUOTE_FOR_HOTEL");

                db.AddInParameter(dbCmd, "@NO_OF_ADULT", DbType.Int32, noofadult );
                db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32, noofcwb );
                db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32, noofcnb );
                db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, noofinfant );
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userid );
                db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, orderstatus);

                ds = db.ExecuteDataSet(dbCmd);
              //  db.ExecuteNonQuery(dbCmd);

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

        #region  FETCH ORDER STAUTS RAD COMBO BOX
        public DataSet fetchComboData(String sp_name, String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        #endregion

        public DataSet  fetchno_of_rooms(int userid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_ROOM_TYPE");

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userid);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
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

        public DataSet fetch_dates(int userid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_DATE");

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userid);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
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
         //   return ds.Tables[1];
        }

        public DataTable fetch_no_of_rooms_for_persons(int user_id, int supplier_hotel_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_TOTAL_NO_OF_ROOMS_HOTEL_WISE");

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, user_id);
                db.AddInParameter(dbCmd, "@SUPPLIER_HOTEL_ID", DbType.Int32, supplier_hotel_id);
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

        public DataTable fetch_city_names(int userid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_CITY_NAME_FROM_CART");

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userid);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
                //db.AddInParameter(dbCmd, "",DbType.Int32,);
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
    }
}
