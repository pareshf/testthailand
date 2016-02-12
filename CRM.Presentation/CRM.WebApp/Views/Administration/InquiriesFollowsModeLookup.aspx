<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    CodeBehind="InquiriesFollowsModeLookup.aspx.cs" Inherits="CRM.WebApp.Views.Administration.InquiriesFollowsModeLookup"
    Title="Untitled Page" %>

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
            if (eventArgs.EventTarget == "ctl00$cphPageContent$acbInquiriesFollowsMode$imgExcelExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbInquiriesFollowsMode$imgWordExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbInquiriesFollowsMode$imgPdfExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbInquiriesFollowsMode$imgCsvExport") {
                eventArgs.EnableAjax = false;               
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        var selectionAlertMessage ='<%=ConfigurationManager.AppSettings["AtleastOneRecord"].ToString() %>';
        var deleteAlertMessage ='<%=ConfigurationManager.AppSettings["DeleteAlert"].ToString() %>';
    </script>

    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="cntPageContent" ContentPlaceHolderID="cphPageContent" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="ramRequestStarted" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbInquiriesFollowsMode$imgExcelExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbInquiriesFollowsMode$imgWordExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbInquiriesFollowsMode$imgPdfExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbInquiriesFollowsMode$imgCsvExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadWindowManager ID="RadWindowManager" runat="server" Style="z-index: 100100">
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
        <asp:Literal ID="lblPageTitle" runat="server" Text="Inquiry FollowUp Mode"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="upActionBar" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <crmUC:ActionBar ID="acbInquiriesFollowsMode" runat="server" OnbtnNewClick="acbInquiriesFollowsMode_NewClick"
                OnbtnSaveClick="acbInquiriesFollowsMode_SaveClick" OnbtnCancelClick="acbInquiriesFollowsMode_CancelClick"
                OnbtnDeleteClick="acbInquiriesFollowsMode_DeleteClick" OnbtnEditClick="acbInquiriesFollowsMode_EditClick"
                OnbtnSearchClick="acbInquiriesFollowsMode_SearchClick" SaveButtonValidationGroup="ReqacbInquiriesFollowsMode"
                OnbtnSaveNewClick="acbacbInquiriesFollowsMode_SaveNewClick" SaveNewButtonValidationGroup="ReqacbInquiriesFollowsMode"
                OnimgExcelExport_Click="acbacbInquiriesFollowsMode_ExcelExportClick" OnimgCsvExport_Click="acbacbInquiriesFollowsMode_CsvExportClick"
                OnimgPdfExport_Click="acbacbInquiriesFollowsMode_PdfExportClick" OnimgWordExport_Click="acbacbInquiriesFollowsMode_WordExportClick"
                OnbtnRefreshClick="acbacbInquiriesFollowsMode_RefreshClick" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="80%">
        <tr>
            <td>
                <asp:UpdatePanel ID="upAddNew" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAddNewMode" runat="server" Visible="false">
                            <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                                <tr>
                                    <th>
                                        <asp:Literal runat="server" ID="ltlTableHeader" Text="New Inquiry FollowUp Mode"></asp:Literal>
                                    </th>
                                    <th>
                                        <div class="MandatoryNote" align="right" style="margin-top: 3px; margin-bottom: 3px">
                                            <asp:Literal ID="ltlMandatoryNote" runat="server">Fields marked with <span class="error">*</span> are mandatory.&nbsp;</asp:Literal>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <td style="width: 160px">
                                        <asp:Label ID="lblInquiriesFollowUp" runat="server" Text="Inquiry FollowUp Mode:"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInquiriesFollowsMode" runat="server" ValidationGroup="ReqacbInquiriesFollowsMode"
                                            Width="<%$appSettings:TextBoxWidth %>" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="requireInquiriesFollowsMode" runat="server" ControlToValidate="txtInquiriesFollowsMode"
                                            ValidationGroup="ReqacbInquiriesFollowsMode" Display="Dynamic" ErrorMessage="<br />Enter Valid InquiriesFollowsMode"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbInquiriesFollowsMode" />
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
                                        <telerik:RadGrid ID="radgrdInquiriesFollowsMode" runat="server" AutoGenerateColumns="false"
                                            AllowMultiRowSelection="true" AllowMultiRowEdit="true" AllowPaging="true" AllowSorting="true"
                                            ClientSettings-EnableRowHoverStyle="true" EnableEmbeddedSkins="false" OnPreRender="radgrdInquiriesFollowsMode_PreRender"
                                            OnItemDataBound="radgrdInquiriesFollowsMode_ItemDataBound" OnNeedDataSource="radgrdInquiriesFollowsMode_NeedDataSource"
                                            PageSize="<%$appSettings:GridPageSize %>" Width="100%">
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
                                                            <asp:CheckBox ID="chkItemWrt" runat="server" AutoPostBack="false" OnCheckedChanged="chkItemWrt_CheckChanged" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Id" UniqueName="IF_ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrdInqFollowsModeItem" runat="server" Text='<%# Bind("IF_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblGrdInqFollowsModeEdit" runat="server" Text='<%# Bind("IF_ID") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Inquiry FollowUp Mode" UniqueName="IF_DESC" SortExpression="IF_DESC">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lblGrdInqFollowsModeDescItem" runat="server" Title="Click here to Sort"
                                                                CommandName='Sort' CommandArgument='IF_DESC' />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrdInqFollowsModeDescItem" runat="server" Text='<%# Bind("IF_DESC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtGrdInqFollowsModeDescEdit" runat="server" Text='<%# Bind("IF_DESC") %>'
                                                                MaxLength="50"></asp:TextBox>
                                                            <span class="error">*</span>
                                                            <asp:RequiredFieldValidator ID="requireGrdInqFollowsModeDesc" runat="server" ControlToValidate="txtGrdInqFollowsModeDescEdit"
                                                                ValidationGroup="ReqacbInquiriesFollowsMode" Display="Dynamic" ErrorMessage="<br />Enter Valid InquiriesFollowsMode"
                                                                CssClass="error"></asp:RequiredFieldValidator>
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
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:HiddenField ID="hdnCheckIndex" runat="server" />
                        <asp:HiddenField ID="hdnEditableMode" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbInquiriesFollowsMode" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
