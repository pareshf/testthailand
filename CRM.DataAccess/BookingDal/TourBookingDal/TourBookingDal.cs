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

namespace CRM.DataAccess.BookingDal.TourBookingDal
{
	public class TourBookingDal
	{
		#region Booking Info

		public DataTable GetTourDetailsById(int tourId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.UPS_FARE_TOUR_SELECT_BY_ID);
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

		public DataSet GetTourBookingDetailById(int bookingId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			//DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_HDR_SELECT_BY_BOOKING_ID);
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
				ds = db.ExecuteDataSet(dbCmd);
				//if (ds != null && ds.Tables.Count > 0)
				//{
				//    dt = ds.Tables[0];
				//}
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

		public DataSet GetExistingTourBookingDetail(int customerId, int inquiryId, int tourId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			//DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_BOOKING_SELECT_FOR_EXIST_BOOKING");
				db.AddInParameter(dbCmd, "@CUSTOMER_ID", DbType.Int32, customerId);
				db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
				db.AddInParameter(dbCmd, "@TOUR", DbType.Int32, tourId);

				ds = db.ExecuteDataSet(dbCmd);
				//if (ds != null && ds.Tables.Count > 0)
				//{
				//    dt = ds.Tables[0];
				//}
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

		public DataTable GetTourBookingDetails(TourBookingHrdBDto tourBookingHrd, CustomerBDto customer)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_HDR_SELECT);

				if (tourBookingHrd.TourTypeId != 0)
					db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, tourBookingHrd.TourTypeId);
				else
					db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.TourId != 0)
					db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourBookingHrd.TourId);
				else
					db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.BookingDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@BOOKING_DATE", DbType.DateTime, tourBookingHrd.BookingDate);
				else
					db.AddInParameter(dbCmd, "@BOOKING_DATE", DbType.DateTime, DBNull.Value);

				if (tourBookingHrd.SalesExecutiveId != 0)
					db.AddInParameter(dbCmd, "@SALES_EXECUTIVE_ID", DbType.Int32, tourBookingHrd.SalesExecutiveId);
				else
					db.AddInParameter(dbCmd, "@SALES_EXECUTIVE_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.BranchId != 0)
					db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, tourBookingHrd.BranchId);
				else
					db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.AgentId != 0)
					db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, tourBookingHrd.AgentId);
				else
					db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.BoardingFrom != 0)
					db.AddInParameter(dbCmd, "@BOARDING_FROM", DbType.Int32, tourBookingHrd.BoardingFrom);
				else
					db.AddInParameter(dbCmd, "@BOARDING_FROM", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.ArrivalTo != 0)
					db.AddInParameter(dbCmd, "@ARRIVAL_TO", DbType.Int32, tourBookingHrd.ArrivalTo);
				else
					db.AddInParameter(dbCmd, "@ARRIVAL_TO", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.DepartureDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@DEPARTURE_DATE", DbType.DateTime, tourBookingHrd.DepartureDate);
				else
					db.AddInParameter(dbCmd, "@DEPARTURE_DATE", DbType.DateTime, DBNull.Value);

				if (!String.IsNullOrEmpty(customer.UniqueId))
					db.AddInParameter(dbCmd, "@CUSTOMER_ID", DbType.String, customer.UniqueId);
				else
					db.AddInParameter(dbCmd, "@CUSTOMER_ID", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(customer.Name))
					db.AddInParameter(dbCmd, "@CUSTOMER_FIRST_NAME", DbType.String, customer.Name);
				else
					db.AddInParameter(dbCmd, "@CUSTOMER_FIRST_NAME", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(customer.SurName))
					db.AddInParameter(dbCmd, "@CUSTOMER_LAST_NAME", DbType.String, customer.SurName);
				else
					db.AddInParameter(dbCmd, "@CUSTOMER_LAST_NAME", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.EmergencyPersonName))
					db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_NAME", DbType.String, tourBookingHrd.EmergencyPersonName);
				else
					db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_NAME", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.EmergencyMobileNo))
					db.AddInParameter(dbCmd, "@EMERGENCY_MOBILE_NO", DbType.String, tourBookingHrd.EmergencyMobileNo);
				else
					db.AddInParameter(dbCmd, "@EMERGENCY_MOBILE_NO", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.EmergencyEmail))
					db.AddInParameter(dbCmd, "@EMERGENCY_EMAIL", DbType.String, tourBookingHrd.EmergencyEmail);
				else
					db.AddInParameter(dbCmd, "@EMERGENCY_EMAIL", DbType.String, DBNull.Value);

				ds = db.ExecuteDataSet(dbCmd);

				using (ds)
				{
					if (ds != null && ds.Tables.Count > 0)
					{
						dt = ds.Tables[0];
					}
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


		public DataTable GetCurrencyByInqSrNo(int InqSrNo)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("USP_BOOKING_PAYMENT_GET_CURRENCY_BY_INQSRNO");
				db.AddInParameter(dbCmd, "@INQUIRY_SR_NO", DbType.Int32, InqSrNo);
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



		public DataSet GetInquiryDetail(int inquirySrNo)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQUIRY_DETAIL_SELECT");
				db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, inquirySrNo);
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

		public int InsertCustomerBooking(TourBookingHrdBDto tourBookingHrd, ref int bookingId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_HRD_INSERT);

				if (tourBookingHrd.TourTypeId != 0)
					db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, tourBookingHrd.TourTypeId);
				else
					db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.TourId != 0)
					db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourBookingHrd.TourId);
				else
					db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.BookingDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@BOOKING_DATE", DbType.DateTime, tourBookingHrd.BookingDate);
				else
					db.AddInParameter(dbCmd, "@BOOKING_DATE", DbType.DateTime, DBNull.Value);

				if (tourBookingHrd.SalesExecutiveId != 0)
					db.AddInParameter(dbCmd, "@SALES_EXECUTIVE_ID", DbType.Int32, tourBookingHrd.SalesExecutiveId);
				else
					db.AddInParameter(dbCmd, "@SALES_EXECUTIVE_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.BranchId != 0)
					db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, tourBookingHrd.BranchId);
				else
					db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.AgentId != 0)
					db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, tourBookingHrd.AgentId);
				else
					db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.CustomerId != 0)
					db.AddInParameter(dbCmd, "@CUSTOMER_ID", DbType.Int32, tourBookingHrd.CustomerId);
				else
					db.AddInParameter(dbCmd, "@CUSTOMER_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.InquiryId != 0)
					db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, tourBookingHrd.InquiryId);
				else
					db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.NoOfAdult != 0)
					db.AddInParameter(dbCmd, "@NO_OF_ADULT", DbType.Int32, tourBookingHrd.NoOfAdult);
				else
					db.AddInParameter(dbCmd, "@NO_OF_ADULT", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.NoOfChildWithBed != 0)
					db.AddInParameter(dbCmd, "@NO_OF_CHILD_WITH_BED", DbType.Int32, tourBookingHrd.NoOfChildWithBed);
				else
					db.AddInParameter(dbCmd, "@NO_OF_CHILD_WITH_BED", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.NoOfChildWithoutBed != 0)
					db.AddInParameter(dbCmd, "@NO_OF_CHILD_WITHOUT_BED", DbType.Int32, tourBookingHrd.NoOfChildWithoutBed);
				else
					db.AddInParameter(dbCmd, "@NO_OF_CHILD_WITHOUT_BED", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.AdultCost != 0)
					db.AddInParameter(dbCmd, "@ADULT_COST", DbType.Decimal, tourBookingHrd.AdultCost);
				else
					db.AddInParameter(dbCmd, "@ADULT_COST", DbType.Decimal, DBNull.Value);

				if (tourBookingHrd.ChildCostWithBed != 0)
					db.AddInParameter(dbCmd, "@CHILD_COST_WITH_BED", DbType.Decimal, tourBookingHrd.ChildCostWithBed);
				else
					db.AddInParameter(dbCmd, "@CHILD_COST_WITH_BED", DbType.Decimal, DBNull.Value);

				if (tourBookingHrd.ChildCostWithoutBed != 0)
					db.AddInParameter(dbCmd, "@CHILD_COST_WITHOUT_BED", DbType.Decimal, tourBookingHrd.ChildCostWithoutBed);
				else
					db.AddInParameter(dbCmd, "@CHILD_COST_WITHOUT_BED", DbType.Decimal, DBNull.Value);

				if (tourBookingHrd.BoardingFrom != 0)
					db.AddInParameter(dbCmd, "@BOARDING_FROM", DbType.Int32, tourBookingHrd.BoardingFrom);
				else
					db.AddInParameter(dbCmd, "@BOARDING_FROM", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.ArrivalTo != 0)
					db.AddInParameter(dbCmd, "@ARRIVAL_TO", DbType.Int32, tourBookingHrd.ArrivalTo);
				else
					db.AddInParameter(dbCmd, "@ARRIVAL_TO", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.DepartureDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@DEPARTURE_DATE", DbType.DateTime, tourBookingHrd.DepartureDate);
				else
					db.AddInParameter(dbCmd, "@DEPARTURE_DATE", DbType.DateTime, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.EmergencyPersonName))
					db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_NAME", DbType.String, tourBookingHrd.EmergencyPersonName);
				else
					db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_NAME", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.EmergencyPersonAddress))
					db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_ADDRESS", DbType.String, tourBookingHrd.EmergencyPersonAddress);
				else
					db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_ADDRESS", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.EmergencyPhoneNo))
					db.AddInParameter(dbCmd, "@EMERGENCY_PHONE_NO", DbType.String, tourBookingHrd.EmergencyPhoneNo);
				else
					db.AddInParameter(dbCmd, "@EMERGENCY_PHONE_NO", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.EmergencyMobileNo))
					db.AddInParameter(dbCmd, "@EMERGENCY_MOBILE_NO", DbType.String, tourBookingHrd.EmergencyMobileNo);
				else
					db.AddInParameter(dbCmd, "@EMERGENCY_MOBILE_NO", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.EmergencyEmail))
					db.AddInParameter(dbCmd, "@EMERGENCY_EMAIL", DbType.String, tourBookingHrd.EmergencyEmail);
				else
					db.AddInParameter(dbCmd, "@EMERGENCY_EMAIL", DbType.String, DBNull.Value);

				if (tourBookingHrd.MarginAmount != 0)
					db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, tourBookingHrd.MarginAmount);
				else
					db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, DBNull.Value);

				db.AddInParameter(dbCmd, "@IS_PERCENT", DbType.Boolean, tourBookingHrd.IsPercent);

				if (tourBookingHrd.AmountDeposite != 0)
					db.AddInParameter(dbCmd, "@AMOUNT_DEPOSIT", DbType.Decimal, tourBookingHrd.AmountDeposite);
				else
					db.AddInParameter(dbCmd, "@AMOUNT_DEPOSIT", DbType.Decimal, DBNull.Value);

				if (tourBookingHrd.BalanceToBePaid != 0)
					db.AddInParameter(dbCmd, "@BALANCE_TO_BE_PAY", DbType.Decimal, tourBookingHrd.BalanceToBePaid);
				else
					db.AddInParameter(dbCmd, "@BALANCE_TO_BE_PAY", DbType.Decimal, DBNull.Value);

				if (tourBookingHrd.ModeOfPayment != 0)
					db.AddInParameter(dbCmd, "@MODE_OF_PAYMENT", DbType.Int32, tourBookingHrd.ModeOfPayment);
				else
					db.AddInParameter(dbCmd, "@MODE_OF_PAYMENT", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.BankId != 0)
					db.AddInParameter(dbCmd, "@BANK_ID", DbType.Int32, tourBookingHrd.BankId);
				else
					db.AddInParameter(dbCmd, "@BANK_ID", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.BankBranchName))
					db.AddInParameter(dbCmd, "@BANK_BRANCH_NAME", DbType.String, tourBookingHrd.BankBranchName);
				else
					db.AddInParameter(dbCmd, "@BANK_BRANCH_NAME", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.ReceiptNoChequeNoDdNo))
					db.AddInParameter(dbCmd, "@RECEPT_NO_CHQNO_DDNO", DbType.String, tourBookingHrd.ReceiptNoChequeNoDdNo);
				else
					db.AddInParameter(dbCmd, "@RECEPT_NO_CHQNO_DDNO", DbType.String, DBNull.Value);

				if (tourBookingHrd.Amount != 0)
					db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, tourBookingHrd.Amount);
				else
					db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, DBNull.Value);

				db.AddOutParameter(dbCmd, "@BOOKING_ID", DbType.Int32, 10);
				Result = db.ExecuteNonQuery(dbCmd);
				bookingId = Convert.ToInt32(db.GetParameterValue(dbCmd, "@BOOKING_ID"));
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

		public int UpdateCustomerBooking(TourBookingHrdBDto tourBookingHrd)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_HRD_UPDATE);

				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, tourBookingHrd.BookingId);

				if (tourBookingHrd.TourTypeId != 0)
					db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, tourBookingHrd.TourTypeId);
				else
					db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.TourId != 0)
					db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourBookingHrd.TourId);
				else
					db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.BookingDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@BOOKING_DATE", DbType.DateTime, tourBookingHrd.BookingDate);
				else
					db.AddInParameter(dbCmd, "@BOOKING_DATE", DbType.DateTime, DBNull.Value);

				if (tourBookingHrd.SalesExecutiveId != 0)
					db.AddInParameter(dbCmd, "@SALES_EXECUTIVE_ID", DbType.Int32, tourBookingHrd.SalesExecutiveId);
				else
					db.AddInParameter(dbCmd, "@SALES_EXECUTIVE_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.BranchId != 0)
					db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, tourBookingHrd.BranchId);
				else
					db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.AgentId != 0)
					db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, tourBookingHrd.AgentId);
				else
					db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.CustomerId != 0)
					db.AddInParameter(dbCmd, "@CUSTOMER_ID", DbType.Int32, tourBookingHrd.CustomerId);
				else
					db.AddInParameter(dbCmd, "@CUSTOMER_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.InquiryId != 0)
					db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, tourBookingHrd.InquiryId);
				else
					db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.NoOfAdult != 0)
					db.AddInParameter(dbCmd, "@NO_OF_ADULT", DbType.Int32, tourBookingHrd.NoOfAdult);
				else
					db.AddInParameter(dbCmd, "@NO_OF_ADULT", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.NoOfChildWithBed != 0)
					db.AddInParameter(dbCmd, "@NO_OF_CHILD_WITH_BED", DbType.Int32, tourBookingHrd.NoOfChildWithBed);
				else
					db.AddInParameter(dbCmd, "@NO_OF_CHILD_WITH_BED", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.NoOfChildWithoutBed != 0)
					db.AddInParameter(dbCmd, "@NO_OF_CHILD_WITHOUT_BED", DbType.Int32, tourBookingHrd.NoOfChildWithoutBed);
				else
					db.AddInParameter(dbCmd, "@NO_OF_CHILD_WITHOUT_BED", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.AdultCost != 0)
					db.AddInParameter(dbCmd, "@ADULT_COST", DbType.Decimal, tourBookingHrd.AdultCost);
				else
					db.AddInParameter(dbCmd, "@ADULT_COST", DbType.Decimal, DBNull.Value);

				if (tourBookingHrd.ChildCostWithBed != 0)
					db.AddInParameter(dbCmd, "@CHILD_COST_WITH_BED", DbType.Decimal, tourBookingHrd.ChildCostWithBed);
				else
					db.AddInParameter(dbCmd, "@CHILD_COST_WITH_BED", DbType.Decimal, DBNull.Value);

				if (tourBookingHrd.ChildCostWithoutBed != 0)
					db.AddInParameter(dbCmd, "@CHILD_COST_WITHOUT_BED", DbType.Decimal, tourBookingHrd.ChildCostWithoutBed);
				else
					db.AddInParameter(dbCmd, "@CHILD_COST_WITHOUT_BED", DbType.Decimal, DBNull.Value);

				if (tourBookingHrd.BoardingFrom != 0)
					db.AddInParameter(dbCmd, "@BOARDING_FROM", DbType.Int32, tourBookingHrd.BoardingFrom);
				else
					db.AddInParameter(dbCmd, "@BOARDING_FROM", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.ArrivalTo != 0)
					db.AddInParameter(dbCmd, "@ARRIVAL_TO", DbType.Int32, tourBookingHrd.ArrivalTo);
				else
					db.AddInParameter(dbCmd, "@ARRIVAL_TO", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.DepartureDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@DEPARTURE_DATE", DbType.DateTime, tourBookingHrd.DepartureDate);
				else
					db.AddInParameter(dbCmd, "@DEPARTURE_DATE", DbType.DateTime, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.EmergencyPersonName))
					db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_NAME", DbType.String, tourBookingHrd.EmergencyPersonName);
				else
					db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_NAME", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.EmergencyPersonAddress))
					db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_ADDRESS", DbType.String, tourBookingHrd.EmergencyPersonAddress);
				else
					db.AddInParameter(dbCmd, "@EMERGENCY_CONTACT_PERSON_ADDRESS", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.EmergencyPhoneNo))
					db.AddInParameter(dbCmd, "@EMERGENCY_PHONE_NO", DbType.String, tourBookingHrd.EmergencyPhoneNo);
				else
					db.AddInParameter(dbCmd, "@EMERGENCY_PHONE_NO", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.EmergencyMobileNo))
					db.AddInParameter(dbCmd, "@EMERGENCY_MOBILE_NO", DbType.String, tourBookingHrd.EmergencyMobileNo);
				else
					db.AddInParameter(dbCmd, "@EMERGENCY_MOBILE_NO", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.EmergencyEmail))
					db.AddInParameter(dbCmd, "@EMERGENCY_EMAIL", DbType.String, tourBookingHrd.EmergencyEmail);
				else
					db.AddInParameter(dbCmd, "@EMERGENCY_EMAIL", DbType.String, DBNull.Value);

				if (tourBookingHrd.MarginAmount != 0)
					db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, tourBookingHrd.MarginAmount);
				else
					db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, DBNull.Value);

				db.AddInParameter(dbCmd, "@IS_PERCENT", DbType.Boolean, tourBookingHrd.IsPercent);

				if (tourBookingHrd.AmountDeposite != 0)
					db.AddInParameter(dbCmd, "@AMOUNT_DEPOSIT", DbType.Decimal, tourBookingHrd.AmountDeposite);
				else
					db.AddInParameter(dbCmd, "@AMOUNT_DEPOSIT", DbType.Decimal, DBNull.Value);

				if (tourBookingHrd.BalanceToBePaid != 0)
					db.AddInParameter(dbCmd, "@BALANCE_TO_BE_PAY", DbType.Decimal, tourBookingHrd.BalanceToBePaid);
				else
					db.AddInParameter(dbCmd, "@BALANCE_TO_BE_PAY", DbType.Decimal, DBNull.Value);

				if (tourBookingHrd.ModeOfPayment != 0)
					db.AddInParameter(dbCmd, "@MODE_OF_PAYMENT", DbType.Int32, tourBookingHrd.ModeOfPayment);
				else
					db.AddInParameter(dbCmd, "@MODE_OF_PAYMENT", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.BankId != 0)
					db.AddInParameter(dbCmd, "@BANK_ID", DbType.Int32, tourBookingHrd.BankId);
				else
					db.AddInParameter(dbCmd, "@BANK_ID", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.BankBranchName))
					db.AddInParameter(dbCmd, "@BANK_BRANCH_NAME", DbType.String, tourBookingHrd.BankBranchName);
				else
					db.AddInParameter(dbCmd, "@BANK_BRANCH_NAME", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.ReceiptNoChequeNoDdNo))
					db.AddInParameter(dbCmd, "@RECEPT_NO_CHQNO_DDNO", DbType.String, tourBookingHrd.ReceiptNoChequeNoDdNo);
				else
					db.AddInParameter(dbCmd, "@RECEPT_NO_CHQNO_DDNO", DbType.String, DBNull.Value);



				if (tourBookingHrd.Amount != 0)
					db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, tourBookingHrd.Amount);
				else
					db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, DBNull.Value);

				if (tourBookingHrd.MarginAmount != 0)
					db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, tourBookingHrd.UserId);
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

		public int FixBooking(int bookingId)
		{

			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_BOOKING_FIX_BOOKING");
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
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

			return 0;
		}

		#endregion

		#region Booking Detail

		public DataSet GetTourBookingDetail(int bookingId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			//DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_DETAILS_SELECT);
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
				ds = db.ExecuteDataSet(dbCmd);
				//if (ds != null && ds.Tables.Count > 0)
				//{
				//    dt = ds.Tables[0];
				//}
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

		public DataTable GetTourBookingDetailForRoomAllocation(int bookingId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("USP_BOOKING_TOUR_BOOKING_DETAILS_SELECT_FOR_ROOM_ALLOCATION");
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
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

		public DataTable GetCustomerForHotelBooking(int bookingId, int tourId, int hotelId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				//dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_DETAILS_SELECT_FOR_ROOM_ALLOCATION);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_NOT_BOOKED_CUST_FOR_HOTEL_SELECT_TEST");
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
				db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
				db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, hotelId);
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

		public DataTable GetCustomerMembers(int inquirySrNo)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_CUSTOMER_MEMBER_SELECT");
				db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, inquirySrNo);
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

		public DataSet GetTourAmount(int inquirySrNo, int tourId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_QUOTED_AMOUNT_SELECT");
				db.AddInParameter(dbCmd, "@INQUIRY_SR_NO", DbType.Int32, inquirySrNo);
				db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
				ds = db.ExecuteDataSet(dbCmd);
				//if (ds != null && ds.Tables.Count > 0)
				//{
				//    dt = ds.Tables[0];
				//}
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

		public DataTable GetCustomerMembersDetail(int bookingId, int customerSrNo)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_BOOKING_DETAILS_SELECT_BY_CUSTOMER");
				db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, customerSrNo);
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
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

		public DataSet GetTourBookingDetailById(int serialNo, int bookingId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			//DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_DETAILS_SELECT_BY_ID);
				db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, serialNo);
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
				ds = db.ExecuteDataSet(dbCmd);
				//if (ds != null && ds.Tables.Count > 0)
				//{
				//    dt = ds.Tables[0];
				//}
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

		public int InsertCustomerBookingDetail(TourBookingDetailsBDto tourBookingDetails, ref int serialNo)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_DETAILS_INSERT);

				if (tourBookingDetails.BookingId != 0)
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, tourBookingDetails.BookingId);
				else
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, DBNull.Value);

				if (tourBookingDetails.CustomerSerialNo != 0)
					db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, tourBookingDetails.CustomerSerialNo);
				else
					db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, DBNull.Value);

				if (tourBookingDetails.MealId != 0)
					db.AddInParameter(dbCmd, "@MEAL_ID", DbType.Int32, tourBookingDetails.MealId);
				else
					db.AddInParameter(dbCmd, "@MEAL_ID", DbType.Int32, DBNull.Value);

				if (tourBookingDetails.TitleId != 0)
					db.AddInParameter(dbCmd, "@TITLE_ID", DbType.Int32, tourBookingDetails.TitleId);
				else
					db.AddInParameter(dbCmd, "@TITLE_ID", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingDetails.FirstName))
					db.AddInParameter(dbCmd, "@FIRST_NAME", DbType.String, tourBookingDetails.FirstName);
				else
					db.AddInParameter(dbCmd, "@FIRST_NAME", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingDetails.SurName))
					db.AddInParameter(dbCmd, "@SUR_NAME", DbType.String, tourBookingDetails.SurName);
				else
					db.AddInParameter(dbCmd, "@SUR_NAME", DbType.String, DBNull.Value);

				if (tourBookingDetails.RelationId != 0)
					db.AddInParameter(dbCmd, "@RELATION_ID", DbType.Int32, tourBookingDetails.RelationId);
				else
					db.AddInParameter(dbCmd, "@RELATION_ID", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingDetails.Diet))
					db.AddInParameter(dbCmd, "@DIET", DbType.String, tourBookingDetails.Diet);
				else
					db.AddInParameter(dbCmd, "@DIET", DbType.String, DBNull.Value);

				if (tourBookingDetails.BirthDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@BIRTH_DATE", DbType.DateTime, tourBookingDetails.BirthDate);
				else
					db.AddInParameter(dbCmd, "@BIRTH_DATE", DbType.DateTime, DBNull.Value);

				if (tourBookingDetails.RoomTypeId != 0)
					db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, tourBookingDetails.RoomTypeId);
				else
					db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingDetails.PassportNo))
					db.AddInParameter(dbCmd, "@PASSPORT_NO", DbType.String, tourBookingDetails.PassportNo);
				else
					db.AddInParameter(dbCmd, "@PASSPORT_NO", DbType.String, DBNull.Value);

				if (tourBookingDetails.VisaValidDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@VISA_VALID_DATE", DbType.DateTime, tourBookingDetails.VisaValidDate);
				else
					db.AddInParameter(dbCmd, "@VISA_VALID_DATE", DbType.DateTime, DBNull.Value);

				if (tourBookingDetails.DOE != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@DOE", DbType.DateTime, tourBookingDetails.DOE);
				else
					db.AddInParameter(dbCmd, "@DOE", DbType.DateTime, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingDetails.CruiseDeckCabin))
					db.AddInParameter(dbCmd, "@CRUISE_DECK_CABIN", DbType.String, tourBookingDetails.CruiseDeckCabin);
				else
					db.AddInParameter(dbCmd, "@CRUISE_DECK_CABIN", DbType.String, DBNull.Value);

				db.AddInParameter(dbCmd, "@ADULT_OR_CHILD", DbType.Boolean, tourBookingDetails.AdultOrChild);
				db.AddInParameter(dbCmd, "@WITH_BED_OR_WITHOUT_BED", DbType.Boolean, tourBookingDetails.WithBedOrWithoutBed);
				db.AddInParameter(dbCmd, "@INFANT", DbType.Boolean, tourBookingDetails.InfantRd);

				if (tourBookingDetails.FrequentlyFlyId != 0)
					db.AddInParameter(dbCmd, "@FREQUENTLY_FLY_AIRLINE_ID", DbType.Int32, tourBookingDetails.FrequentlyFlyId);
				else
					db.AddInParameter(dbCmd, "@FREQUENTLY_FLY_AIRLINE_ID", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingDetails.FrequentlyFlyNo))
					db.AddInParameter(dbCmd, "@FREQUENTLY_FLY_NO", DbType.String, tourBookingDetails.FrequentlyFlyNo);
				else
					db.AddInParameter(dbCmd, "@FREQUENTLY_FLY_NO", DbType.String, DBNull.Value);

				if (tourBookingDetails.ForeignCurrency != 0)
					db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, tourBookingDetails.ForeignCurrency);
				else
					db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, DBNull.Value);

				if (tourBookingDetails.ForeignCurrencyCost != 0)
					db.AddInParameter(dbCmd, "@CURRENCY_COST", DbType.Decimal, tourBookingDetails.ForeignCurrencyCost);
				else
					db.AddInParameter(dbCmd, "@CURRENCY_COST", DbType.Decimal, DBNull.Value);

				if (tourBookingDetails.ForeignCurrencyRate != 0)
					db.AddInParameter(dbCmd, "@CURRENCY_RATE", DbType.Decimal, tourBookingDetails.ForeignCurrencyRate);
				else
					db.AddInParameter(dbCmd, "@CURRENCY_RATE", DbType.Decimal, DBNull.Value);

				if (tourBookingDetails.InrCost != 0)
					db.AddInParameter(dbCmd, "@INR_COST", DbType.Decimal, tourBookingDetails.InrCost);
				else
					db.AddInParameter(dbCmd, "@INR_COST", DbType.Decimal, DBNull.Value);

				if (tourBookingDetails.TotalCost != 0)
					db.AddInParameter(dbCmd, "@TOTAL_COST", DbType.Decimal, tourBookingDetails.TotalCost);
				else
					db.AddInParameter(dbCmd, "@TOTAL_COST", DbType.Decimal, DBNull.Value);

				if (tourBookingDetails.UserId != 0)
					db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, tourBookingDetails.UserId);
				else
					db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, DBNull.Value);

				db.AddOutParameter(dbCmd, "@SR_NO", DbType.Int32, 10);

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

		public int UpdateCustomerBookingDetail(TourBookingDetailsBDto tourBookingDetails)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_DETAILS_UPDATE);

				if (tourBookingDetails.SerialNo != 0)
					db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, tourBookingDetails.SerialNo);
				else
					db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, DBNull.Value);

				if (tourBookingDetails.BookingId != 0)
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, tourBookingDetails.BookingId);
				else
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, DBNull.Value);

				if (tourBookingDetails.MealId != 0)
					db.AddInParameter(dbCmd, "@MEAL_ID", DbType.Int32, tourBookingDetails.MealId);
				else
					db.AddInParameter(dbCmd, "@MEAL_ID", DbType.Int32, DBNull.Value);

				if (tourBookingDetails.TitleId != 0)
					db.AddInParameter(dbCmd, "@TITLE_ID", DbType.Int32, tourBookingDetails.TitleId);
				else
					db.AddInParameter(dbCmd, "@TITLE_ID", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingDetails.FirstName))
					db.AddInParameter(dbCmd, "@FIRST_NAME", DbType.String, tourBookingDetails.FirstName);
				else
					db.AddInParameter(dbCmd, "@FIRST_NAME", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingDetails.SurName))
					db.AddInParameter(dbCmd, "@SUR_NAME", DbType.String, tourBookingDetails.SurName);
				else
					db.AddInParameter(dbCmd, "@SUR_NAME", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingDetails.Diet))
					db.AddInParameter(dbCmd, "@DIET", DbType.String, tourBookingDetails.Diet);
				else
					db.AddInParameter(dbCmd, "@DIET", DbType.String, DBNull.Value);

				if (tourBookingDetails.BirthDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@BIRTH_DATE", DbType.DateTime, tourBookingDetails.BirthDate);
				else
					db.AddInParameter(dbCmd, "@BIRTH_DATE", DbType.DateTime, DBNull.Value);

				if (tourBookingDetails.RoomTypeId != 0)
					db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, tourBookingDetails.RoomTypeId);
				else
					db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingDetails.PassportNo))
					db.AddInParameter(dbCmd, "@PASSPORT_NO", DbType.String, tourBookingDetails.PassportNo);
				else
					db.AddInParameter(dbCmd, "@PASSPORT_NO", DbType.String, DBNull.Value);

				if (tourBookingDetails.VisaValidDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@VISA_VALID_DATE", DbType.DateTime, tourBookingDetails.VisaValidDate);
				else
					db.AddInParameter(dbCmd, "@VISA_VALID_DATE", DbType.DateTime, DBNull.Value);

				if (tourBookingDetails.DOE != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@DOE", DbType.DateTime, tourBookingDetails.DOE);
				else
					db.AddInParameter(dbCmd, "@DOE", DbType.DateTime, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingDetails.CruiseDeckCabin))
					db.AddInParameter(dbCmd, "@CRUISE_DECK_CABIN", DbType.String, tourBookingDetails.CruiseDeckCabin);
				else
					db.AddInParameter(dbCmd, "@CRUISE_DECK_CABIN", DbType.String, DBNull.Value);

				db.AddInParameter(dbCmd, "@ADULT_OR_CHILD", DbType.Boolean, tourBookingDetails.AdultOrChild);
				db.AddInParameter(dbCmd, "@WITH_BED_OR_WITHOUT_BED", DbType.Boolean, tourBookingDetails.WithBedOrWithoutBed);
				db.AddInParameter(dbCmd, "@INFANT", DbType.Boolean, tourBookingDetails.InfantRd);

				if (tourBookingDetails.FrequentlyFlyId != 0)
					db.AddInParameter(dbCmd, "@FREQUENTLY_FLY_AIRLINE_ID", DbType.Int32, tourBookingDetails.FrequentlyFlyId);
				else
					db.AddInParameter(dbCmd, "@FREQUENTLY_FLY_AIRLINE_ID", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingDetails.FrequentlyFlyNo))
					db.AddInParameter(dbCmd, "@FREQUENTLY_FLY_NO", DbType.String, tourBookingDetails.FrequentlyFlyNo);
				else
					db.AddInParameter(dbCmd, "@FREQUENTLY_FLY_NO", DbType.String, DBNull.Value);

				if (tourBookingDetails.ForeignCurrency != 0)
					db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, tourBookingDetails.ForeignCurrency);
				else
					db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, DBNull.Value);

				if (tourBookingDetails.ForeignCurrencyRate != 0)
					db.AddInParameter(dbCmd, "@CURRENCY_RATE", DbType.Decimal, tourBookingDetails.ForeignCurrencyRate);
				else
					db.AddInParameter(dbCmd, "@CURRENCY_RATE", DbType.Decimal, DBNull.Value);

				if (tourBookingDetails.ForeignCurrencyCost != 0)
					db.AddInParameter(dbCmd, "@CURRENCY_COST", DbType.Decimal, tourBookingDetails.ForeignCurrencyCost);
				else
					db.AddInParameter(dbCmd, "@CURRENCY_COST", DbType.Decimal, DBNull.Value);

				if (tourBookingDetails.InrCost != 0)
					db.AddInParameter(dbCmd, "@INR_COST", DbType.Decimal, tourBookingDetails.InrCost);
				else
					db.AddInParameter(dbCmd, "@INR_COST", DbType.Decimal, DBNull.Value);

				if (tourBookingDetails.TotalCost != 0)
					db.AddInParameter(dbCmd, "@TOTAL_COST", DbType.Decimal, tourBookingDetails.TotalCost);
				else
					db.AddInParameter(dbCmd, "@TOTAL_COST", DbType.Decimal, DBNull.Value);

				if (tourBookingDetails.UserId != 0)
					db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, tourBookingDetails.UserId);
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

		public int DeleteCustomerBookingDetail(int serialNo)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_DETAILS_DELETE);

				db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, serialNo);
				db.AddOutParameter(dbCmd, "@ERRORCODE", DbType.Int32, 8);

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

		public decimal GetCurrencyRate(int bookingId, int currencyId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_BOOKING_CURRENCY_RATE");
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
				db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.Int32, currencyId);
				object o = db.ExecuteScalar(dbCmd);
				return (decimal)o;
			}
			catch (Exception ex)
			{
				return 0;
			}
			finally
			{
				DALHelper.Destroy(ref dbCmd);
			}
		}

		#endregion

		#region Room Allocation
		public DataSet GetRoomAllocationDetails(int bookingId, int tourId, int tourTypeId, int hotelId, int roomTypeId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			//DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_HOTEL_ROOM_DETAILS_SELECT);
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);

				if (tourId != 0)
					db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
				else
					db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, DBNull.Value);

				if (tourTypeId != 0)
					db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, tourTypeId);
				else
					db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, DBNull.Value);

				if (hotelId != 0)
					db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, hotelId);
				else
					db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, DBNull.Value);

				if (roomTypeId != 0)
					db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, roomTypeId);
				else
					db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.Int32, DBNull.Value);

				ds = db.ExecuteDataSet(dbCmd);

				//if (ds != null && ds.Tables.Count > 0)
				//{
				//    dt = ds.Tables[0];
				//}
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

		public DataTable GetNotAllocatedRoomsNo(int hotelId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("[dbo].[USP_BOOKING_HOTEL_ROOM_NO_SELECT]");
				db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, hotelId);
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

		public int InsertRoomAllocation(TourBookingHotelRoomDetailsBDto tourBookingHotelRoomDetails)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_HOTEL_ROOM_DETAILS_INSERT);

				if (tourBookingHotelRoomDetails.BookingId != 0)
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, tourBookingHotelRoomDetails.BookingId);
				else
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHotelRoomDetails.TourTypeId != 0)
					db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, tourBookingHotelRoomDetails.TourTypeId);
				else
					db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHotelRoomDetails.TourId != 0)
					db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourBookingHotelRoomDetails.TourId);
				else
					db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHotelRoomDetails.HotelId != 0)
					db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, tourBookingHotelRoomDetails.HotelId);
				else
					db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHotelRoomDetails.SerialNo != 0)
					db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, tourBookingHotelRoomDetails.SerialNo);
				else
					db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHotelRoomDetails.RoonNo))
					db.AddInParameter(dbCmd, "@ROOM_NO", DbType.String, tourBookingHotelRoomDetails.RoonNo);
				else
					db.AddInParameter(dbCmd, "@ROOM_NO", DbType.String, DBNull.Value);

				if (tourBookingHotelRoomDetails.DateFrom != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@DATE_FROM", DbType.DateTime, tourBookingHotelRoomDetails.DateFrom);
				else
					db.AddInParameter(dbCmd, "@DATE_FROM", DbType.DateTime, DBNull.Value);

				if (tourBookingHotelRoomDetails.DateTo != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@DATE_TO", DbType.DateTime, tourBookingHotelRoomDetails.DateTo);
				else
					db.AddInParameter(dbCmd, "@DATE_TO", DbType.DateTime, DBNull.Value);

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

		#region Get Travelling Hotels
		/// <summary>
		/// Gets Travelling Hotels list.
		/// </summary>
		/// <returns>Returns dataset contains Travelling Hotels data.</returns>
		public DataSet GetTravellingHotelsOfTours(int tourId, int cityId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TRAVELLING_HOTELS_SELECT_BY_TOUR");
				db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
				db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, cityId);
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

		#endregion

		#region Airline Allocation

		public DataTable GetTourTicketBookingDetial(int bookingId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_TICKET_BOOKING_DETAILS_SELECT");
				db.AddInParameter(dbCmd, "@TOUR_BOOKING_ID", DbType.Int32, bookingId);
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

		public DataTable GetTourTicketBookingDetialById(int bookingId, int serialNo)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_TICKET_BOOKING_DETAILS_SELECT_BY_ID");
				db.AddInParameter(dbCmd, "@TOUR_BOOKING_ID", DbType.Int32, bookingId);
				db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, serialNo);
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

		public DataSet GetTourTicketTravelCities1(int tourbooingId, int tourBookingSerialNo, int tourId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_TICKET_TRAVEL_CITY_SELECT");
				db.AddInParameter(dbCmd, "@TOUR_BOOKING_ID", DbType.Int32, tourbooingId);
				db.AddInParameter(dbCmd, "@TOUR_BOOKING_SRNO", DbType.Int32, tourBookingSerialNo);
				db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
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

		public DataSet GetTourTicketTravelCities(int tourbooingId, int tourBookingSerialNo, int tourId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_TICKET_TRAVEL_CITY_SELECT1");
				db.AddInParameter(dbCmd, "@TOUR_BOOKING_ID", DbType.Int32, tourbooingId);
				db.AddInParameter(dbCmd, "@TOUR_BOOKING_SRNO", DbType.Int32, tourBookingSerialNo);
				db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
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

		public DataSet GetFlightBookingDetial(int tourId, int airLineId, DateTime departureDate, DateTime arrivalDate, int departurePlace, int arrivalPlace)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_TICKET_FLIGHTS_DETAIL_SELECT");
				db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
				db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, airLineId);
				db.AddInParameter(dbCmd, "@DATE_OF_DEPARTURE", DbType.DateTime, departureDate);
				db.AddInParameter(dbCmd, "@DATE_OF_ARRIVAL", DbType.DateTime, arrivalDate);
				db.AddInParameter(dbCmd, "@PLACE_OF_DEPARTURE", DbType.Int32, departurePlace);
				db.AddInParameter(dbCmd, "@PLACE_OF_ARRIVAL", DbType.Int32, arrivalPlace);
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

		public int InsertTicketBookingHeader(TicketBookingHdr ticketBookingHdr, ref int ticketBookingId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TICKET_BOOKING_HDR_INSERT_FROM_TOUR_BOOKING");

				if (ticketBookingHdr.TourBookingId != 0)
					db.AddInParameter(dbCmd, "@TOUR_BOOKING_ID", DbType.Int32, ticketBookingHdr.TourBookingId);
				else
					db.AddInParameter(dbCmd, "@TOUR_BOOKING_ID", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.TicketTypeId != 0)
					db.AddInParameter(dbCmd, "@TICKET_TYPE_ID", DbType.Int32, ticketBookingHdr.TicketTypeId);
				else
					db.AddInParameter(dbCmd, "@TICKET_TYPE_ID", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.TravelModeId != 0)
					db.AddInParameter(dbCmd, "@MODE_OF_TRAVEL", DbType.Int32, ticketBookingHdr.TravelModeId);
				else
					db.AddInParameter(dbCmd, "@MODE_OF_TRAVEL", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.DateFrom != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@DATE_FROM", DbType.DateTime, ticketBookingHdr.DateFrom);
				else
					db.AddInParameter(dbCmd, "@DATE_FROM", DbType.DateTime, DBNull.Value);

				if (ticketBookingHdr.DateTo != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@DATE_TO", DbType.DateTime, ticketBookingHdr.DateTo);
				else
					db.AddInParameter(dbCmd, "@DATE_TO", DbType.DateTime, DBNull.Value);

				if (ticketBookingHdr.InquiryId != 0)
					db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, ticketBookingHdr.InquiryId);
				else
					db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.PlaceFrom != 0)
					db.AddInParameter(dbCmd, "@PLACE_FROM", DbType.Int32, ticketBookingHdr.PlaceFrom);
				else
					db.AddInParameter(dbCmd, "@PLACE_FROM", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.PlaceTo != 0)
					db.AddInParameter(dbCmd, "@PLACE_TO", DbType.Int32, ticketBookingHdr.PlaceTo);
				else
					db.AddInParameter(dbCmd, "@PLACE_TO", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.AirlineId != 0)
					db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, ticketBookingHdr.AirlineId);
				else
					db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(ticketBookingHdr.PnrNo))
					db.AddInParameter(dbCmd, "@PNR_NO", DbType.String, ticketBookingHdr.PnrNo);
				else
					db.AddInParameter(dbCmd, "@PNR_NO", DbType.String, DBNull.Value);

				db.AddOutParameter(dbCmd, "@TICKET_BOOKING_ID", DbType.Int32, 10);
				Result = db.ExecuteNonQuery(dbCmd);
				ticketBookingId = Convert.ToInt32(db.GetParameterValue(dbCmd, "@TICKET_BOOKING_ID"));
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

		public int UpdateTicketBookingHeader(TicketBookingHdr ticketBookingHdr)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TICKET_BOOKING_HDR_INSERT_FROM_TOUR_BOOKING");

				//if (ticketBookingHdr.TourBookingId != 0)
				//    db.AddInParameter(dbCmd, "@TOUR_BOOKING_ID", DbType.Int32, ticketBookingHdr.TourBookingId);
				//else
				//    db.AddInParameter(dbCmd, "@TOUR_BOOKING_ID", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.TicketBookingId != 0)
					db.AddInParameter(dbCmd, "@TICKET_BOOKING_ID", DbType.Int32, ticketBookingHdr.TicketBookingId);
				else
					db.AddInParameter(dbCmd, "@TICKET_BOOKING_ID", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.TicketTypeId != 0)
					db.AddInParameter(dbCmd, "@TICKET_TYPE_ID", DbType.Int32, ticketBookingHdr.TicketTypeId);
				else
					db.AddInParameter(dbCmd, "@TICKET_TYPE_ID", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.TravelModeId != 0)
					db.AddInParameter(dbCmd, "@MODE_OF_TRAVEL", DbType.Int32, ticketBookingHdr.TravelModeId);
				else
					db.AddInParameter(dbCmd, "@MODE_OF_TRAVEL", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.DateFrom != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@DATE_FROM", DbType.DateTime, ticketBookingHdr.DateFrom);
				else
					db.AddInParameter(dbCmd, "@DATE_FROM", DbType.DateTime, DBNull.Value);

				if (ticketBookingHdr.DateTo != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@DATE_TO", DbType.DateTime, ticketBookingHdr.DateTo);
				else
					db.AddInParameter(dbCmd, "@DATE_TO", DbType.DateTime, DBNull.Value);

				if (ticketBookingHdr.InquiryId != 0)
					db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, ticketBookingHdr.InquiryId);
				else
					db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.PlaceFrom != 0)
					db.AddInParameter(dbCmd, "@PLACE_FROM", DbType.Int32, ticketBookingHdr.PlaceFrom);
				else
					db.AddInParameter(dbCmd, "@PLACE_FROM", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.PlaceTo != 0)
					db.AddInParameter(dbCmd, "@PLACE_TO", DbType.Int32, ticketBookingHdr.PlaceTo);
				else
					db.AddInParameter(dbCmd, "@PLACE_TO", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.AirlineId != 0)
					db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, ticketBookingHdr.AirlineId);
				else
					db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(ticketBookingHdr.PnrNo))
					db.AddInParameter(dbCmd, "@PNR_NO", DbType.String, ticketBookingHdr.PnrNo);
				else
					db.AddInParameter(dbCmd, "@PNR_NO", DbType.String, DBNull.Value);

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

		public int InsertTourTicketBookingDetial(TourTicketDetailBDto tourTicketDetail, ref int ticketSrNo)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_TICKET_BOOKING_DETAILS_INSERT");
				if (!String.IsNullOrEmpty(tourTicketDetail.xmlString))
					db.AddInParameter(dbCmd, "@XML_DATA", DbType.String, tourTicketDetail.xmlString);
				else
					db.AddInParameter(dbCmd, "@XML_DATA", DbType.String, DBNull.Value);
				db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);

				Result = db.ExecuteNonQuery(dbCmd);
				ticketSrNo = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));

				#region MyRegion
				//if (tourTicketDetail.SerialNo != 0)
				//    db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, tourTicketDetail.SerialNo);
				//else
				//    db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, DBNull.Value);

				//if (tourTicketDetail.TourBookingId != 0)
				//    db.AddInParameter(dbCmd, "@TOUR_BOOKING_ID", DbType.Int32, tourTicketDetail.TourBookingId);
				//else
				//    db.AddInParameter(dbCmd, "@TOUR_BOOKING_ID", DbType.Int32, DBNull.Value);

				//if (tourTicketDetail.TourBookingSerialNo != 0)
				//    db.AddInParameter(dbCmd, "@TOUR_BOOKING_SRNO", DbType.Int32, tourTicketDetail.TourBookingSerialNo);
				//else
				//    db.AddInParameter(dbCmd, "@TOUR_BOOKING_SRNO", DbType.Int32, DBNull.Value);

				//if (tourTicketDetail.TitleId != 0)
				//    db.AddInParameter(dbCmd, "@TITLE_ID", DbType.Int32, tourTicketDetail.TitleId);
				//else
				//    db.AddInParameter(dbCmd, "@TITLE_ID", DbType.Int32, DBNull.Value);

				//if (!String.IsNullOrEmpty(tourTicketDetail.LastName))
				//    db.AddInParameter(dbCmd, "@SURNAME", DbType.String, tourTicketDetail.LastName);
				//else
				//    db.AddInParameter(dbCmd, "@SURNAME", DbType.String, DBNull.Value);

				//if (!String.IsNullOrEmpty(tourTicketDetail.FirstName))
				//    db.AddInParameter(dbCmd, "@NAME", DbType.String, tourTicketDetail.FirstName);
				//else
				//    db.AddInParameter(dbCmd, "@NAME", DbType.String, DBNull.Value);

				//if (!String.IsNullOrEmpty(tourTicketDetail.PnrNo))
				//    db.AddInParameter(dbCmd, "@PNR_NO", DbType.String, tourTicketDetail.PnrNo);
				//else
				//    db.AddInParameter(dbCmd, "@PNR_NO", DbType.String, DBNull.Value);

				//if (!String.IsNullOrEmpty(tourTicketDetail.TicketNo))
				//    db.AddInParameter(dbCmd, "@TICKET_NO", DbType.String, tourTicketDetail.TicketNo);
				//else
				//    db.AddInParameter(dbCmd, "@TICKET_NO", DbType.String, DBNull.Value);

				//if (tourTicketDetail.DateOfDeparture != DateTime.MinValue)
				//    db.AddInParameter(dbCmd, "@DATE_OF_DEPARTURE", DbType.DateTime, tourTicketDetail.DateOfDeparture);
				//else
				//    db.AddInParameter(dbCmd, "@DATE_OF_DEPARTURE", DbType.DateTime, DBNull.Value);

				//if (tourTicketDetail.DateOfArrival != DateTime.MinValue)
				//    db.AddInParameter(dbCmd, "@DATE_OF_ARRIVAL", DbType.DateTime, tourTicketDetail.DateOfArrival);
				//else
				//    db.AddInParameter(dbCmd, "@DATE_OF_ARRIVAL", DbType.DateTime, DBNull.Value);

				//if (tourTicketDetail.PlaceFrom != 0)
				//    db.AddInParameter(dbCmd, "@FROM_PLACE", DbType.Int32, tourTicketDetail.PlaceFrom);
				//else
				//    db.AddInParameter(dbCmd, "@FROM_PLACE", DbType.Int32, DBNull.Value);

				//if (tourTicketDetail.PlaceTo != 0)
				//    db.AddInParameter(dbCmd, "@TO_PLACE", DbType.Int32, tourTicketDetail.PlaceTo);
				//else
				//    db.AddInParameter(dbCmd, "@TO_PLACE", DbType.Int32, DBNull.Value);

				//if (tourTicketDetail.AirLineId != 0)
				//    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, tourTicketDetail.AirLineId);
				//else
				//    db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, DBNull.Value);

				//if (tourTicketDetail.ClassId != 0)
				//    db.AddInParameter(dbCmd, "@CLASS_ID", DbType.Int32, tourTicketDetail.ClassId);
				//else
				//    db.AddInParameter(dbCmd, "@CLASS_ID", DbType.Int32, DBNull.Value);

				//if (!String.IsNullOrEmpty(tourTicketDetail.FlightFlyNo))
				//    db.AddInParameter(dbCmd, "@FLIGHT_FLY_NO", DbType.String, tourTicketDetail.FlightFlyNo);
				//else
				//    db.AddInParameter(dbCmd, "@FLIGHT_FLY_NO", DbType.String, DBNull.Value);

				//if (!String.IsNullOrEmpty(tourTicketDetail.FrequentFlyNo))
				//    db.AddInParameter(dbCmd, "@FREQUENT_FLY_NO", DbType.String, tourTicketDetail.FrequentFlyNo);
				//else
				//    db.AddInParameter(dbCmd, "@FREQUENT_FLY_NO", DbType.String, DBNull.Value);

				//if (tourTicketDetail.MarginAmount != 0)
				//    db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, tourTicketDetail.MarginAmount);
				//else
				//    db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, DBNull.Value);

				//if (tourTicketDetail.FareAmount != 0)
				//    db.AddInParameter(dbCmd, "@FAIR_AMOUNT", DbType.Decimal, tourTicketDetail.FareAmount);
				//else
				//    db.AddInParameter(dbCmd, "@FAIR_AMOUNT", DbType.Decimal, DBNull.Value); 
				#endregion
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

		public int InsertTourTicketBookingDetialIndividual(TourTicketDetailBDto tourTicketDetail, ref int ticketSrNo)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_TICKET_BOOKING_DETAILS_INSERT1");

				if (tourTicketDetail.TicketBookingHeader.TicketTypeId != 0)
					db.AddInParameter(dbCmd, "@TICKET_TYPE_ID", DbType.Int32, tourTicketDetail.TicketBookingHeader.TicketTypeId);
				else
					db.AddInParameter(dbCmd, "@TICKET_TYPE_ID", DbType.Int32, DBNull.Value);

				if (tourTicketDetail.TicketBookingHeader.TravelModeId != 0)
					db.AddInParameter(dbCmd, "@MODE_OF_TRAVEL", DbType.Int32, tourTicketDetail.TicketBookingHeader.TravelModeId);
				else
					db.AddInParameter(dbCmd, "@MODE_OF_TRAVEL", DbType.Int32, DBNull.Value);

				if (tourTicketDetail.TicketBookingHeader.InquiryId != 0)
					db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, tourTicketDetail.TicketBookingHeader.InquiryId);
				else
					db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, DBNull.Value);


				if (tourTicketDetail.TourBookingId != 0)
					db.AddInParameter(dbCmd, "@TOUR_BOOKING_ID", DbType.Int32, tourTicketDetail.TourBookingId);
				else
					db.AddInParameter(dbCmd, "@TOUR_BOOKING_ID", DbType.Int32, DBNull.Value);

				if (tourTicketDetail.TourBookingSerialNo != 0)
					db.AddInParameter(dbCmd, "@TOUR_BOOKING_SRNO", DbType.Int32, tourTicketDetail.TourBookingSerialNo);
				else
					db.AddInParameter(dbCmd, "@TOUR_BOOKING_SRNO", DbType.Int32, DBNull.Value);

				if (tourTicketDetail.TitleId != 0)
					db.AddInParameter(dbCmd, "@TITLE_ID", DbType.Int32, tourTicketDetail.TitleId);
				else
					db.AddInParameter(dbCmd, "@TITLE_ID", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(tourTicketDetail.LastName))
					db.AddInParameter(dbCmd, "@SURNAME", DbType.String, tourTicketDetail.LastName);
				else
					db.AddInParameter(dbCmd, "@SURNAME", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourTicketDetail.FirstName))
					db.AddInParameter(dbCmd, "@NAME", DbType.String, tourTicketDetail.FirstName);
				else
					db.AddInParameter(dbCmd, "@NAME", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourTicketDetail.PnrNo))
					db.AddInParameter(dbCmd, "@PNR_NO", DbType.String, tourTicketDetail.PnrNo);
				else
					db.AddInParameter(dbCmd, "@PNR_NO", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourTicketDetail.TicketNo))
					db.AddInParameter(dbCmd, "@TICKET_NO", DbType.String, tourTicketDetail.TicketNo);
				else
					db.AddInParameter(dbCmd, "@TICKET_NO", DbType.String, DBNull.Value);

				if (tourTicketDetail.DateOfDeparture != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@DATE_OF_DEPARTURE", DbType.DateTime, tourTicketDetail.DateOfDeparture);
				else
					db.AddInParameter(dbCmd, "@DATE_OF_DEPARTURE", DbType.DateTime, DBNull.Value);

				if (tourTicketDetail.DateOfArrival != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@DATE_OF_ARRIVAL", DbType.DateTime, tourTicketDetail.DateOfArrival);
				else
					db.AddInParameter(dbCmd, "@DATE_OF_ARRIVAL", DbType.DateTime, DBNull.Value);

				if (tourTicketDetail.PlaceFrom != 0)
					db.AddInParameter(dbCmd, "@FROM_PLACE", DbType.Int32, tourTicketDetail.PlaceFrom);
				else
					db.AddInParameter(dbCmd, "@FROM_PLACE", DbType.Int32, DBNull.Value);

				if (tourTicketDetail.PlaceTo != 0)
					db.AddInParameter(dbCmd, "@TO_PLACE", DbType.Int32, tourTicketDetail.PlaceTo);
				else
					db.AddInParameter(dbCmd, "@TO_PLACE", DbType.Int32, DBNull.Value);

				if (tourTicketDetail.AirLineId != 0)
					db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, tourTicketDetail.AirLineId);
				else
					db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, DBNull.Value);

				if (tourTicketDetail.ClassId != 0)
					db.AddInParameter(dbCmd, "@CLASS_ID", DbType.Int32, tourTicketDetail.ClassId);
				else
					db.AddInParameter(dbCmd, "@CLASS_ID", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(tourTicketDetail.FlightFlyNo))
					db.AddInParameter(dbCmd, "@FLIGHT_FLY_NO", DbType.String, tourTicketDetail.FlightFlyNo);
				else
					db.AddInParameter(dbCmd, "@FLIGHT_FLY_NO", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourTicketDetail.FrequentFlyNo))
					db.AddInParameter(dbCmd, "@FREQUENT_FLY_NO", DbType.String, tourTicketDetail.FrequentFlyNo);
				else
					db.AddInParameter(dbCmd, "@FREQUENT_FLY_NO", DbType.String, DBNull.Value);

				if (tourTicketDetail.MarginAmount != 0)
					db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, tourTicketDetail.MarginAmount);
				else
					db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.Decimal, DBNull.Value);

				if (tourTicketDetail.FareAmount != 0)
					db.AddInParameter(dbCmd, "@FAIR_AMOUNT", DbType.Decimal, tourTicketDetail.FareAmount);
				else
					db.AddInParameter(dbCmd, "@FAIR_AMOUNT", DbType.Decimal, DBNull.Value);

				db.AddInParameter(dbCmd, "@IS_E_TICKET", DbType.Boolean, tourTicketDetail.IsETicket);

				db.AddOutParameter(dbCmd, "@SR_NO", DbType.Int32, 9);

				Result = db.ExecuteNonQuery(dbCmd);
				ticketSrNo = Convert.ToInt32(db.GetParameterValue(dbCmd, "@SR_NO"));
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

		public int UpdateTourTicketBookingDetial(TourTicketDetailBDto tourTicketDetail)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_TICKET_BOOKING_DETAILS_UPDATE");

				if (!String.IsNullOrEmpty(tourTicketDetail.xmlString))
					db.AddInParameter(dbCmd, "@XML_DATA", DbType.String, tourTicketDetail.xmlString);
				else
					db.AddInParameter(dbCmd, "@XML_DATA", DbType.String, DBNull.Value);
				db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);

				Result = db.ExecuteNonQuery(dbCmd);
				Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
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

		public int DeleteTourTicketBookingDetial(int serialNo)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_DETAILS_DELETE);

				db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, serialNo);
				db.AddOutParameter(dbCmd, "@ERRORCODE", DbType.Int32, 8);
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



		public int InsertTourBookingAirline(TourTicketDetailBDto ticketBookingHdr)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_AIRLINE_DETAILS_INSERT");

				if (ticketBookingHdr.TourBookingId != 0)
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, ticketBookingHdr.TourBookingId);
				else
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.ModeOfTravel != 0)
					db.AddInParameter(dbCmd, "@TRAVEL_MODE", DbType.Int32, ticketBookingHdr.ModeOfTravel);
				else
					db.AddInParameter(dbCmd, "@TRAVEL_MODE", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.CustRelSrNo != 0)
					db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, ticketBookingHdr.CustRelSrNo);
				else
					db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.BranchId != 0)
					db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, ticketBookingHdr.BranchId);
				else
					db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.BookingRequestTo != 0)
					db.AddInParameter(dbCmd, "@BOOKING_REQUEST_TO", DbType.Int32, ticketBookingHdr.BookingRequestTo);
				else
					db.AddInParameter(dbCmd, "@BOOKING_REQUEST_TO", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.ClassId != 0)
					db.AddInParameter(dbCmd, "@CLASS_ID", DbType.Int32, ticketBookingHdr.ClassId);
				else
					db.AddInParameter(dbCmd, "@CLASS_ID", DbType.Int32, DBNull.Value);
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
		public int UpdateTourBookingAirline(TourTicketDetailBDto ticketBookingHdr)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_AIRLINE_DETAILS_UPDATE");



				if (ticketBookingHdr.TourBookingId != 0)
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, ticketBookingHdr.TourBookingId);
				else
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.BranchId != 0)
					db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, ticketBookingHdr.BranchId);
				else
					db.AddInParameter(dbCmd, "@BRANCH_ID", DbType.Int32, DBNull.Value);

				if (ticketBookingHdr.BookingRequestTo != 0)
					db.AddInParameter(dbCmd, "@BOOKING_REQUEST_TO", DbType.Int32, ticketBookingHdr.BookingRequestTo);
				else
					db.AddInParameter(dbCmd, "@BOOKING_REQUEST_TO", DbType.Int32, DBNull.Value);



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
		public DataTable GetTourBookingAirlineDetial(int bookingId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_AIRLINE_DETAILS_SELECT");
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);

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
		public DataTable GetTourBookingAirlineDetialById(int srNo)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_AIRLINE_DETAILS_SELECT_BYID");
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

		#region Payment Detail
		public DataSet GetTourBookingPaymentDetail(int bookingId, int productId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			//DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_BOOKING_PAYMENT_DETAILS");
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
				db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, productId);
				ds = db.ExecuteDataSet(dbCmd);
				//if (ds != null && ds.Tables.Count > 0)
				//{
				//    dt = ds.Tables[0];
				//}
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

		public DataTable GetTourBookingInrPaymentDetail(int bookingId, int productId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_BOOKING_PAYMENT_DETAILS_INR");
				db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, productId);
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
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

		public DataTable GetTourBookingInrPaymentDetail(int bookingId, int productId, int serialNo)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_BOOKING_PAYMENT_DETAILS_INR_SELECT_BY_ID");
				db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, productId);
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
				db.AddInParameter(dbCmd, "@PAYMENT_SRNO", DbType.Int32, serialNo);
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

		public DataTable GetTourBookingForeignPaymentDetail(int bookingId, int productId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_BOOKING_PAYMENT_DETAILS_FOREIGN");
				db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, productId);
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
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

		public DataTable GetTourBookingForeignPaymentDetail(int bookingId, int productId, int serialNo)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_BOOKING_PAYMENT_DETAILS_FOREIGN_SELECT_BY_ID");
				db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, productId);
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
				db.AddInParameter(dbCmd, "@PAYMENT_SRNO", DbType.Int32, serialNo);
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

		public int InsertBookingPayment(TourBookingPaymentBDto tourBookingPayment)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_BOOKING_PAYMENT_DETAILS_INSERT");

				if (tourBookingPayment.Amount != 0)
					db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, tourBookingPayment.Amount);
				else
					db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, DBNull.Value);

				db.AddInParameter(dbCmd, "@TOKEN_AMOUNT", DbType.Decimal, tourBookingPayment.TokenAmount);

				if (tourBookingPayment.BookingId != 0)
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, tourBookingPayment.BookingId);
				else
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, DBNull.Value);

				if (tourBookingPayment.BankId != 0)
					db.AddInParameter(dbCmd, "@BANK_ID", DbType.Int32, tourBookingPayment.BankId);
				else
					db.AddInParameter(dbCmd, "@BANK_ID", DbType.Int32, DBNull.Value);

				if (tourBookingPayment.CurrencyAgentId != 0)
					db.AddInParameter(dbCmd, "@CURRENCY_AGENT_ID", DbType.Int32, tourBookingPayment.CurrencyAgentId);
				else
					db.AddInParameter(dbCmd, "@CURRENCY_AGENT_ID", DbType.Int32, DBNull.Value);

				if (tourBookingPayment.CurrencyId != 0)
					db.AddInParameter(dbCmd, "@PAYMENT_CURRENCY_ID", DbType.Int32, tourBookingPayment.CurrencyId);
				else
					db.AddInParameter(dbCmd, "@PAYMENT_CURRENCY_ID", DbType.Int32, DBNull.Value);

				if (tourBookingPayment.PaymentModeId != 0)
					db.AddInParameter(dbCmd, "@PAYMENT_MODE_ID", DbType.Int32, tourBookingPayment.PaymentModeId);
				else
					db.AddInParameter(dbCmd, "@PAYMENT_MODE_ID", DbType.Int32, DBNull.Value);

				if (tourBookingPayment.ProductId != 0)
					db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, tourBookingPayment.ProductId);
				else
					db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, DBNull.Value);

				if (tourBookingPayment.ReceiveBy != 0)
					db.AddInParameter(dbCmd, "@RECEIVED_BY", DbType.Int32, tourBookingPayment.ReceiveBy);
				else
					db.AddInParameter(dbCmd, "@RECEIVED_BY", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingPayment.BankBranchName))
					db.AddInParameter(dbCmd, "@BRANCH_NAME", DbType.String, tourBookingPayment.BankBranchName);
				else
					db.AddInParameter(dbCmd, "@BRANCH_NAME", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingPayment.RecChqDdNo))
					db.AddInParameter(dbCmd, "@REC_CHEQUE_DD_NO", DbType.String, tourBookingPayment.RecChqDdNo);
				else
					db.AddInParameter(dbCmd, "@REC_CHEQUE_DD_NO", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingPayment.ReceiptNo))
					db.AddInParameter(dbCmd, "@RECEIPT_NO", DbType.String, tourBookingPayment.ReceiptNo);
				else
					db.AddInParameter(dbCmd, "@RECEIPT_NO", DbType.String, DBNull.Value);

				if (tourBookingPayment.PaymentDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@PAYMRNT_DATE", DbType.DateTime, tourBookingPayment.PaymentDate);
				else
					db.AddInParameter(dbCmd, "@PAYMRNT_DATE", DbType.DateTime, DBNull.Value);

				if (tourBookingPayment.ReceiptDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@RECEIPT_DATE", DbType.DateTime, tourBookingPayment.ReceiptDate);
				else
					db.AddInParameter(dbCmd, "@RECEIPT_DATE", DbType.DateTime, DBNull.Value);



				// Added By Jagadish

				if (!String.IsNullOrEmpty(tourBookingPayment.InTheNameOf))
					db.AddInParameter(dbCmd, "@IN_THE_NAME_OF", DbType.String, tourBookingPayment.InTheNameOf);
				else
					db.AddInParameter(dbCmd, "@IN_THE_NAME_OF", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingPayment.BillNo))
					db.AddInParameter(dbCmd, "@BILL_NUMBER", DbType.String, tourBookingPayment.BillNo);
				else
					db.AddInParameter(dbCmd, "@BILL_NUMBER", DbType.String, DBNull.Value);
				if (tourBookingPayment.InrAmount != 0)
					db.AddInParameter(dbCmd, "@INR_AMOUNT", DbType.Decimal, tourBookingPayment.InrAmount);
				else
					db.AddInParameter(dbCmd, "@INR_AMOUNT", DbType.Decimal, DBNull.Value);
				if (tourBookingPayment.Tax != 0)
					db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, tourBookingPayment.Tax);
				else
					db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, DBNull.Value);
				if (tourBookingPayment.ConversionRate != 0)
					db.AddInParameter(dbCmd, "@CONVERSION_RATE", DbType.Decimal, tourBookingPayment.ConversionRate);
				else
					db.AddInParameter(dbCmd, "@CONVERSION_RATE", DbType.Decimal, DBNull.Value);
				if (tourBookingPayment.SentMode != 0)
					db.AddInParameter(dbCmd, "@SENT_MODE", DbType.Int32, tourBookingPayment.SentMode);
				else
					db.AddInParameter(dbCmd, "@SENT_MODE", DbType.Int32, DBNull.Value);














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

		public int UpdateBookingPayment(TourBookingPaymentBDto tourBookingPayment)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_BOOKING_PAYMENT_DETAILS_UPDATE");

				if (tourBookingPayment.Amount != 0)
					db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, tourBookingPayment.Amount);
				else
					db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal, DBNull.Value);

				if (tourBookingPayment.BookingId != 0)
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, tourBookingPayment.BookingId);
				else
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, DBNull.Value);

				if (tourBookingPayment.BankId != 0)
					db.AddInParameter(dbCmd, "@BANK_ID", DbType.Int32, tourBookingPayment.BankId);
				else
					db.AddInParameter(dbCmd, "@BANK_ID", DbType.Int32, DBNull.Value);

				if (tourBookingPayment.CurrencyAgentId != 0)
					db.AddInParameter(dbCmd, "@CURRENCY_AGENT_ID", DbType.Int32, tourBookingPayment.CurrencyAgentId);
				else
					db.AddInParameter(dbCmd, "@CURRENCY_AGENT_ID", DbType.Int32, DBNull.Value);

				if (tourBookingPayment.CurrencyId != 0)
					db.AddInParameter(dbCmd, "@PAYMENT_CURRENCY_ID", DbType.Int32, tourBookingPayment.CurrencyId);
				else
					db.AddInParameter(dbCmd, "@PAYMENT_CURRENCY_ID", DbType.Int32, DBNull.Value);

				if (tourBookingPayment.PaymentModeId != 0)
					db.AddInParameter(dbCmd, "@PAYMENT_MODE_ID", DbType.Int32, tourBookingPayment.PaymentModeId);
				else
					db.AddInParameter(dbCmd, "@PAYMENT_MODE_ID", DbType.Int32, DBNull.Value);

				if (tourBookingPayment.ProductId != 0)
					db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, tourBookingPayment.ProductId);
				else
					db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, DBNull.Value);

				if (tourBookingPayment.ReceiveBy != 0)
					db.AddInParameter(dbCmd, "@RECEIVED_BY", DbType.Int32, tourBookingPayment.ReceiveBy);
				else
					db.AddInParameter(dbCmd, "@RECEIVED_BY", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingPayment.BankBranchName))
					db.AddInParameter(dbCmd, "@BRANCH_NAME", DbType.String, tourBookingPayment.BankBranchName);
				else
					db.AddInParameter(dbCmd, "@BRANCH_NAME", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingPayment.RecChqDdNo))
					db.AddInParameter(dbCmd, "@REC_CHEQUE_DD_NO", DbType.String, tourBookingPayment.RecChqDdNo);
				else
					db.AddInParameter(dbCmd, "@REC_CHEQUE_DD_NO", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingPayment.ReceiptNo))
					db.AddInParameter(dbCmd, "@RECEIPT_NO", DbType.String, tourBookingPayment.ReceiptNo);
				else
					db.AddInParameter(dbCmd, "@RECEIPT_NO", DbType.String, DBNull.Value);

				if (tourBookingPayment.PaymentDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@PAYMRNT_DATE", DbType.DateTime, tourBookingPayment.PaymentDate);
				else
					db.AddInParameter(dbCmd, "@PAYMRNT_DATE", DbType.DateTime, DBNull.Value);

				if (tourBookingPayment.ReceiptDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@RECEIPT_DATE", DbType.DateTime, tourBookingPayment.ReceiptDate);
				else
					db.AddInParameter(dbCmd, "@RECEIPT_DATE", DbType.DateTime, DBNull.Value);

				if (tourBookingPayment.SerialNo != 0)
					db.AddInParameter(dbCmd, "@PAYMENT_SRNO", DbType.Int32, tourBookingPayment.SerialNo);
				else
					db.AddInParameter(dbCmd, "@PAYMENT_SRNO", DbType.Int32, DBNull.Value);



				// Added By Jagadish

				if (!String.IsNullOrEmpty(tourBookingPayment.InTheNameOf))
					db.AddInParameter(dbCmd, "@IN_THE_NAME_OF", DbType.String, tourBookingPayment.InTheNameOf);
				else
					db.AddInParameter(dbCmd, "@IN_THE_NAME_OF", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingPayment.BillNo))
					db.AddInParameter(dbCmd, "@BILL_NUMBER", DbType.String, tourBookingPayment.BillNo);
				else
					db.AddInParameter(dbCmd, "@BILL_NUMBER", DbType.String, DBNull.Value);
				if (tourBookingPayment.InrAmount != 0)
					db.AddInParameter(dbCmd, "@INR_AMOUNT", DbType.Decimal, tourBookingPayment.InrAmount);
				else
					db.AddInParameter(dbCmd, "@INR_AMOUNT", DbType.Decimal, DBNull.Value);
				if (tourBookingPayment.Tax != 0)
					db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, tourBookingPayment.Tax);
				else
					db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, DBNull.Value);
				if (tourBookingPayment.ConversionRate != 0)
					db.AddInParameter(dbCmd, "@CONVERSION_RATE", DbType.Decimal, tourBookingPayment.ConversionRate);
				else
					db.AddInParameter(dbCmd, "@CONVERSION_RATE", DbType.Decimal, DBNull.Value);
				if (tourBookingPayment.SentMode != 0)
					db.AddInParameter(dbCmd, "@SENT_MODE", DbType.Int32, tourBookingPayment.SentMode);
				else
					db.AddInParameter(dbCmd, "@SENT_MODE", DbType.Int32, DBNull.Value);



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


        public DataTable GetTourBookingFormPrint(int bookingId, int productId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_FORM_PRINT");
                db.AddInParameter(dbCmd, "@PRODUCT_ID", DbType.Int32, productId);
                db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
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

        public DataTable GetTourBookingFormPrintAddress(int bookingId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_FORM_PRINT_Address");
                db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
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



		#region Additional Detail
		public int InsertBookingAdditionalDetails(int bookingId, string additonalRequestServices, string paymentMadeBy, string documentHandedByBy)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_ADDITIONAL_DETAILS_INSERT);

				if (bookingId != 0)
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
				else
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(additonalRequestServices))
					db.AddInParameter(dbCmd, "@ADDITIONAL_REQUEST_SERVICE", DbType.String, additonalRequestServices.Trim());
				else
					db.AddInParameter(dbCmd, "@ADDITIONAL_REQUEST_SERVICE", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(paymentMadeBy))
					db.AddInParameter(dbCmd, "@PAYMENT_FOR_BALANCE_AMT_MADE_BY", DbType.String, paymentMadeBy.Trim());
				else
					db.AddInParameter(dbCmd, "@PAYMENT_FOR_BALANCE_AMT_MADE_BY", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(documentHandedByBy))
					db.AddInParameter(dbCmd, "@DOC_TO_PROCESS_VISA_FORM_TO_BE_HANDED_BY", DbType.String, documentHandedByBy.Trim());
				else
					db.AddInParameter(dbCmd, "@DOC_TO_PROCESS_VISA_FORM_TO_BE_HANDED_BY", DbType.String, DBNull.Value);

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

		public int UpdateBookingAdditionalDetails(int bookingId, string additonalRequestServices, string paymentMadeBy, string documentHandedByBy)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_ADDITIONAL_DETAILS_UPDATE);

				if (bookingId != 0)
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
				else
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, DBNull.Value);

				if (!String.IsNullOrEmpty(additonalRequestServices))
					db.AddInParameter(dbCmd, "@ADDITIONAL_REQUEST_SERVICE", DbType.String, additonalRequestServices.Trim());
				else
					db.AddInParameter(dbCmd, "@ADDITIONAL_REQUEST_SERVICE", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(paymentMadeBy))
					db.AddInParameter(dbCmd, "@PAYMENT_FOR_BALANCE_AMT_MADE_BY", DbType.String, paymentMadeBy.Trim());
				else
					db.AddInParameter(dbCmd, "@PAYMENT_FOR_BALANCE_AMT_MADE_BY", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(documentHandedByBy))
					db.AddInParameter(dbCmd, "@DOC_TO_PROCESS_VISA_FORM_TO_BE_HANDED_BY", DbType.String, documentHandedByBy.Trim());
				else
					db.AddInParameter(dbCmd, "@DOC_TO_PROCESS_VISA_FORM_TO_BE_HANDED_BY", DbType.String, DBNull.Value);

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

		public DataTable GetBookingAdditionalDetails(int bookingId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_ADDITIONAL_DETAILS_SELECT);
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
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

		#region Operation Detail
		public int InsertBookingOperationDetail(OperationDetailBDto operationDetailBDto, ref int isExists)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_OPERATION_DETAILS_INSERT);

				if (operationDetailBDto.BookingId != 0)
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, operationDetailBDto.BookingId);
				else
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, DBNull.Value);

				if (operationDetailBDto.SerialNo != 0)
					db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, operationDetailBDto.SerialNo);
				else
					db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, DBNull.Value);

				if (operationDetailBDto.TourId != 0)
					db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, operationDetailBDto.TourId);
				else
					db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, DBNull.Value);

				if (operationDetailBDto.TourTypeId != 0)
					db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, operationDetailBDto.TourTypeId);
				else
					db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, DBNull.Value);

				if (operationDetailBDto.PassportTakenDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@PASSPORT_TAKEN_DATE", DbType.DateTime, operationDetailBDto.PassportTakenDate);
				else
					db.AddInParameter(dbCmd, "@PASSPORT_TAKEN_DATE", DbType.DateTime, DBNull.Value);

				if (operationDetailBDto.PhotographTakenDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@PHOTOGRAPHS_TAKEN_DATE", DbType.DateTime, operationDetailBDto.PhotographTakenDate);
				else
					db.AddInParameter(dbCmd, "@PHOTOGRAPHS_TAKEN_DATE", DbType.DateTime, DBNull.Value);

				if (operationDetailBDto.DocumentsTakenDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@DOCUMENTS_TAKEN_DATE", DbType.DateTime, operationDetailBDto.DocumentsTakenDate);
				else
					db.AddInParameter(dbCmd, "@DOCUMENTS_TAKEN_DATE", DbType.DateTime, DBNull.Value);

				if (operationDetailBDto.VisaSentDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@SEND_VISA_DATE", DbType.DateTime, operationDetailBDto.VisaSentDate);
				else
					db.AddInParameter(dbCmd, "@SEND_VISA_DATE", DbType.DateTime, DBNull.Value);

				if (operationDetailBDto.PassportDeliveredBackOnDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@PASSPORT_DELIVERED_BACK_ON_DATE", DbType.DateTime, operationDetailBDto.PassportDeliveredBackOnDate);
				else
					db.AddInParameter(dbCmd, "@PASSPORT_DELIVERED_BACK_ON_DATE", DbType.DateTime, DBNull.Value);

				if (operationDetailBDto.VouchuersGivenDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@VOUCHUERS_GIVEN_DATE", DbType.DateTime, operationDetailBDto.VouchuersGivenDate);
				else
					db.AddInParameter(dbCmd, "@VOUCHUERS_GIVEN_DATE", DbType.DateTime, DBNull.Value);

				if (operationDetailBDto.ItenaryGivenDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@ITENARY_GIVEN_DATE", DbType.DateTime, operationDetailBDto.ItenaryGivenDate);
				else
					db.AddInParameter(dbCmd, "@ITENARY_GIVEN_DATE", DbType.DateTime, DBNull.Value);

				db.AddInParameter(dbCmd, "@VISAS_CHECKED", DbType.Boolean, operationDetailBDto.IsVisaChecked);


				db.AddOutParameter(dbCmd, "@IS_EXISTS", DbType.Int32, 10);

				Result = db.ExecuteNonQuery(dbCmd);
				isExists = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_EXISTS"));
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

		public int UpdateBookingOperationDetail(OperationDetailBDto operationDetailBDto)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_OPERATION_DETAILS_UPDATE);

				if (operationDetailBDto.SerialNo != 0)
					db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, operationDetailBDto.SerialNo);
				else
					db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, DBNull.Value);

				if (operationDetailBDto.BookingId != 0)
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, operationDetailBDto.BookingId);
				else
					db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, DBNull.Value);

				if (operationDetailBDto.TourId != 0)
					db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, operationDetailBDto.TourId);
				else
					db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, DBNull.Value);

				if (operationDetailBDto.TourTypeId != 0)
					db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, operationDetailBDto.TourTypeId);
				else
					db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, DBNull.Value);

				if (operationDetailBDto.PassportTakenDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@PASSPORT_TAKEN_DATE", DbType.DateTime, operationDetailBDto.PassportTakenDate);
				else
					db.AddInParameter(dbCmd, "@PASSPORT_TAKEN_DATE", DbType.DateTime, DBNull.Value);

				if (operationDetailBDto.PhotographTakenDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@PHOTOGRAPHS_TAKEN_DATE", DbType.DateTime, operationDetailBDto.PhotographTakenDate);
				else
					db.AddInParameter(dbCmd, "@PHOTOGRAPHS_TAKEN_DATE", DbType.DateTime, DBNull.Value);

				if (operationDetailBDto.DocumentsTakenDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@DOCUMENTS_TAKEN_DATE", DbType.DateTime, operationDetailBDto.DocumentsTakenDate);
				else
					db.AddInParameter(dbCmd, "@DOCUMENTS_TAKEN_DATE", DbType.DateTime, DBNull.Value);

				if (operationDetailBDto.VisaSentDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@SEND_VISA_DATE", DbType.DateTime, operationDetailBDto.VisaSentDate);
				else
					db.AddInParameter(dbCmd, "@SEND_VISA_DATE", DbType.DateTime, DBNull.Value);

				if (operationDetailBDto.PassportDeliveredBackOnDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@PASSPORT_DELIVERED_BACK_ON_DATE", DbType.DateTime, operationDetailBDto.PassportDeliveredBackOnDate);
				else
					db.AddInParameter(dbCmd, "@PASSPORT_DELIVERED_BACK_ON_DATE", DbType.DateTime, DBNull.Value);

				if (operationDetailBDto.VouchuersGivenDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@VOUCHUERS_GIVEN_DATE", DbType.DateTime, operationDetailBDto.VouchuersGivenDate);
				else
					db.AddInParameter(dbCmd, "@VOUCHUERS_GIVEN_DATE", DbType.DateTime, DBNull.Value);

				if (operationDetailBDto.ItenaryGivenDate != DateTime.MinValue)
					db.AddInParameter(dbCmd, "@ITENARY_GIVEN_DATE", DbType.DateTime, operationDetailBDto.ItenaryGivenDate);
				else
					db.AddInParameter(dbCmd, "@ITENARY_GIVEN_DATE", DbType.DateTime, DBNull.Value);

				db.AddInParameter(dbCmd, "@VISAS_CHECKED", DbType.Boolean, operationDetailBDto.IsVisaChecked);

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

		public DataSet GetBookingOperationDetail(int bookingId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			//DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_OPERATION_DETAILS_SELECT);
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);

				//if (tourId != 0)
				//    db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
				//else
				//    db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, DBNull.Value);

				//if (tourTypeId != 0)
				//    db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, tourTypeId);
				//else
				//    db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, DBNull.Value);

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

		public int DeleteBookingOperationDetail(int bookingId, int serialNo, int tourId, int tourTypeId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_OPERATION_DETAILS_DELETE");
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
				db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, serialNo);
				db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
				db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, tourTypeId);
				db.AddOutParameter(dbCmd, "@ERRORCODE", DbType.Int32, 8);

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

		#region Checklist Details
		public DataTable GetBookingChecklist(int bookingId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_CHECKLIST_SELECT);
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);

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



		public DataSet GetBookingOutputChecklist(int bookingId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("BOOKING_TOUR_BOOKING_OUTPUT_CHECKLIST_SELECT");
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
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



		public int InsertUpdateBookingChecklist(TourBookingChecklistBDto bookingChecklist)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_TOUR_BOOKING_CHECKLIST_INSERT_UPDATE);

				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingChecklist.BookingId);
				db.AddInParameter(dbCmd, "@CHECKLIST_ID", DbType.Int32, bookingChecklist.CheckListId);
				db.AddInParameter(dbCmd, "@CHECKLIST_SRNO", DbType.Int32, bookingChecklist.SerialNo);

				if (!string.IsNullOrEmpty(bookingChecklist.Remarks))
					db.AddInParameter(dbCmd, "@REMARKS", DbType.String, bookingChecklist.Remarks);
				else
					db.AddInParameter(dbCmd, "@REMARKS", DbType.String, DBNull.Value);

				db.AddInParameter(dbCmd, "@ANSWER_YES_OR_NO", DbType.Boolean, bookingChecklist.Answer);

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

		#region Output Details
		public DataTable GetBookingOutputData(int tourId, string columnList)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_BOOKING_OUTPUT_DATA_SELECT");
				db.AddInParameter(dbCmd, "@TOUR_ID", DbType.String, tourId);
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


		public DataTable GetBookingOutputDocumentData(int tourId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TOUR_BROWSE_DOCUMENT_NAME_SELECT");
				db.AddInParameter(dbCmd, "@TOUR_ID", DbType.String, tourId);
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



		public DataTable GetDocumentData(int tourId, string ContentType)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_CUSTOMER_EMAIL_DOCUMENT_SELECT");
				db.AddInParameter(dbCmd, "@TOUR_ID", DbType.String, tourId);
				db.AddInParameter(dbCmd, "@CONTENT_TYPE", DbType.String, ContentType);

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

		public int UploadBookingAmendmentForm(TourBookingHrdBDto tourBookingHrd)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_BOOKING_HRD_UPLOAD_AMENDMENT_FORM");

				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, tourBookingHrd.BookingId);

				if (tourBookingHrd.TourTypeId != 0)
					db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, tourBookingHrd.TourTypeId);
				else
					db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.TourId != 0)
					db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourBookingHrd.TourId);
				else
					db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, DBNull.Value);

				if (tourBookingHrd.AmendmentForm != null)
					db.AddInParameter(dbCmd, "@AMENDMENT_FORM", DbType.Binary, tourBookingHrd.AmendmentForm);
				else
					db.AddInParameter(dbCmd, "@AMENDMENT_FORM", DbType.Binary, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.AmendmentFormContentType))
					db.AddInParameter(dbCmd, "@AMENDMENT_FORM_CONTENT_TYPE", DbType.String, tourBookingHrd.AmendmentFormContentType);
				else
					db.AddInParameter(dbCmd, "@AMENDMENT_FORM_CONTENT_TYPE", DbType.String, DBNull.Value);

				if (!String.IsNullOrEmpty(tourBookingHrd.AmendmentFormFileName))
					db.AddInParameter(dbCmd, "@AMENDMENT_FORM_FILE_NAME", DbType.String, tourBookingHrd.AmendmentFormFileName);
				else
					db.AddInParameter(dbCmd, "@AMENDMENT_FORM_FILE_NAME", DbType.String, DBNull.Value);

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

		public DataTable GetBookingAmendmentForm(int bookingId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TOUR_BOOKING_HRD_DOWNLOAD_AMENDMENT_FORM");
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
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

		public int InsertOutputBookingChecklist(TourBookingChecklistBDto bookingChecklist)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("BOOKING_TOUR_BOOKING_OUTPUT_CHECKLIST_INSERT");


				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingChecklist.BookingId);
				db.AddInParameter(dbCmd, "@CHECKLIST_ID", DbType.String, bookingChecklist.CheckListId);
				db.AddInParameter(dbCmd, "@DOC_NAME", DbType.String, bookingChecklist.documentName);
				db.AddInParameter(dbCmd, "@DOC_CONTENT", DbType.String, bookingChecklist.DocumentContent);
				db.AddInParameter(dbCmd, "@CHECKLIST_SRNO", DbType.Int32, bookingChecklist.SerialNo);

				if (!string.IsNullOrEmpty(bookingChecklist.Remarks))
					db.AddInParameter(dbCmd, "@REMARKS", DbType.String, bookingChecklist.Remarks);
				else
					db.AddInParameter(dbCmd, "@REMARKS", DbType.String, DBNull.Value);

				db.AddInParameter(dbCmd, "@ANSWER_YES_OR_NO", DbType.Boolean, bookingChecklist.Answer);

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

		public int UpdateOutputBookingChecklist(TourBookingChecklistBDto bookingChecklist)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("BOOKING_TOUR_BOOKING_OUTPUT_CHECKLIST_UPDATE");

				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingChecklist.BookingId);
				db.AddInParameter(dbCmd, "@CHECKLIST_ID", DbType.String, bookingChecklist.CheckListId);
				db.AddInParameter(dbCmd, "@DOC_NAME", DbType.String, bookingChecklist.documentName);
				db.AddInParameter(dbCmd, "@DOC_CONTENT", DbType.String, bookingChecklist.DocumentContent);

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


		public DataTable GetOutputBookingCheckList(int bookingId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.BOOKING_TOUR_BOOKING_OUTPUT_CHECKLIST_SELECT");
				db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, bookingId);
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

		public int ChangeBookingStatus(int inquiryId, int CompititorId, decimal CompititorPrice, string remarks)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_INQUIRY");

				if (inquiryId != 0)
					db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, inquiryId);
				else
					db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, DBNull.Value);

				if (CompititorId != 0)
					db.AddInParameter(dbCmd, "@COMPITITOR_ID", DbType.Int32, CompititorId);
				else
					db.AddInParameter(dbCmd, "@COMPITITOR_ID", DbType.Int32, DBNull.Value);

				if (CompititorPrice != 0)
					db.AddInParameter(dbCmd, "@COMPITITOR_PRICE", DbType.Decimal, CompititorPrice);
				else
					db.AddInParameter(dbCmd, "@COMPITITOR_PRICE", DbType.Decimal, DBNull.Value);

				if (!string.IsNullOrEmpty(remarks))
					db.AddInParameter(dbCmd, "@REMARKS", DbType.String, remarks);
				else
					db.AddInParameter(dbCmd, "@REMARKS", DbType.String, DBNull.Value);

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

		public int UpdateCustomerEmailMobile(int CustID, int CustRelId, int CustRelsrno, string Email, string Mobile)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_CUSTOMER_MOBILE_EMAIL_UPDATE");
				db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, CustID);
				db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, CustRelId);
				db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.Int32, CustRelsrno);
				db.AddInParameter(dbCmd, "@CUST_REL_EMAIL", DbType.String, Email);
				db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, Mobile);

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

		public int SendCustomerEmail(string EmailTo, string EmailCC, string filename, string Subject, string Body)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_CUST_EMAIL_SENDMAIL");
				db.AddInParameter(dbCmd, "@EMAILTO", DbType.String, EmailTo);
				db.AddInParameter(dbCmd, "@EMAILCC", DbType.String, EmailCC);
				db.AddInParameter(dbCmd, "@pinfilename", DbType.String, filename);
				db.AddInParameter(dbCmd, "@emailSubject", DbType.String, Subject);
				db.AddInParameter(dbCmd, "@emailBody", DbType.String, Body);

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

        //public int InsertCustomerEmail(int userId, int CustId, int InquiryId, int followupNo, string EmailTo, string EmailCc, string EmailFrom, string Subject, byte[] Attachment1,
        //                                string Attachment1ContentType, string Attachment1FileName, byte[] Attachment2, string Attachment2ContentType,
        //                                string Attachment2Filename, string Attachment3Filename, string Attachment4Filename, string Attachment5Filename, string EmailBody)
        //{
        //    Database db = null;
        //    DbCommand dbCmd = null;
        //    int Result = 0;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
        //        dbCmd = db.GetStoredProcCommand("dbo.USP_CUSTOMER_EMAIL_INSERT");
        //        db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userId);
        //        db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, CustId);
        //        if (InquiryId != 0)
        //            db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, InquiryId);
        //        else
        //            db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, DBNull.Value);
        //        if (followupNo != 0)
        //            db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, followupNo);
        //        else
        //            db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, DBNull.Value);
        //        db.AddInParameter(dbCmd, "@EMAIL_TO", DbType.String, EmailTo);
        //        if (!string.IsNullOrEmpty(EmailCc))
        //            db.AddInParameter(dbCmd, "@EMAIL_CC", DbType.String, EmailCc);
        //        else
        //            db.AddInParameter(dbCmd, "@EMAIL_CC", DbType.String, DBNull.Value);
        //        if (!string.IsNullOrEmpty(EmailFrom))
        //            db.AddInParameter(dbCmd, "@EMAIL_FROM", DbType.String, EmailFrom);
        //        else
        //            db.AddInParameter(dbCmd, "@EMAIL_FROM", DbType.String, DBNull.Value);
        //        db.AddInParameter(dbCmd, "@SUBJECT", DbType.String, Subject);
        //        db.AddInParameter(dbCmd, "@ATTACHMENT1", DbType.Binary, Attachment1);
        //        if (!string.IsNullOrEmpty(Attachment1ContentType))
        //            db.AddInParameter(dbCmd, "@ATTACHMENT1_CONTENT_TYPE", DbType.String, Attachment1ContentType);
        //        else
        //            db.AddInParameter(dbCmd, "@ATTACHMENT1_CONTENT_TYPE", DbType.String, DBNull.Value);
        //        if (!string.IsNullOrEmpty(Attachment1FileName))
        //            db.AddInParameter(dbCmd, "@ATTACHMENT1_FILE_NAME", DbType.String, Attachment1FileName);
        //        else
        //            db.AddInParameter(dbCmd, "@ATTACHMENT1_FILE_NAME", DbType.String, DBNull.Value);
        //        db.AddInParameter(dbCmd, "@ATTACHMENT2", DbType.Binary, Attachment2);
        //        if (!string.IsNullOrEmpty(Attachment2ContentType))
        //            db.AddInParameter(dbCmd, "@ATTACHMENT2_CONTENT_TYPE", DbType.String, Attachment2ContentType);
        //        else
        //            db.AddInParameter(dbCmd, "@ATTACHMENT2_CONTENT_TYPE", DbType.String, DBNull.Value);
        //        if (!string.IsNullOrEmpty(Attachment2Filename))
        //            db.AddInParameter(dbCmd, "@ATTACHMENT2_FILE_NAME", DbType.String, Attachment2Filename);
        //        else
        //            db.AddInParameter(dbCmd, "@ATTACHMENT2_FILE_NAME", DbType.String, DBNull.Value);
				
        //        if (!string.IsNullOrEmpty(Attachment3Filename))
        //            db.AddInParameter(dbCmd, "@ATTACHMENT3_FILE_NAME", DbType.String, Attachment3Filename);
        //        else
        //            db.AddInParameter(dbCmd, "@ATTACHMENT3_FILE_NAME", DbType.String, DBNull.Value);
               
        //        if (!string.IsNullOrEmpty(Attachment4Filename))
        //            db.AddInParameter(dbCmd, "@ATTACHMENT4_FILE_NAME", DbType.String, Attachment4Filename);
        //        else
        //            db.AddInParameter(dbCmd, "@ATTACHMENT4_FILE_NAME", DbType.String, DBNull.Value);
               
        //        if (!string.IsNullOrEmpty(Attachment5Filename))
        //            db.AddInParameter(dbCmd, "@ATTACHMENT5_FILE_NAME", DbType.String, Attachment5Filename);
        //        else
        //            db.AddInParameter(dbCmd, "@ATTACHMENT5_FILE_NAME", DbType.String, DBNull.Value);

        //        db.AddInParameter(dbCmd, "@EMAIL_BODY", DbType.String, EmailBody);
        //        Result = db.ExecuteNonQuery(dbCmd);
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
        //    return Result;
        //}







        #region by sunil for emailinquiry
        public int InsertCustomerEmail1(int userId, int CustId, int InquiryId, int followupNo, string EmailTo, string EmailCc, string EmailFrom, string Subject, byte[] Attachment1,
                                    string Attachment1ContentType, string Attachment1FileName, byte[] Attachment2, string Attachment2ContentType,
                                    string Attachment2Filename,byte[] Attachment3, string Attachment3ContentType,
                                    string Attachment3Filename,byte[] Attachment4, string Attachment4ContentType,
                                    string Attachment4Filename,byte[] Attachment5, string Attachment5ContentType,
                                    string Attachment5Filename, string EmailBody)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_INQUIRY_EMAIL_ATTACHMENT_GET");
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, userId);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, CustId);
                if (InquiryId != 0)
                    db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, InquiryId);
                else
                    db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, DBNull.Value);
                if (followupNo != 0)
                    db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, followupNo);
                else
                    db.AddInParameter(dbCmd, "@FOLLOWUP_NO", DbType.Int32, DBNull.Value);
                db.AddInParameter(dbCmd, "@EMAIL_TO", DbType.String, EmailTo);
                if (!string.IsNullOrEmpty(EmailCc))
                    db.AddInParameter(dbCmd, "@EMAIL_CC", DbType.String, EmailCc);
                else
                    db.AddInParameter(dbCmd, "@EMAIL_CC", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(EmailFrom))
                    db.AddInParameter(dbCmd, "@EMAIL_FROM", DbType.String, EmailFrom);
                else
                    db.AddInParameter(dbCmd, "@EMAIL_FROM", DbType.String, DBNull.Value);
                db.AddInParameter(dbCmd, "@SUBJECT", DbType.String, Subject);

                db.AddInParameter(dbCmd, "@ATTACHMENT1", DbType.Binary, Attachment1);

                if (!string.IsNullOrEmpty(Attachment1ContentType))
                    db.AddInParameter(dbCmd, "@ATTACHMENT1_CONTENT_TYPE", DbType.String, Attachment1ContentType);
                else
                    db.AddInParameter(dbCmd, "@ATTACHMENT1_CONTENT_TYPE", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(Attachment1FileName))
                    db.AddInParameter(dbCmd, "@ATTACHMENT1_FILE_NAME", DbType.String, Attachment1FileName);
                else
                    db.AddInParameter(dbCmd, "@ATTACHMENT1_FILE_NAME", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@ATTACHMENT2", DbType.Binary, Attachment2);
                if (!string.IsNullOrEmpty(Attachment2ContentType))
                    db.AddInParameter(dbCmd, "@ATTACHMENT2_CONTENT_TYPE", DbType.String, Attachment2ContentType);
                else
                    db.AddInParameter(dbCmd, "@ATTACHMENT2_CONTENT_TYPE", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(Attachment2Filename))
                    db.AddInParameter(dbCmd, "@ATTACHMENT2_FILE_NAME", DbType.String, Attachment2Filename);
                else
                    db.AddInParameter(dbCmd, "@ATTACHMENT2_FILE_NAME", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@ATTACHMENT3", DbType.Binary, Attachment3);
                if (!string.IsNullOrEmpty(Attachment3ContentType))
                    db.AddInParameter(dbCmd, "@ATTACHMENT3_CONTENT_TYPE", DbType.String, Attachment3ContentType);
                else
                    db.AddInParameter(dbCmd, "@ATTACHMENT3_CONTENT_TYPE", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(Attachment3Filename))
                    db.AddInParameter(dbCmd, "@ATTACHMENT3_FILE_NAME", DbType.String, Attachment3Filename);
                else
                    db.AddInParameter(dbCmd, "@ATTACHMENT3_FILE_NAME", DbType.String, DBNull.Value);


                db.AddInParameter(dbCmd, "@ATTACHMENT4", DbType.Binary, Attachment4);
                if (!string.IsNullOrEmpty(Attachment4ContentType))
                    db.AddInParameter(dbCmd, "@ATTACHMENT4_CONTENT_TYPE", DbType.String, Attachment4ContentType);
                else
                    db.AddInParameter(dbCmd, "@ATTACHMENT4_CONTENT_TYPE", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(Attachment4Filename))
                    db.AddInParameter(dbCmd, "@ATTACHMENT4_FILE_NAME", DbType.String, Attachment4Filename);
                else
                    db.AddInParameter(dbCmd, "@ATTACHMENT4_FILE_NAME", DbType.String, DBNull.Value);


                db.AddInParameter(dbCmd, "@ATTACHMENT5", DbType.Binary, Attachment5);
                if (!string.IsNullOrEmpty(Attachment5ContentType))
                    db.AddInParameter(dbCmd, "@ATTACHMENT5_CONTENT_TYPE", DbType.String, Attachment5ContentType);
                else
                    db.AddInParameter(dbCmd, "@ATTACHMENT5_CONTENT_TYPE", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(Attachment5Filename))
                    db.AddInParameter(dbCmd, "@ATTACHMENT5_FILE_NAME", DbType.String, Attachment5Filename);
                else
                    db.AddInParameter(dbCmd, "@ATTACHMENT5_FILE_NAME", DbType.String, DBNull.Value);
                
                
                db.AddInParameter(dbCmd, "@EMAIL_BODY", DbType.String, EmailBody);

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