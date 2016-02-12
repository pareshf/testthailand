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
   public class SupplierBankAccountStoredProcedure
    {
        public void InsertUpdateSupplierBank(ArrayList SupplierAccount)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SUPPLIER_BANK_ACCOUNT_DETAIL");
                db.AddInParameter(dbCmd, "@SUPPLIER_BANK_ACCOUNT_ID", DbType.Int32, SupplierAccount[0]);
                db.AddInParameter(dbCmd, "@SUPPLIER_COMAPNY", DbType.String, SupplierAccount[1]);
                db.AddInParameter(dbCmd, "@ACC_NO", DbType.String, SupplierAccount[2]);
                db.AddInParameter(dbCmd, "@ACC_NAME", DbType.String, SupplierAccount[3]);
                db.AddInParameter(dbCmd, "@BANK_NAME", DbType.String, SupplierAccount[4]);
                db.AddInParameter(dbCmd, "@BANK_BRANCH", DbType.String, SupplierAccount[5]);
                db.AddInParameter(dbCmd, "@BANK_ADDRESS", DbType.String, SupplierAccount[6]);
                db.AddInParameter(dbCmd, "@SWIFT_CODE", DbType.String, SupplierAccount[7]);
                db.AddInParameter(dbCmd, "@COMP_ACC_BANK", DbType.String, SupplierAccount[8]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(SupplierAccount[9]));
                db.AddInParameter(dbCmd, "@COMPANY_BANK_BRANCH", DbType.String, SupplierAccount[10]);
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
        public void SupplierBankAccount(int delSupplierbank)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_SUPPLIER_BANK_ACCOUNT");
                db.AddInParameter(dbCmd, "@BANKID", DbType.Int32, delSupplierbank);
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
