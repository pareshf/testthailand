<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="RoomListGIT.aspx.cs" Inherits="CRM.WebApp.Views.GIT.RoomListGIT" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">

    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
    </script>

    <asp:Label runat="server" Text="Room List" ID="headlbl" Width="200px" Font-Bold="true"
        Font-Size="Large" class="pageTitle"></asp:Label>
    <br />
    <div id="RoomList" class="pageTitle">
        <asp:UpdatePanel ID="upRoomList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <table width="400px" cellspacing="5" cellpadding="5" >
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Room List" ID="lblRoomlist" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="80px" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                    <tr >
                        <td>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblView" CssClass="lblstyle" visible = "false"></asp:Label>&nbsp&nbsp
                            <a runat="server" href="" id="ViewRoomList" target="_blank" visible = "false">Download</a>
                        </td>                        
                        <td>
                            <%--<a runat="server" href="#" id="DeleteRoomlist" target="_blank" visible = "false"  onclick = "btnDeleteFile_Click">Delete</a>--%>
                            <asp:Button ID="DeleteRoomlist" runat="server" Text="Delete" Width="80px" OnClick="btnDeleteFile_Click" visible = "false"/>
                        </td>
                        <td>
                            <asp:Button ID="btnSkip" runat="server" Text="Skip Step" Width="80px" OnClick="btnSkip_Click" visible = "false"/>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
