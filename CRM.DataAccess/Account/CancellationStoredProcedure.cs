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
   public  class CancellationStoredProcedure
    {
       public DataSet Get_Purchase_Voucher(String invoice_no)
       {

           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_PURCHASE_VOUCHER_NO");
               db.AddInParameter(dbCmd, "@SALES_INVOICE_NO", DbType.String, invoice_no);
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       public DataSet Get_Supplier_Email(int @SUPPLIER_SR_NO)
       {

           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("FETCH_EMAIL_FOR_CANCELLATION");
               db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, SUPPLIER_SR_NO);
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }
    }
}
