using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;

namespace CRM.DataAccess.AdministratorEntity
{
   public class NextTravelPlanStoredProcedure
    {
        public void InsertUpdateTravelPlan(ArrayList TravelPlan)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_CUSTOMER_NEXT_TRAVEL_PLAN");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, TravelPlan[0]);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, TravelPlan[1]);
                if (TravelPlan[2].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@NEXT_TRAVEL_PLAN_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                db.AddInParameter(dbCmd, "@NEXT_TRAVEL_PLAN_DATE", DbType.DateTime, DateTime.ParseExact(TravelPlan[2].ToString(), "dd/MM/yyyy", null));              
                }
                //db.AddInParameter(dbCmd, "@NEXT_TRAVEL_PLAN_DATE", DbType.DateTime, TravelPlan[2]);
                db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, TravelPlan[6]);
                db.AddInParameter(dbCmd, "@NO_OF_PERSON", DbType.Int32, TravelPlan[4]);
                db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, TravelPlan[5]);
                db.AddInParameter(dbCmd, "@STATE_NAME", DbType.String, TravelPlan[7]);
                db.AddInParameter(dbCmd, "@REGION_NAME", DbType.String, TravelPlan[3]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(TravelPlan[8]));
                
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

        public void InsertNewTravelPlansp(string CUST_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_NEXT_TRAVEL_PLAN_ADD_ANOTHER");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, Convert.ToInt32(CUST_ID));
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

        public void InsertNewTravelPlan2(string CUST_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_NEXT_TRAVEL_PLAN2_ADD_ANOTHER");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, Convert.ToInt32(CUST_ID));
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

        public void InsertNewTravelPlan3(string CUST_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_NEXT_TRAVEL_PLAN3_ADD_ANOTHER");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, Convert.ToInt32(CUST_ID));
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

        public void InsertNewTravelPlan4(string CUST_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_NEXT_TRAVEL_PLAN4_ADD_ANOTHER");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, Convert.ToInt32(CUST_ID));
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

        public void InsertNewTravelPlan5(string CUST_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_NEXT_TRAVEL_PLAN5_ADD_ANOTHER");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, Convert.ToInt32(CUST_ID));
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
        public void InsertUpdateTravelHistory1(ArrayList  History)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CUSTOMER_TRAVEL_HISTORY_WITH_US");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, History[0]);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, History[1]);
                if (History[2].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@CUSTOMER_TRAVEL_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {              
                db.AddInParameter(dbCmd, "@CUSTOMER_TRAVEL_DATE", DbType.DateTime, DateTime.ParseExact(History[2].ToString(), "dd/MM/yyyy", null));
                }
                //db.AddInParameter(dbCmd, "@CUSTOMER_TRAVEL_DATE", DbType.DateTime, History[2]);
                db.AddInParameter(dbCmd, "@NO_OF_PERSON", DbType.Int32, History[3]);
                db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, History[4]);
                db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, History[5]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(History[6]));
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
        public void InsertUpdateTravelWithOther(ArrayList Other)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CUSTOMER_TRAVEL_WITH_OTHER");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, Other[0]);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, Other[1]);
                if (Other[2].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@TRAVEL_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {

                    db.AddInParameter(dbCmd, "@TRAVEL_DATE", DbType.DateTime, DateTime.ParseExact(Other[2].ToString(), "dd/MM/yyyy", null));
                }
                //db.AddInParameter(dbCmd, "@TRAVEL_DATE", DbType.DateTime, Other[2]);
                //db.AddInParameter(dbCmd, "@TOUR_TYPE", DbType.String, Other[3]);
                db.AddInParameter(dbCmd, "@NO_OF_PERSON", DbType.Int32, Other[4]);
                db.AddInParameter(dbCmd, "@DESCRIPATION", DbType.String, Other[5]);
                db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, Other[6]);
                db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.String, Other[7]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Other[8]));
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
        public void deleteNextTravelPlan(int NextTravelPlan)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_CUSTOMER_NEXT_TRAVEL_PLAN");
                db.AddInParameter(dbCmd, "@SRNO", DbType.Int32, NextTravelPlan);
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
        public void deleteTravelHistory(int TravelHistory)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_CUSTOMER_HISTORY_WITH_US");
                db.AddInParameter(dbCmd, "@SRNO", DbType.Int32, TravelHistory);
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
        public void deleteTravelHistoryWithOther(int Historywithother)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_CUSTOMER_HISTORY_WITH_OTHER");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, Historywithother);
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
        public void InsertUpdateAirLineDetail(ArrayList Airline)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_CUSTOMER_AIR_LINE_DETAIL");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, Airline[0]);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, Airline[1]);
                db.AddInParameter(dbCmd, "CUST_REL_SRNO", DbType.Int32, Airline[2]);
                db.AddInParameter(dbCmd, "@PREF_AIRLINE_NAME", DbType.String, Airline[3]);
                db.AddInParameter(dbCmd, "@PREF_AIRLINE_CLASS", DbType.String, Airline[4]);
                db.AddInParameter(dbCmd, "@FREQUENTLY_FLY_NO", DbType.String, Airline[5]);
                db.AddInParameter(dbCmd, "@CORPORATE_CLIENT_NO", DbType.String, Airline[6]);
                db.AddInParameter(dbCmd, "@cust_rel_id", DbType.String, Airline[7]);
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
        public void deleteAirlinedetail(int delairline)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_AIR_LINE_DETAIL");
                db.AddInParameter(dbCmd, "@SRNO", DbType.Int32, delairline);
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
        public void InsertUpdateVisaDetail(ArrayList Visa)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_CUSTOMER_VISA_DETAIL");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, Visa[0]);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, Visa[1]);
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, Visa[2]);
                db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, Visa[3]);
                //db.AddInParameter(dbCmd, "@VISA_EXPIRY_DATE", DbType.DateTime, Visa[4]);
                db.AddInParameter(dbCmd, "@cust_rel_id", DbType.Int32, Visa[5]);
                if (Visa[4].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@VISA_EXPIRY_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {

                    db.AddInParameter(dbCmd, "@VISA_EXPIRY_DATE", DbType.DateTime, DateTime.ParseExact(Visa[4].ToString(), "dd/MM/yyyy", null));
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
        public void deleteVisaDetail(int delvisa)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_CUSTOMER_VISA_DETAIL");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, delvisa);
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
