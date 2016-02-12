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
     public class FareHotelRoomTypeSp
    {
         public void InsertUpdateHotelRoom(ArrayList hotel)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FARE_HOTEL_ROOM_TYPE_MASTER");
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, hotel[1]);
                db.AddInParameter(dbCmd, "@ROOM_TYPE_NAME", DbType.String, hotel[0]);
                db.AddInParameter(dbCmd, "@ROOM_SIZE", DbType.String, hotel[2]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(hotel[3]));
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
        public void deleteHotelType(int HOTELID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FOR_HOTEL_ROOM_TYPE_MASTER");
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, HOTELID);
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
