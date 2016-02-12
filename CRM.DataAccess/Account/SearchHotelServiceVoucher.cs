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
   public class SearchHotelServiceVoucher
    {
       public DataSet fetch_Invoice_no(String sp_name)
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

       #region Hotel
       // Search Grid
       public DataSet fetchHotelVoucher(String sp_name,String invoice,String city,String supplier, String agent, String fromdate, String todate)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice);
               db.AddInParameter(dbCmd, "@CITY", DbType.String, city);

               db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String,supplier);
               db.AddInParameter(dbCmd, "@AGENT", DbType.String, agent);
              
               if (fromdate.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(fromdate.ToString(), "dd/MM/yyyy", null));
               }
               if (todate.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(todate.ToString(), "dd/MM/yyyy", null));
               }
               
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }
       public DataSet fetchHotelVoucherEdit(String sp_name, String VOUCHERID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {

               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@HOTEL_VOUCHERID", DbType.String, VOUCHERID);
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }
       #endregion

       #region SightSeeing
       // Search Grid
       public DataSet fetchSightSeeingVoucher(String sp_name, String invoice, String city, String client, String agent, String fdate)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {

               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice);
               db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, city);

               db.AddInParameter(dbCmd, "@CLIENT_NAME", DbType.String, client);
               db.AddInParameter(dbCmd, "@AGENT", DbType.String, agent);

               //if (fdate.ToString().Equals(""))
               //{
               //    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DBNull.Value);
               //}
               //else
               //{
               //    db.AddInParameter(dbCmd, "@DATE", DbType.DateTime, DateTime.ParseExact(fdate.ToString(), "dd/MM/yyyy", null));
               //}
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       public DataSet SightSeeingVoucherEdit(String sp_name, String invoice, String city)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {

               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice);
               db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, city);
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }
        #endregion

       #region Transfer Service
       public DataSet FetchTransferVoucher(String sp_name, String invoice, String client, String package, String agent)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {

               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice);
               db.AddInParameter(dbCmd, "@CLIENT_NAME", DbType.String, client);
               db.AddInParameter(dbCmd, "@PACKAGE", DbType.String, package);


               db.AddInParameter(dbCmd, "@AGENT", DbType.String, package);
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       public DataSet TransferVoucherEdit(String sp_name, String invoice)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {

               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@INVOICE_NO", DbType.String, invoice);
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       #endregion
    }
}
