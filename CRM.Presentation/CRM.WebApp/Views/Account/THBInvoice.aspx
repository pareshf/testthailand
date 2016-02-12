<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="THBInvoice.aspx.cs" Inherits="CRM.WebApp.Views.Account.THBInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
    <style type="text/css">
        .downloadlink
        {
            font-size: 13px !important;
            text-decoration: none;
            font-family: Arial;
            font-weight: bold;
        }
    </style>
    <asp:Label ID="lbltitle" runat="server" Text="Generate Invoice" Width="200px" Font-Bold="true"
        Font-Size="Large" class="pageTitle"></asp:Label>
    <br />
    <div>
        <asp:UpdatePanel ID="UpdatePanel_Generate_Invoice" runat="server" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <contenttemplate>
                <table width="100%">
                    <tr>
                        <td width="40%">
                            <table style="border-collapse: collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5"
                                cellpadding="5">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Agent Name" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList1" runat="server" Width="160px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Agent Name is required."
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="DropDownList1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <%--  <tr>
                                    <td>
                                        <asp:Label ID="lblquote" runat="server" Text="Quotation Refrence No" CssClass="lblstyle" Width="160px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuote" runat="server" Width="160px" AutoPostBack="True" OnTextChanged="txtQuote_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Invoice No" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInvoiceNo" runat="server" Width="160px" AutoPostBack="True" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="GL Date" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox42" runat="server" Width="160px"></asp:TextBox>
                                        <ajax:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBox42"
                                            Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Client Name" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtclientname" runat="server" Width="160px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Client Name is required."
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtclientname"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblorderplacedby" runat="server" Text="Order Placed By" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtorderplacedby" runat="server" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label ID="lblpersonemail" runat="server" Text="Order Placed From Email" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtpersonemail" runat="server" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td>
                                        <asp:Label ID="Label15" runat="server" Text="Package Name" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPackageName" runat="server" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPeriodStayFrom" runat="server" Text="Period Stay From" CssClass="lblstyle"></asp:Label>
                                        &nbsp;<span class="error">*</span>
                                    </td>
                                    <td>
                                        <%-- <asp:TextBox ID="txtPeriodStayFrom" runat="server" Width="160px" onclick="showPopup(this, event);"
                                            onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);" Text="From"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtPeriodStayFrom" runat="server" Width="160px"></asp:TextBox>
                                        <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPeriodStayFrom"
                                            Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Period Stay From is required."
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtPeriodStayFrom"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPeriodStayTo" runat="server" Text="Period Stay To" CssClass="lblstyle"></asp:Label>
                                        &nbsp;<span class="error">*</span>
                                    </td>
                                    <td>
                                        <%-- <asp:TextBox ID="txtPeriodStayTo" runat="server" Width="160px" onclick="showPopup(this, event);"
                                            onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);" Text="To"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtPeriodStayTo" runat="server" Width="160px" OnTextChanged="txtPeriodStayTo_TextChanged"
                                            AutoPostBack="true"></asp:TextBox>
                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPeriodStayTo"
                                            Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Period Stay To is required."
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtPeriodStayTo"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="NoOfNights" runat="server" Text="No Of Nights" CssClass="lblstyle"></asp:Label>
                                        &nbsp;<span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoOfNights" runat="server" Width="160px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="No of nights is required."
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtNoOfNights"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAmount1" runat="server" Text="Amount" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmount" runat="server" Width="160px" AutoPostBack="true" OnTextChanged="tax_onblur"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTax" runat="server" Text="Tax" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTax" runat="server" Width="160px" AutoPostBack="True" Text="0"
                                            OnTextChanged="txtTax_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbltotalAmount1" runat="server" Text="Total Amount" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalAmount" runat="server" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCurrency" runat="server" Text="Currency" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpCurrency" runat="server" Width="160px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td>
                                        <asp:Label ID="lblNoOfPerson" runat="server" Text="No Of Person" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoOfPerson" runat="server" Width="200"></asp:TextBox>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNoOfAdult" runat="server" Text="No Of Adult" CssClass="lblstyle"></asp:Label>
                                        &nbsp;<span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoOfAdult" runat="server" Width="160px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="No of adult is required."
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtNoOfAdult"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNoOfCNB" runat="server" Text="No Of CNB" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoOfCNB" runat="server" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNOofCWB" runat="server" Text="No Of CWB" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoOfCWB" runat="server" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNoOfInfant" runat="server" Text="NO Of Infant" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoOfInfant" runat="server" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblOrderStatus" runat="server" Text="Order Status" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpOrderStatus" runat="server" Width="160px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td>
                                        <asp:Label ID="lblBookingReferanceNo" runat="server" Text="Booking Referance No"
                                            CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBookingReferanceNo" runat="server" Width="200"></asp:TextBox>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="lblInvoiceDescription" runat="server" Text="Exchange Rate" CssClass="lblstyle"></asp:Label>
                                        &nbsp;<span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExRate" runat="server" Width="200" OnTextChanged="txtExRate_TextChanged"
                                            AutoPostBack="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Exchange rate is required."
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtExRate"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPaymentMode" runat="server" Text="Payment Mode" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpPaymentMode" runat="server" Width="160px" AutoPostBack="true"
                                            OnSelectedIndexChanged="drpPayment_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Payment Mode is required."
                                            CssClass="errorclass" ValidationGroup="Required" ControlToValidate="drpPaymentMode"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="t1" runat="server" style="display: none">
                                    <td>
                                        <asp:Label runat="server" Text="Total Credit Limit" ID="lbltotalcreditlmit" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <%--Here bind Database Field in text--%>
                                        <asp:Label runat="server" Text="" ID="lblcreditlimitAmount" CssClass="lblstyle"></asp:Label>
                                        <asp:Label runat="server" Text="USD" ID="Label2" CssClass="lblstyle"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="t2" runat="server" style="display: none">
                                    <td>
                                        <asp:Label runat="server" Text="Credit Limit Used As on Date" ID="lblcrlimituseddate"
                                            CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <%--Here bind Database Field in text--%>
                                        <asp:Label runat="server" Text="" ID="lblcrlimitdate" align="left" CssClass="lblstyle"></asp:Label>
                                        <asp:Label runat="server" Text="USD" ID="lblcrlimitdatecurrency" align="left" CssClass="lblstyle"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="t3" runat="server" style="display: none">
                                    <td>
                                        <asp:Label runat="server" Text="Current Usable" ID="lblcurrentusable" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <%--Here bind Database Field in text--%>
                                        <asp:Label runat="server" Text="" ID="lblcurrentusableamount" align="left" CssClass="lblstyle"></asp:Label>
                                        <asp:Label runat="server" Text="USD" ID="lblcurrentusablecurrency" align="left" CssClass="lblstyle"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="t4" runat="server" style="display: none">
                                    <td>
                                        <asp:Label runat="server" Text="Total Invoice Amount" ID="lblTotalinvoiceamt" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <%--Here bind Database Field in text--%>
                                        <asp:Label runat="server" Text="" ID="lbltotalInvoiceAmount" align="left" CssClass="lblstyle"></asp:Label>
                                        <asp:TextBox runat="server" Text="" ID="txttotalInvoiceAmount" align="left" Visible="false"
                                            Width="140px"></asp:TextBox>
                                        <asp:Label runat="server" Text="USD" ID="lbltotalinvoicecurrency" align="left" CssClass="lblstyle"></asp:Label>
                                    </td>
                                </tr>
                                <%-- <tr  id="Tr1" runat="server" >
                                    <td>
                                        <asp:Label runat="server" Text="Discount Amount" ID="Label15" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                       
                                        <asp:TextBox runat="server" Text="" ID="txtDiscount" align="left" Width="80px" 
                                            ontextchanged="txtDiscount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:Label runat="server" Text="USD" ID="Label21" align="left" CssClass="lblstyle"></asp:Label>
                                    </td>
                                   
                                </tr>--%>
                                <tr id="Tr2" runat="server" style="display: none">
                                    <td>
                                        <asp:Label runat="server" Text="Final Invoice Amount" ID="Label19" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <%--Here bind Database Field in text--%>
                                        <asp:Label runat="server" Text="" ID="lblFinalAmount" align="left" CssClass="lblstyle"></asp:Label>
                                        <asp:Label runat="server" Text="USD" ID="Label22" align="left" CssClass="lblstyle"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblbookingrefNO" runat="server" Text="Booking Refernce No" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td>
                                        <%--Here bind Database Field in text--%>
                                        <asp:TextBox ID="txtBook_ref_no" runat="server" CssClass="textboxstyle" Width="160px"></asp:TextBox><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator2" runat="server" ErrorMessage="Booking Reference no Required."
                                            CssClass="errorclass" ValidationGroup="Required" ControlToValidate="txtBook_ref_no"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="Carbon Copy To" CssClass="lblstyle"></asp:Label><%--&nbsp;<span class="error">*</span>--%>
                                    </td>
                                    <td>
                                        <%--Here bind Database Field in text--%>
                                        <asp:TextBox ID="TextBox21" runat="server" CssClass="textboxstyle" Width="160px"></asp:TextBox><%--</asp:TextBox><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator1" runat="server" ErrorMessage="No of Adult Required "
                                            CssClass="errorclass" ValidationGroup="Required" ControlToValidate="txtBook_ref_no"
                                            Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <asp:HiddenField ID="hiddensalesid" runat="server" />
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" style="padding:0px" Text="Save" ValidationGroup="Required" OnClick="btnSave_Click" />
                                       <asp:Button ID="btnclose1" runat="server" Text="Close-1" style="padding:0px" ValidationGroup="Required" Visible="false" OnClick="btnclose1_Click" OnClientClick="javascript:return confirm('After clicking on close-1 you cannot edit this invoice,are you sure want to proceed?');"/>
                                        <asp:Button ID="btnclose2" runat="server" Text="Close-2" style="padding:0px" ValidationGroup="Required" Visible="false" OnClick="btnclose2_Click" OnClientClick="javascript:return confirm('Are you sure you want to close-2 this invoice?');"/>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSendInvoice" runat="server" Text="Send Invoice To Agent" Visible="false"
                                            OnClick="btnSendInvoice_Click" />
                                        &nbsp;<a id="lnkbtn" runat="server" target="_blank" class="downloadlink" style="display: none;"> Download
                                            Invoice</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="60%" valign="top">
                            <table>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridInvoice" runat="server" AutoGenerateColumns="false" Width="100%"
                                            HeaderStyle-BackColor="Silver">
                                            <Columns>
                                                <asp:TemplateField ControlStyle-Width="200px">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblInvoice" runat="server" Text="Invoice Description" CssClass="lblstyleGIT"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtDescription"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ControlStyle-Width="100px">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblInvoiceAmount" runat="server" Text="Unit No." CssClass="lblstyleGIT"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtUnitNo" OnTextChanged="txtUnitNo_TextChanged"
                                                            AutoPostBack="true"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ControlStyle-Width="100px">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblInvoiceAmountTHB" runat="server" Text="Amount Per Person[THB]"
                                                            CssClass="lblstyleGIT"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtAPP" OnTextChanged="txtAPP_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ControlStyle-Width="100px">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Text="Total Amount[THB]" CssClass="lblstyleGIT"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalAmount" runat="server" CssClass="lblstyleGIT"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ControlStyle-Width="100px" Visible="false">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeatilsID" runat="server" CssClass="lblstyleGIT"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ControlStyle-Width="100px">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Button runat="server" ID="btnHotelRemove" Text="Remove" OnClick="btnRemove_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:Button ID="btnAddRow" runat="server" Text="ADD" OnClick="btnAddRow_Click" Width="100px"
                                CssClass="BtnStyle" />
                            <br />
                        </td>
                    </tr>
                </table>
            </contenttemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel_Generate_Invoice">
        <progresstemplate>
            <div class="TransparentGrayBackground">
            </div>
            <div class="Sample6PageUpdateProgress">
                <asp:Image ID="ajaxLoadNotificationImage" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                    AlternateText="" />
                &nbsp;Please Wait...
            </div>
        </progresstemplate>
    </asp:UpdateProgress>
    <br />
    <form id="form2" runat="server" visible="false">
    <div>
        <rsweb:ReportViewer ID="rptViewer1" runat="server" BorderColor="Silver" BorderStyle="Solid"
            BorderWidth="1px" Height="8.5in" Width="14in">
        </rsweb:ReportViewer>
    </div>
    </form>
</asp:Content>
