using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;  


namespace CRM.DataAccess.AdministratorEntity
{
    public class BookingFormStep2
    {
        public void InsertUpdateBookingInfoMaster(ArrayList Booking)
        {
            Database db = null;
            DbCommand dbCmd = null;

            Database db1 = null;
            DbCommand dbCmd1 = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_TOUR_BOOKING_INFORMATION_MASTER"); 
                db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, Booking[0]);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Booking[1]);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, Booking[2]);
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, Booking[3]);
                db.AddInParameter(dbCmd, "@BOOKING_CODE", DbType.String, Booking[4]);
                if (Booking[5].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@BOOKING_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@BOOKING_DATE", DbType.DateTime, DateTime.ParseExact(Booking[5].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@BOOKING_TAKEN_BY_ID", DbType.String, Booking[6]);
                db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, 1);
                db.AddInParameter(dbCmd, "@AGENT_ID", DbType.String, Booking[8]);
                db.AddInParameter(dbCmd, "@ADD_REQ_SERVICE", DbType.String, Booking[9]);
                if (Booking[10].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@DOCS_FOR_VISA_HANDED_OVER_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DOCS_FOR_VISA_HANDED_OVER_DATE", DbType.DateTime, DateTime.ParseExact(Booking[10].ToString(), "dd/MM/yyyy", null));
                }
                if (Booking[11].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@PAYMENT_BAL_TOUR_MADE_BY_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_BAL_TOUR_MADE_BY_DATE", DbType.DateTime, DateTime.ParseExact(Booking[11].ToString(), "dd/MM/yyyy", null));
                }

                //db.AddInParameter(dbCmd, "@TOTAL_ACTUAL_TOUR_COST_C1", DbType.Decimal, Booking[12]);
                //db.AddInParameter(dbCmd, "@TOTAL_ACTUAL_TOUR_C2", DbType.Decimal, Booking[13]);
                //db.AddInParameter(dbCmd, "@BALANCE_TO_BE_PAID_C1", DbType.Decimal, Booking[14]);
                //db.AddInParameter(dbCmd, "@BALANCE_TO_BE_PAID_C2", DbType.Decimal, Booking[15]);
                db.AddInParameter(dbCmd, "@BOOKING_STATUS_ID", DbType.String, Booking[16]);
                db.ExecuteNonQuery(dbCmd);

                if (Booking[0].ToString().Equals("0"))
                {

                    db1 = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                    dbCmd1 = db.GetStoredProcCommand("INSERT_UPDATE_BOOKING_INQ_QOUTED_TOURS");
                    db1.AddInParameter(dbCmd1, "@TOUR_ID", DbType.Int32, Booking[1]);
                    db1.AddInParameter(dbCmd1, "@INQUIRY_ID", DbType.Int32, Booking[3]);
                    db1.ExecuteNonQuery(dbCmd1);
                }

                
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

        public void Insertingnulvalues(ArrayList Detail)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try 
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_BOOKING_DETAIL_WITH_CHECKED");
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, Detail[0]);
                db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, Detail[1]);
                db.AddInParameter(dbCmd, "@IS_CHECKED", DbType.String, Detail[2]);
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

        public void InsertUpdateBookinginfoDetail(ArrayList Details)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try 
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_TOUR_BOOKING_INFORMATION_DETAIL");
                db.AddInParameter(dbCmd, "@BOOKING_DETAIL_ID",DbType.Int32, Details[0]);
                db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, Details[1]);
                db.AddInParameter(dbCmd, "@VALID_VISA", DbType.String,  Details[3]);
                db.AddInParameter(dbCmd, "@VALID_PASSPORT", DbType.String, Details[4]);
                db.AddInParameter(dbCmd, "@MEAL", DbType.String, Details[12]);
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.String, Details[5]);
                db.AddInParameter(dbCmd, "@ROOM_NO", DbType.Int32, Details[17]);               
                db.AddInParameter(dbCmd, "@CLASS_ID", DbType.String, Details[6]);
                db.AddInParameter(dbCmd, "@CATEGORY_ID", DbType.String, Details[7]);
                db.AddInParameter(dbCmd, "@SHARE_ROOM_IN_HOTEL ", DbType.String, Details[13]);
                db.AddInParameter(dbCmd, "@SHARE_ROOM_IN_CRUISE", DbType.String, Details[14]);
                if (Details[10].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@DEPARUTRE_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DEPARUTRE_DATE", DbType.DateTime, DateTime.ParseExact(Details[10].ToString(), "dd/MM/yyyy", null));
                }
                
                if (Details[11].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@ARRIVAL_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ARRIVAL_DATE", DbType.DateTime, DateTime.ParseExact(Details[11].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@BORDING_FROM", DbType.String, Details[8]);
                db.AddInParameter(dbCmd, "@ARRIVAL_TO", DbType.String, Details[9]);
                db.AddInParameter(dbCmd, "@BOOKING_STATUS_ID", DbType.String, Details[15]);
                db.AddInParameter(dbCmd, "@RELATION", DbType.String, Details[2]);
                if (Details[16].ToString().Equals("T"))
                {
                    db.AddInParameter(dbCmd, "@IS_CHECKED", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@IS_CHECKED", DbType.Boolean, false);
                }


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
        public void InsertupdateCostInfo(ArrayList Cost)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_COST_INFORMATION");
                db.AddInParameter(dbCmd, "@RELATION", DbType.String, Cost[0]);
                db.AddInParameter(dbCmd, "@FAMILY_COST", DbType.Int32, Cost[1]);
                db.AddInParameter(dbCmd, "@INR_FOR_FLAMINGO ", DbType.Decimal, Convert.ToDecimal(Cost[2]));
                db.AddInParameter(dbCmd, "@INR_FOR_FOREX", DbType.Decimal,Convert.ToDecimal(Cost[3]));
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, Cost[4]);
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



        public void InsertUpdateBookingPaymentDetail(ArrayList Payment)
        {
            Database dbp = null;
            DbCommand dbCmdp = null;
            Database db = null;
            DbCommand dbCmd = null;
            try 
            {
                dbp = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmdp = dbp.GetStoredProcCommand("INSERT_UPDATE_BOOKING_TOUR_BOOKING_PAYMENT_DETAILS");
                
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
                dbp.AddInParameter(dbCmdp, "@TOKEN_AMOUNT", DbType.Decimal,Convert.ToDecimal(Payment[4]));
                dbp.AddInParameter(dbCmdp, "@BANK_ID", DbType.String, Payment[5]);
                dbp.AddInParameter(dbCmdp, "@BRANCH_NAME", DbType.String, Payment[6]);
                dbp.AddInParameter(dbCmdp, "@RECEIPT_NO", DbType.String, Payment[7]);
                if (Payment[8].ToString().Equals("0"))
                {

                    dbp.AddInParameter(dbCmdp, "@RECEIPT_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    dbp.AddInParameter(dbCmdp, "@RECEIPT_DATE", DbType.DateTime, DateTime.ParseExact(Payment[8].ToString(), "dd/MM/yyyy", null));
                }
                dbp.AddInParameter(dbCmdp, "@RECEIVED_BY", DbType.String, Payment[9]);
                dbp.AddInParameter(dbCmdp, "@CURRENCY_AGENT_ID", DbType.String, Payment[10]);
                dbp.AddInParameter(dbCmdp, "@IN_THE_NAME_OF", DbType.String, Payment[11]);
                dbp.AddInParameter(dbCmdp, "@CONVERSION_RATE", DbType.Decimal, Payment[12]);
                dbp.AddInParameter(dbCmdp, "@PAYMENT_SRNO", DbType.Int32, Convert.ToInt32(Payment[13]));
                dbp.AddInParameter(dbCmdp, "@BOOKING_ID", DbType.Int32, Convert.ToInt32(Payment[14]));
                dbp.ExecuteNonQuery(dbCmdp);

               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_AMOUNT_IN_BOOKING_MASTER_TABLE");
               db.AddInParameter(dbCmd, "@PAYMENT_CURRENCY_CODE", DbType.String, Payment[1]);
               db.AddInParameter(dbCmd, "@TOKEN_AMOUNT", DbType.Decimal, Convert.ToDecimal(Payment[4]));
               db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, Convert.ToInt32(Payment[14]));
               db.AddInParameter(dbCmd, "@CURRENCY_AGENT_ID", DbType.String, Payment[10]);
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


        public DataTable getBookingId(int inq_id, int cust_id, string tour_snm, string tour_code)
        {
            DataSet ds = null;
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_EXISTING_BOOKING_ID");
                db.AddInParameter(dbCmd, "@INQ_ID", DbType.Int32,inq_id);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, cust_id);
                db.AddInParameter(dbCmd, "@TOUR_SHORT_NAME", DbType.String, tour_snm);
                db.AddInParameter(dbCmd, "@TOUR_CODE", DbType.String, tour_code);

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
            return ds.Tables[0];
        }

        public DataTable getTourType(string tour_snm,string tour_code)
        { 
            DataSet ds = null;
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_TOUR_TYPE_FROM_TOUR_MASTER");
                db.AddInParameter(dbCmd, "@TOUR_NAME", DbType.String, tour_snm);
                db.AddInParameter(dbCmd, "@TOUR_CODE", DbType.String, tour_code);

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
            return ds.Tables[0];
        }

        //public DataTable getRelationname(int custid)
        //{
        //    Database db = null;
        //    DbCommand dbCmd = null;
        //    DbDataReader dr = null;
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
        //        dbCmd = db.GetStoredProcCommand("FETCH_DATA_FOR_CUSTOMER_REL_NAME");
        //        db.AddInParameter(dbCmd, "@QUERY", DbType.Int32, custid);
        //        dr = (DbDataReader)db.ExecuteReader(dbCmd);
        //        dt.Load(dr);
        //    }
        //    catch (Exception ex)
        //    {
        //        bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
        //        if (rethrow)
        //        {
        //            throw ex;

        //        }
        //    }
        //    finally
        //    {
        //        DALHelper.Destroy(ref dbCmd);
        //    }
        //    return dt;

        //}

        

    }
}
