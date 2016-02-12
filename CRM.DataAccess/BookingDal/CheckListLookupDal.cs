#region Program Information
/**********************************************************************************************************************************************
 Class Name           : CkeckListLookup
 Class Description    : Implementation Logic CkeckListLookup database releated transaction.
 Author               : Priyam.
 Created Date         : Mar 19, 2010
***********************************************************************************************************************************************/
#endregion

#region Impoerts assemblies
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Data;
using System.Data.Common;
using CRM.Model.Booking.TourBooking;

#endregion

namespace CRM.DataAccess.BookingDal 
{
    public class CheckListLookupDal : IDisposable
    {
        #region CheckListTab:

        #region Get CheckList
        /// <summary>
        /// Gets CheckList list.
        /// </summary>
        /// <returns>Returns dataset contains CheckList data.</returns>
        public DataSet GetCheckList(String CheckListFor)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_CHECKLIST_SELECT);
                if (!String.IsNullOrEmpty(CheckListFor))
                    db.AddInParameter(dbCmd, "@CHECKLIST_FOR", DbType.String, CheckListFor);
                else
                    db.AddInParameter(dbCmd, "@CHECKLIST_FOR", DbType.String, DBNull.Value);
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

        #region Insert CheckList
        /// <summary>
        /// Insert CheckList detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertCheckList(LookupBDto objLookupBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_CHECKLIST_INSERT);
                db.AddInParameter(dbCmd, "@CHECKLIST_FOR", DbType.String, objLookupBDto.LookupName);
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

        #region DeleteCheckList
        /// <summary>
        /// Delete CheckList detail.
        /// </summary>
        /// <param name="idCollections">CheckList Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteCheckList(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_CHECKLIST_DELETE);
                db.AddInParameter(dbCmd, "@CHECKLIST_ID", DbType.String, idCollections);
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

        #region Update  CheckList
        /// <summary>
        /// Update CheckList detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateCheckList(String xmlData)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_CHECKLIST_UPDATE);
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

        #endregion  

        #region CheckList Details Tab:

        #region Get checkListDetails
        /// <summary>
        /// Gets checkList list.
        /// </summary>
        /// <returns>Returns dataset contains checkList data.</returns>
        public DataSet GetChecklistDetails(int CheckListId, string CheckListDesc)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_CHECKLIST_MST_SELECT);
                if (CheckListId != 0)
                    db.AddInParameter(dbCmd, "@CHECKLIST_ID", DbType.Int32, CheckListId);
                else
                    db.AddInParameter(dbCmd, "@CHECKLIST_ID", DbType.Int32, DBNull.Value);
                if (!string.IsNullOrEmpty(CheckListDesc))
                    db.AddInParameter(dbCmd, "@CHECKLIST_DESCRIPTION", DbType.String, CheckListDesc);
                else
                    db.AddInParameter(dbCmd, "@CHECKLIST_DESCRIPTION", DbType.String, DBNull.Value);

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

        #region Insert ChecklistDetails
        /// <summary>
        /// Insert City Name.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertChecklistDetails(CheckListMasterDetailsBDto objCheckListMasterDetailsBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_CHECKLIST_MST_INSERT);
                db.AddInParameter(dbCmd, "@CHECKLIST_ID", DbType.Int32, objCheckListMasterDetailsBDto.CheckListId);
                db.AddInParameter(dbCmd, "@CHECKLIST_DESCRIPTION", DbType.String, objCheckListMasterDetailsBDto.Description);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, objCheckListMasterDetailsBDto.UserId);
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

        #region Delete checkListDetails
        /// <summary>
        /// Delete City detail.
        /// </summary>
        /// <param name="idCollections">City Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteChecklistDetails(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_CHECKLIST_MST_DELETE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.String, idCollections);
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

        #region Update checkListDetails
        /// <summary>
        /// Update City detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateChecklistDetails(String xmlDataCheckList)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_CHECKLIST_MST_UPDATE);
                db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml, xmlDataCheckList);
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
