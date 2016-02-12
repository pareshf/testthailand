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
     public class  SupplierMasterStoredProcedure
    {
         public DataSet InsertUpdateSupplier(ArrayList Supply)
         {
             Database db = null;
             DbCommand dbCmd = null;

             Database db1 = null;
             DbCommand dbCmd1 = null;
             DataSet ds = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("dbo.INSERT_UPDATE_SUPPLIER_MASTER");
                 db.AddInParameter(dbCmd, "@SUPPLIER_ID", DbType.Int32,Convert.ToInt32(Supply[1]));
                 db.AddInParameter(dbCmd, "@SUPPLIER_COMPANY_NAME", DbType.String, Supply[0]);
                 db.AddInParameter(dbCmd, "@REMARKS", DbType.String,Supply[2]);
                 db.AddInParameter(dbCmd, "@SUPPLIER_PHOTO_PATH", DbType.String , Supply[3]);
                 db.AddInParameter(dbCmd, "@CREDIT_LIMIT", DbType.Decimal, Convert.ToDecimal(Supply[4]));
                 db.AddInParameter(dbCmd, "@COMMUNICATION_TIME", DbType.String , Supply[5]);
                 db.AddInParameter(dbCmd, "@ACCOUNTING_CODE ", DbType.String , Supply[6]);
                 db.AddInParameter(dbCmd, "@SUPPLIER_UNQ_ID", DbType.String, Supply[7]);
                 db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.String, Supply[8]);
                 db.AddInParameter(dbCmd, "@COMMUNICATION_MODE_ID", DbType.String, Supply[9]);
                 db.AddInParameter(dbCmd, "@DESIGNATION_ID", DbType.String, Supply[10]);
                 db.AddInParameter(dbCmd, "@SUPPLIER_TYPE_ID", DbType.String, Supply[11]);
                 db.AddInParameter(dbCmd, "@GROUP_ID", DbType.String, Supply[12]);
                 db.AddInParameter(dbCmd, "@TO_ISSUE_VOUCHER", DbType.String, Supply[17]);
                 db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Supply[18]));
                 //db.ExecuteNonQuery(dbCmd);
                 ds = db.ExecuteDataSet(dbCmd);
                 
                 db1 = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd1 = db.GetStoredProcCommand("dbo.INSERT_UPDATE_RELATION_FOR_SUPPLIER_MASTER");
                 db1.AddInParameter(dbCmd1, "@MOBILE", DbType.String, Supply[13]);
                 db1.AddInParameter(dbCmd1, "@PHONE", DbType.String, Supply[14]);
                 db1.AddInParameter(dbCmd1, "@EMAIL", DbType.String, Supply[15]);
                 db1.AddInParameter(dbCmd1, "@PASSWORD", DbType.String, Supply[16]);
                 db1.AddInParameter(dbCmd1, "@SUPPLIER_REL_SRNO", DbType.Int32, Convert.ToInt32(Supply[21]));
                 //db1.ExecuteNonQuery(dbCmd1);
                 ds = db.ExecuteDataSet(dbCmd1);
                
                 
                
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
         public void deleteSupplier(int SUPPLIERID)
         {
             Database db = null;
             DbCommand dbCmd = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("DLEETE_FOR_SUPPLIER_MASTER");
                 db.AddInParameter(dbCmd, "@SUPPLIER_ID", DbType.Int32, SUPPLIERID);
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
         public void InsertUpdateSupplierContect(ArrayList Contect)
         {
             Database db = null;
             DbCommand dbCmd = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("dbo.INSERT_UPDATE_FOR_SUPPLIER_CONTECT_DETAIL");
                 db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.Int32, Convert.ToInt32(Contect[0]));
                 db.AddInParameter(dbCmd, "@ADDRESS_TYPE", DbType.String, Contect[1]);
                 db.AddInParameter(dbCmd, "@ADDRESS_LINE1", DbType.String, Contect[2]);
                 db.AddInParameter(dbCmd, "@ADDRESS_LINE2", DbType.String, Contect[3]);
                 db.AddInParameter(dbCmd, "@CITY", DbType.String, Contect[4]);
                 db.AddInParameter(dbCmd, "@STATE", DbType.String, Contect[5]);
                 db.AddInParameter(dbCmd, "@COUNTRY", DbType.String, Contect[6]);
                 db.AddInParameter(dbCmd, "@PINCODE", DbType.String, Contect[7]);
                 db.AddInParameter(dbCmd, "@PHONE", DbType.String, Contect[8]);
                 db.AddInParameter(dbCmd, "@WEBSITE", DbType.String, Contect[9]);
                 db.AddInParameter(dbCmd, "@CHAIN_NAME", DbType.String, Contect[10]);
                 db.AddInParameter(dbCmd, "@VIDEO_CODE", DbType.String, Contect[11]);
                 db.AddInParameter(dbCmd, "@LOGO", DbType.String, Contect[12]);
                 db.AddInParameter(dbCmd, "@CHECK_IN_TIME", DbType.String, Contect[13]);
                 db.AddInParameter(dbCmd, "@CHECK_OUT_TIME", DbType.String, Contect[14]);
                 db.AddInParameter(dbCmd, "@FAX1", DbType.String, Contect[15]);
                 db.AddInParameter(dbCmd, "@FAX2", DbType.String, Contect[16]);
                 db.AddInParameter(dbCmd, "@STAR", DbType.Int32, Convert.ToInt32(Contect[17]));
                 db.AddInParameter(dbCmd, "@LOCATION", DbType.String, Contect[18]);
                 db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Contect[19]));
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
         public void deleteSupplierContect(int Contectid)
         {
             Database db = null;
             DbCommand dbCmd = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("DELETE_FROM_SUPPLIER_CONTECT_DETAIL");
                 db.AddInParameter(dbCmd, "@CONTECTID", DbType.Int32, Contectid);
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
         public void InsertNewContect(string SUPPLIER_ID)
         {
             Database db = null;
             DbCommand dbCmd = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("INSERT_NEW_CONTECT_DETAIL_FOR_SUPPLIER");
                 db.AddInParameter(dbCmd, "@CONTECT_ID", DbType.Int32, Convert.ToInt32(SUPPLIER_ID));
                // db.AddInParameter(dbCmd, "@COMPANY_NAME", DbType.String, SUPPLIER_COMPANY_NAME);
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
         public DataSet InsertUpdateSupplierEmployeeNew(ArrayList Employee)
         {
             Database db = null;
             DbCommand dbCmd = null;
             //Boolean flag;
             DataSet ds = null; 
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("dbo.INSERT_UPDATE_FOR_SUPPLIER_EMPLOYEE_DETAIL");
                 db.AddInParameter(dbCmd, "@SUPPLIER_REL_SRNO", DbType.Int32, Convert.ToInt32(Employee[0]));
                 db.AddInParameter(dbCmd, "@SUPPLIER_REL_TITLE", DbType.String, Employee[1]);
                 db.AddInParameter(dbCmd, "@SUPPLIER_REL_SURNAME", DbType.String, Employee[2]);
                 db.AddInParameter(dbCmd, "@SUPPLIER_REL_NAME", DbType.String, Employee[3]);
                 //db.AddInParameter(dbCmd, "@SUPPLIER_REL_GENDER", DbType.String, Employee[4]);
                 db.AddInParameter(dbCmd, "@SUPPLIER_REL_EMAIL", DbType.String, Employee[4]);
                 db.AddInParameter(dbCmd, "@SUPPLIER_REL_MOBILE", DbType.String, Employee[5]);
                 db.AddInParameter(dbCmd, "@SUPPLIER_REL_PHONE", DbType.String, Employee[6]);
                 db.AddInParameter(dbCmd, "@PASSWORD", DbType.String, Employee[7]);
                 db.AddInParameter(dbCmd, "@ALTERNATE_EMAIL", DbType.String, Employee[8]);
                 //if (Convert.ToString(Employee[10]).Equals("YES"))
                 //{
                 //    flag = true;
                 //}
                 //else
                 //{
                 //    flag = false;
                 //}
                 //db.AddInParameter(dbCmd, "@IS_ACCOUNT", DbType.Boolean, flag);
                 db.AddInParameter(dbCmd, "@USER_STATUS", DbType.String, Employee[9]);
                 //db.AddInParameter(dbCmd, "@CREDIT_LIMIT", DbType.String, Employee[12]);
                 //db.AddInParameter(dbCmd, "@PARENT_SUPPLIER", DbType.String, Employee[13]);
                 db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Employee[10]));
                 //db.ExecuteNonQuery(dbCmd);
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
         public void InsertNewEmployee(string SUPPLIER_SR_NO)
         {
             Database db = null;
             DbCommand dbCmd = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("INSERT_NEW_SUPPLIER_EMPLOYEE_DETAIL");
                 db.AddInParameter(dbCmd, "@SUPPLIER_ID", DbType.Int32, Convert.ToInt32(SUPPLIER_SR_NO));
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
         public void deleteSupplierEmployee(int employee)
         {
             Database db = null;
             DbCommand dbCmd = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("DELETE_FORM_SUPPLIER_EMPLOYEE_DETAIL");
                 db.AddInParameter(dbCmd, "@SUPPLIER_EMP_ID", DbType.Int32, employee);
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
         public void InsertNewGenerateCode(string SUPPLIER_COMPANY_NAME,string SUPPLIER_ID,string Supplier)
         {
             Database db = null;
             DbCommand dbCmd = null;
             try
             {
                 db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                 dbCmd = db.GetStoredProcCommand("INSERT_GL_CODE_FOR_SUPPLIER_MASTER");
                 db.AddInParameter(dbCmd, "@COMPANY_NAME", DbType.String,SUPPLIER_COMPANY_NAME);
                 db.AddInParameter(dbCmd, "@FLAG", DbType.Int32,Convert.ToInt32(SUPPLIER_ID));
                 db.AddInParameter(dbCmd, "@FLAG1", DbType.String,'S');
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
