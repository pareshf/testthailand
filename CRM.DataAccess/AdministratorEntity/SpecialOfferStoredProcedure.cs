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
    public class SpecialOfferStoredProcedure
    {
        public void InsertUpdateSpecialOffer(ArrayList offer)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_SPECIAL_OFFER");
                db.AddInParameter(dbCmd, "@OFFER_ID", DbType.Int32, offer[0]);
                db.AddInParameter(dbCmd, "@PACKAGE_NAME", DbType.String, offer[1]);
                db.AddInParameter(dbCmd, "@CURRENCY", DbType.String, offer[2]);
                db.AddInParameter(dbCmd, "@PRICE", DbType.Decimal, Convert.ToDecimal(offer[3]));
                db.AddInParameter(dbCmd, "@DISPLAY_ON_DASHBOARD", DbType.String, offer[4]);
                //db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Group[2]));
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
        public void delSpecialOffer(int offerid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_SPECIAL_OFFER");
                db.AddInParameter(dbCmd, "@OFFER_ID", DbType.Int32, offerid);
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
