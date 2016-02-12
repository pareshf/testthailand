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
    public class SupplierGuidePriceStoredProcedure
    {
        public DataSet InsertUpdateGuidePrice(ArrayList GuidePrice)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_SUPPLIER_GUIDE_PRICE_LIST");
                db.AddInParameter(dbCmd, "@GUIDE_PRICE_LIST_ID", DbType.Int32, GuidePrice[0]);
                //if (GuidePrice[1].ToString().Equals("DD/MM/YYYY") || GuidePrice[1].ToString().Equals("DD/MM/YYYY"))
                //{

                //    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DateTime.ParseExact(GuidePrice[1].ToString(), "dd/MM/yyyy", null));
                //}
                //if (GuidePrice[2].ToString().Equals("DD/MM/YYYY") || GuidePrice[2].ToString().Equals("0"))
                //{

                //    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DateTime.ParseExact(GuidePrice[2].ToString(), "dd/MM/yyyy", null));
                //}
                db.AddInParameter(dbCmd, "@SITE_NAME", DbType.String, GuidePrice[3]);
                db.AddInParameter(dbCmd, "@CUSTOMER_NAME", DbType.String, GuidePrice[4]);
                db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, GuidePrice[5]);
                db.AddInParameter(dbCmd, "@GIT_RATE", DbType.Decimal, Convert.ToDecimal(GuidePrice[6]));
                db.AddInParameter(dbCmd, "@FIT_RATE", DbType.Decimal, Convert.ToDecimal(GuidePrice[7]));
                db.AddInParameter(dbCmd, "@FIT_DISCOUNT", DbType.Decimal, Convert.ToDecimal(GuidePrice[8]));
                db.AddInParameter(dbCmd, "@GIT_DISCOUNT", DbType.Decimal, Convert.ToDecimal(GuidePrice[9]));
                db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, Convert.ToDecimal(GuidePrice[10]));
                db.AddInParameter(dbCmd, "@MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(GuidePrice[11]));
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, GuidePrice[12]);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS", DbType.String, GuidePrice[13]);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, GuidePrice[14]);
                db.AddInParameter(dbCmd, "@GIT_MARGIN_AMOUNT", DbType.Decimal, Convert.ToDecimal(GuidePrice[15]));
                db.AddInParameter(dbCmd, "@GIT_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(GuidePrice[16]));
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(GuidePrice[17]));
                if (GuidePrice[19].ToString().Equals("DD/MM/YYYY") || GuidePrice[19].ToString().Equals("DD/MM/YYYY"))
                {

                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(GuidePrice[19].ToString(), "dd/MM/yyyy", null));
                }
                if (GuidePrice[20].ToString().Equals("DD/MM/YYYY") || GuidePrice[20].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(GuidePrice[20].ToString(), "dd/MM/yyyy", null));
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
        public void delGuidePrice(int GuidePriceid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_SUPPLIER_GUIDE_PRICE_LIST");
                db.AddInParameter(dbCmd, "@GUIDE_PRICE_LIST", DbType.Int32, GuidePriceid);
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
        public void insertUpdateGuidePriceDocument(int docid, string docname)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_SUPPLIER_GUIDE_PRICE_DOCUMENT");
                db.AddInParameter(dbCmd, "@SUPPLIER_GUIDE_ID", DbType.Int32, docid);
                db.AddInParameter(dbCmd, "@GUIDE_PRICE_DOC", DbType.String, docname);
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
        public DataSet getGuidePriceDocument(int docid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("GET_SUPPLIER_GUIDE_DOCUMENT");
            db.AddInParameter(dbCmd, "@GUIDE_PRICE_LIST_ID", DbType.Int32, docid);
            ds = db.ExecuteDataSet(dbCmd);
            return ds;
        }
        public DataSet CopyData(int supplierid)
        {
            Database db = null;
            DbCommand dbCmd = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("COPY_DATA_FOR_SUPPLIER_GUIDE_PRICE_LIST");
            db.AddInParameter(dbCmd, "@GUIDE_PRICE_LIST_ID", DbType.Int32, supplierid);
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
