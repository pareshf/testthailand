using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.Account;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using CRM.DataAccess.AccountMaster;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;

namespace CRM.WebApp.Views.AccountMaster
{
    public partial class ChartofAccount : System.Web.UI.Page
    {
        GroupTypeMasterStoredProcedure objGroupTypeMasterStoredProcedure = new GroupTypeMasterStoredProcedure();
        ChartofAccountStoredProcedure objChartofAccountStoredProcedure = new ChartofAccountStoredProcedure();

        bool falg_gldesc = true;
        int count = 0;

        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //Check Page Authorization
            String CompId, DeptId, RoleId;
            CompId = Session["CompanyId"].ToString();
            DeptId = Session["DeptId"].ToString();
            RoleId = Session["RoleId"].ToString();

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 201);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                DataSet ds = objGroupTypeMasterStoredProcedure.get_account_code("GET_GROUP_NAME");
                binddropdownlist(drpAccType, ds);

                DataSet ds1 = objGroupTypeMasterStoredProcedure.get_account_code("GET_BALANCE_TYPE");
                binddropdownlist(DropDownList2, ds1);

                DataSet ds2 = objGroupTypeMasterStoredProcedure.get_account_code("GET_ACCOUNT_TYPE");
                bindComboBoxforquestion(drpQuestion, ds2);

                if (Request["CAID"] != null && !string.IsNullOrEmpty(Request["CAID"].ToString()))
                {
                    DataSet ds_data =objChartofAccountStoredProcedure.get_data_for_edit(int.Parse(Request.QueryString["CAID"].ToString()));

                    if (ds_data.Tables[0].Rows[0]["ACCOUNT_FLAG"].ToString() == "S")
                    {
                        drpAccType.Text = ds.Tables[0].Rows[1]["AutoSearchResult"].ToString();
                    }
                    else if (ds_data.Tables[0].Rows[0]["ACCOUNT_FLAG"].ToString() == "A")
                    {
                        drpAccType.Text = ds.Tables[0].Rows[0]["AutoSearchResult"].ToString();
                    }
                    else if (ds_data.Tables[0].Rows[0]["ACCOUNT_FLAG"].ToString() == "E")
                    {
                        drpAccType.Text = ds.Tables[0].Rows[2]["AutoSearchResult"].ToString();
                    }
                    else if (ds_data.Tables[0].Rows[0]["ACCOUNT_FLAG"].ToString() == "P")
                    {
                        drpAccType.Text = ds.Tables[0].Rows[3]["AutoSearchResult"].ToString();
                    }
                    else if (ds_data.Tables[0].Rows[0]["ACCOUNT_FLAG"].ToString() == "IB" || ds_data.Tables[0].Rows[0]["ACCOUNT_FLAG"].ToString() == "OB")
                    {
                        drpAccType.Text = ds.Tables[0].Rows[4]["AutoSearchResult"].ToString();
                    }
                    drpAccType.Enabled = false;
                    if (drpAccType.Text != "")
                    {
                        DataSet ds_ACE = objChartofAccountStoredProcedure.get_all_names("FETCH_ALL_NAMES", drpAccType.Text);
                        binddropdownlist(drpACE, ds_ACE);
                        drpACE.Text = ds_data.Tables[0].Rows[0]["SUPPLIER_AGENT_ID"].ToString();
                        drpACE.Enabled = false;
                    }

                    SAP_autoname_edit();

                    txtGlcode.Text = ds_data.Tables[0].Rows[0]["GL_CODE"].ToString();

                    txtAccountname.Text = ds_data.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();
                    ViewState["OLD_ACCOUNT_NAME"] = txtAccountname.Text;
                   

                    drpQuestion.SelectedItem.Text  = ds_data.Tables[0].Rows[0]["GROUP_NAME"].ToString();
                    ViewState["GROUP_NAME"] = ds_data.Tables[0].Rows[0]["GROUP_NAME"].ToString();

                    txtOpbalance.Text = ds_data.Tables[0].Rows[0]["OP_BALANCE"].ToString();

                    txtOPdate.Text = ds_data.Tables[0].Rows[0]["OP_DATE"].ToString();

                    DropDownList2.Text = ds_data.Tables[0].Rows[0]["OP_BAL_TYPE"].ToString();

                    txtIncometax.Text = ds_data.Tables[0].Rows[0]["IT_NO"].ToString();

                    txtSalestax.Text = ds_data.Tables[0].Rows[0]["SALES_TAX_NO"].ToString();


               //     string termsid = ds_data.Tables[0].Rows[0]["FILE_PATH"].ToString();
               
                termcondition.HRef = "~/ChartofAccountDoc/" + int.Parse(Request.QueryString["CAID"].ToString()) + "/" + ds_data.Tables[0].Rows[0]["FILE_PATH"].ToString();
                
                }
               
            }

        }

        #region METHOD OF BIND DROP DOWNS
        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            //  r.SelectedValue = "1";
        }
        #endregion

        protected void drpAccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpAccType.Text != "")
            {
                DataSet ds = objChartofAccountStoredProcedure.get_all_names("FETCH_ALL_NAMES_FIRST_TIME", drpAccType.Text);
                binddropdownlist(drpACE, ds);
                trACE.Visible = true;
            }
            else
            {
                txtAccountname.Text = "";
                trACE.Visible = false;

            }

            if (drpAccType.Text == "Agent")
            {
                lblASE.Text = "Agent Company Name";

              

            }
            else if (drpAccType.Text == "Supplier")
            {
                lblASE.Text = "Supplier Company Name";
            }
            else if (drpAccType.Text == "Employee")
            {
                lblASE.Text = "Employee Name";
            }
            else if (drpAccType.Text == "Product")
            {
                lblASE.Text = "Product Name";
            }

            UpdatePanel1.Update();
         
        }

        protected void drpACE_SelectedIndexChanged(object sender, EventArgs e)
        {
            SAP_autoname();
        }

        #region SAVE RECORDS
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ViewState["ID"] == null)
            {
                ViewState["ID"] = "0";
            }

            if (ViewState["ID"].ToString() == "")
            {
                ViewState["ID"] = "0";
            }

            if (ViewState["FLAG"] == null)
            {
                ViewState["FLAG"] = "";
            }



            if (Request["CAID"] != null && !string.IsNullOrEmpty(Request["CAID"].ToString()))
            {
                
                validation(txtAccountname.Text);
                string filename = "";
                
                if (falg_gldesc == false)
                {
                    Master.DisplayMessage("Account Name is already exist. Choose Another Name.", "successMessage", 5000);
                }
                else
                {
                    if (ViewState["GROUP_NAME"].ToString() != drpQuestion.Text || txtGlcode.Text == ""  )
                    {
                        txtGlcode.Text = GROUP_CODE();
                    }
                    objChartofAccountStoredProcedure.update_chart_of_account(int.Parse(Request.QueryString["CAID"].ToString()), txtGlcode.Text, txtAccountname.Text, drpQuestion.Text, 0, txtOpbalance.Text, DropDownList2.Text, txtOPdate.Text, Session["COMPANY_NAME"].ToString(), ViewState["ID"].ToString(), ViewState["FLAG"].ToString(), "", txtIncometax.Text, txtSalestax.Text, int.Parse(Session["usersid"].ToString()));

                    if (!System.IO.Directory.Exists(Server.MapPath("~/ChartofAccountDoc/" + Request.QueryString["CAID"].ToString() + "/")))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/ChartofAccountDoc/" + Request.QueryString["CAID"].ToString() + "/"));
                    }
                    foreach (UploadedFile f in upTermsConditionsAttachement.UploadedFiles)
                    {
                        filename = f.GetName().ToString();
                        f.SaveAs(Server.MapPath("~/ChartofAccountDoc/" + Request.QueryString["CAID"].ToString() + "/") + Request.QueryString["CAID"].ToString() + filename);

                        objChartofAccountStoredProcedure.insert_chart_of_file(int.Parse(Request.QueryString["CAID"].ToString()), Request.QueryString["CAID"].ToString() + filename);

                    }

                    if (ViewState["ID"] != null && ViewState["FLAG"] != null)
                    {
                        update_masters(int.Parse(ViewState["ID"].ToString()), ViewState["FLAG"].ToString());
                    }
                    Master.DisplayMessage("Record Updated SuccesFully", "successMessage", 5000);
                }
                
            }
            else
            {
                validation(txtAccountname.Text);
                if (falg_gldesc == false)
                {
                    Master.DisplayMessage("Account Name is already exist. Choose Another Name.", "successMessage", 5000);
                   
                 
                }
                else
                {
                    string filename = "";

                    if (txtGlcode.Text == "" && drpQuestion.Text != "")
                    {
                        txtGlcode.Text = GROUP_CODE();
                    }

                    objChartofAccountStoredProcedure.insert_chart_of_account(txtGlcode.Text, txtAccountname.Text, drpQuestion.Text, 0, txtOpbalance.Text, DropDownList2.Text, txtOPdate.Text, Session["COMPANY_NAME"].ToString(), ViewState["ID"].ToString(), ViewState["FLAG"].ToString(), "", txtIncometax.Text, txtSalestax.Text, int.Parse(Session["usersid"].ToString()));
                    DataSet ds1 = objGroupTypeMasterStoredProcedure.get_account_code("GET_MAX_CHART_OF_ACCOUNT_ID");
                    if (!System.IO.Directory.Exists(Server.MapPath("~/ChartofAccountDoc/" + ds1.Tables[0].Rows[0]["MAX_ID"].ToString() + "/")))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/ChartofAccountDoc/" + ds1.Tables[0].Rows[0]["MAX_ID"].ToString() + "/"));
                    }
                    foreach (UploadedFile f in upTermsConditionsAttachement.UploadedFiles)
                    {
                        filename = f.GetName().ToString();
                        f.SaveAs(Server.MapPath("~/ChartofAccountDoc/" + ds1.Tables[0].Rows[0]["MAX_ID"].ToString() + "/") + ds1.Tables[0].Rows[0]["MAX_ID"].ToString() + filename);
                        
                        objChartofAccountStoredProcedure.insert_chart_of_file(int.Parse(ds1.Tables[0].Rows[0]["MAX_ID"].ToString()), ds1.Tables[0].Rows[0]["MAX_ID"].ToString() + filename);

                    }

                    if (ViewState["ID"] != null && ViewState["FLAG"] != null)
                    {
                        update_masters(int.Parse(ViewState["ID"].ToString()), ViewState["FLAG"].ToString());
                    }
                    clear_hide();

                    Master.DisplayMessage("Record Saved SuccesFully", "successMessage", 5000);

                    ViewState["ID"] = null;
                    ViewState["FLAG"] = null;
                }
            }

          

          
        }
        #endregion

        #region DROP DOWN BINDING METHODS

        protected void bindComboBoxforquestion(RadComboBox r, DataSet d)
        {
            r.Items.Clear();
            r.DataTextField = "MAIN_GROUP";
           // r.DataValueField = "TEST_QUESTION_SRNO";
            r.DataSource = d;
            r.DataBind();
            r.Items.Insert(0, new RadComboBoxItem("", ""));
            r.SelectedValue = "";
        }

        protected void RadComboBox1_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            //set the Text and Value property of every item
            //here you can set any other properties like Enabled, ToolTip, Visible, etc.
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MAIN_GROUP"].ToString();
            //   e.Item.Value = ((DataRowView)e.Item.DataItem)["TEST_QUESTION_SRNO"].ToString();
        }

        #endregion

        

        public void SAP_autoname()
        {
            DataSet ds = objChartofAccountStoredProcedure.get_all_names("FETCH_ALL_NAMES_FIRST_TIME", drpAccType.Text);
            txtAccountname.Text = drpACE.Text;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["AutoSearchResult"].ToString() == drpACE.Text)
                {
                    if (ds.Tables[0].Rows[i]["ADDRESS"].ToString() != "")
                    {
                        trAddress.Visible = true;
                        lblAddress.Text = ds.Tables[0].Rows[i]["ADDRESS"].ToString();

                    }
                    else
                    {
                        trAddress.Visible = false;
                    }

                    if (ds.Tables[0].Rows[i]["COUNTRY"].ToString() != "")
                    {
                        trCountry.Visible = true;
                        lblCountry.Text = ds.Tables[0].Rows[i]["COUNTRY"].ToString();

                    }
                    else
                    {
                        trCountry.Visible = false;
                    }

                    if (ds.Tables[0].Rows[i]["STATES"].ToString() != "")
                    {
                        trState.Visible = true;
                        lblState.Text = ds.Tables[0].Rows[i]["STATES"].ToString();

                    }
                    else
                    {
                        trState.Visible = false;
                    }

                    if (ds.Tables[0].Rows[i]["CITY"].ToString() != "")
                    {
                        trcity.Visible = true;
                        lblCity.Text = ds.Tables[0].Rows[i]["CITY"].ToString();

                    }
                    else
                    {
                        trcity.Visible = false;
                    }

                    if (ds.Tables[0].Rows[i]["EMAIL"].ToString() != "")
                    {
                        trEmail.Visible = true;
                        lblEmail.Text = ds.Tables[0].Rows[i]["EMAIL"].ToString();

                    }
                    else
                    {
                        trEmail.Visible = false;
                    }

                    if (ds.Tables[0].Rows[i]["PHONE"].ToString() != "")
                    {
                        trPhno.Visible = true;
                        lblPhno.Text = ds.Tables[0].Rows[i]["PHONE"].ToString();

                    }
                    else
                    {
                        trPhno.Visible = false;
                    }

                    if (ds.Tables[0].Rows[i]["FAX"].ToString() != "")
                    {
                        trFaxno.Visible = true;
                        lblfaxno.Text = ds.Tables[0].Rows[i]["FAX"].ToString();

                    }
                    else
                    {
                        trFaxno.Visible = false;
                    }
                    ViewState["ID"] = ds.Tables[0].Rows[i]["ID"].ToString();
                    ViewState["FLAG"] = ds.Tables[0].Rows[i]["FLAG"].ToString();
                }
            }

            if (drpAccType.Text == "Supplier")
            {
                DataSet ds1 = objChartofAccountStoredProcedure.get_Supplier_Type(drpACE.Text);

                if (ds1.Tables[0].Rows.Count != 0)
                {
                    if (ds1.Tables[0].Rows[0]["SUPPLIER_TYPE_NAME"].ToString() == "Hotel")
                    {
                        drpQuestion.SelectedItem.Text = "Hotels";
                    }
                    else if (ds1.Tables[0].Rows[0]["SUPPLIER_TYPE_NAME"].ToString() == "Transfer Package Company")
                    {
                        drpQuestion.SelectedItem.Text = "Transfers";
                    }
                    else if (ds1.Tables[0].Rows[0]["SUPPLIER_TYPE_NAME"].ToString() == "Sightseeing Company")
                    {
                        drpQuestion.SelectedItem.Text = "Sightseeings";
                    }

                }
            }

            if (drpAccType.Text == "Agent")
            {
                drpQuestion.SelectedItem.Text = "Agents";
            }


           

            UpdatePanel1.Update();
            UpdatePanel2.Update();
        }

        public void SAP_autoname_edit()
        {
            DataSet ds = objChartofAccountStoredProcedure.get_all_names("FETCH_ALL_NAMES", drpAccType.Text);
            txtAccountname.Text = drpACE.Text;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["AutoSearchResult"].ToString() == drpACE.Text)
                {
                    if (ds.Tables[0].Rows[i]["ADDRESS"].ToString() != "")
                    {
                        trAddress.Visible = true;
                        lblAddress.Text = ds.Tables[0].Rows[i]["ADDRESS"].ToString();

                    }
                    else
                    {
                        trAddress.Visible = false;
                    }

                    if (ds.Tables[0].Rows[i]["COUNTRY"].ToString() != "")
                    {
                        trCountry.Visible = true;
                        lblCountry.Text = ds.Tables[0].Rows[i]["COUNTRY"].ToString();

                    }
                    else
                    {
                        trCountry.Visible = false;
                    }

                    if (ds.Tables[0].Rows[i]["STATES"].ToString() != "")
                    {
                        trState.Visible = true;
                        lblState.Text = ds.Tables[0].Rows[i]["STATES"].ToString();

                    }
                    else
                    {
                        trState.Visible = false;
                    }

                    if (ds.Tables[0].Rows[i]["CITY"].ToString() != "")
                    {
                        trcity.Visible = true;
                        lblCity.Text = ds.Tables[0].Rows[i]["CITY"].ToString();

                    }
                    else
                    {
                        trcity.Visible = false;
                    }

                    if (ds.Tables[0].Rows[i]["EMAIL"].ToString() != "")
                    {
                        trEmail.Visible = true;
                        lblEmail.Text = ds.Tables[0].Rows[i]["EMAIL"].ToString();

                    }
                    else
                    {
                        trEmail.Visible = false;
                    }

                    if (ds.Tables[0].Rows[i]["PHONE"].ToString() != "")
                    {
                        trPhno.Visible = true;
                        lblPhno.Text = ds.Tables[0].Rows[i]["PHONE"].ToString();

                    }
                    else
                    {
                        trPhno.Visible = false;
                    }

                    if (ds.Tables[0].Rows[i]["FAX"].ToString() != "")
                    {
                        trFaxno.Visible = true;
                        lblfaxno.Text = ds.Tables[0].Rows[i]["FAX"].ToString();

                    }
                    else
                    {
                        trFaxno.Visible = false;
                    }
                    ViewState["ID"] = ds.Tables[0].Rows[i]["ID"].ToString();
                    ViewState["FLAG"] = ds.Tables[0].Rows[i]["FLAG"].ToString();
                }
            }

            UpdatePanel1.Update();
            UpdatePanel2.Update();
        }

        protected void txtAccountname_TextChanged(object sender, EventArgs e)
        {
            validation(txtAccountname.Text);
            if (falg_gldesc == false)
            {
                Master.DisplayMessage("Name is already exist.", "successMessage", 5000);
                txtAccountname.Text = "";
                UpdatePanel1.Update();
            }
        }

        public void validation(string name)
        {
            DataSet ds = objGroupTypeMasterStoredProcedure.get_account_code("GET_GLDESCRIPTION");
            if (Request["CAID"] != null && !string.IsNullOrEmpty(Request["CAID"].ToString()))
            {
                if (ViewState["OLD_ACCOUNT_NAME"].ToString() != name)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (name == ds.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString())
                        {
                            falg_gldesc = false;
                        }
                    }
                }
            }
            else
            {


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (name == ds.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString())
                    {
                        falg_gldesc = false;
                    }
                }
            }
        }

        protected void clear_hide()
        {
            drpAccType.Text = "";
            drpACE.Text = "";
            drpQuestion.SelectedItem.Text = "";

            txtAccountname.Text = "";
            txtGlcode.Text = "";
            txtIncometax.Text ="";
            txtOpbalance.Text = "";
            txtOPdate.Text = "";
            txtSalestax.Text = "";

            trACE.Visible = false;
            trAddress.Visible = false;
            trcity.Visible = false;
            trCountry.Visible = false;
            trPhno.Visible = false;
            trEmail.Visible = false;
            trFaxno.Visible = false;

            DropDownList2.Text = "";
            

            UpdatePanel1.Update();
            UpdatePanel2.Update();
               

        }

        public string GROUP_CODE()
        {
            string gl_code = "";
            DataSet ds_companyid = objChartofAccountStoredProcedure.get_CompanyID(Session["COMPANY_NAME"].ToString());

            DataSet code = objChartofAccountStoredProcedure.get_Max_GLCode(drpQuestion.Text);

            string no = ds_companyid.Tables[0].Rows[0]["COMPANY_ID"].ToString();
            int len = ds_companyid.Tables[0].Rows[0]["COMPANY_ID"].ToString().Length;
            for (int i = 0; i < 2 - len; i++)
            {
                no = "0" + no;
            }

            if (code.Tables[1].Rows[0]["CODE"].ToString() == "")
            {
                gl_code = no + code.Tables[0].Rows[0]["GROUP_CODE"].ToString() + "000001";
            }
            else
            {
                decimal digit = decimal.Parse(code.Tables[1].Rows[0]["CODE"].ToString()) % 1000000 + 1 ;
                string no1 = digit.ToString();
                int len1 = digit.ToString().Length;

                for (int i = 0; i < 6 - len1; i++)
                {
                    no1 = "0" + no1;
                }

                gl_code = no + code.Tables[0].Rows[0]["GROUP_CODE"].ToString() + no1;
            }

            return gl_code;
        }

        protected void update_masters(int emp_id, string flag)
        {
            //if (drpAccType.Text != "")
            //{

            //}
            if (drpAccType.Text == "Supplier" && flag=="S")
            {
                objChartofAccountStoredProcedure.update_supplier_master(emp_id, txtGlcode.Text);   
            }
            else if (drpAccType.Text == "Agent" && flag == "A")
            {
                objChartofAccountStoredProcedure.update_agent_master(emp_id, txtGlcode.Text);
            }
            else if (drpAccType.Text == "Employee" && flag == "E")
            {
                objChartofAccountStoredProcedure.update_employee_master(emp_id, txtGlcode.Text);
            }
            else if (drpAccType.Text == "Product" && flag == "P")
            {
                objChartofAccountStoredProcedure.update_product_master(emp_id, txtGlcode.Text);
            }
            else if (drpAccType.Text == "Banks" && flag == "IB" || flag == "OB")
            {
                objChartofAccountStoredProcedure.update_company_bank_master(emp_id, txtGlcode.Text);
            }
        }
    }
}