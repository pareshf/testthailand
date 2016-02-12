<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="PurchaseVoucher.aspx.cs" Inherits="CRM.WebApp.Views.Account.PurchaseVoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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

    <asp:Label ID="lbltitle" runat="server" Text="Purchase Invoice" Width="400px" Font-Bold="true"
        Font-Size="Large" class="pageTitle"></asp:Label>
    <br />
    <div id="Header">
        <asp:UpdatePanel ID="UpdatePanel_Generate_Invoice" runat="server" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <table style="border-collapse: collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5"
                    cellpadding="5">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Supplier Type" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpsupplier_type" runat="server" Width="250px" AutoPostBack="true"
                                OnSelectedIndexChanged="drpsupplier_type_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Supplier" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpsupplier" runat="server" Width="250px" AutoPostBack="true"
                                OnSelectedIndexChanged="drpsupplier_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label27" runat="server" Text="Purchase Invoice No." CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPurchaseVoucher" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Against Sales Invoice" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpinvoice_no" runat="server" Width="250px" AutoPostBack="true"
                                OnSelectedIndexChanged="drpinvoice_no_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Invoice Date" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtinvoice_date" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Due Date" CssClass="lblstyle"></asp:Label><span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtdue_date" runat="server" Width="250px" OnTextChanged="txtdue_date_TextChanged"></asp:TextBox>
                            <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender22" runat="server" targetcontrolid="txtdue_date"
                                watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                            </ajax:textboxwatermarkextender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="GL Date" CssClass="lblstyle"></asp:Label><span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtgl_date" runat="server" Width="250px" OnTextChanged="txtgl_date_TextChanged"></asp:TextBox>
                            <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender1" runat="server" targetcontrolid="txtgl_date"
                                watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                            </ajax:textboxwatermarkextender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label20" runat="server" Text="No of Adults" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtno_of_adults" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label21" runat="server" Text="No of CNB" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtno_of_cnb" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text="No of CWB" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtno_of_cwb" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label23" runat="server" Text="No of Infant" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtno_of_infant" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:Label ID="lblhotel" Text="" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                <table width="100%" style="border-collapse: collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC;"
                    cellspacing="5" cellpadding="5">
                    <tr id="gridheading" runat="server">
                        <td>
                            <table style="border-collapse: collapse; display: none; border-color: #E6E6E6 #E6E6E6 #CCCCCC"
                                cellspacing="5" cellpadding="5" width="100%" id="hotel_table" runat="server">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Peroid Stay From" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtperiod_stay_from" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Peroid Stay To" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtperiod_stay_to" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="No Of Nights" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtno_of_nights" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label24" runat="server" Text="Room Type" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_room_type" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="No Of Room Single" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtsingle_room" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="No Of Room Double" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtdouble_room" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="No Of Room Triple" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txttriple_room" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>

                            <table style="border-collapse: collapse; display: none; border-color: #E6E6E6 #E6E6E6 #CCCCCC"
                                cellspacing="5" cellpadding="5" width="100%" id="tblDiffType" runat="server">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDate" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                    </tr>

                                    <tr>

                                    <td>
                                        <asp:Label ID="lblType" runat="server" Text="" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtType" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>

                                 <tr id="trTime" runat="server" visible="false">

                                    <td>
                                        <asp:Label ID="Label28" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                       <telerik:RadTimePicker ID="RadTimePicker1" runat="server" Width="90px">
                                            <TimeView TimeFormat="h:mm tt">
                                            </TimeView>
                                            <DateInput DisplayDateFormat="hh:mm tt">
                                            </DateInput>
                                        </telerik:RadTimePicker>
                                    </td>
                                </tr>
                                </table>
                        </td>
                        <%-- TRANSFER OR SIGHT SEEING--%>
                        <td valign="top">
                            <asp:DataList ID="dltp_ss" runat="server">
                                <HeaderTemplate>
                                    <table border="1" style="border-collapse: collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC"
                                        cellspacing="5" cellpadding="5" width="100%">
                                        <tr id="gridheading" runat="server">
                                            <td style="width: 100px">
                                                <asp:Label ID="Label26" runat="server" Text="Sr. No." CssClass="gridlabel" Width="100px"></asp:Label>
                                            </td>
                                            <td style="width: 300px">
                                                <asp:Label ID="Label13" runat="server" Text="Description" CssClass="gridlabel" Width="250px"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:Label ID="Label14" runat="server" Text="Flag" CssClass="gridlabel" Width="100px"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:Label ID="Label25" runat="server" Text="Date" CssClass="gridlabel" Width="100px"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:Label ID="Label15" runat="server" Text="Currency" CssClass="gridlabel" Width="100px"></asp:Label>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:Label ID="Label16" runat="server" Text="Amount" CssClass="gridlabel" Width="150px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="1000px" id="hoteldata" runat="server" border="1" style="border-collapse: collapse;
                                        border-color: #E6E6E6 #E6E6E6 #CCCCCC;" cellspacing="5" cellpadding="5">
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:Label ID="lblsrno" runat="server" Text="" Width="100px"></asp:Label>
                                            </td>
                                            <td style="width: 300px">
                                                <asp:Label ID="lbldescription" runat="server" Text='<%# Bind("NAME") %>' Width="250px"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="drpflag" runat="server" Width="100px">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>SIC</asp:ListItem>
                                                    <asp:ListItem>PVT</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txtdate" runat="server" Width="100px" Text='<%# Bind("DATE") %>'
                                                    Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="drpcurrency" runat="server" Width="100px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:TextBox ID="txtamount" runat="server" Width="150px" OnTextChanged="txtamount_TextChanged"
                                                    AutoPostBack="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <div>
        <asp:UpdatePanel ID="update_payment" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:Label ID="lblpayment" Text="PAYMENT" runat="server" CssClass="headlabel"></asp:Label>
                <table width="1000px" id="hoteldata" runat="server" border="1" style="border-collapse: collapse;
                    border-color: #E6E6E6 #E6E6E6 #CCCCCC;" cellspacing="5" cellpadding="5">
                    <tr>
                        <td>
                            <asp:Label ID="lblcurrency" runat="server" Text="Currency" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpcurrency_payment" runat="server" Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label17" runat="server" Text="Amount" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_amount" runat="server" Width="250px" Enabled="false" OnTextChanged="txt_amount_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label18" runat="server" Text="Tax" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txttax" runat="server" Width="250px" OnTextChanged="txttax_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label19" runat="server" Text="Total Amount" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txttotal_amount" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
