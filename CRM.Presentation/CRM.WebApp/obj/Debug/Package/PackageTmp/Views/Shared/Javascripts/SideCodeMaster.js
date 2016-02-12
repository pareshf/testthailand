var SIDE_CODE_ID = "";
var sidecodeCommandName = "";
var sidecodetableView = null;


function radgridsidecodemaster_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    sidecodetableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SideCodeMaster.GetSideCode(sidecodetableView.get_currentPageIndex() * sidecodetableView.get_pageSize(), sidecodetableView.get_pageSize(), sidecodetableView.get_sortExpressions().toString(), sidecodetableView.get_filterExpressions().toDynamicLinq(), updatesidecodeTypeName);
    sidecodeCommandName = args.get_commandName;

}
function radgridsidecodemaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        SIDE_CODE_ID = args.get_gridDataItem()._dataItem.SIDE_CODE_ID;


    }
    catch (e) { }
}
function updateSidecodeVirtualCount(result) { sidecodetableView.set_virtualItemCount(result); }

function updatesidecodeTypeName(result) {

    sidecodetableView.set_dataSource(result);
    sidecodetableView.dataBind();
    if (result.length > 0) { sidecodetableView.selectItem(0); }

    if (sidecodeCommandName == "Filter" || sidecodeCommandName == "Load") { CRM.WebApp.webservice.SideCodeMaster.GetsidecodeTypeCount(updateSidecodeVirtualCount); }
}

// Save Double Click
function addMySideCode(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[1] = sidecodetableView.get_dataItems()[currentRowIndex].findElement("SIDE_CODE_NAME").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SIDE_CODE_ID;
    for (i = 0; i < 2; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    // if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    try {
        CRM.WebApp.webservice.SideCodeMaster.InsertUpdatesidecodetype(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.SideCodeMaster.GetSideCode(0, sidecodetableView.get_pageSize(), sidecodetableView.get_sortExpressions().toString(), sidecodetableView.get_filterExpressions().toDynamicLinq(), updatesidecodeTypeName);
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}
