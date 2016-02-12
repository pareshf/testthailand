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
   public class BookingCurrencyMaster
    {

       public void InsertUpdateBookingCurrency(ArrayList Currency)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_BOOKING_CURRENCY_MASTER");
                db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, Currency[0]);
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, Currency[1]);
                db.AddInParameter(dbCmd, "@CURRENCY_SYMBOL", DbType.String, Currency[2]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Currency[3]));
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

       public void deleteBookingCurrency(int CURRENCYID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FORM_BOOKING_CURRENCY_MASTER");
                db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, CURRENCYID);
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
