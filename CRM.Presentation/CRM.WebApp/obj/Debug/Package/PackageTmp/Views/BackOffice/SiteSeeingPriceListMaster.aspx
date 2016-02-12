<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="SiteSeeingPriceListMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.SiteSeeingPriceListMaster" %>

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
    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>


    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript" src="../Shared/Javascripts/SiteSeeingPriceGridScript.js"></script>
        <script type="text/javascript">
            (function () {
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm) {
                    prm.add_endRequest(
                function (sender, args) {
                    // Any code you want here 

                    if (args.get_error() && args.get_error().name === 'Sys.WebForms.PageRequestManagerServerErrorException') {
                        args.set_errorHandled(args._error.httpStatusCode == 0);
                    }
                });
                }
            })(); 
        </script>
         <script type="text/javascript">
             function pageLoad() {
                 SiteTableView = $find("<%= radgridSiteSeeingPriceList.ClientID %>").get_masterTableView();
                 SiteDayTableView = $find("<%= radgriddays.ClientID %>").get_masterTableView();
                 SiteDateTableView = $find("<%= radgriddate.ClientID %>").get_masterTableView();

                 //CRM.WebApp.webservice.SiteSeeingPriceListWebService.GetSitePrice(0, SiteTableView.get_pageSize(), SiteTableView.get_sortExpressions().toString(), SiteTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateSiteSeeingPriceList);
                 SiteCommand = "Load";
                 SiteDayCommand = "Load";
                 SitedDateCommand = "Load";


                 if (SiteTableView.PageSize = 10) {
                     CRM.WebApp.webservice.SiteSeeingPriceListWebService.GetSitePrice(0, SiteTableView.get_pageSize(), SiteTableView.get_sortExpressions().toString(), SiteTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateSiteSeeingPriceList);
                 }
                 else if (SiteTableView.PageSize > 10) {
                     CRM.WebApp.webservice.SiteSeeingPriceListWebService.GetSitePrice(0, SiteTableView.get_pageSize(), SiteTableView.get_sortExpressions().toString(), SiteTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateSiteSeeingPriceList);
                 }
                 else if (SiteTableView.PageSize > 20) {
                     CRM.WebApp.webservice.SiteSeeingPriceListWebService.GetSitePrice(0, SiteTableView.get_pageSize(), SiteTableView.get_sortExpressions().toString(), SiteTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateSiteSeeingPriceList);
                 }

             }

             function delCustomer() {

                 CRM.WebApp.webservice.SiteSeeingPriceListWebService.deleteSitePrice(SIGHT_SEEING_PRICE_ID);
                 CRM.WebApp.webservice.SiteSeeingPriceListWebService.GetSitePrice(0, SiteTableView.get_pageSize(), SiteTableView.get_sortExpressions().toString(), SiteTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateSiteSeeingPriceList);

             }
             function delDays() {

                 CRM.WebApp.webservice.SiteSeeingPriceListWebService.deleteSiteDay(OPERATED_DAYS_ID);
                 CRM.WebApp.webservice.SiteSeeingPriceListWebService.Getday(SIGHT_SEEING_PRICE_ID, updateday);

             }
             function delDate() {

                 CRM.WebApp.webservice.SiteSeeingPriceListWebService.deleteSiteDate(OPERATED_DATE_ID);
                 CRM.WebApp.webservice.SiteSeeingPriceListWebService.Getdate(SIGHT_SEEING_PRICE_ID, updatedate);

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
             function CopyData() {

                 CRM.WebApp.webservice.SiteSeeingPriceListWebService.CopyData(SIGHT_SEEING_PRICE_ID);
                 var masterTable = $find("<%= radgridSiteSeeingPriceList.ClientID %>").get_masterTableView();
                 masterTable.rebind();
             }
             function PopUpShowing(sender, args) {

                 var divmore = document.getElementById('divmore');
                 divmore.style.display = 'block';
                 divmore.style.position = 'Absolute';
                 divmore.style.left = screen.width / 2 - 150;
                 divmore.style.top = screen.height / 2 - 150;

             }
             function addnewSitePrice(sender, args) {

                 currentRowIndex = sender.parentNode.parentNode.rowIndex;
                 var ary = [];

                 ary[1] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("SIGHT_SEEING_TIME").value;
                 ary[2] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("EFFECTIVE_FROM_DATE").value;
                 ary[3] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("EFFECTIVE_TO_DATE").value;

                 ary[4] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("RESTAURANT_NAME").value;
                // ary[5] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("MARGIN_AMOUNT").value;
                 //ary[6] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("MARGIN_AMOUNT_IN_PERCENTAGE").value;
                 ary[7] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;
                 ary[8] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("CITY_NAME").value;
                 ary[9] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_TERMS").value;

                 ary[10] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("SITE_NAME_LOCATION").value;
                 //ary[11] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("AGENT_NAME").value;
                 ary[12] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("PACKAGE_NAME").value;

                 ary[13] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("IS_MEAL_APPLICABLE").value;
                 ary[14] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_NAME").value;
                 ary[15] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("ADULT_SIC_RATE").value;
                 ary[16] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("CHILD_SIC_RATE").value;
                 ary[17] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("ADULT_PVT_RATE").value;
                 ary[18] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("CHILD_PVT_RATE").value;
                 ary[19] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("SICRATE_PER_PERSON").value;
                 ary[20] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("PVTRATE_PER_PERSON").value;
                 ary[21] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("MEAL_TYPE").value;
                 ary[22] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("STATUS").value;

                 //ary[23] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("TO_ISSUE_SERVICE_VOUCHER").value;
                 ary[24] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("A_MARGIN_IN_AMOUNT").value;
                 ary[25] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("A_PLUS_MARGIN_IN_AMOUNT").value;
                 ary[26] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("A_PLUS_PLUS_MARGIN_IN_AMOUNT").value;
                 ary[27] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("A_MARGIN_AMOUNT_IN_PERCENTAGE").value;
                 ary[28] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE").value;
                 ary[29] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE").value;

                 ary[30] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("TIME1").value;
                 ary[31] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("TIME2").value;
                 ary[32] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("TIME3").value;
                 ary[33] = SiteTableView.get_dataItems()[currentRowIndex - 1].findElement("TIME4").value;

                 ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SIGHT_SEEING_PRICE_ID;

                 if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                 if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
                 if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;
                 if (ary[6] == "" || ary[6] == 'null') ary[6] = 0;
                 if (ary[13] == "" || ary[13] == 'null') ary[13] = 0;
                 if (ary[21] == "" || ary[21] == 'null') ary[21] = 0;

                 //                 if (ary[5] != 0 && ary[6] != 0) {

                 //                     alert("You Cant Enter Both Margin Or Margin in[%]");

                 //                 }
                 //                 else if (ary[5] == 0 && ary[6] == 0) {
                 //                     alert("Enter Either Margin Or Margin in[%]");
                 //                 }
                 //                 else {

                 if (ary[13] == 'YES' && ary[4] != 0 && ary[21] != 0) {
                     try {


                         CRM.WebApp.webservice.SiteSeeingPriceListWebService.InsertUpdateSitePrice(ary);
                         CRM.WebApp.webservice.SiteSeeingPriceListWebService.GetSitePrice(0, SiteTableView.get_pageSize(), SiteTableView.get_sortExpressions().toString(), SiteTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateSiteSeeingPriceList);
                         alert('Record Save Successfully.');
                     }
                     catch (e) {
                         alert('Wrong Data Inserted');
                     }

                 }
                 else if (ary[13] == 'NO') {


                     CRM.WebApp.webservice.SiteSeeingPriceListWebService.InsertUpdateSitePrice(ary);
                     CRM.WebApp.webservice.SiteSeeingPriceListWebService.GetSitePrice(0, SiteTableView.get_pageSize(), SiteTableView.get_sortExpressions().toString(), SiteTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateSiteSeeingPriceList);
                     alert('Record Save Successfully.');
                 }
                 else {
                     alert('Enter Meal Type And Restaurant Name');
                 }

             }

             // }
             //             function OnCallBack(results, userContext, sender) {

             //                 
             //                 if (results == "currency") {

             //                     alert("Invalid Currency Name.");

             //                 }
             //                 else if (results == "Restaurant") {

             //                     alert("Invalid Restaurant Name.");
             //                 }
             //                 else if (results == "Payment") {

             //                     alert("Invalid Payment Terms.");
             //                 }
             //                 else if (results == "status") {

             //                     alert("Invalid Status.");
             //                 }
             //                 else if (results == "agent") {
             //                     alert("Invalid Agent Name");
             //                 }
             ////                 else if (results == "Package") {
             ////                     alert("Invalid Package Name");
             ////                 }
             //                 else if (results == "City") {
             //                     alert("Invalid City Name");
             //                 }
             //                 else if (results == "MealType") {
             //                     alert("Invalid Meal Type");
             //                 }
             //                 else if (results == "SiteName") {
             //                     alert("Invalid Site Name")
             //                 }
             //                 else if (results == "yes_no") {
             //                     alert("Invalid Is Meal");
             //                 }
             //                 else if (results == "Restaurant") {
             //                     alert("Invalid Restaurant Name.");
             //                 }
             //                 else if (results == "Time") {
             //                     alert("Invalid Time.");
             //                 }
             //                 else if (results == 0) {

             //                     alert('Record Save Successfully');
             //                 }
             //                 else {

             //                     alert('This Record All Ready Exist.');
             //                 }

             //             }
             function showpnl() {
                 document.getElementById('<%=pnlMainHead.ClientID %>').style.display = "";

                 document.getElementById('<%=Button4.ClientID %>').style.display = "";
                 document.getElementById('<%=Button3.ClientID %>').style.display = "none";
             }
             function SearchResult() {

                 document.getElementById('<%= pnlMainHead.ClientID %>').style.display = "none";
                 document.getElementById('<%=Button3.ClientID %>').style.display = "";
                 document.getElementById('<%=Button4.ClientID %>').style.display = "none";
                 scity = $("#ctl00_cphPageContent_txtCity").val();
                 sfname = $("#ctl00_cphPageContent_txtfname").val();

                 CRM.WebApp.webservice.SiteSeeingPriceListWebService.GetSitePrice(0, SiteTableView.get_pageSize(), SiteTableView.get_sortExpressions().toString(), SiteTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateSiteSeeingPriceList);
             }
             function addnewdays(sender, args) {

                 currentRowIndex = sender.parentNode.parentNode.rowIndex;
                 var ary = [];

                 ary[0] = SiteDayTableView.get_dataItems()[currentRowIndex - 1].findElement("DAYS").value;
                 ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.OPERATED_DAYS_ID;
                 for (i = 0; i < 2; i++) {
                     if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                 }
                 try {
                     CRM.WebApp.webservice.SiteSeeingPriceListWebService.InsertUpdateday(ary);
                     alert('Record Save Successfully');
                     CRM.WebApp.webservice.SiteSeeingPriceListWebService.Getday(SIGHT_SEEING_PRICE_ID, updateday);

                 }
                 catch (e) {
                     alert('Wrong Data Inserted');
                 }

             }
             function addnewdate(sender, args) {

                 currentRowIndex = sender.parentNode.parentNode.rowIndex;
                 var ary = [];

                 ary[0] = SiteDateTableView.get_dataItems()[currentRowIndex - 1].findElement("NOT_OPERATED_DATE").value;
                 ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.OPERATED_DATE_ID;
                 for (i = 0; i < 2; i++) {
                     if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                 }
                 try {
                     CRM.WebApp.webservice.SiteSeeingPriceListWebService.InsertUpdatedate(ary);
                     alert('Record Save Successfully');
                     CRM.WebApp.webservice.SiteSeeingPriceListWebService.Getdate(SIGHT_SEEING_PRICE_ID, updatedate);

                 }
                 catch (e) {
                     alert('Wrong Data Inserted');
                 }

             }
             function AddNewDate() {


                 CRM.WebApp.webservice.SiteSeeingPriceListWebService.InsertNewDates(SIGHT_SEEING_PRICE_ID);
                 CRM.WebApp.webservice.SiteSeeingPriceListWebService.Getdate(SIGHT_SEEING_PRICE_ID, updatedate);
             }
             function AddNewDay() {

                 CRM.WebApp.webservice.SiteSeeingPriceListWebService.InsertNewDay(SIGHT_SEEING_PRICE_ID);
                 CRM.WebApp.webservice.SiteSeeingPriceListWebService.Getday(SIGHT_SEEING_PRICE_ID, updateday);
             }
             function getMealName(sender) {

                 var value = sender.value;
                 CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
             }

             function onrowclick(sender, args) {
                 
                 currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
                 SIGHT_SEEING_PRICE_ID = args.get_gridDataItem()._dataItem.SIGHT_SEEING_PRICE_ID;
                 PACKAGE_NAME = args.get_gridDataItem()._dataItem.PACKAGE_NAME;

                 Show();
             }
             function Show() {
                 document.getElementById('<%=Button6.ClientID %>').style.display = "";
             }
             function Rate() {
                 window.location = "SiteSeeingPrice.aspx?PriceId=" + SIGHT_SEEING_PRICE_ID + "&name=" + PACKAGE_NAME;
              }
         </script>
    </telerik:radcodeblock>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Sightseeing Price List Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {


            var Currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_TYPE_FOR_ROOM_AUTOSEARCH";
            var paymentterms = "../../webservice/autocomplete.ashx?key=GET_PAYMENT_TERMS_FOR_AUTOSEARCH";
            var Sic_pvt = "../../webservice/autocomplete.ashx?key=GET_SIC_PVT_FOR_TRANSFER_PACKAGE_AUTOSEARCH";
            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            var dinner = "../../webservice/autocomplete.ashx?key=GET_DINNER_DESCRIPTION_FOR_AUTOSEARCH";
            var sitename = "../../webservice/autocomplete.ashx?key=GET_SITE_NAME_FOR_SITE_SEEING_PRICE_LIST";
            var agent = "../../webservice/autocomplete.ashx?key=GET_AGENT_NAME_SUPPLIER_CAR_PRICE_LISTFOR_AUTOSEARCH";
            var chainname = "../../webservice/autocomplete.ashx?key=GET_SUPPLIER_NAME_FOR_AUTOSEARCH";
            var a = "../../webservice/autocomplete.ashx?key=FETCH_YES_NO";
            var time = "../../webservice/autocomplete.ashx?key=GET_TIME_FOR_AUTOSEARCH";
            var days = "../../webservice/autocomplete.ashx?key=FETCH_OPERATED_DAYS_NAME_AUTOSEARCH";
            var mealtype = "../../webservice/autocomplete.ashx?key=FETCH_MEAL_TYPE_DUAL_PARAM?" + globalvalue;
            var status = "../../webservice/autocomplete.ashx?key=GET_PRICE_LIST_STATUS";
            var restaurant = "../../webservice/autocomplete.ashx?key=GET_RESTAURANT_NAME";

            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_CURRENCY_NAME").autocomplete(Currency);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_PAYMENT_TERMS").autocomplete(paymentterms);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_SIC_PVT_FLAG").autocomplete(Sic_pvt);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_CITY_NAME").autocomplete(city);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_DINNER_DESCRIPTION").autocomplete(dinner);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_SITE_NAME_LOCATION").autocomplete(sitename);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_AGENT_NAME").autocomplete(agent);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_RESTAURANT_NAME").autocomplete(chainname);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_IS_MEAL_APPLICABLE").autocomplete(a);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_SIGHT_SEEING_TIME").autocomplete(time);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_TIME1").autocomplete(time);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_TIME2").autocomplete(time);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_TIME3").autocomplete(time);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_TIME4").autocomplete(time);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_SUPPLIER_NAME").autocomplete(chainname);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_MEAL_TYPE").autocomplete(mealtype);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_RESTAURANT_NAME").autocomplete(restaurant);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_STATUS").autocomplete(status);
                $("#ctl00_cphPageContent_radgriddays_ctl00_ctl" + i + "_DAYS").autocomplete(days);
                $("#ctl00_cphPageContent_radgridSiteSeeingPriceList_ctl00_ctl" + i + "_TO_ISSUE_SERVICE_VOUCHER").autocomplete(a);
            }
            $("#ctl00_cphPageContent_txtCity").autocomplete(city);

        });       
    </script>
    <table>
        <tr>
            <td>
                <asp:Button ID="Button1" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Sight Seeing Detail?'))return false; delCustomer(); return false;"
                    Text="Delete" runat="server" />
            </td>
            <td>
                <asp:Button ID="btncopy" OnClientClick="CopyData();" CssClass="button" Style="float: right;
                    margin-right: 10px; color: black; font-weight: bold;" Text="Copy & Create New"
                    runat="server" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridSiteSeeingPriceList" runat="server" allowpaging="true"
                    allowmultirowselection="false" allowsorting="True" pagesize="50" itemstyle-wrap="false"
                    enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="SIGHT_SEEING_PRICE_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="4000px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SIGHT_SEEING_PRICE_ID" DataField="SIGHT_SEEING_PRICE_ID" HeaderText="SIGHT_SEEING_PRICE_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SIGHT_SEEING_PRICE_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_NAME" DataField="SUPPLIER_NAME" HeaderText="Supplier">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_NAME" runat="server" CssClass="radinput" EnableViewState="true"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CITY_NAME" DataField="CITY_NAME" HeaderText="City">
                          <ItemTemplate>
                            <asp:TextBox ID="CITY_NAME" runat="server" CssClass="radinput" EnableViewState="true"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                   
                    <telerik:GridTemplateColumn SortExpression ="PACKAGE_NAME" DataField="PACKAGE_NAME" HeaderText="Sightseeing Package">
                         <HeaderStyle HorizontalAlign="Left" Width="150px"/>  
                          <ItemTemplate>
                            <asp:TextBox ID="PACKAGE_NAME" runat="server" CssClass="radinput" EnableViewState="true"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SITE_NAME_LOCATION" DataField="SITE_NAME_LOCATION" HeaderText="Sight Name">
                          <ItemTemplate>
                            <asp:TextBox ID="SITE_NAME_LOCATION" runat="server" CssClass="radinput" EnableViewState="true"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="IS_MEAL_APPLICABLE" DataField="IS_MEAL_APPLICABLE" HeaderText="Meal Applicable?">
                          <ItemTemplate>
                            <asp:TextBox ID="IS_MEAL_APPLICABLE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="RESTAURANT_NAME" DataField="RESTAURANT_NAME" HeaderText="Restaurant Name">
                          <ItemTemplate>
                            <asp:TextBox ID="RESTAURANT_NAME" runat="server" CssClass="radinput" onblur="getMealName(this);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MEAL_TYPE" DataField="MEAL_TYPE" HeaderText="Meal Type">
                          <ItemTemplate>
                            <asp:TextBox ID="MEAL_TYPE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                   
                    <telerik:GridTemplateColumn SortExpression ="SIGHT_SEEING_TIME" DataField="SIGHT_SEEING_TIME" HeaderText="Time1">
                          <ItemTemplate>
                            <asp:TextBox ID="SIGHT_SEEING_TIME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TIME1" DataField="TIME1" HeaderText="Time2">
                          <ItemTemplate>
                            <asp:TextBox ID="TIME1" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TIME2" DataField="TIME2" HeaderText="Time3">
                          <ItemTemplate>
                            <asp:TextBox ID="TIME2" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TIME3" DataField="TIME3" HeaderText="Time4">
                          <ItemTemplate>
                            <asp:TextBox ID="TIME3" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TIME4" DataField="TIME4" HeaderText="Time5">
                          <ItemTemplate>
                            <asp:TextBox ID="TIME4" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CURRENCY_NAME" DataField="CURRENCY_NAME" HeaderText="Currency">
                          <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ADULT_SIC_RATE" DataField="ADULT_SIC_RATE" HeaderText="Adult SIC Rate">
                          <ItemTemplate>
                            <asp:TextBox ID="ADULT_SIC_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CHILD_SIC_RATE" DataField="CHILD_SIC_RATE" HeaderText="Child SIC Rate">
                          <ItemTemplate>
                            <asp:TextBox ID="CHILD_SIC_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ADULT_PVT_RATE" DataField="ADULT_PVT_RATE" HeaderText="Adult PVT Rate">
                          <ItemTemplate>
                            <asp:TextBox ID="ADULT_PVT_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CHILD_PVT_RATE" DataField="CHILD_PVT_RATE" HeaderText="Child PVT Rate">
                          <ItemTemplate>
                            <asp:TextBox ID="CHILD_PVT_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SICRATE_PER_PERSON" DataField="SICRATE_PER_PERSON" HeaderText="SIC Rate Per Person">
                          <ItemTemplate>
                            <asp:TextBox ID="SICRATE_PER_PERSON" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PVTRATE_PER_PERSON" DataField="PVTRATE_PER_PERSON" HeaderText="PVT Rate Per Person">
                          <ItemTemplate>
                            <asp:TextBox ID="PVTRATE_PER_PERSON" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="FIT_RATE" DataField="FIT_RATE" HeaderText="Rate Per Person" visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="FIT_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    
                     <telerik:GridTemplateColumn SortExpression ="MARGIN_AMOUNT" DataField="MARGIN_AMOUNT" HeaderText="Margin" visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="MARGIN_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MARGIN_AMOUNT_IN_PERCENTAGE" DataField="MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="Margin(%)"  visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="MARGIN_AMOUNT_IN_PERCENTAGE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="EFFECTIVE_FROM_DATE" DataField="EFFECTIVE_FROM_DATE" HeaderText="Validity From Date">
                          <ItemTemplate>
                            <asp:TextBox ID="EFFECTIVE_FROM_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EFFECTIVE_TO_DATE" DataField="EFFECTIVE_TO_DATE" HeaderText="Validity to Date">
                          <ItemTemplate>
                            <asp:TextBox ID="EFFECTIVE_TO_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PAYMENT_TERMS" DataField="PAYMENT_TERMS" HeaderText="Payment Terms">
                          <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_TERMS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="AGENT_NAME" DataField="AGENT_NAME" HeaderText="Agent" visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="AGENT_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="STATUS" DataField="STATUS" HeaderText="Status">
                          <ItemTemplate>
                            <asp:TextBox ID="STATUS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TO_ISSUE_SERVICE_VOUCHER" DataField="TO_ISSUE_SERVICE_VOUCHER" HeaderText="To Issue Service Voucher?" visible="false">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="TO_ISSUE_SERVICE_VOUCHER" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="A_MARGIN_IN_AMOUNT" DataField="A_MARGIN_IN_AMOUNT" HeaderText="A Margin Amount">
                          <ItemTemplate>
                            <asp:TextBox ID="A_MARGIN_IN_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="A_PLUS_MARGIN_IN_AMOUNT" DataField="A_PLUS_MARGIN_IN_AMOUNT" HeaderText="A+ Margin Amount">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="A_PLUS_MARGIN_IN_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="A_PLUS_PLUS_MARGIN_IN_AMOUNT" DataField="A_PLUS_PLUS_MARGIN_IN_AMOUNT" HeaderText="A++ Margin Amount">
                          <ItemTemplate>
                            <asp:TextBox ID="A_PLUS_PLUS_MARGIN_IN_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="A_MARGIN_AMOUNT_IN_PERCENTAGE" DataField="A_MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="A Margin Amount[%]">
                          <ItemTemplate>
                            <asp:TextBox ID="A_MARGIN_AMOUNT_IN_PERCENTAGE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE" DataField="A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="A+ Margin Amount[%]">
                          <ItemTemplate>
                            <asp:TextBox ID="A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE" DataField="A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="A++ Margin Amount[%]">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewSitePrice(this,event);">
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
                <ClientEvents OnCommand="radgridSiteSeeingPriceList_Command" OnRowSelected="radgridSiteSeeingPriceList_RowSelected" OnRowDblClick="SiteSeeingPriceList" OnRowClick="onrowclick"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button6" runat="server" Text="SightSeeing Rate" Width="150px" Style="float: left; margin-right: 10px;
                    display: none; color: black;" CssClass="button" OnClientClick="Rate();" />
            </td>
        </tr>
    </table>
    <table>
       
        <tr>
            <td>
                <asp:Button ID="Button3" runat="server" Text="Search" OnClientClick="showpnl();"
                    Style="float: right; margin-right: 10px; display: block; color: black;" CssClass="button" />
                <asp:Button ID="Button4" runat="server" Text="Search Now" Style="float: right; margin-right: 10px;
                    display: none; color: black;" CssClass="button" OnClientClick="SearchResult();" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Panel ID="pnlMainHead" runat="server" Style="display: none">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Employee Detail:" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkrel" runat="server" onClick="check();" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblfname" runat="server" Text="Sightseeing Package:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfname" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="City:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCity" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>

    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal1" runat="server" Text="Sight Seeing Not Operated Days"></asp:Literal>
    </div>
    <br />
    <br />
    <table>
        <tr>
            <td>
                <asp:Button ID="Button2" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Sight Days?'))return false; delDays(); return false;"
                    Text="Delete" runat="server" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgriddays" runat="server" allowpaging="false" allowmultirowselection="false"
                    allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="OPERATED_DAYS_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="400px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="OPERATED_DAYS_ID" DataField="OPERATED_DAYS_ID" HeaderText="OPERATED_DAYS_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="OPERATED_DAYS_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="DAYS" DataField="DAYS" HeaderText="DAYS">
                          <ItemTemplate>
                            <asp:TextBox ID="DAYS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewdays(this,event);">
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
                <ClientEvents OnCommand="radgriddays_Command" OnRowSelected="radgriddays_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                <asp:LinkButton ID="lbAddday" runat="server" Text="Add New Days" OnClientClick="AddNewDay();"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal2" runat="server" Text="Sight Seeing Not Operated Date"></asp:Literal>
    </div>
    <br />
    <br />
    <table>
        <tr>
            <td>
                <asp:Button ID="Button5" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Sight Date?'))return false; delDate(); return false;"
                    Text="Delete" runat="server" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgriddate" runat="server" allowpaging="false" allowmultirowselection="false"
                    allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="OPERATED_DATE_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="400px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="OPERATED_DATE_ID" DataField="OPERATED_DATE_ID" HeaderText="OPERATED_DATE_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="OPERATED_DATE_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="NOT_OPERATED_DATE" DataField="NOT_OPERATED_DATE" HeaderText="Note Operated Date">
                          <ItemTemplate>
                            <asp:TextBox ID="NOT_OPERATED_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A3" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewdate(this,event);">
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
                <ClientEvents OnCommand="radgriddate_Command" OnRowSelected="radgriddate_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                <asp:LinkButton ID="lbAdddate" runat="server" Text="Add New Date" OnClientClick="AddNewDate();"></asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
