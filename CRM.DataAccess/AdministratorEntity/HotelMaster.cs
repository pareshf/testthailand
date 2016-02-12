#region imports assemblies

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient; 

#endregion

namespace CRM.DataAccess.AdministratorEntity
{
    public class HotelMaster
    {
        public void InsertUpdateHoteldetail(ArrayList Hotel)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_FARE_HOTEL_MASTER");
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, Hotel[0]);
                db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, Hotel[1]);
                db.AddInParameter(dbCmd, "@HOTEL_RATING", DbType.Int32, Hotel[2]);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE1", DbType.String, Hotel[3]);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE2", DbType.String, (Hotel[4]));
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, Hotel[5]);
                db.AddInParameter(dbCmd, "@STATE_NAME", DbType.String, Hotel[6]);
                db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, Hotel[7]);
                db.AddInParameter(dbCmd, "@PINCODE", DbType.String, Hotel[8]);
                db.AddInParameter(dbCmd, "@EMAIL", DbType.String, Hotel[9]);
                db.AddInParameter(dbCmd, "@PHONE", DbType.String, Hotel[10]);
                db.AddInParameter(dbCmd, "@FAX", DbType.String, Hotel[11]);
                db.AddInParameter(dbCmd, "@HOTEL_WEBSITE", DbType.String, Hotel[12]);
                
                if (Hotel[13].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@GMT", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@GMT", DbType.DateTime, Convert.ToDateTime(Hotel[13]));
                }
                db.AddInParameter(dbCmd, "@CHECK_IN_TIME", DbType.String, Hotel[14]);
                db.AddInParameter(dbCmd, "@CHECK_OUT_TIME", DbType.String, Hotel[15]);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, Hotel[16]);
                

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
        public void DeleteMyHotel(int MyHotelId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FOR_FARE_HOTEL_MASTER");
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, MyHotelId);
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
        public void InsertUpdateHotelContactDetail(ArrayList HotelContactDeail)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_FARE_HOTEL_CONTACT_DETAIL");
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, HotelContactDeail[0]);
                db.AddInParameter(dbCmd, "@CONTACT_SRNO", DbType.Int32, HotelContactDeail[1]);
                db.AddInParameter(dbCmd, "@TITLE_DESC", DbType.String, HotelContactDeail[2]);
                db.AddInParameter(dbCmd, "@NAME", DbType.String, HotelContactDeail[3]);
                db.AddInParameter(dbCmd, "@SURNAME", DbType.String, HotelContactDeail[4]);                
                db.AddInParameter(dbCmd, "@DESIGNATION_DESC", DbType.String, HotelContactDeail[5]);
                db.AddInParameter(dbCmd, "@EMAIL", DbType.String, HotelContactDeail[6]);
                db.AddInParameter(dbCmd, "@MOBILE", DbType.String, HotelContactDeail[7]);
                db.AddInParameter(dbCmd, "@PHONE", DbType.String, HotelContactDeail[8]);

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
        public void InsertUpdateHotelCurrencyPriceMaster(ArrayList Currency)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_FARE_HOTEL_CURRENCY_PRICE_DETAIL");
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32,Convert.ToInt32(Currency[0]));
                db.AddInParameter(dbCmd, "@CURRENCY_PRICE_ID", DbType.Int32,Convert.ToInt32(Currency[1]));
                db.AddInParameter(dbCmd, "@ROOM_TYPE_NAME", DbType.String, Currency[2]);
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, Currency[3]);
                db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal,Convert.ToDecimal(Currency[4]));
                db.AddInParameter(dbCmd, "@TAX", DbType.Decimal,Convert.ToDecimal(Currency[5]));
                db.AddInParameter(dbCmd, "@GST", DbType.Decimal,Convert.ToDecimal(Currency[6]));

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
        public void InsertNewRoomType(string HOTEL_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_ROOM_TYPE");
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, Convert.ToInt32(HOTEL_ID));
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
        public void InsertUpdateSrviceType(ArrayList arr)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                         db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                        dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_FARE_HOTEL_SERVICE_TYPE_MASTER");
                        
                        db.AddInParameter(dbCmd, "@SERVICE_TYPE_DESC", DbType.String,arr[0]);
                        db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32,arr[1]);
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
        public DataSet getphotoDetail(int Photoid, int Photoid1)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;


                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_HOTEL_PHOTO");
                db.AddInParameter(dbCmd, "@PHOTO_SRNO", DbType.Int32, Photoid);
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, Photoid1);
                db.ExecuteNonQuery(dbCmd);
                ds = db.ExecuteDataSet(dbCmd);
                return ds;
          }
        public void insertHotelPhoto(int photoid, string Hotelphoto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_PHOTO_FOR_HOTEL_DETAIL");
                db.AddInParameter(dbCmd, "@PHOTO_SRNO", DbType.Int32, photoid);
                db.AddInParameter(dbCmd, "@PHOTO_NAME", DbType.String, Hotelphoto);
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
        public void insertupdatePhotoDetail(ArrayList Photo)
        {
            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_FARE_HOTEL_PHOTO_DETAILS");
                db.AddInParameter(dbCmd, "@PHOTO_TITLE", DbType.String, Photo[0]);
                db.AddInParameter(dbCmd, "@PHOTO_DESC", DbType.String, Photo[1]);
                db.AddInParameter(dbCmd, "@PHOTO_SRNO", DbType.Int32, Photo[2]);
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, Photo[3]);
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
        public void insertnewPhoto(string HOTEL_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_HOTEL_PHOTO_ADD_ANOTHER");
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, HOTEL_ID);
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
