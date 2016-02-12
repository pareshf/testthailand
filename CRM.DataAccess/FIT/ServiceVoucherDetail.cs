using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.FIT
{
    public class ServiceVoucherDetail
    {
        public DataSet GetTime(String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(param);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        public DataSet GetAgent(String param1)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(param1);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        public DataSet GetCity(String city)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(city);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet GetCityWiseSightSeeing(string sp_name,String cityname)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@CITY", DbType.String, cityname);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        #region SIGHT SEEING
        public DataTable fetchsightseeing(String from_date, String to_date, String city)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_SIGHT_SEEING_FOR_FIT");

                if (from_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(from_date.ToString(), "dd/MM/yyyy", null));
                }

                if (to_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@CITY", DbType.String, city);
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
            return ds.Tables[0];
        }
        #endregion

        public DataSet InsertSightSeeingVoucher(int id, String clintname, String agent, String date, String picktime, String sicpvt, String sightname, String city, String vno, int userid, String invoiceno,int quoteid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SIGHT_SEEING_VOUCHER");
                db.AddInParameter(dbCmd, "@SERVICE_VOUCHER_ID", DbType.Int32, id);
                db.AddInParameter(dbCmd, "@CLIENT_NAME", DbType.String, clintname);
                db.AddInParameter(dbCmd, "@AGENT", DbType.String, agent);
                db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
                db.AddInParameter(dbCmd, "@PICK_TIME", DbType.String, picktime);
                db.AddInParameter(dbCmd, "@SICPVT_FLAG", DbType.String, sicpvt);
                db.AddInParameter(dbCmd, "@SIGHT_SEEEING_NAME", DbType.String, sightname);
                db.AddInParameter(dbCmd, "@CITY", DbType.String, city);
                db.AddInParameter(dbCmd, "@VOUCHER_NO", DbType.String, vno);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, userid);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoiceno);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quoteid);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch(Exception ex)
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

        // voucher invoice
        public DataSet fetch_common_data(String sp_name, String invoice_no, String city)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);

                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, city);

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

        public DataSet fetch_transfer_package(String sp_name, String invoice_no, String supplier_name, String city)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
                db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, supplier_name);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, city);
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
