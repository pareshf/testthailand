using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;  


namespace CRM.DataAccess.AdministratorEntity
{
    public class SiteSeeingMasterStoredProcedure
    {


        public void insertupdateSite(ArrayList Site)
        {

            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FARE_SITE_SEEING_MASTER");
                db.AddInParameter(dbCmd, "@SITE_SEEING_DETAILS", DbType.String, Site[1]);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, Site[2]);
                db.AddInParameter(dbCmd, "@ENTRY_FEE", DbType.Int32, Site[3]);
                db.AddInParameter(dbCmd, "@SITE_NAME", DbType.String, Site[0]);
                db.AddInParameter(dbCmd, "@SIGHT_SEEING_SRNO", DbType.Int32, Site[4]);
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
        public void insertupdatePhoto(ArrayList Photo)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SITE_SEEING_PHOTO_DETAILS");
                db.AddInParameter(dbCmd, "@PHOTO_TITLE", DbType.String, Photo[0]);
                db.AddInParameter(dbCmd, "@PHOTO_DESCRIPATION", DbType.String, Photo[1]);
                db.AddInParameter(dbCmd, "@SIGHT_SEEING_SRNO", DbType.Int32, Photo[2]);
                db.AddInParameter(dbCmd, "@SIGHTSEEING_ID", DbType.Int32, Photo[3]);
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
        public void DeleteMySite(int delsite)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_SITE_SEEING_MASTER");
                db.AddInParameter(dbCmd, "@SIGHT_SEEING_SRNO", DbType.Int32, Convert.ToInt32(delsite));
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
        public void insertSitePhoto(int photoid,string sitephoto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_PHOTO_FOR_SITE_DETAIL");
                db.AddInParameter(dbCmd, "@SIGHTSEEING_ID", DbType.Int32, photoid);
                db.AddInParameter(dbCmd,"@PHOT_NAME",DbType.String,sitephoto);
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
       
        public DataSet getphotoDetail(int Photoid,int Photoid1)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("GET_SITE_PHOTO");
            db.AddInParameter(dbCmd, "@SIGHT_SEEING_SRNO", DbType.Int32, Photoid);
            db.AddInParameter(dbCmd, "@SIGHTSEEINGID", DbType.Int32, Photoid1);
            ds = db.ExecuteDataSet(dbCmd);
            return ds;
        }
        
        public void insertnewPhoto(string siteid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_SITE_PHOTO_ADD_ANOTHER");
                db.AddInParameter(dbCmd, "@SIGHT_SEEING_SRNO", DbType.Int32, siteid);
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
        public void DeleteSitePhoto(int photoid)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_PHOTO_FROM_SITE_SEEING_MASTER");
                db.AddInParameter(dbCmd, "@PHOTOID", DbType.Int32, Convert.ToInt32(photoid));
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
