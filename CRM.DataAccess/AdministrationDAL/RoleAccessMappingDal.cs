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
    public class RoleAccessMappingDal
    {
        RoleAccessMappingBDto objRoleAccessMappingBDto = null;
        #region Get AssignModule From RoleID
        /// <summary>
        /// Get AssignModule From RoleID
        /// </summary>
        /// <returns>Returns dataset contains module name data.</returns>
        public DataSet GetModuleByRole(int RoleId,int DeptId,int CompId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_MODULE_MASTER_SELECT_BYROLE);
                db.AddInParameter(dbCmd, "@ROLE_ID", DbType.Int32, RoleId);
                db.AddInParameter(dbCmd, "@DEPT_ID", DbType.Int32, DeptId);
                db.AddInParameter(dbCmd, "@COMP_ID", DbType.Int32, CompId);
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

        #region Bind Role Access grid by Role and Module
        /// <summary>
        /// Get Bind Role Access grid by Role and Module
        /// </summary>
        /// <returns>Returns dataset contains Role Access data.</returns>
        public DataSet GetAccessGrid(RoleAccessMappingBDto AccessBdto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            objRoleAccessMappingBDto = new RoleAccessMappingBDto();
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ROLE_ACCESS_MAPPING_SELECT_BYROLE);
                db.AddInParameter(dbCmd, "@ROLE_ID", DbType.Int32, AccessBdto.RoleID);
                db.AddInParameter(dbCmd, "@MODULE_ID", DbType.Int32, AccessBdto.ModuleID);
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

        #region Insert Role
        /// <summary>
        /// Insert Role detail.
        /// </summary>
        /// <param name="object">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertAccess(RoleAccessMappingBDto RoleAccess)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ROLE_ACCESS_MAPPING_INSERT);
                db.AddInParameter(dbCmd, "@ROLE_ID", DbType.Int32, RoleAccess.RoleID);
                db.AddInParameter(dbCmd, "@PROGRAM_ID", DbType.Int32, RoleAccess.ProgramID);
                db.AddInParameter(dbCmd, "@READ_ACCESS", DbType.Boolean, RoleAccess.ReadAccess);
                db.AddInParameter(dbCmd, "@WRITE_ACCESS", DbType.Boolean, RoleAccess.WriteAccess);
                db.AddInParameter(dbCmd, "@DELETE_ACCESS", DbType.Boolean, RoleAccess.DeleteAccess);
                db.AddInParameter(dbCmd, "@PRINT_ACCESS", DbType.Boolean, RoleAccess.PrintAccess);
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

        #region Delete Role(Deallocate Module)
        /// <summary>
        /// Deleue Role detail.
        /// </summary>
        /// <param name="object">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteAccess(RoleAccessMappingBDto RoleAccess)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ROLE_ACCESS_MAPPING_DELETE);
                db.AddInParameter(dbCmd, "@ROLE_ID", DbType.Int32, RoleAccess.RoleID);
                db.AddInParameter(dbCmd, "@MODULE_ID", DbType.Int32, RoleAccess.ModuleID);
                db.AddOutParameter(dbCmd, "@IS_DELETE", DbType.Int32, 1);
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
    }
}
