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
   public class SupplierHotelPriceListMasterDal
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

       public DataSet fetchSupplierPriceListData(int Supplier_sr_no)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("GET_ALL_SUPPLIER_HOTEL_PRICE_LIST_DETAILS");
               db.AddInParameter(dbCmd, "@SUPPLIER_ID", DbType.String, Supplier_sr_no);



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

       public DataSet fetchSupplierPriceListDataForEdit(int Supplier_sr_no)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("GET_ALL_SUPPLIER_HOTEL_PRICE_LIST_DETAILS_FOR_EDIT");
               db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, Supplier_sr_no);

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


       // Insert into Hotel Price List Master
       public void inserthotelPriceListMaster(string suppliersrno,string roomtype,string singleroomrate,string doubleroomrate,string tripleroomrate,string extraadultrate, string extracwbrate,string extracnbrate,
                                                string isdefault,string status,string currency,string city,string isconf,string isgala,int pricelistid)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_INTO_HOTEL_PRICE_LIST_MASTER_MASTER");
               db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, suppliersrno);
               db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.String, roomtype);
               db.AddInParameter(dbCmd, "@SINGLE_ROOM_RATE", DbType.Decimal, decimal.Parse(singleroomrate));
               db.AddInParameter(dbCmd, "@DOUBLE_ROOM_RATE", DbType.Decimal, decimal.Parse(doubleroomrate));
               db.AddInParameter(dbCmd, "@TRIPLE_ROOM_RATE", DbType.Decimal, decimal.Parse(tripleroomrate));
               db.AddInParameter(dbCmd, "@EXTRA_ADULT_RATE", DbType.Decimal, decimal.Parse(extraadultrate));
               db.AddInParameter(dbCmd, "@EXTRA_CWB_COST", DbType.Decimal, decimal.Parse(extracwbrate));
               db.AddInParameter(dbCmd, "@EXTRA_CNB_COST", DbType.Decimal, decimal.Parse(extracnbrate));
               db.AddInParameter(dbCmd, "@IS_DEFAULT", DbType.String, isdefault);
               db.AddInParameter(dbCmd, "@PRICE_LIST_STATUS_ID", DbType.String, status);
               db.AddInParameter(dbCmd, "@CURRENCY", DbType.String, currency);
               db.AddInParameter(dbCmd, "@CITY_ID", DbType.String, city);
               db.AddInParameter(dbCmd, "@IS_CONFERENCE_APPLICABLE", DbType.String, isconf);
               db.AddInParameter(dbCmd, "@IS_GALA_DINNER_APPLICABLE", DbType.String, isgala);
               db.AddInParameter(dbCmd, "@PRICELISTID", DbType.String, pricelistid);
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


       public DataSet FetchCountForValidation(string Supplier,string roomType,int hotelid) 
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               //Database Connection
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("GET_DATA_FROM_HOTEL_PRICE_LIST_FOR_VALIDATION");
               db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, Supplier);
               db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, roomType);
               db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, hotelid);
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
