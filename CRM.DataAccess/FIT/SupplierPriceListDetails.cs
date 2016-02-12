#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
#endregion

namespace CRM.DataAccess.FIT
{
    public class SupplierPriceListDetails
    {
        public DataSet GetSupplierDetails(int SID, string ROOM, string Status)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("SP_GET_SUPPLIER_HOTEL_PRICE_LIST_DETAILS_FIT");
                db.AddInParameter(dbCmd, "@SUPPLIER_ID", DbType.Int32, SID);
                db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, ROOM);
                db.AddInParameter(dbCmd, "@STATUS", DbType.String, Status);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }

        public DataTable GetRoomType()
        {
            Database db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            DbCommand dbCmd = db.GetStoredProcCommand("SP_GET_ROOM_TYPE_FOR_FIT");
            DataTable dt = null;
            DataSet ds = db.ExecuteDataSet(dbCmd);
            dt = ds.Tables[0];
            return dt;
        }

        public DataTable GetStatus()
        {
            Database db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            DbCommand dbCmd = db.GetStoredProcCommand("SP_FETCH_STATUS_FOR_FIT");
            DataTable dt = null;
            DataSet ds = db.ExecuteDataSet(dbCmd);
            dt = ds.Tables[0];
            return dt;
        }
    }
}
