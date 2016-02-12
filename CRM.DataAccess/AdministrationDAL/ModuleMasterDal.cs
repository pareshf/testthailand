using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;

using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.ComponentModel;


namespace CRM.DataAccess.AdministrationDAL
{
    public class ModuleMasterDal : IDisposable
    {

        //#region Get Modules
        //public DataSet GetModuleList()
        //{

        //    Database db = null;
        //    DbCommand dbcmdModule = null;
        //    DataSet dsModule = null;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("CRM");
        //        dbcmdModule = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_MODULEMASTER_SELECT);
        //        dsModule = db.ExecuteDataSet(dbcmdModule);
        //        return dsModule;
        //    }
        //    catch (Exception ex)
        //    {
        //        bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
        //        if (rethrow)
        //        {
        //        throw ex;
        //        }
        //        return null;
        //    }
        //    finally
        //    {
                
        //        DALHelper.Destroy(ref dbcmdModule);
        //    }
           
        //}
        //#endregion

        //#region Insert Module
        //public bool InsertModule(LookupBDto objLookupBDto)
        //{
        //    int Status = 0;
        //    Database db = null;
        //    DbCommand dbcmdModule = null;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("CRM");

        //        dbcmdModule = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_MODULEMASTER_CREATE);
        //        db.AddInParameter(dbcmdModule, "@MODULE_NAME", DbType.String, objLookupBDto.LookupName);
        //        db.AddInParameter(dbcmdModule, "@SORT_ORDER", DbType.Int32, objLookupBDto.Int_order);
        //        db.AddInParameter(dbcmdModule, "@USER_ID", DbType.Int32, objLookupBDto.UserProfile.UserId);
        //        db.AddOutParameter(dbcmdModule, "@IS_INSERT", DbType.Int32, Int32.MaxValue);
        //        db.ExecuteNonQuery(dbcmdModule);
        //        if (db.GetParameterValue(dbcmdModule, "@IS_INSERT") != DBNull.Value)
        //            Status = Convert.ToInt32(db.GetParameterValue(dbcmdModule, "@IS_INSERT"));
        //        if (Status == 1)
        //            return true; // SUCCESSFUL INSERTION RETURN TRUE
        //        else
        //            return false; // UNSUCCESSFUL INSERTION RETUN FALSE ( ALREADY EXISTS )
        //    }
        //    catch (Exception ex)
        //    {
        //        bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
        //        if (rethrow)
        //        {
        //            throw ex;
        //        }
        //    }
        //    finally
        //    {
        //        DALHelper.Destroy(ref dbcmdModule);
        //    }
        //    return false;
        //}
        //#endregion

        //#region Update Module Master

        //public int UpdateModuleMaster(String xmlData)
        //{
        //    Database db = null;
        //    DbCommand dbCmd = null;
        //    int Result = 0;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
        //        dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_MODULEMASTER_UPDATE);
        //        db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml, xmlData);
        //        db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
        //        db.ExecuteNonQuery(dbCmd);
        //        Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
        //    }
        //    catch (Exception ex)
        //    {
        //        bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
        //        if (rethrow)
        //        {
        //            throw ex;
        //        }
        //    }
        //    finally
        //    {
        //        DALHelper.Destroy(ref dbCmd);
        //    }
        //    return Result;
        //}
        //#endregion


        //#region Delete Module

        //public int DeleteModule(string IdLists)
        //{
        //    Database db = null;
        //    DbCommand dbcmdModule = null;
        //    try
        //    {

        //        db = DatabaseFactory.CreateDatabase("CRM");
        //        dbcmdModule = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_MODULEMASTER_DELETE);
        //        db.AddInParameter(dbcmdModule, "@MODULE_IDS", DbType.String, IdLists);
        //        db.AddOutParameter(dbcmdModule, "@IS_DELETE", DbType.Int32, Int32.MaxValue);
        //        db.ExecuteNonQuery(dbcmdModule);
        //        if (db.GetParameterValue(dbcmdModule, "@IS_DELETE") != DBNull.Value)
        //        {
        //            return Convert.ToInt32(db.GetParameterValue(dbcmdModule, "@IS_DELETE"));
        //        }
        //        else
        //        { return 0; }
        //    }
        //    catch (Exception ex)
        //    {
        //        bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
        //        if (rethrow)
        //        {
        //            throw ex;
        //        }
        //        return 0;
        //    }
        //    finally
        //    {
        //        DALHelper.Destroy(ref dbcmdModule);
        //    }
        //}
        //#endregion
        #region IDisposable Members

        public void Dispose()
        {
            GC.Collect();
        }

        #endregion
    }

    
}
