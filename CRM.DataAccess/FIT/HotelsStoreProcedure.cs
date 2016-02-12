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
   public  class HotelsStoreProcedure
   {
       #region SHOW FORM
       public DataTable fetchpacageid(int userid)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_FIT_PACKAGE_CITY");

               db.AddInParameter(dbCmd, "@FIT_PACKAGE_ID", DbType.Int32, userid);
              
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


       #region SIGHT SEEING
       public DataTable fetchsightseeing(String from_date, String to_date, String city)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_SIGHT_SEEING_FOR_FIT");

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
       #endregion

       #region DROPDOWN OF TELERIK

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

       public DataSet fetchComboDataforHotel(String sp_name, String param, String from_date, String to_date)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, param);

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

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

       public DataSet fetchComboPriceforHotel(String sp_name, String param, String from_date, String to_date,int custType)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, param);

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

               db.AddInParameter(dbCmd, "@CUST_TYPE", DbType.Int32, custType);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       public DataSet fetchComboDataforHotel_Star(String sp_name, String param, String from_date, String to_date, String Hotel_name)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, param);

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
               db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, Hotel_name);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       public DataSet fetchComboDataforHotelroomtype(String sp_name, String param, String from_date, String to_date)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, param);

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

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }
        #endregion

       #region FETCH TRANSFER PACKAGE

       public DataTable fetchTransferPackage(String from_date, String to_date, String atime, String Dtime,String packid )
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_TRANSFER_PACKAGE_FOR_FIT");

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

               if (atime.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, DateTime.Parse("00:00:00"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, DateTime.Parse(atime.ToString()));
               }

               if (Dtime.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DEP_TIME", DbType.DateTime, DateTime.Parse("00:00:00"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DEP_TIME", DbType.DateTime, DateTime.Parse(Dtime.ToString()));
               }
               db.AddInParameter(dbCmd, "@FIT_PACKAGE_ID", DbType.Int32, Convert.ToInt32(packid)); 
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

       public DataTable fetchTransferPackage_DEMO(String from_date, String to_date, String atime, String Dtime, String packid)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_TRANSFER_PACKAGE_FOR_GIT_DEMO");

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

               if (atime.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, DateTime.Parse("00:00:00"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, DateTime.Parse(atime.ToString()));
               }

               if (Dtime.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DEP_TIME", DbType.DateTime, DateTime.Parse("00:00:00"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DEP_TIME", DbType.DateTime, DateTime.Parse(Dtime.ToString()));
               }
               db.AddInParameter(dbCmd, "@FIT_PACKAGE_ID", DbType.Int32, Convert.ToInt32(packid));
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

       #region INSERT UPDATE HOTEL DATA

       public void insertupdate_Hotels(int hotelcartid, String city_id, String hotel_id, String from_date, String to_date, int no_of_rooms, String order_status, int user_id, int room_type, String room_type_id, String start_date, String end_date, Boolean package_flag)
       {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_HOTEL_CART_DETAIL");

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

                 db.AddInParameter(dbCmd,  "@ROOM_TYPE", DbType.Int32, room_type);

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

       public void insert_update_sight_seeing(int serivicecart_id, int package_id, String package_flag, String date, String time, String order_status, int user_id, String no_meal, String package_name, String city_name, int tp_from_to_id, String flag, Boolean package_select_flag)
       {
           Database db = null;
           DbCommand dbCmd = null;


           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SERVICE_CART_DETAIL");

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

       #region GENERATE QUOTE

       #region GET QUOTE INSERT UPADTE
       public DataTable insertupadtegetquotes(String tour_id, String tourshortname, String tourfromdate, String tourtodate, String noofdays, String noofnights, String createdbysession, String no_of_adults, String no_of_cwb, String no_of_cnb, String no_of_infant, String arrival_tiem, String departure_time, String arrival_flight, String departure_flight, String clientname, String remarks, int package_id, Boolean favourite, String last_name, String title, String  fit_package_flag)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

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
                   db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, DateTime.Parse(arrival_tiem.ToString()));
               }

               if (departure_time.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DEPARTURE_TIME", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DEPARTURE_TIME", DbType.DateTime, DateTime.Parse(departure_time.ToString()));
               }

               db.AddInParameter(dbCmd, "@ARRIVAL_FLIGHT", DbType.String, arrival_flight);
               db.AddInParameter(dbCmd, "@DEPARTURE_FLIGHT", DbType.String, departure_flight);

               db.AddInParameter(dbCmd, "@CLIENT_NAME", DbType.String, clientname);
               db.AddInParameter(dbCmd, "@REMARKS", DbType.String, remarks);
               db.AddInParameter(dbCmd, "@FAVOURITE_PACKAGE", DbType.Boolean, favourite);

               db.AddInParameter(dbCmd, "@FIT_PACKAGE_ID", DbType.Int32, package_id);

               db.AddInParameter(dbCmd, "@CLIENT_LASTNAME", DbType.String, last_name);
               db.AddInParameter(dbCmd, "@CLIENT_TITLE", DbType.String, title);

               db.AddInParameter(dbCmd, "@PACKAGE_FLAG", DbType.String, fit_package_flag);

               ds=db.ExecuteDataSet(dbCmd);
               
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

       #region FINAL PDF QUOTE

       public DataTable generate_quote_for_hotel(int noofadult, int noofcwb, int noofcnb, int noofinfant, int userid, String orderstatus, int quote_id, int tour_id, int packageid)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("GENERATE_QUOTE_FOR_HOTEL_TESTING");
               //dbCmd = db.GetStoredProcCommand("GENERATE_QUOTE_FOR_HOTEL_TESTING_THB");

               db.AddInParameter(dbCmd, "@NO_OF_ADULT", DbType.Int32, noofadult);
               db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32, noofcwb);
               db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32, noofcnb);
               db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, noofinfant);
               db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userid);
               db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, orderstatus);
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quote_id);
               db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tour_id);
               db.AddInParameter(dbCmd, "@FIT_PACKAGE_ID", DbType.Int32, packageid);
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

       //#region FINAL PDF QUOTE for THB

       //public DataTable generate_quote_for_hotel_thb(int noofadult, int noofcwb, int noofcnb, int noofinfant, int userid, String orderstatus, int quote_id, int tour_id, int packageid)
       //{
       //    Database db = null;
       //    DbCommand dbCmd = null;
       //    DataSet ds = null;
       //    try
       //    {
       //        db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
       //       // dbCmd = db.GetStoredProcCommand("GENERATE_QUOTE_FOR_HOTEL_TESTING");
       //        dbCmd = db.GetStoredProcCommand("GENERATE_QUOTE_FOR_HOTEL_TESTING_THB");

       //        db.AddInParameter(dbCmd, "@NO_OF_ADULT", DbType.Int32, noofadult);
       //        db.AddInParameter(dbCmd, "@NO_OF_CWB", DbType.Int32, noofcwb);
       //        db.AddInParameter(dbCmd, "@NO_OF_CNB", DbType.Int32, noofcnb);
       //        db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, noofinfant);
       //        db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userid);
       //        db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, orderstatus);
       //        db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quote_id);
       //        db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tour_id);
       //        db.AddInParameter(dbCmd, "@FIT_PACKAGE_ID", DbType.Int32, packageid);
       //        ds = db.ExecuteDataSet(dbCmd);
       //        //  db.ExecuteNonQuery(dbCmd);

       //    }


       //    catch (Exception ex)
       //    {
       //        bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
       //        if (rethrow)
       //        {
       //            throw ex;
       //        }
       //    }
       //    finally
       //    {
       //        DALHelper.Destroy(ref dbCmd);

       //    }
       //    return ds.Tables[0];
       //}

       //#endregion

       public DataSet  getdate(String sightname, String to_date, String city)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_TIME_OF_SIGHT_SEEING_ON_DATE");


               db.AddInParameter(dbCmd, "@SIGHT_NAME", DbType.String, sightname);
             

               if (to_date.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
               }
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
           return ds;
       }

       #region SACHIN

       public DataSet FetchForFitHotelEdit(String sp_name, String param)
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

       public DataTable fetchsiteseeingPackageforedit(String sp_name, String param)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(param));
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

       public DataTable fetchTransferPackageforedit(String sp_name, String param)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(param));
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

       public DataSet insertreconfirmationdate(String sp_name, String quoteid, String reconfirmdate, String city, String staus, String confirmno, String paymentdate)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(quoteid));
               db.AddInParameter(dbCmd, "@HOTEL_STATUS", DbType.String, staus);
               db.AddInParameter(dbCmd, "@CONFIRMATION_NUMBER", DbType.String, confirmno);
               db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, city);
               if (reconfirmdate.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@RECONFIRMATION_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@RECONFIRMATION_DATE", DbType.DateTime, DateTime.ParseExact(reconfirmdate.ToString(), "dd/MM/yyyy", null));
               }
               if (paymentdate.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime, DateTime.ParseExact(paymentdate.ToString(), "dd/MM/yyyy", null));
               }
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

       public void insertreconfirmationdateQUOTE(String sp_name, String quoteid, String reconfirmdate,String orderstatus)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(quoteid));
               if (reconfirmdate.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@RECONFIRMATION_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@RECONFIRMATION_DATE", DbType.DateTime, DateTime.ParseExact(reconfirmdate.ToString(), "dd/MM/yyyy", null));
               }
               db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, orderstatus);
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

       public DataTable objfetchusername(String sp_name,String param)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, int.Parse(param));
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

       public DataTable fetchuserid(String sp_name, String param)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, int.Parse(param));
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

       public DataTable fetchorderstatusname(String sp_name, String param)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@order_id", DbType.Int32, int.Parse(param));
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

       public DataTable fetchemailusingRoleid(String sp_name, String param)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@ROLE_ID", DbType.Int32, int.Parse(param));
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

       public DataTable fetchemailusingqUOTEid(String sp_name, String param)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(param));
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

       public DataTable fetchAUTHORISATIONnum(String sp_name, String param, String param1)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(param));
               db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, param1);
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

       public DataTable fetchagentdetailsforsendrequest(String sp_name, String param)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(param));
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

       public DataTable fetcholddetailsfortobe(String sp_name, String param)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(param));
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


       #region UPDATE QUOTE MASTER

       public void update_quote(int quoteid, String order_satus)
       {
           Database db = null;
           DbCommand dbCmd = null;


           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_FIT_TOUR_QUOTE");

               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quoteid);
               db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, order_satus);
               //db.AddInParameter(dbCmd, "@BOOK_EMAIL_TO_BACKOFFICE", DbType.Int32, book_email);

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

       public void update_INVOICE(String INVOICE_NO, String order_satus)
       {
           Database db = null;
           DbCommand dbCmd = null;


           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_INVOICE_STATUS_ON_CANCEL");

               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, INVOICE_NO);
               db.AddInParameter(dbCmd, "@STATUS", DbType.String, order_satus);
               //db.AddInParameter(dbCmd, "@BOOK_EMAIL_TO_BACKOFFICE", DbType.Int32, book_email);

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

       public void Update_Email_On_Book(int quoteid, int email_id)
       {
           Database db = null;
           DbCommand dbCmd = null;


           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_FIT_TOUR_BOOK_EMAIL");

               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quoteid);
               db.AddInParameter(dbCmd, "@BOOK_EMAIL_TO_BACKOFFICE", DbType.Int32, email_id);
               //db.AddInParameter(dbCmd, "@BOOK_EMAIL_TO_BACKOFFICE", DbType.Int32, book_email);

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

       public void update_quote_tour_favorite(int quoteid, Boolean pack)
       {
           Database db = null;
           DbCommand dbCmd = null;


           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_SAVE_FAVOURITE_PACKAGE");

               db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, quoteid);
               db.AddInParameter(dbCmd, "@PACKAGE", DbType.Boolean, pack);

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

       public DataTable update_quote_for_backoffice(int quoteid, String backoffice)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_FIT_TOUR_QUOTE_FOR_BACKOFFICE_EMAIL");

               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quoteid);
               db.AddInParameter(dbCmd, "@EMAIL", DbType.Int32, int.Parse(backoffice));
               ds=db.ExecuteDataSet(dbCmd);
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

       public DataTable FETCH_quote_for_backoffice()
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_EMAIL_TOUR_QUOTE_FOR_BACKOFFICE_EMAIL");
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

       public DataTable Fetch_Email_From_BackOffice()
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_BACKOFFICE_EMAIL");
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

       
       public DataTable fetch_backoffice_for_book(String quote_id)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_EMAIL_ID_FOR_BACKOFFICE_EMAIL");
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(quote_id));
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

       public DataTable FETCH_EMP_NAME_oF_backoffice(string EMAIL)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_EMPLOYEE_NAME_FROM_SY_USERNAE");
               db.AddInParameter(dbCmd, "@EMAIL", DbType.String, EMAIL);
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

       #region get_time_transfer

       public DataSet transfer_gettime(String sp_name, int tp_form_to_id, String flg, String sic_pvt)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);

               db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_FROM_TO_DETAIL_ID", DbType.Int32, tp_form_to_id);
               db.AddInParameter(dbCmd, "@FLAG", DbType.String, flg);
               db.AddInParameter(dbCmd, "@SIC_PVT", DbType.String, sic_pvt);
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

       #endregion

       #region sachin 06-03-2012

       public DataSet insertstatusforwaitlist(String sp_name, String quoteid, String city, String staus)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(quoteid));
               db.AddInParameter(dbCmd, "@HOTEL_STATUS", DbType.String, staus);
               db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, city);
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

       public void updatetransferpackage(String package_flag, int user_id, String FEES, String QUOTE,String PRICEID)
       {
           Database db = null;
           DbCommand dbCmd = null;


           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_SERVICE_CART_FOR_CANCELLATION");

               db.AddInParameter(dbCmd, "@TRANSFER_SIGHT_SEEING_PACKAGE_FLAG", DbType.String, package_flag);
               
               db.AddInParameter(dbCmd, "@PRICE_ID", DbType.Int32, int.Parse(PRICEID));
               db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, user_id);

               
               if (FEES == "")
               {
                   db.AddInParameter(dbCmd, "@FEES", DbType.Decimal, Convert.ToDecimal(0));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@FEES", DbType.Decimal, Convert.ToDecimal(FEES));
               }
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(QUOTE));

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

       public void updatetransferpackagefordate(String package_flag, int user_id, String date, String QUOTE)
       {
           Database db = null;
           DbCommand dbCmd = null;


           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_SERVICE_CART_FOR_PAYMENT_DUE_DATE");

               db.AddInParameter(dbCmd, "@TRANSFER_SIGHT_SEEING_PACKAGE_FLAG", DbType.String, package_flag);
               if (date.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
               }

               db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, user_id);
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(QUOTE));

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

       public DataSet upadteagentfees(String Quote_id, String fees)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_CANCELLATION_FEES_FOR_AGENT");
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(Quote_id));
               if (fees == "")
               {
                   db.AddInParameter(dbCmd, "@AGENTCANCELLATION_FEES", DbType.Decimal, Convert.ToDecimal(0));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@AGENTCANCELLATION_FEES", DbType.Decimal, Convert.ToDecimal(fees));
               }
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

       public DataSet upadteHotelfees(String Quote_id, String city, String hotelid, String fees)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_CANCELLATION_FEES_FOR_HOTEL");
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(Quote_id));
               db.AddInParameter(dbCmd, "@CITY", DbType.String, city);
               //db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, hotelid);
               if (fees=="")
               {
                    db.AddInParameter(dbCmd, "@HOTELCANCELLATION_FEES", DbType.Decimal, Convert.ToDecimal(0));

               }
               else
               {
               db.AddInParameter(dbCmd, "@HOTELCANCELLATION_FEES", DbType.Decimal, Convert.ToDecimal(fees));
               }
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

       public DataTable FECH_INVOICE_FROM_QUOTE(String Quote_id)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_INVOICE_NO_FROM_QUOTE_ID");
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(Quote_id));
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

       #endregion


       public DataSet transfer_gettime_from_other_time(String sp_name, int tp_form_to_id, String flg, String sic_pvt, String time)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);

               db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_FROM_TO_DETAIL_ID", DbType.Int32, tp_form_to_id);
               db.AddInParameter(dbCmd, "@FLAG", DbType.String, flg);
               db.AddInParameter(dbCmd, "@SIC_PVT", DbType.String, sic_pvt);
               db.AddInParameter(dbCmd, "@PACKAGETIME", DbType.String, time);
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

       public DataSet get_hotel_price(String sp_name, String city, String hotel_name, String room_type, String from_date, String to_date)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);

               db.AddInParameter(dbCmd, "@CITY", DbType.String, city);
               db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, hotel_name);
               db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, room_type);
               if (from_date.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(from_date.ToString(), "dd/MM/yyyy", null));
               }

               if (to_date .ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
               }
              
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
       
       public void insert_flight_entry(String arrival_flight, String Departure_flight, String arrival_time, String deparure_time, int tour_id)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_FLIGHT_DEATILS");
               // ds=db.ExecuteDataSet(dbCmd);
               db.AddInParameter(dbCmd, "@ARRIVAL_FLIGHT", DbType.String, arrival_flight);
               db.AddInParameter(dbCmd, "@DEPARTURE_FLIGHT", DbType.String, Departure_flight);

               if (arrival_time.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, DateTime.Parse(arrival_time.ToString()));
               }

               if (deparure_time.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DEPARTURE_TIME", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DEPARTURE_TIME", DbType.DateTime, DateTime.Parse(deparure_time.ToString()));
               }

               db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tour_id);


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

       public void update_transfer_time(int service_cart_id, String time, String sic_pvt_flag)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_TRANSFER_ON_RECONFIRM");

               db.AddInParameter(dbCmd, "@SERVICE_CART_ID", DbType.Int32, service_cart_id);
               db.AddInParameter(dbCmd, "@TIME", DbType.String, time);

               db.AddInParameter(dbCmd, "@SIC_PVT_FLAG", DbType.String, sic_pvt_flag);


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
       

       public DataSet get_transfer_city(String sp_name, int transfer_detail_id)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);


               //  db.AddInParameter(dbCmd, "@SIGHT_NAME", DbType.String, sightname);


               //if (to_date.ToString().Equals(""))
               //{
               //    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
               //}
               //else
               //{
               //    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
               //}
               db.AddInParameter(dbCmd, "@TRANSFER_DETAIL_ID", DbType.Int32, transfer_detail_id);
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

        public void insertupdate_Hotels_sub_details(int hotelcartid, String city_id, String hotel_id, String from_date, String to_date, int no_of_rooms, String order_status, int user_id, int room_type, String room_type_id, String start_date, String end_date, Boolean package_flag)
       {
           Database db = null;
           DbCommand dbCmd = null;


           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_HOTEL_CART_SUB_DETAIL");

               db.AddInParameter(dbCmd, "@HOTEL_CART_SUB_DETAIL_ID", DbType.Int32, hotelcartid);
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

       #region Additional Service

        public void insertupdate_Additional_Services_details(int servicesid, String service_desc, String chainname, String date, int tour_id, int quote_id, String order_status, int user_id, String no_of_pax, Boolean flag, String net_price, String sell_price, String fromdetail, String todetail, String sic_pvt)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_ADDITIONAL_SERVICE_CART_DETAIL");

                db.AddInParameter(dbCmd, "@ADDITIONAL_SERVICE_CART_ID", DbType.Int32, servicesid);
                db.AddInParameter(dbCmd, "@SERVICE_DESCRIPTION", DbType.String, service_desc);
                db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, chainname);
                db.AddInParameter(dbCmd, "@SIC_PVT", DbType.String, sic_pvt);
                if (date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
                }

                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tour_id);

                db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, order_status);

                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, user_id);
                if (no_of_pax == "")
                {
                    db.AddInParameter(dbCmd, "@NO_OF_PAX", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_PAX", DbType.Int32, int.Parse(no_of_pax));
                }
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quote_id);

                db.AddInParameter(dbCmd, "@PACKAGE_FLAG", DbType.Boolean, flag);
                if (net_price == "")
                {
                    db.AddInParameter(dbCmd, "@NET_PRICE", DbType.Decimal, Convert.ToDecimal(0));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NET_PRICE", DbType.Decimal, Convert.ToDecimal(net_price));
                }
                if (sell_price == "")
                {
                    db.AddInParameter(dbCmd, "@SELL_PRICE", DbType.Decimal, Convert.ToDecimal(0));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@SELL_PRICE", DbType.Decimal, Convert.ToDecimal(sell_price));
                }
                db.AddInParameter(dbCmd, "@FROM_DETAIL", DbType.String, fromdetail);

                db.AddInParameter(dbCmd, "@TO_DETAIL", DbType.String, todetail);

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

        public DataSet fetch_additional_service(String qid, String tid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ADDITIONAL_SERVICES_FROM_QUOTE_ID");
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(qid));
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, int.Parse(tid));
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
       #endregion

       #region ADDITIONAL SERVICES PAYMENT DUE DATE


        public void insert_payment_date_additional_services(String due_date, int cart_id)
        {
            Database db = null;
            DataSet ds = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_PAYMENT_DUE_DATE_ADDITIONAL_SERVICES");

                 if (due_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_DUE_DATE", DbType.DateTime, DBNull.Value);
                 }
                 else
                 {
                db.AddInParameter(dbCmd,"@PAYMENT_DUE_DATE",DbType.DateTime, DateTime.ParseExact(due_date.ToString(), "dd/MM/yyyy", null));
                 }

                 db.AddInParameter(dbCmd, "@SERVICE_CART_ID", DbType.Int32, cart_id);
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

       // EMAIL TEMPLATE GENERATION
        public DataSet get_emailConfig(String sp_name)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);


             
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

        public DataSet get_email_templaet_data(String sp_name, String EVENT_NAME)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);

                db.AddInParameter(dbCmd, "@EVENT_NAME", DbType.String, EVENT_NAME);

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

        public DataSet get_email_adress_backoffice(String sp_name, String DEPARTMENT_NAME)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);

                db.AddInParameter(dbCmd, "@DEPARTMENT_NAME", DbType.String, DEPARTMENT_NAME);

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

       // EMIAL TRAILS

        public void insert_email_trail(int EMAIL_TEMPLATE_MASTER_ID, String FROM_ID, String TO_ID, String CC_ID, String BCC_ID, String SUBJECT, String EMAIL_CONTENT, int QUOTE_ID, String INVOICE_NO, String FILE_PATH, String FILE_NAME, int CREATED_BY, int FLAG)
        {
            Database db = null;
            DataSet ds = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_EMAILS_TRAILS");

                //if (due_date.ToString().Equals(""))
                //{
                //    db.AddInParameter(dbCmd, "@PAYMENT_DUE_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@PAYMENT_DUE_DATE", DbType.DateTime, DateTime.ParseExact(due_date.ToString(), "dd/MM/yyyy", null));
                //}

                db.AddInParameter(dbCmd, "@EMAIL_TEMPLATE_MASTER_ID", DbType.Int32, EMAIL_TEMPLATE_MASTER_ID);

                db.AddInParameter(dbCmd, "@FROM_ID", DbType.String, FROM_ID);
                db.AddInParameter(dbCmd, "@TO_ID", DbType.String, TO_ID);
                db.AddInParameter(dbCmd, "@CC_ID", DbType.String, CC_ID);
                db.AddInParameter(dbCmd, "@BCC_ID", DbType.String, BCC_ID);
                db.AddInParameter(dbCmd, "@SUBJECT", DbType.String, SUBJECT);
                db.AddInParameter(dbCmd, "@EMAIL_CONTENT", DbType.String, EMAIL_CONTENT);

                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, QUOTE_ID);

                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, INVOICE_NO);
                db.AddInParameter(dbCmd, "@FILE_NAME", DbType.String, FILE_NAME);
                db.AddInParameter(dbCmd, "@FILE_PATH", DbType.String, FILE_PATH);

                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);
                db.AddInParameter(dbCmd, "@FLAG", DbType.Int32, FLAG);

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


       ///////////////////////// UPDATE BOOKING
        public DataSet fetch_tour_Dates(String sp_name, int TOUR_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@TOURID", DbType.Int32, TOUR_ID);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

       /////// Multicheck Dropdown
        // ---------------------- 02/07/2012 -----------------------------------------
        public DataSet getALLdateFORmEAL(String FRM_DATE, String to_date)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_DATE_FOR_MEAL_IN_HOTEL");


                if (FRM_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(FRM_DATE.ToString(), "dd/MM/yyyy", null));
                }
                if (to_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
                }
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

       // AMENDMENT
        public DataSet fetch_invoice_no(String sp_name, int TOUR_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, TOUR_ID);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet get_Sight_Dates(String FRM_DATE, String to_date, String NotShowdate)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_DATE_FOR_MEAL_IN_SIGHSEEING");


                if (FRM_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(FRM_DATE.ToString(), "dd/MM/yyyy", null));
                }
                if (to_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
                }

                if (NotShowdate.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NOT_SHOWN_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NOT_SHOWN_DATE", DbType.DateTime, DateTime.ParseExact(NotShowdate.ToString(), "dd/MM/yyyy", null));
                }
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

       /////////////////////////// CANCELLATION IF RECONFIRM DATE IS PASSED FOR QUOTE
        public DataSet change_quote_status(int  QUOTE_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_QUOTE_STATUS");
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, QUOTE_ID);
                db.AddInParameter(dbCmd, "@STATUS", DbType.Int32, 14);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

       /////////////// ROOM TYPE VALIDATION ON DATE CONFLICT
        public DataSet fetch_RoomTypeValidation(String hotel_name, String Date)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ROOM_TYPE_FOR_FIT_HOTEL_VISE_VALIDATION");
                db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, hotel_name);
                if (Date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(Date.ToString(), "dd/MM/yyyy", null));
                }
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

       //// UPDATE BOOKING ENTER AGENT RECONFIRMATION DATE
        public void insertAgentReconfirmationDate(int old_quote_id, int new_quote_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
           
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_AGENT_RECONFIRM_DATE");
                db.AddInParameter(dbCmd, "@OLD_QUOTE_ID", DbType.String, old_quote_id);
                db.AddInParameter(dbCmd, "@NEW_QUOTE_ID", DbType.String, new_quote_id);

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

       /* CHANGE OF SITE SEEING DATES  */
        public void updateSitetime(string SIGHT_PACKAGE_NAME, string CITY_NAME, int QUOTE_ID, string date, string time)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_SITE_SEEING_TIMINGS");

                db.AddInParameter(dbCmd, "@SIGHT_PACKAGE_NAME", DbType.String, SIGHT_PACKAGE_NAME);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, CITY_NAME);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, QUOTE_ID);
                if (date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
                }

                db.AddInParameter(dbCmd, "@TIME", DbType.String, time);
               
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

       /* UPDATE TRANSFER PACKAGE ON BOOK */
        public void updateTransferOnBook(int service_cart_id, String time, String sic_pvt_flag, String date)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_TRANSFER_ON_BOOK");

                db.AddInParameter(dbCmd, "@SERVICE_CART_ID", DbType.Int32, service_cart_id);
                db.AddInParameter(dbCmd, "@TIME", DbType.String, time);

                db.AddInParameter(dbCmd, "@SIC_PVT_FLAG", DbType.String, sic_pvt_flag);
                if (date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
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

       /* GET ORDER STATUS FROM QUOTE ID  */
        public DataSet getQuoteOrderStatus(int QUOTE_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_FIT_QUOTE_STATUS");
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, QUOTE_ID);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

            return dsData;
        }

        public DataSet getsightseen(String sp_name, int param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@SIGHT_SEEING_PRICE_ID", DbType.Int32, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
   }
}
