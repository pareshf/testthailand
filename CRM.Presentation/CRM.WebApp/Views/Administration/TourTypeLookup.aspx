<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    CodeBehind="TourTypeLookup.aspx.cs" Inherits="CRM.WebApp.Views.Administration.TourTypeLookup"
    Title="Tour Type" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Views/Shared/Controls/Navigation/ActionBar.ascx" TagName="ActionBar"
    TagPrefix="crmUC" %>
<asp:Content ID="cntIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
    
		window.blockConfirm = function(text, mozEvent, oWidth, oHeight, callerObj, oQualification) 
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
				radconfirm(text, callBackFn, oWidth, oHeight, callerObj, oQualification);
			}
			return false;
		} 
					  function ramRequestStarted(ajaxManager, eventArgs) {
            //alert(eventArgs.EventTarget);
            if (eventArgs.EventTarget == "ctl00$cphPageContent$acbTourType$imgExcelExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbTourType$imgWordExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbTourType$imgPdfExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbTourType$imgCsvExport") {
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
        <asp:Literal ID="lblPageTitle" runat="server" Text="Tour Type"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="upActionBar" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <crmUC:ActionBar ID="acbTourType" runat="server" SaveButtonValidationGroup="ReqTourType"
                SaveNewButtonValidationGroup="ReqTourType" OnbtnSaveClick="acbTourType_SaveClick"
                OnbtnCancelClick="acbTourType_CancelClick" OnbtnNewClick="acbTourType_NewClick"
                OnbtnSaveNewClick="acbTourType_SaveNewClick" OnbtnDeleteClick="acbTourType_DeleteClick"
                OnbtnSearchClick="acbTourType_SearchClick" OnbtnEditClick="acbTourType_EditClick"
                OnimgExcelExport_Click="acbTourType_ExcelExportClick" OnimgCsvExport_Click="acbTourType_CsvExportClick"
                OnimgPdfExport_Click="acbTourType_PdfExportClick" OnimgWordExport_Click="acbTourType_WordExportClick"
                OnbtnRefreshClick="acbTourType_RefreshClick" />
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
                                        <asp:Literal runat="server" ID="ltlTableHeader" Text="New Tour"></asp:Literal>
                                    </th>
                                    <th>
                                        <div class="MandatoryNote" align="right">
                                            <asp:Literal ID="ltlMandatoryNote" runat="server">Fields marked with <span class="error">*</span> are mandatory.</asp:Literal>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <td style="width: 120px">
                                        <asp:Label ID="lblTourTypeName" runat="server" Text="Tour Name:"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTourName" runat="server" ToolTip="Add Tour Name" Width="<%$appSettings:TextBoxWidth %>"
                                            MaxLength="40"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="requiretxtTourName" runat="server" ControlToValidate="txtTourName"
                                            ValidationGroup="ReqTourType" Display="Dynamic" ErrorMessage="<br />Enter Valid Tour Name"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 120px">
                                        <asp:Label ID="lblTourDesc" runat="server" Text="Tour Desc:"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTourDesc" runat="server" ToolTip="Add Tour Desc" Width="<%$appSettings:TextBoxWidth %>"
                                            MaxLength="40"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTourDesc"
                                            ValidationGroup="ReqTourType" Display="Dynamic" ErrorMessage="<br />Enter Valid Tour Desc"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbTourType" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlGrid" runat="server">
                            <telerik:RadGrid ID="radgrdTour" runat="server" PageSize="<%$appSettings:GridPageSize %>"
                                Width="100%" AllowMultiRowEdit="true" OnPreRender="acbTourType_PreRender" OnItemDataBound="acbTourType_ItemDataBound"
                                OnNeedDataSource="acbTourType_NeedDataSource">
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
                                        <telerik:GridTemplateColumn HeaderText="Id" UniqueName="TOUR_TYPE_ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrdTourIdItem" runat="server" Text='<%# Bind("TOUR_TYPE_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblgrdTourIdEdit" runat="server" Text='<%# Bind("TOUR_TYPE_ID") %>'></asp:Label>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Tour Name" UniqueName="TOUR_TYPE_NAME" SortExpression="TOUR_TYPE_NAME">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkbtngrdTourName" runat="server" Title="Click here to Sort"
                                                    CommandName='Sort' CommandArgument='TOUR_TYPE_NAME' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrdTourName" runat="server" Text='<%# Bind("TOUR_TYPE_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgrdTourName" runat="server" Text='<%# Bind("TOUR_TYPE_NAME") %>'
                                                    MaxLength="50"></asp:TextBox>
                                                <span class="error">*</span>
                                                <asp:RequiredFieldValidator ID="requireTourName" runat="server" ValidationGroup="ReqTourType"
                                                    Display="Dynamic" ErrorMessage="<br />Enter Valid Tour Name" CssClass="error"
                                                    ControlToValidate="txtgrdTourName"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Tour Desc" UniqueName="TOUR_TYPE_DESC" SortExpression="TOUR_TYPE_DESC">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkbtngrdTourDesc" runat="server" Title="Click here to Sort"
                                                    CommandName='Sort' CommandArgument='TOUR_TYPE_DESC' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrdTourDesc" runat="server" Text='<%# Bind("TOUR_TYPE_DESC") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgrdTourDesc" runat="server" Text='<%# Bind("TOUR_TYPE_DESC") %>'
                                                    MaxLength="50"></asp:TextBox>
                                                <span class="error">*</span>
                                                <asp:RequiredFieldValidator ID="requireTourDesc" runat="server" ValidationGroup="ReqTourType"
                                                    Display="Dynamic" ErrorMessage="<br />Enter Valid Tour Desc" CssClass="error"
                                                    ControlToValidate="txtgrdTourDesc"></asp:RequiredFieldValidator>
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
                        </asp:Panel>
                        <div>
                            <asp:HiddenField ID="hdnCheckIndex" runat="server" />
                            <asp:HiddenField ID="hdnEditableMode" runat="server" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbTourType" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
