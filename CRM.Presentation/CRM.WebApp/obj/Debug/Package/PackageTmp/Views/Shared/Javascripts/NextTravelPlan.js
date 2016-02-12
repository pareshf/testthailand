var SR_NO = "";
var TravelPlanTableView = null;
var TravelPlanCommand = "";
var CustomerTravelTableView = null;
var CustomerTravelCommand = "";
var TravelWithOtherTableView = null;
var TravelWithOtherCommand = "";
var AirlineDetailTableView = null;
var AirlineCommand = "";
var VisaDetailTableView = null;
var VisaCommand = "";

function cutomernexttravelplan_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    TravelPlanTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetNextTravelPlan(TravelPlanTableView.get_currentPageIndex() * TravelPlanTableView.get_pageSize(), TravelPlanTableView.get_pageSize(), TravelPlanTableView.get_sortExpressions().toString(), TravelPlanTableView.get_filterExpressions().toDynamicLinq(), updatetravelplan);
    TravelPlanCommand = args.get_commandName;

}
function cutomertravelhistory_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    CustomerTravelTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetTravelHistory(CustomerTravelTableView.get_currentPageIndex() * CustomerTravelTableView.get_pageSize(), CustomerTravelTableView.get_pageSize(), CustomerTravelTableView.get_sortExpressions().toString(), CustomerTravelTableView.get_filterExpressions().toDynamicLinq(), updateCustomerTravelWithUs);
    CustomerTravelCommand = args.get_commandName;
}
function cutomertravelhistorywithother_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    TravelWithOtherTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetTravelWithOther(TravelWithOtherTableView.get_currentPageIndex() * TravelWithOtherTableView.get_pageSize(), TravelWithOtherTableView.get_pageSize(), TravelWithOtherTableView.get_sortExpressions().toString(), TravelWithOtherTableView.get_filterExpressions().toDynamicLinq(), updateCustomerTravelWithOther);
    TravelWithOtherCommand = args.get_commandName;
}

function adgridairlinedetail_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    AirlineDetailTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetAirlineDetail(AirlineDetailTableView.get_currentPageIndex() * AirlineDetailTableView.get_pageSize(), AirlineDetailTableView.get_pageSize(), AirlineDetailTableView.get_sortExpressions().toString(), AirlineDetailTableView.get_filterExpressions().toDynamicLinq(), updateAirLineDetail);
    AirlineCommand = args.get_commandName;
}
function radgridvisadetail_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    VisaDetailTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetCustomerVisa(VisaDetailTableView.get_currentPageIndex() * VisaDetailTableView.get_pageSize(), VisaDetailTableView.get_pageSize(), VisaDetailTableView.get_sortExpressions().toString(), VisaDetailTableView.get_filterExpressions().toDynamicLinq(), updateVisaDetail);
    VisaCommand = args.get_commandName;
}
function radgridvisadetail_RowSelected(sender, args) {

    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        SR_NO = args.get_gridDataItem()._dataItem.SR_NO;

    }
    catch (e) { }
}
function adgridairlinedetail_RowSelected(sender, args) {

    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        SR_NO = args.get_gridDataItem()._dataItem.SR_NO;

    }
    catch (e) { }
}
function cutomertravelhistorywithother_RowSelected(sender, args) {

    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        SR_NO = args.get_gridDataItem()._dataItem.SR_NO;

    }
    catch (e) { }

}
function cutomertravelhistory_RowSelected(sender, args) {

    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        SR_NO = args.get_gridDataItem()._dataItem.SR_NO;

    }
    catch (e) { }
}
function cutomernexttravelplan_RowSelected(sender, args) {

    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        SR_NO = args.get_gridDataItem()._dataItem.SR_NO;

    }
    catch (e) { }
}
function updatetravelplanVirtualItemCount(result) {

    TravelPlanTableView.set_virtualItemCount(result);

}
function updatecustomertravelwithusVirtualItemCount(result) {

    CustomerTravelTableView.set_virtualItemCount(result);

}
function updatecustomertravelwithotherVirtualItemCount(result) {

    TravelWithOtherTableView.set_virtualItemCount(result);

}
function updateairlineVirtualItemCount(result) {

    AirlineDetailTableView.set_virtualItemCount(result);

}
function updateVisaVirtualItemCount(result) {

    VisaDetailTableView.set_virtualItemCount(result);

}
function updatetravelplan(result) {

    TravelPlanTableView.set_dataSource(result);
    TravelPlanTableView.dataBind();
    if (result.length > 0) { TravelPlanTableView.selectItem(0); }

    if (TravelPlanCommand == "Filter" || TravelPlanCommand == "Load") { CRM.WebApp.webservice.CustomerNextTravelPlanWebService.TravelPlanCount(updatetravelplanVirtualItemCount); }

}
function updateCustomerTravelWithUs(result) {

    CustomerTravelTableView.set_dataSource(result);
    CustomerTravelTableView.dataBind();
    if (result.length > 0) { CustomerTravelTableView.selectItem(0); }

    if (CustomerTravelCommand == "Filter" || CustomerTravelCommand == "Load") { CRM.WebApp.webservice.CustomerNextTravelPlanWebService.TravelHistoryCount(updatecustomertravelwithusVirtualItemCount); }

}
function updateCustomerTravelWithOther(result) {

    TravelWithOtherTableView.set_dataSource(result);
    TravelWithOtherTableView.dataBind();
    if (result.length > 0) { TravelWithOtherTableView.selectItem(0); }

    if (TravelWithOtherCommand == "Filter" || TravelWithOtherCommand == "Load") { CRM.WebApp.webservice.CustomerNextTravelPlanWebService.TravelWithOtherCount(updatecustomertravelwithotherVirtualItemCount); }
}
function updateAirLineDetail(result) {
    
    AirlineDetailTableView.set_dataSource(result);
    AirlineDetailTableView.dataBind();
    if (result.length > 0) { AirlineDetailTableView.selectItem(0); }

    if (AirlineCommand == "Filter" || AirlineCommand == "Load") { CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetAirlineCount(updateairlineVirtualItemCount); }
}
function updateVisaDetail(result) {
    
    VisaDetailTableView.set_dataSource(result);
    VisaDetailTableView.dataBind();
    if (result.length > 0) { VisaDetailTableView.selectItem(0); }

    if (VisaCommand == "Filter" || VisaCommand == "Load") { CRM.WebApp.webservice.CustomerNextTravelPlanWebService.CustomerVisaCount(updateVisaVirtualItemCount); }
}