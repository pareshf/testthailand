
#region importsassemblies
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;  

#endregion 

namespace CRM.DataAccess.AdministratorEntity
{
    public class NewBookingInformation
    {
        public void InsertUpdateaddressDetail(ArrayList Address)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CUST_AND_CONTACT_DETAIL");

                //if (!Address[4].ToString().Equals("0") && !Address[5].ToString().Equals("0") && !Address[8].ToString().Equals("0") && !Address[9].ToString().Equals("0") && !Address[10].ToString().Equals("0") && !Address[11].ToString().Equals("0")
                //    && !Address[21].ToString().Equals("0") && !Address[22].ToString().Equals("0") && !Address[25].ToString().Equals("0") && !Address[26].ToString().Equals("0") && !Address[27].ToString().Equals("0") && !Address[28].ToString().Equals("0")
                //    && !Address[20].ToString().Equals("0"))
                //{
                //    if ((!Address[7].ToString().Equals("0") || !Address[6].ToString().Equals("0")) && (!Address[23].ToString().Equals("0") || !Address[24].ToString().Equals("0")))
                //    {
                        db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, Convert.ToInt32(Address[0]));
                        db.AddInParameter(dbCmd, "@CUST_UNQ_ID", DbType.String, Address[29]);
                        db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, Address[30]);
                        db.AddInParameter(dbCmd, "@CUST_TITLE", DbType.String, Address[1]);
                        db.AddInParameter(dbCmd, "@CUST_NAME", DbType.String, Address[2]);
                        db.AddInParameter(dbCmd, "@CUST_SURNAME", DbType.String, Address[3]);
                        db.AddInParameter(dbCmd, "@RES_ADD_LINE1", DbType.String, Address[4]);
                        db.AddInParameter(dbCmd, "@RES_ADD_LINE2", DbType.String, Address[5]);
                        db.AddInParameter(dbCmd, "@RES_COUNTRY", DbType.String, Address[8]);
                        db.AddInParameter(dbCmd, "@RES_STATE", DbType.String, Address[9]);
                        db.AddInParameter(dbCmd, "@RES_CITY", DbType.String, Address[10]);
                        db.AddInParameter(dbCmd, "@RES_PINCODE", DbType.String, Address[11]);
                        db.AddInParameter(dbCmd, "@RES_PHONE", DbType.String, Address[7]);
                        db.AddInParameter(dbCmd, "@RES_MOBILE ", DbType.String, Address[6]);
                        ////db.AddInParameter(dbCmd, "@OFFICE_ADD_LINE1", DbType.String, Address[12]);
                        //db.AddInParameter(dbCmd, "@OFFICE_ADD_LINE2", DbType.String, Address[13]);
                        //db.AddInParameter(dbCmd, "@OFFICE_COUNTRY", DbType.String, Address[16]);
                        //db.AddInParameter(dbCmd, "@OFFICE_STATE", DbType.String, Address[17]);
                        //db.AddInParameter(dbCmd, "@OFFICE_CITY", DbType.String, Address[18]);
                        //db.AddInParameter(dbCmd, "@OFFICE_PINCODE", DbType.String, Address[19]);
                        //db.AddInParameter(dbCmd, "@OFFICE_PHONE", DbType.String, Address[15]);
                        //db.AddInParameter(dbCmd, "@OFFICE_MOBILE", DbType.String, Address[14]);
                        db.AddInParameter(dbCmd, "@EMMERGENCY_ADD_LINE1", DbType.String, Address[21]);
                        db.AddInParameter(dbCmd, "@EMMERGENCY_ADD_LINE2", DbType.String, Address[22]);
                        db.AddInParameter(dbCmd, "@EMMERGENCY_COUNTRY", DbType.String, Address[25]);
                        db.AddInParameter(dbCmd, "@EMMERGENCY_STATE", DbType.String, Address[26]);
                        db.AddInParameter(dbCmd, "@EMMERGENCY_CITY", DbType.String, Address[27]);
                        db.AddInParameter(dbCmd, "@EMMERGENCY_PINCODE", DbType.String, Address[28]);
                        db.AddInParameter(dbCmd, "@EMMERGENCY_PHONE", DbType.String, Address[24]);
                        db.AddInParameter(dbCmd, "@EMMERGENCY_MOBILE", DbType.String, Address[23]);
                        db.AddInParameter(dbCmd, "@EMMERGENCY_NAME", DbType.String, Address[20]);
                        db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32,Convert.ToInt32( Address[31]));
                        db.AddInParameter(dbCmd, "@EMER_SR_NO", DbType.Int32, Convert.ToInt32(Address[32]));

                        db.ExecuteNonQuery(dbCmd);
                    //}
                    //else
                    //{ 
                        
                    //}
               // }
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

        public DataTable getBookingCode(int user_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DbDataReader dr=null;
            DataTable dt=new DataTable();
            try {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_BOOKING_CODE_FROM_TOUR_BOOKING_INFORMATION_MASTER");
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32,user_id);
                dr =(DbDataReader)db.ExecuteReader(dbCmd);
                dt.Load(dr);
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
            return dt;
        }

        public void InsertUpdatevisadetail(ArrayList Visa)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_VISA_DETAILS_FOR_BOOKING");

                db.AddInParameter(dbCmd, "@CUST_ID", DbType.String, Visa[0]);
                //db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, Visa[4]);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, Visa[3]);
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.String, Visa[1]);
                db.AddInParameter(dbCmd, "@VISAEXPIRYDATE", DbType.DateTime, Visa[2]);
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
        public void InsertUpdateRelationDetail(ArrayList Relation)
        {

            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                if (Convert.ToBoolean(Relation[1]) == true)
                {
                    db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                    dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_TOUR_BOOKING_INFORMATION_DETAIL");

                    db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32,Convert.ToInt32(Relation[29]));
                    db.AddInParameter(dbCmd, "@BOOKING_DETAIL_ID", DbType.Int32, Relation[30]);
                    db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, Relation[32]);
                    db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, Relation[33]);
                    db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, Relation[0]);
                    db.AddInParameter(dbCmd, "@VALID_VISA", DbType.String, Relation[18]);
                    db.AddInParameter(dbCmd, "@VALID_PASSPORT", DbType.String, Relation[19]);
                    db.AddInParameter(dbCmd, "@MEAL", DbType.String, Relation[10]);
                    db.AddInParameter(dbCmd, "@NATIONALITY", DbType.String, Relation[9]);
                    db.AddInParameter(dbCmd, "@CUST_UNQ_ID", DbType.String, Relation[31]);
                    db.AddInParameter(dbCmd, "@CUST_REL_TITLE", DbType.String, Relation[2]);
                    db.AddInParameter(dbCmd, "@CUST_REL_NAME", DbType.String, Relation[3]);
                    db.AddInParameter(dbCmd, "@CUST_REL_SURNAME", DbType.String, Relation[4]);
                    if (Relation[6].ToString().Equals("0"))
                    {
                        db.AddInParameter(dbCmd, "@CUST_BIRTH_DATE", DbType.DateTime, DBNull.Value);
                    }
                    else
                    {
                        db.AddInParameter(dbCmd, "@CUST_BIRTH_DATE", DbType.DateTime, Relation[6]);
                    }
                    db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, Relation[7]);
                    db.AddInParameter(dbCmd, "@CUST_REL_PHONE", DbType.String, Relation[8]);
                    db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.String, Relation[11]);
                    db.AddInParameter(dbCmd, "@CLASS_ID", DbType.String, Relation[28]);
                    db.AddInParameter(dbCmd, "@CATEGORY_ID", DbType.String, Relation[12]);
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_NO", DbType.String, Relation[20]);
                    if (Relation[21].ToString().Equals("0"))
                    {
                        db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_DATE", DbType.DateTime, DBNull.Value);
                    }
                    else
                    {
                        db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_DATE", DbType.DateTime, Relation[21]);
                    }
                    if (Relation[23].ToString().Equals("0"))
                    {
                        db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_EXPIRY_DATE", DbType.DateTime, DBNull.Value);
                    }
                    else
                    {
                        db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_EXPIRY_DATE", DbType.DateTime, Relation[23]);
                    }
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_PLACE ", DbType.String, Relation[22]);
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_COUNTRY", DbType.String, Relation[25]);
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_PRINTED_NAME ", DbType.String, Relation[24]);
                    db.AddInParameter(dbCmd, "@SHARE_ROOM_IN_HOTEL ", DbType.String, Relation[13]);
                    db.AddInParameter(dbCmd, "@SHARE_ROOM_IN_CRUISE", DbType.String, Relation[14]);
                    if (Relation[16].ToString().Equals("0"))
                    {
                        db.AddInParameter(dbCmd, "@DEPARUTRE_DATE", DbType.DateTime, DBNull.Value);
                    }
                    else
                    {
                        db.AddInParameter(dbCmd, "@DEPARUTRE_DATE", DbType.DateTime, Relation[16]);
                    }
                    if (Relation[17].ToString().Equals("0"))
                    {
                        db.AddInParameter(dbCmd, "@ARRIVAL_DATE", DbType.DateTime, DBNull.Value);
                    }
                    else
                    {
                        db.AddInParameter(dbCmd, "@ARRIVAL_DATE ", DbType.DateTime, Relation[17]);
                    }
                    db.AddInParameter(dbCmd, "@BORDING_FROM", DbType.String, Relation[27]);
                    db.AddInParameter(dbCmd, "@ARRIVAL_TO", DbType.String, Relation[15]);
                    db.AddInParameter(dbCmd, "@BOOKING_STATUS_ID", DbType.String, Relation[26]);
                    db.AddInParameter(dbCmd, "@RELATION", DbType.String, Relation[5]);
                    db.ExecuteNonQuery(dbCmd);
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
        public void InsertNewVisa(int CUST_ID,int CUST_REL_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_VISA_DETAIL");
                 db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, CUST_ID);
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32,CUST_REL_ID);
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
        public void InsertUpdateBookingInfo(ArrayList INFO)
        {
            Database db = null;
            DbCommand dbCmd = null;
            Database db1 = null;
            DbCommand dbCmd1 = null;
  
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_TOUR_BOOKING_INFORMATION_MASTER");
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32,Convert.ToInt32(INFO[0]));
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.String, INFO[1]);
                if (INFO[2].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@BOOKING_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@BOOKING_DATE", DbType.DateTime, INFO[2]);
                }
                db.AddInParameter(dbCmd, "@BOOKING_TAKEN_BY_ID", DbType.String,INFO[3]);
                db.AddInParameter(dbCmd, "@BRANCH_ID ", DbType.String,INFO[4]);
                db.AddInParameter(dbCmd, "@TOTAL_ACTUAL_TOUR_COST_C1", DbType.Decimal,Convert.ToDecimal(INFO[5]));
                db.AddInParameter(dbCmd, "@TOTAL_ACTUAL_TOUR_C2", DbType.Decimal, Convert.ToDecimal(INFO[6]));
                db.AddInParameter(dbCmd, "@BALANCE_TO_BE_PAID_C1", DbType.Decimal, Convert.ToDecimal(INFO[7]));
                db.AddInParameter(dbCmd, "@BALANCE_TO_BE_PAID_C2", DbType.Decimal, Convert.ToDecimal(INFO[8]));
                db.AddInParameter(dbCmd, "@BOOKING_STATUS_ID", DbType.String,INFO[9]);
                db.AddInParameter(dbCmd, "@AGENT_ID", DbType.String,INFO[10]);
                db.AddInParameter(dbCmd, "@BOOKING_CODE", DbType.String,INFO[11]);
                if (INFO[12].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@DOCS_FOR_VISA_HANDED_OVER_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DOCS_FOR_VISA_HANDED_OVER_DATE", DbType.DateTime, Convert.ToDateTime(INFO[12]));
                }
                if (INFO[13].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_BAL_TOUR_MADE_BY_DATE ", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_BAL_TOUR_MADE_BY_DATE ", DbType.DateTime, Convert.ToDateTime(INFO[13]));
                }
                db.AddInParameter(dbCmd, "@ADD_REQ_SERVICE ", DbType.String,INFO[14]);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32,INFO[15]);
                db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32,Convert.ToInt32(INFO[16]));
                db.AddInParameter(dbCmd, "@TOUR_CODE", DbType.String, INFO[17]);
               
                db.ExecuteNonQuery(dbCmd);

                db1 = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd1 = db.GetStoredProcCommand("INSERT_UPDATE_BOOKING_INQ_QOUTED_TOURS");
                db1.AddInParameter(dbCmd1, "@INQUIRY_ID", DbType.Int32, Convert.ToInt32(INFO[0]));
                db1.AddInParameter(dbCmd1, "@TOUR_SHORT_NAME", DbType.String, INFO[1]);
                db1.AddInParameter(dbCmd1, "@TOUR_CODE", DbType.String, INFO[17]);

                db1.ExecuteNonQuery(dbCmd1);
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
        public void InsertUpdatePayment(ArrayList Payment)
        {

             Database db = null;
             DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_BOOKING_TOUR_BOOKING_PAYMENT_DETAILS");
                db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, Payment[0]);
                db.AddInParameter(dbCmd, "@RECEIPT_NO", DbType.Int32, Payment[1]);
                if (Payment[2].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@RECEIPT_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@RECEIPT_DATE", DbType.DateTime, Payment[2]);
                }
                if (Payment[3].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime,Payment[3]);
                }
                db.AddInParameter(dbCmd, "@PAYMENT_MODE_ID", DbType.String, Payment[4]);
                db.AddInParameter(dbCmd, "@REC_CHEQUE_DD_NO", DbType.Int32, Payment[5]);
                db.AddInParameter(dbCmd, "@TOKEN_AMOUNT", DbType.Decimal,Convert.ToDecimal(Payment[6]));
                db.AddInParameter(dbCmd, "@BANK_ID", DbType.String,Payment[7]);
                db.AddInParameter(dbCmd, "@PAYMENT_SRNO", DbType.Int32, Payment[8]);
                db.ExecuteNonQuery(dbCmd);
            }
            catch(Exception ex)
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
