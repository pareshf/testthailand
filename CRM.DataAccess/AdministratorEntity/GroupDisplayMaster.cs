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
    public class GroupDisplayMaster
    {
        public void InsertUpdateGroupDisplayMaster(ArrayList Display)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GROUP_DISPLAY_MASTER");
                db.AddInParameter(dbCmd, "@GROUP_DISPLAY_ID", DbType.Int32, Display[0]);
                db.AddInParameter(dbCmd, "@GROUP_DISPLAY_NAME", DbType.String, Display[1]);
                db.AddInParameter(dbCmd, "@GROUP_DISPLAY_DESCRIPTION", DbType.String, Display[2]);
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
        public void deleteGroupDisplay(int GROUPDISPLAYID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_GROUP_DISPLAY_MASTER");
                db.AddInParameter(dbCmd, "@GROUP_DISPLAY_ID", DbType.Int32, GROUPDISPLAYID);
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
