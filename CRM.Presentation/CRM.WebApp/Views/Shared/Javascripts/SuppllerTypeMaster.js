var SUPPLIER_TYPE_ID = "";
var SupplierTableView = null;
var SupplierCommandName = "";

function radgridSupplierTypeMaster_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    SupplierTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SupplierTypeMaster.GetSupplierType(SupplierTableView.get_currentPageIndex() * SupplierTableView.get_pageSize(), SupplierTableView.get_pageSize(), SupplierTableView.get_sortExpressions().toString(), SupplierTableView.get_filterExpressions().toDynamicLinq(), updateSupplierGrid);
    SupplierCommandName = args.get_commandName;

}
function radgridSupplierTypeMaster_RowSelected(sender, args) {

    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        SUPPLIER_TYPE_ID = args.get_gridDataItem()._dataItem.SUPPLIER_TYPE_ID;

    }
    catch (e) { }
}
function updateSupplierVirtualItemCount(result) {

    SupplierTableView.set_virtualItemCount(result);

}
function updateSupplierGrid(result) {

    SupplierTableView.set_dataSource(result);
    SupplierTableView.dataBind();
    if (result.length > 0) { SupplierTableView.selectItem(0); }

    if (SupplierCommandName == "Filter" || SupplierCommandName == "Load") { CRM.WebApp.webservice.SupplierTypeMaster.SupplierTypeCount(updateSupplierVirtualItemCount); }

}