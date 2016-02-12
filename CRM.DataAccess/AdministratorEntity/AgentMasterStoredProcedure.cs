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
    public class AgentMasterStoredProcedure
    {
        public void InsertUpdateAgentName(ArrayList Agent)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_FARE_AGENT_MASTER");
                db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, Agent[1]);
                db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.String, Agent[0]);
                db.AddInParameter(dbCmd, "@EMAIL", DbType.String, Agent[2]);
                db.AddInParameter(dbCmd, "@MOBILE", DbType.String, Agent[3]);
                db.AddInParameter(dbCmd, "@PHONE", DbType.String, Agent[4]);
                db.AddInParameter(dbCmd, "@FAX", DbType.String, Agent[5]);
                db.AddInParameter(dbCmd, "@AGENT_TYPE_NAME", DbType.String, Agent[6]);
                db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, Agent[12]);
                db.AddInParameter(dbCmd, "@STATE_NAME", DbType.String, Agent[11]);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, Agent[10]);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE1", DbType.String, Agent[8]);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE2", DbType.String, Agent[9]);
                db.AddInParameter(dbCmd, "@REGION_NAME", DbType.String, Agent[7]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Agent[13]));
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
        public void delAgentName(int delagent)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_FARE_AGENT_MASTER");
                db.AddInParameter(dbCmd, "@AGENTID", DbType.Int32, delagent);
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
