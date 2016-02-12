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
    public class CompanyBankAgentMapping
    {
        public void InsertUpdateCompanyBankAgentMapping(ArrayList SupplierAccount)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_COMPANYBANK_AGENT");
                db.AddInParameter(dbCmd, "@BANK_MAPPING_ID", DbType.Int32, SupplierAccount[0]);
                db.AddInParameter(dbCmd, "@COMP_BANK_NAME", DbType.String, SupplierAccount[1]);
                db.AddInParameter(dbCmd, "@CUST_COMP_NAME", DbType.String, SupplierAccount[2]);
                db.AddInParameter(dbCmd, "@COMPANY_BANK_BRANCH", DbType.String, SupplierAccount[3]);
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
