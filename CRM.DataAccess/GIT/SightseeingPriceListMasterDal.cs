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
    public class SightseeingPriceListMasterDal
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

        public DataSet FetchCity(String ChainName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_CITY_NAME_FROM_CHAIN_NAME");
                db.AddInParameter(dbCmd, "@CHAIN_NAME", DbType.String, ChainName);


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

        public DataSet fetchSightSeeingPriceListDataForEdit(int Sightid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_ALL_SIGHT_SEEING_PRICE_LIST_DETAILS_FOR_EDIT");
                db.AddInParameter(dbCmd, "@SIGHT_SEEING_PRICE_ID", DbType.String, Sightid);

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

        // Insert into Hotel Price List Master
        public void insertSightSeeingPriceListMaster(int SightseeingPricelistId, string suppliersrno,string Packagename,string adultrate,string childrate,string ismeal,string status,string currency,string city)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_INTO_SIGHT_SEEING_PRICE_LIST_MASTER_MASTER");
                db.AddInParameter(dbCmd, "@SIGHT_SEEING_PRICE_ID", DbType.String, SightseeingPricelistId);
                db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, suppliersrno);
                db.AddInParameter(dbCmd, "@SIGHT_SEEING_PACKAGE_NAME", DbType.String, Packagename);
                db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, decimal.Parse(adultrate));
                db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, decimal.Parse(childrate));
                db.AddInParameter(dbCmd, "@IS_MEAL_APPLICABLE", DbType.String,ismeal);
                db.AddInParameter(dbCmd, "@PRICE_LIST_STATUS_ID", DbType.String,status);
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.String, currency);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.String, city);
               
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

        public DataSet FetchCountForValidation(string PackageName,int sightid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_DATA_FROM_SIGHT_SEEING_PRICE_LIST_FOR_VALIDATION");
                db.AddInParameter(dbCmd, "@SIGHT_SEEING_PACKAGE_NAME", DbType.String, PackageName);
                db.AddInParameter(dbCmd, "@SIGHT_ID", DbType.Int32, sightid);

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

        public DataSet FetchChainNameandCity(int Supplierid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_SUPPLIER_NAME_AND_CITY_NAME");
                db.AddInParameter(dbCmd, "@SUPPLIER_ID", DbType.String, Supplierid);


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
