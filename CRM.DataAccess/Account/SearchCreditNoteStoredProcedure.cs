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
   public  class SearchCreditNoteStoredProcedure
    {
       public DataSet fetchallDatasearch(String sp_name, String invoice_no,  String from_date, String to_date,String vtype,String vstatus)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice_no);
              
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
               db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, vtype);
               db.AddInParameter(dbCmd, "@VOUCHER_STATUS", DbType.String, vstatus);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }
    }
}
