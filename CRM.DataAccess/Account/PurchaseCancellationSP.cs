using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.Account
{
    public class PurchaseCancellationSP
    {
        public DataSet commonSp(String sp_name)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                dsData = db.ExecuteDataSet(dbCmd);
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
            return dsData;
        }

        public DataSet getPurchaseVoucherInvoiceVise(String sp_name, String INVOICE_NO)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, INVOICE_NO);
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

        public void updatePurchaseHeaderOnCancellation(string INVOICE_NO, string CANCELLATION_FEES)
        {
            Database db = null;
            DbCommand dbCmd = null;
            

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_PURCHASE_HEADER_ON_CANCELLATION");
                db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, INVOICE_NO);

                if (CANCELLATION_FEES.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@CANCELLATION_FEES", DbType.Decimal, "0");
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CANCELLATION_FEES", DbType.Decimal, CANCELLATION_FEES);
                }
                
                db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

           
        }
        

    }
}
