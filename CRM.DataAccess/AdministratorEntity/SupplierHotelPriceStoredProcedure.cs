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
    public class SupplierHotelPriceStoredProcedure
    {
        public void InsertUpdateHotelPrice(ArrayList HotelPrice)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            Boolean flag;
       
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_HOTEL_PRICE_LIST_MASTER");
                db.AddInParameter(dbCmd, "@SUPPLIER_HOTEL_PRICE_LIST_ID", DbType.Int32, HotelPrice[0]);
                db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, HotelPrice[1]);
                db.AddInParameter(dbCmd, "@SINGLE_ROOM_RATE", DbType.Decimal, Convert.ToDecimal(HotelPrice[2]));
                db.AddInParameter(dbCmd, "@DOUBLE_ROOM_RATE", DbType.Decimal, Convert.ToDecimal(HotelPrice[3]));
                db.AddInParameter(dbCmd, "@EXTRA_ADULT_RATE", DbType.Decimal, Convert.ToDecimal(HotelPrice[4]));
                
                //db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, Convert.ToDecimal(HotelPrice[5]));
                //db.AddInParameter(dbCmd, "@MARGIN_AMOUNT_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(HotelPrice[6]));
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, HotelPrice[7]);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS", DbType.String, HotelPrice[8]);
                
                db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, HotelPrice[9]);
                db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.String, HotelPrice[10]);
               
                db.AddInParameter(dbCmd, "@CWB_COST", DbType.Decimal, Convert.ToDecimal(HotelPrice[11]));
                db.AddInParameter(dbCmd, "@CNB_COST", DbType.Decimal, Convert.ToDecimal(HotelPrice[12]));
                if (HotelPrice[13].ToString().Equals("DD/MM/YYYY") || HotelPrice[13].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(HotelPrice[13].ToString(), "dd/MM/yyyy", null));
                }
                if (HotelPrice[14].ToString().Equals("DD/MM/YYYY") || HotelPrice[14].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(HotelPrice[14].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@SURCHARGE", DbType.Decimal, Convert.ToDecimal(HotelPrice[15]));
                if (Convert.ToString(HotelPrice[16]).Equals("T"))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                db.AddInParameter(dbCmd, "@IS_DEFAULT", DbType.Boolean, flag);
                db.AddInParameter(dbCmd, "@SURCHARG_UNIT", DbType.String, HotelPrice[17]);
                db.AddInParameter(dbCmd, "@TRIPLE_ROOM_RATE", DbType.Decimal, Convert.ToDecimal(HotelPrice[18]));
                db.AddInParameter(dbCmd, "@STATUS", DbType.String,HotelPrice[19]);
                db.AddInParameter(dbCmd, "@SERVICE_VOUCHER", DbType.String, HotelPrice[20]);

                db.AddInParameter(dbCmd, "@A_MARGIN", DbType.Decimal, Convert.ToDecimal(HotelPrice[21]));
                db.AddInParameter(dbCmd, "@A_PLUS_MARGIN", DbType.Decimal, Convert.ToDecimal(HotelPrice[22]));
                db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN", DbType.Decimal, Convert.ToDecimal(HotelPrice[23]));
                db.AddInParameter(dbCmd, "@A_MARGIN_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(HotelPrice[24]));
                db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(HotelPrice[25]));
                db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MAGIN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(HotelPrice[26]));

                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(HotelPrice[27]));
                db.ExecuteNonQuery(dbCmd);
                
                //ds=db.ExecuteDataSet(dbCmd);
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
           // return ds;
            
        }
        public void delHotelPrice(int delHotel)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_SUPPLIER_HOTEL_PRICE_LIST_MASTER");
                db.AddInParameter(dbCmd, "@SUPPLIER_HOTEL_PRICE", DbType.Int32, delHotel);
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
        public void insertHotelProiceDocument(int docid, string docname)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SUPPLIER_HOTEL_PRICE_DOCUMENT");
                db.AddInParameter(dbCmd, "@SUPPLIER_HOTEL_PRICE_LIST_ID", DbType.Int32, docid);
                db.AddInParameter(dbCmd, "@UPLOAD_RATE_DOCUMENT", DbType.String, docname);
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
        public DataSet getHotelPriceDocument(int docid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("GET_HOTEL_PRICE_DOCUMENT");
            db.AddInParameter(dbCmd, "@SUPPLIER_HOTEL_PRICE_LIST_ID", DbType.Int32, docid);
            ds = db.ExecuteDataSet(dbCmd);
            return ds;
        }
        public void InsertUpdateHotelRoomInventory(ArrayList RoomInventory)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SUPPLIER_HOTEL_ROOM_INVENTORY");
                db.AddInParameter(dbCmd, "@SUPPLIER_HOTEL_ROOM_INVENTORY_ID", DbType.Int32, Convert.ToInt32(RoomInventory[0]));
                db.AddInParameter(dbCmd, "@NO_OF_ROOMS_PURCHASED", DbType.Int32, Convert.ToInt32(RoomInventory[1]));
                db.AddInParameter(dbCmd, "@NO_OF_ROOMS_AVAILABLE", DbType.Int32, Convert.ToInt32(RoomInventory[2]));
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
        public DataSet CopyData(int supplierid)
        {
            Database db = null;
            DbCommand dbCmd = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("COPY_DATA_FOR_SUPPLIER_HOTEL_PRICE_LIST");
            db.AddInParameter(dbCmd, "@SUPPLIER_HOTEL_PRICE_LIST_ID", DbType.Int32, supplierid);
            DataSet ds = db.ExecuteDataSet(dbCmd);
            return ds;
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
    }
}
