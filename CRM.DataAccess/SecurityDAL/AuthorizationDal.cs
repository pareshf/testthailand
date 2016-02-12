using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.SecurityDAL
{
    public class AuthorizationDal
    {

        public DataSet ValidateSystemLogin(string username, string password)
        {
            Database db = null;
            DbCommand dbcmd = null;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbcmd = db.GetStoredProcCommand(DALHelper.USP_SYS_VALIDATE_LOGIN);
                db.AddInParameter(dbcmd, "@USER_NAME", DbType.String, username);
                db.AddInParameter(dbcmd, "@USER_PASSWORD", DbType.String, password);
                ds = db.ExecuteDataSet(dbcmd);
                return ds;
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
                DALHelper.Destroy(ref dbcmd);
            }
            return null;
        }

        public DataSet GetPageOperationAccess(int userId, int roleId, int programId)
        {
            Database db = null;
            DbCommand dbcmd = null;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase("CRM");
                //dbcmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_TITLE_FETCH);

                db.AddInParameter(dbcmd, "@UserId", DbType.Int32, userId);
                db.AddInParameter(dbcmd, "@RoleId", DbType.Int32, roleId);
                db.AddInParameter(dbcmd, "@ProgramId", DbType.Int32, programId);
                ds = db.ExecuteDataSet(dbcmd);
                return ds;
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
                DALHelper.Destroy(ref dbcmd);
            }
            return null;
        }

        #region Check UserAuthorization For Program
        public DataSet GetProgrameAccessByProgramName(int RoleId, string ProgramName)
        {

            Database db = null;
            DbCommand dbcmd = null;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbcmd = db.GetStoredProcCommand(DALHelper.USP_ROLE_ACCESS_MAPPING_SELECT_BY_PROGRAM_NAME);

                db.AddInParameter(dbcmd, "@ROLE_ID", DbType.Int32, RoleId);
                db.AddInParameter(dbcmd, "@PROGRAM_TEXT", DbType.String, ProgramName);
                ds = db.ExecuteDataSet(dbcmd);
                return ds;
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
                DALHelper.Destroy(ref dbcmd);
            }
            return null;
        }
        #endregion

        #region Page Preference
        public DataSet PreferenceGridByUserId(int UserId)
        {
            Database db = null;
            DbCommand dbcmd = null;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbcmd = db.GetStoredProcCommand(DALHelper.USP_USER_ROLE_MAPPING_SELECT_BY_USERID);
                db.AddInParameter(dbcmd, "@USER_ID", DbType.Int32, UserId);
                ds = db.ExecuteDataSet(dbcmd);
                return ds;
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
                DALHelper.Destroy(ref dbcmd);
            }
            return null;
        }

        public int UpdateUserDefaultPreference(int userId, int CompanyId, int RoleId, int DeptId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_UPDATE_USER_ROLE_MAPPING_DEFAULT_PREFERENCE);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userId);
                db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, CompanyId);
                db.AddInParameter(dbCmd, "@ROLE_ID", DbType.Int32, RoleId);
                db.AddOutParameter(dbCmd, "@IS_RESULT", DbType.Int32, 0);
                db.AddInParameter(dbCmd, "@DEPT_ID", DbType.Int32, DeptId);
                db.ExecuteNonQuery(dbCmd);
                result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_RESULT"));

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
            return result;
        }
        #endregion

        #region Module Panel (Module Name By Role Id)
        public DataSet FetchModule(int RoleId,int DeptId,int CompId)
        {
            Database db = null;
            DbCommand dbcmd = null;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbcmd = db.GetStoredProcCommand(DALHelper.USP_ROLE_ACCESS_MAIN_MODULE_SELECT_BYROLE);
                db.AddInParameter(dbcmd, "@ROLE_ID", DbType.String, RoleId);
                db.AddInParameter(dbcmd, "@DEPT_ID", DbType.String, DeptId);
                db.AddInParameter(dbcmd, "@COMP_ID", DbType.String, CompId);
                ds = db.ExecuteDataSet(dbcmd);
                return ds;
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
                DALHelper.Destroy(ref dbcmd);
            }
            return null;
        }
        #endregion

        #region Navigation Panel (Menu Item By Role Id)
        public DataSet FetchMenuProgram(int ModuleId, int RoleId,int typeid,int CompId,int DeptId)
        {
            Database db = null;
            DbCommand dbcmd = null;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbcmd = db.GetStoredProcCommand("USP_ROLE_ACCESS_MAPPING_SELECT_LEFTMENU");
                db.AddInParameter(dbcmd, "@ROLE_ID", DbType.String, RoleId);
                db.AddInParameter(dbcmd, "@MODULE_ID", DbType.String, ModuleId);
                db.AddInParameter(dbcmd, "@TYPE_ID", DbType.Int32, typeid);
                db.AddInParameter(dbcmd, "@COMP_ID", DbType.Int32, CompId);
                db.AddInParameter(dbcmd, "@DEPT_ID", DbType.Int32, DeptId);
                ds = db.ExecuteDataSet(dbcmd);
                return ds;
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
                DALHelper.Destroy(ref dbcmd);
            }
            return null;
        }

        public DataSet FetchMenuSubProgram(int ProgramId)
        {
            Database db = null;
            DbCommand dbcmd = null;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbcmd = db.GetStoredProcCommand("USP_ROLE_ACCESS_MAPPING_SELECT_SUB_LEFTMENU");
                db.AddInParameter(dbcmd, "@PROGRAM_ID", DbType.String, ProgramId);
                ds = db.ExecuteDataSet(dbcmd);
                return ds;
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
                DALHelper.Destroy(ref dbcmd);
            }
            return null;
        }
        #endregion

        #region Theme
        public void SetTheme(int userId, String ThemeName)
        {
            Database db = null;
            DbCommand dbcmd = null;
            int result;

            try
            {
                db = DatabaseFactory.CreateDatabase("CRM");
                dbcmd = db.GetStoredProcCommand("[dbo].USP_SET_USER_THEME");

                db.AddInParameter(dbcmd, "@USER_ID", DbType.Int32, userId);
                db.AddInParameter(dbcmd, "@THEME_NAME", DbType.String, ThemeName);

                result = db.ExecuteNonQuery(dbcmd);

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
                DALHelper.Destroy(ref dbcmd);
            }

        }
        #endregion

        #region Forgot Password
        public DataTable CheckEmailId(string emailId, string userName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_SYS_USER_MASTER_FATCH_EMAILID);
                if (!String.IsNullOrEmpty(emailId))
                {
                    db.AddInParameter(dbCmd, "@EMP_EMAIL", DbType.String, emailId);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EMP_EMAIL", DbType.String, DBNull.Value);
                }

                if (!String.IsNullOrEmpty(userName))
                {
                    db.AddInParameter(dbCmd, "@USER_NAME", DbType.String, userName);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@USER_NAME", DbType.String, DBNull.Value);
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
            return ds.Tables[0];



        }

        /// <summary>
        /// Reset new generated password to perticular user.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="password">Password</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int ResetUserPassword(int userId, string password)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_SYS_USER_PASSWORD_UPDATE);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userId);
                db.AddInParameter(dbCmd, "@PASSWORD", DbType.String, password);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));

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
            return result;
        }

        //public DataTable SetPassword(string username, string password)
        //{
        //    try
        //    {
        //        DbCommand cmd = dataBase.GetStoredProcCommand("dbo.SP_UPDATE_SYS_PASSWORD_WIZARD");
        //        dataBase.AddInParameter(cmd, "@USER_NAME", DbType.String, username);
        //        dataBase.AddInParameter(cmd, "@USER_PASSWORD", DbType.String, password);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        DataSet ds = dataBase.ExecuteDataSet(cmd);
        //        return ds.Tables[0];
        //    }
        //    catch (Exception ex) { return null; }
        //}

        #endregion

        #region User Login Log
        public int InsertLoginLog(int userId, string IpAddress, bool IsSucess)
        {
            Database db = null;
            DbCommand dbcmd = null;
            int Result;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbcmd = db.GetStoredProcCommand(DALHelper.USP_SYS_USER_LOGIN_LOG_INSERT);
                db.AddInParameter(dbcmd, "@USER_ID", DbType.Int32, userId);
                db.AddInParameter(dbcmd, "@IP_ADDRESS", DbType.String, IpAddress);
                db.AddInParameter(dbcmd, "@IS_SUCCESS", DbType.Boolean, IsSucess);
                Result = db.ExecuteNonQuery(dbcmd);
                return Result;
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
                DALHelper.Destroy(ref dbcmd);
            }
            return 0;
        }
        #endregion

		#region for online user by sunil

		public DataSet GetOnlineUsers(int USERID,string LOGINDATE)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_SYS_ONLINE_USER);
				db.AddInParameter(dbCmd, "@DATE", DbType.String, LOGINDATE);
				db.AddInParameter(dbCmd, "@USERID", DbType.String, USERID);
				
				
				ds = db.ExecuteDataSet(dbCmd);
				return ds;
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
		public DataSet GetSignOutUsers(int USERID, string LOGINDATE)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_SYS_SIGNOUT_USER);
				db.AddInParameter(dbCmd, "@DATE", DbType.String, LOGINDATE);
				db.AddInParameter(dbCmd, "@USERID", DbType.String, USERID);
				ds = db.ExecuteDataSet(dbCmd);
				return ds;
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

        #region Page Access Rights By Ashish

        public DataTable GetPageRights(int Comp_Id, int Dept_Id, int Role_Id, int Prog_Name)
        { 
            Database db = null;
            DbCommand dbcmd = null;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbcmd = db.GetStoredProcCommand("FETCH_PAGE_RIGHTS");

                db.AddInParameter(dbcmd, "@COMP_ID", DbType.Int32, Comp_Id);
                db.AddInParameter(dbcmd, "@DEPT_ID", DbType.Int32, Dept_Id);
                db.AddInParameter(dbcmd, "@ROLE_ID", DbType.Int32, Role_Id);
                db.AddInParameter(dbcmd, "@PROG_ID", DbType.Int32, Prog_Name);
                ds = db.ExecuteDataSet(dbcmd);
                return ds.Tables[0];
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
                DALHelper.Destroy(ref dbcmd);
            }
            return null;
        }

        #endregion


        public DataSet GetUsertype(int CUST_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_SYS_GET_USERTYPE);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.String, CUST_ID);
                ds = db.ExecuteDataSet(dbCmd);
                return ds;
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
