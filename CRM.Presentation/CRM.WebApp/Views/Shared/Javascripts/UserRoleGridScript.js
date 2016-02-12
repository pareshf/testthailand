var ROLE_ID = "";
var RoleMasterCommandName = "";
var RoleMasterTableView = null;


function radgridsystemrolemaster_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    RoleMasterTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SystemRoleMasterWebService.GetUserRole(RoleMasterTableView.get_currentPageIndex() * RoleMasterTableView.get_pageSize(), RoleMasterTableView.get_pageSize(), RoleMasterTableView.get_sortExpressions().toString(), RoleMasterTableView.get_filterExpressions().toDynamicLinq(), updateRoleMaster);
    RoleMasterCommandName = args.get_commandName;

}
function radgridsystemrolemaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        ROLE_ID = args.get_gridDataItem()._dataItem.ROLE_ID;


    }
    catch (e) { }
}
function updateRoleMasterVirtualCount(result) { RoleMasterTableView.set_virtualItemCount(result); }

function updateRoleMaster(result) {

    RoleMasterTableView.set_dataSource(result);
    RoleMasterTableView.dataBind();
    if (result.length > 0) { RoleMasterTableView.selectItem(0); }

    if (RoleMasterCommandName == "Filter" || RoleMasterCommandName == "Load") { CRM.WebApp.webservice.SystemRoleMasterWebService.GetSystemRoleCount(updateRoleMasterVirtualCount); }
}
