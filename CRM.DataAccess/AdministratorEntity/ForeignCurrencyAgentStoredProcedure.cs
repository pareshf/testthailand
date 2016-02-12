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
   public class ForeignCurrencyAgentStoredProcedure
    {
       public void InsertUpdateForeignCurrencyAgent(ArrayList CurrencyAgent)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_FOREIGN_CURRENCY_AGENT_MASTER");
                db.AddInParameter(dbCmd, "@FOREIGN_CURRENCY_AGENT_ID", DbType.Int32, CurrencyAgent[1]);
                db.AddInParameter(dbCmd, "@FOREIGN_CURRENCY_AGENT_NAME", DbType.String, CurrencyAgent[0]);
                db.AddInParameter(dbCmd, "@EMAIL", DbType.String, CurrencyAgent[2]);
                db.AddInParameter(dbCmd, "@PHONE", DbType.String, CurrencyAgent[3]);
                db.AddInParameter(dbCmd, "@MOBILE", DbType.String, CurrencyAgent[4]);
                db.AddInParameter(dbCmd, "@FAX", DbType.String, CurrencyAgent[5]);
                db.AddInParameter(dbCmd, "@USER", DbType.String, CurrencyAgent[6]);
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
       public void deleteForeignCurrencyAgent(int delCurrencyAgent)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_FOREIGN_CURRENCY_AGENT");
                db.AddInParameter(dbCmd, "@FAGENTID", DbType.Int32, delCurrencyAgent);
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
