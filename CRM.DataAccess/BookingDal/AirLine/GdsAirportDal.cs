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
    public class GdsAirportDal
    {
        #region Get GdsAirportDal
        /// <summary>
        /// Gets GdsAirportDal list.
        /// </summary>
        /// <returns>Returns dataset contains RoomType data.</returns>
        public DataSet GetGdsAirportDal(String SearchParameter)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AIRLINE_GDS_AIRPORT_SELECT);
                if (!String.IsNullOrEmpty(SearchParameter))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, SearchParameter);
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

        #region Insert GdsAirport
        /// <summary>
        /// Insert GdsAirportDal detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertGdsAirport(GdsAirportBDto objGdsAirportBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AIRLINE_GDS_AIRPORT_INSERT);
                db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.String, objGdsAirportBDto.AirlineId);
                db.AddInParameter(dbCmd, "@GDS_AIRPORT_CODE", DbType.String, objGdsAirportBDto.GDSAirportCode);
                db.AddInParameter(dbCmd, "@GDS_PRILIMINARY_BOOKING", DbType.String, objGdsAirportBDto.GDSPriliminaryBooking);
                db.AddInParameter(dbCmd, "@TOTAL_FARE", DbType.Decimal, objGdsAirportBDto.TotalFare);
                db.AddInParameter(dbCmd, "@TOTAL_TAXES", DbType.Decimal, objGdsAirportBDto.TotalTaxs);
                db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.DateTime, objGdsAirportBDto.TimeLimit);
                db.AddInParameter(dbCmd, "@BAGGAGE_ALLWANCE", DbType.String, objGdsAirportBDto.BaggageAllwance);
                db.AddInParameter(dbCmd, "@CANCELLATION_POLICY", DbType.String, objGdsAirportBDto.CancellationPolicy);
                db.AddInParameter(dbCmd, "@DATE_CHANGE_POLICY", DbType.String, objGdsAirportBDto.DatechangePolicy);
                db.AddInParameter(dbCmd, "@LOCAL_CONTACT_NUM_OF_PAX", DbType.String, objGdsAirportBDto.LocalContactPax);
                db.AddInParameter(dbCmd, "@EMAIL_ID", DbType.String, objGdsAirportBDto.Email);
                db.AddInParameter(dbCmd, "@PAYMENT_POLICCY", DbType.String, objGdsAirportBDto.PaymentPolicy);
                db.AddInParameter(dbCmd, "@FAQ_DOCUMENT", DbType.Binary, objGdsAirportBDto.FaqDocument);
                db.AddInParameter(dbCmd, "@TERMS_AND_CONDITION_DOCUMENT", DbType.Binary, objGdsAirportBDto.TermsAndConditionDocument);
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
        #endregion

        #region Delete GdsAirport
        /// <summary>
        /// Delete Title detail.
        /// </summary>
        /// <param name="idCollections">AddressType Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteGdsAirport(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AIRLINE_GDS_AIRPORT_DELETE);
                db.AddInParameter(dbCmd, "@GDS_AIRPORT_CODE", DbType.String, idCollections);
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

        #region Update  GdsAirport
        /// <summary>
        /// Update GdsAirport detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateGdsAirport(GdsAirportBDto objGdsAirportBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AIRLINE_GDS_AIRPORT_UPDATE);


                db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.String, objGdsAirportBDto.AirlineId);
                db.AddInParameter(dbCmd, "@GDS_AIRPORT_CODE", DbType.String, objGdsAirportBDto.GDSAirportCode);
                db.AddInParameter(dbCmd, "@GDS_PRILIMINARY_BOOKING", DbType.String, objGdsAirportBDto.GDSPriliminaryBooking);
                db.AddInParameter(dbCmd, "@TOTAL_FARE", DbType.Decimal, objGdsAirportBDto.TotalFare);
                db.AddInParameter(dbCmd, "@TOTAL_TAXES", DbType.Decimal, objGdsAirportBDto.TotalTaxs);
                db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.DateTime, objGdsAirportBDto.TimeLimit);
                db.AddInParameter(dbCmd, "@BAGGAGE_ALLWANCE", DbType.String, objGdsAirportBDto.BaggageAllwance);
                db.AddInParameter(dbCmd, "@CANCELLATION_POLICY", DbType.String, objGdsAirportBDto.CancellationPolicy);
                db.AddInParameter(dbCmd, "@DATE_CHANGE_POLICY", DbType.String, objGdsAirportBDto.DatechangePolicy);
                db.AddInParameter(dbCmd, "@LOCAL_CONTACT_NUM_OF_PAX", DbType.String, objGdsAirportBDto.LocalContactPax);
                db.AddInParameter(dbCmd, "@EMAIL_ID", DbType.String, objGdsAirportBDto.Email);
                db.AddInParameter(dbCmd, "@PAYMENT_POLICCY", DbType.String, objGdsAirportBDto.PaymentPolicy);
                db.AddInParameter(dbCmd, "@FAQ_DOCUMENT", DbType.Binary, objGdsAirportBDto.FaqDocument);
                db.AddInParameter(dbCmd, "@TERMS_AND_CONDITION_DOCUMENT", DbType.Binary, objGdsAirportBDto.TermsAndConditionDocument);
                
                Result= db.ExecuteNonQuery(dbCmd);
                
                //if (db.GetParameterValue(dbCmd, "@IS_UPDATE") != DBNull.Value)
                //    Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_UPDATE"));
                //if (Result == 1)
                //    return 1; // SUCCESSFUL INSERTION RETURN TRUE
                //else
                //    return 0; // UNSUCCESSFUL INSERTION RETUN FALSE ( ALREADY EXISTS )

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

        #region IDisposable Members

        public void Dispose()
        {
            GC.Collect();
        }

        #endregion

        #region InsertUpdate Airport
        /// <summary>
        /// Insert GdsAirportDal detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertUpdateAirport(GdsAirportBDto objGdsAirportBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AIRPORT_MASTER_INSERT_UPDATE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, objGdsAirportBDto.SrNo);
                db.AddInParameter(dbCmd, "@DESTINATION_CITY", DbType.Int32, objGdsAirportBDto.DestinationCity);
                db.AddInParameter(dbCmd, "@AIRPORT_NAME", DbType.String, objGdsAirportBDto.AirportName);
                db.AddInParameter(dbCmd, "@AIRPORT_CODE", DbType.String, objGdsAirportBDto.AirportCode);
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
        #endregion

        #region Delete Airport
        /// <summary>
        /// Delete Title detail.
        /// </summary>
        /// <param name="idCollections">AddressType Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteAirport(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AIRPORT_MASTER_DELETE);
                db.AddInParameter(dbCmd, "@DESTINATION_CITY", DbType.String, idCollections);
                
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
        #endregion

        #region Delete AirportBYSrNo
        /// <summary>
        /// Delete Title detail.
        /// </summary>
        /// <param name="idCollections">AddressType Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteAirportBySrNo(int SrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AIRPORT_MASTER_DELETE_BYID);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.String, SrNo);

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
        #endregion

        #region FindAirport

        public DataTable FindAirport(string searchPara)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_AIRPORT_MASTER_SELECT");
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

        #region GetAirportByCity


        public DataTable GetAirportByCity(int DestinationCity)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_AIRPORT_MASTER_SELECT_BYID");
                db.AddInParameter(dbCmd, "@DESTINATION_CITY", DbType.Int32, DestinationCity);
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
