var MAR_ID = "";
var globalvalue = "";
var MarketingMaterialCommand = "";
var MarketingMaterialTableView = null;


function radgridMarketingmaster_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    MarketingMaterialTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.MarketingMaterialWebService.GetMarketingMaterail(MarketingMaterialTableView.get_currentPageIndex() * MarketingMaterialTableView.get_pageSize(), MarketingMaterialTableView.get_pageSize(), MarketingMaterialTableView.get_sortExpressions().toString(), MarketingMaterialTableView.get_filterExpressions().toDynamicLinq(), updateMarketingMaterialGrid);
    MarketingMaterialCommand = args.get_commandName;

}
function radgridMarketingmaster_RowSelected(sender, args) {

    try {
        
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        MAR_ID = args.get_gridDataItem()._dataItem.MAR_ID;
        
    }
    catch (e) { }
}
function updateMarketingVirtualunt(result){ MarketingMaterialTableView.set_virtualItemCount(result); }

function updateMarketingMaterialGrid(result) {

    MarketingMaterialTableView.set_dataSource(result);
    MarketingMaterialTableView.dataBind();
    if (result.length > 0) { MarketingMaterialTableView.selectItem(0); }

    if (MarketingMaterialCommand == "Filter" || MarketingMaterialCommand == "Load") { CRM.WebApp.webservice.MarketingMaterialWebService.GetMarketingCount(updateMarketingVirtualunt); }
}
/* double row Click save*/
function MarketingRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[0] = MarketingMaterialTableView.get_dataItems()[currentRowIndex].findElement("TOUR_SHORT_NAME").value;
    ary[2] = MarketingMaterialTableView.get_dataItems()[currentRowIndex].findElement("TYPE").value;
    ary[3] = MarketingMaterialTableView.get_dataItems()[currentRowIndex].findElement("EXPIRATION_DATE").value;
    ary[4] = MarketingMaterialTableView.get_dataItems()[currentRowIndex].findElement("DESCRIPTION").value;
    ary[5] = MarketingMaterialTableView.get_dataItems()[currentRowIndex].findElement("EMBEDCODE").value;
    ary[6] = MarketingMaterialTableView.get_dataItems()[currentRowIndex].findElement("WEBURL").value;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.MAR_ID;
    for (i = 0; i < 7; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.MarketingMaterialWebService.InsertUpdateMarketingMaterial(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.MarketingMaterialWebService.GetMarketingMaterail(MarketingMaterialTableView.get_currentPageIndex() * MarketingMaterialTableView.get_pageSize(), MarketingMaterialTableView.get_pageSize(), MarketingMaterialTableView.get_sortExpressions().toString(), MarketingMaterialTableView.get_filterExpressions().toDynamicLinq(), updateMarketingMaterialGrid);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}