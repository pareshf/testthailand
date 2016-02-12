var FIT_PACKAGE_ID = "";
var FIT_PACKAGE_CITY_ID = "";
var FitPackageTableView = null;
var FitPackageCommandName = "";
var FitMappingTableView = null;
var FitMappingCommand = "";

function radgridfitpackage_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    FitPackageTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.FitPackageWebService.GetFit(FitPackageTableView.get_currentPageIndex() * FitPackageTableView.get_pageSize(), FitPackageTableView.get_pageSize(), FitPackageTableView.get_sortExpressions().toString(), FitPackageTableView.get_filterExpressions().toDynamicLinq(), updateFitPackage);
    FitPackageCommandName = args.get_commandName;

}
function radgridfitpackage_RowSelected(sender, args) {

    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        FIT_PACKAGE_ID = args.get_gridDataItem()._dataItem.FIT_PACKAGE_ID;
        CRM.WebApp.webservice.FitPackageWebService.GetCity(FIT_PACKAGE_ID, updateFitCityPackage)

    }
    catch (e) { }
}
function radgridfitcitymapping_Command(sender, args) {


}
function radgridfitcitymapping_RowSelected(sender, args) {

    FIT_PACKAGE_CITY_ID = args.get_gridDataItem()._dataItem.FIT_PACKAGE_CITY_ID;

}
function updateFitMappingVirtualItemCount(result) {

    FitMappingTableView.set_virtualItemCount(result);

}
function updateFitVirtualItemCount(result) {

    FitPackageTableView.set_virtualItemCount(result);

}

function updateFitPackage(result) {

    FitPackageTableView.set_dataSource(result);
    FitPackageTableView.dataBind();
    if (result.length > 0) { FitPackageTableView.selectItem(0); }

    if (FitPackageCommandName == "Filter" || FitPackageCommandName == "Load") { CRM.WebApp.webservice.FitPackageWebService.FitCount(updateFitVirtualItemCount); }

}

function updateFitCityPackage(result) {

    FitMappingTableView.set_dataSource(result);
    FitMappingTableView.dataBind();
    if (result.length > 0) { FitMappingTableView.selectItem(0); }

    if (FitMappingCommand == "Filter" || FitMappingCommand == "Load") { CRM.WebApp.webservice.FitPackageWebService.FitMappingCount(updateFitMappingVirtualItemCount); }

}

// save Double Click

function addFitPackage(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();


    var ary = [];

    ary[1] = FitPackageTableView.get_dataItems()[currentRowIndex].findElement("FIT_PACKAGE_NAME").value;
    ary[2] = FitPackageTableView.get_dataItems()[currentRowIndex].findElement("PACKAGE_ORDER").value;
    ary[3] = FitPackageTableView.get_dataItems()[currentRowIndex].findElement("IS_VISIBLE").value;
   // ary[4] = FitPackageTableView.get_dataItems()[currentRowIndex].findElement("PACKAGE_MARGIN").value;
    ary[5] = FitPackageTableView.get_dataItems()[currentRowIndex].findElement("SURCHARGE").value;
    ary[6] = FitPackageTableView.get_dataItems()[currentRowIndex].findElement("MINIMUM_NIGHTS").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.FIT_PACKAGE_ID;
    for (i = 0; i < 7; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.FitPackageWebService.InsertUpdateFitPackage(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.FitPackageWebService.GetFit(0, FitPackageTableView.get_pageSize(), FitPackageTableView.get_sortExpressions().toString(), FitPackageTableView.get_filterExpressions().toDynamicLinq(), updateFitPackage);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}