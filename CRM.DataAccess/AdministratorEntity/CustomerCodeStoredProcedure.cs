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
   public class CustomerCodeStoredProcedure
    {
       public void InsertUpdateCustomerCode(ArrayList customerCode)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_CUSTOMER_CODE_MASTER");
               db.AddInParameter(dbCmd, "@CUST_CODE_ID", DbType.Int32, customerCode[2]);
               db.AddInParameter(dbCmd, "@CUST_CODE_NAME", DbType.String, customerCode[0]);
               db.AddInParameter(dbCmd, "@CUST_CODE_DESC", DbType.String, customerCode[1]);
               db.AddInParameter(dbCmd, "@USR", DbType.Int32, Convert.ToInt32(customerCode[3]));
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
       public void deleteCustomerCode(int delCustomerCode)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("DELETE_FROM_CUSTOMER_CODE_MASTER");
               db.AddInParameter(dbCmd, "@CUSTCODE", DbType.Int32, delCustomerCode);
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
