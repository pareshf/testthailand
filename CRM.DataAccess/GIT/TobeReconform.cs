using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.GIT
{
   public class TobeReconform
    {
       


        public DataSet InsertConferencePaymentdate(int tourid, String paymentdate, String hotelname, String conftype,String cityname)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;            
            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_PAYMENT_DATE_CONFERENCE");
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, tourid);

                if (paymentdate.ToString().Equals("DD/MM/YYYY") || paymentdate.ToString().Equals(""))
                {

                    db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime, DateTime.ParseExact(paymentdate, "dd/MM/yyyy", null));
                }

                //db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime, paymentdate);
                db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, hotelname);
                db.AddInParameter(dbCmd, "@CONFERENCE_TYPE_ID", DbType.String, conftype);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, cityname);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsData;
        }

        public DataSet InsertGalaDinnerPaymentdate(int tourid, String paymentdate, String hotelname, String dinnertype, String cityname)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;
            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_PAYMENT_DATE_GALADINNER");
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, tourid);
                if (paymentdate.ToString().Equals("DD/MM/YYYY") || paymentdate.ToString().Equals(""))
                {

                    db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.DateTime, DateTime.ParseExact(paymentdate, "dd/MM/yyyy", null));
                }
                //db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.String, paymentdate);
                db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, hotelname);
                db.AddInParameter(dbCmd, "@GALA_DINNER_TYPE_ID", DbType.String, dinnertype);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, cityname);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public DataSet InsertSitePaymentdate(int tourid, String paymentdate, String site)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;
            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_PAYMENT_DATE_SITE_SEEING");
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, tourid);
                if (paymentdate.ToString().Equals("DD/MM/YYYY") || paymentdate.ToString().Equals(""))
                {

                    db.AddInParameter(dbCmd, "@PAYMENT_DUE_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@PAYMENT_DUE_DATE", DbType.DateTime, DateTime.ParseExact(paymentdate, "dd/MM/yyyy", null));
                }
                //db.AddInParameter(dbCmd, "@PAYMENT_DATE", DbType.String, paymentdate);
                db.AddInParameter(dbCmd, "@SITE_NAME", DbType.String, site);
                
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }
      
       /*get From Email Addess*/
        public DataSet getFromEmailAddress(int tourid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;
            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("GET_EMAIL_ADDRESS_FOR_EMAIL");
                db.AddInParameter(dbCmd, "@GIT_TOUR_ID", DbType.Int32, tourid);


                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return dsData;
        }

    }
}
