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
    public class VisaQuoteStoredProcedure
    {
        public void InsertUpdateVisaQuote(ArrayList VisaQuote)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_FARE_VISA_QUOTE_MASTER");
                db.AddInParameter(dbCmd, "@VISA_QUOTE_ID", DbType.Int32, VisaQuote[1]);
                db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, VisaQuote[7]);
                db.AddInParameter(dbCmd, "@VISA_TYPE", DbType.String, VisaQuote[8]);
                db.AddInParameter(dbCmd, "@TRAVEL_PERIOD_ID", DbType.Int32, VisaQuote[6]);
                db.AddInParameter(dbCmd, "@PROCESS_TYPE_NAME", DbType.String, VisaQuote[0]);
                db.AddInParameter(dbCmd, "@SINGLE_MULTIPLE_VISA", DbType.String, VisaQuote[2]);
                db.AddInParameter(dbCmd, "@VISA_FEE", DbType.Int32, VisaQuote[3]);
                db.AddInParameter(dbCmd, "@VFS", DbType.Int32, VisaQuote[4]);
                db.AddInParameter(dbCmd, "@SERVICE_CHARGE", DbType.Int32, VisaQuote[5]);
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
        public void deleteVisaQuote(int delvisaquote)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_FARE_VISA_QUOTE_MASTER");
                db.AddInParameter(dbCmd, "@VISAQUOTEID", DbType.Int32, delvisaquote);
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
        public DataSet GetdocumentDetail(int VISA_QUOTE_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("GET_VISA_DOC");
            db.AddInParameter(dbCmd, "@VISA_QUOTE_ID", DbType.Int32, VISA_QUOTE_ID);
            ds = db.ExecuteDataSet(dbCmd);
            return ds;

        }

        public void insertnewvisadoc(int docid, string doc)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_NEW_VISA_DOCUMENT");
                db.AddInParameter(dbCmd, "@VISA_QUOTEID", DbType.Int32, docid);
                db.AddInParameter(dbCmd, "@REQ_DOC_PATH", DbType.String, doc);
                db.AddInParameter(dbCmd, "@QUE_DOC_PATH", DbType.String, null);
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
        public void insertnewQuestion(int docid, string doc)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_NEW_VISA_DOCUMENT1");
                db.AddInParameter(dbCmd, "@VISA_QUOTEID", DbType.Int32, docid);
                db.AddInParameter(dbCmd, "@REQ_DOC_PATH", DbType.String, null);
                db.AddInParameter(dbCmd, "@QUE_DOC_PATH", DbType.String, doc);
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
