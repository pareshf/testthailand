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
    public class CompanyBankAccountStoreProcedure
    {

        public DataSet InsertUpdateCompanyBankAccountDetails(ArrayList CompanyBankAccount)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
           
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_COMPANY_BANK_ACCOUNT_DETAILS");
                db.AddInParameter(dbCmd, "@COMP_ACC_ID", DbType.Int32, CompanyBankAccount[0]);
                db.AddInParameter(dbCmd, "@COMPANY_NAME", DbType.String, CompanyBankAccount[1]);
                db.AddInParameter(dbCmd, "@ACC_NAME", DbType.String, CompanyBankAccount[6]);
                db.AddInParameter(dbCmd, "@ACC_NO", DbType.String, CompanyBankAccount[5]);
                db.AddInParameter(dbCmd, "@COMP_BANK_NAME", DbType.String, CompanyBankAccount[2]);
                db.AddInParameter(dbCmd, "@BANK_BRANCH", DbType.String, CompanyBankAccount[3]);
                db.AddInParameter(dbCmd, "@BANK_ADD", DbType.String, CompanyBankAccount[4]);
                db.AddInParameter(dbCmd, "@SWIFT_CODE", DbType.String, CompanyBankAccount[7]);
                db.AddInParameter(dbCmd, "@IBOB", DbType.String, CompanyBankAccount[8]);
                
               // db.ExecuteNonQuery(dbCmd);
                ds = db.ExecuteDataSet(dbCmd);
                 
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
            return ds;
        }
        public DataSet CheckValidation()
        {
            Database db = null;
            DbCommand dbCmd = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("FETCH_ACCOUNT_NAME_FOR_COMAPNY_BANK_VALIDATION");
            DataSet ds1 = db.ExecuteDataSet(dbCmd);
            return ds1;
        }
    }
}
