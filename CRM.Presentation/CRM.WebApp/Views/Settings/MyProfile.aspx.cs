using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using CRM.DataAccess.SecurityDAL;
using CRM.DataAccess.HRDAL;
using CRM.Model.Security;
using CRM.Core.Constants;
using CRM.DataAccess;
using CRM.Model.HRModel;
using CRM.Core.Utility.DateTimeUtility;



namespace CRM.WebApp.Views.Customers
{
    public partial class MyProfile : System.Web.UI.Page
    {
        AuthorizationBDto objAuthorizationBDto;
        EmployeeMasterDal objEmployeeMasterDal = null;
        DataSet DsEmployee = null;
        DataTable dtEmployee = null;
        int EmployeeId = 0;
        int CompanyId = 0;


        #region Page Events

        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
        }
        #endregion

        #region Override Style Sheet Theme

        public override string StyleSheetTheme
        {
            get
            {
                if (HttpContext.Current.Session[PageConstants.ThemeName] == null)
                {
                    return "Default";

                }
                else
                {
                    return HttpContext.Current.Session[PageConstants.ThemeName].ToString();
                }
            }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];

            if (!IsPostBack)
            {
                BindThemeList();
                SetTheme();
                GetEmployeeDetails();
            }

            pnlDisplayInfo.Visible = true;
            pnlEditInfo.Visible = false;

        }

        #endregion

        #region Theme


        protected void btnLoad_Click(object sender, EventArgs e)
        {
            Session["ThemeName"] = radcmbTheme.SelectedValue;

            Response.Redirect("MyProfile.aspx");

        }
        protected void btnSetDefault_Click(object sender, EventArgs e)
        {

            AuthorizationDal objAuthorizationDal = new AuthorizationDal();
            objAuthorizationDal.SetTheme(objAuthorizationBDto.UserProfile.UserId, radcmbTheme.SelectedValue);
            objAuthorizationBDto.UserProfile.DefaultTheme = radcmbTheme.SelectedValue;
            Session["ThemeName"] = radcmbTheme.SelectedValue;
            Response.Redirect("MyProfile.aspx");

        }
        protected void SetTheme()
        {

            radcmbTheme.SelectedValue = Session["ThemeName"].ToString();


        }
        protected void BindThemeList()
        {

            radcmbTheme.Items.Clear();

            DataTable dt = new DataTable();

            DataColumn dc1 = new DataColumn("ThemeName");
            DataColumn dc2 = new DataColumn("ThemeValue");
            DataColumn dc3 = new DataColumn("ThemeImage");

            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);


            DataRow dr1 = dt.NewRow();
            dr1["ThemeName"] = "Default";
            dr1["ThemeValue"] = "Default";
            dr1["ThemeImage"] = "~/Views/Shared/Images/Background/defult.jpg";
            dt.Rows.Add(dr1);


            dr1 = dt.NewRow();
            dr1["ThemeName"] = "Blue";
            dr1["ThemeValue"] = "Blue";
            dr1["ThemeImage"] = "~/Views/Shared/Images/Background/blue.jpg";
            dt.Rows.Add(dr1);



            dr1 = dt.NewRow();
            dr1["ThemeName"] = "Dark Orange";
            dr1["ThemeValue"] = "DarkOrange";
            dr1["ThemeImage"] = "~/Views/Shared/Images/Background/dark_orenge.jpg";
            dt.Rows.Add(dr1);



            dr1 = dt.NewRow();
            dr1["ThemeName"] = "Dark Pink";
            dr1["ThemeValue"] = "DarkPink";
            dr1["ThemeImage"] = "~/Views/Shared/Images/Background/dark_pink.jpg";
            dt.Rows.Add(dr1);



            dr1 = dt.NewRow();
            dr1["ThemeName"] = "Dark Red";
            dr1["ThemeValue"] = "DarkRed";
            dr1["ThemeImage"] = "~/Views/Shared/Images/Background/dark_red.jpg";
            dt.Rows.Add(dr1);


            dr1 = dt.NewRow();
            dr1["ThemeName"] = "Green";
            dr1["ThemeValue"] = "Green";
            dr1["ThemeImage"] = "~/Views/Shared/Images/Background/green.jpg";
            dt.Rows.Add(dr1);




            dr1 = dt.NewRow();
            dr1["ThemeName"] = "Light Blue";
            dr1["ThemeValue"] = "LightBlue";

            dr1["ThemeImage"] = "~/Views/Shared/Images/Background/light_blue.jpg";
            dt.Rows.Add(dr1);



            // dr1 = dt.NewRow();
            //dr1["ThemeName"] = "Light Orange";
            //dr1["ThemeValue"] = "LightOrange";
            //dr1["ThemeImage"] = "~/Views/Shared/Images/Background/blue.jpg";
            //dt.Rows.Add(dr1);




            dr1 = dt.NewRow();
            dr1["ThemeName"] = "Marun";
            dr1["ThemeValue"] = "Marun";
            dr1["ThemeImage"] = "~/Views/Shared/Images/Background/marun.jpg";
            dt.Rows.Add(dr1);



            dr1 = dt.NewRow();
            dr1["ThemeName"] = "Orange Green";
            dr1["ThemeValue"] = "OrangeGreen";
            dr1["ThemeImage"] = "~/Views/Shared/Images/Background/orenge-green.jpg";
            dt.Rows.Add(dr1);



            radcmbTheme.DataSource = dt;
            radcmbTheme.DataValueField = "ThemeValue";
            radcmbTheme.DataTextField = "ThemeName";
            radcmbTheme.DataBind();







            //RadComboBoxItem radcmbItem = new RadComboBoxItem();
            //radcmbItem.Text = "Default";
            //radcmbItem.Value = "Default";
            //radcmbTheme.Items.Add(radcmbItem);


            //radcmbItem = new RadComboBoxItem();
            //radcmbItem.Text = "Blue";
            //radcmbItem.Value = "Blue";
            //radcmbTheme.Items.Add(radcmbItem);

            //radcmbItem = new RadComboBoxItem();
            //radcmbItem.Text = "Dark Orange";
            //radcmbItem.Value = "DarkOrange";
            //radcmbTheme.Items.Add(radcmbItem);

            //radcmbItem = new RadComboBoxItem();
            //radcmbItem.Text = "Dark Pink";
            //radcmbItem.Value = "DarkPink";
            //radcmbTheme.Items.Add(radcmbItem);

            //radcmbItem = new RadComboBoxItem();
            //radcmbItem.Text = "Dark Red";
            //radcmbItem.Value = "DarkRed";
            //radcmbTheme.Items.Add(radcmbItem);

            //radcmbItem = new RadComboBoxItem();
            //radcmbItem.Text = "Green";
            //radcmbItem.Value = "Green";
            //radcmbTheme.Items.Add(radcmbItem);

            //radcmbItem = new RadComboBoxItem();
            //radcmbItem.Text = "Light Blue";
            //radcmbItem.Value = "LightBlue";
            //radcmbTheme.Items.Add(radcmbItem);

            //radcmbItem = new RadComboBoxItem();
            //radcmbItem.Text = "Light Orange";
            //radcmbItem.Value = "LightOrange";
            //radcmbTheme.Items.Add(radcmbItem);

            //radcmbItem = new RadComboBoxItem();
            //radcmbItem.Text = "Marun";
            //radcmbItem.Value = "Marun";
            //radcmbTheme.Items.Add(radcmbItem);

            //radcmbItem = new RadComboBoxItem();
            //radcmbItem.Text = "Orange Green";
            //radcmbItem.Value = "OrangeGreen";
            //radcmbTheme.Items.Add(radcmbItem);


        }

        #endregion

        #region  Profile
        protected void GetEmployeeDetails()
        {

            DataSet DsEmployee = null;
            DataSet DsUsercredential = null;
            objEmployeeMasterDal = new EmployeeMasterDal();
            DsEmployee = new DataSet();

            DsUsercredential = objEmployeeMasterDal.GetEmployeeIDByUserId(objAuthorizationBDto.UserProfile.UserId);
            EmployeeId = Convert.ToInt32(DsUsercredential.Tables[0].Rows[0]["emp_ID"]);
            Session["EmpId"] = EmployeeId;


            if (DsUsercredential.Tables[0].Rows.Count > 0)
            {
                lblUserName.Text = DsUsercredential.Tables[0].Rows[0]["USER_NAME"].ToString();
                lblSecurityQuestion.Text = DsUsercredential.Tables[0].Rows[0]["SECURITY_QUESTION_DESC"].ToString();
                lblSecurityAnswers.Text = DsUsercredential.Tables[0].Rows[0]["SECURITY_ANSWERS"].ToString();

                ViewState["UserCredential"] = DsUsercredential.Tables[0];
            }

            if (objAuthorizationBDto != null)
            {

                CompanyId = objAuthorizationBDto.UserSelectedCompanyId;

                DsEmployee = objEmployeeMasterDal.GetEmployeeById(EmployeeId, CompanyId);
                dtEmployee = new DataTable();
                dtEmployee = DsEmployee.Tables[0];

                if (dtEmployee.Rows.Count > 0)
                {
                    ViewState["Profile"] = dtEmployee;
                    ViewState["Profile"] = dtEmployee;
                    lblTitle.Text = dtEmployee.Rows[0]["TITLE_DESC"].ToString();
                    lblName.Text = dtEmployee.Rows[0]["EMP_NAME"].ToString();
                    lblSurName.Text = dtEmployee.Rows[0]["EMP_SURNAME"].ToString();
                    lblDob.Text = dtEmployee.Rows[0]["EMP_DOB"].ToString();
                    lblDepartment.Text = dtEmployee.Rows[0]["DEPARTMENT_NAME"].ToString();
                    lblDesignation.Text = dtEmployee.Rows[0]["DESIGNATION_DESC"].ToString();
                    lblmgr.Text = dtEmployee.Rows[0]["ManagerName"].ToString();
                    lbldoj.Text = dtEmployee.Rows[0]["EMP_DOJ"].ToString();
                    lblmState.Text = dtEmployee.Rows[0]["MARITAL_STATUS_NAME"].ToString();
                    lblGender.Text = dtEmployee.Rows[0]["EMP_GENDER"].ToString();
                    lblQualification.Text = dtEmployee.Rows[0]["QUALIFICATION_NAME"].ToString();
                    lblMobile.Text = dtEmployee.Rows[0]["EMP_MOBILE"].ToString();
                    lblPhone.Text = dtEmployee.Rows[0]["EMP_PHONE"].ToString();
                    lblEmail.Text = dtEmployee.Rows[0]["EMP_EMAIL"].ToString();
                    lblStatus.Text = dtEmployee.Rows[0]["STATUS_NAME"].ToString();


                }
                else
                {


                    ClearEmployee();

                }

            }
            else
            {
                ClearEmployee();

            }


        }

        protected void btnSaveInfo_Click(object sender, EventArgs e)
        {

            int result = 0;
            objEmployeeMasterDal = new EmployeeMasterDal();
            EmployeeBDto objEmployeeBDto = new EmployeeBDto();
            // objEmployeeBDto.TitleId = Convert.ToInt32(radCmbTitleAdd.SelectedValue);
            objEmployeeBDto.MaritalStatusId = Convert.ToInt32(radCmbMaritalStatusAdd.SelectedValue);
            objEmployeeBDto.QualificationId = Convert.ToInt32(radCmbQualificationAdd.SelectedValue);
            objEmployeeBDto.Mobile = txteditMobile.Text;
            objEmployeeBDto.Phone = txtEditPhone.Text;
            objEmployeeBDto.Email = txtEditEmail.Text;
            objEmployeeBDto.EmpId = Convert.ToInt32(Session["EmpId"]);

            objEmployeeBDto.DateofBirth = RadDateDobAdd.SelectedDate.Value; ;
            objEmployeeBDto.Gender = rblstPerGender.SelectedValue; ;



            result = objEmployeeMasterDal.UpdateEmployeeProfile(objEmployeeBDto);
            if (result == 1)
            {
                rblstPerGender.ClearSelection();
                ViewState["Profile"] = null;
                Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                Master.MessageCssClass = "successMessage";
                GetEmployeeDetails();
                pnlEditInfo.Visible = false;
                pnlDisplayInfo.Visible = true;

            }
            else
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                Master.MessageCssClass = "errorMessage";
            }


        }
        protected void btnCancelInfo_Click(object sender, EventArgs e)
        {
            pnlDisplayInfo.Visible = true;
            pnlEditInfo.Visible = false;

        }
        protected void btnEditPersonalDtl_Click(object sender, EventArgs e)
        {

            pnlDisplayInfo.Visible = false;
            pnlEditInfo.Visible = true;
            FillCmbList();

            dtEmployee = new DataTable();
            dtEmployee = (DataTable)ViewState["Profile"];







            //radCmbTitleAdd.SelectedValue = dtEmployee.Rows[0]["EMP_TITLE_ID"].ToString();
            lblTitleAdd.Text = dtEmployee.Rows[0]["TITLE_DESC"].ToString();
            lblEditName.Text = dtEmployee.Rows[0]["EMP_NAME"].ToString();
            lblEditSurname.Text = dtEmployee.Rows[0]["EMP_SURNAME"].ToString();
            //lbleditdob.Text = dtEmployee.Rows[0]["EMP_DOB"].ToString();
            lbleditdept.Text = dtEmployee.Rows[0]["DEPARTMENT_NAME"].ToString();
            lbleditdesig.Text = dtEmployee.Rows[0]["DESIGNATION_DESC"].ToString();
            lbleditmgr.Text = dtEmployee.Rows[0]["ManagerName"].ToString();
            lbleditdoj.Text = dtEmployee.Rows[0]["EMP_DOJ"].ToString();
            radCmbMaritalStatusAdd.SelectedValue = dtEmployee.Rows[0]["EMP_MARITAL_STATUS"].ToString();
            // lbleditgender.Text = dtEmployee.Rows[0]["EMP_GENDER"].ToString();
            radCmbQualificationAdd.SelectedValue = dtEmployee.Rows[0]["EMP_QUALIFICATION_ID"].ToString();
            txteditMobile.Text = dtEmployee.Rows[0]["EMP_MOBILE"].ToString();
            txtEditPhone.Text = dtEmployee.Rows[0]["EMP_PHONE"].ToString();
            txtEditEmail.Text = dtEmployee.Rows[0]["EMP_EMAIL"].ToString();
            lbleditstatus.Text = dtEmployee.Rows[0]["STATUS_NAME"].ToString();
            RadDateDobAdd.SelectedDate = DateTimeHelper.ConvertToMmDdYyyy(dtEmployee.Rows[0]["EMP_DOB"].ToString());

            rblstPerGender.Items.FindByValue(dtEmployee.Rows[0]["Gender"].ToString()).Selected = true;




        }
        #region Binding List
        private void FillCmbList()
        {

            BindTitle();
            BindMaritalStatus();
            BindQualification();

        }
        private void BindTitle()
        {
            BindCombo objCmbBind = new BindCombo();
            DataSet DsCmb = null;
            DsCmb = objCmbBind.GetTitle();

            radCmbTitleAdd.DataSource = DsCmb;
            radCmbTitleAdd.DataTextField = "TITLE_DESC";
            radCmbTitleAdd.DataValueField = "TITLE_ID";
            radCmbTitleAdd.DataBind();
            objCmbBind = null;
            DsCmb = null;
            radCmbTitleAdd.Items.Insert(0, new RadComboBoxItem("", "0"));

        }
        private void BindMaritalStatus()
        {

            BindCombo objCmbBind = new BindCombo();
            DataSet DsCmb = null;
            DsCmb = objCmbBind.GetMaritalStatus();

            radCmbMaritalStatusAdd.DataSource = DsCmb;
            radCmbMaritalStatusAdd.DataTextField = "MARITAL_STATUS_NAME";
            radCmbMaritalStatusAdd.DataValueField = "MARITAL_STATUS_ID";
            radCmbMaritalStatusAdd.DataBind();
            objCmbBind = null;
            DsCmb = null;
            radCmbMaritalStatusAdd.Items.Insert(0, new RadComboBoxItem("", "0"));

        }
        private void BindQualification()
        {

            BindCombo objCmbBind = new BindCombo();
            DataSet DsCmb = null;
            DsCmb = objCmbBind.GetQualificationKeyValue(); ;

            radCmbQualificationAdd.DataSource = DsCmb;
            radCmbQualificationAdd.DataTextField = "QUALIFICATION_NAME";
            radCmbQualificationAdd.DataValueField = "QUALIFICATION_ID";
            radCmbQualificationAdd.DataBind();
            objCmbBind = null;
            DsCmb = null;
            radCmbQualificationAdd.Items.Insert(0, new RadComboBoxItem("", "0"));

        }
        #endregion
        #endregion

        #region Credential Details

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            DataTable EmpCredential = new DataTable();
            EmpCredential = (DataTable)ViewState["UserCredential"];
            PnlCredencial.Visible = false;
            PnlEditCredencial.Visible = true;
            BindSecurityQuestion();
            lblEditusername.Text = EmpCredential.Rows[0]["USER_NAME"].ToString();
            txtEditSecurityAnswers.Text = EmpCredential.Rows[0]["SECURITY_ANSWERS"].ToString();
            radCmbEditSecurityQuestion.SelectedValue = EmpCredential.Rows[0]["SECURITY_QUESTION_ID"].ToString();


        }
        protected void btnSavePassword_Click(object sender, EventArgs e)
        {
            int result = 0;

            EmployeeMasterDal objEmployeeMasterDal = new EmployeeMasterDal();
            if (txtEditNewPassword.Text != "" && txtEditConfirmPassword.Text != "")
            {
                if (txtEditCurrentPassword.Text != ((DataTable)ViewState["UserCredential"]).Rows[0]["Password"].ToString())
                {
                    showMessage("Old password do not match!!!");

                }
                else
                {

                    result = objEmployeeMasterDal.UpdateEmployeeCredential(objAuthorizationBDto.UserProfile.UserId, Convert.ToInt32(radCmbEditSecurityQuestion.SelectedValue), txtEditSecurityAnswers.Text, txtEditNewPassword.Text);

                    if (result == 1)
                    {
                        Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                        Master.MessageCssClass = "successMessage";

                        GetEmployeeDetails();
                        PnlEditCredencial.Visible = false;
                        PnlCredencial.Visible = true;
                        ClearPasswordControl();
                    }
                    else
                    {
                        Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                        Master.MessageCssClass = "errorMessage";
                    }

                }
            }
            else
            {

                result = objEmployeeMasterDal.UpdateEmployeeCredential(objAuthorizationBDto.UserProfile.UserId, Convert.ToInt32(radCmbEditSecurityQuestion.SelectedValue), txtEditSecurityAnswers.Text, txtEditNewPassword.Text);
                if (result == 1)
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                    Master.MessageCssClass = "successMessage";

                    GetEmployeeDetails();
                    PnlEditCredencial.Visible = false;
                    PnlCredencial.Visible = true;
                    ClearPasswordControl();
                }
                else
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                    Master.MessageCssClass = "errorMessage";

                }



            }






        }
        protected void btnCancelpassword_Click(object sender, EventArgs e)
        {
            PnlCredencial.Visible = true;
            PnlEditCredencial.Visible = false;
            ClearPasswordControl();
        }


        #region  Fill List

        private void BindSecurityQuestion()
        {
            BindCombo objCmbBind = new BindCombo();
            DataSet DsCmb = null;
            DsCmb = objCmbBind.GetSecurityQuestion("");

            radCmbEditSecurityQuestion.DataSource = DsCmb;
            radCmbEditSecurityQuestion.DataTextField = "SECURITY_QUESTION_DESC";
            radCmbEditSecurityQuestion.DataValueField = "SECURITY_QUESTION_ID";
            radCmbEditSecurityQuestion.DataBind();
            objCmbBind = null;
            DsCmb = null;
            radCmbEditSecurityQuestion.Items.Insert(0, new RadComboBoxItem("", "0"));

        }
        #endregion

        #endregion
        public void showMessage(String Message)
        {
            string radalertscript = "<script language='javascript'>function f(){radalert('" + Message + "', 330, 110, 'Warning Message'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", radalertscript, false);

        }

        public void ClearPasswordControl()
        {
            txtEditConfirmPassword.Text = "";
            txtEditCurrentPassword.Text = "";
            txtEditNewPassword.Text = "";
            radCmbEditSecurityQuestion.SelectedValue = "0";
            txtEditSecurityAnswers.Text = "";

        }

        private void ClearEmployee()
        {

            lblTitle.Text = "";
            lblName.Text = "";
            lblSurName.Text = "";
            lblDob.Text = "";
            lblDepartment.Text = "";
            lblDesignation.Text = "";
            lblmgr.Text = "";
            lbldoj.Text = "";
            lblmState.Text = "";
            lblGender.Text = "";
            lblQualification.Text = "";
            lblMobile.Text = "";
            lblPhone.Text = "";
            lblEmail.Text = "";
            lblStatus.Text = "";

        }


        protected void radcmbTheme_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            try
            {

                if (e.Item.FindControl("imgTheme") != null)
                {
                    Image img = (Image)e.Item.FindControl("imgTheme");
                    img.ImageUrl = DataBinder.Eval(e.Item.DataItem, "ThemeImage").ToString();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
