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
    public class TransferServiceDetail
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
        public DataSet GetTransferPackage(String packagename)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(packagename);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        public DataSet GetPlaceDetail(string sp_name, String package)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@PACKAGE_NAME", DbType.String, package);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
        public DataSet GetPlacename(string sp_name)
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

            }

            return dsData;
        }
        public DataSet GetFlag(string sp_name)
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

            }

            return dsData;
        }
        public DataSet InsertTransferService(int id,String clientname,String agent,String date,String package,String picktime,String sicpvt,String frompalce,String toplace,String ad,String vno,int userid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_TRANSFER_SERVICE_VOUCHER");
                db.AddInParameter(dbCmd, "@TRANSFER_DETAIL_ID", DbType.Int32, id);
                
                db.AddInParameter(dbCmd, "@AGENT", DbType.String, agent);
                db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE", DbType.String, package);
                db.AddInParameter(dbCmd, "@PICK_TIME", DbType.String, picktime);
                db.AddInParameter(dbCmd, "@SIC_PVT_FLAG", DbType.String, sicpvt);
                db.AddInParameter(dbCmd, "@FROM_PLACE", DbType.String, frompalce);
                db.AddInParameter(dbCmd, "@TO_PLACE", DbType.String, toplace);
                db.AddInParameter(dbCmd, "@FLAGE", DbType.String, ad);
                db.AddInParameter(dbCmd, "@VOUCHER_NO", DbType.String, vno);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, userid);
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


        #region FETCH TRANSFER PACKAGE

        public DataTable fetchTransferPackage(String packid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_TRANSFER_PACKAGE_FOR_TRANSFER_VOUCHER");
                db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, packid);
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
        #region get_time_transfer

        public DataSet transfer_gettime(String sp_name, int tp_form_to_id, String flg, String sic_pvt)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);

                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_FROM_TO_DETAIL_ID", DbType.Int32, tp_form_to_id);
                db.AddInParameter(dbCmd, "@FLAG", DbType.String, flg);
                db.AddInParameter(dbCmd, "@SIC_PVT", DbType.String, sic_pvt);
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

        public DataSet InsertSightSeeingVoucher(int id, String clintname, String agent, String date, String picktime, String sicpvt, int sightname, String DETAILID, String vno, int userid, String invoiceno, int quoteid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_TRANSFER_SERVICE_VOUCHER");
                db.AddInParameter(dbCmd, "@TRANSFER_SERVICE_VOUCHER_ID", DbType.Int32, id);
                db.AddInParameter(dbCmd, "@CLIENT_NAME", DbType.String, clintname);
                db.AddInParameter(dbCmd, "@AGENT", DbType.String, agent);
                db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
                db.AddInParameter(dbCmd, "@PICK_TIME", DbType.String, picktime);
                db.AddInParameter(dbCmd, "@SIC_PVT_FLAG", DbType.String, sicpvt);
                db.AddInParameter(dbCmd, "@TRANSFER_SIGHT_SEEING_PACKAGE_ID", DbType.Int32, sightname);
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_DETAIL_ID", DbType.Int32, int.Parse(DETAILID));
                db.AddInParameter(dbCmd, "@VOUCHER_NO", DbType.String, vno);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, userid);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoiceno);
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, quoteid);
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

    }
}
