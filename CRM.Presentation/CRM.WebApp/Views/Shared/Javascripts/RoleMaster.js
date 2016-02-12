var ROLE_ID = "";
var roleCommandName = "";
var roleTableView = null;

function radgridrolemaster_Command(sender, args) {

    args.set_Cancel(true);
    CRM.WebApp.webservice.RoleMasterWebService.GetRoleDetails(0, roleTableView.get_currentpageIndex() * roleTableView.get_pageSize(), roleTableView.get_sortExpressions().toString(), roleTableView.get_filterExpressions().toDynamicLinq(), updaterolemasterGrid);
    roleCommandName = args.get_commandName;

}

function radgridrolemaster_RowSelected(sender, args) {

    try {
        ROLE_ID = args.get_gridDataItem()._dataItem.ROLE_ID;
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
    }
    catch (e) { }
}
function updateVirtualItemCount(result) {

    roleTableView.set_virtualItemCount(result);
}

function updaterolemasterGrid() {

    roleTableView.set_dataSource(result);
    roleTableView.dataBind();
}
function loadRole() {

    roleCommandName = "Load";
    CRM.WebApp.webservice.RoleMasterWebService.GetRoleDetails(ROLE_ID, updaterolemasterGrid);
}