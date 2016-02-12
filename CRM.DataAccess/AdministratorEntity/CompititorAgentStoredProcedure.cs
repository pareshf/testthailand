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
    public class CompititorAgentStoredProcedure
    {

        public void InsertUpdateCompititorAgent(ArrayList CompititorAgent)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_COMPITITOR_AGENT_MASTER");
                db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, CompititorAgent[1]);
                db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.String, CompititorAgent[0]);
                db.AddInParameter(dbCmd, "@AGENT_ADDRESS", DbType.String, CompititorAgent[2]);
                db.AddInParameter(dbCmd, "@PHONE_NO", DbType.String, CompititorAgent[3]);
                db.AddInParameter(dbCmd, "@ZIP_POSTAL_CODE", DbType.String, CompititorAgent[4]);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, CompititorAgent[5]);
                db.AddInParameter(dbCmd, "@STATE_NAME", DbType.String, CompititorAgent[6]);
                db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, CompititorAgent[7]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(CompititorAgent[8]));
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

        public void deleteCompititor(int delCompititor)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_COMPITITOR_AGENT_MASTER");
                db.AddInParameter(dbCmd, "@AGENTID", DbType.Int32, delCompititor);
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
