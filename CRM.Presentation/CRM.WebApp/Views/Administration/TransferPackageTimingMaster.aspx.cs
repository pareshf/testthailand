using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
using CRM.WebApp.Views.FIT;
using CRM.DataAccess.Account;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;
using CRM.DataAccess.AdministratorEntity;
using CRM.Model.AdministrationModel;
using CRM.DataAccess;
using System.Globalization;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Net;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using CRM.DataAccess.AdministrationDAL;

namespace CRM.WebApp.Views.Administration
{
    public partial class TransferPackageTimingMaster : System.Web.UI.Page
    {
        TransferPackageTimings objTransferPackageTimings = new TransferPackageTimings();

        public MulticheckDropdown DDC1
        {
            get
            {
                return this.DDL1;
            }
        }

        public MulticheckDropdown DDL1 { get; set; }
          string setchk;
          protected void Page_Load(object sender, EventArgs e)
          {
              if (!IsPostBack)
              {

                  DataSet ds = objTransferPackageTimings.GetAllTrasferpackageDescription(int.Parse(Request.QueryString["ID"].ToString()));
                  GridView1.DataSource = ds;
                  GridView1.DataBind();


                  foreach (GridViewRow item in GridView1.Rows)
                  {
                      Label lblfromTOid = (Label)item.FindControl("lbltp_detialid");
                      Label lblFlag = (Label)item.FindControl("lblFlag");
                      DDL1 = (MulticheckDropdown)item.FindControl("DDL1");
                      DataSet ds1 = objTransferPackageTimings.GetAallTimings();

                      //DataSet DsMealDate = objHotelStoreProcedure.getALLdateFORmEAL(txtpty_CheckIn.Text, txtpty_CheckOut.Text);
                      ListItemCollection list = new ListItemCollection();
                      for (int i1 = 0; i1 < ds1.Tables[0].Rows.Count; i1++)
                      {
                          ListItem listitem = new ListItem(Convert.ToString(ds1.Tables[0].Rows[i1]["TIME"]), Convert.ToString(ds1.Tables[0].Rows[i1]["TIME"]));
                          list.Add(listitem);

                      }
                      DDC1.DDList.DataSource = list;
                      DDC1.DDList.DataTextField = "Text";
                      DDC1.DDList.DataValueField = "Value";
                      DDC1.DDList.DataBind();


                      DataSet ds11 = objTransferPackageTimings.GetAllTrasferpackageTimmings(int.Parse(lblfromTOid.Text), lblFlag.Text, "SIC");

                      if (ds11.Tables[0].Rows.Count != 0)
                      {

                          ListItemCollection list2 = new ListItemCollection();
                          for (int j = 0; j < ds11.Tables[0].Rows.Count; j++)
                          {
                              for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                              {
                                  if (ds1.Tables[0].Rows[i]["TIME"].ToString() == ds11.Tables[0].Rows[j]["AutoSearchResult"].ToString())
                                  {
                                      setchk = setchk + "," + Convert.ToString(ds11.Tables[0].Rows[j]["AutoSearchResult"].ToString());
                                  }

                              }

                          }

                          setchk = setchk.Substring(1);
                          DDC1.SetCheckBoxValues(setchk);
                          setchk = "";
                      }

                  }

              }

          }
          protected void btnSave_Click(object sender, EventArgs e)
          {
              foreach (GridViewRow item in GridView1.Rows)
              {
                  Label lblFromId = (Label)item.FindControl("lblFromId");
                  Label lblToId = (Label)item.FindControl("lblToId");
                  DDL1 = (MulticheckDropdown)item.FindControl("DDL1");

                  objTransferPackageTimings.DeleteTimings(int.Parse(lblFromId.Text), int.Parse(lblToId.Text));

                  string str1 = DDC1.GetCheckBoxValues(); ;
                  string[] words = str1.Split(',');

                  foreach (string word in words)
                  {
                      objTransferPackageTimings.InsetTimings(int.Parse(lblFromId.Text), int.Parse(lblToId.Text), word);
                  }
              }
              Master.DisplayMessage("Record Updated Successfully");
          }
    }

      
    }
