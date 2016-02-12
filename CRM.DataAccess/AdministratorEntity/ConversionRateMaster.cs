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
    public class ConversionRateMaster
    {
        public void InsertUpdateConversionMaster(ArrayList conversion)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_COMMON_CONVERSTION_RATE");
                db.AddInParameter(dbCmd, "@CONVERSION_RATE_ID", DbType.Int32, Convert.ToInt32(conversion[0]));
                db.AddInParameter(dbCmd, "@FROM_CURRENCY", DbType.String, conversion[1]);
                db.AddInParameter(dbCmd, "@TO_CURRENCY", DbType.String, conversion[2]);
                db.AddInParameter(dbCmd, "@CONVERSION_RATE", DbType.Decimal,Convert.ToDecimal(conversion[3]));
              
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

        public void deleteConversionRate(int CONVERSIONRATEID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_CONVERSION_RATE_MASTER");
                db.AddInParameter(dbCmd, "@CONVERSION_RATE_ID", DbType.Int32, CONVERSIONRATEID);
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
            dbCmd = db.GetStoredProcCommand("VALIDATION_FOR_CURRENCY_CONVERSION");
            DataSet ds1 = db.ExecuteDataSet(dbCmd);
            return ds1;
        }

    }
}
