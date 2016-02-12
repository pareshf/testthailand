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
    public class CustomerMasterStoredProcedure
    {
        public DataSet InsertUpdateCust(ArrayList Cust)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CUST_CUSTOMER_MASTER");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.String, Cust[0]);
               // db.AddInParameter(dbCmd, "@CUST_UNQ_ID", DbType.String, Cust[1]);
                db.AddInParameter(dbCmd, "@CUST_SURNAME", DbType.String, Cust[3]);
                db.AddInParameter(dbCmd, "@TITLE_DESC", DbType.String, Cust[2]);
                db.AddInParameter(dbCmd, "@CUST_NAME", DbType.String, Cust[4]);
                db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, Cust[5]);
                db.AddInParameter(dbCmd, "@CUST_REL_EMAIL", DbType.String, Cust[6]);
                db.AddInParameter(dbCmd, "@CUST_TYPE_NAME", DbType.String, Cust[7]);
             
                db.AddInParameter(dbCmd, "@COMMUNICATION_MODE_NAME", DbType.String, Cust[8]);

                db.AddInParameter(dbCmd, "@COMMUNICATION_TIME", DbType.String, Cust[9]);
                db.AddInParameter(dbCmd, "@CUST_REL_PHONE", DbType.String, Cust[10]);
               // db.AddInParameter(dbCmd, "@BRANCH", DbType.String,Cust[11]);
                db.AddInParameter(dbCmd, "@EMPLOYEE", DbType.String, Cust[12]);
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, 1);
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32,Convert.ToInt32(Cust[14]));
                db.AddInParameter(dbCmd, "@RELATION_DESC", DbType.String, "Self");
                db.AddInParameter(dbCmd, "@DESIGNATION", DbType.String, Cust[16]);
                db.AddInParameter(dbCmd, "@GROUP_NAME", DbType.String, Cust[17]);
                db.AddInParameter(dbCmd, "@CREDIT_LIMIT", DbType.Decimal, Convert.ToDecimal(Cust[18]));
                db.AddInParameter(dbCmd, "@ACCOUNTING_CODE", DbType.String, Cust[19]);
                db.AddInParameter(dbCmd, "@GENDER_NAME", DbType.String, Cust[20]);
                db.AddInParameter(dbCmd, "@MEAL_DESC", DbType.String, Cust[21]);
                db.AddInParameter(dbCmd, "@NATIONALITY_NAME", DbType.String, Cust[22]);
                db.AddInParameter(dbCmd, "@CUST_COMPANY_NAME", DbType.String, Cust[23]);
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, Cust[24]);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS", DbType.String, Cust[25]);
                db.AddInParameter(dbCmd, "@PASSWORD", DbType.String, Cust[26]);
                db.AddInParameter(dbCmd, "@CURRENT_CREDIT_LIMIT", DbType.Decimal, Convert.ToDecimal(Cust[27]));
                db.AddInParameter(dbCmd, "@FIT_BOOKING_AMOUNT", DbType.Int32, Convert.ToInt32(Cust[28]));

                db.AddInParameter(dbCmd, "@BANK_NAME", DbType.String, Cust[29]);
                db.AddInParameter(dbCmd, "@BRANCH_NAME", DbType.String, Cust[30]);
                db.AddInParameter(dbCmd, "@ACC_NAME", DbType.String, Cust[31]);
                db.AddInParameter(dbCmd, "@STATUS_NAME", DbType.String, Cust[32]);
                if (Cust[33].ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@BANK_CHARGES", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@BANK_CHARGES", DbType.Decimal, Convert.ToDecimal(Cust[33]));
                }
                if (Cust[34].ToString().Equals("YES") || Cust[34].ToString().Equals("Yes"))
                {
                    db.AddInParameter(dbCmd, "@BANK_CHARGE_APPLICABLE", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@BANK_CHARGE_APPLICABLE", DbType.Boolean, false);
                }
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, Convert.ToInt32(Cust[35]));
                
                ds = db.ExecuteDataSet(dbCmd);                           
                //db.ExecuteNonQuery(dbCmd);
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

        public void InsertUpdateContdet(ArrayList Contdetail)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CUST_CUSTOMER_MASTER_CONTACT_DETAIL");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.String, Contdetail[0]);
                db.AddInParameter(dbCmd, "@RELATION_DESC ", DbType.String, Contdetail[1]);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.String, Contdetail[2]);
                db.AddInParameter(dbCmd, "@ADDRESS_TYPE_NAME", DbType.String, Contdetail[3]);
                db.AddInParameter(dbCmd, "@CUST_ADDRESS_LINE1", DbType.String, Contdetail[4]);
                db.AddInParameter(dbCmd, "@CUST_ADDRESS_LINE2", DbType.String, Contdetail[5]);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, Contdetail[6]);
                db.AddInParameter(dbCmd, "@STATE_NAME", DbType.String, Contdetail[7]);
                db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, Contdetail[8]);
                db.AddInParameter(dbCmd, "@CUST_PINCODE", DbType.String, Contdetail[9]);
                db.AddInParameter(dbCmd, "@CUST_PHONE", DbType.String, Contdetail[10]);
                //db.AddInParameter(dbCmd, "@EMERGENCY_NAME", DbType.String, Contdetail[11]);
                db.AddInParameter(dbCmd, "@CUST_MOBILE", DbType.String, Contdetail[12]);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.String, Contdetail[13]);
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.String, Contdetail[14]);
                db.AddInParameter(dbCmd, "@WEBSITE", DbType.String, Contdetail[16]);
                db.AddInParameter(dbCmd, "@CHAIN_NAME", DbType.String, Contdetail[17]);
                //db.AddInParameter(dbCmd, "@VIDEO", DbType.String, Contdetail[18]);
                //db.AddInParameter(dbCmd, "@LOGO", DbType.String, Contdetail[19]);
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

        public void InsertNewDetail(string CUST_ID, string CUST_REL_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_RESIDENCE_ADD_ANOTHER");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, Convert.ToInt32(CUST_ID));
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, Convert.ToInt32('1'));
               // db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, Convert.ToInt32(CUST_ID));
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

        public void InsertNewRelation(string CUST_ID, string CUST_REL_ID,string CHAIN_SR_NO)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_RELATION_ADD_ANOTHER");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, Convert.ToInt32(CUST_ID));
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, Convert.ToInt32('1'));
                db.AddInParameter(dbCmd, "@CHAIN_SR_NO", DbType.Int32, Convert.ToInt32(CHAIN_SR_NO)); 
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
     
        public DataSet InsertUpdateRel(ArrayList Rel)
        {
            Database db = null;
            Database db1 = null;

            DbCommand dbCmd = null;
            DbCommand dbCmd1 = null;
            DataSet ds = null;

            Boolean flag;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CUST_CUSTOMER_RELATION");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.String, Rel[0]);
                db.AddInParameter(dbCmd, "@CUST_UNQ_ID", DbType.String, Rel[1]);
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.String, Rel[2]);
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.String, Rel[3]);
                db.AddInParameter(dbCmd, "@TITLE_DESC", DbType.String, Rel[4]);
                db.AddInParameter(dbCmd, "@CUST_REL_SURNAME", DbType.String, Rel[5]);
                db.AddInParameter(dbCmd, "@CUST_REL_NAME", DbType.String, Rel[6]);
                db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, Rel[7]);
                db.AddInParameter(dbCmd, "@CUST_REL_EMAIL", DbType.String, Rel[8]);               
                db.AddInParameter(dbCmd, "@CUST_REL_PHONE", DbType.String, Rel[9]);
                db.AddInParameter(dbCmd, "@RELATION_DESC", DbType.String, Rel[10]);
                db.AddInParameter(dbCmd, "@DESIGNATION", DbType.String, Rel[11]);
                db.AddInParameter(dbCmd, "@GENDER_NAME", DbType.String, Rel[12]);
                db.AddInParameter(dbCmd, "@MEAL_DESC", DbType.String, Rel[13]);
                db.AddInParameter(dbCmd, "@NATIONALITY_NAME", DbType.String, Rel[14]);
                
                db.AddInParameter(dbCmd, "@PASSWORD", DbType.String, Rel[15]);
                db.AddInParameter(dbCmd, "@ALT_EMAIL", DbType.String, Rel[16]);
               // db.AddInParameter(dbCmd, "@CREDIT_LIMIT", DbType.Decimal,Convert.ToDecimal (Rel[17]));
                db.AddInParameter(dbCmd, "@USER_STATUS", DbType.String, Rel[18]);
                //db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, Rel[19]);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.String, Rel[20]);
                db.AddInParameter(dbCmd, "@PARENT_CUSTOMER", DbType.Int32,Convert.ToInt32(Rel[22]));
                db.AddInParameter(dbCmd, "@CHAIN_NAME", DbType.Int32,Convert.ToInt32(Rel[23]));
                //if (Convert.ToString(Rel[25]).Equals("YES"))
                //{
                //    flag = true;
                //}
                //else
                //{
                //    flag = false;
                //}
                //db.AddInParameter(dbCmd, "@IS_ACCOUNT", DbType.Boolean, flag);
                //db.ExecuteNonQuery(dbCmd);
                //db1 = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                //dbCmd1 = db.GetStoredProcCommand("INSERT_UPDATE_FOR_SYS_USER_MASTER");
                //db1.AddInParameter(dbCmd1, "@CUST_REL_EMAIL", DbType.String, Rel[8]);
                //db1.AddInParameter(dbCmd1, "@PASSWORD", DbType.String, Rel[15]);
                //db1.AddInParameter(dbCmd1, "@CUST_REL_SRNO", DbType.String, Rel[3]);
                //db1.ExecuteNonQuery(dbCmd1);

                ds = db.ExecuteDataSet(dbCmd);
                //db.ExecuteNonQuery(dbCmd);
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
    
        public void deletecust(int CUST_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FOR_CUSTOMER_MASTER");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, Convert.ToInt32(CUST_ID));             
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

        public void DeleteContact(int SR_NO)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FOR_CUST_CUSTOMER_CONTACT_DETAIL");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, SR_NO);
                
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

        public void DeleteRel(int CUST_REL_SRNO)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FOR_CUST_CUSTOMER_RELATION_DETAIL");
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, CUST_REL_SRNO);

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
      
        public void insertdoc(ArrayList doc)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CUST_CUSTOMER_DOCUMENT");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.String, doc[2]);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.String, doc[3]);
                // db.AddInParameter(dbCmd, "@CUST_UNQ_ID", DbType.String, doc[5]);
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.String, doc[4]);
                db.AddInParameter(dbCmd, "@DOC_NAME", DbType.String, doc[0]);
                db.AddInParameter(dbCmd, "@DOC_DESCRIPTION", DbType.String, doc[1]);
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
        public DataSet getdocumentDetail(int Docid1)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("GET_DOCUMENT_DETAIL");
            db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, Docid1);
          //  db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, Docid);
            ds = db.ExecuteDataSet(dbCmd);
            return ds;
        }
        public void insertdocument(int photoid, string sitephoto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_DOCUMENT_DETAIL");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, photoid);
                db.AddInParameter(dbCmd, "@DOC_FILE_NAME", DbType.String, sitephoto); 
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


        public void insertnewdoc(string docid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_DOCUMENT_ADD_ANOTHER");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, docid);
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

        public DataSet AgentLogin(string userid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("get_cust_id_from_userid");
                db.AddInParameter(dbCmd, "@userid", DbType.Int32, int.Parse(userid));
                DataSet ds = db.ExecuteDataSet(dbCmd);
                return ds;          
        }
        public void InsertNewGenerateCode(string CUST_COMPANY_NAME, string CUST_ID, string Supplier)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_GL_CODE_FOR_SUPPLIER_MASTER");
                db.AddInParameter(dbCmd, "@COMPANY_NAME", DbType.String, CUST_COMPANY_NAME);
                db.AddInParameter(dbCmd, "@FLAG", DbType.Int32, Convert.ToInt32(CUST_ID));
                db.AddInParameter(dbCmd, "@FLAG1", DbType.String, 'A');
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
        public DataSet CheckValidation()
        {
            Database db = null;
            DbCommand dbCmd = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("EMAIL_VALIDATION_FOR_LOGIN");
            DataSet ds1 = db.ExecuteDataSet(dbCmd);
            return ds1;
        }
        
    }

}
