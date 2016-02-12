<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="RoleAccessMapping.aspx.cs" Inherits="CRM.WebApp.Views.Administration.RoleAccessMapping"
    Title="Role Access Mapping - Flamingo CRM" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">

    <script language="javascript" type="text/javascript">

        var selectionAlertMessage = '<%=ConfigurationManager.AppSettings["AtleastOneRecord"].ToString() %>';
        var deleteAlertMessage = '<%=ConfigurationManager.AppSettings["DeleteAlert"].ToString() %>';

        //        function DeletePopup() 
        //        {
        //            debugger;
        //            blockConfirm(deleteAlertMessage, event, 300, 110, null, 'Delete Confirmation');

        //        }
        function DeletePopup() {
            //alert("Hi");
            //radalert("sure?");
            //radconfirm("sure?", CallBackFn);
        }

        //        function CallBackFn(arg)  
        //            {  
        //                if(arg)  
        //                    {  
        //                        <%= GetPostBackEventReference(btnCancel) %> 
        //                    }  
        //            }  
    </script>

    <script type="text/javascript" language="javascript">

        window.blockConfirm = function(text, mozEvent, oWidth, oHeight, callerObj, oTitle) {
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

    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <asp:UpdatePanel ID="upWindow" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Style="z-index: 100100"
                Behaviors="Move">
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="pageTitle">
        <asp:Literal ID="Literal1" runat="server" Text="Role Access"></asp:Literal>
    </div>
    <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
        <tr>
            <th colspan="3">
                <asp:Literal runat="server" ID="ltlTableHeader" Text="Role Access"></asp:Literal>
            </th>
        </tr>
        <tr valign="top">
            <td width="300px" valign="top">
                <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <telerik:RadGrid ID="radgrdRoleAccess" runat="server" AllowPaging="false" ClientSettings-EnableRowHoverStyle="true"
                            PageSize="<%$appSettings:GridPageSize %>" OnNeedDataSource="radgrdRoleAccess_OnNeedDataSource"
                            OnItemCommand="radgrdRoleAccess_OnItemCommand">
                            <HeaderContextMenu EnableEmbeddedSkins="False" EnableTheming="True">
                                <CollapseAnimation Duration="200" Type="OutQuint" />
                            </HeaderContextMenu>
                            <MasterTableView AutoGenerateColumns="False" AllowSorting="True" PagerStyle-AlwaysVisible="true">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Role ID" UniqueName="ROLE_ID" DataField="ROLE_ID"
                                        Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Role" UniqueName="ROLE_NAME" DataField="ROLE_NAME">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn ButtonType="LinkButton" Text="Assign" ItemStyle-Width="50px">
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <PagerStyle AlwaysVisible="True" />
                            </MasterTableView>
                            <FilterMenu EnableEmbeddedSkins="False" EnableTheming="True">
                                <CollapseAnimation Duration="200" Type="OutQuint" />
                            </FilterMenu>
                        </telerik:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlModule" runat="server" Visible="false">
                            <table>
                                <tr valign="top">
                                    <td colspan="3" class="Heading">
                                        <asp:Literal runat="server" ID="lblRoleTitle" Text=""></asp:Literal>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td style="width: 20%;" valign="middle">
                                        <div style="width: 100%; float: left; height: 20px;">
                                            <asp:Label ID="lblModuleList" runat="server" Text="Module"></asp:Label>
                                        </div>
                                        <div style="width: 100%; float: left;">
                                            <asp:ListBox ID="lstModuleName" runat="server" Height="150px" Width="150px"></asp:ListBox>
                                        </div>
                                    </td>
                                    <td style="width: 5%;" valign="middle" align="center">
                                        <div>
                                            <div style="width: 100%; float: left; height: 30px;">
                                                <asp:Button ID="btnAssignRightModule" runat="server" Text=">>" Width="23px" ToolTip="Assign Module to role"
                                                    OnClick="btnAssignRightModule_Click" />
                                            </div>
                                            <div style="width: 100%; float: left; height: 30px;">
                                                <asp:Button ID="btnAssignLeftModule" runat="server" Text="<<" Width="23px" ToolTip="Unassign Module to role"
                                                    OnClick="btnAssignLeftModule_Click" />
                                                <asp:Button ID="btnHidden" runat="server" Text="Button" Visible="false" />
                                            </div>
                                        </div>
                                    </td>
                                    <td valign="middle">
                                        <div style="width: 100%; float: left; height: 20px;">
                                            <asp:Label ID="lblAssignedModule" runat="server" Text="Assigned"></asp:Label>
                                        </div>
                                        <div style="width: 100%; float: left;">
                                            <asp:ListBox ID="lstAssignModuleName" runat="server" Height="150px" Width="150px"
                                                AutoPostBack="true" OnSelectedIndexChanged="lstAssignModuleName_OnSelectedIndexChanged">
                                            </asp:ListBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td colspan="3">
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td colspan="3">
                                        <asp:Panel ID="pnlAccessGrid" runat="server" Visible="false">
                                            <div id="divShowStud" runat="server" style="overflow-y: scroll; max-height: 325px;">
                                                <telerik:RadGrid ID="radGridAccess" runat="server" AutoGenerateColumns="false" ClientSettings-EnableRowHoverStyle="true"
                                                    AllowPaging="false" AllowSorting="false" OnItemDataBound="radGridAccess_OnItemDataBound"
                                                    Width="800px">
                                                    <HeaderContextMenu EnableEmbeddedSkins="False" EnableTheming="True">
                                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                                    </HeaderContextMenu>
                                                    <MasterTableView AutoGenerateColumns="False" AllowSorting="True" PagerStyle-AlwaysVisible="true">
                                                        <Columns>
                                                            <telerik:GridBoundColumn HeaderText="" UniqueName="PROGRAM_ID" DataField="PROGRAM_ID"
                                                                Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="Program Name" UniqueName="PROGRAM_TEXT" DataField="PROGRAM_TEXT"
                                                                ItemStyle-Width="200px">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Read Access">
                                                                <ItemStyle CssClass="ItemAlign" />
                                                                <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="lblRead" Text="Read"></asp:Literal><br />
                                                                    <asp:CheckBox ID="grdChkAllRead" runat="server" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="grdChkRead" runat="server" Checked='<%#bind("READ_ACCESS") %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Write Access">
                                                                <ItemStyle CssClass="ItemAlign" />
                                                                <HeaderStyle CssClass="ItemAlign" />
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="lblWrite" Text="Write"></asp:Literal><br />
                                                                    <asp:CheckBox ID="grdChkAllWrite" runat="server" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="grdChkWrite" runat="server" Checked='<%#bind("WRITE_ACCESS") %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Delete Access">
                                                                <ItemStyle CssClass="ItemAlign" />
                                                                <HeaderStyle CssClass="ItemAlign" />
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="lblDelete" Text="Delete"></asp:Literal><br />
                                                                    <asp:CheckBox ID="grdChkAllDelete" runat="server" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="grdChkDelete" runat="server" Checked='<%#bind("DELETE_ACCESS") %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Print Access">
                                                                <ItemStyle CssClass="ItemAlign" />
                                                                <HeaderStyle CssClass="ItemAlign" />
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="lblPrint" Text="Print"></asp:Literal><br />
                                                                    <asp:CheckBox ID="grdChkAllPrint" runat="server" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="grdChkPrint" runat="server" Checked='<%#bind("PRINT_ACCESS") %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </div>
                                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="57px" OnClick="btnSave_OnClick" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="57px" OnClick="btnCancel_OnClick" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="radgrdRoleAccess" />
                        <asp:PostBackTrigger ControlID="btnSave" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
