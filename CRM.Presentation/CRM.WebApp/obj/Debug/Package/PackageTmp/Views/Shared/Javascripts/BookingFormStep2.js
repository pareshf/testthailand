var PAYMENT_SRNO = "";
var BOOKING_ID = 0;
var BOOKING_DETAIL_ID = "";
var bookingMasterCommandName = "";
var bookingDetailCommandName = "";
var bookingCostinfoCommandName = "";
var booikngMasterTabelView = null;
var bookingDetailTabelView = null;
var bookingPaymentCommandName = "";
var bookingPaymentTabelView = null;
var bookingCostInfoTabelView = null;
var globalvalue = null;
var CUST_ID = "";
var tourid = 0;
var TOUR_ID = 0;
var VISA = "";
var PASSPORT = "";
var TOKEN = "";

function radgridbookingmaster_Command(sender, args) {

    CRM.WebApp.webservice.BookingFormStep2.GetBooking(BOOKING_ID, "0", updatebookinmasterGrid);
    bookingMasterCommandName = args.get_commandName;
}

function radgridbookingmaster_RowSelected(sender, args) {

    BOOKING_ID = args.get_gridDataItem()._dataItem.BOOKING_ID;
    //currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
    CRM.WebApp.webservice.BookingFormStep2.GetBookingInformationDetail(BOOKING_ID, updatebookingdetailGrid);
    CRM.WebApp.webservice.BookingFormStep2.GetBookingPaymentDetail(BOOKING_ID, updatebookingpaymentGrid);
    CRM.WebApp.webservice.BookingFormStep2.GetCostinfo(BOOKING_ID, updatebookingCostGrid);

}

function radgridbookinginformationdetail_Command(sender, args) {
    CRM.WebApp.webservice.BookingFormStep2.GetBookingInformationDetail(BOOKING_ID, updatebookingdetailGrid);
    bookingDetailCommandName = args.get_commandName;
    VISA = args.get_gridDataItem()._dataItem.VALID_VISA;
    PASSPORT = args.get_gridDataItem()._dataItem.VALID_PASSPORT;

}

function radgridbookinginformationdetail_RowSelected(sender, args) {

}

function radgridFamilyCost_Command(sender, args) {

}
function radgridFamilyCost_RowSelected(sender, args) {

}


function radgridbookingpaymentdetail_Command(sender, args) {
    CRM.WebApp.webservice.BookingFormStep2.GetBookingPaymentDetail(BOOKING_ID, updatebookingpaymentGrid);
    bookingPaymentCommandName = args.get_commandName;
    TOKEN = args.get_gridDataItem()._dataItem.TOKEN_AMOUNT;
}

function radgridbookingpaymentdetail_RowSelected(sender, args) {
    TOKEN = args.get_gridDataItem()._dataItem.TOKEN_AMOUNT;
}

function updatebookinmasterGrid(result) {
    booikngMasterTabelView.set_dataSource(result);
    booikngMasterTabelView.dataBind();
    if (result.length > 0) { booikngMasterTabelView.selectItem(0); }
    if (bookingMasterCommandName == "Filter" || bookingMasterCommandName == "Load") { //CRM.WebApp.webservice.BookingFormStep2.GetBooking(updateVirtualItemCount); 
    }
}
function updatebookingdetailGrid(result) {
    bookingDetailTabelView.set_dataSource(result);
    bookingDetailTabelView.dataBind();
    if (result.length > 0) { bookingDetailTabelView.selectItem(0); }
    if (bookingDetailCommandName == "Filter" || bookingDetailCommandName == "Load") { //CRM.WebApp.webservice.BookingFormStep2.GetBookingInformationDetail(updateVirtualItemCount); 
    }
}

function updatebookingCostGrid(result) {
    bookingCostInfoTabelView.set_dataSource(result);
    bookingCostInfoTabelView.dataBind();
    if (result.length > 0) { bookingCostInfoTabelView.selectItem(0); }
    if (bookingCostinfoCommandName == "Filter" || bookingCostinfoCommandName == "Load") { //CRM.WebApp.webservice.BookingFormStep2.GetBookingInformationDetail(updateVirtualItemCount); 
    }

}

function updatebookingpaymentGrid(result) {
    bookingPaymentTabelView.set_dataSource(result);
    bookingPaymentTabelView.dataBind();
    if (result.length > 0) { bookingPaymentTabelView.selectItem(0); }
    if (bookingPaymentCommandName == "Filter" || bookingPaymentCommandName == "Load") { //CRM.WebApp.webservice.BookingFormStep2.GetBookingPaymentDetail(updateVirtualItemCount); 
    }

}

function updateVirtualItemCount(result) { taskTabelView.set_virtualItemCount(result); }

function BookingRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];
    ary[0] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("BOOKING_ID").value;
    ary[1] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("TOUR_ID").value;
    ary[2] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("CUST_ID").value;
    ary[3] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("INQUIRY_ID").value;
    ary[4] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("BOOKING_CODE").value;
    ary[5] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("BOOKING_DATE").value;
    ary[6] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("BOOKING_TAKEN_BY").value;
    ary[7] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("BRANCH_NAME").value;
    ary[8] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("AGENT_NAME").value;
    ary[9] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("ADD_REQ_SERVICE").value;
    ary[10] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("DOCS_FOR_VISA_HANDED_OVER_DATE").value;
    ary[11] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("PAYMENT_BAL_TOUR_MADE_BY_DATE").value;
    // ary[12] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("TOTAL_ACTUAL_TOUR_COST_C1").value;
    // ary[13] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("TOTAL_ACTUAL_TOUR_COST_C2").value;
    // ary[14] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("BALANCE_TO_BE_PAID_C1").value;
    // ary[15] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("BALANCE_TO_BE_PAID_C2").value;
    ary[16] = booikngMasterTabelView.get_dataItems()[currentRowIndex].findElement("BOOKING_STATUS_NAME").value;



    for (i = 0; i < 17; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }

    try {
        CRM.WebApp.webservice.BookingFormStep2.InsertUpdateBookingMaster(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.BookingFormStep2.GetBooking(BOOKING_ID, "0", updatebookinmasterGrid);
    }
    catch (e) {
        alert('Wrong Data Inserted');

    }
}
function PaymentRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var paymentary = [];

    paymentary[0] = bookingPaymentTabelView.get_dataItems()[currentRowIndex].findElement("PAYMENT_DATE").value;
    paymentary[1] = bookingPaymentTabelView.get_dataItems()[currentRowIndex].findElement("PAYMENT_CURRENCY_CODE").value;
    paymentary[2] = bookingPaymentTabelView.get_dataItems()[currentRowIndex].findElement("PAYMENT_MODE_NAME").value;
    paymentary[3] = bookingPaymentTabelView.get_dataItems()[currentRowIndex].findElement("REC_CHEQUE_DD_NO").value;
    paymentary[4] = bookingPaymentTabelView.get_dataItems()[currentRowIndex].findElement("TOKEN_AMOUNT").value;
    paymentary[5] = bookingPaymentTabelView.get_dataItems()[currentRowIndex].findElement("BANK_NAME").value;
    paymentary[6] = bookingPaymentTabelView.get_dataItems()[currentRowIndex].findElement("BRANCH_NAME").value;
    paymentary[7] = bookingPaymentTabelView.get_dataItems()[currentRowIndex].findElement("RECEIPT_NO").value;
    paymentary[8] = bookingPaymentTabelView.get_dataItems()[currentRowIndex].findElement("RECEIPT_DATE").value;
    paymentary[9] = bookingPaymentTabelView.get_dataItems()[currentRowIndex].findElement("RECEIVED_BY").value;
    paymentary[10] = bookingPaymentTabelView.get_dataItems()[currentRowIndex].findElement("FOREIGN_CURRENCY_AGENT_NAME").value;
    paymentary[11] = bookingPaymentTabelView.get_dataItems()[currentRowIndex].findElement("IN_THE_NAME_OF").value;
    paymentary[12] = bookingPaymentTabelView.get_dataItems()[currentRowIndex].findElement("CONVERSION_RATE").value;
    //paymentary[13] = bookingPaymentTabelView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_SRNO").value;
    paymentary[13] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.PAYMENT_SRNO;
    paymentary[14] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.BOOKING_ID;

    for (i = 0; i < 15; i++) {
        if (paymentary[i] == "" || paymentary[i] == 'null') paymentary[i] = 0;

    }
    try {
        if (window.confirm('Check This  "' + paymentary[4] + '"  Amount is Perfect?') == true) {
            if (window.confirm('Are You Damn sure  "' + paymentary[4] + '"  Amount is Perfect?') == true) {

                if (TOKEN == null || TOKEN == 0) {
                    CRM.WebApp.webservice.BookingFormStep2.InsertUpdateBookingPaymentDetails(paymentary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.BookingFormStep2.GetBookingPaymentDetail(BOOKING_ID, updatebookingpaymentGrid);
                    CRM.WebApp.webservice.BookingFormStep2.GetBooking(BOOKING_ID, "0", updatebookinmasterGrid);
                }
                else {
                    alert('Now You cant Update Payment Details');
                }
            }
        }
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}
function BookingInformationRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.BOOKING_DETAIL_ID;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.BOOKING_ID;
    //ary[0] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_DETAIL_ID").value;
    //ary[1] = bookingDetailTabelView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_ID").value;
    ary[2] = bookingDetailTabelView.get_dataItems()[currentRowIndex].findElement("RELATION").value;
    ary[3] = bookingDetailTabelView.get_dataItems()[currentRowIndex].findElement("VALID_VISA").value;
    ary[4] = bookingDetailTabelView.get_dataItems()[currentRowIndex].findElement("VALID_PASSPORT").value;
    ary[5] = bookingDetailTabelView.get_dataItems()[currentRowIndex].findElement("ROOM_TYPE_NAME").value;
    ary[6] = bookingDetailTabelView.get_dataItems()[currentRowIndex].findElement("CLASS_NAME").value;
    ary[7] = bookingDetailTabelView.get_dataItems()[currentRowIndex].findElement("CATEGORY_DESC").value;
    ary[8] = bookingDetailTabelView.get_dataItems()[currentRowIndex].findElement("BORDING_FROM").value;
    ary[9] = bookingDetailTabelView.get_dataItems()[currentRowIndex].findElement("ARRIVAL_TO").value;
    ary[10] = bookingDetailTabelView.get_dataItems()[currentRowIndex].findElement("DEPARTURE_DATE").value;
    ary[11] = bookingDetailTabelView.get_dataItems()[currentRowIndex].findElement("ARRIVAL_DATE").value;
    ary[12] = bookingDetailTabelView.get_dataItems()[currentRowIndex].findElement("MEAL").value;
    ary[13] = bookingDetailTabelView.get_dataItems()[currentRowIndex].findElement("SHARE_ROOM_IN_HOTEL").value;
    ary[14] = bookingDetailTabelView.get_dataItems()[currentRowIndex].findElement("SHARE_ROOM_IN_CRUISE").value;
    ary[15] = bookingDetailTabelView.get_dataItems()[currentRowIndex].findElement("BOOKING_STATUS_NAME").value;
    ary[16] = bookingDetailTabelView.get_dataItems()[currentRowIndex].findElement("IS_CHECKED").value;
    ary[17] = bookingDetailTabelView.get_dataItems()[currentRowIndex].findElement("ROOM_NO").value;

    for (i = 0; i < 17; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    if (ary[16] == "" || ary[16] == 'null') ary[16] = "F";

    try {
        CRM.WebApp.webservice.BookingFormStep2.InsertUpdateBookingInformation(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.BookingFormStep2.GetBookingInformationDetail(BOOKING_ID, updatebookinmasterGrid);
    }
    catch (e) {
        alert('Wrong Data Inserted');

    }

}

function CostinformationRowClick(sender, eventArgs) {
    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var costary = [];
    costary[0] = bookingCostInfoTabelView.get_dataItems()[currentRowIndex].findElement("RELATION").value;
    costary[1] = bookingCostInfoTabelView.get_dataItems()[currentRowIndex].findElement("FAMILY_COST").value;
    costary[2] = bookingCostInfoTabelView.get_dataItems()[currentRowIndex].findElement("INR_FOR_FLAMINGO").value;
    costary[3] = bookingCostInfoTabelView.get_dataItems()[currentRowIndex].findElement("INR_FOR_FOREX").value;
    costary[4] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.INQUIRY_ID;

    for (i = 0; i < 5; i++) {
        if (costary[i] == "" || costary[i] == 'null') costary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.BookingFormStep2.InsertCostInfoForAgegroup(costary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.BookingFormStep2.GetCostinfo(BOOKING_ID, updatebookingCostGrid);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}


