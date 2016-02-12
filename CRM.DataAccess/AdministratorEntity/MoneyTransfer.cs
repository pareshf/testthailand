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
    public class MoneyTransfer
    {
        public void InsertUpdateBookingPaymentDetails(ArrayList paymentary)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_MONEY_TRANSFER_AGENT");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32,Convert.ToInt32(paymentary[15]));
                db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal,Convert.ToDecimal(paymentary[4]));
                db.AddInParameter(dbCmd, "@CONVERSION_RATE", DbType.Decimal,Convert.ToDecimal(paymentary[12]));
                db.AddInParameter(dbCmd, "@REC_CHEQUE_DD_NO", DbType.String, paymentary[3]);
                db.AddInParameter(dbCmd, "@BANK_NAME", DbType.String, paymentary[5]);
                db.AddInParameter(dbCmd, "@BRANCH_NAME", DbType.String, paymentary[6]);
                db.AddInParameter(dbCmd, "@RECEIPT_NO", DbType.String, paymentary[7]);
                if (paymentary[8].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@RECEIPT_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@RECEIPT_DATE", DbType.DateTime, DateTime.ParseExact(paymentary[8].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@RECEIVED_BY", DbType.String, paymentary[9]);
                db.AddInParameter(dbCmd, "@PAYMENT_MODE_NAME", DbType.String, paymentary[2]);
                db.AddInParameter(dbCmd, "@PAYMENT_CURR_CODE", DbType.String, paymentary[1]);
                db.AddInParameter(dbCmd, "@SERVICE_CHARGE", DbType.Decimal,Convert.ToDecimal(paymentary[16]));
                db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32,Convert.ToInt32(1));
                db.AddInParameter(dbCmd, "@INR_AMOUNT", DbType.Decimal,Convert.ToDecimal(paymentary[18]));
                db.AddInParameter(dbCmd, "@PAYMENT_SR_NO", DbType.Int32, Convert.ToInt32(paymentary[13]));
                db.AddInParameter(dbCmd, "@FAM", DbType.String, paymentary[19]);
                db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, Convert.ToInt32(paymentary[14]));
                db.AddInParameter(dbCmd, "@MODIFIED_BY", DbType.Int32, Convert.ToInt32(paymentary[20]));
                db.AddInParameter(dbCmd, "@STATUS_NAME", DbType.String, paymentary[23]);
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
