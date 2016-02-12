<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    CodeBehind="ReligionLookUp.aspx.cs" Inherits="CRM.WebApp.Views.Administration.ReligionLookUp"
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
            if (eventArgs.EventTarget == "ctl00$cphPageContent$acbReligion$imgExcelExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbReligion$imgWordExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbReligion$imgPdfExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbReligion$imgCsvExport") {
                eventArgs.EnableAjax = false;               
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        var selectionAlertMessage = '<%=ConfigurationManager.AppSettings["AtleastOneRecord"].ToString() %>';
        var deleteAlertMessage = '<%=ConfigurationManager.AppSettings["DeleteAlert"].ToString() %>';
    </script>

    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="cntPageContent" ContentPlaceHolderID="cphPageContent" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <clientevents onrequeststart="ramRequestStarted" />
        <ajaxsettings>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbReligion$imgExcelExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbReligion$imgWordExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbReligion$imgPdfExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbReligion$imgCsvExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </ajaxsettings>
    </telerik:RadAjaxManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Style="z-index: 100100">
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
        <asp:Literal ID="lblPageTitle" runat="server" Text="Religion"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="upActionBar" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <crmUC:ActionBar ID="acbReligion" runat="server" OnbtnNewClick="acbReligion_NewClick"
                OnbtnSaveClick="acbReligion_SaveClick" OnbtnCancelClick="acbReligion_CancelClick"
                OnbtnDeleteClick="acbReligion_DeleteClick" OnbtnEditClick="acbReligion_EditClick"
                OnbtnSearchClick="acbReligion_SearchClick" SaveButtonValidationGroup="ReqReligionName"
                OnbtnSaveNewClick="acbReligion_SaveNewClick" SaveNewButtonValidationGroup="ReqReligionName"
                OnimgExcelExport_Click="acbReligion_ExcelExportClick" OnimgCsvExport_Click="acbReligion_CsvExportClick"
                OnimgPdfExport_Click="acbReligion_PdfExportClick" OnimgWordExport_Click="acbReligion_WordExportClick" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="75%">
        <tr>
            <td>
                <asp:UpdatePanel ID="upAddNew" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAddNewMode" runat="server" Visible="false">
                            <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                                <tr>
                                    <th>
                                        <asp:Literal runat="server" ID="ltlTableHeader" Text="New Religion"></asp:Literal>
                                    </th>
                                    <th>
                                        <div class="MandatoryNote" align="right" style="margin-top: 3px; margin-bottom: 3px">
                                            <asp:Literal ID="ltlMandatoryNote" runat="server">Fields marked with <span class="error">*</span> are mandatory.&nbsp;</asp:Literal>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <td style="width: 120px">
                                        <asp:Label ID="lblReligionName" runat="server" Text="Religion :"></asp:Label>
                                        <span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReligionName" runat="server" ValidationGroup="ReqReligionName" Width="<%$appSettings:TextBoxWidth %>"
                                            MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="requireReligionName" runat="server" ControlToValidate="txtReligionName"
                                            ValidationGroup="ReqReligionName" Display="Dynamic" ErrorMessage="<br />Enter Valid Religion"
                                            CssClass="error"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbReligion" />
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
                                        <telerik:RadGrid ID="radgrdReligion" runat="server" AutoGenerateColumns="false" AllowMultiRowSelection="true"
                                            AllowMultiRowEdit="true" AllowPaging="true" AllowSorting="true" ClientSettings-EnableRowHoverStyle="true" 
                                            EnableEmbeddedSkins="false" OnPreRender="radgrdReligion_PreRender" OnItemDataBound="radgrdReligion_ItemDataBound"
                                            OnNeedDataSource="radgrdReligion_NeedDataSource" PageSize="<%$appSettings:GridPageSize %>"
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
                                                    <HeaderStyle Width="20px"/>
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridTemplateColumn>
                                                    <HeaderStyle HorizontalAlign ="Center" Width="7%" />
                                                        <ItemStyle CssClass="ItemAlign" Width="25px" />
                                                        
                                                        <HeaderTemplate>
                                           
                                                               <asp:CheckBox ID="chkHeadWrT" runat="server"/>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkItemWrt" runat="server" AutoPostBack="false" OnCheckedChanged="chkItemWrt_CheckChanged" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Id" UniqueName="RELIGION_ID" Visible="false">
                                                        
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrdReligionIdItem" runat="server" Text='<%# Bind("RELIGION_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblGrdReligionIdEdit" runat="server" Text='<%# Bind("RELIGION_ID") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Religion Name" UniqueName="RELIGION_NAME"
                                                        SortExpression="RELIGION_NAME">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkbtnGrdReligionName" runat="server" Title="Click here to Sort"
                                                                CommandName='Sort' CommandArgument='RELIGION_NAME' />
                                                        </HeaderTemplate>
                                                       <%-- <ItemTemplate>
                                                            <asp:Label ID="lblGrdReligionName" runat="server" Text='<%# Bind("RELIGION_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                        <asp:TextBox ID="txtGrdReligionName" runat="server" Text='<%# Bind("RELIGION_NAME") %>' 
                                                        MaxLength="50" DataValueField ="RELIGION_ID" DataTextField= "RELIGION_NAME"></asp:TextBox>
                                                            <span class="error">*</span>
                                                            <asp:RequiredFieldValidator ID="requireGrdReligionName" runat="server" ControlToValidate="txtGrdReligionName"
                                                                ValidationGroup="ReqReligionName" Display="Dynamic" ErrorMessage="<br />Enter Valid Religion"
                                                                CssClass="error"></asp:RequiredFieldValidator>
                                                        </EditItemTemplate>--%>

                                                         <ItemTemplate>
                                                                    <asp:Label ID="lblGrdReligionName" runat="server" Text='<%# Bind("RELIGION_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtGrdReligionName" runat="server" Text='<%# Bind("RELIGION_NAME") %>'
                                                                        MaxLength="50"></asp:TextBox>
                                                                    <span class="error">*</span>
                                                                    <asp:RequiredFieldValidator ID="requireGrdReligionName" runat="server" ControlToValidate="txtGrdReligionName"
                                                                        ValidationGroup="ReqReligionName" Display="Dynamic" ErrorMessage="<br />Enter Valid Status Name"
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
                                                        <HeaderStyle HorizontalAlign="Left"  />
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
                                                        <HeaderStyle HorizontalAlign="Left"  />
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
                        <asp:AsyncPostBackTrigger ControlID="acbReligion" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
