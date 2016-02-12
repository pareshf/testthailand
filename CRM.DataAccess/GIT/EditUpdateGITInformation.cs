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
    public  class EditUpdateGITInformation
    {

        /******************************************************* HOTELS ***************************************/

        // GET HOTEL FOR EDIT IN SECOND STEP
        public DataSet GetHotelName(int GIT_TOUR_ID, String CITY)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_HOTEL_FOR_EDIT");
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                db.AddInParameter(dbCmd, "@CITY", DbType.String, CITY);


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

        // GET PRICE WHILE CLICK ON RATE OF HOTEL
        public DataSet GetHotelRate(int GIT_TOUR_ID, String HOTEL_NAME, String ROOM_TYPE, String CITY)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_HOTEL_RATE");
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, HOTEL_NAME);
                db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, ROOM_TYPE);
                db.AddInParameter(dbCmd, "@CITY", DbType.String, CITY);


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

        // SAVE THE PRICE AFTER DOING EDIT
        public void saveHotelRate(int GIT_HOTEL_CART_ID, String SINGLE_ROOM_RATE, String DOUBLE_ROOM_RATE, String TRIPLE_ROOM_RATE, String EXTRA_ADULT_RATE, String EXTRA_CWB_COST, String EXTRA_CNB_COST)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_HOTEL_RATE");
                db.AddInParameter(dbCmd, "@GIT_HOTEL_CART_ID", DbType.Int32, GIT_HOTEL_CART_ID);
                if (SINGLE_ROOM_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@SINGLE_ROOM_RATE", DbType.Decimal, 0.00);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@SINGLE_ROOM_RATE", DbType.Decimal, decimal.Parse(SINGLE_ROOM_RATE));
                }

                if (DOUBLE_ROOM_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@DOUBLE_ROOM_RATE", DbType.Decimal, 0.00);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DOUBLE_ROOM_RATE", DbType.Decimal, decimal.Parse(DOUBLE_ROOM_RATE));
                }

                if (TRIPLE_ROOM_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TRIPLE_ROOM_RATE", DbType.Decimal, 0.00);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TRIPLE_ROOM_RATE", DbType.Decimal, decimal.Parse(TRIPLE_ROOM_RATE));
                }

                if (EXTRA_ADULT_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@EXTRA_ADULT_RATE", DbType.Decimal, 0.00);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EXTRA_ADULT_RATE", DbType.Decimal, decimal.Parse(EXTRA_ADULT_RATE));
                }

                if (EXTRA_CWB_COST.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@EXTRA_CWB_COST", DbType.Decimal, 0.00);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EXTRA_CWB_COST", DbType.Decimal, decimal.Parse(EXTRA_CWB_COST));
                }

                if (EXTRA_CNB_COST.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@EXTRA_CNB_COST", DbType.Decimal, 0.00);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EXTRA_CNB_COST", DbType.Decimal, decimal.Parse(EXTRA_CNB_COST));
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

        /******************************************************* MEALS / RESTURANTS ***************************************/
        public DataSet GetResturantName(int GIT_TOUR_ID, String CITY)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_RESTURANT_FOR_EDIT");
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                db.AddInParameter(dbCmd, "@CITY", DbType.String, CITY);


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

        public DataSet GetResturantRate(int GIT_TOUR_ID, String SUPPLIER_CHAIN_NAME, String MEAL_TYPE, String CITY)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_RESTURANT_FOR_RATE");
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                db.AddInParameter(dbCmd, "@SUPPLIER_CHAIN_NAME", DbType.String, SUPPLIER_CHAIN_NAME);
                db.AddInParameter(dbCmd, "@MEAL_TYPE", DbType.String, MEAL_TYPE);
                db.AddInParameter(dbCmd, "@CITY", DbType.String, CITY);


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

        public DataSet saveResturantRate(int GIT_RESTAURENT_CART_ID, String ADULT_RATE, String CHILD_RATE)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_RESTURANTS_RATE");
                db.AddInParameter(dbCmd, "@GIT_RESTAURENT_CART_ID", DbType.Int32, GIT_RESTAURENT_CART_ID);

                if (ADULT_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, 0.00);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, decimal.Parse(ADULT_RATE));
                }

                if (CHILD_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, 0.00);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, decimal.Parse(CHILD_RATE));
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

        /******************************************************* CONFERENCES ***************************************/
        public DataSet GetConferenceHotelName(int GIT_TOUR_ID, String CITY)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_CONFERENCE_FOR_EDIT");
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                db.AddInParameter(dbCmd, "@CITY", DbType.String, CITY);


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

        public DataSet GetConferenceRate(int GIT_TOUR_ID, String SUPPLIER_CHAIN_NAME, String MEAL_TYPE, String CITY)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_CONFERENCE_FOR_RATE");
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                db.AddInParameter(dbCmd, "@SUPPLIER_CHAIN_NAME", DbType.String, SUPPLIER_CHAIN_NAME);
                db.AddInParameter(dbCmd, "@CONFERENCE_TYPE", DbType.String, MEAL_TYPE);
                db.AddInParameter(dbCmd, "@CITY", DbType.String, CITY);


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

        public DataSet saveConferenceRate(int GIT_RESTAURENT_CART_ID, String ADULT_RATE, String CHILD_RATE)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_CONFERENCE_RATE");
                db.AddInParameter(dbCmd, "@GIT_CONFERENCE_CART_ID", DbType.Int32, GIT_RESTAURENT_CART_ID);

                if (ADULT_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, 0.00);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, decimal.Parse(ADULT_RATE));
                }

                if (CHILD_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, 0.00);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, decimal.Parse(CHILD_RATE));
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

        /******************************************************* GALA DINNER ***************************************/
        public DataSet GetGalaDinnerHotelName(int GIT_TOUR_ID, String CITY)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_GALA_DINNER_FOR_EDIT");
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                db.AddInParameter(dbCmd, "@CITY", DbType.String, CITY);


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

        public DataSet GetGalaDinnerRate(int GIT_TOUR_ID, String SUPPLIER_CHAIN_NAME, String MEAL_TYPE, String CITY)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_GALA_DINNER_FOR_RATE");
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                db.AddInParameter(dbCmd, "@SUPPLIER_CHAIN_NAME", DbType.String, SUPPLIER_CHAIN_NAME);
                db.AddInParameter(dbCmd, "@CONFERENCE_TYPE", DbType.String, MEAL_TYPE);
                db.AddInParameter(dbCmd, "@CITY", DbType.String, CITY);


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

        public DataSet saveGalaDinnerRate(int GIT_RESTAURENT_CART_ID, String ADULT_RATE, String CHILD_RATE)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_GALA_DINNER_RATE");
                db.AddInParameter(dbCmd, "@GIT_GALA_DINNER_CART_ID", DbType.Int32, GIT_RESTAURENT_CART_ID);

                if (ADULT_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, 0.00);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, decimal.Parse(ADULT_RATE));
                }

                if (CHILD_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, 0.00);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, decimal.Parse(CHILD_RATE));
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

        /******************************************************* SITE SEEING ***************************************/
        public DataSet GetSiteSeeing(int GIT_TOUR_ID, String CITY)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_SITE_SEEING_FOR_EDIT");
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                db.AddInParameter(dbCmd, "@CITY", DbType.String, CITY);


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

        public DataSet GetSiteSeeingRate(int GIT_TOUR_ID, String SUPPLIER_CHAIN_NAME,  String CITY)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_SITE_SEEING_FOR_RATE");
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                db.AddInParameter(dbCmd, "@SUPPLIER_CHAIN_NAME", DbType.String, SUPPLIER_CHAIN_NAME);
                
                db.AddInParameter(dbCmd, "@CITY", DbType.String, CITY);


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

        public DataSet saveSiteSeeingRate(int GIT_SIGHT_CART_ID, String ADULT_RATE, String CHILD_RATE)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_SITE_SEEING_RATE");
                db.AddInParameter(dbCmd, "@GIT_SIGHT_CART_ID", DbType.Int32, GIT_SIGHT_CART_ID);

                if (ADULT_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, 0.00);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, decimal.Parse(ADULT_RATE));
                }

                if (CHILD_RATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, 0.00);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, decimal.Parse(CHILD_RATE));
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

        /******************************************************* TRANSPORT PACKAGE ***************************************/
         public DataSet GetTransferCartId(int GIT_TOUR_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_TRASPORT_CART_ID");
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

         /******************************************************* COACH ***************************************/
         public DataSet GetCoach(int GIT_TOUR_ID, int TRANSPORT_PACKAGE_ID)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet ds = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("GET_COACH_FOR_EDIT");
                 db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                 db.AddInParameter(dbCmd, "@TRANSPORT_PACKAGE_ID", DbType.String, TRANSPORT_PACKAGE_ID);


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

         public DataSet GetCoachRate(int GIT_TOUR_ID, int TRANSPORT_PACKAGE_ID, String SUPPLIER_NAME)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet ds = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("GET_COACH_FOR_RATE");
                 db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                 db.AddInParameter(dbCmd, "@TRANSPORT_PACKAGE_ID", DbType.String, TRANSPORT_PACKAGE_ID);
                 db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, SUPPLIER_NAME);
                


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

         public DataSet saveCoachRate(int GIT_COACH_CART_ID, String ADULT_RATE, String CHILD_RATE)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet ds = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("UPDATE_COACH_RATE");
                 db.AddInParameter(dbCmd, "@GIT_COACH_CART_ID", DbType.Int32, GIT_COACH_CART_ID);

                 if (ADULT_RATE.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, 0.00);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, decimal.Parse(ADULT_RATE));
                 }

                 if (CHILD_RATE.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, 0.00);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, decimal.Parse(CHILD_RATE));
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

         /******************************************************* BOAT ***************************************/
         public DataSet GetBoat(int GIT_TOUR_ID, int TRANSPORT_PACKAGE_ID)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet ds = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("GET_BOAT_FOR_EDIT");
                 db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                 db.AddInParameter(dbCmd, "@TRANSPORT_PACKAGE_ID", DbType.String, TRANSPORT_PACKAGE_ID);


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

         public DataSet GetBoatRate(int GIT_TOUR_ID, int TRANSPORT_PACKAGE_ID, String SUPPLIER_NAME)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet ds = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("GET_BOAT_FOR_RATE");
                 db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                 db.AddInParameter(dbCmd, "@TRANSPORT_PACKAGE_ID", DbType.String, TRANSPORT_PACKAGE_ID);
                 db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, SUPPLIER_NAME);



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

         public DataSet saveBoatRate(int GIT_BOAT_CART_ID, String ADULT_RATE, String CHILD_RATE)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet ds = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("UPDATE_BOAT_RATE");
                 db.AddInParameter(dbCmd, "@GIT_BOAT_CART_ID", DbType.Int32, GIT_BOAT_CART_ID);

                 if (ADULT_RATE.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, 0.00);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, decimal.Parse(ADULT_RATE));
                 }

                 if (CHILD_RATE.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, 0.00);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, decimal.Parse(CHILD_RATE));
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

         /******************************************************* GUIDE ***************************************/
         public DataSet GetGuide(int GIT_TOUR_ID, int TRANSPORT_PACKAGE_ID)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet ds = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("GET_GUIDE_FOR_EDIT");
                 db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                 db.AddInParameter(dbCmd, "@TRANSPORT_PACKAGE_ID", DbType.String, TRANSPORT_PACKAGE_ID);


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

         public DataSet GetGuideRate(int GIT_TOUR_ID, int TRANSPORT_PACKAGE_ID, String SUPPLIER_NAME)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet ds = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("GET_GUIDE_FOR_RATE");
                 db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                 db.AddInParameter(dbCmd, "@TRANSPORT_PACKAGE_ID", DbType.String, TRANSPORT_PACKAGE_ID);
                 db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, SUPPLIER_NAME);



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

         public DataSet saveGuideRate(int GIT_GUIDE_CART_ID, String ADULT_RATE, String CHILD_RATE)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet ds = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("UPDATE_GUIDE_RATE");
                 db.AddInParameter(dbCmd, "@GIT_GUIDE_CART_ID", DbType.Int32, GIT_GUIDE_CART_ID);

                 if (ADULT_RATE.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, 0.00);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, decimal.Parse(ADULT_RATE));
                 }

                 if (CHILD_RATE.ToString().Equals(""))
                 {
                     db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, 0.00);
                 }
                 else
                 {
                     db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, decimal.Parse(CHILD_RATE));
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



         /*********************************************** Aditional *******************************************/

         public DataSet GetAdditionalServices(int GIT_TOUR_ID)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet ds = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("FETCH_ADDITIONAL_SERVICES_FOR_EDIT");
                 db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
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
    }
}
