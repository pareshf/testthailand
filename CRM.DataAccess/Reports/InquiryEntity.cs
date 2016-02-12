using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.Reports
{
    public class InquiryEntity
    {
        public DataSet GetReportsRowData(int reportid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_REPORT_WRITER_DATA");
                db.AddInParameter(dbCmd, "@REPORT_ID", DbType.Int32, reportid);
                db.AddInParameter(dbCmd, "@MODE", DbType.String, "INQ_FETCH_ROW_DATA");
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet Geteditlistdata(int reportid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_EXISTING_REPORT_OF_CUSTOMER_DATA_INQUIRY");
                db.AddInParameter(dbCmd, "@REPORT_ID", DbType.Int32, reportid);
                db.AddInParameter(dbCmd, "@MODE", DbType.String, "EDITRECORDS");
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet GetRelationData(int reportid, string mode)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_REPORT_WRITER_DATA");
                db.AddInParameter(dbCmd, "@REPORT_ID", DbType.Int32, reportid);
                db.AddInParameter(dbCmd, "@MODE", DbType.String, mode);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet GetReportsListData(int reportid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_REPORT_WRITER_DATA");
                db.AddInParameter(dbCmd, "@REPORT_ID", DbType.Int32, reportid);
                db.AddInParameter(dbCmd, "@MODE", DbType.String, "INQ_FETCH_LIST_DATA");
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet GetReportsColumnData(int reportid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_REPORT_WRITER_DATA");
                db.AddInParameter(dbCmd, "@REPORT_ID", DbType.Int32, reportid);
                db.AddInParameter(dbCmd, "@MODE", DbType.String, "INQ_FETCH_COLUMN_DATA");
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet GetReportsComperatorData(int reportid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_REPORT_WRITER_DATA");
                db.AddInParameter(dbCmd, "@REPORT_ID", DbType.Int32, reportid);
                db.AddInParameter(dbCmd, "@MODE", DbType.String, "INQ_COMPARATOR");
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet GetExecuteddata(string sql, string column, string row)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_REPORT_EXECUTED_DATA");
                db.AddInParameter(dbCmd, "@SQL", DbType.String, sql);
                db.AddInParameter(dbCmd, "@COLUMN_NAME", DbType.String, column);
                db.AddInParameter(dbCmd, "@ROW_NAME", DbType.String, row);
                db.AddInParameter(dbCmd, "@REPORT", DbType.String, "Inquiry");
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet GetEmailGroupData(string data)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_EMAIL_GROUP_DATA");
                db.AddInParameter(dbCmd, "@PASS_DATA", DbType.String, data);
                db.AddInParameter(dbCmd, "@REPORT", DbType.String, "Inquiry");
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet getrelateddata(string passdata,string UserId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_REPORT_RELATED_DATA");
                db.AddInParameter(dbCmd, "@PASS_DATA", DbType.String, passdata);
				db.AddInParameter(dbCmd, "@USER_ID", DbType.String, UserId);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet getsearchresult(string passdata)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_ALL_REPORT_RELATED_SEARCH_RESULT_INQUIRY");
                db.AddInParameter(dbCmd, "@PASS_DATA", DbType.String, passdata);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet GetExecutedSubdata(string sql, string column, string row, string _one, string _two)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_SUBREPORT_EXECUTED_DATA");
                db.AddInParameter(dbCmd, "@SQL", DbType.String, sql);
                db.AddInParameter(dbCmd, "@COLUMN_NAME", DbType.String, column);
                db.AddInParameter(dbCmd, "@ROW_NAME", DbType.String, row);
                db.AddInParameter(dbCmd, "@COLUMN_CLICK", DbType.String, _one);
                db.AddInParameter(dbCmd, "@ROW_CLICK", DbType.String, _two);
                db.AddInParameter(dbCmd, "@Report", DbType.String, "Inquiry");
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet GetReportsData()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_REPORT_WRITER_DATA");
                db.AddInParameter(dbCmd, "@MODE", DbType.String, "SELECT_REPORT_NAME");
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet Deletereportdata(int reportid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_EXISTING_REPORT_OF_CUSTOMER_DATA_INQUIRY");
                db.AddInParameter(dbCmd, "@MODE", DbType.String, "DELETE_REPORT");
                db.AddInParameter(dbCmd, "@REPORT_ID", DbType.Int32, reportid);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet GetReportsCriteria()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_REPORT_WRITER_DATA");
                db.AddInParameter(dbCmd, "@MODE", DbType.String, "FETCH_FILTER_CRITERIA");
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet GetExistingReportData()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_EXISTING_REPORT_OF_CUSTOMER_DATA_INQUIRY");
                db.AddInParameter(dbCmd, "@MODE", DbType.String, "FETCH_DATA");
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet insertreportmanagerData(string name, string column, string row, string reportid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_EXISTING_REPORT_OF_CUSTOMER_DATA_INQUIRY");
                db.AddInParameter(dbCmd, "@MODE", DbType.String, "INSERT_DATA_REPORT_MANAGER");
                db.AddInParameter(dbCmd, "@REPORT_NAME", DbType.String, name);
                db.AddInParameter(dbCmd, "@RPT_SELECTED_COLUMN", DbType.String, column);
                db.AddInParameter(dbCmd, "@RPT_SELECTED_ROW", DbType.String, row);
                db.AddInParameter(dbCmd, "@REPORT_ID", DbType.String, reportid);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet insertreportoutputmanagerdata(string output)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_EXISTING_REPORT_OF_CUSTOMER_DATA_INQUIRY");
                db.AddInParameter(dbCmd, "@MODE", DbType.String, "INSERT_DATA_REPORT_OUTPUT");
                db.AddInParameter(dbCmd, "@OUTPUT_PARAMETER", DbType.String, output);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet insertreportfiltermanagerdata(string result, string filtercrt, string ddlcond, string type, string txt1, string txt2, string filtertext)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_EXISTING_REPORT_OF_CUSTOMER_DATA_INQUIRY");
                db.AddInParameter(dbCmd, "@MODE", DbType.String, "INSERT_DATA_REPORT_FILTER");
                db.AddInParameter(dbCmd, "PARAM1", DbType.String, result);
                db.AddInParameter(dbCmd, "@FILTER_PARAMETER", DbType.String, filtercrt);
                db.AddInParameter(dbCmd, "@PARAM2", DbType.String, ddlcond);
                db.AddInParameter(dbCmd, "@PARAM3", DbType.String, type);
                db.AddInParameter(dbCmd, "@PARAM4", DbType.String, txt1);
                db.AddInParameter(dbCmd, "@PARAM5", DbType.String, txt2);
                db.AddInParameter(dbCmd, "@PARAM6", DbType.String, filtertext);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
		}
		#region by sunil for international tour count
		public DataSet getrelationcount(string sql)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("REPORT_GET_RELATIVE_COUNT");
				db.AddInParameter(dbCmd, "@SQL", DbType.String, sql);
                db.AddInParameter(dbCmd, "@REPORT", DbType.String,"Inquiry");
				ds = db.ExecuteDataSet(dbCmd);
			}
			catch (Exception ex)
			{

			}
			return ds;
		}
		#endregion
		public DataSet getreportdataforEdit(int reportid)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("FETCH_EXISTING_REPORT_OF_CUSTOMER_DATA_INQUIRY");
                db.AddInParameter(dbCmd, "@MODE", DbType.String, "EDIT_REPORT");
                db.AddInParameter(dbCmd, "@REPORT_ID", DbType.Int32, reportid);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public void insertGadgetintotable(string _userid, int moduleid, string Gadgetname, string GadgetDescription, string Gadget, string colname, string rowname, string chartname)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_REPORT_GADGET_DATA");
                db.AddInParameter(dbCmd, "@USER_ID", DbType.String, _userid);
                db.AddInParameter(dbCmd, "@MODULE_ID", DbType.Int32, moduleid);
                db.AddInParameter(dbCmd, "@GADGET_NAME", DbType.String, Gadgetname);
                db.AddInParameter(dbCmd, "@GADGET_DESCRIPTION", DbType.String, GadgetDescription);
                db.AddInParameter(dbCmd, "@GADGET", DbType.String, Gadget);
                db.AddInParameter(dbCmd, "@COLUMN_NAME", DbType.String, colname);
                db.AddInParameter(dbCmd, "@ROW_NAME", DbType.String, rowname);
                db.AddInParameter(dbCmd, "@CHART_NAME", DbType.String, chartname);
                db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            { }
        }

    }
}

