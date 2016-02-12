var CAR_PRICE_LIST_MASTER_ID = "";
var SUPPLIER_SR_NO = "";
var CarPriceCommand = "";
var CarPriceTableView = null;
var scity = "";
var sfname = "";
//var s = "";

function radgridsupplierCarPrice_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    CarPriceTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SupplierCarPriceListWebService.GetCarPriceList(CarPriceTableView.get_currentPageIndex() * CarPriceTableView.get_pageSize(), CarPriceTableView.get_pageSize(), CarPriceTableView.get_sortExpressions().toString(), CarPriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateCarPrice);
    CarPriceCommand = args.get_commandName;

}
function radgridsupplierCarPrice_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        CAR_PRICE_LIST_MASTER_ID = args.get_gridDataItem()._dataItem.CAR_PRICE_LIST_MASTER_ID;


    }
    catch (e) { }
}
function updateCarPriceVirtualCount(result) { CarPriceTableView.set_virtualItemCount(result); }

function updateCarPrice(result) {

    CarPriceTableView.set_dataSource(result);
    CarPriceTableView.dataBind();
    if (result.length > 0) { CarPriceTableView.selectItem(0); }

    if (CarPriceCommand == "Filter" || CarPriceCommand == "Load") { CRM.WebApp.webservice.SupplierCarPriceListWebService.GetCarPriceCount(updateCarPriceVirtualCount); }
}

function CarPriceList(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();


    var ary = [];

    ary[1] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("EFFECTIVE_FROM_DATE").value;
    ary[2] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("EFFECTIVE_TO_DATE").value;
    ary[3] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("CAR_NAME").value;
    ary[4] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("CUSTOMER_NAME").value;
    ary[5] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_NAME").value;
    //ary[6] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("GIT_RATE").value;
    ary[7] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("FIT_RATE").value;
    // ary[8] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("FIT_DISCOUNT").value;
    //ary[9] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("GIT_DISCOUNT").value;
    ary[10] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("MARGIN_AMOUNT").value;
    ary[11] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[12] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    ary[13] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("PAYMENT_TERMS").value;
    // ary[14] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("GIT_MARGIN_AMOUNT").value;
    //ary[15] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("GIT_MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[16] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("FROM_DATE").value;
    ary[17] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("TO_DATE").value;
    ary[18] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("CITY_NAME").value;
    ary[19] = CarPriceTableView.get_dataItems()[currentRowIndex].findElement("RATE_UNIT_NAME").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CAR_PRICE_LIST_MASTER_ID;

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
            CRM.WebApp.webservice.SupplierCarPriceListWebService.InsertUpdateCarPrice(ary);
            CRM.WebApp.webservice.SupplierCarPriceListWebService.GetCarPriceList(0, CarPriceTableView.get_pageSize(), CarPriceTableView.get_sortExpressions().toString(), CarPriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateCarPrice);

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
//    else if (results == "Car_Company") {

//        alert("Invalid Car Company.");
//    }
//    else if (results == "Payment") {

//        alert("Invalid Payment Terms.");
//    }
//    else if (results == "agent") {
//        alert("Invalid Agent Name");
//    }
//    else if (results == "City") {
//        alert("Invalid City Name");
//    }
//    else if (results == "Car") {
//        alert("Invalid Car Name.");
//    }
//    else if (results == "Rate_Unit") {
//        alert("Invalid Car Name.");
//    }
//    else if (results == 0) {

//        alert('Record Save Successfully');
//    }
//    else {

//        alert('This Record All Ready Exist.');
//    }

//}