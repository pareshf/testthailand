using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using CRM.Model.HRModel;

namespace CRM.DataAccess.CustomersDAL.CustomerGadgetDAL
{
  public  class CustomerGadgetDal
    {
      public DataTable GetCustomerByCompany( int CmpId)
      {
          Database db = null;
          DbCommand dbCmd = null;
          DataSet ds = null;
          DataTable Dt = null;
          try
          {
              db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
              dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_GADGET_REPORT);
              if (CmpId != 0)
                  db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, CmpId);
              else
                  db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, DBNull.Value);

              ds = db.ExecuteDataSet(dbCmd);
              Dt = ds.Tables[0];
              return (Dt);
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
          return (Dt);
      }


    }
}
