var SUPPLIER_MARGIN_ID = "";
var SupplierMarginCommandName = "";
var SupplierMarginTableView = null;


function radgridsuppliermarginmaster_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    SupplierMarginTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SupplierMarginMaster.GetSupplierMargin(SupplierMarginTableView.get_currentPageIndex() * SupplierMarginTableView.get_pageSize(), SupplierMarginTableView.get_pageSize(), SupplierMarginTableView.get_sortExpressions().toString(), SupplierMarginTableView.get_filterExpressions().toDynamicLinq(), updateSupplierMarginName);
    SupplierMarginCommandName = args.get_commandName;

}
function radgridsuppliermarginmaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        SUPPLIER_MARGIN_ID = args.get_gridDataItem()._dataItem.SUPPLIER_MARGIN_ID;


    }
    catch (e) { }
}
function updateSupplierMarginVirtualCount(result) { SupplierMarginTableView.set_virtualItemCount(result); }

function updateSupplierMarginName(result) {

    SupplierMarginTableView.set_dataSource(result);
    SupplierMarginTableView.dataBind();
    if (result.length > 0) { SupplierMarginTableView.selectItem(0); }

    if (SupplierMarginCommandName == "Filter" || SupplierMarginCommandName == "Load") { CRM.WebApp.webservice.SupplierMarginMaster.GetSupplierMarginCount(updateSupplierMarginVirtualCount); }
}
