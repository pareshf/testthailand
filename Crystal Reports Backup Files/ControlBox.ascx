<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlBox.ascx.cs"
    Inherits="CRM.WebApp.Views.Shared.Controls.Navigation.ControlBox" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td style="border-bottom: solid 2px #808080; height: 30px; border-left: solid 1px #CCCCCC;
            border-right: solid 1px #CCCCCC; background-color: #E5E5E5;" valign="middle"
            align="left">
            <div style="margin: 2px 3px 2px 3px">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="55px" AccessKey="s" ToolTip="Alt+S" />
                <asp:Button ID="btnSaveNew" runat="server" Text="Save & New" Width="100px" AccessKey="w"
                    ToolTip="Alt+W" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" Width="55px" AccessKey="l"
                    ToolTip="Alt+L" />
                <asp:Button ID="btnCopy" runat="server" Text="Copy" Width="55px" AccessKey="p" ToolTip="Alt+P" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="55px" AccessKey="c"
                    ToolTip="Alt+C" />
                <%--<telerik:RadToolTip ID="RadToolTip1" TargetControlID="btnSave" Text="&nbsp;Alt+S&nbsp;"
                    runat="server">
                </telerik:RadToolTip>
                <telerik:RadToolTip ID="RadToolTip2" TargetControlID="btnSaveNew" Text="&nbsp;Alt+W&nbsp;"
                    runat="server">
                </telerik:RadToolTip>
                <telerik:RadToolTip ID="RadToolTip3" TargetControlID="btnClear" Text="&nbsp;Alt+L&nbsp;"
                    runat="server">
                </telerik:RadToolTip>
                <telerik:RadToolTip ID="RadToolTip4" TargetControlID="btnCopy" Text="&nbsp;Alt+P&nbsp;"
                    runat="server">
                </telerik:RadToolTip>
                <telerik:RadToolTip ID="RadToolTip5" TargetControlID="btnCancel" Text="&nbsp;Alt+C&nbsp;"
                    runat="server">
                </telerik:RadToolTip>--%>
            </div>
        </td>
    </tr>
</table>
