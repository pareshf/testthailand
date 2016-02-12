<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="NewEmail.aspx.cs" Inherits="CRM.WebApp.Views.EmailSettings.NewEmail" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
 <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
 <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
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
            font-weight: normal;
            font-family: Verdana;
        }
        
        .fieldlabel
        {
            font-family: Verdana;
            font-size: 20px;
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
            font-weight: normal;
        }
        
        .gridlabel
        {
            font-family: Verdana;
            font-size: 14px;
            font-weight: bold;
        }
        
        #TextArea1
        {
            width: 390px;
        }
    </style>
      <asp:UpdatePanel ID="UpEmail" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>
                <table>
                <tr>
                          <td width="180px">
                                <asp:Label ID="Label2" runat="server" Text="From" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFrom" runat="server" Width="800px" ReadOnly="true"></asp:TextBox>
                            </td>
                     </tr>
                    <tr>
                          <td width="180px">
                                <asp:Label ID="Label4" runat="server" Text="To" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTo" runat="server" Width="800px" ReadOnly="true"></asp:TextBox>
                            </td>
                     </tr>
                     <tr>
                          <td width="180px">
                                <asp:Label ID="Label5" runat="server" Text="cc" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcc" runat="server" Width="800px" ReadOnly="true"></asp:TextBox>
                            </td>
                     </tr>
                      <tr>
                          <td width="180px">
                                <asp:Label ID="Label1" runat="server" Text="Bcc" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBcc" runat="server" Width="800px" ReadOnly="true"></asp:TextBox>
                            </td>
                     </tr>
                      <tr>
                          <td width="180px">
                                <asp:Label ID="Label7" runat="server" Text="Subject" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSubject" runat="server" Width="800px" ReadOnly="true"></asp:TextBox>
                            </td>
                     </tr>
                     <tr>   
                             <td width="180px"></td>                      
                            <td>
                               <%-- <asp:TextBox ID="txtContect" runat="server" TextMode="MultiLine" Width="800px" Height="800px" ReadOnly="true"></asp:TextBox>--%>
                                <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
                            </td>
                     </tr>
                </table>
                </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
