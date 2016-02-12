using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.AdministrationDAL
{
    public class RoleGadgetMappingDal
    {
        #region Get AssignModule From RoleID
        /// <summary>
        /// Get AssignModule From RoleID
        /// </summary>
        /// <returns>Returns dataset contains module name data.</returns>
        public DataSet GetGadgetByRole(RoleGadgetMappingBDto GadgetBdto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ROLE_GADGET_MAPPING_SELECT_BYROLE);
                db.AddInParameter(dbCmd, "@ROLE_ID", DbType.Int32, GadgetBdto.RoleID);
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
        public int InsertAccess(RoleGadgetMappingBDto GadgetBdto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_GADGET_ACCESS_MAPPING_INSERT);
                db.AddInParameter(dbCmd, "@ROLE_ID", DbType.Int32, GadgetBdto.RoleID);
                db.AddInParameter(dbCmd, "@MODULE_ID", DbType.Int32, GadgetBdto.ModuleID);
                db.AddInParameter(dbCmd, "@GADGET_ID", DbType.Int32, GadgetBdto.GadgetID);
                db.AddInParameter(dbCmd, "@READ_ACCESS ", DbType.Boolean, GadgetBdto.ReadAccess);
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
    }
}
