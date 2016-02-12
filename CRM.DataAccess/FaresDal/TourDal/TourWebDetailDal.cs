using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using CRM.Model.FaresModel.TourModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.FaresDal.TourDal
{
    public class TourWebDetailDal
    {
        private string dbConnectionName;

        public TourWebDetailDal()
        {
            this.dbConnectionName = "CRM_WEB";
        }

        public TourWebDetailDal(string dbConnectionName)
        {
            this.dbConnectionName = dbConnectionName;
        }

        public string GetWebHightlights(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            string s = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase(dbConnectionName);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_HIGHLIGHT_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
                s = db.ExecuteScalar(dbCmd).ToString();
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
            return s;
        }

        public string GetWebCost(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            string s = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase(dbConnectionName);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_TOUR_COST_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
                s = db.ExecuteScalar(dbCmd).ToString();
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
            return s;

        }

        public string GetWebItenary(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            string s = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase(dbConnectionName);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_ITENARY_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
                s = db.ExecuteScalar(dbCmd).ToString();
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
            return s;
        }

        public string GetWebImportantNotes(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            string s = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase(dbConnectionName);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_IMPORTANT_NOTES_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
                s = db.ExecuteScalar(dbCmd).ToString();
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
            return s;
        }

        public string GetWebTerms(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            string s = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase(dbConnectionName);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_TERMS_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
                s = db.ExecuteScalar(dbCmd).ToString();
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
            return s;
        }

        public int UpdateWebHightlights(TourWebBDto objTourWebBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(dbConnectionName);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_HIGHLIGHT_UPDATE);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, objTourWebBDto.tourId);

                if (!string.IsNullOrEmpty(objTourWebBDto.Hightlights))
                    db.AddInParameter(dbCmd, "@WEB_HIGHLIGHT", DbType.String, objTourWebBDto.Hightlights);
                else
                    db.AddInParameter(dbCmd, "@WEB_HIGHLIGHT", DbType.String, DBNull.Value);

                Result = db.ExecuteNonQuery(dbCmd);
                return Result;
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
            return 0;
        }

        public int UpdateWebCost(TourWebBDto objTourWebBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(dbConnectionName);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_TOUR_COST_UPDATE);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, objTourWebBDto.tourId);

                if (!string.IsNullOrEmpty(objTourWebBDto.Cost))
                    db.AddInParameter(dbCmd, "@WEB_TOUR_COST", DbType.String, objTourWebBDto.Cost);
                else
                    db.AddInParameter(dbCmd, "@WEB_TOUR_COST", DbType.String, DBNull.Value);

                Result = db.ExecuteNonQuery(dbCmd);
                return Result;
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
            return 0;
        }

        public int UpdateWebItenary(TourWebBDto objTourWebBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(dbConnectionName);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_ITENARY_UPDATE);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, objTourWebBDto.tourId);

                if (!string.IsNullOrEmpty(objTourWebBDto.Itinerary))
                    db.AddInParameter(dbCmd, "@WEB_ITENARY", DbType.String, objTourWebBDto.Itinerary);
                else
                    db.AddInParameter(dbCmd, "@WEB_ITENARY", DbType.String, DBNull.Value);

                Result = db.ExecuteNonQuery(dbCmd);
                return Result;
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
            return 0;
        }

        public int UpdateWebImportantNotes(TourWebBDto objTourWebBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(dbConnectionName);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_IMPORTANT_NOTES_UPDATE);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, objTourWebBDto.tourId);

                if (!string.IsNullOrEmpty(objTourWebBDto.ImportantNote))
                    db.AddInParameter(dbCmd, "@WEB_IMPORTANT_NOTES", DbType.String, objTourWebBDto.ImportantNote);
                else
                    db.AddInParameter(dbCmd, "@WEB_IMPORTANT_NOTES", DbType.String, DBNull.Value);

                Result = db.ExecuteNonQuery(dbCmd);
                return Result;
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
            return 0;
        }

        public int UpdateWebTerms(TourWebBDto objTourWebBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(dbConnectionName);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_TERMS_UPDATE);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, objTourWebBDto.tourId);

                if (!string.IsNullOrEmpty(objTourWebBDto.Terms))
                    db.AddInParameter(dbCmd, "@WEB_TERMS", DbType.String, objTourWebBDto.Terms);
                else
                    db.AddInParameter(dbCmd, "@WEB_TERMS", DbType.String, DBNull.Value);

                Result = db.ExecuteNonQuery(dbCmd);
                return Result;
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
            return 0;
        }

        public DataSet GetTourType()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(dbConnectionName);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_TOUR_TYPE_SELECT_KEYVALUE);
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

        public DataSet GetItenaryType()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(dbConnectionName);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TOUR_ITENARY_TYPE_SELECT");
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

        public DataSet GetTours(string mainType, int subType, int itenaryType)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(dbConnectionName);
                dbCmd = db.GetStoredProcCommand("dbo.UPS_FARE_TOUR_SELECT_BY_ITENARY");
                db.AddInParameter(dbCmd, "@TOUR_INTER_DOMEST", DbType.String, mainType);
                db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, subType);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY_TYPE_ID", DbType.Int32, itenaryType);

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
