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
    public class SupplierRestaurantPriceStoredProcedure
    {

        public void InsertUpdateRestaurantPrice(ArrayList RestaurantPrice)
        {
            Database db = null;
            DbCommand dbCmd = null;
           // DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_SUPPLIER_RESTAURANT_PRICE_LIST");
                db.AddInParameter(dbCmd, "@SUPPLIER_RESTAURANT_PRICE_LIST_ID", DbType.Int32, RestaurantPrice[0]);
                //if (RestaurantPrice[1].ToString().Equals("DD/MM/YYYY") || RestaurantPrice[1].ToString().Equals("0"))
                //{

                //    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DateTime.ParseExact(RestaurantPrice[1].ToString(), "dd/MM/yyyy", null));
                //}
                //if (RestaurantPrice[2].ToString().Equals("DD/MM/YYYY") || RestaurantPrice[2].ToString().Equals("0"))
                //{

                //    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DateTime.ParseExact(RestaurantPrice[2].ToString(), "dd/MM/yyyy", null));
                //}
                if (RestaurantPrice[3].ToString().Equals("DD/MM/YYYY") || RestaurantPrice[3].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@DINNER_FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DINNER_FROM_DATE", DbType.DateTime, DateTime.ParseExact(RestaurantPrice[3].ToString(), "dd/MM/yyyy", null));
                }
                if (RestaurantPrice[4].ToString().Equals("DD/MM/YYYY") || RestaurantPrice[4].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@DINNER_TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DINNER_TO_DATE", DbType.DateTime, DateTime.ParseExact(RestaurantPrice[4].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@DINNER_FROM_TIME", DbType.String, RestaurantPrice[5]);
                db.AddInParameter(dbCmd, "@DINNER_TO_TIME", DbType.String, RestaurantPrice[6]);
                db.AddInParameter(dbCmd, "@MEAL_NAME", DbType.String, RestaurantPrice[7]);
                db.AddInParameter(dbCmd, "@DINNER_DESCRIPTION", DbType.String, RestaurantPrice[8]);
                db.AddInParameter(dbCmd, "@GIT_RATE", DbType.Decimal, Convert.ToDecimal(RestaurantPrice[9]));
                db.AddInParameter(dbCmd, "@FIT_RATE", DbType.Decimal, Convert.ToDecimal(RestaurantPrice[10]));
                db.AddInParameter(dbCmd, "@FIT_DISCOUNT", DbType.Decimal, Convert.ToDecimal(RestaurantPrice[11]));
                db.AddInParameter(dbCmd, "@GIT_DISCOUNT", DbType.Decimal, Convert.ToDecimal(RestaurantPrice[12]));
                //db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, Convert.ToDecimal(RestaurantPrice[13]));
                //db.AddInParameter(dbCmd, "@MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(RestaurantPrice[14]));
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, RestaurantPrice[15]);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS", DbType.String, RestaurantPrice[16]);
                db.AddInParameter(dbCmd, "@CUSTOMER_NAME", DbType.String,RestaurantPrice[17]);
                db.AddInParameter(dbCmd, "@CHAIN_NAME", DbType.String,RestaurantPrice[18]);
                db.AddInParameter(dbCmd, "@GIT_MARGIN_AMOUNT", DbType.Decimal, Convert.ToDecimal(RestaurantPrice[19]));
                db.AddInParameter(dbCmd, "@GIT_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(RestaurantPrice[20]));
                db.AddInParameter(dbCmd, "@MEAL_TYPE", DbType.String, RestaurantPrice[21]);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, RestaurantPrice[22]);
                db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, Convert.ToDecimal(RestaurantPrice[23]));
                db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, Convert.ToDecimal(RestaurantPrice[24]));

                db.AddInParameter(dbCmd, "@SEVICE_VOUCHER", DbType.String, RestaurantPrice[25]);

                db.AddInParameter(dbCmd, "@A_MARGIN", DbType.Decimal, Convert.ToDecimal(RestaurantPrice[26]));
                db.AddInParameter(dbCmd, "@A_PLUS_MARGIN", DbType.Decimal, Convert.ToDecimal(RestaurantPrice[27]));
                db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN", DbType.Decimal, Convert.ToDecimal(RestaurantPrice[28]));
                db.AddInParameter(dbCmd, "@A_MARGIN_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(RestaurantPrice[29]));
                db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(RestaurantPrice[30]));
                db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MAGIN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(RestaurantPrice[31]));
                

                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(RestaurantPrice[32]));

                //ds = db.ExecuteDataSet(dbCmd);
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
            //return ds;
        }
        public void delRestaurantPrice(int delprice)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FORM_SUPPLIER_RESTAURANT_PRICE_LIST");
                db.AddInParameter(dbCmd, "@RESTAURANT_ID", DbType.Int32, delprice);
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
        public void insertRestaurantPriceDocument(int docid, string docname)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_RESTAURANT_PRICE_DOCUMENT");
                db.AddInParameter(dbCmd, "@SUPPLIER_RESTAURANT_PRICE_LIST_ID", DbType.Int32, docid);
                db.AddInParameter(dbCmd, "@RESTAURANT_DOC", DbType.String, docname);
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
        public DataSet getRestaurantPriceDocument(int docid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("GET_DOCUMENT_NAME_FOR_SUPPLIER_RESTAURANT_PRICE_LIST");
            db.AddInParameter(dbCmd, "@SUPPLIER_RESTAURANT_PRICE_LIST_ID", DbType.Int32, docid);
            ds = db.ExecuteDataSet(dbCmd);
            return ds;
        }
        public DataSet CopyData(int supplierid)
        {
            Database db = null;
            DbCommand dbCmd = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("COPY_DATA_FOR_SUPPLIER_RESTAURANT_PRICE_LIST");
            db.AddInParameter(dbCmd, "@SUPPLIER_RESTAURANT_PRICE_LIST_ID", DbType.Int32, supplierid);
            DataSet ds = db.ExecuteDataSet(dbCmd);
            return ds;
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
    }
}
