var SUPPLIER_HOTEL_PRICE_LIST_ID = "";
var SUPPLIER_SR_NO = "";
var HotelPriceCommand = "";
var HotelPriceTableView = null;
var CHAIN_NAME = "";
var scity = "";
var s = "";


function radgridsupplierHotelPrice_Command(sender, args) {
    args.set_cancel(true);
    pageSize = sender.get_masterTableView().get_pageSize();
    HotelPriceTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SupplierHotelPriceListWebService.GetHotelPrice(HotelPriceTableView.get_currentPageIndex() * HotelPriceTableView.get_pageSize(), HotelPriceTableView.get_pageSize(), HotelPriceTableView.get_sortExpressions().toString(), HotelPriceTableView.get_filterExpressions().toDynamicLinq(), CHAIN_NAME, scity, UpdateSupplierHotelPrice);

    HotelPriceCommand = args.get_commandName;

}
function radgridsupplierHotelPrice_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        SUPPLIER_HOTEL_PRICE_LIST_ID = args.get_gridDataItem()._dataItem.SUPPLIER_HOTEL_PRICE_LIST_ID;
        CRM.WebApp.webservice.SupplierHotelPriceListWebService.GetRoomInventory(SUPPLIER_HOTEL_PRICE_LIST_ID, UpdateHotelRoomInventory);

    }
    catch (e) { }
}


function updateHotelPriceVirtualCount(result) { HotelPriceTableView.set_virtualItemCount(result); }

function UpdateSupplierHotelPrice(result) {

    HotelPriceTableView.set_dataSource(result);
    HotelPriceTableView.dataBind();
    if (result.length > 0) { HotelPriceTableView.selectItem(0); }

    if (HotelPriceCommand == "Filter" || HotelPriceCommand == "Load") { CRM.WebApp.webservice.SupplierHotelPriceListWebService.GetHotelPriceListCount(updateHotelPriceVirtualCount); }
}

function HotelPriceList(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[1] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("ROOM_TYPE_NAME").value;

    ary[2] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("SINGLE_ROOM_RATE").value;
    ary[3] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("DOUBLE_ROOM_RATE").value;
    ary[4] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("EXTRA_ADULT_RATE").value;
    //ary[5] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("MARGIN_AMOUNT").value;
    //ary[6] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[7] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    ary[8] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("PAYMENT_TERMS").value;
    ary[9] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_NAME").value;
    //ary[10] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("CUSTOMER_NAME").value;
    ary[11] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("EXTRA_CWB_COST").value;
    ary[12] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("EXTRA_CNB_COST").value;
    ary[13] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("FROM_DATE").value;
    ary[14] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("TO_DATE").value;
    ary[15] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("SURCHARGE").value;
    ary[16] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("IS_DEFAULT").value;
    ary[17] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("SURCHARGE_UNIT").value;
    ary[18] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("TRIPLE_ROOM_RATE").value;
    ary[19] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("STATUS").value;
    //ary[20] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("TO_ISSUE_SERVICE_VOUCHER").value;

    ary[21] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("A_MARGIN_IN_AMOUNT").value;
    ary[22] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("A_PLUS_MARGIN_IN_AMOUNT").value;
    ary[23] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("A_PLUS_PLUS_MARGIN_IN_AMOUNT").value;
    ary[24] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("A_MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[25] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[26] = HotelPriceTableView.get_dataItems()[currentRowIndex].findElement("A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SUPPLIER_HOTEL_PRICE_LIST_ID;

    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
    if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
    if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;
    if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
    if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;
    if (ary[6] == "" || ary[6] == 'null') ary[6] = 0;
    if (ary[18] == "" || ary[18] == 'null') ary[18] = 0;

    //    if (ary[5] != 0 && ary[6] != 0) {

    //        alert("You Cant Enter Both Margin Or Margin in[%]");

    //    }
    //    else if (ary[5] == 0 && ary[6] == 0) {
    //        alert("Enter Either Margin Or Margin in[%]");
    //    }
    //    else {
    if (ary[2] != 0 && ary[3] != 0) {

        
        if (ary[4] == 0 && ary[18] == 0 || ary[4] != 0 && ary[18] == 0 || ary[4] == 0 && ary[18] != 0 ) {

            try {

                CRM.WebApp.webservice.SupplierHotelPriceListWebService.InsertUpdateHotelPrice(ary);
                CRM.WebApp.webservice.SupplierHotelPriceListWebService.GetHotelPrice(0, HotelPriceTableView.get_pageSize(), HotelPriceTableView.get_sortExpressions().toString(), HotelPriceTableView.get_filterExpressions().toDynamicLinq(), CHAIN_NAME, scity, UpdateSupplierHotelPrice);
                alert('Record Save Successfully');

            }
            catch (e) {
                alert('Wrong Data Inserted');
            }
        }
        else if (ary[4] != 0 && ary[18] != 0) {

            alert("Enter Either Extra Adult Rate Or Triple Room Rate");
        }
        //        else {

        //           // alert("You Cant Enter Both Extra Adult Rate Or Triple Room Rate");
        //        }
    }
    else {
        alert('Enter Single Room Rate And Double Room Rate.');
    }
}
//}

//function OnCallBack(results, userContext, sender) {
//   
//    if (results == 0) {
//        alert('Record Save Successfully');
//    }
//    else {
//        alert('This Record All Ready Exist.');

//    }

//}