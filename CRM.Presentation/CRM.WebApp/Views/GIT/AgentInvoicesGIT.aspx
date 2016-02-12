<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="AgentInvoicesGIT.aspx.cs" Inherits="CRM.WebApp.Views.GIT.AgentInvoicesGIT" %>
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

        <script type="text/javascript">
            var currentTextBox = null;
            var currentDatePicker = null;

            function showPopup(sender, e) {

                try {

                    currentTextBox = sender;
                    var datePicker = $find("<%= RadDatePicker1.ClientID %>");
                    currentDatePicker = datePicker;
                    datePicker.set_selectedDate(currentDatePicker.get_dateInput().parseDate(sender.value));
                    var position = datePicker.getElementPosition(sender);
                    datePicker.showPopup(position.x, position.y + sender.offsetHeight);
                }
                catch (e) { }

            }
            function dateSelected(sender, args) {

                try {

                    if (currentTextBox != null) {

                        currentTextBox.value = args.get_newDate().format('dd/MM/yyyy');
                        currentDatePicker.hidePopup();

                    }

                }
                catch (e) { }

            }


            function parseDate(sender, e) {

                currentDatePicker.hidePopup();
            }

            function PopUpShowing(sender, args) {

                var divmore = document.getElementById('divmore');
                divmore.style.display = 'block';
                divmore.style.position = 'Absolute';
                divmore.style.left = screen.width / 2 - 150;
                divmore.style.top = screen.height / 2 - 150;
                var IMG = document.getElementById("imgexistingimage");
                IMG.src = args.srcElement.all[1].value;
            }

            function disablepopup() {

                var divmore = document.getElementById('divmore');
                divmore.style.display = 'none';
                return false;
            }
        
    </script>
        <div>
        <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
            maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    </div>
<asp:Label ID="lbltitle" runat="server" Text="Generate Invoice" Width="200px" Font-Bold="true"
        Font-Size="Large" class="pageTitle"></asp:Label>
    <br />
    <div id="generate_invoice" runat="server" class="pageTitle">
       <asp:UpdatePanel ID="UpdatePanel_Generate_Invoice" runat="server" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td width="40%">
                            <table style="border-collapse: collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5"
                                cellpadding="5">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Agent Name" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList1" runat="server" Width="160px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblquote" runat="server" Text="Quotation Refrence No" CssClass="lblstyle" Width="160px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuote" runat="server" Width="160px" AutoPostBack="True" OnTextChanged="txtQuote_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Invoice No" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox20" runat="server" Width="160px" AutoPostBack="True" OnTextChanged="txtQuote_TextChanged"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="GL Date" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox42" runat="server" Width="160px" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Client Name" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtclientname" runat="server" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPeriodStayFrom" runat="server" Text="Period Stay From" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPeriodStayFrom" runat="server" Width="160px" onclick="showPopup(this, event);"
                                            onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);" Text="From"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPeriodStayTo" runat="server" Text="Period Stay To" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPeriodStayTo" runat="server" Width="160px" onclick="showPopup(this, event);"
                                            onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);" Text="To"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="NoOfNights" runat="server" Text="No Of Nights" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoOfNights" runat="server" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAmount" runat="server" Text="Amount" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmount" runat="server" Width="160px" AutoPostBack="true"  OnTextChanged="tax_onblur"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTax" runat="server" Text="Tax" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTax" runat="server" Width="160px" AutoPostBack="True" Text="0" OnTextChanged="txtTax_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbltotalAmount" runat="server" Text="Total Amount" CssClass="lblstyle"></asp:Label>
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
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoOfAdult" runat="server" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td>
                                        <asp:Label ID="lblNoOfChild" runat="server" Text="No Of Child" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoOfChild" runat="server" Width="160px"></asp:TextBox>
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
                                        <asp:DropDownList ID="drpOrderStatus" runat="server" Width="160px" AutoPostBack="true" OnSelectedIndexChanged="drpOrderStatus_SelectedIndexChanged" > 
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
                                <%--<tr>
                                    <td valign="top">
                                        <asp:Label ID="lblInvoiceDescription" runat="server" Text="Invoice Description" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInvoiceDescription" runat="server" Width="200" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPaymentMode" runat="server" Text="Payment Mode" CssClass="lblstyle"></asp:Label>&nbsp;<span class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpPaymentMode" runat="server" Width="160px" AutoPostBack="true"
                                            OnSelectedIndexChanged="drpPayment_SelectedIndexChanged">
                                        </asp:DropDownList>

                                       <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator1" runat="server" ErrorMessage="Payment Mode is required."
                                            CssClass="errorclass" ValidationGroup="Required" ControlToValidate="drpPaymentMode"
                                            Display="Dynamic"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr id="t1" runat="server" style="display:none">
                                    <td>
                                        <asp:Label runat="server" Text="Total Credit Limit" ID="lbltotalcreditlmit" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <%--Here bind Database Field in text--%>
                                        <asp:Label runat="server" Text="" ID="lblcreditlimitAmount" CssClass="lblstyle"></asp:Label>
                                        <asp:Label runat="server" Text="USD" ID="Label2" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    
                                </tr>
                                <tr  id="t2" runat="server" style="display:none">
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
                                <tr id="t3" runat="server" style="display:none">
                                    <td>
                                        <asp:Label runat="server" Text="Current Usable" ID="lblcurrentusable" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <%--Here bind Database Field in text--%>
                                        <asp:Label runat="server" Text="" ID="lblcurrentusableamount" align="left" CssClass="lblstyle"></asp:Label>
                                        <asp:Label runat="server" Text="USD" ID="lblcurrentusablecurrency" align="left" CssClass="lblstyle"></asp:Label>
                                    </td>
                                   
                                </tr>
                                <tr  id="t4" runat="server" style="display:none">
                                    <td>
                                        <asp:Label runat="server" Text="Total Invoice Amount" ID="lblTotalinvoiceamt" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <%--Here bind Database Field in text--%>
                                        <asp:Label runat="server" Text="" ID="lbltotalInvoiceAmount" align="left" CssClass="lblstyle"></asp:Label>
                                        <asp:TextBox runat="server" Text="" ID="txttotalInvoiceAmount" align="left" Visible="false" Width="140px"></asp:TextBox>
                                        <asp:Label runat="server" Text="USD" ID="lbltotalinvoicecurrency" align="left" CssClass="lblstyle"></asp:Label>
                                    </td>
                                   
                                </tr>
                                <%--<tr  id="Tr1" runat="server" >
                                    <td>
                                        <asp:Label runat="server" Text="Discount Amount" ID="Label15" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        
                                       
                                        <asp:TextBox runat="server" Text="" ID="txtDiscount" align="left" Width="80px" 
                                            ontextchanged="txtDiscount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:Label runat="server" Text="USD" ID="Label21" align="left" CssClass="lblstyle"></asp:Label>
                                    </td>
                                   
                                </tr>--%>
                                <tr  id="Tr2" runat="server" >
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
                                        <asp:Label ID="lblbookingrefNO" runat="server" Text="Booking Refernce No"
                                            CssClass="lblstyle"></asp:Label>&nbsp;<%--<span class="error">*</span>--%>
                                    </td>
                                    <td>
                                        <%--Here bind Database Field in text--%>
                                        <asp:TextBox ID="txtBook_ref_no" runat="server" CssClass="textboxstyle" Width="160px"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator2" runat="server" ErrorMessage="No of Adult Required "
                                            CssClass="errorclass" ValidationGroup="Required" ControlToValidate="txtBook_ref_no"
                                            Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="Carbon Copy To"
                                            CssClass="lblstyle"></asp:Label><%--&nbsp;<span class="error">*</span>--%>
                                    </td>
                                    <td>
                                        <%--Here bind Database Field in text--%>
                                        <asp:TextBox ID="TextBox21" runat="server" CssClass="textboxstyle" Width="160px"></asp:TextBox>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Required" OnClick="btnSave_Click"  />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSendInvoice" runat="server" Text="Send Invoice To Agent" OnClick="btnSendInvoice_Click" />
                                    </td>

                                    
                                    <td>
                                        <asp:Button ID="btnPostVoucher" runat="server" Text="Post Vouchers" OnClick="btnPostVoucher_Click" Visible="false"/>
                                    </td>


                        </tr> 
                        </table> </td>
                        <td width="60%" valign="top">
                            <table style="border-collapse: collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5"
                                cellpadding="5" width="80%">
                                <tr id="gridheading" runat="server" style="background-color: #f3f3f3">
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Sr. No." CssClass="gridlabel" Width="50px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" Text="Invoice Description" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Unit No." CssClass="gridlabel" Width="80px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Amount(Per Person)" CssClass="gridlabel" ></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label18" runat="server" Text="Total Ammount" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label17" runat="server" Text="Currency" CssClass="gridlabel" Width="80px" ></asp:Label>
                                    </td>
                                    
                                </tr>
                                <%------------------------------------------- ROW 1 -------------------------------------------------------------%>
                                <tr id="row1" runat="server">
                                    <td>
                                        <asp:Label ID="Label20" runat="server" Text="1" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" Width="150px" Text="SINGLE SHARE" ReadOnly="true" ></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox22" runat="server" Width="80px" AutoPostBack="true" OnTextChanged="TextBox22_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox23" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="TextBox23_textchanged" ></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="row1_txt_debit" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="row1_textchanged" ReadOnly = "true"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="row1_drp_currency" runat="server" Width="80px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    
                                </tr>
                                <%------------------------------------------- ROW 2 ------------------------------------------------------------%>
                                <tr id="row2" runat="server" style="display: none">
                                    <td>
                                        <asp:Label ID="Label25" runat="server" Text="2" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="server" Width="150px" Text="DOUBLE SHARE" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox24" runat="server" Width="80px" AutoPostBack="true" OnTextChanged="TextBox24_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox25" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="TextBox25_textchanged" ></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:TextBox ID="TextBox3" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="row2_textchanged" ReadOnly = "true"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="row2_drp_currency" runat="server" Width="80px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                   
                                </tr>
                                <%------------------------------------------- ROW 3 ---------------------------------------------------------%>
                                <tr id="row3" runat="server" style="display: none">
                                    <td>
                                        <asp:Label ID="Label26" runat="server" Text="3" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox4" runat="server" Width="150px" Text="TRIPLE SHARE" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox26" runat="server" Width="80px" AutoPostBack="true" OnTextChanged="TextBox26_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox27" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="TextBox27_textchanged" ></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox5" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="row3_textchanged" ReadOnly = "true"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="row3_drp_currency" runat="server" Width="80px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    
                                </tr>
                                <%------------------------------------------- ROW 4 ---------------------------------------------------%>
                                <tr id="row4" runat="server" style="display: none">
                                    <td>
                                        <asp:Label ID="Label27" runat="server" Text="4" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox6" runat="server" Width="150px" Text="CHILD WITH BED" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox28" runat="server" Width="80px" AutoPostBack="true" OnTextChanged="TextBox28_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox29" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="TextBox29_textchanged" ></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox7" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="row4_textchanged" ReadOnly = "true"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="row4_drp_currency" runat="server" Width="80px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    
                                </tr>
                                <%------------------------------------------- ROW 5 ----------------------------------------------------------------%>
                                <tr id="row5" runat="server" style="display: none">
                                    <td>
                                        <asp:Label ID="Label28" runat="server" Text="5" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox8" runat="server" Width="150px" Text="CHILD WITH NO BED" 
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox30" runat="server" Width="80px" AutoPostBack="true" OnTextChanged="TextBox30_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox31" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="TextBox31_textchanged" ></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox9" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="row5_textchanged" ReadOnly = "true"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="row5_drp_currency" runat="server" Width="80px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    
                                </tr>
                                <%------------------------------------------- ROW 6 ----------------------------------------------------------%>
                                <tr id="row6" runat="server" style="display: none">
                                    <td>
                                        <asp:Label ID="Label29" runat="server" Text="6" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox10" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox32" runat="server" Width="80px" AutoPostBack="true" OnTextChanged="TextBox32_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox33" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="TextBox33_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox11" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="row6_textchanged" ReadOnly = "true"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="row6_drp_currency" runat="server" Width="80px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    
                                </tr>
                                <%------------------------------------------- ROW 7------------------------------------------------------------%>
                                <tr id="row7" runat="server" style="display: none">
                                    <td>
                                        <asp:Label ID="Label30" runat="server" Text="7" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox12" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox34" runat="server" Width="80px" AutoPostBack="true" OnTextChanged="TextBox34_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox35" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="TextBox35_textchanged"></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:TextBox ID="TextBox13" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="row7_textchanged" ReadOnly = "true"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="row7_drp_currency" runat="server" Width="80px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                   
                                </tr>
                                <%------------------------------------------- ROW 8------------------------------------------------------------%>
                                <tr id="row8" runat="server" style="display: none">
                                    <td>
                                        <asp:Label ID="Label31" runat="server" Text="8" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox14" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox36" runat="server" Width="80px" AutoPostBack="true" OnTextChanged="TextBox36_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox37" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="TextBox37_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox15" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="row8_textchanged" ReadOnly = "true"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="row8_drp_currency" runat="server" Width="80px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    
                                </tr>
                                <%------------------------------------------- ROW 9------------------------------------------------------------%>
                                <tr id="row9" runat="server" style="display: none">
                                    <td>
                                        <asp:Label ID="Label32" runat="server" Text="9" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox17" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox38" runat="server" Width="80px" AutoPostBack="true" OnTextChanged="TextBox38_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox39" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="TextBox39_textchanged"></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:TextBox ID="TextBox16" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="row9_textchanged" ReadOnly = "true"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="row9_drp_currency" runat="server" Width="80px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                   
                                </tr>
                                <%------------------------------------------- ROW 10------------------------------------------------------------%>
                                <tr id="row10" runat="server" style="display: none">
                                    <td>
                                        <asp:Label ID="Label33" runat="server" Text="10" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox18" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox40" runat="server" Width="80px" AutoPostBack="true" OnTextChanged="TextBox40_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox41" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="TextBox41_textchanged"></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:TextBox ID="TextBox19" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="row10_textchanged" ReadOnly = "true"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="row10_drp_currency" runat="server" Width="80px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                   
                                </tr>
                                
                                <%------------------------------------------- ROW 11------------------------------------------------------------%>
                                <tr id="row11" runat="server" style="display: none">
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="11" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox43" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox44" runat="server" Width="80px" AutoPostBack="true" OnTextChanged="TextBox44_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox45" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="TextBox45_textchanged"></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:TextBox ID="TextBox46" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="row11_textchanged" ReadOnly = "true"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="row11_drp_currency" runat="server" Width="80px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                   
                                </tr>

                                <%------------------------------------------- ROW 12------------------------------------------------------------%>
                                <tr id="row12" runat="server" style="display: none">
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="12" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox47" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox48" runat="server" Width="80px" AutoPostBack="true" OnTextChanged="TextBox48_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox49" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="TextBox49_textchanged"></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:TextBox ID="TextBox50" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="row12_textchanged" ReadOnly = "true"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="row12_drp_currency" runat="server" Width="80px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                   
                                </tr>
                                <%------------------------------------------- ROW 13------------------------------------------------------------%>
                                <tr id="row13" runat="server" style="display: none">
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="13" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox51" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox52" runat="server" Width="80px" AutoPostBack="true" OnTextChanged="TextBox52_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox53" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="TextBox53_textchanged"></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:TextBox ID="TextBox54" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="row13_textchanged" ReadOnly = "true"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="row13_drp_currency" runat="server" Width="80px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                   
                                </tr>
                                <%------------------------------------------- ROW 14------------------------------------------------------------%>
                                <tr id="row14" runat="server" style="display: none">
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="14" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox55" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox56" runat="server" Width="80px" AutoPostBack="true" OnTextChanged="TextBox56_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox57" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="TextBox57_textchanged"></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:TextBox ID="TextBox58" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="row14_textchanged" ReadOnly = "true"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="row14_drp_currency" runat="server" Width="80px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                   
                                </tr>
                                <%------------------------------------------- ROW 15------------------------------------------------------------%>
                                <tr id="row15" runat="server" style="display: none">
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Text="15" CssClass="gridlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox59" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox60" runat="server" Width="80px" AutoPostBack="true" OnTextChanged="TextBox60_textchanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox61" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="TextBox61_textchanged"></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:TextBox ID="TextBox62" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="row15_textchanged" ReadOnly = "true"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="row15_drp_currency" runat="server" Width="80px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                   
                                </tr>
                            </table>

                            <br />
                            &nbsp; &nbsp;
                            <asp:Button ID="btnadd2" runat="server" Text="ADD" OnClick="btnadd2_Click" Width="100px"
                                CssClass="BtnStyle" />
                            <asp:Button ID="btnadd3" runat="server" Text="ADD" OnClick="btnadd3_Click" Style="display: none"
                                Width="100px" CssClass="BtnStyle" />
                            <asp:Button ID="btnadd4" runat="server" Text="ADD" OnClick="btnadd4_Click" Style="display: none"
                                Width="100px" CssClass="BtnStyle" />
                            <asp:Button ID="btnadd5" runat="server" Text="ADD" OnClick="btnadd5_Click" Style="display: none"
                                Width="100px" CssClass="BtnStyle" />
                            <asp:Button ID="btnadd6" runat="server" Text="ADD" OnClick="btnadd6_Click" Style="display: none"
                                Width="100px" CssClass="BtnStyle" />
                            <asp:Button ID="btnadd7" runat="server" Text="ADD" OnClick="btnadd7_Click" Style="display: none"
                                Width="100px" CssClass="BtnStyle" />
                            <asp:Button ID="btnadd8" runat="server" Text="ADD" OnClick="btnadd8_Click" Style="display: none"
                                Width="100px" CssClass="BtnStyle" />
                            <asp:Button ID="btnadd9" runat="server" Text="ADD" OnClick="btnadd9_Click" Style="display: none"
                                Width="100px" CssClass="BtnStyle" />
                            <asp:Button ID="btnadd10" runat="server" Text="ADD" OnClick="btnadd10_Click" Style="display: none"
                                Width="100px" CssClass="BtnStyle" />
                            <asp:Button ID="btnadd11" runat="server" Text="ADD" OnClick="btnadd11_Click" Style="display: none"
                                Width="100px" CssClass="BtnStyle" />
                            <asp:Button ID="btnadd12" runat="server" Text="ADD" OnClick="btnadd12_Click" Style="display: none"
                                Width="100px" CssClass="BtnStyle" />
                            <asp:Button ID="btnadd13" runat="server" Text="ADD" OnClick="btnadd13_Click" Style="display: none"
                                Width="100px" CssClass="BtnStyle" />
                            <asp:Button ID="btnadd14" runat="server" Text="ADD" OnClick="btnadd14_Click" Style="display: none"
                                Width="100px" CssClass="BtnStyle" />
                            <asp:Button ID="btnadd15" runat="server" Text="ADD" OnClick="btnadd15_Click" Style="display: none"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel_Generate_Invoice">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
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
