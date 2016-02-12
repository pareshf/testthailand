<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="BookingFormStep1.aspx.cs" Inherits="CRM.WebApp.Views.Sales.BookingFormStep1" %>

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
    <script type="text/javascript" src="../Shared/Javascripts/BookingFormStep1.js"></script>
    <script type="text/javascript">
        var inq_id;
        function pageLoad() {


            inqid = getValue("INQ_ID");
            inq_id = getValue("INQ_ID");

            MasterTableView = $find("<%= radgridmaster.ClientID %>").get_masterTableView();
            CustomerTableView = $find("<%= radgridCustomer.ClientID %>").get_masterTableView();
            CustomerRlationTableView = $find("<%= radgridCustomerRelation.ClientID %>").get_masterTableView();
            CustomerVisaTableView = $find("<%= radgridCustomerVisa.ClientID %>").get_masterTableView();

            var q = window.location.search.substring(1);
            if (q != "") {

                BOOKING_ID = getValue("BOOKING_ID");
                if (BOOKING_ID != 0) {

                    document.getElementById('ctl00_cphPageContent_btnnewcustomer').style.visibility = "hidden";
                }
                else {
                    document.getElementById('ctl00_cphPageContent_btnnewcustomer').style.visibility = "visible";
                }

                inq_id = getValue("INQ_ID");
                cust_id = getValue("CUST_ID");
                tour_name = getValue("TOUR_SNM");
                tour_code = getValue("TOUR_CODE");

                document.getElementById('ctl00_cphPageContent_radgridmaster_ctl00_ctl04_TOUR_SHORT_NAME').value = tour_name;
                document.getElementById('ctl00_cphPageContent_radgridmaster_ctl00_ctl04_TOUR_CODE').value = tour_code;
                document.getElementById('ctl00_cphPageContent_radgridmaster_ctl00_ctl04_INQUIRY_ID').value = inq_id;
                CRM.WebApp.webservice.BookingFormStep1.GetCustomer(inqid, updateCustomerGrid);

            }
            else {

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
        var currentTextBox = null; var currentDatePicker = null; function showPopup(sender, e) {
            try { currentTextBox = sender; var datePicker = $find("<%= RadDatePicker1.ClientID %>"); currentDatePicker = datePicker; datePicker.set_selectedDate(currentDatePicker.get_dateInput().parseDate(sender.value)); var position = datePicker.getElementPosition(sender); datePicker.showPopup(position.x, position.y + sender.offsetHeight); }
            catch (e) { }
        }
        function dateSelected(sender, args) {
            try { if (currentTextBox != null) { currentTextBox.value = args.get_newDate().format('dd/MM/yyyy'); currentDatePicker.hidePopup(); } }
            catch (e) { }

        }
        function parseDate(sender, e) { currentDatePicker.hidePopup(); }
        function PopUpShowing(sender, args) { var divmore = document.getElementById('divmore'); divmore.style.display = 'block'; divmore.style.position = 'Absolute'; divmore.style.left = screen.width / 2 - 150; divmore.style.top = screen.height / 2 - 150; var IMG = document.getElementById("imgexistingimage"); IMG.src = args.srcElement.all[1].value; }
        function disablepopup() { var divmore = document.getElementById('divmore'); divmore.style.display = 'none'; return false; }

        function BindData(sender) {
            INQUIRY_ID = sender.value;
            CRM.WebApp.webservice.AutoComplete.searchQueryResult(sender.value);
            CRM.WebApp.webservice.BookingFormStep1.GetCustomer(sender.value, updateCustomerGrid);
        }
        function updateCustomerDetail(sender, args) { //update customer self detail
            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var ary = [];

            ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_ID;
            ary[1] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_UNQ_ID").value;
            ary[2] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_TITLE").value;
            ary[3] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_NAME").value;
            ary[4] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_SURNAME").value;
            ary[5] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_ADDRESS_LINE1").value;
            ary[6] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_ADDRESS_LINE2").value;
            ary[7] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_COUNTRY_NAME").value;
            ary[8] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_STATE_NAME").value;
            ary[9] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_CITY_NAME").value;
            ary[10] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_PINCODE").value;
            ary[11] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_PHONE").value;
            ary[12] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_MOBILE").value;
            ary[13] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("EMM_ADDRESS_LINE1").value;
            ary[14] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("EMM_ADDRESS_LINE2").value;
            ary[15] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("EMM_COUNTRY_NAME").value;
            ary[16] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("EMM_STATE_NAME").value;
            ary[17] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("EMM_CITY_NAME").value;
            ary[18] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("EMM_PINCODE").value;
            ary[19] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("EMM_PHONE").value;
            ary[20] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("EMM_MOBILE").value;
            ary[21] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("EMM_NAME").value;
            ary[22] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.REL_SRNO;
            ary[23] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.EMM_SRNO;
            //ary[24] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_EMAIL").value;
            //ary[25] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_SRNO").value;
            //ary[22] = '';
            //ary[23] = '';
            ary[24] = '';
            ary[25] = '';

            for (i = 0; i < 26; i++) {
                if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
            }

            if (!ary[5] == 0 && !ary[6] == 0) {
                if (!ary[20] == 0 || !ary[21] == 0) {
                    try {
                        CRM.WebApp.webservice.BookingFormStep1.InsertUpdateCustomerDetail(ary);
                        alert('Record Save Successfully');
                        CRM.WebApp.webservice.BookingFormStep1.GetCustomer(INQUIRY_ID, updateCustomerGrid);
                    }
                    catch (e) {
                        alert('Wrong Data Inserted');
                    }
                }
                else {
                    alert('Enter Emmergency Name & Emmergency Mobile No.');
                }
            }
            else {
                alert('Enter Full Residence Address Detail');
            }
        }

        function updateCustomerRelationDetail(sender, args) {
            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var ary = [];

            ary[0] = CustomerRlationTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_TITLE").value;
            ary[1] = CustomerRlationTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_NAME").value;
            ary[2] = CustomerRlationTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_SURNAME").value;
            ary[3] = CustomerRlationTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_BIRTH_DATE").value;
            ary[4] = CustomerRlationTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_MOBILE").value;
            ary[5] = CustomerRlationTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_PHONE").value;
            ary[6] = CustomerRlationTableView.get_dataItems()[currentRowIndex - 1].findElement("NATIONALITY").value;
            ary[7] = CustomerRlationTableView.get_dataItems()[currentRowIndex - 1].findElement("MEAL_NAME").value;
            ary[8] = CustomerRlationTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_PASSPORT_NO").value;
            ary[9] = CustomerRlationTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_PASSPORT_ISSUE_DATE").value;
            ary[10] = CustomerRlationTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_PASSPORT_EXPIRY_DATE").value;
            ary[11] = CustomerRlationTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_PASSPORT_ISSUE_PLACE").value;
            ary[12] = CustomerRlationTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_PASSPORT_ISSUE_COUNTRY").value;
            ary[13] = CustomerRlationTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_PASSPORT_PRINTED_NAME").value;
            ary[14] = CustomerRlationTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_EMAIL").value;
            ary[15] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_ID;
            ary[16] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_ID;
            ary[17] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_SRNO;
            ary[18] = CustomerRlationTableView.get_dataItems()[currentRowIndex - 1].findElement("RELATION").value;
            for (i = 0; i < 19; i++) {
                if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
            }
            try {
                CRM.WebApp.webservice.BookingFormStep1.InsertUpdateCustomerRelationDetail(ary);
                alert('Record Save Successfully');
                CRM.WebApp.webservice.BookingFormStep1.GetCustomerRelative(CUST_ID, updateCustomerRelativeGrid);
            }
            catch (e) {
                alert('Wrong Data Inserted');
            }

        }

        function updateVisaDetail(sender, args) {
            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var ary = [];

            ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_ID;
            ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_ID;
            ary[2] = CustomerVisaTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;
            ary[3] = CustomerVisaTableView.get_dataItems()[currentRowIndex - 1].findElement("VISA_EXPIRY_DATE").value;
            ary[4] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_SRNO;

            for (i = 0; i < 5; i++) {
                if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
            }
            try {
                CRM.WebApp.webservice.BookingFormStep1.InsertUpdateVisaDetail(ary);
                alert('Record Save Successfully');
                CRM.WebApp.webservice.BookingFormStep1.GetVisaDetail(CUST_REL_SRNO, updateVisaGrid);
            }
            catch (e) {
                alert('Wrong Data Inserted');
            }

        }


        function getTourShortName(sender) {
            var value = sender.value;
            CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
        }

        function getCostAndSeats(sender) {
            var tourshortname = document.getElementById('ctl00_cphPageContent_radgridmaster_ctl00_ctl04_TOUR_SHORT_NAME').value;
            var tourcode = document.getElementById('ctl00_cphPageContent_radgridmaster_ctl00_ctl04_TOUR_CODE').value;
            var inquiry = document.getElementById('ctl00_cphPageContent_radgridmaster_ctl00_ctl04_INQUIRY_ID').value;
            value = tourshortname + ' ' + tourcode;
            CRM.WebApp.webservice.AutoComplete.searchQueryResultOnSecondFile(value);
            var a = "";
            var seats = document.getElementById('ctl00_cphPageContent_radgridmaster_ctl00_ctl04_SEATS');
            var cost1 = document.getElementById('ctl00_cphPageContent_radgridmaster_ctl00_ctl04_ACTUAL_COST_C1');
            var cost2 = document.getElementById('ctl00_cphPageContent_radgridmaster_ctl00_ctl04_ACTUAL_COST_C2');

            $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: inquiry, key: "FETCH_TOTAL_QUOTED_COST_C1_DUALPARAM?" + globalvalue + "?" + a }, function (data) { cost1.value = data; });
            $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: tourcode, key: "FETCH_AVAIL_SEATS_FOR_TOUR?" + globalvalue }, function (data) { seats.value = data; });
            $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: inquiry, key: "FETCH_TOTAL_QUOTED_COST_C2_DUALPARAM?" + globalvalue + "?" + a }, function (data) { cost2.value = data; });

        }


        function Redirect() {
            var tourshortname = document.getElementById('ctl00_cphPageContent_radgridmaster_ctl00_ctl04_TOUR_SHORT_NAME').value;
            var tourcode = document.getElementById('ctl00_cphPageContent_radgridmaster_ctl00_ctl04_TOUR_CODE').value;
            //window.location = "BookingFormStep2.aspx?INQ_ID=" + INQUIRY_ID + "&CUST_ID=" + CUST_ID + "&TOUR_SNM=" + tourshortname + "&TOUR_CODE=" + tourcode;

            if (INQUIRY_ID != "") { window.location = "BookingFormStep2.aspx?INQ_ID=" + INQUIRY_ID + "&CUST_ID=" + CUST_ID + "&TOUR_SNM=" + tourshortname + "&TOUR_CODE=" + tourcode; }
            else {
                window.location = "BookingFormStep2.aspx?INQ_ID=" + inq_id + "&CUST_ID=" + CUST_ID + "&TOUR_SNM=" + tourshortname + "&TOUR_CODE=" + tourcode;
            }

        }

        function NewVisa() {
            CRM.WebApp.webservice.BookingFormStep1.InsertNewVisa(cust_id, cust_rel_id, cust_rel_srno);
            CRM.WebApp.webservice.BookingFormStep1.GetVisaDetail(CUST_REL_SRNO, updateVisaGrid);
        }
        function RedirectNewCustomer() {
            var tourshortname = document.getElementById('ctl00_cphPageContent_radgridmaster_ctl00_ctl04_TOUR_SHORT_NAME').value;
            var tourcode = document.getElementById('ctl00_cphPageContent_radgridmaster_ctl00_ctl04_TOUR_CODE').value;

            if (INQUIRY_ID != "") { window.location = "CustomerMaster.aspx?INQ_ID=" + INQUIRY_ID + "&CUST_ID=" + CUST_ID + "&TOUR_SNM=" + tourshortname + "&TOUR_CODE=" + tourcode; }
            else {
                window.location = "CustomerMaster.aspx?INQ_ID=" + inq_id + "&CUST_ID=" + CUST_ID + "&TOUR_SNM=" + tourshortname + "&TOUR_CODE=" + tourcode;
            }



        }
    </script>
</telerik:radcodeblock>
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var tourshortname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_TOUR_SHORTNAME_FOR_BOOKING_MASTER?" + globalvalue;
            var tourcode = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_CODE_ON_TOUR_SHORT_NAME?" + globalvalue;
            var title = "../../webservice/autocomplete.ashx?key=FETCH_TITLE_FOR_EMPLOYEE_MASTER";
            var country = "../../webservice/autocomplete.ashx?key=FETCH_COUNTRY_FOR_EMPLOYEE_MASTER";
            var state = "../../webservice/autocomplete.ashx?key=FETCH_STATE_FOR_EMPLOYEE_MASTER";
            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            var meal = "../../webservice/autocomplete.ashx?key=FETCH_MEAL_DATA_FOR_BOOKINGMASTER_AUTOSEARCH";
            var nationality = "../../webservice/autocomplete.ashx?key=FETCH_NATIONALITY_FOR_BOOKING_MASTER";
            var relation = "../../webservice/autocomplete.ashx?key=FETCH_RELATION_FROM_RELATION_MASTER";

            $("#ctl00_cphPageContent_radgridmaster_ctl00_ctl04_TOUR_SHORT_NAME").autocomplete(tourshortname);
            $("#ctl00_cphPageContent_radgridmaster_ctl00_ctl04_TOUR_CODE").autocomplete(tourcode);
            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridCustomer_ctl00_ctl" + i + "_REL_TITLE").autocomplete(title);
                $("#ctl00_cphPageContent_radgridCustomer_ctl00_ctl" + i + "_REL_CITY_NAME").autocomplete(city);
                $("#ctl00_cphPageContent_radgridCustomer_ctl00_ctl" + i + "_REL_STATE_NAME").autocomplete(state);
                $("#ctl00_cphPageContent_radgridCustomer_ctl00_ctl" + i + "_REL_COUNTRY_NAME").autocomplete(country);
                $("#ctl00_cphPageContent_radgridCustomer_ctl00_ctl" + i + "_EMM_CITY_NAME").autocomplete(city);
                $("#ctl00_cphPageContent_radgridCustomer_ctl00_ctl" + i + "_EMM_STATE_NAME").autocomplete(state);
                $("#ctl00_cphPageContent_radgridCustomer_ctl00_ctl" + i + "_EMM_COUNTRY_NAME").autocomplete(country);
                $("#ctl00_cphPageContent_radgridCustomerRelation_ctl00_ctl" + i + "_REL_TITLE").autocomplete(title);
                $("#ctl00_cphPageContent_radgridCustomerRelation_ctl00_ctl" + i + "_REL_PASSPORT_ISSUE_COUNTRY").autocomplete(country);
                $("#ctl00_cphPageContent_radgridCustomerRelation_ctl00_ctl" + i + "_REL_PASSPORT_ISSUE_PLACE").autocomplete(city);
                $("#ctl00_cphPageContent_radgridCustomerRelation_ctl00_ctl" + i + "_REL_PASSPORT_ISSUE_PLACE").autocomplete(city);
                $("#ctl00_cphPageContent_radgridCustomerRelation_ctl00_ctl" + i + "_MEAL_NAME").autocomplete(meal);
                $("#ctl00_cphPageContent_radgridCustomerRelation_ctl00_ctl" + i + "_NATIONALITY").autocomplete(nationality);
                $("#ctl00_cphPageContent_radgridCustomerRelation_ctl00_ctl" + i + "_RELATION").autocomplete(relation);
                $("#ctl00_cphPageContent_radgridCustomerVisa_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(country);
            }

        });

        
    </script>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageBooking" runat="server" Text="Booking Master Step - 1"></asp:Literal>
    </div>
    <br />
    <br />
    <table>
        <tr style="height: 10px; width: 10px;">
            <td style="background-color: LightBlue; height: 10px; width: 10px;">
            </td>
            <td>
                <asp:Label ID="lbl1" runat="server" Text="Read Only Fields"></asp:Label>
            </td>
        </tr>
    </table>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <br />
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridmaster" runat="server" allowpaging="true" allowmultirowselection="false"
                    allowsorting="True" pagesize="1" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
                        <MasterTableView AllowMultiColumnSorting="true" EditMode="InPlace" Width="1000">
                        <RowIndicatorColumn>
             </RowIndicatorColumn>
             <Columns>
                <telerik:GridTemplateColumn HeaderText="Inquiry Id">
                        <ItemTemplate>
                            <asp:TextBox ID="INQUIRY_ID" runat="server" CssClass="radinput" Text="0" onblur="BindData(this);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Tour Short Name">
                        <ItemTemplate>
                            <asp:TextBox ID="TOUR_SHORT_NAME" runat="server" CssClass="radinput" onblur="getTourShortName(this);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Tour Code">
                        <ItemTemplate>
                            <asp:TextBox ID="TOUR_CODE" runat="server" CssClass="radinput" onblur="getCostAndSeats(this);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Availlable Seats">
                        <ItemTemplate>
                            <asp:TextBox ID="SEATS" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Actual Cost C1">
                        <ItemTemplate>
                            <asp:TextBox ID="ACTUAL_COST_C1" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Actual Cost C2">
                        <ItemTemplate>
                            <asp:TextBox ID="ACTUAL_COST_C2" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
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
    </table>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblCustDetail" runat="server" Text="Customer Detail"></asp:Literal>
    </div>
    <br />
    <br />
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridCustomer" runat="server" allowpaging="true" allowmultirowselection="false"
                    allowsorting="True" pagesize="1" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
                    <MasterTableView AllowMultiColumnSorting="true" EditMode="InPlace" Width="4000px">
                    <RowIndicatorColumn>
             </RowIndicatorColumn>
             <Columns>
                <telerik:GridTemplateColumn DataField="CUST_ID" HeaderText="Cust Id" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CUST_REL_SRNO" HeaderText="Cust Rel Srno" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_SRNO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_SRNO" HeaderText="Residence Srno" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_SRNO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="EMM_SRNO" HeaderText="Emergency Srno" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="EMM_SRNO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CUST_UNQ_ID" HeaderText="Customer Unique Id">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_UNQ_ID" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_TITLE" HeaderText="Title" >
                        <ItemTemplate>
                            <asp:TextBox ID="REL_TITLE" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_NAME" HeaderText="Customer Name">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_NAME" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_SURNAME" HeaderText="Customer Surname" >
                        <ItemTemplate>
                            <asp:TextBox ID="REL_SURNAME" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn DataField="REL_MOBILE" HeaderText="Residence Mobile">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_MOBILE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_PHONE" HeaderText="Residence Phone" >
                        <ItemTemplate>
                            <asp:TextBox ID="REL_PHONE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_ADDRESS_LINE1" HeaderText="Residence Address Line1">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_ADDRESS_LINE1" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_ADDRESS_LINE2" HeaderText="Residence Address Line2">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_ADDRESS_LINE2" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_CITY_NAME" HeaderText="Residence City Name">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_CITY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_STATE_NAME" HeaderText="Residence State Name">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_STATE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_COUNTRY_NAME" HeaderText="Residence Country Name">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_COUNTRY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_PINCODE" HeaderText="Residence Pincode">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_PINCODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="EMM_NAME" HeaderText="Emergency Name">
                        <ItemTemplate>
                            <asp:TextBox ID="EMM_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="EMM_MOBILE" HeaderText="Emergency Mobile">
                        <ItemTemplate>
                            <asp:TextBox ID="EMM_MOBILE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="EMM_PHONE" HeaderText="Emergency Phone" >
                        <ItemTemplate>
                            <asp:TextBox ID="EMM_PHONE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="EMM_ADDRESS_LINE1" HeaderText=" Emergency Address Line1" >
                        <ItemTemplate>
                            <asp:TextBox ID="EMM_ADDRESS_LINE1" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="EMM_ADDRESS_LINE2" HeaderText="Emergency Address Line2" >
                        <ItemTemplate>
                            <asp:TextBox ID="EMM_ADDRESS_LINE2" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="EMM_CITY_NAME" HeaderText="Emergency City Name" >
                        <ItemTemplate>
                            <asp:TextBox ID="EMM_CITY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="EMM_STATE_NAME" HeaderText="Emergency State Name" >
                        <ItemTemplate>
                            <asp:TextBox ID="EMM_STATE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="EMM_COUNTRY_NAME" HeaderText="Emergency Country Name" >
                        <ItemTemplate>
                            <asp:TextBox ID="EMM_COUNTRY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="EMM_PINCODE" HeaderText="Emergency Pincode" >
                        <ItemTemplate>
                            <asp:TextBox ID="EMM_PINCODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CUST_TYPE" HeaderText="Customer Type" >
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_TYPE" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_EMAIL" HeaderText="Customer Email">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_EMAIL" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField="PHOTO">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="updateCustomerDetail(this,event);">
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
                <ClientEvents OnCommand="radgridCustomer_Command" OnRowSelected="radgridCustomer_RowSelected" OnRowDblClick="CustomerRowClick"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>          
                </telerik:radgrid>
            </td>
        </tr>
    </table>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal1" runat="server" Text="Passenger Detail"></asp:Literal>
    </div>
    <br />
    <br />
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridCustomerRelation" runat="server" allowpaging="false"
                    allowmultirowselection="false" allowsorting="True" pagesize="10" itemstyle-wrap="false"
                    enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
                    <MasterTableView AllowMultiColumnSorting="true" EditMode="InPlace" Width="3000px">
                    <RowIndicatorColumn>
             </RowIndicatorColumn>
             <Columns>
                <telerik:GridTemplateColumn DataField="CUST_REL_SRNO" HeaderText="Customer Relation Srno" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_SRNO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CUST_UNQ_ID" HeaderText="Customer Uniqe Id" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_UNQ_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CUST_ID" HeaderText="Customer Id" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_TITLE" HeaderText="Title">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_TITLE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_NAME" HeaderText="Name">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_SURNAME" HeaderText="Surname">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_SURNAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn DataField="RELATION" HeaderText="Relation">
                        <ItemTemplate>
                            <asp:TextBox ID="RELATION" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CUST_REL_ID" HeaderText="Customer Relation Id" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_BIRTH_DATE" HeaderText="Birth Date">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_BIRTH_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"
                            onfocus="showPopup(this, event);"
                            onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_EMAIL" HeaderText="Email">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_EMAIL" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_MOBILE" HeaderText="Mobile">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_MOBILE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_PHONE" HeaderText="Phone">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_PHONE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_PASSPORT_NO" HeaderText="Passport NO">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_PASSPORT_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_PASSPORT_ISSUE_DATE" HeaderText="Passport Issue Date">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_PASSPORT_ISSUE_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"
                            onfocus="showPopup(this, event);"
                            onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_PASSPORT_ISSUE_PLACE" HeaderText="Passport Issue Place">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_PASSPORT_ISSUE_PLACE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_PASSPORT_EXPIRY_DATE" HeaderText="Passport Expiry Date">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_PASSPORT_EXPIRY_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"
                            onfocus="showPopup(this, event);"
                            onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_PASSPORT_PRINTED_NAME" HeaderText="Passport Printed Name">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_PASSPORT_PRINTED_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="REL_PASSPORT_ISSUE_COUNTRY" HeaderText="Passport Issue Country">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_PASSPORT_ISSUE_COUNTRY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="MEAL_NAME" HeaderText="Meal Name">
                        <ItemTemplate>
                            <asp:TextBox ID="MEAL_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="NATIONALITY" HeaderText="Nationality">
                        <ItemTemplate>
                            <asp:TextBox ID="NATIONALITY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField="PHOTO">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="updateCustomerRelationDetail(this,event);">
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
                <ClientEvents OnCommand="radgridCustomerRelation_Command" OnRowSelected="radgridCustomerRelation_RowSelected" OnRowDblClick="RowClick"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>          
                </telerik:radgrid>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Button ID="btnnewcustomer" runat="server" Text="Add New Customer" OnClientClick="RedirectNewCustomer();"
                    Style="color: black;" />
            </td>
        </tr>
    </table>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal2" runat="server" Text="Customer Visa Detail"></asp:Literal>
    </div>
    <br />
    <br />
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridCustomerVisa" runat="server" allowpaging="false" allowmultirowselection="false"
                    allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
                    <MasterTableView AllowMultiColumnSorting="true" EditMode="InPlace" Width="500px">
                    <RowIndicatorColumn>
                    </RowIndicatorColumn>
             <Columns>
                <telerik:GridTemplateColumn DataField="CUST_ID" HeaderText="Customer Id" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="COUNTRY_ID" HeaderText="Customer Id" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CUST_REL_SRNO" HeaderText="Customer Rel Srno" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_SRNO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CUST_REL_ID" HeaderText="Customer Rel Id" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="COUNTRY_NAME" HeaderText="Country Name" >
                        <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="VISA_EXPIRY_DATE" HeaderText="Expiry Date" >
                        <ItemTemplate>
                            <asp:TextBox ID="VISA_EXPIRY_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"
                            onfocus="showPopup(this, event);"
                            onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField="PHOTO">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A3" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="updateVisaDetail(this,event);">
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
                <ClientEvents OnCommand="radgridCustomerVisa_Command" OnRowDblClick="VisaRowClick" />
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>          
                </telerik:radgrid>
                <asp:LinkButton ID="lbAddPayment" runat="server" Text="Add New Visa" OnClientClick="NewVisa();"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Button ID="btnNext" runat="server" Text="Next To Step 2" OnClientClick="Redirect();"
                    Style="color: black; font-weight: bold;" />
            </td>
        </tr>
    </table>
</asp:Content>
