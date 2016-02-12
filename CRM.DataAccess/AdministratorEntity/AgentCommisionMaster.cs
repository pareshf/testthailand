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
    public class AgentCommisionMaster
    {
        public void InsertUpdateAgentcommision(ArrayList agentcomm)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_AGENT_COMMISION_MASTER");
                db.AddInParameter(dbCmd, "@AGENT_COMMISION_ID", DbType.Int32, agentcomm[0]);
                db.AddInParameter(dbCmd, "@COMMISION_PERCENTAGE_LIMIT", DbType.String, agentcomm[1]);
                db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.String, agentcomm[2]);
                db.AddInParameter(dbCmd, "@PRODUCT_DESC", DbType.String, agentcomm[3]);
                db.AddInParameter(dbCmd, "@PRODUCT_TYPE", DbType.String, agentcomm[4]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(agentcomm[5]));
                db.ExecuteNonQuery(dbCmd);
                //ab
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
        public void delAgentcommision(int delagent)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_AGENT_COMMISION_MASTER");
                db.AddInParameter(dbCmd, "@AGENT_COMMISION_ID", DbType.Int32, delagent);
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
