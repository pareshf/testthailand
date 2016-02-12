<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    CodeBehind="CompetitorAgentMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.CompetitorAgentMaster"
    Title="Competitor Master" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Views/Shared/Controls/Navigation/ActionBar.ascx" TagName="ActionBar"
    TagPrefix="crmUC" %>
<asp:Content ID="cntIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
 
    <script type="text/javascript" language="javascript">
    
		window.blockConfirm = function(text, mozEvent, oWidth, oHeight, callerObj, oTitle) 
		{
			var ev = mozEvent ? mozEvent : window.event; //Moz support requires passing the event argument manually
			//Cancel the event
			ev.cancelBubble = true;
			ev.returnValue = false;
			if (ev.stopPropagation) ev.stopPropagation(); 
			if (ev.preventDefault) ev.preventDefault();
  
			//Determine who is the caller
			var callerObj = ev.srcElement ? ev.srcElement : ev.target;
			//Call the original radconfirm and pass it all necessary parameters
			if (callerObj) { 
				//Show the confirm, then when it is closing, if returned value was true, automatically call the caller's click method again.
				var callBackFn = function(arg) {
					if (arg) {
						callerObj["onclick"] = "";
						if (callerObj.click) callerObj.click(); //Works fine every time in IE, but does not work for links in Moz
						else if (callerObj.tagName == "A") //We assume it is a link button!
						{
							try {
								eval(callerObj.href)
							}
							catch (e) { }
						}
					}
				}
				radconfirm(text, callBackFn, oWidth, oHeight, callerObj, oTitle);
			} 
			return false;
			}
			  function ramRequestStarted(ajaxManager, eventArgs) {
           //alert(eventArgs.EventTarget);
          if (eventArgs.EventTarget == "ctl00$cphPageContent$acbAgent$imgExcelExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbAgent$imgWordExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbAgent$imgPdfExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbAgent$imgCsvExport") {
              eventArgs.EnableAjax = false;               
          }
      }

    </script>

    <script language="javascript" type="text/javascript">
        var selectionAlertMessage ='<%=ConfigurationManager.AppSettings["AtleastOneRecord"].ToString() %>';
        var deleteAlertMessage ='<%=ConfigurationManager.AppSettings["DeleteAlert"].ToString() %>';
        
        
         function ValidateGridForEdit(gridControlId) {

            var radgridmaster = $find(gridControlId);
            var row = radgridmaster.get_masterTableView().get_selectedItems().length;
            if (row == 0) {
                radalert(selectionAlertMessage, 330, 110, 'Warning Message');
                return false;
            }
            else if (row > 1) {
                radalert('Select only one row', 330, 110, 'Warning Message');
                return false;
            }
        } 
        
    </script>

    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="cntPageContent" ContentPlaceHolderID="cphPageContent" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="ramRequestStarted" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbAgent$imgExcelExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbAgent$imgWordExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbAgent$imgPdfExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbAgent$imgCsvExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Style="z-index: 100100">
        <ConfirmTemplate>
            <div class="rwDialogPopup radconfirm">
                <div class="rwDialogText">
                    {1}
                </div>
                <div>
                    <a onclick="$find('{0}').close(true);" class="rwPopupButton" href="javascript:void(0);">
                        <span class="rwOuterSpan"><span class="rwInnerSpan">##LOC[OK]##</span></span></a>
                    <a onclick="$find('{0}').close(false);" class="rwPopupButton" href="javascript:void(0);">
                        <span class="rwOuterSpan"><span class="rwInnerSpan">##LOC[Cancel]##</span></span></a>
                </div>
            </div>
        </ConfirmTemplate>
    </telerik:RadWindowManager>
    <div class="pageTitle">
        <asp:Literal ID="lblPageAgent" runat="server" Text="Competitor Master "></asp:Literal>
    </div>
    <asp:UpdatePanel ID="upActionBar" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <crmUC:ActionBar ID="acbAgent" runat="server" SaveButtonValidationGroup="ReqAgentType"
                SaveNewButtonValidationGroup="ReqAgentType" OnbtnSaveClick="acbAgent_SaveClick"
                OnbtnCancelClick="acbAgent_CancelClick" OnbtnNewClick="acbAgent_NewClick" OnbtnSaveNewClick="acbAgent_SaveNewClick"
                OnbtnDeleteClick="acbAgent_DeleteClick" OnbtnSearchClick="acbAgent_SearchClick"
                OnbtnEditClick="acbAgent_EditClick" OnimgExcelExport_Click="acbAgent_ExcelExportClick"
                OnimgCsvExport_Click="acbAgent_CsvExportClick" OnimgPdfExport_Click="acbAgent_PdfExportClick"
                OnimgWordExport_Click="acbAgent_WordExportClick" OnbtnRefreshClick="acbAgent_RefreshClick" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="upAddNew" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAddNewMode" runat="server" Visible="false">
                            <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                                <tr>
                                    <th>
                                        <asp:Literal runat="server" ID="ltlTableHeader" Text="New Competitor"></asp:Literal>
                                    </th>
                                    <th>
                                        <div class="MandatoryNote" align="right">
                                            <asp:Literal ID="ltlMandatoryNote" runat="server">Fields marked with <span class="error">*</span> are mandatory.</asp:Literal>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Label ID="lblAgentType" runat="server" Text="Agent Name :"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAgentName" runat="server" MaxLength="100" ToolTip="Add Agent Name"
                                            Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="requireAgentType" runat="server" ControlToValidate="txtAgentName"
                                            ValidationGroup="Competitor" Display="Dynamic" ErrorMessage="<br />Enter Valid Agent Name"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Label ID="Label1" runat="server" Text="Address :"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ToolTip="Add Address"
                                            Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress"
                                            ValidationGroup="Competitor" Display="Dynamic" ErrorMessage="<br />Enter Address"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Label ID="Label2" runat="server" Text="Country :"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="radCmbCountry" runat="server" OnSelectedIndexChanged="radCmbCountry_SelectedIndexChanged"
                                            AutoPostBack="true" ToolTip="Select Country">
                                        </telerik:RadComboBox>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="radCmbCountry"
                                            ValidationGroup="Competitor" Display="Dynamic" ErrorMessage="<br />Select Company"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Label ID="Label3" runat="server" Text="State :"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="radCmbState" runat="server" OnSelectedIndexChanged="radCmbState_SelectedIndexChanged"
                                            AutoPostBack="true" ToolTip="Select State">
                                        </telerik:RadComboBox>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radCmbState"
                                            ValidationGroup="Competitor" Display="Dynamic" ErrorMessage="<br />Select State"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Label ID="Label4" runat="server" Text="City :"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="radCmbCity" runat="server" ToolTip="Select City">
                                        </telerik:RadComboBox>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radCmbCity"
                                            ValidationGroup="Competitor" Display="Dynamic" ErrorMessage="<br />Select City"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Label ID="Label5" runat="server" Text="Phone :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtphone" runat="server" OnKeyPress="javascript:return ValidPhneNumber('event')" ToolTip="Add Phone number" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbAgent" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlGrid" runat="server">
                            <table width="100%" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="radgrdAgent" runat="server" PageSize="<%$appSettings:GridPageSize %>"
                                            OnItemCommand="radgrdAgent_OnItemCommand" Width="100%" AllowMultiRowEdit="true"
                                            OnPreRender="radgrdAgent_PreRender" OnItemDataBound="radgrdAgent_ItemDataBound"
                                            OnNeedDataSource="radgrdAgent_NeedDataSource">
                                            <ClientSettings>
                                                <Selecting AllowRowSelect="true"></Selecting>
                                                <ClientEvents OnRowClick="OnRowClick" />
                                            </ClientSettings>
                                            <MasterTableView AutoGenerateColumns="False" EditMode="InPlace" AllowSorting="True"
                                                PagerStyle-AlwaysVisible="true">
                                                <RowIndicatorColumn>
                                                    <HeaderStyle Width="25px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <Columns>
                                                    <telerik:GridTemplateColumn>
                                                    <HeaderStyle HorizontalAlign="Center" Width="6%" />
                                                        <ItemStyle CssClass="ItemAlign" Width="25px" />
                                                       
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkHeadWrT" runat="server" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkItemWrt" runat="server" AutoPostBack="false" OnCheckedChanged="chkItemWrt_CheckChanged" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Id" UniqueName="AGENT_ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdAgentIdItem" runat="server" Text='<%# Bind("AGENT_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblgrdAgentIdEdit" runat="server" Text='<%# Bind("AGENT_ID") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Agent Name" UniqueName="AGENT_NAME" SortExpression="AGENT_NAME">
                                                        <HeaderStyle HorizontalAlign="Left" Width="15%"/>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkbtngrdAgentName" runat="server" Title="Click here to Sort"
                                                                CommandName='Sort' CommandArgument='AGENT_NAME' />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgrdAgentNameItem" runat="server" Text='<%# Bind("AGENT_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgrdAgentName" runat="server" Text='<%# Bind("AGENT_NAME") %>'></asp:TextBox>
                                                            <span class="error">*</span>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Address" UniqueName="AGENT_ADDRESS" SortExpression="AGENT_ADDRESS">
                                                        <HeaderStyle HorizontalAlign="Left"/>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkbtngrdAddress" runat="server" Title="Click here to Sort" CommandName='Sort'
                                                                CommandArgument='AGENT_ADDRESS' />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrdAddressItem" runat="server" Text='<%# Bind("AGENT_ADDRESS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblGrdAddressEdit" runat="server" Text='<%# Bind("AGENT_ADDRESS") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="CITY_ID" UniqueName="CITY_ID" SortExpression="CITY_ID"
                                                        Visible="false">
                                                        <HeaderStyle HorizontalAlign="Left"/>
                                                        <ItemStyle Width="90px" />
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkbtngrdCityId" runat="server" Title="Click here to Sort" CommandName='Sort'
                                                                CommandArgument='DATE_CREATED' />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrdCityIdItem" runat="server" Text='<%# Bind("CITY_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblGrdCityIdEdit" runat="server" Text='<%# Bind("CITY_ID") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="City" UniqueName="CITY_NAME" SortExpression="CITY_NAME">
                                                        <HeaderStyle HorizontalAlign="Left"/>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkbtngrdCityName" runat="server" Title="Click here to Sort"
                                                                CommandName='Sort' CommandArgument='CITY_NAME' />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrdCityName" runat="server" Text='<%# Bind("CITY_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblGrdCityNameEdit" runat="server" Text='<%# Bind("CITY_NAME") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="STATE_ID" UniqueName="STATE_ID" Visible="false"
                                                        SortExpression="STATE_ID">
                                                        <ItemStyle Width="95px" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkbtngrdStateId" runat="server" Title="Click here to Sort" CommandName='Sort'
                                                                CommandArgument='STATE_ID' />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrdStateIdItem" runat="server" Text='<%# Bind("STATE_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblGrdStateIdEdit" runat="server" Text='<%# Bind("STATE_ID") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="State" UniqueName="STATE_NAME" SortExpression="STATE_NAME">
                                                        <ItemStyle Width="95px" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkbtngrdStateName" runat="server" Title="Click here to Sort"
                                                                CommandName='Sort' CommandArgument='STATE_NAME' />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrdDateStateNameItem" runat="server" Text='<%# Bind("STATE_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblGrdDateStateNameEdit" runat="server" Text='<%# Bind("STATE_NAME") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="COUNTRY_ID" UniqueName="COUNTRY_ID" Visible="false"
                                                        SortExpression="COUNTRY_ID">
                                                        <ItemStyle Width="95px" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkbtngrdCountryId" runat="server" Title="Click here to Sort"
                                                                CommandName='Sort' CommandArgument='COUNTRY_ID' />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrdCountryIdItem" runat="server" Text='<%# Bind("COUNTRY_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblGrdCountryIdEdit" runat="server" Text='<%# Bind("COUNTRY_ID") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Country" UniqueName="COUNTRY_NAME" SortExpression="COUNTRY_NAME">
                                                        <ItemStyle Width="95px" />
                                                        <HeaderStyle HorizontalAlign="Left"  />
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkbtngrdCountryName" runat="server" Title="Click here to Sort"
                                                                CommandName='Sort' CommandArgument='COUNTRY_NAME' />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrdCountryNameItem" runat="server" Text='<%# Bind("COUNTRY_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblGrdCountryNameEdit" runat="server" Text='<%# Bind("COUNTRY_NAME") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Phone" UniqueName="PHONE_NO" SortExpression="PHONE_NO">
                                                        <ItemStyle Width="95px" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkbtngrdPhone" runat="server" Title="Click here to Sort" CommandName='Sort'
                                                                CommandArgument='PHONE_NO' />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrdPhoneItem" runat="server" Text='<%# Bind("PHONE_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblGrdDPhoneEdit" runat="server" Text='<%# Bind("PHONE_NO") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="OWNER_COMPANY_ID" UniqueName="OWNER_COMPANY_ID"
                                                        Visible="false" SortExpression="OWNER_COMPANY_ID">
                                                        <ItemStyle Width="95px" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkbtngrdOwnerCompany" runat="server" Title="Click here to Sort"
                                                                CommandName='Sort' CommandArgument='PHONE_NO' />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrdOwnerCompanyItem" runat="server" Text='<%# Bind("OWNER_COMPANY_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblGrdOwnerCompanyEdit" runat="server" Text='<%# Bind("OWNER_COMPANY_ID") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    
                                                    
                                                    <telerik:GridButtonColumn ButtonType="PushButton"    ButtonCssClass="button"  ItemStyle-Width="15%"  Text='Customer' >
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div>
                            <asp:HiddenField ID="hdnCheckIndex" runat="server" />
                            <asp:HiddenField ID="hdnEditableMode" runat="server" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbAgent" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
                
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                
                
                <ajax:ModalPopupExtender ID="PopEx_lnkBtnChangePreference" runat="server" BackgroundCssClass="modalPopupBackground"
                    PopupControlID="pnlCustomerAgentDetails" TargetControlID="HiddenField1">
                </ajax:ModalPopupExtender>
                
                <asp:HiddenField ID="HiddenField1" runat="server" />
                
                <asp:Panel ID="pnlCustomerAgentDetails" runat="server" Style="display: none" CssClass="panelhead" >
                    <table class="TableForm" cellpadding="4" cellspacing="0"   >
                        <tr>
                            <th style="background-color:Gray;" >
                                <asp:Label ID="lblTitleAlert" runat="server" Text="Customer Details"  ForeColor="#FEFEFE"
                                    Font-Size="15px"></asp:Label>
                            </th>
                            <th align="right" valign="middle" style="background-color:Gray">
                                <asp:ImageButton ID="ibgbtnClose" runat="server" ImageUrl="~/Views/Shared/Images/close.png"
                                    ImageAlign="Right" />
                            </th>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="radGrdCustomer" runat="server">
                                    <MasterTableView AutoGenerateColumns="False" AllowSorting="false" AllowPaging="false">
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="25px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="First Name" UniqueName="CUST_NAME" ItemStyle-HorizontalAlign="Left"
                                                DataField="CUST_NAME">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Sur Name" UniqueName="CUST_SURNAME" ItemStyle-HorizontalAlign="Left"
                                                DataField="CUST_SURNAME">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Year/Month" UniqueName="YEAR_MONTH" ItemStyle-HorizontalAlign="Left"
                                                DataField="YEAR_MONTH">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Country" UniqueName="COUNTRY_NAME" ItemStyle-HorizontalAlign="Left"
                                                DataField="COUNTRY_NAME">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Agent" UniqueName="AGENT_NAME" ItemStyle-HorizontalAlign="Left"
                                                DataField="AGENT_NAME">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Tour" UniqueName="TOUR_TYPE_NAME" ItemStyle-HorizontalAlign="Left"
                                                DataField="TOUR_TYPE_NAME">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Person" UniqueName="NO_OF_PERSON" ItemStyle-HorizontalAlign="Left"
                                                DataField="NO_OF_PERSON">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Description" UniqueName="DESCRIPATION" ItemStyle-HorizontalAlign="Left"   
                                                   DataField="DESCRIPATION" >
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                
                
                 </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ibgbtnClose" />
                    </Triggers>
                </asp:UpdatePanel>
                
            </td>
        </tr>
    </table>
</asp:Content>
