var DEPARTMENT_ID = "";
var departmentCommandName = "";
var departmentTableView = null;


function radgriddepartmentmaster_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    departmentTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.DepartmentMasterWebService.GetDepartmentName(departmentTableView.get_currentPageIndex() * departmentTableView.get_pageSize(), departmentTableView.get_pageSize(), departmentTableView.get_sortExpressions().toString(), departmentTableView.get_filterExpressions().toDynamicLinq(), updateDepartmentName);
    departmentCommandName = args.get_commandName;

}
function radgriddepartmentmaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        DEPARTMENT_ID = args.get_gridDataItem()._dataItem.DEPARTMENT_ID;


    }
    catch (e) { }
}
function updateDepartmentVirtualCount(result) { departmentTableView.set_virtualItemCount(result); }

function updateDepartmentName(result) {

    departmentTableView.set_dataSource(result);
    departmentTableView.dataBind();
    if (result.length > 0) { departmentTableView.selectItem(0); }

    if (departmentCommandName == "Filter" || departmentCommandName == "Load") { CRM.WebApp.webservice.DepartmentMasterWebService.DepartmentNameCount(updateDepartmentVirtualCount); }
}

// save Double Click
function addMyDepartment(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[0] = departmentTableView.get_dataItems()[currentRowIndex].findElement("DEPARTMENT_NAME").value;
    ary[1] = departmentTableView.get_dataItems()[currentRowIndex].findElement("DEPARTMENT_DESC").value;
    ary[2] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.DEPARTMENT_ID;
    for (i = 0; i < 4; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.DepartmentMasterWebService.InsertUpdateDepartment(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.DepartmentMasterWebService.GetDepartmentName(0, departmentTableView.get_pageSize(), departmentTableView.get_sortExpressions().toString(), departmentTableView.get_filterExpressions().toDynamicLinq(), updateDepartmentName);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}