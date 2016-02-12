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
    public class SiteSeeingPriceStoredProcedure
    {
        public void InsertUpdateSitePrice(ArrayList SitePrice)
        {
            Database db = null;
            DbCommand dbCmd = null;
            Boolean flag;
            //DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_SIGHT_SEEING_PRICE_LIST");
                db.AddInParameter(dbCmd, "@SIGHT_SEEING_PRICE_ID", DbType.Int32, SitePrice[0]);

                if (SitePrice[1].ToString().Equals("DD/MM/YYYY") || SitePrice[1].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@SIGHT_SEEING_TIME", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@SIGHT_SEEING_TIME", DbType.String, SitePrice[1]);
                }

                if (SitePrice[2].ToString().Equals("DD/MM/YYYY") || SitePrice[2].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DateTime.ParseExact(SitePrice[2].ToString(), "dd/MM/yyyy", null));
                }
                if (SitePrice[3].ToString().Equals("DD/MM/YYYY") || SitePrice[3].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DateTime.ParseExact(SitePrice[3].ToString(), "dd/MM/yyyy", null));
                }

                db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, SitePrice[4]);

                // db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, Convert.ToDecimal(SitePrice[5]));
                //db.AddInParameter(dbCmd, "@MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(SitePrice[6]));
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, SitePrice[7]);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, SitePrice[8]);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS", DbType.String, SitePrice[9]);
                db.AddInParameter(dbCmd, "@SITE_NAME", DbType.String, SitePrice[10]);
                db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.String, SitePrice[11]);
                db.AddInParameter(dbCmd, "@PACKAGE_NAME", DbType.String, SitePrice[12]);

                if (Convert.ToString(SitePrice[13]).Equals("YES"))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                db.AddInParameter(dbCmd, "@IS_MEAL", DbType.Boolean, flag);

                db.AddInParameter(dbCmd, "@CHAIN_NAME", DbType.String, SitePrice[14]);
                db.AddInParameter(dbCmd, "@ADULT_SIC_RATE", DbType.Decimal, Convert.ToDecimal(SitePrice[15]));
                db.AddInParameter(dbCmd, "@CHILD_SIC_RATE", DbType.Decimal, Convert.ToDecimal(SitePrice[16]));
                db.AddInParameter(dbCmd, "@ADULT_PVT_RATE", DbType.Decimal, Convert.ToDecimal(SitePrice[17]));
                db.AddInParameter(dbCmd, "@CHILD_PVT_RATE", DbType.Decimal, Convert.ToDecimal(SitePrice[18]));
                db.AddInParameter(dbCmd, "@SICRATE_PER_PERSON", DbType.Decimal, Convert.ToDecimal(SitePrice[19]));
                db.AddInParameter(dbCmd, "@PVTRATE_PER_PERSON", DbType.Decimal, Convert.ToDecimal(SitePrice[20]));
                db.AddInParameter(dbCmd, "@MEAL_TYPE", DbType.String, SitePrice[21]);
                db.AddInParameter(dbCmd, "@STATUS", DbType.String, SitePrice[22]);

                db.AddInParameter(dbCmd, "@SEVICE_VOUCHER", DbType.String, SitePrice[23]);

                db.AddInParameter(dbCmd, "@A_MARGIN", DbType.Decimal, Convert.ToDecimal(SitePrice[24]));
                db.AddInParameter(dbCmd, "@A_PLUS_MARGIN", DbType.Decimal, Convert.ToDecimal(SitePrice[25]));
                db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN", DbType.Decimal, Convert.ToDecimal(SitePrice[26]));
                db.AddInParameter(dbCmd, "@A_MARGIN_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(SitePrice[27]));
                db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(SitePrice[28]));
                db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MAGIN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(SitePrice[29]));

                if (SitePrice[30].ToString().Equals("DD/MM/YYYY") || SitePrice[30].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@TIME1", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME1", DbType.String, SitePrice[30]);
                }

                if (SitePrice[31].ToString().Equals("DD/MM/YYYY") || SitePrice[31].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@TIME2", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME2", DbType.String, SitePrice[31]);
                }

                if (SitePrice[32].ToString().Equals("DD/MM/YYYY") || SitePrice[32].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@TIME3", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME3", DbType.String, SitePrice[32]);
                }

                if (SitePrice[33].ToString().Equals("DD/MM/YYYY") || SitePrice[33].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@TIME4", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME4", DbType.String, SitePrice[33]);
                }

                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(SitePrice[34]));

                db.ExecuteNonQuery(dbCmd);
                //ds = db.ExecuteDataSet(dbCmd);


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
            //return ds;
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

        public DataSet CopyData(int siteid)
        {
            Database db = null;
            DbCommand dbCmd = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("COPY_DATA_FOR_SITESEEING_PRICE_DATA");
            db.AddInParameter(dbCmd, "@SIGHT_SEEING_PRICE_ID", DbType.Int32, siteid);
            DataSet ds = db.ExecuteDataSet(dbCmd);
            return ds;
        }

        public void InsertUpdateSightDay(ArrayList Day)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_DAY_SITE_SEEING");
                db.AddInParameter(dbCmd, "@OPERATED_DAYS_ID", DbType.Int32, Day[1]);
                db.AddInParameter(dbCmd, "@DAYS", DbType.String, Day[0]);
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

        public void InsertUpdateSightDate(ArrayList date)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_DATE_SITE_SEEING");
                db.AddInParameter(dbCmd, "@OPERATED_DATE_ID", DbType.Int32, date[1]);
                if (date[0].ToString().Equals("DD/MM/YYYY") || date[0].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@OPERATED_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@OPERATED_DATE", DbType.DateTime, DateTime.ParseExact(date[0].ToString(), "dd/MM/yyyy", null));
                }
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

        public void InsertNewdays(string day)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_DAYS_SIGHT");
                db.AddInParameter(dbCmd, "@SITE_SEEING_PRICE_ID", DbType.Int32, Convert.ToInt32(day));
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

        public void InsertNewdate(string date)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_DATE_SIGHT");
                db.AddInParameter(dbCmd, "@SITE_SEEING_PRICE_ID", DbType.Int32, Convert.ToInt32(date));
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

        public void deletedate(int delsiteprice)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_DATE_FROM_SITE_SEEING_PRICE_LIST");
                db.AddInParameter(dbCmd, "@OPERATED_DATE_ID", DbType.Int32, delsiteprice);
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

        public void deleteday(int delday)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_DAYS_FROM_SITE_SEEING_PRICE_LIST");
                db.AddInParameter(dbCmd, "@OPERATED_DAYS_ID", DbType.Int32, delday);
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
            dbCmd = db.GetStoredProcCommand("CHECK_VALIDATION_FOR_DROPDOWN_OF_GRID");
            DataSet ds1 = db.ExecuteDataSet(dbCmd);
            return ds1;
        }

        public DataSet FetchSlabData()
        {
            Database db = null;
            DbCommand dbCmd = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("FETCH_FROM_FIT_SLAB_MASTER");
            DataSet ds1 = db.ExecuteDataSet(dbCmd);
            return ds1;
        }

        public DataSet Fetch_Data_for_Price(string price_id, int slab_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_DATA_FROM_SITE_SEEING_PRICE_LIST_SLAB_WISE");
                db.AddInParameter(dbCmd, "@SUPPLIER_PRICE_ID", DbType.Int32, int.Parse(price_id));
                db.AddInParameter(dbCmd, "@SLAB_ID", DbType.Int32, slab_id);
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

        public DataSet InsertUpdateSightSeeingPrice(string slabmasterid, string slab_id, string priceid, int noofpax, string adult_sic, string child_sic, string adult_Pvt, string child_pvt, string A_MARGIN_IN_AMOUNT, string A_PLUS_MARGIN_IN_AMOUNT, string A_PLUS_PLUS_MARGIN_IN_AMOUNT, string A_MARGIN_AMOUNT_IN_PERCENTAGE, string A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE, string A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SITE_SEEING_PRICE_LIST_SLAB_WISE");
                db.AddInParameter(dbCmd, "@SITE_SEEING_SLAB_MASTER_ID", DbType.Int32, int.Parse(slabmasterid));
                db.AddInParameter(dbCmd, "@SLAB_ID", DbType.Int32, int.Parse(slab_id));
                db.AddInParameter(dbCmd, "@SIGHT_SEEING_PRICE_ID", DbType.Int32, int.Parse(priceid));
                db.AddInParameter(dbCmd, "@NO_OF_PAX", DbType.Int32, noofpax);
                if (adult_sic.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ADULT_SIC_RATE", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ADULT_SIC_RATE", DbType.Decimal, Convert.ToDecimal(adult_sic));
                }
                if (child_sic.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CHILD_SIC_RATE", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHILD_SIC_RATE", DbType.Decimal, Convert.ToDecimal(child_sic));
                }
                if (adult_Pvt.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ADULT_PVT_RATE", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ADULT_PVT_RATE", DbType.Decimal, Convert.ToDecimal(adult_Pvt));
                    
                }
                if (child_pvt.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CHILD_PVT_RATE", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHILD_PVT_RATE", DbType.Decimal,Convert.ToDecimal(child_pvt));
                }
                if (A_MARGIN_IN_AMOUNT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@A_MARGIN_IN_AMOUNT", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_MARGIN_IN_AMOUNT", DbType.Decimal, Convert.ToDecimal(A_MARGIN_IN_AMOUNT));
                }
                if (A_PLUS_MARGIN_IN_AMOUNT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_IN_AMOUNT", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_IN_AMOUNT", DbType.Decimal, Convert.ToDecimal(A_PLUS_MARGIN_IN_AMOUNT));
                }
                if (A_PLUS_PLUS_MARGIN_IN_AMOUNT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN_IN_AMOUNT", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN_IN_AMOUNT", DbType.Decimal, Convert.ToDecimal(A_PLUS_PLUS_MARGIN_IN_AMOUNT));
                }
                if (A_MARGIN_AMOUNT_IN_PERCENTAGE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@A_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(A_MARGIN_AMOUNT_IN_PERCENTAGE));
                }
                if (A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE));
                }
                if (A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE));
                }
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
    }

}
