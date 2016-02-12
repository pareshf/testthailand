using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;


namespace CRM.DataAccess.GIT
{
    public class SightSeeingPriceListGIT
    {
        #region  FETCH ORDER STAUTS RAD COMBO BOX
        public DataSet fetchComboData(String sp_name)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        #endregion

        #region Save
        public DataSet InsertUpdateSitePrice(String id,String time1,String fdate,String todate,String restaurant,String currency,String city,String pterms,String sightname,String agent,String package,String ismealApplicable,String chainame,String adultsic,String childsic,String adultpvt,String childpvt,String sicratepp,String pvtratepp,String mealtype,String status,
         String amargin, String AplusMargin,
           String AplusplusMargin, String Amarper, String Aplusper, String AplusplusPer,String time2,String time3,String time4,String time5, int user)
        {
            Database db = null;
            DbCommand dbCmd = null;
            Boolean flag;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_SIGHT_SEEING_PRICE_LIST_GIT");
                db.AddInParameter(dbCmd, "@SIGHT_SEEING_PRICE_ID", DbType.Int32, int.Parse(id));

                if (time1.ToString().Equals(""))
                {

                    db.AddInParameter(dbCmd, "@SIGHT_SEEING_TIME", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@SIGHT_SEEING_TIME", DbType.DateTime, DateTime.Parse(time1.ToString()));
                }

                if (fdate.ToString().Equals(""))
                {

                    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DateTime.ParseExact(fdate, "dd/MM/yyyy", null));
                }
                if (todate.ToString().Equals(""))
                {

                    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DateTime.ParseExact(todate, "dd/MM/yyyy", null));
                }

                db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, restaurant);


                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, currency);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, city);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS", DbType.String, pterms);
                db.AddInParameter(dbCmd, "@SITE_NAME", DbType.String, sightname);
                db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.String, agent);
                db.AddInParameter(dbCmd, "@PACKAGE_NAME", DbType.String, package);

                if (Convert.ToString(ismealApplicable).Equals("YES"))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                db.AddInParameter(dbCmd, "@IS_MEAL", DbType.Boolean, flag);
                db.AddInParameter(dbCmd, "@CHAIN_NAME", DbType.String, chainame);

                if (adultsic == "")
                {
                    db.AddInParameter(dbCmd, "@ADULT_SIC_RATE", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ADULT_SIC_RATE", DbType.Decimal, Convert.ToDecimal(adultsic));
                }
                if (childsic == "")
                {
                    db.AddInParameter(dbCmd, "@CHILD_SIC_RATE", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHILD_SIC_RATE", DbType.Decimal, Convert.ToDecimal(childsic));
                }
                if (adultpvt=="")
                {
                    db.AddInParameter(dbCmd, "@ADULT_PVT_RATE", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ADULT_PVT_RATE", DbType.Decimal, Convert.ToDecimal(adultpvt));
                }
                if (childpvt=="")
                {
                    db.AddInParameter(dbCmd, "@CHILD_PVT_RATE", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHILD_PVT_RATE", DbType.Decimal, Convert.ToDecimal(childpvt));
                }
                if (sicratepp=="")
                {
                    db.AddInParameter(dbCmd, "@SICRATE_PER_PERSON", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@SICRATE_PER_PERSON", DbType.Decimal, Convert.ToDecimal(sicratepp));
                }
                if (pvtratepp=="")
                {
                    db.AddInParameter(dbCmd, "@PVTRATE_PER_PERSON", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PVTRATE_PER_PERSON", DbType.Decimal, Convert.ToDecimal(pvtratepp));
                }
                db.AddInParameter(dbCmd, "@MEAL_TYPE", DbType.String, mealtype);
                db.AddInParameter(dbCmd, "@STATUS", DbType.String,status);


                if (amargin == "")
                {
                    db.AddInParameter(dbCmd, "@A_MARGIN", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_MARGIN", DbType.Decimal, Convert.ToDecimal(amargin));
                }
                if (AplusMargin == "")
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_MARGIN", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_MARGIN", DbType.Decimal, Convert.ToDecimal(AplusMargin));
                }
                if (AplusplusMargin=="")
                {
                     db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN", DbType.Decimal, Convert.ToDecimal(AplusplusMargin));
                }
                if (Amarper == "")
                {
                    db.AddInParameter(dbCmd, "@A_MARGIN_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_MARGIN_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(Amarper));
                }
                if (Aplusper=="")
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(Aplusper));
                }
                if (AplusplusPer == "")
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MAGIN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MAGIN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(AplusplusPer));
                }
                if (time2.ToString().Equals(""))
                {

                    db.AddInParameter(dbCmd, "@TIME1", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME1", DbType.DateTime, DateTime.Parse(time2.ToString()));
                }
                if (time3.ToString().Equals(""))
                {

                    db.AddInParameter(dbCmd, "@TIME2", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME2", DbType.DateTime, DateTime.Parse(time3.ToString()));
                }
                if (time4.ToString().Equals(""))
                {

                    db.AddInParameter(dbCmd, "@TIME3", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME3", DbType.DateTime, DateTime.Parse(time4.ToString()));
                }
                if (time5.ToString().Equals(""))
                {

                    db.AddInParameter(dbCmd, "@TIME4", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME4", DbType.DateTime, DateTime.Parse(time5.ToString()));
                }

                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(user));
                ds = db.ExecuteDataSet(dbCmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        #endregion

        #region For Search Grid page
        public DataSet GetGridData(String city, String agent, String supplier, String package, String fdate, String todate)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_DATA_FRO_SIGHT_SEEING_PRICELIST_GRID_GIT");

                db.AddInParameter(dbCmd, "@CITY", DbType.String, city);
                //db.AddInParameter(dbCmd, "@AGENT", DbType.String, agent);
                db.AddInParameter(dbCmd, "@SUPPLIER", DbType.String, supplier);
                db.AddInParameter(dbCmd, "@SIGHT_PACKAGE", DbType.String, package);
                //if (fdate.ToString().Equals(""))
                //{

                //    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(fdate.ToString(), "dd/MM/yyyy", null));
                //}
                //if (todate.ToString().Equals(""))
                //{

                //    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(todate.ToString(), "dd/MM/yyyy", null));
                //}
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsData;
        }
        #endregion

        #region  Edit Hotel Price List
        public DataSet EditSightPriceList(String sp_name, int a)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@SIGHT_SEEING_PRICE_ID", DbType.String, a);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        #endregion

        #region Fetch Date Validation
        public DataSet fetch_date_validation(String CITY_NAME, String Supplier,String AGENT,String package)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_DATE_VALIDATION_FOR_SIGHT_SEEING_PRICE_LIST");
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, CITY_NAME);
                db.AddInParameter(dbCmd, "@SUPPLIER", DbType.String, Supplier);
                db.AddInParameter(dbCmd, "@AGENT", DbType.String, AGENT);
                db.AddInParameter(dbCmd, "@PACKAGE", DbType.String, package);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        #endregion

        public DataSet InsertUpdateSitePrice1(String id, String time1, String time2, String time3, String time4, String time5)
        {
            Database db = null;
            DbCommand dbCmd = null;
            Boolean flag;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_TIME_SIGHT_SEEING_PRICE_LIST_GIT");
                db.AddInParameter(dbCmd, "@SIGHT_SEEING_PRICE_ID", DbType.Int32, int.Parse(id));                                
                db.AddInParameter(dbCmd, "@TIME1", DbType.String, time1);
                db.AddInParameter(dbCmd, "@TIME2", DbType.String, time2);
                db.AddInParameter(dbCmd, "@TIME3", DbType.String, time3);
                db.AddInParameter(dbCmd, "@TIME4", DbType.String, time4);
                db.AddInParameter(dbCmd, "@TIME5", DbType.String, time5);
                                
                ds = db.ExecuteDataSet(dbCmd);

            }
            catch (Exception ex)
            {

            }
            return ds;
        }

    }
}
