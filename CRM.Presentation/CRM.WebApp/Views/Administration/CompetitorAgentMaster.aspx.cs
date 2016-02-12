using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using CRM.DataAccess.AdministrationDAL;
using CRM.Model.Security;
using CRM.Core.Constants;
using System.Text;
using CRM.Model.AdministrationModel;
using Telerik.Web.UI;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;
using System.Web;
using System.Web.UI;


namespace CRM.WebApp.Views.Administration
{
    public partial class CompetitorAgentMaster : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        CompetitorAgentMasterDal objCompetitorAgentMasterDal = null;
        public const String vsAgent = "Agent";
        AuthorizationBDto objAuthorizationBDto;
        CompetitorAgentMasterBDto objCompetitorAgentMasterBDto = null;
        #endregion

        #region Page Events

        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindGrid();

            }
            if (Session[PageConstants.ssnUserAuthorization] != null)
            {
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                Session["currentevent"] = "Competitor";
            }
            hdnEditableMode.Value = "false";
            acbAgent.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateGridForEdit('{0}')", radgrdAgent.ClientID));
            acbAgent.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdAgent.ClientID));

        }

        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Competitor");
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

        #endregion

        #region Method

        #region Bind Grid
        private void BindGrid()
        {
            objCompetitorAgentMasterDal = new CompetitorAgentMasterDal();
            DataSet dsAgent = objCompetitorAgentMasterDal.GetCompetitor("");
            radgrdAgent.DataSource = dsAgent;
            radgrdAgent.DataBind();
            ViewState[vsAgent] = dsAgent;

        }
        #endregion



        #region Save
        private void SaveCompetitor()
        {
            try
            {
                int result = 0;
                objCompetitorAgentMasterDal = new CompetitorAgentMasterDal();
                objCompetitorAgentMasterBDto = new CompetitorAgentMasterBDto();
                objCompetitorAgentMasterBDto.AgentName = txtAgentName.Text.Trim();
                objCompetitorAgentMasterBDto.AgentAddress = txtAddress.Text.Trim();
                objCompetitorAgentMasterBDto.CityId = Convert.ToInt32(radCmbCity.SelectedValue);
                objCompetitorAgentMasterBDto.StateId = Convert.ToInt32(radCmbState.SelectedValue);
                objCompetitorAgentMasterBDto.CountryId = Convert.ToInt32(radCmbCountry.SelectedValue);
                objCompetitorAgentMasterBDto.Phone = txtphone.Text.Trim();
                objCompetitorAgentMasterBDto.OwnerCompanyId = objAuthorizationBDto.UserSelectedCompanyId;
                objCompetitorAgentMasterBDto.UserId = objAuthorizationBDto.UserProfile.UserId;


                result = objCompetitorAgentMasterDal.InsertAgent(objCompetitorAgentMasterBDto);
                if (result >= 1)
                {

                    // pnlGrid.Visible = true;

                    Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                    Master.MessageCssClass = "successMessage"; 

                    pnlAddNewMode.Visible = false;
                    acbAgent.EditableMode = false;
                    bisEdit = false;

                }
                else
                {
                    //Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                    Master.DisplayMessage("Agent Name Already Exist");
                    Master.MessageCssClass = "errorMessage";

                }
                BindGrid();
                Reset();


            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }
        #endregion



        #region Update
        private void Update()
        {
            try
            {
                int result = 0;
                objCompetitorAgentMasterDal = new CompetitorAgentMasterDal();
                objCompetitorAgentMasterBDto = new CompetitorAgentMasterBDto();
                objCompetitorAgentMasterBDto.AgentName = txtAgentName.Text.Trim();
                objCompetitorAgentMasterBDto.AgentAddress = txtAddress.Text.Trim();
                objCompetitorAgentMasterBDto.CityId = Convert.ToInt32(radCmbCity.SelectedValue);
                objCompetitorAgentMasterBDto.StateId = Convert.ToInt32(radCmbState.SelectedValue);
                objCompetitorAgentMasterBDto.CountryId = Convert.ToInt32(radCmbCountry.SelectedValue);
                objCompetitorAgentMasterBDto.Phone = txtphone.Text.Trim();
                objCompetitorAgentMasterBDto.OwnerCompanyId = objAuthorizationBDto.UserSelectedCompanyId;
                objCompetitorAgentMasterBDto.UserId = objAuthorizationBDto.UserProfile.UserId;
                objCompetitorAgentMasterBDto.AgentId = Convert.ToInt32(ViewState["AgentId"]);

                result = objCompetitorAgentMasterDal.UpdateAgent(objCompetitorAgentMasterBDto);
                if (result >= 1)
                {
                    pnlAddNewMode.Visible = false;
                    acbAgent.EditableMode = false;
                    BindGrid();
                    Reset();

                    Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                    Master.MessageCssClass = "successMessage";
                }
                else
                {
                    Master.DisplayMessage("Agent Name Already Exist");
                    //Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                    Master.MessageCssClass = "errorMessage";
                }

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }
        #endregion

        #region Reset
        private void Reset()
        {

            txtAgentName.Text = "";
            txtAddress.Text = "";
            txtphone.Text = "";
            radCmbCity.Text = "";
            radCmbCity.SelectedValue = "0";
            radCmbCountry.SelectedValue = "0";
            radCmbCountry.Text = "";
            radCmbState.Text = "";
            radCmbState.SelectedValue = "0";

        }
        #endregion


        #region Bind Combobox

        private void BindCountry()
        {
            BindCombo objCmbBind = new BindCombo();
            DataSet DsCmb = null;
            DsCmb = objCmbBind.GetCountryKeyValue();
            //radCmbCountry.Items.Clear();
            radCmbCountry.Items.Add(new RadComboBoxItem("---- Select Country ----","0"));   
            radCmbCountry.DataSource = DsCmb;
            radCmbCountry.DataTextField = "COUNTRY_NAME";
            radCmbCountry.DataValueField = "COUNTRY_ID";
            radCmbCountry.DataBind();
            objCmbBind = null;
            DsCmb = null;
            radCmbCountry.Items.Insert(0, new RadComboBoxItem("", "0"));


        }
        private void BindState(int CountryId)
        {
            BindCombo objCmbBind = new BindCombo();
            DataSet DsCmb = null;
            DsCmb = objCmbBind.GetStateKeyValue(CountryId);
            //radCmbState.Items.Clear();
            radCmbState.Items.Add(new RadComboBoxItem("---- Select Country ----","0"));
            radCmbState.DataSource = DsCmb;
            radCmbState.DataTextField = "STATE_NAME";
            radCmbState.DataValueField = "STATE_ID";
            radCmbState.DataBind();
            objCmbBind = null;
            DsCmb = null;
            radCmbState.Items.Insert(0, new RadComboBoxItem("", "0"));


        }
        private void BindCity(int CountryId, int StateId)
        {
            BindCombo objCmbBind = new BindCombo();
            DataSet DsCmb = null;
            DsCmb = objCmbBind.GetCityKeyValue(CountryId, StateId);
            //radCmbCity.Items.Clear();
            radCmbCity.Items.Add(new RadComboBoxItem("---- Select Country ----", "0"));
            radCmbCity.DataSource = DsCmb;
            radCmbCity.DataTextField = "CITY_NAME";
            radCmbCity.DataValueField = "CITY_ID";
            radCmbCity.DataBind();
            objCmbBind = null;
            DsCmb = null;
            radCmbCity.Items.Insert(0, new RadComboBoxItem("", "0"));
        }

        protected void radCmbCountry_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindState(Convert.ToInt32(radCmbCountry.SelectedValue));
            radCmbCity.Items.Clear();
            radCmbState.Focus();
        }

        protected void radCmbState_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            BindCity(Convert.ToInt32(radCmbCountry.SelectedValue), Convert.ToInt32(radCmbState.SelectedValue));
            radCmbCity.Focus();
        }


        #endregion

        #endregion

        #region Actionbar Events

        protected void acbAgent_NewClick(object sender, EventArgs e)
        {

            acbAgent.SaveButton.ValidationGroup = "Competitor";
            acbAgent.EditableMode = true;
            acbAgent.SaveButton.CommandName = "Save";
            pnlAddNewMode.Visible = true;

            BindCountry();
            //radgrdAgent.Rebind();
            Reset();
            //pnlGrid.Visible = false; 
        }

        protected void acbAgent_SaveClick(object sender, EventArgs e)
        {
            objCompetitorAgentMasterBDto = new CompetitorAgentMasterBDto();


            try
            {
                int result = 0;
                objCompetitorAgentMasterDal = new CompetitorAgentMasterDal();
                switch (acbAgent.SaveButton.CommandName)
                {
                    case "Save":
                      
                        SaveCompetitor();
                        pnlAddNewMode.Visible = false;
                        acbAgent.EditableMode = false;
                        BindGrid();
                        break;

                    case "Update":
                        Update();
                        break;
                }
            }


            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbAgent_SaveNewClick(object sender, EventArgs e)
        {

            SaveCompetitor();
            Reset();
        }

        protected void acbAgent_CancelClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdAgent.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdAgent.Rebind();

            }
            //  pnlGrid.Visible = true;
            acbAgent.DefaultMode = true;
            pnlAddNewMode.Visible = false;
        }

        protected void acbAgent_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder AgentId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {

                        Label lblAgentId = (Label)radgrdAgent.Items[i].FindControl("lblgrdAgentIdItem");
                        if (lblAgentId != null)
                        {
                            AgentId.Append(lblAgentId.Text + ",");
                        }
                    }
                }

                objCompetitorAgentMasterDal = new CompetitorAgentMasterDal();
                String AddId = AgentId.ToString().TrimEnd(',');
                result = objCompetitorAgentMasterDal.DeleteAgent(AddId);


                if (result == 1)
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Delete].ToString());
                    Master.MessageCssClass = "successMessage";
                    ViewState[PageConstants.vsItemIndexes] = null;
                    BindGrid();
                }
                else if (result == 547)
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Delete].ToString());
                    Master.MessageCssClass = "errorMessage";
                }
            }

            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbAgent_SearchClick(object sender, EventArgs e)
        {

            try
            {




                objCompetitorAgentMasterDal = new CompetitorAgentMasterDal();
                DataSet dsAgent = objCompetitorAgentMasterDal.GetCompetitor(acbAgent.SearchTextBox.Text);
                radgrdAgent.DataSource = dsAgent;
                radgrdAgent.DataBind();
                ViewState[vsAgent] = dsAgent;



            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbAgent_EditClick(object sender, EventArgs e)
        {
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                }
                if (htItemIndex != null)
                {
                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblName = (Label)radgrdAgent.Items[Convert.ToInt32(htItemIndex[i].ToString())].FindControl("txtgrdAgentNameItem");
                        txtAgentName.Text = lblName.Text;
                        Label lblAddress = (Label)radgrdAgent.Items[Convert.ToInt32(htItemIndex[i].ToString())].FindControl("lblGrdAddressItem");
                        txtAddress.Text = lblAddress.Text;
                        Label lblCountryId = (Label)radgrdAgent.Items[Convert.ToInt32(htItemIndex[i].ToString())].FindControl("lblGrdCountryIdItem");

                        BindCountry();

                        if (lblCountryId.Text != "")
                        {
                            radCmbCountry.SelectedValue = lblCountryId.Text;
                            Label lblStateId = (Label)radgrdAgent.Items[Convert.ToInt32(htItemIndex[i].ToString())].FindControl("lblGrdStateIdItem");

                            BindState(Convert.ToInt32(lblCountryId.Text));

                            if (lblStateId.Text != "")
                            {
                                radCmbState.SelectedValue = lblStateId.Text;
                                Label lblCityId = (Label)radgrdAgent.Items[Convert.ToInt32(htItemIndex[i].ToString())].FindControl("lblGrdCityIdItem");
                                if (lblCityId.Text != "")
                                {
                                    BindCity(Convert.ToInt32(lblCountryId.Text), Convert.ToInt32(lblStateId.Text));
                                    radCmbCity.SelectedValue = lblCityId.Text;
                                }
                            }

                        }


                        Label lblPhone = (Label)radgrdAgent.Items[Convert.ToInt32(htItemIndex[i].ToString())].FindControl("lblGrdPhoneItem");
                        txtphone.Text = lblPhone.Text;
                        Label lblAgentId = (Label)radgrdAgent.Items[Convert.ToInt32(htItemIndex[i].ToString())].FindControl("lblgrdAgentIdItem");

                        ViewState["AgentId"] = lblAgentId.Text;


                    }
                    bisEdit = true;
                    acbAgent.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbAgent.SaveNewButton.Visible = false;
                    acbAgent.SaveButton.CommandName = "Update";
                    pnlAddNewMode.Visible = true;
                    //pnlGrid.Visible = false;

                }

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbAgent_RefreshClick(object sender, EventArgs e)
        {
            acbAgent.DefaultMode = true;
            pnlAddNewMode.Visible = false;

            acbAgent.SearchTextBox.Text = String.Empty;
            BindGrid();
        }


        #endregion

        #region Grid Event


        protected void radgrdAgent_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdAgent.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdAgent.Items)
                {
                    CheckBox chkItem = (CheckBox)item.FindControl("chkItemWrt");
                    if (chkItem != null)
                        chkItem.Visible = false;
                }
            }
        }


        protected void radgrdAgent_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdAgent.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdAgent.ClientID, chkBox.ClientID, e.Item.ItemIndex));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void radgrdAgent_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsAgent] != null)
                radgrdAgent.DataSource = ViewState[vsAgent];
        }


        #endregion

        #region GridCheckBox event


        public void chkItemWrt_CheckChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            GridDataItem item = (GridDataItem)chkBox.NamingContainer;
            if ((ViewState[PageConstants.vsItemIndexes] != null))
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
            }
            else
            {
                htItemIndex = new Hashtable();
            }
            if (chkBox.Checked == true)
            {
                hdnCheckIndex.Value = item.ItemIndex.ToString();
                htItemIndex.Add(htItemIndex.Count, item.ItemIndex);
                item.Selected = true;
            }
            else
            {
                item.Selected = false;
                for (int i = 0; i <= htItemIndex.Count - 1; i++)
                {
                    if (htItemIndex[i] != null)
                    {
                        if (htItemIndex[i].ToString() == item.ItemIndex.ToString())
                        {
                            radgrdAgent.Items[htItemIndex[i].ToString()].Edit = false;
                            htItemIndex.Remove(i);

                            break;
                        }
                    }
                }
            }
            ViewState.Add(PageConstants.vsItemIndexes, htItemIndex);
        }

        #endregion

        protected void radgrdAgent_OnItemCommand(object sender, GridCommandEventArgs e)
        {

            if (e.Item != null)
            {

                if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
                {
                    GridDataItem item = radgrdAgent.Items[e.Item.ItemIndex];
                    Label lblAgentId = (Label)item.FindControl("lblgrdAgentIdItem");

                    DataSet dsCustomer = new DataSet();
                    CompetitorAgentMasterDal objCompetitorAgentMasterDal = new CompetitorAgentMasterDal();
                    dsCustomer = objCompetitorAgentMasterDal.GetCompetitorCustomer(Convert.ToInt32(lblAgentId.Text));

                    if (dsCustomer.Tables[0].Rows.Count == 0)
                    {

                        lblTitleAlert.Text = "No Data Found";
                    }
                    else
                    {
                        lblTitleAlert.Text = "Customer Details";

                    }

                    radGrdCustomer.DataSource = dsCustomer.Tables[0];
                    radGrdCustomer.DataBind();
                    PopEx_lnkBtnChangePreference.Show();


                }
            }
        }




        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            radgrdAgent.ExportSettings.ExportOnlyData = true;
            radgrdAgent.ExportSettings.OpenInNewWindow = true;
            radgrdAgent.ExportSettings.IgnorePaging = true;

            string filename = "Agent_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdAgent.ExportSettings.FileName = filename;



            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdAgent.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdAgent.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdAgent.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdAgent.MasterTableView.ExportToCSV();
                        break;
                    }
            }


        }


        protected void acbAgent_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbAgent_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbAgent_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbAgent_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion
    }
}
