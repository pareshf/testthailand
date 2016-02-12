using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.WebPortalDal
{
    public class WebPortalDal : IDisposable
    {
        public DataTable GetTourType()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_WEB_SELECT_TOUR_TYPE");
                ds = db.ExecuteDataSet(dbCmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception)
            {
                //bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                //if (rethrow)
                //{
                //    throw ex;
                //}
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return dt;
        }

        public DataTable GetTourRegion(string tourType, int tourSubTypeId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_WEB_SELECT_TOUR_REGION");
                db.AddInParameter(dbCmd, "@tour_type", DbType.String, tourType);
                db.AddInParameter(dbCmd, "@tour_sub_type", DbType.Int32, tourSubTypeId);

                ds = db.ExecuteDataSet(dbCmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
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
            return dt;
        }

        public DataTable GetTourCountry(string tourType, int tourSubTypeId, int regionId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_WEB_SELECT_TOUR_COUNTRY");
                db.AddInParameter(dbCmd, "@tour_type", DbType.String, tourType);
                db.AddInParameter(dbCmd, "@tour_sub_type", DbType.Int32, tourSubTypeId);
                db.AddInParameter(dbCmd, "@tour_region", DbType.Int32, regionId);
                ds = db.ExecuteDataSet(dbCmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
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
            return dt;
        }

        public DataTable GetTourState(string tourType, int tourSubTypeId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_WEB_SELECT_TOUR_STATE");
                db.AddInParameter(dbCmd, "@tour_type", DbType.String, tourType);
                db.AddInParameter(dbCmd, "@tour_sub_type", DbType.Int32, tourSubTypeId);

                ds = db.ExecuteDataSet(dbCmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
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
            return dt;
        }

        public DataTable GetTour(string tourType, int tourSubTypeId, int regionId, int countryId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_WEB_SELECT_TOURS");
                db.AddInParameter(dbCmd, "@tour_type", DbType.String, tourType);
                db.AddInParameter(dbCmd, "@tour_sub_type", DbType.Int32, tourSubTypeId);
                db.AddInParameter(dbCmd, "@tour_region", DbType.Int32, regionId);
                db.AddInParameter(dbCmd, "@tour_country", DbType.Int32, countryId);
                ds = db.ExecuteDataSet(dbCmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
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
            return dt;
        }

        public DataTable GetTourByState(string tourType, int tourSubTypeId, int stateId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_WEB_SELECT_TOURS_BY_STATES");
                db.AddInParameter(dbCmd, "@tour_type", DbType.String, tourType);
                db.AddInParameter(dbCmd, "@tour_sub_type", DbType.Int32, tourSubTypeId);
                db.AddInParameter(dbCmd, "@tour_state", DbType.Int32, stateId);

                ds = db.ExecuteDataSet(dbCmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
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
            return dt;
        }

        public DataTable GetTourDetail(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_WEB_SELECT_TOUR_DETAILS");
                //db.AddInParameter(dbCmd, "@tour_type", DbType.String, tourType);
                db.AddInParameter(dbCmd, "@tour_id", DbType.Int32, tourId);
                //db.AddInParameter(dbCmd, "@tour_region", DbType.Int32, regionId);
                //db.AddInParameter(dbCmd, "@tour_country", DbType.Int32, countryId);
                ds = db.ExecuteDataSet(dbCmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
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
            return dt;
        }

        #region IDisposable Members

        public void Dispose()
        {

        }

        #endregion
    }
}
