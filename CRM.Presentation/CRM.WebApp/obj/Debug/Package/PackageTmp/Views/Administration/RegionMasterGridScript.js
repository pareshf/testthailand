var REGION_ID = "";
var RegionCommand = "";
var RegionTableView = null;


function radgridregionmaster_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    RegionTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.RegionMasterWebService.GetCompanyRegion(RegionTableView.get_currentPageIndex() * RegionTableView.get_pageSize(), RegionTableView.get_pageSize(), RegionTableView.get_sortExpressions().toString(), RegionTableView.get_filterExpressions().toDynamicLinq(), updateCompanyRegionGrid);
    RegionCommand = args.get_commandName;

}
function radgridregionmaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        REGION_ID = args.get_gridDataItem()._dataItem.REGION_ID;

    }
    catch (e) { }
}
function updateRegionCount(result) { RegionTableView.set_virtualItemCount(result); }

function updateCompanyRegionGrid(result) {

    RegionTableView.set_dataSource(result);
    RegionTableView.dataBind();
    if (result.length > 0) { RegionTableView.selectItem(0); }

    if (RegionCommand == "Filter" || RegionCommand == "Load") { CRM.WebApp.webservice.RegionMasterWebService.GetCompanyRegionCount(updateRegionCount); }
}

