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
    public class CruiseMasterStoreprocedure
    {

        public void InsertUpdateCruise(ArrayList Cruise)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_Cruise_MASTER");
                db.AddInParameter(dbCmd, "@CRUISE_COMPANY_ID", DbType.String, Cruise[0]);
                db.AddInParameter(dbCmd, "@CRUISE_COMPANY_NAME", DbType.String, Cruise[1]);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.String, Cruise[2]);                
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

        public void InsertUpdateCruiseScheduleDetail(ArrayList Schedule)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CRUISE_SCHEDULE_MASTER");
                db.AddInParameter(dbCmd, "@CRUISE_ID", DbType.Int32,Convert.ToInt32(Schedule[0]));
                db.AddInParameter(dbCmd, "@CRUISE_NAME", DbType.String, Schedule[1]);
                db.AddInParameter(dbCmd, "@CRUISE_CODE", DbType.String, Schedule[2]);
                if(Schedule[3].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd,"@SCHEDULE_DATE",DbType.DateTime,DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@SCHEDULE_DATE", DbType.DateTime, DateTime.ParseExact(Schedule[3].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@SOURCE_CITY_NAME", DbType.String, Schedule[4]);
                db.AddInParameter(dbCmd, "@DESTINATION_CITY_NAME", DbType.String, Schedule[5]);
                db.AddInParameter(dbCmd, "@SOURCE_PORT_NAME", DbType.String, Schedule[6]);
                db.AddInParameter(dbCmd, "@DESTINATION_PORT_NAME", DbType.String, Schedule[7]);
                db.AddInParameter(dbCmd, "@DEPT_TIME", DbType.String, Schedule[8]);
                db.AddInParameter(dbCmd, "@ARRIVAL_TIME", DbType.String, Schedule[9]);
                db.AddInParameter(dbCmd, "@DURATION", DbType.Decimal,Convert.ToDecimal(Schedule[10]));
                db.AddInParameter(dbCmd, "@CRUISE_COMPANY_NAME", DbType.String, Schedule[11]);
                if(Schedule[12].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd,"@DATE_OF_DEP",DbType.DateTime,DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DATE_OF_DEP", DbType.DateTime, DateTime.ParseExact(Schedule[12].ToString(), "dd/MM/yyyy", null));
                } 
                
                 if(Schedule[13].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd,"@DATE_OF_ARRIVAL",DbType.DateTime,DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DATE_OF_ARRIVAL", DbType.DateTime, DateTime.ParseExact(Schedule[13].ToString(), "dd/MM/yyyy", null));
                }
                 db.AddInParameter(dbCmd, "@CRUISE_COMPANY_ID", DbType.Int32, Convert.ToInt32(Schedule[15]));
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

        public void InsertNewCruise(string CRUISE_COMPANY_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_CRUISE");
                db.AddInParameter(dbCmd, "@CRUISE_COMPANY_ID", DbType.Int32, Convert.ToInt32(CRUISE_COMPANY_ID));
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

        public void InsertUpdateCruiseprice(ArrayList Price)
        { 
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CRUISE_PRICE_MASTER");
                db.AddInParameter(dbCmd, "@CURRENCY_PRICE_ID", DbType.Int32,Convert.ToInt32(Price[0]));
                db.AddInParameter(dbCmd, "@CRUISE_ID", DbType.Int32,Convert.ToInt32(Price[1]));
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, (Price[2]));
                db.AddInParameter(dbCmd, "@AUDULT_AMT", DbType.Decimal,Convert.ToDecimal(Price[3]));
                db.AddInParameter(dbCmd, "@AUDULT_TAX", DbType.Decimal,Convert.ToDecimal(Price[4]));
                db.AddInParameter(dbCmd, "@AUDULT_GST", DbType.Decimal,Convert.ToDecimal(Price[5]));
                db.AddInParameter(dbCmd, "@CHILD_AMT", DbType.Decimal,Convert.ToDecimal(Price[6]));
                db.AddInParameter(dbCmd, "@CHILD_TAX", DbType.Decimal,Convert.ToDecimal(Price[7]));
                db.AddInParameter(dbCmd, "@CHILD_GST", DbType.Decimal,Convert.ToDecimal(Price[8]));
                db.AddInParameter(dbCmd, "@INFANT_AMT", DbType.Decimal,Convert.ToDecimal(Price[9]));
                db.AddInParameter(dbCmd, "@INFANT_TAX", DbType.Decimal,Convert.ToDecimal(Price[10]));
                db.AddInParameter(dbCmd, "@INFANT_GST", DbType.Decimal,Convert.ToDecimal(Price[11]));
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

        public void InsertNewCruiseprice(string CRUISE_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_CRUISE_PRICE");
                db.AddInParameter(dbCmd, "@CRUISE_ID", DbType.Int32, Convert.ToInt32(CRUISE_ID));
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

        public void InsertNewCruisevisa(string CRUISE_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_NEW_CRUISE_VISA");
                db.AddInParameter(dbCmd, "@CRUISE_ID", DbType.Int32, Convert.ToInt32(CRUISE_ID));
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

        public void InsertUpdateCruisevisa(ArrayList Price)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_CRUISE_COUNTRY_VISA_MASTER");
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, Convert.ToInt32(Price[0]));
                db.AddInParameter(dbCmd, "@CRUISE_ID", DbType.Int32, Convert.ToInt32(Price[1]));
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.String, (Price[2]));
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

        public void DeleteCruise(int CRUISE_COMPANY_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_CRUISE_COMPANY");
                db.AddInParameter(dbCmd, "@CRUISE_COMPANY_ID", DbType.Int32, CRUISE_COMPANY_ID);
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

        public void DeleteCruiseSchedule(int CRUISE_ID)
        {

            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_SCHEDULE");
                db.AddInParameter(dbCmd, "@CRUISE_COMPANY_ID", DbType.Int32, CRUISE_ID);
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
