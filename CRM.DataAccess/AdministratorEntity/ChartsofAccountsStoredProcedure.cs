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
    public class ChartsofAccountsStoredProcedure
    {
        public DataSet InsertUpdateChartsofAccounts(ArrayList ChartsofAccount)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CHARTS_OF_ACCOUNTS");
                db.AddInParameter(dbCmd, "@CHART_OF_ACCOUNTS_ID", DbType.Int32, ChartsofAccount[0]);
                db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, ChartsofAccount[1]);
                db.AddInParameter(dbCmd, "@GL_DESCRIPTION", DbType.String, ChartsofAccount[2]);
                db.AddInParameter(dbCmd, "@ACCOUNT_GROUP", DbType.String, ChartsofAccount[3]);
                db.AddInParameter(dbCmd, "@SIDE_CODE", DbType.String, ChartsofAccount[4]);
                db.AddInParameter(dbCmd, "@OP_BALANCE", DbType.Decimal, Convert.ToDecimal(ChartsofAccount[5]));
                db.AddInParameter(dbCmd, "@OP_BAL_TYPE", DbType.String, ChartsofAccount[6]);
                db.AddInParameter(dbCmd, "@OP_BALANCE_MONTH", DbType.Decimal, Convert.ToDecimal(ChartsofAccount[7]));
                db.AddInParameter(dbCmd, "@CL_BALANCE", DbType.Decimal, Convert.ToDecimal(ChartsofAccount[8]));
                db.AddInParameter(dbCmd, "@CL_BAL_TYPE", DbType.String, ChartsofAccount[9]);
                db.AddInParameter(dbCmd, "@CL_BALANCE_MONTH", DbType.Decimal, Convert.ToDecimal(ChartsofAccount[10]));
                db.AddInParameter(dbCmd, "@COMPANY", DbType.String, ChartsofAccount[11]);
                db.AddInParameter(dbCmd, "@ACCOUNT_NAME", DbType.String, ChartsofAccount[12]);
                db.AddInParameter(dbCmd, "@COMPANY_ACCOUNT_NAME", DbType.String, ChartsofAccount[13]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(ChartsofAccount[14]));
                //db.ExecuteNonQuery(dbCmd);
                ds = db.ExecuteDataSet(dbCmd);
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
            return ds;
        }
        public void delchartAccount(int delchartofaccount)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_CHARTS_OF_ACCOUNTS");
                db.AddInParameter(dbCmd, "@CHART_OF_ACCOUNTS_ID", DbType.Int32, delchartofaccount);
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
        public DataSet CheckValidation()
        {
            Database db = null;
            DbCommand dbCmd = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("EMAIL_VALIDATION_FOR_LOGIN");
            DataSet ds1 = db.ExecuteDataSet(dbCmd);
            return ds1;
        }
    }
}
