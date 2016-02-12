#region Program Information
/**********************************************************************************************************************************************
 Class Name           : CustomerRelationDetailsDal
 Class Description    : Implementation Logic customer database releated transaction.
 Author               : Nitesh.
 Created Date         : Mar 2, 2010
***********************************************************************************************************************************************/
#endregion

#region Impoerts assemblies
using System;
using System.Data;
using System.Data.Common;
using CRM.Model.CustomersModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
#endregion

namespace CRM.DataAccess.CustomersDAL
{
    public class CustomerRelationDetailsDal
    {
        #region Get Customer
        /// <summary>
        /// Gets customer list.
        /// </summary>
        /// <returns>Returns dataset contains custromer data.</returns>
        public DataSet GetCustomerRelation(int customerId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_RELATION_DETAILS_SELECT);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, customerId);
                db.AddInParameter(dbCmd, "@REL_ID", DbType.Int32, DBNull.Value);
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
        #endregion

        

        #region Insert Customer
        /// <summary>
        /// Insert customer detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertCustomerRelation(CustomerBDto customer)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CUSTOMER_RELATION_DETAILS_INSERT);
                db.AddInParameter(dbCmd, "@CUST_ID", DbType.Int32, customer.CustomerId);
                db.AddInParameter(dbCmd, "@CUST_REL_ID", DbType.Int32, customer.RelationId);
                db.AddInParameter(dbCmd, "@CUST_REL_TITLE", DbType.Int32, customer.TitleId);
                db.AddInParameter(dbCmd, "@CUST_REL_SURNAME", DbType.String, customer.SurName);
                db.AddInParameter(dbCmd, "@CUST_REL_NAME", DbType.String, customer.Name);
                db.AddInParameter(dbCmd, "@CUST_REL_GENDER", DbType.String, customer.GeneralInfo.Gender);
                db.AddInParameter(dbCmd, "@CUST_REL_MARITAL_STATUS_ID", DbType.Int32, customer.GeneralInfo.MarriageStatusId);
                if (customer.GeneralInfo.MarriageDate == DateTime.MinValue)
                {
                    db.AddInParameter(dbCmd, "@CUST_REL_MARRIAGE_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@CUST_REL_MARRIAGE_DATE", DbType.DateTime, customer.GeneralInfo.MarriageDate);
                }
                db.AddInParameter(dbCmd, "@CUST_REL_RELIGION_ID", DbType.Int32, customer.GeneralInfo.ReligionId);
                db.AddInParameter(dbCmd, "@CUST_REL_EMAIL", DbType.String, customer.ContactInfo.EmailId);
                db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, customer.ContactInfo.MobileNo);
                db.AddInParameter(dbCmd, "@CUST_REL_PHONE", DbType.String, customer.ContactInfo.PhoneNo);
                db.AddInParameter(dbCmd, "@CUST_REL_PROFESSION_ID", DbType.Int32, customer.ProfessionId);
                db.AddInParameter(dbCmd, "@CUST_REL_ANNUAL_INCOME", DbType.Decimal, customer.AnnualIncome);
                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_NO", DbType.String, customer.PassPortInfo.PassportNo);
                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_DATE", DbType.DateTime, customer.PassPortInfo.IssueDate);
                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_PLACE", DbType.String, customer.PassPortInfo.IssuePlace);
                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_EXPIRY_DATE", DbType.DateTime, customer.PassPortInfo.EntryDate);
                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_PRINTED_NAME", DbType.String, customer.PassPortInfo.PrintName);
                db.AddInParameter(dbCmd, "@CUST_REL_PASSPORT_ISSUE_COUNTRY", DbType.String, customer.PassPortInfo.IssueCountry);
                db.AddInParameter(dbCmd, "@CUST_REL_STATE", DbType.String, customer.RelationState);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, customer.UserId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
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
            return Result;
        }
        #endregion
    }
}
