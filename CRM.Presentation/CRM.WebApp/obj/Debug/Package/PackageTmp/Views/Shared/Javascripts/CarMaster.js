var CAR_MASTER_ID = "";
var carCommandName = "";
var carTableView = null;


function radgridcarmaster_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    carTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CarMaster.GetCarType(carTableView.get_currentPageIndex() * carTableView.get_pageSize(), carTableView.get_pageSize(), carTableView.get_sortExpressions().toString(), carTableView.get_filterExpressions().toDynamicLinq(), updateCareTypeName);
    carCommandName = args.get_commandName;

}
function radgridcarmaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        CAR_MASTER_ID = args.get_gridDataItem()._dataItem.CAR_MASTER_ID;


    }
    catch (e) { }
}
function updateCarVirtualCount(result) { carTableView.set_virtualItemCount(result); }

function updateCareTypeName(result) {

    carTableView.set_dataSource(result);
    carTableView.dataBind();
    if (result.length > 0) { carTableView.selectItem(0); }

    if (carCommandName == "Filter" || carCommandName == "Load") { CRM.WebApp.webservice.CarMaster.GetCarTypeCount(updateCarVirtualCount); }
}

// Double Click Save
function addMyCar(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];
    ary[1] = carTableView.get_dataItems()[currentRowIndex].findElement("CAR_NAME").value;
    ary[2] = carTableView.get_dataItems()[currentRowIndex].findElement("CAR_TYPE_DESC").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CAR_MASTER_ID;
    for (i = 0; i < 3; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.CarMaster.InsertUpdateCartype(ary);
        CRM.WebApp.webservice.CarMaster.GetCarType(0, carTableView.get_pageSize(), carTableView.get_sortExpressions().toString(), carTableView.get_filterExpressions().toDynamicLinq(), updateCareTypeName);

        alert('Record Save Successfully');

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}