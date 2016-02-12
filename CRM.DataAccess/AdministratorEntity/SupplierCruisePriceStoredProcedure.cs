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
    public class SupplierCruisePriceStoredProcedure
    {
        public void InsertUpdateCruisePrice(ArrayList CruisePrice)
        {
            Database db = null;
            DbCommand dbCmd = null;
           // DataSet ds = null;
           // Boolean flag;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_SUPPLIER_CRUISE_PRICE_LIST_MASTER");
                db.AddInParameter(dbCmd, "@SUPPLIER_CRUISE_PRICE_LIST_ID", DbType.Int32, CruisePrice[0]);
                //if (CruisePrice[1].ToString().Equals("DD/MM/YYYY") || CruisePrice[1].ToString().Equals("0"))
                //{

                //    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DateTime.ParseExact(CruisePrice[1].ToString(), "dd/MM/yyyy", null));
                //}
                //if (CruisePrice[2].ToString().Equals("DD/MM/YYYY") || CruisePrice[2].ToString().Equals("0"))
                //{

                //    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DateTime.ParseExact(CruisePrice[2].ToString(), "dd/MM/yyyy", null));
                //}
                db.AddInParameter(dbCmd, "@CRUISE_CABIN", DbType.String, CruisePrice[3]);
                db.AddInParameter(dbCmd, "@DECK_NO", DbType.Int32, Convert.ToInt32(CruisePrice[4]));
                db.AddInParameter(dbCmd, "@CRUISE_VIEW", DbType.String, CruisePrice[5]);
                //db.AddInParameter(dbCmd, "@GIT_RATE", DbType.Decimal, Convert.ToDecimal(CruisePrice[6]));
                //db.AddInParameter(dbCmd, "@FIT_RATE", DbType.Decimal, Convert.ToDecimal(CruisePrice[7]));
               // db.AddInParameter(dbCmd, "@FIT_DISCOUNT", DbType.Decimal, Convert.ToDecimal(CruisePrice[8]));
               // db.AddInParameter(dbCmd, "@GIT_DISCOUNT", DbType.Decimal, Convert.ToDecimal(CruisePrice[9]));
                db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, Convert.ToDecimal(CruisePrice[10]));
                db.AddInParameter(dbCmd, "@MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(CruisePrice[11]));
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, CruisePrice[12]);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS", DbType.String, CruisePrice[13]);
                
                db.AddInParameter(dbCmd, "@EXTRA_ADULT_RATE", DbType.Decimal, Convert.ToDecimal(CruisePrice[19]));
                db.AddInParameter(dbCmd, "@EXTRA_CNB_COST", DbType.Decimal, Convert.ToDecimal(CruisePrice[21]));
                db.AddInParameter(dbCmd, "@EXTRA_CWB_COST", DbType.Decimal, Convert.ToDecimal(CruisePrice[20]));
                db.AddInParameter(dbCmd, "@SINGLE_ROOM_RATE", DbType.Decimal, Convert.ToDecimal(CruisePrice[17]));
                db.AddInParameter(dbCmd, "@DOUBLE_ROOM_RATE", DbType.Decimal, Convert.ToDecimal(CruisePrice[18]));

                //if (Convert.ToString(CruisePrice[14]).Equals("YES"))
                //{
                //    flag = true;
                //}
                //else
                //{
                //    flag = false;
                //}
                ////db.AddInParameter(dbCmd, "@AUTO_BOOK_ON_LOW_INVENTORY", DbType.Boolean, flag);
                db.AddInParameter(dbCmd, "@CUSTOMER_NAME", DbType.String, CruisePrice[15]);
                db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, CruisePrice[16]);
               // db.AddInParameter(dbCmd, "@CRUISE_DESC", DbType.String, CruisePrice[17]);
               // db.AddInParameter(dbCmd, "@GIT_MARGIN_AMOUNT", DbType.Decimal, Convert.ToDecimal(CruisePrice[18]));
               // db.AddInParameter(dbCmd, "@GIT_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(CruisePrice[19]));
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(CruisePrice[24]));
                if (CruisePrice[22].ToString().Equals("DD/MM/YYYY") || CruisePrice[22].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(CruisePrice[22].ToString(), "dd/MM/yyyy", null));
                }
                if (CruisePrice[23].ToString().Equals("DD/MM/YYYY") || CruisePrice[23].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(CruisePrice[23].ToString(), "dd/MM/yyyy", null));
                }
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
        public void delCruisePrice(int cruisepriceid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FORM_SUPPLIER_CRUISE_PRICE_LIST");
                db.AddInParameter(dbCmd, "@CRUISE_PRICE_LIST_ID", DbType.Int32, cruisepriceid);
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
        public void insertCruisePriceDocument(int docid, string docname)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_CRUISE_PRICE_DOCUMENT");
                db.AddInParameter(dbCmd, "@SUPPLIER_CRUISE_PRICE_LIST_ID", DbType.Int32, docid);
                db.AddInParameter(dbCmd, "@CRUISE_DOC", DbType.String, docname);
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
        public DataSet getCruisePriceDocument(int docid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("GET_SUPPLIER_CRUISE_DOCUMENT");
            db.AddInParameter(dbCmd, "@CRUISE_PRICE_ID", DbType.Int32, docid);
            ds = db.ExecuteDataSet(dbCmd);
            return ds;
        }
        public void InsertUpdateCruiseCabinCategory(ArrayList Inventory)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SUPPLIER_CRUISE_CABINE_INVENTORY");
                db.AddInParameter(dbCmd, "@SUPPLIER_CRUISE_CABIN_INVENTORY_ID", DbType.Int32, Convert.ToInt32(Inventory[0]));
                db.AddInParameter(dbCmd, "@NO_OF_CABINS_PURCHASED", DbType.Int32, Convert.ToInt32(Inventory[1]));
                db.AddInParameter(dbCmd, "@NO_OF_CABINS_AVAILABLE", DbType.Int32, Convert.ToInt32(Inventory[2]));
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
        public DataSet CopyData(int supplierid)
        {
            Database db = null;
            DbCommand dbCmd = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("COPY_DATA_FOR_SUPPLIER_CRUISE_PRICE_LIST");
            db.AddInParameter(dbCmd, "@SUPPLIER_CRUISE_PRICE_LIST_ID", DbType.Int32, supplierid);
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
