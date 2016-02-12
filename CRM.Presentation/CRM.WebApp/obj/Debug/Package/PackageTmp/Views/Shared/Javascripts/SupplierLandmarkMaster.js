var LANDMARK_ID = "";
var LandmarkCommand = "";
var LandmarkTableView = null;


function radgridsupplierlandmarkmaster_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    LandmarkTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SupplierLandmark.GetLandmark(LandmarkTableView.get_currentPageIndex() * LandmarkTableView.get_pageSize(), LandmarkTableView.get_pageSize(), LandmarkTableView.get_sortExpressions().toString(), LandmarkTableView.get_filterExpressions().toDynamicLinq(), updateLamdmarkName);
    LandmarkCommand = args.get_commandName;

}
function radgridsupplierlandmarkmaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        LANDMARK_ID = args.get_gridDataItem()._dataItem.LANDMARK_ID;


    }
    catch (e) { }
}
function updateLandmarkVirtualCount(result) { LandmarkTableView.set_virtualItemCount(result); }

function updateLamdmarkName(result) {

    LandmarkTableView.set_dataSource(result);
    LandmarkTableView.dataBind();
    if (result.length > 0) { LandmarkTableView.selectItem(0); }

    if (LandmarkCommand == "Filter" || LandmarkCommand == "Load") { CRM.WebApp.webservice.SupplierLandmark.GetLandmarkCount(updateLandmarkVirtualCount); }
}

