var SUPPLIER_RESTAURANT_PRICE_LIST_ID = "";
var SUPPLIER_SR_NO = "";
var RestaurantPriceCommand = "";
var RestaurantPriceTableView = null;
var sfname = "";
var scity = "";
//var s = "";

function radgridsupplierRestaurantPrice_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    RestaurantPriceTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SupplierRestaurantPriceListWebService.GetRestaurantPriceList(RestaurantPriceTableView.get_currentPageIndex() * RestaurantPriceTableView.get_pageSize(), RestaurantPriceTableView.get_pageSize(), RestaurantPriceTableView.get_sortExpressions().toString(), RestaurantPriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateRestaurantPrice);
    RestaurantPriceCommand = args.get_commandName;

}
function radgridsupplierRestaurantPrice_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        SUPPLIER_RESTAURANT_PRICE_LIST_ID = args.get_gridDataItem()._dataItem.SUPPLIER_RESTAURANT_PRICE_LIST_ID;
    }
    catch (e) { }
}
function updateRestaurantPriceVirtualCount(result) { RestaurantPriceTableView.set_virtualItemCount(result); }

function updateRestaurantPrice(result) {

    RestaurantPriceTableView.set_dataSource(result);
    RestaurantPriceTableView.dataBind();
    if (result.length > 0) { RestaurantPriceTableView.selectItem(0); }

    if (RestaurantPriceCommand == "Filter" || RestaurantPriceCommand == "Load") { CRM.WebApp.webservice.SupplierRestaurantPriceListWebService.GetRestaurantPriceCount(updateRestaurantPriceVirtualCount); }
}

function RestaurantPriceList(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();


    var ary = [];

    //ary[1] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("EFFECTIVE_FROM_DATE").value;
    //ary[2] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("EFFECTIVE_TO_DATE").value;
    ary[3] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("DINNER_FROM_DATE").value;
    ary[4] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("DINNER_TO_DATE").value;
    ary[5] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("DINNER_FROM_TIME").value;
    ary[6] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("DINNER_TO_TIME").value;
    ary[7] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("MEAL_DESC").value;
    //ary[8] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("DINNER_DESCRIPTION").value;
    //ary[9] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("GIT_RATE").value;
    ary[10] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("FIT_RATE").value;
    //ary[11] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("FIT_DISCOUNT").value;
    //ary[12] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("GIT_DISCOUNT").value;
    //ary[13] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("MARGIN_AMOUNT").value;
    //ary[14] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[15] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    ary[16] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("PAYMENT_TERMS").value;
    //ary[17] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("CUSTOMER_NAME").value;
    ary[18] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("CHAIN_NAME").value;
    //ary[19] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("GIT_MARGIN_AMOUNT").value;
    //ary[20] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("GIT_MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[21] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("MEAL_TYPE").value;
    ary[22] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("CITY_NAME").value;
    ary[23] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("CHILD_RATE").value;
    ary[24] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("ADULT_RATE").value;

    //ary[25] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("TO_ISSUE_SERVICE_VOUCHER").value;
    ary[26] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("A_MARGIN_IN_AMOUNT").value;
    ary[27] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("A_PLUS_MARGIN_IN_AMOUNT").value;
    ary[28] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("A_PLUS_PLUS_MARGIN_IN_AMOUNT").value;
    ary[29] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("A_MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[30] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[31] = RestaurantPriceTableView.get_dataItems()[currentRowIndex].findElement("A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SUPPLIER_RESTAURANT_PRICE_LIST_ID;
    //                 for (i = 0; i < 16; i++) {
    //                     if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    //                 }
    //abcd
    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
    if (ary[13] == "" || ary[13] == 'null') ary[13] = 0;
    if (ary[14] == "" || ary[14] == 'null') ary[14] = 0;
    //    if (ary[13] != 0 && ary[14] != 0) {

    //        alert("You Cant Enter Both Margin Or Margin in[%]");

    //    }
    //    else if (ary[13] == 0 && ary[14] == 0) {
    //        alert("Enter Either Margin Or Margin in[%]");
    //    }
    //    else {
    try {
        CRM.WebApp.webservice.SupplierRestaurantPriceListWebService.InsertUpdateRestaurantPrice(ary);
        CRM.WebApp.webservice.SupplierRestaurantPriceListWebService.GetRestaurantPriceList(0, RestaurantPriceTableView.get_pageSize(), RestaurantPriceTableView.get_sortExpressions().toString(), RestaurantPriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateRestaurantPrice);

        alert('Record Save Successfully');

    }
    catch (e) {
        alert('Wrong Data Inserted');
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

//        alert("Invalid Status.")
//    }
//    else if (results == "agent") {
//        alert("Invalid Agent Name")
//    }
//    else if (results == "City") {
//        alert("Invalid City Name")
//    }
//    else if (results == "MealType") {
//        alert("Invalid Meal Type")
//    }
//    else if (results == 0) {

//        alert('Record Save Successfully');
//    }
//    else {

//        alert('This Record All Ready Exist.');
//    }

//}