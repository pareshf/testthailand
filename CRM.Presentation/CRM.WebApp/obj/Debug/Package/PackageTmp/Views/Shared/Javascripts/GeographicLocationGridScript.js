var COUNTRY_ID = "";
var STATE_ID = "";
var CITY_ID = "";
var CountryTableView = null;
var CountryCommandName = "";
var StateTableView = null;
var StateCommandName = "";
var CityTableView = null;
var CityCommandName = "";

function radgridCountry_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    CountryTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.GeographicLocationWebService.GetCountryName(CountryTableView.get_currentPageIndex() * CountryTableView.get_pageSize(), CountryTableView.get_pageSize(), CountryTableView.get_sortExpressions().toString(), CountryTableView.get_filterExpressions().toDynamicLinq(), updateCountryName);
    CountryCommandName = args.get_commandName;

}
function radgridState_Command(sender, args) {

}
function radgridCity_Command(sender, args) {

}
function radgridCountry_RowSelected(sender, args) {

    try {
                COUNTRY_ID = args.get_gridDataItem()._dataItem.COUNTRY_ID;
                currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
                loadState();
        }
        catch (e) { }
}
function radgridState_RowSelected(sender, args) {

   STATE_ID = args.get_gridDataItem()._dataItem.STATE_ID;
    loadCity();
}
function radgridCity_RowSelected(sender, args) {
   
    CITY_ID = args.get_gridDataItem()._dataItem.CITY_ID;
}
function updateCountryVirtualCount(result) { CountryTableView.set_virtualItemCount(result); }

function updateCountryName(result) {

    CountryTableView.set_dataSource(result);
    CountryTableView.dataBind();
    if (result.length > 0) { CountryTableView.selectItem(0); }

    if (CountryCommandName == "Filter" || CountryCommandName == "Load") { CRM.WebApp.webservice.GeographicLocationWebService.GetCountryCount(updateCountryVirtualCount); }
}
function loadState()
{
    CRM.WebApp.webservice.GeographicLocationWebService.GetStateName(COUNTRY_ID,updateStateName)
}
function updateStateVirtualCount(result) { StateTableView.set_virtualItemCount(result); }
function updateStateName(result) {
    
    StateTableView.set_dataSource(result);
    StateTableView.dataBind();
    if (result.length > 0) { StateTableView.selectItem(0); STATE_ID = result[0]["STATE_ID"]; }
    else { STATE_ID = ""; }
    if (StateCommandName == "Filter" || StateCommandName == "Load") { CRM.WebApp.webservice.GeographicLocationWebService.GetStateCount(updateStateVirtualCount); }
}
function loadCity() {

    CRM.WebApp.webservice.GeographicLocationWebService.GetCityName(STATE_ID, updateCityGrid)
}
function updateCityVirtualCount(result) { CityTableView.set_virtualItemCount(result); }

function updateCityGrid(result) {
    
    CityTableView.set_dataSource(result);
    CityTableView.dataBind();
    if (result.length > 0) { CityTableView.selectItem(0); CITY_ID = result[0]["CITY_ID"]; }
    else { CITY_ID = ""; }
    if (CityCommandName == "Filter" || CityCommandName == "Load") { CRM.WebApp.webservice.GeographicLocationWebService.GetCityCount(updateCityVirtualCount); }
}

// Double Click Save

function addCountries(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    
    ary[0] = CountryTableView.get_dataItems()[currentRowIndex].findElement("COUNTRY_NAME").value;
    ary[2] = CountryTableView.get_dataItems()[currentRowIndex].findElement("COUNTRY_CODE").value;
    ary[3] = CountryTableView.get_dataItems()[currentRowIndex].findElement("COUNTRY_CURRENCY_SYMBOL").value;
    ary[4] = CountryTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    ary[5] = CountryTableView.get_dataItems()[currentRowIndex].findElement("CONTINENT_NAME").value;

    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.COUNTRY_ID;
    for (i = 0; i < 7; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.GeographicLocationWebService.InsertUpdateCountryName(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.GeographicLocationWebService.GetCountryName(0, CountryTableView.get_pageSize(), CountryTableView.get_sortExpressions().toString(), CountryTableView.get_filterExpressions().toDynamicLinq(), updateCountryName);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}

function addMyStates(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var arr = [];

    arr[0] = StateTableView.get_dataItems()[currentRowIndex].findElement("STATE_NAME").value;
    arr[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.STATE_ID;
    for (i = 0; i < 3; i++) {
        if (arr[i] == "" || arr[i] == 'null') arr[i] = 0;
    }
    try {
        CRM.WebApp.webservice.GeographicLocationWebService.InsertUpdateStateName(arr);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.GeographicLocationWebService.GetStateName(COUNTRY_ID, updateStateName)

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}

function addMyCity(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var a = [];

    a[0] = CityTableView.get_dataItems()[currentRowIndex].findElement("CITY_NAME").value;
    a[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CITY_ID;
    for (i = 0; i < 3; i++) {
        if (a[i] == "" || a[i] == 'null') a[i] = 0;
    }
    try {
        CRM.WebApp.webservice.GeographicLocationWebService.InsertUpdateCityName(a);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.GeographicLocationWebService.GetCityName(COUNTRY_ID, updateCityGrid)

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}