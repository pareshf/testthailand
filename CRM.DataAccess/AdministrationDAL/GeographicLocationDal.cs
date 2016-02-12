#region Program Information
/**********************************************************************************************************************************************
 Class Name           : GeographicLocation Lookup
 Class Description    : Implementation Logic GeographicLocation Lookup database releated transaction.
 Author               : chirag
 Created Date         : Mar 9, 2010
***********************************************************************************************************************************************/
#endregion


#region Impoerts assemblies
using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Data.SqlClient;
#endregion

namespace CRM.DataAccess.AdministrationDAL
{
    public class GeographicLocationDal : IDisposable
    {
        #region Geographical Location (Country)
        #region Get CountryName
        /// <summary>
        /// Gets CountryName list.
        /// </summary>
        /// <returns>Returns dataset contains religionName data.</returns>
        public DataSet GetCountryName(String countryName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COUNTRY_SELECT);
                if (!String.IsNullOrEmpty(countryName))
                    db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, countryName);
                else
                    db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, DBNull.Value);
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

        #region Insert CountryName
        /// <summary>
        /// Insert Country Name.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertCountryName(GeographicLocationBDto GeographicLocation)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COUNTRY_INSERT);
                db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String,GeographicLocation.CountryName);
                db.AddInParameter(dbCmd, "@COUNTRY_CODE", DbType.String, GeographicLocation.CountryCode);
                db.AddInParameter(dbCmd, "@COUNTRY_CURRENCY_SYMBOL", DbType.String, GeographicLocation.CurrencySymbol);
                db.AddInParameter(dbCmd, "@COUNTRY_CURRENCY_NAME", DbType.String, GeographicLocation.CurrencyName);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32,GeographicLocation.UserProfile.UserId);
                db.AddOutParameter(dbCmd, "@ISEXIST", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ISEXIST"));
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
            return Result;
        }

        #endregion

        #region DeleteCountry
        /// <summary>
        /// Delete Country detail.
        /// </summary>
        /// <param name="idCollections">Country Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteCountry(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COUNTRY_DELETE);
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.String, idCollections);
                db.AddOutParameter(dbCmd, "@ERRORCODE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ERRORCODE"));
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
            return Result;
        }
        #endregion

        #region Update Country
        /// <summary>
        /// Update Country detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateCountry(String xmlDataCountry)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COUNTRY_UPDATE);
                db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml, xmlDataCountry);
                db.AddOutParameter(dbCmd, "@IS_EXIST", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_EXIST"));
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
            return Result;

        }
        #endregion

        #endregion

        #region Geographical Location (State)
        #region Get StateName
        /// <summary>
        /// Gets StateName list.
        /// </summary>
        /// <returns>Returns dataset contains religionName data.</returns>
        public DataSet GetStateName(String stateName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_STATE_SELECT);
                if (!String.IsNullOrEmpty(stateName))
                    db.AddInParameter(dbCmd, "@STATE_NAME", DbType.String, stateName);
                else
                    db.AddInParameter(dbCmd, "@STATE_NAME", DbType.String, DBNull.Value);
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

        #region Insert StateName
        /// <summary>
        /// Insert State Name.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertState(GeographicLocationBDto GeographicLocation)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_STATE_INSERT);
                db.AddInParameter(dbCmd, "COUNTRY_ID", DbType.Int32, GeographicLocation.CountryId);
                db.AddInParameter(dbCmd, "@STATE_NAME", DbType.String, GeographicLocation.StateName);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, GeographicLocation.UserProfile.UserId);
                db.AddOutParameter(dbCmd, "@ISEXIST", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ISEXIST"));
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
            return Result;
        }

        #endregion

        #region DeleteState
        /// <summary>
        /// Delete State detail.
        /// </summary>
        /// <param name="idCollections">State Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteState(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_STATE_DELETE);
                db.AddInParameter(dbCmd, "@STATE_ID", DbType.String, idCollections);
                db.AddOutParameter(dbCmd, "@ERRORCODE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ERRORCODE"));
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
            return Result;
        }
        #endregion

        #region Update State
        /// <summary>
        /// Update State detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateState(String xmlDataState)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_STATE_UPDATE);
                db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml,xmlDataState);
                db.AddOutParameter(dbCmd, "@IS_EXIST", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_EXIST"));
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
            return Result;
        }
        #endregion

        #endregion

        #region Geographical Location (City)
        #region Get CityName
        /// <summary>
        /// Gets CityName list.
        /// </summary>
        /// <returns>Returns dataset contains cityName data.</returns>
        public DataSet GetCityName(String cityName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_CITY_SELECT);
                if (!String.IsNullOrEmpty(cityName))
                    db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, cityName);
                else
                    db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, DBNull.Value);
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

        #region Insert CityName
        /// <summary>
        /// Insert City Name.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertCity(GeographicLocationBDto GeographicLocation)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_CITY_INSERT);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, GeographicLocation.CityName);
                db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, GeographicLocation.StateId);
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, GeographicLocation.CountryId);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, GeographicLocation.UserProfile.UserId);
                db.AddOutParameter(dbCmd, "@ISEXIST", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ISEXIST"));
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
            return Result;
        }

        #endregion

        #region DeleteCity
        /// <summary>
        /// Delete City detail.
        /// </summary>
        /// <param name="idCollections">City Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteCity(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_CITY_DELETE);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.String, idCollections);
                db.AddOutParameter(dbCmd, "@ERRORCODE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ERRORCODE"));
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
            return Result;
        }
        #endregion

        #region Update City
        /// <summary>
        /// Update City detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateCity(String xmlDataCity)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_CITY_UPDATE);
                db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml, xmlDataCity);
                db.AddOutParameter(dbCmd, "@IS_EXIST", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_EXIST"));
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
            return Result;
        }
        #endregion

        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
    }
}
