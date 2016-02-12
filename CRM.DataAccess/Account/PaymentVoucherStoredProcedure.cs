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
  public   class PaymentVoucherStoredProcedure
    {
          public DataSet fetch_invoice_dateials(String sp_name, String due_date)
          {
               Database db = null;
          DbCommand dbCmd = null;
          DataSet dsData = null;

          try
          {
              db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
              dbCmd = db.GetStoredProcCommand(sp_name);
              db.AddInParameter(dbCmd, "@DUE_DATE", DbType.String, due_date);
             
              dsData = db.ExecuteDataSet(dbCmd);
          }
          catch (Exception ex)
          {

          }
          finally
          {
              DALHelper.Destroy(ref dbCmd);
          }
          return dsData;

          }

          public DataSet fetch_bank_gl_code(String sp_name, String due_date)
          {
              Database db = null;
              DbCommand dbCmd = null;
              DataSet dsData = null;

              try
              {
                  db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                  dbCmd = db.GetStoredProcCommand(sp_name);
                  db.AddInParameter(dbCmd, "@GL_DESCRIPTION", DbType.String, due_date);
                  //if (due_date.ToString().Equals(""))
                  //{
                  //    db.AddInParameter(dbCmd, "@DUE_DATE", DbType.DateTime, DBNull.Value);
                  //}
                  //else
                  //{
                  //    db.AddInParameter(dbCmd, "@DUE_DATE", DbType.DateTime, DateTime.ParseExact(due_date.ToString(), "dd/MM/yyyy", null));
                  //}
                  dsData = db.ExecuteDataSet(dbCmd);
              }
              catch (Exception ex)
              {

              }
              finally
              {
                  DALHelper.Destroy(ref dbCmd);
              }
              return dsData;

          }
      
    }
}
