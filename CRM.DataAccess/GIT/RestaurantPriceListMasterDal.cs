using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.GIT
{
   public class RestaurantPriceListMasterDal
    {

       #region fetch combo data

       public DataSet fetchComboData(String sp_name)
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
               throw ex;
           }

           return dsData;
       }



       #endregion

       public DataSet fetchRestaurantPriceListDataForEdit(int Restaurantid)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("GET_ALL_RESTAURENT_PRICE_LIST_DETAILS_FOR_EDIT");
               db.AddInParameter(dbCmd, "@RESTAURENT_PRICE_ID", DbType.String, Restaurantid);

               ds = db.ExecuteDataSet(dbCmd);
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
           return ds;
       }
       // Insert into Hotel Price List Master
       public void insertRestaurantPriceListMaster(int restid,string srno,string meal,string adult,string child,string currency,string userid)
       {
           Database db = null;
           DbCommand dbCmd = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_INTO_RESTAURENT_PRICE_LIST_MASTER_MASTER");
               db.AddInParameter(dbCmd, "@RESTAURENT_PRICE_LIST_ID", DbType.String, restid);
               db.AddInParameter(dbCmd, "@SUPPLIER_SR_NO", DbType.String, srno);
               db.AddInParameter(dbCmd, "@MEAL_ID", DbType.String, meal);
               db.AddInParameter(dbCmd, "@ADULT_RATE", DbType.Decimal, decimal.Parse(adult));
               db.AddInParameter(dbCmd, "@CHILD_RATE", DbType.Decimal, decimal.Parse(child));
               db.AddInParameter(dbCmd, "@CURRENCY_ID", DbType.String, currency);
               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.String, userid);

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

       public DataSet FetchCountForValidation(string restname,string mealname,int restid)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               //Database Connection
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("GET_DATA_FROM_RESTAURANT_PRICE_LIST_FOR_VALIDATION");
               db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, restname);
               db.AddInParameter(dbCmd, "@MEAL_TYPE", DbType.String, mealname);
               db.AddInParameter(dbCmd, "@REST_ID", DbType.Int32, restid);
               //Command Execute
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

       public DataSet FetchChainNameandCity(int Supplierid)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               //Database Connection
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("GET_SUPPLIER_NAME_AND_CITY_NAME");
               db.AddInParameter(dbCmd, "@SUPPLIER_ID", DbType.String, Supplierid);


               //Command Execute
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
    }
}
