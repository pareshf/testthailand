using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;
using CRM.DataAccess.AdministratorEntity;

namespace CRM.DataAccess.AdministratorEntity
{
    public class SupplierLandmarkMaster
    {
        public void InsertUpdateLandmarkNew(ArrayList LandMark)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SUPPLIER_LANDMARK_MASTER");
                db.AddInParameter(dbCmd, "@LANDMARK_ID", DbType.Int32, Convert.ToInt32(LandMark[0]));
                db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, LandMark[2]);
                db.AddInParameter(dbCmd, "@LANDMARK_DESC", DbType.String, LandMark[1]);
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
        public void deleteLandmark(int LAMDMARKID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_SUPPLIER_LANDMARK_MASTER");
                db.AddInParameter(dbCmd, "@LANDMARK_ID", DbType.Int32, LAMDMARKID);
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
