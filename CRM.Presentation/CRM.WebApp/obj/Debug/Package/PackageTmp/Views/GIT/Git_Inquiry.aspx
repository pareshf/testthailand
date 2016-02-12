<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="Git_Inquiry.aspx.cs" Inherits="CRM.WebApp.Views.GIT.Git_Inquiry" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<asp:Content ID="cntincudes" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="cntpagecontent" ContentPlaceHolderID="cphPageContent" runat="server">
    <style>
        .sectionHeader
        {
            font-family: Arial;
            font-weight: bold;
            margin-left: 0px;
        }
        .headlabel
        {
            font-size: "40px";
            font-weight: bold;
            font-family: Verdana;
        }
        
        .fieldlabel
        {
            font-family: Verdana;
            font-size: 12px;
        }
        .textboxstyle
        {
            width: 50px;
        }
        .buttonstyle
        {
            width: 150px;
        }
        
        .lblstyle
        {
            font-family: Verdana;
            font-size: 12px;
        }
        .errorclass
        {
            font-family: Verdana;
            font-size: 12px;
            color: Red;
        }
    </style>
    <div>
        <asp:Label runat="server" Text="GIT INQUIRY" ID="headlbl" Width="200px" Font-Bold="true"
            Font-Size="Large" class="pageTitle"></asp:Label>
        <br />
        <br />
        <table>
            <tr>
                <td valign="top">
                    <asp:Label ID="lblreq" runat="server" Text="Requirments" CssClass="lblstyle" Width="100px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtreq" runat="server" TextMode="MultiLine" width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Lblpax" runat="server" Text="No Of Pax" CssClass="lblstyle" Width="100px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtpax" runat="server" width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" Width="100px" 
                        onclick="btnsubmit_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
