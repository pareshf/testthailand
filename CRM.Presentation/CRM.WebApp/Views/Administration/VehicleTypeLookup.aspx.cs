#region Program Information
/**********************************************************************************************************************************************
 Class Name           : VehicleTypeLookUp
 Class Description    : Implementation logic for save, edit, delete and find operation for Status details.
 Author               : Chirag.
 Created Date         : Mar 20, 2010
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
    public partial class VehicleTypeLookup : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        VehicleTypelookupDal objVehicleTypelookup = null;
        public const String vsVehicle = "Vehicle";
        LookupBDto objLookUpBDto = null;
        AuthorizationBDto objAuthorizationBDto = null;

        #endregion

        #region Events

        #region Page Events

        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Vehicle Type");
        }

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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }

            if (Session[PageConstants.ssnUserAuthorization] != null)
            {
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                Session["currentevent"] = "Vehicle Type";
            }

            acbVehicleType.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdVehicleType.ClientID));
            acbVehicleType.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdVehicleType.ClientID));
        }

        #endregion

        #region Actionbar Events


        /// <summary>
        /// Action bar's new button event which open view to insert new Status.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void acbVehicleType_NewClick(object sender, EventArgs e)
        {
            try
            {
                txtVehicleType.Focus();
                acbVehicleType.EditableMode = true;
                acbVehicleType.SaveNewButton.Visible = true;
                acbVehicleType.SaveButton.CommandName = "Save";
                pnlAddNewMode.Visible = true;

                Reset();

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        /// <summary>
        /// Action bar's edit button event which open multiple rows in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void acbVehicleType_EditClick(object sender, EventArgs e)
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
                        radgrdVehicleType.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    radgrdVehicleType.Rebind();
                    acbVehicleType.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbVehicleType.SaveNewButton.Visible = false;
                    acbVehicleType.SaveButton.CommandName = "Update";
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        /// <summary>
        /// Action bar's delete button event which delete records physically from database.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void acbVehicleType_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder vehicleTypeId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {
                        Label lblVehicleId = (Label)radgrdVehicleType.Items[i].FindControl("lblGrdVehicleTypeIdItem");
                        if (lblVehicleId != null)
                        {
                            vehicleTypeId.Append(lblVehicleId.Text + ",");
                        }
                    }
                }
                objVehicleTypelookup = new VehicleTypelookupDal();
                String vehicleId = vehicleTypeId.ToString().TrimEnd(',');
                result = objVehicleTypelookup.DeleteVehicleType(vehicleId);

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

        /// <summary>
        /// Action bar's save button event which saves grid data by xml.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void acbVehicleType_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objVehicleTypelookup = new VehicleTypelookupDal();
                switch (acbVehicleType.SaveButton.CommandName)
                {
                    case "Save":
                        SaveVehicle();
                        pnlAddNewMode.Visible = false;
                        acbVehicleType.EditableMode = false;
                        BindGrid();

                        break;
                    case "Update":
                        String xmlData = GenerateXmlString(radgrdVehicleType);
                        result = objVehicleTypelookup.UpdateVehicleType(xmlData);
                        if (result == 1)
                        {
                            acbVehicleType.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Update].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdVehicleType.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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

                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        /// <summary>
        /// Action bar's cancel button event which opens grid in default mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        /// 
        protected void acbVehicleType_CancelClick(object sender, EventArgs e)
        {
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                    for (int i = 0; i < htItemIndex.Count; i++)
                        radgrdVehicleType.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                    bisEdit = false;
                    ViewState[PageConstants.vsItemIndexes] = null;
                    radgrdVehicleType.Rebind();
                }
                acbVehicleType.DefaultMode = true;
                pnlAddNewMode.Visible = false;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbVehicleType_SearchClick(object sender, EventArgs e)
        {
            try
            {
                objVehicleTypelookup = new VehicleTypelookupDal();
                DataSet dsVehicle = objVehicleTypelookup.GetVehicleType(acbVehicleType.SearchTextBox.Text);
                radgrdVehicleType.DataSource = dsVehicle;
                radgrdVehicleType.DataBind();
                ViewState[vsVehicle] = dsVehicle;
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }

        }

        protected void acbVehicleType_SaveNewClick(object sender, EventArgs e)
        {
            try
            {
                txtVehicleType.Focus();
                SaveVehicle();
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

        #region Grid events

        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdVehicleType_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdVehicleType.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;
                foreach (GridDataItem item in radgrdVehicleType.Items)
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
        protected void radgrdVehicleType_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdVehicleType.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdVehicleType.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdVehicleType_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsVehicle] != null)
                radgrdVehicleType.DataSource = ViewState[vsVehicle];
        }
        #endregion

        #region Grid Checkbox Events

        /// <summary>
        /// Saves checkbox checked status into hashtable after check status changed.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        public void chkItemWrt_CheckChanged(object sender, EventArgs e)
        {
            try
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
                                radgrdVehicleType.Items[htItemIndex[i].ToString()].Edit = false;
                                htItemIndex.Remove(i);

                                break;
                            }
                        }
                    }
                }
                ViewState.Add(PageConstants.vsItemIndexes, htItemIndex);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Bind Grid
        /// <summary>
        /// Bind VehicleTypeLookUp grid
        /// </summary>
        private void BindGrid()
        {
            objVehicleTypelookup = new VehicleTypelookupDal();
            DataSet dsVehicle = objVehicleTypelookup.GetVehicleType("");
            radgrdVehicleType.DataSource = dsVehicle;
            radgrdVehicleType.DataBind();
            ViewState[vsVehicle] = dsVehicle;
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
            string xmlHeaderNodeStructure = "<Vehicle VEHICLE_TYPE_ID=\"{0}\" VEHICLE_TYPE_DESC=\"{1}\" USER_ID=\"{2}\"></Vehicle>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int VehicleId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblGrdVehicleIdItem = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblGrdVehicleTypeIdEdit");
                        TextBox txtVehicle = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtGrdVehicleType");
                        VehicleId = int.Parse(lblGrdVehicleIdItem.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, VehicleId, txtVehicle.Text, objAuthorizationBDto.UserProfile.UserId);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }
        #endregion

        #region Save
        private void SaveVehicle()
        {
            try
            {
                int result = 0;
                objVehicleTypelookup = new VehicleTypelookupDal();
                LookupBDto objStatus = new LookupBDto();
                objLookUpBDto = new LookupBDto();
                objLookUpBDto.LookupName = txtVehicleType.Text;
                objLookUpBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objVehicleTypelookup.InsertVehicleType(objLookUpBDto);
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
            txtVehicleType.Text = string.Empty;

        }

        #endregion
        

        #endregion

        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            radgrdVehicleType.ExportSettings.ExportOnlyData = true;
            radgrdVehicleType.ExportSettings.OpenInNewWindow = true;
            radgrdVehicleType.ExportSettings.IgnorePaging = true;

            string filename = "VehicleType_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdVehicleType.ExportSettings.FileName = filename;

            //radgrdDepartment.MasterTableView.GetColumn("chkDepartment").Visible = false;

            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdVehicleType.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdVehicleType.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdVehicleType.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdVehicleType.MasterTableView.ExportToCSV();
                        break;
                    }
            }

            //ClientScript.RegisterStartupScript(GetType(), "", "window.location.reload();", true);
        }

        protected void acbVehicleType_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbVehicleType_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbVehicleType_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbVehicleType_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion
    }
}
