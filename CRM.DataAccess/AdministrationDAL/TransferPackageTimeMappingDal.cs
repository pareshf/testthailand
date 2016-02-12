using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.AdministrationDAL
{
    public class TransferPackageTimeMappingDal
    {
        #region fetch combo data

        public DataSet fetchComboData(String sp_name)
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
                throw ex;
            }

            return dsData;
        }

       

        #endregion

        // Insert into Transfer Package Time Mapping 
        public void insertTransferPackageTimmingMapping(string from,string to,string time)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_INTO_TRANSFER_PACKAGE_TIME_MAPPING");
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_FROM", DbType.String, from);
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_TO", DbType.String, to);
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_TIMING", DbType.String, time);

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

        // Bind Grid  
        public DataSet bindTransferTimmingGrid()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_TRANSFER_PACKAGE_TIMING_MAPPING");

                //Command Execute
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

            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return dsData;
        }

        public DataSet bindSelectedTransferTimmingGrid(string from,string to)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_TRANSFER_PACKAGE_TIMING_MAPPING");
                db.AddInParameter(dbCmd, "@from", DbType.String, from);
                db.AddInParameter(dbCmd, "@to", DbType.String, to);
                //Command Execute
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

            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return dsData;
        }

        // Delete Transfer Time 
        public DataSet deleteTransferTime(String from,String to)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_TRANSFER_PACKAGE_TIMING_MAPPING");
                db.AddInParameter(dbCmd, "@from", DbType.String, from);
                db.AddInParameter(dbCmd, "@to", DbType.String, to);
                
                //Command Execute
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

            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return dsData;
        }
    }
}
