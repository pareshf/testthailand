using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.AdministrationDAL
{
   public  class SupplierLandmarkMaster
   {
       public void InsertUpdateLandmark(System.Collections.ArrayList LandMark)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SUPPLIER_LANDMARK_MASTER");
               db.AddInParameter(dbCmd, "@LANDMARK_ID", DbType.Int32, LandMark[1]);
               db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, LandMark[0]);
               db.AddInParameter(dbCmd, "@LANDMARK_DESC", DbType.String, LandMark[2]);
               db.AddInParameter(dbCmd, "@GOOGLE_MAP_CODE", DbType.String,LandMark[3]);
               db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(LandMark[4]));
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
