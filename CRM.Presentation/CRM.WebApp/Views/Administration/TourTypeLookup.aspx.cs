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
    public partial class TourTypeLookup : System.Web.UI.Page
    {

        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        TourTypeLookupDal objTourTypeLookup = null;
        public const String vsTourType = "TourType";
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
                Session["currentevent"] = "Tour Type";
            }
            hdnEditableMode.Value = "false";
            acbTourType.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdTour.ClientID));
            acbTourType.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdTour.ClientID));




        }

        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Tour Type");
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
        /// Bind Tour Type grid
        /// </summary>

        private void BindGrid()
        {

            objTourTypeLookup = new TourTypeLookupDal();
            DataSet dsTourType = objTourTypeLookup.GetTourType("");
            radgrdTour.DataSource = dsTourType;
            radgrdTour.DataBind();
            ViewState[vsTourType] = dsTourType;

        }

        #endregion

        #region Save
        private void Save()
        {
            try
            {
                int result = 0;
                objTourTypeLookup = new TourTypeLookupDal();
                objLookupBDto = new LookupBDto();
                objLookupBDto.LookupName = txtTourName.Text;
                objLookupBDto.Description = txtTourDesc.Text;
                objLookupBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objTourTypeLookup.InsertTourType(objLookupBDto);
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
            txtTourName.Text = String.Empty;
            txtTourDesc.Text = String.Empty;

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
            string xmlHeaderNodeStructure = "<Tour TOUR_TYPE_ID=\"{0}\" TOUR_TYPE_NAME=\"{1}\" TOUR_TYPE_DESC=\"{2}\" USER_ID=\"{3}\"></Tour>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int TourId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblgrdTourId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblgrdTourIdEdit");
                        TextBox txtgrdTourName = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdTourName");
                        TextBox txtgrdTourDesc = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdTourDesc");
                        TourId = int.Parse(lblgrdTourId.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, TourId, txtgrdTourName.Text, txtgrdTourDesc.Text, objAuthorizationBDto.UserProfile.UserId);
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

        protected void acbTourType_NewClick(object sender, EventArgs e)
        {
            acbTourType.EditableMode = true;
            acbTourType.SaveNewButton.Visible = true;
            acbTourType.SaveButton.CommandName = "Save";
            pnlAddNewMode.Visible = true;
            txtTourName.Focus();
            radgrdTour.Rebind();
            Reset();
        }

        protected void acbTourType_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objTourTypeLookup = new TourTypeLookupDal();
                switch (acbTourType.SaveButton.CommandName)
                {
                    case "Save":
                        Save();
                        pnlAddNewMode.Visible = false;
                        acbTourType.EditableMode = false;

                        break;

                    case "Update":
                        String xmlData = GenerateXmlString(radgrdTour);
                        result = objTourTypeLookup.UpdateTourType(xmlData);
                        if (result == 1)
                        {
                            acbTourType.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdTour.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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
                objTourTypeLookup = new TourTypeLookupDal();

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbTourType_SaveNewClick(object sender, EventArgs e)
        {
            txtTourName.Focus();

            Save();
            Reset();
        }

        protected void acbTourType_CancelClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdTour.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdTour.Rebind();

            }
            acbTourType.DefaultMode = true;
            pnlAddNewMode.Visible = false;
        }

        protected void acbTourType_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder TourId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {
                        Label lblTourId = (Label)radgrdTour.Items[i].FindControl("lblgrdTourIdItem");

                        if (lblTourId != null)
                        {
                            TourId.Append(lblTourId.Text + ",");
                        }
                    }
                }

                objTourTypeLookup = new TourTypeLookupDal();
                String TourTypeId = TourId.ToString().TrimEnd(',');
                result = objTourTypeLookup.DeleteTourType(TourTypeId);


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

        protected void acbTourType_SearchClick(object sender, EventArgs e)
        {
            try
            {

                objTourTypeLookup = new TourTypeLookupDal();
                DataSet dsTour = objTourTypeLookup.GetTourType(acbTourType.SearchTextBox.Text);
                radgrdTour.DataSource = dsTour;
                radgrdTour.DataBind();
                ViewState[vsTourType] = dsTour;
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }

        }

        protected void acbTourType_EditClick(object sender, EventArgs e)
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
                        radgrdTour.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    radgrdTour.Rebind();
                    acbTourType.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbTourType.SaveButton.CommandName = "Update";
                    acbTourType.SaveNewButton.Visible = false;
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbTourType_RefreshClick(object sender, EventArgs e)
        {
            acbTourType.DefaultMode = true;
            pnlAddNewMode.Visible = false;
            acbTourType.SearchTextBox.Text = String.Empty;
            BindGrid();


        }



        #endregion

        #region Grid Event
        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void acbTourType_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdTour.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdTour.Items)
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
        protected void acbTourType_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdTour.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdTour.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void acbTourType_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsTourType] != null)
                radgrdTour.DataSource = ViewState[vsTourType];
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
                            radgrdTour.Items[htItemIndex[i].ToString()].Edit = false;
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
            radgrdTour.ExportSettings.ExportOnlyData = true;
            radgrdTour.ExportSettings.OpenInNewWindow = true;
            radgrdTour.ExportSettings.IgnorePaging = true;

            string filename = "Tour_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdTour.ExportSettings.FileName = filename;



            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdTour.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdTour.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdTour.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdTour.MasterTableView.ExportToCSV();
                        break;
                    }
            }


        }


        protected void acbTourType_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbTourType_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbTourType_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbTourType_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion

    }
}
