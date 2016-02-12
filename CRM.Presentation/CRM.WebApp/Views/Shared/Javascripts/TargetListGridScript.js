var TARGETLIST_ID = "";
var TargetListCommand = "";
var TargetListTableView = null;


function radgridTargetList_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    TargetListTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.TargetListMasterWebService.GetTargetList(TargetListTableView.get_currentPageIndex() * TargetListTableView.get_pageSize(), TargetListTableView.get_pageSize(), TargetListTableView.get_sortExpressions().toString(), TargetListTableView.get_filterExpressions().toDynamicLinq(), updateTargetList);
    TargetListCommand = args.get_commandName;

}
function radgridTargetList_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        TARGETLIST_ID = args.get_gridDataItem()._dataItem.TARGETLIST_ID;
    }
    catch (e) { }
}
function updateTargetListVirtualCount(result) { TargetListTableView.set_virtualItemCount(result); }

function updateTargetList(result) {

    TargetListTableView.set_dataSource(result);
    TargetListTableView.dataBind();
    if (result.length > 0) { TargetListTableView.selectItem(0); }

    if (TargetListCommand == "Filter" || TargetListCommand == "Load") { CRM.WebApp.webservice.TargetListMasterWebService.GetTargetTypeCount(updateTargetListVirtualCount); }
}
/* Double Row Click Save*/
function TargetRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();
    var ary = [];

    ary[0] = TargetListTableView.get_dataItems()[currentRowIndex].findElement("TARGETLIST_NAME").value;
    ary[2] = TargetListTableView.get_dataItems()[currentRowIndex].findElement("SOURCE").value;
    ary[3] = TargetListTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    ary[4] = TargetListTableView.get_dataItems()[currentRowIndex].findElement("COST").value;
    ary[5] = TargetListTableView.get_dataItems()[currentRowIndex].findElement("DESCRIPTION").value;
    ary[6] = TargetListTableView.get_dataItems()[currentRowIndex].findElement("EMPLOYEE_NAME").value;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.TARGETLIST_ID;
    for (i = 0; i < 7; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.TargetListMasterWebService.InsertUpdateTargetList(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.TargetListMasterWebService.GetTargetList(TargetListTableView.get_currentPageIndex() * TargetListTableView.get_pageSize(), TargetListTableView.get_pageSize(), TargetListTableView.get_sortExpressions().toString(), TargetListTableView.get_filterExpressions().toDynamicLinq(), updateTargetList);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}
