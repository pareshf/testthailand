using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using CRM.Model.HRModel;


namespace CRM.DataAccess.HRDAL.Reports
{
    public class EmployeeReportDal
    {

        #region Get Employee by Department wise
        public DataTable GetEmployeeByDept(int DeptId,int CmpId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable Dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_LIST_BY_DEPARTMENT_REPORT);
                if ( DeptId !=0)
                    db.AddInParameter(dbCmd, "@DEPT_ID", DbType.Int32, DeptId);
                else
                    db.AddInParameter(dbCmd, "@DEPT_ID", DbType.Int32, DBNull.Value);
                 db.AddInParameter(dbCmd, "@CMP_ID", DbType.Int32, CmpId);
                   ds = db.ExecuteDataSet(dbCmd);
                   Dt = ds.Tables[0];
                return(Dt);
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
            return (Dt);
        }

        #endregion

        #region Get Employee by Company and Department wise
        public DataTable GetEmployeeByComp(int CompId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable Dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_LIST_BY_COMPANY_REPORT);


                if (CompId != 0)
                    db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, CompId);
                else
                    db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, DBNull.Value);

                ds = db.ExecuteDataSet(dbCmd);
                Dt = ds.Tables[0];
                return (Dt);
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
            return (Dt);
        }
        #endregion


        #region Get Employee CARD details
        public DataTable GetEmployeeCard(int CompId,int EMPId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable Dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_CARD_REPORT);
                db.AddInParameter(dbCmd, "@EMPLOYEE_ID", DbType.Int32, EMPId);
                db.AddInParameter(dbCmd, "@Company_ID", DbType.Int32, CompId);

                ds = db.ExecuteDataSet(dbCmd);
                Dt = ds.Tables[0];
                return (Dt);
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
            return (Dt);
        }

        public DataTable GetEmployeeCompanyInfo(int EMPId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable Dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_CARD_OTHER_COMPANY_REPORT);
                db.AddInParameter(dbCmd, "@EMPLOYEE_ID", DbType.Int32, EMPId);
                
                ds = db.ExecuteDataSet(dbCmd);
                Dt = ds.Tables[0];
                return (Dt);
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
            return (Dt);
        }

        public DataTable GetEmployeeContactInfo(int EMPId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable Dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_CONTACT_REPORT);
                db.AddInParameter(dbCmd, "@EmployeeId", DbType.Int32, EMPId);
                ds = db.ExecuteDataSet(dbCmd);
                Dt = ds.Tables[0];
                return (Dt);
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
            return (Dt);
        }
        
        
        
        
        #endregion




    }
}
