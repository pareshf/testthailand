var SIGHT_SEEING_PRICE_ID = "";
var OPERATED_DAYS_ID = "";
var OPERATED_DATE_ID = "";
var SiteDayCommand = "";
var SitedDateCommand = "";
var SiteDayTableView = "";
var SiteDateTableView = "";
var SiteCommand = "";
var SiteTableView = null;
var scity = "";
var sfname = "";
var globalvalue = "";
var PACKAGE_NAME = "";

function radgridSiteSeeingPriceList_Command(sender, args) {

    args.set_cancel(true);
    var newPageSize = SiteTableView.get_pageSize();
    SiteTableView.set_pageSize(newPageSize);
    CRM.WebApp.webservice.SiteSeeingPriceListWebService.GetSitePrice(SiteTableView.get_currentPageIndex() * SiteTableView.get_pageSize(), SiteTableView.get_pageSize(), SiteTableView.get_sortExpressions().toString(), SiteTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateSiteSeeingPriceList);
    SiteCommand = args.get_commandName;

}
function radgridSiteSeeingPriceList_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        SIGHT_SEEING_PRICE_ID = args.get_gridDataItem()._dataItem.SIGHT_SEEING_PRICE_ID;
        PACKAGE_NAME = args.get_gridDataItem()._dataItem.PACKAGE_NAME;
        CRM.WebApp.webservice.SiteSeeingPriceListWebService.Getdate(SIGHT_SEEING_PRICE_ID, updatedate);
        CRM.WebApp.webservice.SiteSeeingPriceListWebService.Getday(SIGHT_SEEING_PRICE_ID, updateday);

    }
    catch (e) { }
}
function radgriddays_Command(sender, args) {

}
function radgriddays_RowSelected(sender, args) {

}
function radgriddate_Command(sender, args) {

}
function radgriddate_RowSelected(sender, args) {

}
function updateSitePriceVirtualCount(result) { SiteTableView.set_virtualItemCount(result); }

function updateDayVirtualCount(result) { SiteDayTableView.set_virtualItemCount(result); }

function updateDatePriceVirtualCount(result) { SiteDateTableView.set_virtualItemCount(result); }

//function updateSiteSeeingPriceList(result) {

//    SiteTableView.set_dataSource(result);
//    SiteTableView.dataBind();
//    if (result.length > 0) { SiteTableView.selectItem(0); }

//    if (SiteCommand == "Filter" || SiteCommand == "Load") { CRM.WebApp.webservice.SiteSeeingPriceListWebService.GetSitePriceCount(updateSitePriceVirtualCount); }
//}
//
function updateSiteSeeingPriceList(result) {
    SiteTableView.set_dataSource(result);
    SiteTableView.dataBind();
    if (result.length > 0) { SiteTableView.selectItem(0); SIGHT_SEEING_PRICE_ID = result[0]["SIGHT_SEEING_PRICE_ID"]; }
    else { SIGHT_SEEING_PRICE_ID = ""; }
    if (SiteCommand == "Filter" || SiteCommand == "Load") { CRM.WebApp.webservice.SiteSeeingPriceListWebService.GetSitePriceCount(updateSitePriceVirtualCount); }
}

function updateday(result) {
    SiteDayTableView.set_dataSource(result);
    SiteDayTableView.dataBind();
    if (result.length > 0) { SiteDayTableView.selectItem(0); OPERATED_DAYS_ID = result[0]["OPERATED_DAYS_ID"]; }
    else { OPERATED_DAYS_ID = ""; }
    if (SiteDayCommand == "Filter" || SiteDayCommand == "Load") { CRM.WebApp.webservice.SiteSeeingPriceListWebService.GetdayCount(updateDayVirtualCount); }
}

function updatedate(result) {
    SiteDateTableView.set_dataSource(result);
    SiteDateTableView.dataBind();
    if (result.length > 0) { SiteDateTableView.selectItem(0); OPERATED_DATE_ID = result[0]["OPERATED_DATE_ID"]; }
    else { OPERATED_DATE_ID = ""; }
    if (SitedDateCommand == "Filter" || SitedDateCommand == "Load") { CRM.WebApp.webservice.SiteSeeingPriceListWebService.GetdateCount(updateDatePriceVirtualCount); }
}

function SiteSeeingPriceList(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];


    ary[1] = SiteTableView.get_dataItems()[currentRowIndex].findElement("SIGHT_SEEING_TIME").value;
    ary[2] = SiteTableView.get_dataItems()[currentRowIndex].findElement("EFFECTIVE_FROM_DATE").value;
    ary[3] = SiteTableView.get_dataItems()[currentRowIndex].findElement("EFFECTIVE_TO_DATE").value;

    ary[4] = SiteTableView.get_dataItems()[currentRowIndex].findElement("RESTAURANT_NAME").value;
    //ary[5] = SiteTableView.get_dataItems()[currentRowIndex].findElement("MARGIN_AMOUNT").value;
    //ary[6] = SiteTableView.get_dataItems()[currentRowIndex].findElement("MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[7] = SiteTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    ary[8] = SiteTableView.get_dataItems()[currentRowIndex].findElement("CITY_NAME").value;
    ary[9] = SiteTableView.get_dataItems()[currentRowIndex].findElement("PAYMENT_TERMS").value;

    ary[10] = SiteTableView.get_dataItems()[currentRowIndex].findElement("SITE_NAME_LOCATION").value;
    //ary[11] = SiteTableView.get_dataItems()[currentRowIndex].findElement("AGENT_NAME").value;
    ary[12] = SiteTableView.get_dataItems()[currentRowIndex].findElement("PACKAGE_NAME").value;

    ary[13] = SiteTableView.get_dataItems()[currentRowIndex].findElement("IS_MEAL_APPLICABLE").value;
    ary[14] = SiteTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_NAME").value;
    ary[15] = SiteTableView.get_dataItems()[currentRowIndex].findElement("ADULT_SIC_RATE").value;
    ary[16] = SiteTableView.get_dataItems()[currentRowIndex].findElement("CHILD_SIC_RATE").value;
    ary[17] = SiteTableView.get_dataItems()[currentRowIndex].findElement("ADULT_PVT_RATE").value;
    ary[18] = SiteTableView.get_dataItems()[currentRowIndex].findElement("CHILD_PVT_RATE").value;
    ary[19] = SiteTableView.get_dataItems()[currentRowIndex].findElement("SICRATE_PER_PERSON").value;
    ary[20] = SiteTableView.get_dataItems()[currentRowIndex].findElement("PVTRATE_PER_PERSON").value;
    ary[21] = SiteTableView.get_dataItems()[currentRowIndex].findElement("MEAL_TYPE").value;
    ary[22] = SiteTableView.get_dataItems()[currentRowIndex].findElement("STATUS").value;

    //ary[23] = SiteTableView.get_dataItems()[currentRowIndex].findElement("TO_ISSUE_SERVICE_VOUCHER").value;
    ary[24] = SiteTableView.get_dataItems()[currentRowIndex].findElement("A_MARGIN_IN_AMOUNT").value;
    ary[25] = SiteTableView.get_dataItems()[currentRowIndex].findElement("A_PLUS_MARGIN_IN_AMOUNT").value;
    ary[26] = SiteTableView.get_dataItems()[currentRowIndex].findElement("A_PLUS_PLUS_MARGIN_IN_AMOUNT").value;
    ary[27] = SiteTableView.get_dataItems()[currentRowIndex].findElement("A_MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[28] = SiteTableView.get_dataItems()[currentRowIndex].findElement("A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[29] = SiteTableView.get_dataItems()[currentRowIndex].findElement("A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE").value;

    ary[30] = SiteTableView.get_dataItems()[currentRowIndex].findElement("TIME1").value;
    ary[31] = SiteTableView.get_dataItems()[currentRowIndex].findElement("TIME2").value;
    ary[32] = SiteTableView.get_dataItems()[currentRowIndex].findElement("TIME3").value;
    ary[33] = SiteTableView.get_dataItems()[currentRowIndex].findElement("TIME4").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SIGHT_SEEING_PRICE_ID;

    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
    if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
    if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;
    if (ary[6] == "" || ary[6] == 'null') ary[6] = 0;
    if (ary[13] == "" || ary[13] == 'null') ary[13] = 0;
    if (ary[21] == "" || ary[21] == 'null') ary[21] = 0;

    //    if (ary[5] != 0 && ary[6] != 0) {

    //        alert("You Cant Enter Both Margin Or Margin in[%]");

    //    }
    //    else if (ary[5] == 0 && ary[6] == 0) {
    //        alert("Enter Either Margin Or Margin in[%]");
    //    }
    //    else {

    if (ary[13] == 'YES' && ary[4] != 0 && ary[21] != 0) {
        try {
            CRM.WebApp.webservice.SiteSeeingPriceListWebService.InsertUpdateSitePrice(ary);
            CRM.WebApp.webservice.SiteSeeingPriceListWebService.GetSitePrice(0, SiteTableView.get_pageSize(), SiteTableView.get_sortExpressions().toString(), SiteTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateSiteSeeingPriceList);

            alert('Record Save Successfully');

        }
        catch (e) {
            alert('Wrong Data Inserted');
        }
    }
    else if (ary[13] == 'NO') {
        CRM.WebApp.webservice.SiteSeeingPriceListWebService.InsertUpdateSitePrice(ary);
        CRM.WebApp.webservice.SiteSeeingPriceListWebService.GetSitePrice(0, SiteTableView.get_pageSize(), SiteTableView.get_sortExpressions().toString(), SiteTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateSiteSeeingPriceList);

        alert('Record Save Successfully');
    }
    else {
        alert('Enter Meal Type And Restaurant Name');
    }

}
//}
//function OnCallBack(results, userContext, sender) {


//    if (results == "currency") {

//        alert("Invalid Currency Name.");

//    }
//    else if (results == "Restaurant") {

//        alert("Invalid Restaurant Name.");
//    }
//    else if (results == "Payment") {

//        alert("Invalid Payment Terms.");
//    }
//    else if (results == "status") {

//        alert("Invalid Status.");
//    }
//    else if (results == "agent") {
//        alert("Invalid Agent Name");
//    }
//    //                 else if (results == "Package") {
//    //                     alert("Invalid Package Name");
//    //                 }
//    else if (results == "City") {
//        alert("Invalid City Name");
//    }
//    else if (results == "MealType") {
//        alert("Invalid Meal Type");
//    }
//    else if (results == "SiteName") {
//        alert("Invalid Site Name")
//    }
//    else if (results == "yes_no") {
//        alert("Invalid Is Meal");
//    }
//    else if (results == "Restaurant") {
//        alert("Invalid Restaurant Name.");
//    }
//    else if (results == "Time") {
//        alert("Invalid Time.");
//    }
//    else if (results == 0) {

//        alert('Record Save Successfully');
//    }
//    else {

//        alert('This Record All Ready Exist.');
//    }

//}