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
   public class FareCruiseCarbinCategoryMas
    {
       public void InsertUpdateACategoryType(ArrayList Category)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FARE_CRUISE_CABIN_CATEGORY_MASTER");
               db.AddInParameter(dbCmd, "@CATEGORY_ID", DbType.Int32, Category[0]);
               db.AddInParameter(dbCmd, "@CATEGORY_CODE", DbType.String, Category[1]);
               db.AddInParameter(dbCmd, "@CATEGORY_DESC", DbType.String , Category[2]);
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
       public void deleteCategoryType(int CategoryId)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("DELETE_FARE_CRUISE_CABIN_CATEGORY_MASTER");
               db.AddInParameter(dbCmd, "@CATEGORY_ID", DbType.Int32, CategoryId);
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
