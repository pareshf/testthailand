#region Program Information
/**********************************************************************************************************************************************
 Class Name           : RelationLookUp
 Class Description    : Implementation logic for save, edit, delete and find operation for Relation details.
 Author               : Priyam.
 Created Date         : Mar 6, 2010
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
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;
using System.Web;
#endregion



namespace CRM.WebApp.Views.Administration
{
    public partial class RelationLookUp : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        RelationLookUpDal objRelationLookUp = null;
        public const String vsRelation = "Relation";
        AuthorizationBDto objAuthorizationBDto;
        LookupBDto objLookupBDto = null;
        #endregion

        #region Event

        #region Actionbar Events

        protected void acbRelation_NewClick(object sender, EventArgs e)
        {
            acbRelation.EditableMode = true;
            acbRelation.SaveButton.CommandName = "Save";
            pnlAddNewMode.Visible = true;
            txtRelationDesc.Focus();
            radgrdRelation.Rebind();
            Reset();
        }

        protected void acbRelation_SaveClick(object sender, EventArgs e)
        {
            int result = 0;
            objRelationLookUp = new RelationLookUpDal();
            switch (acbRelation.SaveButton.CommandName)
            {
                case "Save":
                    Save();
                    pnlAddNewMode.Visible = false;
                    acbRelation.EditableMode = false;

                    break;

                case "Update":
                    String xmlData = GenerateXmlString(radgrdRelation);
                    result = objRelationLookUp.UpdateRelation(xmlData);
                    if (result == 1)
                    {
                        acbRelation.DefaultMode = true;
                        Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                        Master.MessageCssClass = "successMessage";

                        if (ViewState[PageConstants.vsItemIndexes] != null)
                            htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                        for (int i = 0; i < htItemIndex.Count; i++)
                            radgrdRelation.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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
            objRelationLookUp = new RelationLookUpDal();

        }

        protected void acbRelation_SaveNewClick(object sender, EventArgs e)
        {
            txtRelationDesc.Focus();
            Save();
            Reset();
        }

        protected void acbRelation_EditClick(object sender, EventArgs e)
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
                        radgrdRelation.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    //Session[PageConstants.ssnHasTableIndex] = htItemIndex;
                    radgrdRelation.Rebind();
                    acbRelation.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbRelation.SaveNewButton.Visible = false;
                    acbRelation.SaveButton.CommandName = "Update";
                }
            }

            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbRelation_CancelClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdRelation.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdRelation.Rebind();

            }
            acbRelation.DefaultMode = true;
            pnlAddNewMode.Visible = false;
        }

        protected void acbRelation_DeleteClick(object sender, EventArgs e)
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

                        Label lblRelationId = (Label)radgrdRelation.Items[i].FindControl("lblgrdRelationIdItem");
                        if (lblRelationId != null)
                        {
                            RelationId.Append(lblRelationId.Text + ",");
                        }
                    }
                }

                objRelationLookUp = new RelationLookUpDal();
                String RelaId = RelationId.ToString().TrimEnd(',');
                result = objRelationLookUp.DeleteRelation(RelaId);


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

        protected void acbRelation_SearchClick(object sender, EventArgs e)
        {
            try
            {
                acbRelation.SearchTextBox.Focus();
                objRelationLookUp = new RelationLookUpDal();
                DataSet dsRelation = objRelationLookUp.GetRelation(acbRelation.SearchTextBox.Text);
                radgrdRelation.DataSource = dsRelation;
                radgrdRelation.DataBind();
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

        protected void acbRelation_RefreshClick(object sender, EventArgs e)
        {
            acbRelation.DefaultMode = true;
            pnlAddNewMode.Visible = false;
            acbRelation.SearchTextBox.Text = String.Empty;
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
                Session["currentevent"] = "Relation";

            }
            hdnEditableMode.Value = "false";
            acbRelation.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdRelation.ClientID));
            acbRelation.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdRelation.ClientID));

        }

        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Relation");
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

        protected void radgrdRelation_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdRelation.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdRelation.Items)
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
        protected void radgrdRelation_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdRelation.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdRelation.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdRelation_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsRelation] != null)
                radgrdRelation.DataSource = ViewState[vsRelation];
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
                            radgrdRelation.Items[htItemIndex[i].ToString()].Edit = false;
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
            objRelationLookUp = new RelationLookUpDal();
            DataSet dsRelation = objRelationLookUp.GetRelation("");
            radgrdRelation.DataSource = dsRelation;
            radgrdRelation.DataBind();
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
            string xmlHeaderNodeStructure = "<Relation RELATION_ID=\"{0}\" RELATION_DESC=\"{1}\" USER_ID=\"{2}\"></Relation>";
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
                objRelationLookUp = new RelationLookUpDal();
                objLookupBDto = new LookupBDto();
                objLookupBDto.LookupName = txtRelationDesc.Text;
                objLookupBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objRelationLookUp.InsertRelation(objLookupBDto);
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

            txtRelationDesc.Text = String.Empty;


        }
        #endregion
        #endregion

        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            radgrdRelation.ExportSettings.ExportOnlyData = true;
            radgrdRelation.ExportSettings.OpenInNewWindow = true;
            radgrdRelation.ExportSettings.IgnorePaging = true;

            string filename = "Address_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdRelation.ExportSettings.FileName = filename;



            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdRelation.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdRelation.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdRelation.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdRelation.MasterTableView.ExportToCSV();
                        break;
                    }
            }


        }


        protected void acbRelation_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbRelation_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbRelation_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbRelation_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion
    }
}
