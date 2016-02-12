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
    public class BookingFormStep1
    {
        public void UpdateCustomerDetail(ArrayList Customer)
        { 
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CUST_AND_CONTACT_DETAIL");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32,Convert.ToInt32(Customer[0]));
                db.AddInParameter(dbCmd, "@CUST_UNQ_ID", DbType.String, Customer[1]);
                db.AddInParameter(dbCmd, "@CUST_TITLE", DbType.String, Customer[2]);
                db.AddInParameter(dbCmd, "@CUST_NAME", DbType.String, Customer[3]);
                db.AddInParameter(dbCmd, "@CUST_SURNAME", DbType.String, Customer[4]);
                db.AddInParameter(dbCmd, "@RES_ADD_LINE1", DbType.String, Customer[5]);
                db.AddInParameter(dbCmd, "@RES_ADD_LINE2", DbType.String, Customer[6]);
                db.AddInParameter(dbCmd, "@RES_COUNTRY", DbType.String, Customer[7]);
                db.AddInParameter(dbCmd, "@RES_STATE", DbType.String, Customer[8]);
                db.AddInParameter(dbCmd, "@RES_CITY", DbType.String, Customer[9]);
                db.AddInParameter(dbCmd, "@RES_PINCODE", DbType.String, Customer[10]);
                db.AddInParameter(dbCmd, "@RES_PHONE", DbType.String, Customer[11]);
                db.AddInParameter(dbCmd, "@RES_MOBILE", DbType.String, Customer[12]);
                db.AddInParameter(dbCmd, "@EMMERGENCY_ADD_LINE1", DbType.String, Customer[13]);
                db.AddInParameter(dbCmd, "@EMMERGENCY_ADD_LINE2", DbType.String, Customer[14]);
                db.AddInParameter(dbCmd, "@EMMERGENCY_COUNTRY", DbType.String, Customer[15]);
                db.AddInParameter(dbCmd, "@EMMERGENCY_STATE", DbType.String, Customer[16]);
                db.AddInParameter(dbCmd, "@EMMERGENCY_CITY", DbType.String, Customer[17]);
                db.AddInParameter(dbCmd, "@EMMERGENCY_PINCODE", DbType.String, Customer[18]);
                db.AddInParameter(dbCmd, "@EMMERGENCY_PHONE", DbType.String, Customer[19]);
                db.AddInParameter(dbCmd, "@EMMERGENCY_MOBILE", DbType.String, Customer[20]);
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32,1);
                db.AddInParameter(dbCmd, "@EMMERGENCY_NAME", DbType.String, Customer[21]);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32,Convert.ToInt32(Customer[22]));
                db.AddInParameter(dbCmd, "@EMER_SR_NO", DbType.Int32,Convert.ToInt32(Customer[23]));
               // db.AddInParameter(dbCmd, "@CUST_REL_EMAIL", DbType.String);
               // db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32);

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


        public void UpdateRelationDetail(ArrayList Relation) 
        { 
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_CUSTOMER_RELATION_DETAIL");
                db.AddInParameter(dbCmd, "@CUST_REL_TITLE", DbType.String,Relation[0]);
                db.AddInParameter(dbCmd, "@CUST_REL_NAME", DbType.String, Relation[1]);
                db.AddInParameter(dbCmd, "@CUST_REL_SURNAME", DbType.String, Relation[2]);
                if (Relation[3].ToString().Equals("0")) {
                    db.AddInParameter(dbCmd, "@CUST_BIRTH_DATE", DbType.DateTime,DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CUST_BIRTH_DATE", DbType.DateTime, DateTime.ParseExact(Relation[3].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, Relation[4]);
                db.AddInParameter(dbCmd, "@CUST_REL_PHONE", DbType.String, Relation[5]);
                db.AddInParameter(dbCmd, "@NATIONALITY", DbType.String, Relation[6]);
                db.AddInParameter(dbCmd, "@MEAL", DbType.String, Relation[7]);
                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_NO", DbType.String, Relation[8]);
                if (Relation[9].ToString().Equals("0")) {
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_DATE", DbType.DateTime,DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_DATE", DbType.DateTime, DateTime.ParseExact(Relation[9].ToString(), "dd/MM/yyyy", null));
                }
                if (Relation[10].ToString().Equals("0")) {
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_EXPIRY_DATE", DbType.DateTime,DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_EXPIRY_DATE", DbType.DateTime, DateTime.ParseExact(Relation[10].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_PLACE", DbType.String, Relation[11]);
                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_COUNTRY", DbType.String, Relation[12]);
                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_PRINTED_NAME", DbType.String, Relation[13]);
                db.AddInParameter(dbCmd, "@EMAIL", DbType.String, Relation[14]);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, Convert.ToInt32(Relation[15]));
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, Convert.ToInt32(Relation[16]));
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, Convert.ToInt32(Relation[17]));
                db.AddInParameter(dbCmd, "@RELATION", DbType.String, Relation[18]);
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

        public void InsertUpdateVisaDetail(ArrayList Visa) 
        { 
             Database db = null;
             DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_VISA_DETAILS_FOR_BOOKING");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32,Convert.ToInt32(Visa[0]));
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32,Convert.ToInt32(Visa[1]));
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.String,Visa[2]);
                if (Visa[3].ToString().Equals("0")) 
                {
                    db.AddInParameter(dbCmd, "@VISAEXPIRYDATE", DbType.DateTime,DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@VISAEXPIRYDATE", DbType.DateTime,Convert.ToDateTime(Visa[3]));
                }
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32,Convert.ToInt32(Visa[4]));

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

        public void NewvisaforCustomer(string cust_id,string cust_rel_id,string cust_rel_srno)
        {

            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_RECORD_IN_CUSTOMER_VISA_DETAILS");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, Convert.ToInt32(cust_id));
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, Convert.ToInt32(cust_rel_id));
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, Convert.ToInt32(cust_rel_srno));
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
