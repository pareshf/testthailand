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
   public class MaritalStatusStoredProcedure
    {
       public void InsertUpdateMaritalStatus(ArrayList marital)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_MARRITAL_STATUS");
               db.AddInParameter(dbCmd, "@MARITAL_STATUS_ID", DbType.Int32, marital[1]);
               db.AddInParameter(dbCmd, "@MARITAL_STATUS_NAME", DbType.String, marital[0]);
               db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(marital[2]));
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
       public void deleteMaritalStatus(int delmarital)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("DELETE_FROM_COMMON_MARRITAL_STATUS_MASTER");
               db.AddInParameter(dbCmd, "@MARITAL_STATUS_ID", DbType.Int32, delmarital);
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
