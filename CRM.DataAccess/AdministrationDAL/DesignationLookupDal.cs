#region Program Information
/**********************************************************************************************************************************************
 Class Name           : DesignationLookupDal
 Class Description    : Implementation Logic DesignationLookupDal database releated transaction.
 Author               : Priyam.
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
   public class DesignationLookupDal : IDisposable
   {
       #region Get Designation
       /// <summary>
       /// Gets Designation list.
        /// </summary>
       /// <returns>Returns dataset contains Designation data.</returns>
       public DataSet GetDesignation(String DesignationDesc)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_DESIGNATION_SELECT);
                if (!String.IsNullOrEmpty(DesignationDesc))
                    db.AddInParameter(dbCmd, "@DESIGNATION_DESC", DbType.String, DesignationDesc);
                else
                    db.AddInParameter(dbCmd, "@DESIGNATION_DESC", DbType.String, DBNull.Value);
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

       #region Insert Designation
       /// <summary>
       /// Insert Designation detail.
       /// </summary>
       /// <param name="xmlData">Data that converted into xml format.</param>
       /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
       public int InsertDesignation(LookupBDto objLookupBDto)
       {
           Database db = null;
           DbCommand dbCmd = null;
           int Result = 0;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_DESIGNATION_INSERT);
               db.AddInParameter(dbCmd, "@DESIGNATION_DESC", DbType.String, objLookupBDto.LookupName);
               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, objLookupBDto.UserProfile.UserId);
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

       #region DeleteDesignation
       /// <summary>
       /// Delete Designation detail.
       /// </summary>
       /// <param name="idCollections">Designation Id collection seperated by commas.</param>
       /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
       public int DeleteDesignation(String idCollections)
       {
           Database db = null;
           DbCommand dbCmd = null;
           int Result = 0;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_DESIGNATION_DELETE);
               db.AddInParameter(dbCmd, "@DESIGNATION_ID", DbType.String, idCollections);
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

       #region Update Designation
       /// <summary>
       /// Update Designation detail.
       /// </summary>
       /// <param name="xmlData">Data that converted into xml format.</param>
       /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
       public int UpdateDesignation(String xmlData)
       {
           Database db = null;
           DbCommand dbCmd = null;
           int Result = 0;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_DESIGNATION_UPDATE);
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
