using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.AdministrationDAL
{
    public class EmailAlertDal
    {
        #region Save Backup
        public void DeleteAlertMessages()
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_EMAIL_ALERT_FORMAT");
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
        public int InsertAlertMessages(string AlertName,string subject,string AlertMessage)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_EMAIL_ALERT_FORMAT");
                db.AddInParameter(dbCmd, "@EMAIL_TYPE", DbType.String, AlertName);
                db.AddInParameter(dbCmd, "@EMAIL_SUBJECT", DbType.String, subject);
                db.AddInParameter(dbCmd, "@EMAIL_MESSAGE", DbType.String, AlertMessage);
                Result = db.ExecuteNonQuery(dbCmd);
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
        public DataSet getexistingdata()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds=new DataSet();
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_EMAIL_ALERT_FORMAT");
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

        #region IDisposable Members
        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
    }
}
