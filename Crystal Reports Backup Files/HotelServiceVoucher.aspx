<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="HotelServiceVoucher.aspx.cs" Inherits="CRM.WebApp.Views.Account.HotelServiceVoucher" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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

    <asp:Label ID="Label55" runat="server" Text="Hotel Service Voucher" class="pageTitle"
        Width="200px" Font-Bold="true" Font-Size="Large"></asp:Label>
    <br />
    <br />
    <asp:UpdatePanel ID="upHotelPty" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Comapany Agent" CssClass="lblstyle"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" Width="205" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
                            AutoPostBack="true" TabIndex="2">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="Voucher No." CssClass="lblstyle"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtVoucherNo" runat="server" Width="200px" TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="Against Sales Invoice" CssClass="lblstyle"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList3" runat="server" Width="205" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"
                            AutoPostBack="true" TabIndex="2">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Client Name" CssClass="fieldlabel"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtClientname" runat="server" Width="200px" TabIndex="3"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="City Name" CssClass="lblstyle"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="ddr_CityName_SelectedIndexChanged"
                            AutoPostBack="true" Width="205px" TabIndex="4">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Hotel Name" CssClass="lblstyle"></asp:Label>
                    </td>
                    <td style="width: 200px">
                        <asp:DropDownList ID="ddrpty_HotelName" runat="server" OnSelectedIndexChanged="ddr_HotelName_SelectedIndexChanged"
                            AutoPostBack="true" Width="205px" TabIndex="5">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Room Type" CssClass="lblstyle"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddrpty_RoomType" runat="server" Width="205px" TabIndex="6">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Arrival Date" CssClass="lblstyle"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtpty_CheckIn" runat="server" CssClass="textboxstyle" Width="100px"
                            OnTextChanged="txtpty_CheckIn_TextChanged" AutoPostBack="true" TabIndex="7"></asp:TextBox>
                        <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender21" runat="server" targetcontrolid="txtpty_CheckIn"
                            watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                        </ajax:textboxwatermarkextender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Departure Date" CssClass="lblstyle"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtpty_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                            OnTextChanged="txtpty_CheckOut_TextChanged" AutoPostBack="true" TabIndex="8"></asp:TextBox>
                        <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender22" runat="server" targetcontrolid="txtpty_CheckOut"
                            watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                        </ajax:textboxwatermarkextender>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Total Rooms" CssClass="fieldlabel"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txttotalroom" runat="server" CssClass="textboxstyle" Width="200px"
                            TabIndex="9"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="No of Nights" CssClass="fieldlabel"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtnonights" runat="server" CssClass="textboxstyle" Width="200px"
                            TabIndex="9"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="No. Of Rooms Single" CssClass="fieldlabel"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNoroomSingle" runat="server" CssClass="textboxstyle" Width="200px"
                            TabIndex="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="No. Of Rooms Double / Twin" CssClass="fieldlabel"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNoroomDouble" runat="server" CssClass="textboxstyle" Width="200px"
                            TabIndex="11"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="No. Of Rooms Triple" CssClass="fieldlabel"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNoroomTriple" runat="server" CssClass="textboxstyle" Width="200px"
                            TabIndex="12"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" TabIndex="13" />
                    </td>
                    <td>
                        <a id="lnkbtn" runat="server" style="font-family: Verdana; font-size: 12px; display: none;">
                            Download Voucher</a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <form id="form2" runat="server" visible="false">
    <div>
        <rsweb:ReportViewer ID="rptViewer1" runat="server" BorderColor="Silver" BorderStyle="Solid"
            BorderWidth="1px" Height="8.5in" Width="14in">
        </rsweb:ReportViewer>
    </div>
    </form>
    </div>
</asp:Content>
