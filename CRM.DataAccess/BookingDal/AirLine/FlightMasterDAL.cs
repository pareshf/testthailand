using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using CRM.Model.Booking.AirlineModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.BookingDal.AirLine
{
    public class FlightMasterDAL
    {
        public DataTable GetFlightsById(int flightId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_FLIGHT_MASTER_SELECT_BY_ID");
                db.AddInParameter(dbCmd, "@FLIGHT_ID", DbType.Int32, flightId);
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

        public DataTable FindFlights(string searchPara)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_FLIGHT_MASTER_SELECT");
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

        public int InsertFlight(FlightBDto flight, ref int flightId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_FLIGHT_MASTER_INSERT");

                if (flight.AirlineId != 0)
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, flight.AirlineId);
                else
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, DBNull.Value);

                if (flight.DaysInOperation != 0)
                    db.AddInParameter(dbCmd, "@DAYS_IN_OPERATION", DbType.Int32, flight.DaysInOperation);
                else
                    db.AddInParameter(dbCmd, "@DAYS_IN_OPERATION", DbType.Int32, DBNull.Value);

                if (flight.EffectiveFrom != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM", DbType.DateTime, flight.EffectiveFrom);
                else
                    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM", DbType.DateTime, DBNull.Value);

                if (flight.DestinationCity != 0)
                    db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, flight.DestinationCity);
                else
                    db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, DBNull.Value);

                if (flight.SourceCity != 0)
                    db.AddInParameter(dbCmd, "@SOURCE_CITY_ID", DbType.Int32, flight.SourceCity);
                else
                    db.AddInParameter(dbCmd, "@SOURCE_CITY_ID", DbType.Int32, DBNull.Value);

                //if (flight.Duration != 0)
                db.AddInParameter(dbCmd, "@DURATION", DbType.Decimal, flight.Duration);
                //else
                //    db.AddInParameter(dbCmd, "@DURATION", DbType.Decimal, DBNull.Value);

                if (flight.TimeOfArrival != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, flight.TimeOfArrival);
                else
                    db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, DBNull.Value);

                if (flight.TimeOfDeparture != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@DEP_TIME", DbType.DateTime, flight.TimeOfDeparture);
                else
                    db.AddInParameter(dbCmd, "@DEP_TIME", DbType.DateTime, DBNull.Value);

                if (!String.IsNullOrEmpty(flight.FlightNo))
                    db.AddInParameter(dbCmd, "@FLIGHT_NO", DbType.String, flight.FlightNo);
                else
                    db.AddInParameter(dbCmd, "@FLIGHT_NO", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@IS_STOP", DbType.Boolean, flight.IsStop);

                if (!String.IsNullOrEmpty(flight.Via))
                    db.AddInParameter(dbCmd, "@VIA", DbType.String, flight.Via);
                else
                    db.AddInParameter(dbCmd, "@VIA", DbType.String, DBNull.Value);

              //  if (flight.Currancy != 0)
                    db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, flight.Currancy);
               // else
                  //  db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, DBNull.Value);

               // if (flight.TotalFare != 0)
                    db.AddInParameter(dbCmd, "@TOTAL_FARE", DbType.Int32, flight.TotalFare);
              //  else
                //    db.AddInParameter(dbCmd, "@TOTAL_FARE", DbType.Int32, DBNull.Value);

               // if (flight.Tax != 0)
                    db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, flight.Tax);
                //else
                //    db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, DBNull.Value);

               // if (flight.GST != 0)
                    db.AddInParameter(dbCmd, "@GST", DbType.Decimal, flight.GST);
                //else
                //    db.AddInParameter(dbCmd, "@GST", DbType.Decimal, DBNull.Value);

                db.AddInParameter(dbCmd, "@DISPLAY_ON_WEB", DbType.Boolean, flight.IsDisplayonweb);

                if (flight.UserId != 0)
                    db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, flight.UserId);
                else
                    db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, DBNull.Value);

                db.AddOutParameter(dbCmd, "@FLIGHT_ID", DbType.Int32, 9);
                Result = db.ExecuteNonQuery(dbCmd);
                flightId = Convert.ToInt32(db.GetParameterValue(dbCmd, "@FLIGHT_ID"));
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

        public int UpdateFlight(FlightBDto flight)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_FLIGHT_MASTER_UPDATE");

                db.AddInParameter(dbCmd, "@FLIGHT_ID", DbType.Int32, flight.FlightId);

                if (flight.AirlineId != 0)
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, flight.AirlineId);
                else
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, DBNull.Value);

                if (flight.DaysInOperation != 0)
                    db.AddInParameter(dbCmd, "@DAYS_IN_OPERATION", DbType.Int32, flight.DaysInOperation);
                else
                    db.AddInParameter(dbCmd, "@DAYS_IN_OPERATION", DbType.Int32, DBNull.Value);

                if (flight.EffectiveFrom != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM", DbType.DateTime, flight.EffectiveFrom);
                else
                    db.AddInParameter(dbCmd, "@EFFECTIVE_FROM", DbType.DateTime, DBNull.Value);

                if (flight.DestinationCity != 0)
                    db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, flight.DestinationCity);
                else
                    db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, DBNull.Value);

                if (flight.SourceCity != 0)
                    db.AddInParameter(dbCmd, "@SOURCE_CITY_ID", DbType.Int32, flight.SourceCity);
                else
                    db.AddInParameter(dbCmd, "@SOURCE_CITY_ID", DbType.Int32, DBNull.Value);

                //if (flight.Duration != 0)
                db.AddInParameter(dbCmd, "@DURATION", DbType.Decimal, flight.Duration);
                //else
                //    db.AddInParameter(dbCmd, "@DURATION", DbType.Decimal, DBNull.Value);

                if (flight.TimeOfArrival != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, flight.TimeOfArrival);
                else
                    db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.DateTime, DBNull.Value);

                if (flight.TimeOfDeparture != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@DEP_TIME", DbType.DateTime, flight.TimeOfDeparture);
                else
                    db.AddInParameter(dbCmd, "@DEP_TIME", DbType.DateTime, DBNull.Value);

                if (!String.IsNullOrEmpty(flight.FlightNo))
                    db.AddInParameter(dbCmd, "@FLIGHT_NO", DbType.String, flight.FlightNo);
                else
                    db.AddInParameter(dbCmd, "@FLIGHT_NO", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@IS_STOP", DbType.Boolean, flight.IsStop);

                if (!String.IsNullOrEmpty(flight.Via))
                    db.AddInParameter(dbCmd, "@VIA", DbType.String, flight.Via);
                else
                    db.AddInParameter(dbCmd, "@VIA", DbType.String, DBNull.Value);

               // if (flight.Currancy != 0)
                    db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, flight.Currancy);
               // else
                //    db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, DBNull.Value);

              //  if (flight.TotalFare != 0)
                    db.AddInParameter(dbCmd, "@TOTAL_FARE", DbType.Int32, flight.TotalFare);
               // else
                 //   db.AddInParameter(dbCmd, "@TOTAL_FARE", DbType.Int32, DBNull.Value);

              //  if (flight.Tax != 0)
                    db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, flight.Tax);
              //  else
               //     db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, DBNull.Value);

              //  if (flight.GST != 0)
                    db.AddInParameter(dbCmd, "@GST", DbType.Decimal, flight.GST);
              //  else
               //     db.AddInParameter(dbCmd, "@GST", DbType.Decimal, DBNull.Value);

                db.AddInParameter(dbCmd, "@DISPLAY_ON_WEB", DbType.Boolean, flight.IsDisplayonweb);




                if (flight.UserId != 0)
                    db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, flight.UserId);
                else
                    db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, DBNull.Value);

                Result = db.ExecuteNonQuery(dbCmd);
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

        public int DeleteFlights(string flights)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int ressult = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_FLIGHT_MASTER_DELETE");
                db.AddInParameter(dbCmd, "@FLIGHT_ID", DbType.String, flights);
                db.AddOutParameter(dbCmd, "@ERRORCODE", DbType.Int32, 3);
                ressult = db.ExecuteNonQuery(dbCmd);
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
            return ressult;
        }

        public DataTable GetFlightsGdsById(int flightId, int airlineId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_FLIGHT_GDS_DETAIL_SELECT");

                if (airlineId != 0)
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, airlineId);
                else
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, DBNull.Value);

                if (flightId != 0)
                    db.AddInParameter(dbCmd, "@FLIGHT_ID", DbType.Int32, flightId);
                else
                    db.AddInParameter(dbCmd, "@FLIGHT_ID", DbType.Int32, DBNull.Value);


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

        public int InsertFlightGds(FlightGdsBDto flightGds, ref int serialNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_FLIGHT_GDS_DETAIL_INSERT");

                if (flightGds.AirlineId != 0)
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, flightGds.AirlineId);
                else
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, DBNull.Value);

                if (flightGds.FlightId != 0)
                    db.AddInParameter(dbCmd, "@FLIGHT_ID", DbType.Int32, flightGds.FlightId);
                else
                    db.AddInParameter(dbCmd, "@FLIGHT_ID", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(flightGds.GDSAirportCode))
                    db.AddInParameter(dbCmd, "@GDS_AIRPORT_CODE", DbType.String, flightGds.GDSAirportCode);
                else
                    db.AddInParameter(dbCmd, "@GDS_AIRPORT_CODE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(flightGds.TimeLimit))
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.String, flightGds.TimeLimit);
                else
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(flightGds.BaggageAllwance))
                    db.AddInParameter(dbCmd, "@BAGGAGE_ALLWANCE", DbType.String, flightGds.BaggageAllwance);
                else
                    db.AddInParameter(dbCmd, "@BAGGAGE_ALLWANCE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(flightGds.Cancellation))
                    db.AddInParameter(dbCmd, "@CANCELLATION", DbType.String, flightGds.Cancellation);
                else
                    db.AddInParameter(dbCmd, "@CANCELLATION", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(flightGds.DateChange))
                    db.AddInParameter(dbCmd, "@DATE_CHANGE", DbType.String, flightGds.DateChange);
                else
                    db.AddInParameter(dbCmd, "@DATE_CHANGE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(flightGds.PaymentPolicy))
                    db.AddInParameter(dbCmd, "@PAYMENT_POLICY", DbType.String, flightGds.PaymentPolicy);
                else
                    db.AddInParameter(dbCmd, "@PAYMENT_POLICY", DbType.String, DBNull.Value);

                db.AddOutParameter(dbCmd, "@SR_NO", DbType.Int32, 9);
                Result = db.ExecuteNonQuery(dbCmd);
                serialNo = Convert.ToInt32(db.GetParameterValue(dbCmd, "@SR_NO"));
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

        public int UpdateFlightGds(FlightGdsBDto flightGds)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_FLIGHT_GDS_DETAIL_UPDATE");

                if (flightGds.AirlineId != 0)
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, flightGds.AirlineId);
                else
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, DBNull.Value);

                if (flightGds.FlightId != 0)
                    db.AddInParameter(dbCmd, "@FLIGHT_ID", DbType.Int32, flightGds.FlightId);
                else
                    db.AddInParameter(dbCmd, "@FLIGHT_ID", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(flightGds.GDSAirportCode))
                    db.AddInParameter(dbCmd, "@GDS_AIRPORT_CODE", DbType.String, flightGds.GDSAirportCode);
                else
                    db.AddInParameter(dbCmd, "@GDS_AIRPORT_CODE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(flightGds.TimeLimit))
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.String, flightGds.TimeLimit);
                else
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(flightGds.BaggageAllwance))
                    db.AddInParameter(dbCmd, "@BAGGAGE_ALLWANCE", DbType.String, flightGds.BaggageAllwance);
                else
                    db.AddInParameter(dbCmd, "@BAGGAGE_ALLWANCE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(flightGds.Cancellation))
                    db.AddInParameter(dbCmd, "@CANCELLATION", DbType.String, flightGds.Cancellation);
                else
                    db.AddInParameter(dbCmd, "@CANCELLATION", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(flightGds.DateChange))
                    db.AddInParameter(dbCmd, "@DATE_CHANGE", DbType.String, flightGds.DateChange);
                else
                    db.AddInParameter(dbCmd, "@DATE_CHANGE", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(flightGds.PaymentPolicy))
                    db.AddInParameter(dbCmd, "@PAYMENT_POLICY", DbType.String, flightGds.PaymentPolicy);
                else
                    db.AddInParameter(dbCmd, "@PAYMENT_POLICY", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, flightGds.SerialNo);
                db.AddOutParameter(dbCmd, "@IS_UPDATE", DbType.Int32, 0);
                Result = db.ExecuteNonQuery(dbCmd);
                //Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_UPDATE"));
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

        public int DeleteFlightsGds(int serialNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int ressult = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_FLIGHT_GDS_DETAIL_DELETE");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, serialNo);
                db.AddOutParameter(dbCmd, "@ERRORCODE", DbType.Int32, 3);
                ressult = db.ExecuteNonQuery(dbCmd);
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
            return ressult;
        }

        #region Insert Flight Currency Price Details

        public int InsertFlightCurrencyPriceDetails(FlightBDto flightGds)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_FLIGHT_CURRENCY_PRICE_INSERT);


                db.AddInParameter(dbCmd, "@FLIGHT_ID", DbType.Int32, flightGds.FlightId);
                db.AddInParameter(dbCmd, "@FLIGHT_CURRENCY", DbType.Int32, flightGds.Currancy);
              //  if (flightGds.Adult_Amt != 0)
                    db.AddInParameter(dbCmd, "@AUDULT_AMT", DbType.Decimal, flightGds.Adult_Amt);
                //else
                  //  db.AddInParameter(dbCmd, "@AUDULT_AMT", DbType.Decimal, DBNull.Value);


              //  if (flightGds.Adult_Tax != 0)
                    db.AddInParameter(dbCmd, "@AUDULT_TAX", DbType.Decimal, flightGds.Adult_Tax);
               // else
                //    db.AddInParameter(dbCmd, "@AUDULT_TAX", DbType.Decimal, DBNull.Value);


              //  if (flightGds.Adult_Gst != 0)
                    db.AddInParameter(dbCmd, "@AUDULT_GST", DbType.Decimal, flightGds.Adult_Gst);
               // else
                //    db.AddInParameter(dbCmd, "@AUDULT_GST", DbType.Decimal, DBNull.Value);


              //  if (flightGds.Child_Amt != 0)
                    db.AddInParameter(dbCmd, "@CHILD_AMT", DbType.Decimal, flightGds.Child_Amt);
              //  else
                //    db.AddInParameter(dbCmd, "@CHILD_AMT", DbType.Decimal, DBNull.Value);


              //  if (flightGds.Child_Tax != 0)
                    db.AddInParameter(dbCmd, "@CHILD_TAX", DbType.Decimal, flightGds.Child_Tax);
              //  else
                 //   db.AddInParameter(dbCmd, "@CHILD_TAX", DbType.Decimal, DBNull.Value);


             //   if (flightGds.Child_Gst != 0)
                    db.AddInParameter(dbCmd, "@CHILD_GST", DbType.Decimal, flightGds.Child_Gst);
             //   else
               //     db.AddInParameter(dbCmd, "@CHILD_GST", DbType.Decimal, DBNull.Value);    


            //    if (flightGds.Infant_Amt != 0)
                    db.AddInParameter(dbCmd, "@INFANT_AMT", DbType.Decimal, flightGds.Infant_Amt);
               // else
                  //  db.AddInParameter(dbCmd, "@INFANT_AMT", DbType.Decimal, DBNull.Value);


              //  if (flightGds.Infant_Tax != 0)
                    db.AddInParameter(dbCmd, "@INFANT_TAX", DbType.Decimal, flightGds.Infant_Tax);
              //  else
               //     db.AddInParameter(dbCmd, "@INFANT_TAX", DbType.Decimal, DBNull.Value);


             //   if (flightGds.Infant_Gst != 0)
                    db.AddInParameter(dbCmd, "@INFANT_GST", DbType.Decimal, flightGds.Infant_Gst);
             //   else
               //     db.AddInParameter(dbCmd, "@INFANT_GST", DbType.Decimal, DBNull.Value);

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

        #region Update Flight Currency Price Details

        public int UpdateFlightCurrencyPriceDetails(FlightBDto flightGds)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_FLIGHT_CURRENCY_PRICE_UPDATE);

                db.AddInParameter(dbCmd, "@CURRENCY_PRICE_ID", DbType.Int32, flightGds.CurrancyPriceId);
                db.AddInParameter(dbCmd, "@FLIGHT_ID", DbType.Int32, flightGds.FlightId);
                db.AddInParameter(dbCmd, "@FLIGHT_CURRENCY", DbType.Int32, flightGds.Currancy);
               // if (flightGds.Adult_Amt != 0)
                    db.AddInParameter(dbCmd, "@AUDULT_AMT", DbType.Decimal, flightGds.Adult_Amt);
               // else
                //    db.AddInParameter(dbCmd, "@AUDULT_AMT", DbType.Decimal, DBNull.Value);


              //  if (flightGds.Adult_Tax != 0)
                    db.AddInParameter(dbCmd, "@AUDULT_TAX", DbType.Decimal, flightGds.Adult_Tax);
              //  else
                //    db.AddInParameter(dbCmd, "@AUDULT_TAX", DbType.Decimal, DBNull.Value);


               // if (flightGds.Adult_Gst != 0)
                    db.AddInParameter(dbCmd, "@AUDULT_GST", DbType.Decimal, flightGds.Adult_Gst);
              //  else
                  //  db.AddInParameter(dbCmd, "@AUDULT_GST", DbType.Decimal, DBNull.Value);
                

                //if (flightGds.Child_Amt != 0)
                    db.AddInParameter(dbCmd, "@CHILD_AMT", DbType.Decimal, flightGds.Child_Amt);
               // else
                //    db.AddInParameter(dbCmd, "@CHILD_AMT", DbType.Decimal, DBNull.Value);


               // if (flightGds.Child_Tax != 0)
                    db.AddInParameter(dbCmd, "@CHILD_TAX", DbType.Decimal, flightGds.Child_Tax);
                //else
                  //  db.AddInParameter(dbCmd, "@CHILD_TAX", DbType.Decimal, DBNull.Value);


              //  if (flightGds.Child_Gst != 0)
                    db.AddInParameter(dbCmd, "@CHILD_GST", DbType.Decimal, flightGds.Child_Gst);
              //  else
                //    db.AddInParameter(dbCmd, "@CHILD_GST", DbType.Decimal, DBNull.Value);   

               // if (flightGds.Infant_Amt != 0)
                    db.AddInParameter(dbCmd, "@INFANT_AMT", DbType.Decimal, flightGds.Infant_Amt);
               // else
                  //  db.AddInParameter(dbCmd, "@INFANT_AMT", DbType.Decimal, DBNull.Value);


              //  if (flightGds.Infant_Tax != 0)
                    db.AddInParameter(dbCmd, "@INFANT_TAX", DbType.Decimal, flightGds.Infant_Tax);
               // else
                  //  db.AddInParameter(dbCmd, "@INFANT_TAX", DbType.Decimal, DBNull.Value);


             //   if (flightGds.Infant_Gst != 0)
                    db.AddInParameter(dbCmd, "@INFANT_GST", DbType.Decimal, flightGds.Infant_Gst);
              //  else
                //    db.AddInParameter(dbCmd, "@INFANT_GST", DbType.Decimal, DBNull.Value);

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

        //#region Delete Room Currency Price Details

        //public int DeleteRoomCurrencyPriceDetails(HotelBDto flightGds)
        //{
        //    Database db = null;
        //    DbCommand dbCmd = null;
        //    int Result = 0;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
        //        dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_CURRENCY_PRICE_DELETE);


        //        db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, flightGds.HotelId);
        //        db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, flightGds.RoomTypeId);
        //        Result = db.ExecuteNonQuery(dbCmd);
        //        return Result;
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
        //    return 0;
        //}
        //#endregion

        #region Select Flight Currency Price
        public DataTable SelectFlightCurrencyPrice(int flightId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_FLIGHT_CURRENCY_PRICE_SELECT);
                db.AddInParameter(dbCmd, "@FLIGHT_ID", DbType.Int32, flightId);
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
    }
}
