<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="MoneyTransferAgent.aspx.cs" Inherits="CRM.WebApp.Views.Sales.MoneyTransferAgent" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="cntIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="cntPageContent" ContentPlaceHolderID="cphPageContent" runat="server">
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
    
    
    
    <script type ="text/javascript" src="../Shared/Javascripts/MoneyTransferAgent.js"></script>
   
    <script type ="text/javascript">
        var signature = '<%=Session["signature"]%>';
        var receivednby = '<%=Session["usersname"]%>';
       
        function pageLoad() {
            MoneyAgentMasterTabelView = $find("<%=radgridmoneytransferagentdetail.ClientID %>").get_masterTableView();
            MoneyAgentMasterCommandName = "Load";
            PAYMENT_SR_NO = getValue("PAYMENT_SRNO");

            var q = window.location.search.substring(1);
            if (q != "") {

               
                CRM.WebApp.webservice.MoneyTransferAgent.GetMoneyAgentDetails(PAYMENT_SR_NO, updateMoneyGrid);

            }
            else { }
           var receivednby = '<%=Session["usersname"]%>';

            var grid = $find("<%=radgridmoneytransferagentdetail.ClientID %>")
            var masterTable = grid.get_masterTableView();

            for (var j = 0; j < masterTable.get_dataItems().length; j++) {

                var recuser;
                masterTable.get_dataItems()[j].findElement("RECEIVED_BY").value = receivednby;
                recuser = receivednby;
                
            }

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
            var IMG = document.getElementById("imgexistingimage");
            IMG.src = args.srcElement.all[1].value;
        }

        function disablepopup() {

            var divmore = document.getElementById('divmore');
            divmore.style.display = 'none';
            return false;
        }


        function bookingPaymentdetails(sender, args) {
            
            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var paymentary = [];
           
            paymentary[15] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
            paymentary[14] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.BOOKING_ID;
            paymentary[13] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.PAYMENT_SR_NO;
            paymentary[1] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_CURR_CODE").value;
            paymentary[2] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_MODE_NAME").value;
            paymentary[3] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("REC_CHEQUE_DD_NO").value;
            paymentary[4] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("AMOUNT").value;
            paymentary[16] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("SERVICE_CHARGE").value;
            paymentary[5] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("BANK_NAME").value;
            paymentary[6] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("BRANCH_NAME").value;
            paymentary[7] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("RECEIPT_NO").value;
            paymentary[8] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("RECEIPT_DATE").value;
            paymentary[9] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("RECEIVED_BY").value;
            paymentary[10] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("FOREIGN_CURRENCY_AGENT_NAME").value;
            paymentary[12] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("CONVERSION_RATE").value;
            paymentary[17] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("Branch").value;
            paymentary[18] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("INR_AMOUNT").value;
            paymentary[19] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("FAM").value;
            paymentary[20] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("Signature").value;
            paymentary[22] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("STATUS_NAME").value;
            for (i = 0; i < 21; i++) {
                if (paymentary[i] == "" || paymentary[i] == 'null') paymentary[i] = 0;

            }
            if (paymentary[20] == signature) {
                try {
                    CRM.WebApp.webservice.MoneyTransferAgent.InsertUpdateBookingPaymentDetails1(paymentary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.MoneyTransferAgent.GetMoneyAgentDetails(PAYMENT_SR_NO, updateMoneyGrid);


                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }
            else {

                alert('Enter Signature Again.')
            }
            var grid = $find("<%=radgridmoneytransferagentdetail.ClientID %>");
            var masterTable = grid.get_masterTableView();
            for (var i = 0; i < masterTable.get_dataItems().length; i++) {

                var recuser = masterTable.get_dataItems()[i].findElement("Signature");
                recuser.value = '';
            }
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

        function nextStep() {
            window.location = "BookingCheckList.aspx?BOOKING_ID=" + BOOKING_ID;
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
            CRM.WebApp.webservice.MoneyTransferAgent.GetMoneyAgentDetails1(TOUR_ID, updateMoneyGrid);
        }
        function BackStep() {
            window.location = "BookingCustomerPayment.aspx?TOUR_ID=" + TOUR_ID;

        }
        function Print() {
            window.location = "MoneyTransferAgentReport.aspx?key=" + TOUR_ID;
        }
    </script>

   </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageBooking" runat="server" Text="Money Transfer Agent"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        $(document).ready(function () {

            var paymentmode = "../../webservice/autocomplete.ashx?key=FETCH_PAYMENT_MODE_FOR_BOOKING_MASTER";
            var bank = "../../webservice/autocomplete.ashx?key=FETCH_BANKNAME_FOR_BOOKING_MASTER";
            var COMPANY = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_COMPANY_NAME_AUTOSEARCH";
            var Agent = "../../webservice/autocomplete.ashx?key=FETCH_AGENT_NAME_AUTOSEARCH";
            var tourshortname = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_SHORT_SHORT_NAME_AUTOSEARCH";
            var tourcode = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_CODE_ON_TOUR_SHORT_NAME?" + globalvalue;
            var status = "../../webservice/autocomplete.ashx?key=GET_PAYMENT_STATUS_FOR_AUTOSEARCH";

            $("#ctl00_cphPageContent_txttourshortname").autocomplete(tourshortname);
            $("#ctl00_cphPageContent_txttourcodename").autocomplete(tourcode);
            for (var i = 1; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridmoneytransferagentdetail_ctl00_ctl" + i + "_Branch").autocomplete(COMPANY);
                $("#ctl00_cphPageContent_radgridmoneytransferagentdetail_ctl00_ctl" + i + "_PAYMENT_MODE_NAME").autocomplete(paymentmode);
                $("#ctl00_cphPageContent_radgridmoneytransferagentdetail_ctl00_ctl" + i + "_BANK_NAME").autocomplete(bank);
                $("#ctl00_cphPageContent_radgridmoneytransferagentdetail_ctl00_ctl" + i + "_FAM").autocomplete(Agent);
                $("#ctl00_cphPageContent_radgridmoneytransferagentdetail_ctl00_ctl" + i + "_STATUS_NAME").autocomplete(status);
            }
        });

    </script>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <div id="divradmastergrid">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lbltourshortname" runat="server" Text="Tour Short Name :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttourshortname" runat="server" Width="250px" onblur="getTourName(this);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbltourcodename" runat="server" Text="Tour Code Name :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttourcodename" runat="server" Width="250px" onblur="getTourId(this);"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnsave" runat="server" Text="Search" Style="color: black;"
                        OnClientClick="getTourData();" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridmoneytransferagentdetail" runat="server" allowmultirowselection="false"
                        allowsorting="True" itemstyle-wrap="false" enableembeddedskins="false" allowautomaticdeletes="True"
                        allowautomaticinserts="True">
                    <MasterTableView AllowMultiColumnSorting="true" EditMode="InPlace" Width="1250px" ClientDataKeyNames="SR_NO">
                    <RowIndicatorColumn>
             </RowIndicatorColumn>
             <Columns>
             <telerik:GridTemplateColumn DataField="SR_NO" HeaderText="sr no" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="SR_NO" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>               
                    <telerik:GridTemplateColumn DataField="BOOKING_ID" HeaderText="Booking Id" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="BOOKING_ID" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="PAYMENT_SR_NO" HeaderText="Payment Sr No" Visible ="False">
                        <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_SR_NO" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOUR_SHORT_NAME" HeaderText="Tour Name" >
                        <ItemTemplate>
                            <asp:TextBox ID="TOUR_SHORT_NAME" runat="server" CssClass="radinput" Style="background-color: LightBlue" ReadOnly="True" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="PAYMENT_CURR_CODE" HeaderText="Currency Name"  >
                        <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_CURR_CODE" runat="server" CssClass="radinput" Style="background-color: LightBlue" ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="PAYMENT_MODE_NAME" HeaderText="▼ Payment Mode Name" >
                        <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_MODE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="REC_CHEQUE_DD_NO" HeaderText="Rec Cheqe DD No" >
                        <ItemTemplate>
                            <asp:TextBox ID="REC_CHEQUE_DD_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                   <telerik:GridTemplateColumn DataField="AMOUNT" HeaderText="Amount" >
                        <ItemTemplate>
                            <asp:TextBox ID="AMOUNT" runat="server" CssClass="radinput" Style="background-color: LightBlue" ReadOnly="True"></asp:TextBox>
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
                        <ItemTemplate>
                            <asp:TextBox ID="RECEIVED_BY" runat="server" CssClass="radinput" Style="background-color: LightBlue" ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="FOREIGN_CURRENCY_AGENT_NAME" HeaderText="MTA" >
                        <ItemTemplate>
                            <asp:TextBox ID="FOREIGN_CURRENCY_AGENT_NAME" runat="server" CssClass="radinput" Style="background-color: LightBlue" ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                   <telerik:GridTemplateColumn DataField="INR_AMOUNT" HeaderText="INR Amount" >
                        <ItemTemplate>
                            <asp:TextBox ID="INR_AMOUNT" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="Branch" HeaderText="Branch" >
                        <ItemTemplate>
                            <asp:TextBox ID="Branch" runat="server" CssClass="radinput" Text ="H.O" Style="background-color: LightBlue" ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                   <telerik:GridTemplateColumn DataField="CONVERSION_RATE" HeaderText="Conversion Rate" >
                        <ItemTemplate>
                            <asp:TextBox ID="CONVERSION_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="SERVICE_CHARGE" HeaderText="Service Charge">
                        <ItemTemplate>
                            <asp:TextBox ID="SERVICE_CHARGE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="FAM" HeaderText="▼ Foreign Agent">
                        <ItemTemplate>
                            <asp:TextBox ID="FAM" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="STATUS_NAME" HeaderText="Status Name" >
                        <ItemTemplate>
                            <asp:TextBox ID="STATUS_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn  HeaderText="Signature" >
                    <HeaderStyle HorizontalAlign="Left" Width="60px"/>
                        <ItemTemplate>
                            <asp:TextBox ID="Signature" runat="server" CssClass="radinput" TextMode="password"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField= "PHOTO">
                        <HeaderStyle Width = "25px" />
                            <ItemTemplate>
                                <a id = "A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="bookingPaymentdetails(this,event); ">
                                    &raquo;
                                </a>
                                
                            </ItemTemplate>
                    
                    </telerik:GridTemplateColumn>

                     </Columns>
                    </MasterTableView>
                    <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true" AllowColumnsReorder="True">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridmoneytransferagent_Command" OnRowSelected="radgridmoneytransferagent_RowSelected" OnRowDblClick="MoneyTransferRowClick"/>
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
                    <asp:Button ID="Btnback" runat="server" Text="Back" OnClientClick="BackStep();"
                        Style="color: black;" />
                </td>
                <td>
                    <asp:Button ID="btnnext" runat="server" Text="Next To Step 7" OnClientClick="nextStep();"
                        Style="color: black;" />
                </td>
                <td>
                    <asp:Button ID="btnsummary" runat="server" Text="Print Payment Summary" OnClientClick="Print();"
                        Style="color: black;" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
