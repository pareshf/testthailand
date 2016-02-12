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
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;
using System.Web;
#endregion

namespace CRM.WebApp.Views.Administration
{
    public partial class EmailTemplateLookup : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        EmailTemplateDal EmailTemplateDal = null;
        public const String vsRelation = "EmailTemplate";
        AuthorizationBDto objAuthorizationBDto;
        LookupBDto objLookupBDto = null;
        #endregion

        #region Event

        #region Actionbar Events

        protected void acbEmailTemplate_NewClick(object sender, EventArgs e)
        {
            acbEmailTemplate.EditableMode = true;
            acbEmailTemplate.SaveButton.CommandName = "Save";
            pnlAddNewMode.Visible = true;
            txtEmailTemplateDesc.Focus();
            radgrdEmailTemplate.Rebind();
            Reset();
        }

        protected void acbEmailTemplate_SaveClick(object sender, EventArgs e)
        {
            int result = 0;
            EmailTemplateDal = new EmailTemplateDal();
            switch (acbEmailTemplate.SaveButton.CommandName)
            {
                case "Save":
                    Save();
                    pnlAddNewMode.Visible = false;
                    acbEmailTemplate.EditableMode = false;

                    break;

                case "Update":
                    String xmlData = GenerateXmlString(radgrdEmailTemplate);
                    result = EmailTemplateDal.UpdateEmailTemplate(xmlData);
                    if (result == 1)
                    {
                        acbEmailTemplate.DefaultMode = true;
                        Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                        Master.MessageCssClass = "successMessage";

                        if (ViewState[PageConstants.vsItemIndexes] != null)
                            htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                        for (int i = 0; i < htItemIndex.Count; i++)
                            radgrdEmailTemplate.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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
            EmailTemplateDal = new EmailTemplateDal();

        }

        protected void acbEmailTemplate_SaveNewClick(object sender, EventArgs e)
        {
            txtEmailTemplateDesc.Focus();
            Save();
            Reset();
        }

        protected void acbEmailTemplate_EditClick(object sender, EventArgs e)
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
                        radgrdEmailTemplate.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    //Session[PageConstants.ssnHasTableIndex] = htItemIndex;
                    radgrdEmailTemplate.Rebind();
                    acbEmailTemplate.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbEmailTemplate.SaveNewButton.Visible = false;
                    acbEmailTemplate.SaveButton.CommandName = "Update";
                }
            }

            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbEmailTemplate_CancelClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdEmailTemplate.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdEmailTemplate.Rebind();

            }
            acbEmailTemplate.DefaultMode = true;
            pnlAddNewMode.Visible = false;
        }

        protected void acbEmailTemplate_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder RelationId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {

                        Label lblRelationId = (Label)radgrdEmailTemplate.Items[i].FindControl("lblgrdRelationIdItem");
                        if (lblRelationId != null)
                        {
                            RelationId.Append(lblRelationId.Text + ",");
                        }
                    }
                }

                EmailTemplateDal = new EmailTemplateDal();
                String RelaId = RelationId.ToString().TrimEnd(',');
                result = EmailTemplateDal.DeleteEmailTemplate(RelaId);


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

        protected void acbEmailTemplate_SearchClick(object sender, EventArgs e)
        {
            try
            {
                acbEmailTemplate.SearchTextBox.Focus();
                EmailTemplateDal = new EmailTemplateDal();
                DataSet dsRelation = EmailTemplateDal.GetEmailTemplate(acbEmailTemplate.SearchTextBox.Text);
                radgrdEmailTemplate.DataSource = dsRelation;
                radgrdEmailTemplate.DataBind();
                ViewState[vsRelation] = dsRelation;
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }


        }

        protected void acbEmailTemplate_RefreshClick(object sender, EventArgs e)
        {
            acbEmailTemplate.DefaultMode = true;
            pnlAddNewMode.Visible = false;
            acbEmailTemplate.SearchTextBox.Text = String.Empty;
            BindGrid();


        }



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
            }
            hdnEditableMode.Value = "false";
            acbEmailTemplate.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdEmailTemplate.ClientID));
            acbEmailTemplate.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdEmailTemplate.ClientID));

        }

        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Email Template");
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

        #region Grid Event
        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void radgrdEmailTemplate_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdEmailTemplate.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdEmailTemplate.Items)
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
        protected void radgrdEmailTemplate_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdEmailTemplate.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdEmailTemplate.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdEmailTemplate_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsRelation] != null)
                radgrdEmailTemplate.DataSource = ViewState[vsRelation];
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
                            radgrdEmailTemplate.Items[htItemIndex[i].ToString()].Edit = false;
                            htItemIndex.Remove(i);

                            break;
                        }
                    }
                }
            }
            ViewState.Add(PageConstants.vsItemIndexes, htItemIndex);
        }
        #endregion



        #endregion

        #region Method

        #region Bind Grid

        /// <summary>
        /// Bind Relation grid
        /// </summary>

        private void BindGrid()
        {
            EmailTemplateDal = new EmailTemplateDal();
            DataSet dsRelation = EmailTemplateDal.GetEmailTemplate("");
            radgrdEmailTemplate.DataSource = dsRelation;
            radgrdEmailTemplate.DataBind();
            ViewState[vsRelation] = dsRelation;
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
            string xmlHeaderNodeStructure = "<Relation TEMPLATE_ID=\"{0}\" TEMPLATE_TYPE=\"{1}\" USER_ID=\"{2}\"></Relation>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int RelationId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblgrdRelationId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblgrdRelationIdEdit");
                        TextBox txtgrdRelationDesc = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdRelationDesc");
                        RelationId = int.Parse(lblgrdRelationId.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, RelationId, txtgrdRelationDesc.Text, objAuthorizationBDto.UserProfile.UserId);
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
                EmailTemplateDal = new EmailTemplateDal();
                objLookupBDto = new LookupBDto();
                objLookupBDto.LookupName = txtEmailTemplateDesc.Text;
                objLookupBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = EmailTemplateDal.InsertEmailTemplate(objLookupBDto);
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

            txtEmailTemplateDesc.Text = String.Empty;


        }
        #endregion
        #endregion

        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            radgrdEmailTemplate.ExportSettings.ExportOnlyData = true;
            radgrdEmailTemplate.ExportSettings.OpenInNewWindow = true;
            radgrdEmailTemplate.ExportSettings.IgnorePaging = true;

            string filename = "Address_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdEmailTemplate.ExportSettings.FileName = filename;



            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdEmailTemplate.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdEmailTemplate.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdEmailTemplate.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdEmailTemplate.MasterTableView.ExportToCSV();
                        break;
                    }
            }


        }


        protected void acbEmailTemplate_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbEmailTemplate_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbEmailTemplate_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbEmailTemplate_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion
    }
}
