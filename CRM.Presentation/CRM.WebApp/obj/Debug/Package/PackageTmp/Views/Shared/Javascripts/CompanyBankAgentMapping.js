var BANK_MAPPING_ID = "";
var CompanyBankAgentCommand = "";
var CompanyBankAgentTableView = null;
var globalvalue = null;

function radgridCompanyBankAgentMapping_Command(sender, args) {

    args.set_cancel(true);
    pageSize = sender.get_masterTableView().get_pageSize();
    CompanyBankAgentTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CompanyBankAgnetMappingWebservice.GetCompanyBankAgent(CompanyBankAgentTableView.get_currentPageIndex() * CompanyBankAgentTableView.get_pageSize(), CompanyBankAgentTableView.get_pageSize(), CompanyBankAgentTableView.get_sortExpressions().toString(), CompanyBankAgentTableView.get_filterExpressions().toDynamicLinq(), updateCompanyBankAgentdetail);
    
    CompanyBankAgentCommand = args.get_commandName;
}

function radgridCompanyBankAgentMapping_RowSelected(sender, args) {
    currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
    BANK_MAPPING_ID = args.get_gridDataItem()._dataItem.BANK_MAPPING_ID;

}
function CompanybankAgentVirtualCount(result) { CompanyBankAgentTableView.set_virtualItemCount(result); }

function updateCompanyBankAgentdetail(result) {

    CompanyBankAgentTableView.set_dataSource(result);
    CompanyBankAgentTableView.dataBind();
    if (result.length > 0) { CompanyBankAgentTableView.selectItem(0); }

    if (CompanyBankAgentCommand == "Filter" || CompanyBankAgentCommand == "Load") { CRM.WebApp.webservice.CompanyBankAgnetMappingWebservice.GetAgentBankCount(CompanybankAgentVirtualCount); }
}

// Double Click Save Data

function addCompanyBankAgent(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[1] = CompanyBankAgentTableView.get_dataItems()[currentRowIndex].findElement("BANK_NAME").value;
    ary[2] = CompanyBankAgentTableView.get_dataItems()[currentRowIndex].findElement("CUST_COMPANY_NAME").value;
    ary[3] = CompanyBankAgentTableView.get_dataItems()[currentRowIndex].findElement("COMP_BANK_BRNACH").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.BANK_MAPPING_ID;

    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
    try {

        CRM.WebApp.webservice.CompanyBankAgnetMappingWebservice.InsetUpdateCompanyBankAgent(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.CompanyBankAgnetMappingWebservice.GetCompanyBankAgent(0, CompanyBankAgentTableView.get_pageSize(), CompanyBankAgentTableView.get_sortExpressions().toString(), CompanyBankAgentTableView.get_filterExpressions().toDynamicLinq(), updateCompanyBankAgentdetail);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}