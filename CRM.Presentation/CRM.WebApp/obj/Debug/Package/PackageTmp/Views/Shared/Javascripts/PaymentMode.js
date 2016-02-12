var PAYMENT_MODE_ID = "";
var PaymentModeTypeCommand = "";
var PaymentModeTypeTableView = null;


function radgridPaymentMode_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    PaymentModeTypeTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.PaymentMode.GetPaymentModeType(PaymentModeTypeTableView.get_currentPageIndex() * PaymentModeTypeTableView.get_pageSize(), PaymentModeTypeTableView.get_pageSize(), PaymentModeTypeTableView.get_sortExpressions().toString(), PaymentModeTypeTableView.get_filterExpressions().toDynamicLinq(), updatePaymentModeTypeName);
    PaymentModeTypeCommand = args.get_commandName;

}
function radgridPaymentMode_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        PAYMENT_MODE_ID = args.get_gridDataItem()._dataItem.PAYMENT_MODE_ID;


    }
    catch (e) { }
}
function PaymentModetypeVirtualCount(result) { PaymentModeTypeTableView.set_virtualItemCount(result); }

function updatePaymentModeTypeName(result) {

    PaymentModeTypeTableView.set_dataSource(result);
    PaymentModeTypeTableView.dataBind();
    if (result.length > 0) { PaymentModeTypeTableView.selectItem(0); }

    if (PaymentModeTypeCommand == "Filter" || PaymentModeTypeCommand == "Load") { CRM.WebApp.webservice.PaymentMode.GetPMODEcount(PaymentModetypeVirtualCount); }
}

// Save On Double Click

function addMyPaymentMode(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[1] = PaymentModeTypeTableView.get_dataItems()[currentRowIndex].findElement("PAYMENT_MODE_NAME").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.PAYMENT_MODE_ID;
    for (i = 0; i < 2; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.PaymentMode.InsertUpdatePayment(ary);
        CRM.WebApp.webservice.PaymentMode.GetPaymentModeType(0, PaymentModeTypeTableView.get_pageSize(), PaymentModeTypeTableView.get_sortExpressions().toString(), PaymentModeTypeTableView.get_filterExpressions().toDynamicLinq(), updatePaymentModeTypeName);

        alert('Record Save Successfully');

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}