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
   public  class SearchPaymentVoucherStoredProcedure
    {
       public DataSet fetchallDatasearch(String sp_name, String invoice_no, String voucher_status, String voucher_type, String gl_code, String from_date, String to_date, String sales_invoice, String supplier_type, String supplier_name)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               //if (param5 == "0")
               //{
               //    param5 = "";
               //}
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
               db.AddInParameter(dbCmd, "@VOUCHER_STATUS", DbType.String, voucher_status);

               db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, voucher_type);
               db.AddInParameter(dbCmd, "@GL_CODE", DbType.String, gl_code);
               //db.AddInParameter(dbCmd, "@TOUR_NAME_S", DbType.String, param4);
               //db.AddInParameter(dbCmd, "@STATUS", DbType.String, param5);
               if (from_date.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@FROM_DATE_S", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@FROM_DATE_S", DbType.DateTime, DateTime.ParseExact(from_date.ToString(), "dd/MM/yyyy", null));
               }
               if (to_date.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@TO_DATE_S", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TO_DATE_S", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
               }

               db.AddInParameter(dbCmd, "@SALES_INVOICE", DbType.String, sales_invoice );
               db.AddInParameter(dbCmd, "@SUPPLIER_TYPE", DbType.String, supplier_type);
               db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, supplier_name);
               
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }
    }
}
