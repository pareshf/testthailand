using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;

namespace CRM.DataAccess.AdministratorEntity
{
   public class CampaignStoredProcedureNew
    {
       public void InsertUpdateCampaign(ArrayList Campaign)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_COMPAIGN_MASTER");
               db.AddInParameter(dbCmd, "@CAMPAIGN_ID", DbType.Int32, Campaign[1]);
               db.AddInParameter(dbCmd, "@CAMPAIGN_NAME", DbType.String, Campaign[0]);
               db.AddInParameter(dbCmd, "@CAMPAIGN_CODE", DbType.String, Campaign[2]);
               if (Campaign[3].ToString().Equals(" "))
               {
                   db.AddInParameter(dbCmd, "@START_DATE", DbType.DateTime,DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@START_DATE", DbType.DateTime, DateTime.ParseExact(Campaign[3].ToString(), "dd/MM/yyyy", null));
               }
               if (Campaign[4].ToString().Equals(" "))
               {
                   db.AddInParameter(dbCmd, "@END_DATE", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@END_DATE", DbType.DateTime, DateTime.ParseExact(Campaign[4].ToString(), "dd/MM/yyyy", null));
               }
               db.AddInParameter(dbCmd, "@CURRENCY_NAME", DbType.String, Campaign[5]);
               db.AddInParameter(dbCmd, "@STATUS_NAME", DbType.String, Campaign[6]);
               db.AddInParameter(dbCmd, "@COMPAIGN_TYPE", DbType.String, Campaign[7]);
               db.AddInParameter(dbCmd, "@BUDGET", DbType.String, Campaign[8]);
               db.AddInParameter(dbCmd, "@ACTUAL_COST", DbType.String, Campaign[9]);
               db.AddInParameter(dbCmd, "@EXPECTED_COST", DbType.String, Campaign[10]);
               db.AddInParameter(dbCmd, "@MISC_COST", DbType.String, Campaign[11]);
               db.AddInParameter(dbCmd, "@EXPECTED_REVENU", DbType.String, Campaign[12]);
               //db.AddInParameter(dbCmd, "@OFFER", DbType.String, Campaign[13]);
               db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, Campaign[13]);
               //db.AddInParameter(dbCmd, "@EMP_NAME", DbType.String, Campaign[15]);
               //db.AddInParameter(dbCmd, "@TRACKER_NAME", DbType.String, Campaign[16]);
               //db.AddInParameter(dbCmd, "@TRACKER_LINK", DbType.String, Campaign[17]);
               db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Campaign[14]));
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
       public void delCampaign(int compdel)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("DELETE_FROM_COMPAIGN_MASTER");
               db.AddInParameter(dbCmd, "@COMPAIGNID", DbType.Int32, compdel);
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
       public void InsertUpdateCampaignTargetListDetail(ArrayList CampaignDetail)
       {
           Database db = null;
           DbCommand dbCmd = null;

           Database db1 = null;
           DbCommand dbCmd1 = null;

           
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_CAMPAIGN_TARGETLIST_DETAIL_NEW");
               
               db.AddInParameter(dbCmd, "@TARGET_DETAILS_ID", DbType.Int32, CampaignDetail[1]);
               db.AddInParameter(dbCmd, "@TARGETLIST_NAME", DbType.String, CampaignDetail[0]);
               //db.AddInParameter(dbCmd, "@CAMPAIGN_NAME", DbType.String, CampaignDetail[2]);
               //db.AddInParameter(dbCmd, "@CUST_TYPE_NAME", DbType.String, CampaignDetail[3]);
               db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(CampaignDetail[2]));
               db.ExecuteNonQuery(dbCmd);



               db1 = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd1 = db.GetStoredProcCommand("INSERT_UPDATE_TARGETLIST_MASTER");
               db1.AddInParameter(dbCmd1, "@TARGETLIST_NAME", DbType.String, CampaignDetail[0]);
               db1.ExecuteNonQuery(dbCmd1);
               
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
       //public void insertnewtargetlistdetails(ArrayList CampaignDetail1)
       //{
       //    Database db = null;
       //    DbCommand dbCmd = null;
       //    try
       //    {
       //        db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
       //        dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_CAMPAIGN_TARGETLIST_DETAIL");
       //        db.AddInParameter(dbCmd, "@TARGET_DETAILS_ID", DbType.Int32, CampaignDetail1[0]);
       //        db.ExecuteNonQuery(dbCmd);
       //    }
       //    catch (Exception ex)
       //    {
       //        bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
       //        if (rethrow)
       //        {
       //            throw ex;

       //        }
       //    }
       //    finally
       //    {
       //        DALHelper.Destroy(ref dbCmd);
       //    }

       //}

       public void delCampaignTargetListDetail(int delCampTargetDetail)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("DELETE_FROM_CAMPAIGN_TARGETLIST_DETAIL");
               db.AddInParameter(dbCmd, "@CAMPTARGETID", DbType.Int32, delCampTargetDetail);
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
       public void InsertUpdateforMapping(ArrayList mapping)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_COMAPAIGN_MARKETING_MAPPING");
               db.AddInParameter(dbCmd, "@CAMP_MAR_ID", DbType.Int32, mapping[3]);
               db.AddInParameter(dbCmd, "@CAMPGAIN_NAME", DbType.String, mapping[0]);
               db.AddInParameter(dbCmd, "@TOURSHORT_NAME", DbType.String, mapping[1]);
               db.AddInParameter(dbCmd, "@TOURCODE", DbType.String, mapping[2]);
               db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(mapping[4]));
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
       public void InsertNewRow(string CAMPAIGN_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_NEW_CAMP_MAR_MAPPING");
               db.AddInParameter(dbCmd, "@CAMPAIGN_ID", DbType.Int32, Convert.ToInt32(CAMPAIGN_ID));
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
       public void InsertNewTargetList(string CAMPAIGN_ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_NEW_TARGET_LIST_FOR_COMPAIGN");
               db.AddInParameter(dbCmd, "@CAMPAIGN_ID", DbType.Int32, Convert.ToInt32(CAMPAIGN_ID));
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
