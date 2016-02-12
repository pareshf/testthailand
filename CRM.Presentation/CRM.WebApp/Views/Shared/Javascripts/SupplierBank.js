var SUPPLIER_BANK_ACCOUNT_ID = "";
var SupplierBankAccountCommand = "";
var SupplierBankAccountTableView = null;
var globalvalue = null;
var sfname = "";


function radgridSupplierBankAccount_Command(sender, args) {
    args.set_cancel(true);
    pageSize = sender.get_masterTableView().get_pageSize();
    SupplierBankAccountTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SupplierBankAccountWebService.GetSupplierBankAcount(SupplierBankAccountTableView.get_currentPageIndex() * SupplierBankAccountTableView.get_pageSize(), SupplierBankAccountTableView.get_pageSize(), SupplierBankAccountTableView.get_sortExpressions().toString(), SupplierBankAccountTableView.get_filterExpressions().toDynamicLinq(), sfname, updateSupplierBankdetail);
    SupplierBankAccountCommand = args.get_commandName;

}
function radgridSupplierBankAccount_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        SUPPLIER_BANK_ACCOUNT_ID = args.get_gridDataItem()._dataItem.SUPPLIER_BANK_ACCOUNT_ID;

    }
    catch (e) { }
}
function SupplierbankVirtualCount(result) { SupplierBankAccountTableView.set_virtualItemCount(result); }

function updateSupplierBankdetail(result) {

    SupplierBankAccountTableView.set_dataSource(result);
    SupplierBankAccountTableView.dataBind();
    if (result.length > 0) { SupplierBankAccountTableView.selectItem(0); }
    if (SupplierBankAccountCommand == "Filter" || SupplierBankAccountCommand == "Load") { CRM.WebApp.webservice.SupplierBankAccountWebService.GetSupplierBankCount(SupplierbankVirtualCount); }
}

// Double Click Save
function addSupplierBankDetail(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[1] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex].findElement("COMPANY_NAME").value;
    ary[2] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex].findElement("ACC_NO").value;
    ary[3] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex].findElement("SUPP_ACC_NAME").value;
    ary[4] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex].findElement("BANK_NAME").value;
    ary[5] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex].findElement("BANK_BRANCH").value;
    ary[6] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex].findElement("BANK_ADDRESS").value;
    ary[7] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex].findElement("SWIFT_CODE").value;
    ary[8] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex].findElement("COMPANY_BANK_NAME").value;
    ary[9] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex].findElement("COMPANY_BANK_BRANCH").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SUPPLIER_BANK_ACCOUNT_ID;
    //                for (i = 0; i < 9; i++) {
    //                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    //                }
    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
    try {

        CRM.WebApp.webservice.SupplierBankAccountWebService.InsertUpdateSupplierBankAccount(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.SupplierBankAccountWebService.GetSupplierBankAcount(0, SupplierBankAccountTableView.get_pageSize(), SupplierBankAccountTableView.get_sortExpressions().toString(), SupplierBankAccountTableView.get_filterExpressions().toDynamicLinq(), sfname, updateSupplierBankdetail);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}

