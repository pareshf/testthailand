<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="InvoiceWiseProfitReport.aspx.cs" Inherits="CRM.WebApp.Views.MIS.InvoiceWiseProfitReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <table>
        <tr>
            <td>
                <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtInvoiceNo" runat="server" Width="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblQuoteRefNo" runat="server" Text="Quotation Reference No"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQuoteRefNo" runat="server" Width="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
            </td>
        </tr>
    </table>
    <br />
  <%--  <div>
        <asp:UpdatePanel ID="upReport" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <rsweb:ReportViewer ID="rptViewer1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                    BorderWidth="1px" Height="8.5in" Width="14in">
                </rsweb:ReportViewer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>--%>
</asp:Content>
