<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    CodeBehind="ProductLookup.aspx.cs" Inherits="CRM.WebApp.Views.Administration.ProductLookup"
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
            if (eventArgs.EventTarget == "ctl00$cphPageContent$acbProduct$imgExcelExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbProduct$imgWordExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbProduct$imgPdfExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbProduct$imgCsvExport") {
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
        <clientevents onrequeststart="ramRequestStarted" />
        <ajaxsettings>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbProduct$imgExcelExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbProduct$imgWordExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbProduct$imgPdfExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbProduct$imgCsvExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </ajaxsettings>
    </telerik:RadAjaxManager>
    <telerik:RadWindowManager ID="RadWindowManager" runat="server" Style="z-index: 100100">
        <confirmtemplate>
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
        </confirmtemplate>
    </telerik:RadWindowManager>
    <div class="pageTitle">
        <asp:Literal ID="lblPageTitle" runat="server" Text="Product"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="upActionBar" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <crmUC:ActionBar ID="acbProduct" runat="server" OnbtnNewClick="acbProduct_NewClick"
                OnbtnSaveClick="acbProduct_SaveClick" OnbtnCancelClick="acbProduct_CancelClick"
                OnbtnDeleteClick="acbProduct_DeleteClick" OnbtnEditClick="acbProduct_EditClick"
                OnbtnSearchClick="acbProduct_SearchClick" SaveButtonValidationGroup="ReqProductDesc"
                OnbtnSaveNewClick="acbProduct_SaveNewClick" SaveNewButtonValidationGroup="ReqProductDesc"
                OnimgExcelExport_Click="acbProduct_ExcelExportClick" OnimgCsvExport_Click="acbProduct_CsvExportClick"
                OnimgPdfExport_Click="acbProduct_PdfExportClick" OnimgWordExport_Click="acbProduct_WordExportClick" />
            <table width="75%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="upAddNew" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel ID="pnlAddNewMode" runat="server" Visible="false">
                                    <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                                        <tr>
                                            <th>
                                                <asp:Literal runat="server" ID="ltlTableHeader" Text="New Product"></asp:Literal>
                                            </th>
                                            <th>
                                                <div class="MandatoryNote" align="right" style="margin-top: 3px; margin-bottom: 3px">
                                                    <asp:Literal ID="ltlMandatoryNote" runat="server">Fields marked with <span class="error">*</span> are mandatory.&nbsp;</asp:Literal>
                                                </div>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="width: 120px">
                                                <asp:Label ID="lblProductDesc" runat="server" Text="Product :"></asp:Label>
                                                <span class="error">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtProductDesc" runat="server" ValidationGroup="ReqProductDesc"
                                                    MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="requireProductDesc" runat="server" ControlToValidate="txtProductDesc"
                                                    ValidationGroup="ReqProductDesc" Display="Dynamic" ErrorMessage="<br />Enter Valid Product Desc"
                                                    CssClass="error"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="acbProduct" />
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
                                                <telerik:RadGrid ID="radgrdProduct" runat="server" AutoGenerateColumns="false" AllowMultiRowSelection="true"
                                                    AllowMultiRowEdit="true" AllowPaging="true" AllowSorting="true" ClientSettings-EnableRowHoverStyle="true"
                                                    EnableEmbeddedSkins="false" OnPreRender="radgrdProduct_PreRender" OnItemDataBound="radgrdProduct_ItemDataBound"
                                                    OnNeedDataSource="radgrdProduct_NeedDataSource" PageSize="<%$appSettings:GridPageSize %>"
                                                    Width="100%">
                                                    <clientsettings>
                                                        <Selecting AllowRowSelect="true"></Selecting>
                                                        <ClientEvents OnRowClick="OnRowClick" />
                                                    </clientsettings>
                                                    <headercontextmenu enableembeddedskins="False" enabletheming="True">
                                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                                    </headercontextmenu>
                                                    <mastertableview autogeneratecolumns="False" editmode="InPlace" allowsorting="True"
                                                        pagerstyle-alwaysvisible="true">
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
                                                            <telerik:GridTemplateColumn HeaderText="Id" UniqueName="PRODUCT_ID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGrdProductIdItem" runat="server" Text='<%# Bind("PRODUCT_ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblGrdProductIdEdit" runat="server" Text='<%# Bind("PRODUCT_ID") %>'></asp:Label>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Product" UniqueName="PRODUCT_DESC" SortExpression="PRODUCT_DESC">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnkbtnGrdProductDesc" runat="server" Title="Click here to Sort"
                                                                        CommandName='Sort' CommandArgument='PRODUCT_DESC' />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGrdProductDesc" runat="server" Text='<%# Bind("PRODUCT_DESC") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtGrdProductDesc" runat="server" Text='<%# Bind("PRODUCT_DESC") %>'
                                                                        MaxLength="50"></asp:TextBox>
                                                                    <span class="error">*</span>
                                                                    <asp:RequiredFieldValidator ID="requireProductDesc" runat="server" ControlToValidate="txtGrdProductDesc"
                                                                        ValidationGroup="ReqProductDesc" Display="Dynamic" ErrorMessage="<br />Enter Valid Product Desc"
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
                                                    </mastertableview>
                                                    <filtermenu enableembeddedskins="False" enabletheming="True">
                                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                                    </filtermenu>
                                                </telerik:RadGrid>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:HiddenField ID="hdnCheckIndex" runat="server" />
                                <asp:HiddenField ID="hdnEditableMode" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="acbProduct" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
