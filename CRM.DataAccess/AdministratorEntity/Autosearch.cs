using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;

namespace CRM.DataAccess.AdministratorEntity
{
    public class Autosearch
    {
        public DataTable ReturnAutoSearchResult(string searchquery,string storeprocedurename)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand(storeprocedurename);
            db.AddInParameter(dbCmd, "@QUERY", DbType.String, searchquery);
            ds = db.ExecuteDataSet(dbCmd);
            return ds.Tables[0];
            
        }
        public DataTable ReturnAutoSearchResult(string searchquery, string sqlquery,string tablename)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand(sqlquery);
            db.AddInParameter(dbCmd, "@QUERY11", DbType.String, searchquery);
            db.AddInParameter(dbCmd, "@NAME", DbType.String, tablename);
            ds = db.ExecuteDataSet(dbCmd);
            return ds.Tables[0];
        }
    }
}
