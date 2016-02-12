var COACH_PRICE_LIST_ID = "";
var SUPPLIER_SR_NO = "";
var CoachPriceCommand = "";
var CoachPriceTableView = null;
var scity = "";
var sfname = "";
//var s = "";

function radgridsupplierCoachPrice_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    CoachPriceTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SupplierCoachPriceListWebService.GetCoachPriceList(CoachPriceTableView.get_currentPageIndex() * CoachPriceTableView.get_pageSize(), CoachPriceTableView.get_pageSize(), CoachPriceTableView.get_sortExpressions().toString(), CoachPriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateCoachPrice);
    CoachPriceCommand = args.get_commandName;

}
function radgridsupplierCoachPrice_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        COACH_PRICE_LIST_ID = args.get_gridDataItem()._dataItem.COACH_PRICE_LIST_ID;
    }
    catch (e) { }
}
function updateCoachPriceVirtualCount(result) { CoachPriceTableView.set_virtualItemCount(result); }

function updateCoachPrice(result) {

    CoachPriceTableView.set_dataSource(result);
    CoachPriceTableView.dataBind();
    if (result.length > 0) { CoachPriceTableView.selectItem(0); }

    if (CoachPriceCommand == "Filter" || CoachPriceCommand == "Load") { CRM.WebApp.webservice.SupplierCoachPriceListWebService.GetCoachCount(updateCoachPriceVirtualCount); }
}

function CoachPriceList(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    //ary[1] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("EFFECTIVE_FROM_DATE").value;
    //ary[2] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("EFFECTIVE_TO_DATE").value;
    ary[3] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("COACH_NAME").value;
    ary[4] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("CUSTOMER_NAME").value;
    ary[5] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_NAME").value;
    //ary[6] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("GIT_RATE").value;
    ary[7] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("FIT_RATE").value;
    //ary[8] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("FIT_DISCOUNT").value;
    //ary[9] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("GIT_DISCOUNT").value;
    ary[10] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("MARGIN_AMOUNT").value;
    ary[11] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[12] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    ary[13] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("PAYMENT_TERMS").value;
    ary[14] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("NO_OF_SEATS").value;
    // ary[15] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("GIT_MARGIN_AMOUNT").value;
    // ary[16] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("GIT_MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[17] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("CITY_NAME").value;
    ary[18] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("FROM_DATE").value;
    ary[19] = CoachPriceTableView.get_dataItems()[currentRowIndex].findElement("TO_DATE").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.COACH_PRICE_LIST_ID;
    //                 for (i = 0; i < 16; i++) {
    //                     if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    //                 }
    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
    if (ary[10] == "" || ary[10] == 'null') ary[10] = 0;
    if (ary[11] == "" || ary[11] == 'null') ary[11] = 0;
    if (ary[10] != 0 && ary[11] != 0) {

        alert("You Cant Enter Both Margin Or Margin in[%]");

    }
    else if (ary[10] == 0 && ary[11] == 0) {
        alert("Enter Either Margin Or Margin in[%]");
    }
    else {
        try {
            CRM.WebApp.webservice.SupplierCoachPriceListWebService.InsertUpdateCoachPrice(ary);
            CRM.WebApp.webservice.SupplierCoachPriceListWebService.GetCoachPriceList(0, CoachPriceTableView.get_pageSize(), CoachPriceTableView.get_sortExpressions().toString(), CoachPriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateCoachPrice);

            alert('Record Save Successfully');

        }
        catch (e) {
            alert('Wrong Data Inserted');
        }
    }
}
//function OnCallBack(results, userContext, sender) {


//    if (results == "currency") {

//        alert("Invalid Currency Name.");
//    }
//    else if (results == "Coach_Company") {

//        alert("Invalid Restaurant Name.");
//    }
//    else if (results == "Payment") {

//        alert("Invalid Payment Terms.");
//    }
//    else if (results == "agent") {
//        alert("Invalid Agent Name")
//    }
//    else if (results == "City") {
//        alert("Invalid City Name")
//    }
//    else if (results == "Coach") {
//        alert("Invalid Coach Name.")
//    }
//    else if (results == 0) {

//        alert('Record Save Successfully');
//    }
//    else {

//        alert('This Record All Ready Exist.');
//    }

//}