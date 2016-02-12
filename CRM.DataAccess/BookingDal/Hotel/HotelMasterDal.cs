using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Booking.Hotel;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;

namespace CRM.DataAccess.BookingDal.Hotel
{
    public class HotelMasterDal
    {
        #region Hotel Detials tab
        #region Get Hotel Details
        public DataSet GetHotelDetails(String searchParameter)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_SELECT);
                if (!String.IsNullOrEmpty(searchParameter))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, searchParameter);
                else
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, DBNull.Value);



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

        #region delete Hotel Details
        public int DeleteHotel(string idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_DELETE);
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.String, idCollections);
                db.AddOutParameter(dbCmd, "@ERRORCODE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ERRORCODE"));
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
            return Result;
        }
        #endregion

        #region Insert Hotel

        public int InsertHotel(HotelBDto objHotelBDto, ref int hotelId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_INSERT);
                db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, objHotelBDto.HotelName);
                db.AddInParameter(dbCmd, "@HOTEL_RATING", DbType.Int32, objHotelBDto.Rating);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE1", DbType.String, objHotelBDto.Address.AddressLine1);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE2", DbType.String, objHotelBDto.Address.AddressLine2);
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, objHotelBDto.Address.CountryId);
                db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, objHotelBDto.Address.StateId);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, objHotelBDto.Address.CityId);
                db.AddInParameter(dbCmd, "@PINCODE", DbType.String, objHotelBDto.Address.PinCodeNo);
                db.AddInParameter(dbCmd, "@EMAIL", DbType.String, objHotelBDto.Contact.EmailId);
                db.AddInParameter(dbCmd, "@PHONE", DbType.String, objHotelBDto.Contact.PhoneNo);
                db.AddInParameter(dbCmd, "@FAX", DbType.String, objHotelBDto.Contact.FaxNo);
                db.AddInParameter(dbCmd, "@HOTEL_WEBSITE", DbType.String, objHotelBDto.HotelWebsite);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, objHotelBDto.UserId);
                db.AddOutParameter(dbCmd, "@HOTEL_ID", DbType.Int32, 10);
                Result = db.ExecuteNonQuery(dbCmd);
                hotelId = Convert.ToInt32(db.GetParameterValue(dbCmd, "@HOTEL_ID"));
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
        #endregion

        #region Update Hotel

        public int UpdateHotel(HotelBDto objHotelBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_UPDATE);
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, objHotelBDto.HotelId);
                db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, objHotelBDto.HotelName);
                db.AddInParameter(dbCmd, "@HOTEL_RATING", DbType.Int32, objHotelBDto.Rating);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE1", DbType.String, objHotelBDto.Address.AddressLine1);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE2", DbType.String, objHotelBDto.Address.AddressLine2);
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, objHotelBDto.Address.CountryId);
                db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, objHotelBDto.Address.StateId);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, objHotelBDto.Address.CityId);
                db.AddInParameter(dbCmd, "@PINCODE", DbType.String, objHotelBDto.Address.PinCodeNo);
                db.AddInParameter(dbCmd, "@EMAIL", DbType.String, objHotelBDto.Contact.EmailId);
                db.AddInParameter(dbCmd, "@PHONE", DbType.String, objHotelBDto.Contact.PhoneNo);
                db.AddInParameter(dbCmd, "@FAX", DbType.String, objHotelBDto.Contact.FaxNo);
                db.AddInParameter(dbCmd, "@HOTEL_WEBSITE", DbType.String, objHotelBDto.HotelWebsite);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, objHotelBDto.UserId);
                db.ExecuteNonQuery(dbCmd);
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
            return Result;
        }
        #endregion

        #region Select HotelId
        public DataTable SelectHotelId(int HotelId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_SELECT_BY_HOTEL_ID);
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, HotelId);
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
        #endregion
        #endregion

        #region Room Details Tab

        #region Get Room Details
        public DataSet GetRoomDetails(int HotelId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_ROOM_DETAILS_SELECT);
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, HotelId);

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

        #region delete Room Details
        public int DeleteRoom(int SrNo, int HotelID, int RoomTypeId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_ROOM_DETAILS_DELETE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, SrNo);
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, HotelID);
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, RoomTypeId);
                db.AddOutParameter(dbCmd, "@ERRORCODE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ERRORCODE"));
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
            return Result;
        }
        #endregion

        #region Insert Room Details

        public int InsertRoom(HotelBDto objHotelBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_ROOM_DETAILS_INSERT);
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, objHotelBDto.HotelId);
                db.AddInParameter(dbCmd, "@ROOM_NO", DbType.String, objHotelBDto.RoomNo);
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, objHotelBDto.RoomTypeId);
                db.AddInParameter(dbCmd, "@ROOM_DESC", DbType.String, objHotelBDto.RoomDesc);
                db.AddInParameter(dbCmd, "@RATE", DbType.Decimal, objHotelBDto.Rate);
                db.AddInParameter(dbCmd, "@CURRANCY", DbType.Int32, objHotelBDto.Currancy);
                if (objHotelBDto.Discount != 0)
                    db.AddInParameter(dbCmd, "@DISCOUNT", DbType.Decimal, objHotelBDto.Discount);
                else
                    db.AddInParameter(dbCmd, "@DISCOUNT", DbType.Decimal, DBNull.Value);

                db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, objHotelBDto.Tax);
                db.AddInParameter(dbCmd, "@GST", DbType.Decimal, objHotelBDto.GST);
                db.AddInParameter(dbCmd, "@PHOTO", DbType.Binary, objHotelBDto.RoomPhoto);
                db.AddInParameter(dbCmd, "@PHOTO_CONTENT_TYPE", DbType.String, objHotelBDto.RoomPhotoType);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, objHotelBDto.UserId);
                db.AddOutParameter(dbCmd, "@ISEXIST", DbType.Int32, 1);
                int r = db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ISEXIST"));
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
        #endregion

        #region Update Room Deatils

        public int UpdateRoom(HotelBDto objHotelBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_ROOM_DETAILS_UPDATE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, objHotelBDto.SrNo);
                db.AddInParameter(dbCmd, "@ROOM_NO", DbType.String, objHotelBDto.RoomNo);
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, objHotelBDto.RoomTypeId);
                db.AddInParameter(dbCmd, "@ROOM_DESC", DbType.String, objHotelBDto.RoomDesc);
                db.AddInParameter(dbCmd, "@RATE", DbType.Decimal, objHotelBDto.Rate);
                db.AddInParameter(dbCmd, "@CURRANCY", DbType.Int32, objHotelBDto.Currancy);
                if (objHotelBDto.Discount != 0)
                    db.AddInParameter(dbCmd, "@DISCOUNT", DbType.Decimal, objHotelBDto.Discount);
                else
                    db.AddInParameter(dbCmd, "@DISCOUNT", DbType.Decimal, DBNull.Value);
                db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, objHotelBDto.Tax);
                db.AddInParameter(dbCmd, "@GST", DbType.Decimal, objHotelBDto.GST);
                db.AddInParameter(dbCmd, "@PHOTO", DbType.Binary, objHotelBDto.RoomPhoto);
                db.AddInParameter(dbCmd, "@PHOTO_CONTENT_TYPE", DbType.String, objHotelBDto.RoomPhotoType);
                db.AddInParameter(dbCmd, "@IsPhotoChange", DbType.Boolean, objHotelBDto.IsRoomPhotoUpdate);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, objHotelBDto.UserId);
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
            return Result;
        }
        #endregion

        #region Insert Room Currency Price Details

        public int InsertRoomCurrencyPriceDetails(HotelBDto objHotelBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_CURRENCY_PRICE_INSERT);


                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, objHotelBDto.HotelId);
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, objHotelBDto.RoomTypeId);
                db.AddInParameter(dbCmd, "@HOTEL_CURRENCY", DbType.Int32, objHotelBDto.Currancy);
                if (objHotelBDto.Rate != 0)
                    db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, objHotelBDto.Rate);
                else
                    db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, DBNull.Value);


                if (objHotelBDto.Tax != 0)
                    db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, objHotelBDto.Tax);
                else
                    db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, DBNull.Value);


                if (objHotelBDto.GST != 0)
                    db.AddInParameter(dbCmd, "@GST", DbType.Decimal, objHotelBDto.GST);
                else
                    db.AddInParameter(dbCmd, "@GST", DbType.Decimal, DBNull.Value);           
                     
                db.AddOutParameter(dbCmd, "@ISEXIST", DbType.Int32, 1);
                int r = db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ISEXIST"));
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
        #endregion

        #region Update Room Currency Price Details

        public int UpdateRoomCurrencyPriceDetails(HotelBDto objHotelBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_CURRENCY_PRICE_UPDATE);
                
                db.AddInParameter(dbCmd, "@CURRENCY_PRICE_ID", DbType.Int32, objHotelBDto.CurrancyPriceId);

                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, objHotelBDto.HotelId);
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, objHotelBDto.RoomTypeId);
                db.AddInParameter(dbCmd, "@HOTEL_CURRENCY", DbType.Int32, objHotelBDto.Currancy);

                if (objHotelBDto.Rate != 0)
                    db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, objHotelBDto.Rate);
                else
                    db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, DBNull.Value);


                if (objHotelBDto.Tax != 0)
                    db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, objHotelBDto.Tax);
                else
                    db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, DBNull.Value);


                if (objHotelBDto.GST != 0)
                    db.AddInParameter(dbCmd, "@GST", DbType.Decimal, objHotelBDto.GST);
                else
                    db.AddInParameter(dbCmd, "@GST", DbType.Decimal, DBNull.Value);  

                db.AddOutParameter(dbCmd, "@ISEXIST", DbType.Int32, 1);
                int r = db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ISEXIST"));
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
        #endregion

        #region Delete Room Currency Price Details

        public int DeleteRoomCurrencyPriceDetails(HotelBDto objHotelBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_CURRENCY_PRICE_DELETE);


                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, objHotelBDto.HotelId);
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, objHotelBDto.RoomTypeId);
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
        #endregion

        #region Select Room Currency Price
        public DataTable SelectRoomCurrencyPrice(int HotelId, int RoomTypeId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_CURRENCY_PRICE_SELECT);
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, HotelId);
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, RoomTypeId);
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
        #endregion

        




        #endregion
    }
}
