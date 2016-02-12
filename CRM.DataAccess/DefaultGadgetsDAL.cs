using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess
{
    public class DefaultGadgetsDAL
    {
        #region FetchData from user( from SYS_USER_DASHBOARD_PERSONALIZATION table)
        public DataSet FetchDashBoardPersonalization(int UserID)
        {
            Database db = null;
            DbCommand dbcmd = null;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbcmd = db.GetStoredProcCommand(DALHelper.USP_SYS_USER_DASHBOARD_PERSONALIZATION_SELECT);
                db.AddInParameter(dbcmd, "@USER_ID", DbType.Int32, UserID);
                ds = db.ExecuteDataSet(dbcmd);
                return ds;
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
                DALHelper.Destroy(ref dbcmd);
            }
            return null;
        }
        #endregion
        public DataSet FetchDashBoardPersonalizationForReport(int UserID)
        {
            Database db = null;
            DbCommand dbcmd = null;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbcmd = db.GetStoredProcCommand("USP_SYS_USER_DASHBOARD_PERSONALIZATION_SELECT_FOR_REPORT");
                db.AddInParameter(dbcmd, "@USER_ID", DbType.Int32, UserID);
                ds = db.ExecuteDataSet(dbcmd);
                return ds;
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
                DALHelper.Destroy(ref dbcmd);
            }
            return null;
        }
        #region FetchData from user( from SYS_USER_DASHBOARD_PERSONALIZATION table)
        public int SaveDashBoardPersonalization(int UserID, string Personalization)
        {
            Database db = null;
            DbCommand dbcmd = null;
            int Result;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbcmd = db.GetStoredProcCommand(DALHelper.USP_SYS_USER_DASHBOARD_PERSONALIZATION_INSERT);
                db.AddInParameter(dbcmd, "@USER_ID", DbType.Int32, UserID);
                db.AddInParameter(dbcmd, "@PERSONALIZATION_STATE", DbType.String, Personalization);
                Result = db.ExecuteNonQuery(dbcmd);
                return Result;
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
                DALHelper.Destroy(ref dbcmd);
            }
            return 0;
        }
        #endregion

        #region MyPortal FetchData from user
        public DataSet FetchMyMortal(int UserID)
        {
            Database db = null;
            DbCommand dbcmd = null;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbcmd = db.GetStoredProcCommand(DALHelper.USP_SYS_USER_LOGIN_LOG_SELECT);
                db.AddInParameter(dbcmd, "@USER_ID", DbType.String, UserID);
                ds = db.ExecuteDataSet(dbcmd);
                return ds;
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
                DALHelper.Destroy(ref dbcmd);
            }
            return null;
        }
        #endregion

        #region report related gadgets
        public DataTable getReportsGadget(string userid, int moduleid)
        {
            Database db = null;
            DbCommand dbcmd = null;
            DataSet ds;
            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbcmd = db.GetStoredProcCommand("REPORT_GADGETS");
            db.AddInParameter(dbcmd, "@USER_ID", DbType.String, userid);
            db.AddInParameter(dbcmd, "@MODULE_ID", DbType.Int32, moduleid);
            ds = db.ExecuteDataSet(dbcmd);
            return ds.Tables[0];

        }
        #endregion
    }
}
