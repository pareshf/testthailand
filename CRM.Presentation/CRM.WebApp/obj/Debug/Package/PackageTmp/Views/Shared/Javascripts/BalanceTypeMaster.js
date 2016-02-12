var BAL_TYPE_ID = "";
var BalanceCommandName = "";
var BalanceTableView = null;


function radgridbalancetypemaster_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    BalanceTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.BalanceTypeMaster.GetBalanceType(BalanceTableView.get_currentPageIndex() * BalanceTableView.get_pageSize(), BalanceTableView.get_pageSize(), BalanceTableView.get_sortExpressions().toString(), BalanceTableView.get_filterExpressions().toDynamicLinq(), updateBalanceTypeName);
    BalanceCommandName = args.get_commandName;

}
function radgridbalancetypemaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        BAL_TYPE_ID = args.get_gridDataItem()._dataItem.BAL_TYPE_ID;


    }
    catch (e) { }
}
function updateBalanceVirtualCount(result) { BalanceTableView.set_virtualItemCount(result); }

function updateBalanceTypeName(result) {

    BalanceTableView.set_dataSource(result);
    BalanceTableView.dataBind();
    if (result.length > 0) { BalanceTableView.selectItem(0); }

    if (BalanceCommandName == "Filter" || BalanceCommandName == "Load") { CRM.WebApp.webservice.BalanceTypeMaster.GetBalanceTypeCount(updateBalanceVirtualCount); }
}
// Save Double Click
function addMyBalanceType(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];
    ary[1] = BalanceTableView.get_dataItems()[currentRowIndex].findElement("BAL_TYPE_NAME").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.BAL_TYPE_ID;
    for (i = 0; i < 2; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.BalanceTypeMaster.InsertUpdateBalancetype(ary);
        CRM.WebApp.webservice.BalanceTypeMaster.GetBalanceType(0, BalanceTableView.get_pageSize(), BalanceTableView.get_sortExpressions().toString(), BalanceTableView.get_filterExpressions().toDynamicLinq(), updateBalanceTypeName);

        alert('Record Save Successfully');

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}