using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using CRM.Model.Booking.Ticket;
using CRM.Model.Booking.TourBooking;
using CRM.Model.CustomersModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.BookingDal.Ticket
{
    public class TicketBookingDal
    {
        #region InsertBookingTicketBooking
        public int InsertBookingTicketBooking(TicketBookingHdrBDto ticketBookingHrd)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TICKET_BOOKING_INSERT);

                if (ticketBookingHrd.TicketTypeId != 0)
                    db.AddInParameter(dbCmd, "@TICKET_TYPE_ID", DbType.Int32, ticketBookingHrd.TicketTypeId);
                else
                    db.AddInParameter(dbCmd, "@TICKET_TYPE_ID", DbType.Int32, DBNull.Value);

                if (ticketBookingHrd.ModeOfTravel != 0)
                    db.AddInParameter(dbCmd, "@MODE_OF_TRAVEL", DbType.Int32, ticketBookingHrd.ModeOfTravel);
                else
                    db.AddInParameter(dbCmd, "@MODE_OF_TRAVEL", DbType.Int32, DBNull.Value);

                if (ticketBookingHrd.DateFrom != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@DATE_FROM", DbType.DateTime, ticketBookingHrd.DateFrom);
                else
                    db.AddInParameter(dbCmd, "@DATE_FROM", DbType.DateTime, DBNull.Value);

                if (ticketBookingHrd.DateTo != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@DATE_TO", DbType.DateTime, ticketBookingHrd.DateTo);
                else
                    db.AddInParameter(dbCmd, "@DATE_TO", DbType.DateTime, DBNull.Value);

                if (ticketBookingHrd.PlaceFrom != 0)
                    db.AddInParameter(dbCmd, "@PLACE_FROM", DbType.Int32, ticketBookingHrd.PlaceFrom);
                else
                    db.AddInParameter(dbCmd, "@PLACE_FROM", DbType.Int32, DBNull.Value);

                if (ticketBookingHrd.PlaceTo != 0)
                    db.AddInParameter(dbCmd, "@PLACE_TO", DbType.Int32, ticketBookingHrd.PlaceTo);
                else
                    db.AddInParameter(dbCmd, "@PLACE_TO", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(ticketBookingHrd.PnrNo))
                    db.AddInParameter(dbCmd, "@PNR_NO", DbType.String, ticketBookingHrd.PnrNo);
                else
                    db.AddInParameter(dbCmd, "@PNR_NO", DbType.String, DBNull.Value);

                if (ticketBookingHrd.AirlineId != 0)
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, ticketBookingHrd.AirlineId);
                else
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, DBNull.Value);

                if (ticketBookingHrd.InquiryId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, ticketBookingHrd.InquiryId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, DBNull.Value);
                if (!String.IsNullOrEmpty(ticketBookingHrd.EmergencyContactPersonName))
                    db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_NAME", DbType.String, ticketBookingHrd.EmergencyContactPersonName);
                else
                    db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_NAME", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(ticketBookingHrd.EmergencyContactPersonAddress))
                    db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_ADDRESS", DbType.String, ticketBookingHrd.EmergencyContactPersonAddress);
                else
                    db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_ADDRESS", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(ticketBookingHrd.EmergencyPhoneNo))
                    db.AddInParameter(dbCmd, "@EMERGENCY_PHONE_NO", DbType.String, ticketBookingHrd.EmergencyPhoneNo);
                else
                    db.AddInParameter(dbCmd, "@EMERGENCY_PHONE_NO", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(ticketBookingHrd.EmergencyMobileNo))
                    db.AddInParameter(dbCmd, "@EMERGENCY_MOBILE_NO", DbType.String, ticketBookingHrd.EmergencyMobileNo);
                else
                    db.AddInParameter(dbCmd, "@EMERGENCY_MOBILE_NO", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(ticketBookingHrd.EmergencyEmail))
                    db.AddInParameter(dbCmd, "@EMERGENCY_EMAIL", DbType.String, ticketBookingHrd.EmergencyEmail);
                else
                    db.AddInParameter(dbCmd, "@EMERGENCY_EMAIL", DbType.String, DBNull.Value);
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

        #region UpdateBookingTicketBooking
        public int UpdateBookingTicketBooking(TicketBookingHdrBDto ticketBookingHrd)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TICKET_BOOKING_INSERT);

                db.AddInParameter(dbCmd, "@TICKET_BOOKING_ID", DbType.Int32, ticketBookingHrd.TicketBookingId);
                if (ticketBookingHrd.TicketTypeId != 0)
                    db.AddInParameter(dbCmd, "@TICKET_TYPE_ID", DbType.Int32, ticketBookingHrd.TicketTypeId);
                else
                    db.AddInParameter(dbCmd, "@TICKET_TYPE_ID", DbType.Int32, DBNull.Value);

                if (ticketBookingHrd.ModeOfTravel != 0)
                    db.AddInParameter(dbCmd, "@MODE_OF_TRAVEL", DbType.Int32, ticketBookingHrd.ModeOfTravel);
                else
                    db.AddInParameter(dbCmd, "@MODE_OF_TRAVEL", DbType.Int32, DBNull.Value);

                if (ticketBookingHrd.DateFrom != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@DATE_FROM", DbType.DateTime, ticketBookingHrd.DateFrom);
                else
                    db.AddInParameter(dbCmd, "@DATE_FROM", DbType.DateTime, DBNull.Value);

                if (ticketBookingHrd.DateTo != DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@DATE_TO", DbType.DateTime, ticketBookingHrd.DateTo);
                else
                    db.AddInParameter(dbCmd, "@DATE_TO", DbType.DateTime, DBNull.Value);

                if (ticketBookingHrd.PlaceFrom != 0)
                    db.AddInParameter(dbCmd, "@PLACE_FROM", DbType.Int32, ticketBookingHrd.PlaceFrom);
                else
                    db.AddInParameter(dbCmd, "@PLACE_FROM", DbType.Int32, DBNull.Value);

                if (ticketBookingHrd.PlaceTo != 0)
                    db.AddInParameter(dbCmd, "@PLACE_TO", DbType.Int32, ticketBookingHrd.PlaceTo);
                else
                    db.AddInParameter(dbCmd, "@PLACE_TO", DbType.Int32, DBNull.Value);

                if (!String.IsNullOrEmpty(ticketBookingHrd.PnrNo))
                    db.AddInParameter(dbCmd, "@PNR_NO", DbType.String, ticketBookingHrd.PnrNo);
                else
                    db.AddInParameter(dbCmd, "@PNR_NO", DbType.String, DBNull.Value);

                if (ticketBookingHrd.AirlineId != 0)
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, ticketBookingHrd.AirlineId);
                else
                    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, DBNull.Value);

                if (ticketBookingHrd.InquiryId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, ticketBookingHrd.InquiryId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, DBNull.Value);
                if (!String.IsNullOrEmpty(ticketBookingHrd.EmergencyContactPersonName))
                    db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_NAME", DbType.String, ticketBookingHrd.EmergencyContactPersonName);
                else
                    db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_NAME", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(ticketBookingHrd.EmergencyContactPersonAddress))
                    db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_ADDRESS", DbType.String, ticketBookingHrd.EmergencyContactPersonAddress);
                else
                    db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_ADDRESS", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(ticketBookingHrd.EmergencyPhoneNo))
                    db.AddInParameter(dbCmd, "@EMERGENCY_PHONE_NO", DbType.String, ticketBookingHrd.EmergencyPhoneNo);
                else
                    db.AddInParameter(dbCmd, "@EMERGENCY_PHONE_NO", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(ticketBookingHrd.EmergencyMobileNo))
                    db.AddInParameter(dbCmd, "@EMERGENCY_MOBILE_NO", DbType.String, ticketBookingHrd.EmergencyMobileNo);
                else
                    db.AddInParameter(dbCmd, "@EMERGENCY_MOBILE_NO", DbType.String, DBNull.Value);

                if (!String.IsNullOrEmpty(ticketBookingHrd.EmergencyEmail))
                    db.AddInParameter(dbCmd, "@EMERGENCY_EMAIL", DbType.String, ticketBookingHrd.EmergencyEmail);
                else
                    db.AddInParameter(dbCmd, "@EMERGENCY_EMAIL", DbType.String, DBNull.Value);
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

        #region GetBookingTicketBooking
        public DataTable GetBookingTicketBooking(int TicketBookingId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.UPS_FARE_TOUR_SELECT_BY_ID);
                if(TicketBookingId !=0)
                db.AddInParameter(dbCmd, "@TICKET_BOOKING_ID", DbType.Int32, TicketBookingId);
                else
                db.AddInParameter(dbCmd, "@TICKET_BOOKING_ID", DbType.Int32, DBNull.Value);
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
