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
   public class SupplierMarginMaster
    {
        public void InsertUpdateSupplierMarginMaster(ArrayList Margin)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_SUPPLIER_MARGIN_MASTER");
                db.AddInParameter(dbCmd, "@SUPPLIER_MARGIN_ID", DbType.Int32, Margin[0]);
                db.AddInParameter(dbCmd, "@MARGIN_AMOUNT", DbType.String, Margin[1]);
                db.AddInParameter(dbCmd, "@MARGIN_AMOUNT_IN_PERCENTAGE", DbType.String, Margin[2]);
                db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, Margin[3]);
                db.AddInParameter(dbCmd, "@PRODUCT_NAME", DbType.String, Margin[4]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Margin[5]));
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

        public void deleteSupplierMarginMaster(int SUPPLIERMARGINID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_SUPPLIER_MARGIN_MASTER");
                db.AddInParameter(dbCmd, "@SUPPLIER_MARGIN_ID", DbType.Int32, SUPPLIERMARGINID);
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
