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
    public class HotelDashboard
    {
        public void InsertUpdateHotelDashboard(ArrayList News)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_HOTEL_DASHBOARD_MASTER");
                db.AddInParameter(dbCmd, "@HOTEL_DASHBOARD_ID", DbType.Int32, News[0]);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.String, News[1]);
                db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, News[2]);
                db.AddInParameter(dbCmd, "@IS_DASHBOARD", DbType.String, News[3]);
                db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, News[4]);
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
        public void delHotelDashboard(int newsid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FOR_HOTEL_DASHBOARD");
                db.AddInParameter(dbCmd, "@HOTEL_DASHBOARD_ID", DbType.Int32, newsid);
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
