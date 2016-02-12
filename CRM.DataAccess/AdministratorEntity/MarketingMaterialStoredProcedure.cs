using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;

namespace CRM.DataAccess.AdministratorEntity
{
    public class MarketingMaterialStoredProcedure
    {
        public void InsertUpdateMarketingMaterial(ArrayList MAR)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_MARKETING_MATERIAL");
                db.AddInParameter(dbCmd, "@MAR_ID", DbType.Int32, MAR[1]);
                db.AddInParameter(dbCmd, "@TOUR_NAME", DbType.String, MAR[0]);
                db.AddInParameter(dbCmd, "@TOURCODE", DbType.String, MAR[2]);
                //db.AddInParameter(dbCmd, "@TYPE", DbType.String, MAR[2]);
                //if (MAR[3].ToString().Equals(" "))
                //{
                //    db.AddInParameter(dbCmd, "@EXPIRATION_DATE", DbType.DateTime, DBNull.Value);
                //}
                //else
                //{
                //    db.AddInParameter(dbCmd, "@EXPIRATION_DATE", DbType.DateTime, DateTime.ParseExact(MAR[3].ToString(), "dd/MM/yyyy", null));
                //}
                //db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, MAR[4]);
                //db.AddInParameter(dbCmd, "@EMBEDCODE", DbType.String, MAR[5]);
                //db.AddInParameter(dbCmd, "@WEBURL", DbType.String, MAR[6]);
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
        public void delMarketingMaterial(int delMar)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_MARKETING_MATERIAL");
                db.AddInParameter(dbCmd, "@MARID", DbType.Int32, delMar);
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
        public DataSet getMarketingMaterialDoc(int mardoc)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("GET_MARKETING_MATERIAL_DOCUMENT");
            db.AddInParameter(dbCmd, "@MARID", DbType.Int32, mardoc);
            ds = db.ExecuteDataSet(dbCmd);
            return ds;
        }
        public DataSet GetEmbadCodeandWeburl(int tourid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("GET_EMBADCODE_WEBURL_FOR_COMMON_MARKETING_MATERIAL");
            db.AddInParameter(dbCmd, "@TOURID", DbType.Int32, tourid);
            ds = db.ExecuteDataSet(dbCmd);
            return ds;
        }
        //public void InsertUpdateMarketingDocument(int marid, string mardocument,string title,int isitenery,int isdefaultdoc)
        //{
        //    Database db = null;
        //    DbCommand dbCmd = null;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
        //        dbCmd = db.GetStoredProcCommand("INSERT_NEW_MARKETING_DOCUMENT");
        //        db.AddInParameter(dbCmd, "@MARID", DbType.Int32, marid);
        //        db.AddInParameter(dbCmd, "@DOC_NAME", DbType.String, mardocument);
        //        db.AddInParameter(dbCmd, "@TITLE", DbType.String, title);
        //        db.AddInParameter(dbCmd, "@ISITENARY", DbType.Int32, isitenery);
        //        db.AddInParameter(dbCmd, "@ISDEFAULT_DOC", DbType.Int32, isdefaultdoc);
        //        db.ExecuteNonQuery(dbCmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
        //        if (rethrow)
        //        {
        //            throw ex;
        //        }
        //    }
        //    finally
        //    {
        //        DALHelper.Destroy(ref dbCmd);
        //    }
        //}
        //public void UpadateMarketingDoc(int marid, string title, int isdefaultdoc)
        //{
        //    Database db = null;
        //    DbCommand dbCmd = null;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
        //        dbCmd = db.GetStoredProcCommand("UPDATE_MARKETING_MATERIAL_DOC");
        //        db.AddInParameter(dbCmd, "@MARID", DbType.Int32, marid);
        //        db.AddInParameter(dbCmd, "@TITLE", DbType.String, title);
        //        db.AddInParameter(dbCmd, "@IS_DEFAULT_DOC", DbType.Int32, isdefaultdoc);
        //        db.ExecuteNonQuery(dbCmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
        //        if (rethrow)
        //        {
        //            throw ex;
        //        }
        //    }
        //    finally
        //    {
        //        DALHelper.Destroy(ref dbCmd);
        //    }
        //}
        public void insertMarketingMaterial(int tourid, string title, string filename, int isitenary, string Description, int isdefaultdoc,string date)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_MARKETING_DOCUMENT");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourid);
                db.AddInParameter(dbCmd, "@DOC", DbType.String, filename);
                db.AddInParameter(dbCmd, "@TITLE", DbType.String, title);
                db.AddInParameter(dbCmd, "@IS_ITENARY", DbType.Int32, isitenary);
                db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, Description);
                db.AddInParameter(dbCmd, "@IS_DEFAULT_DOC", DbType.Int32, isdefaultdoc);
                db.AddInParameter(dbCmd, "@EXPIRYDATE", DbType.DateTime, DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null));
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
        public void updateMarketingMaterial(int tourid, string title, int isdefaultdoc)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_MARKETING_MATERIAL_DOC");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourid);
                db.AddInParameter(dbCmd, "@TITLE", DbType.String, title);
                db.AddInParameter(dbCmd, "@IS_DEFAULT_DOC", DbType.Int32, isdefaultdoc);
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
        public void InsertUpdateEmbadCode(int tourid, string embadcode, string weburl)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_EMBAD_CODE_FOR_COMMON_MARKETING_MATERIAL");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourid);
                db.AddInParameter(dbCmd, "@EMBADCODE", DbType.String, embadcode);
                db.AddInParameter(dbCmd, "@WEBURL", DbType.String, weburl);
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
