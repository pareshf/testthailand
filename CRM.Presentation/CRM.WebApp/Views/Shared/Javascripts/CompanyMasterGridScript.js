var CUST_COMPANY_ID = "";
var CompanyTableView = null;
var CompanyCommand = "";

function radgridcompany_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    CompanyTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CompanyMasterWebService.GetCompanyName(CompanyTableView.get_currentPageIndex() * CompanyTableView.get_pageSize(), CompanyTableView.get_pageSize(), CompanyTableView.get_sortExpressions().toString(), CompanyTableView.get_filterExpressions().toDynamicLinq(), updateCompanyGrid);
    CompanyCommand = args.get_commandName;

}
function radgridcompany_RowSelected(sender, args) {

    try {
            currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
            CUST_COMPANY_ID = args.get_gridDataItem()._dataItem.CUST_COMPANY_ID;

    }
    catch (e) { }
}
function updateCompanyVirtualItemCount(result) {

    CompanyTableView.set_virtualItemCount(result);

}
function updateCompanyGrid(result) {

    CompanyTableView.set_dataSource(result);
    CompanyTableView.dataBind();
    if (result.length > 0) { CompanyTableView.selectItem(0); }

    if (CompanyCommand == "Filter" || CompanyCommand == "Load") { CRM.WebApp.webservice.CompanyMasterWebService.CompanyNameCount(updateCompanyVirtualItemCount); }

}