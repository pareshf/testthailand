<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="Tour2.aspx.cs" Inherits="CRM.WebApp.Views.Sales.Tour2" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <%--Css of Grid Texboxes--%>
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
    <%--Telrik RadCodeblock--%>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">

    <script type ="text/javascript" src="../Shared/Javascripts/Tour2.js"></script>
    <script type="text/javascript">
        //Page Load Function

        function pageLoad() {

            TOUR_ID = getValue("TOUR_ID");
            HotelTableView = $find("<%= radgridHotelDetails.ClientID %>").get_masterTableView();
            AirlineTableView = $find("<%= radgridAirlineDetail.ClientID %>").get_masterTableView();
            CruiseTableView = $find("<%= radgridCruiseDetail.ClientID %>").get_masterTableView();
            CRM.WebApp.webservice.TourMasterWebService.GetHotelDetailsByTOUR_ID(TOUR_ID, updateHotelDetailsGrid);
            CRM.WebApp.webservice.TourMasterWebService.GetFlightDetailOnTourId(TOUR_ID, updateFlightDetailGrid);
            CRM.WebApp.webservice.TourMasterWebService.GetCruiseDetailOnTourId(TOUR_ID, updateCruiseDetailGrid);
        }
        
        //Function for Getting Value From the Query String

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


        // Date And Common Funcion Start
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
       
        

        // Date And Common Funcion End

        // Function For Insert Update Hotel Grid Start
        function newrowaddedforHotel(sender, args) {

            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var ary = [];
            ary[0] = TOUR_ID;

            ary[1] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("FROM_DATE").value;
            ary[2] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("TO_DATE").value;
            ary[3] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;
            ary[4] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("CITY_NAME").value;
            ary[5] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("HOTEL_NAME").value;
            ary[6] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;
            ary[7] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.ROOM_TYPE_NAME.value;
            //ary[8] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("NO_OF_ROOMS").value;
            ary[9] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("AMOUNT").value;
            ary[10] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("TAX").value;
            ary[11] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("GST").value;
            ary[12] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("TOTAL_AMOUNT").value;
            ary[13] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("REMARKS").value;
            ary[14] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_REQUEST_TO").value;
            ary[15] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("ROOM_TO_BE_BLOCKED").value;
            ary[16] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECK_REQUEST_TO").value;
            ary[17] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECK_REQUEST_DATE").value;
            ary[18] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECK_COMMENTS").value;
            ary[19] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("TOTAL_ROOM_BLOCKED").value;
            ary[20] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("TIME_LIMIT").value;
            ary[21] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("APPROVED_BY").value;
            ary[22] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("BOOKED_BY").value;
            ary[23] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_STATUS").value;
            ary[24] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_REQUEST_DATE").value;
            ary[25] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("TOTAL_ROOM_ALLOTEED").value;
            ary[26] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("PARTIAL_ROOM_ALLOTED").value;
            ary[27] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("TOTAL_ADULT_ALLOTED").value;
            ary[28] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("TOTAL_CWB_ALLOTED").value;
            ary[29] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("TOTAL_CNB_ALLOTED").value;
            ary[30] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("TOTAL_INFANT_ALLOTED").value;
            ary[31] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("AVALIBLE_ROOM").value;

            if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
            if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
            if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
            if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;
            if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
            if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;
            if (ary[6] == "" || ary[6] == 'null') ary[6] = 0;
            if (ary[7] == "" || ary[7] == 'null') ary[7] = 0;
            if (ary[8] == "" || ary[8] == 'null') ary[8] = 0;
            if (ary[9] == "" || ary[9] == 'null') ary[9] = 0;
            if (ary[10] == "" || ary[10] == 'null') ary[10] = 0;
            if (ary[11] == "" || ary[11] == 'null') ary[11] = 0;
            if (ary[12] == "" || ary[12] == 'null') ary[12] = 0;
            if (ary[13] == "" || ary[13] == 'null') ary[13] = 0;
            if (ary[14] == "" || ary[14] == 'null') ary[14] = 0;
            if (ary[15] == "" || ary[15] == 'null') ary[15] = 0;
            if (ary[16] == "" || ary[16] == 'null') ary[16] = 0;
            if (ary[17] == "" || ary[17] == 'null') ary[17] = 0;
            if (ary[18] == "" || ary[18] == 'null') ary[18] = 0;
            if (ary[19] == "" || ary[19] == 'null') ary[19] = 0;
            if (ary[20] == "" || ary[20] == 'null') ary[20] = 0;
            if (ary[21] == "" || ary[21] == 'null') ary[21] = 0;
            if (ary[22] == "" || ary[22] == 'null') ary[22] = 0;
            if (ary[23] == "" || ary[23] == 'null') ary[23] = 0;
            if (ary[24] == "" || ary[24] == 'null') ary[24] = 0;
            if (ary[25] == "" || ary[25] == 'null') ary[25] = 0;
            if (ary[26] == "" || ary[26] == 'null') ary[26] = 0;
            if (ary[27] == "" || ary[27] == 'null') ary[27] = 0;
            if (ary[28] == "" || ary[28] == 'null') ary[28] = 0;
            if (ary[29] == "" || ary[29] == 'null') ary[29] = 0;
            if (ary[30] == "" || ary[30] == 'null') ary[30] = 0;
            if (ary[31] == "" || ary[31] == 'null') ary[31] = 0;

            ary[32] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
            try {
                CRM.WebApp.webservice.TourMasterWebService.InsertUpdateHotelDetails(ary);
                alert('Record Save Successfully');
                CRM.WebApp.webservice.TourMasterWebService.GetHotelDetailsByTOUR_ID(TOUR_ID, updateHotelDetailsGrid);
            }
            catch (e) {
                alert('Wrong Data Inserted');
            }

        }

        // Function For Insert Update Hotel Grid End

        // Function For Insert Update Airline Grid Start

        function newrowaddedforflight(sender, args) {
            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var ary = [];
            ary[0] = TOUR_ID;
            ary[1] = AirlineTableView.get_dataItems()[currentRowIndex - 1].findElement("AIRLINE_NAME").value;
            ary[2] = AirlineTableView.get_dataItems()[currentRowIndex - 1].findElement("FLIGHT_NO").value;
            ary[3] = AirlineTableView.get_dataItems()[currentRowIndex - 1].findElement("CLASS_NAME").value;
            ary[4] = AirlineTableView.get_dataItems()[currentRowIndex - 1].findElement("SEATS_TO_BE_BLOCKED").value;
            ary[5] = AirlineTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECK_REQ_TO").value;
            ary[6] = AirlineTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECK_COMMENTS").value;
            ary[7] = AirlineTableView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_REQ_TO").value;
            ary[8] = AirlineTableView.get_dataItems()[currentRowIndex - 1].findElement("SEATS_TO_BLOCK").value;
            ary[9] = AirlineTableView.get_dataItems()[currentRowIndex - 1].findElement("TIME_LIMIT").value;
            ary[10] = AirlineTableView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_STATUS_NAME").value;
            ary[11] = AirlineTableView.get_dataItems()[currentRowIndex - 1].findElement("TOTAL_AMOUNT").value;
            ary[12] = AirlineTableView.get_dataItems()[currentRowIndex - 1].findElement("TAX").value;
            ary[13] = AirlineTableView.get_dataItems()[currentRowIndex - 1].findElement("GST").value;


            if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
            if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
            if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
            if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;
            if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
            if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;
            if (ary[6] == "" || ary[6] == 'null') ary[6] = 0;
            if (ary[7] == "" || ary[7] == 'null') ary[7] = 0;
            if (ary[8] == "" || ary[8] == 'null') ary[8] = 0;
            if (ary[9] == "" || ary[9] == 'null') ary[9] = 0;
            if (ary[10] == "" || ary[10] == 'null') ary[10] = 0;
            if (ary[11] == "" || ary[11] == 'null') ary[11] = 0;
            if (ary[12] == "" || ary[12] == 'null') ary[12] = 0;
            if (ary[13] == "" || ary[13] == 'null') ary[13] = 0;

            ary[14] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.FLIGHT_SR_NO;
            try {
                CRM.WebApp.webservice.TourMasterWebService.InsertUpdateFlight(ary);
                alert('Record Save Successfully');
                CRM.WebApp.webservice.TourMasterWebService.GetFlightDetailOnTourId(TOUR_ID, updateFlightDetailGrid);
            }
            catch (e) {
                alert('Wrong Data Inserted');
            }
        }


        // Function For Insert Update Airline Grid End

        // Function For Insert Update Cruise Grid 

        function newrowaddedforcruise(sender, args) {
            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var ary = [];
            ary[0] = TOUR_ID;
            ary[1] = CruiseTableView.get_dataItems()[currentRowIndex - 1].findElement("CRUISE_COMP_NAME").value;
            ary[2] = CruiseTableView.get_dataItems()[currentRowIndex - 1].findElement("CABINE_CATEGORY").value;
            ary[3] = CruiseTableView.get_dataItems()[currentRowIndex - 1].findElement("CABINE_TO_BE_BLOCKED").value;
            ary[4] = CruiseTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECK_REQ_TO").value;
            ary[5] = CruiseTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECK_COMMENTS").value;
            ary[6] = CruiseTableView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_REQ_TO").value;
            ary[7] = CruiseTableView.get_dataItems()[currentRowIndex - 1].findElement("TOTAL_ROOMS_BLOCKED").value;
            ary[8] = CruiseTableView.get_dataItems()[currentRowIndex - 1].findElement("TIME_LIMIT").value;
            ary[9] = CruiseTableView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_STATUS_NAME").value;
            ary[10] = CruiseTableView.get_dataItems()[currentRowIndex - 1].findElement("TOTAL_AMOUNT").value;
            ary[11] = CruiseTableView.get_dataItems()[currentRowIndex - 1].findElement("TAX").value;
            ary[12] = CruiseTableView.get_dataItems()[currentRowIndex - 1].findElement("GST").value;
            ary[14] = CruiseTableView.get_dataItems()[currentRowIndex - 1].findElement("CRUISE_NAME").value;

            if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
            if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
            if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
            if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;
            if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
            if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;
            if (ary[6] == "" || ary[6] == 'null') ary[6] = 0;
            if (ary[7] == "" || ary[7] == 'null') ary[7] = 0;
            if (ary[8] == "" || ary[8] == 'null') ary[8] = 0;
            if (ary[9] == "" || ary[9] == 'null') ary[9] = 0;
            if (ary[10] == "" || ary[10] == 'null') ary[10] = 0;
            if (ary[11] == "" || ary[11] == 'null') ary[11] = 0;
            if (ary[12] == "" || ary[12] == 'null') ary[12] = 0;
            if (ary[14] == "" || ary[14] == 'null') ary[14] = 0;

            ary[13] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CRUISE_SR_NO;

            try {
                CRM.WebApp.webservice.TourMasterWebService.InsertUpdateCruise(ary);
                alert('Record Save Successfully');
                CRM.WebApp.webservice.TourMasterWebService.GetCruiseDetailOnTourId(TOUR_ID, updateCruiseDetailGrid);
            }
            catch (e) {
                alert('Wrong Data Inserted');
            }
        }
        function cabineadded(sender, args) {
            var a = [];
            a[1] = document.getElementById('<%=txtCabine0.ClientID%>').value;
            a[2] = document.getElementById('<%=txtCabine1.ClientID%>').value;
            a[3] = document.getElementById('<%=txtCabine2.ClientID%>').value;
            a[4] = document.getElementById('<%=txtCabine3.ClientID%>').value;
            a[5] = document.getElementById('<%=txtCabine4.ClientID%>').value;
            a[6] = document.getElementById('<%=txtCabine5.ClientID%>').value;
            a[7] = document.getElementById('<%=txtCabine6.ClientID%>').value;
            a[8] = document.getElementById('<%=txtCabine7.ClientID%>').value;
            a[9] = document.getElementById('<%=txtCabine8.ClientID%>').value;
            a[10] = document.getElementById('<%=txtCabine9.ClientID%>').value;
            a[11] = TOUR_ID;
            a[12] = this.cruise_id;

            if (a[1] == "" || a[1] == 'null') a[1] = 0;
            if (a[2] == "" || a[2] == 'null') a[2] = 0;
            if (a[3] == "" || a[3] == 'null') a[3] = 0;
            if (a[4] == "" || a[4] == 'null') a[4] = 0;
            if (a[5] == "" || a[5] == 'null') a[5] = 0;
            if (a[6] == "" || a[6] == 'null') a[6] = 0;
            if (a[7] == "" || a[7] == 'null') a[7] = 0;
            if (a[8] == "" || a[8] == 'null') a[8] = 0;
            if (a[9] == "" || a[9] == 'null') a[9] = 0;
            if (a[10] == "" || a[10] == 'null') a[10] = 0;

            try {
                CRM.WebApp.webservice.TourMasterWebService.InsertUpdateDeck(a);
                alert('Record Save Successfully');
            }
            catch (e) {
                alert('Wrong Data Inserted');
            }

        }

        function radgridCruiseDetail_RowDataBound(sender, args) {
            cruise_id = args.get_dataItem()["CRUISE_SR_NO"];

        }

        // Function For GetCountryName Start 

        function getCountryName(sender) {
            var value = sender.value;
            CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
        }

        // Function For GetCountryName End 

        // Function For GetCityName Start 

        function getCityName(sender) {
            var value = sender.value;
            CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
        }

        // Function For GetCityName End

        // Function  Date Start
        function checkFromDate(sender) {
            if (Date.parse(sender.value) < Date.parse(tour_from_date) || Date.parse(tour_to_date) < Date.parse(sender.value)) {
                alert('Date is not between tour dates.');
            }
        }

        function checkToDate(sender) {
            if (Date.parse(sender.value) < Date.parse(tour_from_date) || Date.parse(tour_to_date) < Date.parse(sender.value)) {
                alert('Date is not between tour dates.');
            }
        }

        // Function Date End

        // Function ammount Start
        var h_amt;
        var h_tax;
        var h_gst;
        function getHotelAmount(sender) {
            h_amt = sender.value;
        }
        function getHotelTax(sender) {
            h_tax = sender.value;
        }
        function getHotelGst(sender) {
            h_gst = sender.value;
        }
        // Function ammount End

        // Function calculate Toatal Ammount Start
        function calculateHotelAmount(sender) {
            if (h_amt == "" || h_amt == 'null') h_amt = 0;
            if (h_tax == "" || h_tax == 'null') h_tax = 0;
            if (h_gst == "" || h_gst == 'null') h_gst = 0;

            sender.value = Number(h_amt) + Number(h_tax) + Number(h_gst);
        }
        // Function calculate Toatal Ammount End

        // Function openhoteldetail Start
        function openhoteldetail() {
            alert("Hotel Detail");
        }
        // Function openhoteldetail End

        // Function Addanothernewhotel Start
        function AddNewHotel() {
            CRM.WebApp.webservice.TourMasterWebService.InsertNewHotel(TOUR_ID);
            CRM.WebApp.webservice.TourMasterWebService.GetHotelDetailsByTOUR_ID(TOUR_ID, updateHotelDetailsGrid);
        }
        // Function Addanothernewhotel End

        function getAirlineName(sender) {
            var value = sender.value;
            CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
        }
        function getCruiseCompName(sender) {
            var value = sender.value;
            CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
        }

        function AddNewAirline() {
            CRM.WebApp.webservice.TourMasterWebService.InsertNewAirline(TOUR_ID);
        }
        function AddNewCruise() {
            CRM.WebApp.webservice.TourMasterWebService.InsertNewCruise(TOUR_ID);
        }

        function getFlightData(sender) {
            for (i = 1; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                var txtflightno = document.getElementById('ctl00_cphPageContent_radgridAirlineDetail_ctl00_ctl' + i + '_FLIGHT_NO');
                var txtdeptime = document.getElementById('ctl00_cphPageContent_radgridAirlineDetail_ctl00_ctl' + i + '_DEP_TIME');
                var txtarrtime = document.getElementById('ctl00_cphPageContent_radgridAirlineDetail_ctl00_ctl' + i + '_ARRIVAL_TIME');
                var txtdepdate = document.getElementById('ctl00_cphPageContent_radgridAirlineDetail_ctl00_ctl' + i + '_DATE_OF_DEPARTURE');
                var txtarrdate = document.getElementById('ctl00_cphPageContent_radgridAirlineDetail_ctl00_ctl' + i + '_DATE_OF_ARRIVAL');

                if (txtflightno != null && sender.id == txtflightno.id) {
                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: sender.value, key: "FETCH_FLIGHT_DEP_DATE" }, function (data) { txtdepdate.value = data; });
                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: sender.value, key: "FETCH_FLIGHT_DEP_TIME" }, function (data) { txtdeptime.value = data; });
                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: sender.value, key: "FETCH_FLIGHT_ARR_DATE" }, function (data) { txtarrdate.value = data; });
                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: sender.value, key: "FETCH_FLIGHT_ARR_TIME" }, function (data) { txtarrtime.value = data; });
                    break;
                }

            }
        }

        function getCruiseData(sender) {

            for (i = 1; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;
                var txtcruisename = document.getElementById('ctl00_cphPageContent_radgridCruiseDetail_ctl00_ctl' + i + '_CRUISE_NAME');
                var txtdeptime = document.getElementById('ctl00_cphPageContent_radgridCruiseDetail_ctl00_ctl' + i + '_TIME_OF_DEPARTURE');
                var txtarrtime = document.getElementById('ctl00_cphPageContent_radgridCruiseDetail_ctl00_ctl' + i + '_TIME_OF_ARRIVAL');
                var txtdepdate = document.getElementById('ctl00_cphPageContent_radgridCruiseDetail_ctl00_ctl' + i + '_DATE_OF_SAILING');
                var txtarrdate = document.getElementById('ctl00_cphPageContent_radgridCruiseDetail_ctl00_ctl' + i + '_DATE_OF_ARRIVAL');

                if (txtcruisename != null && sender.id == txtcruisename.id) {
                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: sender.value, key: "FETCH_CRUISE_DEP_DATE" }, function (data) { txtdepdate.value = data; });
                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: sender.value, key: "FETCH_CRUISE_DEP_TIME" }, function (data) { txtdeptime.value = data; });
                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: sender.value, key: "FETCH_CRUISE_ARR_DATE" }, function (data) { txtarrdate.value = data; });
                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: sender.value, key: "FETCH_CRUISE_ARR_TIME" }, function (data) { txtarrtime.value = data; });
                    break;
                }

            }
        }       
        
    </script>

</telerik:radcodeblock>
    <%--Javascript Files--%>
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <%--Drop down Store Procedure--%>
    <script type="text/javascript">
        $(document).ready(function () {
            var tourtype = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_TOUR_TYPE_NAME_AUTOSEARCH_NEW";   //set query for dropdown....multiple entry
            var toursubtype = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_TOUR_SUB_TYPE_NAME_AUTOSEARCH_NEW";   //set query for dropdown....multiple entry
            var touritenarytype = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_TOUR_ITENARY_TYPE_NAME_AUTOSEARCH_NEW";   //set query for dropdown....multiple entry
            var tourtitle = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_GUIDE_TITLE_AUTOSEARCH_NEW";   //set query for dropdown....multiple entry
            var tourbasetour = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_BASE_TOUR_AUTOSEARCH_NEW";   //set query for dropdown....multiple entry
            var empname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_EMPLOYEE_FOR_TASKMASTER_AUTOSEARCH";
            var flightclass = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_FLIGHT_CLASS_AUTOSEARCH";
            var airlinename = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_AIRLINE_AUTOSEARCH";
            var bookingstatus = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_BOOKING_STATUS_AUTOSEARCH";
            var cruisecompany = "../../webservice/autocomplete.ashx?key=FETCH_CRUISE_COMP_NAME_AUTOSEARCH";
            var cabinecategory = "../../webservice/autocomplete.ashx?key=FETCH_CRUISE_CABINE_CATEGORY_AUTOSEARCH";
            var flightno = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_FLIGHT_NO_AUTOSEARCH";
            var cruisename = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_CRUISE_AUTOSEARCH_DUALPARAM?" + globalvalue;
            var cityname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_CITY_AUTOSEARCH_DUALPARAM?" + globalvalue;
            var hotelname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_HOTEL_AUTOSEARCH_DUALPARAM?" + globalvalue;
            var flightno1 = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_FLIGHT_NO_AUTOSEARCH_DUALPARAMETER?" + globalvalue;
            var countryname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_COUNTRY_AUTOSEARCH_NEW";
            var roomtype = "../../webservice/autocomplete.ashx?key=FETCH_ROOM_TYPE_FOR_HOTEL_AUTOSEARCH";
            var guidename = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_GUIDE_DETAIL_AUTOSEARCH";
            var country_state_name = "../../webservice/autocomplete.ashx?key=FETCH_COUNTRY_STATE_NAME_AUTOSEARCH";

            $("#ctl00_cphPageContent_searchBox").autocomplete(country_state_name);


            var countradgridmaster = 0; //single entry per grid

            for (var i = 1; i < 55; i++) { //single entry per grid
                if (i < 10)
                    i = '0' + i;
                $("#ctl00_cphPageContent_radgridmaster_ctl00_ctl" + i + "_TOUR_TYPE_NAME").autocomplete(tourtype);  //set id for dropdown ....multiple entry
                $("#ctl00_cphPageContent_radgridmaster_ctl00_ctl" + i + "_TOUR_SUB_TYPE_NAME").autocomplete(toursubtype);  //set id for dropdown ....multiple entry
                $("#ctl00_cphPageContent_radgridmaster_ctl00_ctl" + i + "_TOUR_ITENARY_TYPE_NAME").autocomplete(touritenarytype);  //set id for dropdown ....multiple entry
                $("#ctl00_cphPageContent_radgridmaster_ctl00_ctl" + i + "_GUIDE_TITLE").autocomplete(tourtitle);  //set id for dropdown ....multiple entry
                $("#ctl00_cphPageContent_radgridmaster_ctl00_ctl" + i + "_BASE_TOUR").autocomplete(tourbasetour);  //set id for dropdown ....multiple entry
                $("#ctl00_cphPageContent_radgridmaster_ctl00_ctl" + i + "_GUIDE_TITLE").autocomplete(guidename);
                $("#ctl00_cphPageContent_radgridHotelDetails_ctl00_ctl" + i + "_CITY_NAME").autocomplete(cityname);
                $("#ctl00_cphPageContent_radgridHotelDetails_ctl00_ctl" + i + "_HOTEL_NAME").autocomplete(hotelname);
                $("#ctl00_cphPageContent_radgridHotelDetails_ctl00_ctl" + i + "_BOOKING_REQUEST_TO").autocomplete(empname);
                $("#ctl00_cphPageContent_radgridHotelDetails_ctl00_ctl" + i + "_CHECK_REQUEST_TO").autocomplete(empname);
                $("#ctl00_cphPageContent_radgridHotelDetails_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(countryname);
                $("#ctl00_cphPageContent_radgridHotelDetails_ctl00_ctl" + i + "_ROOM_TYPE_NAME").autocomplete(roomtype);
                $("#ctl00_cphPageContent_radgridHotelDetails_ctl00_ctl" + i + "_BOOKING_STATUS").autocomplete(bookingstatus);
                $("#ctl00_cphPageContent_radgridAirlineDetail_ctl00_ctl" + i + "_CHECK_REQ_TO").autocomplete(empname);
                $("#ctl00_cphPageContent_radgridAirlineDetail_ctl00_ctl" + i + "_BOOKING_REQ_TO").autocomplete(empname);
                $("#ctl00_cphPageContent_radgridAirlineDetail_ctl00_ctl" + i + "_CLASS_NAME").autocomplete(flightclass);
                $("#ctl00_cphPageContent_radgridAirlineDetail_ctl00_ctl" + i + "_AIRLINE_NAME").autocomplete(airlinename);
                $("#ctl00_cphPageContent_radgridAirlineDetail_ctl00_ctl" + i + "_BOOKING_STATUS_NAME").autocomplete(bookingstatus);
                $("#ctl00_cphPageContent_radgridAirlineDetail_ctl00_ctl" + i + "_FLIGHT_NO").autocomplete(flightno1);
                $("#ctl00_cphPageContent_radgridCruiseDetail_ctl00_ctl" + i + "_CRUISE_COMP_NAME").autocomplete(cruisecompany);
                $("#ctl00_cphPageContent_radgridCruiseDetail_ctl00_ctl" + i + "_CHECK_REQ_TO").autocomplete(empname);
                $("#ctl00_cphPageContent_radgridCruiseDetail_ctl00_ctl" + i + "_BOOKING_REQ_TO").autocomplete(empname);
                $("#ctl00_cphPageContent_radgridCruiseDetail_ctl00_ctl" + i + "_BOOKING_STATUS_NAME").autocomplete(bookingstatus);
                $("#ctl00_cphPageContent_radgridCruiseDetail_ctl00_ctl" + i + "_CABINE_CATEGORY").autocomplete(cabinecategory);
                $("#ctl00_cphPageContent_radgridCruiseDetail_ctl00_ctl" + i + "_CRUISE_NAME").autocomplete(cruisename);


            }
        });

    </script>
    <%--Rad_Date_Picker--%>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
    <ClientEvents OnDateSelected="dateSelected" />
</telerik:raddatepicker>
    <%--Hotel Grid--%>
    <div>
        <div class="pageTitle" style="float: left">
            <br />
            <asp:Literal ID="Literal3" runat="server" Text="Hotel Detail"></asp:Literal>
            <br />
        </div>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridHotelDetails" runat="server" pagesize="50" allowpaging="false"
                        itemstyle-wrap="false" enableembeddedskins="false" allowautomaticinserts="True"
                        headerstyle-wrap="false" allowmultirowselection="false">
            <MasterTableView ClientDataKeyNames="TOUR_ID" EditMode="InPlace" Width="3000px">
            <RowIndicatorColumn>
             </RowIndicatorColumn>
              <Columns>
                        <%--template column for raw data editing result--%>
                    <telerik:GridTemplateColumn SortExpression="SR_NO" DataField="SR_NO" HeaderText="SR_NO" Visible="false">
                        <ItemTemplate>
                             <asp:TextBox ID="SR_NO" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="COUNTRY_NAME" HeaderText="Country" SortExpression="COUNTRY_NAME">
                        <ItemTemplate>
                         <asp:TextBox id="COUNTRY_NAME" runat="server" CssClass="radinput" onblur="getCountryName(this);"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CITY_NAME" HeaderText="City" SortExpression="CITY_NAME">
                        <ItemTemplate>
                         <asp:TextBox id="CITY_NAME" runat="server" CssClass="radinput" onblur="getCityName(this);"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="HOTEL_NAME" HeaderText="Hotel" SortExpression="HOTEL_NAME">
                        <ItemTemplate>
                         <asp:TextBox id="HOTEL_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     
                 <telerik:GridTemplateColumn DataField="FROM_DATE" HeaderText="From Date" SortExpression="FROM_DATE">
                        <ItemTemplate>
                             <asp:TextBox ID="FROM_DATE"  onclick="showPopup(this, event);"
                            onfocus="showPopup(this, event);"
                            onkeydown ="parseDate(this, event);" runat="server" CssClass="radinput" onblur="checkFromDate(this);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TO_DATE" HeaderText="To Date" SortExpression="TO_DATE">
                        <ItemTemplate>
                             <asp:TextBox ID="TO_DATE"  onclick="showPopup(this, event);"
                            onfocus="showPopup(this, event);"
                            onkeydown ="parseDate(this, event);" runat="server" CssClass="radinput" onblur="checkToDate(this);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     
                     <telerik:GridTemplateColumn DataField="CURRENCY_NAME" HeaderText="Currency" SortExpression="CURRENCY_NAME">
                        <ItemTemplate>
                             <asp:TextBox ID="CURRENCY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="ROOM_TYPE_NAME" HeaderText="Room Type" SortExpression="ROOM_TYPE_NAME"  Visible="false">
                        <ItemTemplate>
                         <asp:TextBox id="ROOM_TYPE_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="NO_OF_ROOMS" HeaderText="No of Rooms" SortExpression="NO_OF_ROOMS" visible="false">
                        <ItemTemplate>
                             <asp:TextBox ID="NO_OF_ROOMS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn DataField="AMOUNT" HeaderText="Amount" SortExpression="AMOUNT">
                        <ItemTemplate>
                            <asp:TextBox ID="AMOUNT" runat="server" CssClass="radinput" onblur="getHotelAmount(this);"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn DataField="TAX" HeaderText="Tax" SortExpression="TAX">
                        <ItemTemplate>
                             <asp:TextBox ID="TAX" runat="server" CssClass="radinput" onblur="getHotelTax(this);"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="GST" HeaderText="Gst" SortExpression="GST">
                        <ItemTemplate>
                             <asp:TextBox ID="GST" runat="server" CssClass="radinput" onblur="getHotelGst(this);"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="TOTAL_AMOUNT" HeaderText="Total Amount" SortExpression="TOTAL_AMOUNT">
                        <ItemTemplate>
                             <asp:TextBox ID="TOTAL_AMOUNT" runat="server" CssClass="radinput" onfocus="calculateHotelAmount(this);"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="REMARKS" HeaderText="Remarks" SortExpression="REMARKS">
                        <ItemTemplate>
                             <asp:TextBox ID="REMARKS" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="BOOKING_REQUEST_TO" HeaderText="Booking Request To" SortExpression="BOOKING_REQUEST_TO">
                        <ItemTemplate>
                        <asp:TextBox id="BOOKING_REQUEST_TO" runat="Server" CssClass="radinput"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn DataField="ROOM_TO_BE_BLOCKED" HeaderText="Room To be Blocked" SortExpression="ROOM_TO_BE_BLOCKED">
                        <ItemTemplate>
                        <asp:TextBox id="ROOM_TO_BE_BLOCKED" runat="Server" CssClass="radinput"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CHECK_REQUEST_TO" HeaderText="Check Request To" SortExpression="CHECK_REQUEST_TO">
                        <ItemTemplate>
                        <asp:TextBox id="CHECK_REQUEST_TO" runat="Server" CssClass="radinput"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CHECK_REQUEST_DATE" HeaderText="Check Request Date" SortExpression="CHECK_REQUEST_DATE">
                        <ItemTemplate>
                        <asp:TextBox id="CHECK_REQUEST_DATE" runat="Server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CHECK_COMMENTS" HeaderText="Check Comments" SortExpression="CHECK_COMMENTS">
                        <ItemTemplate>
                        <asp:TextBox id="CHECK_COMMENTS" runat="Server" CssClass="radinput"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_ROOM_BLOCKED" HeaderText="Total Room Blocked" SortExpression="TOTAL_ROOM_BLOCKED">
                        <ItemTemplate>
                        <asp:TextBox id="TOTAL_ROOM_BLOCKED" runat="Server" CssClass="radinput"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TIME_LIMIT" HeaderText="Time Limit" SortExpression="TIME_LIMIT">
                        <ItemTemplate>
                        <asp:TextBox id="TIME_LIMIT" runat="Server" CssClass="radinput" onclick="showPopup(this, event);"
                            onfocus="showPopup(this, event);"
                            onkeydown ="parseDate(this, event);"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="APPROVED_BY" HeaderText="Approved By">
                        <ItemTemplate>
                        <asp:TextBox id="APPROVED_BY" runat="Server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BOOKED_BY" HeaderText="Booked By">
                        <ItemTemplate>
                        <asp:TextBox id="BOOKED_BY" runat="Server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BOOKING_STATUS" HeaderText="Booking Status" SortExpression="">
                        <ItemTemplate>
                        <asp:TextBox id="BOOKING_STATUS" runat="Server" CssClass="radinput"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BOOKING_REQUEST_DATE" HeaderText="Booking Request date">
                        <ItemTemplate>
                        <asp:TextBox id="BOOKING_REQUEST_DATE" runat="Server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_ROOM_ALLOTEED" HeaderText="Total Room Alloted">
                        <ItemTemplate>
                        <asp:TextBox id="TOTAL_ROOM_ALLOTEED" runat="Server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="PARTIAL_ROOM_ALLOTED" HeaderText="Partial room Alloted">
                        <ItemTemplate>
                        <asp:TextBox id="PARTIAL_ROOM_ALLOTED" runat="Server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_ADULT_ALLOTED" HeaderText="Total Adult Alloted">
                        <ItemTemplate>
                        <asp:TextBox id="TOTAL_ADULT_ALLOTED" runat="Server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_CWB_ALLOTED" HeaderText="Total CWB Alloted">
                        <ItemTemplate>
                        <asp:TextBox id="TOTAL_CWB_ALLOTED" runat="Server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_CNB_ALLOTED" HeaderText="Total CNB Alloted">
                        <ItemTemplate>
                        <asp:TextBox id="TOTAL_CNB_ALLOTED" runat="Server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_INFANT_ALLOTED" HeaderText="Total INFANT Alloted">
                        <ItemTemplate>
                        <asp:TextBox id="TOTAL_INFANT_ALLOTED" runat="Server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="AVALIBLE_ROOM" HeaderText="Available Room">
                        <ItemTemplate>
                        <asp:TextBox id="AVALIBLE_ROOM" runat="Server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="" DataField="" AllowFiltering="false" ShowSortIcon="false" visible="false">
                    <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                            <asp:Button id="btndetail" runat="server" Text="Detail" onClientclick="openhoteldetail()" />
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField="PHOTO">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A3" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newrowaddedforHotel(this,event);">
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
                <ClientEvents OnCommand="radgridHotelDetails_Command"  OnRowDblClick="RowClick" />
            </ClientSettings>
        </telerik:radgrid>
                    <asp:LinkButton ID="lbAddHotel" runat="server" Text="Add Another Hotel" OnClientClick="AddNewHotel();"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <%--Airline Grid--%>
    <div>
        <div class="pageTitle" style="float: left">
            <br />
            <asp:Literal ID="Literal2" runat="server" Text="Airline Detail"></asp:Literal>
            <br />
        </div>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridAirlineDetail" runat="server" pagesize="50" allowpaging="false"
                        itemstyle-wrap="false" enableembeddedskins="false" allowautomaticinserts="True"
                        headerstyle-wrap="false">
                  <MasterTableView ClientDataKeyNames="TOUR_ID" EditMode="InPlace" Width="3000px">
                    <Columns>
                        <%--template column for raw data editing result--%>
                     <telerik:GridTemplateColumn SortExpression="FLIGHT_SR_NO" DataField="FLIGHT_SR_NO" HeaderText="FLIGHT_SR_NO" Visible="false">
                        <ItemTemplate>
                             <telerik:RadTextBox  ID="FLIGHT_SR_NO" runat="server"></telerik:RadTextBox >
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>  
                    <telerik:GridTemplateColumn DataField="AIRLINE_NAME" HeaderText="Airline" SortExpression="AIRLINE_NAME" UniqueName="AIRLINE_NAME">
                        <ItemTemplate>
                         <asp:TextBox id="AIRLINE_NAME" runat="server" CssClass="radinput" onblur="getAirlineName(this);"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="FLIGHT_NO" HeaderText="Flight No" SortExpression="FLIGHT_NO">
                        <ItemTemplate>
                         <asp:TextBox id="FLIGHT_NO" runat="server" CssClass="radinput" onblur="getFlightData(this);"></asp:TextBox>
                         </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="DATE_OF_DEPARTURE" HeaderText="Date Of Departure">
                        <ItemTemplate>
                            <asp:TextBox id="DATE_OF_DEPARTURE" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="DEP_TIME" HeaderText="Time Of Departure">
                        <ItemTemplate>
                            <asp:TextBox id="DEP_TIME" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="DATE_OF_ARRIVAL" HeaderText="Date Of Arrival">
                        <ItemTemplate>
                            <asp:TextBox id="DATE_OF_ARRIVAL" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="ARRIVAL_TIME" HeaderText="Time Of Arrival">
                        <ItemTemplate>
                            <asp:TextBox id="ARRIVAL_TIME" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CLASS_NAME" HeaderText="Flight Class" SortExpression="CLASS_NAME">
                        <ItemTemplate>
                         <asp:TextBox id="CLASS_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn  DataField="SEATS_TO_BE_BLOCKED" HeaderText="Seats To Be Blocked" SortExpression="SEATS_TO_BE_BLOCKED">
                        <ItemTemplate>
                         <asp:TextBox  id="SEATS_TO_BE_BLOCKED" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CHECK_REQ_TO" HeaderText="Check Request To" SortExpression="CHECK_REQ_TO">
                        <ItemTemplate>
                         <asp:TextBox id="CHECK_REQ_TO" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CHECK_REQ_DATE" HeaderText="Check Request Date" SortExpression="CHECK_REQ_DATE">
                        <ItemTemplate>
                            <asp:TextBox id="CHECK_REQ_DATE" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CHECK_COMMENTS" HeaderText="Check Comment" SortExpression="CHECK_COMMENTS">
                        <ItemTemplate>
                         <asp:TextBox id="CHECK_COMMENTS" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BOOKING_REQ_TO" HeaderText="Booking Request To" SortExpression="BOOKING_REQ_TO">
                        <ItemTemplate>
                         <asp:TextBox id="BOOKING_REQ_TO" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="SEATS_TO_BLOCK" HeaderText="Total Seats Blocked" SortExpression="SEATS_TO_BLOCK">
                        <ItemTemplate>
                         <asp:TextBox id="SEATS_TO_BLOCK" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TIME_LIMIT" HeaderText="Time Limit" SortExpression="TIME_LIMIT">
                        <ItemTemplate>
                         <asp:TextBox id="TIME_LIMIT" runat="server"  onclick="showPopup(this, event);"
                            onfocus="showPopup(this, event);"
                            onkeydown ="parseDate(this, event);" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn DataField="APPROVED_BY" HeaderText="Approved By">
                        <ItemTemplate>
                            <asp:TextBox id="APPROVED_BY" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BOOKED_BY" HeaderText="Booked By">
                        <ItemTemplate>
                            <asp:TextBox id="BOOKED_BY" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BOOKING_STATUS_NAME" HeaderText="Booking Status" SortExpression="BOOKING_STATUS_NAME">
                        <ItemTemplate>
                         <asp:TextBox id="BOOKING_STATUS_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BOOKING_REQ_DATE" HeaderText="Booking Request Date" SortExpression="BOOKING_REQ_DATE">
                        <ItemTemplate>
                            <asp:TextBox id="BOOKING_REQ_DATE" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_AMOUNT" HeaderText="Total Amount" SortExpression="TOTAL_AMOUNT">
                        <ItemTemplate>
                         <asp:TextBox id="TOTAL_AMOUNT" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TAX" HeaderText="TAX" SortExpression="TAX">
                        <ItemTemplate>
                         <asp:TextBox id="TAX" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="GST" HeaderText="GST" SortExpression="GST">
                        <ItemTemplate>
                         <asp:TextBox id="GST" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_SEATS_ALLOTED" HeaderText="Total Seats Alloted">
                        <ItemTemplate>
                            <asp:TextBox id="TOTAL_SEATS_ALLOTED" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_ADULTS_ALLOTED" HeaderText="Total Adults Alloted">
                        <ItemTemplate>
                            <asp:TextBox id="TOTAL_ADULTS_ALLOTED" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_CHILD_ALLOTED" HeaderText="Total Child Alloted">
                        <ItemTemplate>
                            <asp:TextBox id="TOTAL_CHILD_ALLOTED" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_INFANT_ALLOTED" HeaderText="Total Infant Alloted">
                        <ItemTemplate>
                            <asp:TextBox id="TOTAL_INFANT_ALLOTED" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="AVAILABLE_SEATS" HeaderText="Available Seats">
                        <ItemTemplate>
                            <asp:TextBox id="AVAILABLE_SEATS" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" visible="false">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;">
                            Book
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false">
                     <ItemStyle Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A4" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newrowaddedforflight(this,event);">
                            &raquo;
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    </Columns>
                  </MasterTableView>
                  <ClientSettings AllowColumnsReorder="true" ReorderColumnsOnClient="true" AllowDragToGroup="true">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridAirlineDetails_Command" OnRowDblClick="RowClickforflight" />
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
        </telerik:radgrid>
                    <asp:LinkButton ID="lbAddAirline" runat="server" Text="Add Another Airline" OnClientClick="AddNewAirline();"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <!-- Cruise Detail -->
    <div>
        <div class="pageTitle" style="float: left">
            <br />
            <asp:Literal ID="Literal5" runat="server" Text="Cruise Detail"></asp:Literal>
            <br />
        </div>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridCruiseDetail" runat="server" pagesize="50" allowpaging="false"
                        itemstyle-wrap="false" enableembeddedskins="false" allowautomaticinserts="True"
                        headerstyle-wrap="false">
            <MasterTableView ClientDataKeyNames="TOUR_ID" EditMode="InPlace" Width="3000px">
                <Columns>
                    <telerik:GridTemplateColumn SortExpression="CRUISE_SR_NO" DataField="CRUISE_SR_NO" HeaderText="CRUISE_SR_NO" visible="false">
                        <ItemTemplate>
                             <telerik:RadTextBox  ID="CRUISE_SR_NO" runat="server"></telerik:RadTextBox >
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>  
                    <telerik:GridTemplateColumn DataField="CRUISE_COMP_NAME" HeaderText="Cruise Company Name" SortExpression="CRUISE_COMP_NAME">
                        <ItemTemplate>
                         <asp:TextBox id="CRUISE_COMP_NAME" runat="server" CssClass="radinput" onblur="getCruiseCompName(this);"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CRUISE_NAME" HeaderText="Cruise Name" SortExpression="CRUISE_NAME">
                        <ItemTemplate>
                         <asp:TextBox id="CRUISE_NAME" runat="server" CssClass="radinput" onblur="getCruiseData(this);"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="DATE_OF_SAILING" HeaderText="Date Of Sailing" SortExpression="DATE_OF_SAILING">
                        <ItemTemplate>
                         <asp:TextBox id="DATE_OF_SAILING" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TIME_OF_DEPARTURE" HeaderText="Time Of Departure" SortExpression="TIME_OF_DEPARTURE">
                        <ItemTemplate>
                         <asp:TextBox id="TIME_OF_DEPARTURE" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="DATE_OF_ARRIVAL" HeaderText="Date Of Arrival" SortExpression="DATE_OF_ARRIVAL">
                        <ItemTemplate>
                         <asp:TextBox id="DATE_OF_ARRIVAL" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TIME_OF_ARRIVAL" HeaderText="Time Of Arrival" SortExpression="TIME_OF_ARRIVAL">
                        <ItemTemplate>
                         <asp:TextBox id="TIME_OF_ARRIVAL" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn DataField="CABINE_CATEGORY" HeaderText="Cabine Category" SortExpression="CABINE_CATEGORY">
                        <ItemTemplate>
                         <asp:TextBox id="CABINE_CATEGORY" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="CABINE_TO_BE_BLOCKED" HeaderText="Cabine To Be Blocked" SortExpression="CABINE_TO_BE_BLOCKED">
                        <ItemTemplate>
                         <asp:TextBox id="CABINE_TO_BE_BLOCKED" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="CHECK_REQ_TO" HeaderText="Check Request To" SortExpression="CHECK_REQ_TO">
                        <ItemTemplate>
                         <asp:TextBox id="CHECK_REQ_TO" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="CHECK_REQ_DATE" HeaderText="Check Request Date">
                        <ItemTemplate>
                            <asp:TextBox id="CHECK_REQ_DATE" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CHECK_COMMENTS" HeaderText="Check Comment" SortExpression="CHECK_COMMENTS">
                        <ItemTemplate>
                         <asp:TextBox id="CHECK_COMMENTS" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BOOKING_REQ_TO" HeaderText="Booking Request To" SortExpression="BOOKING_REQ_TO">
                        <ItemTemplate>
                         <asp:TextBox id="BOOKING_REQ_TO" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_ROOMS_BLOCKED" HeaderText="Total Rooms Blocked" SortExpression="TOTAL_ROOMS_BLOCKED">
                        <ItemTemplate>
                         <asp:TextBox id="TOTAL_ROOMS_BLOCKED" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TIME_LIMIT" HeaderText="Time Limit" SortExpression="TIME_LIMIT">
                        <ItemTemplate>
                         <asp:TextBox id="TIME_LIMIT" runat="server" onclick="showPopup(this, event);"
                            onfocus="showPopup(this, event);"
                            onkeydown ="parseDate(this, event);" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     
                    <telerik:GridTemplateColumn DataField="APPROVED_BY" HeaderText="Approved By">
                        <ItemTemplate>
                            <asp:TextBox id="APPROVED_BY" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BOOKED_BY" HeaderText="Booked By">
                        <ItemTemplate>
                            <asp:TextBox id="BOOKED_BY" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BOOKING_STATUS_NAME" HeaderText="Booking Status" SortExpression="BOOKING_STATUS_NAME">
                        <ItemTemplate>
                         <asp:TextBox id="BOOKING_STATUS_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BOOKING_REQ_DATE" HeaderText="Booking Request Date">
                        <ItemTemplate>
                            <asp:TextBox id="BOOKING_REQ_DATE" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_AMOUNT" HeaderText="Total Amount" SortExpression="TOTAL_AMOUNT">
                        <ItemTemplate>
                         <asp:TextBox id="TOTAL_AMOUNT" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TAX" HeaderText="TAX" SortExpression="TAX">
                        <ItemTemplate>
                         <asp:TextBox id="TAX" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="GST" HeaderText="GST" SortExpression="GST">
                        <ItemTemplate>
                         <asp:TextBox id="GST" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_CABINES_ALLOTED" HeaderText="Total Cabines Alloted">
                        <ItemTemplate>
                            <asp:TextBox id="TOTAL_CABINES_ALLOTED" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="PARTIAL_CABINES_ALLOTED" HeaderText="Partial Cabines Alloted">
                        <ItemTemplate>
                            <asp:TextBox id="PARTIAL_CABINES_ALLOTED" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_ADULTS_ALLOTED" HeaderText="Total Adults Alloted">
                        <ItemTemplate>
                            <asp:TextBox id="TOTAL_ADULTS_ALLOTED" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_CWB_ALLOTED" HeaderText="Total CWB Alloted">
                        <ItemTemplate>
                            <asp:TextBox id="TOTAL_CWB_ALLOTED" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_CNB_ALLOTED" HeaderText="Total CNB Alloted">
                        <ItemTemplate>
                            <asp:TextBox id="TOTAL_CNB_ALLOTED" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOTAL_INFANT_ALLOTED" HeaderText="Total Infant Alloted">
                        <ItemTemplate>
                            <asp:TextBox id="TOTAL_INFANT_ALLOTED" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="AVAILABLE_CABINES" HeaderText="Available Cabines">
                        <ItemTemplate>
                            <asp:TextBox id="AVAILABLE_CABINES" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" visible="false">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A6" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;">
                            Book
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false">
                     <ItemStyle Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A7" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newrowaddedforcruise(this,event);">
                            &raquo;
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings AllowColumnsReorder="true" ReorderColumnsOnClient="true" AllowDragToGroup="True">
                 <KeyboardNavigationSettings AllowActiveRowCycle="True"/>
            <Resizing AllowColumnResize="True" ResizeGridOnColumnResize="True" />
                <ClientEvents OnCommand="radgridCruiseDetail_Command" onRowDataBound="radgridCruiseDetail_RowDataBound" OnRowSelected="radgridCruiseDetail_RowSelected" OnRowDblClick="RowClickforcruise"/>
                <Selecting AllowRowSelect="true"/>
               </ClientSettings>
        </telerik:radgrid>
                    <asp:LinkButton ID="lbAddCruise" runat="server" Text="Add Another Cruise" OnClientClick="AddNewCruise();"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <!-- Sub grid of Cruise Detail -->
    <table>
        <tr valign="top">
            <td valign="top">
                <div>
                    <div class="pageTitle" style="float: left">
                        <br />
                        <asp:Literal ID="Literal6" runat="server" Text="Deck Detail"></asp:Literal>
                        <br />
                    </div>
                    <br />
                    <br />
                    <br />
                    <table>
                        <tr style="background: LightGray; text-align: center">
                            <td>
                                <asp:Label ID="lblDeck" runat="server" Text="Deck No."></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCabine" runat="server" Text="Cabine"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDeckNo1" runat="server" ReadOnly="true" Text="1" CssClass="radinput"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCabine0" runat="server" TabIndex="1" Width="<%$appSettings:TextBoxWidth%>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDeckNo2" runat="server" ReadOnly="true" Text="2" CssClass="radinput"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCabine1" runat="server" TabIndex="2" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDeckNo3" runat="server" ReadOnly="true" Text="3" CssClass="radinput"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCabine2" runat="server" TabIndex="3" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDeckNo4" runat="server" ReadOnly="true" Text="4" CssClass="radinput"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCabine3" runat="server" TabIndex="4" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDeckNo5" runat="server" ReadOnly="true" Text="5" CssClass="radinput"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCabine4" runat="server" TabIndex="5" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDeckNo6" runat="server" ReadOnly="true" Text="6" CssClass="radinput"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCabine5" runat="server" TabIndex="6" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDeckNo7" runat="server" ReadOnly="true" Text="7" CssClass="radinput"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCabine6" runat="server" TabIndex="7" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDeckNo8" runat="server" ReadOnly="true" Text="8" CssClass="radinput"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCabine7" runat="server" TabIndex="8" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDeckNo9" runat="server" ReadOnly="true" Text="9" CssClass="radinput"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCabine8" runat="server" TabIndex="9" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDeckNo10" runat="server" ReadOnly="true" Text="10" CssClass="radinput"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCabine9" runat="server" TabIndex="10" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="cabineadded(this,event);"
                                    TabIndex="11" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td valign="top">
                <div>
                    <div class="pageTitle" style="float: left">
                        <br />
                        <asp:Literal ID="Literal7" runat="server" Text="Country For Visa"></asp:Literal>
                        <br />
                    </div>
                    <br />
                    <br />
                    <br />
                    <table>
                        <tr style="background: LightGray; text-align: center">
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Country Name"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox0" runat="server" ReadOnly="true" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" ReadOnly="true" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" ReadOnly="true" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox4" runat="server" ReadOnly="true" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
