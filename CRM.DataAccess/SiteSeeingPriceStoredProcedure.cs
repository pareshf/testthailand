using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;

namespace CRM.DataAccess
{
    public class SiteSeeingPriceStoredProcedure
    {
        public void InsertUpdateSitePrice(ArrayList SitePrice)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_SIGHT_SEEING_PRICE_LIST");
                db.AddInParameter(dbCmd, "@SIGHT_SEEING_PRICE_ID", DbType.Int32, SitePrice[0]);
                if (SitePrice[1].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@SIGHT_SEEING_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@SIGHT_SEEING_DATE", DbType.DateTime, DateTime.ParseExact(SitePrice[1].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@SIGHT_SEEING_TIME", DbType.String, SitePrice[2]);
                if (SitePrice[3].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DateTime.ParseExact(SitePrice[3].ToString(), "dd/MM/yyyy", null));
                }
                if (SitePrice[4].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DateTime.ParseExact(SitePrice[4].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@SIC_PVT_FLAG", DbType.String, SitePrice[5]);
                db.AddInParameter(dbCmd, "@DINNER_DESCRIPTION", DbType.String, SitePrice[6]);
                db.AddInParameter(dbCmd, "@GIT_RATE", DbType.Decimal, Convert.ToDecimal(SitePrice[7]));
                db.AddInParameter(dbCmd, "@FIT_RATE", DbType.Decimal, Convert.ToDecimal(SitePrice[8]));
                db.AddInParameter(dbCmd, "@FIT_DISCOUNT", DbType.Decimal, Convert.ToDecimal(SitePrice[9]));
                db.AddInParameter(dbCmd, "@GIT_DISCOUNT", DbType.Decimal, Convert.ToDecimal(SitePrice[10]));
                db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, Convert.ToDecimal(SitePrice[11]));
                db.AddInParameter(dbCmd, "@MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(SitePrice[12]));
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, SitePrice[13]);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, SitePrice[14]);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS", DbType.String, SitePrice[15]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(SitePrice[16]));
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
        public void deleteSitePrice(int delsiteprice)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_SITE_SEEING_PRICE_LIST");
                db.AddInParameter(dbCmd, "@SITE_PRICELIST_ID", DbType.Int32, delsiteprice);
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
