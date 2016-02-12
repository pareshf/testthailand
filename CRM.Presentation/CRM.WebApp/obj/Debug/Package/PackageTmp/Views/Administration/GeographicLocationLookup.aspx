<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    CodeBehind="GeographicLocationLookup.aspx.cs" Inherits="CRM.WebApp.Views.Administration.GeographicLocationLookup"
    Title="" %>

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
            if (eventArgs.EventTarget == "ctl00$cphPageContent$acbGeographicLocation$imgExcelExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbGeographicLocation$imgWordExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbGeographicLocation$imgPdfExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbGeographicLocation$imgCsvExport") {
                eventArgs.EnableAjax = false;               
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        var selectionAlertMessage ='<%=ConfigurationManager.AppSettings["AtleastOneRecord"].ToString() %>';
        var deleteAlertMessage ='<%=ConfigurationManager.AppSettings["DeleteAlert"].ToString() %>';
    </script>

    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>

    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="cntPageContent" ContentPlaceHolderID="cphPageContent" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager" runat="server">
        <ClientEvents OnRequestStart="ramRequestStarted" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbGeographicLocation$imgExcelExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbGeographicLocation$imgWordExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbGeographicLocation$imgPdfExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbGeographicLocation$imgCsvExport">
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
        <asp:Literal ID="lblPageTitle" runat="server" Text="Geographic Location"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="upActionBar" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <crmUC:ActionBar ID="acbGeographicLocation" runat="server" OnbtnNewClick="acbGeographicLocation_NewClick"
                OnbtnSaveClick="acbGeographicLocation_SaveClick" OnbtnCancelClick="acbGeographicLocation_CancelClick"
                OnbtnDeleteClick="acbGeographicLocation_DeleteClick" SaveButtonValidationGroup="ReqGeoLocation"
                OnbtnSaveNewClick="acbGeographicLocation_SaveNewClick" OnbtnEditClick="acbGeographicLocation_EditClick"
                OnbtnSearchClick="acbGeographicLocation_SearchClick" SaveNewButtonValidationGroup="ReqGeoLocation"
                OnimgExcelExport_Click="acbGeographicLocation_ExcelExportClick" OnimgCsvExport_Click="acbGeographicLocation_CsvExportClick"
                OnimgPdfExport_Click="acbGeographicLocation_PdfExportClick" OnimgWordExport_Click="acbGeographicLocation_WordExportClick"
                OnbtnRefreshClick="acbGeographicLocation_RefreshClick" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="upAddNew" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAddNewMode" runat="server" Visible="false">
                            <asp:Panel ID="pnlAddCountry" runat="server" Visible="false">
                                <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <th>
                                            <asp:Literal runat="server" ID="ltrNewCountry" Text="New Country"></asp:Literal>
                                        </th>
                                        <th>
                                            <div class="MandatoryNote" align="right" style="margin-top: 3px; margin-bottom: 3px">
                                                <asp:Literal ID="MandatoryNoteCountry" runat="server">Fields marked with <span class="error">*</span> are mandatory.&nbsp;</asp:Literal>
                                            </div>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="width: 120px">
                                            <asp:Label ID="lblCountryName" runat="server" Text="Country Name :"></asp:Label>
                                            <span class="error">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCountryName" runat="server" ValidationGroup="ReqCountryName"
                                                Visible="true" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requireCountryName" runat="server" ControlToValidate="txtCountryName"
                                                ValidationGroup="ReqGeoLocation" Display="Dynamic" ErrorMessage="Enter Valid Country Name"
                                                CssClass="error"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="CountryCode" runat="server" Text="Country Code :"></asp:Label>
                                            <span class="error">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCountryCode" runat="server" ValidationGroup="ReqCountryCode"
                                                Visible="true" MaxLength="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCountryCode" runat="server" ControlToValidate="txtCountryCode"
                                                ValidationGroup="ReqGeoLocation" Display="Dynamic" ErrorMessage="Enter Valid Country Code"
                                                CssClass="error"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="CurrencySymbol" runat="server" Text="Currency Symbol :"></asp:Label>
                                            <span class="error">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCurrencySymbol" runat="server" ValidationGroup="ReqCurrencySymbol"
                                                Visible="true" MaxLength="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCurrencySymbol" runat="server" ControlToValidate="txtCurrencySymbol"
                                                ValidationGroup="ReqGeoLocation" Display="Dynamic" ErrorMessage="Enter Valid Currency Symbol"
                                                CssClass="error"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="CurrencyName" runat="server" Text="Currency Name :"></asp:Label>
                                            <span class="error">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCurrencyName" runat="server" ValidationGroup="ReqCurrencyName"
                                                Visible="true" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCurrencyName" runat="server" ControlToValidate="txtCurrencyName"
                                                ValidationGroup="ReqGeoLocation" Display="Dynamic" ErrorMessage="Enter Valid Currency Name"
                                                CssClass="error"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlAddState" runat="server" Visible="false">
                                <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <th>
                                            <asp:Literal runat="server" ID="ltrNewState" Text="New State"></asp:Literal>
                                        </th>
                                        <th>
                                            <div class="MandatoryNote" align="right" style="margin-top: 3px; margin-bottom: 3px">
                                                <asp:Literal ID="Literal3" runat="server">Fields marked with <span class="error">*</span> are mandatory.&nbsp;</asp:Literal>
                                            </div>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="lblCountryNameState" runat="server" Text="Country Name :"></asp:Label>
                                            <span class="error">*</span>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbCountryName" runat="server" Height="<%$appSettings:ComboBoxHeight %>">
                                                <CollapseAnimation Duration="50" />
                                                <ExpandAnimation Duration="50" />
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="0" Text="" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="rflcmbCountry" runat="server" ControlToValidate="cmbCountryName"
                                                ValidationGroup="ReqGeoLocation" Display="Dynamic" ErrorMessage="Enter Valid Country Name"
                                                CssClass="error"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="StateName" runat="server" Text="State Name :"></asp:Label>
                                            <span class="error">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtStateName" runat="server" ValidationGroup="ReqStateName" Visible="true"
                                                MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvStateName" runat="server" ControlToValidate="txtStateName"
                                                ValidationGroup="ReqGeoLocation" Display="Dynamic" ErrorMessage="Enter Valid State Name"
                                                CssClass="error"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlAddCity" runat="server" Visible="false">
                                <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <th>
                                            <asp:Literal runat="server" ID="ltrNewCity" Text="New City"></asp:Literal>
                                        </th>
                                        <th>
                                            <div class="MandatoryNote" align="right" style="margin-top: 3px; margin-bottom: 3px">
                                                <asp:Literal ID="Literal2" runat="server">Fields marked with <span class="error">*</span> are mandatory.&nbsp;</asp:Literal>
                                            </div>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="lblCountryNameCity" runat="server" Text="Country Name :"></asp:Label>
                                            <span class="error">*</span>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbCountryNameCity" runat="server" Height="<%$appSettings:ComboBoxHeight %>"
                                                AutoPostBack="true" OnSelectedIndexChanged="cmbCountryNameCity_SelectedIndexChanged">
                                                <CollapseAnimation Duration="50" />
                                                <ExpandAnimation Duration="50" />
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="0" Text="" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbCountryNameCity"
                                                ValidationGroup="ReqGeoLocation" Display="Dynamic" ErrorMessage="Enter Valid Country Name"
                                                CssClass="error"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="lblStateNameCity" runat="server" Text="State Name :"></asp:Label>
                                            <span class="error">*</span>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbStateNameCity" runat="server" Height="<%$appSettings:ComboBoxHeight %>">
                                                <CollapseAnimation Duration="50" />
                                                <ExpandAnimation Duration="50" />
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="0" Text="" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="rfvStateNameCity" runat="server" ControlToValidate="cmbStateNameCity"
                                                ValidationGroup="ReqGeoLocation" Display="Dynamic" ErrorMessage="Enter Valid State Name"
                                                CssClass="error"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="lblCityName" runat="server" Text="City Name :"></asp:Label>
                                            <span class="error">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCityName" runat="server" ValidationGroup="ReqCityName" Visible="true"
                                                Width="<%$appSettings:TextBoxWidth %>" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvReqCityName" runat="server" ControlToValidate="txtCityName"
                                                ValidationGroup="ReqGeoLocation" Display="Dynamic" ErrorMessage="Enter Valid City Name"
                                                CssClass="error"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbGeographicLocation" />
                        <asp:AsyncPostBackTrigger ControlID="rtabGeographicLocation" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upGeographicLocation" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Panel ID="pnlGeographicLocation" runat="server">
                            <telerik:RadTabStrip ID="rtabGeographicLocation" runat="server" SelectedIndex="0"
                                OnTabClick="rtabGeographicLocation_TabClick">
                                <Tabs>
                                    <telerik:RadTab Value="Country" Text="Country">
                                    </telerik:RadTab>
                                    <telerik:RadTab Value="State" Text="State">
                                    </telerik:RadTab>
                                    <telerik:RadTab Value="City" Text="City">
                                    </telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:MultiView ID="mvGeographicLocation" runat="server" ActiveViewIndex="0">
                            <asp:View ID="vwCountry" runat="server">
                                <telerik:RadGrid ID="radgrdCountry" runat="server" AutoGenerateColumns="false" AllowMultiRowSelection="true"
                                    AllowMultiRowEdit="true" AllowPaging="true" AllowSorting="true" ClientSettings-EnableRowHoverStyle="true"
                                    EnableEmbeddedSkins="false" PageSize="<%$appSettings:GridPageSize %>" OnItemDataBound="radgrdCountry_ItemDataBound"
                                    OnNeedDataSource="radgrdCountry_NeedDataSource">
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="true"></Selecting>
                                        <ClientEvents OnRowClick="OnRowClick" />
                                    </ClientSettings>
                                    <HeaderContextMenu EnableEmbeddedSkins="False" EnableTheming="True">
                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                    </HeaderContextMenu>
                                    <MasterTableView AutoGenerateColumns="False" EditMode="InPlace" AllowSorting="True"
                                        PagerStyle-AlwaysVisible="true">
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridTemplateColumn>
                                                <ItemStyle CssClass="ItemAlign" Width="25px" />
                                                <HeaderStyle Width="25px" />
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeadWrT" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItemWrt" runat="server" AutoPostBack="false" OnCheckedChanged="chkItemWrtCountry_CheckChanged" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Id" UniqueName="COUNTRY_ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdCountryIdItem" runat="server" Text='<%# Bind("COUNTRY_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblGrdCountryIdEdit" runat="server" Text='<%# Bind("COUNTRY_ID") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Country Name" UniqueName="COUNTRY_NAME" SortExpression="COUNTRY_NAME">
                                                <HeaderStyle HorizontalAlign="Left" Width ="13%" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdCountryName" runat="server" Title="Click here to sort"
                                                        CommandName='Sort' CommandArgument='COUNTRY_NAME' />
                                                </HeaderTemplate>
                                                <ItemStyle Width="170px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdCountryName" runat="server" Text='<%# Bind("COUNTRY_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtGrdCountryName" runat="server" Text='<%# Bind("COUNTRY_NAME") %>'
                                                        MaxLength="50" Width="150px"></asp:TextBox>
                                                    <span class="error">*</span>
                                                    <asp:RequiredFieldValidator ID="requireGrdCountryName" runat="server" ControlToValidate="txtGrdCountryName"
                                                        ValidationGroup="ReqGeoLocation" Display="Dynamic" ErrorMessage="Enter Valid Country Name"
                                                        CssClass="error"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Code" UniqueName="COUNTRY_CODE" SortExpression="COUNTRY_CODE">
                                                <HeaderStyle HorizontalAlign="Left"  Width ="6%" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdCountryCode" runat="server" Title="Click here to sort"
                                                        CommandName='Sort' CommandArgument='COUNTRY_CODE' />
                                                </HeaderTemplate>
                                                <ItemStyle Width="80px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdCountryCode" runat="server" Text='<%# Bind("COUNTRY_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtGrdCountryCode" runat="server" MaxLength="3" Text='<%# Bind("COUNTRY_CODE") %>'
                                                        Width="60px"></asp:TextBox>
                                                    <span class="error">*</span>
                                                    <asp:RequiredFieldValidator ID="requireGrdCountryCode" runat="server" ControlToValidate="txtGrdCountryCode"
                                                        ValidationGroup="ReqGeoLocation" Display="Dynamic" ErrorMessage="Enter Valid Country Code"
                                                        CssClass="error"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Currency Symbol" UniqueName="COUNTRY_CURRENCY_SYMBOL"
                                                SortExpression="COUNTRY_CURRENCY_SYMBOL">
                                                <HeaderStyle HorizontalAlign="Left" Width ="15%" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdCurrencySymbol" runat="server" Title="Click here to sort"
                                                        CommandName='Sort' CommandArgument='COUNTRY_CURRENCY_SYMBOL' />
                                                </HeaderTemplate>
                                                <ItemStyle Width="100px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdCurrencySymbol" runat="server" Text='<%# Bind("COUNTRY_CURRENCY_SYMBOL") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtGrdCurrencySymbol" runat="server" MaxLength="5" Text='<%# Bind("COUNTRY_CURRENCY_SYMBOL") %>'
                                                        Width="60px"></asp:TextBox>
                                                    <span class="error">*</span>
                                                    <asp:RequiredFieldValidator ID="requiregrdcountrysymbol" runat="server" ControlToValidate="txtGrdCurrencySymbol"
                                                        ValidationGroup="ReqGeoLocation" Display="dynamic" ErrorMessage="Enter Valid Currency Symbol"
                                                        CssClass="error"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Name" UniqueName="COUNTRY_CURRENCY_NAME"
                                                SortExpression="COUNTRY_CURRENCY_NAME">
                                                <HeaderStyle HorizontalAlign="Left" Width ="10%" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdCurrencyName" runat="server" Title="Click here to sort"
                                                        CommandName='Sort' CommandArgument='COUNTRY_CURRENCY_NAME' />
                                                </HeaderTemplate>
                                                <ItemStyle Width="150px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdCurrencyName" runat="server" Text='<%# Bind("COUNTRY_CURRENCY_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtGrdCurrencyName" runat="server" MaxLength="50" Text='<%# Bind("COUNTRY_CURRENCY_NAME") %>'
                                                        Width="100px"></asp:TextBox>
                                                    <span class="error">*</span>
                                                    <asp:RequiredFieldValidator ID="requireGrdCurrencyName" runat="server" ControlToValidate="txtGrdCurrencyName"
                                                        ValidationGroup="ReqGeoLocation" Display="Dynamic" ErrorMessage="Enter Valid Currency Name"
                                                        CssClass="error"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Created By" UniqueName="CREATED_BY" SortExpression="CREATED_BY">
                                                <HeaderStyle HorizontalAlign="Left" Width ="10%" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdCreatedBy" runat="server" Title="Click here to Sort"
                                                        CommandName='Sort' CommandArgument='CREATED_BY' />
                                                </HeaderTemplate>
                                                <ItemStyle Width="80px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdCreatedByItem" runat="server" Text='<%# Bind("CREATED_BY") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblGrdCreatedByEdit" runat="server" Text='<%# Bind("CREATED_BY") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Date Created" UniqueName="DATE_CREATED" SortExpression="DATE_CREATED">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdDateCreated" runat="server" Title="Click here to Sort"
                                                        CommandName='Sort' CommandArgument='DATE_CREATED' />
                                                </HeaderTemplate>
                                                <ItemStyle Width="80px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdDateCreatedItem" runat="server" Text='<%# Bind("DATE_CREATED") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblGrdDateCreatedEdit" runat="server" Text='<%# Bind("DATE_CREATED") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Modified By" UniqueName="MODIFIED_BY" SortExpression="MODIFIED_BY">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdModifiedBy" runat="server" Title="Click here to Sort"
                                                        CommandName='Sort' CommandArgument='MODIFIED_BY' />
                                                </HeaderTemplate>
                                                <ItemStyle Width="80px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdModifiedByItem" runat="server" Text='<%# Bind("MODIFIED_BY") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblGrdModifiedByEdit" runat="server" Text='<%# Bind("MODIFIED_BY") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Date Modified" UniqueName="DATE_MODIFIED"
                                                SortExpression="DATE_MODIFIED">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdDateModified" runat="server" Title="Click here to Sort"
                                                        CommandName='Sort' CommandArgument='DATE_MODIFIED' />
                                                </HeaderTemplate>
                                                <ItemStyle Width="80px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdDateModifiedItem" runat="server" Text='<%# Bind("DATE_MODIFIED") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblGrdDateModifiedEdit" runat="server" Text='<%# Bind("DATE_MODIFIED") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn CancelImageUrl="Cancel.gif" EditImageUrl="Edit.gif" InsertImageUrl="Update.gif"
                                                UpdateImageUrl="Update.gif">
                                            </EditColumn>
                                        </EditFormSettings>
                                        <PagerStyle AlwaysVisible="True" />
                                    </MasterTableView>
                                    <FilterMenu EnableEmbeddedSkins="False" EnableTheming="True">
                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                    </FilterMenu>
                                </telerik:RadGrid>
                                <asp:HiddenField ID="hdnCheckIndexCountry" runat="server" />
                                <asp:HiddenField ID="hdnEditableModeCountry" runat="server" />
                            </asp:View>
                            <asp:View ID="vwState" runat="server">
                                <telerik:RadGrid ID="radgrdState" runat="server" AutoGenerateColumns="false" AllowMultiRowSelection="true"
                                    AllowMultiRowEdit="true" AllowPaging="true" AllowSorting="true" ClientSettings-EnableRowHoverStyle="true"
                                    EnableEmbeddedSkins="false" PageSize="<%$appSettings:GridPageSize %>" OnItemDataBound="radgrdState_ItemDataBound"
                                    OnNeedDataSource="radgrdState_NeedDataSource">
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="true"></Selecting>
                                        <ClientEvents OnRowClick="OnRowClick" />
                                    </ClientSettings>
                                    <HeaderContextMenu EnableEmbeddedSkins="False" EnableTheming="True">
                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                    </HeaderContextMenu>
                                    <MasterTableView AutoGenerateColumns="False" EditMode="InPlace" AllowSorting="True"
                                        PagerStyle-AlwaysVisible="true">
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridTemplateColumn>
                                                <ItemStyle CssClass="ItemAlign" Width="25px" />
                                                <HeaderStyle Width="25px" />
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeadWrT" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItemWrt" runat="server" AutoPostBack="false" OnCheckedChanged="chkItemWrtState_CheckChanged" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Id" UniqueName="STATE_ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdStateIdItem" runat="server" Text='<%# Bind("STATE_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblGrdStateIdEdit" runat="server" Text='<%# Bind("STATE_ID") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="State Name" UniqueName="STATE_NAME" SortExpression="STATE_NAME">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdStateName" runat="server" Title="Click here to sort"
                                                        CommandName='Sort' CommandArgument='STATE_NAME' />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdStateName" runat="server" Text='<%# Bind("STATE_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtGrdStateName" runat="server" Text='<%# Bind("STATE_NAME") %>'
                                                        MaxLength="50"></asp:TextBox>
                                                    <span class="error">*</span>
                                                    <asp:RequiredFieldValidator ID="requireGrdStateName" runat="server" ControlToValidate="txtGrdStateName"
                                                        ValidationGroup="ReqGeoLocation" Display="Dynamic" ErrorMessage="Enter Valid State Name"
                                                        CssClass="error"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Country Name" UniqueName="COUNTRY_NAME" SortExpression="COUNTRY_NAME">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdCountryName" runat="server" Title="Click here to sort"
                                                        CommandName='Sort' CommandArgument='COUNTRY_NAME' />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdCountryNameItem" runat="server" Text='<%# Bind("COUNTRY_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblGrdCountryNameEdit" runat="server" Text='<%# Bind("COUNTRY_NAME") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Created By" UniqueName="CREATED_BY" SortExpression="CREATED_BY">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdCreatedBy" runat="server" Title="Click here to Sort"
                                                        CommandName='Sort' CommandArgument='CREATED_BY' />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdCreatedByItem" runat="server" Text='<%# Bind("CREATED_BY") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblGrdCreatedByEdit" runat="server" Text='<%# Bind("CREATED_BY") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Date Created" UniqueName="DATE_CREATED" SortExpression="DATE_CREATED">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdDateCreated" runat="server" Title="Click here to Sort"
                                                        CommandName='Sort' CommandArgument='DATE_CREATED' />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdDateCreatedItem" runat="server" Text='<%# Bind("DATE_CREATED") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblGrdDateCreatedEdit" runat="server" Text='<%# Bind("DATE_CREATED") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Modified By" UniqueName="MODIFIED_BY" SortExpression="MODIFIED_BY">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdModifiedBy" runat="server" Title="Click here to Sort"
                                                        CommandName='Sort' CommandArgument='MODIFIED_BY' />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdModifiedByItem" runat="server" Text='<%# Bind("MODIFIED_BY") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblGrdModifiedByEdit" runat="server" Text='<%# Bind("MODIFIED_BY") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Date Modified" UniqueName="DATE_MODIFIED"
                                                SortExpression="DATE_MODIFIED">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdDateModified" runat="server" Title="Click here to Sort"
                                                        CommandName='Sort' CommandArgument='DATE_MODIFIED' />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdDateModifiedItem" runat="server" Text='<%# Bind("DATE_MODIFIED") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblGrdDateModifiedEdit" runat="server" Text='<%# Bind("DATE_MODIFIED") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn CancelImageUrl="Cancel.gif" EditImageUrl="Edit.gif" InsertImageUrl="Update.gif"
                                                UpdateImageUrl="Update.gif">
                                            </EditColumn>
                                        </EditFormSettings>
                                        <PagerStyle AlwaysVisible="True" />
                                    </MasterTableView>
                                    <FilterMenu EnableEmbeddedSkins="False" EnableTheming="True">
                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                    </FilterMenu>
                                </telerik:RadGrid>
                                <asp:HiddenField ID="hdnCheckIndexState" runat="server" />
                                <asp:HiddenField ID="hdnEditableModeState" runat="server" />
                            </asp:View>
                            <asp:View ID="vwCity" runat="server">
                                <telerik:RadGrid ID="radgrdCity" runat="server" AutoGenerateColumns="false" AllowMultiRowSelection="true"
                                    AllowMultiRowEdit="true" AllowPaging="true" AllowSorting="true" ClientSettings-EnableRowHoverStyle="true"
                                    EnableEmbeddedSkins="false" PageSize="<%$appSettings:GridPageSize %>" OnItemDataBound="radgrdCity_ItemDataBound"
                                    OnNeedDataSource="radgrdCity_NeedDataSource">
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="true"></Selecting>
                                        <ClientEvents OnRowClick="OnRowClick" />
                                    </ClientSettings>
                                    <HeaderContextMenu EnableEmbeddedSkins="False" EnableTheming="True">
                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                    </HeaderContextMenu>
                                    <MasterTableView AutoGenerateColumns="False" EditMode="InPlace" AllowSorting="True"
                                        PagerStyle-AlwaysVisible="true">
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridTemplateColumn>
                                                <ItemStyle CssClass="ItemAlign" Width="25px" />
                                                <HeaderStyle Width="25px" />
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeadWrT" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItemWrt" runat="server" AutoPostBack="false" OnCheckedChanged="chkItemWrtCity_CheckChanged" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Id" UniqueName="CITY_ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdCityIdItem" runat="server" Text='<%# Bind("CITY_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblGrdCityIdEdit" runat="server" Text='<%# Bind("CITY_ID") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="City Name" UniqueName="CITY_NAME" SortExpression="CITY_NAME">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdCityName" runat="server" Title="Click here to sort"
                                                        CommandName='Sort' CommandArgument='STATE_NAME' />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdCityName" runat="server" Text='<%# Bind("CITY_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtGrdCityName" runat="server" Text='<%# Bind("CITY_NAME") %>' MaxLength="50"></asp:TextBox>
                                                    <span class="error">*</span>
                                                    <asp:RequiredFieldValidator ID="requireGrdCityName" runat="server" ControlToValidate="txtGrdCityName"
                                                        ValidationGroup="ReqGeoLocation" Display="Dynamic" ErrorMessage="Enter Valid City Name"
                                                        CssClass="error"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="State Name" UniqueName="STATE_NAME" SortExpression="STATE_NAME">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdStateName" runat="server" Title="Click here to sort"
                                                        CommandName='Sort' CommandArgument='STATE_NAME' />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdStateName" runat="server" Text='<%# Bind("STATE_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox ID="cmbGrdStateName" runat="server">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="" Value="0" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <%--<asp:Label ID="lblGrdStateName" runat="server" Text='<%# Bind("STATE_NAME") %>'></asp:Label>--%>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Country Name" UniqueName="COUNTRY_NAME" SortExpression="COUNTRY_NAME">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdCountryName" runat="server" Title="Click here to sort"
                                                        CommandName='Sort' CommandArgument='COUNTRY_NAME' />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdCountryName" runat="server" Text='<%# Bind("COUNTRY_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox ID="cmbGrdCountryName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbGrdCountryName_SelectedIndexChanged">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="" Value="0" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <%--<asp:Label ID="lblGrdCountryName" runat="server" Text='<%# Bind("COUNTRY_NAME") %>'></asp:Label>--%>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Created By" UniqueName="CREATED_BY" SortExpression="CREATED_BY">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdCreatedBy" runat="server" Title="Click here to Sort"
                                                        CommandName='Sort' CommandArgument='CREATED_BY' />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdCreatedByItem" runat="server" Text='<%# Bind("CREATED_BY") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblGrdCreatedByEdit" runat="server" Text='<%# Bind("CREATED_BY") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Date Created" UniqueName="DATE_CREATED" SortExpression="DATE_CREATED">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdDateCreated" runat="server" Title="Click here to Sort"
                                                        CommandName='Sort' CommandArgument='DATE_CREATED' />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdDateCreatedItem" runat="server" Text='<%# Bind("DATE_CREATED") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblGrdDateCreatedEdit" runat="server" Text='<%# Bind("DATE_CREATED") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Modified By" UniqueName="MODIFIED_BY" SortExpression="MODIFIED_BY">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdModifiedBy" runat="server" Title="Click here to Sort"
                                                        CommandName='Sort' CommandArgument='MODIFIED_BY' />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdModifiedByItem" runat="server" Text='<%# Bind("MODIFIED_BY") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblGrdModifiedByEdit" runat="server" Text='<%# Bind("MODIFIED_BY") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Date Modified" UniqueName="DATE_MODIFIED"
                                                SortExpression="DATE_MODIFIED">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkbtnGrdDateModified" runat="server" Title="Click here to Sort"
                                                        CommandName='Sort' CommandArgument='DATE_MODIFIED' />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdDateModifiedItem" runat="server" Text='<%# Bind("DATE_MODIFIED") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblGrdDateModifiedEdit" runat="server" Text='<%# Bind("DATE_MODIFIED") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn CancelImageUrl="Cancel.gif" EditImageUrl="Edit.gif" InsertImageUrl="Update.gif"
                                                UpdateImageUrl="Update.gif">
                                            </EditColumn>
                                        </EditFormSettings>
                                        <PagerStyle AlwaysVisible="True" />
                                    </MasterTableView>
                                    <FilterMenu EnableEmbeddedSkins="False" EnableTheming="True">
                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                    </FilterMenu>
                                </telerik:RadGrid>
                                <asp:HiddenField ID="hdnCheckIndexCity" runat="server" />
                                <asp:HiddenField ID="hdnEditableModeCity" runat="server" />
                            </asp:View>
                        </asp:MultiView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbGeographicLocation" />
                        <asp:AsyncPostBackTrigger ControlID="rtabGeographicLocation" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
