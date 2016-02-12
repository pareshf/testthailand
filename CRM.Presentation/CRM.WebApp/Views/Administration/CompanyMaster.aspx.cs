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
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;
using System.Web;
using System.Web.UI;

namespace CRM.WebApp.Views.Administration
{
    public partial class CompanyMaster : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Hashtable htItemIndexCt;
        public const String vsItemIndexesCt = "vsItemIndexesCt";
        Boolean bisEdit = false;
        CompanyMasterDal objCompanyMasterDal = null;
        public const String vsCompany = "Company";
        public const String vsContact = "Contact";
        AuthorizationBDto objAuthorizationBDto;
        public const String ssnHasTableIndexCnt = "HasTableIndexCnt";
        public int CompanyId = 0;
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
                mvCompany.SetActiveView(vwCompany);
            }
            if (Session[PageConstants.ssnUserAuthorization] != null)
            {
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                Session["currentevent"] = "Manage Company";
            }
            hdnEditableMode.Value = "false";
            acbCompany.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateGridForEdit('{0}')", radgrdCompany.ClientID));
            acbCompany.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdCompany.ClientID));

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            if (objAuthorizationBDto != null)
            {
                if (!objAuthorizationBDto.ProgramWriteAccess)
                {
                    cbxCompany.SaveButton.Enabled = false;
                    cbxCompany.SaveNewButton.Enabled = false;
                    cbxCompany.CopyButton.Enabled = false;
                }

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();

            WebHelper.WebManager.CheckUserAuthorizationForProgram("Manage Company");
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


        #endregion

        #region Actionbar Events

        protected void acbCompany_NewClick(object sender, EventArgs e)
        {

            switch (mvCompany.GetActiveView().ID)
            {
                case "vwCompany":
                    mvCompany.SetActiveView(vwCompany);
                    rtsCompanyDetails.SelectedIndex = 0;
                    pnlAddNewMode.Visible = true;
                    pnlGrid.Visible = false;
                    rtsCompanyDetails.Tabs[1].Enabled = false;
                    BindCompanyDropdowns();
                    radCmbType.Focus();
                    radgrdCompany.Rebind();
                    cbxCompany.SaveButton.ValidationGroup = "Company";

                    //acbCompany.SaveButton.ValidationGroup = "Company";
                    Reset();

                    radCmbType.Focus();
                    acbCompany.SaveButton.CommandName = "Save";
                    break;
                case "vwContact":

                    rtsCompanyDetails.Tabs[0].Enabled = false;
                    mvCompany.SetActiveView(vwContact);
                    rtsCompanyDetails.SelectedIndex = 1;
                    pnlContactAdd.Visible = true;
                    cbxCompany.SaveButton.ValidationGroup = "Contact";
                    BindTitle();
                    cbxCompany.SaveButton.CommandName = "Save";
                    ResetContact();
                    pnlContactGrid.Visible = false;

                    break;
            }
            cbxCompany.Visible = true;
            cbxCompany.SaveButton.Enabled = true;
            acbCompany.Visible = false;


        }

        protected void acbCompany_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objCompanyMasterDal = new CompanyMasterDal();

                switch (mvCompany.GetActiveView().ID)
                {
                    case "vwCompany":

                        switch (acbCompany.SaveButton.CommandName)
                        {
                            case "Save":
                                Save();

                                break;
                            case "Update":
                                Update();
                                pnlAddNewMode.Visible = false;
                                acbCompany.EditableMode = false;
                                //pnlGrid.Visible = true;
                                break;
                        }


                        break;
                    case "vwContact":



                        switch (acbCompany.SaveButton.CommandName)
                        {
                            case "Save":
                                SaveContact();

                                break;
                            case "Update":
                                UpdateContact();

                                acbCompany.EditableMode = false;
                                break;
                        }

                        rtsCompanyDetails.Tabs[0].Enabled = true;

                        break;
                }







                objCompanyMasterDal = new CompanyMasterDal();

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbCompany_SaveNewClick(object sender, EventArgs e)
        {
            //txtCompanyName.Focus();
            acbCompany.Visible = false;

            //Save();
            //Reset();
        }

        protected void acbCompany_CancelClick(object sender, EventArgs e)
        {

            switch (mvCompany.GetActiveView().ID)
            {
                case "vwCompany":



                    if (ViewState[PageConstants.vsItemIndexes] != null)
                    {
                        htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                        for (int i = 0; i < htItemIndex.Count; i++)
                            radgrdCompany.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                        bisEdit = false;
                        rtsCompanyDetails.Tabs[1].Enabled = true;
                        ViewState[PageConstants.vsItemIndexes] = null;
                        pnlGrid.Visible = true;
                        radgrdCompany.Rebind();

                    }
                    //  rtsCompanyDetails.Tabs[1].Enabled = true;

                    acbCompany.DefaultMode = true;
                    pnlAddNewMode.Visible = false;
                    break;

                case "vwContact":

                    if (ViewState[vsItemIndexesCt] != null)
                    {
                        htItemIndexCt = (Hashtable)ViewState[vsItemIndexesCt];

                        for (int i = 0; i < htItemIndexCt.Count; i++)
                            radgrdContact.Items[Convert.ToInt32(htItemIndexCt[i].ToString())].Edit = false;
                        bisEdit = false;
                        rtsCompanyDetails.Tabs[1].Enabled = true;
                        ViewState[vsItemIndexesCt] = null;
                        radgrdContact.Rebind();

                    }
                    rtsCompanyDetails.Tabs[0].Enabled = true;

                    acbCompany.DefaultMode = true;
                    pnlContactAdd.Visible = false;
                    break;


            }
        }

        protected void acbCompany_DeleteClick(object sender, EventArgs e)
        {

            int result = 0;
            try
            {
                switch (mvCompany.GetActiveView().ID)
                {
                    case "vwCompany":

                        StringBuilder CompanyId = new StringBuilder();

                        if (ViewState[PageConstants.vsItemIndexes] != null)
                            htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                        if (htItemIndex != null)
                        {
                            foreach (int i in htItemIndex.Values)
                            {
                                Label lblCompanyId = (Label)radgrdCompany.Items[i].FindControl("lblCompIdItem");

                                if (lblCompanyId != null)
                                {
                                    CompanyId.Append(lblCompanyId.Text + ",");
                                }
                            }
                        }

                        objCompanyMasterDal = new CompanyMasterDal();
                        String CmpId = CompanyId.ToString().TrimEnd(',');
                        result = objCompanyMasterDal.DeleteCompany(CmpId);
                        BindGrid();

                        break;
                    case "vwContact":

                        StringBuilder ContactSrNoId = new StringBuilder();

                        if (ViewState[vsItemIndexesCt] != null)
                            htItemIndexCt = (Hashtable)ViewState[vsItemIndexesCt];

                        if (htItemIndexCt != null)
                        {
                            foreach (int i in htItemIndexCt.Values)
                            {
                                Label lblContactSrnoId = (Label)radgrdContact.Items[i].FindControl("lblGrdCustIdItem");

                                if (lblContactSrnoId != null)
                                {
                                    ContactSrNoId.Append(lblContactSrnoId.Text + ",");
                                }
                            }
                        }
                        objCompanyMasterDal = new CompanyMasterDal();
                        String ContactSrNos = ContactSrNoId.ToString().TrimEnd(',');
                        result = objCompanyMasterDal.DeleteContact(ContactSrNos);
                        BindContactGrid(Convert.ToInt32(Session["COMPANY_ID"]), "");
                        break;
                }









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

        protected void acbCompany_SearchClick(object sender, EventArgs e)
        {
            try
            {

                switch (mvCompany.GetActiveView().ID)
                {
                    case "vwCompany":


                        objCompanyMasterDal = new CompanyMasterDal();
                        DataSet dsCompany = objCompanyMasterDal.GetCompany(acbCompany.SearchTextBox.Text);
                        radgrdCompany.DataSource = dsCompany;
                        radgrdCompany.DataBind();
                        ViewState[vsCompany] = dsCompany;
                        break;

                    case "vwContact":

                        BindContactGrid(Convert.ToInt32(Session["COMPANY_ID"]), acbCompany.SearchTextBox.Text);
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

        protected void acbCompany_EditClick(object sender, EventArgs e)
        {
            try
            {
                switch (mvCompany.GetActiveView().ID)
                {
                    case "vwCompany":

                        #region Company
                        rtsCompanyDetails.Tabs[1].Enabled = true;
                        rtsCompanyDetails.SelectedIndex = 0;
                        mvCompany.SetActiveView(vwCompany);

                        if (ViewState[PageConstants.vsItemIndexes] != null)
                        {
                            htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                        }
                        if (htItemIndex != null)
                        {

                            for (int i = 0; i < htItemIndex.Count; i++)
                            {


                                GridDataItem item = radgrdCompany.Items[int.Parse(htItemIndex[i].ToString())];
                                Label lblCompanyId = (Label)radgrdCompany.Items[int.Parse(htItemIndex[i].ToString())].FindControl("lblCompIdItem");
                                CompanyId = Convert.ToInt32(lblCompanyId.Text.Trim());
                                Session["COMPANY_ID"] = CompanyId;

                                BindAssignDepartment_ByCompanyId(CompanyId);

                                BindCompanyDropdowns();
                                radCmbCountry.SelectedValue = item["COUNTRY_ID"].Text;

                                BindState(Convert.ToInt32(radCmbCountry.SelectedValue));
                                radCmbState.SelectedValue = item["STATE_ID"].Text;
                                BindCity(Convert.ToInt32(radCmbCountry.SelectedValue), Convert.ToInt32(radCmbState.SelectedValue));
                                radCmbCity.SelectedValue = item["CITY_ID"].Text;
                                radCmbType.SelectedValue = item["IS_COMPANY_FRANCHISE"].Text.Trim();
                                txtCompanyName.Text = item["COMPANY_NAME"].Text;

                                if (item["ADDRESS_LINE1"].Text == "&nbsp;")
                                    txtAddressline1.Text = string.Empty;
                                else
                                    txtAddressline1.Text = item["ADDRESS_LINE1"].Text;
                                if (item["ADDRESS_LINE2"].Text == "&nbsp;")
                                    txtAddressline2.Text = string.Empty;
                                else
                                    txtAddressline2.Text = item["ADDRESS_LINE2"].Text;

                                if (item["MOBILE"].Text == "&nbsp;")
                                    txtMobile.Text = string.Empty;
                                else
                                    txtMobile.Text = item["MOBILE"].Text;

                                if (item["PHONE"].Text == "&nbsp;")
                                    txtPhone.Text = string.Empty;
                                else
                                    txtPhone.Text = item["PHONE"].Text;

                                if (item["EMAIL_ID"].Text == "&nbsp;")
                                    txtEmailId.Text = string.Empty;
                                else
                                    txtEmailId.Text = item["EMAIL_ID"].Text;

                                if (item["PINCODE"].Text == "&nbsp;")
                                    txtPinCode.Text = string.Empty;
                                else
                                    txtPinCode.Text = item["PINCODE"].Text;
                                if (item["FAX"].Text == "&nbsp;")
                                    txtFax.Text = string.Empty;
                                else
                                    txtFax.Text = item["FAX"].Text;
                                radCmbRegion.SelectedValue = item["REGION_ID"].Text;
                                BindUnderWhome(radCmbType.SelectedValue);
                                radCmbUnderWhome.SelectedValue = item["PARENT_COMPANY_ID"].Text;


                                object img = null; //= Dt.Rows[0]["PHOTO"];

                                try
                                {
                                    if (img == System.DBNull.Value)
                                        imgCompany.ImageUrl = "~/Views/Shared/Images/DefaultImage.jpg";
                                    else
                                        imgCompany.ImageUrl = "~/Views/Shared/Controller/ImageController.ashx?id=" + CompanyId + "&phototype=Company";
                                }
                                catch (Exception ex)
                                {
                                    imgCompany.ImageUrl = "~/Views/Common/Images/DefaultImage.jpg";
                                }




                            }
                            pnlAddNewMode.Visible = true;
                            pnlGrid.Visible = false;
                            // Session[PageConstants.ssnHasTableIndex] = htItemIndex;
                            bisEdit = true;
                            radgrdCompany.Rebind();
                            hdnEditableMode.Value = "true";

                            //acbCompany.EditableMode = true;
                            acbCompany.SaveButton.CommandName = "Update";
                            cbxCompany.EnableSaveButton = true;
                            acbCompany.Visible = false;
                            cbxCompany.Visible = true;

                        }
                        #endregion

                        break;

                    case "vwContact":
                        #region Contact
                        if (ViewState[vsItemIndexesCt] != null)
                        {
                            htItemIndexCt = (Hashtable)ViewState[vsItemIndexesCt];
                        }
                        if (htItemIndexCt != null)
                        {
                            for (int i = 0; i < htItemIndexCt.Count; i++)
                            {
                                radgrdContact.Items[Convert.ToInt32(htItemIndexCt[i].ToString())].Edit = true;
                            }
                            bisEdit = true;
                            radgrdContact.Rebind();
                            acbCompany.EditableMode = true;
                            hdnEditableMode.Value = "true";
                            cbxCompany.SaveButton.CommandName = "Update";
                            acbCompany.SaveNewButton.Visible = false;
                            Session[ssnHasTableIndexCnt] = htItemIndexCt;
                            rtsCompanyDetails.Tabs[0].Enabled = false;

                            cbxCompany.Visible = true;
                            cbxCompany.SaveButton.Enabled = true;
                            acbCompany.Visible = false;

                        }

                        #endregion

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



        protected void acbCompany_RefreshClick(object sender, EventArgs e)
        {
            try
            {
                bisEdit = false;
                mvCompany.SetActiveView(vwCompany);
                rtsCompanyDetails.Tabs[1].Enabled = true;
                ViewState[PageConstants.vsItemIndexes] = null;
                pnlGrid.Visible = true;
                BindGrid();
                acbCompany.DefaultMode = true;
                pnlAddNewMode.Visible = false;
                Reset();
                ResetContact();


            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        #endregion

        #region Control box Events



        protected void cbxCompany_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objCompanyMasterDal = new CompanyMasterDal();

                switch (mvCompany.GetActiveView().ID)
                {
                    case "vwCompany":

                        switch (acbCompany.SaveButton.CommandName)
                        {
                            case "Save":
                                Save();

                                break;
                            case "Update":
                                Update();

                                break;
                        }


                        break;
                    case "vwContact":
                        switch (cbxCompany.SaveButton.CommandName)
                        {
                            case "Save":
                                SaveContact();
                                break;
                            case "Update":
                                UpdateContact();

                                acbCompany.EditableMode = false;
                                break;
                        }

                        rtsCompanyDetails.Tabs[0].Enabled = true;

                        break;
                }


                objCompanyMasterDal = new CompanyMasterDal();

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void cbxCompany_CancelClick(object sender, EventArgs e)
        {

            switch (mvCompany.GetActiveView().ID)
            {
                case "vwCompany":
                    pnlGrid.Visible = true;
                    if (ViewState[PageConstants.vsItemIndexes] != null)
                    {
                        htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                        for (int i = 0; i < htItemIndex.Count; i++)
                            radgrdCompany.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                        bisEdit = false;
                        rtsCompanyDetails.Tabs[1].Enabled = true;
                        ViewState[PageConstants.vsItemIndexes] = null;

                        radgrdCompany.Rebind();

                    }
                    //  rtsCompanyDetails.Tabs[1].Enabled = true;


                    BindGrid();
                    acbCompany.DefaultMode = true;
                    pnlAddNewMode.Visible = false;
                    break;

                case "vwContact":

                    if (ViewState[vsItemIndexesCt] != null)
                    {
                        htItemIndexCt = (Hashtable)ViewState[vsItemIndexesCt];

                        for (int i = 0; i < htItemIndexCt.Count; i++)
                            radgrdContact.Items[Convert.ToInt32(htItemIndexCt[i].ToString())].Edit = false;
                        bisEdit = false;
                        rtsCompanyDetails.Tabs[1].Enabled = true;
                        ViewState[vsItemIndexesCt] = null;
                        radgrdContact.Rebind();

                    }
                    rtsCompanyDetails.Tabs[0].Enabled = true;
                    cbxCompany.VisibleClearButton = true;
                    cbxCompany.Visible = false;
                    acbCompany.DefaultMode = true;
                    pnlContactAdd.Visible = false;
                    pnlContactGrid.Visible = true;

                    break;


            }

            acbCompany.Visible = true;
        }

        protected void cbxCompany_ClearClick(object sender, EventArgs e)
        {
            switch (mvCompany.GetActiveView().ID)
            {
                case "vwCompany":
                    Reset();
                    break;
                case "vwContact":
                    ResetContact();
                    break;
            }

        }



        //protected void cbxCompany_NewClick(object sender, EventArgs e)
        //{

        //    switch (mvCompany.GetActiveView().ID)
        //    {
        //        case "vwCompany":
        //            mvCompany.SetActiveView(vwCompany);
        //            rtsCompanyDetails.SelectedIndex = 0;
        //            pnlAddNewMode.Visible = true;
        //            pnlGrid.Visible = false;
        //            //  rtsCompanyDetails.Tabs[1].Enabled = false;
        //            BindCompanyDropdowns();
        //            radCmbType.Focus();
        //            radgrdCompany.Rebind();

        //            acbCompany.SaveButton.ValidationGroup = "Company";
        //            Reset();
        //            break;
        //        case "vwContact":

        //            rtsCompanyDetails.Tabs[0].Enabled = false;
        //            mvCompany.SetActiveView(vwContact);
        //            rtsCompanyDetails.SelectedIndex = 1;
        //            pnlContactAdd.Visible = true;
        //            acbCompany.SaveButton.ValidationGroup = "Contact";
        //            BindTitle();
        //            break;
        //    }
        //    acbCompany.EditableMode = true;
        //    acbCompany.SaveButton.CommandName = "Save";


        //}
        //protected void cbxCompany_SaveNewClick(object sender, EventArgs e)
        //{
        //    //txtCompanyName.Focus();

        //    //Save();
        //    //Reset();
        //}
        //protected void cbxCompany_DeleteClick(object sender, EventArgs e)
        //{
        //    //try
        //    //{
        //    //    StringBuilder CompanyId = new StringBuilder();
        //    //    int result = 0;
        //    //    if (ViewState[PageConstants.vsItemIndexes] != null)
        //    //        htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

        //    //    if (htItemIndex != null)
        //    //    {
        //    //        foreach (int i in htItemIndex.Values)
        //    //        {
        //    //            Label lblCompanyId = (Label)radgrdCompany.Items[i].FindControl("lblgrdCompanyIdItem");

        //    //            if (lblCompanyId != null)
        //    //            {
        //    //                CompanyId.Append(lblCompanyId.Text + ",");
        //    //            }
        //    //        }
        //    //    }

        //    //    objCompanyMasterDal = new CompanyMasterDal();
        //    //    String QualifiId = CompanyId.ToString().TrimEnd(',');
        //    //    result = objCompanyMasterDal.DeleteCompany(QualifiId);


        //    //    if (result == 1)
        //    //    {
        //    //        Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Delete].ToString());
        //    //        Master.MessageCssClass = "successMessage";
        //    //        ViewState[PageConstants.vsItemIndexes] = null;
        //    //        BindGrid();
        //    //    }
        //    //    else if (result == 547)
        //    //    {
        //    //        Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Delete].ToString());
        //    //        Master.MessageCssClass = "errorMessage";
        //    //    }
        //    //}


        //    //catch (Exception ex)
        //    //{
        //    //    bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
        //    //    if (rethrow)
        //    //    { throw ex; }
        //    //}
        //}
        //protected void cbxCompany_SearchClick(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        objCompanyMasterDal = new CompanyMasterDal();
        //        DataSet dsCompany = objCompanyMasterDal.GetCompany(acbCompany.SearchTextBox.Text);
        //        radgrdCompany.DataSource = dsCompany;
        //        radgrdCompany.DataBind();
        //        ViewState[vsCompany] = dsCompany;
        //    }
        //    catch (Exception ex)
        //    {
        //        bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
        //        if (rethrow)
        //        { throw ex; }
        //    }

        //}


        #endregion

        #region tabStrip Event
        protected void rtsCompanyDetails_TabClick(object sender, RadTabStripEventArgs e)
        {
            switch (e.Tab.Value)
            {
                case "CompanyDetails":
                    acbCompany.Attributes.Remove("onClick");
                    acbCompany.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateGridForEdit('{0}')", radgrdCompany.ClientID));
                    acbCompany.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdCompany.ClientID));

                    if (acbCompany.SaveButton.CommandName == "Save")
                    {
                        cbxCompany.SaveButton.Enabled = false;

                    }
                    cbxCompany.Visible = true;
                    acbCompany.Visible = false;

                    mvCompany.SetActiveView(vwCompany);
                    //acbCompany.EditableMode = true;
                    //pnlGrid.Visible = true;


                    break;
                case "ContactInfo":
                    acbCompany.Attributes.Remove("onClick");
                    acbCompany.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateGridForEdit('{0}')", radgrdContact.ClientID));
                    acbCompany.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdContact.ClientID));

                    acbCompany.Visible = true;
                    acbCompany.DefaultMode = true;
                    cbxCompany.Visible = false;


                    mvCompany.SetActiveView(vwContact);
                    pnlContactGrid.Visible = true;
                    BindContactGrid(Convert.ToInt32(Session["COMPANY_ID"]), "");

                    pnlGrid.Visible = false;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Grid Event
        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void radgrdCompany_PreRender(object source, EventArgs e)
        {
            //if (bisEdit)
            //{
            //    GridHeaderItem headerItem = radgrdCompany.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

            //    CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
            //    if (chkHeader != null)
            //        chkHeader.Visible = false;

            //    foreach (GridDataItem item in radgrdCompany.Items)
            //    {
            //        CheckBox chkItem = (CheckBox)item.FindControl("chkItemWrt");
            //        if (chkItem != null)
            //            chkItem.Visible = false;
            //    }
            //}
        }

        /// <summary>
        /// Rad grid's item databound event which add client side event to check box of rows.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdCompany_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdCompany.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdCompany.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdCompany_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsCompany] != null)
                radgrdCompany.DataSource = ViewState[vsCompany];
        }

        protected void radgrdContact_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsContact] != null)
                radgrdContact.DataSource = ViewState[vsContact];
        }
        protected void radgrdContact_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdContact.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrTCt");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdCompany.Items)
                {
                    CheckBox chkItem = (CheckBox)item.FindControl("chkItemWrtCt");
                    if (chkItem != null)
                        chkItem.Visible = false;
                }
            }
        }
        protected void radgrdContact_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrtCt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdContact.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrtCt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdContact.ClientID, chkBox.ClientID, e.Item.ItemIndex));
                }





                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {

                    GridEditableItem editForm = (GridEditableItem)e.Item;
                    RadComboBox radcombo = (RadComboBox)editForm.FindControl("lblGrdContTitleEdit");
                    string ItemValue = string.Empty;
                    BindCombo objCmbBind = new BindCombo();
                    DataSet DsCmb = null;
                    DsCmb = objCmbBind.GetTitle();

                    radcombo.DataSource = DsCmb;
                    radcombo.DataTextField = "TITLE_DESC";
                    radcombo.DataValueField = "TITLE_ID";
                    radcombo.DataBind();
                    objCmbBind = null;
                    DsCmb = null;

                    // Note : Assigning selected value to dropdown list


                    Label lblTitleId = (Label)editForm.FindControl("lblGrdContTitleIdEdit");
                    radcombo.SelectedValue = lblTitleId.Text;


                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GridCheckBox event

        /// <summary>
        /// Saves checkbox checked status into hashtable after check status changed.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        public void chkItemWrtCt_CheckChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            GridDataItem item = (GridDataItem)chkBox.NamingContainer;
            if ((ViewState[vsItemIndexesCt] != null))
            {
                htItemIndexCt = (Hashtable)ViewState[vsItemIndexesCt];
            }
            else
            {
                htItemIndexCt = new Hashtable();
            }
            if (chkBox.Checked == true)
            {
                hdnCheckIndexCt.Value = item.ItemIndex.ToString();
                htItemIndexCt.Add(htItemIndexCt.Count, item.ItemIndex);
                item.Selected = true;
            }
            else
            {
                item.Selected = false;
                for (int i = 0; i <= htItemIndexCt.Count - 1; i++)
                {
                    if (htItemIndexCt[i] != null)
                    {
                        if (htItemIndexCt[i].ToString() == item.ItemIndex.ToString())
                        {
                            radgrdContact.Items[htItemIndexCt[i].ToString()].Edit = false;
                            htItemIndexCt.Remove(i);

                            break;
                        }
                    }
                }
            }
            ViewState.Add(vsItemIndexesCt, htItemIndexCt);
        }
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
                            radgrdCompany.Items[htItemIndex[i].ToString()].Edit = false;
                            htItemIndex.Remove(i);

                            break;
                        }
                    }
                }
            }
            ViewState.Add(PageConstants.vsItemIndexes, htItemIndex);
        }




        #endregion

        #region Method

        #region Bind Grid

        /// <summary>
        /// Bind Company grid
        /// </summary>

        private void BindGrid()
        {

            objCompanyMasterDal = new CompanyMasterDal();
            DataSet dsCompany = objCompanyMasterDal.GetCompany("");
            radgrdCompany.DataSource = dsCompany;
            radgrdCompany.DataBind();
            ViewState[vsCompany] = dsCompany;

        }
        public void BindContactGrid(int CompanyId, String SearchParam)
        {
            objCompanyMasterDal = new CompanyMasterDal();
            DataSet dsContact = objCompanyMasterDal.GetContactDetails(CompanyId, SearchParam);

            radgrdContact.DataSource = dsContact;
            radgrdContact.DataBind();
            ViewState[vsContact] = dsContact;
        }
        #endregion

        #region Generate Xml String
        /// <summary>
        /// Generate xml format data from grid.
        /// </summary>
        /// <param name="grid">Rad grid control which data to be converted into xml format.</param>
        /// <returns>Returns xml format data in string.</returns>
        private String GenerateXmlString(RadGrid grid)
        {

            string xmlRootStart = "<{0}>";
            string xmlRootEnd = "</{0}>";
            string xmlHeaderRootValue = "Node";
            string xmlHeaderNodeStructure = "<ContactInfo  COMPANY_ID=\"{0}\" SR_NO=\"{1}\" TITLE_ID=\"{2}\" NAME=\"{3}\" EMAIL=\"{4}\" MOBILE=\"{5}\" PHONE=\"{6}\"  ></ContactInfo>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (Session[ssnHasTableIndexCnt] != null)
                {
                    htItemIndexCt = (Hashtable)Session[ssnHasTableIndexCnt];
                }

                if (htItemIndexCt != null && htItemIndexCt.Count > 0)
                {

                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);
                    for (int i = 0; i < htItemIndexCt.Count; i++)
                    {
                        Label lblCompanyId = (Label)grid.Items[Convert.ToInt32(htItemIndexCt[i])].FindControl("GrdCustCompanyIdEdit");
                        Label lblSrno = (Label)grid.Items[Convert.ToInt32(htItemIndexCt[i])].FindControl("lblGrdCustIdEdit");
                        RadComboBox radCmbTitleId = (RadComboBox)grid.Items[Convert.ToInt32(htItemIndexCt[i])].FindControl("lblGrdContTitleEdit");
                        TextBox lblName = (TextBox)grid.Items[Convert.ToInt32(htItemIndexCt[i])].FindControl("lblGrdContNameEdit");
                        TextBox lblEmail = (TextBox)grid.Items[Convert.ToInt32(htItemIndexCt[i])].FindControl("lblGrdContEmailEdit");
                        TextBox lblMobile = (TextBox)grid.Items[Convert.ToInt32(htItemIndexCt[i])].FindControl("lblGrdContMobileEdit");
                        TextBox lblPhone = (TextBox)grid.Items[Convert.ToInt32(htItemIndexCt[i])].FindControl("lblGrdContEmailPhoneEdit");
                        xmlString.AppendFormat(xmlHeaderNodeStructure, lblCompanyId.Text, lblSrno.Text, radCmbTitleId.SelectedValue, lblName.Text, lblEmail.Text, lblMobile.Text, lblPhone.Text);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);


                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }
        #endregion


        #region Generate Xml For Delete
        /// <summary>
        /// Generate xml format data from grid.
        /// </summary>
        /// <param name="grid">Rad grid control which data to be converted into xml format.</param>
        /// <returns>Returns xml format data in string.</returns>
        private String GenerateDeleteXmlString(RadGrid grid)
        {

            string xmlRootStart = "<{0}>";
            string xmlRootEnd = "</{0}>";
            string xmlHeaderRootValue = "Node";
            string xmlHeaderNodeStructure = "<ContactInfo  COMPANY_ID=\"{0}\" SR_NO=\"{1}\"></ContactInfo>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (Session[ssnHasTableIndexCnt] != null)
                {
                    htItemIndexCt = (Hashtable)Session[ssnHasTableIndexCnt];
                }

                if (htItemIndexCt != null && htItemIndexCt.Count > 0)
                {

                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);
                    for (int i = 0; i < htItemIndexCt.Count; i++)
                    {
                        Label lblCompanyId = (Label)grid.Items[Convert.ToInt32(htItemIndexCt[i])].FindControl("GrdCustCompanyIdEdit");
                        Label lblSrno = (Label)grid.Items[Convert.ToInt32(htItemIndexCt[i])].FindControl("lblGrdCustIdEdit");
                        xmlString.AppendFormat(xmlHeaderNodeStructure, lblCompanyId.Text, lblSrno.Text);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);


                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }
        #endregion

        #region Save
        private void Save()
        {
            try
            {
                int result = 0;
                objCompanyMasterDal = new CompanyMasterDal();
                CompanyBDto objCompanyBdto = new CompanyBDto();
                objCompanyBdto.CompanyType = radCmbType.SelectedValue;
                objCompanyBdto.CompanyName = txtCompanyName.Text.Trim();
                objCompanyBdto.AddressLine1 = txtAddressline1.Text.Trim();
                objCompanyBdto.AddressLine2 = txtAddressline2.Text.Trim();
                objCompanyBdto.CityId = Convert.ToInt32(radCmbCity.SelectedValue);
                objCompanyBdto.StateId = Convert.ToInt32(radCmbState.SelectedValue);
                objCompanyBdto.CountryId = Convert.ToInt32(radCmbCountry.SelectedValue);
                objCompanyBdto.Mobile = txtMobile.Text;
                objCompanyBdto.Pincode = txtPinCode.Text.ToString().Trim();
                objCompanyBdto.Phone = txtPhone.Text.ToString().Trim();
                objCompanyBdto.Fax = txtFax.Text.ToString().Trim();
                objCompanyBdto.Email = txtEmailId.Text.Trim();
                objCompanyBdto.ParentCompanyId = Convert.ToInt32(radCmbUnderWhome.SelectedValue);

                if (radCmbRegion.SelectedValue != "0")
                {
                    objCompanyBdto.RegionId = Convert.ToInt32(radCmbRegion.SelectedValue);
                }
                else
                {
                    objCompanyBdto.RegionId = 0;
                }


                //Saving Images

                byte[] image;
                string phototype = "";
                try
                {
                    string DefaultImage = imgCompany.ImageUrl;
                    if (Session["ImageData"] == null)
                    {
                        image = (byte[])ViewState["ImageData"];

                        lblFilePath.Text = "";
                    }
                    else
                    {
                        image = (byte[])Session["ImageData"];// FileUpload_Photo.FileBytes;
                        phototype = Session["ImageType"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    image = null;
                    phototype = "";
                }


                objCompanyBdto.Photo = image;
                objCompanyBdto.Phototype = phototype;


                // End Saving Images       




                objCompanyBdto.UserId = objAuthorizationBDto.UserProfile.UserId;
                result = objCompanyMasterDal.InsertCompany(objCompanyBdto);

                if (result >= 1)
                {


                    SaveDepartmentCompanyMap(result);

                    Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                    Master.MessageCssClass = "successMessage";
                    bisEdit = false;
                    cbxCompany.SaveButton.Enabled = false;

                    Session["COMPANY_ID"] = result;
                    acbCompany.EditableMode = false;
                    rtsCompanyDetails.Tabs[1].Enabled = true;



                }
                else if (result == -1)
                {
                    Master.DisplayMessage("Head office Already Exist");
                    Master.MessageCssClass = "errorMessage";

                }
                else
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                    Master.MessageCssClass = "errorMessage";
                }
                BindGrid();
                //  Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }
        #endregion

        private void ClearContact()
        { }


        #region Update
        private void Update()
        {
            try
            {
                int result = 0;
                objCompanyMasterDal = new CompanyMasterDal();
                CompanyBDto objCompanyBdto = new CompanyBDto();
                objCompanyBdto.CompanyType = radCmbType.SelectedValue;
                objCompanyBdto.CompanyName = txtCompanyName.Text.Trim();
                objCompanyBdto.AddressLine1 = txtAddressline1.Text.Trim();
                objCompanyBdto.AddressLine2 = txtAddressline2.Text.Trim();
                objCompanyBdto.CityId = Convert.ToInt32(radCmbCity.SelectedValue);
                objCompanyBdto.StateId = Convert.ToInt32(radCmbState.SelectedValue);
                objCompanyBdto.CountryId = Convert.ToInt32(radCmbCountry.SelectedValue);
                objCompanyBdto.Mobile = txtMobile.Text;
                objCompanyBdto.Pincode = txtPinCode.Text.ToString().Trim();
                objCompanyBdto.Phone = txtPhone.Text.ToString().Trim();
                objCompanyBdto.Fax = txtFax.Text.ToString().Trim();
                objCompanyBdto.Email = txtEmailId.Text.Trim();

                if (radCmbRegion.SelectedValue != "0")
                {
                    objCompanyBdto.RegionId = Convert.ToInt32(radCmbRegion.SelectedValue);
                }
                else
                {
                    objCompanyBdto.RegionId = 0;
                }

                objCompanyBdto.UserId = objAuthorizationBDto.UserProfile.UserId;
                objCompanyBdto.CompanyId = Convert.ToInt32(Session["COMPANY_ID"]);
                objCompanyBdto.ParentCompanyId = Convert.ToInt32(radCmbUnderWhome.SelectedValue);

                #region Update Saving Images

                byte[] image;
                string phototype = "";
                try
                {
                    string DefaultImage = imgCompany.ImageUrl;
                    if (Session["ImageData"] == null)
                    {
                        image = (byte[])ViewState["ImageData"];

                        lblFilePath.Text = "";
                    }
                    else
                    {
                        image = (byte[])Session["ImageData"];// FileUpload_Photo.FileBytes;
                        phototype = Session["ImageType"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    image = null;
                    phototype = "";
                }


                objCompanyBdto.Photo = image;
                objCompanyBdto.Phototype = phototype;


                #endregion 

                result = objCompanyMasterDal.UpdateCompany(objCompanyBdto);

                if (result == 1)
                {

                    SaveDepartmentCompanyMap(Convert.ToInt32(Session["COMPANY_ID"]));
                    acbCompany.DefaultMode = true;
                    Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                    Master.MessageCssClass = "successMessage";
                    bisEdit = false;
                    ViewState[PageConstants.vsItemIndexes] = null;
                    BindGrid();

                    cbxCompany.SaveButton.Enabled = false;
                    acbCompany.SaveButton.CommandName = "Save";



                }
                else if (result == -1)
                {
                    Master.DisplayMessage("Head office Already Exist");
                    Master.MessageCssClass = "errorMessage";

                }
                else
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Update].ToString());
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

        #region Save Contact
        private void SaveContact()
        {
            try
            {
                int result = 0;

                CompanyContactBDto objCompanyContactBdto = new CompanyContactBDto();
                objCompanyMasterDal = new CompanyMasterDal();
                objCompanyContactBdto.Name = txtName.Text.Trim();
                objCompanyContactBdto.Mobile = txtPersonMobile.Text.Trim();
                objCompanyContactBdto.Phone = txtPersonPhone.Text.Trim();
                objCompanyContactBdto.TitleId = Convert.ToInt32(radCmbTitle.SelectedValue);
                objCompanyContactBdto.Email = txtPersonEmail.Text.Trim();
                objCompanyContactBdto.CompanyId = Convert.ToInt32(Session["COMPANY_ID"]);
                result = objCompanyMasterDal.InsertCompanyContact(objCompanyContactBdto);

                if (result >= 1)
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                    Master.MessageCssClass = "successMessage";
                    bisEdit = false;
                    ResetContact();
                    pnlContactAdd.Visible = false;
                    acbCompany.DefaultMode = true;
                    acbCompany.Visible = true;
                    cbxCompany.Visible = false;
                    pnlContactGrid.Visible = true; ;

                }
                else
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                    Master.MessageCssClass = "errorMessage";
                }
                BindContactGrid(Convert.ToInt32(Session["COMPANY_ID"]), "");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }
        #endregion

        #region Update Contact

        private void UpdateContact()
        {
            try
            {
                int result = 0;
                objCompanyMasterDal = new CompanyMasterDal();

                String xmlData = GenerateXmlString(radgrdContact);

                result = objCompanyMasterDal.UpdateContact(xmlData);

                if (result == 1)
                {


                    Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                    Master.MessageCssClass = "successMessage";
                    bisEdit = false;
                    acbCompany.Visible = true;
                    cbxCompany.Visible = false;
                    acbCompany.DefaultMode = true;


                    if (ViewState[vsItemIndexesCt] != null)
                    {
                        htItemIndexCt = (Hashtable)ViewState[vsItemIndexesCt];

                        for (int i = 0; i < htItemIndexCt.Count; i++)
                            radgrdContact.Items[Convert.ToInt32(htItemIndexCt[i].ToString())].Edit = false;
                        bisEdit = false;
                        ViewState[vsItemIndexesCt] = null;
                        radgrdContact.Rebind();

                    }

                    BindContactGrid(Convert.ToInt32(Session["COMPANY_ID"]), "");

                }
                else
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Update].ToString());
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


        private void DeleteContact()
        {
            try
            {
                int result = 0;
                objCompanyMasterDal = new CompanyMasterDal();

                String xmlData = GenerateDeleteXmlString(radgrdContact);

                result = objCompanyMasterDal.DeleteContact(xmlData);

                if (result == 1)
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                    Master.MessageCssClass = "successMessage";
                    ViewState[vsItemIndexesCt] = null;
                    BindContactGrid(Convert.ToInt32(Session["COMPANY_ID"]), "");

                }
                else
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Update].ToString());
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
        private void ResetContact()
        {
            radCmbTitle.SelectedIndex = 0;
            txtPersonEmail.Text = "";
            txtPersonMobile.Text = "";
            txtPersonPhone.Text = "";
            txtName.Text = "";
        }


        #region Reset
        private void Reset()
        {
            txtAddressline1.Text = "";
            txtAddressline2.Text = "";
            txtCompanyName.Text = "";
            txtEmailId.Text = "";
            txtFax.Text = "";
            txtMobile.Text = "";
            txtPhone.Text = "";
            txtPinCode.Text = "";

            radCmbCity.SelectedIndex = 0;
            radCmbCountry.SelectedIndex = 0;
            radCmbState.SelectedIndex = 0;
            radCmbType.SelectedIndex = 0;
            radCmbRegion.SelectedIndex = 0;

            imgCompany.ImageUrl = "~/Views/Shared/Images/Defaultlogo.PNG";
        }


        #endregion

        #region Bind List

        #region  Company  related DropDowns
        public void BindCompanyDropdowns()
        {
            BindCompanyType();
            BindCountry();
            BindCompanyType();
            BindRegion();
            BindCompanyList();

        }


        private void BindCompanyType()
        {

            radCmbType.Items.Clear();

            RadComboBoxItem radcmbItem = new RadComboBoxItem();
            radcmbItem.Text = "Head Office";
            radcmbItem.Value = "H";
            radCmbType.Items.Add(radcmbItem);

            radcmbItem = new RadComboBoxItem();
            radcmbItem.Text = "Branch";
            radcmbItem.Value = "B";
            radCmbType.Items.Add(radcmbItem);

            radcmbItem = new RadComboBoxItem();
            radcmbItem.Text = "Franchises";
            radcmbItem.Value = "F";
            radCmbType.Items.Add(radcmbItem);
            radCmbType.Items.Insert(0, new RadComboBoxItem("", "0"));

        }

        private void BindCountry()
        {
            BindCombo objCmbBind = new BindCombo();
            DataSet DsCmb = null;
            DsCmb = objCmbBind.GetCountryKeyValue();
            radCmbCountry.Items.Clear();
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
            radCmbState.Items.Clear();
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
            radCmbCity.Items.Clear();
            radCmbCity.DataSource = DsCmb;
            radCmbCity.DataTextField = "CITY_NAME";
            radCmbCity.DataValueField = "CITY_ID";
            radCmbCity.DataBind();
            objCmbBind = null;
            DsCmb = null;
            radCmbCity.Items.Insert(0, new RadComboBoxItem("", "0"));
        }
        private void BindRegion()
        {
            BindCombo objCmbBind = new BindCombo();
            DataSet DsCmb = null;
            DsCmb = objCmbBind.GetRegionKeyValue();

            radCmbRegion.DataSource = DsCmb;
            radCmbRegion.DataTextField = "REGION_LONG_NAME";
            radCmbRegion.DataValueField = "REGION_ID";
            radCmbRegion.DataBind();
            objCmbBind = null;
            DsCmb = null;
            radCmbRegion.Items.Insert(0, new RadComboBoxItem("", "0"));

        }


        private void BindUnderWhome(String Type)
        {
            radCmbUnderWhome.ClearSelection();
            radCmbUnderWhome.Text = "";
            BindCombo objCmbBind = new BindCombo();
            DataSet DsCmb = null;
            DsCmb = objCmbBind.GetCompanyNameType(Type);

            radCmbUnderWhome.DataSource = DsCmb;
            radCmbUnderWhome.DataTextField = "COMPANY_NAME";
            radCmbUnderWhome.DataValueField = "COMPANY_ID";
            radCmbUnderWhome.DataBind();
            objCmbBind = null;
            DsCmb = null;
            radCmbUnderWhome.Items.Insert(0, new RadComboBoxItem("", "0"));

        }



        #endregion

        #region  Contact  related DropDowns
        private void BindTitle()
        {
            BindCombo objCmbBind = new BindCombo();
            DataSet DsCmb = null;
            DsCmb = objCmbBind.GetTitle();

            radCmbTitle.DataSource = DsCmb;
            radCmbTitle.DataTextField = "TITLE_DESC";
            radCmbTitle.DataValueField = "TITLE_ID";
            radCmbTitle.DataBind();
            objCmbBind = null;
            DsCmb = null;
            radCmbTitle.Items.Insert(0, new RadComboBoxItem("", "0"));

        }

        #endregion

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



        protected void radCmbType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            BindUnderWhome(radCmbType.SelectedValue);



        }
        #endregion


        protected void btnAssignRightDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                int CompanyCount = 0;
                for (int i = 0; i < lstDepartmentName.Items.Count; i++)
                {
                    if (lstDepartmentName.Items[i].Selected)
                        CompanyCount++;

                }
                if (CompanyCount > 0)
                {
                    for (int i = 0; i < lstDepartmentName.Items.Count; i++)
                    {
                        if (lstDepartmentName.Items[i].Selected)
                        {
                            lstAssignDeptName.Items.Add(lstDepartmentName.Items[i]);
                            lstDepartmentName.Items.Remove(lstDepartmentName.Items[i]);
                            lstDepartmentName.ClearSelection();
                            lstAssignDeptName.ClearSelection();
                        }
                    }

                }
                else
                {
                    // showMessage("Select Company");
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void btnAssignLeftDepartment_Click(object sender, EventArgs e)
        {
            int AssignCompanyCount = 0;
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["vsAssignedDepartment"];
            bool isPopup = false;
            for (int i = 0; i < lstAssignDeptName.Items.Count; i++)
            {
                if (lstAssignDeptName.Items[i].Selected)
                    AssignCompanyCount++;
            }
            if (AssignCompanyCount > 0)
            {


                lstDepartmentName.Items.Add(lstAssignDeptName.SelectedItem);
                lstAssignDeptName.Items.Remove(lstAssignDeptName.SelectedItem);
                lstDepartmentName.ClearSelection();
                lstAssignDeptName.ClearSelection();

                //}
            }
            else
            {
                // showMessage("Select CompanyName");
            }



        }


        private void BindAssignDepartment_ByCompanyId(int CmpId)
        {
            BindCombo objCmbBind = new BindCombo();
            DataSet ds = null;
            ds = objCmbBind.GetCompanyDepartmentMap(CmpId);

            lstAssignDeptName.Items.Clear();
            lstAssignDeptName.ClearSelection();
            lstAssignDeptName.DataTextField = "DEPARTMENT_NAME";
            lstAssignDeptName.DataValueField = "DEPARTMENT_ID";

            lstAssignDeptName.DataSource = ds;
            lstAssignDeptName.DataBind();
           ViewState["vsAssignedDepartment"] = ds.Tables[0];
        }


        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            string filename = string.Empty;


            switch (mvCompany.GetActiveView().ID)
            {
                case "vwCompany":
                    #region Company

                    radgrdCompany.ExportSettings.ExportOnlyData = true;
                    radgrdCompany.ExportSettings.OpenInNewWindow = true;
                    radgrdCompany.ExportSettings.IgnorePaging = true;
                    filename = "Company_" + System.DateTime.Now.ToString("ddmmyyyy");
                    radgrdCompany.ExportSettings.FileName = filename;
                    switch (format)
                    {
                        case ExportOptions.Excel:
                            {
                                radgrdCompany.MasterTableView.ExportToExcel();
                                break;
                            }
                        case ExportOptions.Word:
                            {
                                radgrdCompany.MasterTableView.ExportToWord();
                                break;
                            }
                        case ExportOptions.Pdf:
                            {
                                radgrdCompany.MasterTableView.ExportToPdf();
                                break;
                            }
                        case ExportOptions.Csv:
                            {
                                radgrdCompany.MasterTableView.ExportToCSV();
                                break;
                            }
                    }


                    break;
                    #endregion

                case "vwContact":

                    #region Contact

                    radgrdContact.ExportSettings.ExportOnlyData = true;
                    radgrdContact.ExportSettings.OpenInNewWindow = true;
                    radgrdContact.ExportSettings.IgnorePaging = true;
                    filename = "Company_Contact_" + System.DateTime.Now.ToString("ddmmyyyy");
                    radgrdContact.ExportSettings.FileName = filename;
                    switch (format)
                    {
                        case ExportOptions.Excel:
                            {
                                radgrdContact.MasterTableView.ExportToExcel();
                                break;
                            }
                        case ExportOptions.Word:
                            {
                                radgrdContact.MasterTableView.ExportToWord();
                                break;
                            }
                        case ExportOptions.Pdf:
                            {
                                radgrdContact.MasterTableView.ExportToPdf();
                                break;
                            }
                        case ExportOptions.Csv:
                            {
                                radgrdContact.MasterTableView.ExportToCSV();
                                break;
                            }
                    }

                    #endregion

                    break;






            }
        }

        protected void acbCompany_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbCompany_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbCompany_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbCompany_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion

        #region Bind Company List

        private void BindCompanyList()
        {

            BindCombo objCmbBind = new BindCombo();
            DataSet DsCmb = null;
            DataTable dt = new DataTable();


            DsCmb = objCmbBind.GetDepartmentKeyValue(""); 

            dt = DsCmb.Tables[0];

            lstDepartmentName.DataSource = dt;
            lstDepartmentName.DataTextField = "DEPARTMENT_NAME";
            lstDepartmentName.DataValueField = "DEPARTMENT_ID";
            lstDepartmentName.DataBind();
            objCmbBind = null;
            DsCmb = null;


            if (lstAssignDeptName.Items.Count > 0 && dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < lstAssignDeptName.Items.Count; j++)
                    {
                        if (dt.Rows[i]["DEPARTMENT_ID"].ToString() == lstAssignDeptName.Items[j].Value)
                        {
                            dt.Rows[i].Delete();
                            break;
                        }
                    }
                }
            }
            lstDepartmentName.DataSource = dt;
            lstDepartmentName.DataBind();






        }

        #endregion




        #region Save Department Company Mapping
        private void SaveDepartmentCompanyMap(int CompanyId)
        {
            try
            {
                int result = 0;
                String DepartmentIdList = String.Empty;

                for (int i = 0; i < lstAssignDeptName.Items.Count; i++)
                {
                    if (DepartmentIdList == string.Empty)
                    {
                        DepartmentIdList = lstAssignDeptName.Items[i].Value;
                    }
                    else
                    {
                        DepartmentIdList = DepartmentIdList + "," + lstAssignDeptName.Items[i].Value;
                    }

                }


                objCompanyMasterDal = new CompanyMasterDal();
                result = objCompanyMasterDal.InsertDepartmentCompanyMap(CompanyId, DepartmentIdList);
                

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

    }
}

