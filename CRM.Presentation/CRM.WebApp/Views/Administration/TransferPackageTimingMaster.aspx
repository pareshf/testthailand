<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="TransferPackageTimingMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.TransferPackageTimingMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Views/FIT/MulticheckDropdown.ascx" TagName="DropDownControl"
    TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>

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

         <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>
              
                <table width="100%">
                    <tr>
                        <td>

                          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsrno" runat="server" Text="Name" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_Bkk_srno" runat="server" Text='<%# Bind("NAME") %>' CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Times">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltp_detialid" runat="server" Text='<%# Bind("TRANSFER_PACKAGE_FROM_TO_DETAIL_ID") %>'
                                                    CssClass="lblstyle" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFlag" runat="server" Text='<%# Bind("FLAG") %>'
                                                    CssClass="lblstyle" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFromId" runat="server" Text='<%# Bind("FROM_ID") %>'
                                                    CssClass="lblstyle" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblToId" runat="server" Text='<%# Bind("TO_ID") %>'
                                                    CssClass="lblstyle" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        </Columns>

                                        </asp:GridView>

                        </td>
                        </tr>
                        </table>

                        <br />
                        <br />

                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"/>
                        </ContentTemplate>
                        </asp:UpdatePanel>
</asp:Content>
