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
    public class SupplierCoachPriceStoredProcedure
    {
        
        public void InsertUpdateCoachPrice(ArrayList CoachPrice)
        {
            Database db = null;
            DbCommand dbCmd = null;
            //DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_SUPPLIER_COACH_PRICE_LIST");
                db.AddInParameter(dbCmd, "@COACH_PRICE_LIST_ID", DbType.Int32, CoachPrice[0]);
                //if (CoachPrice[1].ToString().Equals("DD/MM/YYYY") || CoachPrice[1].ToString().Equals("0"))
                //{

                //    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DateTime.ParseExact(CoachPrice[1].ToString(), "dd/MM/yyyy", null));
                //}
                //if (CoachPrice[2].ToString().Equals("DD/MM/YYYY") || CoachPrice[2].ToString().Equals("0"))
                //{

                //    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DateTime.ParseExact(CoachPrice[2].ToString(), "dd/MM/yyyy", null));
                //}
                db.AddInParameter(dbCmd, "@COACH_NAME", DbType.String, CoachPrice[3]);
                db.AddInParameter(dbCmd, "@CUSTOMER_NAME", DbType.String, CoachPrice[4]);
                db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, CoachPrice[5]);
                //db.AddInParameter(dbCmd, "@GIT_RATE", DbType.Decimal, Convert.ToDecimal(CoachPrice[6]));
                db.AddInParameter(dbCmd, "@FIT_RATE", DbType.Decimal, Convert.ToDecimal(CoachPrice[7]));
               // db.AddInParameter(dbCmd, "@FIT_DISCOUNT", DbType.Decimal, Convert.ToDecimal(CoachPrice[8]));
                //db.AddInParameter(dbCmd, "@GIT_DISCOUNT", DbType.Decimal, Convert.ToDecimal(CoachPrice[9]));
                db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, Convert.ToDecimal(CoachPrice[10]));
                db.AddInParameter(dbCmd, "@MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(CoachPrice[11]));
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, CoachPrice[12]);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS", DbType.String, CoachPrice[13]);
                db.AddInParameter(dbCmd, "@NO_OF_SEATS", DbType.Int32,Convert.ToInt32(CoachPrice[14]));
               // db.AddInParameter(dbCmd, "@GIT_MARGIN_AMOUNT", DbType.Decimal, Convert.ToDecimal(CoachPrice[15]));
               // db.AddInParameter(dbCmd, "@GIT_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(CoachPrice[16]));
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(CoachPrice[20]));
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, CoachPrice[17]);
                if (CoachPrice[18].ToString().Equals("DD/MM/YYYY") || CoachPrice[18].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(CoachPrice[18].ToString(), "dd/MM/yyyy", null));
                }
                if (CoachPrice[19].ToString().Equals("DD/MM/YYYY") || CoachPrice[19].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(CoachPrice[19].ToString(), "dd/MM/yyyy", null));
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
           // return ds;
        }
        public void delCoachPrice(int Coachpriceeid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FORM_COACH_PRICE_LIST");
                db.AddInParameter(dbCmd, "@COACH_PRICE_LIST_ID", DbType.Int32, Coachpriceeid);
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
        public void insertUpdateCoachPriceDocument(int docid, string docname)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_DOCUMENT_FOR_COACH_PRICE_LIST");
                db.AddInParameter(dbCmd, "@COACH_PRICE_ID", DbType.Int32, docid);
                db.AddInParameter(dbCmd, "@COACH_PRICE_DOC", DbType.String, docname);
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
        public DataSet getCoachPriceDocument(int docid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("GET_SUPPLIER_COACH_PRICELIST_DOCUMENT");
            db.AddInParameter(dbCmd, "@COACH_PRICE_LIST_ID", DbType.Int32, docid);
            ds = db.ExecuteDataSet(dbCmd);
            return ds;
        }

        public DataSet CopyData(int supplierid)
        {
            Database db = null;
            DbCommand dbCmd = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("COPY_DATA_FOR_SUPPLIER_COACH_PRICE_LIST");
            db.AddInParameter(dbCmd, "@COACH_PRICE_LIST_ID", DbType.Int32, supplierid);
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
