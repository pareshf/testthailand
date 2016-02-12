var SUPPLIER_ID = "";
var SUPPLIER_SR_NO = "";
var SUPPLIER_REL_SRNO = "";
var SupplierCommand = "";
var SupplierTableView = null;
var ContectDetailCommand = "";
var ContectDetailTableView = null;
var SupplierEmployeeCommand = "";
var SupplierEmployeeTableView = null;
var SUPPLIER_COMPANY_NAME = "";
var scommode = "";
var suniquetid = "";
var sfname = "";
var slname = "";
var semail = "";
var smob = "";
var stele = "";
var city = "";
var state = "";
var country = "";
var SUPPLIER_COMPANY_NAME = "";
var Supplier = "";
var s = "";

function radgridsuppliermaster_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    SupplierTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SupplierMasterWebService.GetSupplier(SupplierTableView.get_currentPageIndex() * SupplierTableView.get_pageSize(), SupplierTableView.get_pageSize(), SupplierTableView.get_sortExpressions().toString(), SupplierTableView.get_filterExpressions().toDynamicLinq(), scommode, sfname, slname, semail, smob, stele, updateSupplierName);

    SupplierCommand = args.get_commandName;

}
function radgridsuppliermaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        SUPPLIER_ID = args.get_gridDataItem()._dataItem.SUPPLIER_ID;
        SUPPLIER_COMPANY_NAME = args.get_gridDataItem()._dataItem.SUPPLIER_COMPANY_NAME;
        Supplier = args.get_gridDataItem()._dataItem.SUPPLIER_COMPANY_NAME;
        //CRM.WebApp.webservice.SupplierMasterWebService.Contectdetail(SUPPLIER_ID,updateContectdetail);

        CRM.WebApp.webservice.SupplierMasterWebService.Contectdetail(SUPPLIER_ID, city, state, country, updateContectdetail);

    }
    catch (e) { }
}
function radgridsupplierContectDetail_Command(sender, args) {


}
function radgridsupplierContectDetail_RowSelected(sender, args) {

    SUPPLIER_SR_NO = args.get_gridDataItem()._dataItem.SUPPLIER_SR_NO;
    currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
    SupplierCommand = "Load";
    CRM.WebApp.webservice.SupplierMasterWebService.EmployeeDetail(SUPPLIER_SR_NO, updateSupplierEmployee);
}
function radgridsupplierEmployeeDetail_Command(sender, args) {

}
function radgridsupplierEmployeeDetail_RowSelected(sender, args) {

}
function updateSupplierVirtualCount(result) { SupplierTableView.set_virtualItemCount(result); }

function updateContectVirtualCount(result) { ContectDetailTableView.set_virtualItemCount(result); }

function updateEmployeeVirtualCount(result) { SupplierEmployeeTableView.set_virtualItemCount(result); }

function updateSupplierName(result) {

    SupplierTableView.set_dataSource(result);
    SupplierTableView.dataBind();
    if (result.length > 0) { SupplierTableView.selectItem(0); }

    if (SupplierCommand == "Filter" || SupplierCommand == "Load") { CRM.WebApp.webservice.SupplierMasterWebService.GetSupplierCount(updateSupplierVirtualCount); }
}
function updateContectdetail(result) {

    ContectDetailTableView.set_dataSource(result);
    ContectDetailTableView.dataBind();
    if (result.length > 0) { ContectDetailTableView.selectItem(0); } //SUPPLIER_ID = result[0]["SUPPLIER_ID"]; }
    //else { SUPPLIER_ID = ""; }

    if (ContectDetailCommand == "Filter" || ContectDetailCommand == "Load") { CRM.WebApp.webservice.SupplierMasterWebService.GetContectDetailCount(updateContectVirtualCount); }
}
function updateSupplierEmployee(result) {

    SupplierEmployeeTableView.set_dataSource(result);
    SupplierEmployeeTableView.dataBind();
    if (result.length > 0) { SupplierEmployeeTableView.selectItem(0); }

    if (SupplierEmployeeCommand == "Filter" || SupplierEmployeeCommand == "Load") { CRM.WebApp.webservice.SupplierMasterWebService.GetEmployeeCount(updateEmployeeVirtualCount); }
}

function addSupplier(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();
    var ary = [];

    ary[0] = SupplierTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_COMPANY_NAME").value;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SUPPLIER_ID;
    ary[2] = SupplierTableView.get_dataItems()[currentRowIndex].findElement("REMARKS").value;
    // ary[3] = SupplierTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_PHOTO_PATH").value;
    //ary[4] = SupplierTableView.get_dataItems()[currentRowIndex].findElement("CREDIT_LIMIT").value;
    ary[4] = "0";
    // ary[5] = SupplierTableView.get_dataItems()[currentRowIndex].findElement("COMMUNICATION_TIME").value;
    //ary[6] = SupplierTableView.get_dataItems()[currentRowIndex].findElement("ACCOUNTING_CODE").value;
    ary[7] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SUPPLIER_UNQ_ID;
    //ary[8] = SupplierTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    ary[8] = "";
    ary[9] = SupplierTableView.get_dataItems()[currentRowIndex].findElement("COMMUNICATION_MODE_NAME").value;
    //ary[10] = SupplierTableView.get_dataItems()[currentRowIndex].findElement("DESIGNATION_DESC").value;
    ary[11] = SupplierTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_TYPE_NAME").value;
    // ary[12] = SupplierTableView.get_dataItems()[currentRowIndex].findElement("GROUP_NAME").value;
    ary[13] = SupplierTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_REL_MOBILE").value;
    ary[14] = SupplierTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_REL_PHONE").value;
    ary[15] = SupplierTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_REL_EMAIL").value;
    ary[16] = SupplierTableView.get_dataItems()[currentRowIndex].findElement("PASSWORD").value;
    //ary[17] = SupplierTableView.get_dataItems()[currentRowIndex].findElement("TO_ISSUE_SERVICE_VOUCHER").value;
    ary[17] = "";
    ary[20] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SUPPLIER_REL_SRNO;

    for (i = 0; i < 21; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    if (ary[15] != '' && ary[16] != '') {
        if (ary[0] != '') {
            
                try {

                    CRM.WebApp.webservice.SupplierMasterWebService.InsertUpdateSupplier(ary,s,OnCallBack1);
                    CRM.WebApp.webservice.SupplierMasterWebService.GetSupplier(0, SupplierTableView.get_pageSize(), SupplierTableView.get_sortExpressions().toString(), SupplierTableView.get_filterExpressions().toDynamicLinq(), scommode, sfname, slname, semail, smob, stele, updateSupplierName);

                    //alert('Record Save Successfully');

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }
            
        else {
            alert('Enter Supplier Company Name.')
        }
    }
    else {
        alert('Enter Email & Password.')
    }

}
function OnCallBack1(results, userContext, sender) {



    if (results == "N") {

        alert('This Email Already Exist.');
    }
    else if (results == "Y") {

        alert('Record Save Successfully');
    }
    else if (results == "") {
        alert('Record Save Successfully');
    }

}
function addEmployee(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[1] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex].findElement("TITLE_DESC").value;
    ary[2] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_REL_SURNAME").value;
    ary[3] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_REL_NAME").value;
    //ary[4] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_REL_GENDER").value;
    ary[4] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_REL_EMAIL").value;
    ary[5] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_REL_MOBILE").value;
    ary[6] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_REL_PHONE").value;
    ary[7] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex].findElement("PASSWORD").value;
    ary[8] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex].findElement("ALTERNATE_EMAIL").value;
    //ary[10] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex].findElement("IS_ACCOUNT").value;
    ary[9] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex].findElement("USER_STATUS_NAME").value;
    //ary[12] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex].findElement("CREDIT_LIMIT").value;
    //ary[13] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex].findElement("PARENT_SUPPLIER").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SUPPLIER_REL_SRNO;


    for (i = 0; i < 10; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = "";
    }
    if (ary[4] != '' && ary[7] != '') {
        try {

            CRM.WebApp.webservice.SupplierMasterWebService.InsertUpdateSupplierEmployeeNew(ary, s, OnCallBack);
            CRM.WebApp.webservice.SupplierMasterWebService.EmployeeDetail(SUPPLIER_SR_NO, updateSupplierEmployee);

            //alert('Record Save Successfully');

        }
        catch (e) {
            alert('Wrong Data Inserted');
        }
    }
    else {
        alert('Enter Valid Email & Password.')
    }
}
function OnCallBack(results, userContext, sender) {


    if (results == "N") {

        alert('This Email Already Exist.');
    }
    else if (results == "Y") {

        alert('Record Save Successfully');
    }
    else if (results == "") {
        alert('Record Save Successfully');
    }

}
function addContect(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[1] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("ADDRESS_TYPE_NAME").value;
    ary[2] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("ADDRESS_LINE1").value;
    ary[3] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("ADDRESS_LINE2").value;
    ary[4] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("CITY_NAME").value;
    ary[5] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("STATE_NAME").value;
    ary[6] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("COUNTRY_NAME").value;
    ary[7] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("PINCODE").value;
    ary[8] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("PHONE").value;
    ary[9] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("WEBSITE").value;
    ary[10] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("CHAIN_NAME").value;
    //ary[11] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("VIDEO_CODE").value;
    // ary[12] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("LOGO").value;
    ary[13] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("CHECK_IN_TIME").value;
    ary[14] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("CHECK_OUT_TIME").value;
    ary[15] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("FAX_NO_1").value;
    ary[16] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("FAX_NO_2").value;
    ary[17] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("RATING_STAR").value;
    ary[18] = ContectDetailTableView.get_dataItems()[currentRowIndex].findElement("LOCATION").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SUPPLIER_SR_NO;
    // ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[1].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SUPPLIER_ID;
    for (i = 0; i < 16; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = "";
    }
    try {

        if (ary[17] > 5) { alert('Hotel Star value not allowed greater then 5.'); }
        else {
            CRM.WebApp.webservice.SupplierMasterWebService.InsertUpdateSupplierContect(ary);
            //CRM.WebApp.webservice.SupplierMasterWebService.GetSupplier(SupplierTableView.get_currentPageIndex() * SupplierTableView.get_pageSize(), SupplierTableView.get_pageSize(), SupplierTableView.get_sortExpressions().toString(), SupplierTableView.get_filterExpressions().toDynamicLinq(), updateSupplierName);
            CRM.WebApp.webservice.SupplierMasterWebService.Contectdetail(SUPPLIER_ID, city, state, country, updateContectdetail);
            alert('Record Save Successfully');
        }

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}