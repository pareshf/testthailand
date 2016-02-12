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
    public class CustomerPaymentStoredProcedure
    {
        public void InsertUpdateBookingPaymentDetail(ArrayList Payment)
        {
            Database dbp = null;
            DbCommand dbCmdp = null;
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                dbp = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmdp = dbp.GetStoredProcCommand("INSERT_UPDATE_BOOKING_TOUR_BOOKING_PAYMENT_DETAIL");
                
                if (Payment[0].ToString().Equals("0"))
                {

                    dbp.AddInParameter(dbCmdp, "@PAYMENT_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    dbp.AddInParameter(dbCmdp, "@PAYMENT_DATE", DbType.DateTime, DateTime.ParseExact(Payment[0].ToString(), "dd/MM/yyyy", null));
                }
                dbp.AddInParameter(dbCmdp, "@PAYMENT_CURRENCY_CODE", DbType.String, Payment[1]);
                dbp.AddInParameter(dbCmdp, "@PAYMENT_MODE_ID", DbType.String, Payment[2]);
                dbp.AddInParameter(dbCmdp, "@REC_CHEQUE_DD_NO", DbType.String, Payment[3]);
                dbp.AddInParameter(dbCmdp, "@BANK_ID", DbType.String, Payment[4]);
                dbp.AddInParameter(dbCmdp, "@BRANCH_NAME", DbType.String, Payment[5]);
                dbp.AddInParameter(dbCmdp, "@RECEIPT_NO", DbType.String, Payment[6]);
                if (Payment[7].ToString().Equals("0"))
                {

                    dbp.AddInParameter(dbCmdp, "@RECEIPT_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    dbp.AddInParameter(dbCmdp, "@RECEIPT_DATE", DbType.DateTime, DateTime.ParseExact(Payment[7].ToString(), "dd/MM/yyyy", null));
                }
                dbp.AddInParameter(dbCmdp, "@RECEIVED_BY", DbType.String, Payment[8]);
                dbp.AddInParameter(dbCmdp, "@CURRENCY_AGENT_ID", DbType.String, Payment[9]);
                dbp.AddInParameter(dbCmdp, "@IN_THE_NAME_OF", DbType.String, Payment[10]);
               // dbp.AddInParameter(dbCmdp, "@CONVERSION_RATE", DbType.Decimal, Payment[11]);
                dbp.AddInParameter(dbCmdp, "@PAYMENT_SRNO", DbType.Int32, Convert.ToInt32(Payment[11]));
                dbp.AddInParameter(dbCmdp, "@AMOUNT", DbType.Decimal, Convert.ToDecimal(Payment[12]));
                dbp.AddInParameter(dbCmdp, "@INR_AMOUNT", DbType.Decimal, Convert.ToDecimal(Payment[16]));
                dbp.AddInParameter(dbCmdp, "@TAX", DbType.Decimal, Convert.ToDecimal(Payment[14]));
                dbp.AddInParameter(dbCmdp, "@GST", DbType.Decimal, Convert.ToDecimal(Payment[15]));
                dbp.AddInParameter(dbCmdp, "@BILL_NUMBER", DbType.String,(Payment[13]));
                dbp.AddInParameter(dbCmdp, "@TOKENAMT", DbType.Decimal, Convert.ToDecimal(Payment[17]));
                dbp.AddInParameter(dbCmdp, "@USER", DbType.Int32, Convert.ToInt32(Payment[18]));
                dbp.AddInParameter(dbCmdp, "@BOOKING_ID", DbType.Int32, Convert.ToInt32(Payment[21]));
                dbp.AddInParameter(dbCmdp, "@STATUS_NAME", DbType.String, Payment[22]);
                dbp.ExecuteNonQuery(dbCmdp);


                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_CUSTOMER_PAYMENT_FROM_BOOKING_MASTER");
                db.AddInParameter(dbCmd, "@PAYMENT_CURRENCY_CODE", DbType.String, Payment[1]);
                db.AddInParameter(dbCmd, "@CURRENCY_AGENT_ID", DbType.String, Payment[9]);
                db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, Convert.ToDecimal(Payment[12]));
                db.AddInParameter(dbCmd, "@INR_AMOUNT", DbType.Decimal, Convert.ToDecimal(Payment[16]));
                db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, Convert.ToInt32(Payment[21]));
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
                DALHelper.Destroy(ref dbCmdp);
                DALHelper.Destroy(ref dbCmd);
            }


        }
        public void delPayment(int Payment)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_BOOKING_CUSTOMER_PAYMENT");
                db.AddInParameter(dbCmd, "@PAYMENTSR", DbType.Int32,Payment);
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
        public void delCustomer(int delcustomer)        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_CUSTOMER_FROM_BOOKING_PAYMENT");
                db.AddInParameter(dbCmd, "@BOOKINGID", DbType.Int32, delcustomer);
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
        public void InsertNewpayment(string paymentid ,string recivedby,string pdate,string rdate)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_FOR_NEW_PAYMENT_DETAIL_ADD_ANOTHER");
                db.AddInParameter(dbCmd, "@BOOKINGID", DbType.Int32, paymentid);
                db.AddInParameter(dbCmd, "@RECIVEDBY", DbType.String, recivedby);
                db.AddInParameter(dbCmd, "@PAYMENTDATE", DbType.DateTime,DateTime.ParseExact(pdate.ToString(), "dd/MM/yyyy", null));
                db.AddInParameter(dbCmd, "@RECIVEDDATE", DbType.DateTime, DateTime.ParseExact(rdate.ToString(), "dd/MM/yyyy", null));
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
