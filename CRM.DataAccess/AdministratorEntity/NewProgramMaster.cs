using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;

namespace CRM.DataAccess.AdministratorEntity
{
    public class NewProgramMaster
    {
        public void InsertUpdateMyProgram(ArrayList Program)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_PROGRAM_MASTER");
                db.AddInParameter(dbCmd, "@PROGRAM_ID", DbType.Int32, Program[0]);
                db.AddInParameter(dbCmd, "@MODULE_NAME1", DbType.String, Program[1]);
                db.AddInParameter(dbCmd, "@PROGRAM_TYPE_NAME1", DbType.String, Program[2]);
                db.AddInParameter(dbCmd, "@PROGRAM_NAME1", DbType.String, Program[3]);
                db.AddInParameter(dbCmd, "@PROGRAM_TEXT1", DbType.String, Program[4]);
                db.AddInParameter(dbCmd, "@PROGRAM_ACCESS_KEY1", DbType.String, Program[5]);
                db.AddInParameter(dbCmd, "@PROGRAM_SORT", DbType.Int32, Program[6]);
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
        public void DeleteMyProgram(int ProgramId)
        {

            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_PROGRAM_MASTER");
                db.AddInParameter(dbCmd, "@PROGRAMID", DbType.Int32, ProgramId);
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
        public void InsertUpdateSubProgram(ArrayList SubProgram)
        {

            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SUBPROGRAM_MASTER");
                //db.AddInParameter(dbCmd, "@PROGRAM_SUB_ID", DbType.Int32, SubProgram[0]);
                db.AddInParameter(dbCmd, "@PROGRAM_TEXT", DbType.String, SubProgram[3]);
                db.AddInParameter(dbCmd, "@PARENT_ID",DbType.String,SubProgram[2]);
                db.AddInParameter(dbCmd, "@PROGRAM_SUB_NAME", DbType.String, SubProgram[1]);
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


        public void InsertUpdateProgAccess(ArrayList ary)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_ROLE_ACCESS_MAPPING_INSERT");
                db.AddInParameter(dbCmd, "@ROLE_ID", DbType.Int32, Convert.ToInt32(ary[1]));
                db.AddInParameter(dbCmd, "@PROGRAM_ID", DbType.String, ary[2]);
                db.AddInParameter(dbCmd, "@DEPT_ID", DbType.Int32, Convert.ToInt32(ary[0]));
                db.AddInParameter(dbCmd, "@COMP_ID", DbType.Int32, Convert.ToInt32(ary[10]));
                db.AddInParameter(dbCmd, "@CUST_DISCOUNT_L", DbType.Decimal, Convert.ToDecimal(ary[7]));
                db.AddInParameter(dbCmd, "@CUST_DISCOUNT_H", DbType.Decimal, Convert.ToDecimal(ary[8]));
                if (ary[3].Equals("T"))
                {
                    db.AddInParameter(dbCmd, "@READ_ACCESS", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@READ_ACCESS", DbType.Boolean, false);
                }
                if (ary[4].Equals("T"))
                {
                    db.AddInParameter(dbCmd, "@WRITE_ACCESS", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@WRITE_ACCESS", DbType.Boolean, false);
                }
                if (ary[5].Equals("T"))
                {
                    db.AddInParameter(dbCmd, "@DELETE_ACCESS", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DELETE_ACCESS", DbType.Boolean, false);
                }
                if (ary[6].Equals("T"))
                {
                    db.AddInParameter(dbCmd, "@PRINT_ACCESS", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PRINT_ACCESS", DbType.Boolean, false);
                }
                if (ary[9].Equals("T"))
                {
                    db.AddInParameter(dbCmd, "@CUST_TYPE", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CUST_TYPE", DbType.Boolean, false);
                }
                if (ary[11].Equals("T"))
                {
                    db.AddInParameter(dbCmd, "@CHANGE_CUST_OWNER", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHANGE_CUST_OWNER", DbType.Boolean, false);
                }
                db.AddInParameter(dbCmd, "@VISA_DIS", DbType.Decimal, Convert.ToDecimal(ary[12]));
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

        public void InsertUpdateSubProgAccess(ArrayList ary)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_ROLE_ACCESS_MAPPING_SUBPROGRAM_INSERT");
                db.AddInParameter(dbCmd, "@ROLE_ID", DbType.Int32, Convert.ToInt32(ary[1]));
                db.AddInParameter(dbCmd, "@SUB_PROGRAM_ID", DbType.String, ary[2]);
                db.AddInParameter(dbCmd, "@DEPT_ID", DbType.Int32, Convert.ToInt32(ary[0]));
                db.AddInParameter(dbCmd, "@COMP_ID", DbType.Int32, Convert.ToInt32(ary[7]));
                if (ary[3].Equals("T"))
                {
                    db.AddInParameter(dbCmd, "@READ_ACCESS", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@READ_ACCESS", DbType.Boolean, false);
                }
                if (ary[4].Equals("T"))
                {
                    db.AddInParameter(dbCmd, "@WRITE_ACCESS", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@WRITE_ACCESS", DbType.Boolean, false);
                }
                if (ary[5].Equals("T"))
                {
                    db.AddInParameter(dbCmd, "@DELETE_ACCESS", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DELETE_ACCESS", DbType.Boolean, false);
                }
                if (ary[6].Equals("T"))
                {
                    db.AddInParameter(dbCmd, "@PRINT_ACCESS", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PRINT_ACCESS", DbType.Boolean, false);
                }
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

        public void InsertNewProgramAccess(string dept_id, string role_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("InsertNewRoleAccessMapping");
                db.AddInParameter(dbCmd, "@ROLE_ID", DbType.Int32, Convert.ToInt32(role_id));
                db.AddInParameter(dbCmd, "@DEPT_ID", DbType.Int32, Convert.ToInt32(dept_id));
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

        public void InsertNewSubProgramAccess(string dept_id, string role_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("InsertNewRoleAccessMappingSubProg");
                db.AddInParameter(dbCmd, "@ROLE_ID", DbType.Int32, Convert.ToInt32(role_id));
                db.AddInParameter(dbCmd, "@DEPT_ID", DbType.Int32, Convert.ToInt32(dept_id));
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

        public int InsertNewDeptRole(string dept, string role,string comp)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int i = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_DEPT_AND_ROLE");
                db.AddInParameter(dbCmd, "@COMP_NAME", DbType.String, comp);
                db.AddInParameter(dbCmd, "@DEPT_NAME", DbType.String, dept);
                db.AddInParameter(dbCmd, "@ROLE_NAME", DbType.String, role);
                db.AddInParameter(dbCmd, "@SUCCESS", DbType.Int32,0);
                db.ExecuteNonQuery(dbCmd);
                i = Convert.ToInt32(db.GetParameterValue(dbCmd, "@SUCCESS"));

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
            return i;
           
        }

        public void DeleteSubProgram(int SubProgramId)
        {

            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DeleteSubProgram");
                db.AddInParameter(dbCmd, "@SUB_PROG_ID", DbType.Int32, SubProgramId);
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
