using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.EmailSettings
{
    public class EmailMaster
    {
        public DataSet fetchallEmailData(String sp_name, String QID, String fromdate,String agentname)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);

                db.AddInParameter(dbCmd, "@QUOTEID", DbType.Int32, int.Parse(QID));

                if (fromdate.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(fromdate.ToString(), "dd/MM/yyyy", null));
                }

                db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.String, agentname);
                //if (agentname == "")
                //{
                //    db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.Int32, int.Parse("0"));
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.Int32, int.Parse(agentname));
                //}
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        public DataSet fetchallData(String sp_name)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        public DataSet GetEmailData(String sp_name,int Id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@MASTER_ID", DbType.Int32,Id);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
    }
}
