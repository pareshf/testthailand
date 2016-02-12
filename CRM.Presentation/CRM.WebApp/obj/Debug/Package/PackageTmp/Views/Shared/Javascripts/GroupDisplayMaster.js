var GROUP_DISPLAY_ID = "";
var groupdisplayCommandName = "";
var groupdisplayTableView = null;


function radgridgroupdisplaymaster_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    groupdisplayTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.GroupDisplayMaster.GetGroupDisplay(groupdisplayTableView.get_currentPageIndex() * groupdisplayTableView.get_pageSize(), groupdisplayTableView.get_pageSize(), groupdisplayTableView.get_sortExpressions().toString(), groupdisplayTableView.get_filterExpressions().toDynamicLinq(), updategroupdisplayName);
    groupdisplayCommandName = args.get_commandName;

}
function radgridgroupdisplaymaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        GROUP_DISPLAY_ID = args.get_gridDataItem()._dataItem.GROUP_DISPLAY_ID;


    }
    catch (e) { }
}
function updategroupdisplayVirtualCount(result) { groupdisplayTableView.set_virtualItemCount(result); }

function updategroupdisplayName(result) {

    groupdisplayTableView.set_dataSource(result);
    groupdisplayTableView.dataBind();
    if (result.length > 0) { groupdisplayTableView.selectItem(0); }

    if (groupdisplayCommandName == "Filter" || groupdisplayCommandName == "Load") { CRM.WebApp.webservice.GroupDisplayMaster.GetGroupDisplayCount(updategroupdisplayVirtualCount); }
}
// Save Double Click
function addMyGroupDisplay(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];
    ary[1] = groupdisplayTableView.get_dataItems()[currentRowIndex].findElement("GROUP_DISPLAY_NAME").value;
    ary[2] = groupdisplayTableView.get_dataItems()[currentRowIndex].findElement("GROUP_DISPLAY_DESCRIPTION").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.GROUP_DISPLAY_ID;
    for (i = 0; i < 3; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.GroupDisplayMaster.InsertUpdateGroupDisplay(ary);
        CRM.WebApp.webservice.GroupDisplayMaster.GetGroupDisplay(0, groupdisplayTableView.get_pageSize(), groupdisplayTableView.get_sortExpressions().toString(), groupdisplayTableView.get_filterExpressions().toDynamicLinq(), updategroupdisplayName);

        alert('Record Save Successfully');

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}