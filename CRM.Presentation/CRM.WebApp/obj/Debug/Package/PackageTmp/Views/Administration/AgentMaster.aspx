<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="AgentMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.AgentMaster" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Views/Shared/Controls/Navigation/ActionBar.ascx" TagName="ActionBar"
    TagPrefix="crmUC" %>
<asp:Content ID="cntIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">

        window.blockConfirm = function(text, mozEvent, oWidth, oHeight, callerObj, oAgent) {
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
                radconfirm(text, callBackFn, oWidth, oHeight, callerObj, oAgent);
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
        var selectionAlertMessage = '<%=ConfigurationManager.AppSettings["AtleastOneRecord"].ToString() %>';
        var deleteAlertMessage = '<%=ConfigurationManager.AppSettings["DeleteAlert"].ToString() %>';
    </script>

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
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$Aacbgent$imgPdfExport">
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
        <asp:Literal ID="lblPageTitle" runat="server" Text="Agent"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="upActionBar" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <crmUC:ActionBar ID="acbAgent" runat="server" SaveButtonValidationGroup="ReqAgent"
                SaveNewButtonValidationGroup="ReqAgent" OnbtnCancelClick="acbAgent_CancelClick"
                OnbtnNewClick="acbAgent_NewClick" OnbtnSaveNewClick="acbAgent_SaveNewClick" OnbtnSaveClick="acbAgent_SaveClick"
                OnbtnDeleteClick="acbAgent_DeleteClick" OnbtnSearchClick="acbAgent_SearchClick"
                OnbtnEditClick="acbAgent_EditClick" OnbtnRefreshClick="acbAgent_RefreshClick"
                OnimgCsvExport_Click="acbAgent_CsvExportClick" OnimgPdfExport_Click="acbAgent_PdfExportClick"
                OnimgWordExport_Click="acbAgent_WordExportClick" OnimgExcelExport_Click="acbAgent_ExcelExportClick" />
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
                                        <asp:Literal runat="server" ID="ltlTableHeader" Text="New Agent"></asp:Literal>
                                    </th>
                                    <th>
                                        <div class="MandatoryNote" align="right">
                                            <asp:Literal ID="ltlMandatoryNote" runat="server">Fields marked with <span class="error">*</span> are mandatory.</asp:Literal>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <td style="width: 120px">
                                        <asp:Label ID="lblAgentName" runat="server" Text="Agent Name:"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAgentName" runat="server" ToolTip="Add Agent Name" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="requireAgentName" runat="server" ControlToValidate="txtAgentName"
                                            ValidationGroup="ReqAgent" Display="Dynamic" ErrorMessage="<br />Enter Valid Agent Name"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 120px">
                                        <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail" runat="server" ToolTip="Add Email" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                                            ValidationGroup="ReqAgent" Display="Dynamic" ErrorMessage="<br />Enter Valid Email"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 120px">
                                        <asp:Label ID="lblMobile" runat="server" Text="Mobile:"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMobile" runat="server" ToolTip="Add Mobile No" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMobile"
                                            ValidationGroup="ReqAgent" Display="Dynamic" ErrorMessage="<br />Enter Valid Mobile No"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 120px">
                                        <asp:Label ID="lblPhone" runat="server" Text="Phone:"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPhone" runat="server" ToolTip="Add Phone No" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPhone"
                                            ValidationGroup="ReqAgent" Display="Dynamic" ErrorMessage="<br />Enter Valid Mobile No"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 120px">
                                        <asp:Label ID="lblFax" runat="server" Text="Fax:"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFax" runat="server" ToolTip="Add Fax No" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFax"
                                            ValidationGroup="ReqAgent" Display="Dynamic" ErrorMessage="<br />Enter Valid Fax No"
                                            CssClass="error"></asp:RequiredFieldValidator>
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
                            <telerik:RadGrid ID="radgrdAgent" runat="server" PageSize="<%$appSettings:GridPageSize %>"
                                Width="100%" AllowMultiRowEdit="true" OnPreRender="radgrdAgent_PreRender" OnItemDataBound="radgrdAgent_ItemDataBound"
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
                                        <telerik:GridTemplateColumn UniqueName="chkAgent">
                                            <ItemStyle CssClass="ItemAlign" Width="25px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="25px" />
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
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkbtngrdAgentName" runat="server" Title="Click here to Sort"
                                                    CommandName='Sort' CommandArgument='AGENT_NAME' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrdAgentName" runat="server" Text='<%# Bind("AGENT_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgrdAgentName" runat="server" Text='<%# Bind("AGENT_NAME") %>'
                                                    MaxLength="50"></asp:TextBox>
                                                <span class="error">*</span>
                                                <asp:RequiredFieldValidator ID="requireAgentName" runat="server" ValidationGroup="ReqAgent"
                                                    Display="Dynamic" ErrorMessage="<br />Enter Valid Name" CssClass="error" ControlToValidate="txtgrdAgentName"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="EMail" UniqueName="EMAIL" SortExpression="EMAIL">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkbtngrdEmail" runat="server" Title="Click here to Sort" CommandName='Sort'
                                                    CommandArgument='EMAIL' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrdEmail" runat="server" Text='<%# Bind("EMAIL") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgrdEmail" runat="server" Text='<%# Bind("EMAIL") %>'></asp:TextBox>
                                                <span class="error">*</span>
                                                <asp:RequiredFieldValidator ID="requireEmail" runat="server" ValidationGroup="ReqAgent"
                                                    Display="Dynamic" ErrorMessage="<br />Enter Valid Email" CssClass="error" ControlToValidate="txtgrdEmail"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Mobile" UniqueName="MOBILE" SortExpression="MOBILE">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkbtngrdMobile" runat="server" Title="Click here to Sort" CommandName='Sort'
                                                    CommandArgument='Mobile' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrdMobile" runat="server" Text='<%# Bind("MOBILE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgrdMobile" runat="server" Text='<%# Bind("MOBILE") %>' MaxLength="13"
                                                    Width="100px"> </asp:TextBox>
                                                <span class="error">*</span>
                                                <asp:RequiredFieldValidator ID="requireMobile" runat="server" ValidationGroup="ReqAgent"
                                                    Display="Dynamic" ErrorMessage="<br />Enter Valid Mobile" CssClass="error" ControlToValidate="txtgrdMobile"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Phone" UniqueName="PHONE" SortExpression="PHONE">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkbtngrdPhone" runat="server" Title="Click here to Sort" CommandName='Sort'
                                                    CommandArgument='PHONE' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrdPhone" runat="server" Text='<%# Bind("PHONE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgrdPhone" runat="server" Text='<%# Bind("PHONE") %>' Width="100px"></asp:TextBox>
                                                <span class="error">*</span>
                                                <asp:RequiredFieldValidator ID="requirePhone" runat="server" ValidationGroup="ReqAgent"
                                                    Display="Dynamic" ErrorMessage="<br />Enter Valid Phone" CssClass="error" ControlToValidate="txtgrdPhone"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Fax" UniqueName="FAX" SortExpression="FAX">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkbtngrdFax" runat="server" Title="Click here to Sort" CommandName='Sort'
                                                    CommandArgument='FAX' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrdFax" runat="server" Text='<%# Bind("FAX") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgrdFax" runat="server" Text='<%# Bind("FAX") %>' Width="100px"></asp:TextBox>
                                                <span class="error">*</span>
                                                <asp:RequiredFieldValidator ID="requireFax" runat="server" ValidationGroup="ReqAgent"
                                                    Display="Dynamic" ErrorMessage="<br />Enter Valid Fax" CssClass="error" ControlToValidate="txtgrdFax"></asp:RequiredFieldValidator>
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
                        <asp:AsyncPostBackTrigger ControlID="acbAgent" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
