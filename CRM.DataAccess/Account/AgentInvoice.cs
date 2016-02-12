using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.Account
{
    public class AgentInvoice
    {
        public DataSet fetchallInvoice(String sp_name, String QID, String INVOICE, String param5, String param6, String param7,String agent_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                if (param5 == "0")
                {
                    param5 = "";
                }
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                // db.AddInParameter(dbCmd, "@USERID", DbType.Int32, int.Parse(param1));
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, int.Parse(QID));
                //db.AddInParameter(dbCmd, "@AGENT_NAME_S", DbType.String, AGENT);
                //db.AddInParameter(dbCmd, "@CLIENT_NAME_S", DbType.String, param3);
                //db.AddInParameter(dbCmd, "@TOUR_NAME_S", DbType.String, param4);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, INVOICE);
                if (param6.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@PERIOD_STAY_FROM", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PERIOD_STAY_FROM", DbType.DateTime, DateTime.ParseExact(param6.ToString(), "dd/MM/yyyy", null));
                }
                if (param7.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@PERIOD_STAY_TO", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PERIOD_STAY_TO", DbType.DateTime, DateTime.ParseExact(param7.ToString(), "dd/MM/yyyy", null));
                }
                if (param5.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@INVOICE_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@INVOICE_DATE", DbType.DateTime, DateTime.ParseExact(param5.ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, int.Parse(agent_id));
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
        public DataSet FechDataForInvoice(String sp,String s)
        {

            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp);
                db.AddInParameter(dbCmd, "@AGENT_ID", DbType.String, int.Parse(s));
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
    }
}
