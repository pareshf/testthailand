
#region imports assemblies

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;  


#endregion 

namespace CRM.DataAccess.Dashboard
{
    public class MyTaskmaster
    {

        public void InsertUpdateTask(ArrayList Task)
        {
            Database db = null;
            DbCommand dbCmd = null;
            
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("InsertUpdateMyTaskMaster");
                db.AddInParameter(dbCmd, "@MYTASK_ID", DbType.String, Task[0]);
                db.AddInParameter(dbCmd, "@TITLE", DbType.String, Task[1]);
                db.AddInParameter(dbCmd, "@REGARDING", DbType.String, Task[2]);
                db.AddInParameter(dbCmd, "@TASK_TYPE", DbType.String, Task[3]);
                db.AddInParameter(dbCmd, "@ASSIGN_BY", DbType.Int32,(Task[14]));
                db.AddInParameter(dbCmd, "@ASSIGN_TO", DbType.String, Task[4]);
                db.AddInParameter(dbCmd, "@DURATION", DbType.String, Task[5]);
                db.AddInParameter(dbCmd, "@ACTUAL_TIME_TAKEN", DbType.String, Task[6]);
                db.AddInParameter(dbCmd, "@START_DATE", DbType.DateTime,Convert.ToDateTime(Task[7]));
                if (Task[8].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@END_DATE", DbType.DateTime,DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@END_DATE", DbType.DateTime, Convert.ToDateTime(Task[8]));
                }
                db.AddInParameter(dbCmd, "@PRIORITY_ID", DbType.String, Task[9]);
                db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.String, Task[10]);
                db.AddInParameter(dbCmd, "@PRODUCT_CODE", DbType.String, Task[11]);
                db.AddInParameter(dbCmd,"@PROJECT_REMARKS",DbType.String,Task[13]);
                db.AddInParameter(dbCmd, "@STATUS_ID", DbType.String, Task[12]);

                db.ExecuteNonQuery(dbCmd);
            }
            catch(Exception ex)
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

        public void DeleteMytask(int MytaskId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DeleteFromMyTaskMaster");
                db.AddInParameter(dbCmd, "@MYTASK_ID", DbType.Int32, MytaskId);
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
