<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="GuideRate.aspx.cs" Inherits="CRM.WebApp.Views.GIT.GuideRate" %>

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
<div>
      <asp:Label ID="Label55" runat="server" Text="Guide Rates" class="pageTitle"
            Width="200px" Font-Bold="true" Font-Size="Large"></asp:Label>
            <br />
            <br />
            <asp:Label runat="server" ID="lblPriceHeader" Text="Price of Guide" CssClass="pageTitle"></asp:Label>

        <br />
        <br /> 
           
    <asp:UpdatePanel runat="server" ID="upCoach" ChildrenAsTriggers="false" UpdateMode="Conditional" >
    <ContentTemplate>
        <asp:GridView ID="gridGuide" runat="server" AllowPaging="false" AutoGenerateColumns="false" CssClass="pageTitle" Width="500px" SkinID="sknSubGrid">
            <Columns>
                
                 <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign = "Center">
                    <HeaderTemplate> <asp:Label runat="server" ID="lblHeader2" Text = "No. of Pax" CssClass="lblstyleGIT"></asp:Label> </HeaderTemplate>
                    <ItemTemplate> <asp:Label runat="server" ID="lblPax" Text = '<%# Bind("NO_OF_PAX") %>'></asp:Label> </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField  ControlStyle-Width="100px">
                    <HeaderTemplate> <asp:Label runat="server" ID="lblHeader1" Text = "Adult Rate Per Pax" CssClass="lblstyleGIT"></asp:Label> </HeaderTemplate>
                    <ItemTemplate> <asp:TextBox ID="txtAdultRate" runat="server" Text='<%# Bind("NET_RATE_PER_PERSON") %>'></asp:TextBox> </ItemTemplate>
                </asp:TemplateField>

               

                <asp:TemplateField  ControlStyle-Width="100px" Visible="false">
                    <HeaderTemplate> <asp:Label runat="server" ID="lblHeader3" Text = "Child Rate Per Pax" CssClass="lblstyleGIT"></asp:Label> </HeaderTemplate>
                    <ItemTemplate> <asp:TextBox ID="txtChildRate" runat="server" Text='<%# Bind("NET_RATE_PER_CHILD") %>'></asp:TextBox> </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField Visible="false">
            <HeaderTemplate></HeaderTemplate>
            <ItemTemplate><asp:Label runat="server" ID="lblCartID" Text='<%# Bind("GIT_GUIDE_CART_ID") %>' CssClass="lblstyleGIT"></asp:Label></ItemTemplate>
        </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
   
    <br />
    <br />

  <asp:UpdatePanel runat="server" ID="upButtons" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button runat="server" ID="btnUpdate" Text = "Save" Width="100px" OnClick="btnUpdate_Click" />&nbsp; &nbsp;
        <asp:Button runat="server" ID="btnBack" Text = "Back" Width= "100px" OnClick="btnBack_Click"/>
    </ContentTemplate>
    </asp:UpdatePanel>
</div>
</asp:Content>
