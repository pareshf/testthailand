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
   public class CustomerTypeStoredProcedure
    {
       public void InsertUpdateCustomerType(ArrayList Ctype)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_CUST_TYPE_MASTER");
                db.AddInParameter(dbCmd, "@CUST_TYPE_ID", DbType.Int32, Ctype[1]);
                db.AddInParameter(dbCmd, "@CUST_TYPE_NAME", DbType.String, Ctype[0]);
                db.AddInParameter(dbCmd, "@CUST_TYPE_DESC", DbType.String, Ctype[2]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Ctype[3]);
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

       public void delCustomerType(int delCtype)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_CUST_TYPE_MASTER");
                db.AddInParameter(dbCmd, "@CUSTTYPEID", DbType.Int32, delCtype);
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
