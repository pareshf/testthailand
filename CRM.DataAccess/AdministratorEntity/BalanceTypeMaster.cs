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
    public class BalanceTypeMaster
    {
        public void InsertUpdateBalanceMaster(ArrayList Balance)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_BALANCE_TYPE_MASTER");
                db.AddInParameter(dbCmd, "@BAL_TYPE_ID", DbType.Int32, Balance[0]);
                db.AddInParameter(dbCmd, "@BAL_TYPE_NAME", DbType.String, Balance[1]);
                //db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(bank[2]));
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
        public void deleteBalanceType(int BALTYPEID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_BALANCE_TYPE_MASTER");
                db.AddInParameter(dbCmd, "@BAL_TYPE_ID", DbType.Int32, BALTYPEID);
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
