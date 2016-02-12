using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.DataAccess.AdministrationDAL
{
   public class TransferPackageTimings
    {

       public DataSet GetAllTrasferpackage()
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_ALL_TRANSFER_PACKAGE_NAMES");
             
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

       public DataSet GetAllTrasferpackageDescription(int TRANSFER_PACKAGE_PRICE_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_ALL_TRANSFER_PACKAGE_FOR_FIT");
               db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_PRICE_ID", DbType.Int32, TRANSFER_PACKAGE_PRICE_ID);
               
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

       public DataSet GetAllTrasferpackageTimmings(int TRANSFER_PACKAGE_PRICE_ID, string FLAG, string SIC_PVT)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_TRANSFER_PACKAGE_TIME");
               db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_FROM_TO_DETAIL_ID", DbType.Int32, TRANSFER_PACKAGE_PRICE_ID);
               db.AddInParameter(dbCmd, "@FLAG", DbType.String, FLAG);
               db.AddInParameter(dbCmd, "@SIC_PVT", DbType.String, SIC_PVT);

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

       public DataSet GetAallTimings()
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("GET_ALL_TIMINGS");

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

       public void DeleteTimings(int TRANSFER_PACKAGE_FROM_ID, int TRANSFER_PACKAGE_TO_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
         //  DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("DELETE_TRANSFER_TIMING");
               db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_FROM_ID", DbType.Int32, TRANSFER_PACKAGE_FROM_ID);
               db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_TO_ID", DbType.Int32, TRANSFER_PACKAGE_TO_ID);
              

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

       public void InsetTimings(int TRANSFER_PACKAGE_FROM_ID, int TRANSFER_PACKAGE_TO_ID, String TRANSFER_PACKAGE_TIMING_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           //  DataSet ds = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_TRANSFER_TIMING");
               db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_FROM_ID", DbType.Int32, TRANSFER_PACKAGE_FROM_ID);
               db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_TO_ID", DbType.Int32, TRANSFER_PACKAGE_TO_ID);
               db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_TIMING_ID", DbType.String, TRANSFER_PACKAGE_TIMING_ID);

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
