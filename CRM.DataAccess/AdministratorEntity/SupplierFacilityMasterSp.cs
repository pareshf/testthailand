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
    public class SupplierFacilityMasterSp
    {
        public void InsertUpdateFacility(ArrayList Facility)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SUPPLIER_FACILITY_MASTER");
                db.AddInParameter(dbCmd, "@FACELITY_ID", DbType.Int32, Facility[1]);
                db.AddInParameter(dbCmd, "@FACILITY_DISCRIPTION", DbType.String, Facility[2]);
                db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, Facility[0]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Facility[3]));
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

        public void deleteFacility(int FACILITYID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_SUPPLIER_FACILITY_MASTER");

                db.AddInParameter(dbCmd, "@FACELITY_ID", DbType.Int32, FACILITYID);
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

