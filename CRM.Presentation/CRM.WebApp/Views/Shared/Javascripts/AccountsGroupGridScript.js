var ACCOUNT_GROUP_ID = "";
var GroupTableView = null;
var GroupCommandName = "";
var company = "";

function radgridaccountgroup_Command(sender, args) {

    args.set_cancel(true);
    pageSize = sender.get_masterTableView().get_pageSize();
    GroupTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.AccountsGroupWebService.GetAccountGroup(GroupTableView.get_currentPageIndex() * GroupTableView.get_pageSize(), GroupTableView.get_pageSize(), GroupTableView.get_sortExpressions().toString(), GroupTableView.get_filterExpressions().toDynamicLinq(), updateAccountsGroupGrid);
    GroupCommandName = args.get_commandName;

}
function radgridaccountgroup_RowSelected(sender, args) {

    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        ACCOUNT_GROUP_ID = args.get_gridDataItem()._dataItem.ACCOUNT_GROUP_ID;

    }
    catch (e) { }
}
function updateGroupVirtualItemCount(result) {

    GroupTableView.set_virtualItemCount(result);

}
function updateAccountsGroupGrid(result) {

    GroupTableView.set_dataSource(result);
    GroupTableView.dataBind();
    if (result.length > 0) { GroupTableView.selectItem(0); }

    if (GroupCommandName == "Filter" || GroupCommandName == "Load") { CRM.WebApp.webservice.AccountsGroupWebService.AccountGroupCount(updateGroupVirtualItemCount); }

}

function AccountGroup(sender, eventArgs) {
    
    currentRowIndex = eventArgs.get_itemIndexHierarchical();
    
    var ary = [];

    ary[1] = GroupTableView.get_dataItems()[currentRowIndex].findElement("GROUP_CODE").value;
    ary[2] = GroupTableView.get_dataItems()[currentRowIndex].findElement("GROUP_NAME").value;
    ary[3] = GroupTableView.get_dataItems()[currentRowIndex].findElement("GROUP_TYPE").value;
    ary[4] = GroupTableView.get_dataItems()[currentRowIndex].findElement("GROUP_DISPLAY_NAME").value;
    ary[5] = GroupTableView.get_dataItems()[currentRowIndex].findElement("GROUP_ORDER").value;
    ary[6] = GroupTableView.get_dataItems()[currentRowIndex].findElement("GROUP_CODE_UNDER").value;
    //ary[7] = GroupTableView.get_dataItems()[currentRowIndex].findElement("COMPANY_NAME").value;
    ary[7] = company;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.ACCOUNT_GROUP_ID;
    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    try {
        CRM.WebApp.webservice.AccountsGroupWebService.InsertUpdateAccountsGroup(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.AccountsGroupWebService.GetAccountGroup(0, GroupTableView.get_pageSize(), GroupTableView.get_sortExpressions().toString(), GroupTableView.get_filterExpressions().toDynamicLinq(), updateAccountsGroupGrid);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}