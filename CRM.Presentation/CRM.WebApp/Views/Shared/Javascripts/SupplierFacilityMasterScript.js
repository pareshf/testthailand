var FACELITY_ID = "";
var FacilityCommand = "";
var FacilityTableView = null;


function radgridfacilitymaster_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    FacilityTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SupplierFacilityMasterWebService.GetFacility(FacilityTableView.get_currentPageIndex() * FacilityTableView.get_pageSize(), FacilityTableView.get_pageSize(), FacilityTableView.get_sortExpressions().toString(), FacilityTableView.get_filterExpressions().toDynamicLinq(), updateFacilityName);
    FacilityCommand = args.get_commandName;

}
function radgridfacilitymaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        FACELITY_ID = args.get_gridDataItem()._dataItem.FACELITY_ID;


    }
    catch (e) { }
}
function updateFacilityVirtualCount(result) { FacilityTableView.set_virtualItemCount(result); }

function updateFacilityName(result) {

    FacilityTableView.set_dataSource(result);
    FacilityTableView.dataBind();
    if (result.length > 0) { FacilityTableView.selectItem(0); }

    if (FacilityCommand == "Filter" || FacilityCommand == "Load") { CRM.WebApp.webservice.SupplierFacilityMasterWebService.GetFacilityCount(updateFacilityVirtualCount); }
}

 