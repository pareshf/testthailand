using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;


namespace CRM.DataAccess.AccountMaster
{
    public  class ChartofAccountStoredProcedure
    {

        public DataSet get_all_names(String SP_NAME, String type)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(SP_NAME);
                db.AddInParameter(dbCmd, "@TYPE", DbType.String, type);
                
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public void insert_chart_of_account(String GL_CODE, String GL_DESCRIPTION, String ACCOUNT_GROUP, int SIDE_CODE_ID, String OP_BALANCE, String OP_BALANCE_TYPE, String OP_BALANCE_ASON_DATE, String COMPANY, String ACCOUNT_ID, String ACCOUNT_FLAG, String FILE_PATH, String ITNO, String SALESTAXNO, int CREATED_BY) 
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_CHART_OF_ACCOUNT");

                db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, GL_CODE);
                db.AddInParameter(dbCmd, "@GL_DESCRIPTION", DbType.String, GL_DESCRIPTION);
                db.AddInParameter(dbCmd, "@ACCOUNT_GROUP", DbType.String, ACCOUNT_GROUP);
                db.AddInParameter(dbCmd, "@SIDE_CODE_ID", DbType.Int32, SIDE_CODE_ID);

                if (OP_BALANCE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@OP_BALANCE", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@OP_BALANCE", DbType.Decimal, decimal.Parse(OP_BALANCE));
                }

                db.AddInParameter(dbCmd, "@OP_BALANCE_TYPE", DbType.String, OP_BALANCE_TYPE);

                if (OP_BALANCE_ASON_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@OP_BALANCE_ASON_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@OP_BALANCE_ASON_DATE", DbType.DateTime, DateTime.ParseExact(OP_BALANCE_ASON_DATE.ToString(), "dd/MM/yyyy", null));
                }
                

                db.AddInParameter(dbCmd, "@COMPANY", DbType.String, COMPANY);

                if (ACCOUNT_ID.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ACCOUNT_ID", DbType.Int32, 0);
                }
                else
                {

                    db.AddInParameter(dbCmd, "@ACCOUNT_ID", DbType.Int32, ACCOUNT_ID);
                }

                db.AddInParameter(dbCmd, "@ACCOUNT_FLAG", DbType.String, ACCOUNT_FLAG);
                db.AddInParameter(dbCmd, "@FILE_PATH", DbType.String, FILE_PATH);
                db.AddInParameter(dbCmd, "@ITNO", DbType.String, ITNO);
                db.AddInParameter(dbCmd, "@SALESTAXNO", DbType.String, SALESTAXNO);

                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);
                

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

        public void update_chart_of_account(int CHART_OF_ACCOUNTS_ID, String GL_CODE, String GL_DESCRIPTION, String ACCOUNT_GROUP, int SIDE_CODE_ID, String OP_BALANCE, String OP_BALANCE_TYPE, String OP_BALANCE_ASON_DATE, String COMPANY, String ACCOUNT_ID, String ACCOUNT_FLAG, String FILE_PATH, String ITNO, String SALESTAXNO, int CREATED_BY)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_CHART_OF_ACCOUNT");

                db.AddInParameter(dbCmd, "@CHART_OF_ACCOUNTS_ID", DbType.Int32, CHART_OF_ACCOUNTS_ID);
                db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, GL_CODE);
                db.AddInParameter(dbCmd, "@GL_DESCRIPTION", DbType.String, GL_DESCRIPTION);
                db.AddInParameter(dbCmd, "@ACCOUNT_GROUP", DbType.String, ACCOUNT_GROUP);
                db.AddInParameter(dbCmd, "@SIDE_CODE_ID", DbType.Int32, SIDE_CODE_ID);

                if (OP_BALANCE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@OP_BALANCE", DbType.Int32, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@OP_BALANCE", DbType.Decimal, decimal.Parse(OP_BALANCE));
                }

                db.AddInParameter(dbCmd, "@OP_BALANCE_TYPE", DbType.String, OP_BALANCE_TYPE);

                if (OP_BALANCE_ASON_DATE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@OP_BALANCE_ASON_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@OP_BALANCE_ASON_DATE", DbType.DateTime, DateTime.ParseExact(OP_BALANCE_ASON_DATE.ToString(), "dd/MM/yyyy", null));
                }


                db.AddInParameter(dbCmd, "@COMPANY", DbType.String, COMPANY);

                if (ACCOUNT_ID.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ACCOUNT_ID", DbType.Int32, 0);
                }
                else
                {

                    db.AddInParameter(dbCmd, "@ACCOUNT_ID", DbType.Int32, ACCOUNT_ID);
                }

                db.AddInParameter(dbCmd, "@ACCOUNT_FLAG", DbType.String, ACCOUNT_FLAG);
                db.AddInParameter(dbCmd, "@FILE_PATH", DbType.String, FILE_PATH);
                db.AddInParameter(dbCmd, "@ITNO", DbType.String, ITNO);
                db.AddInParameter(dbCmd, "@SALESTAXNO", DbType.String, SALESTAXNO);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);
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

        public DataSet get_data_for_edit(int CHART_OF_ACCOUNTS_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_CHART_OF_ACCOUNT_DATA_FOR_EDIT");
                db.AddInParameter(dbCmd, "@CHART_OF_ACCOUNTS_ID", DbType.String, CHART_OF_ACCOUNTS_ID);

                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

         public void insert_chart_of_file(int CHART_OF_ACCOUNTS_ID, String FILE_NAME)
         {
              Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_FILE_PATH_CA");
                db.AddInParameter(dbCmd, "@CHART_OF_ACCOUNT_ID", DbType.Int32, CHART_OF_ACCOUNTS_ID);
                db.AddInParameter(dbCmd, "@FILE_NAME", DbType.String, FILE_NAME);

                 db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {

            }
         }

        /********************************** GL CODE GENERATION*************************************/
         public DataSet get_CompanyID(String COMPANY_NAME)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("FETCH_COMPANY_ID");
                 db.AddInParameter(dbCmd, "@COMPANY_NAME", DbType.String, COMPANY_NAME);

                 dsData = db.ExecuteDataSet(dbCmd);
             }
             catch (Exception ex)
             {

             }

             return dsData;
         }

         public DataSet get_Max_GLCode(String ACCOUNT_GROUP)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("FETCH_MAX_GL_CODE");
                 db.AddInParameter(dbCmd, "@ACCOUNT_GROUP", DbType.String, ACCOUNT_GROUP);

                 dsData = db.ExecuteDataSet(dbCmd);
             }
             catch (Exception ex)
             {

             }

             return dsData;
         }

         public void update_employee_master(int EMP_ID, String GL_CODE)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("UPDATE_GL_CODE_FOR_EMP_EMPLOYEE_MASTER");
                 db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, EMP_ID);
                 db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, GL_CODE);
                 

                 db.ExecuteNonQuery(dbCmd);
             }
             catch (Exception ex)
             {

             }
         }

         public void update_supplier_master(int EMP_ID, String GL_CODE)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("UPDATE_GL_CODE_FOR_SUPPLIER_MASTER");
                 db.AddInParameter(dbCmd, "@SUPPLIER_ID", DbType.Int32, EMP_ID);
                 db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, GL_CODE);


                 db.ExecuteNonQuery(dbCmd);
             }
             catch (Exception ex)
             {

             }
         }

         public void update_agent_master(int EMP_ID, String GL_CODE)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("UPDATE_GL_CODE_FOR_CUST_CUSTOMER_MASTER");
                 db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, EMP_ID);
                 db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, GL_CODE);


                 db.ExecuteNonQuery(dbCmd);
             }
             catch (Exception ex)
             {

             }
         }

         public void update_product_master(int PRODUCT_ID, String GL_CODE)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("UPDATE_GL_CODE_FOR_COMMON_PRODUCT_MASTER");
                 db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, PRODUCT_ID);
                 db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, GL_CODE);


                 db.ExecuteNonQuery(dbCmd);
             }
             catch (Exception ex)
             {

             }
         }

         public void update_company_bank_master(int COMP_ACC_ID, String GL_CODE)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("UPDATE_GL_CODE_COMPANY_BANK");
                 db.AddInParameter(dbCmd, "@COMP_ACC_ID", DbType.Int32, COMP_ACC_ID);
                 db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, GL_CODE);


                 db.ExecuteNonQuery(dbCmd);
             }
             catch (Exception ex)
             {

             }
         }

         public DataSet get_Supplier_Type(String SUPPLIER_NAME)
         {
             Database db = null;
             DbCommand dbCmd = null;
             DataSet dsData = null;

             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("FETCH_SUPPLIER_TYPE_CA");
                 db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, SUPPLIER_NAME);

                 dsData = db.ExecuteDataSet(dbCmd);
             }
             catch (Exception ex)
             {

             }

             return dsData;
         }
        
    }
}
