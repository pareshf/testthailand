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
   public class HotelPriceListGIT
    {
       #region  FETCH ORDER STAUTS RAD COMBO BOX
       public DataSet fetchComboData(String sp_name)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               //   db.AddInParameter(dbCmd, "@QUERY", DbType.String, param);
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       public DataSet fetchDataforHotel(String sp_name, String param)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, param);
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }
       public DataSet fetchDataforHotelroomtype(String sp_name, String param)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, param);
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }
       #endregion

       public DataSet fetch_date_validation(String CITY_NAME, String HOTEL_NAME, String ROOM_TYPE, String AGENT)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("GET_DATES_FOR_VALIDATION_HOTEL_MASTER_GIT");
               db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, CITY_NAME);
               db.AddInParameter(dbCmd, "@HOTEL_NAME", DbType.String, HOTEL_NAME);
               db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, ROOM_TYPE);
               db.AddInParameter(dbCmd, "@AGENT", DbType.String, AGENT);
               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }

       #region Save Record

       
       public DataSet InsertUpdaetHotelPriceList(String priceid, String roomtype, String singleRoom, String doubleRoom, String extraadult, String currency, String Pterms, String supplier,
           String Agent, String cwb, String cnb, String fdate, String todate, String surcharge, String isdefult, String sunit, String tripplerate, String status, String amargin, String AplusMargin,
           String AplusplusMargin, String Amarper, String Aplusper, String AplusplusPer, String city, int user)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;
           bool flag;
           try
           {

               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_HOTEL_PRICE_LIST_MASTER_GIT");
               db.AddInParameter(dbCmd, "@SUPPLIER_HOTEL_PRICE_LIST_ID", DbType.Int32, int.Parse(priceid));
               db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, roomtype);

               if (singleRoom == "")
               {
                   db.AddInParameter(dbCmd, "@SINGLE_ROOM_RATE", DbType.Decimal, Convert.ToDecimal("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@SINGLE_ROOM_RATE", DbType.Decimal, Convert.ToDecimal(singleRoom));
               }
               if (doubleRoom == "")
               {
                   db.AddInParameter(dbCmd, "@DOUBLE_ROOM_RATE", DbType.Decimal, Convert.ToDecimal("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@DOUBLE_ROOM_RATE", DbType.Decimal, Convert.ToDecimal(doubleRoom));
               }
               if (extraadult == "")
               {
                   db.AddInParameter(dbCmd, "@EXTRA_ADULT_RATE", DbType.Decimal, Convert.ToDecimal("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@EXTRA_ADULT_RATE", DbType.Decimal, Convert.ToDecimal(extraadult));
               }
               db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, currency);
               db.AddInParameter(dbCmd, "@PAYMENT_TERMS", DbType.String, Pterms);

               db.AddInParameter(dbCmd, "@SUPPLIER_NAME", DbType.String, supplier);
               db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.String, Agent);

               if (cwb == "")
               {
                   db.AddInParameter(dbCmd, "@CWB_COST", DbType.Decimal, Convert.ToDecimal("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@CWB_COST", DbType.Decimal, Convert.ToDecimal(cwb));
               }
               if (cnb == "")
               {
                   db.AddInParameter(dbCmd, "@CNB_COST", DbType.Decimal, Convert.ToDecimal("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@CNB_COST", DbType.Decimal, Convert.ToDecimal(cnb));
               }
               if (fdate.ToString().Equals("DD/MM/YYYY") || fdate.ToString().Equals("0"))
               {

                   db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(fdate, "dd/MM/yyyy", null));
               }
               if (todate.ToString().Equals("DD/MM/YYYY") || todate.ToString().Equals("0"))
               {

                   db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(todate.ToString(), "dd/MM/yyyy", null));
               }

               if (surcharge == "")
               {
                   db.AddInParameter(dbCmd, "@SURCHARGE", DbType.Decimal, Convert.ToDecimal("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@SURCHARGE", DbType.Decimal, Convert.ToDecimal(surcharge));
               }
               if (Convert.ToString(isdefult).Equals("YES"))
               {
                   flag = true;
               }
               else
               {
                   flag = false;
               }
               db.AddInParameter(dbCmd, "@IS_DEFAULT", DbType.Boolean, flag);
               db.AddInParameter(dbCmd, "@SURCHARG_UNIT", DbType.String, sunit);

               if (tripplerate == "")
               {
                   db.AddInParameter(dbCmd, "@TRIPLE_ROOM_RATE", DbType.Decimal, Convert.ToDecimal("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TRIPLE_ROOM_RATE", DbType.Decimal, Convert.ToDecimal(tripplerate));
               }
               db.AddInParameter(dbCmd, "@STATUS", DbType.String, status);

               if (amargin == "")
               {
                   db.AddInParameter(dbCmd, "@A_MARGIN", DbType.Decimal, Convert.ToDecimal("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@A_MARGIN", DbType.Decimal, Convert.ToDecimal(amargin));
               }
               if (AplusMargin == "")
               {
                   db.AddInParameter(dbCmd, "@A_PLUS_MARGIN", DbType.Decimal, Convert.ToDecimal("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@A_PLUS_MARGIN", DbType.Decimal, Convert.ToDecimal(AplusMargin));
               }
               if (AplusplusMargin == "")
               {
                   db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN", DbType.Decimal, Convert.ToDecimal("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MARGIN", DbType.Decimal, Convert.ToDecimal(AplusplusMargin));
               }
               if (Amarper == "")
               {
                   db.AddInParameter(dbCmd, "@A_MARGIN_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal("0"));
               }
               else
               {

                   db.AddInParameter(dbCmd, "@A_MARGIN_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(Amarper));
               }
               if (Aplusper == "")
               {
                   db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@A_PLUS_MARGIN_IN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(Aplusper));
               }
               if (AplusplusPer == "")
               {
                   db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MAGIN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal("0"));
               }
               else
               {
                   db.AddInParameter(dbCmd, "@A_PLUS_PLUS_MAGIN_PERCENTAGE", DbType.Decimal, Convert.ToDecimal(AplusplusPer));
               }
               db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, city);
               db.AddInParameter(dbCmd, "@USER", DbType.Int32, user);

               dsData = db.ExecuteDataSet(dbCmd);
           }
           catch (Exception ex)
           {

           }

           return dsData;
       }
       #endregion

       #region For Search Grid page
       public DataSet GetGridData(String city, String agent, String supplier, String roomtype, String fdate, String todate)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {

               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("GET_DATA_SUPPLIER_HOTEL_PRICE_LIST_GRID_GIT");

               db.AddInParameter(dbCmd, "@CITY", DbType.String, city);
               db.AddInParameter(dbCmd, "@AGENT", DbType.String, agent);
               db.AddInParameter(dbCmd, "@SUPPLIER", DbType.String, supplier);
               db.AddInParameter(dbCmd, "@ROOM_TYPE", DbType.String, roomtype);
               if (fdate.ToString().Equals(""))
               {

                   db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(fdate.ToString(), "dd/MM/yyyy", null));
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
       #endregion

       #region  Edit Hotel Price List
       public DataSet EditHotelPriceList(String sp_name, int a)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               db.AddInParameter(dbCmd, "@SUPPLIER_HOTEL_PRICE_LIST_ID", DbType.String, a);
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
