using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.SecurityDAL
{
   public  class ForgotPasswords
    {
        public DataSet FetchUsername()
        {
            Database db = null;
            DbCommand dbcmd = null;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbcmd = db.GetStoredProcCommand("FETCH_USER_NAME_FOR_FORGET_PASSWORD");
                //db.AddInParameter(dbcmd, "@USER_ID", DbType.Int32, UserId);
                ds = db.ExecuteDataSet(dbcmd);
                return ds;
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
                DALHelper.Destroy(ref dbcmd);
            }
            return null;
        }

        public DataSet Fetch_New_Password(String useremail)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_FORGOT_PASSWORD");

                db.AddInParameter(dbCmd, "@USER_NAME", DbType.String, useremail);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return dsData;

        }
        public DataSet Fetch_UpdatedPassword(String useremail)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_UPDATED_PASSWORD");

                db.AddInParameter(dbCmd, "@USER_NAME", DbType.String, useremail);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return dsData;

        }
    }
}
