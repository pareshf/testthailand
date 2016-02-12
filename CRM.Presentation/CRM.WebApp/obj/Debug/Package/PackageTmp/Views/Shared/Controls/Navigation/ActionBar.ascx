<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActionBar.ascx.cs" Inherits="CRM.WebApp.Views.Shared.Controls.Navigation.ActionBar" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<script language="javascript" type="text/javascript">
    function SearchTips_BeforeHide(sender, eventArgs) {
        try {
            if (document.getElementById("hdnTT").value != "mouseout") {
                eventArgs.set_cancel(true);
            }
            else {   // mouseout 
                document.getElementById("hdnTT").value = "";
            }
        }
        catch (Error) { }
    }

    function SearchTips_Hide(senderObj) {
        try {
            var rToolTip = $find("<%= rttTips.ClientID %>");
            if (rToolTip != null) {
                if (rToolTip.isVisible()) {
                    document.getElementById("hdnTT").value = "mouseout";
                    rToolTip.hide();
                }
            }
        }
        catch (Error)
        { }
    }
</script>

<table width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td colspan="2" style="height: 2px; background-color: #666666">
        </td>
    </tr>
    <tr>
        <td style="height: 30px; background-color: #e5e5e5;" valign="middle">
            <asp:Table ID="tblDefaultMode" runat="server" CellPadding="3" CellSpacing="1" Visible="true">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Button ID="btnNew" runat="server" Text="New" Width="55px" AccessKey="n" ToolTip="Alt+N" />
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="55px" AccessKey="e" ToolTip="Alt+E"  />
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="55px" AccessKey="l"
                            ToolTip="Alt+L" />
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Button ID="btnExport" runat="server" Text="Export" Width="55px" AccessKey="x"
                            ToolTip="Alt+X" Visible="true" OnClientClick="return false;" />
                        <ajax:PopupControlExtender ID="PopEx_btnExport" runat="server" TargetControlID="btnExport"
                            PopupControlID="pnlExportOptions" Position="Bottom" />
                        <asp:Panel ID="pnlExportOptions" runat="server" Style="display: none">
                            <table cellpadding="2" cellspacing="3" class="exportpanelbg">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="imgExcelExport" runat="server" Text="Excel"></asp:LinkButton>
                                    </td>
                                    <td>
                                        |
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="imgWordExport" runat="server" Text="Word"></asp:LinkButton>
                                    </td>
                                    <td>
                                        |
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="imgPdfExport" runat="server" Text="Pdf"></asp:LinkButton>
                                    </td>
                                    <td style="display: none">
                                        |
                                    </td>
                                    <td style="display: none">
                                        <asp:LinkButton ID="imgCsvExport" runat="server" Text="Csv"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" AccessKey="f" ToolTip="Alt+F" />
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                            <asp:TextBox ID="txtSearch" runat="server" Width="250px" Style="border: solid 1px #c4c4c4;"
                                AccessKey="r" ToolTip="Alt+R" />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="55px" />
                        </asp:Panel>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:ImageButton ID="imgToolTip" runat="server" ImageUrl="~/Views/Shared/Images/tips.png"
                            OnClientClick="return false;" />
                        <telerik:RadToolTip runat="server" ID="rttTips" Position="BottomRight" TargetControlID="imgToolTip"
                            ShowEvent="OnMouseOver" OnClientBeforeHide="SearchTips_BeforeHide" Title="" ShowDelay="0"
                            Animation="None" ManualClose="false" Font-Names="Arial">
                        </telerik:RadToolTip>
                        <input type="hidden" name="hdnTT" id="hdnTT" value="" />
                        <%--<asp:Button ID="Button1" runat="server" Text="Export" Width="55px" AccessKey="x"
                            ToolTip="Alt+X" Visible="false" />--%>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <%--<telerik:RadToolTip ID="RadToolTip1" TargetControlID="btnNew" Text="&nbsp;Alt+N&nbsp;"
                runat="server">
            </telerik:RadToolTip>
            <telerik:RadToolTip ID="RadToolTip2" TargetControlID="btnEdit" Text="&nbsp;Alt+E&nbsp;"
                runat="server">
            </telerik:RadToolTip>
            <telerik:RadToolTip ID="RadToolTip3" TargetControlID="btnDelete" Text="&nbsp;Alt+L&nbsp;"
                runat="server">
            </telerik:RadToolTip>
            <telerik:RadToolTip ID="RadToolTip4" TargetControlID="btnExport" Text="&nbsp;Alt+X&nbsp;"
                runat="server">
            </telerik:RadToolTip>
            <telerik:RadToolTip ID="RadToolTip5" TargetControlID="btnRefresh" Text="&nbsp;Alt+F&nbsp;"
                runat="server">
            </telerik:RadToolTip>
            <telerik:RadToolTip ID="RadToolTip6" TargetControlID="pnlSearch" Text="&nbsp;Alt+R&nbsp;"
                runat="server">
            </telerik:RadToolTip>--%>
        </td>
        <td style="height: 30px; background-color: #e5e5e5;" valign="middle">
            <asp:Table ID="tblEditMode" runat="server" CellPadding="3" CellSpacing="1" Visible="false">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="55px" AccessKey="s" ToolTip="Alt+S" CommandName = "Save" />
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Button ID="btnSaveNew" runat="server" Text="Save & New" Width="100px" AccessKey="w"
                            ToolTip="Alt+W" />
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="55px" AccessKey="c"
                            ToolTip="Alt+C" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <%--<telerik:RadToolTip ID="RadToolTip7" TargetControlID="btnSave" Text="&nbsp;Alt+S&nbsp;"
                runat="server">
            </telerik:RadToolTip>
            <telerik:RadToolTip ID="RadToolTip8" TargetControlID="btnSaveNew" Text="&nbsp;Alt+W&nbsp;"
                runat="server">
            </telerik:RadToolTip>
            <telerik:RadToolTip ID="RadToolTip9" TargetControlID="btnCancel" Text="&nbsp;Alt+C&nbsp;"
                runat="server">
            </telerik:RadToolTip>--%>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 1px; background-color: #666666">
        </td>
    </tr>
</table>
