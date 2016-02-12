<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="SiteSeeingPrice.aspx.cs" Inherits="CRM.WebApp.Views.BackOffice.SiteSeeingPrice" %>

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
    <style type="text/css">
        .headlabel
        {
            font-size: "40px";
            font-weight: normal;
            font-family: Verdana;
        }
    </style>
    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);

        function isNumberKeyDecimal(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
    <asp:Label ID="Label55" runat="server" Text="SiteSeeing Price" class="pageTitle"
        Width="200px" Font-Bold="true" Font-Size="Large"></asp:Label>
    <br />
    <br />
    <div id="SiteSeeingPrice" class="pageTitle">
        <asp:UpdatePanel ID="upHotel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <table width="475px">
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Price Of" ID="Price_of" Font-Size="Large" Font-Bold="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="" ID="lblPrice" Font-Size="Large" Font-Bold="false"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GridSiteSeeingPrice" runat="server" AutoGenerateColumns="false"
                                SkinID="sknSubGrid" Width="700px">
                                <Columns>
                                    <asp:TemplateField ControlStyle-Width="200px" Visible="false">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblslab" runat="server" Text="Slab id" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblslabid" runat="server" CssClass="lblstyleGIT"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="200px" Visible="false">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblmaster" runat="server" Text="Master id" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblmasterid" runat="server" CssClass="lblstyleGIT"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="200px" Visible="false">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblP" runat="server" Text="Price id" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPriceid" runat="server" CssClass="lblstyleGIT"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblPax" runat="server" Text="No Of Pax" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoOfPax" runat="server" CssClass="lblstyleGIT"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="85px" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblAdultSIC" runat="server" Text="Adult SIC Rate" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAdultSIC" runat="server" onkeypress="return isNumberKeyDecimal(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="85px" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblChildSIC" runat="server" Text="Child SIC Rate" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtChildSIC" runat="server" onkeypress="return isNumberKeyDecimal(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="85px" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblAdultPVT" runat="server" Text="Adult PVT Rate" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAdultPVT" runat="server" onkeypress="return isNumberKeyDecimal(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="85px" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblChildPVT" runat="server" Text="Child PVT Rate" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtChildPVT" runat="server" onkeypress="return isNumberKeyDecimal(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblAmargin" runat="server" Text="A Margin Amount" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAmargin" runat="server" onkeypress="return isNumberKeyDecimal(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblAPlusmargin" runat="server" Text="A+ Margin Amount" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAPlusmargin" runat="server" onkeypress="return isNumberKeyDecimal(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblAPlusPlusmargin" runat="server" Text="A++ Margin Amount" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAPlusPlusmargin" runat="server" onkeypress="return isNumberKeyDecimal(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblAmargininper" runat="server" Text="A Margin Amount[%]" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAmargininper" runat="server" onkeypress="return isNumberKeyDecimal(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblAPlusmargininper" runat="server" Text="A+ Margin Amount[%]" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAPlusmarginInper" runat="server" onkeypress="return isNumberKeyDecimal(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblAPlusPlusmargininper" runat="server" Text="A++ Margin Amount[%]"
                                                CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAPlusPlusmargininper" runat="server" onkeypress="return isNumberKeyDecimal(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Button runat="server" Text="Save" ID="btnSave" Width="150px" OnClick="btnSave_Click" />
                        </td>
                        <td>
                            <asp:Button runat="server" Text="Back" ID="Button1" Width="150px" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
