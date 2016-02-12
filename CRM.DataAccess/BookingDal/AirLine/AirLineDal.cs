#region Program Information
/**********************************************************************************************************************************************
 Class Name           : AirLineDal
 Class Description    : Implementation Logic AirLineDal database releated transaction.
 Author               : Priyam.
 Created Date         : Mar 13, 2010
***********************************************************************************************************************************************/
#endregion

#region Impoerts assemblies
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.Model.Booking.AirlineModel;
#endregion

namespace CRM.DataAccess.BookingDal.AirLine
{
    public class AirLineDal
    {
        #region Get AirLine
        /// <summary>
        /// Gets AirLine list.
        /// </summary>
        /// <returns>Returns dataset contains AirLine data.</returns>
        public DataSet GetAirLine(String AirLineCode)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AIRLINE_SELECT);
                if (!String.IsNullOrEmpty(AirLineCode))
                    db.AddInParameter(dbCmd, "@AIRLINE_CODE", DbType.String, AirLineCode);

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

        #region Insert AirLine
        /// <summary>
        /// Insert AirLine detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertAirLine(AirLineMasterBDto objAirLineMasterBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AIRLINE_INSERT);
                db.AddInParameter(dbCmd, "@AIRLINE_CODE", DbType.String, objAirLineMasterBDto.AirLineCode);
                db.AddInParameter(dbCmd, "@AIRLINE_NAME", DbType.String, objAirLineMasterBDto.AirLineName);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, objAirLineMasterBDto.UserId);
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

        #region DeleteAirLine
        /// <summary>
        /// Delete AirLine detail.
        /// </summary>
        /// <param name="idCollections">Qualification Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteAirLine(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AIRLINE_DELETE);
                db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.String, idCollections);
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

        #region Update AirLine
        /// <summary>
        /// Update AirLine detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateAirLine(String xmlData)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AIRLINE_UPDATE);
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
