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
    public class GeograficLocationStoredProcedure
    {
        public void InsertUpdateCountryName(ArrayList country)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_COUNTRY_FOR_GEOGRAPHIC_LOCATION");
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, country[1]);
                db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, country[0]);
                db.AddInParameter(dbCmd, "@COUNTRY_CODE", DbType.String, country[2]);
                db.AddInParameter(dbCmd, "@COUNTRY_CURRENCY_SYMBOL", DbType.String, country[3]);
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, country[4]);
                db.AddInParameter(dbCmd, "@CONTINENT", DbType.String, country[5]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(country[6]));
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

        public void delCountryName(int delcountry)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_COMMON_COUNTRY_MASTER");
                db.AddInParameter(dbCmd, "@COUNTRYID", DbType.Int32, delcountry);
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
        public void InsertUpdateStateName(ArrayList state)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_STATE_FOR_COMMON_STATE_MASTER");
                db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, state[1]);
                db.AddInParameter(dbCmd, "@STATE_NAME", DbType.String, state[0]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(state[2]));
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
        public void InsertNewState(string COUNTRY_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_STATE");
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, Convert.ToInt32(COUNTRY_ID));
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
        public void InsertNewCity(string COUNTRY_ID,string STATE_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_CITY");
                db.AddInParameter(dbCmd, "@COUNTRYID", DbType.Int32, Convert.ToInt32(COUNTRY_ID));
                db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, Convert.ToInt32(STATE_ID));

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
        public void InsertUpdateCityName(ArrayList City)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CITY_FOR_COMMON_CITY_MASTER");
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, City[1]);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, City[0]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(City[2]));
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
        public void deleteState(int STATE_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_COMMON_STATE_MASTER");
                db.AddInParameter(dbCmd, "@STATEID", DbType.Int32, STATE_ID);
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
        public void deleteCity(int CITY_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_COMMON_CITY_MASTER");
                db.AddInParameter(dbCmd, "@CITYID", DbType.Int32, CITY_ID);
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
