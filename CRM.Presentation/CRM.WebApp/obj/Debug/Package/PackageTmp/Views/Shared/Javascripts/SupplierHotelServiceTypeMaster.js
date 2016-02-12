var SERVICE_TYPE_ID = "";
var ServiceTypeCommand = "";
var ServiceTypeTableView = null;


function radgridSupplierHotelServiceTypeMaster_Command(sender,args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    ServiceTypeTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SupplierHotelServiceTypeMasterWebservice.GetServiceType(ServiceTypeTableView.get_currentPageIndex() * ServiceTypeTableView.get_pageSize(), ServiceTypeTableView.get_pageSize(), ServiceTypeTableView.get_sortExpressions().toString(), ServiceTypeTableView.get_filterExpressions().toDynamicLinq(), updateServeiceTypeName);
    ServiceTypeCommand = args.get_commandName;

}
function radgridSupplierHotelServiceTypeMaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        SERVICE_TYPE_ID = args.get_gridDataItem()._dataItem.SERVICE_TYPE_ID;
        
       
    }
    catch (e) { }
}
function updateServeiceTypeVirtualCount(result) { ServiceTypeTableView.set_virtualItemCount(result); }

function updateServeiceTypeName(result) {
   
    ServiceTypeTableView.set_dataSource(result);
    ServiceTypeTableView.dataBind();
    if (result.length > 0) { ServiceTypeTableView.selectItem(0);}


    if (ServiceTypeCommand == "Filter" || ServiceTypeCommand == "Load") { CRM.WebApp.webservice.SupplierHotelServiceTypeMasterWebservice.GetServiceTypeCount(updateServeiceTypeVirtualCount); }
}

