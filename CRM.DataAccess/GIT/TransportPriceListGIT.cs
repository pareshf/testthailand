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
     public  class TransportPriceListGIT
    {

        /* COMMON SP WITHOUT PARAMETERS */
        public DataSet CommonSp(string SP_NAME)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(SP_NAME);



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

        public DataSet insertTransportPackage(int GIT_TRANSFER_PACKGE_ID, String GIT_TRANSFER_PACKGE_NAME, String GIT_PACKAGE_ID, String PRICE_LIST_STATUS_ID, String SUPPLIER_SR_NO, int CREATED_BY)
        {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;
           
           try
           {

               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_TRANSFER_PACKAGE_MASTER");

               db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.Int32, GIT_TRANSFER_PACKGE_ID);
               db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_NAME", DbType.String, GIT_TRANSFER_PACKGE_NAME);
               db.AddInParameter(dbCmd, "@GIT_PACKAGE_ID", DbType.String, GIT_PACKAGE_ID);
               db.AddInParameter(dbCmd, "@PRICE_LIST_STATUS_ID", DbType.String, PRICE_LIST_STATUS_ID);
               db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, SUPPLIER_SR_NO);
               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);

               dsData = db.ExecuteDataSet(dbCmd);    
                       
           }

           catch (Exception ex)
           {
            
                   throw ex;
             
           }

           finally
           {

           }
           return dsData;
        }

        public DataSet insertTransportPackageDetails(int GIT_TRANSFER_PACKGE_DETAILS_ID, int GIT_TRANSFER_PACKGE_ID,string from ,string To, int CREATED_BY)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_TRANSFER_PACKAGE_DETAIL");
                db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKAGE_DETAIL_ID", DbType.Int32, GIT_TRANSFER_PACKGE_DETAILS_ID);
                db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.Int32, GIT_TRANSFER_PACKGE_ID);
                db.AddInParameter(dbCmd, "@FROM_ID", DbType.String, from);
                db.AddInParameter(dbCmd, "@TO_ID", DbType.String, To);                
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);

                dsData = db.ExecuteDataSet(dbCmd);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

            }
            return dsData;
        }

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

        public DataSet insertCoach(int CoachPriceListID,string supplier_sr ,string CoachRate,int PackageID,int CurrencyID,int CREATED_BY)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_COACH_PRICE_LIST_MASTER");
                db.AddInParameter(dbCmd, "@COACH_PRICE_LIST_ID", DbType.Int32, CoachPriceListID);
                db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, supplier_sr);
                db.AddInParameter(dbCmd, "@COACH_RATE", DbType.Decimal,decimal.Parse(CoachRate));
                db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.Int32, PackageID);
                db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, CurrencyID);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);

                dsData = db.ExecuteDataSet(dbCmd);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

            }
            return dsData;
        }

        public DataSet insertGuide(int GuidePriceListID, string supplier_sr, string GuideRate, int PackageID, int CurrencyID, int CREATED_BY)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_GUIDE_PRICE_LIST_MASTER");
                db.AddInParameter(dbCmd, "@GUIDE_PRICE_LIST_ID", DbType.Int32, GuidePriceListID);
                db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, supplier_sr);
                db.AddInParameter(dbCmd, "@GUIDE_RATE", DbType.Decimal, decimal.Parse(GuideRate));
                db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.Int32, PackageID);
                db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, CurrencyID);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);

                dsData = db.ExecuteDataSet(dbCmd);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

            }
            return dsData;
        }

        public DataSet insertBoat(int BoatPriceListID, string supplier_sr, string BoatRate, int PackageID, int CurrencyID, int CREATED_BY)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_BOAT_PRICE_LIST_MASTER");
                db.AddInParameter(dbCmd, "@BOAT_PRICE_LIST_ID", DbType.Int32, BoatPriceListID);
                db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, supplier_sr);
                db.AddInParameter(dbCmd, "@BOAT_RATE", DbType.Decimal, decimal.Parse(BoatRate));
                db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.Int32, PackageID);
                db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, CurrencyID);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, CREATED_BY);

                dsData = db.ExecuteDataSet(dbCmd);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

            }
            return dsData;
        }


        // Bind Grid  
        public DataSet bindCoachGrid(int TranferPackID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_COACH_PRICE_LIST_DATA_GIT");
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_ID", DbType.Int32, TranferPackID);

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
        public DataSet bindBoatGrid(int TranferPackID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_BOAT_PRICE_LIST_DATA_GIT");
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_ID", DbType.Int32, TranferPackID);
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
        public DataSet bindGuideGrid(int TranferPackID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_GUIDE_PRICE_LIST_DATA_GIT");
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_ID", DbType.Int32, TranferPackID);
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


        public DataSet FetchCountForValidation(string GitPackageName, int Priceid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_DATA_FROM_TRANSFER_PACKAGE_PRICE_LIST_FOR_VALIDATION");
                db.AddInParameter(dbCmd, "@GIT_PACKAGE_NAME ", DbType.String, GitPackageName);
                db.AddInParameter(dbCmd, "@TRANSFER_PACK_PRICE_ID", DbType.Int32, Priceid);
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

        public DataSet fetchTransferPriceListDataForEdit(int TransferPackageid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_ALL_TRANSFER_PACKAGE_PRICE_LIST_DETAILS_FOR_EDIT");
                db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.String, TransferPackageid);

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

        public DataSet fetchTransferPackageDetails(int TransferPackageid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_ALL_TRANSFER_PACKAGE_DETAILS");
                db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.String, TransferPackageid);

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

        public DataSet fetchCoachDataForEdit(int TransferPackageid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_ALL_COACH_DETAILS_FOR_EDIT");
                db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.String, TransferPackageid);

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


        #region Delete Record
        public DataSet deleteTransferPackageDetails(int TransferPackageid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_GIT_TRANSFER_PACKAGE_DETAIL");
                db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.Int32,TransferPackageid);

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

        public DataSet FetchCountForCoachValidation(string coachname, int coachid, int PackID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_DATA_FROM_COACH_PRICE_LIST_FOR_VALIDATION");
                db.AddInParameter(dbCmd, "@COACH_NAME", DbType.String, coachname);
                db.AddInParameter(dbCmd, "@COACH_ID", DbType.Int32, coachid);
                db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.Int32, PackID);
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

        public DataSet FetchCountForBoatValidation(string Boatname, int Boatid, int PackID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_DATA_FROM_BOAT_PRICE_LIST_FOR_VALIDATION");
                db.AddInParameter(dbCmd, "@BOAT_NAME", DbType.String, Boatname);
                db.AddInParameter(dbCmd, "@BOAT_ID", DbType.Int32, Boatid);
                db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.Int32, PackID);

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

        public DataSet FetchCountForGuideValidation(string Guidename, int Guideid ,int PackID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                //Database Connection
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_DATA_FROM_GUIDE_PRICE_LIST_FOR_VALIDATION");
                db.AddInParameter(dbCmd, "@GUIDE_NAME", DbType.String, Guidename);
                db.AddInParameter(dbCmd, "@GUIDE_ID", DbType.Int32, Guideid);
                db.AddInParameter(dbCmd, "@GIT_TRANSFER_PACKGE_ID", DbType.Int32, PackID);
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
