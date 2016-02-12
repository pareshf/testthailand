using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CRM.DataAccess.SecurityDAL;
using CRM.Model.Security;
using CRM.Core.Constants;

namespace CRM.WebApp.Views.Shared.Controls.Navigation
{
    public partial class ActionBar : System.Web.UI.UserControl
    {
        #region Member Declaration

        AuthorizationBDto objAuthorizationBDto;

        public event System.EventHandler btnNewClick;
        public event System.EventHandler btnEditClick;
        public event System.EventHandler btnDeleteClick;
        public event System.EventHandler btnExportClick;
        public event System.EventHandler btnSearchClick;
        public event System.EventHandler btnSaveClick;
        public event System.EventHandler btnSaveNewClick;
        public event System.EventHandler btnCancelClick;
        public event System.EventHandler btnRefreshClick;

        public event System.EventHandler imgExcelExport_Click;
        public event System.EventHandler imgWordExport_Click;
        public event System.EventHandler imgPdfExport_Click;
        public event System.EventHandler imgCsvExport_Click;

        private Int32 m_SubProgramId;
        private Int32 UserId = 0;
        private Int32 RoleId = 0;

        #endregion

        #region Properties

        public Int32 ProgramId
        {
            get { return m_SubProgramId; }
            set { m_SubProgramId = value; }
        }

        #region Get Control Property
        /// <summary>
        /// Gets Search textbox control of action bar.
        /// </summary>
        public TextBox SearchTextBox
        {
            get { return txtSearch; }
        }

        /// <summary>
        /// Gets New button control of action bar.
        /// </summary>
        public Button NewButton
        {
            get { return btnNew; }
        }

        /// <summary>
        /// Gets Edit button control of action bar.
        /// </summary>
        public Button EditButton
        {
            get { return btnEdit; }
        }

        /// <summary>
        /// Gets Delete button control of action bar.
        /// </summary>
        public Button DeleteButton
        {
            get { return btnDelete; }
        }

        /// <summary>
        /// Gets Export button control of action bar.
        /// </summary>
        public Button ExportButton
        {
            get { return btnExport; }
        }

        /// <summary>
        /// Gets Search button control of action bar.
        /// </summary>
        public Button SearchButton
        {
            get { return btnSearch; }
        }

        /// <summary>
        /// Gets Save button control of action bar.
        /// </summary>
        public Button SaveButton
        {
            get { return btnSave; }
        }

        /// <summary>
        /// Gets Save and New button control of action bar.
        /// </summary>
        public Button SaveNewButton
        {
            get { return btnSaveNew; }
        }

        /// <summary>
        /// Gets Cancel button control of action bar.
        /// </summary>
        public Button CancelButton
        {
            get { return btnCancel; }
        }

        /// <summary>
        /// Gets Refresh button control of action bar.
        /// </summary>
        public Button RefreshButton
        {
            get { return btnRefresh; }
        }

        #endregion

        #region Enable/Desable Control Property
        /// <summary>
        /// Gets or sets a value indicating whether the New button of action bar is enabled.
        /// </summary>
        public Boolean EnableNewButton
        {
            get { return btnNew.Enabled; }
            set
            {
                if (value)
                {
                    objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                    if (objAuthorizationBDto != null)
                    {
                        if (objAuthorizationBDto.ProgramWriteAccess)
                        {
                            btnNew.Enabled = value;
                        }
                    }
                }
                else
                    btnNew.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Edit button of action bar is enabled.
        /// </summary>
        public Boolean EnableEditButton
        {
            get { return btnEdit.Enabled; }
            set
            {
                if (value)
                {
                    objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                    if (objAuthorizationBDto != null)
                    {
                        if (objAuthorizationBDto.ProgramWriteAccess)
                        {
                            btnEdit.Enabled = value;
                        }
                    }
                }
                else
                    btnEdit.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Delete button of action bar is enabled.
        /// </summary>
        public Boolean EnableDeleteButton
        {
            get { return btnDelete.Enabled; }
            set
            {
                if (value)
                {
                    objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                    if (objAuthorizationBDto != null)
                    {
                        if (objAuthorizationBDto.ProgramDeleteAccess)
                        {
                            btnDelete.Enabled = value;
                        }
                    }
                }
                else
                    btnDelete.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Export button of action bar is enabled.
        /// </summary>
        public Boolean EnableExportButton
        {
            get { return btnExport.Enabled; }
            set
            {
                if (value)
                {
                    objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                    if (objAuthorizationBDto != null)
                    {
                        if (objAuthorizationBDto.ProgramPrintAccess)
                        {
                            btnExport.Enabled = value;
                        }
                    }
                }
                else
                    btnExport.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Refressh button of action bar is enabled.
        /// </summary>
        public Boolean EnableRefreshButton
        {
            get { return btnRefresh.Enabled; }
            set { btnRefresh.Enabled = value; }
        }


        /// <summary>
        /// Gets or sets a value indicating whether the Search button of action bar is enabled.
        /// </summary>
        public Boolean EnableSearchButton
        {
            get { return btnSearch.Enabled; }
            set { btnSearch.Enabled = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Save button of action bar is enabled.
        /// </summary>
        public Boolean EnableSaveButton
        {
            get { return btnSave.Enabled; }
            set
            {
                if (value)
                {
                    objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                    if (objAuthorizationBDto != null)
                    {
                        if (objAuthorizationBDto.ProgramWriteAccess)
                        {
                            btnSave.Enabled = value;
                        }
                    }
                }
                else
                    btnSave.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Save and  New button of action bar is enabled.
        /// </summary>
        public Boolean EnableSaveNewButton
        {
            get { return btnSaveNew.Enabled; }
            set
            {
                if (value)
                {
                    objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                    if (objAuthorizationBDto != null)
                    {
                        if (objAuthorizationBDto.ProgramWriteAccess)
                        {
                            btnSaveNew.Enabled = value;
                        }
                    }
                }
                else
                    btnSaveNew.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Cancel button of action bar is enabled.
        /// </summary>
        public Boolean EnableCancelButton
        {
            get { return btnCancel.Enabled; }
            set { btnCancel.Enabled = value; }
        }
        #endregion

        #region Visible/Invisible Control Property

        /// <summary>
        /// Gets or sets a value indicating whether the seatch-textbox of action bar is visibled.
        /// </summary>
        public Boolean VisibleSearchTextBox
        {
            get { return txtSearch.Visible; }
            set { txtSearch.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the New button of action bar is visible.
        /// </summary>
        public Boolean VisibleNewButton
        {
            get { return btnNew.Visible; }
            set { btnNew.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Edit button of action bar is visible.
        /// </summary>
        public Boolean VisibleEditButton
        {
            get { return btnEdit.Visible; }
            set { btnEdit.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Delete button of action bar is visible.
        /// </summary>
        public Boolean VisibleDeleteButton
        {
            get { return btnDelete.Visible; }
            set { btnDelete.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Export button of action bar is visible.
        /// </summary>
        public Boolean VisibleExportButton
        {
            get { return btnExport.Visible; }
            set { btnExport.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Refressh button of action bar is visible.
        /// </summary>
        public Boolean VisibleRefreshButton
        {
            get { return btnRefresh.Visible; }
            set { btnRefresh.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Search button of action bar is visible.
        /// </summary>
        public Boolean VisibleSearchButton
        {
            get { return btnSearch.Visible; }
            set { btnSearch.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Save button of action bar is visible.
        /// </summary>
        public Boolean VisibleSaveButton
        {
            get { return btnSave.Visible; }
            set { btnSave.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Save and  New button of action bar is enabled.
        /// </summary>
        public Boolean VisibleSaveNewButton
        {
            get { return btnSaveNew.Visible; }
            set { btnSaveNew.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Cancel button of action bar is visible.
        /// </summary>
        public Boolean VisibleCancelButton
        {
            get { return btnCancel.Visible; }
            set { btnCancel.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Default mode of action bar is visible.
        /// </summary>
        public Boolean DefaultMode
        {
            get { return tblDefaultMode.Visible; }
            set
            {
                tblDefaultMode.Visible = value;
                if (value)
                    tblEditMode.Visible = false;
                else
                    tblEditMode.Visible = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Editable mode of action bar is visible.
        /// </summary>
        public Boolean EditableMode
        {
            get { return tblEditMode.Visible; }
            set
            {
                tblEditMode.Visible = value;
                if (value)
                    tblDefaultMode.Visible = false;
                else
                    tblDefaultMode.Visible = true;
            }
        }
        #endregion

        #region Sets Client Side Events

        /// <summary>
        /// Sets the client-side scripts that executes when New button's click event is raised.
        /// </summary>
        public string OnbtnNewClientClick
        {
            set
            { btnNew.OnClientClick = value; }
        }

        /// <summary>
        /// Sets the client-side scripts that executes when Edit button's click event is raised.
        /// </summary>
        public string OnbtnEditClientClick
        {
            set
            { btnEdit.OnClientClick = value; }
        }

        /// <summary>
        /// Sets the client-side scripts that executes when Delete button's click event is raised.
        /// </summary>
        public string OnbtnDeleteClientClick
        {
            set
            { btnDelete.OnClientClick = value; }
        }

        /// <summary>
        /// Sets the client-side scripts that executes when Export button's click event is raised.
        /// </summary>
        public string OnbtnExportClientClick
        {
            set
            { btnExport.OnClientClick = value; }
        }

        /// <summary>
        /// Sets the client-side scripts that executes when Search button's click event is raised.
        /// </summary>
        public string OnbtnSearchClientClick
        {
            set
            { btnSearch.OnClientClick = value; }
        }

        /// <summary>
        /// Sets the client-side scripts that executes when Save button's click event is raised.
        /// </summary>
        public string OnbtnSaveClientClick
        {
            set
            { btnSave.OnClientClick = value; }
        }

        /// <summary>
        /// Sets the client-side scripts that executes when  Save & New button's click event is raised.
        /// </summary>
        public string OnbtnSaveNewClientClick
        {
            set
            { btnSaveNew.OnClientClick = value; }
        }

        /// <summary>
        /// Sets the client-side scripts that executes when Cancel button's click event is raised.
        /// </summary>
        public string OnbtnCancelClientClick
        {
            set
            { btnCancel.OnClientClick = value; }
        }

        #endregion

        #region Validatoin Group

        public String NewButtonValidationGroup
        {
            get { return btnNew.ValidationGroup; }
            set { btnNew.ValidationGroup = value; }
        }

        public String EditButtonValidationGroup
        {
            get { return btnEdit.ValidationGroup; }
            set { btnEdit.ValidationGroup = value; }
        }

        public String DeleteButtonValidationGroup
        {
            get { return btnDelete.ValidationGroup; }
            set { btnDelete.ValidationGroup = value; }
        }

        public String ExportButtonValidationGroup
        {
            get { return btnExport.ValidationGroup; }
            set { btnExport.ValidationGroup = value; }
        }

        public String SearchButtonValidationGroup
        {
            get { return btnSearch.ValidationGroup; }
            set { btnSearch.ValidationGroup = value; }
        }

        public String SaveButtonValidationGroup
        {
            get { return btnSave.ValidationGroup; }
            set { btnSave.ValidationGroup = value; }
        }

        public String SaveNewButtonValidationGroup
        {
            get { return btnSaveNew.ValidationGroup; }
            set { btnSaveNew.ValidationGroup = value; }
        }

        public String CancelButtonValidationGroup
        {
            get { return btnCancel.ValidationGroup; }
            set { btnCancel.ValidationGroup = value; }
        }

        #endregion

        public Boolean ActionBarVisible
        {
            set
            {
                tblDefaultMode.Visible = value;
                tblEditMode.Visible = value;
            }
        }

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnNew.Click += this.btnNewClick;
            this.btnEdit.Click += this.btnEditClick;
            this.btnDelete.Click += this.btnDeleteClick;
            //this.btnExport.Click += new EventHandler(this.btnExportClick);
            this.btnSearch.Click += this.btnSearchClick;
            this.btnSave.Click += this.btnSaveClick;
            this.btnSaveNew.Click += this.btnSaveNewClick;
            this.btnCancel.Click += this.btnCancelClick;
            this.btnRefresh.Click += this.btnRefreshClick;

            this.imgExcelExport.Click += imgExcelExport_Click;
            this.imgPdfExport.Click += imgPdfExport_Click;
            this.imgCsvExport.Click += imgCsvExport_Click;
            this.imgWordExport.Click += imgWordExport_Click;

            //GlobalTooltip
            rttTips.Text = TooltipText();

            imgToolTip.Attributes.Add("onmouseout", "SearchTips_Hide(this)");
            ShowPageAccess();
        }

        #endregion

        #region Control Methods

        #region ShowPageAccess
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <param name="progId"></param>
        public void ShowPageAccess()
        {
            try
            {
                //bool readAccess = false;
                //bool writeAccess = false;
                //bool deleteAccess = false;
                //bool printAccess = false;
                //DataSet dsAccess = null;

                //AuthorizationDal objAuthorizationDal = new AuthorizationDal();

                //dsAccess = objAuthorizationDal.GetPageOperationAccess(userId, roleId, progId);
                //if (dsAccess != null && dsAccess.Tables.Count > 0)
                //{
                //    if (dsAccess.Tables[0] != null && dsAccess.Tables[0].Rows.Count > 0)
                //    {
                //        readAccess = Convert.ToBoolean(dsAccess.Tables[0].Rows[0]["READ_ACCESS"].ToString());
                //        writeAccess = Convert.ToBoolean(dsAccess.Tables[0].Rows[0]["WRITE_ACCESS"].ToString());
                //        deleteAccess = Convert.ToBoolean(dsAccess.Tables[0].Rows[0]["DELETE_ACCESS"].ToString());
                //        printAccess = Convert.ToBoolean(dsAccess.Tables[0].Rows[0]["PRINT_ACCESS"].ToString());
                //    }
                //}
                //btnSearch.Enabled = readAccess;
                //btnNew.Enabled = writeAccess;
                //btnEdit.Enabled = writeAccess;
                //btnDelete.Enabled = deleteAccess;
                //btnExport.Enabled = printAccess;

                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                if (objAuthorizationBDto != null)
                {
                    if (!objAuthorizationBDto.ProgramWriteAccess)
                    {
                        btnNew.Enabled = false;
                        btnSave.Enabled = false;
                        btnSaveNew.Enabled = false;
                    }
                    if (!objAuthorizationBDto.ProgramDeleteAccess)
                        btnDelete.Enabled = false;
                    if (!objAuthorizationBDto.ProgramPrintAccess)
                        btnExport.Enabled = false;
                }


            }
            catch (Exception) { }
        }
        #endregion

        #region Add Attributes to Action Bar Button

        public void AddAttributeToNewButton(string key, string value)
        {
            btnNew.Attributes.Add(key, value);
        }

        public void AddAttributeToEditButton(string key, string value)
        {
            btnEdit.Attributes.Add(key, value);
        }

        public void AddAttributeToDeleteButton(string key, string value)
        {
            btnDelete.Attributes.Add(key, value);
        }

        public void AddAttributeToExportButton(string key, string value)
        {
            btnExport.Attributes.Add(key, value);
        }

        public void AddAttributeToSearchButton(string key, string value)
        {
            btnSearch.Attributes.Add(key, value);
        }

        public void AddAttributeToSaveButton(string key, string value)
        {
            btnSave.Attributes.Add(key, value);
        }

        public void AddAttributeToSaveNewButton(string key, string value)
        {
            btnSaveNew.Attributes.Add(key, value);
        }

        public void AddAttributeToCancelButton(string key, string value)
        {
            btnCancel.Attributes.Add(key, value);
        }

        #endregion

        #region Clear Attributes from Action Bar Button

        public void ClearAttributeOfNewButton()
        {
            btnNew.Attributes.Clear();
        }

        public void ClearAttributeOfEditButton()
        {
            btnEdit.Attributes.Clear();
        }

        public void ClearAttributeOfDeleteButton()
        {
            btnDelete.Attributes.Clear();
        }

        public void ClearAttributeOfExportButton()
        {
            btnExport.Attributes.Clear();
        }

        public void ClearAttributeOfSearchButton()
        {
            btnSearch.Attributes.Clear();
        }

        public void ClearAttributeOfSaveButton()
        {
            btnSave.Attributes.Clear();
        }

        public void ClearAttributeOfSaveNewButton()
        {
            btnSaveNew.Attributes.Clear();
        }

        public void ClearAttributeOfCancelButton()
        {
            btnCancel.Attributes.Clear();
        }

        #endregion

        #region IsNumber
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private Boolean IsNumber(Object obj)
        {
            try
            {
                Convert.ToInt32(obj);
                return true;
            }
            catch (FormatException)
            { return false; }
            catch (Exception)
            { return false; }
        }
        #endregion

        #region TooltipText
        public string TooltipText()
        {
            //String text = "<h3>Search Tips</h3>";
            //text += "By default the search engine tries to locate records which have exact matches <b><i>all</i></b> of keywords entered in your search query.";
            //text += "<br/><br/>In addition, there are several ways to modify the default search behavior.";
            //text += "<br/><br/><b>1. % wildcard</b><br/>";
            //text += "&nbsp;&nbsp;&nbsp;<b>a)</b>&nbsp;If your search query ends with {keyword}% then it tries to locate records which starts with {keyword}.<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;For ex. Ah%</b>";
            //text += "<br/>&nbsp;&nbsp;&nbsp;<b>b)</b>&nbsp;If your search query starts with %{keyword} then it tries to locate records which ends with {keyword}.<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;For ex. %Ah</b>";
            //text += "<br/>&nbsp;&nbsp;&nbsp;<b>c)</b>&nbsp;If your search query starts and ends with %{keyword}% then it tries to locate records which have {keyword} in records.<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;For ex. %Ah%</b>";

            String text = "<b>Search Tips</b>";
            text += "<br/>To search for records similar to your keyword, use <b>%</b> wildcard";
            text += "<br/>ex. 'Ah%' will search all records starting with 'Ah'<br/>";
            text += "<br/>ex. '%Ah' will search all records ending with 'Ah'<br/>";
            text += "<br/>ex. '%Ah%' will search all records which has 'Ah' in between<br/>";



            return text;
        }
        #endregion

        #endregion
    }
}