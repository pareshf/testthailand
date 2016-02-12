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
	public partial class RegionCountryMap : System.Web.UI.Page
	{
		#region Member variables
		Hashtable htItemIndex;
		Boolean bisEdit = false;
		CountryRegionMapBDto CountryRegionMapBDto = null;
		public const String vsGDSAirport = "Country Region Map";
		CountryRegionMapDal CountryRegionMapDal = null;
		AuthorizationBDto objAuthorizationBDto = null;
		BindCombo objBindCombo = null;		
		public static int GDSCode = 0;
		public static int AirlineId = 0;
		#endregion

		#region Events

		#region Page Events

		protected void Page_PreInit(object sender, EventArgs e)
		{
			WebHelper.WebManager.CheckSessionIsActive();
			WebHelper.WebManager.CheckUserAuthorizationForProgram("Product");
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
                Session["currentevent"] = "Country Region Map";

            }

			acbGDSCode.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateGridForEdit('{0}')", radgrdGDSCode.ClientID));
			acbGDSCode.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete1('{0}')", radgrdGDSCode.ClientID));
		}

		protected void Page_PreRender(object sender, EventArgs e)
		{
			objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
			if (objAuthorizationBDto != null)
			{
				if (!objAuthorizationBDto.ProgramWriteAccess)
				{

				}
			}

			if (!IsPostBack)
			{
				objBindCombo = new BindCombo();
				DataSet dsCountry = objBindCombo.GetCountryKeyValue();
				DataSet dsregion = objBindCombo.GetRegionKeyValue();
			BindRegionCombo(radAirline, dsregion);
			bindCountryCombo(radCmbDestination, dsCountry);

			}
		}




		#endregion

		#region Actionbar Events

		protected void acbGDSCode_NewClick(object sender, EventArgs e)
		{
			acbGDSCode.EditableMode = true;
			acbGDSCode.SaveNewButton.Visible = true;
			acbGDSCode.SaveButton.CommandName = "Save";
			pnlAddNewMode.Visible = true;
			radCmbDestination.Focus();
			pnlGrid.Visible = false;
			Reset();
			radCmbDestination.Enabled = true;
			radAirline.Enabled = true;



		}

		protected void acbGDSCode_SaveClick(object sender, EventArgs e)
		{
			try
			{
				int result = 0;
				switch (acbGDSCode.SaveButton.CommandName)
				{
					case "Save":
						Save();
						pnlAddNewMode.Visible = false;
						acbGDSCode.EditableMode = false;
						radCmbDestination.Enabled = true;
						radAirline.Enabled = true;
						break;
					case "Update":
						Update();
						pnlAddNewMode.Visible = false;
						acbGDSCode.EditableMode = false;
						pnlGrid.Visible = true;
						radCmbDestination.Enabled = false;
						radAirline.Enabled = false;
						break;
				}







			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
				if (rethrow)
				{ throw ex; }
			}
		}

		protected void acbGDSCode_SaveNewClick(object sender, EventArgs e)
		{
			radAirline.Focus();
			radAirline.Enabled = true;
			radAirline.Enabled = true;
			Save();
			// Reset();
			pnlGrid.Visible = false;

		}

		protected void acbGDSCode_CancelClick(object sender, EventArgs e)
		{
			if (ViewState[PageConstants.vsItemIndexes] != null)
			{
				htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

				for (int i = 0; i < htItemIndex.Count; i++)
					radgrdGDSCode.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
				bisEdit = false;
				ViewState[PageConstants.vsItemIndexes] = null;
				radgrdGDSCode.Rebind();

			}
			acbGDSCode.DefaultMode = true;
			pnlAddNewMode.Visible = false;
			pnlGrid.Visible = true;
			radCmbDestination.Enabled = true;
			radAirline.Enabled = true;
		}

		protected void acbGDSCode_DeleteClick(object sender, EventArgs e)
		{
			try
			{
				StringBuilder GDSCode = new StringBuilder();
				int result = 0;
				if (ViewState[PageConstants.vsItemIndexes] != null)
					htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

				if (htItemIndex != null)
				{
					foreach (int i in htItemIndex.Values)
					{

						Label lblGDSCode = (Label)radgrdGDSCode.Items[i].FindControl("lblGDSCodeItem");
						if (lblGDSCode != null)
						{
							GDSCode.Append(lblGDSCode.Text + ",");
						}
					}
				}

				CountryRegionMapDal = new CountryRegionMapDal(); 
				String CodeId = GDSCode.ToString().TrimEnd(',');
				result = CountryRegionMapDal.DeleteCountryRegionMap(CodeId);


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

		protected void acbGDSCode_SearchClick(object sender, EventArgs e)
		{
			try
			{

				CountryRegionMapDal = new CountryRegionMapDal();
				DataTable dsGDSCode = CountryRegionMapDal.FindCountryRegionMap(acbGDSCode.SearchTextBox.Text);
				radgrdGDSCode.DataSource = dsGDSCode;
				radgrdGDSCode.DataBind();
				ViewState[vsGDSAirport] = dsGDSCode;
				Reset();
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
				if (rethrow)
				{ throw ex; }
			}

		}

		protected void acbGDSCode_RefreshClick(object sender, EventArgs e)
		{
			acbGDSCode.DefaultMode = true;
			pnlAddNewMode.Visible = false;
			radCmbDestination.Enabled = true;
			radAirline.Enabled = true;
			acbGDSCode.SearchTextBox.Text = String.Empty;
			BindGrid();
		}

		protected void acbGDSCode_EditClick(object sender, EventArgs e)
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
						int DestinationCityID = 0;
						int AirLineId = 0;
						GridDataItem item = radgrdGDSCode.Items[int.Parse(htItemIndex[i].ToString())];
						Label lblGDSCodeItem = (Label)item.FindControl("lblGDSCodeItem");
						Label lblAirLineId = (Label)item.FindControl("lblAirLineId");
						Label lblDestinationCity = (Label)item.FindControl("lblDestinationCity");
						ViewState.Remove("SrNo");
						ViewState.Add("SrNo", lblGDSCodeItem.Text);

						DestinationCityID = Convert.ToInt32(lblDestinationCity.Text);
						AirLineId = Convert.ToInt32(lblAirLineId.Text);
						try
						{
							radCmbDestination.ClearSelection();
							radCmbDestination.SelectedValue = DestinationCityID.ToString();
							radAirline.ClearSelection();
							radAirline.SelectedValue = AirLineId.ToString();
						}
						catch { }

					}
					CountryRegionMapDal = new CountryRegionMapDal();

				}
				bisEdit = true;
				pnlAddNewMode.Visible = true;
				pnlGrid.Visible = false;
				acbGDSCode.EditableMode = true;
				acbGDSCode.SaveButton.CommandName = "Update";
				acbGDSCode.SaveNewButton.Visible = false;

			}

			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
				if (rethrow)
				{ throw ex; }

			}

		}

		#endregion

		#region Grid Event

		protected void radgrdGDSCode_PreRender(object source, EventArgs e)
		{
			if (bisEdit)
			{
				GridHeaderItem headerItem = radgrdGDSCode.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

				CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
				if (chkHeader != null)
					chkHeader.Visible = false;

				foreach (GridDataItem item in radgrdGDSCode.Items)
				{
					CheckBox chkItem = (CheckBox)item.FindControl("chkItemWrt");
					if (chkItem != null)
						chkItem.Visible = false;
				}
			}
		}


		protected void radgrdGDSCode_ItemDataBound(object sender, GridItemEventArgs e)
		{
			try
			{
				if (e.Item.ItemType == GridItemType.Header)
				{
					CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
					if (chkBox != null)
						chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdGDSCode.ClientID, 0, chkBox.ClientID));
				}

				if (e.Item is GridDataItem)
				{
					CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
					if (chkBox != null)
						chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdGDSCode.ClientID, chkBox.ClientID, e.Item.ItemIndex));
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		protected void radgrdGDSCode_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			if (ViewState[vsGDSAirport] != null)
				radgrdGDSCode.DataSource = ViewState[vsGDSAirport];
		}		

		#endregion

		#region GridCheckBox event


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
							radgrdGDSCode.Items[htItemIndex[i].ToString()].Edit = false;
							htItemIndex.Remove(i);

							break;
						}
					}
				}
			}
			ViewState.Add(PageConstants.vsItemIndexes, htItemIndex);
		}

		#endregion

		//#region btnDeleteAirport_Click
		//protected void btnDeleteAirport_Click(object sender, EventArgs e)
		//{


		//    try
		//    {
		//        int serialNo = 0;
		//        ImageButton imgbtnDetailDelete = (ImageButton)sender;
		//        if (imgbtnDetailDelete != null)
		//            Int32.TryParse(imgbtnDetailDelete.CommandArgument, out serialNo);

		//        objAirlineMapDal = new AirlineMapDal();

		//        int result = objAirlineMapDal.DeleteAirportBySrNo(serialNo);
		//        if (result == 1)
		//        {
		//            Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Delete].ToString());
		//            Master.MessageCssClass = "successMessage";
		//            BindAirportGrid();
		//        }
		//        else if (result == 547)
		//        {
		//            Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Delete].ToString());
		//            Master.MessageCssClass = "errorMessage";
		//        }
		//        else
		//        {
		//            Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Delete].ToString());
		//            Master.MessageCssClass = "errorMessage";
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
		//        if (rethrow)
		//        { throw ex; }
		//    }
		//}
		//#endregion


		#endregion

		#region Method

		private void BindRegionCombo(RadComboBox sender, DataSet source)
		{
			objBindCombo = new BindCombo();
			sender.Items.Clear();
			sender.ClearSelection();
			sender.DataTextField = "REGION_LONG_NAME";
			sender.DataValueField = "REGION_ID";
			if (source != null)
				sender.DataSource = source;
			else
				sender.DataSource = objBindCombo.GetCityKeyValue(0, 0);
			sender.DataBind();
			sender.Items.Insert(0, new RadComboBoxItem("", "0"));
			sender.SelectedValue = "0";
		}

		private void bindCountryCombo(RadComboBox sender, DataSet source)
		{
			objBindCombo = new BindCombo();
			sender.Items.Clear();
			sender.ClearSelection();
			sender.DataTextField = "COUNTRY_NAME";
			sender.DataValueField = "COUNTRY_ID";
			if (source != null)
				sender.DataSource = source;
			else
				sender.DataSource = objBindCombo.GetAirLine();
			sender.DataBind();
			sender.Items.Insert(0, new RadComboBoxItem("", "0"));
			sender.SelectedValue = "0";
		}

		#region Bind Grid

		private void BindGrid()
		{
			CountryRegionMapDal = new CountryRegionMapDal();
			DataTable dsGDSCode = CountryRegionMapDal.FindCountryRegionMap("");
			radgrdGDSCode.DataSource = dsGDSCode;
			radgrdGDSCode.DataBind();
			ViewState[vsGDSAirport] = dsGDSCode;
			pnlGrid.Visible = true;

		}

		#endregion

		

		#region Save
		private void Save()
		{
			try
			{
				int result = 0;
				CountryRegionMapDal = new CountryRegionMapDal();

				bool IsInsert = false;
				CountryRegionMapBDto = new CountryRegionMapBDto();
					if (radAirline.SelectedValue != "0")
						CountryRegionMapBDto.RegionId = Convert.ToInt32(radAirline.SelectedValue);
					if (radCmbDestination.SelectedValue != "0")
						CountryRegionMapBDto.CountryId = Convert.ToInt32(radCmbDestination.SelectedValue);
					result = CountryRegionMapDal.InsertRegionCountryMap(CountryRegionMapBDto);
					if (result > 0)
					{
						IsInsert = true;
					}
					else
					{
						IsInsert = false;
					}


				


				if (IsInsert)
				{
					Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
					Master.MessageCssClass = "successMessage";
					acbGDSCode.SaveButton.CommandName = "Update";
					bisEdit = false;
					Session.Remove("DocData");
					Session.Remove("DocData2");
				}
				else
				{
					Master.DisplayMessage(ConfigurationSettings.AppSettings[GeneralMessage.ExistRecord].ToString());
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
			radCmbDestination.ClearSelection();
			radCmbDestination.SelectedIndex = 0;
			radAirline.ClearSelection();
			radAirline.SelectedIndex = 0;

		}
		#endregion

		#region Update
		private void Update()
		{
			try
			{
				int result = 0;
				CountryRegionMapDal = new CountryRegionMapDal();

				bool IsInsert = false;
				CountryRegionMapBDto = new CountryRegionMapBDto();

				if (ViewState["SrNo"] != null)
				{
					CountryRegionMapBDto.SrNO = Convert.ToInt32(ViewState["SrNo"]);
				}
			
					if (radAirline.SelectedValue != "0")
						CountryRegionMapBDto.RegionId = Convert.ToInt32(radAirline.SelectedValue);
					if (radCmbDestination.SelectedValue != "0")
						CountryRegionMapBDto.CountryId = Convert.ToInt32(radCmbDestination.SelectedValue);

					result = CountryRegionMapDal.UpdateRegionCountryMap(CountryRegionMapBDto);
					if (result > 0)
					{
						IsInsert = true;
					}
					else
					{
						IsInsert = false;
					}

				
				if (IsInsert)
				{
					Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Update].ToString());
					Master.MessageCssClass = "successMessage";
					acbGDSCode.SaveButton.CommandName = "Update";
					BindGrid();
				}
				else
				{
					Master.DisplayMessage(ConfigurationSettings.AppSettings[GeneralMessage.ExistRecord].ToString());
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
		#endregion

	   #endregion
	}
}
