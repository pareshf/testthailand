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
   public class BookingCheckListStoredProcedure
    {
        public void InsertUpdateforCheckList(ArrayList CheckList)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_BOOKING_CHECK_LIST");
                db.AddInParameter(dbCmd, "@CHECKLIST_ID", DbType.Int32, CheckList[1]);
                db.AddInParameter(dbCmd, "@CHECKLIST_FOR", DbType.String, CheckList[0]);
                db.AddInParameter(dbCmd, "@DEPARTMENT_NAME", DbType.String, CheckList[2]);
                db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, CheckList[3]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32,Convert.ToInt32( CheckList[4]));
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
        public void deleteMyCheckList(int delChecklist)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_BOOKING_CHECKLIST_MASTER");
                db.AddInParameter(dbCmd, "@CHECKLISTID", DbType.Int32, delChecklist);
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
        public void InsertUpdateCheckListDetails(ArrayList CheckListDetails)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_CHECK_LIST_DETAIL");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, CheckListDetails[1]);
                db.AddInParameter(dbCmd, "@CHECKLIST_DESCRIPTION", DbType.String, CheckListDetails[0]);
                db.AddInParameter(dbCmd, "@PRIORITY_NAME", DbType.String, CheckListDetails[2]);
                db.AddInParameter(dbCmd, "@IMPORTANCE", DbType.String, CheckListDetails[3]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(CheckListDetails[4]));
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
        public void deleteChkdetails(int delChecklistdetail)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_BOOKING_CHECKLIST_MST_DETAILS");
                db.AddInParameter(dbCmd, "@SRNO", DbType.Int32, delChecklistdetail);
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
        public void InsertNewCheckList(string ChkListId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_CHECK_LIST_DETAILS");
                db.AddInParameter(dbCmd, "@CHKID", DbType.Int32, ChkListId);
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
