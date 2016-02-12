using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
using CRM.DataAccess.Account;
using CRM.DataAccess.GIT;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Globalization;
using CRM.DataAccess;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.WebApp.Views.GIT
{
    
    public partial class GitGroupInformation : System.Web.UI.Page
    {
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();
        GenerateInvoiceSp objgenerateInvoiceStoredProcedure = new GenerateInvoiceSp();
        GitGroupInforamtion objGitGroupInforamtion = new GitGroupInforamtion();


        string CompId;
        string DeptId;
        string RoleId;


        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            //Check Page Authorization

            CompId = Session["CompanyId"].ToString();
            DeptId = Session["DeptId"].ToString();
            RoleId = Session["RoleId"].ToString();


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (RoleId != "18")
                {
                    Label2.Visible = false;
                    Label31.Visible = false;

                    DropDownList2.Visible = false;

                    drpSubagent.Visible = false;
                }

                DataSet  dsPackages = objGitGroupInforamtion.CommonSp("FETCH_GIT_PACKAGES"); // Fetch All Git Packages
                datalist_packages.DataSource = dsPackages;
                datalist_packages.DataBind();

                DataSet dsSlab = objGitGroupInforamtion.CommonSp("GET_SLAB"); // Fetch All Slab
                GridSlab.DataSource = dsSlab;
                GridSlab.DataBind();

                DataSet ds3 = objBookingFitStoreProcedure.fetchComboData("FETCH_AGENT_COMPANY_NAME");
                binddropdownlist(DropDownList2, ds3);

                DataSet dsstatus = objBookingFitStoreProcedure.fetchComboData("FETCH_ORDER_STATUS");
                binddropdownlist(drpOrderStatus, dsstatus);
                drpOrderStatus.Text = "In Process";

                if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
                {
                    DataSet ds = objGitGroupInforamtion.fetchData(int.Parse(Request.QueryString["TOURID"].ToString()));

                    DropDownList2.Text = ds.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString();
                    DataSet DS = objgenerateInvoiceStoredProcedure.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", DropDownList2.Text);
                    if (DS.Tables[0].Rows.Count != 0)
                    {
                        DataSet ds_SubAgent = objBookingFitStoreProcedure.GET_SUB_AGENT(int.Parse(DS.Tables[0].Rows[0]["CUST_ID"].ToString()));
                        if (ds_SubAgent.Tables[0].Rows.Count != 0)
                        {
                            binddropdownlist(drpSubagent, ds_SubAgent);

                            drpSubagent.Text = ds.Tables[0].Rows[0]["CUST_REL_NAME"].ToString();

                            Session["AgentEmail"] = ds.Tables[0].Rows[0]["CUST_REL_EMAIL"].ToString();
                            if (drpSubagent.Text != "")
                            {
                                DataTable DTQW = objHotelStoreProcedure.fetchuserid("FETCH_USER_ID_FROM_CUST_REL", ds_SubAgent.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString());
                                Session["agentId"] = DTQW.Rows[0]["USERSID"].ToString();
                                Session["rel_sr_no"] = DTQW.Rows[0]["empID"].ToString();
                                // Session["AgentId"] = ds_SubAgent.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                            }
                            else
                            {
                                DataTable DTQW = objHotelStoreProcedure.fetchuserid("FETCH_USER_ID_FROM_CUST_REL", ds_SubAgent.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString());
                                Session["agentId"] = DTQW.Rows[0]["USERSID"].ToString();
                                Session["rel_sr_no"] = DTQW.Rows[0]["empID"].ToString();
                                //   Session["AgentId"] = DS.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                            }
                        }
                        else
                        {
                            DataTable DTQW = objHotelStoreProcedure.fetchuserid("FETCH_USER_ID_FROM_CUST_REL", ds_SubAgent.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString());
                            Session["agentId"] = DTQW.Rows[0]["USERSID"].ToString();
                            Session["rel_sr_no"] = DTQW.Rows[0]["empID"].ToString();
                            // Session["AgentId"] = DS.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                        }
                    }
                    txtGroupName.Text = ds.Tables[0].Rows[0]["GIT_GROUP_NAME"].ToString();

                    if (ds.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString() == "0")
                    {
                        txtNo_Adult.Text = "";
                    }
                    else
                    {
                        txtNo_Adult.Text = ds.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["NO_OF_CWB"].ToString() == "0")
                    {

                        txtNo_CWB.Text = "";
                    }
                    else
                    {
                        txtNo_CWB.Text = ds.Tables[0].Rows[0]["NO_OF_CWB"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["NO_OF_CNB"].ToString() == "0")
                    {
                        txtNo_CNB.Text= "";
                        
                    }
                    else
                    {
                        txtNo_CNB.Text= ds.Tables[0].Rows[0]["NO_OF_CNB"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["NO_OF_INFANT"].ToString() == "0")
                    {
                        txtNo_Infant.Text= "";
                    }
                    else
                    {
                        txtNo_Infant.Text = ds.Tables[0].Rows[0]["NO_OF_INFANT"].ToString();
                    }
                  
                    
                   
                    txtStartDate.Text = ds.Tables[0].Rows[0]["START_DATE"].ToString();
                    txtEndDate.Text = ds.Tables[0].Rows[0]["END_DATE"].ToString();
                    txtTotalRoom.Text = ds.Tables[0].Rows[0]["TOTAL_NO_OF_ROOMS"].ToString();
                    txtPackageName.Text = ds.Tables[0].Rows[0]["GIT_PACKAGE_NAME"].ToString();
                    txtNoofNights.Text = ds.Tables[0].Rows[0]["NO_OF_NIGHTS"].ToString();
                    drpOrderStatus.Text = ds.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString();

                    DataSet ds_Slab = objGitGroupInforamtion.fetchSlab(int.Parse(Request.QueryString["TOURID"].ToString()));

                    for (int i = 0; i < ds_Slab.Tables[0].Rows.Count; i++)
                    {
                        foreach (GridViewRow item in GridSlab.Rows)
                        {
                            Label lblSlab = (Label)item.FindControl("lblSlabId");

                            if (lblSlab.Text == ds_Slab.Tables[0].Rows[i]["SLAB_ID"].ToString())
                            {
                                CheckBox chk = (CheckBox)item.FindControl("chkSelect");

                                chk.Checked = true;
                            }
                        }
                    }

                    foreach (DataListItem item in datalist_packages.Items)
                    {
                        Label packname = (Label)item.FindControl("Label1");


                        if (packname.Text == ds.Tables[0].Rows[0]["GIT_PACKAGE_NAME"].ToString())
                        {
                            Label hotelid = (Label)item.FindControl("lblPackgeId");
                            RadioButton rbtn = (RadioButton)item.FindControl("rbtnpackage");
                            rbtn.Checked = true;

                            Session["packgeId"] = hotelid.Text;

                            //  Label packagName = (Label)item.FindControl("Label1");
                            Session["Packgename"] = packname.Text;
                            break;
                        }


                    }
                }
                else
                {
                    Session["agentId"] = Session["usersid"].ToString();
                }
            }
        }

        protected void BtnNext_Click(object sender, EventArgs e)
        {
            getSlabID();

            DataTable dt = (DataTable)Session["SlabID"];
            if (dt.Rows.Count == 0)
            {
                Master.DisplayMessage("Select At least one Slab.", "successMessage", 3000);
            }
            else if (drpOrderStatus.Text == "In Process" && txtTotalRoom.Text == "")
            {
                Master.DisplayMessage("Total No of Rooms is Required.", "successMessage", 5000);
            }
            else if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
            {
                Session["GroupName"] = txtGroupName.Text;
                Session["FromDate"] = txtStartDate.Text;
                Session["ToDate"] = txtEndDate.Text;
                Session["NoofRooms"] = txtTotalRoom.Text;
                Session["Nights"] = txtNoofNights.Text;
                Session["OrderStatus"] = drpOrderStatus.Text;

                Session["Adults"] = txtNo_Adult.Text;
                Session["CWB"]= txtNo_CWB.Text;
                Session["CNB"] = txtNo_CNB.Text;
                Session["infants"] = txtNo_Infant.Text;
                Session["CancellationFees"] = txtCancelFees.Text;
                Response.Redirect("~/Views/GIT/GitDetails.aspx?TOURID=" + Request.QueryString["TOURID"].ToString());
            }
            else
            {
                Session["GroupName"] = txtGroupName.Text;
                Session["FromDate"] = txtStartDate.Text;
                Session["ToDate"] = txtEndDate.Text;
                Session["NoofRooms"] = txtTotalRoom.Text;
                Session["Nights"] = txtNoofNights.Text;
                Session["OrderStatus"] = drpOrderStatus.Text;

                Session["Adults"] = txtNo_Adult.Text;
                Session["CWB"] = txtNo_CWB.Text;
                Session["CNB"] = txtNo_CNB.Text;
                Session["infants"] = txtNo_Infant.Text;
                Session["CancellationFees"] = txtCancelFees.Text;
                Response.Redirect("~/Views/GIT/GitDetails.aspx");
            }
           
            
        }

        public void CheckChanged(object sender, EventArgs e)
        {
            //on each item checked, remove any other items checked
            foreach (DataListItem item in datalist_packages.Items)
            {
                RadioButton rb = (RadioButton)item.FindControl("rbtnpackage");
                if (rb != sender)
                {
                    rb.Checked = false;
                }
                else
                {
                   Label packagid = (Label)item.FindControl("lblPackgeId");
                   Session["packgeId"] = packagid.Text;

                    Label packagName  = (Label)item.FindControl("Label1");
                    Session["Packgename"] = packagName.Text;
                }
            }
           
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList2.Text != "")
            {
                string name = DropDownList2.Text;
                DataSet DS = objGitGroupInforamtion.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", name);
                Session["agentId"] = DS.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                Session["rel_sr_no"] = DS.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                if (DS.Tables[0].Rows.Count != 0)
                {
                    DataSet ds_SubAgent = objBookingFitStoreProcedure.GET_SUB_AGENT(int.Parse(DS.Tables[0].Rows[0]["CUST_ID"].ToString()));
                    binddropdownlist(drpSubagent, ds_SubAgent);
                }
            }
            UpdatePanel_TourDetails.Update();
        }

        protected void drpSubagent_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet DS = objGitGroupInforamtion.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", DropDownList2.Text);
            if (DS.Tables[0].Rows.Count != 0 && drpSubagent.Text != "")
            {
                DataSet ds_SubAgent = objBookingFitStoreProcedure.GET_SUB_AGENT_REL_NO(int.Parse(DS.Tables[0].Rows[0]["CUST_ID"].ToString()), drpSubagent.Text);
                if (ds_SubAgent.Tables[0].Rows.Count != 0)
                {
                   // Session["agentId"] = ds_SubAgent.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();

                    DataTable DTQW = objHotelStoreProcedure.fetchuserid("FETCH_USER_ID_FROM_CUST_REL", ds_SubAgent.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString());
                    Session["agentId"] = DTQW.Rows[0]["USERSID"].ToString();
                    Session["rel_sr_no"] = DTQW.Rows[0]["empID"].ToString();
                }
            }
        }

        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            
        }

        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow item in GridSlab.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                chk.Checked = true;

            }
            UpdatePanel_TourDetails.Update();
        }

        protected void getSlabID()
        {
            DataTable dt = new DataTable();
            foreach (GridViewRow item in GridSlab.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                if (chk.Checked == true)
                {
                    Label slbid = (Label)item.FindControl("lblSlabId");
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("slabId");
                    }
                    DataRow dr = dt.NewRow();
                    dr["slabId"] = slbid.Text;
                    dt.Rows.Add(dr);
                }
            }
            Session["SlabID"] = dt; 
        }

        protected void txtFrom_Date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.DateTime today = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", null);

                if (txtNoofNights.Text != "")
                {
                    System.DateTime answer1 = today.AddDays(double.Parse(txtNoofNights.Text));

                    txtEndDate.Text = answer1.ToString("dd/MM/yyyy");
                }

                txtPackageName.Text = txtGroupName.Text + " " + Session["Packgename"].ToString() + " " + txtNoofNights.Text + "Nights";
            }
            catch (Exception ex)
            {

            }
            finally
            {
                UpdatePanel_TourDetails.Update();
            }
        }

     
    }
}