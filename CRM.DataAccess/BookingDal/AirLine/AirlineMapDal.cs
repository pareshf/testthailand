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
   public class AirlineMapDal
    {

        #region InsertAirLineMap
        /// <summary>
        /// Insert GdsAirportDal detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertAirLineMap(AirlineMapBDto objGdsAirportBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AIRLINE_AIRPORT_MAP_INSERT);
                db.AddInParameter(dbCmd, "@AIRPORT_ID", DbType.Int32, objGdsAirportBDto.AirportId);
                db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, objGdsAirportBDto.AirLineID);
                db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, objGdsAirportBDto.DestinationCityId);
                db.AddInParameter(dbCmd, "@BAGGAGE_ALLOWANCE", DbType.String, objGdsAirportBDto.BaggageAllowance);
				db.AddInParameter(dbCmd, "@CANCELLATION", DbType.String, objGdsAirportBDto.Cancellation);
				db.AddInParameter(dbCmd, "@DATE_CHANGE", DbType.String, objGdsAirportBDto.DateChange);
				db.AddInParameter(dbCmd, "@PAYMENT_POLICY", DbType.String, objGdsAirportBDto.PaymentPolicy);

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

        #region UpdateAirLineMap
        /// <summary>
        /// Insert GdsAirportDal detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateAirLineMap(AirlineMapBDto objGdsAirportBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AIRLINE_AIRPORT_MAP_UPDATE);

                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, objGdsAirportBDto.srNo);
                db.AddInParameter(dbCmd, "@AIRPORT_ID", DbType.Int32, objGdsAirportBDto.AirportId);
                db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, objGdsAirportBDto.AirLineID);
                db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, objGdsAirportBDto.DestinationCityId);
                db.AddInParameter(dbCmd, "@BAGGAGE_ALLOWANCE", DbType.String, objGdsAirportBDto.BaggageAllowance);
				db.AddInParameter(dbCmd, "@CANCELLATION", DbType.String, objGdsAirportBDto.Cancellation);
				db.AddInParameter(dbCmd, "@DATE_CHANGE", DbType.String, objGdsAirportBDto.DateChange);
				db.AddInParameter(dbCmd, "@PAYMENT_POLICY", DbType.String, objGdsAirportBDto.PaymentPolicy);

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

        #region Delete AirLineMap
        /// <summary>
        /// Delete Title detail.
        /// </summary>
        /// <param name="idCollections">AddressType Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteAirLineMap(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AIRLINE_AIRPORT_MAP_DELETE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.String, idCollections);
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

        #region FindAirlineMap

        public DataTable FindAirlineMap(string searchPara)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_AIRLINE_AIRPORT_MAP_SELECT");
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

        #region GetAirlineMap


        public DataTable GetAirlineMap(int srNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_AIRLINE_AIRPORT_MAP_SELECT_BYID");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, srNo);
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

	   
        #region GetAirlineMap


        public DataTable GetAirlineMapDocument()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_COMMON_DOCUMENT_MASTER_SELECT");				
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

		public DataTable GetDocumentOutputData(string columnList)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_COMMON_DOCUMENT_MASTER_OUTPUT_DATA_SELECT");
				db.AddInParameter(dbCmd, "@SELECTED_COLUMN", DbType.String, columnList);

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

        #region GetAirlineMapByCity


        public DataTable GetAirlineMapByCity(int airlineId,int destinationCity)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_AIRLINE_AIRPORT_MAP_SELECT_BYCITY");
                db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, airlineId);
                db.AddInParameter(dbCmd, "@DESTINATION_CITY", DbType.Int32, destinationCity);
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

		#region InsertAirLineMapDocument

		/// <summary>
		/// Insert GdsAirportDal detail.
		/// </summary>
		/// <param name="xmlData">Data that converted into xml format.</param>
		/// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
		public int InsertAirLineMapDocument(AirlineMapBDto objGdsAirportBDto)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("USP_COMMON_DOCUMENT_MASTER_INSERT");
				if (objGdsAirportBDto.FaqDocument != null)
					db.AddInParameter(dbCmd, "@FAQ_DOC", DbType.Binary, objGdsAirportBDto.FaqDocument);
				else
					db.AddInParameter(dbCmd, "@FAQ_DOC", DbType.Binary, DBNull.Value);

				if (!String.IsNullOrEmpty(objGdsAirportBDto.FaqDocType))
					db.AddInParameter(dbCmd, "@FAQ_DOC_CONTENT_TYPE", DbType.String, objGdsAirportBDto.FaqDocType);
				else
					db.AddInParameter(dbCmd, "@FAQ_DOC_CONTENT_TYPE", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(objGdsAirportBDto.FaqDocFileName))
					db.AddInParameter(dbCmd, "@FAQ_DOC_FILE_NAME", DbType.String, objGdsAirportBDto.FaqDocFileName);
				else
					db.AddInParameter(dbCmd, "@FAQ_DOC_FILE_NAME", DbType.String, DBNull.Value);

				if (objGdsAirportBDto.TermsDocument != null)
					db.AddInParameter(dbCmd, "@T_C_DOC", DbType.Binary, objGdsAirportBDto.TermsDocument);
				else
					db.AddInParameter(dbCmd, "@T_C_DOC", DbType.Binary, DBNull.Value);

				if (!String.IsNullOrEmpty(objGdsAirportBDto.TermsDocType))
					db.AddInParameter(dbCmd, "@T_C_DOC_CONTENT_TYPE", DbType.String, objGdsAirportBDto.TermsDocType);
				else
					db.AddInParameter(dbCmd, "@T_C_DOC_CONTENT_TYPE", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(objGdsAirportBDto.TermsDocFileName))
					db.AddInParameter(dbCmd, "@T_C_DOC_FILE_NAME", DbType.String, objGdsAirportBDto.TermsDocFileName);
				else
					db.AddInParameter(dbCmd, "@T_C_DOC_FILE_NAME", DbType.String, DBNull.Value);

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

		#region UpdateAirLineMapDocument
		/// <summary>
		/// Insert GdsAirportDal detail.
		/// </summary>
		/// <param name="xmlData">Data that converted into xml format.</param>
		/// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
		public int UpdateAirLineMapDocument(AirlineMapBDto objGdsAirportBDto)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AIRLINE_AIRPORT_MAP_UPDATE);

				db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, objGdsAirportBDto.srNo);
				db.AddInParameter(dbCmd, "@AIRPORT_ID", DbType.Int32, objGdsAirportBDto.AirportId);
				db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, objGdsAirportBDto.AirLineID);
				db.AddInParameter(dbCmd, "@DESTINATION_CITY_ID", DbType.Int32, objGdsAirportBDto.DestinationCityId);
				db.AddInParameter(dbCmd, "@BAGGAGE_ALLOWANCE", DbType.String, objGdsAirportBDto.BaggageAllowance);
				db.AddInParameter(dbCmd, "@CANCELLATION", DbType.String, objGdsAirportBDto.Cancellation);
				db.AddInParameter(dbCmd, "@DATE_CHANGE", DbType.String, objGdsAirportBDto.DateChange);
				db.AddInParameter(dbCmd, "@PAYMENT_POLICY", DbType.String, objGdsAirportBDto.PaymentPolicy);

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
    }
}
