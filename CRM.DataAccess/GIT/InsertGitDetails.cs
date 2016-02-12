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
   public  class InsertGitDetails
    {
       // SLAB AND QUOTES

       public void insertTour_Quote(int GIT_TOUR_ID, int GIT_PACKAGE_ID, String GIT_GROUP_NAME, String START_DATE, String END_DATE, int TOTAL_NO_OF_ROOMS, int AGENT_ID, int CREATED_BY, String ORDER_STATUS, int NO_OF_NIGHTS, String NO_OF_ADULTS, String NO_OF_CWB, String NO_OF_CNB, String NO_OF_INFANT)
       {
           Database db = null;
           DbCommand dbCmd = null;
          
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_TOUR_MASTER");
               db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
               db.AddInParameter(dbCmd, "@GIT_PACKAGE_ID", DbType.Int32, GIT_PACKAGE_ID);
               db.AddInParameter(dbCmd, "@GIT_GROUP_NAME", DbType.String, GIT_GROUP_NAME);

               if (START_DATE.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@START_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@START_DATE", DbType.DateTime, DateTime.ParseExact(START_DATE.ToString(), "dd/MM/yyyy", null));
               }

               if (END_DATE.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@END_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@END_DATE", DbType.DateTime, DateTime.ParseExact(END_DATE.ToString(), "dd/MM/yyyy", null));
               }

               db.AddInParameter(dbCmd, "@TOTAL_NO_OF_ROOMS", DbType.Int32, TOTAL_NO_OF_ROOMS);
               db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, AGENT_ID);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);
                db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, ORDER_STATUS);

                db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, NO_OF_NIGHTS);

                if (NO_OF_ADULTS.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32, int.Parse(NO_OF_ADULTS));
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

                if (NO_OF_INFANT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_INFANT", DbType.Int32, int.Parse(NO_OF_INFANT));
                }


             db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

          
       }

       public void insertSlab(int GIT_TOUR_SLAB_ID, int SLAB_ID, int CREATED_BY)
       {
            Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_TOUR_SLAB_MASTER");
               db.AddInParameter(dbCmd, "@GIT_TOUR_SLAB_ID", DbType.Int32, GIT_TOUR_SLAB_ID);
               db.AddInParameter(dbCmd, "@SLAB_ID", DbType.Int32, SLAB_ID);
               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);

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

       // HOTEL MAP ENTRY
       public void insertQuoteHotelMap(int GIT_QUOTE_HOTEL_ID, String GIT_HOTEL1, String ROOM_TYPE_ID1, String CITY_ID1,  String GIT_HOTEL2, String ROOM_TYPE_ID2, String CITY_ID2, int CREATED_BY)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_QUOTE_HOTEL_MAP");
               db.AddInParameter(dbCmd, "@GIT_QUOTE_HOTEL_ID", DbType.Int32, GIT_QUOTE_HOTEL_ID);

               db.AddInParameter(dbCmd, "@GIT_HOTEL1", DbType.String, GIT_HOTEL1);
               db.AddInParameter(dbCmd, "@ROOM_TYPE_ID1", DbType.String, ROOM_TYPE_ID1);
               db.AddInParameter(dbCmd, "@CITY_ID1", DbType.String, CITY_ID1);
               
               db.AddInParameter(dbCmd, "@GIT_HOTEL2", DbType.String, GIT_HOTEL2);
               db.AddInParameter(dbCmd, "@ROOM_TYPE_ID2", DbType.String, ROOM_TYPE_ID2);
               db.AddInParameter(dbCmd, "@CITY_ID2", DbType.String, CITY_ID2);
              
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY); 
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

       public void insertSubHotels(int GIT_QUOTE_HOTEL_ID, String GIT_HOTEL1, String ROOM_TYPE_ID1, String CITY_ID1, String GIT_HOTEL2, String ROOM_TYPE_ID2, String CITY_ID2, int CREATED_BY)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_QUOTE_HOTEL_MAP_DETAIL");
               db.AddInParameter(dbCmd, "@GIT_QUOTE_HOTEL_MAP_DETAIL_ID", DbType.Int32, GIT_QUOTE_HOTEL_ID);

               db.AddInParameter(dbCmd, "@GIT_HOTEL1", DbType.String, GIT_HOTEL1);
               db.AddInParameter(dbCmd, "@ROOM_TYPE_ID1", DbType.String, ROOM_TYPE_ID1);
               db.AddInParameter(dbCmd, "@CITY_ID1", DbType.String, CITY_ID1);

               db.AddInParameter(dbCmd, "@GIT_HOTEL2", DbType.String, GIT_HOTEL2);
               db.AddInParameter(dbCmd, "@ROOM_TYPE_ID2", DbType.String, ROOM_TYPE_ID2);
               db.AddInParameter(dbCmd, "@CITY_ID2", DbType.String, CITY_ID2);

               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);
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


       // HOTEL ENTRY
       public void insertHotelsDetails(int GIT_HOTEL_CART_ID, String HOTEL_NAME, String ROOM_TYPE_ID, int GIT_TOUR_SLAB_ID, String FROM_DATE, String TO_DATE, String NO_OF_NIGHTS, String CITY_ID, int GIT_TOUR_ID, String CURRENCY_ID, bool PACKAGE_FLAG, int CREATED_BY)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_HOTEL_CART_DETAIL");
               db.AddInParameter(dbCmd, "@GIT_HOTEL_CART_ID", DbType.Int32, GIT_HOTEL_CART_ID);

               db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, HOTEL_NAME);
               db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.String, ROOM_TYPE_ID);
               db.AddInParameter(dbCmd, "@GIT_TOUR_SLAB_ID", DbType.Int32, GIT_TOUR_SLAB_ID);
               if (FROM_DATE.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(FROM_DATE.ToString(), "dd/MM/yyyy", null));
               }

               if (TO_DATE.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(TO_DATE.ToString(), "dd/MM/yyyy", null));
               }


               if (TO_DATE.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, 0);
               }
               else
               {

                   db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, int.Parse ( NO_OF_NIGHTS));
               }
               db.AddInParameter(dbCmd, "@CITY_ID", DbType.String, CITY_ID);
               db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
               db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.String, CURRENCY_ID);

               db.AddInParameter(dbCmd, "@PACKAGE_FLAG", DbType.Boolean, PACKAGE_FLAG);
               
               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);
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

       // RESTURANT ENTRY
       public void insertResturantsDetails(int GIT_RESTAURENT_CART_ID, int GIT_TOUR_SLAB_ID, String RESTURANT_NAME, String MEAL_TYPE, String DATE, String CURRENCY_ID, Boolean PACKAGE_FLAG, int CREATED_BY, String CITY_NAME, int TOUR_ID, String TIME, String VEG, String NONVEG, String JAIN)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_RESTAURENT_CART_DETAIL");

               db.AddInParameter(dbCmd, "@GIT_RESTAURENT_CART_ID", DbType.Int32, GIT_RESTAURENT_CART_ID);

               db.AddInParameter(dbCmd, "@GIT_TOUR_SLAB_ID", DbType.Int32, GIT_TOUR_SLAB_ID);
               db.AddInParameter(dbCmd, "@RESTURANT_NAME", DbType.String, RESTURANT_NAME);
               db.AddInParameter(dbCmd, "@MEAL_TYPE", DbType.String, MEAL_TYPE);
               if (DATE.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(DATE.ToString(), "dd/MM/yyyy", null));
               }

               db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.String, CURRENCY_ID);
               db.AddInParameter(dbCmd, "@PACKAGE_FLAG", DbType.Boolean, PACKAGE_FLAG);

               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);
               db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, CITY_NAME);
               db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, TOUR_ID);
               if (TIME.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse(TIME.ToString()));
               }
               if (VEG.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@VEG", DbType.Int32, 0);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@VEG", DbType.Int32, Convert.ToInt32(VEG));
               }
               if (NONVEG.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@NONVEG", DbType.Int32, 0);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@NONVEG", DbType.Int32, NONVEG);
               }
               if (JAIN.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@JAIN", DbType.Int32, 0);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@JAIN", DbType.Int32, JAIN);
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

       // CONFERENCE ENTRY
       public void insertConferenceDetails(int GIT_CONFERENCE_CART_ID, int GIT_TOUR_SLAB_ID, String HOTEL_NAME, String CONFERENCE_TYPE_ID, String CONFERENCE_DATE, String CURRENCY_ID, Boolean PACKAGE_FLAG, int CREATED_BY, String CITY_NAME, int TOUR_ID, String Time)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_CONFERENCE_CART_DETAIL");

               db.AddInParameter(dbCmd, "@GIT_CONFERENCE_CART_ID", DbType.Int32, GIT_CONFERENCE_CART_ID);

               db.AddInParameter(dbCmd, "@GIT_TOUR_SLAB_ID", DbType.Int32, GIT_TOUR_SLAB_ID);
               db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, HOTEL_NAME);
               db.AddInParameter(dbCmd, "@CONFERENCE_TYPE_ID", DbType.String, CONFERENCE_TYPE_ID);
               if (CONFERENCE_DATE.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@CONFERENCE_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@CONFERENCE_DATE", DbType.DateTime, DateTime.ParseExact(CONFERENCE_DATE.ToString(), "dd/MM/yyyy", null));
               }

               db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.String, CURRENCY_ID);
               db.AddInParameter(dbCmd, "@PACKAGE_FLAG", DbType.Boolean, PACKAGE_FLAG);

               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);
               db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, CITY_NAME);
               db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, TOUR_ID);
               if (Time.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse(Time.ToString()));
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

       // GALA DIINER ENTRY
       public void insertGalaDinnerDetails(int GIT_GALA_DINNER_CART_ID, int GIT_TOUR_SLAB_ID, String HOTEL_NAME, String GALA_DINNER_TYPE_ID, String DINNER_DATE, String CURRENCY_ID, Boolean PACKAGE_FLAG, int CREATED_BY, String CITY_NAME, int TOUR_ID, string time)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_GALA_DINNER_CART_DETAIL");

               db.AddInParameter(dbCmd, "@GIT_GALA_DINNER_CART_ID", DbType.Int32, GIT_GALA_DINNER_CART_ID);

               db.AddInParameter(dbCmd, "@GIT_TOUR_SLAB_ID", DbType.Int32, GIT_TOUR_SLAB_ID);
               db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, HOTEL_NAME);
               db.AddInParameter(dbCmd, "@GALA_DINNER_TYPE_ID", DbType.String, GALA_DINNER_TYPE_ID);
               if (DINNER_DATE.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DINNER_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DINNER_DATE", DbType.DateTime, DateTime.ParseExact(DINNER_DATE.ToString(), "dd/MM/yyyy", null));
               }

               db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.String, CURRENCY_ID);
               db.AddInParameter(dbCmd, "@PACKAGE_FLAG", DbType.Boolean, PACKAGE_FLAG);

               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);
               db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, CITY_NAME);
               db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, TOUR_ID);
               if (time.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@GALA_DINNER_TIME", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@GALA_DINNER_TIME", DbType.DateTime, DateTime.Parse(time.ToString()));
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

       // SITE SEEING ENTRY
       public void insertSiteSeeingDetails(int GIT_SIGHT_CART_ID, int GIT_TOUR_SLAB_ID, String SIGHT_SEEING_PRICE_ID,  String CURRENCY_ID, Boolean PACKAGE_FLAG, int CREATED_BY, String CITY_NAME, int TOUR_ID, String date, string time)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_SIGHT_SEEING_CART_DETAIL");

               db.AddInParameter(dbCmd, "@GIT_SIGHT_CART_ID", DbType.Int32, GIT_SIGHT_CART_ID);

               db.AddInParameter(dbCmd, "@GIT_TOUR_SLAB_ID", DbType.Int32, GIT_TOUR_SLAB_ID);

               db.AddInParameter(dbCmd, "@SIGHT_SEEING_PRICE_ID", DbType.String, SIGHT_SEEING_PRICE_ID);
               db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, TOUR_ID);
               db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.String, CURRENCY_ID);
               db.AddInParameter(dbCmd, "@PACKAGE_FLAG", DbType.Boolean, PACKAGE_FLAG);

               if (date.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
               }

               
                   db.AddInParameter(dbCmd, "@TIME", DbType.String, time);
               

               db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, CITY_NAME);
               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);
              
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

       
       // ADDITIONAL SERVICES
       public void insertupdate_Additional_Services_details(int servicesid, String service_desc, String chainname, String date, int tour_id, int user_id, String no_of_pax, Boolean flag, String net_price, String sell_price, String fromdetail, String todetail, String NOOFPASSENGER, Boolean ADDITIONAL_FLAG)
       {
           Database db = null;
           DbCommand dbCmd = null;


           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_ADDITIONAL_SERVICE_CART_DETAIL");

               db.AddInParameter(dbCmd, "@ADDITIONAL_SERVICE_CART_ID", DbType.Int32, servicesid);
               db.AddInParameter(dbCmd, "@SERVICE_DESCRIPTION", DbType.String, service_desc);
               db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, chainname);

               if (date.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
               }

               db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tour_id);

               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, user_id);
               if (no_of_pax == "")
               {
                   db.AddInParameter(dbCmd, "@NO_OF_PAX", DbType.Int32, 0);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@NO_OF_PAX", DbType.Int32, int.Parse(no_of_pax));
               }
              

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
               if (NOOFPASSENGER == "")
               {
                   db.AddInParameter(dbCmd, "@NOOFPASSENGER", DbType.Int32, Convert.ToInt32(0));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@NOOFPASSENGER", DbType.Int32, Convert.ToInt32(NOOFPASSENGER));
               }
               db.AddInParameter(dbCmd, "@ADDITIONAL_FLAG", DbType.Boolean, ADDITIONAL_FLAG);
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

       // GUIDE
       public void insertGuide(int GIT_GUIDE_CART_ID, int GIT_TRANSFER_CART_ID, String GUIDE_NAME, String NO_OF_GUIDE,   int CREATED_BY,  int GIT_TRANSFER_PACKGE_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_GUIDE_CART_DETAIL");

               db.AddInParameter(dbCmd, "@GIT_GUIDE_CART_ID", DbType.Int32, GIT_GUIDE_CART_ID);

               db.AddInParameter(dbCmd, "@GIT_TRANSFER_CART_ID", DbType.Int32, GIT_TRANSFER_CART_ID);

               db.AddInParameter(dbCmd, "@GUIDE_NAME", DbType.String, GUIDE_NAME);

               if (NO_OF_GUIDE.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@NO_OF_GUIDE", DbType.Int32, 0);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@NO_OF_GUIDE", DbType.Int32, NO_OF_GUIDE);
               }
               
               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);
               db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.Int32, GIT_TRANSFER_PACKGE_ID);
               

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

       // BOAT
       public DataSet insertBoat(int GIT_BOAT_CART_ID, int GIT_TRANSFER_CART_ID, String SUPPLIER_NAME, String NO_OF_BOATS, int CREATED_BY, int GIT_TRANSFER_PACKGE_ID, string date, string time)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_BOAT_CART_DETAIL");

               db.AddInParameter(dbCmd, "@GIT_BOAT_CART_ID", DbType.Int32, GIT_BOAT_CART_ID);

               db.AddInParameter(dbCmd, "@GIT_TRANSFER_CART_ID", DbType.Int32, GIT_TRANSFER_CART_ID);

               db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, SUPPLIER_NAME);

               if (NO_OF_BOATS.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@NO_OF_BOATS", DbType.Int32, 0);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@NO_OF_BOATS", DbType.Int32, NO_OF_BOATS);
               }
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
                   db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse(time.ToString()));
               }
               //db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.String, CURRENCY_ID);

               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);
               db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.Int32, GIT_TRANSFER_PACKGE_ID);


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

       public DataSet UpdateBoat(int GIT_cart_ID, String SUPPLIER_NAME, int GIT_TRANSFER_PACKGE_ID, string date, string time)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_BOAT_TIME");

               db.AddInParameter(dbCmd, "@GIT_TRANSFER_CART_ID", DbType.Int32, GIT_cart_ID);


               db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, SUPPLIER_NAME);

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
                   db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse(time.ToString()));
               }

               db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.Int32, GIT_TRANSFER_PACKGE_ID);


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

       // COACH
       public DataSet insertCoach(int GIT_COACH_CART_ID, int GIT_TRANSFER_CART_ID, String SUPPLIER_NAME, int CREATED_BY, int GIT_TRANSFER_PACKGE_ID, string date, string time)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_COACH_CART_DETAIL");

               db.AddInParameter(dbCmd, "@GIT_COACH_CART_ID", DbType.Int32, GIT_COACH_CART_ID);

               db.AddInParameter(dbCmd, "@GIT_TRANSFER_CART_ID", DbType.Int32, GIT_TRANSFER_CART_ID);

               db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, SUPPLIER_NAME);

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
                   db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse(time.ToString()));
               }
               //db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.String, CURRENCY_ID);

               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);
               db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.Int32, GIT_TRANSFER_PACKGE_ID);


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

       public DataSet UpdateCoach(int GIT_cart_ID,String SUPPLIER_NAME,int GIT_TRANSFER_PACKGE_ID, string date, string time)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_COACH_TIME");

               db.AddInParameter(dbCmd, "@GIT_TRANSFER_CART_ID", DbType.Int32, GIT_cart_ID);


               db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, SUPPLIER_NAME);

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
                   db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse(time.ToString()));
               }

               db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.Int32, GIT_TRANSFER_PACKGE_ID);


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

       public DataSet FetchCartId(int GIT_TOUR_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_TRANSFER_CART_ID");

               db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, GIT_TOUR_ID);

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

       // INSERT TRANSPORT PACKAGE
       public void insertTrasportPackage(int GIT_TRANSFER_CART_ID, int GIT_TOUR_SLAB_ID, int GIT_TOUR_ID, int CREATED_BY, int GIT_TRANSFER_PACKGE_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_TRANSFER_CART_DETAIL");

               db.AddInParameter(dbCmd, "@GIT_TRANSFER_CART_ID", DbType.Int32, GIT_TRANSFER_CART_ID);

               db.AddInParameter(dbCmd, "@GIT_TOUR_SLAB_ID", DbType.Int32, GIT_TOUR_SLAB_ID);

               db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);

              

               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);
               db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.Int32, GIT_TRANSFER_PACKGE_ID);


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

       public void insertTrasportPackageDetails(int GIT_TRANSFER_CART_DETAIL_ID, int GIT_TOUR_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_TRANSFER_CART_DETAIL_INFORMATION");

               db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKAGE_DETAIL_ID", DbType.Int32, GIT_TRANSFER_CART_DETAIL_ID);

               db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);

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
       

       // GENERATE QUOTE SP'S VARIBLE
       public void GenerateQuote(string TOUR_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("GENERATE_QUOTE_FOR_GIT");

               db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, TOUR_ID);


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

       // INSERT QUOTE DETAILS
       public void insertQuoteDetails(int GIT_QUOTE_DETAIL_ID, int GIT_QUOTE_ID, int GIT_TOUR_SLAB_ID, int GIT_QUOTE_HOTEL_ID, int CREATED_BY)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_QUOTE_DETAIL");

               db.AddInParameter(dbCmd, "@GIT_QUOTE_DETAIL_ID", DbType.Int32, GIT_QUOTE_DETAIL_ID);
               db.AddInParameter(dbCmd, "@GIT_QUOTE_ID", DbType.Int32, GIT_QUOTE_ID);
               db.AddInParameter(dbCmd, "@GIT_TOUR_SLAB_ID", DbType.Int32, GIT_TOUR_SLAB_ID);
               db.AddInParameter(dbCmd, "@GIT_QUOTE_HOTEL_ID", DbType.Int32, GIT_QUOTE_HOTEL_ID);
               
               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, GIT_QUOTE_HOTEL_ID);
               
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

       // INSERT EXCHANGE RATE & MARGIN AMOUNT
       public void insertExRate(int GIT_TOUR_ID, String MARGIN_AMOUNT, String EXCHANGE_RATE)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_EXRATE_MARGIN_FOR_GIT_QUOTE");

               db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);

               if (MARGIN_AMOUNT.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, 0.00);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal ,decimal.Parse( MARGIN_AMOUNT));
               }

               if (EXCHANGE_RATE.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@EXCHANGE_RATE", DbType.Decimal, 0.00);
               }

               else
               {
                   db.AddInParameter(dbCmd, "@EXCHANGE_RATE", DbType.Decimal, decimal.Parse(EXCHANGE_RATE));
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

       // UPDATE QUOTE'S STATUS
       public void updateQuoteStatus(int GIT_TOUR_ID, String ORDER_STATUS)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_ORDER_SATUS_FOR_GIT_QUOTE");

               db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
               db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, ORDER_STATUS);
             
              

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
       
       // INSERT MICE EXPENCE
       public void insertMiceExp(int GIT_TOUR_ID, String MICE_EXPENCE)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_MICE_EXPENSE");

               db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);

               if (MICE_EXPENCE.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@MICE_EXPENCE", DbType.Decimal, 0.00);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@MICE_EXPENCE", DbType.Decimal, MICE_EXPENCE);
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

        /********************** CHANGE QUOTATION STATUS************************/
       public void changeQuotestatus(int GIT_TOUR_ID, String ORDER_STATUS)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("CHANGE_QUOTE_STATUS");

               db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);

               db.AddInParameter(dbCmd, "@ORDER_STATUS", DbType.String, ORDER_STATUS);



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

       /********************** INSERT NO OF ROOMS ************************/
       public void insertNoOfRooms(String hotel, String roomtype, int tourid, int noofsingalroom, int noofDoubleroom, int nooftripleroom)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_NO_OF_ROOMS_GIT_HOTEL_CART_DETAIL");
               db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, hotel);
               db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, roomtype);
               db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, tourid);
               db.AddInParameter(dbCmd, "@NO_OF_SINGLE_ROOM", DbType.Int32, noofsingalroom);
               db.AddInParameter(dbCmd, "@NO_OF_DOUBLE_ROOM", DbType.Int32, noofDoubleroom);
               db.AddInParameter(dbCmd, "@NO_OF_TRIPLE_ROOM", DbType.Int32, nooftripleroom);


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

       /********************** UPDATE VEG NON VEG AND JAIN ************************/
       public void UPDATE_TIME_VEG_NON_VEG_JAIN_IN_MEALS(String RESTURANT_NAME, String MEAL_TYPE, String DATE, String VEG, String NONVEG, String JAIN, String time)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_TIME_VEG_NON_VEG_JAIN_IN_MEALS");
               db.AddInParameter(dbCmd, "@RESTURANT_NAME", DbType.String, RESTURANT_NAME);
               db.AddInParameter(dbCmd, "@MEAL_TYPE", DbType.String, MEAL_TYPE);
               if (time.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse(time.ToString()));
               }
               if (VEG.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@VEG", DbType.Int32, 0);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@VEG", DbType.Int32, Convert.ToInt32(VEG));
               }
               if (NONVEG.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@NONVEG", DbType.Int32, 0);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@NONVEG", DbType.Int32, Convert.ToInt32(NONVEG));
               }
               if (JAIN.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@JAIN", DbType.Int32, 0);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@JAIN", DbType.Int32, Convert.ToInt32(JAIN));
               }
               if (DATE.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(DATE.ToString(), "dd/MM/yyyy", null));
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

       /********************** Update Additional Services ************************/

       public void UpdateAdditionalService_flag(String service_desc, String chainname, String date, String fromdetail, String todetail, String NOOFPASSENGER, Boolean ADDITIONAL_FLAG)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_ADDITTIONAL_SERVICES");

               db.AddInParameter(dbCmd, "@SERVICE_DESCRIPTION", DbType.String, service_desc);
               db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, chainname);

               if (date.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
               }
               db.AddInParameter(dbCmd, "@FROM_DETAIL", DbType.String, fromdetail);

               db.AddInParameter(dbCmd, "@TO_DETAIL", DbType.String, todetail);
               if (NOOFPASSENGER == "")
               {
                   db.AddInParameter(dbCmd, "@NOOFPASSENGER", DbType.Int32, Convert.ToInt32(0));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@NOOFPASSENGER", DbType.Int32, Convert.ToInt32(NOOFPASSENGER));
               }
               db.AddInParameter(dbCmd, "@ADDITIONAL_FLAG", DbType.Boolean, ADDITIONAL_FLAG);


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

       /********************** Upadate Trasport Package Details************************/
       public DataSet FetchDateOfTransfer(int GIT_TOUR_ID, int detailid)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_GIT_TRANSFER_PACKAGE_INFORMATION");

               db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
               db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKAGE_DETAIL_ID", DbType.Int32, detailid);


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

       public void UpadateTrasportPackageDetails(int GIT_TRANSFER_CART_DETAIL_ID, int GIT_TOUR_ID, String Date, String Time)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_GIT_TRANSFER_CART_DETAIL_INFORMATION");

               db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKAGE_DETAIL_ID", DbType.Int32, GIT_TRANSFER_CART_DETAIL_ID);

               db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
               if (Date.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(Date.ToString(), "dd/MM/yyyy", null));
               }
               if (Time.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TIME", DbType.DateTime, DateTime.Parse(Time.ToString()));
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

       public void insertCancelltionFees(int GIT_TOUR_ID, String CancellationFees) /*Insert cancellation Fees in invoice header*/
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_CANCELLATION_FEES");

               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, GIT_TOUR_ID);

               if (CancellationFees.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@CANCELLATION_FEES", DbType.Decimal, "0.00");
               }
               else
               {
                   db.AddInParameter(dbCmd, "@CANCELLATION_FEES", DbType.Decimal, CancellationFees);
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
    }
}
