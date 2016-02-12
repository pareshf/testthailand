var PAYMENT_TERMS_ID = "";
var PaymentCommandName = "";
var PaymentTableView = null;


function radgridpaymenttermmaster_Command(sender, args) {

    args.set_cancel(true);
    pageSize = sender.get_masterTableView().get_pageSize();
    PaymentTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.PaymentTermsMaster.GetPaymentterms(PaymentTableView.get_currentPageIndex() * PaymentTableView.get_pageSize(), PaymentTableView.get_pageSize(), PaymentTableView.get_sortExpressions().toString(), PaymentTableView.get_filterExpressions().toDynamicLinq(), updatePaymentTypeName);
    PaymentCommandName = args.get_commandName;

}
function radgridpaymenttermmaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        PAYMENT_TERMS_ID = args.get_gridDataItem()._dataItem.PAYMENT_TERMS_ID;


    }
    catch (e) { }
}
function updatePaymentVirtualCount(result) { PaymentTableView.set_virtualItemCount(result); }

function updatePaymentTypeName(result) {

    PaymentTableView.set_dataSource(result);
    PaymentTableView.dataBind();
    if (result.length > 0) { PaymentTableView.selectItem(0); }

    if (PaymentCommandName == "Filter" || PaymentCommandName == "Load") { CRM.WebApp.webservice.PaymentTermsMaster.GetPaymentTermsCount(updatePaymentVirtualCount); }
}
