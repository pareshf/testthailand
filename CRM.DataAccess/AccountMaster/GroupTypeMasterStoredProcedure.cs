using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.AccountMaster
{
    public class GroupTypeMasterStoredProcedure
    {
        public void insert_accounts_entry(String GROUP_TYPE_NAME, int FROM_DIGIT, int INTERVAL, int STARTING_DIGIT)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_GROUP_TYPE_MASTER");

                db.AddInParameter(dbCmd, "@GROUP_TYPE_NAME", DbType.String, GROUP_TYPE_NAME);
                db.AddInParameter(dbCmd, "@FROM_DIGIT", DbType.Int32, FROM_DIGIT);
                db.AddInParameter(dbCmd, "@INTERVAL", DbType.Int32, INTERVAL);
                db.AddInParameter(dbCmd, "@STARTING_DIGIT", DbType.Int32, STARTING_DIGIT);

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

        public DataSet get_account_code(String SP_NAME)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(SP_NAME);
             
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet get_record_edit(int ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_GROUP_TYPE_MASTER_EDIT");

                db.AddInParameter(dbCmd, "@GROUP_TYPE_MASTER_ID", DbType.Int32, ID);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public void update_accounts_entry(String GROUP_TYPE_NAME, int INTERVAL, int ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_GROUP_TYPE_MASTER");
                db.AddInParameter(dbCmd, "@GROUP_TYPE_ID", DbType.Int32, ID);
                db.AddInParameter(dbCmd, "@GROUP_TYPE_NAME", DbType.String, GROUP_TYPE_NAME);
               
                db.AddInParameter(dbCmd, "@INTERVAL", DbType.Int32, INTERVAL);
              

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