using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.Account
{
    public  class SalaryVoucherStoredProcedure
    {
        public DataSet fetch_employee_details_dalary(String sp_name)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                dsData = db.ExecuteDataSet(dbCmd);
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
            return dsData;
        }


        public void insert_salary_entry(int salary_header_id, int emp_id, String amount, int prepared_by, int approved_by, int posted_by, String voucher_status, int salary_details_id, String gl_desc, String cr_amount, String dr_amount, int company_id, int flag)
        {
              Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_SALARY_HEADER_DETAILS");

               db.AddInParameter(dbCmd, "@SALARY_VOUCHER_ID", DbType.Int32, salary_header_id);
               db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, emp_id);

               if (amount.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal, Decimal.Parse(amount));
               }

               db.AddInParameter(dbCmd, "@PREPARED_BY", DbType.Int32, prepared_by );
               db.AddInParameter(dbCmd, "@APPROVED_BY", DbType.Int32, approved_by );
               db.AddInParameter(dbCmd, "@POSTED_BY", DbType.Int32, posted_by);
               db.AddInParameter(dbCmd, "@VOUCHER_STATUS_ID", DbType.String, voucher_status);


               db.AddInParameter(dbCmd, "@SALARY_DETAILS_ID", DbType.Int32, salary_details_id);
               db.AddInParameter(dbCmd, "@GL_CODE", DbType.String , gl_desc);

               if (cr_amount .ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@CR_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@CR_AMOUNT", DbType.Decimal, Decimal.Parse(cr_amount));
               }

               if (dr_amount.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DR_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DR_AMOUNT", DbType.Decimal, Decimal.Parse(dr_amount));
               }

               db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, company_id);
               db.AddInParameter(dbCmd, "@FLAG", DbType.Int32, flag);
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

        public DataSet fetch_employee_bank(String sp_name, int comp_acc_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@COMP_ACC_ID", DbType.Int32, comp_acc_id);
                dsData = db.ExecuteDataSet(dbCmd);
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
            return dsData;
        }

        public void insert_last_salary_date(String date, int emp_id)
        {
            
              Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_LAST_DATE_EMP_EMPLOYEE_MASTER");

               if (date.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
               }
               db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, emp_id);
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
