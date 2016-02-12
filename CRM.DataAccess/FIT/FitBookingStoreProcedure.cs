using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;

namespace CRM.DataAccess.FIT
{
    public class FitBookingStoreProcedure
    {
        #region Variable Declaration
        String Order_Status = "In Cart";
        #endregion

        public DataTable fetchHotelDetailForSearch(String from_date, String to_date, String city, String serach_param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_HOTEL_DETAIL_FOR_CART_SEARCH");
                db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(from_date.ToString(), "dd/MM/yyyy", null));
                db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
                db.AddInParameter(dbCmd, "@CITY", DbType.String, city);
                db.AddInParameter(dbCmd, "@SEARCH_PARAM", DbType.String, serach_param);
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

        public DataTable fetchCruiseDetailForSearch(String from_date, String to_date, String city, String serach_param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_CRUISE_DETAIL_FOR_CART_SEARCH");
                db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(from_date.ToString(), "dd/MM/yyyy", null));
                db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
                db.AddInParameter(dbCmd, "@CITY", DbType.String, city);
                db.AddInParameter(dbCmd, "@SEARCH_PARAM", DbType.String, serach_param);
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

        public DataTable fetchSightDetailForSearch(String from_date, String to_date, String city, String serach_param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_SIGHT_SEEING_DETAIL_FOR_CART_SEARCH");
                db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(from_date.ToString(), "dd/MM/yyyy", null));
                db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
                db.AddInParameter(dbCmd, "@CITY", DbType.String, city);
                db.AddInParameter(dbCmd, "@SEARCH_PARAM", DbType.String, serach_param);
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

        public DataTable fetchTransferDetailForSearch(String from_date, String to_date, String city)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_TRANSFER_DETAIL_FOR_CART_SEARCH");
                db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(from_date.ToString(), "dd/MM/yyyy", null));
                db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
                db.AddInParameter(dbCmd, "@CITY", DbType.String, city);

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

        public void insertUpdateHotelIntoCart(String city, int pk, String from_date, String to_date, String no_of_room, String user_id,String room_type)
        {
            Database db = null;
            DbCommand dbCmd = null;
            

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_HOTEL_CART_DETAIL");
                
                db.AddInParameter(dbCmd, "@HOTEL_CART_ID", DbType.Int32,Convert.ToInt32(0));
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.String,city);
                db.AddInParameter(dbCmd, "@SUPPLIER_HOTEL_PRICE_LIST_ID", DbType.Int32,Convert.ToInt32(pk));
                if(from_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(from_date.ToString(), "dd/MM/yyyy", null));
                }
                if(to_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@NO_OF_ROOMS", DbType.Int32, Convert.ToInt32(no_of_room));
                db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String,Order_Status);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, Convert.ToInt32(user_id));
                db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.Int32, Convert.ToInt32(room_type));

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

        public void insertUpdateCartOrderDetail(String table_id, String table_flag, String user_id)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CART_ORDER_DETAIL");

                db.AddInParameter(dbCmd, "@CART_ORDER_ID", DbType.Int32, Convert.ToInt32(0));
                db.AddInParameter(dbCmd, "@ORDER_NO", DbType.Int32, Convert.ToInt32(0));
                db.AddInParameter(dbCmd, "@CART_TABLE_ID", DbType.Int32, Convert.ToInt32(table_id));
                db.AddInParameter(dbCmd, "@CART_TABLE_FLAG", DbType.String, table_flag);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, Convert.ToInt32(user_id));

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

        public DataTable fetchHotalCartData(String user_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_HOTEL_DETAIL_FOR_ORDER_SUMMARY");
                db.AddInParameter(dbCmd, "@USER_ID", DbType.String, Convert.ToInt32(user_id));

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

        public DataTable fetchMaxHotelCartSrNo(String user_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_MAX_HOTEL_CART_DETAIL");
                db.AddInParameter(dbCmd, "@USER_ID", DbType.String, Convert.ToInt32(user_id));

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

        public void insertUpdateCruiseIntoCart(String city, int pk, String from_date, String to_date, String no_of_room, String user_id, String room_type)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CRUISE_CART_DETAIL");

                db.AddInParameter(dbCmd, "@CRUISE_CART_ID", DbType.Int32, Convert.ToInt32(0));
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.String, city);
                db.AddInParameter(dbCmd, "@SUPPLIER_CRUISE_PRICE_LIST_ID", DbType.Int32, Convert.ToInt32(pk));
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
                db.AddInParameter(dbCmd, "@NO_OF_ROOMS", DbType.Int32, Convert.ToInt32(no_of_room));
                db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, Order_Status);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, Convert.ToInt32(user_id));
                db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.Int32, Convert.ToInt32(room_type));

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

        public DataTable fetchMaxCruiseCartSrNo(String user_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_MAX_CRUISE_CART_DETAIL");
                db.AddInParameter(dbCmd, "@USER_ID", DbType.String, Convert.ToInt32(user_id));

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

        public DataTable fetchCruiseCartData(String user_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_CRUISE_DETAIL_FOR_ORDER_SUMMARY");
                db.AddInParameter(dbCmd, "@USER_ID", DbType.String, Convert.ToInt32(user_id));

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

        public void insertUpdateSightIntoCart(int pk, String date, String time, String user_id)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SERVICE_CART_DETAIL");

                db.AddInParameter(dbCmd, "@SERVICE_CART_ID", DbType.Int32, Convert.ToInt32(0));
                db.AddInParameter(dbCmd, "@TRANSFER_SIGHT_SEEING_PACKAGE_ID", DbType.Int32, Convert.ToInt32(pk));
                db.AddInParameter(dbCmd, "@TRANSFER_SIGHT_SEEING_PACKAGE_FLAG", DbType.String, "SIGHT");
                if (date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse(time));
                db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, Order_Status);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, Convert.ToInt32(user_id));

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

        public DataTable fetchMaxServiceCartSrNo(String user_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_MAX_SERVICE_CART_DETAIL");
                db.AddInParameter(dbCmd, "@USER_ID", DbType.String, Convert.ToInt32(user_id));

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

        public void insertUpdateTransferIntoCart(int pk, String date, String time, String user_id)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SERVICE_CART_DETAIL");

                db.AddInParameter(dbCmd, "@SERVICE_CART_ID", DbType.Int32, Convert.ToInt32(0));
                db.AddInParameter(dbCmd, "@TRANSFER_SIGHT_SEEING_PACKAGE_ID", DbType.Int32, Convert.ToInt32(pk));
                db.AddInParameter(dbCmd, "@TRANSFER_SIGHT_SEEING_PACKAGE_FLAG", DbType.String, "TRANSFER");
                if (date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse(time));
                db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, Order_Status);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, Convert.ToInt32(user_id));

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

        public DataTable fetchSightCartData(String user_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_SIGHT_DETAIL_FOR_ORDER_SUMMARY");
                db.AddInParameter(dbCmd, "@USER_ID", DbType.String, Convert.ToInt32(user_id));

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

        public DataTable fetchTransferCartData(String user_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_TRANSFER_DETAIL_FOR_ORDER_SUMMARY");
                db.AddInParameter(dbCmd, "@USER_ID", DbType.String, Convert.ToInt32(user_id));

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
