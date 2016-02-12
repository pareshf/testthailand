var COMP_ACC_ID = "";
var CompanyBankAccountCommand = "";
var CompanyBankAccountTableView = null;
var globalvalue = null;
var s = "";

function radgridCompanyBankAccount_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    CompanyBankAccountTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CompanyBankAccountWebservice.GetCompanyBankAcount(CompanyBankAccountTableView.get_currentPageIndex() * CompanyBankAccountTableView.get_pageSize(), CompanyBankAccountTableView.get_pageSize(), CompanyBankAccountTableView.get_sortExpressions().toString(), CompanyBankAccountTableView.get_filterExpressions().toDynamicLinq(), updateCompanyBankdetail);
    CompanyBankAccountCommand = args.get_commandName;
}

function radgridCompanyBankAccount_RowSelected(sender, args) {
    currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
    COMP_ACC_ID = args.get_gridDataItem()._dataItem.COMP_ACC_ID;
}
function CompanybankVirtualCount(result) { CompanyBankAccountTableView.set_virtualItemCount(result); }

function updateCompanyBankdetail(result) {

    CompanyBankAccountTableView.set_dataSource(result);
    CompanyBankAccountTableView.dataBind();
    if (result.length > 0) { CompanyBankAccountTableView.selectItem(0); }

    if (CompanyBankAccountCommand == "Filter" || CompanyBankAccountCommand == "Load") { CRM.WebApp.webservice.CompanyBankAccountWebservice.GetBankCount(CompanybankVirtualCount); }
}

// Double Click Save

function addCompanyBank(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    
    var ary = [];

    ary[1] = CompanyBankAccountTableView.get_dataItems()[currentRowIndex].findElement("COMPANY_NAME_BANK").value;
    ary[2] = CompanyBankAccountTableView.get_dataItems()[currentRowIndex].findElement("BANK_NAME_OF_COMP").value;
    ary[3] = CompanyBankAccountTableView.get_dataItems()[currentRowIndex].findElement("COMP_BANK_BRNACH").value;
    ary[4] = CompanyBankAccountTableView.get_dataItems()[currentRowIndex].findElement("BANK_ADD_COMP").value;
    ary[5] = CompanyBankAccountTableView.get_dataItems()[currentRowIndex].findElement("ACC_NO_BANK").value;
    ary[6] = CompanyBankAccountTableView.get_dataItems()[currentRowIndex].findElement("BANK_ACC_NAME").value;
    ary[7] = CompanyBankAccountTableView.get_dataItems()[currentRowIndex].findElement("SWIFT_CODE").value;
    ary[8] = CompanyBankAccountTableView.get_dataItems()[currentRowIndex].findElement("IBOB").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.COMP_ACC_ID;

    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
    try {

        CRM.WebApp.webservice.CompanyBankAccountWebservice.InsetUpdateCompanyBank(ary, s, OnCallBack);
        //alert('Record Save Successfully');
        CRM.WebApp.webservice.CompanyBankAccountWebservice.GetCompanyBankAcount(0, CompanyBankAccountTableView.get_pageSize(), CompanyBankAccountTableView.get_sortExpressions().toString(), CompanyBankAccountTableView.get_filterExpressions().toDynamicLinq(), updateCompanyBankdetail);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}
function OnCallBack(results, userContext, sender) {

    if (results == "N") {

        alert('This Account No. Already Exist.');
    }
    else if (results == "Y") {

        alert('Record Save Successfully');
    }
    else if (results == "") {
        alert('Record Save Successfully');
    }


}