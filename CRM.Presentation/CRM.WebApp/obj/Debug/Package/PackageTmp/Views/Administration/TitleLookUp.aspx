<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    CodeBehind="TitleLookUp.aspx.cs" Inherits="CRM.WebApp.Views.Administration.TitleLookUp"
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
            if (eventArgs.EventTarget == "ctl00$cphPageContent$acbTitle$imgExcelExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbTitle$imgWordExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbTitle$imgPdfExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbTitle$imgCsvExport") {
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
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbAddress$imgExcelExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbAddress$imgWordExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbAddress$imgPdfExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbAddress$imgCsvExport">
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
        <asp:Literal ID="lblPageTitle" runat="server" Text="Title"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="upActionBar" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <crmUC:ActionBar ID="acbTitle" runat="server" OnbtnSaveClick="acbTitle_SaveClick"
                OnbtnCancelClick="acbTitle_CancelClick" OnbtnNewClick="acbTitle_NewClick" OnbtnDeleteClick="acbTitle_DeleteClick"
                OnbtnEditClick="acbTitle_EditClick" OnbtnSearchClick="acbTitle_SearchClick" SaveButtonValidationGroup="ReqTitleName"
                OnbtnSaveNewClick="acbTitle_SaveNewClick" SaveNewButtonValidationGroup="ReqTitleName"
                OnimgExcelExport_Click="acbTitle_ExcelExportClick" OnimgCsvExport_Click="acbTitle_CsvExportClick"
                OnimgPdfExport_Click="acbTitle_PdfExportClick" OnimgWordExport_Click="acbTitle_WordExportClick"
                OnbtnRefreshClick="acbTitle_RefreshClick" />
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
                                        <asp:Literal runat="server" ID="ltlTableHeader" Text="New Title"></asp:Literal>
                                    </th>
                                    <th>
                                        <div class="MandatoryNote" align="right">
                                            <asp:Literal ID="ltlMandatoryNote" runat="server">Fields marked with <span class="error">*</span> are mandatory.</asp:Literal>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <td style="width: 70px">
                                        <asp:Label ID="lblTitleDesc" runat="server" Text="Title :"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTitleDesc" runat="server" ToolTip="Add Title Desc" Width="<%$appSettings:TextBoxWidth %>"
                                            MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="requireTitleName" runat="server" ControlToValidate="txtTitleDesc"
                                            ValidationGroup="ReqTitleName" Display="Dynamic" ErrorMessage="<br />Enter Valid Title"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbTitle" />
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
                                        <telerik:RadGrid ID="radgrdTitle" runat="server" PageSize="<%$appSettings:GridPageSize %>"
                                            OnPreRender="radgrdTitle_PreRender" OnItemDataBound="radgrdTitle_ItemDataBound"
                                            OnNeedDataSource="radgrdTitle_NeedDataSource" Width="100%" AllowMultiRowEdit="true">
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
                                                    <HeaderStyle HorizontalAlign ="Center" Width="7%" />
                                                        <ItemStyle CssClass="ItemAlign" Width="25px" />                                                      
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkHeadWrT" runat="server" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkItemWrt" runat="server" AutoPostBack="false" OnCheckedChanged="chkItemWrt_CheckChanged" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Id" UniqueName="TITLE_ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdTitleIdItem" runat="server" Text='<%# Bind("TITLE_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblgrdTitleIdEdit" runat="server" Text='<%# Bind("TITLE_ID") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Title Desc" UniqueName="TITLE_DESC" ShowSortIcon="true"
                                                        SortExpression="TITLE_DESC">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkbtngrdTitleDesc" runat="server" Title="Click here to Sort"
                                                                CommandName='Sort' CommandArgument='TITLE_DESC' />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrd" runat="server" Text='<%# Bind("TITLE_DESC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgrdTitleDesc" runat="server" Text='<%# Bind("TITLE_DESC") %>'
                                                                Width="100px"></asp:TextBox>
                                                            <span class="error">*</span>
                                                            <asp:RequiredFieldValidator ID="requireTitleName" runat="server" ValidationGroup="ReqTitleName"
                                                                Display="Dynamic" ErrorMessage="Enter Valid Title" CssClass="error" ControlToValidate="txtgrdTitleDesc"></asp:RequiredFieldValidator>
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
                                                            <asp:LinkButton ID="lnkbtngrdDateCreated" runat="server" Title="Click here to Sort"
                                                                CommandName='Sort' CommandArgument='DATE_CREATED' />
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
                        <asp:AsyncPostBackTrigger ControlID="acbTitle" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
