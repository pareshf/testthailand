var GROUP_TYPE_ID = "";
var GroupTypeCommandName = "";
var GroupTypeTableView = null;


function radgridgrouptypemaster_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    GroupTypeTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.GroupType.GetGroupType(GroupTypeTableView.get_currentPageIndex() * GroupTypeTableView.get_pageSize(), GroupTypeTableView.get_pageSize(), GroupTypeTableView.get_sortExpressions().toString(), GroupTypeTableView.get_filterExpressions().toDynamicLinq(), updateGroupTypeName);
    GroupTypeCommandName = args.get_commandName;

}
function radgridgrouptypemaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        GROUP_TYPE_ID = args.get_gridDataItem()._dataItem.GROUP_TYPE_ID;


    }
    catch (e) { }
}
function updateGroupTypeVirtualCount(result) { GroupTypeTableView.set_virtualItemCount(result); }

function updateGroupTypeName(result) {

    GroupTypeTableView.set_dataSource(result);
    GroupTypeTableView.dataBind();
    if (result.length > 0) { GroupTypeTableView.selectItem(0); }

    if (GroupTypeCommandName == "Filter" || GroupTypeCommandName == "Load") { CRM.WebApp.webservice.GroupType.GetGroupTypeCount(updateGroupTypeVirtualCount); }
}
// Save Double Click
function addMyGroupType(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();


    var ary = [];
    ary[1] = GroupTypeTableView.get_dataItems()[currentRowIndex].findElement("GROUP_TYPE").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.GROUP_TYPE_ID;
    for (i = 0; i < 2; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.GroupType.InsertUpdateGroupType(ary);
        CRM.WebApp.webservice.GroupType.GetGroupType(0, GroupTypeTableView.get_pageSize(), GroupTypeTableView.get_sortExpressions().toString(), GroupTypeTableView.get_filterExpressions().toDynamicLinq(), updateGroupTypeName);

        alert('Record Save Successfully');

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}