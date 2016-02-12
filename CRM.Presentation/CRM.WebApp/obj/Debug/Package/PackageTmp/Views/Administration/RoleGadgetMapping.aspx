<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    CodeBehind="RoleGadgetMapping.aspx.cs" Inherits="CRM.WebApp.Views.Administration.RoleGadgetMapping"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">

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
    <div class="pageTitle">
        <asp:Literal ID="Literal1" runat="server" Text="Gadget Access"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                <tr>
                    <th colspan="3">
                        <asp:Literal runat="server" ID="ltlTableHeader" Text="Gadget Access"></asp:Literal>
                    </th>
                </tr>
                <tr valign="top">
                    <td width="300px" valign="top">
                        <telerik:RadGrid ID="radgrdRoleAccess" runat="server" AllowPaging="false" ClientSettings-EnableRowHoverStyle="true"
                            PageSize="<%$appSettings:GridPageSize %>" OnItemCommand="radgrdRoleAccess_OnItemCommand">
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
                    </td>
                    <td valign="top">
                        <asp:Panel ID="pnlAccessGrid" runat="server" Visible="false">
                            <asp:Label ID="lblRoleTitle1" runat="server" Text="" CssClass="Heading"></asp:Label>
                            <div id="divShowStud" runat="server" style="overflow-y: scroll; max-height: 325px;">
                                <telerik:RadGrid ID="radGridAccess" runat="server" AutoGenerateColumns="false" ClientSettings-EnableRowHoverStyle="true"
                                    AllowPaging="false" AllowSorting="false" OnItemDataBound="radGridAccess_OnItemDataBound"
                                    Width="800px">
                                    <HeaderContextMenu EnableEmbeddedSkins="False" EnableTheming="True">
                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                    </HeaderContextMenu>
                                    <MasterTableView AutoGenerateColumns="False" AllowSorting="True" PagerStyle-AlwaysVisible="true">
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="" UniqueName="GADGET_ID" DataField="GADGET_ID"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="" UniqueName="MODULE_ID" DataField="MODULE_ID"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Gadget Name" UniqueName="GADGET_NAME" DataField="GADGET_NAME">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Description" UniqueName="GADGET_DESC" DataField="GADGET_DESC">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Module" UniqueName="MODULE_NAME" DataField="MODULE_NAME">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Access">
                                                <ItemStyle CssClass="ItemAlign" />
                                                <HeaderStyle Width="20px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Literal runat="server" ID="lblRead" Text="Access"></asp:Literal><br />
                                                    <asp:CheckBox ID="grdChkAllRead" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="grdChkRead" runat="server" Checked='<%#bind("READ_ACCESS") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <br />
                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="57px" OnClick="btnSave_OnClick" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="57px" OnClick="btnCancel_OnClick" />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="radgrdRoleAccess" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
