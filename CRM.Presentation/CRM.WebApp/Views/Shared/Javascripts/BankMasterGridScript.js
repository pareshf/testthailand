var BANK_ID = "";
var BankTableView = null;
var BankCommandName = "";

function radgridBank_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    BankTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.BankMasterWebService.GetBankName(BankTableView.get_currentPageIndex() * BankTableView.get_pageSize(), BankTableView.get_pageSize(), BankTableView.get_sortExpressions().toString(), BankTableView.get_filterExpressions().toDynamicLinq(), updateBankGrid);
    BankCommandName = args.get_commandName;

}
function radgridBank_RowSelected(sender, args) {

    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        BANK_ID = args.get_gridDataItem()._dataItem.BANK_ID;

    }
    catch (e) { }
}
function updateBankVirtualItemCount(result) {

    BankTableView.set_virtualItemCount(result);

}
function updateBankGrid(result) {

    BankTableView.set_dataSource(result);
    BankTableView.dataBind();
    if (result.length > 0) { BankTableView.selectItem(0); }

    if (BankCommandName == "Filter" || BankCommandName == "Load") { CRM.WebApp.webservice.BankMasterWebService.BankNameCount(updateBankVirtualItemCount); }

}

// save Double Click
function addMyBank(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[0] = BankTableView.get_dataItems()[currentRowIndex].findElement("BANK_NAME").value;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.BANK_ID;
    for (i = 0; i < 2; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.BankMasterWebService.InsertUpdateBankName(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.BankMasterWebService.GetBankName(0, BankTableView.get_pageSize(), BankTableView.get_sortExpressions().toString(), BankTableView.get_filterExpressions().toDynamicLinq(), updateBankGrid);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}