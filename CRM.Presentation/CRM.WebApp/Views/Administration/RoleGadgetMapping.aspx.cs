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
using CRM.DataAccess;
using CRM.Core.Constants;
using Telerik.Web.UI;
using CRM.Model.AdministrationModel;
using CRM.DataAccess.AdministrationDAL;

namespace CRM.WebApp.Views.Administration
{
    public partial class RoleGadgetMapping : System.Web.UI.Page
    {
        BindCombo objBindCombo = null;
        RoleGadgetMappingBDto objRoleGadgetMappingBDto = null;
        RoleGadgetMappingDal objRoleGadgetMappingDal = null;
        public static int GlobalRoleId;
        #region Page Event


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRoleGrid();
            }
            if (Session[PageConstants.ssnUserAuthorization] != null)
            {
                Session["currentevent"] = "Gadget Access";
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Gadget Access");
        }
        #region Override Style Sheet Theme
        public override string StyleSheetTheme
        {
            get
            {
                if (HttpContext.Current.Session[PageConstants.ThemeName] == null)
                    return "Default";
                else
                    return HttpContext.Current.Session[PageConstants.ThemeName].ToString();
            }
        }
        #endregion
        #endregion

        #region GridEvent
        protected void radgrdRoleAccess_OnItemCommand(object Sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
                {
                    //showWarningMessage();
                    GridDataItem item = radgrdRoleAccess.Items[e.Item.ItemIndex];
                    GlobalRoleId = int.Parse(item["ROLE_ID"].Text);
                    lblRoleTitle1.Text = item["ROLE_NAME"].Text;
                    pnlAccessGrid.Visible = true;
                    BindGridGadgetAccess(GlobalRoleId);
                }
                else
                {
                    pnlAccessGrid.Visible = false;
                }
            }
            catch (Exception ex)
            {
                pnlAccessGrid.Visible = false;
            }
        }

        protected void radGridAccess_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBoxRead = (CheckBox)e.Item.FindControl("grdChkAllRead");
                    if (chkBoxRead != null)
                        chkBoxRead.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')",
                            radGridAccess.ClientID,
                            3,
                            chkBoxRead.ClientID));
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region Button Event

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            int result;
            bool SaveFail = true;
            objRoleGadgetMappingBDto = new RoleGadgetMappingBDto();
            objRoleGadgetMappingDal = new RoleGadgetMappingDal();
            foreach (GridDataItem item in radGridAccess.Items)
            {
                CheckBox chkRead = (CheckBox)item.FindControl("grdChkRead");
                if (item["GADGET_ID"] != null)
                    objRoleGadgetMappingBDto.GadgetID = int.Parse(item["GADGET_ID"].Text);
                if (item["MODULE_ID"] != null)
                    objRoleGadgetMappingBDto.ModuleID = int.Parse(item["MODULE_ID"].Text);
                if (GlobalRoleId != 0)
                    objRoleGadgetMappingBDto.RoleID = GlobalRoleId;
                objRoleGadgetMappingBDto.ReadAccess = chkRead.Checked;
                result = objRoleGadgetMappingDal.InsertAccess(objRoleGadgetMappingBDto);
                if (result != 1)
                    SaveFail = false;
            }
            if (SaveFail)
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                Master.MessageCssClass = "successMessage";
            }
            pnlAccessGrid.Visible = false;
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            pnlAccessGrid.Visible = false;
        }

        #endregion

        #region Methods

        #region Bind Grid
        /// <summary>
        /// Bind customer grid
        /// </summary>
        private void BindRoleGrid()
        {
            objBindCombo = new BindCombo();
            DataSet dsRoleMapping = objBindCombo.GetRoleKeyValue();
            radgrdRoleAccess.DataSource = dsRoleMapping;
            radgrdRoleAccess.DataBind();
        }
        #endregion

        #region Bind Role Access Grid
        /// <summary>
        /// Bind customer grid
        /// </summary>
        private void BindGridGadgetAccess(int RoleId)
        {
            DataSet ds = null;
            objRoleGadgetMappingDal = new RoleGadgetMappingDal();
            objRoleGadgetMappingBDto = new RoleGadgetMappingBDto();
            objRoleGadgetMappingBDto.RoleID = RoleId;
            ds = objRoleGadgetMappingDal.GetGadgetByRole(objRoleGadgetMappingBDto);
            radGridAccess.DataSource = ds;
            radGridAccess.DataBind();
        }
        #endregion

        #endregion

    }
}
