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
    public class CompanyRegionMaster
    {
        public void InsertUpdateCompanyRegion(ArrayList Cregion)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_COMPANY_REGION_MASTER");
                db.AddInParameter(dbCmd, "@REGION_ID", DbType.Int32, Cregion[1]);
                db.AddInParameter(dbCmd, "@REGION_SHORT_NAME", DbType.String, Cregion[0]);
                db.AddInParameter(dbCmd, "@REGION_LONG_NAME", DbType.String, Cregion[2]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Cregion[3]));
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

        public void deleteCompanyRegion(int region)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_COMPANY_REGION_MASTER");
                db.AddInParameter(dbCmd, "@COMPANY_REGIONID", DbType.Int32, region);
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
