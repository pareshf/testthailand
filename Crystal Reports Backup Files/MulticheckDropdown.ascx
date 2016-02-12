<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MulticheckDropdown.ascx.cs" Inherits="CRM.WebApp.Views.FIT.MulticheckDropdown" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<script type="text/javascript">
    // NOTE - these two functions can be moved to the page
    function StopPropagation(e) {
        //cancel bubbling 
        e.cancelBubble = true;
        if (e.stopPropagation) {
            e.stopPropagation();
        }
    }

    //this method removes the ending character from a string
    function removeLastDelimiter(str) {

        var len = str.length;
        if (len > 1)
            return str.substr(0, len - 1);
        return str;
    }

   
  



</script>



<%--<link href="../../Styles/style.css" rel="stylesheet" type="text/css" />--%>
<%--CHANGE--%>
<%--<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>--%>
<%--CHANGE END--%>
<div style="position:relative">
    <table>
        <tr>
            <td>
                
                <telerik:radcombobox ID="CB1" runat="server"  
                    AllowCustomText="True" OnItemDataBound="CB1_ItemDataBound" 
                    HighlightTemplatedItems="True" width="150px">
                    <HeaderTemplate>
                        <asp:CheckBox runat="server" ID="SelectAll" />
                        <asp:Label runat="server" ID="hdrLabel" AssociatedControlID="SelectAll">Select All</asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div onclick="StopPropagation(event)">
                            <asp:CheckBox runat="server" ID="chk1" />
                            <asp:Label runat="server" ID="label1" AssociatedControlID="chk1"><%# DataBinder.Eval(Container, "Text") %></asp:Label>
                        </div>
                    </ItemTemplate>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                
                
                
                </telerik:radcombobox>
                <%--<telerik:RadComboBox ID="CB1" runat="server" Skin="Office2007" 
                    AllowCustomText="True" OnItemDataBound="CB1_ItemDataBound" 
                    HighlightTemplatedItems="True">
                    <HeaderTemplate>
                        <asp:CheckBox runat="server" ID="SelectAll" />
                        <asp:Label runat="server" ID="hdrLabel" AssociatedControlID="SelectAll">Select All</asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div onclick="StopPropagation(event)">
                            <asp:CheckBox runat="server" ID="chk1" />
                            <asp:Label runat="server" ID="label1" AssociatedControlID="chk1"><%# DataBinder.Eval(Container, "Text") %></asp:Label>
                        </div>
                    </ItemTemplate>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>--%>
            </td>
        </tr>
    </table>
</div>