<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="ContraVoucherMy.aspx.cs" Inherits="CRM.WebApp.Views.Account.SearchPages.ContraVoucherMy" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
<link href="../../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
<script src="../../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
 <script language="javascript" type="text/javascript">

     var sessionTimeout = "<%= Session.Timeout %>";

     var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
     setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>
     <div>
        <asp:Label runat="server" Text="My Contra Voucher" ID="headlbl" Width="400px" Font-Bold="true"
            Font-Size="Large" class="pageTitle"></asp:Label>
        <br />
        <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="Button3" runat="server" Text="Search" OnClick="search_onclick" Style="float: right;
                                margin-right: 10px; display: block; color: black;" CssClass="button" />
                            <asp:Button ID="Button4" runat="server" Text="Search Now" Style="float: right; margin-right: 10px;
                                display: none; color: black;" CssClass="button" OnClick="searchnow_onclick" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlMainHead" runat="server" Style="display: none">
                    <table>
                      
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label6" runat="server" Text="Voucher Status" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drp_voucher_status" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label2" runat="server" Text="Voucher Type" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drp_voucher_type" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label3" runat="server" Text="From Date" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label4" runat="server" Text="To Date" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                      
                    </table>
                </asp:Panel>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Result" runat="server" OnSelectedIndexChanging="GV_Result_SelectedIndexChanging"
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="true" OnPageIndexChanging="GV_Result_PageIndexChanging" pagesize="10" Width="800px">
                                <pagersettings mode="NumericFirstLast" position="Bottom" pagebuttoncount="10"/>
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Edit" />
                                    <asp:BoundField DataField="ACCOUNT_VOUCHER_ID" HeaderText="Voucher Id" />
                                    <asp:BoundField DataField="SEQ_NO" HeaderText="Sequence No" />
                                    <%--<asp:BoundField DataField="INVOICE_NO" HeaderText="Invoice No" />--%>
                                    <asp:BoundField DataField="VOUCHER_STATUS" HeaderText="Voucher Status" />
                                    <asp:BoundField DataField="VOUCHER_TYPE" HeaderText="Voucher Type" />
                                    <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No." />
                                    <asp:BoundField DataField="DATE" HeaderText="Date" />
                                     

                                </Columns>
                                <HeaderStyle CssClass="rgHeader" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
