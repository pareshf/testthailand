using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;

namespace CRM.DataAccess.AdministratorEntity
{
    public class CompanyStoredProcedure
    {
        public void InsertUpdateCompanyMaster(ArrayList company)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_COMPANY_MASTER");
                db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, company[0]);
                db.AddInParameter(dbCmd, "@COMPANY_NAME", DbType.String, company[1]);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE1", DbType.String, company[2]);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE2", DbType.String, company[3]);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, company[4]);
                db.AddInParameter(dbCmd, "@STATE_NAME", DbType.String, company[5]);
                db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, company[6]);
                db.AddInParameter(dbCmd, "@PINCODE", DbType.String, company[7]);
                db.AddInParameter(dbCmd, "@MOBILE", DbType.String, company[8]);
                db.AddInParameter(dbCmd, "@PHONE", DbType.String, company[9]);
                db.AddInParameter(dbCmd, "@FAX", DbType.String, company[10]);
                db.AddInParameter(dbCmd, "@EMAIL_ID", DbType.String, company[11]);
                db.AddInParameter(dbCmd, "@IS_COMPANY_FRANCHISE", DbType.String, company[12]);
                db.AddInParameter(dbCmd, "@PARENT_COMPANY", DbType.String, company[13]);
                db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, company[14]);

                db.AddInParameter(dbCmd, "@ENABLE_AUTO_BAKUP", DbType.String, company[15]);
                db.AddInParameter(dbCmd, "@CURRENCY_SYMBOL", DbType.String, company[16]);
                if (company[17].ToString().Equals(""))
                {

                    db.AddInParameter(dbCmd, "@FINANCIAL_YEAR_FROM", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FINANCIAL_YEAR_FROM", DbType.DateTime, DateTime.ParseExact(company[17].ToString(), "dd/MM/yyyy", null));
                }
                if (company[18].ToString().Equals(""))
                {

                    db.AddInParameter(dbCmd, "@BOOK_BEGINING_FROM", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@BOOK_BEGINING_FROM", DbType.DateTime, DateTime.ParseExact(company[18].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@SECURITY_PASSWORD", DbType.String, company[19]);
                db.AddInParameter(dbCmd, "@BASE_CURRENCY", DbType.String, company[20]);
                db.AddInParameter(dbCmd, "@BASE_CURRENCY_SYMBOL", DbType.String, company[21]);
                db.AddInParameter(dbCmd, "@IS_SYMBOL_SUFFIXED_TO_AMOUNT", DbType.String, company[22]);
                db.AddInParameter(dbCmd, "@SYMBOL_FOR_DECIMAL_PORTION", DbType.String, company[23]);

                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(company[24]));
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
        public void delCompanyName(int delCompany)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("DELETE_FROM_COMPANY_MASTER");
                db.AddInParameter(dbCmd, "@COMPANYID", DbType.Int32, delCompany);
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
    }
}
