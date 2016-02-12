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
    public partial class DesignationLookup : System.Web.UI.Page
    {

        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        DesignationLookupDal objDesignationLookup = null;
        public const String vsDesignation = "Designation";
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
                Session["currentevent"] = "Designation";
            }
            hdnEditableMode.Value = "false";
            acbDesignation.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdDesignation.ClientID));
            acbDesignation.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdDesignation.ClientID));

        }

        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Designation");
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
        /// Bind Designation grid
        /// </summary>

        private void BindGrid()
        {
            objDesignationLookup = new DesignationLookupDal();
            DataSet dsDesignation = objDesignationLookup.GetDesignation("");
            radgrdDesignation.DataSource = dsDesignation;
            radgrdDesignation.DataBind();
            ViewState[vsDesignation] = dsDesignation;

        }
        #endregion

        #region Save
        private void Save()
        {
            try
            {
                int result = 0;
                objDesignationLookup = new DesignationLookupDal();
                objLookupBDto = new LookupBDto();
                objLookupBDto.LookupName = txtDesignationDesc.Text;
                objLookupBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objDesignationLookup.InsertDesignation(objLookupBDto);
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

            txtDesignationDesc.Text = String.Empty;
            


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
            string xmlHeaderNodeStructure = "<Designation DESIGNATION_ID=\"{0}\" DESIGNATION_DESC=\"{1}\" USER_ID=\"{2}\"></Designation>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int DesignationId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblgrdDesignationId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblgrdDesignationIdEdit");
                        TextBox txtgrdDesignationDesc = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdDesignationDesc");
                        DesignationId = int.Parse(lblgrdDesignationId.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, DesignationId, txtgrdDesignationDesc.Text, objAuthorizationBDto.UserProfile.UserId);
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

        protected void acbDesignation_NewClick(object sender, EventArgs e)
        {
            acbDesignation.EditableMode = true;
            acbDesignation.SaveButton.CommandName = "Save";
            pnlAddNewMode.Visible = true;
            txtDesignationDesc.Focus();
            radgrdDesignation.Rebind();
            Reset();
        }

        protected void acbDesignation_SaveClick(object sender, EventArgs e)
        {

            try
            {
                int result = 0;
                objDesignationLookup = new DesignationLookupDal();
                switch (acbDesignation.SaveButton.CommandName)
                {
                    case "Save":
                        Save();
                        pnlAddNewMode.Visible = false;
                        acbDesignation.EditableMode = false;

                        break;

                    case "Update":
                        String xmlData = GenerateXmlString(radgrdDesignation);
                        result = objDesignationLookup.UpdateDesignation(xmlData);
                        if (result == 1)
                        {
                            acbDesignation.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdDesignation.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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
                objDesignationLookup = new DesignationLookupDal();

            }


            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbDesignation_SaveNewClick(object sender, EventArgs e)
        {
            txtDesignationDesc.Focus();
            Save();
            Reset();
        }

        protected void acbDesignation_CancelClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdDesignation.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdDesignation.Rebind();

            }

            acbDesignation.DefaultMode = true;
            pnlAddNewMode.Visible = false;
        }

        protected void acbDesignation_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder DesignationId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {

                        Label lblDesignationId = (Label)radgrdDesignation.Items[i].FindControl("lblgrdDesignationIdItem");
                        if (lblDesignationId != null)
                        {
                            DesignationId.Append(lblDesignationId.Text + ",");
                        }
                    }
                }

                objDesignationLookup = new DesignationLookupDal();
                String DesigId = DesignationId.ToString().TrimEnd(',');
                result = objDesignationLookup.DeleteDesignation(DesigId);


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

        protected void acbDesignation_EditClick(object sender, EventArgs e)
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
                        radgrdDesignation.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;

                    }
                    bisEdit = true;
                    radgrdDesignation.Rebind();
                    acbDesignation.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbDesignation.SaveNewButton.Visible = false;
                    acbDesignation.SaveButton.CommandName = "Update";


                }

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbDesignation_SearchClick(object sender, EventArgs e)
        {
            try
            {
                
                objDesignationLookup = new DesignationLookupDal();
                DataSet dsDesignation = objDesignationLookup.GetDesignation(acbDesignation.SearchTextBox.Text);
                radgrdDesignation.DataSource = dsDesignation;
                radgrdDesignation.DataBind();
                ViewState[vsDesignation] = dsDesignation;
                Reset();

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbDesignation_RefreshClick(object sender, EventArgs e)
        {
            acbDesignation.DefaultMode = true;
            pnlAddNewMode.Visible = false;
            acbDesignation.SearchTextBox.Text = String.Empty;
            BindGrid();
          
            
        }

        #endregion

        #region Grid Event
        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void radgrdDesigion_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdDesignation.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdDesignation.Items)
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
        protected void radgrdDesigion_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdDesignation.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdDesignation.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdDesigion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsDesignation] != null)
                radgrdDesignation.DataSource = ViewState[vsDesignation];
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
                            radgrdDesignation.Items[htItemIndex[i].ToString()].Edit = false;
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
            radgrdDesignation.ExportSettings.ExportOnlyData = true;
            radgrdDesignation.ExportSettings.OpenInNewWindow = true;
            radgrdDesignation.ExportSettings.IgnorePaging = true;

            string filename = "Designation_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdDesignation.ExportSettings.FileName = filename;

            

            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdDesignation.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdDesignation.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdDesignation.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdDesignation.MasterTableView.ExportToCSV();
                        break;
                    }
            }

            
        }


        protected void acbDesignation_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbDesignation_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbDesignation_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbDesignation_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion


    }

}
