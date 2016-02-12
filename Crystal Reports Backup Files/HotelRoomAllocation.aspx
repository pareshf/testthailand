<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="HotelRoomAllocation.aspx.cs" Inherits="CRM.WebApp.Views.Sales.HotelRoomAllocation" %>

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
        <script type="text/javascript" src="../Shared/Javascripts/HotelRoomAllocation.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                masterTableView = $find("<%= radgridmaster.ClientID %>").get_masterTableView();
                hotelTableView = $find("<%= radgridHotelDetails.ClientID %>").get_masterTableView();
                roomTableView = $find("<%= radgridRoomDetails.ClientID %>").get_masterTableView();
                custTableView = $find("<%= radgridcustmaster.ClientID %>").get_masterTableView();

                var q = window.location.search.substring(1);
                if (q != "") {
                    BOOKING_ID = getValue("BOOKING_ID");
                    TOUR_ID = getValue("TOUR_ID");
                    CRM.WebApp.webservice.AutoComplete.searchQueryResultOnSecondFile(TOUR_ID);
                    CRM.WebApp.webservice.HotelRoomAllocationWebService.GetPassengerDetail(BOOKING_ID, updateMasterGrid);
                    document.getElementById('ctl00_cphPageContent_btnAllocaterooms').style.visibility = "visible";
                    document.getElementById('ctl00_cphPageContent_btnconsolidate').style.visibility = "hidden";
                    document.getElementById('ctl00_cphPageContent_btnFinalize').style.visibility = "hidden";
                }
                else {
                    document.getElementById('ctl00_cphPageContent_btnconsolidate').style.visibility = "visible";
                    document.getElementById('ctl00_cphPageContent_btnAllocaterooms').style.visibility = "hidden";
                    document.getElementById('ctl00_cphPageContent_btnFinalize').style.visibility = "visible";
                }
            }
            var currentTextBox = null; var currentDatePicker = null; function showPopup(sender, e) {
                try { currentTextBox = sender; var datePicker = $find("<%= RadDatePicker1.ClientID %>"); currentDatePicker = datePicker; datePicker.set_selectedDate(currentDatePicker.get_dateInput().parseDate(sender.value)); var position = datePicker.getElementPosition(sender); datePicker.showPopup(position.x, position.y + sender.offsetHeight); }
                catch (e) { }
            }
            function dateSelected(sender, args) {
                try { if (currentTextBox != null) { currentTextBox.value = args.get_newDate().format('dd/MM/yyyy'); currentDatePicker.hidePopup(); } }
                catch (e) { }

            }
            function parseDate(sender, e) { currentDatePicker.hidePopup(); }
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

            function getCityName(sender) {
                var value = sender.value;
                CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
            }
            function getCountryName(sender) {
                var value = sender.value;
                CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
            }

            /*new row add for hotel*/
            function newRowAddForHotel(sender, args) {
                var currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                
                ary[0] = TOUR_ID;
                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
                ary[2] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("FROM_DATE").value;
                ary[3] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("TO_DATE").value;
                ary[4] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;
                ary[5] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("CITY_NAME").value;
                ary[6] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("HOTEL_NAME").value;
                ary[7] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("AMOUNT").value;
                ary[8] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;
                //ary[9] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("ROOM_TYPE_NAME").value;
                ary[10] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("TAX").value;
                ary[11] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("GST").value;
                ary[12] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("TOTAL_AMOUNT").value;
                ary[13] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("REMARKS").value;
                ary[14] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_REQUEST_TO").value;
                ary[15] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECK_REQUEST_TO").value;
                ary[16] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("TIME_LIMIT").value;
                ary[17] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_STATUS").value;
                ary[18] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("ROOM_TO_BE_BLOCKED").value;
                ary[19] = hotelTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECK_COMMENTS").value;

                for (var i = 0; i < 20; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }

                try {
                    CRM.WebApp.webservice.HotelRoomAllocationWebService.InsertUpdateHotel(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.HotelRoomAllocationWebService.GetPassengerDetail(BOOKING_ID, updateMasterGrid);
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }

            /* Insert-Update Room Allocation */
            function newRowAdded(sender, args) {
                var currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.BOOKING_HOTEL_SRNO;
                ary[2] = roomTableView.get_dataItems()[currentRowIndex - 1].findElement("ROOM_NO").value;
                ary[3] = roomTableView.get_dataItems()[currentRowIndex - 1].findElement("ADULT1").value;
                ary[4] = roomTableView.get_dataItems()[currentRowIndex - 1].findElement("ADULT2").value;
                ary[5] = roomTableView.get_dataItems()[currentRowIndex - 1].findElement("ADULT3").value;
                ary[6] = roomTableView.get_dataItems()[currentRowIndex - 1].findElement("CWB").value;
                ary[7] = roomTableView.get_dataItems()[currentRowIndex - 1].findElement("CNB1").value;
                ary[8] = roomTableView.get_dataItems()[currentRowIndex - 1].findElement("CNB2").value;
                ary[9] = roomTableView.get_dataItems()[currentRowIndex - 1].findElement("INFANT").value;
                ary[10] = SHARE;

//                for (var i = 0; i < 11; i++) {
//                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
//                }

                try {
                    CRM.WebApp.webservice.HotelRoomAllocationWebService.InsertUpdateRoom(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.HotelRoomAllocationWebService.GetRoomDetail(HOTEL_SRNO, updateRoomGrid);
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }

            function newHotelAdded() {
                CRM.WebApp.webservice.HotelRoomAllocationWebService.InsertNewHotel(TOUR_ID, BOOKING_DETAIL_ID);
            }

            function getTourName(sender) {
                var value = sender.value;
                CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
            }

            function getTourId(sender) {
                var tourcode = sender.value;
                $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: tourcode, key: "FETCH_TOUR_CODE?" + globalvalue }, function (data) { TOUR_ID = data; });
            }

            function getHotelData() {
                CRM.WebApp.webservice.HotelRoomAllocationWebService.GetSelfCustDetail(TOUR_ID, updateCustGrid);
                
            }

            function FamilyAllocation() {
                window.open('HotelRoomAllocationFamilySTD.aspx?key=' + BOOKING_ID);
            }

            function FamilyAllocationDev() {
                window.open('HotelRoomAllocationFamilyDEVReport.aspx?key=' + BOOKING_ID);
            }

            function TourAllocation() {
                window.open('TourRoomAllocationSTDReport.aspx?key=' + TOUR_ID);
            }

            function TourAllocationDev() {
                window.open('TourRoomAllocationDEVReport.aspx?key=' + TOUR_ID);
            }

            function CountryReport() {
                window.open('CountryWiseRoomAllocation.aspx?key=' + TOUR_ID + "&key1=" + COUNTRY_ID);
            }

            function CityReport() {
                window.open('CityWiseRoomAllocation.aspx?key=' + TOUR_ID + "&key1=" + CITY_ID);
            }

            function NextStep() {
                window.location = "CruiseRoomAllocation.aspx?BOOKING_ID=" + BOOKING_ID + "&TOUR_ID=" + TOUR_ID;
            }

            function AllocateRooms() {
                try {
                    CRM.WebApp.webservice.HotelRoomAllocationWebService.InsertHotelRoomAllocation(BOOKING_ID);
                    alert('Rooms Allocated Successfully');
                }
                catch (e) {
                    alert(e);
                }

            }

            function ConsolidateRooms() {
                try {
                    CRM.WebApp.webservice.HotelRoomAllocationWebService.GenerateHotelWiseRoom(TOUR_ID);
                    alert('Rooms Are Consolidate');
                    CRM.WebApp.webservice.HotelRoomAllocationWebService.GetRoomDetail(HOTEL_SRNO, updateRoomGrid);
                }
                catch (e) {
                    alert(e);
                }

            }

            function FinalizeRooms() {
                try {
                   
                    CRM.WebApp.webservice.HotelRoomAllocationWebService.FinalizingRooms(TOUR_ID);
                    CRM.WebApp.webservice.HotelRoomAllocationWebService.GetRoomDetail(HOTEL_SRNO, updateRoomGrid);
                }
                catch (e) {
                    alert(e);
                }
            
            }

        </script>
    </telerik:radcodeblock>
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            var empname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_EMPLOYEE_FOR_TASKMASTER_AUTOSEARCH";
            var bookingstatus = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_BOOKING_STATUS_AUTOSEARCH";
            var cityname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_CITY_AUTOSEARCH_DUALPARAM?" + globalvalue;
            var hotelname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_HOTEL_AUTOSEARCH_DUALPARAM?" + globalvalue;
            var countryname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_COUNTRY_AUTOSEARCH_NEW";
           // var roomtype = "../../webservice/autocomplete.ashx?key=FETCH_ROOM_TYPE_FOR_HOTEL_AUTOSEARCH";
            var adult_passenger_name = "../../webservice/autocomplete.ashx?key=FETCH_ADULT_PASSENGER_NAME_FROM_BOOKING?" + globalvalue + "?" + a;
            var cwb_passenger_name = "../../webservice/autocomplete.ashx?key=FETCH_CWB_PASSENGER_NAME_FROM_BOOKING?" + globalvalue + "?" + a;
            var cnb_passenger_name = "../../webservice/autocomplete.ashx?key=FETCH_CNB_PASSENGER_NAME_FROM_BOOKING?" + globalvalue + "?" + a;
            var infant_passenger_name = "../../webservice/autocomplete.ashx?key=FETCH_INFANT_PASSENGER_NAME_FROM_BOOKING?" + globalvalue + "?" + a;
            var tourshortname = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_SHORT_SHORT_NAME_AUTOSEARCH";
            var tourcode = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_CODE_ON_TOUR_SHORT_NAME?" + globalvalue;
            var currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_TOUR";

            $("#ctl00_cphPageContent_txtTourName").autocomplete(tourshortname);
            $("#ctl00_cphPageContent_txtTourCode").autocomplete(tourcode);

            for (var i = 1; i < 55; i++) { //single entry per grid
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridHotelDetails_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(countryname);
                $("#ctl00_cphPageContent_radgridHotelDetails_ctl00_ctl" + i + "_CITY_NAME").autocomplete(cityname);
                $("#ctl00_cphPageContent_radgridHotelDetails_ctl00_ctl" + i + "_HOTEL_NAME").autocomplete(hotelname);
                $("#ctl00_cphPageContent_radgridHotelDetails_ctl00_ctl" + i + "_CHECK_REQUEST_TO").autocomplete(empname);
                $("#ctl00_cphPageContent_radgridHotelDetails_ctl00_ctl" + i + "_BOOKING_REQUEST_TO").autocomplete(empname);
                $("#ctl00_cphPageContent_radgridHotelDetails_ctl00_ctl" + i + "_BOOKING_STATUS").autocomplete(bookingstatus);
               // $("#ctl00_cphPageContent_radgridHotelDetails_ctl00_ctl" + i + "_ROOM_TYPE_NAME").autocomplete(roomtype);
                $("#ctl00_cphPageContent_radgridRoomDetails_ctl00_ctl" + i + "_ADULT1").autocomplete(adult_passenger_name);
                $("#ctl00_cphPageContent_radgridRoomDetails_ctl00_ctl" + i + "_ADULT2").autocomplete(adult_passenger_name);
                $("#ctl00_cphPageContent_radgridRoomDetails_ctl00_ctl" + i + "_ADULT3").autocomplete(adult_passenger_name);
                $("#ctl00_cphPageContent_radgridRoomDetails_ctl00_ctl" + i + "_CWB").autocomplete(cwb_passenger_name);
                $("#ctl00_cphPageContent_radgridRoomDetails_ctl00_ctl" + i + "_CNB1").autocomplete(cnb_passenger_name);
                $("#ctl00_cphPageContent_radgridRoomDetails_ctl00_ctl" + i + "_CNB2").autocomplete(cnb_passenger_name);
                $("#ctl00_cphPageContent_radgridRoomDetails_ctl00_ctl" + i + "_INFANT").autocomplete(infant_passenger_name);
                $("#ctl00_cphPageContent_radgridRoomDetails_ctl00_ctl" + i + "_CURRENCY_NAME").autocomplete(currency);
            }
        });
    </script>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <br />
    <div>
        <div class="pageTitle" style="float: left">
            <asp:Literal ID="Literal1" runat="server" Text="Hotel Room Allocation"></asp:Literal>
        </div>
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblTourName" runat="server" Text="Tour Short Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTourName" runat="server" Width="250px" onblur="getTourName(this);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTourCode" runat="server" Text="Tour Code"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTourCode" runat="server" Width="250px" onblur="getTourId(this);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="OK" Style="color: black; font-weight: bold;"
                        OnClientClick="getHotelData();" />
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridcustmaster" runat="server" allowpaging="false" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
                            <MasterTableView ClientDataKeyNames="" AllowMultiColumnSorting="true" EditMode="InPlace" width="700">
                                <Columns>
                                <telerik:GridTemplateColumn SortExpression="BOOKING_DETAIL_ID" DataField="BOOKING_DETAIL_ID" HeaderText="BOOKING_DETAIL_ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox id="BOOKING_DETAIL_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn> 
                                      <telerik:GridTemplateColumn SortExpression="BOOKING_ID" DataField="BOOKING_ID" HeaderText="BOOKING_ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox id="BOOKING_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                          <telerik:GridTemplateColumn SortExpression="TITLE_DESC" DataField="TITLE_DESC" HeaderText="Title">
                                            <ItemTemplate>
                                                <asp:TextBox id="TITLE_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn SortExpression="CUST_REL_SURNAME" DataField="CUST_REL_SURNAME" HeaderText="Surname">
                                            <ItemTemplate>
                                                <asp:TextBox id="CUST_REL_SURNAME" runat="server" CssClass="radinput" ></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn SortExpression="CUST_REL_NAME" DataField="CUST_REL_NAME" HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:TextBox id="CUST_REL_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn SortExpression="CUST_REL_SRNO" DataField="CUST_REL_SRNO" HeaderText="srno" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox id="CUST_REL_SRNO" runat="server" CssClass="radinput" ></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>    
                                         <telerik:GridTemplateColumn SortExpression="TOUR_ID" DataField="TOUR_ID" HeaderText="tourId" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox id="TOUR_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>                                        
                                        </Columns>
                            </MasterTableView>
                            <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridcustmaster_Command" OnRowSelected="radgridcustmaster_RowSelected"/>
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
                    <telerik:radgrid id="radgridmaster" runat="server" allowpaging="false" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
                            <MasterTableView ClientDataKeyNames="" AllowMultiColumnSorting="true" EditMode="InPlace" width="700">
                                <RowIndicatorColumn>
                                </RowIndicatorColumn>
                                    <Columns>
                                               <telerik:GridTemplateColumn SortExpression="BOOKING_ID" DataField="BOOKING_ID" HeaderText="BOOKING_ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox id="BOOKING_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn SortExpression="BOOKING_DETAIL_ID" DataField="BOOKING_DETAIL_ID" HeaderText="BOOKING_DETAIL_ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox id="BOOKING_DETAIL_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>            
                                        <telerik:GridTemplateColumn SortExpression="TITLE_DESC" DataField="TITLE_DESC" HeaderText="Title">
                                            <ItemTemplate>
                                                <asp:TextBox id="TITLE_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn SortExpression="CUST_REL_NAME" DataField="CUST_REL_NAME" HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:TextBox id="CUST_REL_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn SortExpression="CUST_REL_SURNAME" DataField="CUST_REL_SURNAME" HeaderText="Surname">
                                            <ItemTemplate>
                                                <asp:TextBox id="CUST_REL_SURNAME" runat="server" CssClass="radinput" ></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn SortExpression="SHARE_ROOM_IN_HOTEL" DataField="SHARE_ROOM_IN_HOTEL" HeaderText="Share Room?">
                                            <ItemTemplate>
                                                <asp:TextBox id="SHARE_ROOM_IN_HOTEL"  runat="server" CssClass="radinput" ></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn SortExpression="" DataField="" HeaderText="Full Route">
                                            <ItemTemplate>
                                                <asp:TextBox  runat="server" CssClass="radinput" ></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                            </MasterTableView>
                            <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridmaster_Command" OnRowSelected="radgridmaster_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
                        </telerik:radgrid>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnAllocaterooms" runat="server" Text="Allocate" Style="color: black;
                        font-weight: bold;" OnClientClick="AllocateRooms();" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div class="pageTitle" style="float: left">
            <br />
            <asp:Literal ID="Literal3" runat="server" Text="Hotel Detail"></asp:Literal>
            <br />
        </div>
        <br />
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridHotelDetails" runat="server" allowpaging="false" allowmultirowselection="false"
                        allowsorting="True" pagesize="50" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
            <MasterTableView ClientDataKeyNames="" EditMode="InPlace" Width="3000px">
            <RowIndicatorColumn>
             </RowIndicatorColumn>
              <Columns>
                        
                    <telerik:GridTemplateColumn SortExpression="SR_NO" DataField="SR_NO" HeaderText="SR_NO" Visible="false">
                        <ItemTemplate>
                             <asp:TextBox ID="SR_NO" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="CITY_ID" DataField="CITY_ID" HeaderText="CITY_ID" Visible="false">
                        <ItemTemplate>
                             <asp:TextBox ID="CITY_ID" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="COUNTRY_ID" DataField="COUNTRY_ID" HeaderText="COUNTRY_ID" Visible="false">
                        <ItemTemplate>
                             <asp:TextBox ID="COUNTRY_ID" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="COUNTRY_NAME" HeaderText="▼ Country" SortExpression="COUNTRY_NAME">
                        <ItemTemplate>
                         <asp:TextBox id="COUNTRY_NAME" runat="server" CssClass="radinput" onblur="getCountryName(this);"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CITY_NAME" HeaderText="▼ City" SortExpression="CITY_NAME">
                        <ItemTemplate>
                         <asp:TextBox ID="CITY_NAME" runat="server" CssClass="radinput" onblur="getCityName(this);"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="HOTEL_NAME" HeaderText="Hotel" SortExpression="HOTEL_NAME">
                        <ItemTemplate>
                         <asp:TextBox ID="HOTEL_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     
                 <telerik:GridTemplateColumn DataField="FROM_DATE" HeaderText="From Date" SortExpression="FROM_DATE">
                        <ItemTemplate>
                             <asp:TextBox id="FROM_DATE"  onclick="showPopup(this, event);"
                            onfocus="showPopup(this, event);"
                            onkeydown ="parseDate(this, event);" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TO_DATE" HeaderText="To Date" SortExpression="TO_DATE">
                        <ItemTemplate>
                             <asp:TextBox id="TO_DATE" onclick="showPopup(this, event);"
                            onfocus="showPopup(this, event);"
                            onkeydown ="parseDate(this, event);" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     
                     <telerik:GridTemplateColumn DataField="CURRENCY_NAME" HeaderText="▼ Currency" SortExpression="CURRENCY_NAME">
                        <ItemTemplate>
                             <asp:TextBox id="CURRENCY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <%--<telerik:GridTemplateColumn DataField="ROOM_TYPE_NAME" HeaderText="▼ Room Type" SortExpression="ROOM_TYPE_NAME" Visible="false">
                        <ItemTemplate>
                         <asp:TextBox id="ROOM_TYPE_NAME" runat="server" CssClass="radinput" Visible="false"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>
                     <telerik:GridTemplateColumn DataField="AMOUNT" HeaderText="Amount" SortExpression="AMOUNT">
                        <ItemTemplate>
                            <asp:TextBox id="AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn DataField="TAX" HeaderText="Tax" SortExpression="TAX">
                        <ItemTemplate>
                             <asp:TextBox id="TAX" runat="server" CssClass="radinput" ></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="GST" HeaderText="Gst" SortExpression="GST">
                        <ItemTemplate>
                             <asp:TextBox id="GST" runat="server" CssClass="radinput" ></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="TOTAL_AMOUNT" HeaderText="Total Amount" SortExpression="TOTAL_AMOUNT">
                        <ItemTemplate>
                             <asp:TextBox id="TOTAL_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                          </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="REMARKS" HeaderText="Remarks" SortExpression="REMARKS">
                        <ItemTemplate>
                             <asp:TextBox id="REMARKS" runat="server" CssClass="radinput"></asp:TextBox>
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
                    <telerik:GridTemplateColumn DataField="TOTAL_ROOM_BLOCKED" HeaderText="Total Room Blocked" SortExpression="TOTAL_ROOM_BLOCKED" Visible="false">
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
                    <telerik:GridTemplateColumn DataField="APPROVED_BY" HeaderText="▼ Approved By">
                        <ItemTemplate>
                        <asp:TextBox id="APPROVED_BY" runat="Server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BOOKED_BY" HeaderText="▼ Booked By">
                        <ItemTemplate>
                        <asp:TextBox id="BOOKED_BY" runat="Server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BOOKING_STATUS" HeaderText="▼ Booking Status" SortExpression="">
                        <ItemTemplate>
                        <asp:TextBox  id="BOOKING_STATUS" runat="Server" CssClass="radinput"></asp:TextBox>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn DataField="BOOKING_REQUEST_TO" HeaderText="Booking Request To" SortExpression="BOOKING_REQUEST_TO">
                        <ItemTemplate>
                        <asp:TextBox id="BOOKING_REQUEST_TO" runat="Server" CssClass="radinput"></asp:TextBox>
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
                    
                     <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField="PHOTO">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A3" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newRowAddForHotel(this,event);">
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
                <ClientEvents OnCommand="radgridHotelDetails_Command" OnRowSelected="radgridHotelDetails_RowSelected" OnRowDblClick="HotelRowClick"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
        </telerik:radgrid>
                    <asp:LinkButton ID="lbAddHotel" runat="server" Text="Add Another Hotel" OnClientClick="newHotelAdded();"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div class="pageTitle" style="float: left">
            <br />
            <asp:Literal ID="Literal2" runat="server" Text="Room Allocation Detail"></asp:Literal>
            <br />
        </div>
        <br />
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridRoomDetails" runat="server" pagesize="50" allowpaging="false"
                        allowmultirowselection="false" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticinserts="True" headerstyle-wrap="false">
                        <MasterTableView ClientDataKeyNames="" EditMode="InPlace" Width="1000px">
                            <RowIndicatorColumn>
                            </RowIndicatorColumn>
                            <Columns>
                                <telerik:GridTemplateColumn SortExpression="SR_NO" DataField="SR_NO" HeaderText="SR_NO" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox id="SR_NO" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="BOOKING_HOTEL_SRNO" DataField="BOOKING_HOTEL_SRNO" HeaderText="BOOKING_HOTEL_SRNO" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox id="BOOKING_HOTEL_SRNO" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="ROOM_NO" HeaderText="Room No" UniqueName="ROOM_NO">
                                    <ItemTemplate>
                                        <asp:TextBox id="ROOM_NO" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="ROOM_TYPE_NAME" HeaderText="Room Type" UniqueName="ROOM_TYPE_NAME">
                                    <ItemTemplate>
                                        <asp:TextBox id="ROOM_TYPE_NAME" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="ROOM_SHARED" HeaderText="Room Share">
                                    <ItemTemplate>
                                        <asp:TextBox id="ROOM_SHARED" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="ADULT1" HeaderText="▼ Adult 1" >
                                    <ItemTemplate>
                                        <asp:TextBox id="ADULT1" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="ADULT2" HeaderText="▼ Adult 2" >
                                    <ItemTemplate>
                                        <asp:TextBox id="ADULT2" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="ADULT3" HeaderText="▼ Adult 3" >
                                    <ItemTemplate>
                                        <asp:TextBox id="ADULT3" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="CWB" HeaderText="▼ CWB" >
                                    <ItemTemplate>
                                        <asp:TextBox id="CWB" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="CNB1" HeaderText="▼ CNB 1" >
                                    <ItemTemplate>
                                        <asp:TextBox id="CNB1" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="CNB2" HeaderText="▼ CNB 2" >
                                    <ItemTemplate>
                                        <asp:TextBox id="CNB2" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="INFANT" HeaderText="▼ Infant" >
                                    <ItemTemplate>
                                        <asp:TextBox id="INFANT" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="ROOM_NO_FROM_B2" HeaderText="Room Preferance" >
                                    <ItemTemplate>
                                        <asp:TextBox id="ROOM_NO_FROM_B2" runat="server" CssClass="radinput" readOnly="true" style="background-color:LightBlue"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                 <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField="PHOTO">
                                    <ItemStyle CssClass="ItemAlign" Width="25px" />
                                    <HeaderStyle Width="25px" />
                                    <ItemTemplate>
                                          <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newRowAdded(this,event);">
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
                <ClientEvents OnCommand="radgridRoomDetails_Command" OnRowDblClick="RoomAllocationRowClick" />
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
                        </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <table>
        <tr>
            <td>
                <asp:Button ID="btnconsolidate" runat="server" Text="Consolidate" Style="color: black;
                    font-weight: bold;" OnClientClick="ConsolidateRooms();" />
            </td>
             <td>
                <asp:Button ID="btnFinalize" runat="server" Text="Finalize" Style="color: black;
                    font-weight: bold;" OnClientClick="FinalizeRooms();" />
            </td>

        </tr>
        <tr>
            <td>
                <asp:Button ID="btnFamily" runat="server" Text="Family Allocation" Style="color: black;
                    font-weight: bold;" OnClientClick="FamilyAllocation();" />
            </td>
            <td>
                <asp:Button ID="btnFamilyDev" runat="server" Text="Deviation In Family Allocation"
                    Style="color: black; font-weight: bold;" OnClientClick="FamilyAllocationDev();" />
            </td>
            <td>
                <asp:Button ID="btnTour" runat="server" Text="Tour Allocation" Style="color: black;
                    font-weight: bold;" OnClientClick="TourAllocation();" />
            </td>
            <td>
                <asp:Button ID="btnTourDev" runat="server" Text="Deviation In Tour Allocation" Style="color: black;
                    font-weight: bold;" OnClientClick="TourAllocationDev();" />
            </td>
            <td>
                <asp:Button ID="btnCountryReport" runat="server" Text="Country Room Allocation" Style="color: black;
                    font-weight: bold;" OnClientClick="CountryReport();" />
            </td>
            <td>
                <asp:Button ID="btnCityReport" runat="server" Text="City Tour Allocation" Style="color: black;
                    font-weight: bold;" OnClientClick="CityReport();" />
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Next To Step 4" Style="color: black;
                    font-weight: bold;" OnClientClick="NextStep();" />
            </td>
        </tr>
    </table>
</asp:Content>
