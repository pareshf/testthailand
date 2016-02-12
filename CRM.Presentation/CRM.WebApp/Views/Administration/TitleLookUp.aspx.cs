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


namespace CRM.WebApp.Views.Administration
{
    public partial class TitleLookUp : System.Web.UI.Page
    {


        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        TitleLookUpDal objTitleLookUp = null;
        public const String vsTitle = "Title";
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
                Session["currentevent"] = "Title";
            }
            hdnEditableMode.Value = "false";
            acbTitle.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdTitle.ClientID));
            acbTitle.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdTitle.ClientID));

        }

        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Title");
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

        /// <summary>
        /// Bind Title grid
        /// </summary>

        private void BindGrid()
        {
            objTitleLookUp = new TitleLookUpDal();
            DataSet dsTitle = objTitleLookUp.GetTitle("");
            radgrdTitle.DataSource = dsTitle;
            radgrdTitle.DataBind();
            ViewState[vsTitle] = dsTitle;

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
            string xmlHeaderNodeStructure = "<Title TITLE_ID=\"{0}\" TITLE_DESC=\"{1}\" USER_ID=\"{2}\"></Title>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int TitleId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblgrdTitleId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblgrdTitleIdEdit");
                        TextBox txtgrdTitleDesc = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdTitleDesc");
                        TitleId = int.Parse(lblgrdTitleId.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, TitleId, txtgrdTitleDesc.Text, objAuthorizationBDto.UserProfile.UserId);
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
                objTitleLookUp = new TitleLookUpDal();
                objLookupBDto = new LookupBDto();
                objLookupBDto.LookupName = txtTitleDesc.Text;
                objLookupBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objTitleLookUp.InsertTitle(objLookupBDto);
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
            txtTitleDesc.Text = String.Empty;

        }
        #endregion
        #endregion


        #region Actionbar Events

        protected void acbTitle_NewClick(object sender, EventArgs e)
        {
            acbTitle.EditableMode = true;
            acbTitle.SaveButton.CommandName = "Save";
            pnlAddNewMode.Visible = true;
            txtTitleDesc.Focus();
            radgrdTitle.Rebind();
            Reset();
        }

        protected void acbTitle_SaveClick(object sender, EventArgs e)
        {

            try
            {
                int result = 0;
                objTitleLookUp = new TitleLookUpDal();
                switch (acbTitle.SaveButton.CommandName)
                {
                    case "Save":
                        Save();
                        pnlAddNewMode.Visible = false;
                        acbTitle.EditableMode = false;

                        break;

                    case "Update":
                        String xmlData = GenerateXmlString(radgrdTitle);
                        result = objTitleLookUp.UpdateTitle(xmlData);
                        if (result == 1)
                        {
                            acbTitle.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdTitle.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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
                objTitleLookUp = new TitleLookUpDal();

            }


            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbTitle_SaveNewClick(object sender, EventArgs e)
        {
            txtTitleDesc.Focus();
            Save();
            Reset();
        }

        protected void acbTitle_CancelClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdTitle.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdTitle.Rebind();

            }

            acbTitle.DefaultMode = true;
            pnlAddNewMode.Visible = false;
        }

        protected void acbTitle_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder TitleId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {

                        Label lblTitleId = (Label)radgrdTitle.Items[i].FindControl("lblgrdTitleIdItem");
                        if (lblTitleId != null)
                        {
                            TitleId.Append(lblTitleId.Text + ",");
                        }
                    }
                }

                objTitleLookUp = new TitleLookUpDal();
                String TitId = TitleId.ToString().TrimEnd(',');
                result = objTitleLookUp.DeleteTitle(TitId);


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

        protected void acbTitle_EditClick(object sender, EventArgs e)
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
                        radgrdTitle.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;

                    }
                    bisEdit = true;
                    radgrdTitle.Rebind();
                    acbTitle.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbTitle.SaveNewButton.Visible = false;
                    acbTitle.SaveButton.CommandName = "Update";


                }

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbTitle_SearchClick(object sender, EventArgs e)
        {
            try
            {
                objTitleLookUp = new TitleLookUpDal();
                DataSet dsTitle = objTitleLookUp.GetTitle(acbTitle.SearchTextBox.Text);
                radgrdTitle.DataSource = dsTitle;
                radgrdTitle.DataBind();
                ViewState[vsTitle] = dsTitle;

            }


            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbTitle_RefreshClick(object sender, EventArgs e)
        {
            acbTitle.DefaultMode = true;
            pnlAddNewMode.Visible = false;
            acbTitle.SearchTextBox.Text = String.Empty;
            BindGrid();


        }

        #endregion

        #region Grid Event
        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void radgrdTitle_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdTitle.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdTitle.Items)
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
        protected void radgrdTitle_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdTitle.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdTitle.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdTitle_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsTitle] != null)
                radgrdTitle.DataSource = ViewState[vsTitle];
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
                            radgrdTitle.Items[htItemIndex[i].ToString()].Edit = false;
                            htItemIndex.Remove(i);

                            break;
                        }
                    }
                }
            }
            ViewState.Add(PageConstants.vsItemIndexes, htItemIndex);
        }

        #endregion

        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            radgrdTitle.ExportSettings.ExportOnlyData = true;
            radgrdTitle.ExportSettings.OpenInNewWindow = true;
            radgrdTitle.ExportSettings.IgnorePaging = true;

            string filename = "Title_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdTitle.ExportSettings.FileName = filename;



            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdTitle.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdTitle.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdTitle.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdTitle.MasterTableView.ExportToCSV();
                        break;
                    }
            }


        }


        protected void acbTitle_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbTitle_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbTitle_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbTitle_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion

    }
}
