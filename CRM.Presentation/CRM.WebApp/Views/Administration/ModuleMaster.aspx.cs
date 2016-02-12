using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CRM.DataAccess.AdministrationDAL;
using CRM.WebApp.Views.Shared.Controls.Navigation;
using Telerik.Web.UI;
using CRM.Model.AdministrationModel;
using System.Text;
using CRM.Core.Constants;
using CRM.Model.Security;

namespace CRM.WebApp.Views.Administration
{
    public partial class ModuleMaster : System.Web.UI.Page
    {



        //#region Member variables
        //Hashtable htItemIndex;
        //Boolean bisEdit = false;
        //ModuleMasterDal objModuleMasterDal = null;
  

        //public const String vsItemIndexes = "ItemIndexes";
        //public const String vsModuleName = "ModuleName";
        //public const String ssnHasTableIndex = "HasTableIndex";
        //#endregion

        //#region Page Events

        //protected void Page_Load(object sender, EventArgs e)
        //{



        //    if (!IsPostBack)
        //    {
        //        ModuleMasterDal objModuleMasterDal = new ModuleMasterDal();
        //        DataSet dsModuleMaster = objModuleMasterDal.GetModuleList();
        //        radGrdModuleMaster.DataSource = dsModuleMaster;
        //        radGrdModuleMaster.DataBind();
        //        ViewState[vsModuleName] = dsModuleMaster;
        //    }
        //    hdnEditableMode.Value = "false";
        //    udcActionbar.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radGrdModuleMaster.ClientID));
        //    udcActionbar.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radGrdModuleMaster.ClientID));


        //}

        //#endregion

        //#region Actionbar Events

        //protected void udcActionbar_EditClick(object sender, EventArgs e)
        //{
        //    if (ViewState[vsItemIndexes] != null)
        //    {
        //        htItemIndex = (Hashtable)ViewState[vsItemIndexes];
        //    }
        //    if (htItemIndex != null)
        //    {
        //        for (int i = 0; i < htItemIndex.Count; i++)
        //        {
        //            radGrdModuleMaster.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
        //        }
        //        bisEdit = true;
        //        Session[ssnHasTableIndex] = htItemIndex;
        //        radGrdModuleMaster.Rebind();
        //        udcActionbar.EditableMode = true;
        //        hdnEditableMode.Value = "true";
        //    }
        //}
        //protected void udcActionbar_SaveClick(object sender, EventArgs e)
        //{
        //    objModuleMasterDal = new ModuleMasterDal();

        //    String xmlData = GenerateXmlString(radGrdModuleMaster);
        //    int result = objModuleMasterDal.UpdateModuleMaster(xmlData);
        //    if (result == 1)
        //    {
        //        udcActionbar.DefaultMode = true;
        //        Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
        //        Master.MessageCssClass = "successMessage";

        //        if (ViewState[vsItemIndexes] != null)
        //            htItemIndex = (Hashtable)ViewState[vsItemIndexes];
        //        for (int i = 0; i < htItemIndex.Count; i++)
        //            radGrdModuleMaster.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
        //        bisEdit = false;
        //        ViewState[vsItemIndexes] = null;
        //        BindGrid();
        //    }
        //    else
        //    {
        //        Master.DisplayMessage(ConfigurationSettings.AppSettings["UnableUpdateRecord"].ToString());
        //        Master.MessageCssClass = "errorMessage";
        //    }
        //}
        //protected void udcActionbar_CancelClick(object sender, EventArgs e)
        //{
        //    if (ViewState[vsItemIndexes] != null)
        //    {
        //        htItemIndex = (Hashtable)ViewState[vsItemIndexes];
        //    }
        //    for (int i = 0; i < htItemIndex.Count; i++)
        //    {
        //        radGrdModuleMaster.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
        //    }
        //    bisEdit = false;
        //    ViewState[vsItemIndexes] = null;
        //    radGrdModuleMaster.Rebind();
        //    udcActionbar.DefaultMode = true;
        //}
        //protected void udcActionbar_AddClick(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        pnlAddEdit.Visible = true;



        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //protected void udcActionbar_DeleteClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ViewState["ItemIndexes"] != null)
        //        {
        //            htItemIndex = (Hashtable)ViewState["ItemIndexes"];
        //        }
        //        GridItemCollection gvSelectedItms = radGrdModuleMaster.SelectedItems;
        //        StringBuilder ModuleIdCollection = new StringBuilder();
        //        if (htItemIndex != null)
        //        {
        //            if (htItemIndex.Count > 0)
        //            {
        //                for (int i = 0; i < htItemIndex.Count; i++)
        //                {
        //                    Label lblModuleId = (Label)radGrdModuleMaster.Items[Convert.ToInt32(htItemIndex[i].ToString())].Cells[3].FindControl("lblModuleId");
        //                    String tmpRoleId = lblModuleId.Text.ToString();
        //                    if (!String.IsNullOrEmpty(tmpRoleId))
        //                        ModuleIdCollection.Append(tmpRoleId + ",");
        //                }

        //                if (!String.IsNullOrEmpty(ModuleIdCollection.ToString().Trim(',')))
        //                {

        //                    string ModuleIdList = ModuleIdCollection.ToString().Trim(',');
        //                    ModuleMasterDal objModuleMasterDal = new ModuleMasterDal();


        //                    objModuleMasterDal.DeleteModule(ModuleIdList);

        //                    ViewState["ItemIndexes"] = null;
        //                    BindGrid();

        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //protected void udcActionbar_SearchClick(object sender, EventArgs e)
        //{
        //    BindGrid();

        //}






        //#endregion

        //#region Grid events
        //protected void radGrdModuleMaster_PreRender(object source, System.EventArgs e)
        //{
        //    if (bisEdit)
        //    {
        //        GridHeaderItem headerItem = radGrdModuleMaster.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

        //        CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
        //        if (chkHeader != null)
        //            chkHeader.Visible = false;

        //        foreach (GridDataItem item in radGrdModuleMaster.Items)
        //        {
        //            CheckBox chkItem = (CheckBox)item.FindControl("chkItemWrt");
        //            if (chkItem != null)
        //                chkItem.Visible = false;
        //        }
        //    }
        //}
        //protected void radGrdModuleMaster_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Item.ItemType == GridItemType.Header)
        //        {
        //            CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
        //            if (chkBox != null)
        //                chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radGrdModuleMaster.ClientID, 0, chkBox.ClientID));
        //        }

        //        if (e.Item is GridDataItem)
        //        {
        //            CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
        //            if (chkBox != null)
        //                chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radGrdModuleMaster.ClientID, chkBox.ClientID, e.Item.ItemIndex));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //protected void BindGrid()
        //{


        //    objModuleMasterDal = new ModuleMasterDal();
        //    DataSet dsModuleMaster = objModuleMasterDal.GetModuleList();
        //    radGrdModuleMaster.DataSource = dsModuleMaster;
        //    radGrdModuleMaster.DataBind();
        //    ViewState[vsModuleName] = dsModuleMaster;

            


        //}
        //protected void radGrdModuleMaster_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    radGrdModuleMaster.DataSource = ViewState[vsModuleName];
        //}
        //#endregion

        //#region Grid Checkbox Events
        //public void chkItemWrt_CheckChanged(object sender, EventArgs e)
        //{
        //    CheckBox chkBox = (CheckBox)sender;
        //    GridDataItem item = (GridDataItem)chkBox.NamingContainer;
        //    if ((ViewState[vsItemIndexes] != null))
        //    {
        //        htItemIndex = (Hashtable)ViewState[vsItemIndexes];
        //    }
        //    else
        //    {
        //        htItemIndex = new Hashtable();
        //    }
        //    if (chkBox.Checked == true)
        //    {

        //        hdnCheckindex.Value = item.ItemIndex.ToString();
        //        htItemIndex.Add(htItemIndex.Count, item.ItemIndex);
        //        item.Selected = true;
        //    }
        //    else
        //    {
        //        item.Selected = false;
        //        for (int i = 0; i <= htItemIndex.Count - 1; i++)
        //        {
        //            if (htItemIndex[i] != null)
        //            {
        //                if (htItemIndex[i].ToString() == item.ItemIndex.ToString())
        //                {
        //                    radGrdModuleMaster.Items[htItemIndex[i].ToString()].Edit = false;
        //                    htItemIndex.Remove(i);

        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    ViewState.Add(vsItemIndexes, htItemIndex);
        //}
        //#endregion

        //#region Button Click Events

        //protected void btnAddSave_Click(object sender, EventArgs e)
        //{
        //    bool Ststus = false;
        //    string ModuleName = txtAddModuleName.Text.Trim();
        //    int ModuleSortOrder = Convert.ToInt32(txtAddModuleSortOrder.Text.Trim());
        //    LookupBDto objLookupBdto = new LookupBDto();
        //    ModuleMasterDal objUserRolDal = new ModuleMasterDal();

        //    UserProfileBDto objUserProfile = null;
        //    objUserProfile = (UserProfileBDto)Session[PageConstants.ssnUserAuthorization];


        //    objLookupBdto.UserProfile.UserId = objUserProfile.UserId;
        //    objLookupBdto.LookupName = ModuleName;
        //    objLookupBdto.Int_order = ModuleSortOrder;
        //    Ststus = objUserRolDal.InsertModule(objLookupBdto);


        //    if (Ststus)
        //    {
        //        ClearControl();
        //        Master.DisplayMessage("Saved Successfully");
        //        pnlAddEdit.Visible = false;
        //        udcActionbar.DefaultMode = true;
        //        BindGrid();
        //    }
        //    else
        //    {
        //        Master.DisplayMessage("Record allready exist");
        //        Master.MessageCssClass = "Error";

        //    }


        //}


        //protected void btnAddCancel_Click(object sender, EventArgs e)
        //{
        //    pnlAddEdit.Visible = false;

        //    ClearControl();
        //}




        //#endregion

        //#region Custom Methods



        //private void ClearControl()
        //{

        //    txtAddModuleName.Text = "";
        //    txtAddModuleSortOrder.Text = "";

        //}

        //#endregion

        //#region Generate Xml String
        ///// <summary>
        ///// Generate xml format data from grid.
        ///// </summary>
        ///// <param name="grid">Rad grid control which data to be converted into xml format.</param>
        ///// <returns>Returns xml format data in string.</returns>
        //private String GenerateXmlString(RadGrid grid)
        //{
        //    string xmlRootStart = "<{0}>";
        //    string xmlRootEnd = "</{0}>";
        //    string xmlHeaderRootValue = "Node";
        //    string xmlHeaderNodeStructure = "<UserRole  Role_Id=\"{0}\" Role_NAME=\"{1}\" ></UserRole>";
        //    StringBuilder xmlString = new StringBuilder();
        //    try
        //    {
        //        if (Session[ssnHasTableIndex] != null)
        //        {
        //            htItemIndex = (Hashtable)Session[ssnHasTableIndex];
        //        }

        //        if (htItemIndex != null && htItemIndex.Count > 0)
        //        {

        //            xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

        //            for (int i = 0; i < htItemIndex.Count; i++)
        //            {
        //                Label lblGrdRoleId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblModuleId");
        //                TextBox txtModuleName = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblEditModuleName");
        //                xmlString.AppendFormat(xmlHeaderNodeStructure, lblGrdRoleId.Text, txtModuleName.Text);
        //            }
        //            xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
        //        }
        //    }
        //    catch (Exception ex) { }
        //    return xmlString.ToString();
        //}
        //#endregion

        //#region Methods
        //private void ResetActionBar()
        //{
        //    udcActionbar.VisibleNewButton = true;
        //    udcActionbar.VisibleEditButton = true;
        //    udcActionbar.VisibleDeleteButton = true;
        //    udcActionbar.VisibleExportButton = true;
        //    udcActionbar.VisibleSearchButton = true;
        //    udcActionbar.VisibleSearchTextBox = true;
        //    udcActionbar.VisibleSaveButton = false;
        //    udcActionbar.VisibleCancelButton = false;
        //}
        //#endregion
    }



}
