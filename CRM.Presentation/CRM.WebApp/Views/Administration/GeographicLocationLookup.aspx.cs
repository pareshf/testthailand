#region Program Information
/**********************************************************************************************************************************************
 Class Name           : GeographicLocation Lookup
 Class Description    : Implementation logic for save, edit, delete and find operation for GeographicLocation details.
 Author               : Chirag.
 Created Date         : Mar 09, 2010
***********************************************************************************************************************************************/
#endregion

#region Imports assemblies
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

using CRM.Core.Constants;
using CRM.DataAccess.AdministrationDAL;
using CRM.Model.AdministrationModel;
using CRM.Model.Security;
using Telerik.Web.UI;
using CRM.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Web;
#endregion

namespace CRM.WebApp.Views.Administration
{
    public partial class GeographicLocationLookup : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;

        GeographicLocationDal objGeographicLocation = null;
        public const String vsGeographicCountry = "GeographicCountry";
        public const String vsGeographicState = "GeographicState";
        public const String vsGeographicCity = "GeographicCity";
        GeographicLocationBDto objGeographicLocationBDto = null;
        AuthorizationBDto objAuthorizationBDto = null;
        BindCombo objBindCombo = null;
        #endregion

        #region Event

        #region Page Event
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("GeographicLocation");

        }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                acbGeographicLocation.ExportButton.Enabled = false;
                BindGridCountry();
                BindGridState();
                BindGridCity();
            }


            if (Session[PageConstants.ssnUserAuthorization] != null)
            {
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                Session["currentevent"] = "GeographicLocation";
            }

            hdnEditableModeCountry.Value = "false";
            hdnEditableModeState.Value = "false";
            hdnEditableModeCity.Value = "false";
            acbGeographicLocation.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdCountry.ClientID));
            acbGeographicLocation.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdCountry.ClientID));



        }

        #endregion

        protected void rtabGeographicLocation_TabClick(object sender, RadTabStripEventArgs e)
        {
            switch (e.Tab.Value.ToLower())
            {
                case "country":
                    mvGeographicLocation.SetActiveView(vwCountry);
                    acbGeographicLocation.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdCountry.ClientID));
                    acbGeographicLocation.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdCountry.ClientID));
                    acbGeographicLocation.ExportButton.Enabled = false;

                    break;
                case "state":
                    mvGeographicLocation.SetActiveView(vwState);
                    acbGeographicLocation.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdState.ClientID));
                    acbGeographicLocation.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdState.ClientID));
                    acbGeographicLocation.ExportButton.Enabled = false;
                    break;
                case "city":
                    mvGeographicLocation.SetActiveView(vwCity);
                    acbGeographicLocation.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdCity.ClientID));
                    acbGeographicLocation.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdCity.ClientID));
                    acbGeographicLocation.ExportButton.Enabled = true;
                    break;

            }
            acbGeographicLocation.EditableMode = false;
            pnlAddCity.Visible = false;
            pnlAddCountry.Visible = false;
            pnlAddState.Visible = false;
            BindGridCountry();
            BindGridState();
            BindGridCity();
            up.Update();
        }

        #region Actionbar Events

        protected void acbGeographicLocation_NewClick(object sender, EventArgs e)
        {
            try
            {
                switch (mvGeographicLocation.GetActiveView().ID.ToLower())
                {
                    case "vwcountry":
                        pnlAddCountry.Visible = true;
                        pnlAddState.Visible = false;
                        pnlAddCity.Visible = false;

                        break;
                    case "vwstate":
                        BindCountryCombo(cmbCountryName);
                        pnlAddState.Visible = true;
                        pnlAddCountry.Visible = false;
                        pnlAddCity.Visible = false;
                        break;
                    case "vwcity":
                        BindCountryCombo(cmbCountryNameCity);
                        BindCountryCombo(cmbStateNameCity);
                        pnlAddCity.Visible = true;
                        pnlAddCountry.Visible = false;
                        pnlAddState.Visible = false;
                        break;
                }

                acbGeographicLocation.EditableMode = true;
                acbGeographicLocation.SaveButton.CommandName = "Save";
                pnlAddNewMode.Visible = true;
                Reset();

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbGeographicLocation_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objGeographicLocation = new GeographicLocationDal();
                switch (acbGeographicLocation.SaveButton.CommandName)
                {
                    case "Save":
                        switch (mvGeographicLocation.GetActiveView().ID.ToLower())
                        {
                            case "vwcountry":
                                vwCountrySave();
                                pnlAddCountry.Visible = false;
                                acbGeographicLocation.EditableMode = false;
                                break;
                            case "vwstate":
                                vwStateSave();
                                pnlAddState.Visible = false;
                                acbGeographicLocation.EditableMode = false;
                                break;
                            case "vwcity":
                                vwCitySave();
                                pnlAddCity.Visible = false;
                                acbGeographicLocation.EditableMode = false;
                                break;
                        }
                        break;
                    case "Update":
                        switch (mvGeographicLocation.GetActiveView().ID.ToLower())
                        {

                            case "vwcountry":
                                String xmlDataCountry = GenerateXmlStringCountry(radgrdCountry);
                                result = objGeographicLocation.UpdateCountry(xmlDataCountry);
                                if (result == 1)
                                {
                                    acbGeographicLocation.DefaultMode = true;
                                    Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Update].ToString());
                                    Master.MessageCssClass = "successMessage";

                                    if (ViewState[PageConstants.vsItemIndexes] != null)
                                        htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                                    for (int i = 0; i < htItemIndex.Count; i++)
                                        radgrdCountry.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
                                    bisEdit = false;
                                    ViewState[PageConstants.vsItemIndexes] = null;
                                    BindGridCountry();
                                }
                                else
                                {
                                    Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Update].ToString());
                                    Master.MessageCssClass = "errorMessage";
                                }
                                break;
                            case "vwstate":
                                String xmlDataState = GenerateXmlStringState(radgrdState);
                                result = objGeographicLocation.UpdateState(xmlDataState);
                                if (result == 1)
                                {
                                    acbGeographicLocation.DefaultMode = true;
                                    Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Update].ToString());
                                    Master.MessageCssClass = "successMessage";

                                    if (ViewState[PageConstants.vsItemIndexes] != null)
                                        htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                                    for (int i = 0; i < htItemIndex.Count; i++)
                                        radgrdState.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
                                    bisEdit = false;
                                    ViewState[PageConstants.vsItemIndexes] = null;
                                    BindGridState();
                                }
                                else
                                {
                                    Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Update].ToString());
                                    Master.MessageCssClass = "errorMessage";
                                }
                                break;
                            case "vwcity":
                                String xmlDataCity = GenerateXmlStringCity(radgrdCity);
                                result = objGeographicLocation.UpdateCity(xmlDataCity);
                                if (result == 1)
                                {
                                    acbGeographicLocation.DefaultMode = true;
                                    Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Update].ToString());
                                    Master.MessageCssClass = "successMessage";

                                    if (ViewState[PageConstants.vsItemIndexes] != null)
                                        htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                                    for (int i = 0; i < htItemIndex.Count; i++)
                                        radgrdCity.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
                                    bisEdit = false;
                                    ViewState[PageConstants.vsItemIndexes] = null;
                                    BindGridCity();
                                }
                                else
                                {
                                    Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Update].ToString());
                                    Master.MessageCssClass = "errorMessage";
                                }
                                break;
                        }

                        break;
                }

                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbGeographicLocation_CancelClick(object sender, EventArgs e)
        {
            try
            {
                switch (mvGeographicLocation.GetActiveView().ID.ToLower())
                {
                    case "vwcountry":
                        if (ViewState[PageConstants.vsItemIndexes] != null)
                        {
                            htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdCountry.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                            bisEdit = false;
                            ViewState[PageConstants.vsItemIndexes] = null;
                            radgrdCountry.Rebind();
                        }
                        acbGeographicLocation.DefaultMode = true;
                        pnlAddCountry.Visible = false;
                        break;
                    case "vwstate":
                        if (ViewState[PageConstants.vsItemIndexes] != null)
                        {
                            htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdState.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                            bisEdit = false;
                            ViewState[PageConstants.vsItemIndexes] = null;
                            radgrdState.Rebind();
                        }
                        acbGeographicLocation.DefaultMode = true;
                        pnlAddState.Visible = false;
                        break;
                    case "vwcity":
                        if (ViewState[PageConstants.vsItemIndexes] != null)
                        {
                            htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdCity.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                            bisEdit = false;
                            ViewState[PageConstants.vsItemIndexes] = null;
                            radgrdCity.Rebind();
                        }
                        acbGeographicLocation.DefaultMode = true;
                        pnlAddCity.Visible = false;
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

        protected void acbGeographicLocation_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                switch (mvGeographicLocation.GetActiveView().ID.ToLower())
                {
                    case "vwcountry":
                        StringBuilder CountryId = new StringBuilder();
                        int result = 0;
                        if (ViewState[PageConstants.vsItemIndexes] != null)
                            htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                        if (htItemIndex != null)
                        {
                            foreach (int i in htItemIndex.Values)
                            {

                                Label lblCountryId = (Label)radgrdCountry.Items[i].FindControl("lblGrdCountryIdItem");
                                if (lblCountryId != null)
                                {
                                    CountryId.Append(lblCountryId.Text + ",");
                                }
                            }
                        }
                        objGeographicLocation = new GeographicLocationDal();
                        String ContryId = CountryId.ToString().TrimEnd(',');
                        result = objGeographicLocation.DeleteCountry(ContryId);

                        if (result == 1)
                        {
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Delete].ToString());
                            Master.MessageCssClass = "successMessage";
                            ViewState[PageConstants.vsItemIndexes] = null;
                            BindGridCountry();
                        }
                        else if (result == 547)
                        {
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Delete].ToString());
                            Master.MessageCssClass = "errorMessage";
                        }
                        break;
                    case "vwstate":
                        StringBuilder StateId = new StringBuilder();
                        if (ViewState[PageConstants.vsItemIndexes] != null)
                            htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                        if (htItemIndex != null)
                        {
                            foreach (int i in htItemIndex.Values)
                            {

                                Label lblStateId = (Label)radgrdState.Items[i].FindControl("lblGrdStateIdItem");
                                if (lblStateId != null)
                                {
                                    StateId.Append(lblStateId.Text + ",");
                                }
                            }
                        }
                        objGeographicLocation = new GeographicLocationDal();
                        String StatId = StateId.ToString().TrimEnd(',');
                        result = objGeographicLocation.DeleteState(StatId);

                        if (result == 1)
                        {
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Delete].ToString());
                            Master.MessageCssClass = "successMessage";
                            ViewState[PageConstants.vsItemIndexes] = null;
                            BindGridState();
                        }
                        else if (result == 547)
                        {
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Delete].ToString());
                            Master.MessageCssClass = "errorMessage";
                        }
                        break;
                    case "vwcity":
                        StringBuilder CityId = new StringBuilder();
                        if (ViewState[PageConstants.vsItemIndexes] != null)
                            htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                        if (htItemIndex != null)
                        {
                            foreach (int i in htItemIndex.Values)
                            {

                                Label lblCityId = (Label)radgrdCity.Items[i].FindControl("lblGrdCityIdItem");
                                if (lblCityId != null)
                                {
                                    CityId.Append(lblCityId.Text + ",");
                                }
                            }
                        }
                        objGeographicLocation = new GeographicLocationDal();
                        String CtyId = CityId.ToString().TrimEnd(',');
                        result = objGeographicLocation.DeleteCity(CtyId);

                        if (result == 1)
                        {
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Delete].ToString());
                            Master.MessageCssClass = "successMessage";
                            ViewState[PageConstants.vsItemIndexes] = null;
                            BindGridCity();
                        }
                        else if (result == 547)
                        {
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Delete].ToString());
                            Master.MessageCssClass = "errorMessage";
                        }
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

        protected void acbGeographicLocation_SaveNewClick(object sender, EventArgs e)
        {
            try
            {
                switch (mvGeographicLocation.GetActiveView().ID.ToLower())
                {
                    case "vwcountry":
                        txtCountryName.Focus();
                        vwCountrySave();
                        Reset();
                        break;
                    case "vwstate":
                        txtStateName.Focus();
                        vwStateSave();
                        Reset();
                        break;
                    case "vwcity":
                        txtCityName.Focus();
                        vwCitySave();
                        Reset();
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

        protected void acbGeographicLocation_EditClick(object sender, EventArgs e)
        {
            try
            {
                switch (mvGeographicLocation.GetActiveView().ID.ToLower())
                {
                    case "vwcountry":
                        if (ViewState[PageConstants.vsItemIndexes] != null)
                        {
                            htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                        }
                        if (htItemIndex != null)
                        {
                            for (int i = 0; i < htItemIndex.Count; i++)
                            {
                                radgrdCountry.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                            }
                            bisEdit = true;
                            radgrdCountry.Rebind();
                            acbGeographicLocation.EditableMode = true;
                            hdnEditableModeCountry.Value = "true";
                            acbGeographicLocation.SaveNewButton.Visible = false;
                            acbGeographicLocation.SaveNewButton.CommandName = "Update";
                            acbGeographicLocation.SaveButton.CommandName = "Update";
                        }

                        break;
                    case "vwstate":
                        if (ViewState[PageConstants.vsItemIndexes] != null)
                        {
                            htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                        }
                        if (htItemIndex != null)
                        {
                            for (int i = 0; i < htItemIndex.Count; i++)
                            {
                                radgrdState.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                            }
                            bisEdit = true;
                            radgrdState.Rebind();
                            acbGeographicLocation.EditableMode = true;
                            hdnEditableModeState.Value = "true";
                            acbGeographicLocation.SaveNewButton.Visible = false;
                            acbGeographicLocation.SaveNewButton.CommandName = "Update";
                            acbGeographicLocation.SaveButton.CommandName = "Update";
                        }
                        break;
                    case "vwcity":
                        if (ViewState[PageConstants.vsItemIndexes] != null)
                        {
                            htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                        }
                        if (htItemIndex != null)
                        {
                            Label lblGrdStateName = null;
                            Label lblGrdCountryName = null;
                            for (int i = 0; i < htItemIndex.Count; i++)
                            {

                                lblGrdStateName = (Label)radgrdCity.Items[Convert.ToInt32(htItemIndex[i].ToString())].FindControl("lblGrdStateName");
                                lblGrdCountryName = (Label)radgrdCity.Items[Convert.ToInt32(htItemIndex[i].ToString())].FindControl("lblGrdCountryName");
                                radgrdCity.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                            }
                            bisEdit = true;
                            radgrdCity.Rebind();
                            acbGeographicLocation.EditableMode = true;
                            hdnEditableModeCity.Value = "true";
                            acbGeographicLocation.SaveNewButton.Visible = false;
                            acbGeographicLocation.SaveNewButton.CommandName = "Update";
                            acbGeographicLocation.SaveButton.CommandName = "Update";


                            for (int i = 0; i < htItemIndex.Count; i++)
                            {
                                RadComboBox cmbGrdStateName = (RadComboBox)radgrdCity.Items[Convert.ToInt32(htItemIndex[i].ToString())].FindControl("cmbGrdStateName");
                                RadComboBox cmbGrdCountryName = (RadComboBox)radgrdCity.Items[Convert.ToInt32(htItemIndex[i].ToString())].FindControl("cmbGrdCountryName");

                                if (cmbGrdCountryName != null)
                                    BindCountryCombo(cmbGrdCountryName);

                                if (lblGrdCountryName != null)
                                {
                                    try
                                    {
                                        cmbGrdCountryName.ClearSelection();
                                        cmbGrdCountryName.FindItemByText(lblGrdCountryName.Text).Selected = true;
                                        cmbGrdCountryName.TabIndex = Convert.ToInt16(htItemIndex[i].ToString());
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }

                                if (cmbGrdStateName != null)
                                    BindStateCombo(cmbGrdStateName, int.Parse(cmbGrdCountryName.SelectedValue));

                                if (lblGrdStateName != null)
                                {
                                    try
                                    {
                                        cmbGrdStateName.ClearSelection();
                                        cmbGrdStateName.FindItemByText(lblGrdStateName.Text).Selected = true;
                                        cmbGrdStateName.TabIndex = Convert.ToInt16(htItemIndex[i].ToString());
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                            }
                        }
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

        protected void acbGeographicLocation_SearchClick(object sender, EventArgs e)
        {
            try
            {
                switch (mvGeographicLocation.GetActiveView().ID.ToLower())
                {
                    case "vwcountry":
                        objGeographicLocation = new GeographicLocationDal();
                        DataSet dsGeographicCountry = objGeographicLocation.GetCountryName(acbGeographicLocation.SearchTextBox.Text);
                        radgrdCountry.DataSource = dsGeographicCountry;
                        radgrdCountry.DataBind();
                        ViewState[vsGeographicCountry] = dsGeographicCountry;
                        Reset();
                        break;
                    case "vwstate":
                        objGeographicLocation = new GeographicLocationDal();
                        DataSet dsGeographicState = objGeographicLocation.GetStateName(acbGeographicLocation.SearchTextBox.Text);
                        radgrdState.DataSource = dsGeographicState;
                        radgrdState.DataBind();
                        ViewState[vsGeographicState] = dsGeographicState;
                        Reset();
                        break;
                    case "vwcity":
                        objGeographicLocation = new GeographicLocationDal();
                        DataSet dsGeographicCity = objGeographicLocation.GetCityName(acbGeographicLocation.SearchTextBox.Text);
                        radgrdCity.DataSource = dsGeographicCity;
                        radgrdCity.DataBind();
                        ViewState[vsGeographicCity] = dsGeographicCity;
                        Reset();
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

        protected void acbGeographicLocation_RefreshClick(object sender, EventArgs e)
        {
            acbGeographicLocation.DefaultMode = true;
            pnlAddCountry.Visible = false;
            pnlAddState.Visible = false;
            pnlAddCity.Visible = false;
            acbGeographicLocation.SearchTextBox.Text = string.Empty;
            BindGridCountry();
            BindGridState();
            BindGridCity();
        }

        protected void cmbCountryNameCity_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindStateCombo(cmbStateNameCity, int.Parse(e.Value));
        }

        #endregion

        #region Grid Events

        #region vwCountryGrid Events
        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdCountry_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdCountry.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdCountry.Items)
                {
                    CheckBox chkItem = (CheckBox)item.FindControl("chkItemWrt");
                    if (chkItem != null)
                        chkItem.Visible = false;
                }
            }
        }

        /// <summary>
        /// Rad grid's item databound event which add client side event to check box of rows.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdCountry_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdCountry.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdCountry.ClientID, chkBox.ClientID, e.Item.ItemIndex));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Assign data source after grid's page changing and sorting.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdCountry_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsGeographicCountry] != null)
                radgrdCountry.DataSource = ViewState[vsGeographicCountry];
        }


        #region Grid Checkbox Events

        /// <summary>
        /// Saves checkbox checked status into hashtable after check status changed.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        public void chkItemWrtCountry_CheckChanged(object sender, EventArgs e)
        {
            try
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
                    hdnCheckIndexCountry.Value = item.ItemIndex.ToString();
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
                                radgrdCountry.Items[htItemIndex[i].ToString()].Edit = false;
                                htItemIndex.Remove(i);

                                break;
                            }
                        }
                    }
                }
                ViewState.Add(PageConstants.vsItemIndexes, htItemIndex);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        #endregion

        #endregion

        #region vwStateGrid Events
        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdState_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdState.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdState.Items)
                {
                    CheckBox chkItem = (CheckBox)item.FindControl("chkItemWrt");
                    if (chkItem != null)
                        chkItem.Visible = false;
                }
            }
        }

        /// <summary>
        /// Rad grid's item databound event which add client side event to check box of rows.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdState_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdState.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdState.ClientID, chkBox.ClientID, e.Item.ItemIndex));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Assign data source after grid's page changing and sorting.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdState_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsGeographicState] != null)
                radgrdState.DataSource = ViewState[vsGeographicState];
        }

        #region Grid Checkbox Events

        /// <summary>
        /// Saves checkbox checked status into hashtable after check status changed.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        public void chkItemWrtState_CheckChanged(object sender, EventArgs e)
        {
            try
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
                    hdnCheckIndexState.Value = item.ItemIndex.ToString();
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
                                radgrdState.Items[htItemIndex[i].ToString()].Edit = false;
                                htItemIndex.Remove(i);

                                break;
                            }
                        }
                    }
                }
                ViewState.Add(PageConstants.vsItemIndexes, htItemIndex);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }
        #endregion

        #endregion

        #region vwCityGrid Events
        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdCity_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdCity.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdCity.Items)
                {
                    CheckBox chkItem = (CheckBox)item.FindControl("chkItemWrt");
                    if (chkItem != null)
                        chkItem.Visible = false;
                }
            }
        }

        /// <summary>
        /// Rad grid's item databound event which add client side event to check box of rows.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdCity_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdCity.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdCity.ClientID, chkBox.ClientID, e.Item.ItemIndex));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Assign data source after grid's page changing and sorting.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdCity_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsGeographicCity] != null)
                radgrdCity.DataSource = ViewState[vsGeographicCity];
        }

        #region Grid Checkbox Events

        /// <summary>
        /// Saves checkbox checked status into hashtable after check status changed.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        public void chkItemWrtCity_CheckChanged(object sender, EventArgs e)
        {
            try
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
                    hdnCheckIndexCity.Value = item.ItemIndex.ToString();
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
                                radgrdCity.Items[htItemIndex[i].ToString()].Edit = false;
                                htItemIndex.Remove(i);

                                break;
                            }
                        }
                    }
                }
                ViewState.Add(PageConstants.vsItemIndexes, htItemIndex);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        #endregion

        protected void cmbGrdCountryName_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox comboBox = (RadComboBox)sender;
            int index = 0;
            Int32.TryParse(comboBox.TabIndex.ToString(), out index);
            GridDataItem item = radgrdCity.Items[index];
            RadComboBox cmbGrdStateName = (RadComboBox)item.FindControl("cmbGrdStateName");

            if (cmbGrdStateName != null)
                BindStateCombo(cmbGrdStateName, int.Parse(comboBox.SelectedValue));



        }

        #endregion


        #endregion

        #region Generate Xml String
        /// <summary>
        /// Generate xml format data from grid.
        /// </summary>
        /// <param name="grid">Rad grid control which data to be converted into xml format.</param>
        /// <returns>Returns xml format data in string.</returns>
        private String GenerateXmlStringCountry(RadGrid grid)
        {
            string xmlRootStart = "<{0}>";
            string xmlRootEnd = "</{0}>";
            string xmlHeaderRootValue = "Node";
            string xmlHeaderNodeStructure = "<Country COUNTRY_ID=\"{0}\" COUNTRY_NAME=\"{1}\" COUNTRY_CODE=\"{2}\" COUNTRY_CURRENCY_SYMBOL=\"{3}\" COUNTRY_CURRENCY_NAME =\"{4}\" USER_ID=\"{5}\"></Country>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int CountryId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblgrdCountryId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblGrdCountryIdEdit");
                        TextBox txtgrdCountryName = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtGrdCountryName");
                        TextBox txtgrdCountryCode = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtGrdCountryCode");
                        TextBox txtgrdCurrencySymbol = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtGrdCurrencySymbol");
                        TextBox txtgrdCurrencyName = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtGrdCurrencyName");
                        CountryId = int.Parse(lblgrdCountryId.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, CountryId, txtgrdCountryName.Text, txtgrdCountryCode.Text, txtgrdCurrencySymbol.Text, txtgrdCurrencyName.Text, objAuthorizationBDto.UserProfile.UserId);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }

        /// <summary>
        /// Generate xml format data from grid.
        /// </summary>
        /// <param name="grid">Rad grid control which data to be converted into xml format.</param>
        /// <returns>Returns xml format data in string.</returns>
        private String GenerateXmlStringState(RadGrid grid)
        {
            string xmlRootStart = "<{0}>";
            string xmlRootEnd = "</{0}>";
            string xmlHeaderRootValue = "Node";
            string xmlHeaderNodeStructure = "<State STATE_ID=\"{0}\" STATE_NAME=\"{1}\" USER_ID=\"{2}\"></State>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int StateId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblgrdStateId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblGrdStateIdEdit");
                        TextBox txtgrdStateName = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtGrdStateName");
                        StateId = int.Parse(lblgrdStateId.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, StateId, txtgrdStateName.Text, objAuthorizationBDto.UserProfile.UserId);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }

        /// <summary>
        /// Generate xml format data from grid.
        /// </summary>
        /// <param name="grid">Rad grid control which data to be converted into xml format.</param>
        /// <returns>Returns xml format data in string.</returns>
        private String GenerateXmlStringCity(RadGrid grid)
        {
            string xmlRootStart = "<{0}>";
            string xmlRootEnd = "</{0}>";
            string xmlHeaderRootValue = "Node";
            string xmlHeaderNodeStructure = "<City CITY_ID=\"{0}\" CITY_NAME=\"{1}\" STATE_ID=\"{2}\" COUNTRY_ID=\"{3}\" USER_ID=\"{4}\"></City>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int CityId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblgrdCityId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblGrdCityIdEdit");
                        TextBox txtgrdCityName = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtGrdCityName");
                        RadComboBox cmbGrdStateName = (RadComboBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("cmbGrdStateName");
                        RadComboBox cmbGrdCountryName = (RadComboBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("cmbGrdCountryName");
                        CityId = int.Parse(lblgrdCityId.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, CityId, txtgrdCityName.Text, cmbGrdStateName.SelectedValue, cmbGrdCountryName.SelectedValue, objAuthorizationBDto.UserProfile.UserId);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }


        #endregion
        #endregion

        #region Method

        #region Bind Grid
        /// <summary>
        /// Bind GeographicLocationLookupCountry grid
        /// </summary>
        private void BindGridCountry()
        {
            try
            {
                objGeographicLocation = new GeographicLocationDal();
                DataSet dsGeographicCountry = objGeographicLocation.GetCountryName("");

                radgrdCountry.DataSource = dsGeographicCountry;
                radgrdCountry.DataBind();
                ViewState[vsGeographicCountry] = dsGeographicCountry;
                if (radgrdCountry != null)
                {
                    for (int i = 0; i < radgrdCountry.Items.Count; i++)
                        radgrdCountry.Items[i].Edit = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Bind GeographicLocationLookupState grid
        /// </summary>
        private void BindGridState()
        {
            try
            {
                objGeographicLocation = new GeographicLocationDal();
                DataSet dsGeographicState = objGeographicLocation.GetStateName("");
                radgrdState.DataSource = dsGeographicState;
                radgrdState.DataBind();
                ViewState[vsGeographicState] = dsGeographicState;
                if (radgrdState != null)
                {
                    for (int i = 0; i < radgrdState.Items.Count; i++)
                        radgrdState.Items[i].Edit = false;
                }
            }
            catch (Exception ex)
            { }

        }

        /// <summary>
        /// Bind GeographicLocationLookupCity grid
        /// </summary>
        private void BindGridCity()
        {
            try
            {
                objGeographicLocation = new GeographicLocationDal();
                DataSet dsGeographicCity = objGeographicLocation.GetCityName("");
                radgrdCity.DataSource = dsGeographicCity;
                radgrdCity.DataBind();
                ViewState[vsGeographicCity] = dsGeographicCity;
                if (radgrdCity != null)
                {
                    for (int i = 0; i < radgrdCity.Items.Count; i++)
                        radgrdCity.Items[i].Edit = false;
                }
            }
            catch (Exception ex)
            { }

        }

        ///// <summary>
        ///// Saves checkbox checked status into hashtable after check status changed.
        ///// </summary>
        ///// <param name="sender">The object which raised the event</param>
        ///// <param name="e">The event listener object</param>
        //public void chkItemWrt_CheckChanged(object sender, EventArgs e)
        //{
        //    CheckBox chkBox = (CheckBox)sender;
        //    GridDataItem item = (GridDataItem)chkBox.NamingContainer;
        //    if ((ViewState[PageConstants.vsItemIndexes] != null))
        //    {
        //        htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
        //    }
        //    else
        //    {
        //        htItemIndex = new Hashtable();
        //    }
        //    if (chkBox.Checked == true)
        //    {
        //        hdnCheckIndex.Value = item.ItemIndex.ToString();
        //        htItemIndex.Add(htItemIndex.Count, item.ItemIndex);
        //        item.Selected = true;
        //    }
        //    else
        //    {
        //        item.Selected = false;
        //        for (int i = 0; i <= htItemIndex.Count - 1; i++)
        //        {
        //            if (htItemIndex[i] != null)
        //            {
        //                if (htItemIndex[i].ToString() == item.ItemIndex.ToString())
        //                {
        //                    radgrdCountry.Items[htItemIndex[i].ToString()].Edit = false;
        //                    htItemIndex.Remove(i);

        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    ViewState.Add(PageConstants.vsItemIndexes, htItemIndex);
        //}

        #endregion

        #region Save

        #region Save vwCountry
        private void vwCountrySave()
        {
            try
            {
                int result = 0;
                objGeographicLocation = new GeographicLocationDal();
                GeographicLocationBDto objGeographicLocationBDto = new GeographicLocationBDto();
                objGeographicLocationBDto = new GeographicLocationBDto();
                objGeographicLocationBDto.CountryName = txtCountryName.Text;
                objGeographicLocationBDto.CountryCode = txtCountryCode.Text;
                objGeographicLocationBDto.CurrencySymbol = txtCurrencySymbol.Text;
                objGeographicLocationBDto.CurrencyName = txtCurrencyName.Text;
                objGeographicLocationBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objGeographicLocation.InsertCountryName(objGeographicLocationBDto);
                if (result >= 1)
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                    Master.MessageCssClass = "successMessage";
                }
                else
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                    Master.MessageCssClass = "errorMessage";
                }
                BindGridCountry();
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

        #region Save vwState
        private void vwStateSave()
        {
            try
            {
                int result = 0;
                objGeographicLocation = new GeographicLocationDal();
                objGeographicLocationBDto = new GeographicLocationBDto();
                objGeographicLocationBDto.CountryId = int.Parse(cmbCountryName.SelectedValue);
                objGeographicLocationBDto.StateName = txtStateName.Text;
                objGeographicLocationBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objGeographicLocation.InsertState(objGeographicLocationBDto);
                if (result >= 1)
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                    Master.MessageCssClass = "successMessage";
                }
                else
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                    Master.MessageCssClass = "errorMessage";
                }
                BindGridState();
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

        #region Save vwCity
        private void vwCitySave()
        {
            try
            {
                int result = 0;
                objGeographicLocation = new GeographicLocationDal();
                objGeographicLocationBDto = new GeographicLocationBDto();
                objGeographicLocationBDto.CityName = txtCityName.Text;
                objGeographicLocationBDto.CountryId = int.Parse(cmbCountryNameCity.SelectedValue);
                objGeographicLocationBDto.StateId = int.Parse(cmbStateNameCity.SelectedValue);
                objGeographicLocationBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objGeographicLocation.InsertCity(objGeographicLocationBDto);
                if (result >= 1)
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                    Master.MessageCssClass = "successMessage";
                }
                else
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                    Master.MessageCssClass = "errorMessage";
                }
                BindGridCity();
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

        #endregion

        #region Reset

        private void Reset()
        {
            txtCountryName.Text = string.Empty;
            txtCountryCode.Text = string.Empty;
            txtCurrencySymbol.Text = string.Empty;
            txtCurrencyName.Text = string.Empty;
            txtStateName.Text = string.Empty;
            //cmbCountryName.SelectedValue = "0";
            txtCityName.Text = string.Empty;
            //cmbCountryNameCity.SelectedValue = "0";
            //cmbStateNameCity.SelectedValue = "0";
        }
        #endregion

        #region Bind Combo

        private void BindCountryCombo(RadComboBox sender)
        {
            objBindCombo = new BindCombo();
            sender.Items.Clear();
            sender.ClearSelection();
            sender.DataTextField = "COUNTRY_NAME";
            sender.DataValueField = "COUNTRY_ID";
            sender.DataSource = objBindCombo.GetCountryKeyValue();
            sender.DataBind();
            sender.Items.Insert(0, new RadComboBoxItem("", "0"));
            sender.SelectedValue = "0";
        }

        private void BindStateCombo(RadComboBox sender, int contryId)
        {
            objBindCombo = new BindCombo();
            sender.Items.Clear();
            sender.ClearSelection();
            sender.DataTextField = "STATE_NAME";
            sender.DataValueField = "STATE_ID";
            sender.DataSource = objBindCombo.GetStateKeyValue(contryId);
            sender.DataBind();
            sender.Items.Insert(0, new RadComboBoxItem("", "0"));
            sender.SelectedValue = "0";
        }

        #endregion


        #endregion

        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            radgrdCity.ExportSettings.ExportOnlyData = true;
            radgrdCity.ExportSettings.OpenInNewWindow = true;
            radgrdCity.ExportSettings.IgnorePaging = true;

            string filename = "GeographicLocation_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdCity.ExportSettings.FileName = filename;


            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdCity.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdCity.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdCity.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdCity.MasterTableView.ExportToCSV();
                        break;
                    }
            }

        }

        protected void acbGeographicLocation_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbGeographicLocation_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbGeographicLocation_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbGeographicLocation_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion

    }
}
