var INQUIRY_ID = "0";
var CUST_REL_SRNO = "";
var bookingaddressTableView = null;
var bookingaddressCommandName = "";
var customerrelationTableView = null;
var customerrelationCommandName = "";
var customervisaTableView = null;
var customervisaCommandName = "";
var customerPaymentTableView = null;
var customerpaymentCommandName = "";
var globalvalue = null;
var BOOKING_ID = null;
var CUST_ID = "";
var value = "";


function radgridaddressinfo_Command(sender, args) {
  //  args.set_cancel(true);
    CRM.WebApp.webservice.BookingMaster.GetaddressDetails(value, updatebookingaddressGrid);
    bookingaddressCommandName = args.get_commandName;
    
}

function radgridcustomerrelation_Command(sender, args) {

}
function radgridcustomervisa_Command(sender, args) {

}
function radgridpaymentinfo_Command(sender, args) {

}
function radgridaddressinfo_RowSelected(sender, args) {

    try {

        INQUIRY_ID = args.get_gridDataItem()._dataItem.INQUIRY_ID;
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        CUST_ID = args.get_gridDataItem()._dataItem.CUST_ID;
        loadaddressdetail();
        loadcustomerrelation();
       
        loadpaymentdetail();
        //loadbookinginfo();
       
    }
    catch (e) { }
}
function radgridcustomerrelation_RowSelected(sender, args) {
    try {

        CUST_REL_SRNO = args.get_gridDataItem()._dataItem.CUST_REL_SRNO;
        loadvisadetail();
    }
    catch (e) { }

}

function loadaddressdetail() {
    bookingaddressCommandName = "Load";
    CRM.WebApp.webservice.BookingMaster.GetaddressDetails(INQUIRY_ID,updatebookingaddressGrid);
   
}
function loadcustomerrelation() {

    CRM.WebApp.webservice.BookingMaster.Getcustomerrelation(CUST_ID, updatecustomerrelationGrid);
    
}
function loadvisadetail() {
    CRM.WebApp.webservice.BookingMaster.Getcustomervisa(CUST_REL_SRNO, updatecustomervisa);
}
function loadpaymentdetail() {
    CRM.WebApp.webservice.BookingMaster.Getpaymentinfo(CUST_ID, updatepaymentinfo);
}
function loadbookinginfo() {
    CRM.WebApp.webservice.BookingMaster.Getbookinginfo(INQUIRY_ID,output);
}
function output(result) {
 
//       document.getElementById('ctl00_cphPageContent_txtbookingdate').value=result[0][0].BOOKING_DATE;
//       document.getElementById('ctl00_cphPageContent_txtactualcost1').value=result[0][0].TOTAL_ACTUAL_TOUR_COST_C1;
//       document.getElementById('ctl00_cphPageContent_txtactualcost2').value=result[0][0].TOTAL_ACTUAL_TOUR_COST_C2;
//       document.getElementById('ctl00_cphPageContent_txtbalancepaid1').value = result[0][0].BALANCE_TO_BE_PAID_C1;
//       document.getElementById('ctl00_cphPageContent_txtbalancepaid2').value = result[0][0].BALANCE_TO_BE_PAID_C2;
//       document.getElementById('ctl00_cphPageContent_txtagentname').value = result[0][0].FOREIGN_CURRENCY_AGENT_NAME;
//       document.getElementById('ctl00_cphPageContent_txtbookingcode').value = result[0][0].BOOKING_CODE;
//       document.getElementById('ctl00_cphPageContent_txtdocvisadate').value = result[0][0].DOCS_FOR_VISA_HANDED_OVER_DATE;
//       document.getElementById('ctl00_cphPageContent_txtpaymentdate').value = result[0][0].PAYMENT_BAL_TOUR_MADE_BY_DATE;
//       document.getElementById('ctl00_cphPageContent_txtagentrequireservice').value = result[0][0].ADD_REQ_SERVICE;
//       document.getElementById('ctl00_cphPageContent_txtbranchname').value = result[0][0].COMPANY_NAME;
//       document.getElementById('ctl00_cphPageContent_txtbookingtakenby').value = result[0][0].BOOKING_TAKEN_BY;
         document.getElementById('ctl00_cphPageContent_txtBOOKING_ID').value = result[0][0].BOOKING_ID;
//       document.getElementById('ctl00_cphPageContent_txtfamilybookingstatus').value = result[0][0].BOOKING_STATUS;
       
    }


//function updateVirtualItemCount(result) { bookingaddressTableView.set_virtualItemCount(result); }

function updatebookingaddressGrid(result) {
    bookingaddressTableView.set_dataSource(result);
    bookingaddressTableView.dataBind();
}
function updatecustomerrelationGrid(result) {

    customerrelationTableView.set_dataSource(result);
    customerrelationTableView.dataBind();
}
function updatecustomervisa(result) {

    customervisaTableView.set_dataSource(result);
    customervisaTableView.dataBind();
}
function updatepaymentinfo(result) {
    customerPaymentTableView.set_dataSource(result);
    customerPaymentTableView.dataBind();
}
function ddlChanged(sender,args) {
    //sender.selectedIndex;
    //debugger;
//    var e = document.getElementById("BOOKING_CODE")
//    BOOKING_ID = e.options[e.selectedIndex].value;
//    alert(BOOKING_ID);
    //BOOKING_ID = 
}