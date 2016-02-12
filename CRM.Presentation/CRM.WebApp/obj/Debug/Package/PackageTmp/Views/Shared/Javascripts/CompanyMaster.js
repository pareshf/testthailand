var COMPANY_ID = "";
var CompanyMasterTableView = null;
var CompanyMasterCommand = "";

function radgridCompanyMaster_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    CompanyMasterTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CompanyWebService.GetCompanyName(CompanyMasterTableView.get_currentPageIndex() * CompanyMasterTableView.get_pageSize(), CompanyMasterTableView.get_pageSize(), CompanyMasterTableView.get_sortExpressions().toString(), CompanyMasterTableView.get_filterExpressions().toDynamicLinq(), updateCompany);
    CompanyMasterCommand = args.get_commandName;

}
function radgridCompanyMaster_RowSelected(sender, args) {

    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        COMPANY_ID = args.get_gridDataItem()._dataItem.COMPANY_ID;

    }
    catch (e) { }
}
function updateCompanyVirtualItemCount(result) {

    CompanyMasterTableView.set_virtualItemCount(result);

}
function updateCompany(result) {

    CompanyMasterTableView.set_dataSource(result);
    CompanyMasterTableView.dataBind();
    if (result.length > 0) { CompanyMasterTableView.selectItem(0); }

    if (CompanyMasterCommand == "Filter" || CompanyMasterCommand == "Load") { CRM.WebApp.webservice.CompanyWebService.CompanyCount(updateCompanyVirtualItemCount); }

}
function addCompany(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[1] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("COMPANY_NAME").value;
    ary[2] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("ADDRESS_LINE1").value;
    ary[3] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("ADDRESS_LINE2").value;
    ary[4] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("CITY_NAME").value;
    ary[5] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("STATE_NAME").value;
    ary[6] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("COUNTRY_NAME").value;
    ary[7] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("PINCODE").value;
    ary[8] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("MOBILE").value;
    ary[9] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("PHONE").value;
    ary[10] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("FAX").value;
    ary[11] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("EMAIL").value;
    ary[12] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("IS_COMPANY_FRANCHISE").value;
    ary[13] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("PARENT_NAME").value;
    ary[14] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;

    ary[15] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("ENABLE_AUTO_BAKUP").value;
    ary[16] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_SYMBOL").value;
    ary[17] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("FINANCIAL_YEAR_FROM").value;
    ary[18] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("BOOK_BEGINING_FROM").value;
    ary[19] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("SECURITY_PASSWORD").value;
    ary[20] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("BASE_CURRENCY").value;
    ary[21] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("BASE_CURRENCY_SYMBOL").value;
    ary[22] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("IS_SYMBOL_SUFFIXED_TO_AMOUNT").value;
    ary[23] = CompanyMasterTableView.get_dataItems()[currentRowIndex].findElement("SYMBOL_FOR_DECIMAL_PORTION").value;


    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.COMPANY_ID;

    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    try {
        CRM.WebApp.webservice.CompanyWebService.InsertUpdateCompany(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.CompanyWebService.GetCompanyName(CompanyMasterTableView.get_currentPageIndex() * CompanyMasterTableView.get_pageSize(), CompanyMasterTableView.get_pageSize(), CompanyMasterTableView.get_sortExpressions().toString(), CompanyMasterTableView.get_filterExpressions().toDynamicLinq(), updateCompany);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}