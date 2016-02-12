using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

#region Impoerts assemblies
using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
#endregion

namespace CRM.DataAccess.AdministratorEntity
{
    public class EmployeemasterStoreprocedures
    {
        public void deleteemployee(int empid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_EMPLOYEE_FROM_EMPLOYEE_MASTER_NEW");
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32,Convert.ToInt32(empid));
                //db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, Convert.ToInt32(userid));
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


        public DataSet InsertUpdateEmployee(ArrayList employee)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_EMPLOYEE_FROM_EMPLOYEE_MASTER_NEW");

                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, Convert.ToInt32(employee[14]));

                db.AddInParameter(dbCmd, "@SALARY", DbType.Decimal, Convert.ToDecimal(employee[13]));
                db.AddInParameter(dbCmd, "@SIGNATURE_PW", DbType.String, employee[12]);
                db.AddInParameter(dbCmd, "@STATUS_NAME1", DbType.String,employee[11]);
                db.AddInParameter(dbCmd, "@QUALIFICATION_NAME1", DbType.String,employee[10]);
                db.AddInParameter(dbCmd, "@MARITAL_STATUS_NAME1", DbType.String,employee[9]);
                db.AddInParameter(dbCmd, "@EMP_PHONE", DbType.String, employee[8]);
                db.AddInParameter(dbCmd, "@EMP_MOBILE", DbType.String, employee[7]);
                db.AddInParameter(dbCmd, "@EMP_EMAIL", DbType.String, employee[6]);
                db.AddInParameter(dbCmd, "@GENDER_NAME1", DbType.String, employee[5]);
                if (employee[4].ToString().Equals("DD/MM/YYYY") || employee[4].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@EMP_DOB", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EMP_DOB", DbType.DateTime,DateTime.ParseExact(employee[4].ToString(),"dd/MM/yyyy",null));
                }
                db.AddInParameter(dbCmd, "@EMP_NAME", DbType.String, employee[3]);
                db.AddInParameter(dbCmd, "@EMP_SURNAME", DbType.String, employee[2]);
                db.AddInParameter(dbCmd, "@TITLE_DESC1", DbType.String,employee[1]);
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, Convert.ToInt32(employee[0]));
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
        public void InsertUpdateEmployeeContectDetails(ArrayList contect)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CONTACT_EMPLOYEE_NEW1");
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32,Convert.ToInt32(contect[8]));
                db.AddInParameter(dbCmd, "@ADDRESS_TYPE_NAME1", DbType.String,contect[5]);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE1", DbType.String,contect[1]);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE2", DbType.String,contect[2]);
                db.AddInParameter(dbCmd, "@CITY_NAME1",DbType.String,contect[4]);
                db.AddInParameter(dbCmd, "@STATE_NAME1", DbType.String,contect[3]);
                db.AddInParameter(dbCmd, "@COUNTRY_NAME1", DbType.String,contect[0]);
                db.AddInParameter(dbCmd, "@PINCODE", DbType.String,contect[6]);
                db.AddInParameter(dbCmd, "@PHONE", DbType.String,contect[7]);
                
                db.AddInParameter(dbCmd, "@EMP_CONTACT_SRNO1", DbType.Int32,Convert.ToInt32(contect[9]));
                db.AddInParameter(dbCmd, "@USER_BY", DbType.Int32, Convert.ToInt32(contect[10]));
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
        
        public DataSet UpdateUserDetails(ArrayList employee)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_SYS_USER_MASTER_NEW");

                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32,Convert.ToInt32(employee[5]));
                db.AddInParameter(dbCmd, "@MODIFIED_BY", DbType.String,Convert.ToInt32(employee[6]));
                db.AddInParameter(dbCmd, "@PASSWORD", DbType.String, employee[4]);
                if (employee[3].ToString().Equals("DD/MM/YYYY") || employee[3].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(employee[3].ToString(), "dd/MM/yyyy", null));
                }
                if (employee[2].ToString().Equals("DD/MM/YYYY") || employee[2].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(employee[2].ToString(), "dd/MM/yyyy", null));
                } 
                db.AddInParameter(dbCmd, "@USER_NAME", DbType.String, employee[1]);
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


        public void InsertAssignCompany(string CompanyId, int UserID, int CreatedBy)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_USER_COMPANY_FRANCHIES_MAPPING_INSERT);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, UserID);
                db.AddInParameter(dbCmd, "@COMPANY_FRANCHIES_ID", DbType.String, CompanyId);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CreatedBy);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
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
          public void InsertUserRoleForEmployee(ArrayList MyRole)
          {
              Database db = null;
              DbCommand dbCmd = null;
              try
              {
                  db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                  dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_ROLE_FOR_EMPLOYEE");
                  db.AddInParameter(dbCmd, "@USER", DbType.Int32,Convert.ToInt32(MyRole[0]));
                  db.AddInParameter(dbCmd, "@COMPANY", DbType.String, MyRole[1]);
                  db.AddInParameter(dbCmd, "@DEPARTMENT", DbType.String, MyRole[2]);
                  db.AddInParameter(dbCmd, "@ROLE", DbType.String, MyRole[3]);
                  db.AddInParameter(dbCmd, "@REPORT", DbType.String, MyRole[4]);
                  
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

          public void DeleteEmployeeRole(int user_id, string comp_name, string dept_name, string role_name)
          { 
                Database db = null;
                DbCommand dbCmd = null;
                try
                {
                    db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                    dbCmd = db.GetStoredProcCommand("DELETE_FROM_SYS_USER_ROLE_MAPPING");
                    db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, user_id);
                    db.AddInParameter(dbCmd, "@COMP_ID", DbType.String, comp_name);
                    db.AddInParameter(dbCmd, "@DEPT_ID", DbType.String, dept_name);
                    db.AddInParameter(dbCmd, "@ROLE_ID", DbType.String, role_name);
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

          #region UnAssigned CompanyList
          /// <summary>
          /// Insert Assign Company list with coma seprated value
          /// </summary>
          /// <param name="xmlData">Data that converted into xml format.</param>
          /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
          public void UnAssignCompany(string CompanyId, int UserID)
          {
              Database db = null;
              DbCommand dbCmd = null;
              int Result = 0;
              try
              {
                  db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                  dbCmd = db.GetStoredProcCommand(DALHelper.USP_USER_COMPANY_FRANCHIES_MAPPING_DELETE);
                  db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, UserID);
                  db.AddInParameter(dbCmd, "@COMPANY_FRANCHIES_ID", DbType.String, CompanyId);
                  db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                  db.ExecuteNonQuery(dbCmd);
                  Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
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
          #endregion

          #region Insert RoleList(Assigned)
          /// <summary>
          /// Insert Assign Company list with coma seprated value
          /// </summary>
          /// <param name="xmlData">Data that converted into xml format.</param>
          /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
          public void InsertAssignRole(int UserId, int CompanyId, int RoleId)
          {
              Database db = null;
              DbCommand dbCmd = null;
              int Result = 0;
              try
              {
                  db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                  dbCmd = db.GetStoredProcCommand(DALHelper.USP_USER_ROLE_MAPPING_INSERT);
                  db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, UserId);
                  db.AddInParameter(dbCmd, "@COMPANY_FRANCHIES_ID", DbType.String, CompanyId);
                  db.AddInParameter(dbCmd, "@ROLE_ID", DbType.Int32, RoleId);
                  db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                  db.ExecuteNonQuery(dbCmd);
                  Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
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
          #endregion

          #region UnAssigned CompanyList
          /// <summary>
          /// Insert Assign Company list with coma seprated value
          /// </summary>
          /// <param name="xmlData">Data that converted into xml format.</param>
          /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
          public void UnAssignRole(int UserID, int CompanyId, int RoleId)
          {
              Database db = null;
              DbCommand dbCmd = null;
              int Result = 0;
              try
              {
                  db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                  dbCmd = db.GetStoredProcCommand(DALHelper.USP_USER_ROLE_MAPPING_DELETE);
                  db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, UserID);
                  db.AddInParameter(dbCmd, "@COMPANY_FRANCHIES_ID", DbType.String, CompanyId);
                  db.AddInParameter(dbCmd, "@ROLE_ID", DbType.Int32, RoleId);
                  db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                  db.ExecuteNonQuery(dbCmd);
                  Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
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
          #endregion

    }
}
