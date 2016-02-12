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
   public  class SupplierHotelServiceTypeMaster
    {
       public void InsertUpdateServiceType(ArrayList Service)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SUPPLIER_HOTEL_SERVICE_TYPE_MASTER");
               db.AddInParameter(dbCmd, "@SERVICE_TYPE_ID", DbType.Int32, Service[1]);
               db.AddInParameter(dbCmd, "@SERVICE_TYPE_DESC", DbType.String, Service[0]);
               db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, Service[2]);
               db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Service[3]));
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

       public void deleteServiceType(int SERVICEID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("DELETE_SUPPLIER_HOTEL_SERVICE_TYPE_MASTER");
               db.AddInParameter(dbCmd, "@SERVICE_TYPE_ID", DbType.Int32, SERVICEID);
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
