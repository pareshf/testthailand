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
   public class TransferPackageToFromMaterDal
    {
        // Insert into Transfer Package Time Mapping 
        public void insertTransferPackageToFrom(string Transferid, string TransferName)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_INTO_TRANSFER_PACKAGE_TO_FROM_MASTER");
                db.AddInParameter(dbCmd, "@TRANSFER_ID", DbType.Int32, int.Parse(Transferid));
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_NAME", DbType.String, TransferName);
              
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
        public DataSet bindTransferNameGrid()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_TRANSFER_PACKAGE_NAME");

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

        #region Fetch Data For Edit The Record
        public DataSet editTransferName(String transferid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_TRANSFER_PACKAGE_NAME_FOR_EDIT");
                db.AddInParameter(dbCmd, "@TRANSFER_ID", DbType.Int32, int.Parse(transferid));

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
        #endregion

        #region Delete Record
        public DataSet deleteTransferName(String transferid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_TRANSFER_PACKAGE_NAME");
                db.AddInParameter(dbCmd, "@TRANSFER_ID", DbType.Int32, int.Parse(transferid));

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
        #endregion
    }
}
