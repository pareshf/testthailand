using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
namespace CRM.DataAccess.CustomersDAL
{
   public  class CustomerReportDal
    {

       public DataTable GetCustomerProfile(int customerId)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           DataTable dt = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_PROFILE_FULL_REPORT);
               db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, customerId);
               ds = db.ExecuteDataSet(dbCmd);
               dt = ds.Tables[0];
           }
           catch (Exception ex)
           {
               bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
               if (rethrow)
               { throw ex; }
           }
           finally
           {
               DALHelper.Destroy(ref dbCmd);
           }
           return dt;
       }


       public DataTable GetCustomerContactInfo(int customerId)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           DataTable dt = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_CONTACT_DETAILS_REPORT);
               db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, customerId);
               ds = db.ExecuteDataSet(dbCmd);
               dt = ds.Tables[0];
           }
           catch (Exception ex)
           {
               bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
               if (rethrow)
               { throw ex; }
           }
           finally
           {
               DALHelper.Destroy(ref dbCmd);
           }
           return dt;
       }


    }
}
