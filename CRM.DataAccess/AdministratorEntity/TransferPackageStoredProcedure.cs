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
    public class TransferPackageStoredProcedure
    {
        public void InsertUpdateTransferPackage(ArrayList TransferPackage)
        {
            Database db = null;
            DbCommand dbCmd = null;
          //  DataSet ds = null;
            
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_TRANSFER_PACKAGE_PRICE_LIST");
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_PRICE_ID", DbType.Int32, TransferPackage[0]);
                db.AddInParameter(dbCmd, "@FROM_PLACE", DbType.String, TransferPackage[1]);
                if (TransferPackage[2].ToString().Equals("DD/MM/YYYY") || TransferPackage[2].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM_DATE", DbType.DateTime, DateTime.ParseExact(TransferPackage[2].ToString(), "dd/MM/yyyy", null));
                }
                if (TransferPackage[3].ToString().Equals("DD/MM/YYYY") || TransferPackage[3].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EFFECTIVE_TO_DATE", DbType.DateTime, DateTime.ParseExact(TransferPackage[3].ToString(), "dd/MM/yyyy", null));
                }
                
                //db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, Convert.ToDecimal(TransferPackage[4]));
                //db.AddInParameter(dbCmd, "@MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(TransferPackage[5]));
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, TransferPackage[6]);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS", DbType.String, TransferPackage[7]);
                db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.String, TransferPackage[8]);
                
                db.AddInParameter(dbCmd, "@FIT_PACKAGE", DbType.String, TransferPackage[9]);
                db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, TransferPackage[10]);
                db.AddInParameter(dbCmd, "@STATUS", DbType.String, TransferPackage[11]);
                db.AddInParameter(dbCmd, "@SEVICE_VOUCHER", DbType.String, TransferPackage[12]);

                db.AddInParameter(dbCmd, "@A_MARGIN", DbType.Decimal, Convert.ToDecimal(TransferPackage[13]));
                db.AddInParameter(dbCmd, "@A_PLUS_MARGIN", DbType.Decimal, Convert.ToDecimal(TransferPackage[14]));
                db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN", DbType.Decimal, Convert.ToDecimal(TransferPackage[15]));
                db.AddInParameter(dbCmd, "@A_MARGIN_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(TransferPackage[16]));
                db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(TransferPackage[17]));
                db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MAGIN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(TransferPackage[18]));
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(TransferPackage[19]));
                db.ExecuteNonQuery(dbCmd);
                //ds = db.ExecuteDataSet(dbCmd);
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
            //return ds;
        }
        
        public void delTransferPackage(int deltransferpackageid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FORM_TRANSFER_PACKAGE_PRICE_LIST");
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_PRICE_ID", DbType.Int32, deltransferpackageid);
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

        public DataSet CopyData(int transid)
        {
            Database db = null;
            DbCommand dbCmd = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("COPY_DATA_FOR_TRANSFER_PACKAGE_DATA");
            db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_PRICE_ID", DbType.Int32, transid);
            DataSet ds = db.ExecuteDataSet(dbCmd);
            return ds;
        }
        

        public void InsertUpdateTransferDetail(ArrayList detail)
        {
            Database db = null;
            DbCommand dbCmd = null;
            
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_TRANSFER_DETAIL");
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_FROM_TO_DETAIL_ID", DbType.Int32, detail[0]);
                db.AddInParameter(dbCmd, "@FROM_PLACE", DbType.String, detail[1]);
                db.AddInParameter(dbCmd, "@TO_PLACE", DbType.String, detail[2]);
                db.AddInParameter(dbCmd, "@PACKAGE_NAME", DbType.String, detail[3]);
                db.AddInParameter(dbCmd, "@ARRIVAL_DEPARTURE", DbType.String, detail[4]);
                db.AddInParameter(dbCmd, "@ADULTSIC", DbType.Decimal, Convert.ToDecimal(detail[5]));
                db.AddInParameter(dbCmd, "@CHILDSIC", DbType.Decimal, Convert.ToDecimal(detail[6]));
                db.AddInParameter(dbCmd, "@ADULTPVT", DbType.Decimal, Convert.ToDecimal(detail[7]));
                db.AddInParameter(dbCmd, "@CHILDPVT", DbType.Decimal, Convert.ToDecimal(detail[8]));
                db.AddInParameter(dbCmd, "@SICRATE_PERSON", DbType.Decimal, Convert.ToDecimal(detail[9]));
                db.AddInParameter(dbCmd, "@PVTRATE_PERSON", DbType.Decimal, Convert.ToDecimal(detail[10]));
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
        public void InsertNewTransferDetail(string detail_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_TRANSFER_DETAIL_ADD_ANOTHER");
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_PRICE_ID", DbType.Int32, Convert.ToInt32(detail_id));
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
        public DataSet CheckValidation()
        {
            Database db = null;
            DbCommand dbCmd = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("CHECK_VALIDATION_FOR_DROPDOWN_OF_GRID");
            DataSet ds1 = db.ExecuteDataSet(dbCmd);
            return ds1;
        }

        public DataSet FetchTransferPrice(int transid,int PAX)
        {
            Database db = null;
            DbCommand dbCmd = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("FETCH_TRANSFER_PACKAGE_PRICE_SLAB_WISE");
            db.AddInParameter(dbCmd, "@NO_OF_PAX", DbType.Int32, PAX);
            db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_PRICE_ID", DbType.Int32, transid);
            DataSet ds = db.ExecuteDataSet(dbCmd);
            return ds;
        }
        
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

        public DataSet InsertUpdateTransferPrice(string slabmasterid, string slab_id, string priceid, int noofpax, string adult_sic, string child_sic, string adult_Pvt, string child_pvt,string A_MARGIN_IN_AMOUNT,string A_PLUS_MARGIN_IN_AMOUNT,string A_PLUS_PLUS_MARGIN_IN_AMOUNT,string A_MARGIN_AMOUNT_IN_PERCENTAGE,string A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE,string A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_TRANSFER_PACKAGE_PRICE_SLAB_WISE");
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_PRICE_SLAB_ID", DbType.Int32, int.Parse(slabmasterid));
                db.AddInParameter(dbCmd, "@SLAB_ID", DbType.Int32, int.Parse(slab_id));
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_FROM_TO_DETAIL_ID", DbType.Int32, int.Parse(priceid));
                
                db.AddInParameter(dbCmd, "@NO_OF_PAX", DbType.Int32, noofpax);
                if (adult_sic.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ADULT_SIC_RATE", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ADULT_SIC_RATE", DbType.Decimal, Convert.ToDecimal(adult_sic));
                }
                if (child_sic.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CHILD_SIC_RATE", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHILD_SIC_RATE", DbType.Decimal, Convert.ToDecimal(child_sic));
                }
                if (adult_Pvt.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@ADULT_PVT_RATE", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@ADULT_PVT_RATE", DbType.Decimal, Convert.ToDecimal(adult_Pvt));

                }
                if (child_pvt.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CHILD_PVT_RATE", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CHILD_PVT_RATE", DbType.Decimal, Convert.ToDecimal(child_pvt));
                }
                if (A_MARGIN_IN_AMOUNT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@A_MARGIN_IN_AMOUNT", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_MARGIN_IN_AMOUNT", DbType.Decimal, Convert.ToDecimal(A_MARGIN_IN_AMOUNT));
                }
                if (A_PLUS_MARGIN_IN_AMOUNT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_IN_AMOUNT", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_IN_AMOUNT", DbType.Decimal, Convert.ToDecimal(A_PLUS_MARGIN_IN_AMOUNT));
                }
                if (A_PLUS_PLUS_MARGIN_IN_AMOUNT.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN_IN_AMOUNT", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN_IN_AMOUNT", DbType.Decimal, Convert.ToDecimal(A_PLUS_PLUS_MARGIN_IN_AMOUNT));
                }
                if (A_MARGIN_AMOUNT_IN_PERCENTAGE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@A_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(A_MARGIN_AMOUNT_IN_PERCENTAGE));
                }
                if (A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE));
                }
                if (A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE));
                }

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

        public DataSet Fetch_Data_for_Price(string price_id, int slab_id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_TRANSFER_PACKAGE_PRICE_SLAB_WISE");
                db.AddInParameter(dbCmd, "@TRANSFER_PACKAGE_FROM_TO_DETAIL_ID", DbType.Int32, int.Parse(price_id));
                db.AddInParameter(dbCmd, "@SLAB_ID", DbType.Int32, slab_id);
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
