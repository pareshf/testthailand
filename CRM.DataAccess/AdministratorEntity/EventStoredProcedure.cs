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
    public class EventStoredProcedure
    {
        public void InsertUpdateEventMaster(ArrayList EVENT)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_EVENT_MASTER_FOR_EMAIL");
                db.AddInParameter(dbCmd, "@EVENT_ID", DbType.Int32, EVENT[0]);
                db.AddInParameter(dbCmd, "@EVENT_NAME", DbType.String, EVENT[1]);
                db.AddInParameter(dbCmd, "@FROM_NAME", DbType.String, EVENT[2]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(EVENT[3]));
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
        public void delEventMaster(int eventid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_EVENT_MASTER_FOR_EMAIL");
                db.AddInParameter(dbCmd, "@EVENT_ID", DbType.Int32, eventid);
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
