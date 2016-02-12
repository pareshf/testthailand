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
    public class TargetListTypeStoredProcedure
    {
        public void InsertUpdateTargetListType(ArrayList ttype)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_TARGETLIST_TYPE_MASTER");
                db.AddInParameter(dbCmd, "@CUST_TYPE_ID", DbType.Int32, ttype[1]);
                db.AddInParameter(dbCmd, "@TYPE", DbType.String, ttype[0]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(ttype[2]));
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

        public void delTargetListType(int deltype)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_TARGETLIST_TYPE_MASTER");
                db.AddInParameter(dbCmd, "@TYPEID", DbType.Int32, deltype);
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
