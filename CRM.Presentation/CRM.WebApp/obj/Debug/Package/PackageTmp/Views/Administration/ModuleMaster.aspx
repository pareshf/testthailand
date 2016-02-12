<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    CodeBehind="ModuleMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.ModuleMaster"
    Title="Module Master" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="../Shared/Controls/Navigation/ActionBar.ascx" TagName="ActionBar"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">

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
    </script>

    <script language="javascript" type="text/javascript">
        var selectionAlertMessage ='<%=ConfigurationManager.AppSettings["AtleastOneRecord"].ToString() %>';
        var deleteAlertMessage ='<%=ConfigurationManager.AppSettings["DeleteAlert"].ToString() %>';
        
        function ValidateDelete(event, gridControlId) 
        {
            var RadGrid = $find(gridControlId)
            var row = RadGrid.get_masterTableView().get_selectedItems().length;
            if (row == 0) {
                radalert(selectionAlertMessage, 330, 110, 'Warning Message');
                return false;
            }
            else {
                blockConfirm(deleteAlertMessage, event, 300, 110, null, 'Delete Confirmation');
            }
        }
                
        function OnRowClick(sender,eventargs)
        {
            var MasterTable = sender.get_masterTableView();           
            MasterTable.deselectItem(eventargs.get_itemIndexHierarchical());
        } 
    </script>

    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
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
    <table>
        <tr valign="top">
            <td>
                <table>
                    <tr>
                        <td style="padding-bottom: 5px; padding-top: 10px; padding-left: 10px; font-family: Arial;
                            font-size: 18px; font-weight: bold;">
                            Module Master
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 2px; background-color: #666666">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px; background-color: #e5e5e5;">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <uc1:ActionBar ID="udcActionbar" runat="server" />
                                        <%--OnbtnNewClick="udcActionbar_AddClick"
                                                    OnbtnDeleteClick="udcActionbar_DeleteClick" OnbtnSearchClick="udcActionbar_SearchClick"
                                                    OnbtnEditClick="udcActionbar_EditClick" OnbtnExport_Click="btnExport_Click" OnbtnSearch_Click="btnSearch_Click"
                                                    OnbtnSaveClick="udcActionbar_SaveClick" OnbtnCancelClick="udcActionbar_CancelClick"--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td>
                            <asp:Panel ID="pnlAddEdit" Visible="false" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="right" valign="top" class="clsLabel" style="width: 110px">
                                            <span class="clsMandatory">*</span>&nbsp;<asp:Label ID="Label1" CssClass="clsLabel"
                                                runat="server" Text="Module Name"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAddModuleName" runat="server" Width="150px" Style="border: solid 1px #c4c4c4;"
                                                MaxLength="30"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" class="clsLabel" style="width: 110px">
                                            <span class="clsMandatory">*</span>&nbsp;<asp:Label ID="Label2" CssClass="clsLabel"
                                                runat="server" Text="Module Sort Order"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAddModuleSortOrder" runat="server" Width="150px" Style="border: solid 1px #c4c4c4;"
                                                MaxLength="30"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAddSave" runat="server" Text="Save" Width="60px" />
                                            <asp:Button ID="btnAddCancel" runat="server" Text="Cancel" />
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <telerik:RadGrid ID="radGrdModuleMaster" runat="server" AutoGenerateColumns="false"
                                AllowMultiRowSelection="true" AllowMultiRowEdit="true" AllowPaging="true" AllowSorting="true"
                                ClientSettings-EnableRowHoverStyle="true" EnableEmbeddedSkins="false" Skin="CRM_Default"
                                Width="100%">
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true"></Selecting>
                                </ClientSettings>
                                <MasterTableView AutoGenerateColumns="False" EditMode="InPlace" AllowSorting="True"
                                    PagerStyle-AlwaysVisible="true">
                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <Columns>
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeadWrT" runat="server" />
                                            </HeaderTemplate>
                                            <ItemStyle CssClass="ItemAlign" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkItemWrt" runat="server" onclick="javascript:SelectRow(this)"
                                                    AutoPostBack="false" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="RoleID" Visible="false">
                                            <HeaderStyle Width="100px" />
                                            <HeaderTemplate>
                                                Module id
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblModuleId" runat="server" Text='<%# Bind("Module_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="25%" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="ModuleName">
                                            <HeaderStyle Width="100px" />
                                            <HeaderTemplate>
                                                Module Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblModuleName" runat="server" Text='<%# Bind("Module_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="lblEditModuleName" runat="server" Text='<%# Bind("Module_NAME") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="SortOrder_NAME">
                                            <HeaderStyle Width="100px" />
                                            <HeaderTemplate>
                                                Module Sort Order
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSortOrder" runat="server" Text='<%# Bind("MODULE_SORT_ORDER") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="lblEditSortOrder" runat="server" Text='<%# Bind("MODULE_SORT_ORDER") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="CreatedBY">
                                            <HeaderStyle Width="100px" />
                                            <HeaderTemplate>
                                                Created By
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="CreatedBY">
                                            <HeaderStyle Width="100px" />
                                            <HeaderTemplate>
                                                Created On
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreatedOn" runat="server" Text='<%# Bind("CreatedOn","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="CreatedBY">
                                            <HeaderStyle Width="100px" />
                                            <HeaderTemplate>
                                                Modified By
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbllblModifiedBy" runat="server" Text='<%# Bind("ModifiedBy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="CreatedBY">
                                            <HeaderStyle Width="100px" />
                                            <HeaderTemplate>
                                                Modified On
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblModifiedOn" runat="server" Text='<%# Bind("ModifiedOn","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnEditableMode" runat="server" />
    <asp:HiddenField ID="hdnCheckindex" runat="server" />
</asp:Content>
