#region Impoerts assemblies
using System;
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
    public class TourmasterStoreprocedures
    {
        public DataSet getdataforuploaddetails(int Tourid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
           
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_DATA_FOR_TOUR_DOC_AND_DROPDOWN_NEW");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Tourid);
                ds=db.ExecuteDataSet(dbCmd);
               return ds;
         
        }

      

        public void InsertUpdateTourQuote(ArrayList Tour)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_TOUR_QUOT_NEW");
                db.AddInParameter(dbCmd, "@QUOTE_ID", DbType.Int32, Tour[19]);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Tour[0]);
                db.AddInParameter(dbCmd, "@TOUR_CURRANCY_1", DbType.String,Tour[1]);
                db.AddInParameter(dbCmd, "@ADULT_COST_C1", DbType.Decimal,decimal.Parse(Tour[2].ToString()));
                db.AddInParameter(dbCmd, "@ADULT_TAX_C1", DbType.Decimal,decimal.Parse(Tour[3].ToString()));
                db.AddInParameter(dbCmd, "@ADULT_GST_C1", DbType.Decimal,decimal.Parse(Tour[4].ToString()));
                db.AddInParameter(dbCmd, "@CWB_COST_C1", DbType.Decimal,decimal.Parse(Tour[5].ToString()));
                db.AddInParameter(dbCmd, "@CWB_TAX_C1", DbType.Decimal,decimal.Parse(Tour[6].ToString()));
                db.AddInParameter(dbCmd, "@CWB_GST_C1", DbType.Decimal,decimal.Parse(Tour[7].ToString()));
                db.AddInParameter(dbCmd, "@CNB_COST_C1", DbType.Decimal,decimal.Parse(Tour[8].ToString()));
                db.AddInParameter(dbCmd, "@CNB_TAX_C1", DbType.Decimal,decimal.Parse(Tour[9].ToString()));
                db.AddInParameter(dbCmd, "@CNB_GST_C1", DbType.Decimal,decimal.Parse(Tour[10].ToString()));
                db.AddInParameter(dbCmd, "@INFANT_COST_C1", DbType.Decimal,decimal.Parse(Tour[11].ToString()));
                db.AddInParameter(dbCmd, "@INFANT_TAX_C1", DbType.Decimal,decimal.Parse(Tour[12].ToString()));
                db.AddInParameter(dbCmd, "@INFANT_GST_C1", DbType.Decimal,decimal.Parse(Tour[13].ToString()));
                db.AddInParameter(dbCmd, "@TOUR_CURRANCY_2", DbType.String, Tour[14]);
                db.AddInParameter(dbCmd, "@ADULT_COST_C2", DbType.Decimal,decimal.Parse(Tour[15].ToString()));
                db.AddInParameter(dbCmd, "@CWB_COST_C2", DbType.Decimal,decimal.Parse(Tour[16].ToString()));
                db.AddInParameter(dbCmd, "@CNB_COST_C2", DbType.Decimal,decimal.Parse(Tour[17].ToString()));
                db.AddInParameter(dbCmd, "@INFANT_COST_C2", DbType.Decimal, decimal.Parse(Tour[18].ToString()));
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, Tour[20]);
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
        public void InsertUpdateTour(ArrayList Tour)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_TOUR_TO_FARE_TOUR_MASTER_NEW");
                db.AddInParameter(dbCmd, "@TOUR_TYPE", DbType.String, Tour[0]);
                db.AddInParameter(dbCmd, "@TOUR_CODE", DbType.String, Tour[1]);
                db.AddInParameter(dbCmd, "@TOUR_SHORT_NAME", DbType.String, Tour[2]);
                db.AddInParameter(dbCmd, "@TOUR_LONG_DESC", DbType.String, Tour[14]);
                if (Tour[3].ToString().Equals(" "))
                {
                    db.AddInParameter(dbCmd, "@TOUR_FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOUR_FROM_DATE", DbType.DateTime, DateTime.ParseExact(Tour[3].ToString(), "dd/MM/yyyy", null));
                }
               // db.AddInParameter(dbCmd, "@TOUR_FROM_DATE", DbType.String, Tour[3]);
                if (Tour[4].ToString().Equals(" "))
                {
                    db.AddInParameter(dbCmd, "@TOUR_TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TOUR_TO_DATE", DbType.DateTime, DateTime.ParseExact(Tour[4].ToString(), "dd/MM/yyyy", null));
                }
                //db.AddInParameter(dbCmd, "@TOUR_TO_DATE", DbType.String, Tour[4]);
                db.AddInParameter(dbCmd, "@TOUR_SUB_TYPE", DbType.String, Tour[5]);
                db.AddInParameter(dbCmd, "@TOUR_ITENARY_TYPE", DbType.String, Tour[6]);
                db.AddInParameter(dbCmd, "@NO_OF_DAYS", DbType.Int32, Tour[7]);
                db.AddInParameter(dbCmd, "@NO_OF_NIGHTS", DbType.Int32, Tour[8]);
                db.AddInParameter(dbCmd, "@NO_OF_SEATS", DbType.Int32, Tour[9]);
                db.AddInParameter(dbCmd, "@NO_OF_AVAILABLE_SEATS", DbType.String, Tour[10]);
                db.AddInParameter(dbCmd, "@GUIDE_TITLE", DbType.String, Tour[11]);
                db.AddInParameter(dbCmd, "@BASE_TOUR_ID", DbType.String, Tour[12]);
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Tour[13]);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, Tour[15]);
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

        public void InsertUpdateHotelDetails(ArrayList Tour)
        {
            Database db = null;
            DbCommand dbCmd = null;
            Database db_task = null;
            DbCommand dbCmd_task = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_TOUR_HOTEL_DETAILS_NEW");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Tour[0]);
                if (Tour[1].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(Tour[1].ToString(), "dd/MM/yyyy", null));
                }
                if (Tour[2].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(Tour[2].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.String,Tour[3]);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.String,Tour[4]);
                db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.String,Tour[5]);
                db.AddInParameter(dbCmd, "@NO_OF_ROOMS", DbType.Int32,Convert.ToInt32(Tour[8]));
                db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal,Convert.ToDecimal(Tour[9]));
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.String, Tour[6]);
                db.AddInParameter(dbCmd, "@ROOM_TYPE_ID", DbType.String,Tour[7]);
                db.AddInParameter(dbCmd, "@TAX", DbType.Decimal,Convert.ToDecimal(Tour[10]));
                db.AddInParameter(dbCmd, "@GST", DbType.Decimal,Convert.ToDecimal(Tour[11]));
                db.AddInParameter(dbCmd, "@TOTAL_AMOUNT", DbType.Decimal,Convert.ToDecimal(Tour[12]));
                db.AddInParameter(dbCmd, "@REMARKS", DbType.String, Tour[13]);
                db.AddInParameter(dbCmd, "@BOOKING_REQUEST_TO", DbType.String,Tour[14]);
                db.AddInParameter(dbCmd, "@ROOM_TO_BE_BLOCKED", DbType.Int32, Convert.ToInt32(Tour[15]));
                db.AddInParameter(dbCmd, "@CHECK_REQUEST_TO", DbType.String, Tour[16]);
                //db.AddInParameter(dbCmd, "@CHECK_REQUEST_DATE", DbType.DateTime , Tour[17]);
                //db.AddInParameter(dbCmd, "@CHECK_COMMENTS", DbType.String , Tour[18]);
                //db.AddInParameter(dbCmd, "@TOTAL_ROOM_BLOCKED", DbType.Int32, Tour[19]);
                if (Tour[20].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.DateTime, DateTime.ParseExact(Tour[20].ToString(), "dd/MM/yyyy", null));
                }
                //db.AddInParameter(dbCmd, "@APPROVED_BY", DbType.Int32, Tour[21]);
                //db.AddInParameter(dbCmd, "@BOOKED_BY", DbType.Int32, Tour[22]);
                db.AddInParameter(dbCmd, "@BOOKING_STATUS", DbType.String, Tour[23]);
                //db.AddInParameter(dbCmd, "@BOOKING_REQUEST_DATE", DbType.DateTime , Tour[24]);
                //db.AddInParameter(dbCmd, "@TOTAL_ROOM_ALLOTEED", DbType.Int32, Tour[25]);
                //db.AddInParameter(dbCmd, "@PARTIAL_ROOM_ALLOTED", DbType.Int32, Tour[26]);
                //db.AddInParameter(dbCmd, "@TOTAL_ADULT_ALLOTED", DbType.Int32, Tour[27]);
                //db.AddInParameter(dbCmd, "@TOTAL_CWB_ALLOTED", DbType.Int32, Tour[28]);
                //db.AddInParameter(dbCmd, "@TOTAL_CNB_ALLOTED", DbType.Int32, Tour[29]);
                //db.AddInParameter(dbCmd, "@TOTAL_INFANT_ALLOTED", DbType.Int32, Tour[30]);
                //db.AddInParameter(dbCmd, "@AVALIBLE_ROOM", DbType.Int32, Tour[31]);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, Convert.ToInt32(Tour[32]));
                db.AddInParameter(dbCmd, "@FINAL_AMOUNT", DbType.Decimal, Convert.ToDecimal(0));
                db.ExecuteNonQuery(dbCmd);

                if (Tour[16].ToString() != "0")
                {
                    db_task = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                    dbCmd_task = db.GetStoredProcCommand("GENERATE_TASK_FOR_TOUR");
                    string check_req_title = "Check Hotel Availibility";
                    string check_req_regarding = "Check rooms in hotel " + Tour[5];
                    string check_req_type = "Check Hotel Availibility";
                    DateTime start_date = DateTime.Today;
                    DateTime end_date = DateTime.Today.AddDays(5);

                    db_task.AddInParameter(dbCmd_task, "@TITLE", DbType.String, check_req_title);
                    db_task.AddInParameter(dbCmd_task, "@REGARDING", DbType.String, check_req_regarding);
                    db_task.AddInParameter(dbCmd_task, "@TASK_TYPE", DbType.String, check_req_type);
                    db_task.AddInParameter(dbCmd_task, "@ASSIGN_BY", DbType.Int32,Convert.ToInt32(Tour[33]));
                    db_task.AddInParameter(dbCmd_task, "@ASSIGN_TO", DbType.String, Tour[16]);
                    db_task.AddInParameter(dbCmd_task, "@START_DATE", DbType.DateTime, start_date);
                    db_task.AddInParameter(dbCmd_task, "@END_DATE", DbType.DateTime, end_date);
                    db_task.AddInParameter(dbCmd_task, "@PRIORITY_ID", DbType.String, "Medium");
                    db_task.AddInParameter(dbCmd_task, "@PRODUCT_CODE", DbType.String, Tour[32]);
                    db_task.AddInParameter(dbCmd_task, "@STATUS_ID", DbType.String, "Pending");
                    db_task.ExecuteNonQuery(dbCmd_task);
                }

                if (Tour[14].ToString() != "0")
                {
                    db_task = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                    dbCmd_task = db.GetStoredProcCommand("GENERATE_TASK_FOR_TOUR");
                    string booking_req_title = "Book Hotel";
                    string booking_req_regarding = "Book hotel " + Tour[5];
                    string booking_req_type = "Hotel Booking";
                    DateTime start_date = DateTime.Today;
                    DateTime end_date = Convert.ToDateTime(Tour[20]).AddDays(-3);

                    db_task.AddInParameter(dbCmd_task, "@TITLE", DbType.String, booking_req_title);
                    db_task.AddInParameter(dbCmd_task, "@REGARDING", DbType.String, booking_req_regarding);
                    db_task.AddInParameter(dbCmd_task, "@TASK_TYPE", DbType.String, booking_req_type);
                    db_task.AddInParameter(dbCmd_task, "@ASSIGN_BY", DbType.Int32, Convert.ToInt32(Tour[33]));
                    db_task.AddInParameter(dbCmd_task, "@ASSIGN_TO", DbType.String, Tour[14]);
                    db_task.AddInParameter(dbCmd_task, "@START_DATE", DbType.DateTime, start_date);
                    db_task.AddInParameter(dbCmd_task, "@END_DATE", DbType.DateTime, end_date);
                    db_task.AddInParameter(dbCmd_task, "@PRIORITY_ID", DbType.String, "Medium");
                    db_task.AddInParameter(dbCmd_task, "@PRODUCT_CODE", DbType.String, Tour[32]);
                    db_task.AddInParameter(dbCmd_task, "@STATUS_ID", DbType.String, "Pending");
                    db_task.ExecuteNonQuery(dbCmd_task);
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
        }
        public void InsertUpdateTransportDetails(ArrayList Tour)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
      
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_TOUR_TRANSPORT_DETAILS_NEW");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Tour[0]);
                db.AddInParameter(dbCmd, "@TRANSPORT_NO", DbType.String, Tour[1]);
                db.AddInParameter(dbCmd, "@TRANSPORT_MODE_ID", DbType.Int32,Tour[2]);
                db.AddInParameter(dbCmd, "@TRANSPORT_DETAILS", DbType.String, Tour[3]);
                db.AddInParameter(dbCmd, "@DATE_OF_ARRIVAL", DbType.DateTime, Tour[4]);
                db.AddInParameter(dbCmd, "@PLACE_OF_ARRIVAL", DbType.Int32,Tour[6]);
                db.AddInParameter(dbCmd, "@DATE_OF_DEPARTURE", DbType.DateTime, Tour[5]);
                db.AddInParameter(dbCmd, "@PLACE_OF_DEPARTURE", DbType.Int32, Tour[7]);
                db.AddInParameter(dbCmd, "@NO_OF_SEATS", DbType.Int32,Tour[8]);
                db.AddInParameter(dbCmd, "@AMOUNT", DbType.Decimal,Tour[9]);
                db.AddInParameter(dbCmd, "@BOOKING_REQUEST_TO", DbType.Int32,Tour[11]);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32,Tour[12]);
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


        public void deleteTour(int TourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FAR_TOUR_MASTER_NEW");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, TourId);
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
        public void AssignCountry(int TourId, string Country)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_SELECTED_COUNTRY_FAR_TOUR_MASTER_NEW");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, TourId);
                db.AddInParameter(dbCmd, "@COUNTRY", DbType.String, Country);
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
        public void AssignCITY(int TourId, string CITY)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_SELECTED_CITY_FAR_TOUR_MASTER_NEW");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, TourId);
                db.AddInParameter(dbCmd, "@CITY", DbType.String, CITY);
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
        public void AssignStartEndCity(int TourId, string StartEndCity)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_SELECTED_STARTENDCITY_FAR_TOUR_MASTER_NEW");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, TourId);
                db.AddInParameter(dbCmd, "@STARTENDCITY", DbType.String, StartEndCity);
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
        public void insertMarketingMaterial(int tourid, string title, string filename,int isitenary,string Description,int isdefaultdoc)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_TOUR_AND_MATERIAL_NEW");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourid);
                db.AddInParameter(dbCmd, "@DOC", DbType.String,filename);
                db.AddInParameter(dbCmd, "@TITLE", DbType.String, title);
                db.AddInParameter(dbCmd, "@IS_ITENARY", DbType.Int32, isitenary);
                db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, Description);
                db.AddInParameter(dbCmd, "@IS_DEFAULT_DOC", DbType.Int32,isdefaultdoc);
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
        
        public void updateMarketingMaterial(int tourid, string title, int isdefaultdoc)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_ITERNARY_DATA");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourid);
                db.AddInParameter(dbCmd, "@TITLE", DbType.String, title);
                db.AddInParameter(dbCmd, "@IS_DEFAULT_DOC", DbType.Int32, isdefaultdoc);
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

        public DataSet CopyData(int tourid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_COPY_DATA_FAR_TOUR_MASTER_NEW");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourid);
               DataSet ds=db.ExecuteDataSet(dbCmd);
               return ds;
        }

        public DataSet GetFlightDataByTour_Id(int TourId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null; 
            try 
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_TOUR_DETAIL_FOR_TOURMASTER");
                db.AddInParameter(dbCmd, "@Tour_id", DbType.Int32, TourId);
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

        public void InsertUpdateFlightDetail(ArrayList arr)
        {
            Database db = null;
            DbCommand dbCmd = null;
            Database db_task = null;
            DbCommand dbCmd_task = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("InsertUpdateTourFlightMaster");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32,Convert.ToInt32(arr[0]));
                db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.String,arr[1]);
                db.AddInParameter(dbCmd, "@FLIGHT_ID", DbType.String , arr[2]);
                db.AddInParameter(dbCmd, "@FLIGHT_CLASS", DbType.String, arr[3]);
                db.AddInParameter(dbCmd, "@SEATS_TO_BE_BLOCKED", DbType.Int32, Convert.ToInt32(arr[4]));
                db.AddInParameter(dbCmd, "@CHECK_REQUEST_TO", DbType.String , arr[5]);
                db.AddInParameter(dbCmd, "@BOOKING_REQ_TO", DbType.String, arr[7]);
                db.AddInParameter(dbCmd, "@TOTAL_SEATS_BLOCKED", DbType.Int32, Convert.ToInt32(arr[8]));
                if(arr[9].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.DateTime, DateTime.ParseExact(arr[9].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@BOOKING_STATUS", DbType.String,arr[10]);
                db.AddInParameter(dbCmd, "@CHECK_COMMENT", DbType.String, arr[6]);
                db.AddInParameter(dbCmd, "@TOTAL_AMT_PAID", DbType.Decimal,Convert.ToDecimal(arr[11]));
                db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, Convert.ToDecimal(arr[12]));
                db.AddInParameter(dbCmd, "@GST", DbType.Decimal, Convert.ToDecimal(arr[13]));
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32,Convert.ToInt32(arr[14]));
                db.ExecuteNonQuery(dbCmd);

                if (arr[5].ToString() != "0")
                {
                    db_task = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                    dbCmd_task = db.GetStoredProcCommand("GENERATE_TASK_FOR_TOUR");
                    string check_req_title = "Check Flight Availibility";
                    string check_req_regarding = "Check seats in flight " + arr[2];
                    string check_req_type = "Check Flight Availibility";
                    DateTime start_date = DateTime.Today;
                    DateTime end_date = DateTime.Today.AddDays(5);

                    db_task.AddInParameter(dbCmd_task, "@TITLE", DbType.String, check_req_title);
                    db_task.AddInParameter(dbCmd_task, "@REGARDING", DbType.String, check_req_regarding);
                    db_task.AddInParameter(dbCmd_task, "@TASK_TYPE", DbType.String, check_req_type);
                    db_task.AddInParameter(dbCmd_task, "@ASSIGN_BY", DbType.Int32, Convert.ToInt32(arr[15]));
                    db_task.AddInParameter(dbCmd_task, "@ASSIGN_TO", DbType.String, arr[5]);
                    db_task.AddInParameter(dbCmd_task, "@START_DATE", DbType.DateTime, start_date);
                    db_task.AddInParameter(dbCmd_task, "@END_DATE", DbType.DateTime, end_date);
                    db_task.AddInParameter(dbCmd_task, "@PRIORITY_ID", DbType.String, "Medium");
                    db_task.AddInParameter(dbCmd_task, "@PRODUCT_CODE", DbType.String, arr[14]);
                    db_task.AddInParameter(dbCmd_task, "@STATUS_ID", DbType.String, "Pending");
                    db_task.ExecuteNonQuery(dbCmd_task);
                }

                if (arr[7].ToString() != "0")
                {
                    db_task = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                    dbCmd_task = db.GetStoredProcCommand("GENERATE_TASK_FOR_TOUR");
                    string booking_req_title = "Book Flight";
                    string booking_req_regarding = "Book Flight " + arr[2];
                    string booking_req_type = "Flight Booking";
                    DateTime start_date = DateTime.Today;
                    DateTime end_date = Convert.ToDateTime(arr[9]).AddDays(-3);

                    db_task.AddInParameter(dbCmd_task, "@TITLE", DbType.String, booking_req_title);
                    db_task.AddInParameter(dbCmd_task, "@REGARDING", DbType.String, booking_req_regarding);
                    db_task.AddInParameter(dbCmd_task, "@TASK_TYPE", DbType.String, booking_req_type);
                    db_task.AddInParameter(dbCmd_task, "@ASSIGN_BY", DbType.Int32, Convert.ToInt32(arr[15]));
                    db_task.AddInParameter(dbCmd_task, "@ASSIGN_TO", DbType.String, arr[7]);
                    db_task.AddInParameter(dbCmd_task, "@START_DATE", DbType.DateTime, start_date);
                    db_task.AddInParameter(dbCmd_task, "@END_DATE", DbType.DateTime, end_date);
                    db_task.AddInParameter(dbCmd_task, "@PRIORITY_ID", DbType.String, "Medium");
                    db_task.AddInParameter(dbCmd_task, "@PRODUCT_CODE", DbType.String, arr[14]);
                    db_task.AddInParameter(dbCmd_task, "@STATUS_ID", DbType.String, "Pending");
                    db_task.ExecuteNonQuery(dbCmd_task);
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
        }

        public void InsertUpdateTourCruiseDetail(ArrayList arr)
        {
            Database db = null;
            DbCommand dbCmd = null;
            Database db_task = null;
            DbCommand dbCmd_task = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("InsertUpdateTourCruiseDetail");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32,Convert.ToInt32(arr[13]));
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32,Convert.ToInt32(arr[0]));
                db.AddInParameter(dbCmd, "@CRUISE_ID", DbType.String,arr[1]);
                db.AddInParameter(dbCmd, "@CABINES_TO_BE_BLOCKED", DbType.Int32,Convert.ToInt32(arr[3]));
                db.AddInParameter(dbCmd, "@CHECK_REQUEST_TO", DbType.String,arr[4]);
                db.AddInParameter(dbCmd, "@CHECK_COMMENTS", DbType.String,arr[5]);
                db.AddInParameter(dbCmd, "@BOOKING_REQUEST_TO", DbType.String,arr[6]);
                db.AddInParameter(dbCmd, "@TOTAL_ROOMS_BLOCKED", DbType.Int32,Convert.ToInt32(arr[7]));
                if (arr[8].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TIME_LIMIT", DbType.DateTime, DateTime.ParseExact(arr[8].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@BOOKING_STATUS", DbType.String,arr[9]);
                db.AddInParameter(dbCmd, "@TOTAL_AMOUNT_PAID", DbType.Decimal,Convert.ToDecimal(arr[10]));
                db.AddInParameter(dbCmd, "@TAX", DbType.Decimal, Convert.ToDecimal(arr[11]));
                db.AddInParameter(dbCmd, "@GST", DbType.Decimal, Convert.ToDecimal(arr[12]));
                db.AddInParameter(dbCmd, "@CABINE_CATEGORY", DbType.String,arr[2]);
                db.AddInParameter(dbCmd, "@CRUISE_NAME", DbType.String, arr[14]);
                db.ExecuteNonQuery(dbCmd);

                if (arr[4].ToString() != "0")
                {
                    db_task = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                    dbCmd_task = db.GetStoredProcCommand("GENERATE_TASK_FOR_TOUR");
                    string check_req_title = "Check Cruise Availibility";
                    string check_req_regarding = "Check seats in cruise " + arr[14];
                    string check_req_type = "Check Cruise Availibility";
                    DateTime start_date = DateTime.Today;
                    DateTime end_date = DateTime.Today.AddDays(5);

                    db_task.AddInParameter(dbCmd_task, "@TITLE", DbType.String, check_req_title);
                    db_task.AddInParameter(dbCmd_task, "@REGARDING", DbType.String, check_req_regarding);
                    db_task.AddInParameter(dbCmd_task, "@TASK_TYPE", DbType.String, check_req_type);
                    db_task.AddInParameter(dbCmd_task, "@ASSIGN_BY", DbType.Int32, Convert.ToInt32(arr[15]));
                    db_task.AddInParameter(dbCmd_task, "@ASSIGN_TO", DbType.String, arr[4]);
                    db_task.AddInParameter(dbCmd_task, "@START_DATE", DbType.DateTime, start_date);
                    db_task.AddInParameter(dbCmd_task, "@END_DATE", DbType.DateTime, end_date);
                    db_task.AddInParameter(dbCmd_task, "@PRIORITY_ID", DbType.String, "Medium");
                    db_task.AddInParameter(dbCmd_task, "@PRODUCT_CODE", DbType.String, arr[13]);
                    db_task.AddInParameter(dbCmd_task, "@STATUS_ID", DbType.String, "Pending");
                    db_task.ExecuteNonQuery(dbCmd_task);
                }

                if (arr[6].ToString() != "0")
                {
                    db_task = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                    dbCmd_task = db.GetStoredProcCommand("GENERATE_TASK_FOR_TOUR");
                    string booking_req_title = "Book Cruise";
                    string booking_req_regarding = "Book Cruise " + arr[14];
                    string booking_req_type = "Cruise Booking";
                    DateTime start_date = DateTime.Today;
                    DateTime end_date = Convert.ToDateTime(arr[8]).AddDays(-3);

                    db_task.AddInParameter(dbCmd_task, "@TITLE", DbType.String, booking_req_title);
                    db_task.AddInParameter(dbCmd_task, "@REGARDING", DbType.String, booking_req_regarding);
                    db_task.AddInParameter(dbCmd_task, "@TASK_TYPE", DbType.String, booking_req_type);
                    db_task.AddInParameter(dbCmd_task, "@ASSIGN_BY", DbType.Int32, Convert.ToInt32(arr[15]));
                    db_task.AddInParameter(dbCmd_task, "@ASSIGN_TO", DbType.String, arr[6]);
                    db_task.AddInParameter(dbCmd_task, "@START_DATE", DbType.DateTime, start_date);
                    db_task.AddInParameter(dbCmd_task, "@END_DATE", DbType.DateTime, end_date);
                    db_task.AddInParameter(dbCmd_task, "@PRIORITY_ID", DbType.String, "Medium");
                    db_task.AddInParameter(dbCmd_task, "@PRODUCT_CODE", DbType.String, arr[13]);
                    db_task.AddInParameter(dbCmd_task, "@STATUS_ID", DbType.String, "Pending");
                    db_task.ExecuteNonQuery(dbCmd_task);
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
        }

        public void InsertUpdateDeckDetail(ArrayList arr)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int i;
            try 
            {
                for (i = 1; i <= 10; i++)
                {
                    if (Convert.ToInt32(arr[i]) >= 0)
                    {
                        db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                        dbCmd = db.GetStoredProcCommand("InsertUpdateDeckDetail");
                        db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Convert.ToInt32(arr[11]));
                        db.AddInParameter(dbCmd, "@TOUR_CRUISE_ID", DbType.Int32, Convert.ToInt32(arr[12]));
                        db.AddInParameter(dbCmd, "@DECK_NO", DbType.Int32, i);
                        db.AddInParameter(dbCmd, "@CABINES", DbType.Int32, Convert.ToInt32(arr[i]));
                        db.ExecuteNonQuery(dbCmd); 
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
        }

        public DataTable GetDeckDetail(int Tour_Id, int Tour_Cruise_Id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds=null;
            try {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_DATA_OF_DECK_CABINE");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Tour_Id);
                db.AddInParameter(dbCmd, "@TOUR_CRUISE_ID", DbType.Int32, Tour_Cruise_Id);
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
            return ds.Tables[0];
        }

        public void InsertNewAirline(string Tour_Id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_TOUR_AIRLINE");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32,Convert.ToInt32(Tour_Id));
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

        public void InsertNewCruise(string Tour_Id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_TOUR_CRUISE");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Convert.ToInt32(Tour_Id));
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

        public void InsertNewHotel(string Tour_Id)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_TOUR_HOTEL");
                db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, Convert.ToInt32(Tour_Id));
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
