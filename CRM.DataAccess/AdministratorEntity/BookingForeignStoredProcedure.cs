#region Impoerts assemblies
using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;
#endregion

namespace CRM.DataAccess.AdministratorEntity
{
    public class BookingForeignStoredProcedure
    {
        public void InsertUpdateForeignAgent(ArrayList payment)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_BOOKING_FOREIGN");
                db.AddInParameter(dbCmd, "@FA_SR_NO", DbType.Int32, Convert.ToInt32(payment[0]));
                db.AddInParameter(dbCmd, "@AGENT_ID", DbType.String, payment[1]);
                db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, Convert.ToInt32(payment[2]));
                db.AddInParameter(dbCmd, "@FOREIGN_CURR_AGENT_ID", DbType.String, payment[3]);
                db.AddInParameter(dbCmd, "@PAYMENT_CURR_CODE", DbType.String, payment[4]);
                db.AddInParameter(dbCmd, "@PAYMENT_MODE_NAME", DbType.String, payment[5]);
                db.AddInParameter(dbCmd, "@REC_CHEQUE_DD_NO", DbType.String, payment[6]);
                db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, Convert.ToDecimal(payment[7]));
                db.AddInParameter(dbCmd, "@BANK_ID", DbType.String, payment[8]);
                db.AddInParameter(dbCmd, "@BANK_BRANCH_NAME", DbType.String, payment[9]);
                db.AddInParameter(dbCmd, "@RECEIPT_NO", DbType.String, payment[10]);
                if (payment[11].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@RECEIPT_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@RECEIPT_DATE", DbType.DateTime, DateTime.ParseExact(payment[11].ToString(), "dd/MM/yyyy", null));
                }
                //db.AddInParameter(dbCmd, "@RECEIPT_DATE", DbType.String, payment[11]);
                db.AddInParameter(dbCmd, "@RECEIVED_BY", DbType.String, payment[12]);
                db.AddInParameter(dbCmd, "@SERVICE_CHARGE", DbType.Decimal, Convert.ToDecimal(payment[13]));
                db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, 1);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Convert.ToInt32(payment[15]));
                db.AddInParameter(dbCmd, "@STATUS_NAME", DbType.String, payment[17]);
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

        public DataSet getdocumentDetail(int Docid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("GET_DOCUMENT_DETAIL_FOR_BOOKING");

            db.AddInParameter(dbCmd, "@FA_SR_NO", DbType.Int32, Docid);
            ds = db.ExecuteDataSet(dbCmd);
            return ds;
        }

        public void insertdocument(int Docid,string photoid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_DOCUMENT_DETAIL_FOR_BOOKING");
                db.AddInParameter(dbCmd, "@FA_SR_NO", DbType.Int32, Docid);
                db.AddInParameter(dbCmd, "@TT_COPY_ATTACHMENT", DbType.String, photoid);
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

        public void InsertNewDetail(string TOUR_ID,string recivedby)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                //INSERT_NEW_RESIDENCE_ADD_ANOTHER
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_FOREIGN_AGENT_ADD_ANOTHER");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Convert.ToInt32(TOUR_ID));
                db.AddInParameter(dbCmd, "@RECIVEDBY", DbType.String, recivedby);                
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
