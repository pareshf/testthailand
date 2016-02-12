<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="BookingFormStep2.aspx.cs" Inherits=" CRM.WebApp.Views.Sales.BookingFormStep2" %>

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
    
    <script type ="text/javascript" src="../Shared/Javascripts/BookingFormStep2.js"></script>
    <script type="text/javascript">
        var inq_id;
        var cust_id;
        var tour_name;
        var tour_code;
        var tour_type = 0;
        function pageLoad() {

            inq_id = getValue("INQ_ID");
            cust_id = getValue("CUST_ID");
            tour_name = getValue("TOUR_SNM");
            tour_code = getValue("TOUR_CODE");
            //Fetch tour type from the database.
            $.ajax({ type: "POST", url: "../../webservice/BookingFormStep2.asmx/GetTourType", data: " { 'tour_snm': '" + tour_name + "', 'tour_code': '" + tour_code + "' }", contentType: "application/json; charset=utf-8", dataType: "json", success: function (msg) { tour_type = msg.d; }, error: function (e) { alert(e); } });
            //Fetch booking id from the database.
            $.ajax({ type: "POST", url: "../../webservice/BookingFormStep2.asmx/GetExistingBookingId", data: " { 'inq_id': '" + inq_id + "', 'cust_id': '" + cust_id + "', 'tour_snm': '" + tour_name + "', 'tour_code': '" + tour_code + "' }", contentType: "application/json; charset=utf-8", dataType: "json", success: function (msg) { BOOKING_ID = msg.d; check(); }, error: function (e) { alert(e); } });
            //Fetch Tour Id
            var toursnm = getValue("TOUR_SNM");
            CRM.WebApp.webservice.AutoComplete.searchQueryResult(toursnm);
            var tourcode = getValue("TOUR_CODE");

            $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: tourcode, key: "FETCH_TOUR_CODE?" + globalvalue }, function (data) { TOUR_ID = data; });

            booikngMasterTabelView = $find("<%=radgridbookingmaster.ClientID %>").get_masterTableView();
            bookingMasterCommandName = "Load";
            bookingDetailTabelView = $find("<%=radgridbookinginformationdetail.ClientID %>").get_masterTableView();
            bookingCostInfoTabelView = $find("<%=radgridFamilyCost.ClientID %>").get_masterTableView();
            bookingPaymentTabelView = $find("<%=radgridbookingpaymentdetail.ClientID %>").get_masterTableView();




            //CRM.WebApp.webservice.BookingFormStep2.GetBooking(BOOKING_ID, "0", updatebookinmasterGrid);

            /* Chceck Valid*/

            //            if (VISA == "INVALID") {
            //               
            //                var grid = $find("<%=radgridbookinginformationdetail.ClientID %>");
            //                var masterTable = grid.get_masterTableView();
            //                for (var i = 0; i < masterTable.get_dataItems().length; i++) {

            //                    var type = masterTable.get_dataItems()[i].findElement("VALID_VISA");
            //                    type.Style.ForeColor = Color.Red;

            //                }
            //            }
            //            else if (VISA == "VALID") {

            //                var grid = $find("<%=radgridbookinginformationdetail.ClientID %>");
            //                var masterTable = grid.get_masterTableView();
            //                for (var i = 0; i < masterTable.get_dataItems().length; i++) {

            //                    var type = masterTable.get_dataItems()[i].findElement("VALID_VISA");
            //                    type.Style.ForeColor = Color.Green;

            //                }
            //            }
            //            else { }
        }

        /*Following function check that booking is new or exist*/
        function check() {

            if (BOOKING_ID != 0) {
                CRM.WebApp.webservice.BookingFormStep2.GetBooking(BOOKING_ID, "0", updatebookinmasterGrid);
                CRM.WebApp.webservice.BookingFormStep2.GetBookingInformationDetail(BOOKING_ID, updatebookingdetailGrid);
                CRM.WebApp.webservice.BookingFormStep2.GetCostinfo(BOOKING_ID, updatebookingCostGrid);
                CRM.WebApp.webservice.BookingFormStep2.GetBookingPaymentDetail(BOOKING_ID, updatebookingpaymentGrid);
            }
            else {

                var grid = $find("<%=radgridbookingpaymentdetail.ClientID %>");
                var masterTable = grid.get_masterTableView();
                for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                    var receivednby = '<%=Session["usersname"]%>';
                    var recuser = masterTable.get_dataItems()[i].findElement("RECEIVED_BY");
                    recuser.value = receivednby;
                    var masterTable = grid.get_masterTableView();
                    for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                        var tokenamount = masterTable.get_dataItems()[i].findElement("TOKEN_AMOUNT");

                    }

                }

                var grid = $find("<%= radgridbookingmaster.ClientID %>");
                var masterTable = grid.get_masterTableView();
                for (var i = 0; i < masterTable.get_dataItems().length; i++) {

                    var a = "";
                    // current date display
                    var today = new Date().format('dd/MM/yyyy');
                    var txtdate = masterTable.get_dataItems()[i].findElement("BOOKING_DATE");
                    txtdate.value = today;
                    // user name display 

                    var takenby = '<%=Session["usersname"]%>';
                    var txtuser = masterTable.get_dataItems()[i].findElement("BOOKING_TAKEN_BY");
                    txtuser.value = takenby;



                    // auto gentered booking Code
                    var minimum = 1;
                    var maximum = 100000;
                    var txtbookingcode = masterTable.get_dataItems()[i].findElement("BOOKING_CODE");
                    txtbookingcode.value = "B" + (Math.floor(Math.random() * (maximum - minimum + 1)) + minimum).toString();

                    // tour cost

                    // get cust id from the session

                    var cust_id = getValue("CUST_ID");
                    var txtcustid = masterTable.get_dataItems()[i].findElement("CUST_ID");
                    txtcustid.value = cust_id;
                    CUST_ID = cust_id;

                    //get inquiry_id from the session

                    var inquiry_id = getValue("INQ_ID");
                    var txtinqid = masterTable.get_dataItems()[i].findElement("INQUIRY_ID");
                    txtinqid.value = inquiry_id;


                    var toursnm = getValue("TOUR_SNM");
                    CRM.WebApp.webservice.AutoComplete.searchQueryResult(toursnm);
                    var tourcode = getValue("TOUR_CODE");

                    var txttourid = masterTable.get_dataItems()[i].findElement("TOUR_ID");
                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: tourcode, key: "FETCH_TOUR_CODE?" + globalvalue }, function (data) { txttourid.value = data; /*tourid = data;*/ });


                    //                    var tourcostc1 = masterTable.get_dataItems()[i].findElement("TOTAL_ACTUAL_TOUR_COST_C1");
                    //                    var tourcostc2 = masterTable.get_dataItems()[i].findElement("TOTAL_ACTUAL_TOUR_COST_C2");
                    //                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: inquiry_id, key: "FETCH_TOTAL_QUOTED_COST_C1_DUALPARAM?" + globalvalue + "?" + a }, function (data) { tourcostc1.value = data; });
                    //                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: inquiry_id, key: "FETCH_TOTAL_QUOTED_COST_C2_DUALPARAM?" + globalvalue + "?" + a }, function (data) { tourcostc2.value = data; });

                }
            }
        }

        function addcustreldetails(sender, args) {

            var e = document.getElementById("ctl00_cphPageContent_drpCustomer");
            var strUser = e.options[e.selectedIndex].value;
            var grid = $find("<%= radgridbookingmaster.ClientID %>");
            var masterTable = grid.get_masterTableView();
            for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                var BOOKINGID = masterTable.get_dataItems()[i].findElement("BOOKING_ID").value;
                var bookingID = BOOKINGID;
            }
            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var ary = [];
            ary[0] = strUser;
            ary[1] = bookingID;
            ary[2] = "True";
            try {

                CRM.WebApp.webservice.BookingFormStep2.UpdateBookingDetails(ary);
                alert('Record Save Successfully');

            }
            catch (e) {
                alert('Wrong Data Inserted');
            }


        }
        function removecustreldetails(sender, args) {

            var e = document.getElementById("ctl00_cphPageContent_drpCustomer");
            var strUser = e.options[e.selectedIndex].value;
            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var ary = [];
            ary[0] = strUser;
            ary[1] = "False";
            try {

                CRM.WebApp.webservice.BookingFormStep2.UpdateBookingDetails(ary);
                alert('Record Save Successfully');

            }
            catch (e) {
                alert('Wrong Data Inserted');

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

        function bookingmaster(sender, args) {


            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var ary = [];
            ary[0] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_ID").value;
            ary[1] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("TOUR_ID").value;
            ary[2] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("CUST_ID").value;
            ary[3] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("INQUIRY_ID").value;
            ary[4] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_CODE").value;
            ary[5] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_DATE").value;
            ary[6] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_TAKEN_BY").value;
            ary[7] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("BRANCH_NAME").value;
            ary[8] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("AGENT_NAME").value;
            ary[9] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("ADD_REQ_SERVICE").value;
            ary[10] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("DOCS_FOR_VISA_HANDED_OVER_DATE").value;
            ary[11] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_BAL_TOUR_MADE_BY_DATE").value;
            //            ary[12] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("TOTAL_ACTUAL_TOUR_COST_C1").value;
            //            ary[13] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("TOTAL_ACTUAL_TOUR_COST_C2").value;
            //            ary[14] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("BALANCE_TO_BE_PAID_C1").value;
            //            ary[15] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("BALANCE_TO_BE_PAID_C2").value;
            ary[16] = booikngMasterTabelView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_STATUS_NAME").value;



            for (i = 0; i < 17; i++) {
                if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
            }

            try {
                CRM.WebApp.webservice.BookingFormStep2.InsertUpdateBookingMaster(ary);
                alert('Record Save Successfully');
                CRM.WebApp.webservice.BookingFormStep2.GetBooking(BOOKING_ID, "-1", updatebookinmasterGrid);
            }
            catch (e) {
                alert('Wrong Data Inserted');

            }
            var grid = $find("<%=radgridbookingpaymentdetail.ClientID %>");
            var masterTable = grid.get_masterTableView();
            for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                var receivednby = '<%=Session["usersname"]%>';
                var recuser = masterTable.get_dataItems()[i].findElement("RECEIVED_BY");
                recuser.value = receivednby;

            }

        }


        function bookingdetails(sender, args) {

            currentRowIndex = sender.parentNode.parentNode.rowIndex;

            var ary = [];
            ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.BOOKING_DETAIL_ID;
            ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.BOOKING_ID;
            //ary[0] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_DETAIL_ID").value;
            //ary[1] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_ID").value;
            ary[2] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("RELATION").value;
            ary[3] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("VALID_VISA").value;
            ary[4] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("VALID_PASSPORT").value;
            ary[5] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("ROOM_TYPE_NAME").value;
            ary[6] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("CLASS_NAME").value;
            ary[7] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("CATEGORY_DESC").value;
            ary[8] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("BORDING_FROM").value;
            ary[9] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("ARRIVAL_TO").value;
            ary[10] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("DEPARTURE_DATE").value;
            ary[11] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("ARRIVAL_DATE").value;
            ary[12] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("MEAL").value;
            ary[13] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("SHARE_ROOM_IN_HOTEL").value;
            ary[14] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("SHARE_ROOM_IN_CRUISE").value;
            ary[15] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_STATUS_NAME").value;
            ary[16] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("IS_CHECKED").value;
            ary[17] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("ROOM_NO").value;


            for (i = 0; i < 17; i++) {
                if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
            }
            if (ary[16] == "" || ary[16] == 'null') ary[16] = "F";

            try {

                CRM.WebApp.webservice.BookingFormStep2.InsertUpdateBookingInformation(ary);
                alert('Record Save Successfully');
                CRM.WebApp.webservice.BookingFormStep2.GetBookingInformationDetail(BOOKING_ID, updatebookinmasterGrid);
            }
            catch (e) {
                alert('Wrong Data Inserted');

            }

        }





        function bookingPaymentdetails(sender, args) {

            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var paymentary = [];

            paymentary[0] = bookingPaymentTabelView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_DATE").value;
            paymentary[1] = bookingPaymentTabelView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_CURRENCY_CODE").value;
            paymentary[2] = bookingPaymentTabelView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_MODE_NAME").value;
            paymentary[3] = bookingPaymentTabelView.get_dataItems()[currentRowIndex - 1].findElement("REC_CHEQUE_DD_NO").value;
            paymentary[4] = bookingPaymentTabelView.get_dataItems()[currentRowIndex - 1].findElement("TOKEN_AMOUNT").value;
            paymentary[5] = bookingPaymentTabelView.get_dataItems()[currentRowIndex - 1].findElement("BANK_NAME").value;
            paymentary[6] = bookingPaymentTabelView.get_dataItems()[currentRowIndex - 1].findElement("BRANCH_NAME").value;
            paymentary[7] = bookingPaymentTabelView.get_dataItems()[currentRowIndex - 1].findElement("RECEIPT_NO").value;
            paymentary[8] = bookingPaymentTabelView.get_dataItems()[currentRowIndex - 1].findElement("RECEIPT_DATE").value;
            paymentary[9] = bookingPaymentTabelView.get_dataItems()[currentRowIndex - 1].findElement("RECEIVED_BY").value;
            paymentary[10] = bookingPaymentTabelView.get_dataItems()[currentRowIndex - 1].findElement("FOREIGN_CURRENCY_AGENT_NAME").value;
            paymentary[11] = bookingPaymentTabelView.get_dataItems()[currentRowIndex - 1].findElement("IN_THE_NAME_OF").value;
            paymentary[12] = bookingPaymentTabelView.get_dataItems()[currentRowIndex - 1].findElement("CONVERSION_RATE").value;
            //paymentary[13] = bookingPaymentTabelView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_SRNO").value;
            paymentary[13] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.PAYMENT_SRNO;
            paymentary[14] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.BOOKING_ID;

            for (i = 0; i < 15; i++) {
                if (paymentary[i] == "" || paymentary[i] == 'null') paymentary[i] = 0;

            }
            try {
                if (window.confirm('Check This  "' + paymentary[4] + '"  Amount is Perfect?') == true) {
                    if (window.confirm('Are You Damn sure  "' + paymentary[4] + '"  Amount is Perfect?') == true) {

                        if (TOKEN == null || TOKEN == 0) {

                            CRM.WebApp.webservice.BookingFormStep2.InsertUpdateBookingPaymentDetails(paymentary);
                            alert('Record Save Successfully');
                            CRM.WebApp.webservice.BookingFormStep2.GetBookingPaymentDetail(BOOKING_ID, updatebookingpaymentGrid);
                            CRM.WebApp.webservice.BookingFormStep2.GetBooking(BOOKING_ID, "0", updatebookinmasterGrid);
                        }
                        else {
                            alert('Now You cant Update Payment Details')
                        }
                    }
                }
                else {

                }
            }
            catch (e) {
                alert('Wrong Data Inserted');
            }


        }

        var totamt = "";
        var inrflamingo = "";
        function getTotalAmount(sender) {
            totamt = sender.value;
        }
        function getInrAmount(sender) {
            inrflamingo = sender.value;
        }
        function getInrForforex(sender) {
            if (totamt == "" || totamt == 'null') totamt = 0;
            if (inrflamingo == "" || inrflamingo == 'null') inrflamingo = 0;
            sender.value = Number(totamt) - Number(inrflamingo);
        }
        function costInformation(sender, args) {

            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var costary = [];
            costary[0] = bookingCostInfoTabelView.get_dataItems()[currentRowIndex - 1].findElement("RELATION").value;
            costary[1] = bookingCostInfoTabelView.get_dataItems()[currentRowIndex - 1].findElement("FAMILY_COST").value;
            costary[2] = bookingCostInfoTabelView.get_dataItems()[currentRowIndex - 1].findElement("INR_FOR_FLAMINGO").value;
            costary[3] = bookingCostInfoTabelView.get_dataItems()[currentRowIndex - 1].findElement("INR_FOR_FOREX").value;
            costary[4] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.INQUIRY_ID;

            for (i = 0; i < 5; i++) {
                if (costary[i] == "" || costary[i] == 'null') costary[i] = 0;
            }
            try {
                CRM.WebApp.webservice.BookingFormStep2.InsertCostInfoForAgegroup(costary);
                alert('Record Save Successfully');
                CRM.WebApp.webservice.BookingFormStep2.GetCostinfo(BOOKING_ID, updatebookingCostGrid);

            }
            catch (e) {
                alert('Wrong Data Inserted');
            }

        }

        function print() {
            if (tour_type == 24) {
                //Call report of international tour.
                window.open('BookingReport.aspx?key=' + BOOKING_ID);
            }
            else if (tour_type == 25) {
                //Call report of domestic tour.
                window.open('BookingReport2.aspx?key=' + BOOKING_ID);
            }

        }
        function Redirect() {
            window.location = "HotelRoomAllocation.aspx?INQ_ID=" + inq_id + "&CUST_ID=" + cust_id + "&BOOKING_ID=" + BOOKING_ID + "&TOUR_ID=" + TOUR_ID;
        }

        function NextPageCruise() {

            window.location = "CruiseRoomAllocation.aspx?&BOOKING_ID=" + BOOKING_ID + "&TOUR_ID=" + TOUR_ID;
        }

        function RedirectStep1() {
            window.location = "BookingFormStep1.aspx?INQ_ID=" + inq_id + "&TOUR_SNM=" + tour_name + "&TOUR_CODE=" + tour_code + "&BOOKING_ID=" + BOOKING_ID;
        }

    </script>
</telerik:radcodeblock>
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        $(document).ready(function () {

            var bookingstatus = "../../webservice/autocomplete.ashx?key=FETCH_BOOKING_STATUS_FOR_BOOKING_MASTER_AUTOSEARCH";
            var roomtype = "../../webservice/autocomplete.ashx?key=FETCH_ROOM_TYPE_FOR_HOTEL_AUTOSEARCH";
            var flightclass = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_FLIGHT_CLASS_AUTOSEARCH";
            var cabinecategory = "../../webservice/autocomplete.ashx?key=FETCH_CRUISE_CABINE_CATEGORY_AUTOSEARCH";
            var paymentmode = "../../webservice/autocomplete.ashx?key=FETCH_PAYMENT_MODE_FOR_BOOKING_MASTER";
            var bank = "../../webservice/autocomplete.ashx?key=FETCH_BANKNAME_FOR_BOOKING_MASTER";
            var currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_TOUR";
            var startcity = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            var endcity = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            var agentname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_AGENT_NAME";
            var relation = "../../webservice/autocomplete.ashx?key=FETCH_RELATION_DETAILS_AUTOSEARCH";
            var meal = "../../webservice/autocomplete.ashx?key=FETCH_MEAL_DATA_FOR_BOOKINGMASTER_AUTOSEARCH";
            var foregiagent = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_AGENT_NAME";

            for (var i = 1; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridbookingmaster_ctl00_ctl" + i + "_BOOKING_STATUS_NAME").autocomplete(bookingstatus);
                $("#ctl00_cphPageContent_radgridbookingmaster_ctl00_ctl" + i + "_AGENT_NAME").autocomplete(agentname);

                $("#ctl00_cphPageContent_radgridbookinginformationdetail_ctl00_ctl" + i + "_ROOM_TYPE_NAME").autocomplete(roomtype);
                $("#ctl00_cphPageContent_radgridbookinginformationdetail_ctl00_ctl" + i + "_CLASS_NAME").autocomplete(flightclass);
                $("#ctl00_cphPageContent_radgridbookinginformationdetail_ctl00_ctl" + i + "_CATEGORY_DESC").autocomplete(cabinecategory);
                $("#ctl00_cphPageContent_radgridbookinginformationdetail_ctl00_ctl" + i + "_RELATION").autocomplete(relation);
                $("#ctl00_cphPageContent_radgridbookinginformationdetail_ctl00_ctl" + i + "_BORDING_FROM").autocomplete(startcity);
                $("#ctl00_cphPageContent_radgridbookinginformationdetail_ctl00_ctl" + i + "_ARRIVAL_TO").autocomplete(endcity);
                $("#ctl00_cphPageContent_radgridbookinginformationdetail_ctl00_ctl" + i + "_BOOKING_STATUS_NAME").autocomplete(bookingstatus);
                $("#ctl00_cphPageContent_radgridbookinginformationdetail_ctl00_ctl" + i + "_MEAL").autocomplete(meal);

                $("#ctl00_cphPageContent_radgridbookingpaymentdetail_ctl00_ctl" + i + "_PAYMENT_CURRENCY_CODE").autocomplete(currency);
                $("#ctl00_cphPageContent_radgridbookingpaymentdetail_ctl00_ctl" + i + "_PAYMENT_MODE_NAME").autocomplete(paymentmode);
                $("#ctl00_cphPageContent_radgridbookingpaymentdetail_ctl00_ctl" + i + "_BANK_NAME").autocomplete(bank);
                $("#ctl00_cphPageContent_radgridbookingpaymentdetail_ctl00_ctl" + i + "_FOREIGN_CURRENCY_AGENT_NAME").autocomplete(foregiagent);
            }
        });

    </script>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageBooking" runat="server" Text="Booking Master Step - 2"></asp:Literal>
    </div>
    <br />
    <br />
    <div>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridbookingmaster" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="1" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True" pagerstyle mode="NextPrevAndNumeric">
                    <MasterTableView AllowMultiColumnSorting="true" EditMode="InPlace" Width="2500px">
                    <RowIndicatorColumn>
             </RowIndicatorColumn>
             <Columns>
                
                    <telerik:GridTemplateColumn DataField="INQUIRY_ID" HeaderText="Inquiry Id" >
                        <ItemTemplate>
                            <asp:TextBox ID="INQUIRY_ID" runat="server" CssClass="radinput" Text ="0" Style="background-color: LightBlue" ReadOnly="True" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="BOOKING_CODE" HeaderText="Booking Code" >
                        <ItemTemplate>
                            <asp:TextBox ID="BOOKING_CODE" runat="server" CssClass="radinput" Style="background-color: LightBlue" ReadOnly="True" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn DataField="BOOKING_DATE" HeaderText="Booking Date" >
                        <ItemTemplate>
                            <asp:TextBox ID="BOOKING_DATE" runat="server" CssClass="radinput" Style="background-color: LightBlue" ReadOnly="True" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BOOKING_TAKEN_BY" HeaderText="Booking Taken By">
                        <ItemTemplate>
                            <asp:TextBox ID="BOOKING_TAKEN_BY" runat="server" CssClass="radinput" Style="background-color: LightBlue" ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BRANCH_NAME" HeaderText="Branch Name">
                        <ItemTemplate>
                            <asp:TextBox ID="BRANCH_NAME" runat="server" CssClass="radinput" Text ="H.O" Style="background-color: LightBlue" ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="AGENT_NAME" HeaderText="▼ Agent Name">
                        <ItemTemplate>
                            <asp:TextBox ID="AGENT_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="ADD_REQ_SERVICE" HeaderText="Add Request Service">
                        <ItemTemplate>
                            <asp:TextBox ID="ADD_REQ_SERVICE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="DOCS_FOR_VISA_HANDED_OVER_DATE" HeaderText="Docs For Visa Handed Over Date">
                        <ItemTemplate>
                            <asp:TextBox ID="DOCS_FOR_VISA_HANDED_OVER_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="PAYMENT_BAL_TOUR_MADE_BY_DATE" HeaderText="Payment Bal Tour Made By Date">
                        <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_BAL_TOUR_MADE_BY_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                   
                     <telerik:GridTemplateColumn DataField="BOOKING_STATUS_NAME" HeaderText="▼ Family Booking Status">
                        <ItemTemplate>
                            <asp:TextBox ID="BOOKING_STATUS_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CUST_ID" HeaderText="Cust Id" >
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" Text ="0"  Style="background-color: LightBlue" ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BOOKING_ID" HeaderText="Booking Id"  >
                        <ItemTemplate>
                            <asp:TextBox ID="BOOKING_ID" runat="server" CssClass="radinput" Text ="0" ViewStateMode = "Enabled" Style="background-color: LightBlue" ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOUR_ID" HeaderText="Tour Id" >
                        <ItemTemplate>
                            <asp:TextBox ID="TOUR_ID" runat="server" CssClass="radinput" Text ="0" Style="background-color: LightBlue" ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                      <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField= "PHOTO">
                        <HeaderStyle Width = "25px" />
                            <ItemTemplate>
                                <a id = "More" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="bookingmaster(this,event);">
                                    &raquo;
                                </a>
                                
                            </ItemTemplate>
                    
                    </telerik:GridTemplateColumn>

                     </Columns>
                    </MasterTableView>
                    <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridbookingmaster_Command" OnRowSelected="radgridbookingmaster_RowSelected" OnRowDblClick="BookingRowClick"/>
                <Selecting AllowRowSelect="True"/>
            </ClientSettings>
         </telerik:radgrid>
                </td>
            </tr>
        </table>
        <br />
        <div class="pageTitle" style="float: left">
            <asp:Literal ID="Literal1" runat="server" Text="Booking Information Detail"></asp:Literal>
        </div>
        <%--<table>
            <tr>
                <td>
                    <asp:DropDownList ID="drpCustomer" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClientClick="addcustreldetails(this, event);" />
                </td>
                <td>
                    <asp:Button ID="btnRemove" runat="server" Text="Remove" OnClientClick="removecustreldetails(this, event);" />
                </td>
            </tr>
        </table>--%>
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridbookinginformationdetail" runat="server" allowpaging="true"
                        allowmultirowselection="false" allowsorting="True" pagesize="10" itemstyle-wrap="false"
                        enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
                        <MasterTableView AllowMultiColumnSorting="true" EditMode="InPlace" Width="2500px">
                             <RowIndicatorColumn>
                             </RowIndicatorColumn>
                                <Columns>
                  <telerik:GridTemplateColumn DataField="BOOKING_DETAIL_ID" HeaderText="Booking Detail Id" Visible = "False">
                        <ItemTemplate>
                            <asp:TextBox ID="BOOKING_DETAIL_ID" runat="server" CssClass="radinput" Text="0"></asp:TextBox>
                        </ItemTemplate>
                 </telerik:GridTemplateColumn>
                 <telerik:GridTemplateColumn DataField="BOOKING_ID" HeaderText="Booking Id" Visible = "False">
                        <ItemTemplate>
                            <asp:TextBox ID="BOOKING_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                 </telerik:GridTemplateColumn>
                 <telerik:GridTemplateColumn DataField="REL_SURNAME" HeaderText="Surname" >
                        <ItemTemplate>
                            <asp:TextBox ID="REL_SURNAME" runat="server" CssClass="radinput" Style="background-color: LightBlue" ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="REL_NAME" HeaderText="Name" >
                        <ItemTemplate>
                            <asp:TextBox ID="REL_NAME" runat="server" CssClass="radinput" Style="background-color: LightBlue" ReadOnly="True" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="MEAL" HeaderText="▼ Meal" >
                        <ItemTemplate>
                            <asp:TextBox ID="MEAL" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="RELATION" HeaderText="▼ Age Group" >
                        <ItemTemplate>
                            <asp:TextBox ID="RELATION" runat="server" CssClass="radinput" Style = "ba"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                   <telerik:GridTemplateColumn DataField="ROOM_TYPE_NAME" HeaderText="▼ Room Type Name" >
                        <ItemTemplate>
                            <asp:TextBox ID="ROOM_TYPE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="ROOM_NO" HeaderText="Room No" >
                        <ItemTemplate>
                            <asp:TextBox ID="ROOM_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CATEGORY_DESC" HeaderText="▼ Cruise Cabin Category Desc" >
                        <ItemTemplate>
                            <asp:TextBox ID="CATEGORY_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn DataField="IS_CHECKED" HeaderText="Travelling?" >
                        <ItemTemplate>
                            <asp:TextBox ID="IS_CHECKED" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn DataField="VALID_VISA" HeaderText="Valid Visa" >
                        <ItemTemplate>
                            <asp:TextBox ID="VALID_VISA" runat="server" CssClass="radinput" ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="VALID_PASSPORT" HeaderText="Valid Passport" >
                        <ItemTemplate>
                            <asp:TextBox ID="VALID_PASSPORT" runat="server" CssClass="radinput"  ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     
                     <telerik:GridTemplateColumn DataField="CLASS_NAME" HeaderText="▼ Class Name" >
                        <ItemTemplate>
                            <asp:TextBox ID="CLASS_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     
                     <telerik:GridTemplateColumn DataField="BORDING_FROM" HeaderText="▼ Boarding From" >
                        <ItemTemplate>
                            <asp:TextBox ID="BORDING_FROM" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="ARRIVAL_TO" HeaderText="▼ Arrival To" >
                        <ItemTemplate>
                            <asp:TextBox ID="ARRIVAL_TO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="DEPARTURE_DATE" HeaderText="Departure Date" >
                        <ItemTemplate>
                            <asp:TextBox ID="DEPARTURE_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="ARRIVAL_DATE" HeaderText="Arrival Date" >
                        <ItemTemplate>
                            <asp:TextBox ID="ARRIVAL_DATE" runat="server" CssClass="radinput"  onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     
                    <telerik:GridTemplateColumn DataField="SHARE_ROOM_IN_HOTEL" HeaderText="Share Room In Hotel" >
                        <ItemTemplate>
                            <asp:TextBox ID="SHARE_ROOM_IN_HOTEL" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="SHARE_ROOM_IN_CRUISE" HeaderText="Share Room In Cruise" >
                        <ItemTemplate>
                            <asp:TextBox ID="SHARE_ROOM_IN_CRUISE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BOOKING_STATUS_NAME" HeaderText="▼ Personal Booking Status" >
                        <ItemTemplate>
                            <asp:TextBox ID="BOOKING_STATUS_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField= "PHOTO">
                        <HeaderStyle Width = "25px" />
                            <ItemTemplate>
                                <a id = "A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="bookingdetails(this,event);">
                                    &raquo;
                                </a>
                          
                            </ItemTemplate>
                    
                    </telerik:GridTemplateColumn>
                                </Columns>
                        </MasterTableView>
                        <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridbookinginformationdetail_Command" OnRowSelected="radgridbookinginformationdetail_RowSelected" OnRowDblClick="BookingInformationRowClick"/>
                <Selecting AllowRowSelect="True"/>
            </ClientSettings>
                        </telerik:radgrid>
                </td>
            </tr>
        </table>
        <br />
        <div class="pageTitle" style="float: left">
            <asp:Literal ID="Literal3" runat="server" Text="Cost Information"></asp:Literal>
        </div>
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridFamilyCost" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
                        <MasterTableView AllowMultiColumnSorting="true" EditMode="InPlace" Width="1000px" ClientDataKeyNames="PAYMENT_SRNO">
                        <RowIndicatorColumn>
                        </RowIndicatorColumn>
                        <Columns>
                            <telerik:GridTemplateColumn DataField="BOOKING_ID" HeaderText="Booking Id" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="BOOKING_ID" runat="server" CssClass="radinput"></asp:TextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="INQUIRY_ID" HeaderText="Inquiry Id"  Visible="false">
                            <ItemTemplate>
                                <asp:TextBox ID="INQUIRY_ID" runat="server"   ></asp:TextBox>
                            </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="CUST_NAME" HeaderText="Customer Name" >
                                <ItemTemplate>
                                    <asp:TextBox ID="CUST_NAME" runat="server" CssClass="radinput" ReadOnly="True"></asp:TextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="RELATION" HeaderText="Age Group" >
                                <ItemTemplate>
                                    <asp:TextBox ID="RELATION" runat="server" CssClass="radinput" ReadOnly="True"></asp:TextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="FAMILY_COST" HeaderText="Total INR Amount" >
                                <ItemTemplate>
                                    <asp:TextBox ID="FAMILY_COST" runat="server" CssClass="radinput" onblur = "getTotalAmount(this);"></asp:TextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="INR_FOR_FLAMINGO" HeaderText="INR Flamingo" >
                                <ItemTemplate>
                                    <asp:TextBox ID="INR_FOR_FLAMINGO" runat="server" CssClass="radinput" onblur="getInrAmount(this);"></asp:TextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn DataField="INR_FOR_FOREX" HeaderText="INR For Forex" >
                                <ItemTemplate>
                                    <asp:TextBox ID="INR_FOR_FOREX" runat="server" CssClass="radinput" onfocus="getInrForforex(this);"></asp:TextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField= "PHOTO">
                            <HeaderStyle Width = "25px" />
                            <ItemTemplate>
                                <a id = "A3" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="costInformation(this,event);">
                                    &raquo;
                                </a>
                          
                            </ItemTemplate>
                    
                    </telerik:GridTemplateColumn>

                        </Columns>
                        </MasterTableView>
                 <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true" AllowColumnsReorder="True">
                    <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                    <ClientEvents OnCommand="radgridFamilyCost_Command" OnRowSelected="radgridFamilyCost_RowSelected" OnRowDblClick="CostinformationRowClick"/>
                    <Selecting AllowRowSelect="true"/>
                </ClientSettings>          
                </telerik:radgrid>
                </td>
            </tr>
        </table>
        <br />
        <div class="pageTitle" style="float: left">
            <asp:Literal ID="Literal2" runat="server" Text="Booking Payment Detail"></asp:Literal>
        </div>
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridbookingpaymentdetail" runat="server" allowpaging="true"
                        allowmultirowselection="false" allowsorting="True" pagesize="1" itemstyle-wrap="false"
                        enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
                    <MasterTableView AllowMultiColumnSorting="true" EditMode="InPlace" Width="2500px" ClientDataKeyNames="PAYMENT_SRNO">
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
                     
                     <telerik:GridTemplateColumn DataField="PAYMENT_CURRENCY_CODE" HeaderText="▼ Currency Name" >
                        <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_CURRENCY_CODE" runat="server" CssClass="radinput"></asp:TextBox>
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
                   <%--  <telerik:GridTemplateColumn DataField="AMMOUNT" HeaderText="Amount" >
                        <ItemTemplate>
                            <asp:TextBox ID="AMMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>
                     <telerik:GridTemplateColumn DataField="TOKEN_AMOUNT" HeaderText="Token Amount" >
                        <ItemTemplate>
                            <asp:TextBox ID="TOKEN_AMOUNT" runat="server" CssClass="radinput"></asp:TextBox>
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
                     <telerik:GridTemplateColumn DataField="FOREIGN_CURRENCY_AGENT_NAME" HeaderText="▼ Foreign Currency Agent Name" >
                        <ItemTemplate>
                            <asp:TextBox ID="FOREIGN_CURRENCY_AGENT_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="IN_THE_NAME_OF" HeaderText="In The Name Of" >
                        <ItemTemplate>
                            <asp:TextBox ID="IN_THE_NAME_OF" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <%--<telerik:GridTemplateColumn DataField="INR_AMMOUNT" HeaderText="INR Amount" >
                        <ItemTemplate>
                            <asp:TextBox ID="INR_AMMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>
                    <%--<telerik:GridTemplateColumn DataField="TAX" HeaderText="Tax" >
                        <ItemTemplate>
                            <asp:TextBox ID="TAX" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>
                   <%-- <telerik:GridTemplateColumn DataField="BILL_NUMBER" HeaderText="Bill No" >
                        <ItemTemplate>
                            <asp:TextBox ID="BILL_NUMBER" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>
                    <telerik:GridTemplateColumn DataField="CONVERSION_RATE" HeaderText="Conversion Rate" >
                        <ItemTemplate>
                            <asp:TextBox ID="CONVERSION_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <%--<telerik:GridTemplateColumn DataField="GST" HeaderText="Gst" >
                        <ItemTemplate>
                            <asp:TextBox ID="GST" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>

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
                <ClientEvents OnCommand="radgridbookingpaymentdetail_Command" OnRowSelected="radgridbookingpaymentdetail_RowSelected" OnRowDblClick="PaymentRowClick"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>          
     </telerik:radgrid>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="print();" Style="color: black;
                        font-weight: bold;" />
                </td>
                <td>
                    <asp:Button ID="btnnextpage" runat="server" Text="Next To Step 3" OnClientClick="Redirect();"
                        Style="color: black; font-weight: bold;" />
                </td>
                <%--  <td>
                    <asp:Button ID="btnnextcruisepage" runat="server" Text="Next To CruiseRoomAllocation" OnClientClick="NextPageCruise();"
                        Style="color: black; font-weight: bold;" />
                </td>--%>
                <td>
                    <asp:Button ID="Btnback" runat="server" Text="Go Back" OnClientClick="RedirectStep1();"
                        Style="color: black;" />
                </td>
            </tr>
        </table>
</asp:Content>
