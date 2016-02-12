var COACH_TYPE_MASTER_ID = "";
var CoachTypeCommand = "";
var CoachTypeTableView = null;


function radgridCoachMaster_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    CoachTypeTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CoachMasterWeb.GetCoachType(CoachTypeTableView.get_currentPageIndex() * CoachTypeTableView.get_pageSize(), CoachTypeTableView.get_pageSize(), CoachTypeTableView.get_sortExpressions().toString(), CoachTypeTableView.get_filterExpressions().toDynamicLinq(), updateCoachTypeName);
    CoachTypeCommand = args.get_commandName;

}
function radgridCoachMaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        COACH_TYPE_MASTER_ID = args.get_gridDataItem()._dataItem.COACH_TYPE_MASTER_ID;


    }
    catch (e) { }
}
function updateCoachtypeVirtualCount(result) { CoachTypeTableView.set_virtualItemCount(result); }

function updateCoachTypeName(result) {

    CoachTypeTableView.set_dataSource(result);
    CoachTypeTableView.dataBind();
    if (result.length > 0) { CoachTypeTableView.selectItem(0); }

    if (CoachTypeCommand == "Filter" || CoachTypeCommand == "Load") { CRM.WebApp.webservice.CoachMasterWeb.GetCoachTypeCount(updateCoachtypeVirtualCount); }
}

// Double Click Save
function addMyCoach(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();
    var ary = [];

    ary[1] = CoachTypeTableView.get_dataItems()[currentRowIndex].findElement("COACH_NAME").value;
    ary[2] = CoachTypeTableView.get_dataItems()[currentRowIndex].findElement("COACH_TYPE_DESC").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.COACH_TYPE_MASTER_ID;
    for (i = 0; i < 3; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.CoachMasterWeb.InsertUpdatecoachtype(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.CoachMasterWeb.GetCoachType(0, CoachTypeTableView.get_pageSize(), CoachTypeTableView.get_sortExpressions().toString(), CoachTypeTableView.get_filterExpressions().toDynamicLinq(), updateCoachTypeName);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}