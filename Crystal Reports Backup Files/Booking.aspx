<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="CRM.WebApp.Views.Sales.Booking" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="cntIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="cntPageConten" ContentPlaceHolderID="cphPageContent" runat="server">
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
            <script type ="text/javascript" src="../Shared/Javascripts/BookingMaster.js"></script>
            
            <script type="text/javascript">

                function pageLoad() {

                    customerrelationTableView = $find("<%= radgridcustomerrelation.ClientID %>").get_masterTableView();
                    customerrelationCommandName = "Load";
                    customervisaTableView = $find("<%=radgridcustomervisa.ClientID %>").get_masterTableView();
                    customervisaCommandName = "Load";
                    customerPaymentTableView = $find("<%=radgridpaymentinfo.ClientID %>").get_masterTableView();
                    customerpaymentCommandName = "Load";
                    bookingaddressTableView = $find("<%= radgridaddressinfo.ClientID %>").get_masterTableView();
                    bookingaddressCommandName = "Load";
                    
                    var today = new Date().format('MM/dd/yyyy');
                    document.getElementById('<%=txtbookingdate.ClientID%>').value = today;
                    var takenby = '<%=Session["usersname"]%>';
                    document.getElementById('<%=txtbookingtakenby.ClientID%>').value = takenby;

                }

                function getaddressname(sender) {
                    var minimum = 1;
                    var maximum = 100000;
                    document.getElementById("ctl00_cphPageContent_txtbookingcode").value = "B" + (Math.floor(Math.random() * (maximum - minimum + 1)) + minimum).toString();
                    value = sender.value;
                    CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
                    bookingaddressTableView = $find("<%= radgridaddressinfo.ClientID %>").get_masterTableView();
                    bookingaddressCommandName = "Load";
                    CRM.WebApp.webservice.BookingMaster.GetaddressDetails(value, updatebookingaddressGrid);
                    INQUIRY_ID = document.getElementById('<%=txtINQUIRY_ID.ClientID%>').value;
                    //CRM.WebApp.webservice.BookingMaster.Getbookinginfo(INQUIRY_ID, output);
                    //document.getElementById('<%=txttour.ClientID%>');

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

                            currentTextBox.value = args.get_newDate().format('MM/dd/yyyy');
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
                function addnewaddressdetail(sender, args) {
                    currentRowIndex = sender.parentNode.parentNode.rowIndex;
                    var ary = [];
                    for (var i = 0; i < 21; i++) {

                        if (i < 10)
                            i = '0' + i;

                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_TITLE_DESC') {
                            ary[1] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_CUST_NAME') {
                            ary[2] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_CUST_SURNAME') {
                            ary[3] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_RES_ADD_LINE1') {
                            ary[4] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_RES_ADD_LINE2') {
                            ary[5] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_RES_MOBILE') {
                            ary[6] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_RES_PHONE') {
                            ary[7] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_RES_COUNTRY') {
                            ary[8] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_RES_STATE') {
                            ary[9] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_RES_CITY') {
                            ary[10] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_RES_PINCODE') {
                            ary[11] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_OFFICE_ADD_LINE1') {
                        //                            ary[12] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        //                        }
                        //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_OFFICE_ADD_LINE2') {
                        //                            ary[13] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        //                        }
                        //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_OFFICE_MOBILE') {
                        //                            ary[14] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        //                        }
                        //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_OFFICE_PHONE') {
                        //                            ary[15] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        //                        }
                        //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_OFFICE_COUNTRY') {
                        //                            ary[16] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        //                        }
                        //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_OFFICE_STATE') {
                        //                            ary[17] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        //                        }
                        //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_OFFICE_CITY') {
                        //                            ary[18] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        //                        }
                        //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_OFFICE_PINECODE') {
                        //                            ary[19] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        //                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_EMERGENCY_NAME') {
                            ary[20] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_EMMERGENCY_ADD_LINE1') {
                            ary[21] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_EMMERGENCY_ADD_LINE2') {
                            ary[22] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_EMMERGENCY_MOBILE') {
                            ary[23] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_EMMERGENCY_PHONE') {
                            ary[24] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_EMMERGENCY_COUNTRY') {
                            ary[25] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_EMMERGENCY_STATE') {
                            ary[26] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_EMMERGENCY_CITY') {
                            ary[27] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_EMMERGENCY_PINCODE') {
                            ary[28] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl04_CUST_UNQ_ID') {
                            ary[29] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }

                    }
                    ary[12] = 0;
                    ary[13] = 0;
                    ary[14] = 0;
                    ary[15] = 0;
                    ary[16] = 0;
                    ary[17] = 0;
                    ary[18] = 0;
                    ary[19] = 0;

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


                    ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_ID;
                    ary[30] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_ID;
                    ary[31] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
                    ary[32] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.EMER_SR_NO;


                    if (!ary[4] == 0 && !ary[5] == 0 && !ary[8] == 0 && !ary[9] == 0 && !ary[10] == 0 && !ary[11] == 0 && !ary[21] == 0 && !ary[22] == 0 && !ary[25] == 0 && !ary[26] == 0 && !ary[27] == 0 && !ary[28] == 0 && !ary[20] == 0) {

                        if ((!ary[7] == 0 || !ary[6] == 0) && (!ary[23] == 0 || !ary[24] == 0)) {
                            try {

                                CRM.WebApp.webservice.BookingMaster.InsertUpdateaddressDetail(ary);
                                var masterTable = $find("<%=radgridaddressinfo.ClientID %>").get_masterTableView();
                                //  masterTable.rebind();
                                alert('Record Save Successfully');
                            }
                            catch (e) {
                                alert('Wrong Data Inserted');
                            }
                        }
                        else {
                            alert('Enter Contact No.');
                        }
                    }
                    else {
                        alert('Enter Full Address Detail');
                    }

                }
                function addcustomerrelation(sender, args) {
                    currentRowIndex = sender.parentNode.parentNode.rowIndex;
                    var arry = [];
                    for (var i = 0; i < 27; i++) {

                        if (i < 10)
                            i = '0' + i;

                        //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_IS_CHECKED') {
                        //                           // arry[1] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        //                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_TITLE_DESC') {
                            arry[2] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_CUST_REL_NAME') {
                            arry[3] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_CUST_REL_SURNAME') {
                            arry[4] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_RELATION') {
                            arry[5] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_CUST_BIRTH_DATE') {
                            arry[6] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_CUST_REL_MOBILE') {
                            arry[7] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_CUST_REL_PHONE') {
                            arry[8] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_NATIONALITY_NAME') {
                            arry[9] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_MEAL_DESC') {
                            arry[10] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_ROOM_TYPE_NAME') {
                            arry[11] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_CATEGORY_DESC') {
                            arry[12] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_SHARE_ROOM_IN_HOTEL') {
                            arry[13] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_SHARE_ROOM_IN_CRUISE') {
                            arry[14] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_ARIVAL_TO') {
                            arry[15] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_DEPARTURE_DATE') {
                            arry[16] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_ARRIVAL_DATE') {
                            arry[17] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_VALID_VISA') {
                            arry[18] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_VALID_PASSPORT') {
                            arry[19] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_CUSTOMER_REL_PASSPORT_NO') {
                            arry[20] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_CUSTOMER_REL_PASSPORT_ISSUE_DATE') {
                            arry[21] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_CUSTOMER_REL_PASSPORT_ISSUE_PLACE') {
                            arry[22] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_CUSTOMER_REL_PASSPORT_EXPIRY_DATE') {
                            arry[23] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_CUSTOMER_REL_PASSPORT_PRINTED_NAME') {
                            arry[24] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_CUSTOMER_REL_PASSPORT_ISSUE_COUNTRY') {
                            arry[25] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }

                        //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_BOOKING_STATUS_NAME') {
                        //                            //                            arry[26] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        //                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_BORDING_FROM') {
                            arry[27] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl04_CLASS_NAME') {
                            arry[28] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }

                    }

                    arry[1] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("IS_CHECKED").checked;

                    arry[26] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_STATUS_NAME").value;

                    if (arry[0] == "" || arry[0] == 'null') arry[0] = 0;
                    if (arry[1] == "" || arry[1] == 'null') arry[1] = 0;
                    if (arry[2] == "" || arry[2] == 'null') arry[2] = 0;
                    if (arry[3] == "" || arry[3] == 'null') arry[3] = 0;
                    if (arry[4] == "" || arry[4] == 'null') arry[4] = 0;
                    if (arry[5] == "" || arry[5] == 'null') arry[5] = 0;
                    if (arry[6] == "" || arry[6] == 'null') arry[6] = 0;
                    if (arry[7] == "" || arry[7] == 'null') arry[7] = 0;
                    if (arry[8] == "" || arry[8] == 'null') arry[8] = 0;
                    if (arry[9] == "" || arry[9] == 'null') arry[9] = 0;
                    if (arry[10] == "" || arry[10] == 'null') arry[10] = 0;
                    if (arry[11] == "" || arry[11] == 'null') arry[11] = 0;
                    if (arry[12] == "" || arry[12] == 'null') arry[12] = 0;
                    if (arry[13] == "" || arry[13] == 'null') arry[13] = 0;
                    if (arry[14] == "" || arry[14] == 'null') arry[14] = 0;
                    if (arry[15] == "" || arry[15] == 'null') arry[15] = 0;
                    if (arry[16] == "" || arry[16] == 'null') arry[16] = 0;
                    if (arry[17] == "" || arry[17] == 'null') arry[17] = 0;
                    if (arry[18] == "" || arry[18] == 'null') arry[18] = 0;
                    if (arry[19] == "" || arry[19] == 'null') arry[19] = 0;
                    if (arry[20] == "" || arry[20] == 'null') arry[20] = 0;
                    if (arry[21] == "" || arry[21] == 'null') arry[21] = 0;
                    if (arry[22] == "" || arry[22] == 'null') arry[22] = 0;
                    if (arry[23] == "" || arry[23] == 'null') arry[23] = 0;
                    if (arry[24] == "" || arry[24] == 'null') arry[24] = 0;
                    if (arry[25] == "" || arry[25] == 'null') arry[25] = 0;
                    if (arry[26] == "" || arry[26] == 'null') arry[26] = 0;
                    if (arry[27] == "" || arry[27] == 'null') arry[27] = 0;
                    if (arry[28] == "" || arry[28] == 'null') arry[28] = 0;
                    if (arry[29] == "" || arry[29] == 'null') arry[29] = 0;
                    if (arry[30] == "" || arry[30] == 'null') arry[30] = 0;
                    if (arry[31] == "" || arry[31] == 'null') arry[31] = 0;
                    if (arry[32] == "" || arry[32] == 'null') arry[32] = 0;
                    if (arry[33] == "" || arry[33] == 'null') arry[33] = 0;


                    arry[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_SRNO;
                    arry[29] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_ID;
                    arry[30] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.BOOKING_DETAIL_ID;
                    arry[31] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_UNQ_ID;
                    arry[32] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.BOOKING_ID;
                    arry[33] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_ID;
                    if (arry[30] == "" || arry[30] == 'null' || arry[30] == null) arry[30] = 0;
                    if (arry[29] == "" || arry[29] == 'null' || arry[29] == null) arry[29] = 0;
                    try {

                        CRM.WebApp.webservice.BookingMaster.InsertUpdateRelationDetail(arry);
                        var masterTable = $find("<%=radgridcustomerrelation.ClientID %>").get_masterTableView();
                        //  masterTable.rebind();
                        alert('Record Save Successfully');
                    }
                    catch (e) {
                        alert('Wrong Data Inserted');
                    }
                }
                function addcustomervisadetail(sender, args) {

                    currentRowIndex = sender.parentNode.parentNode.rowIndex;
                    var arr = [];
                    for (var i = 0; i < 2; i++) {

                        if (i < 10)
                            i = '0' + i;

                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomervisa_ctl00_ctl04_COUNTRY_NAME') {
                            arr[1] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridcustomervisa_ctl00_ctl04_VISA_EXPIRY_DATE') {
                            arr[2] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                        }
                    }
                    // debugger;
                    if (arr[0] == "" || arr[0] == 'null') arr[0] = 0;
                    if (arr[1] == "" || arr[1] == 'null') arr[1] = 0;
                    if (arr[2] == "" || arr[2] == 'null') arr[2] = 0;
                    if (arr[3] == "" || arr[3] == 'null') arr[3] = 0;
                    if (arr[4] == "" || arr[4] == 'null') arr[4] = 0;

                    arr[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_ID;
                    arr[3] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
                    arr[4] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_ID;
                    try {

                        CRM.WebApp.webservice.BookingMaster.InsertUpdatevisadetail(arr);
                        var masterTable = $find("<%=radgridcustomervisa.ClientID %>").get_masterTableView();
                        alert('Record Save Successfully');
                    }
                    catch (e) {
                        alert('Wrong Data Inserted');
                    }

                }
                function AddNewVisa() {
                    CRM.WebApp.webservice.BookingMaster.InsertNewVisa(CUST_ID, CUST_REL_ID);
                }


                function AddNewBookinginfo() {

                    var a = [];
                    a[0] = document.getElementById('<%=txtINQUIRY_ID.ClientID%>').value;
                    a[1] = document.getElementById('<%=txttour.ClientID%>').value;
                    a[2] = document.getElementById('<%=txtbookingdate.ClientID%>').value;
                    a[3] = document.getElementById('<%=txtbookingtakenby.ClientID%>').value;
                    a[4] = document.getElementById('<%=txtbranchname.ClientID%>').value;
                    a[5] = document.getElementById('<%=txtactualcost1.ClientID%>').value;
                    a[6] = document.getElementById('<%=txtactualcost2.ClientID%>').value;
                    a[7] = document.getElementById('<%=txtbalancepaid1.ClientID%>').value;
                    a[8] = document.getElementById('<%=txtbalancepaid2.ClientID%>').value;
                    a[9] = document.getElementById('<%=txtfamilybookingstatus.ClientID%>').value;
                    a[10] = document.getElementById('<%=txtagentname.ClientID%>').value;
                    a[11] = document.getElementById('<%=txtbookingcode.ClientID%>').value;
                    a[12] = document.getElementById('<%=txtdocvisadate.ClientID%>').value;
                    a[13] = document.getElementById('<%=txtpaymentdate.ClientID%>').value;
                    a[14] = document.getElementById('<%=txtagentrequireservice.ClientID%>').value;
                    a[15] = CUST_ID;
                    a[16] = document.getElementById('<%=txtBOOKING_ID.ClientID%>').value;
                    a[17] = document.getElementById('<%=txttourcode.ClientID%>').value;
                    //                    date = new Date().format('MM/dd/yyyy');
                    //                    ary[18] = date;

                    if (a[0] == "" || a[0] == 'null') a[0] = 0;
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
                    if (a[11] == "" || a[11] == 'null') a[11] = 0;
                    if (a[12] == "" || a[12] == 'null') a[12] = 0;
                    if (a[13] == "" || a[13] == 'null') a[13] = 0;
                    if (a[14] == "" || a[14] == 'null') a[14] = 0;
                    if (a[15] == "" || a[15] == 'null') a[15] = 0;
                    if (a[16] == "" || a[16] == 'null') a[16] = 0;
                    if (a[17] == "" || a[17] == 'null') a[17] = 0;

                    try {
                        CRM.WebApp.webservice.BookingMaster.InsertUpdateBookingInfo(a);
                        alert('Record Save Successfully');
                       
                    }
                    catch (e) {
                        alert('Wrong Data Inserted');
                    }
                }
                function radgridaddressinfo_RowDataBound(sender, args) {
                    CUST_ID = args.get_dataItem()["CUST_ID"];
                }
                function addnewPaymentinfo(sender, args) {
                    currentRowIndex = sender.parentNode.parentNode.rowIndex;
                    var aa = [];
                    aa[0] = CUST_ID;

                    aa[1] = customerPaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("RECEIPT_NO").value;
                    aa[2] = customerPaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("RECEIPT_DATE").value;
                    aa[3] = customerPaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_DATE").value;
                    aa[4] = customerPaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_MODE_ID").value;
                    aa[5] = customerPaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("REC_CHEQUE_DD_NO").value;
                    aa[6] = customerPaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("TOKEN_AMOUNT").value;
                    aa[7] = customerPaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("BANK_ID").value;



                    if (aa[0] == "" || aa[0] == 'null') aa[0] = 0;
                    if (aa[1] == "" || aa[1] == 'null') aa[1] = 0;
                    if (aa[2] == "" || aa[2] == 'null') aa[2] = 0;
                    if (aa[3] == "" || aa[3] == 'null') aa[3] = 0;
                    if (aa[4] == "" || aa[4] == 'null') aa[4] = 0;
                    if (aa[5] == "" || aa[5] == 'null') aa[5] = 0;
                    if (aa[6] == "" || aa[6] == 'null') aa[6] = 0;
                    if (aa[7] == "" || aa[7] == 'null') aa[7] = 0;



                    aa[8] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.PAYMENT_SRNO;

                    try {

                        CRM.WebApp.webservice.BookingMaster.InsertUpdatePaymentDetail(aa);
                        CRM.WebApp.webservice.BookingMaster.Getpaymentinfo(CUST_ID, updatepaymentinfo);
                        alert('Record Save Successfully');
                    }
                    catch (e) {
                        alert('Wrong Data Inserted');
                    }
                }
                function getcost(sender) {
                    var inqury = document.getElementById('ctl00_cphPageContent_txtINQUIRY_ID').value;
                    var tourname = document.getElementById('ctl00_cphPageContent_txttour').value;
                    var txtcost1 = document.getElementById('ctl00_cphPageContent_txtactualcost1');
                    var txtcost2 = document.getElementById('ctl00_cphPageContent_txtactualcost2');
                    var tourshortname = document.getElementById('ctl00_cphPageContent_txttourcode').value;
                    var txtseats = document.getElementById('ctl00_cphPageContent_txtAvailableSeats');
                    var name = tourname + " " + tourshortname;
                    var a = "";
                    CRM.WebApp.webservice.AutoComplete.searchQueryResultOnSecondFile(name);

                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: inqury, key: "FETCH_TOTAL_QUOTED_COST_C1_DUALPARAM?" + globalvalue + "?" + a }, function (data) { txtcost1.value = data; });
                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: inqury, key: "FETCH_TOTAL_QUOTED_COST_C2_DUALPARAM?" + globalvalue + "?" + a }, function (data) { txtcost2.value = data; });
                    //getAvailSeats();

                }

                function getcost1(sender) {
                    var inqury = document.getElementById('ctl00_cphPageContent_txtINQUIRY_ID').value;
                    var tourname = document.getElementById('ctl00_cphPageContent_txttour').value;
                    var txtcost1 = document.getElementById('ctl00_cphPageContent_txtactualcost1');
                    var txtcost2 = document.getElementById('ctl00_cphPageContent_txtactualcost2');
                    var tourshortname = document.getElementById('ctl00_cphPageContent_txttourcode').value;
                    var txtseats = document.getElementById('ctl00_cphPageContent_txtAvailableSeats');
                    var name = tourname + " " + tourshortname;
                    CRM.WebApp.webservice.AutoComplete.searchQueryResult(name);

                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: inqury, key: "FETCH_TOTAL_QUOTED_COST_C1_DUALPARAM?" + globalvalue }, function (data) { txtcost1.value = data; });
                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: inqury, key: "FETCH_TOTAL_QUOTED_COST_C2_DUALPARAM?" + globalvalue }, function (data) { txtcost2.value = data; });
                    //getAvailSeats();

                }


                function getAvailSeats(sender) {
                    var tourname = document.getElementById('ctl00_cphPageContent_txttour').value;
                    var tourshortname = document.getElementById('ctl00_cphPageContent_txttourcode').value;
                    var txtseats = document.getElementById('ctl00_cphPageContent_txtAvailableSeats');
                    CRM.WebApp.webservice.AutoComplete.searchQueryResult(tourname);
                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: tourshortname, key: "FETCH_AVAIL_SEATS_FOR_TOUR?" + globalvalue }, function (data) { txtseats.value = data; });
                    //getcost();
                }
                function gettourshortname(sender) {

                    var inqury = document.getElementById('ctl00_cphPageContent_txtINQUIRY_ID').value;
                    var tourname = document.getElementById('ctl00_cphPageContent_txttour').value;
                    var txtcost1 = document.getElementById('ctl00_cphPageContent_txtactualcost1');
                    var txtcost2 = document.getElementById('ctl00_cphPageContent_txtactualcost2');
                    CRM.WebApp.webservice.AutoComplete.searchQueryResult(tourname);

                    var tourcode = document.getElementById('ctl00_cphPageContent_txttourcode');

                    $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: tourcode.value, key: "FETCH_TOUR_CODE_ON_TOUR_SHORT_NAME?" + globalvalue }, function (data) { tourcode.value = data; });

                }
                function printReport() {
                    var bookingid = document.getElementById('ctl00_cphPageContent_txtBOOKING_ID').value;
                    window.open('BookingReport.aspx?key=' + bookingid);

                }
            </script>
         </telerik:radcodeblock>
    <br />
    <%--<div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageTask" runat="server" Text="CUSTOMER ADDRESS DETAIL"></asp:Literal>
    </div>
    <br />
    <br />
    <br />--%>
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var tourtype = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_TOUR_SHORTNAME_FOR_BOOKING_MASTER?" + globalvalue;
            var title = "../../webservice/autocomplete.ashx?key=FETCH_TITLE_FOR_EMPLOYEE_MASTER";
            var country = "../../webservice/autocomplete.ashx?key=FETCH_COUNTRY_FOR_EMPLOYEE_MASTER";
            var state = "../../webservice/autocomplete.ashx?key=FETCH_STATE_FOR_EMPLOYEE_MASTER";
            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            var branch = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_COMPANY_NAME_AUTOSEARCH";
            var meal = "../../webservice/autocomplete.ashx?key=FETCH_MEAL_DATA_FOR_BOOKINGMASTER_AUTOSEARCH";
            var roomtype = "../../webservice/autocomplete.ashx?key=FETCH_ROOM_TYPE_FOR_HOTEL_AUTOSEARCH";
            var cabinecategory = "../../webservice/autocomplete.ashx?key=FETCH_CRUISE_CABINE_CATEGORY_AUTOSEARCH";
            var bookingtakenby = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_EMPLOYEE_FOR_TASKMASTER_AUTOSEARCH";
            var bookingstatus = "../../webservice/autocomplete.ashx?key=FETCH_BOOKING_STATUS_FOR_BOOKING_MASTER_AUTOSEARCH";
            var nationality = "../../webservice/autocomplete.ashx?key=FETCH_NATIONALITY_FOR_BOOKING_MASTER";
            var flightclass = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_FLIGHT_CLASS_AUTOSEARCH";
            var bank = "../../webservice/autocomplete.ashx?key=FETCH_BANKNAME_FOR_BOOKING_MASTER";
            var paymentmode = "../../webservice/autocomplete.ashx?key=FETCH_PAYMENT_MODE_FOR_BOOKING_MASTER";
            var agent = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_AGENT_NAME";
            var tourcode = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_CODE_ON_TOUR_SHORT_NAME?" + globalvalue;

            $("#ctl00_cphPageContent_txttour").autocomplete(tourtype);
            $("#ctl00_cphPageContent_txtbranchname").autocomplete(branch);
            $("#ctl00_cphPageContent_txtbookingtakenby").autocomplete(bookingtakenby);
            $("#ctl00_cphPageContent_txtfamilybookingstatus").autocomplete(bookingstatus);
            $("#ctl00_cphPageContent_txtagentname").autocomplete(agent);
            $("#ctl00_cphPageContent_txttourcode").autocomplete(tourcode);

            for (var i = 1; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;
                $("#ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl" + i + "_TITLE_DESC").autocomplete(title);
                $("#ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl" + i + "_RES_COUNTRY").autocomplete(country);
                $("#ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl" + i + "_RES_STATE").autocomplete(state);
                $("#ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl" + i + "_RES_CITY").autocomplete(city);
                $("#ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl" + i + "_OFFICE_COUNTRY").autocomplete(country);
                $("#ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl" + i + "_OFFICE_STATE").autocomplete(state);
                $("#ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl" + i + "_OFFICE_CITY").autocomplete(city);
                $("#ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl" + i + "_EMMERGENCY_COUNTRY").autocomplete(country);
                $("#ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl" + i + "_EMMERGENCY_CITY").autocomplete(city);
                $("#ctl00_cphPageContent_radgridaddressinfo_ctl00_ctl" + i + "_EMMERGENCY_STATE").autocomplete(state);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_CUSTOMER_REL_PASSPORT_ISSUE_COUNTRY").autocomplete(country);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_TITLE_DESC").autocomplete(title);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_MEAL_DESC").autocomplete(meal);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_ROOM_TYPE_NAME").autocomplete(roomtype);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_CATEGORY_DESC").autocomplete(cabinecategory);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_CLASS_NAME").autocomplete(flightclass);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_NATIONALITY_NAME").autocomplete(nationality);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_BOOKING_STATUS_NAME").autocomplete(bookingstatus);
                $("#ctl00_cphPageContent_radgridcustomervisa_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(country);
                $("#ctl00_cphPageContent_radgridpaymentinfo_ctl00_ctl" + i + "_BANK_ID").autocomplete(bank);
                $("#ctl00_cphPageContent_radgridpaymentinfo_ctl00_ctl" + i + "_PAYMENT_MODE_ID").autocomplete(paymentmode);
            }

        });
    </script>
    <div class="pageTitle">
        <asp:Literal ID="lblPageAddress" runat="server" Text="Booking Master"></asp:Literal>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Detail?'))return false; deleteCurrent(); return false;"
                        Text="Delete" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <asp:DropDownList ID="BOOKING_CODE" runat="server" 
                        OnChange="ddlChanged(this,event);" 
                        onselectedindexchanged="BOOKING_CODE_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr valign="top">
                <td valign="top">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblinquiry" runat="server" Text="Inquiry No. :" CssClass="RadMenu"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtINQUIRY_ID" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                                    onblur="getaddressname(this);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbltour" runat="server" Text="Tour :" CssClass="RadMenu"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txttour" runat="server" Width="<%$appSettings:TextBoxWidth%>" onblur="gettourshortname(this);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbltourcode" runat="server" Text="Tour Code :" CssClass="RadMenu"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txttourcode" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                                    onblur="getAvailSeats(this);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblbookindate" runat="server" Text="Booking Date :" CssClass="RadMenu"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbookingdate" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                                    ReadOnly="true" Style="background-color: LightBlue" onblur="getcost(this);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblbookintakenby" runat="server" Text="Booking Taken By :" CssClass="RadMenu"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbookingtakenby" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                                    ReadOnly="true" Style="background-color: LightBlue" onblur="getcost1(this);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblbranchname" runat="server" Text="Branch Name :" CssClass="RadMenu"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbranchname" runat="server" Width="<%$appSettings:TextBoxWidth%>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblactualtourcost1" runat="server" Text="Total Actual Tour Cost-1 :"
                                    CssClass="RadMenu" Visible="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtactualcost1" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                                    Visible="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblactualtourcost2" runat="server" Text="Total Actual Tour Cost-2 :"
                                    CssClass="RadMenu" Visible="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtactualcost2" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                                    Visible="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="visibility: hidden">
                            <td>
                                <asp:Label ID="lblAvailbaleSeats" runat="server" Text="Available Seats :" CssClass="RadMenu"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAvailableSeats" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                                    Text="0"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblbalancepaid1" runat="server" Text="Balance To Be Paid-1 :" CssClass="RadMenu"
                                    Visible="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbalancepaid1" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                                    Visible="true" Text="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblbalancepaid2" runat="server" Text="Balance To Be Paid-2 :" CssClass="RadMenu"
                                    Visible="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbalancepaid2" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                                    Visible="true" Text="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblfamilybookingstatus" runat="server" Text="Family Booking Status :"
                                    CssClass="RadMenu" Visible="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfamilybookingstatus" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                                    Visible="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblagentname" runat="server" Text="Agent Name :" CssClass="RadMenu"
                                    Visible="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtagentname" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                                    Visible="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblagentreqservice" runat="server" Text="Additional Require Service :"
                                    CssClass="RadMenu" Visible="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtagentrequireservice" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                                    Visible="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblbookingcode" runat="server" Text="Booking Code :" CssClass="RadMenu"
                                    Visible="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbookingcode" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                                    Visible="true" ReadOnly="true" Style="background-color: LightBlue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbldocforvisadate" runat="server" Text="Document To Process Visa formalities To Be Handed Over By Date :"
                                    CssClass="RadMenu" Visible="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdocvisadate" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                                    Visible="true" onclick="showPopup(this, event);" onfocus="showPopup(this, event);"
                                    onkeydown="parseDate(this, event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblpaymentdate" runat="server" Text="Payment For The Balance Amount of Tour Be Made By Date :"
                                    CssClass="RadMenu" Visible="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtpaymentdate" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                                    Visible="true" onclick="showPopup(this, event);" onfocus="showPopup(this, event);"
                                    onkeydown="parseDate(this, event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSavebookinginfo" runat="server" Text="Save" Style="color: black;
                                    font-weight: bold;" OnClientClick="AddNewBookinginfo();" 
                                    onclick="btnSavebookinginfo_Click" />
                            </td>
                        </tr>
                        <tr style="visibility: hidden;">
                            <td>
                                <asp:Label ID="lblbookingid" runat="server" Text="BOOKING_ID :" CssClass="RadMenu"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBOOKING_ID" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                                    Text="0"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageTask" runat="server" Text="Customer Address Detail"></asp:Literal>
    </div>
    <br />
    <br />
    <br />
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <div id="radgridmaster">
        <table width="100%" cellpadding="0" border="0">
            <tr>
                <td>
                    <telerik:radgrid id="radgridaddressinfo" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="" AllowMultiColumSorting="true" EditMode ="InPlace" Width="4500px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>

                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="INQUIRY_ID" DataField="INQUIRY_ID" HeaderText="INQUIRY_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="INQUIRY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_ID" DataField="CUST_ID" HeaderText="CUST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_REL_ID" DataField="CUST_REL_ID" HeaderText="CUST_REL_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression ="SR_NO" DataField="SR_NO" HeaderText="SR_NO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SR_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="EMER_SR_NO" DataField="EMER_SR_NO" HeaderText="EMER_SR_NO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="EMER_SR_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                   
                    <telerik:GridTemplateColumn SortExpression ="CUST_UNQ_ID" DataField="CUST_UNQ_ID" HeaderText="CUSTOMER ID" >
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_UNQ_ID" runat="server" CssClass="radinput" readonly="true" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    


                    <telerik:GridTemplateColumn SortExpression ="TITLE_DESC" DataField="TITLE_DESC" HeaderText="TITLE">
                          <ItemTemplate>
                            <asp:TextBox ID="TITLE_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_NAME" DataField="CUST_NAME" HeaderText="NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_SURNAME" DataField="CUST_SURNAME" HeaderText="SURNAME">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_SURNAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="RES_ADD_LINE1" DataField="RES_ADD_LINE1" HeaderText="RES ADD LINE1">
                          <ItemTemplate>
                            <asp:TextBox ID="RES_ADD_LINE1" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="RES_ADD_LINE2" DataField="RES_ADD_LINE2" HeaderText="RES ADD LINE2">
                          <ItemTemplate>
                            <asp:TextBox ID="RES_ADD_LINE2" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="RES_MOBILE" DataField="RES_MOBILE" HeaderText="RES MOBILE">
                          <ItemTemplate>
                            <asp:TextBox ID="RES_MOBILE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="RES_PHONE" DataField="RES_PHONE" HeaderText="RES PHONE">
                          <ItemTemplate>
                            <asp:TextBox ID="RES_PHONE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="RES_COUNTRY" DataField="RES_COUNTRY" HeaderText="COUNTRY">
                          <ItemTemplate>
                            <asp:TextBox ID="RES_COUNTRY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="RES_STATE" DataField="RES_STATE" HeaderText="STATE">
                          <ItemTemplate>
                            <asp:TextBox ID="RES_STATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="RES_CITY" DataField="RES_CITY" HeaderText="CITY">
                          <ItemTemplate>
                            <asp:TextBox ID="RES_CITY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="RES_PINCODE" DataField="RES_PINCODE" HeaderText="RES PINCODE">
                          <ItemTemplate>
                            <asp:TextBox ID="RES_PINCODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OFFICE_ADD_LINE1" DataField="OFFICE_ADD_LINE1" HeaderText="OFFICE ADDRESS LINE1"  Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="OFFICE_ADD_LINE1" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OFFICE_ADD_LINE2" DataField="OFFICE_ADD_LINE2" HeaderText="OFFICE ADDRESS LINE2"  Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="OFFICE_ADD_LINE2" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OFFICE_MOBILE" DataField="OFFICE_MOBILE" HeaderText="OFFICE MOBILE"  Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="OFFICE_MOBILE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OFFICE_PHONE" DataField="OFFICE_PHONE" HeaderText="OFFICE PHONE"  Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="OFFICE_PHONE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OFFICE_COUNTRY" DataField="OFFICE_COUNTRY" HeaderText="OFFICE COUNTRY"  Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="OFFICE_COUNTRY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="OFFICE_STATE" DataField="OFFICE_STATE" HeaderText="OFFICE STATE"  Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="OFFICE_STATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OFFICE_CITY" DataField="OFFICE_CITY" HeaderText="OFFICE CITY"  Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="OFFICE_CITY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OFFICE_PINECODE" DataField="OFFICE_PINECODE" HeaderText="OFFICE PINECODE"  Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="OFFICE_PINECODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMERGENCY_NAME" DataField="EMERGENCY_NAME" HeaderText="EMERGENCY NAME" >
                          <ItemTemplate>
                            <asp:TextBox ID="EMERGENCY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMMERGENCY_ADD_LINE1" DataField="EMMERGENCY_ADD_LINE1" HeaderText="EMMERGENCY ADDRESS LINE1">
                          <ItemTemplate>
                            <asp:TextBox ID="EMMERGENCY_ADD_LINE1" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMMERGENCY_ADD_LINE2" DataField="EMMERGENCY_ADD_LINE2" HeaderText="EMMERGENCY ADDRESS LINE2">
                          <ItemTemplate>
                            <asp:TextBox ID="EMMERGENCY_ADD_LINE2" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMMERGENCY_MOBILE" DataField="EMMERGENCY_MOBILE" HeaderText="EMMERGENCY MOBILE">
                          <ItemTemplate>
                            <asp:TextBox ID="EMMERGENCY_MOBILE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMMERGENCY_PHONE" DataField="EMMERGENCY_PHONE" HeaderText="EMMERGENCY PHONE">
                          <ItemTemplate>
                            <asp:TextBox ID="EMMERGENCY_PHONE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <%--<telerik:GridTemplateColumn SortExpression ="CUST_ADD_LINE2" DataField="CUST_ADD_LINE2" HeaderText="CUSTOMER ADDRESS LINE2">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_ADD_LINE2" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>--%>

                     <telerik:GridTemplateColumn SortExpression ="EMMERGENCY_COUNTRY" DataField="EMMERGENCY_COUNTRY" HeaderText="EMMERGENCY COUNTRY">
                          <ItemTemplate>
                            <asp:TextBox ID="EMMERGENCY_COUNTRY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="EMMERGENCY_STATE" DataField="EMMERGENCY_STATE" HeaderText="EMMERGENCY STATE">
                          <ItemTemplate>
                            <asp:TextBox ID="EMMERGENCY_STATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMMERGENCY_CITY" DataField="EMMERGENCY_CITY" HeaderText="EMMERGENCY CITY">
                          <ItemTemplate>
                            <asp:TextBox ID="EMMERGENCY_CITY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMMERGENCY_PINCODE" DataField="EMMERGENCY_PINCODE" HeaderText="EMMERGENCY PINCODE">
                          <ItemTemplate>
                            <asp:TextBox ID="EMMERGENCY_PINCODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewaddressdetail(this,event);">
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
                <ClientEvents OnCommand="radgridaddressinfo_Command" OnRowSelected="radgridaddressinfo_RowSelected" onRowDataBound="radgridaddressinfo_RowDataBound"/>
                <Selecting AllowRowSelect="True"/>
                </ClientSettings>
               </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal1" runat="server" Text="Customer Relation Detail"></asp:Literal>
    </div>
    <br />
    <br />
    <br />
    <div id="SubGrid1">
        <table width="100%" cellpadding="0" border="0">
            <tr>
                <td>
                    <telerik:radgrid id="radgridcustomerrelation" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="BOOKING_DETAIL_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="4500px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>

                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="BOOKING_DETAIL_ID" DataField="BOOKING_DETAIL_ID" HeaderText="BOOKING_DETAIL_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="BOOKING_DETAIL_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="BOOKING_ID" DataField="BOOKING_ID" HeaderText="BOOKING_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="BOOKING_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_ID" DataField="CUST_ID" HeaderText="CUST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_REL_ID" DataField="CUST_REL_ID" HeaderText="CUST_REL_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CUST_UNQ_ID" DataField="CUST_UNQ_ID" HeaderText="CUST_UNQ_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_UNQ_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn HeaderText="SELECT" DataField="IS_CHECKED">
                          <ItemTemplate>
                            <asp:CheckBox ID="IS_CHECKED" runat="server"></asp:CheckBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TITLE_DESC" DataField="TITLE_DESC" HeaderText="TITLE">
                          <ItemTemplate>
                            <asp:TextBox ID="TITLE_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CUST_REL_NAME" DataField="CUST_REL_NAME" HeaderText="NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_REL_SURNAME" DataField="CUST_REL_SURNAME" HeaderText="SURNAME">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_SURNAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="RELATION" DataField="RELATION" HeaderText="RELATION">
                          <ItemTemplate>
                            <asp:TextBox ID="RELATION" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CUST_BIRTH_DATE" DataField="CUST_BIRTH_DATE" HeaderText="BIRTH DATE">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_BIRTH_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <%--<telerik:GridTemplateColumn SortExpression ="CUST_REL_EMAIL" DataField="CUST_REL_EMAIL" HeaderText="EMAIL">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_EMAIL" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>--%>

                    <telerik:GridTemplateColumn SortExpression ="CUST_REL_MOBILE" DataField="CUST_REL_MOBILE" HeaderText="MOBILE">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_MOBILE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_REL_PHONE" DataField="CUST_REL_PHONE" HeaderText="PHONE">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_PHONE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="NATIONALITY_NAME" DataField="NATIONALITY_NAME" HeaderText="NATIONALITY">
                          <ItemTemplate>
                            <asp:TextBox ID="NATIONALITY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MEAL_DESC" DataField="MEAL_DESC" HeaderText="MEAL">
                          <ItemTemplate>
                            <asp:TextBox ID="MEAL_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ROOM_TYPE_NAME" DataField="ROOM_TYPE_NAME" HeaderText="ROOM TYPE">
                          <ItemTemplate>
                            <asp:TextBox ID="ROOM_TYPE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CLASS_NAME" DataField="CLASS_NAME" HeaderText="CLASS NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="CLASS_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CATEGORY_DESC" DataField="CATEGORY_DESC" HeaderText="CATEGORY">
                          <ItemTemplate>
                            <asp:TextBox ID="CATEGORY_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SHARE_ROOM_IN_HOTEL" DataField="SHARE_ROOM_IN_HOTEL" HeaderText="SHARE ROOM IN HOTEL">
                          <ItemTemplate>
                            <asp:TextBox ID="SHARE_ROOM_IN_HOTEL" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SHARE_ROOM_IN_CRUISE" DataField="SHARE_ROOM_IN_CRUISE" HeaderText="SHARE ROOM IN CRUISE">
                          <ItemTemplate>
                            <asp:TextBox ID="SHARE_ROOM_IN_CRUISE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="BORDING_FROM" DataField="BORDING_FROM" HeaderText="BORDING FROM">
                          <ItemTemplate>
                            <asp:TextBox ID="BORDING_FROM" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ARIVAL_TO" DataField="ARIVAL_TO" HeaderText="ARIVAL TO">
                          <ItemTemplate>
                            <asp:TextBox ID="ARIVAL_TO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="DEPARTURE_DATE" DataField="DEPARTURE_DATE" HeaderText="DEPARTURE DATE">
                          <ItemTemplate>
                            <asp:TextBox ID="DEPARTURE_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ARRIVAL_DATE" DataField="ARRIVAL_DATE" HeaderText="ARRIVAL DATE">
                          <ItemTemplate>
                            <asp:TextBox ID="ARRIVAL_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>


                    <telerik:GridTemplateColumn SortExpression ="VALID_VISA" DataField="VALID_VISA" HeaderText="VALID VISA">
                          <ItemTemplate>
                            <asp:TextBox ID="VALID_VISA" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="VALID_PASSPORT" DataField="VALID_PASSPORT" HeaderText="VALID PASSPORT">
                          <ItemTemplate>
                            <asp:TextBox ID="VALID_PASSPORT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUSTOMER_REL_PASSPORT_NO" DataField="CUSTOMER_REL_PASSPORT_NO" HeaderText="PASSPORT NO">
                          <ItemTemplate>
                            <asp:TextBox ID="CUSTOMER_REL_PASSPORT_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUSTOMER_REL_PASSPORT_ISSUE_DATE" DataField="CUSTOMER_REL_PASSPORT_ISSUE_DATE" HeaderText="PASSPORT ISSUE DATE">
                          <ItemTemplate>
                            <asp:TextBox ID="CUSTOMER_REL_PASSPORT_ISSUE_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUSTOMER_REL_PASSPORT_ISSUE_PLACE" DataField="CUSTOMER_REL_PASSPORT_ISSUE_PLACE" HeaderText="PASSPORT ISSUE PLACE">
                          <ItemTemplate>
                            <asp:TextBox ID="CUSTOMER_REL_PASSPORT_ISSUE_PLACE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUSTOMER_REL_PASSPORT_EXPIRY_DATE" DataField="CUSTOMER_REL_PASSPORT_EXPIRY_DATE" HeaderText="PASSPORT EXPIRY DATE">
                          <ItemTemplate>
                            <asp:TextBox ID="CUSTOMER_REL_PASSPORT_EXPIRY_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUSTOMER_REL_PASSPORT_PRINTED_NAME" DataField="CUSTOMER_REL_PASSPORT_PRINTED_NAME" HeaderText="PASSPORT PRINTED NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="CUSTOMER_REL_PASSPORT_PRINTED_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUSTOMER_REL_PASSPORT_ISSUE_COUNTRY" DataField="CUSTOMER_REL_PASSPORT_ISSUE_COUNTRY" HeaderText="PASSPORT ISSUE COUNTRY">
                          <ItemTemplate>
                            <asp:TextBox ID="CUSTOMER_REL_PASSPORT_ISSUE_COUNTRY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="BOOKING_STATUS_NAME" DataField="BOOKING_STATUS_NAME" HeaderText="BOOKING STATUS">
                          <ItemTemplate>
                            <asp:TextBox ID="BOOKING_STATUS_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addcustomerrelation(this,event);">
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
                <ClientEvents OnCommand="radgridcustomerrelation_Command"  OnRowSelected="radgridcustomerrelation_RowSelected" />
                <Selecting AllowRowSelect="True"/>
                </ClientSettings>
            </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal2" runat="server" Text="Customer Visa Detail"></asp:Literal>
    </div>
    <br />
    <br />
    </br/>
    <div id="SUB GRID2">
        <table width="100%" cellpadding="0" border="0">
            <tr>
                <td>
                    <telerik:radgrid id="radgridcustomervisa" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="" AllowMultiColumSorting="true" EditMode ="InPlace" Width="300px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>

                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SR_NO" DataField="SR_NO" HeaderText="SR_NO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SR_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CUST_ID" DataField="CUST_ID" HeaderText="CUST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CUST_REL_ID" DataField="CUST_REL_ID" HeaderText="CUST_REL_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COUNTRY_NAME" DataField="COUNTRY_NAME" HeaderText="COUNTRY NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="VISA_EXPIRY_DATE" DataField="VISA_EXPIRY_DATE" HeaderText="VISA EXPIRY DATE">
                          <ItemTemplate>
                            <asp:TextBox ID="VISA_EXPIRY_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A3" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addcustomervisadetail(this,event);">
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
                <ClientEvents OnCommand="radgridcustomervisa_Command" />
                <Selecting AllowRowSelect="True"/>
                </ClientSettings>
            </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
    </br>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal3" runat="server" Text="Payment Information"></asp:Literal>
    </div>
    <br />
    <br />
    <br />
    <div id="Sub grid3">
        <table width="100%" cellpadding="0" border="0">
            <tr>
                <td>
                    <telerik:radgrid id="radgridpaymentinfo" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1400px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>

                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="BOOKING_ID" DataField="BOOKING_ID" HeaderText="BOOKING_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="BOOKING_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PAYMENT_SRNO" DataField="PAYMENT_SRNO" HeaderText="PAYMENT_SRNO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_SRNO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                   <%-- <telerik:GridTemplateColumn SortExpression ="CUST_ID" DataField="CUST_ID" HeaderText="CUST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>--%>

                    <%-- <telerik:GridTemplateColumn SortExpression ="BILL_NUMBER" DataField="BILL_NUMBER" HeaderText="BILL NUMBER">
                          <ItemTemplate>
                            <asp:TextBox ID="BILL_NUMBER" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>--%>

                    <telerik:GridTemplateColumn SortExpression ="RECEIPT_NO" DataField="RECEIPT_NO" HeaderText="RECEIPT NO">
                          <ItemTemplate>
                            <asp:TextBox ID="RECEIPT_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="RECEIPT_DATE" DataField="RECEIPT_DATE" HeaderText="RECEIPT DATE">
                          <ItemTemplate>
                            <asp:TextBox ID="RECEIPT_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <%-- <telerik:GridTemplateColumn SortExpression ="IN_THE_NAME_OF" DataField="IN_THE_NAME_OF" HeaderText="IN THE NAME OF">
                          <ItemTemplate>
                            <asp:TextBox ID="IN_THE_NAME_OF" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>--%>

                     <telerik:GridTemplateColumn SortExpression ="PAYMENT_DATE" DataField="PAYMENT_DATE" HeaderText="PAYMENT DATE">
                          <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <%--<telerik:GridTemplateColumn SortExpression ="PAYMENT_CURRENCY_ID" DataField="PAYMENT_CURRENCY_ID" HeaderText="CURRENCY_TYPE">
                          <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_CURRENCY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>--%>

                    <telerik:GridTemplateColumn SortExpression ="PAYMENT_MODE_ID" DataField="PAYMENT_MODE_ID" HeaderText="PAYMENT MODE">
                          <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_MODE_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="REC_CHEQUE_DD_NO" DataField="REC_CHEQUE_DD_NO" HeaderText="CHEQUE/DD NO">
                          <ItemTemplate>
                            <asp:TextBox ID="REC_CHEQUE_DD_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <%--<telerik:GridTemplateColumn SortExpression ="AMOUNT" DataField="AMOUNT" HeaderText="AMOUNT">
                          <ItemTemplate>
                            <asp:TextBox ID="AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TAX" DataField="TAX" HeaderText="TAX">
                          <ItemTemplate>
                            <asp:TextBox ID="TAX" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="GST" DataField="GST" HeaderText="GST">
                          <ItemTemplate>
                            <asp:TextBox ID="GST" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>--%>

                     <telerik:GridTemplateColumn SortExpression ="TOKEN_AMOUNT" DataField="TOKEN_AMOUNT" HeaderText="TOKEN AMOUNT">
                          <ItemTemplate>
                            <asp:TextBox ID="TOKEN_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="BANK_ID" DataField="BANK_ID" HeaderText="BANK">
                          <ItemTemplate>
                            <asp:TextBox ID="BANK_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <%-- <telerik:GridTemplateColumn SortExpression ="RECEIVED_BY" DataField="RECEIVED_BY" HeaderText="RECEIVED BY">
                          <ItemTemplate>
                            <asp:TextBox ID="RECEIVED_BY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CURRENCY_AGENT_ID" DataField="CURRENCY_AGENT_ID" HeaderText="CURRENCY AGENT">
                          <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_AGENT_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="INR_AMOUNT" DataField="INR_AMOUNT" HeaderText="INR AMOUNT">
                          <ItemTemplate>
                            <asp:TextBox ID="INR_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CONVERSION_RATE" DataField="CONVERSION_RATE" HeaderText="CONVERSION_RATE">
                          <ItemTemplate>
                            <asp:TextBox ID="CONVERSION_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>--%>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A4" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewPaymentinfo(this,event);">
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
                <ClientEvents OnCommand="radgridpaymentinfo_Command" />
                <Selecting AllowRowSelect="True"/>
                </ClientSettings>
            </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="Print" CssClass="button" Style="color: black; font-weight: bold;"
                        Text="Print" runat="server" OnClientClick="printReport();" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
