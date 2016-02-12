#region Impoerts assemblies
using System;
using System.Data;
using System.Data.Common;
using CRM.Model.Booking.Bus;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
#endregion

namespace CRM.DataAccess.BookingDal.Bus
{
    public class BusDal : IDisposable
    {
        #region Insert Fare Bus Master
        /// <summary>
        /// Insert customer detail.
        /// </summary>
        /// <param name="customer">CustomerBDto object that customer data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int InsertFareBusMaster(BusBDto BusBDto, ref int BusId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_BUS_MASTER_INSERT);

                db.AddInParameter(dbCmd, "@BUS_CODE", DbType.String, BusBDto.BusCode);
                db.AddInParameter(dbCmd, "@BUS_NAME", DbType.String, BusBDto.BusName);
                db.AddInParameter(dbCmd, "@SOURCE_CITY_ID", DbType.Int32, BusBDto.SourceCity);
                db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, BusBDto.DestinationCity);
                db.AddInParameter(dbCmd, "@DEP_TIME", DbType.DateTime, BusBDto.DepTime);
                db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, BusBDto.ArrivalTime);
                db.AddInParameter(dbCmd, "@EFFECTIVE_FROM", DbType.DateTime, BusBDto.EffecticeFrom);
                db.AddInParameter(dbCmd, "@DURATION", DbType.Decimal, BusBDto.Duration);

                db.AddOutParameter(dbCmd, "@BUS_ID", DbType.Int32, 9);
                Result = db.ExecuteNonQuery(dbCmd);
                BusId = Convert.ToInt32(db.GetParameterValue(dbCmd, "@BUS_ID"));              

              
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

        #region Update Fare Bus Master
        /// <summary>
        /// Insert customer detail.
        /// </summary>
        /// <param name="customer">CustomerBDto object that customer data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int UpdateFareBusMaster(BusBDto BusBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_BUS_MASTER_UPDATE);

                db.AddInParameter(dbCmd, "@BUS_ID", DbType.Int32, BusBDto.BusId);
                db.AddInParameter(dbCmd, "@BUS_CODE", DbType.String, BusBDto.BusCode);
                db.AddInParameter(dbCmd, "@BUS_NAME", DbType.String, BusBDto.BusName);
                db.AddInParameter(dbCmd, "@SOURCE_CITY_ID", DbType.Int32, BusBDto.SourceCity);
                db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, BusBDto.DestinationCity);
                db.AddInParameter(dbCmd, "@DEP_TIME", DbType.DateTime, BusBDto.DepTime);
                db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, BusBDto.ArrivalTime);
                
                db.AddInParameter(dbCmd, "@EFFECTIVE_FROM", DbType.DateTime, BusBDto.EffecticeFrom);
                db.AddInParameter(dbCmd, "@DURATION", DbType.Decimal, BusBDto.Duration);
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

        #region DeleteFareBusMaster
        /// <summary>
        /// Delete customers detail.
        /// </summary>
        /// <param name="idCollections">Customer Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteFareBusMaster(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_BUS_MASTER_DELETE);
                db.AddInParameter(dbCmd, "@BUS_ID", DbType.String, idCollections);
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

        #region FindFareBusMaster

        public DataTable FindFareBusMaster(string searchPara)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_BUS_MASTER_SELECT");
                if (!string.IsNullOrEmpty(searchPara))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, searchPara);
                else
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, DBNull.Value);
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

        #region GetFareBusMasterById

        public DataTable GetFareBusMasterById(int BusId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_BUS_MASTER_SELECT_BY_ID");
                db.AddInParameter(dbCmd, "@BUS_ID", DbType.Int32, BusId);
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
        
        #region Insert Bus Currency Price Details

        public int InsertBusCurrencyPriceDetails(BusBDto flightGds)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_BUS_CURRENCY_PRICE_INSERT);


                db.AddInParameter(dbCmd, "@BUS_ID", DbType.Int32, flightGds.BusId);
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.Int32, flightGds.Currancy);
                if (flightGds.Adult_Amt != 0)
                    db.AddInParameter(dbCmd, "@AUDULT_AMT", DbType.Decimal, flightGds.Adult_Amt);
                else
                    db.AddInParameter(dbCmd, "@AUDULT_AMT", DbType.Decimal, DBNull.Value);


                if (flightGds.Adult_Tax != 0)
                    db.AddInParameter(dbCmd, "@AUDULT_TAX", DbType.Decimal, flightGds.Adult_Tax);
                else
                    db.AddInParameter(dbCmd, "@AUDULT_TAX", DbType.Decimal, DBNull.Value);


                if (flightGds.Adult_Gst != 0)
                    db.AddInParameter(dbCmd, "@AUDULT_GST", DbType.Decimal, flightGds.Adult_Gst);
                else
                    db.AddInParameter(dbCmd, "@AUDULT_GST", DbType.Decimal, DBNull.Value);


                if (flightGds.Child_Amt != 0)
                    db.AddInParameter(dbCmd, "@CHILD_AMT", DbType.Decimal, flightGds.Child_Amt);
                else
                    db.AddInParameter(dbCmd, "@CHILD_AMT", DbType.Decimal, DBNull.Value);


                if (flightGds.Child_Tax != 0)
                    db.AddInParameter(dbCmd, "@CHILD_TAX", DbType.Decimal, flightGds.Child_Tax);
                else
                    db.AddInParameter(dbCmd, "@CHILD_TAX", DbType.Decimal, DBNull.Value);


                if (flightGds.Child_Gst != 0)
                    db.AddInParameter(dbCmd, "@CHILD_GST", DbType.Decimal, flightGds.Child_Gst);
                else
                    db.AddInParameter(dbCmd, "@CHILD_GST", DbType.Decimal, DBNull.Value);


                if (flightGds.Infant_Amt != 0)
                    db.AddInParameter(dbCmd, "@INFANT_AMT", DbType.Decimal, flightGds.Infant_Amt);
                else
                    db.AddInParameter(dbCmd, "@INFANT_AMT", DbType.Decimal, DBNull.Value);


                if (flightGds.Infant_Tax != 0)
                    db.AddInParameter(dbCmd, "@INFANT_TAX", DbType.Decimal, flightGds.Infant_Tax);
                else
                    db.AddInParameter(dbCmd, "@INFANT_TAX", DbType.Decimal, DBNull.Value);


                if (flightGds.Infant_Gst != 0)
                    db.AddInParameter(dbCmd, "@INFANT_GST", DbType.Decimal, flightGds.Infant_Gst);
                else
                    db.AddInParameter(dbCmd, "@INFANT_GST", DbType.Decimal, DBNull.Value);

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

        #region Update Bus Currency Price Details

        public int UpdateBusCurrencyPriceDetails(BusBDto flightGds)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_BUS_CURRENCY_PRICE_UPDATE);

                db.AddInParameter(dbCmd, "@CURRENCY_PRICE_ID", DbType.Int32, flightGds.CurrancyPriceId);
                db.AddInParameter(dbCmd, "@BUS_ID", DbType.Int32, flightGds.BusId);
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.Int32, flightGds.Currancy);
                if (flightGds.Adult_Amt != 0)
                    db.AddInParameter(dbCmd, "@AUDULT_AMT", DbType.Decimal, flightGds.Adult_Amt);
                else
                    db.AddInParameter(dbCmd, "@AUDULT_AMT", DbType.Decimal, DBNull.Value);


                if (flightGds.Adult_Tax != 0)
                    db.AddInParameter(dbCmd, "@AUDULT_TAX", DbType.Decimal, flightGds.Adult_Tax);
                else
                    db.AddInParameter(dbCmd, "@AUDULT_TAX", DbType.Decimal, DBNull.Value);


                if (flightGds.Adult_Gst != 0)
                    db.AddInParameter(dbCmd, "@AUDULT_GST", DbType.Decimal, flightGds.Adult_Gst);
                else
                    db.AddInParameter(dbCmd, "@AUDULT_GST", DbType.Decimal, DBNull.Value);


                if (flightGds.Child_Amt != 0)
                    db.AddInParameter(dbCmd, "@CHILD_AMT", DbType.Decimal, flightGds.Child_Amt);
                else
                    db.AddInParameter(dbCmd, "@CHILD_AMT", DbType.Decimal, DBNull.Value);


                if (flightGds.Child_Tax != 0)
                    db.AddInParameter(dbCmd, "@CHILD_TAX", DbType.Decimal, flightGds.Child_Tax);
                else
                    db.AddInParameter(dbCmd, "@CHILD_TAX", DbType.Decimal, DBNull.Value);


                if (flightGds.Child_Gst != 0)
                    db.AddInParameter(dbCmd, "@CHILD_GST", DbType.Decimal, flightGds.Child_Gst);
                else
                    db.AddInParameter(dbCmd, "@CHILD_GST", DbType.Decimal, DBNull.Value);

                if (flightGds.Infant_Amt != 0)
                    db.AddInParameter(dbCmd, "@INFANT_AMT", DbType.Decimal, flightGds.Infant_Amt);
                else
                    db.AddInParameter(dbCmd, "@INFANT_AMT", DbType.Decimal, DBNull.Value);


                if (flightGds.Infant_Tax != 0)
                    db.AddInParameter(dbCmd, "@INFANT_TAX", DbType.Decimal, flightGds.Infant_Tax);
                else
                    db.AddInParameter(dbCmd, "@INFANT_TAX", DbType.Decimal, DBNull.Value);


                if (flightGds.Infant_Gst != 0)
                    db.AddInParameter(dbCmd, "@INFANT_GST", DbType.Decimal, flightGds.Infant_Gst);
                else
                    db.AddInParameter(dbCmd, "@INFANT_GST", DbType.Decimal, DBNull.Value);

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

        #region Select Bus Currency Price
        public DataTable SelectBusCurrencyPrice(int BusID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_BUS_CURRENCY_PRICE_SELECT);
                db.AddInParameter(dbCmd, "@BUS_ID", DbType.Int32, BusID);
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

        #region IDisposable Members
        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
    }
}
