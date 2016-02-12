var PHOTO_SRNO = "";
var HOTEL_ID = "";
var HotelComandName = "";
var HotelTableView = null;
var HotelContactDetailView = null;
var HotelCurrencyPriceMasterView = null;
var HotelphotoView = null;
var SERVICE_TYPE_DESC = "";
function radgridhotelmaster_Command(sender, args) {

    //args.set_Cancel(true);

    CRM.WebApp.webservice.HotelMaster.getHotelDetail(HotelTableView.get_currentPageIndex() * HotelTableView.get_pageSize(), HotelTableView.get_pageSize(), HotelTableView.get_sortExpressions().toString(), HotelTableView.get_filterExpressions().toDynamicLinq(), updateHotelGrid);
    HotelComandName = args.get_commandName;
   
}

function radgridhotelmaster_RowSelected(sender, args) {
    try
     {
        HOTEL_ID = args.get_gridDataItem()._dataItem.HOTEL_ID;
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        
        loadhotelcontect();
        loadhotelcurrency();
        filltable();
        CRM.WebApp.webservice.HotelMaster.GetHotelPhotoDetails(HOTEL_ID, updateHotelphotoGrid);
    }
    catch (e) {
        
    } 

}
function loadhotelcontect() {

    CRM.WebApp.webservice.HotelMaster.GetHotelContectDetailByHotel_ID(HOTEL_ID, updateHotelContactDetailGrid);
}
function loadhotelcurrency() {
    CRM.WebApp.webservice.HotelMaster.GetHotelCurrencyPriceMasterByHotel_ID(HOTEL_ID, updateHotelCurrencyPriceMasterGrid);

}
function filltable() {
    CRM.WebApp.webservice.HotelMaster.GetServiceType(HOTEL_ID, output);
}
function output(result) {
        try {
            if (SERVICE_TYPE_DESC == null) {

        }
        else {
            document.getElementById('ctl00_cphPageContent_txtservicetype').value = result[0][0].SERVICE_TYPE_DESC;
        }
    }
    catch (e) {

    } 
    
}
function radgridHotelDetail_Command(sender, args) { }
function radgridHotelCurrencyPriceMaster_Command(sender, args) { }
function radgridHotalphotomaster_Command(sender, args) { }
function radgridHotalphotomaster_RowSelected(sender, args) {
    PHOTO_SRNO = args.get_gridDataItem()._dataItem.PHOTO_SRNO;
    currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        
}
function updateVirtualItemCount(result) {
    HotelTableView.set_virtualItemCount(result);
    
  }

function updateHotelGrid(result) {
    HotelTableView.set_dataSource(result);
    HotelTableView.dataBind();
    if (result.length > 0) { HotelTableView.selectItem(0); }
    if (HotelComandName == "Filter" || HotelComandName == "Load") { CRM.WebApp.webservice.HotelMaster.GetHotelCount(updateVirtualItemCount); }
   
}
function updateHotelContactDetailGrid(result) {
    HotelContactDetailView.set_dataSource(result);
    HotelContactDetailView.dataBind();

}
function updateHotelCurrencyPriceMasterGrid(result) {
    HotelCurrencyPriceMasterView.set_dataSource(result);
    HotelCurrencyPriceMasterView.dataBind();

}
function updateHotelphotoGrid(result) {

    HotelphotoView.set_dataSource(result);
    HotelphotoView.dataBind();
//    if (result.length > 0) { HotelphotoView.selectItem(0); HOTEL_ID = result[0]["HOTEL_ID"]; }
//    else { HOTEL_ID = ""; }
//    if (HotelComandName == "Filter" || HotelComandName == "Load") { CRM.WebApp.webservice.HotelMaster.GetPhotoCount(updatePhotoVirtualItemCount); }
}
function HotelRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();
    var ary = [];
   
    ary[1] = HotelTableView.get_dataItems()[currentRowIndex].findElement("HOTEL_NAME").value;
    ary[2] = HotelTableView.get_dataItems()[currentRowIndex].findElement("HOTEL_RATING").value;
    ary[3] = HotelTableView.get_dataItems()[currentRowIndex].findElement("ADDRESS_LINE1").value;
    ary[4] = HotelTableView.get_dataItems()[currentRowIndex].findElement("ADDRESS_LINE2").value;
    ary[5] = HotelTableView.get_dataItems()[currentRowIndex].findElement("CITY_NAME").value;
    ary[6] = HotelTableView.get_dataItems()[currentRowIndex].findElement("STATE_NAME").value;
    ary[7] = HotelTableView.get_dataItems()[currentRowIndex].findElement("COUNTRY_NAME").value;
    ary[8] = HotelTableView.get_dataItems()[currentRowIndex].findElement("PINCODE").value;
    ary[9] = HotelTableView.get_dataItems()[currentRowIndex].findElement("EMAIL").value;
    ary[10] = HotelTableView.get_dataItems()[currentRowIndex].findElement("PHONE").value;
    ary[11] = HotelTableView.get_dataItems()[currentRowIndex].findElement("FAX").value;
    ary[12] = HotelTableView.get_dataItems()[currentRowIndex].findElement("HOTEL_WEBSITE").value;
    ary[13] = HotelTableView.get_dataItems()[currentRowIndex].findElement("GMT").value;
    ary[14] = HotelTableView.get_dataItems()[currentRowIndex].findElement("CHECK_IN_TIME").value;
    ary[15] = HotelTableView.get_dataItems()[currentRowIndex].findElement("CHECK_OUT_TIME").value;

    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
    if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
    if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
    if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;
    if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
    if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;
    if (ary[6] == "" || ary[6] == 'null') ary[6] = 0;
    if (ary[7] == "" || ary[7] == 'null') ary[7] = 0;
    if (ary[8] == "" || ary[8] == 'null') ary[8] = 0;
    if (ary[9] == "" || ary[9] == 'null') ary[9] = 0;
    if (ary[10] == "" || ary[10] == 'null') ary[10] = 0;
    if (ary[11] == "" || ary[11] == 'null') ary[11] = 0;
    if (ary[12] == "" || ary[12] == 'null') ary[12] = 0;
    if (ary[13] == "" || ary[13] == 'null') ary[13] = 0;
    if (ary[14] == "" || ary[14] == 'null') ary[14] = 0;
    if (ary[15] == "" || ary[15] == 'null') ary[15] = 0;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.HOTEL_ID;

    try {
        CRM.WebApp.webservice.HotelMaster.InsertUpdateHotel(ary);

        CRM.WebApp.webservice.HotelMaster.getHotelDetail(0, HotelTableView.get_pageSize(), HotelTableView.get_sortExpressions().toString(), HotelTableView.get_filterExpressions().toDynamicLinq(), updateHotelGrid);
      
        alert('Record Save Successfully');

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}
function HotelDetailRowClick(sender, eventArgs) {
    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.HOTEL_ID;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CONTACT_SRNO;
    ary[2] = HotelContactDetailView.get_dataItems()[currentRowIndex].findElement("TITLE_DESC").value;
    ary[3] = HotelContactDetailView.get_dataItems()[currentRowIndex].findElement("NAME").value;
    ary[4] = HotelContactDetailView.get_dataItems()[currentRowIndex].findElement("SURNAME").value;
    ary[5] = HotelContactDetailView.get_dataItems()[currentRowIndex].findElement("DESIGNATION_DESC").value;
    ary[6] = HotelContactDetailView.get_dataItems()[currentRowIndex].findElement("EMAIL").value;
    ary[7] = HotelContactDetailView.get_dataItems()[currentRowIndex].findElement("MOBILE").value;
    ary[8] = HotelContactDetailView.get_dataItems()[currentRowIndex].findElement("PHONE").value;


    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
    if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
    if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
    if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;
    if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
    if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;
    if (ary[6] == "" || ary[6] == 'null') ary[6] = 0;
    if (ary[7] == "" || ary[7] == 'null') ary[7] = 0;
    if (ary[8] == "" || ary[8] == 'null') ary[8] = 0;



    try {

        CRM.WebApp.webservice.HotelMaster.InsertUpdateHotelContactDetail(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.HotelMaster.GetHotelContectDetailByHotel_ID(HOTEL_ID, updateHotelContactDetailGrid);

        alert('Record Save Successfully');

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}
function HotelPriceRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();
    var ary = [];
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.HOTEL_ID;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CURRENCY_PRICE_ID;
    ary[2] = HotelCurrencyPriceMasterView.get_dataItems()[currentRowIndex].findElement("ROOM_TYPE_NAME").value;
    ary[3] = HotelCurrencyPriceMasterView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    ary[4] = HotelCurrencyPriceMasterView.get_dataItems()[currentRowIndex].findElement("AMOUNT").value;
    ary[5] = HotelCurrencyPriceMasterView.get_dataItems()[currentRowIndex].findElement("TAX").value;
    ary[6] = HotelCurrencyPriceMasterView.get_dataItems()[currentRowIndex].findElement("GST").value;

    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;



    try {

        CRM.WebApp.webservice.HotelMaster.InsertUpdateHotelCurrencyPriceMaster(ary);
        CRM.WebApp.webservice.HotelMaster.GetHotelCurrencyPriceMasterByHotel_ID(HOTEL_ID, updateHotelCurrencyPriceMasterGrid);

        alert('Record Save Successfully');

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}
function PhotoRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();


    a[0] = HotelphotoView.get_dataItems()[currentRowIndex].findElement("PHOTO_TITLE").value;
    a[1] = HotelphotoView.get_dataItems()[currentRowIndex].findElement("PHOTO_DESC").value;
    a[2] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.PHOTO_SRNO;
    a[3] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.HOTEL_ID;
    for (i = 0; i < 1; i++) {
        if (a[i] == "" || a[i] == 'null') a[i] = 0;
    }
    try {
        CRM.WebApp.webservice.HotelMaster.insertupdatePhotoDetail(a);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.HotelMaster.GetHotelPhotoDetails(HOTEL_ID, updateHotelphotoGrid);
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}