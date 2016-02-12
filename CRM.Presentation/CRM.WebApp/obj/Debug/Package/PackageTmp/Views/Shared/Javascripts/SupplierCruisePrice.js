var SUPPLIER_CRUISE_PRICE_LIST_ID = "";
var SUPPLIER_CRUISE_CABIN_INVENTORY_ID = "";
var SUPPLIER_SR_NO = "";
var CruisePriceCommand = "";
var CruisePriceTableView = null;
var CabinInventoryCommand = "";
var CabinInventoryTableView = "";
var sfname = "";
//var s = "";


function radgridsupplierCruisePrice_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    CruisePriceTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SupplierCruisePriceWebService.GetCrusiePriceList(CruisePriceTableView.get_currentPageIndex() * CruisePriceTableView.get_pageSize(), CruisePriceTableView.get_pageSize(), CruisePriceTableView.get_sortExpressions().toString(), CruisePriceTableView.get_filterExpressions().toDynamicLinq(), sfname, updateSupplierCruisePrice);
    CruisePriceCommand = args.get_commandName;

}
function radgridsupplierCruisePrice_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        SUPPLIER_CRUISE_PRICE_LIST_ID = args.get_gridDataItem()._dataItem.SUPPLIER_CRUISE_PRICE_LIST_ID;
        // CRM.WebApp.webservice.SupplierCruisePriceWebService.GetCruiseCabin(SUPPLIER_CRUISE_PRICE_LIST_ID, updateCruiseCabinInventory);

    }
    catch (e) { }
}
function radgridsupplierCruiseCabinInventory_Command(sender, args) {



}
function radgridsupplierCruiseCabinInventory_RowSelected(sender, args) {



}
function updateCruisePriceVirtualCount(result) { CruisePriceTableView.set_virtualItemCount(result); }

function updateCruiseCabinInventoryVirtualCount(result) { CabinInventoryTableView.set_virtualItemCount(result); }

function updateSupplierCruisePrice(result) {

    CruisePriceTableView.set_dataSource(result);
    CruisePriceTableView.dataBind();
    if (result.length > 0) { CruisePriceTableView.selectItem(0); }

    if (CruisePriceCommand == "Filter" || CruisePriceCommand == "Load") { CRM.WebApp.webservice.SupplierCruisePriceWebService.GetCrusieCount(updateCruisePriceVirtualCount); }
}
function updateCruiseCabinInventory(result) {

    CabinInventoryTableView.set_dataSource(result);
    CabinInventoryTableView.dataBind();
    if (result.length > 0) { CabinInventoryTableView.selectItem(0); }

    if (CruisePriceCommand == "Filter" || CruisePriceCommand == "Load") { CRM.WebApp.webservice.SupplierCruisePriceWebService.GetCrusieCabin(updateCruiseCabinInventoryVirtualCount); }
}

function CruisePriceList(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    //ary[1] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("EFFECTIVE_FROM_DATE").value;
    //ary[2] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("EFFECTIVE_TO_DATE").value;
    ary[3] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CATEGORY_CODE").value;
    ary[4] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("DECK_NO").value;
    ary[5] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("CRUISE_VIEW").value;
    //ary[6] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("GIT_RATE").value;
    //ary[7] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("FIT_RATE").value;
    //ary[8] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("FIT_DISCOUNT").value;
    //ary[9] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("GIT_DISCOUNT").value;
    ary[10] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("MARGIN_AMOUNT").value;
    ary[11] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[12] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    ary[13] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("PAYMENT_TERMS").value;
    //ary[14] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("AUTO_BOOK_ON_LOW_INVENTORY").value;
    ary[15] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("CUSTOMER_NAME").value;
    ary[16] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_NAME").value;
    //ary[17] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CRUISE_DESC").value;
    //ary[18] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("GIT_MARGIN_AMOUNT").value;
    //ary[19] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("GIT_MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[17] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("SINGLE_ROOM_RATE").value;
    ary[18] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("DOUBLE_ROOM_RATE").value;
    ary[19] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("EXTRA_ADULT_RATE").value;
    ary[20] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("EXTRA_CWB_COST").value;
    ary[21] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("EXTRA_CNB_COST").value;
    ary[22] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("FROM_DATE").value;
    ary[23] = CruisePriceTableView.get_dataItems()[currentRowIndex].findElement("TO_DATE").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SUPPLIER_CRUISE_PRICE_LIST_ID;

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
            CRM.WebApp.webservice.SupplierCruisePriceWebService.InsertUpdateCruisePrice(ary);
            CRM.WebApp.webservice.SupplierCruisePriceWebService.GetCrusiePriceList(0, CruisePriceTableView.get_pageSize(), CruisePriceTableView.get_sortExpressions().toString(), CruisePriceTableView.get_filterExpressions().toDynamicLinq(), sfname, updateSupplierCruisePrice);

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
//        alert("Invalid Agent Name");
//    }
//    else if (results == "Cabin_category") {
//        alert("Invalid Cabin Category.")
//    }
//    else if (results == 0) {

//        alert('Record Save Successfully');
//    }
//    else {

//        alert('This Record All Ready Exist.');
//    }

//}
