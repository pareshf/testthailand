<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="BookingCustomerPayment.aspx.cs" Inherits="CRM.WebApp.Views.Sales.BookingCustomerPayment" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="cphIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="cphPageContent" ContentPlaceHolderID="cphPageContent" runat="server">
    <style>
        .disable
        {
            display: none;
            width: 0px;
            height: 0px;
            border: 0px solid #fff;
        }
        div.RadGrid_Default .rgFilterRow td
        {
            background-color: #e5e5e5;
        }
        div.RadGrid_Default .rgHeader
        {
            background-color: #F3F3F3;
            background-position: 0 0;
            background-repeat: repeat-x !important;
            border-color: #E6E6E6 #E6E6E6 #CCCCCC;
            color: #636363;
            font-family: Arial;
            font-size: 12px;
            font-style: normal;
            font-weight: bold;
            height: 25px;
            line-height: 16px;
            text-align: left;
            text-decoration: none;
            text-indent: 0;
        }
        
        .RadMenu_Default
        {
            background-color: #fff;
            border: solid 0px #fff;
        }
        .RadMenu_Default UL.rmRootGroup
        {
            background-color: #fff;
            border: solid 0px #fff;
            padding: 2px;
        }
        .RadMenu rmLink
        {
            padding-left: 0px;
        }
        .RadMenu_Default .rmLink
        {
            color: #000;
            text-decoration: none;
            font-family: Verdana;
            font-size: 8pt;
            padding-top: 2px;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink .rmText
        {
            border: solid 0px #fff;
            padding-top: 2px;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink .rmText:hover
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmVertical .rmItem:hover
        {
            border: solid 0px #fff;
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink:active
        {
            border: solid 0px #fff;
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink:active
        {
            border: solid 0px #fff;
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmGroup .rmItem .rmLink
        {
            color: #000;
            padding-top: 2px;
        }
        .RadMenu_Default .rmGroup .rmItem .rmLink:hover
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadPanelBar_Default .rpSlide
        {
            padding-left: 2px;
        }
        .RadMenu_Default .rmLink:active
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmLink:active
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .radinput
        {
            width: 100%;
            border: 0px solid #c2c2c2;
        }
    </style>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
    
        <script type="text/javascript" src="../Shared/Javascripts/CustomerPayment.js"></script>
   
    <script type="text/javascript">
        var receivednby = '<%=Session["usersname"]%>';
        var signature = '<%=Session["signature"]%>';

        var today = new Date().format('dd/MM/yyyy');
        var newtoday = new Date().format('dd/MM/yyyy');
        var recuser = "";
        var pdate = "";
        var rdate = "";
        function pageLoad() {

            TOUR_ID = getValue("TOUR_ID");
            var TOUR_ID;
            var booking_id;
            booking_id = getValue("BOOKING_ID");

            PaymentTableView = $find("<%= radgridbookingpaymentdetail.ClientID %>").get_masterTableView();
            CustomerTableView = $find("<%= radgridcustomer.ClientID %>").get_masterTableView();

            var q = window.location.search.substring(1);
            if (q != "") {
               
                CRM.WebApp.webservice.CustomerBookingPaymentWebService.GetBookingCustomerDetail(booking_id, updatecustomer);
            }
            else {

            }
            recuvedbytodate();

        }
        function recuvedbytodate() {

            var receivednby = '<%=Session["usersname"]%>';
            var today = new Date().format('dd/MM/yyyy');
            var newtoday = new Date().format('dd/MM/yyyy');


            var grid = $find("<%=radgridbookingpaymentdetail.ClientID %>")
            var masterTable = grid.get_masterTableView();
            for (var j = 0; j < masterTable.get_dataItems().length; j++) {
                masterTable.get_dataItems()[j].findElement("RECEIVED_BY").value = receivednby;
                masterTable.get_dataItems()[j].findElement("PAYMENT_DATE").value = today;
                masterTable.get_dataItems()[j].findElement("RECEIPT_DATE").value = newtoday;
            }
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
        function getValue(variable) {

            var query = window.location.search.substring(1);
            var vars = query.split("&");
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split("=");
                if (pair[0] == variable) {
                    return pair[1];
                }
            }


        }
        function deletepayment() {

            CRM.WebApp.webservice.CustomerBookingPaymentWebService.delPayment(PAYMENT_SRNO);
            CRM.WebApp.webservice.CustomerBookingPaymentWebService.GetPaymentDetail(BOOKING_ID, updatepayment);

        }
        function delCustomer() {
            var table = $find("<%= radgridcustomer.ClientID %>").get_masterTableView().get_element();
            var row = table.rows[currentRowIndex]; table.deleteRow(currentRowIndex);
            var dataItem = $find(row.id);
            if (dataItem) {
                dataItem.dispose();
                Array.remove($find("<%= radgridcustomer.ClientID %>").get_masterTableView()._dataItems, dataItem);
            }
            var gridItems = $find("<%= radgridcustomer.ClientID %>").get_masterTableView().get_dataItems();
            CRM.WebApp.webservice.CustomerBookingPaymentWebService.delCustomer(BOOKING_ID);
            gridItems[gridItems.length - 1].set_selected(true);
        }
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

        }

        function disablepopup() {

            var divmore = document.getElementById('divmore');
            divmore.style.display = 'none';
            return false;
        }
        function bookingPaymentdetails(sender, args) {

            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var paymentary = [];
            paymentary[0] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_DATE").value;
            paymentary[1] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_CURRENCY_CODE").value;
            paymentary[2] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_MODE_NAME").value;
            paymentary[3] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("REC_CHEQUE_DD_NO").value;
            paymentary[4] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("BANK_NAME").value;
            paymentary[5] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("BRANCH_NAME").value;
            paymentary[6] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("RECEIPT_NO").value;
            paymentary[7] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("RECEIPT_DATE").value;
            paymentary[8] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("RECEIVED_BY").value;
            paymentary[9] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("FOREIGN_CURRENCY_AGENT_NAME").value;
            paymentary[10] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("IN_THE_NAME_OF").value;
            paymentary[11] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.PAYMENT_SRNO;
            paymentary[12] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("AMOUNT").value;
            paymentary[13] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("BILL_NUMBER").value;
            paymentary[14] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("TAX").value;
            paymentary[15] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("GST").value;
            paymentary[16] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("INR_AMT").value;
            paymentary[17] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("TOKEN_AMOUNT").value;
            paymentary[18] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("Signature").value;
            paymentary[20] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.BOOKING_ID;
            paymentary[21] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("STATUS_NAME").value;
            //INR_AMOUNT,BILL_NUMBER


            for (i = 0; i < 21; i++) {
                if (paymentary[i] == "" || paymentary[i] == 'null') paymentary[i] = 0;

            }
            if (paymentary[16] != 0 && paymentary[12] != 0) {

                alert("You Cant Enter Both Received Amount & INR Forex Amount .");

            }
            else {

                if (paymentary[18] == signature)
                    try {
                        CRM.WebApp.webservice.CustomerBookingPaymentWebService.InsertUpdateBookingPaymentDetail(paymentary);
                        alert('Record Save Successfully');
                        CRM.WebApp.webservice.CustomerBookingPaymentWebService.GetPaymentDetail(BOOKING_ID, updatepayment);


                    }
                    catch (e) {
                        alert('Wrong Data Inserted');
                    }
                else {

                    alert('Enter Correct Signature Password .');
                }
                var grid = $find("<%=radgridbookingpaymentdetail.ClientID %>");
                var masterTable = grid.get_masterTableView();
                for (var i = 0; i < masterTable.get_dataItems().length; i++) {

                    var recuser = masterTable.get_dataItems()[i].findElement("Signature");
                    recuser.value = '';
                }
            }
        }


        function getTourName(sender) {
            var value = sender.value;
            CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
        }

        function getTourId(sender) {
            var tourcode = sender.value;
            $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: tourcode, key: "FETCH_TOUR_CODE?" + globalvalue }, function (data) { TOUR_ID = data; });
        }
        function getTourData() {

            CRM.WebApp.webservice.CustomerBookingPaymentWebService.GetCustomerDetail(TOUR_ID, updatecustomer);
            recuvedbytodate();
        }
        function Redirect() {

            if (MTA != "" && INR_AMOUNT != 0) {
                window.location = "MoneyTransferAgent.aspx?PAYMENT_SRNO=" + PAYMENT_SRNO;
            }
            else if (ccode == "INR") {
                window.location = "BookingCheckList.aspx?BOOKING_ID=" + BOOKING_ID;
            }
            else if (ccode != "INR" && INR_AMOUNT == 0 && MTA == "") {
                window.location = "BookingCheckList.aspx?BOOKING_ID=" + BOOKING_ID;

            }
            else {
                window.location = "BookingCheckList.aspx?BOOKING_ID=" + BOOKING_ID;

            }
        }

        function addnewPayment() {
            CRM.WebApp.webservice.CustomerBookingPaymentWebService.InsertNewpayment(BOOKING_ID, receivednby, today, newtoday);
            CRM.WebApp.webservice.CustomerBookingPaymentWebService.GetPaymentDetail(BOOKING_ID, updatepayment);
        }
        function checkValidation(sender) {
            var INRAMT;
            var MTA;
            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            INRAMT = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_CURRENCY_CODE").value;
            if (INRAMT != 'INR') {
                PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("FOREIGN_CURRENCY_AGENT_NAME").readOnly = false;
            }
            else if (INRAMT = 'INR') {
                PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("FOREIGN_CURRENCY_AGENT_NAME").readOnly = true;
                alert('You Cant Enter MTA');
            }
            else { }
        }
        function Print() {
            window.open("PaymentSummary.aspx?key=" + BOOKING_ID);
        }
        function agrement() {
            window.location = "CruiseRoomAllocation.aspx?BOOKING_ID=" + BOOKING_ID + "&" + "TOUR_ID=" + TOUR_ID;
        }
        function PrintTourSummary() {
            window.open("TourPaymentSummary.aspx?key=" + TOUR_ID);
        }

        
        </script>
         </telerik:radcodeblock>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Customer Payment"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var tourshortname = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_SHORT_SHORT_NAME_AUTOSEARCH";
            var tourcode = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_CODE_ON_TOUR_SHORT_NAME?" + globalvalue;
            var foregiagent = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_AGENT_NAME";
            var paymentmode = "../../webservice/autocomplete.ashx?key=FETCH_PAYMENT_MODE_FOR_BOOKING_MASTER";
            var bank = "../../webservice/autocomplete.ashx?key=FETCH_BANKNAME_FOR_BOOKING_MASTER";
            var currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_TOUR";
            var Status = "../../webservice/autocomplete.ashx?key=GET_PAYMENT_STATUS_FOR_AUTOSEARCH";

            $("#ctl00_cphPageContent_txttourname").autocomplete(tourshortname);
            $("#ctl00_cphPageContent_txttourCode").autocomplete(tourcode);
            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridbookingpaymentdetail_ctl00_ctl" + i + "_PAYMENT_MODE_NAME").autocomplete(paymentmode);
                $("#ctl00_cphPageContent_radgridbookingpaymentdetail_ctl00_ctl" + i + "_BANK_NAME").autocomplete(bank);
                $("#ctl00_cphPageContent_radgridbookingpaymentdetail_ctl00_ctl" + i + "_FOREIGN_CURRENCY_AGENT_NAME").autocomplete(foregiagent);
                $("#ctl00_cphPageContent_radgridbookingpaymentdetail_ctl00_ctl" + i + "_PAYMENT_CURRENCY_CODE").autocomplete(currency);
                $("#ctl00_cphPageContent_radgridbookingpaymentdetail_ctl00_ctl" + i + "_STATUS_NAME").autocomplete(Status);

            }

        });       
    </script>
    <table>
        <tr>
            <td>
                <asp:Label ID="lbltourname" runat="server" Text="Tour Name :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txttourname" runat="server" Width="250px" onblur="getTourName(this);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbltourcode" runat="server" Text="Tour Code :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txttourCode" runat="server" Width="250px" onblur="getTourId(this);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnsave" runat="server" Text="Search" Style="color: black;" OnClientClick="getTourData();" />
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td>
                <asp:Button ID="Button1" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Customer Detail?'))return false; delCustomer(); return false;"
                    Text="Delete" runat="server" Visible="false" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridcustomer" runat="server" allowpaging="false" allowmultirowselection="false"
                    allowsorting="True" pagesize="25" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="BOOKING_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="400px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="BOOKING_ID" DataField="BOOKING_ID" HeaderText="BOOKING_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="BOOKING_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TOUR_ID" DataField="TOUR_ID" HeaderText="TOUR_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="TOUR_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_ID" DataField="CUST_ID" HeaderText="CUST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="TITLE_DESC" DataField="TITLE_DESC" HeaderText=" TITLE">
                          <ItemTemplate>
                            <asp:TextBox ID="TITLE_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUSTOMER_NAME" DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="CUSTOMER_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <%--<telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewcompany(this,event);">
                            &raquo;
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>
                    </Columns>
                    </MasterTableView>
               <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridcustomer_Command" OnRowSelected="radgridcustomer_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td>
                <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Customer Payment Detail?'))return false; deletepayment(); return false;"
                    Text="Delete" runat="server" Visible="false" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridbookingpaymentdetail" runat="server" allowpaging="false"
                    allowmultirowselection="false" allowsorting="false" pagesize="15" itemstyle-wrap="false"
                    enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
                    <MasterTableView AllowMultiColumnSorting="true" EditMode="InPlace" Width="2000px" ClientDataKeyNames="PAYMENT_SRNO">
                    <RowIndicatorColumn>
             </RowIndicatorColumn>
             <Columns>               
                    <telerik:GridTemplateColumn DataField="BOOKING_ID" HeaderText="Booking Id" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="BOOKING_ID" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="PAYMENT_SRNO" HeaderText="Payment Sr No" Visible ="False">
                        <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_SRNO" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                   <telerik:GridTemplateColumn DataField="PAYMENT_DATE" HeaderText="Payment Date" >
                        <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     
                     <telerik:GridTemplateColumn DataField="PAYMENT_CURRENCY_CODE" HeaderText="▼ Currency" >
                     <HeaderStyle HorizontalAlign="Left" Width="70px"/>
                        <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_CURRENCY_CODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn DataField="INR_AMT" HeaderText="INR For Forex" >
                        <ItemTemplate>
                            <asp:TextBox ID="INR_AMT" runat="server" CssClass="radinput" onblur="checkValidation(this);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn DataField="AMOUNT" HeaderText="Amount Recived" >
                        <ItemTemplate>
                            <asp:TextBox ID="AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn DataField="FOREIGN_CURRENCY_AGENT_NAME" HeaderText="▼ MTA" >
                        <ItemTemplate>
                            <asp:TextBox ID="FOREIGN_CURRENCY_AGENT_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn DataField="CONVERSION_RATE" HeaderText="Conversion Rate" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CONVERSION_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn DataField="PAYMENT_MODE_NAME" HeaderText="▼ Payment Mode" >
                        <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_MODE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="REC_CHEQUE_DD_NO" HeaderText="Rec Cheqe DD No" >
                        <ItemTemplate>
                            <asp:TextBox ID="REC_CHEQUE_DD_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     
                     <telerik:GridTemplateColumn DataField="TOKEN_AMOUNT" HeaderText="Token Amount" Visible="true">
                        <ItemTemplate>
                            <asp:TextBox ID="TOKEN_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="BANK_NAME" HeaderText="▼ Bank Name" >
                        <ItemTemplate>
                            <asp:TextBox ID="BANK_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="BRANCH_NAME" HeaderText="Branch Name" >
                        <ItemTemplate>
                            <asp:TextBox ID="BRANCH_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="RECEIPT_NO" HeaderText="Receipt No" >
                     <HeaderStyle HorizontalAlign="Left" Width="70px"/>
                        <ItemTemplate>
                            <asp:TextBox ID="RECEIPT_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="RECEIPT_DATE" HeaderText="Receipt Date" >
                        <ItemTemplate>
                            <asp:TextBox ID="RECEIPT_DATE" runat="server" CssClass="radinput"  onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="RECEIVED_BY" HeaderText="Received By" >
                     <HeaderStyle HorizontalAlign="Left" Width="70px"/>
                        <ItemTemplate>
                            <asp:TextBox ID="RECEIVED_BY" runat="server" CssClass="radinput" Style="background-color: LightBlue" ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     

                    <telerik:GridTemplateColumn DataField="IN_THE_NAME_OF" HeaderText="In The Name Of" >
                        <ItemTemplate>
                            <asp:TextBox ID="IN_THE_NAME_OF" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn DataField="TAX" HeaderText="Tax" >
                    <HeaderStyle HorizontalAlign="Left" Width="60px"/>
                        <ItemTemplate>
                            <asp:TextBox ID="TAX" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BILL_NUMBER" HeaderText="Bill No" >
                    <HeaderStyle HorizontalAlign="Left" Width="50px"/>
                        <ItemTemplate>
                            <asp:TextBox ID="BILL_NUMBER" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
        
                    <telerik:GridTemplateColumn DataField="GST" HeaderText="Gst" >
                    <HeaderStyle HorizontalAlign="Left" Width="60px"/>
                        <ItemTemplate>
                            <asp:TextBox ID="GST" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn  HeaderText="Signature Password" >
                    <HeaderStyle HorizontalAlign="Left" Width="60px"/>
                        <ItemTemplate>
                            <asp:TextBox ID="Signature" runat="server" CssClass="radinput" TextMode="password"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn DataField="STATUS_NAME" HeaderText="Payment Status" >
                    <HeaderStyle HorizontalAlign="Left" Width="60px"/>
                        <ItemTemplate>
                            <asp:TextBox ID="STATUS_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField= "PHOTO">
                        <HeaderStyle Width = "25px" />
                            <ItemTemplate>
                                <a id = "A2" href="#"  style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="bookingPaymentdetails(this,event); ">
                                    &raquo;
                                </a>
                                <%--<div id="divmore" style="width:300px;display:none;background-color:#fff;border:1px solid #c2c2c2;"><br />
                        <div class="pageTitle" style="float: left">
                        <asp:Label ID="Literal2" runat="server" Text="Enter Password :"></asp:Label>
                        <asp:TextBox ID="flupld" runat="server" Width="100px" TextMode="Password"></asp:TextBox><br /><br />
                       <asp:Button ID="btnok" runat="server" Text="Done !" OnClientClick="Check(this,event)"/>&nbsp;&nbsp; <asp:Button ID="btncalcel" runat="server" Text="Cancel" OnClientClick="return disablepopup()"/>
                       <br /><br /> </div>--%>
                            </ItemTemplate>
                    
                    </telerik:GridTemplateColumn>
                     </Columns>
                    </MasterTableView>
                    <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true" AllowColumnsReorder="True">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridbookingpaymentdetail_Command" OnRowSelected="radgridbookingpaymentdetail_RowSelected" OnRowDblClick="CustomerPayementRowClick"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
               </telerik:radgrid>
                <asp:LinkButton ID="lbAddPayment" runat="server" Text="Add New Payment Detail" OnClientClick="addnewPayment();"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Button ID="btnagree" runat="server" Text="Back" OnClientClick="agrement();"
                    Style="color: black;" Visible="true" />
            </td>
            <td>
                <asp:Button ID="btnnext" runat="server" Text="Next To Step 6" OnClientClick="Redirect();"
                    Style="color: black;" />
            </td>
            <td>
                <asp:Button ID="btnsummary" runat="server" Text="Customer Payment Summary" OnClientClick="Print();"
                    Style="color: black;" />
            </td>

            <td>
                <asp:Button ID="btntourpayment" runat="server" Text="Tour Payment Summary" OnClientClick="PrintTourSummary();"
                    Style="color: black;" />
            </td>
        </tr>
    </table>
</asp:Content>
