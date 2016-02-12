using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;

namespace CRM.DataAccess.AdministratorEntity
{
    public class FitPackageStoredProcedure
    {
        public void InsertUpdateFitPackage(ArrayList FIT)
        {
            Database db = null;
            DbCommand dbCmd = null;
            Boolean flag;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_FIT_PACKAGE");
                db.AddInParameter(dbCmd, "@FIT_PACKAGE_ID", DbType.Int32, FIT[0]);
                db.AddInParameter(dbCmd, "@FIT_PACKAGE_NAME", DbType.String, FIT[1]);
                db.AddInParameter(dbCmd, "@PACKAGE_ORDER", DbType.Int32,Convert.ToInt32(FIT[2]));
                if (Convert.ToString(FIT[3]).Equals("YES"))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                db.AddInParameter(dbCmd, "@IS_VISIBLE", DbType.Boolean, flag);
                db.AddInParameter(dbCmd, "@PACKAGE_MARGIN", DbType.Decimal, Convert.ToDecimal(FIT[4]));
                db.AddInParameter(dbCmd, "@SURCHARGE", DbType.Decimal, Convert.ToDecimal(FIT[5]));
                db.AddInParameter(dbCmd, "@MINIMUM_NIGHTS", DbType.Int32, FIT[6]);

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
        public void delFitPackage(int delfit)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_FIT_PACKAGE");
                db.AddInParameter(dbCmd, "@FIT_PACKAGE_ID", DbType.Int32, delfit);
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
        public void InsertUpdateFitPackageCityMapping(ArrayList city)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FIT_CITY_MAPPING");
                db.AddInParameter(dbCmd, "@FIT_PACKAGE_CITY_ID", DbType.Int32, city[0]);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, city[1]);
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
        public void InsertNewCity(string FIT_PACKAGE_CITY_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_CITY_FOR_FIT_PACKAGE_MASTER");
                db.AddInParameter(dbCmd, "@FIT_PACKAGE_CITY_ID", DbType.Int32, Convert.ToInt32(FIT_PACKAGE_CITY_ID));
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
        public void  deleteCity(int delCity)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_CITY_FOR_FIT_PACKAGE_CITY_MAPPING");
                db.AddInParameter(dbCmd, "@FIT_PACKAGE_CITY_ID", DbType.Int32, delCity);
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
