using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.GIT
{
   public class GalaDinnerTypeMasterDal
    {
        // Insert into Conference Type Time Mapping 
        public void insertGalaDinnerTypeMaster(string GalaDinnerTypeid, string GalaDinnerTypeName, string userID)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GALA_DINNER_TYPE_MASTER");
                db.AddInParameter(dbCmd, "@GALA_DINNER_TYPE_ID", DbType.Int32, int.Parse(GalaDinnerTypeid));
                db.AddInParameter(dbCmd, "@GALA_DINNER_TYPE", DbType.String, GalaDinnerTypeName);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, int.Parse(userID));
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
        public DataSet bindGalaDinnerTypeGrid()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_GALA_DINNER_TYPE");

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
        public DataSet editGalaDinnerType(String GalaDinnerTypeid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_GALA_DINNER_TYPE_FOR_EDIT");
                db.AddInParameter(dbCmd, "@GALA_DINNER_TYPE_ID", DbType.Int32, int.Parse(GalaDinnerTypeid));

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
        public DataSet deleteGalaDinnerType(String GalaDinnerTypeid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_GALA_DINNER_TYPE");
                db.AddInParameter(dbCmd, "@GALA_DINNER_TYPE_ID", DbType.Int32, int.Parse(GalaDinnerTypeid));

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

        public DataSet FetchCountForValidation(string Conf)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_DATA_FROM_GALA_DINNER_TYPE_MASTER_FOR_VALIDATION");
                db.AddInParameter(dbCmd, "@GALA_DINNER", DbType.String, Conf);


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
