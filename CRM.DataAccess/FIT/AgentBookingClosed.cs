using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;

namespace CRM.DataAccess.FIT
{
    public class AgentBookingClosed
    {
        public DataSet fetchallData(String sp_name, String param)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@USERID", DbType.Int32, int.Parse(param));
                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }

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
        #endregion

        public DataSet fetchallDatasearch(String sp_name, String userid, String quoteid, String tour, String status, String fdate, String todate)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                if (status == "0")
                {
                    status = "";
                }
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(sp_name);
                db.AddInParameter(dbCmd, "@USERID", DbType.Int32, int.Parse(userid));
                db.AddInParameter(dbCmd, "@QUOTATION_NO", DbType.Int32, int.Parse(quoteid));
                //db.AddInParameter(dbCmd, "@AGENT_NAME_S", DbType.String, AGENT);
                //db.AddInParameter(dbCmd, "@CLIENT_NAME_S", DbType.String, param3);
                db.AddInParameter(dbCmd, "@TOUR_NAME_S", DbType.String, tour);
                db.AddInParameter(dbCmd, "@STATUS", DbType.String, status);
                if (fdate.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE_S", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE_S", DbType.DateTime, DateTime.ParseExact(fdate.ToString(), "dd/MM/yyyy", null));
                }
                if (todate.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TO_DATE_S", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE_S", DbType.DateTime, DateTime.ParseExact(todate.ToString(), "dd/MM/yyyy", null));
                }


                dsData = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }

            return dsData;
        }


        public void insert_block_dates(int Id, String from_date, String to_date)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet dsData = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_BLOCK_DATES");
                db.AddInParameter(dbCmd, "@ADMIN_BLOCK_DATE_ID", DbType.String, Id);
                if (from_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FROM_DATE", DbType.DateTime, DateTime.ParseExact(from_date.ToString(), "dd/MM/yyyy", null));
                }

                if (to_date.ToString().Equals(""))
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@TO_DATE", DbType.DateTime, DateTime.ParseExact(to_date.ToString(), "dd/MM/yyyy", null));
                }

                db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {

            }


        }
    }
}
