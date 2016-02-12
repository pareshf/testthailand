#region Program Information
/**********************************************************************************************************************************************
 Class Name           : InquiriesFollowsModeDal
 Class Description    : Implementation Logic InquiriesFollowsMode database releated transaction.
 Author               : Chirag.
 Created Date         : Mar 18, 2010
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
   public class InquiriesFollowsModeLookupDal : IDisposable
   {
       #region Get InquiriesFollowsMode
       /// <summary>
       /// Gets InquiriesFollowsMode list.
        /// </summary>
       /// <returns>Returns dataset contains InquiriesFollowsMode data.</returns>
       public DataSet GetInquiriesFollowsMode(String inquiriesFollowsMode)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_SELECT);
                if (!String.IsNullOrEmpty(inquiriesFollowsMode))
                    db.AddInParameter(dbCmd, "@IF_DESC", DbType.String, inquiriesFollowsMode);
                else
                    db.AddInParameter(dbCmd, "@IF_DESC", DbType.String, DBNull.Value);
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

       #region Insert  InquiriesFollowsMode
       /// <summary>
       /// Insert  InquiriesFollowsMode detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
       public int InsertInquiriesFollowsMode(LookupBDto InquiriesFollowsMode)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_INSERT);
                db.AddInParameter(dbCmd, "@IF_DESC", DbType.String, InquiriesFollowsMode.LookupName);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, InquiriesFollowsMode.UserProfile.UserId);
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

       #region Update InquiriesFollowsMode
       /// <summary>
       /// Update InquiriesFollowsMode detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
       public int UpdateInquiriesFollowsMode(String xmlData)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_UPDATE);
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

       #region DeleteInquiriesFollowsMode
       /// <summary>
       /// Delete InquiriesFollowsMode detail.
        /// </summary>
       /// <param name="idCollections">InquiriesFollowsMode Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
       public int DeleteInquiriesFollowsMode(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_DELETE);
                db.AddInParameter(dbCmd, "@IF_ID", DbType.String, idCollections);
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
