#region Program Information
/**********************************************************************************************************************************************
 Class Name           : Currency AgentMaster
 Class Description    : Implementation Logic Currency database releated transaction.
 Author               : Priyam.
 Created Date         : Mar 19, 2010
***********************************************************************************************************************************************/
#endregion

#region Impoerts assemblies
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
#endregion


namespace CRM.DataAccess.BookingDal
{
    public class CurrencyAgentMasterDal : IDisposable
    {

        #region Get CurrencyAgent
        /// <summary>
        /// Gets AddressType list.
        /// </summary>
        /// <returns>Returns dataset contains AddressType data.</returns>
        public DataSet GetCurrencyAgent(String SearchParameter)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_FOREIGN_CURRENCY_AGENT_SELECT);
                if (!String.IsNullOrEmpty(SearchParameter))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, SearchParameter);
                else
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, DBNull.Value);
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

        #region Insert CurrencyAgent
        /// <summary>
        /// Insert CurrencyAgent detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertCurrencyAgent(AgentBDto objAgentBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_FOREIGN_CURRENCY_AGENT_INSERT);
                db.AddInParameter(dbCmd, "@FOREIGN_CURRENCY_AGENT_NAME", DbType.String, objAgentBDto.AgentName);
                db.AddInParameter(dbCmd, "@EMAIL", DbType.String, objAgentBDto.Email);
                db.AddInParameter(dbCmd, "@PHONE", DbType.String, objAgentBDto.Phone);
                db.AddInParameter(dbCmd, "@MOBILE", DbType.String, objAgentBDto.Mobile);
                db.AddInParameter(dbCmd, "@FAX", DbType.String, objAgentBDto.Fax);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, objAgentBDto.UserId);
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

        #region Delete CurrencyAgent
        /// <summary>
        /// Delete Title detail.
        /// </summary>
        /// <param name="idCollections">AddressType Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteCurrencyAgent(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_FOREIGN_CURRENCY_AGENT_DELETE);
                db.AddInParameter(dbCmd, "@FOREIGN_CURRENCY_AGENT_ID", DbType.String, idCollections);
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

        #region Update  CurrencyAgent
        /// <summary>
        /// Update Address detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateCurrencyAgent(String xmlData)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_FOREIGN_CURRENCY_AGENT_UPDATE);
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
