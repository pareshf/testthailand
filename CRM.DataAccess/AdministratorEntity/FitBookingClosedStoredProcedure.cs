using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;

namespace CRM.DataAccess.AdministratorEntity
{
    public class FitBookingClosedStoredProcedure
    {
        public void InsertUpdateFitBookingClosed(ArrayList FitClosed)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FIT_BOOKING_CLOSED");
                db.AddInParameter(dbCmd, "@FIT_BOOKING_CLOSED_ID", DbType.Int32, Convert.ToInt32(FitClosed[0]));
                db.AddInParameter(dbCmd, "@FIT_PACKAGE", DbType.String, FitClosed[1]);
                if (FitClosed[2].ToString().Equals("DD/MM/YYYY") || FitClosed[2].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(FitClosed[2].ToString(), "dd/MM/yyyy", null));
                }
                if (FitClosed[3].ToString().Equals("DD/MM/YYYY") || FitClosed[3].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(FitClosed[3].ToString(), "dd/MM/yyyy", null));
                }
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

        public void deleteFitBookingClosed(int defitclosed)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_FIT_BOOKING_CLOSED");
                db.AddInParameter(dbCmd, "@FITBOOKING_CLOSED_ID", DbType.Int32, defitclosed);
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
        public void InsertUpdateFitDay(ArrayList FitDay)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_FIT_BOOKING_DAYS");
                db.AddInParameter(dbCmd, "@FIT_BOOKING_DAY_ID", DbType.Int32, Convert.ToInt32(FitDay[0]));
                db.AddInParameter(dbCmd, "@DAY", DbType.Int32, Convert.ToInt32(FitDay[1]));
                if (FitDay[2].ToString().Equals("DD/MM/YYYY") || FitDay[2].ToString().Equals("0"))
                {

                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(FitDay[2].ToString(), "dd/MM/yyyy", null));
                }
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
