<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    CodeBehind="RelationLookUp.aspx.cs" Inherits="CRM.WebApp.Views.Administration.RelationLookUp"
    EnableEventValidation="false" Title="" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Views/Shared/Controls/Navigation/ActionBar.ascx" TagName="ActionBar"
    TagPrefix="crmUC" %>
<asp:Content ID="cntIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

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
            if (eventArgs.EventTarget == "ctl00$cphPageContent$acbRelation$imgExcelExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbRelation$imgWordExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbRelation$imgPdfExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbRelation$imgCsvExport") {
                eventArgs.EnableAjax = false;               
            }
            }
    </script>

    <script language="javascript" type="text/javascript">
        var selectionAlertMessage ='<%=ConfigurationManager.AppSettings["AtleastOneRecord"].ToString() %>';
        var deleteAlertMessage ='<%=ConfigurationManager.AppSettings["DeleteAlert"].ToString() %>';
    </script>

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
        <asp:Literal ID="lblPageTitle" runat="server" Text="Relation"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="upActionBar" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <crmUC:ActionBar ID="acbRelation" runat="server" OnbtnSaveClick="acbRelation_SaveClick"
                OnbtnCancelClick="acbRelation_CancelClick" OnbtnNewClick="acbRelation_NewClick"
                OnbtnEditClick="acbRelation_EditClick" OnbtnDeleteClick="acbRelation_DeleteClick"
                OnbtnSearchClick="acbRelation_SearchClick" OnbtnSaveNewClick="acbRelation_SaveNewClick"
                SaveButtonValidationGroup="ReqRelationName" SaveNewButtonValidationGroup="ReqRelationName"
                OnimgExcelExport_Click="acbRelation_ExcelExportClick" OnimgCsvExport_Click="acbRelation_CsvExportClick"
                OnimgPdfExport_Click="acbRelation_PdfExportClick" OnimgWordExport_Click="acbRelation_WordExportClick"
                OnbtnRefreshClick="acbRelation_RefreshClick" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="65%">
        <tr>
            <td>
                <asp:UpdatePanel ID="upAddNew" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAddNewMode" runat="server" Visible="false">
                            <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                                <tr>
                                    <th>
                                        <asp:Literal runat="server" ID="ltlTableHeader" Text="New Relation"></asp:Literal>
                                    </th>
                                    <th>
                                        <div class="MandatoryNote" align="right">
                                            <asp:Literal ID="ltlMandatoryNote" runat="server">Fields marked with <span class="error">*</span> are mandatory.</asp:Literal>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <td style="width: 70px">
                                        <asp:Label ID="lblRelationDesc" runat="server" Text="Relation :"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRelationDesc" runat="server" ToolTip="Add Relation Desc" Width="<%$appSettings:TextBoxWidth %>"
                                            MaxLength="25"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="requireRelationName" runat="server" ControlToValidate="txtRelationDesc"
                                            ValidationGroup="ReqRelationName" Display="Dynamic" ErrorMessage="<br />Enter Valid Relation"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbRelation" />
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
                                        <telerik:RadGrid ID="radgrdRelation" runat="server" OnPreRender="radgrdRelation_PreRender"
                                            OnItemDataBound="radgrdRelation_ItemDataBound" OnNeedDataSource="radgrdRelation_NeedDataSource"
                                            PageSize="<%$appSettings:GridPageSize %>" Width="100%" AllowMultiRowEdit="true">
                                            <ClientSettings>
                                                <Selecting AllowRowSelect="true"></Selecting>
                                                <ClientEvents OnRowClick="OnRowClick" />
                                            </ClientSettings>
                                            <MasterTableView AutoGenerateColumns="False" EditMode="InPlace" AllowSorting="True"
                                                PagerStyle-AlwaysVisible="true">
                                                <RowIndicatorColumn>
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
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
                                                    <telerik:GridTemplateColumn HeaderText="Id" UniqueName="RELATION_ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdRelationIdItem" runat="server" Text='<%# Bind("RELATION_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblgrdRelationIdEdit" runat="server" Text='<%# Bind("RELATION_ID") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Relation Desc" UniqueName="RELATION_DESC"
                                                        SortExpression="RELATION_DESC">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkbtngrdRelationDesc" runat="server" Title="Click here to Sort"
                                                                CommandName='Sort' CommandArgument='RELATION_DESC' />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrd" runat="server" Text='<%# Bind("RELATION_DESC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgrdRelationDesc" runat="server" Text='<%# Bind("RELATION_DESC") %>'></asp:TextBox>
                                                            <span class="error">*</span>
                                                            <asp:RequiredFieldValidator ID="requireRelationName" runat="server" ValidationGroup="ReqRelationName"
                                                                Display="Dynamic" ErrorMessage="Enter Valid Relation" CssClass="error" ControlToValidate="txtgrdRelationDesc"></asp:RequiredFieldValidator>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Created By" UniqueName="CREATED_BY" SortExpression="CREATED_BY">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkbtngrdCreatedBy" runat="server" CommandName='Sort' Title="Click here to Sort"
                                                                CommandArgument='CREATED_BY' />
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
                                                            <asp:LinkButton ID="lnkbtngrdDateCreated" runat="server" CommandName='Sort' Title="Click here to Sort"
                                                                CommandArgument='DATE_CREATED' />
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
                                                            <asp:LinkButton ID="lnkbtngrdModifiedBy" runat="server" CommandName='Sort' Title="Click here to Sort"
                                                                CommandArgument='MODIFIED_BY' />
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
                                                            <asp:LinkButton ID="lnkbtngrdDateModified" runat="server" CommandName='Sort' Title="Click here to Sort"
                                                                CommandArgument='DATE_MODIFIED' />
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
                        <asp:AsyncPostBackTrigger ControlID="acbRelation" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
