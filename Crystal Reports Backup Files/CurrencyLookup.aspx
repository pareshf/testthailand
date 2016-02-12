<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="CurrencyLookup.aspx.cs" Inherits="CRM.WebApp.Views.Administration.CurrencyLookup" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Views/Shared/Controls/Navigation/ActionBar.ascx" TagName="ActionBar"
    TagPrefix="crmUC" %>
<asp:Content ID="cntIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        window.blockConfirm = function(text, mozEvent, oWidth, oHeight, callerObj, oCurrency) {
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
                radconfirm(text, callBackFn, oWidth, oHeight, callerObj, oCurrency);
            }
            return false;
        }

        function ramRequestStarted(ajaxManager, eventArgs) {
            //alert(eventArgs.EventTarget);
            if (eventArgs.EventTarget == "ctl00$cphPageContent$acbCurrency$imgExcelExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbCurrency$imgWordExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbCurrency$imgPdfExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbCurrency$imgCsvExport") {
                eventArgs.EnableAjax = false;
            }
        }
        
    </script>

    <script language="javascript" type="text/javascript">
        var selectionAlertMessage = '<%=ConfigurationManager.AppSettings["AtleastOneRecord"].ToString() %>';
        var deleteAlertMessage = '<%=ConfigurationManager.AppSettings["DeleteAlert"].ToString() %>';
    </script>

</asp:Content>
<asp:Content ID="cntPageContent" ContentPlaceHolderID="cphPageContent" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="ramRequestStarted" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbCurrency$imgExcelExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbCurrency$imgWordExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbCurrency$imgPdfExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbCurrency$imgCsvExport">
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
        <asp:Literal ID="lblPageTitle" runat="server" Text="Currency"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="upActionBar" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <crmUC:ActionBar ID="acbCurrency" runat="server" SaveButtonValidationGroup="ReqCurrency"
                SaveNewButtonValidationGroup="ReqCurrency" OnbtnCancelClick="acbCurrency_CancelClick"
                OnbtnNewClick="acbCurrency_NewClick" OnbtnSaveNewClick="acbCurrency_SaveNewClick"
                OnbtnSaveClick="acbCurrency_SaveClick" OnbtnDeleteClick="acbCurrency_DeleteClick"
                OnbtnSearchClick="acbCurrency_SearchClick" OnbtnEditClick="acbCurrency_EditClick"
                OnimgExcelExport_Click="acbCurrency_ExcelExportClick" OnimgCsvExport_Click="acbCurrency_CsvExportClick"
                OnimgPdfExport_Click="acbCurrency_PdfExportClick" OnimgWordExport_Click="acbCurrency_WordExportClick"
                OnbtnRefreshClick="acbCurrency_RefreshClick" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="upAddNew" ru nat="server" U pdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAddNewMode" runat="server" Visible="false">
                            <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                                <tr>
                                    <th>
                                        <asp:Literal runat="server" ID="ltlTableHeader" Text="New Currency"></asp:Literal>
                                    </th>
                                    <th>
                                        <div class="MandatoryNote" align="right">
                                            <asp:Literal ID="ltlMandatoryNote" runat="server">Fields marked with <span class="error">*</span> are mandatory.</asp:Literal>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <td style="width: 120px">
                                        <asp:Label ID="lblCurrency" runat="server" Text="Currency Name:"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCurrency" runat="server" ToolTip="Add Currency Name" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="requireCurrencyName" runat="server" ControlToValidate="txtCurrency"
                                            ValidationGroup="ReqCurrency" Display="Dynamic" ErrorMessage="<br />Enter Valid Currency Name"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 120px">
                                        <asp:Label ID="lblCurrencySymbol" runat="server" Text="Currency Symbol:"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCurrencySymbol" runat="server" ToolTip="Add Currency Symbol"
                                            MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="requireCurrencySymbol" runat="server" ControlToValidate="txtCurrencySymbol"
                                            ValidationGroup="ReqCurrency" Display="Dynamic" ErrorMessage="<br />Enter Valid Currency Symbol"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbCurrency" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlGrid" runat="server">
                            <telerik:RadGrid ID="radgrdCurrency" runat="server" PageSize="<%$appSettings:GridPageSize %>"
                                Width="100%" AllowMultiRowEdit="true" OnPreRender="radgrdCurrency_PreRender"
                                OnItemDataBound="radgrdCurrency_ItemDataBound" OnNeedDataSource="radgrdCurrency_NeedDataSource">
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
                                        <telerik:GridTemplateColumn UniqueName="chkRoomType">
                                            <ItemStyle CssClass="ItemAlign" Width="25px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="25px" />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeadWrT" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkItemWrt" runat="server" AutoPostBack="false" OnCheckedChanged="chkItemWrt_CheckChanged" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Id" UniqueName="CURRENCY_ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrdCurrencyIdItem" runat="server" Text='<%# Bind("CURRENCY_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblgrdCurrencyEdit" runat="server" Text='<%# Bind("CURRENCY_ID") %>'></asp:Label>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Currency Name" UniqueName="CURRENCY_NAME"
                                            SortExpression="CURRENCY_NAME">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkbtngrdCurrencyName" runat="server" Title="Click here to Sort"
                                                    CommandName='Sort' CommandArgument='CURRENCY_NAME' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrdCurrencyName" runat="server" Text='<%# Bind("CURRENCY_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgrdCurrencyName" runat="server" Text='<%# Bind("CURRENCY_NAME") %>'
                                                    MaxLength="50"></asp:TextBox>
                                                <span class="error">*</span>
                                                <asp:RequiredFieldValidator ID="requireCurrencyName" runat="server" ValidationGroup="ReqCurrency"
                                                    Display="Dynamic" ErrorMessage="<br />Enter Valid Currency Name" CssClass="error"
                                                    ControlToValidate="txtgrdCurrencyName"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Currency Symbol" UniqueName="CURRENCY_SYMBOL"
                                            SortExpression="CURRENCY_SYMBOL">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkbtngrdCurrencySymbol" runat="server" Title="Click here to Sort"
                                                    CommandName='Sort' CommandArgument='CURRENCY_SYMBOL' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrdCurrencySymbol" runat="server" Text='<%# Bind("CURRENCY_SYMBOL") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgrdCurrencySymbol" runat="server" Text='<%# Bind("CURRENCY_SYMBOL") %>'
                                                    MaxLength="2" Width="50px"></asp:TextBox>
                                                <span class="error">*</span>
                                                <asp:RequiredFieldValidator ID="requireCurrencySymbol" runat="server" ValidationGroup="ReqCurrency"
                                                    Display="Dynamic" ErrorMessage="<br />Enter Valid Currency Symbol" CssClass="error"
                                                    ControlToValidate="txtgrdCurrencySymbol"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Created By" UniqueName="CREATED_BY" SortExpression="CREATED_BY">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkbtngrdCreatedBy" runat="server" Title="Click here to Sort"
                                                    CommandName='Sort' CommandArgument='CREATED_BY' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrdCreatedDateItem" runat="server" Text='<%# Bind("CREATED_BY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblGrdCreatedDateEdit" runat="server" Text='<%# Bind("CREATED_BY") %>'></asp:Label>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Date Created" UniqueName="DATE_CREATED" SortExpression="DATE_CREATED">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrdDateItem" runat="server" Text='<%# Bind("DATE_CREATED") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblGrdDateCreatedEdit" runat="server" Text='<%# Bind("DATE_CREATED") %>'></asp:Label>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Modified By" UniqueName="MODIFIED_BY" SortExpression="MODIFIED_BY">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="85px" />
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkbtngrdModifiedBy" runat="server" Title="Click here to Sort"
                                                    CommandName='Sort' CommandArgument='MODIFIED_BY' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrdModifiedItem" runat="server" Text='<%# Bind("MODIFIED_BY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblGrdModifiedEdit" runat="server" Text='<%# Bind("MODIFIED_BY") %>'></asp:Label>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Date Modified" UniqueName="DATE_MODIFIED"
                                            SortExpression="DATE_MODIFIED">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkbtngrdDateModified" runat="server" Title="Click here to Sort"
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
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:Panel>
                        <div>
                            <asp:HiddenField ID="hdnCheckIndex" runat="server" />
                            <asp:HiddenField ID="hdnEditableMode" runat="server" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbCurrency" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
