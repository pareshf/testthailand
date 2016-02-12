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
    public class SupplierCarPriceStoredProcedure
    {
        public void InsertUpdateCarPrice(ArrayList CarPrice)
        {
            Database db = null;
            DbCommand dbCmd = null;
            //DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_SUPPLIER_CAR_PRICE_LIST");
                db.AddInParameter(dbCmd, "@CAR_PRICE_LIST_MASTER_ID", DbType.Int32, CarPrice[0]);
                //if (CarPrice[1].ToString().Equals("DD/MM/YYYY") || CarPrice[1].ToString().Equals("0"))
                //{

                //    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DateTime.ParseExact(CarPrice[1].ToString(), "dd/MM/yyyy", null));
                //}
                //if (CarPrice[2].ToString().Equals("DD/MM/YYYY") || CarPrice[2].ToString().Equals("0"))
                //{

                //    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DateTime.ParseExact(CarPrice[2].ToString(), "dd/MM/yyyy", null));
                //}
                db.AddInParameter(dbCmd, "@CAR_NAME", DbType.String, CarPrice[3]);
                db.AddInParameter(dbCmd, "@CUST_NAME", DbType.String, CarPrice[4]);
                db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, CarPrice[5]);
                //db.AddInParameter(dbCmd, "@GIT_RATE", DbType.Decimal, Convert.ToDecimal(CarPrice[6]));
                db.AddInParameter(dbCmd, "@FIT_RATE", DbType.Decimal, Convert.ToDecimal(CarPrice[7]));
                //db.AddInParameter(dbCmd, "@FIT_DISCOUNT", DbType.Decimal, Convert.ToDecimal(CarPrice[8]));
               // db.AddInParameter(dbCmd, "@GIT_DISCOUNT", DbType.Decimal, Convert.ToDecimal(CarPrice[9]));
                db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, Convert.ToDecimal(CarPrice[10]));
                db.AddInParameter(dbCmd, "@MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(CarPrice[11]));
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, CarPrice[12]);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS", DbType.String, CarPrice[13]);
                //db.AddInParameter(dbCmd, "@GIT_MARGIN_AMOUNT", DbType.Decimal, Convert.ToDecimal(CarPrice[14]));
                //db.AddInParameter(dbCmd, "@GIT_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(CarPrice[15]));

                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, CarPrice[18]);
                db.AddInParameter(dbCmd, "@RATE_UNIT_NAME", DbType.String, CarPrice[19]);

                if (CarPrice[16].ToString().Equals("DD/MM/YYYY") || CarPrice[16].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(CarPrice[16].ToString(), "dd/MM/yyyy", null));
                }
                if (CarPrice[17].ToString().Equals("DD/MM/YYYY") || CarPrice[17].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(CarPrice[17].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(CarPrice[20]));
               // ds = db.ExecuteDataSet(dbCmd);
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
        public void delCarPrice(int Carpriceid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_SUPPLIER_CAR_PRICE_LIST");
                db.AddInParameter(dbCmd, "@CAR_PRICELIST_ID", DbType.Int32, Carpriceid);
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
        public void insertUpdateCarPriceDocument(int docid, string docname)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("ISNERT_UPDATE_FOR_SUPPLIER_DOCUMENT");
                db.AddInParameter(dbCmd, "@SUPPLIER_CAR_ID", DbType.Int32, docid);
                db.AddInParameter(dbCmd, "@CAR_DOCUMENT", DbType.String, docname);
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
        public DataSet getCarPriceDocument(int docid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("GET_DOCUMENT_FOR_SUPPLIER_CAR_PRICE_LIST");
            db.AddInParameter(dbCmd, "@CAR_PRICE_ID", DbType.Int32, docid);
            ds = db.ExecuteDataSet(dbCmd);
            return ds;
        }
        public DataSet CopyData(int supplierid)
        {
            Database db = null;
            DbCommand dbCmd = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("COPY_DATA_FOR_SUPPLIER_CAR_PRICE_LIST");
            db.AddInParameter(dbCmd, "@CAR_PRICE_LIST_MASTER_ID", DbType.Int32, supplierid);
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
