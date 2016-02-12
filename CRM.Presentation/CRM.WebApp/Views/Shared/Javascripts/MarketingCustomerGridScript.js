var MAR_DATA_ID = "";
var searchparam = "";
var MarketingCustomerCommand = "";
var MarketingCustomerTableView = null;
var fname = "";
var lname = "";
var city = "";
var state = "";
var country = "";
var mobile = "";
var phone = "";
var cmode = "";
var ccompany = "";

function radgridMarketingCustomer_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    MarketingCustomerTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.MarketingCustomerWebService.GetMarketingCustomer(MarketingCustomerTableView.get_currentPageIndex() * MarketingCustomerTableView.get_pageSize(), MarketingCustomerTableView.get_pageSize(), MarketingCustomerTableView.get_sortExpressions().toString(), MarketingCustomerTableView.get_filterExpressions().toDynamicLinq(), fname, lname, city, state, country, mobile, phone, cmode, ccompany, updateMarketingCustomer);
    MarketingCustomerCommand = args.get_commandName;

}
function radgridMarketingCustomer_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        MAR_DATA_ID = args.get_gridDataItem()._dataItem.MAR_DATA_ID;
    }
    catch (e) { }
}
function updateMarketingCustomerVirtualCount(result) { MarketingCustomerTableView.set_virtualItemCount(result); }

function updateMarketingCustomer(result) {

    MarketingCustomerTableView.set_dataSource(result);
    MarketingCustomerTableView.dataBind();
    if (result.length > 0) { MarketingCustomerTableView.selectItem(0); }
   
    if (MarketingCustomerCommand == "Filter" || MarketingCustomerCommand == "Load") { CRM.WebApp.webservice.MarketingCustomerWebService.GetMarCustomerCount(updateMarketingCustomerVirtualCount); }
}
/* double Click Save Data*/

function MarketingCustomerRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();
    var ary = [];
   
    ary[0] = MarketingCustomerTableView.get_dataItems()[currentRowIndex].findElement("TITLE_DESC").value;
    ary[2] = MarketingCustomerTableView.get_dataItems()[currentRowIndex].findElement("MAR_DATA_NAME").value;
    ary[3] = MarketingCustomerTableView.get_dataItems()[currentRowIndex].findElement("MAR_DATA_SURNAME").value;
    ary[4] = MarketingCustomerTableView.get_dataItems()[currentRowIndex].findElement("ADDRESS_LINE_1").value;
    ary[5] = MarketingCustomerTableView.get_dataItems()[currentRowIndex].findElement("ADDRESS_LINE_2").value;
    ary[6] = MarketingCustomerTableView.get_dataItems()[currentRowIndex].findElement("MOBILE_NO").value;
    ary[7] = MarketingCustomerTableView.get_dataItems()[currentRowIndex].findElement("PHONE_NO").value;
    ary[8] = MarketingCustomerTableView.get_dataItems()[currentRowIndex].findElement("CITY_NAME").value;
    ary[9] = MarketingCustomerTableView.get_dataItems()[currentRowIndex].findElement("STATE_NAME").value;
    ary[10] = MarketingCustomerTableView.get_dataItems()[currentRowIndex].findElement("COUNTRY_NAME").value;
    ary[11] = MarketingCustomerTableView.get_dataItems()[currentRowIndex].findElement("COMMUNICATION_MODE_NAME").value;
    ary[12] = MarketingCustomerTableView.get_dataItems()[currentRowIndex].findElement("CUST_COMPANY_NAME").value;

    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.MAR_DATA_ID;
    for (i = 0; i < 13; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.MarketingCustomerWebService.InsertUpdateMarketingCustomer(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.MarketingCustomerWebService.GetMarketingCustomer(MarketingCustomerTableView.get_currentPageIndex() * MarketingCustomerTableView.get_pageSize(), MarketingCustomerTableView.get_pageSize(), MarketingCustomerTableView.get_sortExpressions().toString(), MarketingCustomerTableView.get_filterExpressions().toDynamicLinq(), fname, lname, city, state, country, mobile, phone, cmode, ccompany, updateMarketingCustomer);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}