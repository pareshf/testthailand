var CRUISE_ID = "";
var CRUISE_COMPANY_ID = "";
var customersCommandName = "";
var customersTableView = null;
var CruiseScheduleCommandName = "";
var CruiseScheduleTableView = null;
var CruisePriceCommandName = "";
var CruisePriceTableView = null;
var CruiseVisaTableView = null;
var CruiseVisaCommandName = "";

function radgridmaster_Command(sender, args) {
    args.set_cancel(true);
    CRM.WebApp.webservice.CruiseMasterWebService.GetCruise(customersTableView.get_currentPageIndex() * customersTableView.get_pageSize(), customersTableView.get_pageSize(), customersTableView.get_sortExpressions().toString(), customersTableView.get_filterExpressions().toDynamicLinq(), updateCruiseGrid);
    customersCommandName = args.get_commandName();
}

function radgridmaster_RowSelected(sender, args) {
    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        CRUISE_COMPANY_ID = args.get_gridDataItem()._dataItem.CRUISE_COMPANY_ID;
        CRM.WebApp.webservice.CruiseMasterWebService.CruiseScheduleGrid(CRUISE_COMPANY_ID, updateCruiseScheduleGrid);

//        CRM.WebApp.webservice.CruiseMasterWebService.CruisepriceGrid(CRUISE_ID, updateCruisePrice);
//        CRM.WebApp.webservice.CruiseMasterWebService.CruisevisaGrid(CRUISE_ID, updateCruiseVisa);
        }
    catch (e) { }
}
function updateVirtualItemCount(result) {

    customersTableView.set_virtualItemCount(result);

}
function updateCruiseGrid(result) {

    customersTableView.set_dataSource(result);
    customersTableView.dataBind();
    if (result.length > 0) { customersTableView.selectItem(0); }
    if (customersCommandName == "Filter" || customersCommandName == "Load") { CRM.WebApp.webservice.CruiseMasterWebService.GetCruiseCount(updateVirtualItemCount); }

}
function loadCruise() {
    customersCommandName = "Load";
    CRM.WebApp.webservice.CruiseMasterWebService.GetCruise(CRUISE_COMPANY_ID, updateCruiseGrid);

}

function radgridschedule_Command(sender, args) {
    CRM.WebApp.webservice.CruiseMasterWebService.CruiseScheduleGrid(CRUISE_COMPANY_ID, updateCruiseScheduleGrid);
    CruiseScheduleCommandName = args.get_commandName();

}
function radgridschedule_RowSelected(sender, args) {
    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        CRUISE_ID = args.get_gridDataItem()._dataItem.CRUISE_ID;
        CRM.WebApp.webservice.CruiseMasterWebService.CruisepriceGrid(CRUISE_ID, updateCruisePrice);
        CRM.WebApp.webservice.CruiseMasterWebService.CruisevisaGrid(CRUISE_ID, updateCruiseVisa);
    }
    catch (e) { }
}

function radgridprice_Command(sender, args) {
//    CRM.WebApp.webservice.CruiseMasterWebService.CruisepriceGrid(CRUISE_ID, updateCruisePrice);

//    CruisePriceCommandName = args.get_commandName();
}

function radgridvisa_Command(sender, args) {
//    CRM.WebApp.webservice.CruiseMasterWebService.CruisevisaGrid(CRUISE_ID, updateCruiseVisa);
//    CruiseVisaCommandName = args.get_commandName();
}





function updateScheduleVirtualItemCount(result) {
    CruiseScheduleTableView.set_virtualItemCount(result);
}

function updatePriceVirtualItemCount(result) {
    CruisePriceTableView.set_virtualItemCount(result);
}

function updateVisaVirtualItemCount(result) {
    CruiseVisaTableView.set_virtualItemCount(result);
}

function updateCruiseScheduleGrid(result) {

    CruiseScheduleTableView.set_dataSource(result);
    CruiseScheduleTableView.dataBind();
    if (result.length > 0) { CruiseScheduleTableView.selectItem(0); }
    if (CruiseScheduleCommandName == "Filter" || CruiseScheduleCommandName == "Load") { CRM.WebApp.webservice.CruiseMasterWebService.GetCruiseScheduleCount(updateScheduleVirtualItemCount); }

}

function updateCruisePrice(result) {
    CruisePriceTableView.set_dataSource(result);
    CruisePriceTableView.dataBind();
    if (result.length > 0) { CruisePriceTableView.selectItem(0); }
    if (CruisePriceCommandName == "Filter" || CruisePriceCommandName == "Load") { CRM.WebApp.webservice.CruiseMasterWebService.CruisepriceGrid(updatePriceVirtualItemCount); }
}

function updateCruiseVisa(result) {
    CruiseVisaTableView.set_dataSource(result);
    CruiseVisaTableView.dataBind();
    if (result.length > 0) { CruiseVisaTableView.selectItem(0); }
    if (CruiseVisaCommandName == "Filter" || CruiseVisaCommandName == "Load") { CRM.WebApp.webservice.CruiseMasterWebService.CruisevisaGrid(updateVisaVirtualItemCount); }
}