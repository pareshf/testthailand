#region Program Information
/**********************************************************************************************************************************************
 Class Name           : CurrencyLookupDal
 Class Description    : Implementation Logic CurrencyLookupDal database releated transaction.
 Author               : Priyam.
 Created Date         : Mar 29, 2010
***********************************************************************************************************************************************/
#endregion

#region Impoerts assemblies

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Data;
#endregion

namespace CRM.DataAccess.AdministrationDAL
{
    public class CurrencyLookupDal
    {
        #region Get Currency
        /// <summary>
        /// Gets Currency list.
        /// </summary>
        /// <returns>Returns dataset contains RoomType data.</returns>
        public DataSet GetCurrency(String CurrencyName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_CURRENCY_SELECT);
                if (!String.IsNullOrEmpty(CurrencyName))
                    db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, CurrencyName);
                else
                    db.AddInParameter(dbCmd, "@CURRENCY_SYMBOL", DbType.String, DBNull.Value);
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

        #region Insert Currency
        /// <summary>
        /// Insert RoomType detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertCurrency(CurrencyBDto objCurrencyLookupBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_CURRENCY_INSERT);
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, objCurrencyLookupBDto.CurrencyName);
                db.AddInParameter(dbCmd, "@CURRENCY_SYMBOL", DbType.String, objCurrencyLookupBDto.CurrencySymbol);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, objCurrencyLookupBDto.UserId);
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

        #region DeleteRoomType
        /// <summary>
        /// Delete RoomType detail.
        /// </summary>
        /// <param name="idCollections">RoomType Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteCurrency(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_CURRENCY_DELETE);
                db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.String, idCollections);
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

        #region Update Currency
        /// <summary>
        /// Update Currency detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateCurrency(String xmlData)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_CURRENCY_UPDATE);
                db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml, xmlData);
                db.AddOutParameter(dbCmd, "@IS_EXIST", DbType.Int32, 1);
                int x = db.ExecuteNonQuery(dbCmd);
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
