using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.FIT
{
    public class SupplierPriceListMaster
    {
        #region Get Details
        public DataSet  GetSupplierName()
        {
            Database db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            DbCommand dbCmd = db.GetStoredProcCommand("GET_SUPPLIER_NAME_FOR_FIT");
           
            DataSet ds = db.ExecuteDataSet(dbCmd);
            return ds;
            
        }

        public DataTable GetCity(int SI)
        {
            Database db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            DbCommand dbCmd = db.GetStoredProcCommand("GET_CITY_NAME_FOR_FIT");
            DataTable dt = null;
            DataSet ds = db.ExecuteDataSet(dbCmd);
            dt = ds.Tables[0];
            return dt;
        }

        public DataSet GetNameandCity(int SI)
        {
            Database db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            DbCommand dbCmd = db.GetStoredProcCommand("SP_GET_SUPPLIER_NAME_AND_CITY_NAME", SI);
            DataTable dt = null;
            DataSet ds = db.ExecuteDataSet(dbCmd);
            dt = ds.Tables[0];
            return ds;
        }

        public DataTable GetRoomType()
        {
            Database db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            DbCommand dbCmd = db.GetStoredProcCommand("SP_GET_ROOM_TYPE_FOR_FIT");
            DataTable dt = null;
            DataSet ds = db.ExecuteDataSet(dbCmd);
            dt = ds.Tables[0];
            return dt;
        }

        public DataTable GetCurrency()
        {
            Database db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            DbCommand dbCmd = db.GetStoredProcCommand("SP_FETCH_CURRENCY_NAME_FOR_FIT");
            DataTable dt = null;
            DataSet ds = db.ExecuteDataSet(dbCmd);
            dt = ds.Tables[0];
            return dt;
        }

        public DataTable GetStatus()
        {
            Database db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            DbCommand dbCmd = db.GetStoredProcCommand("SP_FETCH_STATUS_FOR_FIT");
            DataTable dt = null;
            DataSet ds = db.ExecuteDataSet(dbCmd);
            dt = ds.Tables[0];
            return dt;
        }
        #endregion

        #region Insert
        public void INSERTFITPRICE(string SID, string ROOMT, string FROMDT, string TODT, string SINGLE, string DOUBLE, string TRIPLE, string EXTRAADULTRATE, string EXTRACWBRATE, string EXTRACNBRATE,
                                                string IsDefault, string STATUS, string AMA,string APMA,string APPMA,string AMP,string APMP,string APPMP,string CURRENCY, string CITY)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                DbCommand dbCmd = db.GetStoredProcCommand("SP_INSERT_PRICE_LIST_FOR_FIT");
                db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, SID);
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.String, ROOMT);
                if (FROMDT != "")
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(FROMDT, "dd/MM/yyyy", null));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                if (TODT != "")
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(TODT, "dd/MM/yyyy", null));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                db.AddInParameter(dbCmd, "@SINGLE_ROOM_RATE", DbType.Decimal, decimal.Parse(SINGLE));
                db.AddInParameter(dbCmd, "@DOUBLE_ROOM_RATE", DbType.Decimal, decimal.Parse(DOUBLE));
                db.AddInParameter(dbCmd, "@TRIPLE_ROOM_RATE", DbType.Decimal, decimal.Parse(TRIPLE));
                db.AddInParameter(dbCmd, "@EXTRA_ADULT_RATE", DbType.Decimal, decimal.Parse(EXTRAADULTRATE));
                db.AddInParameter(dbCmd, "@EXTRA_CWB_COST", DbType.Decimal, decimal.Parse(EXTRACWBRATE));
                db.AddInParameter(dbCmd, "@EXTRA_CNB_COST", DbType.Decimal, decimal.Parse(EXTRACNBRATE));
                db.AddInParameter(dbCmd, "@IS_DEFAULT", DbType.String, IsDefault);
                db.AddInParameter(dbCmd, "@PRICE_LIST_STATUS_ID", DbType.String, STATUS);
                db.AddInParameter(dbCmd, "@A_MARGIN_IN_AMOUNT", DbType.Decimal, decimal.Parse(AMA));
                db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_IN_AMOUNT", DbType.Decimal, decimal.Parse(APMA));
                db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN_IN_AMOUNT", DbType.Decimal, decimal.Parse(APPMA));
                db.AddInParameter(dbCmd, "@A_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, decimal.Parse(AMP));
                db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, decimal.Parse(APMP));
                db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, decimal.Parse(APPMP));
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.String, CURRENCY);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.String, CITY);
                db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update
        public void UPDATEFITPRICE(string SID, string ROOMT, string FROMDT, string TODT, string SINGLE, string DOUBLE, string TRIPLE, string EXTRAADULTRATE, string EXTRACWBRATE, string EXTRACNBRATE,
                                                string IsDefault, string STATUS, string AMA, string APMA, string APPMA, string AMP, string APMP, string APPMP, string CURRENCY, string CITY, string pricelistid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                DbCommand dbCmd = db.GetStoredProcCommand("SP_UPDATE_PRICE_LIST_FOR_FIT");
                db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, SID);
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.String, ROOMT);
                if (FROMDT != "")
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(FROMDT, "dd/MM/yyyy", null));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                if (TODT != "")
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(TODT, "dd/MM/yyyy", null));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                db.AddInParameter(dbCmd, "@SINGLE_ROOM_RATE", DbType.Decimal, decimal.Parse(SINGLE));
                db.AddInParameter(dbCmd, "@DOUBLE_ROOM_RATE", DbType.Decimal, decimal.Parse(DOUBLE));
                db.AddInParameter(dbCmd, "@TRIPLE_ROOM_RATE", DbType.Decimal, decimal.Parse(TRIPLE));
                db.AddInParameter(dbCmd, "@EXTRA_ADULT_RATE", DbType.Decimal, decimal.Parse(EXTRAADULTRATE));
                db.AddInParameter(dbCmd, "@EXTRA_CWB_COST", DbType.Decimal, decimal.Parse(EXTRACWBRATE));
                db.AddInParameter(dbCmd, "@EXTRA_CNB_COST", DbType.Decimal, decimal.Parse(EXTRACNBRATE));
                db.AddInParameter(dbCmd, "@IS_DEFAULT", DbType.String, IsDefault);
                db.AddInParameter(dbCmd, "@PRICE_LIST_STATUS_ID", DbType.String, STATUS);
                db.AddInParameter(dbCmd, "@A_MARGIN_IN_AMOUNT", DbType.Decimal, decimal.Parse(AMA));
                db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_IN_AMOUNT", DbType.Decimal, decimal.Parse(APMA));
                db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN_IN_AMOUNT", DbType.Decimal, decimal.Parse(APPMA));
                db.AddInParameter(dbCmd, "@A_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, decimal.Parse(AMP));
                db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, decimal.Parse(APMP));
                db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, decimal.Parse(APPMP));
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.String, CURRENCY);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.String, CITY);
                db.AddInParameter(dbCmd, "@PRICELISTID", DbType.String, pricelistid);
                db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Supplier Details
        public DataSet GetSupplierDetailsForEdit(int SID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("SP_GET_SUPPLIER_HOTEL_PRICE_LIST_DETAILS_FIT_EDIT");
                db.AddInParameter(dbCmd, "@SUPPLIER_ID", DbType.Int32, SID);
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
        public DataSet GetSupplierDetails(int SID, string Room, string Status)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("SP_GET_SUPPLIER_HOTEL_PRICE_LIST_DETAILS_FIT");
                db.AddInParameter(dbCmd, "@SUPPLIER_ID", DbType.Int32, SID);
                db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, Room);
                db.AddInParameter(dbCmd, "@STATUS", DbType.String, Status);
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
        #endregion

        public DataSet FetchChainNameandCity(int Supplierid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_SUPPLIER_NAME_AND_CITY_NAME");
                db.AddInParameter(dbCmd, "@SUPPLIER_ID", DbType.String, Supplierid);


                //Command Execute
                dsData = db.ExecuteDataSet(dbCmd);
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
            return dsData;
        }
    }
}
