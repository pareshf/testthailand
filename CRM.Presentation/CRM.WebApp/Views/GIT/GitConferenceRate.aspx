<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="GitConferenceRate.aspx.cs" Inherits="CRM.WebApp.Views.GIT.GitConferenceRate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
<link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
<asp:Label ID="Label55" runat="server" Text="Conferences Rates" class="pageTitle"
            Width="200px" Font-Bold="true" Font-Size="Large"></asp:Label>
        <br />
        <br />

     
        <asp:Label runat="server" ID="lblPriceHeader" Text="Price of Conferences" CssClass="pageTitle"></asp:Label>

        <br />
        <br />

           <div class="pageTitle">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>
        
        <asp:GridView runat="server" ID="gridHotelRate" SkinID="sknSubGrid" AutoGenerateColumns="false" AllowPaging="false" Width="300px">
        
        <Columns>
        
        <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign = "Center">
            <HeaderTemplate><asp:Label runat="server" ID="lblNoPax" Text="No Of PAX" CssClass="lblstyleGIT"></asp:Label></HeaderTemplate>
            <ItemTemplate><asp:Label runat="server" ID="lblNoPax" Text='<%# Bind("NO_OF_PAX") %>'></asp:Label></ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField ControlStyle-Width="100px">
            <HeaderTemplate><asp:Label runat="server" ID="lblNetRate" Text="AdultRate Per Pax" CssClass="lblstyleGIT"></asp:Label></HeaderTemplate>
            <ItemTemplate><asp:TextBox runat="server" ID = "txtAdultRate" Text='<%# Bind("ADULT_RATE_PER_PERSON") %>'></asp:TextBox></ItemTemplate>
        </asp:TemplateField>


        <asp:TemplateField ControlStyle-Width="100px">
            <HeaderTemplate><asp:Label runat="server" ID="lblTotalRate" Text="Child Rate per Pax" CssClass="lblstyleGIT"></asp:Label></HeaderTemplate>
            <ItemTemplate><asp:TextBox runat="server" ID = "txtChildRate" Text='<%# Bind("CHILD_RATE_PER_PERSON") %>'></asp:TextBox></ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField Visible="false">
            <HeaderTemplate></HeaderTemplate>
            <ItemTemplate><asp:Label runat="server" ID="lblCartID" Text='<%# Bind("GIT_CONFERENCE_CART_ID") %>' CssClass="lblstyleGIT"></asp:Label></ItemTemplate>
        </asp:TemplateField>

        </Columns>

        </asp:GridView>

        <br />
        <br />
        <asp:Button runat="server" ID="btnUpdate" Text = "Save" Width="100px" OnClick="btnUpdate_Click"/>&nbsp; &nbsp;
        <asp:Button runat="server" ID="btnBack" Text="Back" OnClick="btnBack_Click" Width="100px"/>
        </ContentTemplate>
        </asp:UpdatePanel>
        </div>
</asp:Content>
