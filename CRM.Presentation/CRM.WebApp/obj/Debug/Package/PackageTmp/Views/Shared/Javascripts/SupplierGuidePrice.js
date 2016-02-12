var GUIDE_PRICE_LIST_ID = "";
var SUPPLIER_SR_NO = "";
var GuidePriceCommand = "";
var GuidePriceTableView = null;
var scity = "";
var sfname = "";
var s = "";


function radgridsupplierGuidePrice_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    GuidePriceTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SupplierGuidePriceListWebService.GetGuidePrice(GuidePriceTableView.get_currentPageIndex() * GuidePriceTableView.get_pageSize(), GuidePriceTableView.get_pageSize(), GuidePriceTableView.get_sortExpressions().toString(), GuidePriceTableView.get_filterExpressions().toDynamicLinq(),sfname,scity,updateGuidePrice);
    GuidePriceCommand = args.get_commandName;

}
function radgridsupplierGuidePrice_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        GUIDE_PRICE_LIST_ID = args.get_gridDataItem()._dataItem.GUIDE_PRICE_LIST_ID;


    }
    catch (e) { }
}
function updateGuidePriceVirtualCount(result) { GuidePriceTableView.set_virtualItemCount(result); }

function updateGuidePrice(result) {

    GuidePriceTableView.set_dataSource(result);
    GuidePriceTableView.dataBind();
    if (result.length > 0) { GuidePriceTableView.selectItem(0); }

    if (GuidePriceCommand == "Filter" || GuidePriceCommand == "Load") { CRM.WebApp.webservice.SupplierGuidePriceListWebService.GetGuidePriceCount(updateGuidePriceVirtualCount); }
}

function GuidePriceList(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    //ary[1] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("EFFECTIVE_FROM_DATE").value;
    //ary[2] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("EFFECTIVE_TO_DATE").value;
    //ary[3] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("SITE_NAME").value;
    ary[4] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("CUSTOMER_NAME").value;
    ary[5] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_NAME").value;
    //ary[6] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("GIT_RATE").value;
    ary[7] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("FIT_RATE").value;
    //ary[8] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("FIT_DISCOUNT").value;
    //ary[9] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("GIT_DISCOUNT").value;
    ary[10] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("MARGIN_AMOUNT").value;
    ary[11] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[12] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    ary[13] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("PAYMENT_TERMS").value;
    ary[14] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("CITY_NAME").value;
  //  ary[15] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("GIT_MARGIN_AMOUNT").value;
 //   ary[16] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("GIT_MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[18] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("FROM_DATE").value;
    ary[19] = GuidePriceTableView.get_dataItems()[currentRowIndex].findElement("TO_DATE").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.GUIDE_PRICE_LIST_ID;

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
             CRM.WebApp.webservice.SupplierGuidePriceListWebService.InsertUpdateGuidePrice(ary, s, OnCallBack);
             CRM.WebApp.webservice.SupplierGuidePriceListWebService.GetGuidePrice(GuidePriceTableView.get_currentPageIndex() * GuidePriceTableView.get_pageSize(), GuidePriceTableView.get_pageSize(), GuidePriceTableView.get_sortExpressions().toString(), GuidePriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateGuidePrice);

             alert('Record Save Successfully');

         }
         catch (e) {
             alert('Wrong Data Inserted');
         }
     }
 }
 function OnCallBack(results, userContext, sender) {


     if (results == "currency") {

         alert("Invalid Currency Name.");
     }
     else if (results == "Guide_Company") {

         alert("Invalid GuideC ompany Name.");
     }
     else if (results == "Payment") {

         alert("Invalid Payment Terms.");
     }
     else if (results == "agent") {
         alert("Invalid Agent Name");
     }
     else if (results == "City") {
         alert("Invalid City Name.")
     }
     else if (results == 0) {

         alert('Record Save Successfully');
     }
     else {

         alert('This Record All Ready Exist.');
     }

 }