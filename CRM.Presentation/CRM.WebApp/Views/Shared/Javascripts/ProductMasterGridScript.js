var PRODUCT_ID = "";
var ProductTableView = null;
var ProductCommandName = "";

function radgridproduct_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    ProductTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.ProductMasterWebService.GetProductName(ProductTableView.get_currentPageIndex() * ProductTableView.get_pageSize(), ProductTableView.get_pageSize(), ProductTableView.get_sortExpressions().toString(), ProductTableView.get_filterExpressions().toDynamicLinq(), updateProductName);
    ProductCommandName = args.get_commandName;

}

function radgridproduct_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        PRODUCT_ID = args.get_gridDataItem()._dataItem.PRODUCT_ID;


    }
    catch (e) { }
}
function updateProductVirtualItemCount(result) {

    ProductTableView.set_virtualItemCount(result);

}
function updateProductName(result) {

    ProductTableView.set_dataSource(result);
    ProductTableView.dataBind();
    if (result.length > 0) { ProductTableView.selectItem(0); }

    if (ProductCommandName == "Filter" || ProductCommandName == "Load") { CRM.WebApp.webservice.ProductMasterWebService.ProductNameCount(updateProductVirtualItemCount); }

}