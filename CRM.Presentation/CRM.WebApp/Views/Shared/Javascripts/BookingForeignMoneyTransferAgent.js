var CUST_ID = "";
var CRUISE_COMPANY_ID = "";
var paymentmasterCommandName = "";
var paymentmasterTableView = null;
var FA_SR_NO = null;
var globalvalue = null;
var TOUR_ID = null;
var paymentagentcommandname = "";
var paymentagenttableview = null;
var paymentreporttableview = null;
var paymentreportcommandname = "";

function radgridforeignagent_Command(sender, args) {
    args.set_cancel(true);
    CRM.WebApp.webservice.BookingForeignWebService.GetForeign(TOUR_ID, updateForeignGrid);
    paymentmasterCommandName = args.get_commandName();
    
    }
function radgridforeignagent_RowSelected(sender, args) {
    currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
    FA_SR_NO = args.get_gridDataItem()._dataItem.FA_SR_NO;
}
function radgridforeignagentreport_Command(sender, args) {
}
function radgridforeignagentreport_RowSelected(sender, args) {
}
function updateForeignGrid(result) {

    paymentmasterTableView.set_dataSource(result);
    paymentmasterTableView.dataBind();
    if (result.length > 0) { paymentmasterTableView.selectItem(0); }
    if (paymentmasterCommandName == "Filter" || paymentmasterCommandName == "Load") { CRM.WebApp.webservice.BookingForeignWebService.GetCount(updateVirtualItemCount); }
}

function updateForeignreportGrid(result) {

    paymentreporttableview.set_dataSource(result);
    paymentreporttableview.dataBind();
    if (result.length > 0) { paymentreporttableview.selectItem(0); }
    if (paymentreportcommandname == "Filter" || paymentreportcommandname == "Load") { CRM.WebApp.webservice.BookingForeignWebService.GetCountreport(updateVirtualItemCountReport); }
}

function updateForeignAgentGrid(result) {

    paymentagenttableview.set_dataSource(result);
    paymentagenttableview.dataBind();
    if (result.length > 0) { paymentmasterTableView.selectItem(0); }
    if (paymentagentcommandname == "Filter" || paymentagentcommandname == "Load") { CRM.WebApp.webservice.BookingForeignWebService.GetCountAgent(updateVirtualItemCountAgent); }
}

function updateVirtualItemCount(result) {
    paymentmasterTableView.set_virtualItemCount(result);
}

function updateVirtualItemCountReport(result) {
    paymentreporttableview.set_virtualItemCount(result);
}

function updateVirtualItemCountAgent(result) {
    paymentagenttableview.set_virtualItemCount(result);
}
function AgentRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.FA_SR_NO;
    //ary[2] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("BOOKING_ID").value;
    ary[2] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.BOOKING_ID;
    ary[3] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("FOREIGN_CURR_AGENT_ID").value;
    ary[4] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("PAYMENT_CURR_CODE").value;
    ary[5] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("PAYMENT_MODE_NAME").value;
    ary[6] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("REC_CHEQUE_DD_NO").value;
    ary[7] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("AMOUNT").value;
    ary[8] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("BANK_NAME").value;
    ary[9] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("BANK_BRANCH_NAME").value;
    ary[10] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("RECEIPT_NO").value;
    ary[11] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("RECEIPT_DATE").value;
    ary[12] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("USER_NAME").value;
    ary[13] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("SERVICE_CHARGE").value;
    ary[14] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("COMPANY_NAME").value;
    //ary[15] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("TOUR_ID").value;
    ary[15] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.TOUR_ID;
    ary[1] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("AGENT_ID").value;
    ary[16] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("Signature").value;
    ary[17] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("STATUS_NAME").value;
    for (i = 0; i < 17; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    if (ary[16] == signature) {
        try {
            CRM.WebApp.webservice.BookingForeignWebService.InsertUpdateForeignAgent(ary);
            CRM.WebApp.webservice.BookingForeignWebService.GetForeign(TOUR_ID, updateForeignGrid);
            alert('Record Save Successfully');
        }
        catch (e) {
            alert('Wrong Data Inserted');
        }
    }
    else {

        alert('Enter Correct Signature Password.');
    }
    if (ary[16] != '') {

        ary[16] = paymentmasterTableView.get_dataItems()[currentRowIndex].findElement("Signature").value = '';
    }
}