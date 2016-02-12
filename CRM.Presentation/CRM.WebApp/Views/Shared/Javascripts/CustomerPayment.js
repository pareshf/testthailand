var PAYMENT_SRNO = "";
var PaymentTableView = null;
var PaymentCommand = "";
var globalvalue = "";
var CustomerTableView = null;
var CustomerCommand = "";
var TOUR_ID = "";
var MTA = 0;
var INR_AMOUNT = 0;
var ccode = 0;
var BOOKING_ID = "";

function radgridbookingpaymentdetail_Command(sender, args) {

    //    pageSize = sender.get_masterTableView().get_pageSize();
    //    PaymentTableView.set_pageSize(pageSize);
    //    CRM.WebApp.webservice.CustomerBookingPaymentWebService.GetPaymentDetail(PaymentTableView.get_currentPageIndex() * PaymentTableView.get_pageSize(), PaymentTableView.get_pageSize(), PaymentTableView.get_sortExpressions().toString(), PaymentTableView.get_filterExpressions().toDynamicLinq(), updatepayment);
    //    PaymentCommand = args.get_commandName;

}
function radgridcustomer_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    CustomerTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CustomerBookingPaymentWebService.GetCustomerDetail(TOUR_ID, updatecustomer);
    CustomerCommand = args.get_commandName;

}
function radgridcustomer_RowSelected(sender, args) {

    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        BOOKING_ID = args.get_gridDataItem()._dataItem.BOOKING_ID;
        loadpaymentdetail();

    }
    catch (e) { }

}
function radgridbookingpaymentdetail_RowSelected(sender, args) {

    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        PAYMENT_SRNO = args.get_gridDataItem()._dataItem.PAYMENT_SRNO;
        MTA = args.get_gridDataItem()._dataItem.FOREIGN_CURRENCY_AGENT_NAME;
        INR_AMOUNT = args.get_gridDataItem()._dataItem.INR_AMT;
        ccode = args.get_gridDataItem()._dataItem.PAYMENT_CURRENCY_CODE;

    }
    catch (e) { }

}
function updatePaymentVirtualItemCount(result) {

    PaymentTableView.set_virtualItemCount(result);

}
function updateCustomerPaymentVirtualItemCount(result) {

    CustomerTableView.set_virtualItemCount(result);

}
function updatecustomer(result) {

    CustomerTableView.set_dataSource(result);
    CustomerTableView.dataBind();
    if (result.length > 0) { CustomerTableView.selectItem(0); }

    if (CustomerCommand == "Filter" || CustomerCommand == "Load") { CRM.WebApp.webservice.CustomerBookingPaymentWebService.CustomerCount(updateCustomerPaymentVirtualItemCount); }
}
function updatepayment(result) {

    PaymentTableView.set_dataSource(result);
    PaymentTableView.dataBind();
    if (result.length > 0) { PaymentTableView.selectItem(0); }

    //if (PaymentCommand == "Filter" || PaymentCommand == "Load") { CRM.WebApp.webservice.CustomerBookingPaymentWebService.PaymentCount(updatePaymentVirtualItemCount); }

}
function loadpaymentdetail() {

    CRM.WebApp.webservice.CustomerBookingPaymentWebService.GetPaymentDetail(BOOKING_ID, updatepayment);

}
function CustomerPayementRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();


    var paymentary = [];
   
    paymentary[0] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("PAYMENT_DATE").value;
    paymentary[1] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("PAYMENT_CURRENCY_CODE").value;
    paymentary[2] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("PAYMENT_MODE_NAME").value;
    paymentary[3] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("REC_CHEQUE_DD_NO").value;
    paymentary[4] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("BANK_NAME").value;
    paymentary[5] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("BRANCH_NAME").value;
    paymentary[6] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("RECEIPT_NO").value;
    paymentary[7] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("RECEIPT_DATE").value;
    paymentary[8] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("RECEIVED_BY").value;
    paymentary[9] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("FOREIGN_CURRENCY_AGENT_NAME").value;
    paymentary[10] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("IN_THE_NAME_OF").value;
    paymentary[11] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.PAYMENT_SRNO;
    paymentary[12] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("AMOUNT").value;
    paymentary[13] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("BILL_NUMBER").value;
    paymentary[14] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("TAX").value;
    paymentary[15] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("GST").value;
    paymentary[16] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("INR_AMT").value;
    paymentary[17] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("TOKEN_AMOUNT").value;
    paymentary[18] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("Signature").value;
    paymentary[21] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("STATUS_NAME").value;
    paymentary[20] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.BOOKING_ID;


    for (i = 0; i < 21; i++) {
        if (paymentary[i] == "" || paymentary[i] == 'null') paymentary[i] = 0;

    }
    if (paymentary[16] != 0 && paymentary[12] != 0) {

        alert("You Cant Enter Both Received Amount & INR Forex Amount .");

    }
    else {

        if (paymentary[18] == signature)
            try {
                CRM.WebApp.webservice.CustomerBookingPaymentWebService.InsertUpdateBookingPaymentDetail(paymentary);
                alert('Record Save Successfully');
                CRM.WebApp.webservice.CustomerBookingPaymentWebService.GetPaymentDetail(BOOKING_ID, updatepayment);



            }
            catch (e) {
                alert('Wrong Data Inserted');
            }
        else {

            alert('Enter Correct Signature Password.')
        }

    }
    if (paymentary[18] != '') {

        paymentary[18] = PaymentTableView.get_dataItems()[currentRowIndex].findElement("Signature").value = '';
    }
}