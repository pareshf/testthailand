#region Impoerts assemblies
using System;
using System.Data;
using System.Data.Common;
using CRM.Model.CustomersModel;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
#endregion

namespace CRM.DataAccess.CustomersDAL
{
    public class CustomerEmailTemplateDal : IDisposable
    {
        #region Insert Customer Email Template
        /// <summary>
        /// Insert customer detail.
        /// </summary>
        /// <param name="customer">CustomerBDto object that customer data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int InsertCustomerEmailTemplate(CustomerEmailTemplateBDto objCustomerEmailTemplateBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_EMAIL_TEMPLATE_TYPE_MASTER_INSERT);
                
                db.AddInParameter(dbCmd, "@TEMPLATE_ID", DbType.Int32, objCustomerEmailTemplateBDto.templateId);
                db.AddInParameter(dbCmd, "@TEMPLATE", DbType.String, objCustomerEmailTemplateBDto.templateDesc);
                Result = db.ExecuteNonQuery(dbCmd);
                return Result;
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

        #region Update Customer Email Template
        /// <summary>
        /// Insert customer detail.
        /// </summary>
        /// <param name="customer">CustomerBDto object that customer data to insert.</param>
        /// <returns>Returns 1 and 0; 1 indicates successfull operation.</returns>
        public int UpdateCustomerEmailTemplate(CustomerEmailTemplateBDto objCustomerEmailTemplateBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_EMAIL_TEMPLATE_TYPE_MASTER_UPDATE);

                db.AddInParameter(dbCmd, "@TEMPLATE_SR_ID", DbType.Int32, objCustomerEmailTemplateBDto.SrNotemplate);                
                db.AddInParameter(dbCmd, "@TEMPLATE_ID", DbType.Int32, objCustomerEmailTemplateBDto.templateId);
                db.AddInParameter(dbCmd, "@TEMPLATE", DbType.String, objCustomerEmailTemplateBDto.templateDesc);
                Result = db.ExecuteNonQuery(dbCmd);
                return Result;
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

        #region DeleteCustomerEmailTemplate
        /// <summary>
        /// Delete customers detail.
        /// </summary>
        /// <param name="idCollections">Customer Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteCustomerEmailTemplate(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_EMAIL_TEMPLATE_TYPE_MASTER_DELETE);
                db.AddInParameter(dbCmd, "@TEMPLATE_SR_ID", DbType.String, idCollections);
                Result = db.ExecuteNonQuery(dbCmd);
                return Result;
                
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

        #region FindCustomerEmailTemplate

        public DataTable FindCustomerEmailTemplate(string searchPara)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_EMAIL_TEMPLATE_TYPE_MASTER_SELECT");
                if (!string.IsNullOrEmpty(searchPara))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, searchPara);
                else
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, DBNull.Value);
                ds = db.ExecuteDataSet(dbCmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
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
            return dt;
        }

        #endregion

        #region GetCustomerEmailTemplateById

        public DataTable GetCustomerEmailTemplateById(int SrNoTemplate)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_EMAIL_TEMPLATE_TYPE_MASTER_SELECT_BYID");
                db.AddInParameter(dbCmd, "@TEMPLATE_SR_ID", DbType.Int32, SrNoTemplate);
                ds = db.ExecuteDataSet(dbCmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
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
            return dt;
        }

        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
    }
}
