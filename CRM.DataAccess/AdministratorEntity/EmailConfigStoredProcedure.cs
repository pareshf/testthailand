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
    public class EmailConfigStoredProcedure
    {
        public void InsertUpdateEmailConfig(ArrayList Config)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_EMAIL_CONFIG_MASTER");
                db.AddInParameter(dbCmd, "@EMAIL_CONFIG_ID", DbType.Int32, Config[0]);
                db.AddInParameter(dbCmd, "@SMTP_USERID", DbType.String, Config[1]);
                db.AddInParameter(dbCmd, "@SMTP_PASSWORD", DbType.String, Config[2]);
                db.AddInParameter(dbCmd, "@SMTP_HOST", DbType.String, Config[3]);
                db.AddInParameter(dbCmd, "@SMTP_PORT", DbType.String, Config[4]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Config[5]));
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
        public void delEmailConfig(int delConfig)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_EMAIL_CONFIG_MASTER");
                db.AddInParameter(dbCmd, "@CONFIG_ID", DbType.Int32, delConfig);
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
