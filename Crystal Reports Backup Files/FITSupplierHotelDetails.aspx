<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="FITSupplierHotelDetails.aspx.cs" Inherits="CRM.WebApp.Views.FIT.FITSupplierHotelDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
        <table>
            <tr>
                <td>
                    <div class="pageTitle">
                    <asp:Label ID="Label1" runat="server" Text="Supplier Hotel Price List Details" class="pageTitle" Width="400px" Font-Bold="true" Font-Size="Large"></asp:Label>
                    <br />
                    <br />
                    <asp:UpdatePanel ID="upPriceList" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btn_Search" runat="server" Text="Search" OnClick="btn_Search_OnClick" Style="float: right;
                                        margin-right: 10px; display: block; color: black;" CssClass="button" />
                                    <asp:Button ID="btn_SearchNow" runat="server" Text="Search Now" Style="float: right; margin-right: 10px;
                                        display: none; color: black;" CssClass="button" OnClick="btn_SearchNow_Onclick" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlMainHead" runat="server" Style="display: none">
                            <table>                       
                                <tr>
                                    <td width="180px">
                                        <asp:Label ID="Label5" runat="server" Text="Room Type" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                    <asp:DropDownList ID="ddlRoomType" runat="server" Width="255px">
                                    </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="180px">
                                        <asp:Label ID="Label2" runat="server" Text="Status" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="255px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                            <table>
                                <tr>
                                    <td>
                                        <asp:GridView ID="dgvSupplierPriceDetails" runat="server"  OnSelectedIndexChanging="GV_Result_SelectedIndexChanging" Width="1200px"
                                            AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="true"  OnPageIndexChanging="GV_Result_PageIndexChanging">
                                            <pagersettings mode="NumericFirstLast" position="Bottom" />
                                            <Columns>     
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Edit" />  
                                                <asp:BoundField DataField="SUPPLIER_HOTEL_PRICE_LIST_ID" HeaderText="Sr No" />
                                                <asp:BoundField DataField="SUPPLIER_SR_NO" HeaderText="Sr No" Visible = "false"/>
                                                <asp:TemplateField  Visible="false">
                                                    <HeaderTemplate>
                                                        <asp:Label ID = "lblHeader1" runat="server" Text="Transfer ID" CssClass="lblstyleGIT"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID = "lblSUPPLIER_SR_NO" runat="server" Text='<%# Bind("SUPPLIER_SR_NO") %>' CssClass="lblstyle"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CITY_NAME" HeaderText="City Name" />            
                                                <asp:BoundField DataField="CHAIN_NAME" HeaderText="Hotel" />
                                                <asp:BoundField DataField="FROM_DATE" HeaderText ="From Date" />
                                                <asp:BoundField DataField="TO_DATE" HeaderText ="To Date" />
                                                <asp:BoundField DataField="ROOM_TYPE_NAME" HeaderText="Room Type" />            
                                                <asp:BoundField DataField="SINGLE_ROOM_RATE" HeaderText="Single Room Rate" />
                                                <asp:BoundField DataField="DOUBLE_ROOM_RATE" HeaderText="Double Room Rate" />
                                                <asp:BoundField DataField="TRIPLE_ROOM_RATE" HeaderText="Triple Room Rate" />
                                                <asp:BoundField DataField="EXTRA_ADULT_RATE" HeaderText="Extra Adult Rate" />
                                                <asp:BoundField DataField="EXTRA_CWB_COST" HeaderText="Extra CWB Rate" />            
                                                <asp:BoundField DataField="IS_DEFAULT" HeaderText="Is Default" />            
                                                <asp:BoundField DataField="STATUS" HeaderText="Status" /> 
                                                <asp:BoundField DataField="A_MARGIN_IN_AMOUNT" HeaderText="A Margin Amount" />
                                                <asp:BoundField DataField="A_PLUS_MARGIN_IN_AMOUNT" HeaderText="A+ Margin Amount" />
                                                <asp:BoundField DataField="A_PLUS_PLUS_MARGIN_IN_AMOUNT" HeaderText="A++ Margin Amount" />
                                                <asp:BoundField DataField="A_MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="A Margin Percent" />
                                                <asp:BoundField DataField="A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="A+ Margin Percent" />
                                                <asp:BoundField DataField="A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="A++ Margin Percent" />
                                            </Columns>
                                <HeaderStyle CssClass="rgHeader" />
                                </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:UpdatePanel runat="server" ID="upButtons" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button runat="server" ID="btnNew" Text = "New" Width="100px" OnClick="btnNew_Click"/>&nbsp; &nbsp;
                            <asp:Button runat="server" ID="btnBack" Text="Back"  Width="100px"  OnClick="btnBack_Click"/>            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </table>
</asp:Content>
