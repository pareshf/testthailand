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

namespace CRM.WebApp.Views.Administration
{
    public partial class RegionLookup : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        RegionLookupDal objRegionLookup = null;
        public const String vsRegion = "Region";
        AuthorizationBDto objAuthorizationBDto;
        LookupBDto objLookupBDto = null;
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
                Session["currentevent"] = "Region";
            }
            hdnEditableMode.Value = "false";
            acbRegion.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdRegion.ClientID));
            acbRegion.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdRegion.ClientID));

        }

        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Region");
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

        #region Actionbar Events

        protected void acbRegion_NewClick(object sender, EventArgs e)
        {
            acbRegion.EditableMode = true;
            acbRegion.SaveNewButton.Visible = true;
            acbRegion.SaveButton.CommandName = "Save";
            pnlAddNewMode.Visible = true;
            txtRegionShortName.Focus();
            radgrdRegion.Rebind();
            Reset();
        }

        protected void acbRegion_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objRegionLookup = new RegionLookupDal();
                switch (acbRegion.SaveButton.CommandName)
                {
                    case "Save":
                        Save();
                        pnlAddNewMode.Visible = false;
                        acbRegion.EditableMode = false;

                        break;

                    case "Update":
                        String xmlData = GenerateXmlString(radgrdRegion);
                        result = objRegionLookup.UpdateRegion(xmlData);
                        if (result == 1)
                        {
                            acbRegion.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdRegion.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
                            bisEdit = false;
                            ViewState[PageConstants.vsItemIndexes] = null;
                            BindGrid();
                        }
                        else
                        {
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Update].ToString());
                            Master.MessageCssClass = "errorMessage";
                        }
                        break;
                }
                objRegionLookup = new RegionLookupDal();

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbRegion_SaveNewClick(object sender, EventArgs e)
        {
            txtRegionShortName.Focus();

            Save();
            Reset();
        }

        protected void acbRegion_CancelClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdRegion.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdRegion.Rebind();

            }
            acbRegion.DefaultMode = true;
            pnlAddNewMode.Visible = false;
        }

        protected void acbRegion_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder RegionId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {
                        Label lblRegionId = (Label)radgrdRegion.Items[i].FindControl("lblgrdRegionIdItem");

                        if (lblRegionId != null)
                        {
                            RegionId.Append(lblRegionId.Text + ",");
                        }
                    }
                }

                objRegionLookup = new RegionLookupDal();
                String RegId = RegionId.ToString().TrimEnd(',');
                result = objRegionLookup.DeleteRegion(RegId);


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

        protected void acbRegion_SearchClick(object sender, EventArgs e)
        {
            try
            {

                objRegionLookup = new RegionLookupDal();
                DataSet dsRegion = objRegionLookup.GetRegion(acbRegion.SearchTextBox.Text);
                radgrdRegion.DataSource = dsRegion;
                radgrdRegion.DataBind();
                ViewState[vsRegion] = dsRegion;
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }

        }

        protected void acbRegion_EditClick(object sender, EventArgs e)
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
                        radgrdRegion.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    radgrdRegion.Rebind();
                    acbRegion.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbRegion.SaveButton.CommandName = "Update";
                    acbRegion.SaveNewButton.Visible = false;
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbRegion_RefreshClick(object sender, EventArgs e)
        {
            acbRegion.DefaultMode = true;
            pnlAddNewMode.Visible = false;
            acbRegion.SearchTextBox.Text = String.Empty;
            BindGrid();


        }


        #endregion

        #region Grid Event
        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void radgrdRegion_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdRegion.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdRegion.Items)
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
        protected void radgrdRegion_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdRegion.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdRegion.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdRegion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsRegion] != null)
                radgrdRegion.DataSource = ViewState[vsRegion];
        }


        #endregion

        #region GridCheckBox event

        /// <summary>
        /// Saves checkbox checked status into hashtable after check status changed.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

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
                            radgrdRegion.Items[htItemIndex[i].ToString()].Edit = false;
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
        /// Bind Region grid
        /// </summary>

        private void BindGrid()
        {

            objRegionLookup = new RegionLookupDal();
            DataSet dsRegion = objRegionLookup.GetRegion("");
            radgrdRegion.DataSource = dsRegion;
            radgrdRegion.DataBind();
            ViewState[vsRegion] = dsRegion;

        }
        #endregion

        #region Save
        private void Save()
        {
            try
            {
                int result = 0;
                objRegionLookup = new RegionLookupDal();
                objLookupBDto = new LookupBDto();
                objLookupBDto.LookupName = txtRegionShortName.Text;
                objLookupBDto.Description = txtRegionLongName.Text;
                objLookupBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objRegionLookup.InsertRegion(objLookupBDto);
                if (result >= 1)
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                    Master.MessageCssClass = "successMessage";
                    bisEdit = false;
                }
                else
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
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

        #region Reset
        private void Reset()
        {
            txtRegionShortName.Text = String.Empty;
            txtRegionLongName.Text = String.Empty;
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
            string xmlHeaderNodeStructure = "<Region REGION_ID=\"{0}\" REGION_SHORT_NAME=\"{1}\" REGION_LONG_NAME=\"{2}\" USER_ID=\"{3}\"></Region>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int RegionId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblgrdRegionId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblgrdRegionIdEdit");
                        TextBox txtgrdRegionShortName = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdRegionShortName");
                        TextBox txtgrdRegionLongName = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdRegionLongName");
                        RegionId = int.Parse(lblgrdRegionId.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, RegionId, txtgrdRegionShortName.Text, txtgrdRegionLongName.Text, objAuthorizationBDto.UserProfile.UserId);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }

        #endregion

        #endregion

        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            radgrdRegion.ExportSettings.ExportOnlyData = true;
            radgrdRegion.ExportSettings.OpenInNewWindow = true;
            radgrdRegion.ExportSettings.IgnorePaging = true;

            string filename = "Region_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdRegion.ExportSettings.FileName = filename;



            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdRegion.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdRegion.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdRegion.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdRegion.MasterTableView.ExportToCSV();
                        break;
                    }
            }


        }


        protected void acbRegion_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbRegion_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbRegion_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbRegion_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion
    }
}
