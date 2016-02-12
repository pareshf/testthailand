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
    public class TargetListStoredProcedure
    {
        public void InsertUpdateTargetList(ArrayList Target)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_CAMPAIGN_TARGETLIST_MASTER");
                db.AddInParameter(dbCmd, "@TARGETLIST_ID", DbType.Int32, Target[1]);
                db.AddInParameter(dbCmd, "@TARGETLIST_NAME", DbType.String, Target[0]);
                db.AddInParameter(dbCmd, "@SOURCE", DbType.String, Target[2]);
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, Target[3]);
                db.AddInParameter(dbCmd, "@COST", DbType.String, Target[4]);
                db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, Target[5]);
                db.AddInParameter(dbCmd, "@EMP_NAME", DbType.String, Target[6]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Target[7]));
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
        public void delTargetList(int delTarget)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_TARGET_LIST_MASTER");
                db.AddInParameter(dbCmd, "@TARGETLISTID", DbType.Int32, delTarget);
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
