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
   public class AgentPurchaseVoucher
    {

       public DataSet fetchAgentVoucher(String sp_name, String param1, String param2, String param3, String param6, String param7,int custid)
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
               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, param1);
               db.AddInParameter(dbCmd, "@VOUCHER_STATUS", DbType.String, param2);

               db.AddInParameter(dbCmd, "@VOUCHER_TYPE", DbType.String, param3);
               //db.AddInParameter(dbCmd, "@TOUR_NAME_S", DbType.String, param4);
               //db.AddInParameter(dbCmd, "@STATUS", DbType.String, param5);
               if (param6.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@FROM_DATE_S", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@FROM_DATE_S", DbType.DateTime, DateTime.ParseExact(param6.ToString(), "dd/MM/yyyy", null));
               }
               if (param7.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@TO_DATE_S", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TO_DATE_S", DbType.DateTime, DateTime.ParseExact(param7.ToString(), "dd/MM/yyyy", null));
               }
               db.AddInParameter(dbCmd, "@CUST_ID", DbType.String, custid);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }
    }
}
