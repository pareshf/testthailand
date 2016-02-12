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
    public class HotelServiceVoucher
    {
        public DataSet fetchDataforHotel(String sp_name, String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        
        public DataSet fetchDataforHotelroomtype(String sp_name, String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        
        public DataSet InsertHotelServiceVoucher(int id,String clientname,String city,String hotel,String roomtype,String arrivaldate,String departuredate,int totalroom,int sroom,int droom,int troom,String agent,String vno,int nights,int userid,String invoiceno)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_HOTEL_SERVICE_VOUCHER");
                db.AddInParameter(dbCmd, "@HOTEL_VOUCHER_ID", DbType.Int32, id);
                db.AddInParameter(dbCmd, "@CLIENT_NAME", DbType.String, clientname);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, city);
                db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, hotel);
                db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, roomtype);
                db.AddInParameter(dbCmd, "@ARRIVAL_DATE", DbType.DateTime, DateTime.ParseExact(arrivaldate.ToString(), "dd/MM/yyyy", null));
                db.AddInParameter(dbCmd, "@DEPARTURE_DATE", DbType.DateTime, DateTime.ParseExact(departuredate.ToString(), "dd/MM/yyyy", null));
                db.AddInParameter(dbCmd, "@TOTAL_ROOMS", DbType.Int32, totalroom);
                db.AddInParameter(dbCmd, "@SINGLE_ROOMS", DbType.Int32, sroom);
                db.AddInParameter(dbCmd, "@DOUBLE_ROOMS", DbType.Int32, droom);
                db.AddInParameter(dbCmd, "@TRIPPLE_ROOMS", DbType.Int32, troom);
                db.AddInParameter(dbCmd, "@AGENT", DbType.String, agent);
                db.AddInParameter(dbCmd, "@VOUCHER_NO", DbType.String, vno);
                db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, nights);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoiceno);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, userid);

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
        
        public DataSet get_cust_rel_sr_no(String sp_name, String company_name)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@AGENT_COMPANY_NAME", DbType.String, company_name);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet get_invoice_left(String sp_name, int USER_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@USERID", DbType.String, USER_ID);
                //    db.AddInParameter(dbCmd, "@AGENT_COMPANY_NAME", DbType.String, company_name);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetchDataforCLIENTNAME(String sp_name, String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        public DataSet fetchDataforsupid(String sp_name, String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@hotel_name", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }


        // OPERATION SHEET ENTRY

        #region INSERT UPDATE HOTEL DATA

        public void insertupdate_Hotels(int hotelcartid, String city_id, String hotel_id, String from_date, String to_date, int no_of_rooms, String order_status, int user_id, int room_type, String room_type_id, String start_date, String end_date, Boolean package_flag, int quote_id)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_HOTEL_CART_DETAIL_MANUAL");

                db.AddInParameter(dbCmd, "@HOTEL_CART_ID", DbType.Int32, hotelcartid);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.String, city_id);
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.String, hotel_id);


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


                db.AddInParameter(dbCmd, "@NO_OF_ROOMS", DbType.Int32, no_of_rooms);

                db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, order_status);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, user_id);

                db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.Int32, room_type);

                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.String, room_type_id);



                if (start_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@START_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@START_DATE", DbType.DateTime, DateTime.ParseExact(start_date.ToString(), "dd/MM/yyyy", null));
                }

                if (end_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@END_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@END_DATE", DbType.DateTime, DateTime.ParseExact(end_date.ToString(), "dd/MM/yyyy", null));
                }

                db.AddInParameter(dbCmd, "@PACKAGE_FLAG", DbType.String, package_flag);

                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quote_id);

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

        #region INSERT UPDATE TRANSFER PACKAGE AND SIGHT SEEING

        public void insert_update_sight_seeing(int serivicecart_id, int package_id, String package_flag, String date, String time, String order_status, int user_id, String no_meal, String package_name, String city_name, int tp_from_to_id, String flag, Boolean package_select_flag, int quote_id)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SERVICE_CART_DETAIL_MANUAL");

                db.AddInParameter(dbCmd, "@SERVICE_CART_ID", DbType.Int32, serivicecart_id);
                db.AddInParameter(dbCmd, "@TRANSFER_SIGHT_SEEING_PACKAGE_ID", DbType.Int32, package_id);
                db.AddInParameter(dbCmd, "@TRANSFER_SIGHT_SEEING_PACKAGE_FLAG", DbType.String, package_flag);
                if (date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
                }

                //if (time.ToString().Equals(""))
                //{
                //    db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse("00:00:00"));
                //}
                //else
                //{
                db.AddInParameter(dbCmd, "@TIME", DbType.String, time);
                //}

                db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, order_status);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, user_id);

                if (no_meal.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_MEALS", DbType.Int32, Convert.ToInt32("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_MEALS", DbType.Int32, Convert.ToInt32(no_meal));
                }

                db.AddInParameter(dbCmd, "@SIGHT_PACKAGE_NAME", DbType.String, package_name);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, city_name);

                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_FROM_TO_DETAIL_ID", DbType.Int32, tp_from_to_id);
                db.AddInParameter(dbCmd, "@SIC_PVT_FLAG", DbType.String, flag);
                db.AddInParameter(dbCmd, "@PACKAGE_FLAG", DbType.Boolean, package_select_flag);

                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quote_id);

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
    }
}
