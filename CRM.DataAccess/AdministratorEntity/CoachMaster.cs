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
   public class CoachMaster
    {
        public void InsertUpdatecoachtype(ArrayList Coach)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_COACH_MASTER");
                db.AddInParameter(dbCmd, "@COACH_TYPE_MASTER_ID", DbType.Int32, Coach[0]);
                db.AddInParameter(dbCmd, "@COACH_TYPE_DESC", DbType.String, Coach[2]);
                db.AddInParameter(dbCmd, "@COACH_NAME", DbType.String, Coach[1]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Coach[3]));
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

        public void deleteCoachType(int COACHTYPEID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_COACH_MASTER");
                db.AddInParameter(dbCmd, "@COACH_TYPE_MASTER_ID", DbType.Int32, COACHTYPEID);
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
