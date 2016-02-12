var MoneyAgentMasterTabelView = null;
var MoneyAgentMasterCommandName = "";
var BOOKING_ID = "";
var PAYMENT_SR_NO = "";
var globalvalue = "";
var TOUR_ID = "";



function radgridmoneytransferagent_Command(sender, args) {
    args.set_cancel(true);
    CRM.WebApp.webservice.MoneyTransferAgent.GetMoneyAgentDetails(PAYMENT_SR_NO, updateMoneyGrid);
    
    MoneyAgentMasterCommandName = args.get_commandName();
}
function radgridmoneytransferagent_RowSelected(sender, args) {
    currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
    BOOKING_ID = args.get_gridDataItem()._dataItem.BOOKING_ID;
}
function updateMoneyGrid(result) {

    MoneyAgentMasterTabelView.set_dataSource(result);
    MoneyAgentMasterTabelView.dataBind();
    if (result.length > 0) { MoneyAgentMasterTabelView.selectItem(0); }
    if (MoneyAgentMasterCommandName == "Filter" || MoneyAgentMasterCommandName == "Load") { CRM.WebApp.webservice.MoneyTransferAgent.GetMONEYCount(updateVirtualItemCount); }
}
function updateVirtualItemCount(result) {
    MoneyAgentMasterTabelView.set_virtualItemCount(result);
}
function MoneyTransferRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var paymentary = [];
    paymentary[15] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SR_NO;
    paymentary[14] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.BOOKING_ID;
    paymentary[13] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.PAYMENT_SR_NO;
    paymentary[1] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("PAYMENT_CURR_CODE").value;
    paymentary[2] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("PAYMENT_MODE_NAME").value;
    paymentary[3] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("REC_CHEQUE_DD_NO").value;
    paymentary[4] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("AMOUNT").value;
    paymentary[16] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("SERVICE_CHARGE").value;
    paymentary[5] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("BANK_NAME").value;
    paymentary[6] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("BRANCH_NAME").value;
    paymentary[7] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("RECEIPT_NO").value;
    paymentary[8] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("RECEIPT_DATE").value;
    paymentary[9] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("RECEIVED_BY").value;
    paymentary[10] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("FOREIGN_CURRENCY_AGENT_NAME").value;
    paymentary[12] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("CONVERSION_RATE").value;
    paymentary[17] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("Branch").value;
    paymentary[18] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("INR_AMOUNT").value;
    paymentary[19] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("FAM").value;
    paymentary[20] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("Signature").value;
    paymentary[22] = MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("STATUS_NAME").value;

    for (i = 0; i < 21; i++) {
        if (paymentary[i] == "" || paymentary[i] == 'null') paymentary[i] = 0;

    }
    if (paymentary[20] == signature) {
        try {
            CRM.WebApp.webservice.MoneyTransferAgent.InsertUpdateBookingPaymentDetails1(paymentary);
            alert('Record Save Successfully');
            CRM.WebApp.webservice.MoneyTransferAgent.GetMoneyAgentDetails(PAYMENT_SR_NO, updateMoneyGrid);


        }
        catch (e) {
            alert('Wrong Data Inserted');
        }
    }
    else {

        alert('Enter Signature Again.')
    }
    if (paymentary[20] != '') {

        MoneyAgentMasterTabelView.get_dataItems()[currentRowIndex].findElement("Signature").value='';
    }

}