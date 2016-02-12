var MasterTableView = null;
var CustomerTableView = null;
var CustomerRlationTableView = null;
var CustomerVisaTableView = null;
var INQUIRY_ID = "";
var CUST_ID = "";
var CUST_REL_SRNO = "";
var globalvalue = "";
var cust_id = "";
var cust_rel_id = "";
var cust_rel_srno = "";
var BOOKING_ID = "";

function radgridmaster_Command(sender, args) {
    INQUIRY_ID = args.get_gridDataItem()._dataItem.INQUIRY_ID;
    CRM.WebApp.webservice.BookingFormStep1.GetCustomer(INQUIRY_ID, updateCustomerGrid);
}
function radgridmaster_RowSelected(sender, args) {
   // CRM.WebApp.webservice.BookingFormStep1.GetCustomer(INQUIRY_ID, updateCustomerGrid);
}
function RowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();
    
    var ary = [];
        ary[0] = CustomerRlationTableView.get_dataItems()[currentRowIndex].findElement("REL_TITLE").value;
        ary[1] = CustomerRlationTableView.get_dataItems()[currentRowIndex].findElement("REL_NAME").value;
        ary[2] = CustomerRlationTableView.get_dataItems()[currentRowIndex].findElement("REL_SURNAME").value;
        ary[3] = CustomerRlationTableView.get_dataItems()[currentRowIndex].findElement("REL_BIRTH_DATE").value;
        ary[4] = CustomerRlationTableView.get_dataItems()[currentRowIndex].findElement("REL_MOBILE").value;
        ary[5] =CustomerRlationTableView.get_dataItems()[currentRowIndex].findElement("REL_PHONE").value;
        ary[6] = CustomerRlationTableView.get_dataItems()[currentRowIndex].findElement("NATIONALITY").value;
        ary[7] = CustomerRlationTableView.get_dataItems()[currentRowIndex].findElement("MEAL_NAME").value;
        ary[8] = CustomerRlationTableView.get_dataItems()[currentRowIndex].findElement("REL_PASSPORT_NO").value;
        ary[9] = CustomerRlationTableView.get_dataItems()[currentRowIndex].findElement("REL_PASSPORT_ISSUE_DATE").value;
        ary[10] = CustomerRlationTableView.get_dataItems()[currentRowIndex].findElement("REL_PASSPORT_EXPIRY_DATE").value;
        ary[11] = CustomerRlationTableView.get_dataItems()[currentRowIndex].findElement("REL_PASSPORT_ISSUE_PLACE").value;
        ary[12] =CustomerRlationTableView.get_dataItems()[currentRowIndex].findElement("REL_PASSPORT_ISSUE_COUNTRY").value;
        ary[13] = CustomerRlationTableView.get_dataItems()[currentRowIndex].findElement("REL_PASSPORT_PRINTED_NAME").value;
        ary[14] = CustomerRlationTableView.get_dataItems()[currentRowIndex].findElement("REL_EMAIL").value;
        ary[15] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_ID;
        ary[16] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_REL_ID;
        ary[17] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_REL_SRNO;
        ary[18] = CustomerRlationTableView.get_dataItems()[currentRowIndex].findElement("RELATION").value;

    for (i = 0; i < 19; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.BookingFormStep1.InsertUpdateCustomerRelationDetail(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.BookingFormStep1.GetCustomerRelative(CUST_ID, updateCustomerRelativeGrid);
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}
function radgridCustomer_Command(sender, args){
    
}

function radgridCustomerRelation_Command(sender, args){
    
}

function radgridCustomerVisa_Command(sender, args){
    
}
function radgridCustomerRelation_RowSelected(sender, args) {
    CUST_REL_SRNO = args.get_gridDataItem()._dataItem.CUST_REL_SRNO;
    CRM.WebApp.webservice.BookingFormStep1.GetVisaDetail(CUST_REL_SRNO, updateVisaGrid);
    cust_id = args.get_gridDataItem()._dataItem.CUST_ID;
    cust_rel_id = args.get_gridDataItem()._dataItem.CUST_REL_ID;
    cust_rel_srno = args.get_gridDataItem()._dataItem.CUST_REL_SRNO;

}
function radgridCustomer_RowSelected(sender, args) {
    CUST_ID = args.get_gridDataItem()._dataItem.CUST_ID;
    CRM.WebApp.webservice.BookingFormStep1.GetCustomerRelative(CUST_ID, updateCustomerRelativeGrid);
}
function updateCustomerGrid(result)
{
    CustomerTableView.set_dataSource(result);
    CustomerTableView.dataBind();
    if (result.length > 0) { CustomerTableView.selectItem(0); }
}
function updateCustomerRelativeGrid(result){
    CustomerRlationTableView.set_dataSource(result);
    CustomerRlationTableView.dataBind();
}
function updateVisaGrid(result) {
    CustomerVisaTableView.set_dataSource(result);
    CustomerVisaTableView.dataBind();
}
function CustomerRowClick(sender, eventArgs) {
    currentRowIndex = eventArgs.get_itemIndexHierarchical();
    var ary = [];

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_ID;
    ary[1] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("CUST_UNQ_ID").value;
    ary[2] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("REL_TITLE").value;
    ary[3] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("REL_NAME").value;
    ary[4] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("REL_SURNAME").value;
    ary[5] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("REL_ADDRESS_LINE1").value;
    ary[6] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("REL_ADDRESS_LINE2").value;
    ary[7] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("REL_COUNTRY_NAME").value;
    ary[8] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("REL_STATE_NAME").value;
    ary[9] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("REL_CITY_NAME").value;
    ary[10] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("REL_PINCODE").value;
    ary[11] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("REL_PHONE").value;
    ary[12] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("REL_MOBILE").value;
    ary[13] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("EMM_ADDRESS_LINE1").value;
    ary[14] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("EMM_ADDRESS_LINE2").value;
    ary[15] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("EMM_COUNTRY_NAME").value;
    ary[16] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("EMM_STATE_NAME").value;
    ary[17] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("EMM_CITY_NAME").value;
    ary[18] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("EMM_PINCODE").value;
    ary[19] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("EMM_PHONE").value;
    ary[20] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("EMM_MOBILE").value;
    ary[21] = CustomerTableView.get_dataItems()[currentRowIndex].findElement("EMM_NAME").value;
    ary[22] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.REL_SRNO;
    ary[23] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.EMM_SRNO;
    //ary[24] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("REL_EMAIL").value;
    //ary[25] = CustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_SRNO").value;
    //ary[22] = '';
    //ary[23] = '';
    ary[24] = '';
    ary[25] = '';

    for (i = 0; i < 26; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }

    if (!ary[5] == 0 && !ary[6] == 0 && !ary[7] == 0 && !ary[8] == 0 && !ary[9] == 0 && !ary[10] == 0) {
        if (!ary[11] == 0 || !ary[12] == 0) {
            try {
                CRM.WebApp.webservice.BookingFormStep1.InsertUpdateCustomerDetail(ary);
                alert('Record Save Successfully');
                CRM.WebApp.webservice.BookingFormStep1.GetCustomer(INQUIRY_ID, updateCustomerGrid);
            }
            catch (e) {
                alert('Wrong Data Inserted');
            }
        }
        else {
            alert('Enter Mobile No Or Phone No');
        }
    }
    else {
        alert('Enter Full Address Detail');
    }
}
function VisaRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();
    var ary = [];

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_ID;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_REL_ID;
    ary[2] = CustomerVisaTableView.get_dataItems()[currentRowIndex].findElement("COUNTRY_NAME").value;
    ary[3] = CustomerVisaTableView.get_dataItems()[currentRowIndex].findElement("VISA_EXPIRY_DATE").value;
    ary[4] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CUST_REL_SRNO;

    for (i = 0; i < 5; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.BookingFormStep1.InsertUpdateVisaDetail(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.BookingFormStep1.GetVisaDetail(CUST_REL_SRNO, updateVisaGrid);
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}