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
    public partial class DepartmentLookup : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        DepartmentLookupDal objDepartmentLookup = null;
        public const String vsDepartment = "Department";
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
                Session["currentevent"] = "Department";
            }
            hdnEditableMode.Value = "false";
            acbDepartment.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdDepartment.ClientID));
            acbDepartment.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdDepartment.ClientID));

        }

        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Department");
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
        /// Bind Qualification grid
        /// </summary>
        private void BindGrid()
        {

            objDepartmentLookup = new DepartmentLookupDal();
            DataSet dsDepartment = objDepartmentLookup.GetDepartment("");
            radgrdDepartment.DataSource = dsDepartment;
            radgrdDepartment.DataBind();
            ViewState[vsDepartment] = dsDepartment;

        }

        #endregion

        #region Save
        private void Save()
        {
            try
            {
                int result = 0;
                objDepartmentLookup = new DepartmentLookupDal();
                objLookupBDto = new LookupBDto();
                objLookupBDto.LookupName = txtDepartmentName.Text;
                objLookupBDto.Description = txtDepartmentDesc.Text;
                objLookupBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objDepartmentLookup.InsertDepartment(objLookupBDto);
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
            txtDepartmentName.Text = String.Empty;
            txtDepartmentDesc.Text = String.Empty;

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
            string xmlHeaderNodeStructure = "<Department DEPARTMENT_ID=\"{0}\" DEPARTMENT_NAME=\"{1}\" DEPARTMENT_DESC=\"{2}\" USER_ID=\"{3}\"></Department>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int DepartmentId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblgrdDepartmentId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblgrdDepartmentIdEdit");
                        TextBox txtgrdDepartmentName = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdDepartmentName");
                        TextBox txtgrdDepartmentDesc = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdDepartmentDesc");
                        DepartmentId = int.Parse(lblgrdDepartmentId.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, DepartmentId, txtgrdDepartmentName.Text, txtgrdDepartmentDesc.Text, objAuthorizationBDto.UserProfile.UserId);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }

        #endregion

        #endregion

        #region Actionbar Events

        protected void acbDepartment_NewClick(object sender, EventArgs e)
        {
            acbDepartment.EditableMode = true;
            acbDepartment.SaveNewButton.Visible = true;
            acbDepartment.SaveButton.CommandName = "Save";
            pnlAddNewMode.Visible = true;
            txtDepartmentName.Focus();
            radgrdDepartment.Rebind();
            Reset();
        }

        protected void acbDepartment_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objDepartmentLookup = new DepartmentLookupDal();
                switch (acbDepartment.SaveButton.CommandName)
                {
                    case "Save":
                        Save();
                        pnlAddNewMode.Visible = false;
                        acbDepartment.EditableMode = false;

                        break;

                    case "Update":
                        String xmlData = GenerateXmlString(radgrdDepartment);
                        result = objDepartmentLookup.UpdateDepartment(xmlData);
                        if (result == 1)
                        {
                            acbDepartment.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdDepartment.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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
                objDepartmentLookup = new DepartmentLookupDal();

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbDepartment_SaveNewClick(object sender, EventArgs e)
        {
            txtDepartmentName.Focus();

            Save();
            Reset();
        }

        protected void acbDepartment_CancelClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdDepartment.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdDepartment.Rebind();

            }
            acbDepartment.DefaultMode = true;
            pnlAddNewMode.Visible = false;
        }

        protected void acbDepartment_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder DepartmentId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {
                        Label lblDepartmentId = (Label)radgrdDepartment.Items[i].FindControl("lblgrdDepartmentIdItem");

                        if (lblDepartmentId != null)
                        {
                            DepartmentId.Append(lblDepartmentId.Text + ",");
                        }
                    }
                }

                objDepartmentLookup = new DepartmentLookupDal();
                String DeptId = DepartmentId.ToString().TrimEnd(',');
                result = objDepartmentLookup.DeleteDepartment(DeptId);


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

        protected void acbDepartment_SearchClick(object sender, EventArgs e)
        {
            try
            {

                objDepartmentLookup = new DepartmentLookupDal();
                DataSet dsDepartment = objDepartmentLookup.GetDepartment(acbDepartment.SearchTextBox.Text);
                radgrdDepartment.DataSource = dsDepartment;
                radgrdDepartment.DataBind();
                ViewState[vsDepartment] = dsDepartment;
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }

        }

        protected void acbDepartment_EditClick(object sender, EventArgs e)
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
                        radgrdDepartment.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    radgrdDepartment.Rebind();
                    acbDepartment.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbDepartment.SaveButton.CommandName = "Update";
                    acbDepartment.SaveNewButton.Visible = false;
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbDepartment_RefreshClick(object sender, EventArgs e)
        {
            acbDepartment.DefaultMode = true;
            pnlAddNewMode.Visible = false;
            acbDepartment.SearchTextBox.Text = String.Empty;
            BindGrid();
        }
        #endregion

        #region Grid Event

        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdDepartment_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdDepartment.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdDepartment.Items)
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
        protected void radgrdDepartment_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdDepartment.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdDepartment.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdDepartment_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsDepartment] != null)
                radgrdDepartment.DataSource = ViewState[vsDepartment];
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
                            radgrdDepartment.Items[htItemIndex[i].ToString()].Edit = false;
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
            radgrdDepartment.ExportSettings.ExportOnlyData = true;
            radgrdDepartment.ExportSettings.OpenInNewWindow = true;
            radgrdDepartment.ExportSettings.IgnorePaging = true;

            string filename = "Department_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdDepartment.ExportSettings.FileName = filename;

            //radgrdDepartment.MasterTableView.GetColumn("chkDepartment").Visible = false;

            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdDepartment.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdDepartment.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdDepartment.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdDepartment.MasterTableView.ExportToCSV();
                        break;
                    }
            }

            //ClientScript.RegisterStartupScript(GetType(), "", "window.location.reload();", true);
        }

        protected void acbDepartment_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbDepartment_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbDepartment_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbDepartment_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion

    }
}
