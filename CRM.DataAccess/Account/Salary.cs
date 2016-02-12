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
    public class Salary
    {
        public DataSet fetch_voucher_type(String sp_name)
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
            return dsData;
        }
        public void Updatesalary(int salary_voucher_id, String v_amount, int prepared_by, int approved_by, int posted_by, String v_status, int salary_detail_id, String cr_amount, String dr_amount, String gl_code, int seq_no, String gl_Date, int flag)
        {

            Database db = null;
           DbCommand dbCmd = null;
           //DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_FOR_SALARY_VOUCHER");
               // ds=db.ExecuteDataSet(dbCmd);
               db.AddInParameter(dbCmd, "@SALARY_VOUCHER_ID", DbType.Int32, salary_voucher_id);
               //db.AddInParameter(dbCmd, "@EMPLOYEE_NAME", DbType.String, emp_name);

               if (v_amount.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal, Decimal.Parse("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@VOUCHER_AMOUNT", DbType.Decimal, Decimal.Parse(v_amount));
               }
               
               db.AddInParameter(dbCmd, "@PREPARED_BY", DbType.Int32, prepared_by);
               db.AddInParameter(dbCmd, "@APPROVED_BY", DbType.Int32, approved_by);
               db.AddInParameter(dbCmd, "@POSTED_BY", DbType.Int32, posted_by);
               db.AddInParameter(dbCmd, "@VOUCHER_STATUS_ID", DbType.String, v_status);
               db.AddInParameter(dbCmd, "@SALARY_DETAIL_ID", DbType.Int32, salary_detail_id);

               if (cr_amount.ToString().Equals(""))
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
               db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, gl_code);
               db.AddInParameter(dbCmd, "@SEQ_NO", DbType.Int32, seq_no);

               if (gl_Date.ToString().Equals("") )
               {
                   db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@GL_DATE", DbType.DateTime, DateTime.ParseExact(gl_Date.ToString(), "dd/MM/yyyy", null));
               }

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
          // return ds;

        }
        public DataSet GetAllData(String sp_name, int seqno)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@SEQ_NO", DbType.Int32, seqno);
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
    }
}
