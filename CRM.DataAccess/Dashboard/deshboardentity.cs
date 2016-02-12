using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.Dashboard
{
    public class deshboardentity
    {
        public DataSet Getbirthdatedata(string condition,string flag)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_BIRTHDATE_FOR_DASHBOARD");
                db.AddInParameter(dbCmd, "@CONDITION", DbType.String,condition);
                db.AddInParameter(dbCmd, "@FLAG", DbType.String,flag);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet insertdatefilterdata(int userid,string fromdate, string todate,int filter,int empid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DSB_INSERT_DATE_FOR_DASHBOARD");
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32,userid);
                db.AddInParameter(dbCmd, "@FROM_DATE", DbType.String,fromdate);
                db.AddInParameter(dbCmd, "@TO_DATE", DbType.String, todate);
                db.AddInParameter(dbCmd, "@FILTER_ID", DbType.Int32, filter);
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, empid); 
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet ExecuteQuery(string query)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DSB_EXECUTE_ANY_QUERY");
                db.AddInParameter(dbCmd, "@QUERY", DbType.String, query);
              
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet getfilter(int userid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DSB_GET_FILTER_OF_ANY_QUERY");
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userid);

                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet fillfilterdata()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DSB_FILL_DATA_FOR_FILTER");
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet fillEmployeedata(int userid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                              dbCmd = db.GetStoredProcCommand("DSB_FILL_DATA_FOR_EMPLOYEE");
                              db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, userid);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        // Shasvat for Agent
        public DataSet fillAgentdata(int userid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DATA_FOR_AGENT");
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, userid);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }


    }
}
