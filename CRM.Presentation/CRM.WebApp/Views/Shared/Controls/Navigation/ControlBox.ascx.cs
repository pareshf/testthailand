using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CRM.Model.Security;
using CRM.Core.Constants;

namespace CRM.WebApp.Views.Shared.Controls.Navigation
{
    public partial class ControlBox : System.Web.UI.UserControl
    {
        #region Members
        AuthorizationBDto objAuthorizationBDto;
        public event System.EventHandler btnSaveClick;
        public event System.EventHandler btnSaveNewClick;
        public event System.EventHandler btnClearClick;
        public event System.EventHandler btnCopyClick;
        public event System.EventHandler btnCancelClick;
        #endregion

        #region Properties
        #region Control Property
        /// <summary>
        /// Gets Save button control of Control box.
        /// </summary>
        public Button SaveButton
        {
            get { return btnSave; }
        }

        /// <summary>
        /// Gets Save and New button control of Control box.
        /// </summary>
        public Button SaveNewButton
        {
            get { return btnSaveNew; }
        }

        /// <summary>
        /// Gets Clear button control of Control box.
        /// </summary>
        public Button ClearButton
        {
            get { return btnClear; }
        }

        /// <summary>
        /// Gets Copy button control of Control box.
        /// </summary>
        public Button CopyButton
        {
            get { return btnCopy; }
        }

        /// <summary>
        /// Gets Cancel button control of Control box.
        /// </summary>
        public Button CancelButton
        {
            get { return btnCancel; }
        }
        #endregion

        #region Enable/Desable Control Property

        /// <summary>
        /// Gets or sets a value indicating whether the Save button of control box is enabled.
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
        /// Gets or sets a value indicating whether the Save and  New button of control box is enabled.
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
        /// Gets or sets a value indicating whether the Clear button of control box is enabled.
        /// </summary>
        public Boolean EnableClearButton
        {
            get { return btnClear.Enabled; }
            set { btnClear.Enabled = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Copy button of control box is enabled.
        /// </summary>
        public Boolean EnableCopyButton
        {
            get { return btnCopy.Enabled; }
            set
            {
                if (value)
                {
                    objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                    if (objAuthorizationBDto != null)
                    {
                        if (objAuthorizationBDto.ProgramWriteAccess)
                        {
                            btnCopy.Enabled = value;
                        }
                    }
                }
                else
                    btnCopy.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Cancel button of control box is enabled.
        /// </summary>
        public Boolean EnableCancelButton
        {
            get { return btnCancel.Enabled; }
            set { btnCancel.Enabled = value; }
        }
        #endregion

        #region Visible/Invisible Control Property

        /// <summary>
        /// Gets or sets a value indicating whether the Save button of control box is enabled.
        /// </summary>
        public Boolean VisibleSaveButton
        {
            get { return btnSave.Visible; }
            set { btnSave.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Save and  New button of control box is enabled.
        /// </summary>
        public Boolean VisibleSaveNewButton
        {
            get { return btnSaveNew.Visible; }
            set { btnSaveNew.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Clear button of control box is enabled.
        /// </summary>
        public Boolean VisibleClearButton
        {
            get { return btnClear.Visible; }
            set { btnClear.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Copy button of control box is enabled.
        /// </summary>
        public Boolean VisibleCopyButton
        {
            get { return btnCopy.Visible; }
            set { btnCopy.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Cancel button of control box is enabled.
        /// </summary>
        public Boolean VisibleCancelButton
        {
            get { return btnCancel.Visible; }
            set { btnCancel.Visible = value; }
        }
        #endregion

        #region Sets Client Side Events

        /// <summary>
        /// Sets the client-side scripts that executes when Save button's click event is raised.
        /// </summary>
        public string OnbtnSaveClientClick
        {
            set
            { btnSave.OnClientClick = value; }
        }

        /// <summary>
        /// Sets the client-side scripts that executes when New button's click event is raised.
        /// </summary>
        public string OnbtnSaveNewClientClick
        {
            set
            { btnSaveNew.OnClientClick = value; }
        }

        /// <summary>
        /// Sets the client-side scripts that executes when Edit button's click event is raised.
        /// </summary>
        public string OnbtnClearClientClick
        {
            set
            { btnClear.OnClientClick = value; }
        }

        /// <summary>
        /// Sets the client-side scripts that executes when Delete button's click event is raised.
        /// </summary>
        public string OnbtnCopyClientClick
        {
            set
            { btnCopy.OnClientClick = value; }
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

        public String ClearButtonValidationGroup
        {
            get { return btnClear.ValidationGroup; }
            set { btnClear.ValidationGroup = value; }
        }

        public String CopyButtonValidationGroup
        {
            get { return btnCopy.ValidationGroup; }
            set { btnCopy.ValidationGroup = value; }
        }

        public String CancelButtonValidationGroup
        {
            get { return btnCancel.ValidationGroup; }
            set { btnCancel.ValidationGroup = value; }
        }
        #endregion
        #endregion

        #region Events
        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.Click += btnSaveClick;
            this.btnSaveNew.Click += btnSaveNewClick;
            this.btnClear.Click += btnClearClick;
            this.btnCopy.Click += btnCopyClick;
            this.btnCancel.Click += btnCancelClick;

            #region BY Program->Roll ControlBox Button Enabled True/False
            ShowPageAccess();
            #endregion
        }
        #endregion
        #endregion

        #region Methods
        #region Add Attributes to Action Bar Button

        public void AddAttributeToSaveButton(string key, string value)
        {
            btnSave.Attributes.Add(key, value);
        }

        public void AddAttributeToSaveNewButton(string key, string value)
        {
            btnSaveNew.Attributes.Add(key, value);
        }

        public void AddAttributeToClearButton(string key, string value)
        {
            btnClear.Attributes.Add(key, value);
        }

        public void AddAttributeToCopyButton(string key, string value)
        {
            btnCopy.Attributes.Add(key, value);
        }

        public void AddAttributeToCancelButton(string key, string value)
        {
            btnCancel.Attributes.Add(key, value);
        }
        # endregion

        #region Clear Attributes from Action Bar Button

        public void ClearAttributeOfSaveButton()
        {
            btnSave.Attributes.Clear();
        }

        public void ClearAttributeOfSaveNewButton()
        {
            btnSaveNew.Attributes.Clear();
        }

        public void ClearAttributeOfClearButton()
        {
            btnClear.Attributes.Clear();
        }

        public void ClearAttributeOfCopyButton()
        {
            btnCopy.Attributes.Clear();
        }

        public void ClearAttributeOfCancelButton()
        {
            btnCancel.Attributes.Clear();
        }

        #endregion

        public void ShowPageAccess()
        {
            objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            if (objAuthorizationBDto != null)
            {
                if (!objAuthorizationBDto.ProgramWriteAccess)
                {
                    btnSave.Enabled = false;
                    btnSaveNew.Enabled = false;
                    btnCopy.Enabled = false;
                }

            }
        }
        #endregion
    }
}