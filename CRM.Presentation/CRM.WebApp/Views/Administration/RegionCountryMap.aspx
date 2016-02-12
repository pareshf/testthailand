<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="RegionCountryMap.aspx.cs" Inherits="CRM.WebApp.Views.Administration.RegionCountryMap" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Views/Shared/Controls/Navigation/ActionBar.ascx" TagName="ActionBar"
    TagPrefix="crmUC" %>
<%@ Register Src="~/Views/Shared/Controls/Navigation/ControlBox.ascx" TagName="ControlBox"
    TagPrefix="crmUC" %>
<asp:Content ID="cntIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../Shared/Javascripts/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        window.blockConfirm = function(text, mozEvent, oWidth, oHeight, callerObj, oGDSCode) {
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
                radconfirm(text, callBackFn, oWidth, oHeight, callerObj, oGDSCode);
            }
            return false;
        }

        function ramRequestStarted(ajaxManager, eventArgs) {
            //alert(eventArgs.EventTarget);
            if (eventArgs.EventTarget == "ctl00$cphPageContent$acbGDSCode$imgExcelExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbGDSCode$imgWordExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbGDSCode$imgPdfExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbGDSCode$imgCsvExport") {
                eventArgs.EnableAjax = false;
            }
        }

        
    </script>

    <script language="javascript" type="text/javascript">
        var selectionAlertMessage = '<%=ConfigurationManager.AppSettings["AtleastOneRecord"].ToString() %>';
        var deleteAlertMessage = '<%=ConfigurationManager.AppSettings["DeleteAlert"].ToString() %>';



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
        function ValidateDelete1(gridControlId) {
            blockConfirm(deleteAlertMessage, event, 300, 110, null, 'Delete Confirmation');
        }

 
        
    </script>

</asp:Content>
<asp:Content ID="cntPageContent" ContentPlaceHolderID="cphPageContent" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="ramRequestStarted" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbGDSCode$imgExcelExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbGDSCode$imgWordExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbGDSCode$imgPdfExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbGDSCode$imgCsvExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="UpvwContact" />
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
        <asp:Literal ID="lblPageGDSCOde" runat="server" Text="Country-Region Map"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="upActionBar" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <crmUC:ActionBar ID="acbGDSCode" runat="server" SaveButtonValidationGroup="ReqGDSCode"
                SaveNewButtonValidationGroup="ReqGDSCode" OnbtnNewClick="acbGDSCode_NewClick"
                OnbtnSaveClick="acbGDSCode_SaveClick" OnbtnCancelClick="acbGDSCode_CancelClick"
                OnbtnSaveNewClick="acbGDSCode_SaveNewClick" OnbtnDeleteClick="acbGDSCode_DeleteClick"
                OnbtnSearchClick="acbGDSCode_SearchClick" OnbtnRefreshClick="acbGDSCode_RefreshClick"
                OnbtnEditClick="acbGDSCode_EditClick" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="100%" class="TableForm" cellspacing="0" cellpadding="4" border="0">
        <tr>
            <td>
                <asp:UpdatePanel ID="upAddNew" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAddNewMode" runat="server" Visible="false">
                            <table width="100%" class="TableForm" cellspacing="0" cellpadding="4" border="0">
                                <tr>
                                    <th>
                                        <asp:Literal runat="server" ID="Airport" Text="New Map"></asp:Literal>
                                    </th>
                                    <th>
                                        <div class="MandatoryNote" align="right">
                                            <asp:Literal ID="ltlMandatoryNote" runat="server">Fields marked with <span class="error">*</span> are mandatory.</asp:Literal>
                                        </div>
                                    </th>
                                </tr>
                                <tr style="padding: 0px 0px 0px 0px;" valign="top">
                                    <td>
                                        <table border="0" class="SubTableForm" cellpadding="4">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Region:" >
                                                    </asp:Label>
                                                    <span class="error">*</span>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="radAirline" runat="server">
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radAirline"
                                                        ValidationGroup="ReqGDSCode" Display="Dynamic" ErrorMessage="<br />Select Region"
                                                        CssClass="error"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAirline" runat="server" Text="Country:"></asp:Label>
                                                    <span class="error">*</span>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="radCmbDestination" runat="server">
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="requireAirline" runat="server" ControlToValidate="radCmbDestination"
                                                        ValidationGroup="ReqGDSCode" Display="Dynamic" ErrorMessage="<br />Select Country"
                                                        CssClass="error"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbGDSCode" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlGrid" runat="server">
                            <telerik:RadGrid ID="radgrdGDSCode" runat="server" Width="100%" AllowMultiRowEdit="true"
                                OnPreRender="radgrdGDSCode_PreRender" OnItemDataBound="radgrdGDSCode_ItemDataBound"
                                OnNeedDataSource="radgrdGDSCode_NeedDataSource">
                                <ClientSettings>
                                    <Selecting AllowRowSelect="false"></Selecting>
                                </ClientSettings>
                                <HeaderContextMenu EnableEmbeddedSkins="False" EnableTheming="True">
                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                </HeaderContextMenu>
                                <MasterTableView AutoGenerateColumns="False" AllowSorting="false" PagerStyle-AlwaysVisible="true">
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
                                        <telerik:GridTemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDestinationCity" runat="server" Text='<%#Bind("COUNTRY_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGDSCodeItem" runat="server" Text='<%#Bind("SR_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAirLineId" runat="server" Text='<%#Bind("REGION_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="REGION_LONG_NAME" UniqueName="REGION_LONG_NAME"
                                            HeaderText="Region"><HeaderStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="COUNTRY_NAME" UniqueName="COUNTRY_NAME" HeaderText="Country"><HeaderStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <PagerStyle AlwaysVisible="True" />
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:Panel>
                        <div>
                            <asp:HiddenField ID="hdnCheckIndex" runat="server" />
                            <asp:HiddenField ID="hdnEditableMode" runat="server" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbGDSCode" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
