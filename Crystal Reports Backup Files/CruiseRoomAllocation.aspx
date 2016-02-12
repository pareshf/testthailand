<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="CruiseRoomAllocation.aspx.cs" Inherits="CRM.WebApp.Views.Sales.CruiseRoomAllocation" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
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
    <%--Coding of Page Start--%>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript" src="../Shared/Javascripts/CruiseRoomAllocation.js"></script>
        <script type="text/javascript">

            function pageLoad() {
                passengerMasterTableView = $find("<%=radgridpassengermaster.ClientID %>").get_masterTableView();
                cruiseMasterTableView = $find("<%=radgridCruiseMaster.ClientID %>").get_masterTableView();
                cruiseRoomTableView = $find("<%=radgridCruiseRoomDetails.ClientID %>").get_masterTableView();

                var q = window.location.search.substring(1);
                if (q != "") {
                    BOOKING_ID = getValue("BOOKING_ID");
                    TOUR_ID = getValue("TOUR_ID");
                    CRM.WebApp.webservice.AutoComplete.searchQueryResultOnSecondFile(TOUR_ID);
                   
                    CRM.WebApp.webservice.CruiseRoomAllocationWebService._staticInstance.GetCruisePassDetails(BOOKING_ID, upadtepassgrid);
                }
                else {

                    CRM.WebApp.webservice.CruiseRoomAllocationWebService._staticInstance.GetCruisePassDetails(BOOKING_ID, upadtepassgrid);
                }
            }

            function newCruise() {
                CRM.WebApp.webservice.CruiseRoomAllocationWebService.NewCruiseInsert(TOUR_ID, BOOKING_DETAIL_ID);

            }

            function newrowAdded(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                ary[1] = TOUR_ID;
                ary[2] = cruiseMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CRUISE_COMP_NAME").value;
                ary[3] = cruiseMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CABINE_CATEGORY").value;
                ary[4] = cruiseMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CABINE_TO_BE_BLOCKED").value;
                ary[5] = cruiseMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECK_REQ_TO").value;
                ary[6] = cruiseMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECK_COMMENTS").value;
                ary[7] = cruiseMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_REQ_TO").value;
                ary[8] = cruiseMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("TOTAL_ROOMS_BLOCKED").value;
                ary[9] = cruiseMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("TIME_LIMIT").value;
                ary[10] = cruiseMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_STATUS_NAME").value;
                ary[11] = cruiseMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("TOTAL_AMOUNT").value;
                ary[12] = cruiseMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("TAX").value;
                ary[13] = cruiseMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("GST").value;
                ary[14] = cruiseMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CRUISE_NAME").value;

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

                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CRUISE_SR_NO;

                try {
                    CRM.WebApp.webservice.CruiseRoomAllocationWebService.InsertUpdateCruiseDetails(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.CruiseRoomAllocationWebService.GetCruiseDetails(BOOKING_DETAIL_ID, updateCruiseGrid);
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }


            function roomAllocation(sender, args) {

                var currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
                ary[1] = cruiseRoomTableView.get_dataItems()[currentRowIndex - 1].findElement("DECK_NO").value;
                ary[2] = cruiseRoomTableView.get_dataItems()[currentRowIndex - 1].findElement("CABINE_NO").value;
                ary[3] = cruiseRoomTableView.get_dataItems()[currentRowIndex - 1].findElement("ADULT1").value;
                ary[4] = cruiseRoomTableView.get_dataItems()[currentRowIndex - 1].findElement("ADULT2").value;
                ary[5] = cruiseRoomTableView.get_dataItems()[currentRowIndex - 1].findElement("ADULT3").value;
                ary[6] = cruiseRoomTableView.get_dataItems()[currentRowIndex - 1].findElement("CWB").value;
                ary[7] = cruiseRoomTableView.get_dataItems()[currentRowIndex - 1].findElement("CNB1").value;
                ary[8] = cruiseRoomTableView.get_dataItems()[currentRowIndex - 1].findElement("CNB2").value;
                ary[9] = cruiseRoomTableView.get_dataItems()[currentRowIndex - 1].findElement("INFANT").value;
                ary[10] = SHARE;

                for (var i = 0; i < 11; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }

                try {
                    CRM.WebApp.webservice.CruiseRoomAllocationWebService.InsertCruiseRoom(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.CruiseRoomAllocationWebService.GetCruiseRooms(CRUISE_SRNO, updateCruiseRoomGrid);
                }
                catch (e) {
                    alert('Wrong Data Inserted');
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

            function getCruiseRoomData() {
                CRM.WebApp.webservice.CruiseRoomAllocationWebService.GetCruiseDetailsReport(TOUR_ID, updateCruiseGrid);
            }

            function getCruiseCompName(sender) {
                var value = sender.value;
                CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
            }

            function getCruiseData(sender) {

                for (i = 1; i < 55; i++) {
                    if (i < 10)
                        i = '0' + i;
                    var txtcruisename = document.getElementById('ctl00_cphPageContent_radgridCruiseMaster_ctl00_ctl' + i + '_CRUISE_NAME');
                    var txtdeptime = document.getElementById('ctl00_cphPageContent_radgridCruiseMaster_ctl00_ctl' + i + '_TIME_OF_DEPARTURE');
                    var txtarrtime = document.getElementById('ctl00_cphPageContent_radgridCruiseMaster_ctl00_ctl' + i + '_TIME_OF_ARRIVAL');
                    var txtdepdate = document.getElementById('ctl00_cphPageContent_radgridCruiseMaster_ctl00_ctl' + i + '_DATE_OF_SAILING');
                    var txtarrdate = document.getElementById('ctl00_cphPageContent_radgridCruiseMaster_ctl00_ctl' + i + '_DATE_OF_ARRIVAL');

                    if (txtcruisename != null && sender.id == txtcruisename.id) {
                        $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: sender.value, key: "FETCH_CRUISE_DEP_DATE" }, function (data) { txtdepdate.value = data; });
                        $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: sender.value, key: "FETCH_CRUISE_DEP_TIME" }, function (data) { txtdeptime.value = data; });
                        $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: sender.value, key: "FETCH_CRUISE_ARR_DATE" }, function (data) { txtarrdate.value = data; });
                        $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: sender.value, key: "FETCH_CRUISE_ARR_TIME" }, function (data) { txtarrtime.value = data; });
                        break;
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

            function NextStep() {
                window.location = "BookingCustomerPayment.aspx?TOUR_ID=" + TOUR_ID;
            }

            function TourAllocation() {
                window.open('TourCruiseRoomAllocationStd.aspx?key=' + TOUR_ID);
            }

            function TourAllocationDeviation() {
                window.open('TourCruiseRoomAllocationDev.aspx?key=' + TOUR_ID);
            }
    </script>

    


    </telerik:radcodeblock>
    <%--Coding of Page End--%>
    <%--Coding Dropdown Start--%>
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        $(document).ready(function () {

            var bookingstatus = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_BOOKING_STATUS_AUTOSEARCH";
            var empname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_EMPLOYEE_FOR_TASKMASTER_AUTOSEARCH";
            var cruisename = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_CRUISE_AUTOSEARCH_DUALPARAM?" + globalvalue;
            var cabinecategory = "../../webservice/autocomplete.ashx?key=FETCH_CRUISE_CABINE_CATEGORY_AUTOSEARCH";
            var cruisecompany = "../../webservice/autocomplete.ashx?key=FETCH_CRUISE_COMP_NAME_AUTOSEARCH";

            var adult_passenger_name = "../../webservice/autocomplete.ashx?key=FETCH_ADULT_PASSENGER_NAME_FROM_BOOKING?" + globalvalue + "?" + a;
            var cwb_passenger_name = "../../webservice/autocomplete.ashx?key=FETCH_CWB_PASSENGER_NAME_FROM_BOOKING?" + globalvalue + "?" + a;
            var cnb_passenger_name = "../../webservice/autocomplete.ashx?key=FETCH_CNB_PASSENGER_NAME_FROM_BOOKING?" + globalvalue + "?" + a;
            var infant_passenger_name = "../../webservice/autocomplete.ashx?key=FETCH_INFANT_PASSENGER_NAME_FROM_BOOKING?" + globalvalue + "?" + a;
            var tourshortname = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_SHORT_SHORT_NAME_AUTOSEARCH";
            var tourcode = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_CODE_ON_TOUR_SHORT_NAME?" + globalvalue;

            $("#ctl00_cphPageContent_txtTourName").autocomplete(tourshortname);
            $("#ctl00_cphPageContent_txtTourCode").autocomplete(tourcode);



            for (var i = 1; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;


                $("#ctl00_cphPageContent_radgridCruiseMaster_ctl00_ctl" + i + "_CRUISE_COMP_NAME").autocomplete(cruisecompany);
                $("#ctl00_cphPageContent_radgridCruiseMaster_ctl00_ctl" + i + "_CABINE_CATEGORY").autocomplete(cabinecategory);
                $("#ctl00_cphPageContent_radgridCruiseMaster_ctl00_ctl" + i + "_CHECK_REQ_TO").autocomplete(empname);
                $("#ctl00_cphPageContent_radgridCruiseMaster_ctl00_ctl" + i + "_BOOKING_REQ_TO").autocomplete(empname);
                $("#ctl00_cphPageContent_radgridCruiseMaster_ctl00_ctl" + i + "_BOOKING_STATUS_NAME").autocomplete(bookingstatus);
                $("#ctl00_cphPageContent_radgridCruiseMaster_ctl00_ctl" + i + "_CRUISE_NAME").autocomplete(cruisename);

                $("#ctl00_cphPageContent_radgridCruiseRoomDetails_ctl00_ctl" + i + "_ADULT1").autocomplete(adult_passenger_name);
                $("#ctl00_cphPageContent_radgridCruiseRoomDetails_ctl00_ctl" + i + "_ADULT2").autocomplete(adult_passenger_name);
                $("#ctl00_cphPageContent_radgridCruiseRoomDetails_ctl00_ctl" + i + "_ADULT3").autocomplete(adult_passenger_name);
                $("#ctl00_cphPageContent_radgridCruiseRoomDetails_ctl00_ctl" + i + "_CWB").autocomplete(cwb_passenger_name);
                $("#ctl00_cphPageContent_radgridCruiseRoomDetails_ctl00_ctl" + i + "_CNB1").autocomplete(cnb_passenger_name);
                $("#ctl00_cphPageContent_radgridCruiseRoomDetails_ctl00_ctl" + i + "_CNB2").autocomplete(cnb_passenger_name);
                $("#ctl00_cphPageContent_radgridCruiseRoomDetails_ctl00_ctl" + i + "_INFANT").autocomplete(infant_passenger_name);
            }
        });
    
    </script>
    <%--Coding Dropdown Start--%>
    <%--Designing of Page Start--%>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <%--Designing TourName Start--%>
    <div id="radgridpassengerdetails">
        <div class="pageTitle" style="float: left">
            <asp:Literal ID="Literal1" runat="server" Text="Cruise Room Allocation"></asp:Literal>
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
                        OnClientClick="getCruiseRoomData();" />
                </td>
            </tr>
        </table>
        <br />
        <%--Designing TourName End--%>
        <%--Designing of 1st grid Start--%>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridpassengermaster" runat="server" allowpaging="false" allowmultirowselection="false"
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
                                        <telerik:GridTemplateColumn SortExpression="TITLE_DESC" DataField="TITLE_DESC" HeaderText="Title" >
                                            <ItemTemplate>
                                                <asp:TextBox id="TITLE_DESC" runat="server" CssClass="radinput" Readonly="True"></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn SortExpression="CUST_NAME" DataField="CUST_NAME" HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:TextBox id="CUST_NAME" runat="server" CssClass="radinput" Readonly="True"></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn SortExpression="CUST_SURNAME" DataField="CUST_SURNAME" HeaderText="Surname">
                                            <ItemTemplate>
                                                <asp:TextBox id="CUST_SURNAME" runat="server" CssClass="radinput" Readonly="True"></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn SortExpression="SHARE_ROOM_IN_CRUISE" DataField="SHARE_ROOM_IN_CRUISE" HeaderText="Share Room In Cruise?">
                                            <ItemTemplate>
                                                <asp:TextBox id="SHARE_ROOM_IN_CRUISE"  runat="server" CssClass="radinput" Readonly="True"></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                            </MasterTableView>
                            <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridpassengermaster_Command" OnRowSelected="radgridpassengermaster_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
                  </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
    <%--Designing of 1st grid End--%>
    <%--Designing of 2nds grid Start--%>
    <div id="radgridCruiseMaster">
        <div class="pageTitle" style="float: left">
            <asp:Literal ID="Literal3" runat="server" Text="Cruise Detail"></asp:Literal>
        </div>
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridCruiseMaster" runat="server" allowpaging="false" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
                            <MasterTableView ClientDataKeyNames="" AllowMultiColumnSorting="true" EditMode="InPlace" width="4500px">
                                <RowIndicatorColumn>
                                </RowIndicatorColumn>
                                    <Columns>
                                               <telerik:GridTemplateColumn SortExpression="TOUR_ID" DataField="TOUR_ID" HeaderText="TOUR_ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox id="TOUR_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn SortExpression="BOOKING_DETAIL_ID" DataField="BOOKING_DETAIL_ID" HeaderText="BOOKING_DETAIL_ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox id="BOOKING_DETAIL_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>            
                                        <telerik:GridTemplateColumn SortExpression="CRUISE_SR_NO" DataField="CRUISE_SR_NO" HeaderText="Title"   Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox id="CRUISE_SR_NO" runat="server" CssClass="radinput" ></asp:TextBox>
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
                    
                                    <telerik:GridTemplateColumn DataField="CABINE_CATEGORY" HeaderText="▼ Cabine Category" SortExpression="CABINE_CATEGORY">
                                         <ItemTemplate>
                                             <asp:TextBox id="CABINE_CATEGORY" runat="server" CssClass="radinput"></asp:TextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="CABINE_TO_BE_BLOCKED" HeaderText="Cabine To Be Blocked" SortExpression="CABINE_TO_BE_BLOCKED">
                                        <ItemTemplate>
                                            <asp:TextBox id="CABINE_TO_BE_BLOCKED" runat="server" CssClass="radinput"></asp:TextBox>
                                         </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="CHECK_REQ_TO" HeaderText="▼ Check Request To" SortExpression="CHECK_REQ_TO">
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
                                    <telerik:GridTemplateColumn DataField="BOOKING_REQ_TO" HeaderText="▼ Booking Request To" SortExpression="BOOKING_REQ_TO">
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
                     
                                <telerik:GridTemplateColumn DataField="APPROVED_BY" HeaderText="▼ Approved By">
                                    <ItemTemplate>
                                            <asp:TextBox id="APPROVED_BY" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="BOOKED_BY" HeaderText="▼ Booked By">
                                    <ItemTemplate>
                                           <asp:TextBox id="BOOKED_BY" runat="server" readonly="true" CssClass="radinput" style="background-color:LightBlue"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="BOOKING_STATUS_NAME" HeaderText="▼ Booking Status" SortExpression="BOOKING_STATUS_NAME">
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
                                    <a id="A7" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newrowAdded(this,event);">
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
                          <ClientEvents OnCommand="radgridCruiseMaster_Command" OnRowSelected="radgridCruiseMaster_RowSelected" OnRowDblClick="CruiseRowClick"/>
                          <Selecting AllowRowSelect="true"/>
                    </ClientSettings>
                  </telerik:radgrid>
                    <asp:LinkButton ID="lblCruise" runat="server" Text="Add Another Cruise" OnClientClick="newCruise();"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <%--Designing of 2nd grid End--%>
    <%--Designing of 2nd grid End--%>
    <div>
        <div class="pageTitle" style="float: left">
            <asp:Literal ID="Literal2" runat="server" Text="Room Allocation Detail"></asp:Literal>
        </div>
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridCruiseRoomDetails" runat="server" pagesize="50" allowpaging="false"
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
                                <telerik:GridTemplateColumn SortExpression="BOOKING_CRUISE_SRNO" DataField="BOOKING_CRUISE_SRNO" HeaderText="BOOKING_CRUISE_SRNO" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox id="BOOKING_CRUISE_SRNO" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="DECK_NO" HeaderText="Deck No" UniqueName="DECK_NO">
                                    <ItemTemplate>
                                        <asp:TextBox id="DECK_NO" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="CABINE_NO" HeaderText="Cabine No" UniqueName="CABINE_NO">
                                    <ItemTemplate>
                                        <asp:TextBox id="CABINE_NO" runat="server" CssClass="radinput"></asp:TextBox>
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
                                 <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField="PHOTO">
                                    <ItemStyle CssClass="ItemAlign" Width="25px" />
                                    <HeaderStyle Width="25px" />
                                    <ItemTemplate>
                                          <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="roomAllocation(this,event);">
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
                <ClientEvents OnCommand="radgridCruiseRoomDetails_Command" OnRowDblClick="CruiseRoomRowClick"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
                        </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
    <%--Designing of 2nd grid End--%>
    <%--Designing of Report Button Start--%>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnreport3" runat="server" Text="Tour Allocation" Style="color: black;
                        font-weight: bold;" OnClientClick="TourAllocation();" />
                </td>
                <td>
                    <asp:Button ID="btnreport4" runat="server" Text="Deviation In Tour Allocation" Style="color: black;
                        font-weight: bold;" OnClientClick="TourAllocationDeviation();" />
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Next To Step 5" Style="color: black;
                        font-weight: bold;" OnClientClick="NextStep();" />
                </td>
            </tr>
        </table>
    </div>
    <%--Designing of Report Button Start--%>
    <%--Designing of Page End--%>
</asp:Content>
