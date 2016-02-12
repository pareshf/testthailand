
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Text;
using CRM.Core.Constants;
using CRM.DataAccess.AdministrationDAL;
using CRM.Model.AdministrationModel;
using CRM.Model.Security;
using Telerik.Web.UI;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;
using System.Collections;

namespace CRM.WebApp.Views.Administration
{
    public partial class AgentMaster : System.Web.UI.Page
    {

        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        AgentDal objAgentLookup = null;
        public const String vsAgent = "Agent";
        AuthorizationBDto objAuthorizationBDto;
        AgentBDto objAgentBDto = null;
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
                Session["currentevent"] = "Agent";
            }
            hdnEditableMode.Value = "false";
            acbAgent.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdAgent.ClientID));
            acbAgent.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdAgent.ClientID));

        }

        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Agent");
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
        /// Bind Agent grid
        /// </summary>
        private void BindGrid()
        {
            objAgentLookup = new AgentDal();
            DataSet dsAgent = objAgentLookup.GetAgent("");
            radgrdAgent.DataSource = dsAgent;
            radgrdAgent.DataBind();
            ViewState[vsAgent] = dsAgent;

        }

        #endregion

        #region Save
        private void Save()
        {
            try
            {
                int result = 0;
                objAgentLookup = new AgentDal();
                objAgentBDto = new AgentBDto();
                objAgentBDto.AgentName = txtAgentName.Text;
                objAgentBDto.Email = txtEmail.Text;
                objAgentBDto.Mobile = txtMobile.Text;
                objAgentBDto.Phone = txtPhone.Text;
                objAgentBDto.Fax = txtFax.Text;

                objAgentBDto.UserId = objAuthorizationBDto.UserProfile.UserId;
                result = objAgentLookup.InsertAgent(objAgentBDto);
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
            txtAgentName.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtMobile.Text = String.Empty;
            txtPhone.Text = String.Empty;
            txtFax.Text = String.Empty;

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
            string xmlHeaderNodeStructure = "<Agent AGENT_ID=\"{0}\" AGENT_NAME=\"{1}\" EMAIL=\"{2}\" MOBILE=\"{3}\" PHONE=\"{4}\" FAX=\"{5}\" USER_ID=\"{6}\"></Agent>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int AgentId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblgrdAgentId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblgrdAgentIdEdit");
                        TextBox txtgrdAgentName = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdAgentName");
                        TextBox txtgrdEmail = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdEmail");
                        TextBox txtgrdMobile = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdMobile");
                        TextBox txtgrdPhone = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdPhone");
                        TextBox txtgrdFax = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdFax");
                        AgentId = int.Parse(lblgrdAgentId.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, AgentId, txtgrdAgentName.Text, txtgrdEmail.Text, txtgrdMobile.Text, txtgrdPhone.Text, txtgrdFax.Text, objAuthorizationBDto.UserProfile.UserId);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }

        #endregion

        #endregion

        #region Grid Event

        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
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

        /// <summary>
        /// Rad grid's item databound event which add client side event to check box of rows.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
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

        /// <summary>
        /// Assign data source after grid's page changing and sorting.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdAgent_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsAgent] != null)
                radgrdAgent.DataSource = ViewState[vsAgent];
        }

        #endregion

        #region Actionbar Events

        protected void acbAgent_NewClick(object sender, EventArgs e)
        {
            acbAgent.EditableMode = true;
            acbAgent.SaveNewButton.Visible = true;
            acbAgent.SaveButton.CommandName = "Save";
            pnlAddNewMode.Visible = true;
            txtAgentName.Focus();
            radgrdAgent.Rebind();

        }

        protected void acbAgent_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objAgentLookup = new AgentDal();
                switch (acbAgent.SaveButton.CommandName)
                {
                    case "Save":
                        Save();
                        pnlAddNewMode.Visible = false;
                        acbAgent.EditableMode = false;

                        break;

                    case "Update":
                        String xmlData = GenerateXmlString(radgrdAgent);
                        result = objAgentLookup.UpdateAgent(xmlData);
                        if (result == 1)
                        {
                            acbAgent.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdAgent.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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
                //objAgentBDto = new AgentDal();

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
            txtAgentName.Focus();

            Save();
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


                objAgentLookup = new AgentDal();
                String AgeId = AgentId.ToString().TrimEnd(',');
                result = objAgentLookup.DeleteAgent(AgeId);


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

                objAgentLookup = new AgentDal();
                DataSet dsAgent = objAgentLookup.GetAgent(acbAgent.SearchTextBox.Text);
                radgrdAgent.DataSource = dsAgent;
                radgrdAgent.DataBind();
                ViewState[vsAgent] = dsAgent;
                Reset();
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
                        radgrdAgent.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    radgrdAgent.Rebind();
                    acbAgent.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbAgent.SaveButton.CommandName = "Update";
                    acbAgent.SaveNewButton.Visible = false;
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

        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            radgrdAgent.ExportSettings.ExportOnlyData = true;
            radgrdAgent.ExportSettings.OpenInNewWindow = true;
            radgrdAgent.ExportSettings.IgnorePaging = true;

            string filename = "Agent_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdAgent.ExportSettings.FileName = filename;

            //radgrdDepartment.MasterTableView.GetColumn("chkDepartment").Visible = false;

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

            //ClientScript.RegisterStartupScript(GetType(), "", "window.location.reload();", true);
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

