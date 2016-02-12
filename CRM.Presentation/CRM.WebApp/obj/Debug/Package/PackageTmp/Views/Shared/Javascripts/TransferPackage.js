var TRANSFER_PACKAGE_PRICE_ID = "";
var TransferpackageCommand = "";
var TransferpackageTableView = null;
var scity = "";
var sfname = "";
var slname = "";
var userid = "";
var TRANSFER_PACKAGE_FROM_TO_DETAIL_ID = '';
var TransferdetailTableView = null;
var TransferdetailCommand = "";
var FIT_PACKAGE_NAME = "";
var d = "";
var FROM_PLACE = "";
var TO_PLACE = "";


function radgridTransferPackagePriceList_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    TransferpackageTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.TransferPackageWebService.GetTransferPackage(TransferpackageTableView.get_currentPageIndex() * TransferpackageTableView.get_pageSize(), TransferpackageTableView.get_pageSize(), TransferpackageTableView.get_sortExpressions().toString(), TransferpackageTableView.get_filterExpressions().toDynamicLinq(), scity, sfname, slname, UpdateTransferPackage);
    TransferpackageCommand = args.get_commandName;

}
function radgridtransferdetail_Command(sender, args) {

}
function radgridtransferdetail_RowSelected(sender, args) {
    currentRowIndex = args.get_gridDataItem().get_element().rowIndex;

    TRANSFER_PACKAGE_FROM_TO_DETAIL_ID = args.get_gridDataItem()._dataItem.TRANSFER_PACKAGE_FROM_TO_DETAIL_ID;


}
function radgridTransferPackagePriceList_RowSelected(sender, args) {
    
    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        TRANSFER_PACKAGE_PRICE_ID = args.get_gridDataItem()._dataItem.TRANSFER_PACKAGE_PRICE_ID;

        CRM.WebApp.webservice.TransferPackageWebService.GetTransferPackagedetail(TRANSFER_PACKAGE_PRICE_ID,UpdateTransferdetail);

    }
    catch (e) { }
}

function updateTransferPackageVirtualCount(result) { TransferpackageTableView.set_virtualItemCount(result); }

function updateTransferdetailVirtualCount(result) { TransferdetailTableView.set_virtualItemCount(result); }

function UpdateTransferPackage(result) {

    TransferpackageTableView.set_dataSource(result);
    TransferpackageTableView.dataBind();
    if (result.length > 0) { TransferpackageTableView.selectItem(0); }

    if (result.length > 0) {
        TransferpackageTableView.selectItem(0);
        TRANSFER_PACKAGE_PRICE_ID = result[0]["TRANSFER_PACKAGE_PRICE_ID"];
    }
    else {
        TRANSFER_PACKAGE_PRICE_ID = "";

    }

    if (TransferpackageCommand == "Filter" || TransferpackageCommand == "Load") { CRM.WebApp.webservice.TransferPackageWebService.GetTransferPackageCount(updateTransferPackageVirtualCount); }

}
function UpdateTransferdetail(result) {

    TransferdetailTableView.set_dataSource(result);
    TransferdetailTableView.dataBind();
    if (result.length > 0) { TransferdetailTableView.selectItem(0); }

    if (result.length > 0) {
        TransferdetailTableView.selectItem(0);
        TRANSFER_PACKAGE_FROM_TO_DETAIL_ID = result[0]["TRANSFER_PACKAGE_FROM_TO_DETAIL_ID"];
    }
    else {
        TRANSFER_PACKAGE_FROM_TO_DETAIL_ID = "";

    }

    if (TransferdetailCommand == "Filter" || TransferdetailCommand == "Load") { CRM.WebApp.webservice.TransferPackageWebService.GetTransferdetailCount(updateTransferdetailVirtualCount); }

}
function TransferPackagePriceList(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];


    ary[1] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("FROM_PLACE").value;

    ary[2] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("EFFECTIVE_FROM_DATE").value;
    ary[3] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("EFFECTIVE_TO_DATE").value;
    //ary[4] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("MARGIN_AMOUNT").value;
    //ary[5] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[6] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    ary[7] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("PAYMENT_TERMS").value;
    //ary[8] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("CUSTOMER_NAME").value;
    ary[9] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("FIT_PACKAGE_NAME").value;
    ary[10] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_NAME").value;
    ary[11] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("STATUS").value;
    //ary[12] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("TO_ISSUE_SERVICE_VOUCHER").value;
    ary[13] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("A_MARGIN_IN_AMOUNT").value;
    ary[14] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("A_PLUS_MARGIN_IN_AMOUNT").value;
    ary[15] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("A_PLUS_PLUS_MARGIN_IN_AMOUNT").value;
    ary[16] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("A_MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[17] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE").value;
    ary[18] = TransferpackageTableView.get_dataItems()[currentRowIndex].findElement("A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.TRANSFER_PACKAGE_PRICE_ID;

    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
    if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
    if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;


    //    if (ary[4] != 0 && ary[5] != 0) {

    //        alert("You Cant Enter Both Margin Or Margin in[%]");

    //    }
    //    else if (ary[4] == 0 && ary[5] == 0) {
    //        alert("Enter Either Margin Or Margin in[%]");
    //    }
    //    else {

    try {
        CRM.WebApp.webservice.TransferPackageWebService.InsertUpdateTransferPackage(ary);
        CRM.WebApp.webservice.TransferPackageWebService.GetTransferPackage(0, TransferpackageTableView.get_pageSize(), TransferpackageTableView.get_sortExpressions().toString(), TransferpackageTableView.get_filterExpressions().toDynamicLinq(), scity, sfname, slname, UpdateTransferPackage);

        alert('Record Save Successfully');

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

    // }
}
//function OnCallBack(results, userContext, sender) {

//      if (results == 0) {
//            alert('Record Save Successfully');
//       }
//       else {
//              alert('This Record All Ready Exist.');

//        }

//}

/*child Double Click*/
function TransferPackageDetail(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();


    var ary = [];

    ary[1] = TransferdetailTableView.get_dataItems()[currentRowIndex].findElement("FROM_PLACE").value;
    ary[2] = TransferdetailTableView.get_dataItems()[currentRowIndex].findElement("TO_PLACE").value;
    ary[3] = TransferdetailTableView.get_dataItems()[currentRowIndex].findElement("SIGHT_SEEING_PACKAGE_NAME").value;
    ary[4] = TransferdetailTableView.get_dataItems()[currentRowIndex].findElement("FLAG").value;
    ary[5] = TransferdetailTableView.get_dataItems()[currentRowIndex].findElement("ADULT_SIC_RATE").value;
    ary[6] = TransferdetailTableView.get_dataItems()[currentRowIndex].findElement("CHILD_SIC_RATE").value;
    ary[7] = TransferdetailTableView.get_dataItems()[currentRowIndex].findElement("ADULT_PVT_RATE").value;
    ary[8] = TransferdetailTableView.get_dataItems()[currentRowIndex].findElement("CHILD_PVT_RATE").value;
    ary[9] = TransferdetailTableView.get_dataItems()[currentRowIndex].findElement("SICRATE_PER_PERSON").value;
    ary[10] = TransferdetailTableView.get_dataItems()[currentRowIndex].findElement("PVTRATE_PER_PERSON").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.TRANSFER_PACKAGE_FROM_TO_DETAIL_ID;
    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
    try {
        CRM.WebApp.webservice.TransferPackageWebService.InsertUpdateTransferPackagedetail(ary, d, OnCallBack);
        CRM.WebApp.webservice.TransferPackageWebService.GetTransferPackagedetail(TRANSFER_PACKAGE_PRICE_ID, UpdateTransferdetail);

        //alert('Record Save Successfully');

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}