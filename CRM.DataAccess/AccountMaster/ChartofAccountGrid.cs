using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.AccountMaster
{
    public class ChartofAccountGrid
    {
        public DataSet FechDataForGrid(String sp)
        {

            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        public DataSet fetchallInvoice(String sp_name, String id,String glcode,String gldesc,String group,String sidecode)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                
                db.AddInParameter(dbCmd, "@ACCOUNT_ID", DbType.Int32, int.Parse(id));
                db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, glcode);
                db.AddInParameter(dbCmd, "@GL_DESCRIPTION", DbType.String, gldesc);
                db.AddInParameter(dbCmd, "@GROUP_NAME", DbType.String, group);
                db.AddInParameter(dbCmd, "@SIDE_CODE", DbType.String, sidecode);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
    }
}
