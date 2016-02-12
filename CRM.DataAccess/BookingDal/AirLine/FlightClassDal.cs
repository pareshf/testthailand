#region Program Information
/**********************************************************************************************************************************************
 Class Name           : FlightClassDal
 Class Description    : Implementation Logic FlightClassDal database releated transaction.
 Author               : Priyam.
 Created Date         : Mar 24, 2010
***********************************************************************************************************************************************/
#endregion


#region Impoerts assemblies
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using CRM.Model.AdministrationModel;
using CRM.Model.Booking.AirlineModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
#endregion
namespace CRM.DataAccess.BookingDal.AirLine
{
    public class FlightClassDal
    {
        #region Get FlightClass
        /// <summary>
        /// Gets FlightClass list.
        /// </summary>
        /// <returns>Returns dataset contains FlightClass data.</returns>
        public DataSet GetFlightClass(String ClassName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_FLIGHT_CLASS_SELECT);
                if (!String.IsNullOrEmpty(ClassName))
                    db.AddInParameter(dbCmd, "@CLASS_NAME", DbType.String, ClassName);

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

        #region Insert FlightClass
        /// <summary>
        /// Insert FlightClass detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertFlightClass(LookupBDto objLookupBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_FLIGHT_CLASS_INSERT);
                db.AddInParameter(dbCmd, "@CLASS_NAME", DbType.String, objLookupBDto.LookupName);
                db.AddInParameter(dbCmd, "@CLASS_SHORT_NAME", DbType.String, objLookupBDto.LookupShortName);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, objLookupBDto.UserProfile.UserId);
                db.AddOutParameter(dbCmd, "@ISEXIST", DbType.Int32, 1);
                int a = db.ExecuteNonQuery(dbCmd);
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

        #region DeleteFlightClass
        /// <summary>
        /// Delete FlightClass detail.
        /// </summary>
        /// <param name="idCollections">FlightClass Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteFlightClass(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_FLIGHT_CLASS_DELETE);
                db.AddInParameter(dbCmd, "@CLASS_ID", DbType.String, idCollections);
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

        #region Update FlightClass
        /// <summary>
        /// Update FlightClass detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateFlightClass(String xmlData)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_FLIGHT_CLASS_UPDATE);
                db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml, xmlData);
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

        #region IDisposable Members

        public void Dispose()
        {
            GC.Collect();
        }

        #endregion
    }
}
