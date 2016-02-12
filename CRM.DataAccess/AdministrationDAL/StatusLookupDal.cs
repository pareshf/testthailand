#region Program Information
/**********************************************************************************************************************************************
 Class Name           : StatusLookUpDal
 Class Description    : Implementation Logic customer database releated transaction.
 Author               : Chirag.
 Created Date         : Mar 13, 2010
***********************************************************************************************************************************************/
#endregion

#region Impoerts assemblies
using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
#endregion

namespace CRM.DataAccess.AdministrationDAL
{
    public class StatusLookupDal : IDisposable
    {
        #region Get StatusName
        /// <summary>
        /// Gets StatusName list.
        /// </summary>
        /// <returns>Returns dataset contains StatusName data.</returns>
        public DataSet GetStatusName(String statusName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_STATUS_SELECT);
                if (!String.IsNullOrEmpty(statusName))
                    db.AddInParameter(dbCmd, "@STATUS_NAME", DbType.String, statusName);
                else
                    db.AddInParameter(dbCmd, "@STATUS_NAME", DbType.String, DBNull.Value);
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

        #region Insert Status
        /// <summary>
        /// Insert Status detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertStatus(LookupBDto StatusName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_STATUS_INSERT);
                db.AddInParameter(dbCmd, "@STATUS_NAME", DbType.String, StatusName.LookupName);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, StatusName.UserProfile.UserId);
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

        #region Update Status
        /// <summary>
        /// Update Status detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateStatus(String xmlData)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_STATUS_UPDATE);
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

        #region DeleteStatus
        /// <summary>
        /// Delete Status detail.
        /// </summary>
        /// <param name="idCollections">Status Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteStatus(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_STATUS_DELETE);
                db.AddInParameter(dbCmd, "@STATUS_ID", DbType.String, idCollections);
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

        #region IDisposable Members
        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
    }
}
