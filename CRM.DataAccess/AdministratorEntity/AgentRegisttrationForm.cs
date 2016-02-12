using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.AdministratorEntity
{
    public class AgentRegisttrationForm
    {
        #region COMBOBOX

        public DataSet fetchTitle()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_TITLE");
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetchagenttype()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_AGENT_TYPE");
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        
        public DataSet fetCompanyBank()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_COMPANY_BANK_NAME");
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        public DataSet fetchBankBranch(string param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_BRANCH_FROM_BANK_FOR_AGENT_REGISTRATION");
                db.AddInParameter(dbCmd, "@BANK", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        public DataSet fetchAccountName(string param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_ACCOUNT_NAME_FOR_AGENT_REGISTRATION");
                db.AddInParameter(dbCmd, "@BANK_BRANCH", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        public DataSet fetchSTATE()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_STATE_FOR_MASTER");
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        public DataSet fetchSTATEFROMCITY(String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_STATE_FOR_FROM_CITY");
                db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        public DataSet fetchCONTRYFROMSTATE(String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_COUNTRY_FOR_FROM_STATE");
                db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        public DataSet fetchcity()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_CITY_FOR_MASTER");
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetchcountry()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_COUNTRY_FOR_MASTER");
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetchcommunicationmode()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_COMMUNICATION_MODE_NAME_AUTOSEARCH");
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetchpaymentterms()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_ALL_PAYMENT_TERMS");
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetchaddresstype()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_ADDRESS_TYPE");
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet fetchdesignation()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_DESIGNATION_MASTER");
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        
        public DataSet fetchstatus()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_ALL_USER_STATUS_FOR_AGENT");
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet GetMaritalStatus()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_MARITAL_STATUS_MASTER");
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

        public DataSet SECURITY_QUESTION()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_SECURITY_QUESTION_FOR_NEW_REGISTRATION");
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        


        //================= sachin new form ==================//

        public DataSet InsertUpdateCustnewregistraion(String custid, String surname, String title, String name, String mobile, String email, String custtype, String mode, String phone, String designation, String company,String pwd, String TERMS,
            String addtype, String add1, String add2, String CITY, String STATE, String COUNTERY, String pincode, String website, String chainname,String USERSTATUS,bool offline)
        {
            Database db = null;
            DbCommand dbCmd = null;
            String s;
            String srno;
            DataSet ds = null;
            Database db1 = null;
            DbCommand dbCmd1 = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_AGENT_REGISTRATION_FORM");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, int.Parse(custid));
                db.AddInParameter(dbCmd, "@TITLE_DESC", DbType.String, title);
                db.AddInParameter(dbCmd, "@CUST_SURNAME", DbType.String, surname);
                db.AddInParameter(dbCmd, "@CUST_NAME", DbType.String, name);
                db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, mobile);
                db.AddInParameter(dbCmd, "@CUST_REL_EMAIL", DbType.String, email);
                db.AddInParameter(dbCmd, "@CUST_TYPE_NAME", DbType.String, custtype);
                db.AddInParameter(dbCmd, "@COMMUNICATION_MODE_NAME", DbType.String, mode);
                db.AddInParameter(dbCmd, "@CUST_REL_PHONE", DbType.String, phone);
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, 1);
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, Convert.ToInt32(custid));
                db.AddInParameter(dbCmd, "@RELATION_DESC", DbType.String, "Self");
                db.AddInParameter(dbCmd, "@DESIGNATION", DbType.String, designation);
                db.AddInParameter(dbCmd, "@CUST_COMPANY_NAME", DbType.String, company);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, Convert.ToInt32("5"));
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS", DbType.String, TERMS);
                db.AddInParameter(dbCmd, "@PASSWORD", DbType.String, pwd);
                db.AddInParameter(dbCmd, "@STATUS", DbType.String, USERSTATUS);
                db.AddInParameter(dbCmd, "@IS_OFFLINE", DbType.Boolean, offline); 
                ds = db.ExecuteDataSet(dbCmd);
                s = ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();
                srno= ds.Tables[0].Rows[0]["sr_no"].ToString();
                db1 = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd1 = db1.GetStoredProcCommand("INSERT_UPDATE_CONTACT_DETAIL_FOR_AGENT_REGISTRATION_FORM");
                db1.AddInParameter(dbCmd1, "@CUST_ID", DbType.String, s);
                db1.AddInParameter(dbCmd1, "@SR_NO", DbType.String, srno);
                db1.AddInParameter(dbCmd1, "@RELATION_DESC", DbType.String, "Self");
                db1.AddInParameter(dbCmd1, "@CUST_REL_ID", DbType.Int32, 1);
                db1.AddInParameter(dbCmd1, "@ADDRESS_TYPE_NAME", DbType.String, addtype);
                db1.AddInParameter(dbCmd1, "@CUST_ADDRESS_LINE1", DbType.String, add1);
                db1.AddInParameter(dbCmd1, "@CUST_ADDRESS_LINE2", DbType.String, add2);
                db1.AddInParameter(dbCmd1, "@CITY_NAME", DbType.String, CITY);
                db1.AddInParameter(dbCmd1, "@STATE_NAME", DbType.String, STATE);
                db1.AddInParameter(dbCmd1, "@COUNTRY_NAME", DbType.String, COUNTERY);
                db1.AddInParameter(dbCmd1, "@CUST_PINCODE", DbType.String, pincode);
                db1.AddInParameter(dbCmd1, "@CUST_PHONE", DbType.String, phone);
                //db.AddInParameter(dbCmd, "@EMERGENCY_NAME", DbType.String, Contdetail[11]);
                db1.AddInParameter(dbCmd1, "@CUST_MOBILE", DbType.String, mobile);
                db1.AddInParameter(dbCmd1, "@CREATED_BY", DbType.Int32, 5);
                db1.AddInParameter(dbCmd1, "@WEBSITE", DbType.String, website);
                db1.AddInParameter(dbCmd1, "@CHAIN_NAME", DbType.String, company);
                DataSet DS1 = db1.ExecuteDataSet(dbCmd1);
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
        #endregion


        public DataSet fetchALLDATA()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_DATA_FOR_APPROVED_AGENT");
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet InsertUpdatesysusermaster(String Email, String pwd, String cust_rel_srno)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_EMAIL_PASSWORD_FOR_AGENT");
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, int.Parse(cust_rel_srno));
                db.AddInParameter(dbCmd, "@PASSWORD", DbType.String, pwd);
                db.AddInParameter(dbCmd, "@CUST_REL_EMAIL", DbType.String, Email);
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
       
        public DataSet InsertUpdateBankCharge(Boolean Is_Applicable, Decimal charge, String cust_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_BANK_CHARGE_OF_AGENT");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, int.Parse(cust_id));
                db.AddInParameter(dbCmd, "@IS_APPLICABLE", DbType.Boolean, Is_Applicable);
                db.AddInParameter(dbCmd, "@BANK_CHARGE", DbType.Decimal, charge);
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
       
        public DataSet DISAPPROVED(String cust_rel_srno)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_STATUS_CHANGED_WHEN_DISAPPROVED");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, int.Parse(cust_rel_srno));
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

        public DataSet fetchSINGLUSERDATA(String Email)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_DATA_FOR_SELECTED_APPROVED_AGENT");
                db.AddInParameter(dbCmd, "@QUERY", DbType.Int32, int.Parse(Email));
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet UpdateCreditlimitincust(String type, String CREDIT, String cust_rel_srno)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_CREDITLIMIT_FOR_APPROVED_AGENT");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, int.Parse(cust_rel_srno));
                //db.AddInParameter(dbCmd, "@CREDITLIMIT", DbType.String, CREDIT);
                if (CREDIT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CREDITLIMIT", DbType.Decimal, Convert.ToDecimal("0"));
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CREDITLIMIT", DbType.Decimal, Convert.ToDecimal(CREDIT));
                }
                db.AddInParameter(dbCmd, "@AGENTTYPE", DbType.String, type);
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

        public DataSet fetchALLUSERNAME()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_USERNAME_FOR_VALIDATE_APPROVED_AGENT");
                //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
      
        public DataSet UpdateBankAgnetMaster(String bank, String branch, String accname,String custid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_COMPANY_ACCOUNT_AGENT_MASTER");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, int.Parse(custid));
                db.AddInParameter(dbCmd, "@BANK", DbType.String, bank);
                db.AddInParameter(dbCmd, "@BRANCH", DbType.String, branch);
                db.AddInParameter(dbCmd, "@ACCOUNT_NAME", DbType.String, accname);
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

        public DataSet InsertUpdateRel(String cust_id, String sr_no, String title, String surname, String name, String mobile, String email, String phone, String designation,
           String pwd, String altemail, String status, String userid,String empid)
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
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CUST_CUSTOMER_RELATION_FOR_SUB_AGENT");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32,int.Parse(cust_id));
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, 2);
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, int.Parse(sr_no));
                db.AddInParameter(dbCmd, "@TITLE_DESC", DbType.String, title);
                db.AddInParameter(dbCmd, "@CUST_REL_SURNAME", DbType.String, surname);
                db.AddInParameter(dbCmd, "@CUST_REL_NAME", DbType.String, name);
                db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, mobile);
                db.AddInParameter(dbCmd, "@CUST_REL_EMAIL", DbType.String, email);
                db.AddInParameter(dbCmd, "@CUST_REL_PHONE", DbType.String, phone);
                db.AddInParameter(dbCmd, "@DESIGNATION", DbType.String, designation);
                db.AddInParameter(dbCmd, "@PASSWORD", DbType.String, pwd);
                db.AddInParameter(dbCmd, "@ALT_EMAIL", DbType.String, altemail);
                db.AddInParameter(dbCmd, "@USER_STATUS", DbType.String, status);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, userid);
                db.AddInParameter(dbCmd, "@PARENT_CUSTOMER", DbType.Int32, empid);
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

        public DataSet fetchSUBAGENTDETAIL(String param,String param1,String param2,String param3)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("SEARCH_SUB_AGENT");
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, param);
                db.AddInParameter(dbCmd, "@NAME", DbType.String, param1);
                db.AddInParameter(dbCmd, "@USER_STATUS", DbType.String, param2);
                db.AddInParameter(dbCmd, "@EMAIL", DbType.String, param3);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        
        public DataSet fetchSubAgentDetail(String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_SUB_AGENT_EDIT");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }


        public DataSet InsertUpdateProfile(String sr_no, String title, String surname, String name, String mobile, String email, String phone, String designation,
         String status, String mstatus,String date)
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
                dbCmd = db.GetStoredProcCommand("UPDATE_AGENT_PROFILE");
                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, int.Parse(sr_no));
                db.AddInParameter(dbCmd, "@TITLE", DbType.String, title);
                db.AddInParameter(dbCmd, "@SURNAME", DbType.String, surname);
                db.AddInParameter(dbCmd, "@NAME", DbType.String, name);
                db.AddInParameter(dbCmd, "@MOBILE", DbType.String, mobile);
                db.AddInParameter(dbCmd, "@EMAIL", DbType.String, email);
                db.AddInParameter(dbCmd, "@PHONE", DbType.String, phone);
                db.AddInParameter(dbCmd, "@DESIGNATION_DESC", DbType.String, designation);
                db.AddInParameter(dbCmd, "@STATUS", DbType.String, status);
                db.AddInParameter(dbCmd, "@MARTIAL_STATUS", DbType.String, mstatus);
                if (date.ToString().Equals(""))
                {

                    db.AddInParameter(dbCmd, "@BIRTH_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@BIRTH_DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
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

        public DataSet FetchProfileData(String user_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_DATA_FOR_AGENT_PROFILE");
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, int.Parse(user_id));
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public void UpdatePassword(int userid, String Password)
        {
             Database db = null;
           DbCommand dbCmd = null;
             try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("UPDATE_USER_PASSWORD");

               db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userid);
               db.AddInParameter(dbCmd, "@PASSWORD", DbType.String, Password);
               //db.AddInParameter(dbCmd, "@BOOK_EMAIL_TO_BACKOFFICE", DbType.Int32, book_email);

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
