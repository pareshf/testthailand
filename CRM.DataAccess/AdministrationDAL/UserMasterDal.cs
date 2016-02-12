using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.Model.AdministrationModel;

namespace CRM.DataAccess.AdministrationDAL
{
    public class UserMasterDal : IDisposable
    {
        #region Get User
        /// <summary>
        /// Gets User list.
        /// </summary>
        /// <returns>Returns dataset contains custromer data.</returns>
        public DataSet GetUser()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_USER_SELECT);
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
        #endregion

        #region Get User Search
        /// <summary>
        /// Gets User list by filter criteria.
        /// </summary>
        /// <returns>Returns dataset contains custromer data.</returns>
        public DataSet GetUserSearch(UserMasterBDto objUser)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_USER_DYNAMIC_SELECT);
                db.AddInParameter(dbCmd, "@USER_NAME", DbType.String, objUser.UserName);
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
        #endregion

        #region Update User
        /// <summary>
        /// Update User detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateUser(UserMasterBDto objUser)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_USER_UPDATE);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, objUser.m_UserId);
                db.AddInParameter(dbCmd, "@USER_NAME", DbType.String, objUser.UserName);
                db.AddInParameter(dbCmd, "@PASSWORD", DbType.String, objUser.Password);
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, objUser.EmpID);
                db.AddInParameter(dbCmd, "@SECURITY_QUESTION_ID", DbType.Int32, objUser.SecurityQusId);
                db.AddInParameter(dbCmd, "@SECURITY_ANSWERS", DbType.String, objUser.SecurityQusAns);
                db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, objUser.FromDate);

                if (objUser.ToDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, objUser.ToDate);

                //db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, objUser.ToDate);
                db.AddInParameter(dbCmd, "@MODIFIED_BY", DbType.Int32, objUser.UserId);
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
            return Result;
        }
        #endregion

        #region DeleteUser
        /// <summary>
        /// Delete Users detail.
        /// </summary>
        /// <param name="idCollections">User Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteUser(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_USER_DELETE);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.String, idCollections);
                db.AddOutParameter(dbCmd, "@IS_DELETE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_DELETE"));
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
            return Result;
        }
        #endregion

        #region Insert User
        /// <summary>
        /// Insert User detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertUser(UserMasterBDto User)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_USER_INSERT);
                db.AddInParameter(dbCmd, "@USER_NAME", DbType.String, User.UserName);
                db.AddInParameter(dbCmd, "@PASSWORD", DbType.String, User.Password);
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, User.EmpID);
                db.AddInParameter(dbCmd, "@SECURITY_QUESTION_ID", DbType.Int32, User.SecurityQusId);
                db.AddInParameter(dbCmd, "@SECURITY_ANSWERS", DbType.String, User.SecurityQusAns);
                db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, User.FromDate);

                if (User.ToDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, User.ToDate);
                //db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, User.ToDate);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, User.UserId);
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
            return Result;
        }
        #endregion

        #region Get Security Question
        /// <summary>
        /// Gets Customer Type.
        /// </summary>
        /// <returns>Returns dataset contains Customer Type.</returns>
        public DataSet GetSecurityQuestion()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_USER_SECURITY_QUESTION_KEYVALUE);
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

        #endregion

        #region Get Assigned CompanyName By UserId
        /// <summary>
        /// Gets Customer Type.
        /// </summary>
        /// <returns>Returns dataset contains Customer Type.</returns>
        public DataSet GetAssignedCompanyList_ByUserID(int UserId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_USER_COMPANY_FRANCHIES_MAPPING_SELECT_BYUSERID);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, UserId);
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

        #endregion

        #region Get Assigned RoelNmae using UserId and companyid
        /// <summary>
        /// Gets Assign role .
        /// </summary>
        /// <returns>Returns dataset contains Role Name.</returns>
        public DataSet GetAssignedRoleList_ByUserID(int UserId,int CompanyId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_USER_ROLE_MAPPING_SELECT_ASSIGNED);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@COMPANY_FRANCHIES_ID", DbType.Int32, CompanyId); 
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

        #endregion

        #region Get Employee
        /// <summary>
        /// Gets Customer Type.
        /// </summary>
        /// <returns>Returns dataset contains Customer Type.</returns>
        public DataSet GetEmployeeKeyValue()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_EMPLOYEE_SELECT_KEYVALUE);
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

        #endregion

        #region Get Company By EmployeeId
        /// <summary>
        /// Gets Customer Type.
        /// </summary>
        /// <returns>Returns dataset contains Customer Type.</returns>
        public DataSet GetCompanyByEmployeeId(int EmployeeId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPANY_SELECT_BY_EMPLOYEE_ID);
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, EmployeeId);
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

        #endregion

        #region Insert CompanyList(Assigned)
        /// <summary>
        /// Insert Assign Company list with coma seprated value
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertAssignCompany(string CompanyId, int UserID, int CreatedBy)
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
            return Result;
        }
        #endregion

        #region UnAssigned CompanyList
        /// <summary>
        /// Insert Assign Company list with coma seprated value
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UnAssignCompany(string CompanyId, int UserID)
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
            return Result;
        }
        #endregion

        #region Insert RoleList(Assigned)
        /// <summary>
        /// Insert Assign Company list with coma seprated value
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertAssignRole(int UserId,int CompanyId,int RoleId)
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
            return Result;
        }
        #endregion

        #region UnAssigned CompanyList
        /// <summary>
        /// Insert Assign Company list with coma seprated value
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UnAssignRole(int UserID, int CompanyId,int RoleId)
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
            return Result;
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
    }
}
