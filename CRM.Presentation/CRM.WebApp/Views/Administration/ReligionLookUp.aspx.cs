#region Program Information
/**********************************************************************************************************************************************
 Class Name           : ReligionLookUp
 Class Description    : Implementation logic for save, edit, delete and find operation for customer details.
 Author               : Chirag.
 Created Date         : Mar 06, 2010
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
    public partial class ReligionLookUp : System.Web.UI.Page
    {

        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        ReligionLookUpDal objReligionLookUp = null;
        public const String vsReligion = "Religion";
        LookupBDto objLookUpBDto = null;
        AuthorizationBDto objAuthorizationBDto = null;
        

        #endregion

        #region Events

        #region Page Events

        protected void Page_PreInit(object sender, EventArgs e)
        {
            
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Religion");
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
                Session["currentevent"] = "Religion";
            }

            acbReligion.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdReligion.ClientID));
            acbReligion.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdReligion.ClientID));
        }

        #endregion

        #region Actionbar Events


        /// <summary>
        /// Action bar's new button event which open view to insert new customer.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void acbReligion_NewClick(object sender, EventArgs e)
        {
            try
            {
                txtReligionName.Focus();
                acbReligion.EditableMode = true;
                acbReligion.SaveNewButton.Visible = true;
                acbReligion.SaveButton.CommandName = "Save";
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
        protected void acbReligion_EditClick(object sender, EventArgs e)
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
                        radgrdReligion.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    //Session[PageConstants.ssnHasTableIndex] = htItemIndex;
                    radgrdReligion.Rebind();
                    acbReligion.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbReligion.SaveNewButton.Visible = false;
                    acbReligion.SaveButton.CommandName = "Update";
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
        protected void acbReligion_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder religionId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {
                        Label lblReligionId = (Label)radgrdReligion.Items[i].FindControl("lblGrdReligionIdItem");
                        if (lblReligionId != null)
                        {
                            religionId.Append(lblReligionId.Text + ",");
                        }
                    }
                }
                objReligionLookUp = new ReligionLookUpDal();
                String religId = religionId.ToString().TrimEnd(',');
                result = objReligionLookUp.DeleteReligion(religId);

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
        protected void acbReligion_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objReligionLookUp = new ReligionLookUpDal();
                switch (acbReligion.SaveButton.CommandName)
                {
                    case "Save":
                        SaveReligion();
                        pnlAddNewMode.Visible = false;
                        acbReligion.EditableMode = false;
                        BindGrid();
                        break;

                    case "Update":
                        String xmlData = GenerateXmlString(radgrdReligion);
                        result = objReligionLookUp.UpdateReligion(xmlData);
                        if (result == 1)
                        {
                            acbReligion.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Update].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                            {
                                radgrdReligion.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
                            }
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
            }catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
            

            Reset();
        }

        /// <summary>
        /// Action bar's cancel button event which opens grid in default mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        /// 
        protected void acbReligion_CancelClick(object sender, EventArgs e)
        {
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                    for (int i = 0; i < htItemIndex.Count; i++)
                        radgrdReligion.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                    bisEdit = false;
                    ViewState[PageConstants.vsItemIndexes] = null;
                    radgrdReligion.Rebind();
                }
                acbReligion.DefaultMode = true;
                pnlAddNewMode.Visible = false;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbReligion_SearchClick(object sender, EventArgs e)
        {
            try
            {
               
                objReligionLookUp = new ReligionLookUpDal();
                DataSet dsReligion = objReligionLookUp.GetReligionName(acbReligion.SearchTextBox.Text);
                radgrdReligion.DataSource = dsReligion;
                radgrdReligion.DataBind();
                ViewState[vsReligion] = dsReligion;
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }

        }

        protected void acbReligion_SaveNewClick(object sender, EventArgs e)
        {
            try
            {
                txtReligionName.Focus();
                SaveReligion();
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
        protected void radgrdReligion_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdReligion.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;
                foreach (GridDataItem item in radgrdReligion.Items)
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
        protected void radgrdReligion_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdReligion.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdReligion.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdReligion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsReligion] != null)
                radgrdReligion.DataSource = ViewState[vsReligion];
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
                                radgrdReligion.Items[htItemIndex[i].ToString()].Edit = false;
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
        /// Bind ReligionLookUp grid
        /// </summary>
        private void BindGrid()
        {
            objReligionLookUp = new ReligionLookUpDal();
            DataSet dsReligion = objReligionLookUp.GetReligionName("");
            radgrdReligion.DataSource = dsReligion;
            radgrdReligion.DataBind();
            ViewState[vsReligion] = dsReligion;
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
            string xmlHeaderNodeStructure = "<Religions RELIGION_ID=\"{0}\" RELIGION_NAME=\"{1}\" USER_ID=\"{2}\"></Religions>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int ReligionId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblGrdReligionIdItem = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblGrdReligionIdEdit");
                        TextBox txtreligion = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtGrdReligionName");
                        ReligionId = int.Parse(lblGrdReligionIdItem.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, ReligionId, txtreligion.Text, objAuthorizationBDto.UserProfile.UserId);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }
        #endregion

        #region Save
        private void SaveReligion()
        {
            try
            {
                int result = 0;
                objReligionLookUp = new ReligionLookUpDal();
                LookupBDto objReligion = new LookupBDto();
                objLookUpBDto = new LookupBDto();
                objLookUpBDto.LookupName = txtReligionName.Text;
                objLookUpBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objReligionLookUp.InsertRelagion(objLookUpBDto);
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
            txtReligionName.Text = string.Empty;

        }
        #endregion
        

        #endregion

        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            radgrdReligion.ExportSettings.ExportOnlyData = true;
            radgrdReligion.ExportSettings.OpenInNewWindow = true;
            radgrdReligion.ExportSettings.IgnorePaging = true;

            string filename = "Religion_" + System.DateTime.Now.ToString("ddmmyyyy") + "_" + System.DateTime.Now.Hour.ToString();
            radgrdReligion.ExportSettings.FileName = filename;

            //radgrdDepartment.MasterTableView.GetColumn("chkDepartment").Visible = false;

            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdReligion.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdReligion.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdReligion.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdReligion.MasterTableView.ExportToCSV();
                        break;
                    }
            }

            //ClientScript.RegisterStartupScript(GetType(), "", "window.location.reload();", true);
        }

        protected void acbReligion_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbReligion_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbReligion_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbReligion_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion

    }
}
