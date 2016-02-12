using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;  


namespace CRM.DataAccess.AdministratorEntity
{
    public class BookingCheckList
    {
        public void UpdateCheckListDetail(ArrayList ary)
        {
            Database db = null;
            DbCommand dbCmd = null;
            Boolean flag;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UpdatePassengerCheckListDetail");
                if (Convert.ToString(ary[1]).Equals("YES"))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32,Convert.ToInt32(ary[0]));
                db.AddInParameter(dbCmd, "@CHECKED", DbType.Boolean, flag);
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

        public void UpdateAgreementInBookingMaster(String Booking_Id)
        { 
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_CHECKLIST_AGREEMENT_IN_BOOKING_MASTER");
                db.AddInParameter(dbCmd, "@BOOKING_ID", DbType.Int32, Convert.ToInt32(Booking_Id));
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
