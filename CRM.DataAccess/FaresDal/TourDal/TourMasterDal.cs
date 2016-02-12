#region Program Information
/**********************************************************************************************************************************************
 Class Name           : TourMaster
 Class Description    : Implementation Logic customer database releated transaction.
 Author               : Chirag.
 Created Date         : April 5, 2010
***********************************************************************************************************************************************/
#endregion

#region Impoerts assemblies
using System;
using System.Data;
using System.Data.Common;
using CRM.Model.FaresModel.TourModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
#endregion

namespace CRM.DataAccess.FaresDal.TourDal
{
    public class TourMasterDal : IDisposable
    {
        #region Get Tourdetail
        /// <summary>
        /// Gets Tour information.
        /// </summary>
        /// <returns>Returns dataset contains Tour information.</returns>
        public DataSet GetTourInfo()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_SELECT);
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

        #region search TourDetail
        /// <summary>
        /// Gets Tour Detail by filter criteria.
        /// </summary>
        /// <returns>Returns dataset contains Tour data.</returns>
        public DataSet GetTourInfo(string searchParameter)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_SELECT);
                if (!String.IsNullOrEmpty(searchParameter))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, searchParameter);
                else
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, DBNull.Value);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                //bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                //if (rethrow)
                //{
                //    throw ex;
                //}
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return ds;
        }
        #endregion

        #region DeleteTourInfo

        public int DeleteTourInfo(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_DELETE);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.String, idCollections);
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

        #region Insert TourInfo
        /// <summary>
        /// Insert TourInfo.
        /// </summary>
        /// <param name="customer">TourBDto object that TourInfo data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int InsertTourInfo(TourBDto objTourBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_INSERT);
                db.AddInParameter(dbCmd, "@TOUR_CODE", DbType.String, objTourBDto.TourCode);
                db.AddInParameter(dbCmd, "@TOUR_SHORT_NAME", DbType.String, objTourBDto.TourSortName);
                db.AddInParameter(dbCmd, "@TOUR_LONG_DESC", DbType.String, objTourBDto.TourLongName);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY_TYPE_ID", DbType.Int32, objTourBDto.TourItenaryId);
                db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, objTourBDto.TourTypeId);
                db.AddInParameter(dbCmd, "@TOUR_FROM_DATE", DbType.DateTime, objTourBDto.TourFormDate);
                db.AddInParameter(dbCmd, "@TOUR_TO_DATE", DbType.DateTime, objTourBDto.TourToDate);
                db.AddInParameter(dbCmd, "@NO_OF_DAYS", DbType.Int32, objTourBDto.NoOfDays);

                //db.AddInParameter(dbCmd, "@TOUR_COST_ADULT", DbType.Int32, objTourBDto.TourCostAdult);
                //db.AddInParameter(dbCmd, "@TOUR_COST_CHILD_WITH_BED", DbType.Int32, objTourBDto.TourCostChildWithBed);
                //db.AddInParameter(dbCmd, "@TOUR_COST_CHILD_WITHOUT_BED", DbType.Int32, objTourBDto.TourCostChildWithoutBed);
                //db.AddInParameter(dbCmd, "@INFANT_PRICE", DbType.Int32, objTourBDto.InfantPrice);
                //db.AddInParameter(dbCmd, "@TOUR_CURRANCY", DbType.String, objTourBDto.TourCurrancy);


                db.AddInParameter(dbCmd, "@NO_OF_SEATS", DbType.Int32, objTourBDto.NoOfSeats);
                db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, objTourBDto.NoOfNights);
                db.AddInParameter(dbCmd, "@COUNTRIES_FOR_VISA", DbType.String, objTourBDto.CountriesForVisa);
                db.AddInParameter(dbCmd, "@CITIES_TO_TRAVEL", DbType.String, objTourBDto.CityToTravel);
                db.AddInParameter(dbCmd, "@START_END_CITY", DbType.String, objTourBDto.StartEndCity);
                db.AddInParameter(dbCmd, "@TOUR_HIGHLIGHT", DbType.Binary, objTourBDto.TourHighlight);
                db.AddInParameter(dbCmd, "@TOUR_HIGHLIGHT_CONTENT_TYPE", DbType.String, objTourBDto.TourHighlightContentType);
                db.AddInParameter(dbCmd, "@TOUR_HIGHLIGHT_FILE_NAME", DbType.String, objTourBDto.TourHighlightFileName);
                db.AddInParameter(dbCmd, "@IMPORTANT_NOTES", DbType.Binary, objTourBDto.ImportantNotes);
                db.AddInParameter(dbCmd, "@IMPORTANT_NOTES_CONTENT_TYPE", DbType.String, objTourBDto.ImportantNotesContentType);
                db.AddInParameter(dbCmd, "@IMPORTANT_NOTES_FILE_NAME", DbType.String, objTourBDto.ImportantNotesFileName);
                db.AddInParameter(dbCmd, "@TERMS_AND_CONDITIONS", DbType.Binary, objTourBDto.TermsAndConditions);
                db.AddInParameter(dbCmd, "@TERMS_AND_CONDITIONS_CONTENT_TYPE", DbType.String, objTourBDto.TermsAndConditionsContentType);
                db.AddInParameter(dbCmd, "@TERMS_AND_CONDITIONS_FILE_NAME", DbType.String, objTourBDto.TermsAndConditionsFileName);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS", DbType.Binary, objTourBDto.PaymentTerms);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS_CONTENT_TYPE", DbType.String, objTourBDto.PaymentTermsContentType);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS_FILE_NAME", DbType.String, objTourBDto.PaymentTermsFileName);
                db.AddInParameter(dbCmd, "@CANCELLATION_CHARGES", DbType.Binary, objTourBDto.CancellationCharges);
                db.AddInParameter(dbCmd, "@CANCELLATION_CHARGES_CONTENT_TYPE", DbType.String, objTourBDto.CancellationChargesContentType);
                db.AddInParameter(dbCmd, "@CANCELLATION_CHARGES_FILE_NAME", DbType.String, objTourBDto.CancellationChargesFileName);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY", DbType.Binary, objTourBDto.TourItenary);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY_CONTENT_TYPE", DbType.String, objTourBDto.TourItenaryContentType);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY_FILE_NAME", DbType.String, objTourBDto.TourItenaryFileName);
                db.AddInParameter(dbCmd, "@OPTIONAL_SITE_SEEING_COST", DbType.Binary, objTourBDto.OptionalSiteSeeingCost);
                db.AddInParameter(dbCmd, "@OPTIONAL_SITE_SEEING_COST_CONTENT_TYPE", DbType.String, objTourBDto.OptionalSiteSeeingCostContentType);
                db.AddInParameter(dbCmd, "@OPTIONAL_SITE_SEEING_COST_FILE_NAME", DbType.String, objTourBDto.OptionalSiteSeeingCostFileName);
                db.AddInParameter(dbCmd, "@USP", DbType.Binary, objTourBDto.Usp);
                db.AddInParameter(dbCmd, "@USP_CONTENT_TYPE", DbType.String, objTourBDto.UspContentType);
                db.AddInParameter(dbCmd, "@USP_FILE_NAME", DbType.String, objTourBDto.UspFileName);
                db.AddInParameter(dbCmd, "@LIMITATIONS", DbType.Binary, objTourBDto.Limitations);
                db.AddInParameter(dbCmd, "@LIMITATIONS_CONTENT_TYPE", DbType.String, objTourBDto.LimitationsContentType);
                db.AddInParameter(dbCmd, "@LIMITATIONS_FILE_NAME", DbType.String, objTourBDto.LimitationsFileName);
                db.AddInParameter(dbCmd, "@COMPETITORS_COMPARISON", DbType.Binary, objTourBDto.CompetitorsComparison);
                db.AddInParameter(dbCmd, "@COMPETITORS_COMPARISON_CONTENT_TYPE", DbType.String, objTourBDto.CompetitorsComparisonContentType);
                db.AddInParameter(dbCmd, "@COMPETITORS_COMPARISON_FILE_NAME", DbType.String, objTourBDto.CompetitorsComparisonFileName);
                db.AddInParameter(dbCmd, "@FINAL_ITERNARY", DbType.Binary, objTourBDto.FinalIternary);
                db.AddInParameter(dbCmd, "@FINAL_ITERNARY_CONTENT_TYPE", DbType.String, objTourBDto.FinalIternaryContentType);
                db.AddInParameter(dbCmd, "@FINAL_ITERNARY_FILE_NAME", DbType.String, objTourBDto.FinalIternaryFileName);
                db.AddInParameter(dbCmd, "@FINAL_TERMS_CONDITIONS", DbType.Binary, objTourBDto.FinalTermsConditions);
                db.AddInParameter(dbCmd, "@FINAL_TERMS_CONDITIONS_CONTENT_TYPE", DbType.String, objTourBDto.FinalTermsConditionsContentType);
                db.AddInParameter(dbCmd, "@FINAL_TERMS_CONDITIONS_FILE_NAME", DbType.String, objTourBDto.FinalTermsConditionsFileName);
                db.AddInParameter(dbCmd, "@FINAL_VOUCHURES", DbType.Binary, objTourBDto.FinalVouchures);
                db.AddInParameter(dbCmd, "@FINAL_VOUCHURES_CONTENT_TYPE", DbType.String, objTourBDto.FinalVouchuresContentType);
                db.AddInParameter(dbCmd, "@FINAL_VOUCHURES_FILE_NAME", DbType.String, objTourBDto.FinalVouchuresFileName);



                db.AddInParameter(dbCmd, "@DESTINATION_DETAILS", DbType.Binary, objTourBDto.DestinationDetails);
                db.AddInParameter(dbCmd, "@DESTINATION_DETAILS_CONTENT_TYPE", DbType.String, objTourBDto.DestinationDetailsContentType);
                db.AddInParameter(dbCmd, "@DESTINATION_DETAILS_FILE_NAME", DbType.String, objTourBDto.DestinationDetailsFileName);
                db.AddInParameter(dbCmd, "@GENERAL_GUIDE_LINES", DbType.Binary, objTourBDto.GeneralGuideLines);
                db.AddInParameter(dbCmd, "@GENERAL_GUIDE_LINES_CONTENT_TYPE", DbType.String, objTourBDto.GeneralGuideLinesContentType);
                db.AddInParameter(dbCmd, "@GENERAL_GUIDE_LINES_FILE_NAME", DbType.String, objTourBDto.GeneralGuideLinesFileName);
                db.AddInParameter(dbCmd, "@BROCHURE1", DbType.Binary, objTourBDto.Brochure1);
                db.AddInParameter(dbCmd, "@BROCHURE1_CONTENT_TYPE", DbType.String, objTourBDto.Brochure1ContentType);
                db.AddInParameter(dbCmd, "@BROCHURE1_FILE_NAME", DbType.String, objTourBDto.Brochure1FileName);
                db.AddInParameter(dbCmd, "@BROCHURE2", DbType.Binary, objTourBDto.Brochure2);
                db.AddInParameter(dbCmd, "@BROCHURE2_CONTENT_TYPE", DbType.String, objTourBDto.Brochure2ContentType);
                db.AddInParameter(dbCmd, "@BROCHURE2_FILE_NAME", DbType.String, objTourBDto.Brochure2FileName);
                db.AddInParameter(dbCmd, "@BROCHURE3", DbType.Binary, objTourBDto.Brochure3);
                db.AddInParameter(dbCmd, "@BROCHURE3_CONTENT_TYPE", DbType.String, objTourBDto.Brochure3ContentType);
                db.AddInParameter(dbCmd, "@BROCHURE3_FILE_NAME", DbType.String, objTourBDto.Brochure3FileName);




                db.AddInParameter(dbCmd, "@WEB_PHOTO", DbType.Binary, objTourBDto.WebPhoto);
                db.AddInParameter(dbCmd, "@WEB_PHOTO_CONTENT", DbType.String, objTourBDto.WebPhotoContent);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, objTourBDto.UserId);
                db.AddInParameter(dbCmd, "@TOUR_INTER_DOMEST", DbType.String, objTourBDto.TourInterDomestic);
				db.AddInParameter(dbCmd, "@BASE_TOUR_ID", DbType.Int32, objTourBDto.BaseTourId);

                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);

                ds = db.ExecuteDataSet(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
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

        #region Copy TourInfo
        /// <summary>
        /// Insert TourInfo.
        /// </summary>
        /// <param name="customer">TourBDto object that TourInfo data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int CopyTourInfo(TourBDto objTourBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_COPY_BY_TOUR);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.String, objTourBDto.TourId);
                db.AddInParameter(dbCmd, "@TOUR_CODE", DbType.String, objTourBDto.TourCode);
                db.AddInParameter(dbCmd, "@TOUR_SHORT_NAME", DbType.String, objTourBDto.TourSortName);
                db.AddInParameter(dbCmd, "@TOUR_LONG_DESC", DbType.String, objTourBDto.TourLongName);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY_TYPE_ID", DbType.Int32, objTourBDto.TourItenaryId);
                db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, objTourBDto.TourTypeId);
                db.AddInParameter(dbCmd, "@TOUR_FROM_DATE", DbType.DateTime, objTourBDto.TourFormDate);
                db.AddInParameter(dbCmd, "@TOUR_TO_DATE", DbType.DateTime, objTourBDto.TourToDate);
                db.AddInParameter(dbCmd, "@NO_OF_DAYS", DbType.Int32, objTourBDto.NoOfDays);

                //db.AddInParameter(dbCmd, "@TOUR_COST_ADULT", DbType.Int32, objTourBDto.TourCostAdult);
                //db.AddInParameter(dbCmd, "@TOUR_COST_CHILD_WITH_BED", DbType.Int32, objTourBDto.TourCostChildWithBed);
                //db.AddInParameter(dbCmd, "@TOUR_COST_CHILD_WITHOUT_BED", DbType.Int32, objTourBDto.TourCostChildWithoutBed);
                //db.AddInParameter(dbCmd, "@INFANT_PRICE", DbType.Int32, objTourBDto.InfantPrice);
                //db.AddInParameter(dbCmd, "@TOUR_CURRANCY", DbType.String, objTourBDto.TourCurrancy);


                db.AddInParameter(dbCmd, "@NO_OF_SEATS", DbType.Int32, objTourBDto.NoOfSeats);
                db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, objTourBDto.NoOfNights);
                db.AddInParameter(dbCmd, "@COUNTRIES_FOR_VISA", DbType.String, objTourBDto.CountriesForVisa);
                db.AddInParameter(dbCmd, "@CITIES_TO_TRAVEL", DbType.String, objTourBDto.CityToTravel);
                db.AddInParameter(dbCmd, "@START_END_CITY", DbType.String, objTourBDto.StartEndCity);
                db.AddInParameter(dbCmd, "@TOUR_HIGHLIGHT", DbType.Binary, objTourBDto.TourHighlight);
                db.AddInParameter(dbCmd, "@TOUR_HIGHLIGHT_CONTENT_TYPE", DbType.String, objTourBDto.TourHighlightContentType);
                db.AddInParameter(dbCmd, "@TOUR_HIGHLIGHT_FILE_NAME", DbType.String, objTourBDto.TourHighlightFileName);
                db.AddInParameter(dbCmd, "@IMPORTANT_NOTES", DbType.Binary, objTourBDto.ImportantNotes);
                db.AddInParameter(dbCmd, "@IMPORTANT_NOTES_CONTENT_TYPE", DbType.String, objTourBDto.ImportantNotesContentType);
                db.AddInParameter(dbCmd, "@IMPORTANT_NOTES_FILE_NAME", DbType.String, objTourBDto.ImportantNotesFileName);
                db.AddInParameter(dbCmd, "@TERMS_AND_CONDITIONS", DbType.Binary, objTourBDto.TermsAndConditions);
                db.AddInParameter(dbCmd, "@TERMS_AND_CONDITIONS_CONTENT_TYPE", DbType.String, objTourBDto.TermsAndConditionsContentType);
                db.AddInParameter(dbCmd, "@TERMS_AND_CONDITIONS_FILE_NAME", DbType.String, objTourBDto.TermsAndConditionsFileName);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS", DbType.Binary, objTourBDto.PaymentTerms);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS_CONTENT_TYPE", DbType.String, objTourBDto.PaymentTermsContentType);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS_FILE_NAME", DbType.String, objTourBDto.PaymentTermsFileName);
                db.AddInParameter(dbCmd, "@CANCELLATION_CHARGES", DbType.Binary, objTourBDto.CancellationCharges);
                db.AddInParameter(dbCmd, "@CANCELLATION_CHARGES_CONTENT_TYPE", DbType.String, objTourBDto.CancellationChargesContentType);
                db.AddInParameter(dbCmd, "@CANCELLATION_CHARGES_FILE_NAME", DbType.String, objTourBDto.CancellationChargesFileName);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY", DbType.Binary, objTourBDto.TourItenary);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY_CONTENT_TYPE", DbType.String, objTourBDto.TourItenaryContentType);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY_FILE_NAME", DbType.String, objTourBDto.TourItenaryFileName);
                db.AddInParameter(dbCmd, "@OPTIONAL_SITE_SEEING_COST", DbType.Binary, objTourBDto.OptionalSiteSeeingCost);
                db.AddInParameter(dbCmd, "@OPTIONAL_SITE_SEEING_COST_CONTENT_TYPE", DbType.String, objTourBDto.OptionalSiteSeeingCostContentType);
                db.AddInParameter(dbCmd, "@OPTIONAL_SITE_SEEING_COST_FILE_NAME", DbType.String, objTourBDto.OptionalSiteSeeingCostFileName);
                db.AddInParameter(dbCmd, "@USP", DbType.Binary, objTourBDto.Usp);
                db.AddInParameter(dbCmd, "@USP_CONTENT_TYPE", DbType.String, objTourBDto.UspContentType);
                db.AddInParameter(dbCmd, "@USP_FILE_NAME", DbType.String, objTourBDto.UspFileName);
                db.AddInParameter(dbCmd, "@LIMITATIONS", DbType.Binary, objTourBDto.Limitations);
                db.AddInParameter(dbCmd, "@LIMITATIONS_CONTENT_TYPE", DbType.String, objTourBDto.LimitationsContentType);
                db.AddInParameter(dbCmd, "@LIMITATIONS_FILE_NAME", DbType.String, objTourBDto.LimitationsFileName);
                db.AddInParameter(dbCmd, "@COMPETITORS_COMPARISON", DbType.Binary, objTourBDto.CompetitorsComparison);
                db.AddInParameter(dbCmd, "@COMPETITORS_COMPARISON_CONTENT_TYPE", DbType.String, objTourBDto.CompetitorsComparisonContentType);
                db.AddInParameter(dbCmd, "@COMPETITORS_COMPARISON_FILE_NAME", DbType.String, objTourBDto.CompetitorsComparisonFileName);
                db.AddInParameter(dbCmd, "@FINAL_ITERNARY", DbType.Binary, objTourBDto.FinalIternary);
                db.AddInParameter(dbCmd, "@FINAL_ITERNARY_CONTENT_TYPE", DbType.String, objTourBDto.FinalIternaryContentType);
                db.AddInParameter(dbCmd, "@FINAL_ITERNARY_FILE_NAME", DbType.String, objTourBDto.FinalIternaryFileName);
                db.AddInParameter(dbCmd, "@FINAL_TERMS_CONDITIONS", DbType.Binary, objTourBDto.FinalTermsConditions);
                db.AddInParameter(dbCmd, "@FINAL_TERMS_CONDITIONS_CONTENT_TYPE", DbType.String, objTourBDto.FinalTermsConditionsContentType);
                db.AddInParameter(dbCmd, "@FINAL_TERMS_CONDITIONS_FILE_NAME", DbType.String, objTourBDto.FinalTermsConditionsFileName);
                db.AddInParameter(dbCmd, "@FINAL_VOUCHURES", DbType.Binary, objTourBDto.FinalVouchures);
                db.AddInParameter(dbCmd, "@FINAL_VOUCHURES_CONTENT_TYPE", DbType.String, objTourBDto.FinalVouchuresContentType);
                db.AddInParameter(dbCmd, "@FINAL_VOUCHURES_FILE_NAME", DbType.String, objTourBDto.FinalVouchuresFileName);

                db.AddInParameter(dbCmd, "@DESTINATION_DETAILS", DbType.Binary, objTourBDto.DestinationDetails);
                db.AddInParameter(dbCmd, "@DESTINATION_DETAILS_CONTENT_TYPE", DbType.String, objTourBDto.DestinationDetailsContentType);
                db.AddInParameter(dbCmd, "@DESTINATION_DETAILS_FILE_NAME", DbType.String, objTourBDto.DestinationDetailsFileName);
                db.AddInParameter(dbCmd, "@GENERAL_GUIDE_LINES", DbType.Binary, objTourBDto.GeneralGuideLines);
                db.AddInParameter(dbCmd, "@GENERAL_GUIDE_LINES_CONTENT_TYPE", DbType.String, objTourBDto.GeneralGuideLinesContentType);
                db.AddInParameter(dbCmd, "@GENERAL_GUIDE_LINES_FILE_NAME", DbType.String, objTourBDto.GeneralGuideLinesFileName);
                db.AddInParameter(dbCmd, "@BROCHURE1", DbType.Binary, objTourBDto.Brochure1);
                db.AddInParameter(dbCmd, "@BROCHURE1_CONTENT_TYPE", DbType.String, objTourBDto.Brochure1ContentType);
                db.AddInParameter(dbCmd, "@BROCHURE1_FILE_NAME", DbType.String, objTourBDto.Brochure1FileName);
                db.AddInParameter(dbCmd, "@BROCHURE2", DbType.Binary, objTourBDto.Brochure2);
                db.AddInParameter(dbCmd, "@BROCHURE2_CONTENT_TYPE", DbType.String, objTourBDto.Brochure2ContentType);
                db.AddInParameter(dbCmd, "@BROCHURE2_FILE_NAME", DbType.String, objTourBDto.Brochure2FileName);
                db.AddInParameter(dbCmd, "@BROCHURE3", DbType.Binary, objTourBDto.Brochure3);
                db.AddInParameter(dbCmd, "@BROCHURE3_CONTENT_TYPE", DbType.String, objTourBDto.Brochure3ContentType);
                db.AddInParameter(dbCmd, "@BROCHURE3_FILE_NAME", DbType.String, objTourBDto.Brochure3FileName);

                db.AddInParameter(dbCmd, "@WEB_PHOTO", DbType.Binary, objTourBDto.WebPhoto);
                db.AddInParameter(dbCmd, "@WEB_PHOTO_CONTENT", DbType.String, objTourBDto.WebPhotoContent);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, objTourBDto.UserId);
                db.AddInParameter(dbCmd, "@TOUR_INTER_DOMEST", DbType.String, objTourBDto.TourInterDomestic);
				db.AddInParameter(dbCmd, "@BASE_TOUR_ID", DbType.Int32, objTourBDto.BaseTourId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);

                ds = db.ExecuteDataSet(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
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

        #region Browse Info

        public DataTable BrowseInfo(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_SELECT_BROWSE);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
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

        #region Update TourInfo

        public int UpdateTourInfo(TourBDto objTourBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_UPDATE);


                db.AddInParameter(dbCmd, "@TOUR_CODE", DbType.String, objTourBDto.TourCode);
                db.AddInParameter(dbCmd, "@TOUR_SHORT_NAME", DbType.String, objTourBDto.TourSortName);
                db.AddInParameter(dbCmd, "@TOUR_LONG_DESC", DbType.String, objTourBDto.TourLongName);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY_TYPE_ID", DbType.Int32, objTourBDto.TourItenaryId);
                db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, objTourBDto.TourTypeId);
                db.AddInParameter(dbCmd, "@TOUR_FROM_DATE", DbType.DateTime, objTourBDto.TourFormDate);
                db.AddInParameter(dbCmd, "@TOUR_TO_DATE", DbType.DateTime, objTourBDto.TourToDate);
                db.AddInParameter(dbCmd, "@NO_OF_DAYS", DbType.Int32, objTourBDto.NoOfDays);
                db.AddInParameter(dbCmd, "@NO_OF_SEATS", DbType.Int32, objTourBDto.NoOfSeats);
                db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, objTourBDto.NoOfNights);

                //db.AddInParameter(dbCmd, "@TOUR_COST_ADULT", DbType.Int32, objTourBDto.TourCostAdult);
                //db.AddInParameter(dbCmd, "@TOUR_COST_CHILD_WITH_BED", DbType.Int32, objTourBDto.TourCostChildWithBed);
                //db.AddInParameter(dbCmd, "@TOUR_COST_CHILD_WITHOUT_BED", DbType.Int32, objTourBDto.TourCostChildWithoutBed);
                //db.AddInParameter(dbCmd, "@INFANT_PRICE", DbType.Int32, objTourBDto.InfantPrice);
                //db.AddInParameter(dbCmd, "@TOUR_CURRANCY", DbType.String, objTourBDto.TourCurrancy);


                db.AddInParameter(dbCmd, "@COUNTRIES_FOR_VISA", DbType.String, objTourBDto.CountriesForVisa);
                db.AddInParameter(dbCmd, "@CITIES_TO_TRAVEL", DbType.String, objTourBDto.CityToTravel);
                db.AddInParameter(dbCmd, "@START_END_CITY", DbType.String, objTourBDto.StartEndCity);
                db.AddInParameter(dbCmd, "@TOUR_HIGHLIGHT", DbType.Binary, objTourBDto.TourHighlight);
                db.AddInParameter(dbCmd, "@TOUR_HIGHLIGHT_CONTENT_TYPE", DbType.String, objTourBDto.TourHighlightContentType);
                db.AddInParameter(dbCmd, "@TOUR_HIGHLIGHT_FILE_NAME", DbType.String, objTourBDto.TourHighlightFileName);
                db.AddInParameter(dbCmd, "@IMPORTANT_NOTES", DbType.Binary, objTourBDto.ImportantNotes);
                db.AddInParameter(dbCmd, "@IMPORTANT_NOTES_CONTENT_TYPE", DbType.String, objTourBDto.ImportantNotesContentType);
                db.AddInParameter(dbCmd, "@IMPORTANT_NOTES_FILE_NAME", DbType.String, objTourBDto.ImportantNotesFileName);
                db.AddInParameter(dbCmd, "@TERMS_AND_CONDITIONS", DbType.Binary, objTourBDto.TermsAndConditions);
                db.AddInParameter(dbCmd, "@TERMS_AND_CONDITIONS_CONTENT_TYPE", DbType.String, objTourBDto.TermsAndConditionsContentType);
                db.AddInParameter(dbCmd, "@TERMS_AND_CONDITIONS_FILE_NAME", DbType.String, objTourBDto.TermsAndConditionsFileName);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS", DbType.Binary, objTourBDto.PaymentTerms);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS_CONTENT_TYPE", DbType.String, objTourBDto.PaymentTermsContentType);
                db.AddInParameter(dbCmd, "@PAYMENT_TERMS_FILE_NAME", DbType.String, objTourBDto.PaymentTermsFileName);
                db.AddInParameter(dbCmd, "@CANCELLATION_CHARGES", DbType.Binary, objTourBDto.CancellationCharges);
                db.AddInParameter(dbCmd, "@CANCELLATION_CHARGES_CONTENT_TYPE", DbType.String, objTourBDto.CancellationChargesContentType);
                db.AddInParameter(dbCmd, "@CANCELLATION_CHARGES_FILE_NAME", DbType.String, objTourBDto.CancellationChargesFileName);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY", DbType.Binary, objTourBDto.TourItenary);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY_CONTENT_TYPE", DbType.String, objTourBDto.TourItenaryContentType);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY_FILE_NAME", DbType.String, objTourBDto.TourItenaryFileName);
                db.AddInParameter(dbCmd, "@OPTIONAL_SITE_SEEING_COST", DbType.Binary, objTourBDto.OptionalSiteSeeingCost);
                db.AddInParameter(dbCmd, "@OPTIONAL_SITE_SEEING_COST_CONTENT_TYPE", DbType.String, objTourBDto.OptionalSiteSeeingCostContentType);
                db.AddInParameter(dbCmd, "@OPTIONAL_SITE_SEEING_COST_FILE_NAME", DbType.String, objTourBDto.OptionalSiteSeeingCostFileName);
                db.AddInParameter(dbCmd, "@USP", DbType.Binary, objTourBDto.Usp);
                db.AddInParameter(dbCmd, "@USP_CONTENT_TYPE", DbType.String, objTourBDto.UspContentType);
                db.AddInParameter(dbCmd, "@USP_FILE_NAME", DbType.String, objTourBDto.UspFileName);
                db.AddInParameter(dbCmd, "@LIMITATIONS", DbType.Binary, objTourBDto.Limitations);
                db.AddInParameter(dbCmd, "@LIMITATIONS_CONTENT_TYPE", DbType.String, objTourBDto.LimitationsContentType);
                db.AddInParameter(dbCmd, "@LIMITATIONS_FILE_NAME", DbType.String, objTourBDto.LimitationsFileName);
                db.AddInParameter(dbCmd, "@COMPETITORS_COMPARISON", DbType.Binary, objTourBDto.CompetitorsComparison);
                db.AddInParameter(dbCmd, "@COMPETITORS_COMPARISON_CONTENT_TYPE", DbType.String, objTourBDto.CompetitorsComparisonContentType);
                db.AddInParameter(dbCmd, "@COMPETITORS_COMPARISON_FILE_NAME", DbType.String, objTourBDto.CompetitorsComparisonFileName);
                db.AddInParameter(dbCmd, "@FINAL_ITERNARY", DbType.Binary, objTourBDto.FinalIternary);
                db.AddInParameter(dbCmd, "@FINAL_ITERNARY_CONTENT_TYPE", DbType.String, objTourBDto.FinalIternaryContentType);
                db.AddInParameter(dbCmd, "@FINAL_ITERNARY_FILE_NAME", DbType.String, objTourBDto.FinalIternaryFileName);
                db.AddInParameter(dbCmd, "@FINAL_TERMS_CONDITIONS", DbType.Binary, objTourBDto.FinalTermsConditions);
                db.AddInParameter(dbCmd, "@FINAL_TERMS_CONDITIONS_CONTENT_TYPE", DbType.String, objTourBDto.FinalTermsConditionsContentType);
                db.AddInParameter(dbCmd, "@FINAL_TERMS_CONDITIONS_FILE_NAME", DbType.String, objTourBDto.FinalTermsConditionsFileName);
                db.AddInParameter(dbCmd, "@FINAL_VOUCHURES", DbType.Binary, objTourBDto.FinalVouchures);
                db.AddInParameter(dbCmd, "@FINAL_VOUCHURES_CONTENT_TYPE", DbType.String, objTourBDto.FinalVouchuresContentType);
                db.AddInParameter(dbCmd, "@FINAL_VOUCHURES_FILE_NAME", DbType.String, objTourBDto.FinalVouchuresFileName);

                db.AddInParameter(dbCmd, "@DESTINATION_DETAILS", DbType.Binary, objTourBDto.DestinationDetails);
                db.AddInParameter(dbCmd, "@DESTINATION_DETAILS_CONTENT_TYPE", DbType.String, objTourBDto.DestinationDetailsContentType);
                db.AddInParameter(dbCmd, "@DESTINATION_DETAILS_FILE_NAME", DbType.String, objTourBDto.DestinationDetailsFileName);
                db.AddInParameter(dbCmd, "@GENERAL_GUIDE_LINES", DbType.Binary, objTourBDto.GeneralGuideLines);
                db.AddInParameter(dbCmd, "@GENERAL_GUIDE_LINES_CONTENT_TYPE", DbType.String, objTourBDto.GeneralGuideLinesContentType);
                db.AddInParameter(dbCmd, "@GENERAL_GUIDE_LINES_FILE_NAME", DbType.String, objTourBDto.GeneralGuideLinesFileName);
                db.AddInParameter(dbCmd, "@BROCHURE1", DbType.Binary, objTourBDto.Brochure1);
                db.AddInParameter(dbCmd, "@BROCHURE1_CONTENT_TYPE", DbType.String, objTourBDto.Brochure1ContentType);
                db.AddInParameter(dbCmd, "@BROCHURE1_FILE_NAME", DbType.String, objTourBDto.Brochure1FileName);
                db.AddInParameter(dbCmd, "@BROCHURE2", DbType.Binary, objTourBDto.Brochure2);
                db.AddInParameter(dbCmd, "@BROCHURE2_CONTENT_TYPE", DbType.String, objTourBDto.Brochure2ContentType);
                db.AddInParameter(dbCmd, "@BROCHURE2_FILE_NAME", DbType.String, objTourBDto.Brochure2FileName);
                db.AddInParameter(dbCmd, "@BROCHURE3", DbType.Binary, objTourBDto.Brochure3);
                db.AddInParameter(dbCmd, "@BROCHURE3_CONTENT_TYPE", DbType.String, objTourBDto.Brochure3ContentType);
                db.AddInParameter(dbCmd, "@BROCHURE3_FILE_NAME", DbType.String, objTourBDto.Brochure3FileName);

                db.AddInParameter(dbCmd, "@WEB_PHOTO", DbType.Binary, objTourBDto.WebPhoto);
                db.AddInParameter(dbCmd, "@WEB_PHOTO_CONTENT", DbType.String, objTourBDto.WebPhotoContent);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, objTourBDto.UserId);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, objTourBDto.TourId);
                db.AddInParameter(dbCmd, "@TOUR_INTER_DOMEST", DbType.String, objTourBDto.TourInterDomestic);
				db.AddInParameter(dbCmd, "@BASE_TOUR_ID", DbType.Int32, objTourBDto.BaseTourId);
                db.AddOutParameter(dbCmd, "@IS_UPDATE", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_UPDATE"));

                if (db.GetParameterValue(dbCmd, "@IS_UPDATE") != DBNull.Value)
                    Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_UPDATE"));
                if (Result == 1)
                    return 1; // SUCCESSFUL INSERTION RETURN TRUE
                else
                    return 0; // UNSUCCESSFUL INSERTION RETUN FALSE ( ALREADY EXISTS )

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

        #region GetHotelNameByCountryCity

        public DataTable GetHotelNameByCountryCity(int countryId, int cityId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_HOTELNAME_BY_COUNTRYCITY_SELECT);
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, countryId);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, cityId);
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

        #region HotelOfCity

        public DataTable HotelOfCity(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_TRAVELLING_CITY_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
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

        #region delete Delete Hotel Details
        public int DeleteHotel(int SrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_HOTEL_DETAILS_DELETE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, SrNo);
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

        #region GetTourCountryCityByTourId
        public DataSet GetTourCountryCityByTourId(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_COUNTRYCITY_BY_TOURID_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
                ds = db.ExecuteDataSet(dbCmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds;
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
            return ds;
        }


        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            GC.Collect();
        }
        #endregion

        #region Insert Update TourHotelInfo
        /// <summary>
        /// Insert TourInfo.
        /// </summary>
        /// <param name="customer">TourBDto object that TourInfo data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int InsertUpdateTourHotelInfo(TourHotelBDto objTourHotelBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_HOTEL_INSERT_UPDATE);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, objTourHotelBDto.tourId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, objTourHotelBDto.SrNo);
                if (objTourHotelBDto.FromDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, objTourHotelBDto.FromDate);
                else
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);


                if (objTourHotelBDto.ToDate != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, objTourHotelBDto.ToDate);
                else
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, objTourHotelBDto.CountryId);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, objTourHotelBDto.CityId);
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, objTourHotelBDto.HotelId);
                db.AddInParameter(dbCmd, "@NO_OF_ROOMS", DbType.Int32, objTourHotelBDto.NoOfRooms);

                if(objTourHotelBDto.Amount !=0)
                db.AddInParameter(dbCmd, "@AMOUNT", DbType.Int32, objTourHotelBDto.Amount);
                else
                    db.AddInParameter(dbCmd, "@AMOUNT", DbType.Int32,DBNull.Value);


                db.AddInParameter(dbCmd, "@CURRENCY", DbType.Int32, objTourHotelBDto.Currency);
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, objTourHotelBDto.RoomTypeId);
                

                if (objTourHotelBDto.Amount != 0)
                    db.AddInParameter(dbCmd, "@TAX", DbType.Int32, objTourHotelBDto.Tax);
                else
                    db.AddInParameter(dbCmd, "@TAX", DbType.Int32, DBNull.Value);

                if (objTourHotelBDto.Amount != 0)
                    db.AddInParameter(dbCmd, "@GST", DbType.Int32, objTourHotelBDto.Gst);
                else
                    db.AddInParameter(dbCmd, "@GST", DbType.Int32, DBNull.Value);

                if (objTourHotelBDto.Amount != 0)
                    db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Int32, objTourHotelBDto.TotalAmount);
                else
                    db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Int32, DBNull.Value);

                if (objTourHotelBDto.Amount != 0)
                    db.AddInParameter(dbCmd, "@FINAL_AMOUNT", DbType.Int32, objTourHotelBDto.FinalAmount);
                else
                    db.AddInParameter(dbCmd, "@FINAL_AMOUNT", DbType.Int32, DBNull.Value);
                db.AddInParameter(dbCmd, "@REMARKS", DbType.String, objTourHotelBDto.Remarks);




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

        #region Insert TourTravelDetail
        /// <summary>
        /// Insert TourInfo.
        /// </summary>
        /// <param name="customer">TourBDto object that TourInfo data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int InsertTourTravelDetail(TourTravelBDto objTourTravelBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_TRAVEL_DETAIL_INSERT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, objTourTravelBDto.tourId);
                db.AddInParameter(dbCmd, "@TRANSPORT_MODE_ID", DbType.Int32, objTourTravelBDto.TransportModeId);
                db.AddInParameter(dbCmd, "@TRANSPORT_ID", DbType.Int32, objTourTravelBDto.TransportId);
                db.AddInParameter(dbCmd, "@TRANSPORT_NO", DbType.String, objTourTravelBDto.FlightNo);
                db.AddInParameter(dbCmd, "@TRANSPORT_DETAILS", DbType.String, objTourTravelBDto.TransportDetail);
                db.AddInParameter(dbCmd, "@DATE_OF_ARRIVAL", DbType.DateTime, objTourTravelBDto.DateOfArrival);
                db.AddInParameter(dbCmd, "@TIME_OF_ARRIVAL", DbType.DateTime, objTourTravelBDto.TimeOfArrival);
                db.AddInParameter(dbCmd, "@PLACE_OF_ARRIVAL", DbType.Int32, objTourTravelBDto.PlaceOfArrivalDestination);
                db.AddInParameter(dbCmd, "@DATE_OF_DEPARTURE", DbType.DateTime, objTourTravelBDto.DateOfDeparture);
                db.AddInParameter(dbCmd, "@TIME_OF_DEPARTURE", DbType.DateTime, objTourTravelBDto.TimeOfDeparture);
                db.AddInParameter(dbCmd, "@PLACE_OF_DEPARTURE", DbType.Int32, objTourTravelBDto.PlaceOfDepartureSource);
                db.AddInParameter(dbCmd, "@NO_OF_SEATS", DbType.Int32, objTourTravelBDto.NoOfSeats);
                db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, objTourTravelBDto.Amount);
				db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, objTourTravelBDto.Branch);
				db.AddInParameter(dbCmd, "@BOOKING_REQUEST_TO", DbType.Int32, objTourTravelBDto.BookingRequestTo);

                db.AddOutParameter(dbCmd, "@ISINSERT", DbType.Int32, 1);
                ds = db.ExecuteDataSet(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ISINSERT"));
                return Result;

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

        #region Update TourTravelDetail
        /// <summary>
        /// Insert TourInfo.
        /// </summary>
        /// <param name="customer">TourBDto object that TourInfo data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int UpdateTourTravelDetail(TourTravelBDto objTourTravelBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_TRAVEL_DETAIL_UPDATE);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, objTourTravelBDto.tourId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, objTourTravelBDto.SrNo);
                db.AddInParameter(dbCmd, "@TRANSPORT_MODE_ID", DbType.Int32, objTourTravelBDto.TransportModeId);
                db.AddInParameter(dbCmd, "@TRANSPORT_ID", DbType.Int32, objTourTravelBDto.TransportId);
                db.AddInParameter(dbCmd, "@TRANSPORT_NO", DbType.String, objTourTravelBDto.FlightNo);
                db.AddInParameter(dbCmd, "@TRANSPORT_DETAILS", DbType.String, objTourTravelBDto.TransportDetail);
                db.AddInParameter(dbCmd, "@DATE_OF_ARRIVAL", DbType.DateTime, objTourTravelBDto.DateOfArrival);
                db.AddInParameter(dbCmd, "@TIME_OF_ARRIVAL", DbType.DateTime, objTourTravelBDto.TimeOfArrival);
                db.AddInParameter(dbCmd, "@PLACE_OF_ARRIVAL", DbType.Int32, objTourTravelBDto.PlaceOfArrivalDestination);
                db.AddInParameter(dbCmd, "@DATE_OF_DEPARTURE", DbType.DateTime, objTourTravelBDto.DateOfDeparture);
                db.AddInParameter(dbCmd, "@TIME_OF_DEPARTURE", DbType.DateTime, objTourTravelBDto.TimeOfDeparture);
                db.AddInParameter(dbCmd, "@PLACE_OF_DEPARTURE", DbType.Int32, objTourTravelBDto.PlaceOfDepartureSource);
                db.AddInParameter(dbCmd, "@NO_OF_SEATS", DbType.Int32, objTourTravelBDto.NoOfSeats);
                db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, objTourTravelBDto.Amount);
				db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, objTourTravelBDto.Branch);
				db.AddInParameter(dbCmd, "@BOOKING_REQUEST_TO", DbType.Int32, objTourTravelBDto.BookingRequestTo);



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

        #region delete TourTravelDetail
        /// <summary>
        /// Insert TourInfo. 
        /// </summary>
        /// <param name="customer">TourBDto object that TourInfo data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int DeleteTourTravelDetail(int tourId, int SrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_TRAVEL_DETAIL_DELETE);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, SrNo);
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

        #region Insert TourTravelPriceDetail
        /// <summary>
        /// Insert TourInfo.
        /// </summary>
        /// <param name="customer">TourBDto object that TourInfo data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int InsertTourTravelPriceDetail(TourTravelBDto objTourTravelBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_TRAVEL_CURRENCY_PRICE_INSERT);


                db.AddInParameter(dbCmd, "@TRANSPORT_MODE", DbType.Int32, objTourTravelBDto.TransportModeId);
                db.AddInParameter(dbCmd, "@TRANSPORTATTION_ID", DbType.Int32, objTourTravelBDto.transporationId);
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.Int32, objTourTravelBDto.Currency);
                db.AddInParameter(dbCmd, "@SEATS_FOR", DbType.Int32, objTourTravelBDto.SeatsFor);
                db.AddInParameter(dbCmd, "@NO_OF_SEATS", DbType.Int32, objTourTravelBDto.NoOfSeats);                
                db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, objTourTravelBDto.Amount);
                db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, objTourTravelBDto.Tax);
                db.AddInParameter(dbCmd, "@GST", DbType.Decimal, objTourTravelBDto.Gst);
                db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, objTourTravelBDto.TotalAmount);
                db.AddInParameter(dbCmd, "@FINAL_AMOUNT", DbType.Decimal, objTourTravelBDto.FinalAmount);
                db.AddInParameter(dbCmd, "@REMARKS", DbType.String, objTourTravelBDto.Remarks);
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

        #region Update TourTravelPriceDetail
        /// <summary>
        /// Insert TourInfo.
        /// </summary>
        /// <param name="customer">TourBDto object that TourInfo data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int UpdateTourTravelPriceDetail(TourTravelBDto objTourTravelBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_TRAVEL_CURRENCY_PRICE_UPDATE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, objTourTravelBDto.SrNoPrice);
                db.AddInParameter(dbCmd, "@TRANSPORT_MODE", DbType.Int32, objTourTravelBDto.TransportModeId);
                db.AddInParameter(dbCmd, "@TRANSPORTATTION_ID", DbType.Int32, objTourTravelBDto.transporationId);
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.Int32, objTourTravelBDto.Currency);
                db.AddInParameter(dbCmd, "@SEATS_FOR", DbType.Int32, objTourTravelBDto.SeatsFor);
                db.AddInParameter(dbCmd, "@NO_OF_SEATS", DbType.Int32, objTourTravelBDto.NoOfSeats);
                db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, objTourTravelBDto.Amount);
                db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, objTourTravelBDto.Tax);
                db.AddInParameter(dbCmd, "@GST", DbType.Decimal, objTourTravelBDto.Gst);
                db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal, objTourTravelBDto.TotalAmount);
                db.AddInParameter(dbCmd, "@FINAL_AMOUNT", DbType.Decimal, objTourTravelBDto.FinalAmount);
                db.AddInParameter(dbCmd, "@REMARKS", DbType.String, objTourTravelBDto.Remarks);

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

        #region GetTravelCurrencyPriceDetails

        public DataTable GetTravelCurrencyPriceDetails(int transportModeId,int SrNoTransportaion)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_TRAVEL_CURRENCY_PRICE_SELECT);
                db.AddInParameter(dbCmd, "@TRANSPORT_MODE", DbType.Int32, transportModeId);                
                db.AddInParameter(dbCmd, "@TRANSPORTATTION_ID", DbType.Int32, SrNoTransportaion);

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

        #region GetFlightPriceDetailsByID

        public DataTable GetFlightPriceDetailsByID(string transportMode,int Id, int NoOfSeats, int SeatsFor, int CurrencyId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_FLIGHT_CURRENCY_PRICE_SELECT_BYID);

                db.AddInParameter(dbCmd, "@TRANSPORT_MODE", DbType.String, transportMode);
                db.AddInParameter(dbCmd, "@ID", DbType.Int32, Id);
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.Int32, CurrencyId);                
                db.AddInParameter(dbCmd, "@NO_OF_SEATS", DbType.Int32, NoOfSeats);
                db.AddInParameter(dbCmd, "@SEATS_FOR", DbType.Int32, SeatsFor);
                
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

        #region GetFlightTravelCurrency

        public DataTable GetFlightTravelCurrency(string transportMode, int flightID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_FLIGHT_CURRENCY_SELECT);
                db.AddInParameter(dbCmd, "@FLIGHT_ID", DbType.Int32, flightID);
                db.AddInParameter(dbCmd, "@TRANSPORT_MODE", DbType.String, transportMode);
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
        
        #region GetFlightCurrencyByID

        public DataTable GetFlightCurrencyByID(int FlightId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_FLIGHT_CURRENCY_SELECT_BYID);
                db.AddInParameter(dbCmd, "@FLIGHT_ID", DbType.Int32, FlightId);
             
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

        #region GetHotelDetailsByTourId

        public DataTable GetHotelDetailsByTourId(int tourId, int countryId, int cityId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_HOTEL_DETAILS_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, countryId);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, cityId);

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

        #region GetHotelRemarksBySrNo

        public DataTable GetHotelRemarksBySrNo(int SrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_HOTEL_REMARKS_SELECT);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, SrNo);
             
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

        #region GetTravelPriceRemarksBySrNo

        public DataTable GetTravelPriceRemarksBySrNo(int SrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_TRAVEL_CURRENCY_PRICE_REMARKS_SELECT);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, SrNo);

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

        #region GetTravelDetailsByTourId

        public DataTable GetTravelDetailsByTourId(int tourId,int srNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_TRAVEL_DETAIL_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
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


		#region GetTourName

		public DataTable GetTourName()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

				dbCmd = db.GetStoredProcCommand("USP_FARE_TOUR_NAME_GET");
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


		

        #region GetFlightDetailsByAirlineId

        public DataSet GetFlightDetailsAirlineId(int AirlineId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TRAVEL_FLIGHT_KEYVALUE);
                db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, AirlineId);
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

        public DataTable GetFlightsById(int flightId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_FLIGHT_MASTER_SELECT_BY_ID);
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
        public DataTable GetBrowseDoc(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_BROWSE_DATA_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
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
        public DataTable GetRoomTypeByHotelId(int HotelId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_ROOMTYPE_BY_HOTELID_SELECT);
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
        public DataTable GetCurrencyByHotelIdRoomId(int HotelId, int RoomTypeId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_CURRENCY_HOTEL_ROOMID_SELECT);

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
        public DataTable GetAmountByHotelRoomCurrencyId(int HotelId, int RoomTypeId, int CurrencyId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_AMOUNT_ROOM_CURRENCYID_SELECT);

                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, HotelId);
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, RoomTypeId);
                db.AddInParameter(dbCmd, "@CURRANCY", DbType.Int32, CurrencyId);

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

        public DataTable GetATGByHotelRoomCurrencyId(int HotelId, int RoomTypeId, int CurrencyId,int NoOfRooms)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_HOTEL_CURRENCY_PRICE_SELECT_BY_ID);

                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, HotelId);
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, RoomTypeId);
                db.AddInParameter(dbCmd, "@HOTEL_CURRENCY", DbType.Int32, CurrencyId);
                db.AddInParameter(dbCmd, "@NO_OF_ROOMS", DbType.Int32, NoOfRooms);             
                
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

        

        public int InsertFareTourCurrencyPrice(TourBDto objTourBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_CURRENCY_PRICE_INSERT);

                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, objTourBDto.TourId);
                //if (objTourBDto.TourCostAdult != 0)
                    db.AddInParameter(dbCmd, "@TOUR_COST_ADULT", DbType.Int32, objTourBDto.TourCostAdult);
                //else
                    //db.AddInParameter(dbCmd, "@TOUR_COST_ADULT", DbType.Int32, DBNull.Value);
              //  if (objTourBDto.TourCostChildWithBed != 0)
                    db.AddInParameter(dbCmd, "@TOUR_COST_CHILD_WITH_BED", DbType.Int32, objTourBDto.TourCostChildWithBed);
               // else
                   // db.AddInParameter(dbCmd, "@TOUR_COST_CHILD_WITH_BED", DbType.Int32, DBNull.Value);
               // if (objTourBDto.TourCostChildWithoutBed != 0)
                    db.AddInParameter(dbCmd, "@TOUR_COST_CHILD_WITHOUT_BED", DbType.Int32, objTourBDto.TourCostChildWithoutBed);
               // else
                //    db.AddInParameter(dbCmd, "@TOUR_COST_CHILD_WITHOUT_BED", DbType.Int32, DBNull.Value);
               // if (objTourBDto.InfantPrice != 0)
                    db.AddInParameter(dbCmd, "@INFANT_PRICE", DbType.Int32, objTourBDto.InfantPrice);
               // else
                   // db.AddInParameter(dbCmd, "@INFANT_PRICE", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@TOUR_CURRANCY", DbType.String, objTourBDto.TourCurrancy);
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
        public int UpdateFareTourCurrencyPrice(TourBDto objTourBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_CURRENCY_PRICE_UPDATE);

                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, objTourBDto.TourId);
                db.AddInParameter(dbCmd, "@CURRENCY_PRICE_ID", DbType.Int32, objTourBDto.CurrencyPriceId);
              //  if (objTourBDto.TourCostAdult != 0)
                    db.AddInParameter(dbCmd, "@TOUR_COST_ADULT", DbType.Int32, objTourBDto.TourCostAdult);
              //  else
                 //   db.AddInParameter(dbCmd, "@TOUR_COST_ADULT", DbType.Int32, DBNull.Value);
              //  if (objTourBDto.TourCostChildWithBed != 0)
                    db.AddInParameter(dbCmd, "@TOUR_COST_CHILD_WITH_BED", DbType.Int32, objTourBDto.TourCostChildWithBed);
              //  else
                //    db.AddInParameter(dbCmd, "@TOUR_COST_CHILD_WITH_BED", DbType.Int32, DBNull.Value);
               // if (objTourBDto.TourCostChildWithoutBed != 0)
                    db.AddInParameter(dbCmd, "@TOUR_COST_CHILD_WITHOUT_BED", DbType.Int32, objTourBDto.TourCostChildWithoutBed);
               // else
                //    db.AddInParameter(dbCmd, "@TOUR_COST_CHILD_WITHOUT_BED", DbType.Int32, DBNull.Value);
               // if (objTourBDto.InfantPrice != 0)
                    db.AddInParameter(dbCmd, "@INFANT_PRICE", DbType.Int32, objTourBDto.InfantPrice);
               // else
                   // db.AddInParameter(dbCmd, "@INFANT_PRICE", DbType.Int32, DBNull.Value);

                db.AddInParameter(dbCmd, "@TOUR_CURRANCY", DbType.String, objTourBDto.TourCurrancy);
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


        public DataTable GetTourCurrecnyPrice(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_CURRENCY_PRICE_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
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

        public DataTable GetBusCodeByBusName(string busName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_BUS_NO_SELECT_BY_BUSNAME);
                db.AddInParameter(dbCmd, "@BUS_NAME", DbType.String, busName);
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

        public DataTable GetCruiseCodeByBusName(string cruiseName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_CRUISE_NO_SELECT_BY_CRUISENAME);
                db.AddInParameter(dbCmd, "@CRUISE_NAME", DbType.String, cruiseName);
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

        

        #region Insert InsertItenaryType
        /// <summary>
        /// Insert TourInfo.
        /// </summary>
        /// <param name="customer">TourBDto object that TourInfo data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int InsertItenaryType(string itenaryName, int userId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_ITENARY_TYPE_MASTER_INSERT);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY_TYPE_NAME", DbType.String, itenaryName);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY_TYPE_DESC", DbType.String, itenaryName);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, userId);
                db.AddOutParameter(dbCmd, "@ISEXIST", DbType.Int32, 1);

                ds = db.ExecuteDataSet(dbCmd);
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

        #region WEB TAB

        #region Get WebHightlights

        public string GetWebHightlights(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            string s = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_HIGHLIGHT_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
                s = db.ExecuteScalar(dbCmd).ToString();
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
            return s;
        }
        #endregion

        #region Get WebCost

        public string GetWebCost(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            string s = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_TOUR_COST_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
                s = db.ExecuteScalar(dbCmd).ToString();
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
            return s;

        }
        #endregion

        #region Get Itenary

        public string GetWebItenary(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            string s = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_ITENARY_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
                s = db.ExecuteScalar(dbCmd).ToString();
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
            return s;
        }
        #endregion

        #region Get Web ImportantNotes

        public string GetWebImportantNotes(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            string s = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_IMPORTANT_NOTES_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
                s = db.ExecuteScalar(dbCmd).ToString();
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
            return s;
        }
        #endregion

        #region Get Web Terms

        public string GetWebTerms(int tourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            string s = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_TERMS_SELECT);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
                s = db.ExecuteScalar(dbCmd).ToString();
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
            return s;
        }
        #endregion

        #region Update Web hightlights
        /// <summary>
        /// Insert TourInfo.
        /// </summary>
        /// <param name="customer">TourBDto object that TourInfo data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int UpdateWebHightlights(TourWebBDto objTourWebBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_HIGHLIGHT_UPDATE);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, objTourWebBDto.tourId);
                db.AddInParameter(dbCmd, "@WEB_HIGHLIGHT", DbType.String, objTourWebBDto.Hightlights);

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

        #region Update WebCost
        /// <summary>
        /// Insert TourInfo.
        /// </summary>
        /// <param name="customer">TourBDto object that TourInfo data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int UpdateWebCost(TourWebBDto objTourWebBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_TOUR_COST_UPDATE);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, objTourWebBDto.tourId);

                db.AddInParameter(dbCmd, "@WEB_TOUR_COST", DbType.String, objTourWebBDto.Cost);


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

        #region Update Itenary
        /// <summary>
        /// Insert TourInfo.
        /// </summary>
        /// <param name="customer">TourBDto object that TourInfo data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int UpdateWebItenary(TourWebBDto objTourWebBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_ITENARY_UPDATE);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, objTourWebBDto.tourId);

                db.AddInParameter(dbCmd, "@WEB_ITENARY", DbType.String, objTourWebBDto.Itinerary);


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

        #region Update Web ImportantNotes
        /// <summary>
        /// Insert TourInfo.
        /// </summary>
        /// <param name="customer">TourBDto object that TourInfo data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int UpdateWebImportantNotes(TourWebBDto objTourWebBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_IMPORTANT_NOTES_UPDATE);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, objTourWebBDto.tourId);

                db.AddInParameter(dbCmd, "@WEB_IMPORTANT_NOTES", DbType.String, objTourWebBDto.ImportantNote);


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

        #region Update Web Terms
        /// <summary>
        /// Insert TourInfo.
        /// </summary>
        /// <param name="customer">TourBDto object that TourInfo data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int UpdateWebTerms(TourWebBDto objTourWebBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_MASTER_WEB_TERMS_UPDATE);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, objTourWebBDto.tourId);

                db.AddInParameter(dbCmd, "@WEB_TERMS", DbType.String, objTourWebBDto.Terms);


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
        #endregion


        public DataTable GetTourFlash()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TOUR_FLASH_SELECT");
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

        public int SetTourFlash(string topLeft, string topRight, string bottomLeft, string bottomRight)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TOUR_FLASH_SAVE");

                if (!string.IsNullOrEmpty(topLeft))
                    db.AddInParameter(dbCmd, "@TOP_LEFT", DbType.String, topLeft.Trim());
                else
                    db.AddInParameter(dbCmd, "@TOP_LEFT", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(topRight))
                    db.AddInParameter(dbCmd, "@TOP_RIGHT", DbType.String, topRight.Trim());
                else
                    db.AddInParameter(dbCmd, "@TOP_RIGHT", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(bottomLeft))
                    db.AddInParameter(dbCmd, "@BOTTOM_LEFT", DbType.String, bottomLeft.Trim());
                else
                    db.AddInParameter(dbCmd, "@BOTTOM_LEFT", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(bottomRight))
                    db.AddInParameter(dbCmd, "@BOTTOM_RIGHT", DbType.String, bottomRight.Trim());
                else
                    db.AddInParameter(dbCmd, "@BOTTOM_RIGHT", DbType.String, DBNull.Value);

                return db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }

                return 0;
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
        }


        

    }
}
