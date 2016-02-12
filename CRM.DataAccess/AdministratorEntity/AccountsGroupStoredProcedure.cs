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
    public class AccountsGroupStoredProcedure
    {
       
        public void InsertUpdateAccounts(ArrayList AccountsGroup)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_ACCOUNT_GROUP_MASTER");
                db.AddInParameter(dbCmd, "@ACCOUNT_GROUP_ID", DbType.Int32, AccountsGroup[0]);
                db.AddInParameter(dbCmd, "@GROUP_CODE", DbType.String, AccountsGroup[1]);
                db.AddInParameter(dbCmd, "@GROUP_NAME", DbType.String, AccountsGroup[2]);
                db.AddInParameter(dbCmd, "@GROUP_TYPE", DbType.String, AccountsGroup[3]);
                db.AddInParameter(dbCmd, "@GROUP_DISPLAY", DbType.String, AccountsGroup[4]);
                db.AddInParameter(dbCmd, "@GROUP_ORDER", DbType.Int32,Convert.ToInt32(AccountsGroup[5]));
                db.AddInParameter(dbCmd, "@GROUP_CODE_UNDER", DbType.String, AccountsGroup[6]);
                db.AddInParameter(dbCmd, "@COMPANY_NAME", DbType.String, AccountsGroup[7]);
                //db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(AccountsGroup[2]));
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
        public void delAccounts(int Group)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_ACCOUNTS_GROUP_MASTER");
                db.AddInParameter(dbCmd, "@ACCOUNT_GROUP_ID", DbType.Int32, Group);
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
