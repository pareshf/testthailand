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
    public class BookSp
    {
        #region ALL SPS USING EMAIL ON GETTING GIT BOOKING BOOK

        public DataSet getHotelForEmail(int GIT_TOUR_ID, int HOTEL_PRICE_LIST_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_HOTEL_FOR_EMAIL");
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                db.AddInParameter(dbCmd, "@HOTEL_PRICE_LIST_ID", DbType.Int32, HOTEL_PRICE_LIST_ID);
                

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

        public DataSet getGalaDinnerForEmail(int GIT_TOUR_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_GALA_DINNER_FOR_EMAIL");
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

        public DataSet getConferenceForEmail(int GIT_TOUR_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_CONF_FOR_EMAIL");
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

        public DataSet getSiteSeeingForEmail(int GIT_TOUR_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_SITE_SEEING_FOR_EMAIL");
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

        #endregion

        public void insertPassengerDetails(int GIT_TOUR_ID, String NO_OF_ADULTS, String NO_OF_CWB, String NO_OF_CNB, String NO_OF_INFANTS)
        {
              Database db = null;
            DataSet ds = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_PASSRNGER_INFORMATION");

                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);

                if (NO_OF_ADULTS.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_ADULTS", DbType.Int32,0);
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

                if (NO_OF_INFANTS.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@NO_OF_INFANTS", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@NO_OF_INFANTS", DbType.Int32, int.Parse(NO_OF_INFANTS));
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

        public void insertPaymentDateGalaDinner(int GIT_TOUR_ID, String PAYMENT_DATE, String HOTEL_NAME, String GALA_DINNER_TYPE_ID, String CITY_NAME)
        {
            Database db = null;
            DataSet ds = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_PAYMENT_DATE_CONFERENCE");

                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                if (PAYMENT_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime, DateTime.ParseExact(PAYMENT_DATE.ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, HOTEL_NAME);
                db.AddInParameter(dbCmd, "@CONFERENCE_TYPE_ID", DbType.String, GALA_DINNER_TYPE_ID);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, CITY_NAME);


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

        public void insertPaymentDateConference(int GIT_TOUR_ID, String PAYMENT_DATE, String HOTEL_NAME, String GALA_DINNER_TYPE_ID, String CITY_NAME)
        {
            Database db = null;
            DataSet ds = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_PAYMENT_DATE_GALADINNER");

                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, GIT_TOUR_ID);
                if (PAYMENT_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime, DateTime.ParseExact(PAYMENT_DATE.ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, HOTEL_NAME);
                db.AddInParameter(dbCmd, "@GALA_DINNER_TYPE_ID", DbType.String, GALA_DINNER_TYPE_ID);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, CITY_NAME);
            

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

        public void insertBookEmail(int GIT_TOUR_ID, String BOOK_EMAIL)
        {
            Database db = null;
            DataSet ds = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("ENTER_EMAIL_GIT_QUOTE_MASTER");

                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, GIT_TOUR_ID);

                db.AddInParameter(dbCmd, "@BOOK_EMAIL", DbType.String, BOOK_EMAIL);

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

        public void insertBookHotel(int GIT_TOUR_ID, int HOTEL_CART_ID)
        {
            Database db = null;
            DataSet ds = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_HOTEL_CART_FOR_BOOK");

                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, GIT_TOUR_ID);

                db.AddInParameter(dbCmd, "@HOTEL_CART_ID", DbType.String, HOTEL_CART_ID);

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

        #region  ALL SPS FOR EMAIL ON RECONFIRMATION OF GIT PACKAGE 

        public DataSet getHotelForreconfirmEmail(int GIT_TOUR_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_BOOKED_HOTELS_FOR_RECONFIRM_MAIL");
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

        public DataSet getGalaDinnerForconfirmEmail(int GIT_TOUR_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_BOOKED_GALA_DINNER_FOR_RECONFIRM_MAIL");
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

        public DataSet getConferenceForconfirmEmail(int GIT_TOUR_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_BOOKED_CONF_FOR_RECONFIRM_MAIL");
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

        public DataSet getSiteSeeingForconfirmEmail(int GIT_TOUR_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_BOOKED_SITE_FOR_RECONFIRM_MAIL");
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

        public DataSet getHotelForTobereconfirmEmail(int GIT_TOUR_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_BOOKED_HOTELS_FOR_TO_BE_RECONFIRM_MAIL");
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

        #endregion

        public DataSet GetDetailForReconfirmEmail(int GIT_TOUR_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_AGENTNAME_GROUPNAME_FOR_AGENT_EMAIL");
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
