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
    public class CloseInvoiceDal
    {
        public DataSet commonSp(String sp_name)
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
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            return dsData;
        }

        public void closeInvoice(String sp_name, String invoice_no)
        {
            Database db = null;
            DbCommand dbCmd = null;
          
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
                
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

        public DataSet fetchallDatasearch(String sp_name, String INVOICE, String param5, String param6, String param7)
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

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
    }
}
