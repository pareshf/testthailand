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
    public partial class ProfessionLookup : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        ProfessionLookUpDal objProfessionLookUp = null;
        public const String vsProfession = "Profession";
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
                Session["currentevent"] = "Profession";

            }
            hdnEditableMode.Value = "false";
            acbProfession.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdProfession.ClientID));
            acbProfession.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdProfession.ClientID));

        }

        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Profession");
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
        /// Bind Profession grid
        /// </summary>

        private void BindGrid()
        {

            objProfessionLookUp = new ProfessionLookUpDal();
            DataSet dsProfession = objProfessionLookUp.GetProfession("");
            radgrdProfession.DataSource = dsProfession;
            radgrdProfession.DataBind();
            ViewState[vsProfession] = dsProfession;

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
            string xmlHeaderNodeStructure = "<Profession PROFESSION_ID=\"{0}\" PROFESSION_DESC=\"{1}\" USER_ID=\"{2}\"></Profession>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int ProfessionId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblgrdProfessionId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblgrdProfessionIdEdit");
                        TextBox txtgrdProfessionDesc = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdProfessionDesc");
                        ProfessionId = int.Parse(lblgrdProfessionId.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, ProfessionId, txtgrdProfessionDesc.Text, objAuthorizationBDto.UserProfile.UserId);
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
                objProfessionLookUp = new ProfessionLookUpDal();
                objLookupBDto = new LookupBDto();
                objLookupBDto.LookupName = txtProfessionDesc.Text;
                objLookupBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objProfessionLookUp.InsertProfession(objLookupBDto);
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
            txtProfessionDesc.Text = String.Empty;

        }
        #endregion

        #endregion

        #region Actionbar Events

        protected void acbProfession_NewClick(object sender, EventArgs e)
        {
            acbProfession.EditableMode = true;
            acbProfession.SaveButton.CommandName = "Save";
            pnlAddNewMode.Visible = true;
            txtProfessionDesc.Focus();
            radgrdProfession.Rebind();
            Reset();
        }

        protected void acbProfession_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objProfessionLookUp = new ProfessionLookUpDal();
                switch (acbProfession.SaveButton.CommandName)
                {
                    case "Save":
                        Save();
                        pnlAddNewMode.Visible = false;
                        acbProfession.EditableMode = false;

                        break;

                    case "Update":
                        String xmlData = GenerateXmlString(radgrdProfession);
                        result = objProfessionLookUp.UpdateProfession(xmlData);
                        if (result == 1)
                        {
                            acbProfession.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdProfession.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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
                objProfessionLookUp = new ProfessionLookUpDal();

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbProfession_SaveNewClick(object sender, EventArgs e)
        {
            txtProfessionDesc.Focus();
            Save();
            Reset();
        }

        protected void acbProfession_CancelClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdProfession.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdProfession.Rebind();

            }
            acbProfession.DefaultMode = true;
            pnlAddNewMode.Visible = false;
        }

        protected void acbProfession_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder ProfessionId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {

                        Label lblProfessionId = (Label)radgrdProfession.Items[i].FindControl("lblgrdProfessionIdItem");
                        if (lblProfessionId != null)
                        {
                            ProfessionId.Append(lblProfessionId.Text + ",");
                        }
                    }
                }

                objProfessionLookUp = new ProfessionLookUpDal();
                String ProfeId = ProfessionId.ToString().TrimEnd(',');
                result = objProfessionLookUp.DeleteProfession(ProfeId);


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

        protected void acbProfession_EditClick(object sender, EventArgs e)
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
                        radgrdProfession.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    radgrdProfession.Rebind();
                    acbProfession.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbProfession.SaveButton.CommandName = "Update";
                    acbProfession.SaveNewButton.Visible = false;
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbProfession_SearchClick(object sender, EventArgs e)
        {
            try
            {

                objProfessionLookUp = new ProfessionLookUpDal();
                DataSet dsProfession = objProfessionLookUp.GetProfession(acbProfession.SearchTextBox.Text);
                radgrdProfession.DataSource = dsProfession;
                radgrdProfession.DataBind();
                ViewState[vsProfession] = dsProfession;
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }

        }

        protected void acbProfession_RefreshClick(object sender, EventArgs e)
        {
            acbProfession.DefaultMode = true;
            pnlAddNewMode.Visible = false;
            acbProfession.SearchTextBox.Text = String.Empty;
            BindGrid();


        }

        #endregion

        #region Grid Event
        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void radgrdProfession_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdProfession.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdProfession.Items)
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
        protected void radgrdProfession_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdProfession.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdProfession.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdProfession_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsProfession] != null)
                radgrdProfession.DataSource = ViewState[vsProfession];
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
                            radgrdProfession.Items[htItemIndex[i].ToString()].Edit = false;
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
            radgrdProfession.ExportSettings.ExportOnlyData = true;
            radgrdProfession.ExportSettings.OpenInNewWindow = true;
            radgrdProfession.ExportSettings.IgnorePaging = true;

            string filename = "Profession_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdProfession.ExportSettings.FileName = filename;



            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdProfession.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdProfession.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdProfession.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdProfession.MasterTableView.ExportToCSV();
                        break;
                    }
            }


        }


        protected void acbProfession_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbProfession_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbProfession_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbProfession_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion

    }
}
