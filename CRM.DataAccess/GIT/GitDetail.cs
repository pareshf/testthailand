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
   public class GitDetail
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

               //if (FROM_DATE.ToString().Equals(""))
               //{
               //    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
               //}
               //else
               //{
               //    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(FROM_DATE.ToString(), "dd/MM/yyyy", null));
               //}

               //if (TO_DATE.ToString().Equals(""))
               //{
               //    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
               //}
               //else
               //{
               //    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(TO_DATE.ToString(), "dd/MM/yyyy", null));
               //}

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       /* MELAS */
       public DataSet fetchResturants(String SPNAME, String CITY_NAME)
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

       public DataSet fetchMealType(String SPNAME, String RESTURANTS_NAME)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@RESTURANTS_NAME", DbType.String, RESTURANTS_NAME);



               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       /* SIGHT SEEING */
       public DataSet fetchSiteSeeing(String SPNAME, String CITY_NAME)
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

       /* CONFERENCE + GALA DINNER */

       public DataSet fetchConfGala(String SPNAME, String CITY_NAME)
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

       /* FETCH CITIES IN FIT PACKAGES */
       public DataSet fetchGitCities(String SPNAME, String PACKAGE_NAME)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@GIT_PACKAGE_ID", DbType.String, PACKAGE_NAME);



               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       /* FETCH TRASPORT PACKAGE */
       public DataSet fetchTransportPackage(String SPNAME, int GIT_PACKAGE_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@GIT_PACKAGE_ID", DbType.Int32, GIT_PACKAGE_ID);



               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       /* FETCH TRASPORT PACKAGE NAME  */
       public DataSet fetchTransportPackageName(String SPNAME, int GIT_PACKAGE_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@GIT_PACKAGE_ID", DbType.Int32, GIT_PACKAGE_ID);



               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       /* FETCH COACH SUPPLIER */
       public DataSet fetchTransportPackageCoach(String SPNAME, int GIT_PACKAGE_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@GIT_PACKAGE_ID", DbType.Int32, GIT_PACKAGE_ID);



               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       /* FETCH BOAT SUPPLIER */
       public DataSet fetchTransportPackageBoat(String SPNAME, int GIT_PACKAGE_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@GIT_PACKAGE_ID", DbType.Int32, GIT_PACKAGE_ID);



               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       /* FETCH GUIDE SUPPLIER */
       public DataSet fetchTransportPackageGuide(String SPNAME, int GIT_PACKAGE_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@GIT_PACKAGE_ID", DbType.Int32, GIT_PACKAGE_ID);



               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       /* FETCH GIT HOTEL QUOTE ID */
       public DataSet fetchGitHotelQuoteid(String SPNAME, int QUOTE_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, QUOTE_ID);



               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       /* FETCH ALL COMBINATION */
       public DataSet fetchAllCombination(String SPNAME, int QUOTE_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@GIT_QUOTE_ID", DbType.Int32, QUOTE_ID);



               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       public DataSet fetchAllSupplierMail(String SPNAME, int QUOTE_ID, int combid)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@GIT_QUOTE_ID", DbType.Int32, QUOTE_ID);
               db.AddInParameter(dbCmd, "@COMBITATION_ID", DbType.Int32, combid);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       /* FETCH ALL FLIGHT DETAILS */
       public DataSet fetchAllFlightDetails(String SPNAME, int TOUR_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, TOUR_ID);



               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       #region Sachin 14/12/2012

       /* FETCH SLAB AND NO OF PAX FOR CONFIRM SLAB */

       public DataSet fetchSlabAndCount(String SPNAME, int TOUR_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, TOUR_ID);



               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       public DataSet UpdateIsSlabFinal(String SPNAME, int GIT_TOUR_SLAB_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@GIT_TOUR_SLAB_ID", DbType.Int32, GIT_TOUR_SLAB_ID);
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       //CONNFIRM HOTEL AND RECONFIRMATION DATE

       public DataSet UpdateHotelReconfirmationDate(int GIT_TOUR_ID, String HOTEL_NAME, String ROOM_TYPE, String RECONFIRMATION_DATE, String PAYMENT_DUE_DATE, String CONFIRMATION_NUMBER)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_PAYMENT_DATE_HOTEL");
               db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
               db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, HOTEL_NAME);
               db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, ROOM_TYPE);
               if (RECONFIRMATION_DATE.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@RECONFIRMATION_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@RECONFIRMATION_DATE", DbType.DateTime, DateTime.ParseExact(RECONFIRMATION_DATE.ToString(), "dd/MM/yyyy", null));
               }
               if (PAYMENT_DUE_DATE.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@PAYMENT_DUE_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@PAYMENT_DUE_DATE", DbType.DateTime, DateTime.ParseExact(PAYMENT_DUE_DATE.ToString(), "dd/MM/yyyy", null));
               }
               db.AddInParameter(dbCmd, "@CONFIRMATION_NUMBER", DbType.String, CONFIRMATION_NUMBER);
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

       public DataSet UpdateBookCombination(int Tour_ID, int combid, String userid)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_BOOKED_COMBINATION");
               db.AddInParameter(dbCmd, "@GIT_QUOTE_HOTEL_ID", DbType.Int32, combid);
               db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Tour_ID);
               db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, int.Parse(userid));
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {
               throw ex;
           }

           return dsData;
       }

       /* FETCH ALL Booked Hotels */
       public DataSet fetchAllBookedHotels(String SPNAME, int TOUR_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, TOUR_ID);



               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {
               throw ex;
           }

           return dsData;
       }

       public DataSet UpdateReconforamtiondate(String SPNAME, int TOUR_ID, string date)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, TOUR_ID);
               //db.AddInParameter(dbCmd, "@AGENT_RECONFIRMATION_DATE", DbType.DateTime, date);
               if (date.ToString().Equals("DD/MM/YYYY") || date.ToString().Equals(""))
               {

                   db.AddInParameter(dbCmd, "@AGENT_RECONFIRMATION_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@AGENT_RECONFIRMATION_DATE", DbType.DateTime, DateTime.ParseExact(date, "dd/MM/yyyy", null));
               }
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {
               throw ex;
           }

           return dsData;
       }

       /*Room Validation*/
       public DataSet fetchAllHotelsForRoomValidation(String SPNAME, int TOUR_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, TOUR_ID);



               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {
               throw ex;
           }

           return dsData;
       }

       /*fill Hotel Conform Popup*/
       public DataSet fetchHotelsConformdateForPopup(String SPNAME, int TOUR_ID, String HOTEL_NAME, String ROOM_TYPE)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, TOUR_ID);
               db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, HOTEL_NAME);
               db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, ROOM_TYPE);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {
               throw ex;
           }

           return dsData;
       }

       /* ROOM LIST*/
       public DataSet INSERTROOMLIST(String SPNAME, int TOUR_ID, int listid, string FILENAME)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, TOUR_ID);
               db.AddInParameter(dbCmd, "@ROOM_LIST_ID", DbType.Int32, listid);
               db.AddInParameter(dbCmd, "@ROOM_LIST_FILE", DbType.String, FILENAME);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {
               throw ex;
           }

           return dsData;
       }

       public DataSet DELETEROOMLIST(String SPNAME, int TOUR_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, TOUR_ID);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {
               throw ex;
           }

           return dsData;
       }

       public DataSet GETROOMLIST(String SPNAME, int TOUR_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, TOUR_ID);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {
               throw ex;
           }

           return dsData;
       }

       /* GET TIME FOR SITE SEEING */
       public DataSet getTimeForSiteSeeing(String SPNAME, String siteName, String cityName)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(SPNAME);
               db.AddInParameter(dbCmd, "@SITE_SEEING_NAME", DbType.String, siteName);
               db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, cityName);

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
