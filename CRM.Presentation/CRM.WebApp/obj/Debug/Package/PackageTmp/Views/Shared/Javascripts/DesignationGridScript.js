var DESIGNATION_ID = "";
var DesignationCommandName = "";
var DesignationTableView = null;


function radgriddesignationmaster_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    DesignationTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.DesignationMasterWebService.GetDesignationName(DesignationTableView.get_currentPageIndex() * DesignationTableView.get_pageSize(), DesignationTableView.get_pageSize(), DesignationTableView.get_sortExpressions().toString(), DesignationTableView.get_filterExpressions().toDynamicLinq(), updateDesignationName);
    DesignationCommandName = args.get_commandName;

}
function radgriddesignationmaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        DESIGNATION_ID = args.get_gridDataItem()._dataItem.DESIGNATION_ID;


    }
    catch (e) { }
}
function updateDesignationVirtualCount(result) { DesignationTableView.set_virtualItemCount(result); }

function updateDesignationName(result) {

    DesignationTableView.set_dataSource(result);
    DesignationTableView.dataBind();
    if (result.length > 0) { DesignationTableView.selectItem(0); }

    if (DesignationCommandName == "Filter" || DesignationCommandName == "Load") { CRM.WebApp.webservice.DesignationMasterWebService.GetDesignationCount(updateDesignationVirtualCount); }
}

// Save Double Click

function addMyDesignation(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[0] = DesignationTableView.get_dataItems()[currentRowIndex].findElement("DESIGNATION_DESC").value;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.DESIGNATION_ID;
    for (i = 0; i < 3; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.DesignationMasterWebService.InsertUpdateDesignation(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.DesignationMasterWebService.GetDesignationName(0, DesignationTableView.get_pageSize(), DesignationTableView.get_sortExpressions().toString(), DesignationTableView.get_filterExpressions().toDynamicLinq(), updateDesignationName);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}