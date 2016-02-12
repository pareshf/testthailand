#region imports assemblies
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;
#endregion
namespace CRM.DataAccess.AdministratorEntity
{

    public class InquiryMasterStoredProcedure
    {
        public void InsertUpdateInqinsupd(ArrayList inquiry)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("SP_INQ_MASTER_FOR_TOURS_INSERTUPDATE");

                db.AddInParameter(dbCmd, "@CUST_REL_SRNO", DbType.String, inquiry[0]);
                db.AddInParameter(dbCmd, "@CUSTOMER_UNQ_ID", DbType.String, inquiry[1]);                
                db.AddInParameter(dbCmd, "@TITLE_DESC", DbType.String, inquiry[2]);
                db.AddInParameter(dbCmd, "@CUST_REL_SURNAME", DbType.String, inquiry[3]);
                db.AddInParameter(dbCmd, "@CUST_REL_NAME", DbType.String, inquiry[4]);
                db.AddInParameter(dbCmd, "@CUST_REL_EMAIL", DbType.String, inquiry[5]);
                db.AddInParameter(dbCmd, "@CUST_REL_MOBILE", DbType.String, inquiry[6]);
                db.AddInParameter(dbCmd, "@CUST_TYPE_DESC", DbType.String, inquiry[7]);
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.String, inquiry[8]);

                db.AddInParameter(dbCmd, "@InquiryDate", DbType.String, inquiry[9]);                
                db.AddInParameter(dbCmd, "@RatingId", DbType.Int32, Convert.ToInt32(inquiry[10]));
                db.AddInParameter(dbCmd, "@InquiryStatusId", DbType.Int32, Convert.ToInt32(inquiry[11]));                
                db.AddInParameter(dbCmd, "@BranchId", DbType.Int32, Convert.ToInt32(inquiry[12]));
                db.AddInParameter(dbCmd, "@AssignedTo",  DbType.Int32, Convert.ToInt32(inquiry[13]));
                db.AddInParameter(dbCmd, "@TransferredBy",  DbType.Int32, Convert.ToInt32(inquiry[14]));
                db.AddInParameter(dbCmd, "@InquiryFor", DbType.String, inquiry[15]);
                db.AddInParameter(dbCmd, "@ReferenceName", DbType.Int32,Convert.ToInt32(inquiry[16]));                
                db.AddInParameter(dbCmd, "@Country", DbType.String, inquiry[17]);
                db.AddInParameter(dbCmd, "@State", DbType.String, inquiry[18]);
                db.AddInParameter(dbCmd, "@Adult", DbType.Int32, Convert.ToInt32(inquiry[19]));
                db.AddInParameter(dbCmd, "@Cwb", DbType.Int32, Convert.ToInt32(inquiry[20]));
                db.AddInParameter(dbCmd, "@Cnb", DbType.Int32, Convert.ToInt32(inquiry[21]));
                db.AddInParameter(dbCmd, "@Infant", DbType.Int32, Convert.ToInt32(inquiry[22]));
                db.AddInParameter(dbCmd, "@Remarks", DbType.String, inquiry[23]);
                db.AddInParameter(dbCmd, "@TotalBudget", DbType.Decimal,Convert.ToDecimal(inquiry[24]));
                db.AddInParameter(dbCmd, "@TravelDuration", DbType.String, inquiry[25]);
                db.AddInParameter(dbCmd, "@TravelDate", DbType.String, inquiry[26]);
                db.AddInParameter(dbCmd, "@NextFollowupDate", DbType.String, inquiry[27]);
                db.AddInParameter(dbCmd, "@AgentId", DbType.String, inquiry[28]);
                db.AddInParameter(dbCmd, "@ReferenceId", DbType.Int32, Convert.ToInt32(inquiry[29]));
                db.AddInParameter(dbCmd, "@ReferenceName", DbType.String, inquiry[30]);
                
                if (inquiry[31].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@Followup1_Date", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@Followup1_Date", DbType.DateTime, DateTime.ParseExact(inquiry[31].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@Followup1_Remarks", DbType.String, inquiry[32]);

                if (inquiry[33].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@Followup1_Date", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP2_DATE", DbType.DateTime, DateTime.ParseExact(inquiry[33].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@FOLLOWUP2_REMARKS", DbType.String, inquiry[34]);

                if (inquiry[35].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP3_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP3_DATE", DbType.DateTime, DateTime.ParseExact(inquiry[35].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@FOLLOWUP3_REMARKS", DbType.String, inquiry[36]);

                if (inquiry[37].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP4_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP4_DATE", DbType.DateTime, DateTime.ParseExact(inquiry[37].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@FOLLOWUP4_REMARKS", DbType.String, inquiry[38]);

                if (inquiry[39].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP5_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP5_DATE", DbType.DateTime, DateTime.ParseExact(inquiry[39].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@FOLLOWUP5_REMARKS", DbType.String, inquiry[40]);

                if (inquiry[41].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP6_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP6_DATE", DbType.DateTime, DateTime.ParseExact(inquiry[41].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@FOLLOWUP6_REMARKS", DbType.String, inquiry[42]);

                if (inquiry[43].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP7_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP7_DATE", DbType.DateTime, DateTime.ParseExact(inquiry[43].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@FOLLOWUP7_REMARKS", DbType.String, inquiry[44]);

                if (inquiry[45].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP8_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP8_DATE", DbType.DateTime, DateTime.ParseExact(inquiry[45].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@FOLLOWUP8_REMARKS", DbType.String, inquiry[46]);

                if (inquiry[47].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP9_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP9_DATE", DbType.DateTime, DateTime.ParseExact(inquiry[47].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@FOLLOWUP9_REMARKS", DbType.String, inquiry[48]);

                if (inquiry[49].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP10_DATE", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@FOLLOWUP10_DATE", DbType.DateTime, DateTime.ParseExact(inquiry[49].ToString(), "dd/MM/yyyy", null));
                }
                db.AddInParameter(dbCmd, "@FOLLOWUP10_REMARKS", DbType.String, inquiry[50]);
                db.AddInParameter(dbCmd, "@CampaignId", DbType.Int32, Convert.ToInt32(inquiry[51]));
                db.AddInParameter(dbCmd, "@CampaignOwner", DbType.String, inquiry[52]);
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
