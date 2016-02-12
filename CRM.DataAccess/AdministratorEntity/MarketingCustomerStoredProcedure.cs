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
    public class MarketingCustomerStoredProcedure
    {
        public void InsertUpdateMarketingCustomer(ArrayList MarCustomer)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_MARKETING_CUSTOMER");
                db.AddInParameter(dbCmd, "@MAR_DATA_ID", DbType.Int32, MarCustomer[1]);
                db.AddInParameter(dbCmd, "@MAR_DATA_TITLE_ID", DbType.String, MarCustomer[0]);
                db.AddInParameter(dbCmd, "@MAR_DATA_NAME", DbType.String, MarCustomer[2]);
                db.AddInParameter(dbCmd, "@MAR_DATA_SURNAME", DbType.String, MarCustomer[3]);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE_1", DbType.String, MarCustomer[4]);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE_2", DbType.String, MarCustomer[5]);
                db.AddInParameter(dbCmd, "@MOBILE_NO", DbType.String, MarCustomer[6]);
                db.AddInParameter(dbCmd, "@PHONE_NO", DbType.String, MarCustomer[7]);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, MarCustomer[8]);
                db.AddInParameter(dbCmd, "@STATE_NAME", DbType.String, MarCustomer[9]);
                db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, MarCustomer[10]);
                db.AddInParameter(dbCmd, "@COMMUNICATION_NAME", DbType.String, MarCustomer[11]);
                db.AddInParameter(dbCmd, "@CUST_COMPANY_NAME", DbType.String, MarCustomer[12]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(MarCustomer[13]));
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
        public void delMarketingCustomer(int delmarcust)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_TARGET_LIST_MARKETING_DATA_MASTER");
                db.AddInParameter(dbCmd, "@MARDATAID", DbType.Int32, delmarcust);
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
