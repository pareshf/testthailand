var CUST_ID = "";
var CRUISE_COMPANY_ID = "";
var customermasterCommandName = "";
var customermasterTableView = null;
var custcontactdetCommandName = "";
var custcontactdetTableView = null;
var customerrelationTableView = null;
var customerrelationCommandName = "";
var custmsgTableView = null;
var custmsgCommandName = "";
var scomapany = "";
var scity = "";
var scode = "";
var stype = "";
var sbranch = "";
var semp = "";
var scommode = "";
var srelation = "";
var suniquetid = "";
var sfname = "";
var slname = "";
var semail = "";
var smob = "";
var stele = "";
var CUST_REL_ID = "";
var SR_NO = "";
var CUST_REL_SRNO = "";
var name = "";
var custidsms = "";
var CUST_SURNAME = "";
var customerDocumentTableView = null;
var customerid = "";
var CUST_COMPANY_NAME = "";
var Company = "";
var Agent = "";
var s = "";
var globalvalue = null;

function radgridcustomermaster_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    customermasterTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CustomerMasterWebService.GetCust(customermasterTableView.get_currentPageIndex() * customermasterTableView.get_pageSize(), customermasterTableView.get_pageSize(), customermasterTableView.get_sortExpressions().toString(), customermasterTableView.get_filterExpressions().toDynamicLinq(), scomapany, scity, scode, stype, sbranch, semp, scommode, suniquetid, sfname, slname, semail, smob, stele, updateCustGrid);
    customermasterCommandName = args.get_commandName();
}

function radgridcontdetmaster_Command(sender, args) {
}

function radgridcustomerrelation_Command(sender, args) {
}

function radgridcusmsg_Command(sender, args) {
}

function radgridcustomermaster_RowSelected(sender, args) {
    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        CUST_ID = args.get_gridDataItem()._dataItem.CUST_ID;
        CUST_REL_SRNO = args.get_gridDataItem()._dataItem.CUST_REL_SRNO;
        CUST_REL_ID = args.get_gridDataItem()._dataItem.CUST_REL_ID;
       
        CUST_COMPANY_NAME = args.get_gridDataItem()._dataItem.CUST_COMPANY_NAME;
        Agent = args.get_gridDataItem()._dataItem.CUST_COMPANY_NAME;

        CRM.WebApp.webservice.CustomerMasterWebService.ContdetGrid(CUST_ID, updateCustcontactdet);
        //CRM.WebApp.webservice.CustomerMasterWebService.RelGrid(CUST_ID, scity, suniquetid, sfname, slname, semail, smob, stele, updateRel);
        CRM.WebApp.webservice.CustomerMasterWebService.sms(CUST_ID, updateCustmsgdet);
        CRM.WebApp.webservice.CustomerMasterWebService.doc(CUST_ID, updateCustdocument);
    }
    catch (e) { }
}
function radgridcontdetmaster_RowSelected(sender, args) {
    try {

        SR_NO = args.get_gridDataItem()._dataItem.SR_NO;
        CRM.WebApp.webservice.CustomerMasterWebService.RelGrid(SR_NO, scity, suniquetid, sfname, slname, semail, smob, stele, updateRel);
    }
    catch (e) { }
}
function radgridcustomerrelation_RowSelected(sender, args) {
    try {
        CUST_ID = args.get_gridDataItem()._dataItem.CUST_ID;
        CUST_REL_SRNO = args.get_gridDataItem()._dataItem.CUST_REL_SRNO;
        CUST_REL_ID = args.get_gridDataItem()._dataItem.CUST_REL_ID;
    }
    catch (e) { }
}

function radgridcusmsg_RowSelected(sender, args) {
    try {
    }
    catch (e) { }
}

function updateCustGrid(result) {

    customermasterTableView.set_dataSource(result);
    customermasterTableView.dataBind();
    if (result.length > 0) { customermasterTableView.selectItem(0); }
    if (customermasterCommandName == "Filter" || customermasterCommandName == "Load") { CRM.WebApp.webservice.CustomerMasterWebService.GetCustCount(updateVirtualItemCount); }
}

function updateVirtualItemCount(result) {
    customermasterTableView.set_virtualItemCount(result);
}
function updateVirtualContDetItemCount(result) {
    custcontactdetTableView.set_virtualItemCount(result);
}

function updateVirtualRelItemCount(result) {
    customerrelationTableView.set_virtualItemCount(result);
}

function updateVirtualcustmsgItemCount(result) {
    custmsgTableView.set_virtualItemCount(result);
}

function updateCustcontactdet(result) {

    custcontactdetTableView.set_dataSource(result);
    custcontactdetTableView.dataBind();
    if (result.length > 0) { custcontactdetTableView.selectItem(0); }
    if (custcontactdetCommandName == "Filter" || custcontactdetCommandName == "Load") { CRM.WebApp.webservice.CustomerMasterWebService.GetCustomerCount(updateVirtualContDetItemCount); }
}

function updateCustmsgdet(result) {

    custmsgTableView.set_dataSource(result);
    custmsgTableView.dataBind();
    if (result.length > 0) { custmsgTableView.selectItem(0); }
    if (custmsgCommandName == "Filter" || custmsgCommandName == "Load") { CRM.WebApp.webservice.CustomerMasterWebService.GetCustomerMsgCount(updateVirtualcustmsgItemCount); }
}

function updateRel(result) {

    customerrelationTableView.set_dataSource(result);
    customerrelationTableView.dataBind();
    if (result.length > 0) { }
    if (customerrelationCommandName == "Filter" || customerrelationCommandName == "Load") { CRM.WebApp.webservice.CustomerMasterWebService.GetRelCount(updateVirtualRelItemCount); }
}

function radgriddocmaster_Command(sender, args) { }
function radgriddocmaster_RowSelected(sender, args) {
    SR_NO = args.get_gridDataItem()._dataItem.SR_NO;
}
function updateCustdocument(result) {
    customerDocumentTableView.set_dataSource(result);
    customerDocumentTableView.dataBind();
}

function ContactAddeddouble(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();
    var ary = [];
    ary[2] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SR_NO;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_ID;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.RELATION_DESC;
    //ary[3] = custcontactdetTableView.get_dataItems()[currentRowIndex].findElement("ADDRESS_TYPE_NAME").value;
    ary[3] = custcontactdetTableView.get_dataItems()[currentRowIndex].findElement("ADDRESS_TYPE_NAME").value;
    ary[4] = custcontactdetTableView.get_dataItems()[currentRowIndex].findElement("CUST_ADDRESS_LINE1").value;
    ary[5] = custcontactdetTableView.get_dataItems()[currentRowIndex].findElement("CUST_ADDRESS_LINE2").value;
    ary[6] = custcontactdetTableView.get_dataItems()[currentRowIndex].findElement("CITY_NAME").value;
    ary[7] = custcontactdetTableView.get_dataItems()[currentRowIndex].findElement("STATE_NAME").value;
    ary[8] = custcontactdetTableView.get_dataItems()[currentRowIndex].findElement("COUNTRY_NAME").value;
    ary[9] = custcontactdetTableView.get_dataItems()[currentRowIndex].findElement("CUST_PINCODE").value;
    ary[10] = custcontactdetTableView.get_dataItems()[currentRowIndex].findElement("CUST_PHONE").value;
    //ary[11] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("EMERGENCY_NAME").value;
    ary[12] = custcontactdetTableView.get_dataItems()[currentRowIndex].findElement("CUST_MOBILE").value;

    ary[14] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_REL_ID;
    ary[15] = custcontactdetTableView.get_dataItems()[currentRowIndex].findElement("WEBSITE").value;
    ary[16] = custcontactdetTableView.get_dataItems()[currentRowIndex].findElement("CHAIN_NAME").value;
    //ary[17] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_ID;
    //ary[18] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_ID;

    //                ary[17] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("VIDEO").value;
    //                ary[18] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("LOGO").value;
    for (i = 0; i < 39; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = "";
    }
    //    if (!ary[10] == 0 || !ary[12] == 0) {

    try {
        CRM.WebApp.webservice.CustomerMasterWebService.InsertUpdateContdet(ary); // add new cust
        CRM.WebApp.webservice.CustomerMasterWebService.ContdetGrid(CUST_ID, updateCustcontactdet);
        alert('Record Save Successfully');
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
    //    }
    //    else {
    //        alert('Enter Mobile No Or Phone No');
    //    }
}

function RelationAddeddouble(sender, eventArgs) {
    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_UNQ_ID;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_ID;
    ary[2] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_REL_ID;
    ary[3] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_REL_SRNO;

    ary[4] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("TITLE_DESC").value;
    ary[5] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("CUST_REL_SURNAME").value;
    ary[6] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("CUST_REL_NAME").value;
    ary[7] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("CUST_REL_MOBILE").value;
    ary[8] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("CUST_REL_EMAIL").value;

    ary[9] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("CUST_REL_PHONE").value;

    //ary[10] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("RELATION_DESC").value;
    ary[11] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("DESIGNATION_DESC").value;

    //ary[12] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("CUST_REL_GENDER").value;
    // ary[13] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("MEAL_DESC").value;
    //ary[14] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("NATIONALITY_NAME").value;

    ary[15] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("PASSWORD").value;
    ary[16] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("ALT_EMAIL").value;
    //ary[17] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("CREDIT_LIMIT").value;
    ary[18] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("USER_STATUS_NAME").value;
    //ary[19] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    //ary[21] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("PARENT_CUSTORMER").value;
    ary[21] = CUST_ID;
    ary[22] = SR_NO;
    // ary[24] = customerrelationTableView.get_dataItems()[currentRowIndex].findElement("IS_ACCOUNT").value;
    for (i = 0; i < 21; i++) {
        if (ary[i] == "" || ary[i] == 'null' || ary[i] == ' ') ary[i] = 0;
    }
    if (ary[8] != '' && ary[15] != '') {
        try {
            CRM.WebApp.webservice.CustomerMasterWebService.InsertUpdateRelation(ary, s, OnCallBack); // add new cust
            CRM.WebApp.webservice.CustomerMasterWebService.RelGrid(SR_NO, scity, suniquetid, sfname, slname, semail, smob, stele, updateRel);
            //alert('Record Save Successfully');
        }
        catch (e) {
            alert('Wrong Data Inserted');
        }
    }
    else {
        alert('Enter Email and Password. ');
    }

}

function CustomerMasterRowClick(sender, eventArgs) {
    currentRowIndex = eventArgs.get_itemIndexHierarchical();
    
    var ary = [];
    ary[2] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("TITLE_DESC").value;
    ary[3] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("CUST_SURNAME").value;
    ary[4] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("CUST_NAME").value;
    ary[5] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("CUST_REL_MOBILE").value;
    ary[6] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("CUST_REL_EMAIL").value;
    ary[7] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("CUST_TYPE_NAME").value;

    ary[8] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("COMMUNICATION_MODE_NAME").value;
    // ary[9] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("COMMUNICATION_TIME").value;
    ary[10] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("CUST_REL_PHONE").value;
    //ary[11] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("BRANCH").value;
    //ary[12] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("EMPLOYEE").value;

    //ary[13] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("CUST_REL_ID").value;
    //ary[13] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_REL_ID;
    //ary[14] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("CUST_REL_SRNO").value;
    ary[14] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_REL_SRNO;
    //ary[15] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("RELATION_DESC").value;
    ary[16] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("DESIGNATION_DESC").value;

    //ary[17] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("GROUP_NAME").value;
    ary[18] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("CREDIT_LIMIT").value;
    //ary[19] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("ACCOUNTING_CODE").value;
    //ary[20] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("CUST_REL_GENDER").value;
    //ary[21] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("MEAL_DESC").value;
    //ary[22] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("NATIONALITY_NAME").value;
    ary[23] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("CUST_COMPANY_NAME").value;
    ary[24] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    ary[25] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("PAYMENT_TERMS").value;
    ary[26] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("PASSWORD").value;
    ary[27] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("CURRENT_USABLE_CREDIT_LIMIT").value;
    ary[28] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("FIT_BOOKING_AMOUNT_IN_PECENTAGE").value;

    ary[29] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("BANK_NAME").value;
    ary[30] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("BANK_BRNACH").value;
    ary[31] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("ACC_NAME").value;
    ary[32] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("STATUS_NAME").value;
    ary[33] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("BANK_CHARGES").value;
    ary[34] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("BANK_CHARGE_APPLICABLE").value;
    //ary[0] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("CUST_ID").value;
    //ary[1] = customermasterTableView.get_dataItems()[currentRowIndex].findElement("CUST_UNQ_ID").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_ID;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_UNQ_ID;

    for (i = 0; i < 26; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    if (ary[6] != '' && ary[26] != '') {

        if (ary[23] != '') {
            if (ary[7] != '') {
                try {

                    var q = window.location.search.substring(1);
                    if (q != "") {

                        CRM.WebApp.webservice.CustomerMasterWebService.InsertUpdateCust(ary, s, OnCallBack);
                        CRM.WebApp.webservice.CustomerMasterWebService.GetCustwithID(cust_id, updateCustGrid);
                        //alert('Record Save Successfully');
                    }
                    else {

                        CRM.WebApp.webservice.CustomerMasterWebService.InsertUpdateCust(ary, s, OnCallBack); // add new cust
                        CRM.WebApp.webservice.CustomerMasterWebService.GetCust(0, customermasterTableView.get_pageSize(), customermasterTableView.get_sortExpressions().toString(), customermasterTableView.get_filterExpressions().toDynamicLinq(), scomapany, scity, scode, stype, sbranch, semp, scommode, suniquetid, sfname, slname, semail, smob, stele, updateCustGrid);

                        //alert('Record Save Successfully');
                    }


                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }
            else {

                alert('Enter Agent Type');
            }
        }
        else {
            alert('Enter Agent Company Name.')
        }
    }
    else {

        alert('Enter User Name and Password.')
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




